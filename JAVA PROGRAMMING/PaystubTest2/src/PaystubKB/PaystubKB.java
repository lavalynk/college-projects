package PaystubKB;

import java.io.BufferedReader;
import java.io.InputStreamReader;

/**
* Name: PaystubTest2
* Abstract:  This program will calculate the Gross Earnings, FICA tax, Federal Withheld
* and Net Amount of the payroll check for each employee of a company.
* @author Keith Brock
*/

public class PaystubKB {
	private static float[] dblGrossTotals = new float[10];	
	private static int intGrossCount = 0;
	static float dblTotalFederalTax = 0;	
	static float dblTotalNetIncome = 0;
	/**
	 * Default constructor for the PaystubKB class.
	 * Initializes an instance of PaystubKB with default settings.
	 */
	public PaystubKB() {
	    // Default constructor with no specific initialization
	}
		
	/**
	 * The main method serves as the entry point for the PaystubKB program.
	 * It calculates the Gross Earnings, FICA tax, Federal Withheld, and Net Amount 
	 * of payroll checks for each employee of a company.
	 * @param args None
	 */
	public static void main(String[] args) {
		String strEmployeeName = " ";
		float dblHourlyWage = 0;
		float dblHoursWorked = 0;
		int intWithholdings = 0;
		boolean blnLoop = true;
		float dblGrossEarnings = 0;
		float dblFederal = 0;
		String strMarital = " "; 
		double dblSocialSecurityTax = 0;
		double dblMedicare = 0;
		double dblNetIncome = 0;
		float dblTotalGrossIncome = 0;
		double dblTotalMedicareTax = 0;
		double dblTotalSocialSecurityTax = 0;
		double dblTotalFICATax = 0;

        System.out.println("Paystub Calculator--------------------------");
	    System.out.println("--------------------------------------------\n");
	    
	    while (blnLoop == true)
	    {
			System.out.print("Enter Employee Name (type 'Quit' to end program):  ");
			strEmployeeName = ReadStringFromUser();

	        if (strEmployeeName.toUpperCase().equals("QUIT")) 
	        {
	        	break;
	        }
	        
	        dblHourlyWage = GetValidHourlyWage();
	        dblHoursWorked = GetValidHoursWorked();
	        intWithholdings = GetValidExemptions();
	        strMarital = GetValidMaritalStatus();
	        System.out.println("");
		    System.out.println("--------------------------------------------");	
	        // Calculations
	        dblGrossEarnings = CalculateGross(dblHourlyWage, dblHoursWorked);
	        AddGrossTotal(dblGrossEarnings);
	        dblSocialSecurityTax = CalculateSocialSecurity(dblGrossEarnings);
	        dblFederal = CalculateFederal(dblGrossEarnings, intWithholdings, strMarital);
	        dblMedicare = CalculateMedicareTax(dblGrossEarnings);
	        dblNetIncome = CalculateNetIncome(dblGrossEarnings, dblMedicare, dblSocialSecurityTax, dblFederal);
	        
	        //Outputs
	        DisplayPaystub(strEmployeeName, dblGrossEarnings, dblMedicare, dblSocialSecurityTax, dblFederal, dblNetIncome);
	    }

	    // Calculate Grand Totals
	    dblTotalGrossIncome = CalcGrossTotal();
	    dblTotalSocialSecurityTax = CalculateSocialSecurity(dblTotalGrossIncome);
	    dblTotalMedicareTax = CalculateMedicareTax(dblTotalGrossIncome);
	    dblTotalFICATax = dblTotalSocialSecurityTax + dblTotalMedicareTax;
	    
	    // Display Final Outputs
	    DisplayGrandTotals(dblTotalGrossIncome, dblTotalMedicareTax, dblTotalSocialSecurityTax, dblTotalFICATax, dblTotalFederalTax, dblTotalNetIncome);	    	    
	}

	

	/**
	* Name: GetValidHourlyWage
	* Abstract: Gets Valid Hourly Wage from user.
	* @return Returns hourly wage.
	*/
	public static Float GetValidHourlyWage() 
	{
		Float dblWage = (float) 0.0;
		System.out.print("Hourly Wage: $");
		
		dblWage = ReadFloatFromUser();
		
		return dblWage;
	}

	
	/**
	* Name: GetValidHoursWorked
	* Abstract: Gets Valid Hours Worked from user.
	* @return Returns Hours Worked.
	*/	
	public static Float GetValidHoursWorked() {
		Float dblHours = (float) 0.0;
		System.out.print("Hours Worked: ");
		
		dblHours = ReadFloatFromUser();
		
		return dblHours;
	}	
	

	
	/**
	* Name: GetValidExemptions
	* Abstract: Gets Number of Withholdings
	* @return Returns Withholdings
	*/		
	public static int GetValidExemptions() {
		int intExemptions = -1;
		
		while (intExemptions < 0 || intExemptions > 10)
		{			
		System.out.print("Withholding Exemptions: ");
		intExemptions = ReadIntegerFromUser();
		
			if (intExemptions < 0 || intExemptions > 10)
			{
				System.out.println("Invalid.  Please try again!");
			}
		
		}				
		return intExemptions;
	}
	
	
	
