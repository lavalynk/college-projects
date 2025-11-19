// ============================================================================
// Bedside Bistro API (Minimal API)
// ----------------------------------------------------------------------------
// Purpose: Hospital meal ordering system (menu, inventory, users, patients,
//          admissions, orders, diet profiles, uploads, and a help-chat endpoint).
//
// Important Notes for New Readers:
// - This is a .NET Minimal API. Endpoints are declared with app.MapGet/MapPost/etc.
// - SQL Server is accessed via Microsoft.Data.SqlClient using raw SQL / stored procs.
// - All routes and behavior below are intentionally preserved exactly as provided.
//   This file only reorganizes and documents the original implementation.
// - Security/robustness concerns (plaintext password, hardcoded API key, etc.) are
//   called out in comments but not changed per the request.
//
// Structure:
//   1) Usings
//   2) App/Builder Setup (config, CORS, Swagger, static files)
//   3) Diagnostics (health, DB check)
//   4) Menu endpoints (CRUD) + Menu Ingredients & Allergens
//   5) Ingredients + Inventory endpoints
//   6) Users CRUD + Auth
//   7) Patients + Admissions
//   8) Meal Periods (read-only)
//   9) Uploads (images, ping)
//  10) Help Chat endpoint (LLM proxy)
//  11) Orders (TVPs, place/list/get/cancel/status/timeline)
//  12) Profile API (patient demographics, diets, allergies)
//  13) App Run
//  14) DTOs / Records
// ============================================================================

#region 1) Usings

using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.OpenApi.Models;
using System.Text.RegularExpressions;   // (Currently unused; kept to avoid changing behavior)
using System.Text.Json;
using System.Net.Http.Json;

#endregion


// ============================================================================
// 2) App / Builder Setup
//    - Builds the app and registers platform features (CORS, Swagger).
//    - Sets up middleware (static files, HTTPS/HSTS, Swagger UI, CORS).
//    - Provides a health endpoint.
// ============================================================================
#region 2) App / Builder Setup

// Builder + configuration
var builder = WebApplication.CreateBuilder(args);
var cs = builder.Configuration.GetConnectionString("Default"); // Connection string "Default" must be present (appsettings.*)

// CORS policy
// - Allows local dev sites and your production domains.
// - Adjust as needed for your deployment and security posture.
builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
	p.WithOrigins(
		// local dev sites
		"http://localhost:5130",
		"https://localhost:7130",

		// local API if you open Swagger UI from it
		"http://localhost:5023",

		// your domain, both schemes in case you browse via http
		"http://bedsidebistro.com",
		"http://www.bedsidebistro.com",
		"https://bedsidebistro.com",
		"https://www.bedsidebistro.com"
	)
	.AllowAnyHeader()
	.AllowAnyMethod()
));

// Swagger/OpenAPI
// - Enables the interactive docs at /swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHostedService<DbWarmupService>();
builder.Services.AddMemoryCache();
builder.Services.AddSwaggerGen(o =>
{
	o.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "Bedside Bistro API",
		Version = "v1",
		Description = "API for managing hospital meal orders"
	});
});

var app = builder.Build();

// HTTPS / HSTS (enforce secure transport in non-dev)
if (!app.Environment.IsDevelopment())
{
	app.UseHsts();
}
app.UseHttpsRedirection();

// Static files + Swagger + CORS

app.UseDefaultFiles();                          // Serves index.html if present (wwwroot)
app.UseStaticFiles();                           // Serves /wwwroot
app.UseSwagger();                               // Exposes /swagger/v1/swagger.json
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bedside Bistro API v1"));
app.UseCors();                                  // Applies the default CORS policy
app.UseRouting();


// Simple health check (infra probing)
app.MapGet("/health", () => Results.Ok(new { ok = true }));
app.Use(async (ctx, next) =>
{
	await next();

	if (ctx.Response.StatusCode == StatusCodes.Status401Unauthorized)
	{
		// nuke any challenge header that would trigger the browser modal
		ctx.Response.Headers.Remove("WWW-Authenticate");
		if (!ctx.Response.HasStarted)
			ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
	}
});

//builder.Services.ConfigureHttpJsonOptions(o =>
//{
//  o.SerializerOptions.PropertyNamingPolicy = null; // keep PascalCase if preferred
//});

#endregion


// ============================================================================
// 3) Diagnostics
//    - DB connection test for quick verification of SQL connectivity.
// ============================================================================
#region 3) Diagnostics

// GET /dbcheck
// Quick connectivity test: opens SQL connection and counts menu items.
app.MapGet("/dbcheck", async () =>
{
	try
	{
		await using var conn = new SqlConnection(cs);
		await conn.OpenAsync();
		await using var cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.TMenuItems", conn);
		var count = (int)await cmd.ExecuteScalarAsync();
		return Results.Ok(new { connected = true, itemCount = count });
	}
	catch (Exception ex)
	{
		return Results.Problem($"Database connection failed: {ex.Message}");
	}
});

#endregion


// ============================================================================
// 4) Menu - List / Create / Read / Update / Delete
//    - Core menu management endpoints.
//    - NOTE: Business logic and SQL remain unchanged, only commented/grouped.
// ============================================================================
#region 4) Menu - List / Create / Read / Update / Delete

// Lists active menu items (bitIsActive = 1), projecting fields to a simple payload.
// GET /api/menu[?mrn=MRN001]
app.MapGet("/api/menu", async (HttpRequest req) =>
{
	string? mrn = string.IsNullOrWhiteSpace(req.Query["mrn"])
		? null : req.Query["mrn"].ToString();

	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();

	var sql = @"
SELECT
    mi.intMenuItemID,
    mi.strItemName,
    mi.strCategory,
    mi.decCalories,
    mi.decSodiumMG,
    mi.decProteinG,
    mi.decCarbsG,
    mi.decFatG,
    mi.bitLowSodium,
    mi.strImagePath,
    mi.bitIsActive,
    COALESCE(al.AllergenNames, '') AS AllergenNames,
    CASE 
        WHEN @mrn IS NULL THEN CAST(0 AS bit)
        WHEN EXISTS (
            SELECT 1
            FROM dbo.TMenuItemAllergens mia
            JOIN dbo.TPatientAllergies pa ON pa.intAllergenID = mia.intAllergenID
            JOIN dbo.TPatients p ON p.intPatientID = pa.intPatientID
            WHERE mia.intMenuItemID = mi.intMenuItemID
              AND UPPER(LTRIM(RTRIM(p.strMRN))) = UPPER(LTRIM(RTRIM(@mrn)))
        ) THEN CAST(1 AS bit)
        ELSE CAST(0 AS bit)
    END AS HasConflict
FROM dbo.TMenuItems mi
LEFT JOIN (
    SELECT mia.intMenuItemID,
           STRING_AGG(a.strAllergen, ', ') 
             WITHIN GROUP (ORDER BY a.strAllergen) AS AllergenNames
    FROM dbo.TMenuItemAllergens mia
    JOIN dbo.TAllergens a ON a.intAllergenID = mia.intAllergenID
    GROUP BY mia.intMenuItemID
) al ON al.intMenuItemID = mi.intMenuItemID
WHERE mi.bitIsActive = 1
ORDER BY mi.strItemName;";

	await using var cmd = new SqlCommand(sql, conn);
	cmd.Parameters.AddWithValue("@mrn", (object?)mrn ?? DBNull.Value);

	await using var rdr = await cmd.ExecuteReaderAsync();
	var list = new List<object>();
	while (await rdr.ReadAsync())
	{
		list.Add(new
		{
			id = rdr.GetInt32(0),
			name = rdr.GetString(1),
			category = rdr.GetString(2),
			calories = rdr.IsDBNull(3) ? (decimal?)null : rdr.GetDecimal(3),
			sodiumMg = rdr.IsDBNull(4) ? (decimal?)null : rdr.GetDecimal(4),
			proteinG = rdr.IsDBNull(5) ? (decimal?)null : rdr.GetDecimal(5),
			carbsG = rdr.IsDBNull(6) ? (decimal?)null : rdr.GetDecimal(6),
			fatG = rdr.IsDBNull(7) ? (decimal?)null : rdr.GetDecimal(7),
			lowSodium = rdr.GetBoolean(8),
			imagePath = rdr.IsDBNull(9) ? null : rdr.GetString(9),
			isActive = rdr.GetBoolean(10),
			allergenNames = rdr.IsDBNull(11) ? "" : rdr.GetString(11),
			hasConflict = !rdr.IsDBNull(12) && rdr.GetBoolean(12)
		});
	}
	return Results.Ok(list);
});



// POST /api/menu
// Creates a menu item via dbo.usp_AddMenuItem (stored procedure).
// Returns conflict/bad request based on SP return codes (unchanged).
app.MapPost("/api/menu", async (MenuItemDto dto, IConfiguration cfg) =>
{
	if (string.IsNullOrWhiteSpace(dto.ItemName))
		return Results.BadRequest(new { message = "ItemName is required." });

	if (dto.MealPeriodID is null)
		return Results.BadRequest(new { message = "MealPeriodID is required." });

	try
	{
		await using var cn = new SqlConnection(cfg.GetConnectionString("Default"));
		await cn.OpenAsync();

		using var cmd = new SqlCommand("dbo.usp_AddMenuItem", cn)
		{ CommandType = CommandType.StoredProcedure };

		cmd.Parameters.Add(new SqlParameter("@strItemName", SqlDbType.VarChar, 120) { Value = dto.ItemName });
		cmd.Parameters.Add(new SqlParameter("@strCategory", SqlDbType.VarChar, 40) { Value = (object?)dto.Category ?? DBNull.Value });
		cmd.Parameters.Add(new SqlParameter("@intMealPeriodID", SqlDbType.Int) { Value = (object?)dto.MealPeriodID ?? DBNull.Value });

		SqlParameter P(string name, decimal? v) =>
			new(name, SqlDbType.Decimal) { Precision = 10, Scale = 2, Value = (object?)v ?? DBNull.Value };

		cmd.Parameters.Add(P("@decCalories", dto.Calories));
		cmd.Parameters.Add(P("@decSodiumMG", dto.SodiumMG));
		cmd.Parameters.Add(P("@decProteinG", dto.ProteinG));
		cmd.Parameters.Add(P("@decCarbsG", dto.CarbsG));
		cmd.Parameters.Add(P("@decFatG", dto.FatG));

		cmd.Parameters.Add(new SqlParameter("@bitLowSodium", SqlDbType.Bit) { Value = dto.LowSodium });
		cmd.Parameters.Add(new SqlParameter("@bitIsActive", SqlDbType.Bit) { Value = dto.IsActive ?? true });
		cmd.Parameters.Add(new SqlParameter("@strImagePath", SqlDbType.VarChar, 255) { Value = (object?)dto.ImagePath ?? DBNull.Value });
		cmd.Parameters.Add(new SqlParameter("@strCreatedBy", SqlDbType.VarChar, 100) { Value = (object?)dto.CreatedBy ?? DBNull.Value });

		var outId = new SqlParameter("@NewMenuItemID", SqlDbType.Int) { Direction = ParameterDirection.Output };
		cmd.Parameters.Add(outId);

		var ret = new SqlParameter { ParameterName = "@RETURN_VALUE", Direction = ParameterDirection.ReturnValue };
		cmd.Parameters.Add(ret);

		await cmd.ExecuteNonQueryAsync();

		var retCode = (ret.Value is int i) ? i : 0;
		if (retCode == -1) return Results.Conflict(new { message = $"Menu item '{dto.ItemName}' already exists." });
		if (retCode == -2) return Results.BadRequest(new { message = "Invalid Meal Period ID." });

		var newId = (outId.Value is int id) ? id : 0;
		return Results.Ok(new { id = newId });
	}
	catch (SqlException ex)
	{
		return Results.Problem(statusCode: 500, detail: $"Failed to create menu item: {ex.Message}");
	}
});

// GET /api/menu/{id}
// Loads a single menu item including its ingredient IDs (for UI binding).
app.MapGet("/api/menu/{id:int}", async (int id) =>
{
	try
	{
		await using var conn = new SqlConnection(cs);
		await conn.OpenAsync();

		const string sqlItem = @"
            SELECT intMenuItemID, strItemName, strCategory, intMealPeriodID,
                   decCalories, decSodiumMG, decProteinG, decCarbsG, decFatG,
                   bitLowSodium, bitIsActive, strImagePath
            FROM dbo.TMenuItems
            WHERE intMenuItemID = @id;";

		await using var cmd = new SqlCommand(sqlItem, conn);
		cmd.Parameters.AddWithValue("@id", id);

		await using var rdr = await cmd.ExecuteReaderAsync();
		if (!await rdr.ReadAsync())
			return Results.NotFound(new { error = "Menu item not found." });

		int itemId = rdr.GetInt32(0);
		string? name = rdr.IsDBNull(1) ? null : rdr.GetString(1);
		string? category = rdr.IsDBNull(2) ? null : rdr.GetString(2);
		int? mealPeriodId = rdr.IsDBNull(3) ? (int?)null : rdr.GetInt32(3);
		decimal? calories = rdr.IsDBNull(4) ? (decimal?)null : rdr.GetDecimal(4);
		decimal? sodiumMg = rdr.IsDBNull(5) ? (decimal?)null : rdr.GetDecimal(5);
		decimal? proteinG = rdr.IsDBNull(6) ? (decimal?)null : rdr.GetDecimal(6);
		decimal? carbsG = rdr.IsDBNull(7) ? (decimal?)null : rdr.GetDecimal(7);
		decimal? fatG = rdr.IsDBNull(8) ? (decimal?)null : rdr.GetDecimal(8);
		bool lowSodium = !rdr.IsDBNull(9) && rdr.GetBoolean(9);
		bool isActive = !rdr.IsDBNull(10) && rdr.GetBoolean(10);
		string? imagePath = rdr.IsDBNull(11) ? null : rdr.GetString(11);

		await rdr.CloseAsync();

		const string sqlIngr = @"
            SELECT intIngredientID
            FROM dbo.TMenuItemIngredients
            WHERE intMenuItemID = @id
            ORDER BY intIngredientID;";

		var ingredientIds = new List<int>();
		await using (var cmd2 = new SqlCommand(sqlIngr, conn))
		{
			cmd2.Parameters.AddWithValue("@id", id);
			await using var rdr2 = await cmd2.ExecuteReaderAsync();
			while (await rdr2.ReadAsync())
				ingredientIds.Add(rdr2.GetInt32(0));
		}

		return Results.Ok(new
		{
			id = itemId,
			name,
			category,
			mealPeriodId,
			calories,
			sodiumMg,
			proteinG,
			carbsG,
			fatG,
			lowSodium,
			isActive,
			imagePath,
			ingredientIds
		});
	}
	catch (Exception ex)
	{
		return Results.Problem($"Failed to load menu item: {ex.Message}");
	}
});

