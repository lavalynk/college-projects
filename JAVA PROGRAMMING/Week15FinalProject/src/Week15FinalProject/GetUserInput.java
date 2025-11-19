package Week15FinalProject;

import java.io.BufferedReader;
import java.io.InputStreamReader;

/**
 * The GetUserInput class prompts the user to view or modify customer records.
 * It uses BufferedReader for input and delegates actions to ViewCustomers or ModifyCustomers.
 * 
 * @author Keith Brock
 * @version 1.0
 */
public class GetUserInput {
    /**
     * Default constructor for GetUserInput.
     */
    public GetUserInput() {
        // No initialization needed
    }
    
	/**
     * Entry point to prompt user for an option using custom buffered input method.
     * @param args Command-line arguments (not used in this program)
     */
    public static void main(String[] args) {
        try {
            System.out.println("Please select an option:");
            System.out.println("1 - View Customers");
            System.out.println("2 - Create Modified Customers File");
            System.out.print("Enter your choice: ");

            String strChoice = ReadStringFromUser();
            int intChoice = Integer.parseInt(strChoice);

            switch (intChoice) {
                case 1:
                    ViewCustomers.main(null);
                    break;
                case 2:
                    ModifyCustomers.main(null);
                    break;
                default:
                    System.out.println("Invalid choice. Please enter 1 or 2.");
            }
        } catch (Exception ex) {
            System.out.println("Input error: " + ex.getMessage());
        }
    }

    /**
     * Reads a line of text from the user using BufferedReader.
     * 
     * @return The string entered by the user.
     */
    public static String ReadStringFromUser() {
        String strBuffer = "";
        try {
            BufferedReader burInput = new BufferedReader(new InputStreamReader(System.in));
            strBuffer = burInput.readLine();
        } catch (Exception excError) {
            System.out.println(excError.toString());
        }
        return strBuffer;
    }
}