	/**
	 * Name: GetValidMaritalStatus
	 * Abstract: Gets valid marital status from the user.
	 * @return Returns a valid marital status as a String ("Single" or "Married")
	 */
	public static String GetValidMaritalStatus() {
	    String strResponse = "";
	    String strResult = ""; 

	    while (strResult.isEmpty()) {
	        System.out.print("Enter Marital Status (Single/Married): ");
	        strResponse = ReadStringFromUser().trim();

	        if (strResponse.toUpperCase().equals("SINGLE") || strResponse.toUpperCase().equals("MARRIED")) {
	            strResult = strResponse; 
	        } else {
	            System.out.println("Invalid input. Please enter 'Single' or 'Married'.");
	        }
	    }

	    return strResult; 
	}

	
	
	/**
	 * Name: CalculateGross
	 * Abstract: Calculates the gross pay based on hourly wage and hours worked, including overtime pay at 1.5x for hours over 40.
	 * @param dblHourlyWage The hourly wage of the employee as a Float.
	 * @param dblHoursWorked The total hours worked by the employee as a Float.
	 * @return Returns the gross pay as a float, including 1.5x pay for hours worked over 40.
	 */
	public static float CalculateGross(Float dblHourlyWage, Float dblHoursWorked) {
	    float dblGrossPay = 0;

	    if (dblHoursWorked > 40) {
	        // Calculate regular pay for the first 40 hours
	        dblGrossPay = dblHourlyWage * 40;
	        // Calculate overtime pay for hours over 40 at 1.5x the hourly wage
	        dblGrossPay += (dblHoursWorked - 40) * dblHourlyWage * 1.5f;
	    } else {
	        // No overtime, calculate regular pay
	        dblGrossPay = dblHourlyWage * dblHoursWorked;
	    }

	    return dblGrossPay;
	}


	
	/**
	 * Name: CalculateSocialSecurity
	 * Abstract: Calculate Social Security
	 * @param dblGrossEarnings The gross earnings of the employee.
	 * @return Social Security Dollars
	 */	
	public static double CalculateSocialSecurity(float dblGrossEarnings) {	
		return dblGrossEarnings * 0.062;
	}
	
	
	
	/**
	 * Name: AddGrossTotal
	 * Abstract: Adds a gross total to the dblGrossTotals array, resizing if necessary.
	 * @param dblGross The gross pay to add.
	 */	
	public static void AddGrossTotal(float dblGross) {
	    int intIndex = 0; 

	    if (intGrossCount == dblGrossTotals.length) {
	        float[] dblNewGrossTotals = new float[dblGrossTotals.length + 1];
	        
	        // Manually copy elements from old array to new array
	        for (intIndex = 0; intIndex < dblGrossTotals.length; intIndex++) {
	            dblNewGrossTotals[intIndex] = dblGrossTotals[intIndex];
	        }

	        dblGrossTotals = dblNewGrossTotals; // Assign the new array to dblGrossTotals
	    }

	    // Add the new gross total to the array and increment the count
	    dblGrossTotals[intGrossCount] = dblGross;
	    intGrossCount += 1;
	}
	
	

	/**
	 * Name: CalcGrossTotal
	 * Abstract: Calculates the total gross earnings by summing all values in the dblGrossTotals array.
	 * @return Returns the total gross earnings as a float.
	 */
	public static float CalcGrossTotal() {
	    int intIndex = 0;
	    float dblGross = 0;
	    for (intIndex = 0; intIndex < intGrossCount; intIndex += 1) {
	        dblGross += dblGrossTotals[intIndex];
	    }
	    
	    return dblGross;
	}

	
	