// PUT /api/menu/{id}
// Updates a menu item (direct SQL). Prototype code preserves original field names/types.
// NOTE: 'UpdatedBy' is sourced from dto.CreatedBy in original code; left as-is.
app.MapPut("/api/menu/{id:int}", async (int id, MenuItemUpdateDto dto) =>
{
	await using var conn = new Microsoft.Data.SqlClient.SqlConnection(cs);
	await conn.OpenAsync();

	await using var cmd = new Microsoft.Data.SqlClient.SqlCommand(@"
UPDATE dbo.TMenuItems
SET
    strItemName     = @ItemName,
    strCategory     = @Category,
    intMealPeriodID = @MealPeriodID,
    decCalories     = @Calories,
    decSodiumMG     = @SodiumMG,
    decProteinG     = @ProteinG,
    decCarbsG       = @CarbsG,
    decFatG         = @FatG,
    bitLowSodium    = @LowSodium,
    bitIsActive     = @IsActive,
    strImagePath    = @ImagePath,
    dtmUpdatedOn    = SYSUTCDATETIME(),
    strUpdatedBy    = COALESCE(@UpdatedBy, 'system')
WHERE intMenuItemID = @Id;", conn);

	cmd.Parameters.AddWithValue("@Id", id);
	cmd.Parameters.AddWithValue("@ItemName", dto.ItemName);
	cmd.Parameters.AddWithValue("@Category", (object?)dto.Category ?? DBNull.Value);
	cmd.Parameters.AddWithValue("@MealPeriodID", dto.MealPeriodID);
	cmd.Parameters.AddWithValue("@Calories", dto.Calories);
	cmd.Parameters.AddWithValue("@SodiumMG", dto.SodiumMG);
	cmd.Parameters.AddWithValue("@ProteinG", dto.ProteinG);
	cmd.Parameters.AddWithValue("@CarbsG", dto.CarbsG);
	cmd.Parameters.AddWithValue("@FatG", dto.FatG);
	cmd.Parameters.AddWithValue("@LowSodium", dto.LowSodium);
	cmd.Parameters.AddWithValue("@IsActive", dto.IsActive);
	cmd.Parameters.AddWithValue("@ImagePath", (object?)dto.ImagePath ?? DBNull.Value);
	cmd.Parameters.AddWithValue("@UpdatedBy", (object?)dto.CreatedBy ?? DBNull.Value);

	var rows = await cmd.ExecuteNonQueryAsync();
	return rows == 0 ? Results.NotFound(new { error = $"Menu item {id} not found" })
					 : Results.NoContent();
})
.Accepts<MenuItemUpdateDto>("application/json")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.Produces(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status500InternalServerError);

// DELETE /api/menu/{id}
// Transactionally removes a menu item and its dependency links.
app.MapDelete("/api/menu/{id:int}", async (int id) =>
{
	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();
	await using var tx = await conn.BeginTransactionAsync();

	try
	{
		var sql = @"
DELETE FROM dbo.TMenuItemAllergens   WHERE intMenuItemID = @Id;
DELETE FROM dbo.TMenuItemIngredients WHERE intMenuItemID = @Id;
DELETE FROM dbo.TMenuItems           WHERE intMenuItemID = @Id;";

		await using var cmd = new SqlCommand(sql, conn, (SqlTransaction)tx);
		cmd.Parameters.AddWithValue("@Id", id);
		var affected = await cmd.ExecuteNonQueryAsync();

		if (affected == 0) { await tx.RollbackAsync(); return Results.NotFound(new { error = $"Menu item {id} not found" }); }
		await tx.CommitAsync();
		return Results.NoContent();
	}
	catch (Exception ex)
	{
		await tx.RollbackAsync();
		return Results.Problem(ex.Message);
	}
})
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.Produces(StatusCodes.Status500InternalServerError);

// GET /api/menu/sp?mrn=MRN001
app.MapGet("/api/menu/sp", async (HttpRequest req) =>
{
	var mrn = req.Query["mrn"].ToString();

	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();

	using var cmd = new SqlCommand("dbo.usp_Menu_ListWithAllergens", conn)
	{ CommandType = CommandType.StoredProcedure };
	cmd.Parameters.AddWithValue("@MRN", string.IsNullOrWhiteSpace(mrn) ? (object)DBNull.Value : mrn);

	var rows = new List<object>();
	await using var rdr = await cmd.ExecuteReaderAsync();
	while (await rdr.ReadAsync())
	{
		rows.Add(new
		{
			id = rdr["id"],
			name = rdr["name"],
			category = rdr["category"],
			calories = rdr["calories"],
			sodiumMg = rdr["sodiumMg"],
			proteinG = rdr["proteinG"],
			carbsG = rdr["carbsG"],
			fatG = rdr["fatG"],
			lowSodium = rdr["lowSodium"],
			imagePath = rdr["imagePath"],
			isActive = rdr["isActive"],
			allergenNames = rdr["allergenNames"] ?? "",
			hasConflict = rdr["hasConflict"] ?? false
		});
	}
	return Results.Ok(rows);
});

#endregion


// ============================================================================
// 4b) Menu - Ingredients + Allergens (per menu item)
//    - Helpers to create allergens, attach allergens/ingredients to items.
// ============================================================================
#region 4b) Menu - Ingredients + Allergens

// POST /api/menu/{id}/allergens
// Creates an allergen (usp_CreateAllergen) then links it to a menu item
// (usp_AddAllergenToMenuItem). Surface 50000 error messages as BadRequest.
app.MapPost("/api/menu/{id}/allergens", async (int id, AllergenDto dto) =>
{
	try
	{
		await using var conn = new SqlConnection(cs);
		await conn.OpenAsync();
		int newAllergenId;
		await using (var cmd = new SqlCommand("dbo.usp_CreateAllergen", conn) { CommandType = CommandType.StoredProcedure })
		{
			cmd.Parameters.AddWithValue("@AllergenName", dto.AllergenName);
			var newAllergenIdParam = new SqlParameter("@NewAllergenID", SqlDbType.Int) { Direction = ParameterDirection.Output };
			cmd.Parameters.Add(newAllergenIdParam);
			await cmd.ExecuteNonQueryAsync();
			newAllergenId = (int)newAllergenIdParam.Value;
		}
		await using (var cmd = new SqlCommand("dbo.usp_AddAllergenToMenuItem", conn) { CommandType = CommandType.StoredProcedure })
		{
			cmd.Parameters.AddWithValue("@MenuItemID", id);
			cmd.Parameters.AddWithValue("@AllergenID", newAllergenId);
			var rowsAffectedParam = new SqlParameter("@RowsAffected", SqlDbType.Int) { Direction = ParameterDirection.Output };
			cmd.Parameters.Add(rowsAffectedParam);
			await cmd.ExecuteNonQueryAsync();
		}
		return Results.Ok(new { message = "Allergen added successfully." });
	}
	catch (SqlException ex) when (ex.Number == 50000)
	{
		return Results.BadRequest(new { error = ex.Message });
	}
	catch (Exception ex)
	{
		return Results.Problem($"Failed to add allergen: {ex.Message}");
	}
});

// POST /api/menu/{id}/ingredients
// Replaces menu-item ingredient links with up to 6 distinct ingredient IDs.
// The operation is transactional.
app.MapPost("/api/menu/{id:int}/ingredients", async (int id, MenuIngrAttachDto dto) =>
{
	if (dto is null || dto.IngredientIds is null) return Results.BadRequest(new { error = "IngredientIds required." });
	if (dto.IngredientIds.Length > 6) return Results.BadRequest(new { error = "Maximum 6 ingredients allowed." });

	try
	{
		await using var conn = new SqlConnection(cs);
		await conn.OpenAsync();
		await using var tx = await conn.BeginTransactionAsync();

		var del = new SqlCommand("DELETE FROM dbo.TMenuItemIngredients WHERE intMenuItemID = @id;", conn, (SqlTransaction)tx);
		del.Parameters.AddWithValue("@id", id);
		await del.ExecuteNonQueryAsync();

		var distinct = dto.IngredientIds.Distinct().Take(6).ToArray();
		foreach (var ingId in distinct)
		{
			var ins = new SqlCommand(@"
                INSERT INTO dbo.TMenuItemIngredients (intMenuItemID, intIngredientID)
                VALUES (@m, @i);", conn, (SqlTransaction)tx);
			ins.Parameters.AddWithValue("@m", id);
			ins.Parameters.AddWithValue("@i", ingId);
			await ins.ExecuteNonQueryAsync();
		}

		await tx.CommitAsync();
		return Results.NoContent();
	}
	catch (SqlException ex) when (ex.Number == 50000)
	{
		return Results.BadRequest(new { error = ex.Message });
	}
	catch (Exception ex)
	{
		return Results.Problem($"Failed to attach ingredients: {ex.Message}");
	}
});


#endregion


// ============================================================================
// 5) Ingredients CRUD + Inventory
//    - Ingredient dictionary + stock (inventory) operations.
// ============================================================================
#region 5) Ingredients CRUD + Inventory

// GET /api/ingredients
// Returns the catalog of ingredients (id, name, qty).
app.MapGet("/api/ingredients", async () =>
{
	var list = new List<object>();
	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();

	const string sql = @"SELECT intIngredientID, strIngredient, intQty FROM dbo.TIngredients ORDER BY strIngredient;";
	await using var cmd = new SqlCommand(sql, conn);
	await using var rdr = await cmd.ExecuteReaderAsync();
	while (await rdr.ReadAsync())
	{
		list.Add(new
		{
			Id = rdr.GetInt32(0),
			Name = rdr.GetString(1),
			Quantity = rdr.IsDBNull(2) ? 0 : rdr.GetInt32(2)
		});
	}
	return Results.Ok(list);
});

// POST /api/ingredients
// Creates a new ingredient with an initial quantity (default 0).
app.MapPost("/api/ingredients", async (IngredientCreateDto dto) =>
{
	if (string.IsNullOrWhiteSpace(dto.Name)) return Results.BadRequest(new { error = "Name required" });
	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();
	const string sql = @"INSERT INTO dbo.TIngredients (strIngredient, intQty) VALUES (@n, @q); SELECT SCOPE_IDENTITY();";
	await using var cmd = new SqlCommand(sql, conn);
	cmd.Parameters.AddWithValue("@n", dto.Name.Trim());
	cmd.Parameters.AddWithValue("@q", (object)dto.Quantity ?? 0);
	var idObj = await cmd.ExecuteScalarAsync();
	var id = Convert.ToInt32(idObj);
	return Results.Created($"/api/ingredients/{id}", new { Id = id });
});

// PUT /api/ingredients/{id}
// Renames an ingredient (only name change).
app.MapPut("/api/ingredients/{id}", async (int id, IngredientRenameDto dto) =>
{
	if (string.IsNullOrWhiteSpace(dto.Name)) return Results.BadRequest(new { error = "Name required" });
	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();
	const string sql = @"UPDATE dbo.TIngredients SET strIngredient = @n WHERE intIngredientID = @id;";
	await using var cmd = new SqlCommand(sql, conn);
	cmd.Parameters.AddWithValue("@n", dto.Name.Trim());
	cmd.Parameters.AddWithValue("@id", id);
	var rows = await cmd.ExecuteNonQueryAsync();
	return rows == 0 ? Results.NotFound(new { error = "Ingredient not found" }) : Results.NoContent();
});

// DELETE /api/ingredients/{id}
// Removes an ingredient from the dictionary.
app.MapDelete("/api/ingredients/{id}", async (int id) =>
{
	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();
	const string sql = @"DELETE FROM dbo.TIngredients WHERE intIngredientID = @id;";
	await using var cmd = new SqlCommand(sql, conn);
	cmd.Parameters.AddWithValue("@id", id);
	var rows = await cmd.ExecuteNonQueryAsync();
	return rows == 0 ? Results.NotFound(new { error = "Ingredient not found" }) : Results.NoContent();
});

// GET /api/inventory?activeOnly={bool}
// Lists inventory (optionally only active). Output is normalized for UI convenience.
app.MapGet("/api/inventory", async (HttpRequest req) =>
{
	var activeOnly = bool.TryParse(req.Query["activeOnly"], out var b) ? b : false;

	var rows = new List<object>();
	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();

	var sql = @"
        SELECT intIngredientID, strIngredient, intQty, bitIsActive
        FROM dbo.TIngredients
        /**where**/
        ORDER BY strIngredient;
    ";

	if (activeOnly)
		sql = sql.Replace("/**where**/", "WHERE bitIsActive = 1");
	else
		sql = sql.Replace("/**where**/", "");

	await using (var cmd = new SqlCommand(sql, conn))
	await using (var rdr = await cmd.ExecuteReaderAsync())
	{
		while (await rdr.ReadAsync())
		{
			rows.Add(new
			{
				Id = rdr["intIngredientID"] as int? ?? Convert.ToInt32(rdr["intIngredientID"]),
				Name = rdr["strIngredient"] as string ?? "",
				Quantity = rdr["intQty"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["intQty"]),
				IsActive = rdr["bitIsActive"] == DBNull.Value ? true : Convert.ToBoolean(rdr["bitIsActive"])
			});
		}
	}

	return Results.Ok(rows);
})
.WithName("GetInventory")
.Produces<List<object>>(StatusCodes.Status200OK);

// PUT /api/inventory/{id}
// Updates a single inventory quantity. Rejects negative quantities.
app.MapPut("/api/inventory/{id:int}", async (int id, InventoryUpdate payload) =>
{
	if (payload is null) return Results.BadRequest(new { message = "Body required." });
	if (payload.Quantity < 0) return Results.BadRequest(new { message = "Quantity must be ≥ 0." });

	var sql = @"
        UPDATE dbo.TIngredients
           SET intQty = @qty
         WHERE intIngredientID = @id;
        SELECT @@ROWCOUNT;
    ";

	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();
	await using var cmd = new SqlCommand(sql, conn);
	cmd.Parameters.AddWithValue("@qty", payload.Quantity);
	cmd.Parameters.AddWithValue("@id", id);

	var affected = Convert.ToInt32(await cmd.ExecuteScalarAsync());
	if (affected == 0) return Results.NotFound(new { message = $"Ingredient {id} not found." });

	return Results.NoContent();
})
.WithName("UpdateInventoryOne")
.Accepts<InventoryUpdate>("application/json")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status404NotFound);

// PUT /api/inventory
// Bulk update endpoint: accepts an array of { id, quantity }. Validates each.
app.MapPut("/api/inventory", async (List<InventoryPatch> patches) =>
{
	if (patches == null || patches.Count == 0)
		return Results.BadRequest(new { message = "Provide an array of { id, quantity }." });

	foreach (var p in patches)
	{
		if (p.Id <= 0) return Results.BadRequest(new { message = "Each item must include a valid id." });
		if (p.Quantity < 0) return Results.BadRequest(new { message = "Quantities must be ≥ 0." });
	}

	var updated = 0;
	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();

	var sql = @"
        UPDATE dbo.TIngredients
           SET intQty = @qty
         WHERE intIngredientID = @id;
        SELECT @@ROWCOUNT;
    ";
	await using var cmd = new SqlCommand(sql, conn);
	var pQty = cmd.Parameters.Add("@qty", System.Data.SqlDbType.Int);
	var pId = cmd.Parameters.Add("@id", System.Data.SqlDbType.Int);

	foreach (var p in patches)
	{
		pQty.Value = p.Quantity;
		pId.Value = p.Id;
		updated += Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}

	return Results.Ok(new { updated, total = patches.Count });
})
.WithName("UpdateInventoryBulk")
.Accepts<List<InventoryPatch>>("application/json")
.Produces(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest);


#endregion


// ============================================================================
// 6) Users CRUD + Auth
//    - Basic admin/staff user management and a simple login.
//    - NOTE: Prototype stores plaintext passwords (do not use in production).
// ============================================================================
#region 6) Users CRUD + Auth

// GET /api/users
// Lists users with role/active flags for admin UI tables.
app.MapGet("/api/users", async () =>
{
	try
	{
		await using var conn = new SqlConnection(cs);
		await conn.OpenAsync();

		const string sql = @"
            SELECT intUserID, strUserName, strDisplayName, strRole, bitIsActive
            FROM dbo.TUsers
            ORDER BY strUserName;";

		await using var cmd = new SqlCommand(sql, conn);
		await using var rdr = await cmd.ExecuteReaderAsync();

		var list = new List<object>();
		while (await rdr.ReadAsync())
		{
			list.Add(new
			{
				Id = rdr.GetInt32(0),
				UserName = rdr.GetString(1),
				DisplayName = rdr.GetString(2),
				Role = rdr.GetString(3),
				IsActive = rdr.GetBoolean(4)
			});
		}

		return Results.Ok(list);
	}
	catch (Exception ex)
	{
		return Results.Problem($"Failed to retrieve users: {ex.Message}");
	}
});

// POST /api/users
// Creates a user via dbo.usp_CreateUser (prototype: plaintext password).
app.MapPost("/api/users", async (UserDto dto) =>
{
	try
	{
		await using var conn = new SqlConnection(cs);
		await conn.OpenAsync();
		await using var cmd = new SqlCommand("dbo.usp_CreateUser", conn) { CommandType = CommandType.StoredProcedure };
		cmd.Parameters.AddWithValue("@UserName", dto.UserName);
		cmd.Parameters.AddWithValue("@DisplayName", dto.DisplayName);
		cmd.Parameters.AddWithValue("@Role", dto.Role);
		cmd.Parameters.AddWithValue("@Password", dto.Password);
		cmd.Parameters.AddWithValue("@IsActive", dto.IsActive);
		var newUserIdParam = new SqlParameter("@NewUserID", SqlDbType.Int) { Direction = ParameterDirection.Output };
		cmd.Parameters.Add(newUserIdParam);
		await cmd.ExecuteNonQueryAsync();
		int newUserId = (int)newUserIdParam.Value;
		return Results.Created($"/api/users/{newUserId}", new { Id = newUserId });
	}
	catch (SqlException ex) when (ex.Number == 50000)
	{
		return Results.BadRequest(new { error = ex.Message });
	}
	catch (Exception ex)
	{
		return Results.Problem($"Failed to create user: {ex.Message}");
	}
});

// PUT /api/users/{id}
// Updates user fields using dbo.usp_UpdateUser. Uses nullable fields for partial updates.
app.MapPut("/api/users/{id}", async (int id, UserUpdateDto dto) =>
{
	try
	{
		await using var conn = new SqlConnection(cs);
		await conn.OpenAsync();
		await using var cmd = new SqlCommand("dbo.usp_UpdateUser", conn) { CommandType = CommandType.StoredProcedure };
		cmd.Parameters.AddWithValue("@UserID", id);
		cmd.Parameters.AddWithValue("@UserName", (object)dto.UserName ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@DisplayName", (object)dto.DisplayName ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@Role", (object)dto.Role ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@Password", (object)dto.Password ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@IsActive", (object)dto.IsActive ?? DBNull.Value);
		var rowsAffectedParam = new SqlParameter("@RowsAffected", SqlDbType.Int) { Direction = ParameterDirection.Output };
		cmd.Parameters.Add(rowsAffectedParam);
		await cmd.ExecuteNonQueryAsync();
		int rowsAffected = (int)rowsAffectedParam.Value;
		if (rowsAffected == 0) return Results.NotFound(new { error = "User not found or no changes made." });
		return Results.NoContent();
	}
	catch (SqlException ex) when (ex.Number == 50000)
	{
		return Results.BadRequest(new { error = ex.Message });
	}
	catch (Exception ex)
	{
		return Results.Problem($"Failed to update user: {ex.Message}");
	}
});

// GET /api/users/{id}/edit
// Loads user including plaintext password (prototype only) for an edit form.
app.MapGet("/api/users/{id:int}/edit", async (int id) =>
{
	try
	{
		await using var conn = new SqlConnection(cs);
		await conn.OpenAsync();

		const string sql = @"
            SELECT intUserID, strUserName, strDisplayName, strRole, bitIsActive, strPassword
            FROM dbo.TUsers
            WHERE intUserID = @id;";

		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@id", id);

		await using var rdr = await cmd.ExecuteReaderAsync();
		if (!await rdr.ReadAsync()) return Results.NotFound();

		var user = new
		{
			Id = rdr.GetInt32(0),
			UserName = rdr.GetString(1),
			DisplayName = rdr.GetString(2),
			Role = rdr.GetString(3),
			IsActive = rdr.GetBoolean(4),
			Password = rdr.GetString(5) // NOTE: plaintext only for prototype
		};

		return Results.Ok(user);
	}
	catch (Exception ex)
	{
		return Results.Problem($"Failed to load user: {ex.Message}");
	}
});

// DELETE /api/users/{id}?softDelete=true|false
// Soft delete by default via dbo.usp_DeleteUser (configurable soft/hard).
app.MapDelete("/api/users/{id:int}", async (int id, bool softDelete = true) =>
{
	try
	{
		await using var conn = new SqlConnection(cs);
		await conn.OpenAsync();

		await using var cmd = new SqlCommand("dbo.usp_DeleteUser", conn)
		{
			CommandType = CommandType.StoredProcedure
		};
		cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = id });
		cmd.Parameters.Add(new SqlParameter("@SoftDelete", SqlDbType.Bit) { Value = softDelete });

		var rowsAffectedParam = new SqlParameter("@RowsAffected", SqlDbType.Int)
		{
			Direction = ParameterDirection.Output
		};
		cmd.Parameters.Add(rowsAffectedParam);

		await cmd.ExecuteNonQueryAsync();

		var rowsAffectedObj = rowsAffectedParam.Value;
		var rowsAffected = (rowsAffectedObj is DBNull or null) ? 0 : Convert.ToInt32(rowsAffectedObj);

		if (rowsAffected == 0)
			return Results.NotFound(new { error = "User not found." });

		return Results.NoContent();
	}
	catch (SqlException ex) when (ex.Number == 50000)
	{
		return Results.BadRequest(new { error = ex.Message });
	}
	catch (Exception ex)
	{
		return Results.Problem($"Failed to delete user: {ex.Message}");
	}
});

