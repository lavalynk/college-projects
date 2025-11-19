package Week05Test1;

/**
 * Test 1
 * @author Keith Brock
 * @version 1.0
 */

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.sql.Connection;
import java.sql.SQLException;
import com.microsoft.sqlserver.jdbc.SQLServerDataSource;

/**
 * Main method that provides a menu for viewing student and major records.
 */

public class MainViewer {
    /**
     * Main method that provides a menu for viewing student and major records.
     * @param args command-line arguments (not used)
     */
    public static void main(String[] args) {
        Connection conn = null;
        try {
            conn = connectToDatabase();
            if (conn == null) {
                System.out.println("Failed to connect to the database. Exiting program.");
                return;
            }
            char chrChoice;
            do {
                System.out.println("Enter an option:");
                System.out.println("1 - View Student records");
                System.out.println("2 - View Major records");
                System.out.println("3 - View both Student and Major records");
                System.out.println("0 - Exit");
                System.out.print("Your choice: ");
                chrChoice = ReadCharacterFromUser();
                
                switch (chrChoice) {
                    case '1':
                        DisplayStudentInfo.display(conn);
                        break;
                    case '2':
                        DisplayMajorInfo.display(conn);
                        break;
                    case '3':
                        ViewStudentsMajors.displayBoth(conn);
                        break;
                    case '0':
                        System.out.println("Exiting program...");
                        break;
                    default:
                        System.out.println("Invalid choice. Please enter 1, 2, 3, or 0.");
                }
            } while (chrChoice != '0');
        } finally {
            closeDatabaseConnection(conn);
        }
        System.out.println("Processing Complete by Week05Test1");
    }

    /**
     * Establishes a connection to the SQL Server database.
     * @return the database connection, or null if connection fails
     */
    private static Connection connectToDatabase() {
        try {
            SQLServerDataSource sdsDataSource = new SQLServerDataSource();
            sdsDataSource.setServerName("localhost\\SQLExpress");
            sdsDataSource.setPortNumber(1433);
            sdsDataSource.setDatabaseName("dbHCM");
            sdsDataSource.setUser("sa");
            sdsDataSource.setPassword("Allison14!");
            Connection conn = sdsDataSource.getConnection();
            System.out.println("Connected to database!\n");
            return conn;
        } catch (SQLException excError) {
            System.out.println("Database connection issue in method connectToDatabase. Please contact Support for Project CS.");
            System.out.println("Error: " + excError.getMessage());
            return null;
        }
    }

    /**
     * Closes the database connection.
     * @param conn the database connection to close
     */
    private static void closeDatabaseConnection(Connection conn) {
        try {
            if (conn != null && !conn.isClosed()) {
                conn.close();
                System.out.println("\nDatabase connection closed...");
            }
        } catch (SQLException excError) {
            System.out.println("Error closing the database connection: " + excError.getMessage());
        }
    }

    /**
     * Reads a single character from user input.
     * @return the character inputted by the user
     */
    public static char ReadCharacterFromUser() {
        char chrCharacterInput = '4';
        try {
            BufferedReader burInput = new BufferedReader(new InputStreamReader(System.in));
            chrCharacterInput = (char) burInput.read();
        } catch (Exception excError) {
            System.out.println(excError.toString());
        }
        return chrCharacterInput;
    }
}