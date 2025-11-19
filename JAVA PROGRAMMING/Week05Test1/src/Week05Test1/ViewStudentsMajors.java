package Week05Test1;

/**
 * Test 1
 * @author Keith Brock
 * @version 1.0
 */

import java.sql.Connection;
/**
 * Displays both Student and Major records.
 */
public class ViewStudentsMajors {
    /**
     * Displays both Student and Major records.
     * @param conn the database connection
     */
    public static void displayBoth(Connection conn) {
        System.out.println("Displaying Both Student and Major Records:");
        DisplayStudentInfo.display(conn);
        DisplayMajorInfo.display(conn);
    }
}