// POST /api/users/login
// Simple login: matches username + plaintext password on active users.
app.MapPost("/api/users/login", async (HttpContext http, LoginDto dto) =>
{
	if (string.IsNullOrWhiteSpace(dto.UserName) || string.IsNullOrWhiteSpace(dto.Password))
	{
		http.Response.Headers.Remove("WWW-Authenticate");
		return Results.BadRequest(new { message = "Username and password are required." });
	}

	try
	{
		await using var conn = new SqlConnection(cs);
		await conn.OpenAsync();

		const string sqlUser = @"
            SELECT TOP (1) intUserID, strUserName, strDisplayName, strRole, strPassword, bitIsActive
            FROM dbo.TUsers
            WHERE strUserName = @u;";

		await using var cmd = new SqlCommand(sqlUser, conn);
		cmd.Parameters.Add(new SqlParameter("@u", SqlDbType.NVarChar, 100) { Value = dto.UserName.Trim() });

		await using var rdr = await cmd.ExecuteReaderAsync();

		http.Response.Headers.Remove("WWW-Authenticate"); // never challenge from this route

		if (!await rdr.ReadAsync())
			return Results.NotFound(new { message = "User not found." });

		var id = rdr.GetInt32(0);
		var userName = rdr.GetString(1);
		var display = rdr.GetString(2);
		var role = rdr.GetString(3);
		var pwDb = rdr.GetString(4);
		var active = rdr.GetBoolean(5);

		if (!active)
			return Results.Json(new { message = "User inactive." }, statusCode: StatusCodes.Status403Forbidden);

		var passOk = string.Equals(dto.Password.Trim(), pwDb, StringComparison.Ordinal);
		if (!passOk)
			return Results.Json(new { message = "Wrong password." }, statusCode: StatusCodes.Status403Forbidden);

		return Results.Ok(new { Id = id, UserName = userName, DisplayName = display, Role = role, IsActive = active });
	}
	catch (Exception ex)
	{
		http.Response.Headers.Remove("WWW-Authenticate");
		return Results.Problem($"Login failed: {ex.Message}");
	}
});


#endregion


// ============================================================================
// 7) Patients + Admissions
//    - Patient creation and admission workflows.
// ============================================================================
#region 7) Patients + Admissions

// POST /api/patients
// Creates a patient via dbo.usp_CreatePatient.
app.MapPost("/api/patients", async (PatientDto dto) =>
{
	try
	{
		await using var conn = new SqlConnection(cs);
		await conn.OpenAsync();
		await using var cmd = new SqlCommand("dbo.usp_CreatePatient", conn) { CommandType = CommandType.StoredProcedure };
		cmd.Parameters.AddWithValue("@MRN", dto.MRN);
		cmd.Parameters.AddWithValue("@FirstName", dto.FirstName);
		cmd.Parameters.AddWithValue("@LastName", dto.LastName);
		cmd.Parameters.AddWithValue("@DOB", dto.DOB);
		cmd.Parameters.AddWithValue("@Sex", (object)dto.Sex ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@Phone", (object)dto.Phone ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@Email", (object)dto.Email ?? DBNull.Value);
		var newPatientIdParam = new SqlParameter("@NewPatientID", SqlDbType.Int) { Direction = ParameterDirection.Output };
		cmd.Parameters.Add(newPatientIdParam);
		await cmd.ExecuteNonQueryAsync();
		int newPatientId = (int)newPatientIdParam.Value;
		return Results.Created($"/api/patients/{newPatientId}", new { Id = newPatientId });
	}
	catch (SqlException ex) when (ex.Number == 50000)
	{
		return Results.BadRequest(new { error = ex.Message });
	}
	catch (Exception ex)
	{
		return Results.Problem($"Failed to create patient: {ex.Message}");
	}
});
// Updates an existing patient record.
// MRN is not changed here. Only name, DOB, sex, phone, and email can be updated.
// Returns 404 if the patient ID does not exist.
app.MapPut("/api/patients/{id:int}", async (int id, PatientDto dto) =>
{
	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();

	const string sql = @"
        UPDATE dbo.TPatients
        SET
            strFirstName = @fn,
            strLastName  = @ln,
            dtmDOB       = @dob,
            strSex       = @sex,
            strPhone     = @phone,
            strEmail     = @mail
        WHERE intPatientID = @id;

        SELECT @@ROWCOUNT;
    ";

	await using var cmd = new SqlCommand(sql, conn);
	cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id });
	cmd.Parameters.Add(new SqlParameter("@fn", SqlDbType.VarChar, 100) { Value = dto.FirstName });
	cmd.Parameters.Add(new SqlParameter("@ln", SqlDbType.VarChar, 100) { Value = dto.LastName });
	cmd.Parameters.Add(new SqlParameter("@dob", SqlDbType.Date) { Value = (object?)dto.DOB ?? DBNull.Value });
	cmd.Parameters.Add(new SqlParameter("@sex", SqlDbType.VarChar, 20) { Value = (object?)dto.Sex ?? DBNull.Value });
	cmd.Parameters.Add(new SqlParameter("@phone", SqlDbType.VarChar, 30) { Value = (object?)dto.Phone ?? DBNull.Value });
	cmd.Parameters.Add(new SqlParameter("@mail", SqlDbType.VarChar, 200) { Value = (object?)dto.Email ?? DBNull.Value });

	var rowsObj = await cmd.ExecuteScalarAsync();
	var rows = rowsObj is int i ? i : Convert.ToInt32(rowsObj ?? 0);

	if (rows == 0)
		return Results.NotFound();

	// no need for dto.PatientID = id;
	return Results.Ok(dto);
});


// GET /api/admissions/active
// Lists active admissions with MRN, patient name, and room for nurse dropdowns.
app.MapGet("/api/admissions/active", async () =>
{
	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();

	const string sql = @"
        SELECT 
            p.strMRN            AS MRN,
            (p.strFirstName + ' ' + p.strLastName) AS PatientName,
            r.strRoomNumber     AS RoomNumber
        FROM dbo.TAdmissions a
        JOIN dbo.TPatients  p ON a.intPatientID = p.intPatientID
        JOIN dbo.TRooms     r ON a.intRoomID    = r.intRoomID
        WHERE a.bitActive = 1 AND a.dtmDischarge IS NULL
        ORDER BY r.strRoomNumber, PatientName;";

	var list = new List<object>();
	await using var cmd = new SqlCommand(sql, conn);
	await using var rdr = await cmd.ExecuteReaderAsync();
	while (await rdr.ReadAsync())
		list.Add(new
		{
			mrn = rdr.GetString(0),
			name = rdr.GetString(1),
			room = rdr.GetString(2)
		});

	return Results.Ok(list);
});

