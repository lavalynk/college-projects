package CHomework4B;

/**
* 
* 
*
* @author  Keith Brock
* @version 1.0
* @since   2024-09-10
*/

import java.io.*;

public class CHomework4B{
	/** This is Java's main method entry point of this program.
	 *  The program uses constants and variables.
	 */
	public static void main( String astrCommandLine[] ) 	{
			//Declare Variables
		
			int intInput = 0;
			int intRange = 0;
			
			// --------------------------------------------------------------------------------
			// Read in an integer value from the user and print out if the number is even or odd.
			// --------------------------------------------------------------------------------
			System.out.print("Please enter a whole number: ");
			intInput = ReadIntegerFromUser();
			
			if (intInput % 2 == 0)
			{
				System.out.print("The number " + intInput + " is even.\n");
				System.out.print("\n");				
			}
			else {
				System.out.print("The number " + intInput + " is odd.\n");
				System.out.print("\n");
			}
			
			// --------------------------------------------------------------------------------
			// Read in an integer value from the user from 1 and 100.  Loop until a value in that
			// range is entered.  After a number in that range is entered print it out.
			// --------------------------------------------------------------------------------

			
			while (intRange < 1 || intRange > 100)				
			{
				System.out.print("Please enter a whole number from 1 to 100: ");
				intRange = ReadIntegerFromUser();	
	            // Check if the input is out of the valid range
	            if (intRange < 1 || intRange > 100) 
	            {
	                System.out.println("Invalid input.  Try Again.\n");
	            }
			}
			
			System.out.print("You entered a valid number: " + intRange);


			

	}
	/**
	 * Method ReadIntegerFromUser
	 * Abstract: Read an integer from the user
	 */
	public static int ReadIntegerFromUser( )
	{

		int intValue = 0;

		try
		{
			String strBuffer = "";	

			// Input stream
			BufferedReader burInput = new BufferedReader( new InputStreamReader( System.in ) ) ;

			// Read a line from the user
			strBuffer = burInput.readLine( );
			
			// Convert from string to integer
			intValue = Integer.parseInt( strBuffer );
		}
		catch( Exception excError )
		{
			System.out.println( excError.toString( ) );
		}
		
		// Return integer value
		return intValue;
	}

}