	/**
	 * Name: CalculateWithholdings
	 * Abstract: Calculates the federal tax withheld based on adjusted gross income and marital status.
	 * @param dblGrossEarnings The gross earnings of the employee as a float.
	 * @param intWithholdings The number of withholding exemptions as an integer.
	 * @param strMaritalStatus The marital status of the employee ("Single" or "Married").
	 * @return Returns the calculated federal tax as a float.
	 */
	public static float CalculateFederal(float dblGrossEarnings, int intWithholdings, String strMaritalStatus) {
	    // Calculate Adjusted Gross Income by subtracting $55.77 for each exemption
	    float dblAdjustedGrossIncome = dblGrossEarnings - (intWithholdings * 55.77f);

	    // Ensure Adjusted Gross Income is not negative
	    if (dblAdjustedGrossIncome < 0) {
	        dblAdjustedGrossIncome = 0;
	    }

	    float dblFederalTax = 0.0f;

	    // Calculate federal tax based on marital status and adjusted gross income
	    if (strMaritalStatus.equalsIgnoreCase("Single")) {
	        if (dblAdjustedGrossIncome <= 50.99) {
	            dblFederalTax = 0.0f;
	        } else if (dblAdjustedGrossIncome <= 500.99) {
	            dblFederalTax = (dblAdjustedGrossIncome - 51) * 0.10f;
	        } else if (dblAdjustedGrossIncome <= 2500.99) {
	            dblFederalTax = 45.0f + ((dblAdjustedGrossIncome - 500) * 0.15f);
	        } else if (dblAdjustedGrossIncome <= 5000) {
	            dblFederalTax = 345.0f + ((dblAdjustedGrossIncome - 2500) * 0.20f);
	        } else { // Over $5000
	            dblFederalTax = 845.0f + ((dblAdjustedGrossIncome - 5000) * 0.25f);
	        }
	    } else if (strMaritalStatus.equalsIgnoreCase("Married")) {
	        if (dblAdjustedGrossIncome <= 50.99) {
	            dblFederalTax = 0.0f;
	        } else if (dblAdjustedGrossIncome <= 500.99) {
	            dblFederalTax = (dblAdjustedGrossIncome - 51) * 0.05f;
	        } else if (dblAdjustedGrossIncome <= 2500.99) {
	            dblFederalTax = 22.5f + ((dblAdjustedGrossIncome - 500) * 0.10f);
	        } else if (dblAdjustedGrossIncome <= 5000) {
	            dblFederalTax = 225.5f + ((dblAdjustedGrossIncome - 2500) * 0.15f);
	        } else { // Over $5000
	            dblFederalTax = 600.5f + ((dblAdjustedGrossIncome - 5000) * 0.20f);
	        }
	    }
	    dblTotalFederalTax += dblFederalTax;
	    
	    return dblFederalTax;
	}

	

	/**
	 * Name: CalculateNetIncome
	 * Abstract: Calculates the net income by subtracting Medicare, Social Security, and federal taxes from gross earnings.
	 * @param dblGrossEarnings The gross earnings of the employee as a float.
	 * @param dblMedicare The Medicare tax as a double.
	 * @param dblSocialSecurityTax The Social Security tax as a double.
	 * @param dblFederal The federal tax as a float.
	 * @return Returns the calculated net income as a double.
	 */
	public static double CalculateNetIncome(float dblGrossEarnings, double dblMedicare, double dblSocialSecurityTax,
	        float dblFederal) {
	    double dblNetIncome = 0;
	    
	    dblNetIncome = dblGrossEarnings - dblMedicare - dblSocialSecurityTax - dblFederal;
	    
	    dblTotalNetIncome += dblNetIncome;
	    
	    return dblNetIncome;
	}

	
	
	/**
	 * Name: CalculateMedicareTax
	 * Abstract: Calculates the Medicare tax based on the gross earnings.
	 * @param dblGrossEarnings The gross earnings of the employee as a float.
	 * @return Returns the Medicare tax as a double, calculated at 1.45% of gross earnings.
	 */
	public static double CalculateMedicareTax(float dblGrossEarnings) {
	    return dblGrossEarnings * 0.0145;
	}

	
	
	/**
	 * Name: DisplayPaystub
	 * Abstract: Displays the paycheck details for an employee, including gross income, deductions, and net income.
	 * @param strEmployeeName The name of the employee.
	 * @param dblGrossEarnings The gross earnings of the employee as a float.
	 * @param dblMedicare The Medicare tax amount as a double.
	 * @param dblSocialSecurityTax The Social Security tax amount as a double.
	 * @param dblFederal The federal tax amount as a float.
	 * @param dblNetIncome The net income after deductions as a double.
	 */
	private static void DisplayPaystub(String strEmployeeName, float dblGrossEarnings, double dblMedicare,
	                                   double dblSocialSecurityTax, float dblFederal, double dblNetIncome) {
	    System.out.println("Paycheck for " + strEmployeeName);
	    System.out.println("Total Gross Income:                   $" + String.format("%.2f", dblGrossEarnings));
	    System.out.println(" less Medicare Tax:                   $" + String.format("%.2f", dblMedicare));
	    System.out.println(" less Social Security Tax:            $" + String.format("%.2f", dblSocialSecurityTax));
	    System.out.println(" less FICA Tax:                       $" + String.format("%.2f", dblMedicare + dblSocialSecurityTax));
	    System.out.println(" less Federal Income Tax Withheld:    $" + String.format("%.2f", dblFederal));
	    System.out.println("Net Income:                           $" + String.format("%.2f", dblNetIncome));
	    System.out.println("--------------------------------------------");
	    System.out.println("");
	}

	
	