// POST /api/admissions
// Admits a patient (usp_AdmitPatient). Links patient to a room and optional diet order.
app.MapPost("/api/admissions", async (AdmissionDto dto) =>
{
	try
	{
		await using var conn = new SqlConnection(cs);
		await conn.OpenAsync();
		await using var cmd = new SqlCommand("dbo.usp_AdmitPatient", conn) { CommandType = CommandType.StoredProcedure };
		cmd.Parameters.AddWithValue("@PatientID", dto.PatientID);
		cmd.Parameters.AddWithValue("@RoomID", dto.RoomID);
		cmd.Parameters.AddWithValue("@DietOrder", (object)dto.DietOrder ?? DBNull.Value);
		var newAdmissionIdParam = new SqlParameter("@NewAdmissionID", SqlDbType.Int) { Direction = ParameterDirection.Output };
		cmd.Parameters.Add(newAdmissionIdParam);
		await cmd.ExecuteNonQueryAsync();
		int newAdmissionId = (int)newAdmissionIdParam.Value;
		return Results.Created($"/api/admissions/{newAdmissionId}", new { Id = newAdmissionId });
	}
	catch (SqlException ex) when (ex.Number == 50000)
	{
		return Results.BadRequest(new { error = ex.Message });
	}
	catch (Exception ex)
	{
		return Results.Problem($"Failed to admit patient: {ex.Message}");
	}
});

// GET /api/patients/exists?mrn=mrn001
// Returns 200/404 with a boolean { exists } for quick MRN validation.
app.MapGet("/api/patients/exists", async (string mrn) =>
{
	if (string.IsNullOrWhiteSpace(mrn))
		return Results.BadRequest(new { message = "MRN required" });

	try
	{
		await using var conn = new SqlConnection(cs);
		await conn.OpenAsync();

		const string sql = @"
            SELECT TOP (1) 1
            FROM dbo.TPatients
            WHERE UPPER(LTRIM(RTRIM(strMRN))) = UPPER(@mrn);";

		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add(new SqlParameter("@mrn", SqlDbType.NVarChar, 64) { Value = mrn.Trim() });

		var found = await cmd.ExecuteScalarAsync();
		return found is not null ? Results.Ok(new { exists = true })
								 : Results.NotFound(new { exists = false });
	}
	catch (Exception ex)
	{
		return Results.Problem($"Failed to check MRN: {ex.Message}");
	}
});

#endregion


// ============================================================================
// 8) Meal Periods (read-only)
//    - Helper for UI dropdowns / filters.
// ============================================================================
#region 8) Meal Periods (read-only)

// GET /api/mealperiods
// Lists active meal periods (id, name) for client-side selection.
app.MapGet("/api/mealperiods", async () =>
{
	try
	{
		var list = new List<object>();
		await using var conn = new SqlConnection(cs);
		await conn.OpenAsync();

		const string sql = @"
SELECT
    intMealPeriodID AS Id,
    strMealPeriod   AS Name
FROM dbo.TMealPeriods
WHERE bitIsActive = 1
ORDER BY intSort, strMealPeriod";

		await using var cmd = new SqlCommand(sql, conn);
		await using var rdr = await cmd.ExecuteReaderAsync();
		while (await rdr.ReadAsync())
			list.Add(new { Id = rdr.GetInt32(0), Name = rdr.GetString(1) });

		return Results.Ok(list);
	}
	catch (Exception ex)
	{
		return Results.Problem(title: "Failed to retrieve meal periods", detail: ex.Message, statusCode: 500);
	}
});

#endregion


// ============================================================================
// 9) Uploads
//    - Ping endpoint + file upload for menu images (stored under /wwwroot/food-images).
// ============================================================================
#region 9) Uploads

// GET /api/ping
// Lightweight ping for app liveness and clock sanity.
app.MapGet("/api/ping", () =>
{
	return Results.Ok(new { ok = true, time = DateTime.UtcNow });
});

// POST /api/uploads/food-images
// Saves an uploaded image to wwwroot/food-images and returns a relative URL.
// NOTE: Validates presence/length but not file type (prototype).
app.MapPost("/api/uploads/food-images", async (HttpRequest request, IWebHostEnvironment env) =>
{
	var form = await request.ReadFormAsync();
	var file = form.Files["file"];
	if (file is null || file.Length == 0)
		return Results.BadRequest("No file uploaded.");

	var webroot = env.WebRootPath ?? Path.Combine(AppContext.BaseDirectory, "wwwroot");
	var dir = Path.Combine(webroot, "food-images");
	Directory.CreateDirectory(dir);

	var safeName = Path.GetFileName(file.FileName);   // simplistic; sanitize further in production
	var path = Path.Combine(dir, safeName);

	await using var fs = new FileStream(path, FileMode.Create);
	await file.CopyToAsync(fs);

	return Results.Ok(new { url = $"/food-images/{safeName}" });
});

#endregion


// ============================================================================
// 10) Help Chat Endpoint
//     - Guards scope to "site navigation" questions.
//     - Proxies to OpenAI /v1/responses for short, UI-oriented answers.
//     - NOTE: Contains a hard-coded API key (prototype). Do NOT commit secrets.
// ============================================================================
#region 10) Help Chat Endpoint

app.MapPost("/api/helpchat", async (HelpIn input, HttpContext ctx) =>
{
	// --- 0) Read + normalize ---
	var userMessage = input?.message?.Trim() ?? "";
	if (string.IsNullOrWhiteSpace(userMessage))
		return Results.Json(new { reply = "Tell me which page you are on and what you are trying to do. For example: On Staff tab, how do I add a user?" });

	var msgLc = userMessage.ToLowerInvariant();
	// 0b) Very explicit command patterns (demo-only)
	if (msgLc.StartsWith("add ingredient "))
	{
		// pattern: add ingredient NAME qty NUMBER
		// you could be stricter here
		var rest = userMessage.Substring("add ingredient ".Length).Trim();
		var parts = rest.Split(" qty ", StringSplitOptions.RemoveEmptyEntries);
		var name = parts[0].Trim();
		var qty = 0;
		if (parts.Length > 1) int.TryParse(parts[1], out qty);

		if (string.IsNullOrWhiteSpace(name))
			return Results.Json(new { reply = "I saw 'add ingredient' but I could not find the name. Try: add ingredient Tomato qty 40" });

		// Call your existing POST /api/ingredients logic directly
		try
		{
			await using var conn = new SqlConnection(cs);
			await conn.OpenAsync();

			const string sql = @"INSERT INTO dbo.TIngredients (strIngredient, intQty)
                             VALUES (@n, @q); SELECT SCOPE_IDENTITY();";

			await using var cmd = new SqlCommand(sql, conn);
			cmd.Parameters.AddWithValue("@n", name);
			cmd.Parameters.AddWithValue("@q", qty);

			var idObj = await cmd.ExecuteScalarAsync();
			var id = Convert.ToInt32(idObj);

			return Results.Json(new { reply = $"I added ingredient '{name}' with quantity {qty} (ID {id})." });
		}
		catch (Exception ex)
		{
			Console.WriteLine("Helpdesk add ingredient error: " + ex);
			return Results.Json(new { reply = "I tried to add that ingredient but something went wrong. Check the log and try again." });
		}
	}

	// --- 1) FAQ: quick keyword based answers for common flows ---
	var faqs = new[]
	{
		new {
			Keys = new[] { "patient", "login" },
			Answer =
				"Patient login steps:\n" +
				"1. On the login screen choose the Patient MRN tab.\n" +
				"2. Enter the MRN from the wristband or paperwork.\n" +
				"3. Click Login. If it is valid you go straight to the patient menu for that MRN."
		},
		new {
			Keys = new[] { "staff", "login" },
			Answer =
				"Staff login steps:\n" +
				"1. On the login screen choose the Staff tab.\n" +
				"2. Enter your staff username and password.\n" +
				"3. Click Login.\n" +
				"Admins land in the Admin view. Nurses land in Nurse View. Kitchen and Runner roles land in Kitchen View."
		},
		new {
			Keys = new[] { "place", "order", "patient" },
			Answer =
				"To place an order as a patient:\n" +
				"1. Log in with the Patient MRN.\n" +
				"2. On the menu page choose a category tab such as Breakfast, Lunch or Dinner.\n" +
				"3. Click an item card to open details.\n" +
				"4. Adjust ingredients, quantity and notes, then click Add to Order.\n" +
				"5. Click the cart icon to open Order Summary.\n" +
				"6. Pick the meal period and click Place Order."
		},
		new {
			Keys = new[] { "nurse", "start", "order" },
			Answer =
				"To start an order from Nurse View:\n" +
				"1. Go to the Order tab in Nurse View.\n" +
				"2. Use the dropdown to pick the patient MRN and room.\n" +
				"3. Click Start Order.\n" +
				"4. The patient menu opens for that MRN so you can add items and place the order."
		},
		new {
			Keys = new[] { "kitchen", "status" },
			Answer =
				"In Kitchen View the order status flow is:\n" +
				"1. Placed.\n" +
				"2. In Kitchen.\n" +
				"3. Ready.\n" +
				"4. Delivered.\n" +
				"Use the buttons in the right hand summary panel such as Accept, Mark Ready and Mark Delivered to move the ticket forward."
		},
		new {
			Keys = new[] { "new", "user" },
			Answer =
				"To add a new staff user:\n" +
				"1. Go to Admin then the Staff tab.\n" +
				"2. Click the New User button in the top right.\n" +
				"3. Fill in Username, Display Name, Role and Password and leave Active checked.\n" +
				"4. Click Save."
		},
		new {
			Keys = new[] { "edit", "user" },
			Answer =
				"To modify a staff user:\n" +
				"1. Go to Admin then the Staff tab.\n" +
				"2. Find the user and click Modify.\n" +
				"3. Update display name, role or password as needed.\n" +
				"4. Click Save."
		},
		new {
			Keys = new[] { "new", "ingredient" },
			Answer =
				"To add an ingredient:\n" +
				"1. Go to Admin then the Ingredients tab.\n" +
				"2. Click New Ingredient.\n" +
				"3. Enter the name and an initial quantity.\n" +
				"4. Click Save."
		},
		new {
			Keys = new[] { "delete", "ingredient" },
			Answer =
				"To delete an ingredient:\n" +
				"1. Go to Admin then the Ingredients tab.\n" +
				"2. Click Delete on the ingredient row.\n" +
				"3. If the API says it is in use by menu items, remove it from those menu items in the Menu Items tab before trying again."
		},
		new {
			Keys = new[] { "new", "menu", "item" },
			Answer =
				"To add a menu item:\n" +
				"1. Go to Admin then the Menu Items tab.\n" +
				"2. Click New Item.\n" +
				"3. Enter Name and select a Meal Period.\n" +
				"4. Optionally upload an image using the Browse button.\n" +
				"5. Fill in calories, protein, carbs, fat and sodium if you have them.\n" +
				"6. Select up to six ingredients.\n" +
				"7. Click Save."
		},
		new {
			Keys = new[] { "edit", "menu", "item" },
			Answer =
				"To modify a menu item:\n" +
				"1. Go to Admin then the Menu Items tab.\n" +
				"2. Find the item in the list and click Modify.\n" +
				"3. Update name, meal period, image path, nutrition or ingredients.\n" +
				"4. Click Save."
		},
		new {
			Keys = new[] { "inventory", "quantity" },
			Answer =
				"To update inventory quantities:\n" +
				"1. Go to Admin then the Inventory tab.\n" +
				"2. In the New Qty column, type the new quantity for each ingredient you want to change.\n" +
				"3. Click Submit Changes. Only rows with a New Qty value are updated."
		},
		new {
			Keys = new[] { "ordering", "suggested" },
			Answer =
				"In Admin on the Ordering page:\n" +
				"1. Current Qty shows what you have on hand.\n" +
				"2. Suggested Ordering comes from how often each ingredient was used in orders.\n" +
				"3. Enter your actual Order Qty in the last column.\n" +
				"4. Check the total pieces at the bottom.\n" +
				"5. Click Submit Order to open a printable sheet that only shows ingredients with an Order Qty greater than zero."
		},
		new {
			Keys = new[] { "helpdesk" },
			Answer =
				"On the Helpdesk page:\n" +
				"1. Type your question in the box, such as: On Menu Items tab, how do I upload an image.\n" +
				"2. Click Send.\n" +
				"3. The helpdesk will answer with steps that match the actual BedsideBistro screens."
		}
	};

	foreach (var faq in faqs)
	{
		bool match = true;
		foreach (var k in faq.Keys)
		{
			if (!msgLc.Contains(k))
			{
				match = false;
				break;
			}
		}
		if (match)
			return Results.Json(new { reply = faq.Answer });
	}

	// --- 2) Scope gate: only assist with navigation and UI, not infra or secrets ---
	bool InScope(string s)
	{
		string[] allow =
		{
			"where is","how do i","how to","navigate","open","find","go to",
			"menu","inventory","ingredients","users","staff","login","logout",
			"tab","button","link","field","form","dialog","modal","window",
			"patient view","patient menu","nurse view","kitchen view","admin","helpdesk",
			"ordersummary","order summary","itemcustomization","item customization",
			"ordering","order status","ticket","cart","profile","diet","allergy",
			"edit","modify","create","new","delete","upload","image","search","filter","sort","save"
		};

		string[] block =
		{
			"medical record number list",
			"diagnose","diagnosis",
			"sql","query","database","schema","table structure",
			"api key","connection string","credit card","billing","secret","ssh",
			"password reset for user account"
		};

		foreach (var b in block)
			if (s.Contains(b))
				return false;

		foreach (var a in allow)
			if (s.Contains(a))
				return true;

		// short vague things like "help" are allowed
		return s.Length <= 16;
	}

	if (!InScope(msgLc))
	{
		return Results.Json(new
		{
			reply = "I am your BedsideBistro help buddy, but I only know the screens and buttons. Tell me which page you are on (for example Admin Staff or Nurse View) and what you are trying to do."
		});
	}

	// --- 3) LLM call (navigation assistant) ---
	// DEMO ONLY: hard coded key, as you requested.
	var apiKey = "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";
	if (string.IsNullOrWhiteSpace(apiKey))
	{
		return Results.Json(new { reply = "The helpdesk is not configured with an AI key right now." });
	}

	using var http = new HttpClient();
	http.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

	var systemText =
		"You are the Bedside Bistro in app help assistant.\n" +
		"Only answer questions about how to use the existing BedsideBistro screens: Login, Admin tabs (Emulator, Staff, Ingredients, Menu Items, Inventory, Ordering, Helpdesk), Patient View, Item Customization, Order Summary, Nurse View and Kitchen View.\n" +
		"Do not invent new pages or tabs. If something does not exist, say that it is not part of the current site and suggest the closest real page.\n" +
		"Focus on short step by step instructions using the real button labels, links and tab names.\n" +
		"Your tone is friendly and slightly playful, but you are still clear and professional.\n" +
		"If the question is too vague, say you are not sure what they mean and give two or three examples of questions you can answer.\n" +
		"Never talk about internal details such as SQL, schemas, API keys, or MRN lists.\n";

	var payload = new
	{
		model = "gpt-4.1-mini",
		temperature = 0.3,
		max_output_tokens = 350,
		input = new object[]
		{
			new { role = "system", content = systemText },
			new { role = "user",   content = userMessage }
		}
	};

	HttpResponseMessage res;
	try
	{
		res = await http.PostAsJsonAsync("https://api.openai.com/v1/responses", payload);
	}
	catch (Exception ex)
	{
		Console.WriteLine("OpenAI network error: " + ex);
		return Results.Json(new { reply = "The helpdesk could not reach the AI service. Try again in a bit." });
	}

	var responseText = await res.Content.ReadAsStringAsync();

	if (!res.IsSuccessStatusCode)
	{
		Console.WriteLine("OpenAI error: " + responseText);
		return Results.Json(new { reply = "The helpdesk got an error from the AI service. Try rephrasing your question or mention the exact page you are on." });
	}

	try
	{
		using var doc = System.Text.Json.JsonDocument.Parse(responseText);

		string? reply = null;
		if (doc.RootElement.TryGetProperty("output_text", out var ot))
			reply = ot.GetString();

		if (reply == null)
		{
			// fallback to the responses.output[0].content[0].text path
			reply = doc.RootElement
				.GetProperty("output")
				.EnumerateArray().FirstOrDefault()
				.GetProperty("content").EnumerateArray().FirstOrDefault()
				.GetProperty("text").GetString();
		}

		if (string.IsNullOrWhiteSpace(reply))
		{
			reply = "I can only help with navigating the BedsideBistro screens. Tell me which page you are on and what you want to do, and I will walk you through it.";
		}
		else if (reply.Length > 1000)
		{
			reply = reply.Substring(0, 1000);
		}

		return Results.Json(new { reply });
	}
	catch (Exception ex)
	{
		Console.WriteLine("Parse error: " + ex);
		return Results.Json(new { reply = "Something went sideways reading the AI reply. Try again and mention the page name and the button you are looking at." });
	}
});

