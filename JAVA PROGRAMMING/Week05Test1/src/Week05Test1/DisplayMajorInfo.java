package Week05Test1;

/**
 * Test 1
 * @author Keith Brock
 * @version 1.0
 */

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;
import java.sql.SQLException;

/**
 * Displays major records from the database.
 */
public class DisplayMajorInfo {
    /**
     * Displays major records from the database.
     * @param conn the database connection
     */
    public static void display(Connection conn) {
        System.out.println("Displaying Major Records:");
        loadListFromDatabase(conn, "TMajors");
    }

    /**
     * Loads major records from the specified table and prints them.
     * @param conn the database connection
     * @param strTable the table name to fetch records from
     */
    private static void loadListFromDatabase(Connection conn, String strTable) {
        try {
            if (conn == null) {
                System.out.println("Database connection failed.");
                return;
            }

            String strSelect = "SELECT * FROM " + strTable;
            Statement sqlCommand = conn.createStatement();
            ResultSet rstTSource = sqlCommand.executeQuery(strSelect);
            
            System.out.println("\n--- Data from " + strTable + " ---");
            int columnCount = rstTSource.getMetaData().getColumnCount();
            while (rstTSource.next()) {
                for (int intIndex = 1; intIndex <= columnCount; intIndex++) {
                    System.out.print(rstTSource.getString(intIndex) + " | ");
                }
                System.out.println();
            }
            System.out.println("");
            rstTSource.close();
            sqlCommand.close();
        } catch (IndexOutOfBoundsException e) {
            System.out.println("You have an issue with your index. Please contact level 1 Support for Project CS.");
        } catch (NullPointerException e) {
            System.out.println("The value passed is NULL – data needs to be fixed. Please contact level 1 Support for Project CS.");
        } catch (SQLException e) {
            System.out.println("Error loading table.");
            System.out.println("Error: " + e.getMessage());
        }
    }
}
