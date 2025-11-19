package Test1Practical;
/**
* 
* 
*
* @author  Keith Brock
* @version 1.0
* @since   2024-09-10
*/

import java.io.*;

public class Test1Practical{
	/** This is Java's main method entry point of this program.
	 *  The program uses constants and variables.
	 */
	public static void main( String astrCommandLine[] ) 	{
		//Declaring Variables
		int intLoan = 0;
		int intYears = 0;
		float sngInterest = 0;
		float sngMonthlyRate = 0;
		float sngMonthlyInterest = 0;
		float sngPrincipal = 0;
		float sngMonthlyPayment = 0;
		float sngTotalPayment = 0;
		int intTotalMonths = 0;
		int intInput = 0;
		boolean blnValidated = false;
		
		System.out.print("Keith Brock - Loan Amortization Schedule\n");
		System.out.print("Class - JAVA PROGRAMMING 1(161-400)\n\n");
		
		//Validation
		while (blnValidated == false)
		{
			System.out.print("Enter Loan Amount: \n");
			intLoan = ReadIntegerFromUser();
            if (intLoan > 0) 
            {
                blnValidated = true;
            }
		}
		
		blnValidated = false;
		
		while (blnValidated == false)
		{
			System.out.print("Enter Number of Years: \n");
			intYears = ReadIntegerFromUser();
			if (intYears > 0)
			{
				blnValidated = true;
			}
		}

		blnValidated = false;
		
		while (blnValidated == false)
		{
			System.out.print("Enter Annual Interest Rate: \n ");
			sngInterest = ReadFloatFromUser();			
			if (sngInterest > 0)
			{
				blnValidated = true;
			}
		}
		//End of Validation and Input
		
		//Total amount of months.
		intTotalMonths = (intYears * 12);
		
		// Calculate monthly interest rate by dividing the annual interest rate by 1200		
		sngMonthlyRate = sngInterest/1200;
		
		// Calculate monthly payment = loan amount * monthly rate / ( 1 - 1 / Math.pow(1 + monthlyRate, years * 12));

		sngMonthlyPayment = (float) (intLoan * sngMonthlyRate / (1-1/Math.pow(1+ sngMonthlyRate,  intYears * 12)));
		
		// Calculate total payment = total = months * monthly payment
		sngTotalPayment = (float) (intTotalMonths * sngMonthlyPayment);		
		
		//Display Monthly Payment
		System.out.printf("Monthly payment: %.2f\n", sngMonthlyPayment);
		
		//Display Total Payment
		System.out.printf("Total Payment: %.2f\n", sngTotalPayment);
		
		//Header for the amortization schedule
		System.out.print("Payment #\t Interest\t Principal \t Balance\n");
					
		// Create amortization schedule with the Payment#, Interest, Principal, and Balance			
		for (intInput = 1; intInput <= intTotalMonths; intInput += 1)
		{
			sngMonthlyInterest = sngTotalPayment * sngMonthlyRate;
			sngPrincipal = sngMonthlyPayment - sngMonthlyInterest;
			sngTotalPayment -= sngMonthlyInterest + sngPrincipal;
			//I was experimenting a different way to tab here.  Felt this was more consistent than \t.
			System.out.printf("%-16d %-15.2f %-15.2f %-11.2f\n", intInput,sngMonthlyInterest, sngPrincipal, sngTotalPayment);
			//System.out.printf(intInput +"\t\t %.2f \t\t %.2f \t %.2f\n",sngMonthlyInterest, sngPrincipal, sngTotalPayment);
		}
		
	}
	
	
	/**
	 * Method ReadFloatFromUser
	 * Abstract: Read a float from the user
	 */
	public static float ReadFloatFromUser( )
	{

		float sngValue = 0;

		try
		{
			String strBuffer = "";	

			// Input stream
			BufferedReader burInput = new BufferedReader( new InputStreamReader( System.in ) ) ;

			// Read a line from the user
			strBuffer = burInput.readLine( );
			
			// Convert from string to float
			sngValue = Float.parseFloat( strBuffer );
		}
		catch( Exception excError )
		{
			//I changed these to show this message if an invalid input is put in instead of the error message.
			System.out.print("Invalid Input!  Please try again.\n");
		}
		

		// Return float value
		return sngValue;
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
			//I changed these to show this message if an invalid input is put in instead of the error message.			
			System.out.print("Invalid Input!  Please try again.\n");
		}
		

		// Return integer value
		return intValue;
	}
}	