#endregion


// ============================================================================
// 11) Orders
//     - TVP builders for bulk items
//     - Place order
//     - List orders
//     - Order status & timeline
//     - Get order (header + items)
//     - Cancel order
// ============================================================================
#region 11) Orders

// ---------------------------
// TVP builders (in-process)
// ---------------------------

// TVP: names (dbo.OrderItemNameTVP: ItemName, Qty, SpecialInstr)
// Converts external items (Name/Qty/SpecialInstr) to a DataTable for SP input.
static DataTable BuildItemNameTvp(IEnumerable<PlaceOrderItem>? items)
{
	var tvp = new DataTable();
	tvp.Columns.Add("ItemName", typeof(string));
	tvp.Columns.Add("Qty", typeof(int));
	tvp.Columns.Add("SpecialInstr", typeof(string));
	if (items != null)
	{
		foreach (var it in items)
		{
			if (string.IsNullOrWhiteSpace(it.Name) || it.Qty <= 0) continue;
			tvp.Rows.Add(it.Name.Trim(), it.Qty, (object?)it.SpecialInstr ?? DBNull.Value);
		}
	}
	return tvp; // OK if zero rows
}

// TVP: ids (dbo.OrderItemTVP: MenuItemID, Qty, SpecialInstr)
// Alternate builder (not used by the place-order endpoint in this file).
static DataTable BuildItemIdTvp(IEnumerable<(int MenuItemID, int Qty, string? SpecialInstr)>? items = null)
{
	var tvp = new DataTable();
	tvp.Columns.Add("MenuItemID", typeof(int));
	tvp.Columns.Add("Qty", typeof(int));
	tvp.Columns.Add("SpecialInstr", typeof(string));
	if (items != null)
	{
		foreach (var it in items)
			tvp.Rows.Add(it.MenuItemID, it.Qty, (object?)it.SpecialInstr ?? DBNull.Value);
	}
	return tvp; // zero rows is fine; columns must exist
}

// POST /api/orders
// Places an order by MRN and meal period name using dbo.usp_PlaceOrderWithItems_ByMRN.
// Requires item names (TVP by name). Returns order id + room number, etc.
app.MapPost("/api/orders", async (HttpContext http, IConfiguration cfg) =>
{
	// Parse JSON body
	using var doc = await System.Text.Json.JsonDocument.ParseAsync(http.Request.Body);
	var root = doc.RootElement;

	// Required
	var mrn = root.GetProperty("mrn").GetString();

	// Optional - support both name and ID
	int? orderedByUserID = null;
	if (root.TryGetProperty("orderedByUserID", out var obIdEl) && obIdEl.ValueKind != JsonValueKind.Null)
		orderedByUserID = obIdEl.GetInt32();

	string? orderedByUserName = null;
	if (root.TryGetProperty("orderedByUserName", out var obNameEl) && obNameEl.ValueKind == JsonValueKind.String)
		orderedByUserName = string.IsNullOrWhiteSpace(obNameEl.GetString()) ? null : obNameEl.GetString();

	string? mealPeriodName = null;
	if (root.TryGetProperty("mealPeriodName", out var mpEl) && mpEl.ValueKind == JsonValueKind.String)
		mealPeriodName = mpEl.GetString();

	DateTime? requestedFor = null;
	if (root.TryGetProperty("requestedFor", out var rfEl) && rfEl.ValueKind == JsonValueKind.String)
		requestedFor = DateTime.TryParse(rfEl.GetString(), out var dt) ? dt : null;

	string? notes = null;
	if (root.TryGetProperty("notes", out var nEl) && nEl.ValueKind == JsonValueKind.String)
		notes = string.IsNullOrWhiteSpace(nEl.GetString()) ? null : nEl.GetString();

	// Build TVP for ItemsByName: ItemName, Qty, SpecialInstr
	var itemsByName = new DataTable();
	itemsByName.Columns.Add("ItemName", typeof(string));
	itemsByName.Columns.Add("Qty", typeof(int));
	itemsByName.Columns.Add("SpecialInstr", typeof(string));

	if (root.TryGetProperty("items", out var itemsEl) && itemsEl.ValueKind == JsonValueKind.Array)
	{
		foreach (var it in itemsEl.EnumerateArray())
		{
			var nm = it.TryGetProperty("name", out var nmEl) ? nmEl.GetString() : null;
			var qty = it.TryGetProperty("qty", out var qEl) ? Math.Max(1, qEl.GetInt32()) : 1;
			string? instr = null;
			if (it.TryGetProperty("specialInstr", out var sEl) && sEl.ValueKind == JsonValueKind.String)
				instr = string.IsNullOrWhiteSpace(sEl.GetString()) ? null : sEl.GetString();

			if (!string.IsNullOrWhiteSpace(nm))
				itemsByName.Rows.Add(nm, qty, (object?)instr ?? DBNull.Value);
		}
	}

	// Empty TVP for ItemsById path
	var itemsById = new DataTable();
	itemsById.Columns.Add("MenuItemID", typeof(int));
	itemsById.Columns.Add("Qty", typeof(int));
	itemsById.Columns.Add("SpecialInstr", typeof(string));
	// leave empty intentionally

	try
	{
		await using var con = new SqlConnection(cfg.GetConnectionString("Default"));
		await con.OpenAsync();

		await using var cmd = new SqlCommand("dbo.usp_PlaceOrderWithItems_ByMRN", con)
		{ CommandType = CommandType.StoredProcedure };

		cmd.Parameters.Add(new SqlParameter("@MRN", SqlDbType.VarChar, 50) { Value = mrn });

		// either user name or id - both nullable
		cmd.Parameters.Add(new SqlParameter("@OrderedByUserName", SqlDbType.VarChar, 120) { Value = (object?)orderedByUserName ?? DBNull.Value });
		cmd.Parameters.Add(new SqlParameter("@OrderedByUserID", SqlDbType.Int) { Value = (object?)orderedByUserID ?? DBNull.Value });

		cmd.Parameters.Add(new SqlParameter("@MealPeriodName", SqlDbType.VarChar, 40) { Value = (object?)mealPeriodName ?? DBNull.Value });
		cmd.Parameters.Add(new SqlParameter("@MealPeriodID", SqlDbType.Int) { Value = DBNull.Value });

		cmd.Parameters.Add(new SqlParameter("@RequestedFor", SqlDbType.DateTime2) { Value = (object?)requestedFor ?? DBNull.Value });
		cmd.Parameters.Add(new SqlParameter("@Notes", SqlDbType.VarChar, 4000) { Value = (object?)notes ?? DBNull.Value });

		// TVPs
		cmd.Parameters.Add(new SqlParameter("@ItemsByName", SqlDbType.Structured)
		{ TypeName = "dbo.OrderItemNameTVP", Value = itemsByName.Rows.Count > 0 ? itemsByName : DBNull.Value });

		cmd.Parameters.Add(new SqlParameter("@ItemsById", SqlDbType.Structured)
		{ TypeName = "dbo.OrderItemTVP", Value = itemsById }); // empty TVP is OK

		// Outputs
		var pOrderId = cmd.Parameters.Add(new SqlParameter("@NewOrderID", SqlDbType.Int) { Direction = ParameterDirection.Output });
		var pRoom = cmd.Parameters.Add(new SqlParameter("@OutRoomNumber", SqlDbType.VarChar, 20) { Direction = ParameterDirection.Output });

		await cmd.ExecuteNonQueryAsync();

		var id = pOrderId.Value is DBNull ? 0 : (int)pOrderId.Value;
		var room = pRoom.Value is DBNull ? null : (string)pRoom.Value;

		return Results.Ok(new { orderId = id, roomNumber = room, mealPeriodName, requestedFor });
	}
	catch (SqlException ex)
	{
		// Surface SQL messages as 400 so your page shows the reason
		return Results.BadRequest(new { error = ex.Message });
	}
	catch (Exception ex)
	{
		// Temporary during debugging
		return Results.Problem(ex.Message, statusCode: 500);
	}
});

app.MapPost("/api/ordersSummary", async (PlaceOrderRequest req) =>
{
	if (string.IsNullOrWhiteSpace(req.MRN))
		return Results.BadRequest("MRN is required.");
	if (string.IsNullOrWhiteSpace(req.MealPeriodName))
		return Results.BadRequest("MealPeriodName is required.");
	if ((req.OrderedByUserName is null || string.IsNullOrWhiteSpace(req.OrderedByUserName))
		&& req.OrderedByUserID is null)
		return Results.BadRequest("Provide OrderedByUserName or OrderedByUserID.");
	if (req.Items is null || req.Items.Count == 0)
		return Results.BadRequest("At least one item is required.");

	var requestedFor = req.RequestedFor?.ToLocalTime() ?? DateTime.Now;

	var tvp = BuildItemNameTvp(req.Items);
	if (tvp.Rows.Count == 0)
		return Results.BadRequest("No valid items found.");

	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();
	await using var cmd = new SqlCommand("dbo.usp_PlaceOrderWithItems_ByMRN", conn)
	{
		CommandType = CommandType.StoredProcedure
	};

	cmd.Parameters.AddWithValue("@MRN", req.MRN.Trim());
	cmd.Parameters.AddWithValue("@OrderedByUserName",
		(object?)req.OrderedByUserName?.Trim() ?? DBNull.Value);
	cmd.Parameters.AddWithValue("@OrderedByUserID",
		(object?)req.OrderedByUserID ?? DBNull.Value);
	cmd.Parameters.AddWithValue("@MealPeriodName", req.MealPeriodName.Trim());
	cmd.Parameters.AddWithValue("@MealPeriodID", DBNull.Value);
	cmd.Parameters.AddWithValue("@RequestedFor", requestedFor);
	cmd.Parameters.AddWithValue("@Notes", (object?)req.Notes ?? DBNull.Value);

	var pItemsByName = cmd.Parameters.Add("@ItemsByName", SqlDbType.Structured);
	pItemsByName.TypeName = "dbo.OrderItemNameTVP";
	pItemsByName.Value = tvp;

	var pItemsById = cmd.Parameters.Add("@ItemsById", SqlDbType.Structured);
	pItemsById.TypeName = "dbo.OrderItemTVP";
	pItemsById.Value = new DataTable(); // unused here

	var pOrderId = new SqlParameter("@NewOrderID", SqlDbType.Int) { Direction = ParameterDirection.Output };
	var pRoom = new SqlParameter("@OutRoomNumber", SqlDbType.VarChar, 20) { Direction = ParameterDirection.Output };
	cmd.Parameters.Add(pOrderId);
	cmd.Parameters.Add(pRoom);

	try
	{
		await cmd.ExecuteNonQueryAsync();

		return Results.Ok(new PlaceOrderResponse
		{
			OrderId = (int)(pOrderId.Value ?? 0),
			RoomNumber = (string)(pRoom.Value ?? ""),
			RequestedFor = requestedFor,
			Status = "Placed"
		});
	}
	catch (SqlException ex)
	{
		return Results.Problem(ex.Message, statusCode: 400);
	}
});

