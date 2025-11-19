package CHomework4C;
/**
* 
* @author  Keith Brock
* @version 1.0
* @since   2024-09-10
*/

import java.io.*;

public class CHomework4C {
    public static void main(String[] args) {
        int[] array10 = new int[10];
        int[] array11 = new int[11];

        System.out.println("Length of array10: " + array10.length);
        System.out.println("Elements of array10 (indices 0 to " + (array10.length - 1) + "):");
        for (int i = 0; i < array10.length; i++) {
            array10[i] = i; // Assigning index number for clarity
            System.out.print(array10[i] + " ");
        }

        System.out.println("\n\nLength of array11: " + array11.length);
        System.out.println("Elements of array11 (indices 0 to " + (array11.length - 1) + "):");
        for (int i = 0; i < array11.length; i++) {
            array11[i] = i; // Assigning index number for clarity
            System.out.print(array11[i] + " ");
        }
       
    }



    /**
     * Method ReadStringFromUser
     * Abstract: Read a string from the user
     */
	public static String ReadStringFromUser( )
	{			  

		String strBuffer = "";	
		
		try
		{
			
			// Input stream
			BufferedReader burInput = new BufferedReader( new InputStreamReader( System.in ) ) ;

			// Read a line from the user
			strBuffer = burInput.readLine( );
	
		}
		catch( Exception excError )
		{
			System.out.println( excError.toString( ) );
		}
		
		// Return integer value
		return strBuffer;
	}
}