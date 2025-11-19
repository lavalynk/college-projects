package DBProcess;

//Import for SQL Connection, DriverManager, ResultSet, Statement
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.Statement;

/**
* Abstract: Displays records from the specified table in the dbHCM database.
* @author Keith Brock
* @version 1.0
*/
public class DBProcessPart1 {

 // Define the Connection
 private static Connection m_conAdministrator;

 /**
  * Main method: Connect to the database and display records on the console.
  */
 public static void main(String[] args) {
     // Check for missing arguments
     if (args.length < 3) {
         System.out.println("Since you did not pass your table, primary key, and column – cannot process.");
         return;
     }

     // Read arguments
     String table = args[0];
     String primaryKey = args[1];
     String column = args[2];

     try {
         // Can we connect to the database?
         if (OpenDatabaseConnection()) {
             // Yes, load the list
             LoadListFromDatabase(table, primaryKey, column);
         } else {
             // No, warn the user ...
             System.out.println("Error loading the table.");
         }
         System.out.println("Yes we processed the list!");
     } catch (Exception e) {
         System.out.println("An I/O error occurred: " + e.getMessage());
     } finally {
         CloseDatabaseConnection();
     }
 }

 /** 
  * Name: LoadListFromDatabase
  * This will load the list from the specified table and display it in the required format.
  */
 public static boolean LoadListFromDatabase(String strTable, String strPrimaryKeyColumn, String strNameColumn) {
	    boolean blnResult = false;

	    try {
	        // Build the SQL query
	        String strSelect = "SELECT [" + strPrimaryKeyColumn + "], [" + strNameColumn + "] FROM [" + strTable + "]";
	        //System.out.println("Executing query: " + strSelect);

	        // Create and execute the SQL command
	        Statement sqlCommand = m_conAdministrator.createStatement();
	        ResultSet rstTSource = sqlCommand.executeQuery(strSelect);

	        // Loop through all records and display results
	        while (rstTSource.next()) {
	            int intID = rstTSource.getInt(strPrimaryKeyColumn);
	            String strName = rstTSource.getString(strNameColumn);

	            // Display each record
	            System.out.printf("Table: %s ID: %d %s: %s%n", strTable, intID, strNameColumn, strName);
	        }
	        System.out.println("");
	        // Clean up
	        rstTSource.close();
	        sqlCommand.close();
	        blnResult = true;
	    } catch (Exception e) {
	        System.out.println("Error loading table.");
	        System.out.println("Error: " + e.getMessage());
	    }

	    return blnResult;
	}



 /**
  * Name: OpenDatabaseConnection
  * Opens a connection to the MS Access database.
  * @return True if connection is successful, otherwise false.
  */
 public static boolean OpenDatabaseConnection() {
     boolean blnResult = false;

     try {
         String connectionString = "jdbc:ucanaccess://" + System.getProperty("user.dir") + "/Database/dbHCM.accdb";
         m_conAdministrator = DriverManager.getConnection(connectionString);
         blnResult = true;
         System.out.println("Connected to database!");
         System.out.println("");
     } catch (Exception excError) {
         System.out.println("Cannot connect - error: " + excError.getMessage());
         System.out.println("Ensure the UCanAccess drivers are installed.");
     }

     return blnResult;
 }

 /**
  * Name: CloseDatabaseConnection
  * Abstract: Close the connection to the database.
  * @return True if connection is closed successfully, otherwise false.
  */
 public static boolean CloseDatabaseConnection() {
     boolean blnResult = false;

     try {
         // Is there a connection object?
         if (m_conAdministrator != null) {
             // Yes, close the connection if not closed already
             if (!m_conAdministrator.isClosed()) {
                 m_conAdministrator.close();
                 System.out.println(" ");                 
                 System.out.println("Database connection closed...");
                 m_conAdministrator = null; 
             }
         }
         blnResult = true;
     } catch (Exception excError) {
         System.out.println("Error closing the database connection: " + excError.getMessage());
     }

     return blnResult;
 }
}