// GET /api/orders
// Lists orders (optionally filtered by MRN) with key info for dashboards.
app.MapGet("/api/orders", async (HttpRequest req) =>
{
	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();

	string? mrn = req.Query["mrn"];
	string sql = @"
        SELECT 
            o.intOrderID AS OrderID,
            p.strMRN AS MRN,
            CONCAT(p.strFirstName, ' ', p.strLastName) AS PatientName,
            r.strRoomNumber AS RoomNumber,
            mp.strMealPeriod AS MealPeriod,
            s.strStatus AS Status,
            o.dtmPlaced AS PlacedOn,
            o.dtmRequestedFor AS RequestedFor,
            o.strNotes AS Notes
        FROM dbo.TOrders o
        JOIN dbo.TAdmissions a ON o.intAdmissionID = a.intAdmissionID
        JOIN dbo.TPatients p ON a.intPatientID = p.intPatientID
        JOIN dbo.TRooms r ON a.intRoomID = r.intRoomID
        JOIN dbo.TOrderStatus s ON o.intOrderStatusID = s.intOrderStatusID
        LEFT JOIN dbo.TMealPeriods mp ON o.intMealPeriodID = mp.intMealPeriodID
        /**where**/
        ORDER BY o.dtmPlaced DESC;";

	// Optional MRN filter
	if (!string.IsNullOrWhiteSpace(mrn))
		sql = sql.Replace("/**where**/", "WHERE p.strMRN = @mrn");
	else
		sql = sql.Replace("/**where**/", "");

	await using var cmd = new SqlCommand(sql, conn);
	if (!string.IsNullOrWhiteSpace(mrn))
		cmd.Parameters.AddWithValue("@mrn", mrn);

	var list = new List<object>();
	await using var rdr = await cmd.ExecuteReaderAsync();
	while (await rdr.ReadAsync())
	{
		list.Add(new
		{
			OrderID = rdr.GetInt32(0),
			MRN = rdr.GetString(1),
			PatientName = rdr.GetString(2),
			RoomNumber = rdr.GetString(3),
			MealPeriod = rdr.IsDBNull(4) ? null : rdr.GetString(4),
			Status = rdr.GetString(5),
			PlacedOn = rdr.GetDateTime(6),                        // NOT NULL in schema
			RequestedFor = rdr.IsDBNull(7) ? (DateTime?)null : rdr.GetDateTime(7),
			Notes = rdr.IsDBNull(8) ? null : rdr.GetString(8)
		});
	}

	return Results.Ok(list);
});

// ---------------------------
// Order Status + Timeline
// ---------------------------

// GET /api/orderstatus
// Lists active statuses for client dropdowns.
app.MapGet("/api/orderstatus", async () =>
{
	var rows = new List<object>();
	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();

	const string sql = @"
        SELECT intOrderStatusID, strStatus
        FROM dbo.TOrderStatus
        WHERE bitIsActive = 1
        ORDER BY intOrderStatusID;";

	await using var cmd = new SqlCommand(sql, conn);
	await using var rdr = await cmd.ExecuteReaderAsync();
	while (await rdr.ReadAsync())
		rows.Add(new { Id = rdr.GetInt32(0), Name = rdr.GetString(1) });

	return Results.Ok(rows);
});

// GET /api/orders/{id}/events
// Returns the status history (timeline) for a given order.
app.MapGet("/api/orders/{id:int}/events", async (int id) =>
{
	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();

	const string sql = @"
        SELECT e.intOrderEventID, e.dtmEventTime, s.strStatus,
               e.intUserID, u.strDisplayName, e.strComment
        FROM dbo.TOrderEvents e
        JOIN dbo.TOrderStatus s ON e.intOrderStatusID = s.intOrderStatusID
        LEFT JOIN dbo.TUsers u   ON e.intUserID = u.intUserID
        WHERE e.intOrderID = @id
        ORDER BY e.dtmEventTime;";

	await using var cmd = new SqlCommand(sql, conn);
	cmd.Parameters.AddWithValue("@id", id);

	var list = new List<object>();
	await using var rdr = await cmd.ExecuteReaderAsync();
	while (await rdr.ReadAsync())
	{
		list.Add(new
		{
			EventID = rdr.GetInt32(0),
			EventTime = rdr.GetDateTime(1),
			Status = rdr.GetString(2),
			UserID = rdr.IsDBNull(3) ? (int?)null : rdr.GetInt32(3),
			User = rdr.IsDBNull(4) ? null : rdr.GetString(4),
			Comment = rdr.IsDBNull(5) ? null : rdr.GetString(5)
		});
	}
	return Results.Ok(list);
});

