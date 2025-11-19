package CHomework2B;
/**
* The MyFirstPgm program implements an application that
* simply displays constants and variables to the standard output.
*
* @author  Keith Brock
* @version 1.0
* @since   2024-09-03
*/

public class CHomework2B {
	/** This is Java's main method entry point of this program.
	 *  The program uses constants and variables.
	 */
	public static void main( String astrCommandLine[] ) {
		
		// this is for short
		
		// Constants/Variables
		short shtMinimum = 0;
		short shtMaximum = 0;
		
		//set to min/max value
		shtMinimum = -32768;
		shtMaximum = +32767;
		
		//print min/max value
		System.out.print("Short Data Type--------------------------\n");
		System.out.print("\tMinimum: " + shtMinimum + "\n");
		System.out.print("\tMaximum: " + shtMaximum + "\n");
		System.out.print("\n");
		
		//confirm by subtracting and add one
		//The short data type rolls over to the start/end of the numbers.  Minimum switches actually to the maximum, and maximum switches to minimum.
		shtMinimum -= 1;
		shtMaximum +=1;
		System.out.print("Confirmation:\n");
		System.out.print("\tMinimum: " + shtMinimum + "\n");
		System.out.print("\tMaximum: " + shtMaximum + "\n");
		System.out.print( "\n");		
		
		// this is for integer
		// declare variables
		int intMinimum = 0;
		int intMaximum = 0;
		
		// Set to min/max value
		intMinimum = -2147483648;
		intMaximum = 2147483647;
		// Print min/max values
		System.out.print( "Integer Data Type--------------------------\n");
		System.out.print( "\tMinimum: " + intMinimum + "\n" );
		System.out.print( "\tMaximum: " + intMaximum + "\n" );
		System.out.print( "\n" );
		
		// Confirm by subtracting and adding one
		//The Integer data type rolls over to the start/end of the numbers.  Minimum switches actually to the maximum, and maximum switches to minimum.		
		intMinimum -= 1;
		intMaximum += 1;
		
		System.out.print("Confirmation:\n");
		System.out.print("\tMinimum: " + intMinimum + "\n" );
		System.out.print("\tMaximum: " + intMaximum + "\n" );
		System.out.print( "\n" );
		
		// this is for long
		
		//declare variables
		long lngMinimum = 0;
		long lngMaximum = 0;
		
		// Set to min/max value
		lngMinimum = -9223372036854775808L;
		lngMaximum = 9223372036854775807L;
		
		// Print min/max values
		System.out.print( "Long Data Type--------------------------\n");
		System.out.print( "\tMinimum: " + lngMinimum + "\n");
		System.out.print( "\tMaximum: " + lngMaximum + "\n");
		System.out.print( "\n" );
		
		// Confirm by subtracting and adding one
		// The Long data type rolls over to the start/end of the numbers.  Minimum switches actually to the maximum, and maximum switches to minimum.				
		lngMinimum -= 1;
		lngMaximum += 1;
		
		System.out.print("Confirmation:\n");		
		System.out.print( "\tMinimum: " + lngMinimum + "\n");
		System.out.print( "\tMaximum: " + lngMaximum + "\n");
		System.out.print( "\n");
		
		// this is for float
		
		//declare variables
		float sngMinimum = 0;
		float sngMaximum = 0;
		
		// Set to min/max value
		
        sngMinimum = -3.40282335E38f;
        sngMaximum = 3.4028235E38f;
		
		// Print min/max values
        System.out.print("Float Data Type--------------------------\n");
        System.out.print("\tMinimum: " + sngMinimum + "\n");
        System.out.print("\tMaximum: " + sngMaximum + "\n");
        System.out.print("\n");
		
        
        // The Float data type results in Infinity when you add a very large number to it.
        sngMinimum -= 3.4E38f;
        sngMaximum += 3.4E38f;
		
        System.out.print("Confirmation:\n");
        System.out.print("\tMinimum: " + sngMinimum + "\n");  // Expecting -Infinity
        System.out.print("\tMaximum: " + sngMaximum + "\n");  // Expecting Infinity
        System.out.print("\n");
								
	}
}