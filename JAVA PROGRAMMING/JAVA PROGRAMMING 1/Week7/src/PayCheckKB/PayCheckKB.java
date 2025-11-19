package PayCheckKB;

import java.io.BufferedReader;
import java.io.InputStreamReader;

/**
* Name: PayCheckKB
* Abstract: Pay Check Calculator
* @author Keith Brock
*/

public class PayCheckKB
{
	/** 
	* Name: main
	* Abstract: This is where the program starts.
	*/
	public static void main( String astrCommandLine[ ] )
	{
		float sngHoursWorked = 1;
		float sngPay = 0;
		double dblGross = 0;
		double dblFederal = 0;
		double dblState = 0;
		double dblLocal = 0;
		double dblFICA = 0;
		double dblNet = 0;

		
		while (sngHoursWorked != 0)
		{
			sngHoursWorked = GetHours();
			if (sngHoursWorked > 0)
			{
				sngPay = GetPay();
			}
			if (sngPay != 0 && sngHoursWorked != 0)
			{
				dblGross = calculatePay(sngHoursWorked, sngPay);
				dblFederal = calculateTaxFederal(dblGross);
				dblState = calculateTaxState(dblGross);
				dblLocal = calculateTaxLocal(dblGross);
				dblFICA = calculateTaxFICA(dblGross);
				dblNet = GetNetPay(dblGross, dblFederal, dblState, dblLocal, dblFICA);
				displayOutputs(dblGross, dblFederal, dblState, dblLocal, dblFICA, dblNet);
			}
		}
		System.out.println("\nPay Check Calculator Program Ended\n");
	}

	
	// ----------------------------------------------------------------------
	// Name: GetHours
	// Abstract: Collects Number of Hours from User
	// ----------------------------------------------------------------------	
	
	public static float GetHours()
	{
		boolean blnValidated = false;
		float sngHoursWorked = 0;
		
		while (blnValidated == false)
		{
			System.out.print("Please enter number of hours worked or 0 to end program: ");
			sngHoursWorked = ReadFloatFromUser();
	        if (sngHoursWorked >= 0) 
	        {
	            blnValidated = true;
	        }
		}
		return sngHoursWorked;
	}

	
	// ----------------------------------------------------------------------
	// Name: GetPay
	// Abstract: Collects Pay Rate from User
	// ----------------------------------------------------------------------
	
	public static float GetPay()
	{
		boolean blnValidated = false;
		float sngPay = 0;
		
		while (blnValidated == false)
		{
			System.out.print("Please enter the payrate or 0 to end program: ");
			sngPay = ReadFloatFromUser();
	        if (sngPay >= 0) 
	        {
	            blnValidated = true;
	        }
		}	
		return sngPay;
	}
	
	
	// ----------------------------------------------------------------------
	// Name: calculatePay
	// Abstract: Calculates Pay
	// ----------------------------------------------------------------------	
	private static double calculatePay(float sngHoursWorked, float sngPayRate) {
	    double dblRegularPay = 0;
	    double dblOvertimePay = 0;
	    
	    if (sngHoursWorked > 40) {
	        dblRegularPay = 40 * sngPayRate;
	        dblOvertimePay = (sngHoursWorked - 40) * sngPayRate * 1.5;
	    } else {
	        dblRegularPay = sngHoursWorked * sngPayRate;
	    }

	    double dblGrossPay = dblRegularPay + dblOvertimePay;
	    
	    return Math.round(dblGrossPay * 100.0) / 100.0;
	}


	
	// ----------------------------------------------------------------------
	// Name: calculateTaxFederal
	// Abstract: Calculates Federal Tax
	// ----------------------------------------------------------------------	
	private static double calculateTaxFederal(double dblGross) {
	    return Math.round(dblGross * 0.25 * 100.0) / 100.0;
	}

	// ----------------------------------------------------------------------
	// Name: calculateTaxState
	// Abstract: Calculates State Tax
	// ----------------------------------------------------------------------	
	private static double calculateTaxState(double dblGross) {
	    return Math.round(dblGross * 0.07 * 100.0) / 100.0;
	}

	// ----------------------------------------------------------------------
	// Name: calculateTaxLocal
	// Abstract: Calculates Local Tax
	// ----------------------------------------------------------------------	
	private static double calculateTaxLocal(double sngGross) {
	    return Math.round(sngGross * 0.025 * 100.0) / 100.0;
	}

	// ----------------------------------------------------------------------
	// Name: calculateTaxFICA
	// Abstract: Calculates FICA Tax
	// ----------------------------------------------------------------------	
	private static double calculateTaxFICA(double sngGross) {
	    return Math.round(sngGross * 0.0475 * 100.0) / 100.0;
	}

	// ----------------------------------------------------------------------
	// Name: GetNetPay
	// Abstract: Calculates Net Pay
	// ----------------------------------------------------------------------
	private static double GetNetPay(double sngGross, double dblFederal, double dblState, double dblLocal,
	        double dblFICA) {
	    return Math.round((sngGross - dblFederal - dblState - dblLocal - dblFICA) * 100) / 100.0;
	}

	
	// ----------------------------------------------------------------------
	// Name: displayOutputs
	// Abstract: Displays Outputs
	// ----------------------------------------------------------------------	
	private static void displayOutputs(double sngGross, double dblFederal, double dblState, double dblLocal,
			double dblFICA, double dblNet) {
		System.out.println("Your Gross Pay is---> \t$" + sngGross);
		System.out.println("Federal is----------> \t$" + dblFederal);
		System.out.println("State Tax is--------> \t$" + dblState);
		System.out.println("Local Tax is--------> \t$" + dblLocal);
		System.out.println("FICA Tax is---------> \t$" + dblFICA);
		System.out.println("------------------------------");
		System.out.println("Net Pay is----------> \t$" + dblNet);
		System.out.print("\n");
		
	}	
	
	public static float ReadFloatFromUser() {
	    float sngValue = 0;
	    boolean blnValid = false;  // Add a flag to track valid input

	    while (blnValid == false) {  // Keep asking for input until valid
	        try {
	            String strBuffer = "";    

	            // Input stream
	            BufferedReader burInput = new BufferedReader(new InputStreamReader(System.in));

	            // Read a line from the user
	            strBuffer = burInput.readLine();
	            
	            // Convert from string to float
	            sngValue = Float.parseFloat(strBuffer);
	            blnValid = true;  // If parsing succeeds, set isValid to true to exit the loop
	        } 
	        catch (Exception excError) {
	            // If invalid input, display an error message and loop back to ask again
	            System.out.print("Invalid Input! Please try again.\n");
	        }
	    }

	    // Return float value after valid input is entered
	    return sngValue;
	}
}	