// PUT /api/orders/{id}/status
// Transitions an order to a new status and logs an event.
// - Validates order exists, target status is active, and user is active.
// - If no-op (same status + no comment), returns a snapshot without logging.
app.MapPut("/api/orders/{id:int}/status", async (int id, OrderStatusDto dto) =>
{
	if (dto is null) return Results.BadRequest(new { error = "Body required." });
	if (dto.NewStatusID <= 0) return Results.BadRequest(new { error = "NewStatusID must be > 0." });
	if (dto.UserID <= 0) return Results.BadRequest(new { error = "UserID must be > 0." });

	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();
	await using var tx = await conn.BeginTransactionAsync();

	try
	{
		// 1) Order exists?
		int currentStatusId;
		await using (var cmd = new SqlCommand(
			"SELECT intOrderStatusID FROM dbo.TOrders WHERE intOrderID = @id;",
			conn, (SqlTransaction)tx))
		{
			cmd.Parameters.AddWithValue("@id", id);
			var obj = await cmd.ExecuteScalarAsync();
			if (obj is null)
			{
				await tx.RollbackAsync();
				return Results.NotFound(new { error = "Order not found." });
			}
			currentStatusId = Convert.ToInt32(obj);
		}

		// 2) Target status (active)?
		string statusName;
		await using (var cmd = new SqlCommand(
			"SELECT strStatus FROM dbo.TOrderStatus WHERE intOrderStatusID = @sid AND bitIsActive = 1;",
			conn, (SqlTransaction)tx))
		{
			cmd.Parameters.AddWithValue("@sid", dto.NewStatusID);
			var obj = await cmd.ExecuteScalarAsync();
			if (obj is null)
			{
				await tx.RollbackAsync();
				return Results.BadRequest(new { error = "Invalid or inactive status." });
			}
			statusName = Convert.ToString(obj)!;
		}

		// 3) Acting user active?
		await using (var cmd = new SqlCommand(
			"SELECT 1 FROM dbo.TUsers WHERE intUserID = @uid AND bitIsActive = 1;",
			conn, (SqlTransaction)tx))
		{
			cmd.Parameters.AddWithValue("@uid", dto.UserID);
			var ok = await cmd.ExecuteScalarAsync();
			if (ok is null)
			{
				await tx.RollbackAsync();
				return Results.BadRequest(new { error = "User not found or inactive." });
			}
		}

		// 4) No-op?
		if (currentStatusId == dto.NewStatusID && string.IsNullOrWhiteSpace(dto.Comment))
		{
			await tx.CommitAsync();
			// After commit, fetch current snapshot outside txn
			await using var cmd = new SqlCommand(@"
                SELECT o.intOrderID, s.intOrderStatusID, s.strStatus, o.dtmDelivered
                FROM dbo.TOrders o
                JOIN dbo.TOrderStatus s ON o.intOrderStatusID = s.intOrderStatusID
                WHERE o.intOrderID = @id;", conn);
			cmd.Parameters.AddWithValue("@id", id);
			await using var rdr = await cmd.ExecuteReaderAsync();
			if (!await rdr.ReadAsync()) return Results.NotFound();
			var payload = new
			{
				OrderID = rdr.GetInt32(0),
				StatusID = rdr.GetInt32(1),
				Status = rdr.GetString(2),
				DeliveredOn = rdr.IsDBNull(3) ? (DateTime?)null : rdr.GetDateTime(3)
			};
			return Results.Ok(payload);
		}

		// 5) Update order
		await using (var cmd = new SqlCommand(@"
            UPDATE dbo.TOrders
               SET intOrderStatusID = @sid,
                   dtmDelivered = CASE WHEN @isDelivered = 1 THEN COALESCE(dtmDelivered, SYSDATETIME()) ELSE dtmDelivered END
             WHERE intOrderID = @id;", conn, (SqlTransaction)tx))
		{
			cmd.Parameters.AddWithValue("@sid", dto.NewStatusID);
			cmd.Parameters.AddWithValue("@id", id);
			cmd.Parameters.AddWithValue("@isDelivered", statusName.Equals("Delivered", StringComparison.OrdinalIgnoreCase) ? 1 : 0);
			if (await cmd.ExecuteNonQueryAsync() == 0)
			{
				await tx.RollbackAsync();
				return Results.NotFound(new { error = "Order not found." });
			}
		}
		// 5b) If this is a first time transition into Delivered, consume inventory
		// We treat "old status != new status AND new status is Delivered" as the trigger.
		if (currentStatusId != dto.NewStatusID &&
			statusName.Equals("Delivered", StringComparison.OrdinalIgnoreCase))
		{
			// Each order line has a menu item and a quantity.
			// Each menu item has one or more ingredients.
			// We want: for this order, sum qty per ingredient, then subtract from TIngredients.intQty.

			await using var cmdConsume = new SqlCommand(@"
			;WITH Used AS (
				SELECT
					mii.intIngredientID,
					SUM(oi.intQty) AS TotalQty
				FROM dbo.TOrderItems oi
				JOIN dbo.TMenuItemIngredients mii
					ON mii.intMenuItemID = oi.intMenuItemID
				WHERE oi.intOrderID = @orderId
				GROUP BY mii.intIngredientID
			)
			UPDATE i
			   SET i.intQty =
					CASE
						WHEN i.intQty >= u.TotalQty THEN i.intQty - u.TotalQty
						ELSE 0
					END,
				   i.dtmUpdatedOn = SYSDATETIME(),
				   i.strUpdatedBy = COALESCE(i.strUpdatedBy, SUSER_SNAME())
			FROM dbo.TIngredients i
			JOIN Used u
			  ON u.intIngredientID = i.intIngredientID;
			", conn, (SqlTransaction)tx);

			cmdConsume.Parameters.AddWithValue("@orderId", id);

			await cmdConsume.ExecuteNonQueryAsync();
		}

		// 6) Log event
		await using (var cmd = new SqlCommand(@"
            INSERT INTO dbo.TOrderEvents (intOrderID, intOrderStatusID, intUserID, strComment)
            VALUES (@id, @sid, @uid, @comment);", conn, (SqlTransaction)tx))
		{
			cmd.Parameters.AddWithValue("@id", id);
			cmd.Parameters.AddWithValue("@sid", dto.NewStatusID);
			cmd.Parameters.AddWithValue("@uid", dto.UserID);
			cmd.Parameters.AddWithValue("@comment", (object?)dto.Comment ?? DBNull.Value);
			await cmd.ExecuteNonQueryAsync();
		}

		// 7) Commit first, then read payload on a fresh command (no active reader at commit)
		await tx.CommitAsync();

		await using var cmdOut = new SqlCommand(@"
            SELECT o.intOrderID, s.intOrderStatusID, s.strStatus, o.dtmDelivered
            FROM dbo.TOrders o
            JOIN dbo.TOrderStatus s ON o.intOrderStatusID = s.intOrderStatusID
            WHERE o.intOrderID = @id;", conn);
		cmdOut.Parameters.AddWithValue("@id", id);

		await using var rdrOut = await cmdOut.ExecuteReaderAsync(CommandBehavior.SingleRow);
		if (!await rdrOut.ReadAsync()) return Results.NotFound();

		var result = new
		{
			OrderID = rdrOut.GetInt32(0),
			StatusID = rdrOut.GetInt32(1),
			Status = rdrOut.GetString(2),
			DeliveredOn = rdrOut.IsDBNull(3) ? (DateTime?)null : rdrOut.GetDateTime(3)
		};
		return Results.Ok(result);
	}
	catch (Exception ex)
	{
		await tx.RollbackAsync();
		return Results.Problem(ex.Message, statusCode: 500);
	}
})
.Accepts<OrderStatusDto>("application/json")
.Produces(StatusCodes.Status200OK)
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status404NotFound)
.Produces(StatusCodes.Status500InternalServerError);

// GET /api/orders/{id}
// Returns a full order payload (header + item lines) for detail views/print.
app.MapGet("/api/orders/{id:int}", async (int id) =>
{
	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();

	const string sqlHeader = @"
        SELECT 
            o.intOrderID,
            p.strMRN,
            CONCAT(p.strFirstName,' ',p.strLastName) AS PatientName,
            r.strRoomNumber,
            mp.strMealPeriod,
            s.strStatus,
            o.dtmPlaced,
            o.dtmRequestedFor,
            o.dtmDelivered,
            o.strNotes
        FROM dbo.TOrders o
        JOIN dbo.TAdmissions a ON o.intAdmissionID = a.intAdmissionID
        JOIN dbo.TPatients p   ON a.intPatientID   = p.intPatientID
        JOIN dbo.TRooms r      ON a.intRoomID      = r.intRoomID
        JOIN dbo.TOrderStatus s ON o.intOrderStatusID = s.intOrderStatusID
        LEFT JOIN dbo.TMealPeriods mp ON o.intMealPeriodID = mp.intMealPeriodID
        WHERE o.intOrderID = @id;";

	await using var cmd = new SqlCommand(sqlHeader, conn);
	cmd.Parameters.AddWithValue("@id", id);

	await using var rdr = await cmd.ExecuteReaderAsync();
	if (!await rdr.ReadAsync())
		return Results.NotFound(new { error = "Order not found." });

	var header = new
	{
		OrderID = rdr.GetInt32(0),
		MRN = rdr.GetString(1),
		PatientName = rdr.GetString(2),
		RoomNumber = rdr.GetString(3),
		MealPeriod = rdr.IsDBNull(4) ? null : rdr.GetString(4),
		Status = rdr.GetString(5),
		PlacedOn = rdr.GetDateTime(6),
		RequestedFor = rdr.IsDBNull(7) ? (DateTime?)null : rdr.GetDateTime(7),
		DeliveredOn = rdr.IsDBNull(8) ? (DateTime?)null : rdr.GetDateTime(8),
		Notes = rdr.IsDBNull(9) ? null : rdr.GetString(9)
	};

	await rdr.CloseAsync();

	const string sqlItems = @"
        SELECT 
            oi.intOrderItemID,
            oi.intMenuItemID,
            mi.strItemName,
            oi.intQty,
            oi.strSpecialInstr
        FROM dbo.TOrderItems oi
        JOIN dbo.TMenuItems mi ON oi.intMenuItemID = mi.intMenuItemID
        WHERE oi.intOrderID = @id
        ORDER BY oi.intOrderItemID;";

	var items = new List<object>();
	await using (var cmd2 = new SqlCommand(sqlItems, conn))
	{
		cmd2.Parameters.AddWithValue("@id", id);
		await using var rdr2 = await cmd2.ExecuteReaderAsync();
		while (await rdr2.ReadAsync())
		{
			items.Add(new
			{
				OrderItemID = rdr2.GetInt32(0),
				MenuItemID = rdr2.GetInt32(1),
				ItemName = rdr2.GetString(2),
				Qty = rdr2.GetInt32(3),
				SpecialInstr = rdr2.IsDBNull(4) ? null : rdr2.GetString(4)
			});
		}
	}

	return Results.Ok(new { header, items });
});


// Menu mix report - delivered items only

app.MapGet("/api/reports/menumix", async (DateTime? from, DateTime? to) =>
{
	// UI will send:
	//   from = selected from date (inclusive)
	//   to   = day AFTER selected to date (exclusive)
	DateTime? fromDate = from;
	DateTime? toExclusive = to;

	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();

	const string sql = @"
        SELECT
            mi.intMenuItemID,
            mi.strItemName,
            SUM(oi.intQty) AS TotalSold
        FROM dbo.TOrderItems oi
        JOIN dbo.TOrders o        ON oi.intOrderID      = o.intOrderID
        JOIN dbo.TOrderStatus s   ON o.intOrderStatusID = s.intOrderStatusID
        JOIN dbo.TMenuItems mi    ON oi.intMenuItemID   = mi.intMenuItemID
        WHERE s.strStatus = 'Delivered'
          AND (@from IS NULL OR o.dtmPlaced >= @from)
          AND (@to   IS NULL OR o.dtmPlaced <  @to)
        GROUP BY mi.intMenuItemID, mi.strItemName
        HAVING SUM(oi.intQty) > 0
        ORDER BY TotalSold DESC, mi.strItemName;";

	await using var cmd = new SqlCommand(sql, conn);

	cmd.Parameters.Add("@from", SqlDbType.DateTime2).Value =
		(object)fromDate ?? DBNull.Value;

	cmd.Parameters.Add("@to", SqlDbType.DateTime2).Value =
		(object)toExclusive ?? DBNull.Value;

	var rows = new List<object>();

	await using var rdr = await cmd.ExecuteReaderAsync();
	while (await rdr.ReadAsync())
	{
		rows.Add(new
		{
			MenuItemID = rdr.GetInt32(0),
			ItemName = rdr.GetString(1),
			TotalSold = rdr.GetInt32(2)
		});
	}

	return Results.Ok(rows);
});



// PATCH/POST /api/orders/{id}/cancel
// Cancels an order (sets status to 'Cancelled' if active) and logs an event.
// Accepts optional acting user and comment.
app.MapMethods("/api/orders/{id:int}/cancel", new[] { "PATCH", "POST" }, async (int id, CancelOrderDto dto) =>
{
	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();
	await using var tx = await conn.BeginTransactionAsync();

	try
	{
		// 1) Ensure order exists
		await using (var cmd = new SqlCommand("SELECT 1 FROM dbo.TOrders WHERE intOrderID=@id;", conn, (SqlTransaction)tx))
		{
			cmd.Parameters.AddWithValue("@id", id);
			if (await cmd.ExecuteScalarAsync() is null)
			{ await tx.RollbackAsync(); return Results.NotFound(new { error = "Order not found." }); }
		}

		// 2) Resolve Cancelled status
		int cancelStatusId;
		await using (var cmd = new SqlCommand(
			"SELECT intOrderStatusID FROM dbo.TOrderStatus WHERE strStatus='Cancelled' AND bitIsActive=1;",
			conn, (SqlTransaction)tx))
		{
			var obj = await cmd.ExecuteScalarAsync();
			if (obj is null) { await tx.RollbackAsync(); return Results.BadRequest(new { error = "Cancelled status not configured." }); }
			cancelStatusId = Convert.ToInt32(obj);
		}

		// 3) Resolve user (nullable if invalid/missing)
		int? userIdDb = null;
		if (dto?.UserID is int uid && uid > 0)
		{
			await using var cmd = new SqlCommand("SELECT intUserID FROM dbo.TUsers WHERE intUserID=@u AND bitIsActive=1;", conn, (SqlTransaction)tx);
			cmd.Parameters.AddWithValue("@u", uid);
			var obj = await cmd.ExecuteScalarAsync();
			if (obj != null) userIdDb = Convert.ToInt32(obj);
		}

		// 4) Update order → Cancelled
		await using (var cmd = new SqlCommand(
			"UPDATE dbo.TOrders SET intOrderStatusID=@sid WHERE intOrderID=@id;", conn, (SqlTransaction)tx))
		{
			cmd.Parameters.AddWithValue("@sid", cancelStatusId);
			cmd.Parameters.AddWithValue("@id", id);
			await cmd.ExecuteNonQueryAsync();
		}

		// 5) Log event
		await using (var cmd = new SqlCommand(@"
            INSERT INTO dbo.TOrderEvents (intOrderID,intOrderStatusID,intUserID,strComment)
            VALUES (@id,@sid,@uid,COALESCE(@cmt,'Cancelled'));", conn, (SqlTransaction)tx))
		{
			cmd.Parameters.AddWithValue("@id", id);
			cmd.Parameters.AddWithValue("@sid", cancelStatusId);
			cmd.Parameters.AddWithValue("@uid", (object?)userIdDb ?? DBNull.Value);
			cmd.Parameters.AddWithValue("@cmt", (object?)dto?.Comment ?? DBNull.Value);
			await cmd.ExecuteNonQueryAsync();
		}

		await tx.CommitAsync();

		// 6) Return compact snapshot
		await using var cmdOut = new SqlCommand(@"
            SELECT o.intOrderID, s.intOrderStatusID, s.strStatus
            FROM dbo.TOrders o
            JOIN dbo.TOrderStatus s ON o.intOrderStatusID = s.intOrderStatusID
            WHERE o.intOrderID = @id;", conn);
		cmdOut.Parameters.AddWithValue("@id", id);
		await using var rdr = await cmdOut.ExecuteReaderAsync();
		if (!await rdr.ReadAsync()) return Results.NotFound();

		return Results.Ok(new { OrderID = rdr.GetInt32(0), StatusID = rdr.GetInt32(1), Status = rdr.GetString(2) });
	}
	catch (Exception ex)
	{
		await tx.RollbackAsync();
		return Results.Problem(ex.Message, statusCode: 500);
	}
});

// GET /api/inventory/ordering
// Returns current quantity plus a simple "suggested" quantity based on ingredient usage in orders.
app.MapGet("/api/inventory/ordering", async () =>
{
	var rows = new List<object>();

	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();

	const string sql = @"
;WITH Usage AS (
    SELECT
        mi.intIngredientID,
        SUM(oi.intQty) AS UsageCount
    FROM dbo.TOrderItems oi
    JOIN dbo.TMenuItemIngredients mi
        ON mi.intMenuItemID = oi.intMenuItemID
    JOIN dbo.TOrders o
        ON o.intOrderID = oi.intOrderID
    JOIN dbo.TOrderStatus s
        ON s.intOrderStatusID = o.intOrderStatusID
    WHERE s.strStatus <> 'Cancelled'
    GROUP BY mi.intIngredientID
)
SELECT
    i.intIngredientID,
    i.strIngredient,
    i.intQty,
    ISNULL(u.UsageCount, 0) AS SuggestedQty
FROM dbo.TIngredients i
LEFT JOIN Usage u
    ON u.intIngredientID = i.intIngredientID
ORDER BY i.strIngredient;
";

	await using var cmd = new SqlCommand(sql, conn);
	await using var rdr = await cmd.ExecuteReaderAsync();

	while (await rdr.ReadAsync())
	{
		rows.Add(new
		{
			Id = rdr.GetInt32(0),
			Name = rdr.GetString(1),
			CurrentQty = rdr.IsDBNull(2) ? 0 : rdr.GetInt32(2),
			SuggestedQty = rdr.IsDBNull(3) ? 0 : rdr.GetInt32(3)
		});
	}

	return Results.Ok(rows);
});

#endregion


// ============================================================================
// 12) PROFILE API (grouped under /api/profile)
//     - Patient demographics updates (by MRN)
//     - Diets lookup, assign diet to active admission (by MRN)
//     - Active diet limits (by MRN)
//     - Allergies (list/add/remove)
// ============================================================================
#region 12) Profile API

// Base group: /api/profile
var profileApi = app.MapGroup("/api/profile");

// PUT /api/profile
// Partially updates patient demographics by MRN.
profileApi.MapPut("", async (PatientProfileUpdateDto dto) =>
{
	if (string.IsNullOrWhiteSpace(dto.MRN))
		return Results.BadRequest(new { error = "MRN is required" });

	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();

	const string sql = @"
        UPDATE dbo.TPatients
           SET strFirstName = COALESCE(@FirstName, strFirstName),
               strLastName  = COALESCE(@LastName,  strLastName),
               strPhone     = COALESCE(@Phone,     strPhone),
               strEmail     = COALESCE(@Email,     strEmail),
               dtmUpdatedOn = SYSUTCDATETIME(),
               strUpdatedBy = SUSER_SNAME()
         WHERE UPPER(LTRIM(RTRIM(strMRN))) = UPPER(@MRN);
        SELECT @@ROWCOUNT;";

	await using var cmd = new SqlCommand(sql, conn);
	cmd.Parameters.AddWithValue("@MRN", dto.MRN.Trim());
	cmd.Parameters.AddWithValue("@FirstName", (object?)dto.FirstName ?? DBNull.Value);
	cmd.Parameters.AddWithValue("@LastName", (object?)dto.LastName ?? DBNull.Value);
	cmd.Parameters.AddWithValue("@Phone", (object?)dto.Phone ?? DBNull.Value);
	cmd.Parameters.AddWithValue("@Email", (object?)dto.Email ?? DBNull.Value);

	var rows = Convert.ToInt32(await cmd.ExecuteScalarAsync());
	return rows == 0 ? Results.NotFound(new { error = "Patient not found." }) : Results.NoContent();
});

// GET /api/profile/basic?mrn=MRN001
profileApi.MapGet("/basic", async (string mrn) =>
{
	if (string.IsNullOrWhiteSpace(mrn))
		return Results.BadRequest(new { error = "mrn is required" });

	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();

	const string sql = @"
        SELECT TOP (1)
            p.intPatientID            AS PatientID,
            p.strMRN                  AS MRN,
            (p.strFirstName + ' ' + p.strLastName) AS PatientName,
            a.intAdmissionID          AS AdmissionID,
            r.strRoomNumber           AS RoomNumber
        FROM dbo.TPatients p
        JOIN dbo.TAdmissions a ON a.intPatientID = p.intPatientID
        JOIN dbo.TRooms     r ON r.intRoomID     = a.intRoomID
        WHERE UPPER(LTRIM(RTRIM(p.strMRN))) = UPPER(@mrn)
          AND a.bitActive = 1
          AND a.dtmDischarge IS NULL
        ORDER BY a.dtmAdmit DESC;";

	await using var cmd = new SqlCommand(sql, conn);
	cmd.Parameters.AddWithValue("@mrn", mrn.Trim());

	await using var rdr = await cmd.ExecuteReaderAsync();
	if (!await rdr.ReadAsync())
		return Results.NotFound(new { error = "No active admission for MRN." });

	return Results.Ok(new
	{
		PatientID = rdr.GetInt32(0),
		MRN = rdr.GetString(1),
		PatientName = rdr.GetString(2),
		AdmissionID = rdr.GetInt32(3),
		RoomNumber = rdr.GetString(4)
	});
});

// GET /api/profile/diets?activeOnly=true
// Lists diets with limits; filters to active by default.
profileApi.MapGet("/diets", async (HttpRequest req, IConfiguration cfg) =>
{
	var activeOnly = bool.TryParse(req.Query["activeOnly"], out var b) ? b : true;
	var mealsPerDay = cfg.GetValue<int?>("DietDefaults:MealsPerDay") ?? 3;

	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();

	var sql = @"
        SELECT
            intDietID              AS Id,
            strDietName            AS Name,
            decSodiumLimitMG,
            intFluidsLimitML,
            decCarbsPerMealMin,
            decCarbsPerMealMax,
            decCalorieLimitKCal,
            decProteinLimitG,
            decFatLimitG
        FROM dbo.TDiets
        /**where**/
        ORDER BY strDietName;";

	sql = sql.Replace("/**where**/", activeOnly ? "WHERE bitIsActive = 1" : "");

	await using var cmd = new SqlCommand(sql, conn);
	await using var rdr = await cmd.ExecuteReaderAsync();

	var list = new List<object>();
	while (await rdr.ReadAsync())
	{
		// read with null safety
		decimal? sodium = rdr.IsDBNull(2) ? (decimal?)null : rdr.GetDecimal(2);
		int? fluids = rdr.IsDBNull(3) ? (int?)null : rdr.GetInt32(3);
		decimal? carbMin = rdr.IsDBNull(4) ? (decimal?)null : rdr.GetDecimal(4);
		decimal? carbMax = rdr.IsDBNull(5) ? (decimal?)null : rdr.GetDecimal(5);
		decimal? cal = rdr.IsDBNull(6) ? (decimal?)null : rdr.GetDecimal(6);
		decimal? protein = rdr.IsDBNull(7) ? (decimal?)null : rdr.GetDecimal(7);
		decimal? fat = rdr.IsDBNull(8) ? (decimal?)null : rdr.GetDecimal(8);

		list.Add(new
		{
			Id = rdr.GetInt32(0),
			Name = rdr.GetString(1),

			SodiumLimitMG = sodium,
			FluidsLimitML = fluids,
			CarbsPerMealMin = carbMin,
			CarbsPerMealMax = carbMax,

			// NEW daily caps
			CalorieLimitKCal = cal,
			ProteinLimitG = protein,
			FatLimitG = fat,

			// Helpful derived daily target so the UI can show a carbs bar
			CarbsPerDayMax = (carbMax.HasValue ? (decimal?)(carbMax.Value * mealsPerDay) : null)
		});
	}

	return Results.Ok(list);
});

// POST /api/profile/assign-diet
// Assigns a diet to the active admission by MRN via dbo.usp_Admission_AssignDiet_ByMRN.
profileApi.MapPost("/assign-diet", async (AssignDietDto dto) =>
{
	if (string.IsNullOrWhiteSpace(dto.MRN) || string.IsNullOrWhiteSpace(dto.DietName))
		return Results.BadRequest(new { error = "MRN and DietName are required" });

	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();
	using var cmd = new SqlCommand("dbo.usp_Admission_AssignDiet_ByMRN", conn)
	{ CommandType = CommandType.StoredProcedure };
	cmd.Parameters.AddWithValue("@MRN", dto.MRN.Trim());
	cmd.Parameters.AddWithValue("@DietName", dto.DietName.Trim());

	try { await cmd.ExecuteNonQueryAsync(); return Results.NoContent(); }
	catch (SqlException ex) when (ex.Number == 50000)
	{ return Results.BadRequest(new { error = ex.Message }); }
});



// GET /api/profile/allergies?mrn=MRN001
// Lists a patient's allergies (by MRN).
profileApi.MapGet("/allergies", async (string mrn) =>
{
	if (string.IsNullOrWhiteSpace(mrn))
		return Results.BadRequest(new { error = "mrn is required" });

	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();
	const string sql = @"
        SELECT al.intAllergenID, al.strAllergen
        FROM dbo.TPatientAllergies pa
        JOIN dbo.TPatients p  ON p.intPatientID = pa.intPatientID
        JOIN dbo.TAllergens al ON al.intAllergenID = pa.intAllergenID
        WHERE UPPER(LTRIM(RTRIM(p.strMRN))) = UPPER(@mrn)
        ORDER BY al.strAllergen;";
	await using var cmd = new SqlCommand(sql, conn);
	cmd.Parameters.AddWithValue("@mrn", mrn.Trim());

	await using var rdr = await cmd.ExecuteReaderAsync();
	var list = new List<object>();
	while (await rdr.ReadAsync())
		list.Add(new { Id = rdr.GetInt32(0), Name = rdr.GetString(1) });

	return Results.Ok(list);
});

// POST /api/profile/allergies
// Adds or updates a patient allergy (usp_Patient_AddAllergy).
profileApi.MapPost("/allergies", async (PatientAllergyDto dto) =>
{
	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();
	using var cmd = new SqlCommand("dbo.usp_Patient_AddAllergy", conn)
	{ CommandType = CommandType.StoredProcedure };
	cmd.Parameters.AddWithValue("@PatientID", dto.PatientID);
	cmd.Parameters.AddWithValue("@AllergenID", dto.AllergenID);
	cmd.Parameters.AddWithValue("@Reaction", (object?)dto.Reaction ?? DBNull.Value);
	cmd.Parameters.AddWithValue("@Severity", (object?)dto.Severity ?? DBNull.Value);
	await cmd.ExecuteNonQueryAsync();
	return Results.NoContent();
});

// DELETE /api/profile/allergies?patientId=1&allergenId=2
// Removes a patient allergy link (usp_Patient_RemoveAllergy).
profileApi.MapDelete("/allergies", async (int patientId, int allergenId) =>
{
	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();
	using var cmd = new SqlCommand("dbo.usp_Patient_RemoveAllergy", conn)
	{ CommandType = CommandType.StoredProcedure };
	cmd.Parameters.AddWithValue("@PatientID", patientId);
	cmd.Parameters.AddWithValue("@AllergenID", allergenId);
	await cmd.ExecuteNonQueryAsync();
	return Results.NoContent();
});

#endregion

// Returns the currently assigned diet for a patient's active admission.
// GET /api/profile/active-diet?mrn=MRN001
profileApi.MapGet("/active-diet", async (string mrn) =>
{
	if (string.IsNullOrWhiteSpace(mrn))
		return Results.BadRequest(new { error = "mrn is required" });

	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();

	const string sql = @"
;WITH ActiveAdmission AS (
  SELECT TOP (1) a.intAdmissionID, a.strDietOrder
  FROM dbo.TPatients p
  JOIN dbo.TAdmissions a ON a.intPatientID = p.intPatientID
  WHERE UPPER(LTRIM(RTRIM(p.strMRN))) = UPPER(LTRIM(RTRIM(@MRN)))
    AND a.bitActive = 1
    AND a.dtmDischarge IS NULL
  ORDER BY a.dtmAdmit DESC
)
SELECT TOP (1)
       COALESCE(d.strDietName, aa.strDietOrder) AS DietName,
       d.decCalorieLimitKCal                     AS CalorieLimitKCal,
       d.decSodiumLimitMG                        AS SodiumLimitMG,
       d.decProteinLimitG                        AS ProteinLimitG,
       d.decFatLimitG                            AS FatLimitG,
       d.decCarbsPerMealMax                      AS CarbsPerMealMax,
       CASE WHEN d.decCarbsPerMealMax IS NOT NULL THEN d.decCarbsPerMealMax * 3 END AS CarbsPerDayMax
FROM ActiveAdmission aa
LEFT JOIN dbo.TAdmissionDiets ad ON ad.intAdmissionID = aa.intAdmissionID
LEFT JOIN dbo.TDiets d           ON d.intDietID      = ad.intDietID;";

	await using var cmd = new SqlCommand(sql, conn);
	cmd.Parameters.AddWithValue("@MRN", mrn.Trim());

	await using var rdr = await cmd.ExecuteReaderAsync();
	if (!await rdr.ReadAsync())
		return Results.NotFound(new { error = "No active admission for MRN." });

	return Results.Ok(new
	{
		DietName = rdr["DietName"] as string,
		CalorieLimitKCal = rdr["CalorieLimitKCal"] as decimal?,  // null if no link
		SodiumLimitMG = rdr["SodiumLimitMG"] as decimal?,
		ProteinLimitG = rdr["ProteinLimitG"] as decimal?,
		FatLimitG = rdr["FatLimitG"] as decimal?,
		CarbsPerMealMax = rdr["CarbsPerMealMax"] as decimal?,
		CarbsPerDayMax = rdr["CarbsPerDayMax"] as decimal?
	});
});




// GET /api/profile/nutrition/summary?mrn=MRN001&from=2025-01-01T00:00:00Z&to=2025-01-02T00:00:00Z
profileApi.MapGet("/nutrition/summary", async (string mrn, DateTime? from, DateTime? to) =>
{
	if (string.IsNullOrWhiteSpace(mrn))
		return Results.BadRequest(new { error = "mrn is required" });

	// Default window = today (server local)
	var fromDt = from ?? DateTime.Today;
	var toDt = to ?? DateTime.Now;

	await using var conn = new SqlConnection(cs);
	await conn.OpenAsync();

	// Sum over non-cancelled orders in the window.
	const string sql = @"
        SELECT
            SUM(COALESCE(mi.decCalories,  0) * oi.intQty) AS Calories,
            SUM(COALESCE(mi.decSodiumMG,  0) * oi.intQty) AS SodiumMg,
            SUM(COALESCE(mi.decProteinG,  0) * oi.intQty) AS ProteinG,
            SUM(COALESCE(mi.decCarbsG,    0) * oi.intQty) AS CarbsG,
            SUM(COALESCE(mi.decFatG,      0) * oi.intQty) AS FatG,
            COUNT(DISTINCT o.intOrderID)                  AS OrderCount
        FROM dbo.TOrders o
        JOIN dbo.TAdmissions a   ON o.intAdmissionID = a.intAdmissionID
        JOIN dbo.TPatients   p   ON a.intPatientID   = p.intPatientID
        JOIN dbo.TOrderItems oi  ON o.intOrderID     = oi.intOrderID
        JOIN dbo.TMenuItems  mi  ON oi.intMenuItemID = mi.intMenuItemID
        JOIN dbo.TOrderStatus s  ON o.intOrderStatusID = s.intOrderStatusID
        WHERE UPPER(LTRIM(RTRIM(p.strMRN))) = UPPER(@mrn)
          AND o.dtmPlaced >= @from AND o.dtmPlaced < @to
          AND s.strStatus <> 'Cancelled';";

	await using var cmd = new SqlCommand(sql, conn);
	cmd.Parameters.AddWithValue("@mrn", mrn.Trim());
	cmd.Parameters.AddWithValue("@from", fromDt);
	cmd.Parameters.AddWithValue("@to", toDt);

	await using var rdr = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);
	if (!await rdr.ReadAsync())
		return Results.Ok(new { calories = 0, sodiumMg = 0, proteinG = 0, carbsG = 0, fatG = 0, orderCount = 0 });

	// Minimal API defaults to camelCase JSON (you’re using defaults now).
	var payload = new
	{
		calories = rdr.IsDBNull(0) ? 0 : Convert.ToDecimal(rdr[0]),
		sodiumMg = rdr.IsDBNull(1) ? 0 : Convert.ToDecimal(rdr[1]),
		proteinG = rdr.IsDBNull(2) ? 0 : Convert.ToDecimal(rdr[2]),
		carbsG = rdr.IsDBNull(3) ? 0 : Convert.ToDecimal(rdr[3]),
		fatG = rdr.IsDBNull(4) ? 0 : Convert.ToDecimal(rdr[4]),
		orderCount = rdr.IsDBNull(5) ? 0 : Convert.ToInt32(rdr[5])
	};

	return Results.Ok(payload);
});

// ============================================================================
// 13) App Run
//    - The hosting loop starts here. Declarations below are type definitions only.
// ============================================================================
#region 13) App Run

app.Run();

#endregion

#region
public class DbWarmupService : BackgroundService
{
	private readonly IConfiguration _config;

	public DbWarmupService(IConfiguration config)
	{
		_config = config;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		var cs = _config.GetConnectionString("DefaultConnection");

		while (!stoppingToken.IsCancellationRequested)
		{
			try
			{
				using var conn = new SqlConnection(cs);
				await conn.OpenAsync();
				using var cmd = new SqlCommand("SELECT 1", conn);
				await cmd.ExecuteScalarAsync();
			}
			catch
			{
			}

			await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
		}
	}
}
#endregion
// ============================================================================
// 14) DTOs / Records
//    - Contract types for request/response payloads.
//    - Left functionally identical; comments added for clarity.
// ============================================================================
#region 14) DTOs / Records

/// <summary>Single inventory quantity update.</summary>
public record InventoryUpdate(int Quantity);

/// <summary>Bulk inventory update patch item.</summary>
public record InventoryPatch(int Id, int Quantity);

/// <summary>User create request. NOTE: Password is plaintext (prototype).</summary>
public record UserDto(string UserName, string DisplayName, string Role, string Password, bool IsActive = true);

/// <summary>User update request (nullable fields enable partial update).</summary>
public record UserUpdateDto(string? UserName = null, string? DisplayName = null, string? Role = null, string? Password = null, bool? IsActive = null);

/// <summary>Patient create request.</summary>
public record PatientDto(string MRN, string FirstName, string LastName, DateTime DOB, string? Sex = null, string? Phone = null, string? Email = null);

/// <summary>Admission create request (links existing patient to a room).</summary>
public record AdmissionDto(int PatientID, int RoomID, string? DietOrder = null);

/// <summary>(Unused in endpoints here) Ingredient DTO placeholder.</summary>
public record IngredientDto(string IngredientNames);

/// <summary>Allergen create request (name).</summary>
public record AllergenDto(string AllergenName);

/// <summary>Order item (by menu item id) — not used by the /api/orders endpoint above.</summary>
public record OrderItemDto(int MenuItemID, int Quantity, string? SpecialInstructions = null);

/// <summary>Order create (by AdmissionID) — not used by current /api/orders.</summary>
public record OrderCreateDto(int AdmissionID, int OrderedByUserID, int MealPeriodID, DateTime RequestedFor, List<OrderItemDto> Items, string? Notes = null);

/// <summary>Order status change payload.</summary>
public record OrderStatusDto(int NewStatusID, int UserID, string? Comment = null);

/// <summary>Ingredient read model (example).</summary>
public readonly record struct IngredientView(int Id, string Name, decimal? Quantity);

/// <summary>Login request (prototype; plaintext password).</summary>
public record LoginDto(string UserName, string Password);

/// <summary>Create an ingredient (name + initial qty).</summary>
public record IngredientCreateDto(string Name, int Quantity);

/// <summary>Rename an ingredient.</summary>
public record IngredientRenameDto(string Name);

/// <summary>Attach ingredient IDs to a menu item (replaces links; max 6).</summary>
public record MenuIngrAttachDto(int[] IngredientIds);

/// <summary>Create a menu item via stored procedure.</summary>
public record MenuItemDto(string ItemName, string? Category, int? MealPeriodID, decimal? Calories, decimal? SodiumMG, decimal? ProteinG, decimal? CarbsG, decimal? FatG, bool LowSodium, bool? IsActive, string? ImagePath, string? CreatedBy);

/// <summary>Update a menu item (full payload expected by SQL UPDATE).</summary>
public sealed record MenuItemUpdateDto(string ItemName, string Category, int MealPeriodID, decimal Calories, decimal SodiumMG, decimal ProteinG, decimal CarbsG, decimal FatG, bool LowSodium, bool IsActive, string? ImagePath, string? CreatedBy);

/// <summary>Help-chat inbound message wrapper.</summary>
public record HelpIn(string? message);

/// <summary>Client-side order item (by name) used for TVP builder.</summary>
public sealed class PlaceOrderItem
{
	public string Name { get; set; } = "";
	public int Qty { get; set; } = 1;
	public string? SpecialInstr { get; set; }
}

/// <summary>Order request (by MRN + MealPeriodName) used by /api/orders.</summary>
public sealed class PlaceOrderRequest
{
	public string MRN { get; set; } = "";
	public string? OrderedByUserName { get; set; }
	public int? OrderedByUserID { get; set; }
	public string MealPeriodName { get; set; } = "";
	public DateTime? RequestedFor { get; set; }
	public string? Notes { get; set; }
	public List<PlaceOrderItem> Items { get; set; } = new();
}

/// <summary>Order placement response payload.</summary>
public sealed class PlaceOrderResponse
{
	public int OrderId { get; set; }
	public string RoomNumber { get; set; } = "";
	public DateTime RequestedFor { get; set; }
	public string Status { get; set; } = "Placed";
}

/// <summary>Cancel order payload (optional user + comment).</summary>
public record CancelOrderDto(int? UserID, string? Comment = null);

// =======================
// PROFILE API DTOs
// =======================

/// <summary>Partial patient profile update by MRN.</summary>
public record PatientProfileUpdateDto(
	string MRN,
	string? FirstName = null,
	string? LastName = null,
	string? Phone = null,
	string? Email = null
);

/// <summary>Assign a named diet by MRN.</summary>
public record AssignDietDto(
	string MRN,
	string DietName
);

/// <summary>Add patient allergy link (optional reaction/severity).</summary>
public record PatientAllergyDto(
	int PatientID,
	int AllergenID,
	string? Reaction = null,
	string? Severity = null
);



#endregion