	/**
	 * Name: DisplayGrandTotals
	 * Abstract: Displays the grand totals for all employees, including total gross income, Medicare tax, Social Security tax, FICA tax, income tax withheld, and net income.
	 * @param dblTotalGrossIncome The total gross income for all employees as a float.
	 * @param dblTotalMedicareTax The total Medicare tax for all employees as a double.
	 * @param dblTotalSocialSecurityTax The total Social Security tax for all employees as a double.
	 * @param dblTotalFICATax The total FICA tax for all employees as a double.
	 * @param dblTotalFederalTax The total federal income tax withheld for all employees as a float.
	 * @param dblTotalNetIncome The total net income for all employees as a double.
	 */
	public static void DisplayGrandTotals(float dblTotalGrossIncome, double dblTotalMedicareTax, 
	                                       double dblTotalSocialSecurityTax, double dblTotalFICATax, 
	                                       float dblTotalFederalTax, double dblTotalNetIncome) {
	    System.out.println("Grand Paycheck Totals-----------------------");
	    System.out.println("--------------------------------------------");
	    System.out.println("Total Gross Income:             $" + String.format("%.2f", dblTotalGrossIncome));
	    System.out.println("Total Medicare Tax:             $" + String.format("%.2f", dblTotalMedicareTax));
	    System.out.println("Total Social Security Tax:      $" + String.format("%.2f", dblTotalSocialSecurityTax));	    
	    System.out.println("Total FICA Tax:                 $" + String.format("%.2f", dblTotalFICATax));
	    System.out.println("Total Income Tax Withheld:      $" + String.format("%.2f", dblTotalFederalTax));
	    System.out.println("Total Net Income:               $" + String.format("%.2f", dblTotalNetIncome));
	    System.out.println("--------------------------------------------");
	    System.out.println("--------------------------------------------");
	    System.out.println("");
	}

	
	
	
	/**
	* Name: ReadFloatFromUser
	* Abstract: Reads Float Input
	* @return The float value entered by the user.
	*/

	public static float ReadFloatFromUser() 
	{
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

	

	
	/**
	* Name: ReadIntegerFromUser
	* Abstract: Reads Integer Input
	* @return The integer value entered by the user.
	*/
	
	public static int ReadIntegerFromUser() {
	    int intValue = 0;
	    boolean blnValid = false;  // Add a flag to track valid input
	
	    while (blnValid == false) {  // Keep asking for input until valid
	        try {
	            String strBuffer = "";    
	
	            // Input stream
	            BufferedReader burInput = new BufferedReader(new InputStreamReader(System.in));
	
	            // Read a line from the user
	            strBuffer = burInput.readLine();
	            
	            // Convert from string to integer
	            intValue = Integer.parseInt(strBuffer);
	            blnValid = true;  // If parsing succeeds, set isValid to true to exit the loop
	        } 
	        catch (Exception excError) {
	            // If invalid input, display an error message and loop back to ask again
	            System.out.print("Invalid Input! Please try again.\n");
	        }
	    }
	
	    // Return integer value after valid input is entered
	    return intValue;
	}

	/**
	 * Method ReadStringFromUser
	 * Abstract: Reads a string from the user with validation.
	 * @return The string entered by the user.
	 */
	public static String ReadStringFromUser() {
	    String strBuffer = "";
	    boolean blnValid = false;  // Flag to track valid input

	    while (!blnValid) {  // Keep asking for input until valid
	        try {
	            // Input stream
	            BufferedReader burInput = new BufferedReader(new InputStreamReader(System.in));

	            // Read a line from the user
	            strBuffer = burInput.readLine();
	            
	            // Check if the input is non-empty (you can adjust this validation as needed)
	            if (!strBuffer.trim().isEmpty()) {
	                blnValid = true;  // If input is valid, set flag to true to exit the loop
	            } else {
	                System.out.print("Invalid Input! Please enter a non-empty string.\n");
	            }
	        } catch (Exception excError) {
	            // If an error occurs, display an error message and loop back to ask again
	            System.out.println("An error occurred. Please try again.");
	        }
	    }

	    // Return the valid string
	    return strBuffer;
	}

	
}		
