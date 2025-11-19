package PizzaSalesKB;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.text.NumberFormat;
import java.util.Locale;

/**
* Name: PizzaSalesKB
* Abstract: This program calculates total pizza sales, highest and lowest sale,
* average sale, and commission per employee.
* @author Keith Brock
*/

public class PizzaSalesKB {
	/**
	 * Default Constructor for the PizzaSalesKB Class.  It wouldn't let me create the JavaDoc without this...  I just don't remember in the videos...will revisit.
	 */
	public PizzaSalesKB() {
		
	}
	
	/** 
	* Name: main
	* Abstract: This is where the program starts.
	* 
	* @param args Command-line arguments (not used in this program).	* 
	*/
	public static void main(String[] args)
	{
		int intNumberOfSales = 0;
		int intIndex = 0;
		float dblAverage = 0;
		float dblTotalSales = 0;
		float dblHighest = 0;
		float dblLowest = 0;
		int intEmployeeCount = 0;
		float dblTotalCommission = 0;
		float dblEmployeeCommission = 0;
		
		System.out.print("Enter the total number of sales for the day: ");
		intNumberOfSales = ReadIntegerFromUser();	
		
		float[] dblSales = new float[intNumberOfSales];
		
		// Loop to enter each sale amount
		for (intIndex = 0; intIndex < intNumberOfSales; intIndex++)
		{
			System.out.print("Enter ticket #" + (intIndex + 1) + " amount: ");
			dblSales[intIndex] = ReadFloatFromUser();
		}
		
		System.out.print("How many employees worked today?: ");
		intEmployeeCount = ReadIntegerFromUser();
		System.out.print("\n");
		
		dblTotalSales = TotalSales(dblSales);
		dblHighest = HighestSale(dblSales);
		dblLowest = LowestSale(dblSales);
		dblAverage = AverageSales(dblSales);
		
		dblTotalCommission = TotalCommission(dblTotalSales);
		dblEmployeeCommission = EmployeeCommission(dblTotalCommission, intEmployeeCount);
		
		DisplayOutputs(dblTotalSales, dblHighest, dblLowest, dblAverage, dblTotalCommission, dblEmployeeCommission);
	}
	


	/**
	 * Name: TotalSales
	 * Abstract: This adds all of the sales together to get the total sales.
	 * 
	 * @param dblSales An array of float values representing sales.
	 * @return The total sales for the day.
	 */
	public static float TotalSales(float[] dblSales) {
	    float dblTotalSales = 0;
	    int intIndex = 0;
	    
	    for (intIndex = 0; intIndex < dblSales.length; intIndex += 1) {
	        dblTotalSales += dblSales[intIndex];
	    }
	    
	    return dblTotalSales;
	}


	/**
	* Name: AverageSales
	* Abstract: Takes the Total Sales and Divides it by the number of checks.
	* @param dblSales An array of float values representing sales. 
	* @return Average Sales
	*/		
	public static float AverageSales(float[] dblSales) {
		float dblTotalSales = 0;
		float dblAverageSales = 0;
		int intIndex = 0;
		
		for (intIndex = 0; intIndex < dblSales.length; intIndex += 1)
		{
			dblTotalSales += dblSales[intIndex];
		}
		
		dblAverageSales = dblTotalSales/ dblSales.length;
		
		return dblAverageSales;
	}
	
	/**
	* Name: HighestSale
	* Abstract: Finds the highest check amount for the day.
	* @param dblSales An array of float values representing sales.
	* @return Returns highest check amount.
	*/		
	public static float HighestSale(float[] dblSales) {
		float dblHighestSale = dblSales[0];
		int intIndex = 0;
		
		for (intIndex = 1; intIndex < dblSales.length; intIndex += 1)
		{
			if (dblSales[intIndex] > dblHighestSale)
			{
				dblHighestSale = dblSales[intIndex];
			}
		}			
		return dblHighestSale;
	}	
	

	/**
	* Name: LowestSale
	* Abstract: Finds the lowest check amount for the day.
	* @param dblSales An array of float values representing sales.	
	* @return Returns lowest check amount.
	*/		
	public static float LowestSale(float[] dblSales) {
		float dblLowestSale = dblSales[0];
		int intIndex = 0;
		
		for (intIndex = 1; intIndex < dblSales.length; intIndex += 1)
		{
			if (dblSales[intIndex] < dblLowestSale)
			{
				dblLowestSale = dblSales[intIndex];
			}
		}			
		return dblLowestSale;
	}	
	
	/**
	 * Name: TotalCommission
	 * Abstract: Calculates the total commission based on total sales.
	 * 
	 * @param dblTotalSales The total sales for the day.
	 * @return The total commission, which is 2% of the total sales.
	 */
	public static float TotalCommission(float dblTotalSales) {		
	    return (float) (dblTotalSales * 0.02);
	}	
	
	
	/**
	 * Name: EmployeeCommission
	 * Abstract: Calculates the employee commission based on the total commission divided by the employee count.
	 * 
	 * @param dblTotalCommission The total commission for the day.
	 * @param intEmployeeCount The number of employees working that day.
	 * @return The commission amount each employee will receive.
	 */
	public static float EmployeeCommission(float dblTotalCommission, int intEmployeeCount) {
	    return (float) dblTotalCommission / intEmployeeCount;
	}
	
	
	/**
	* Name: DisplayOutputs
	* Abstract: Display All Outputs
	* @param dblTotalSales The total sales for the day.
	* @param dblHighest The highest sale for the day.
	* @param dblLowest The lowest sale for the day.
	* @param dblAverage The average sale amount.
	* @param dblTotalCommission The total commission for all employees.
	* @param dblEmployeeCommission The commission each employee receives. 
	*/		
	public static void DisplayOutputs(float dblTotalSales, float dblHighest, float dblLowest, float dblAverage,
			float dblTotalCommission, float dblEmployeeCommission) {
		NumberFormat currencyFormat = NumberFormat.getCurrencyInstance(Locale.US); 
		System.out.println("-----------------------------------------------");
		System.out.println("Total Sales for the Day         : " + currencyFormat.format(dblEmployeeCommission));
		System.out.println("Highest Sale for the Day        : " + currencyFormat.format(dblHighest));
		System.out.println("Lowest Sale for the Day         : " + currencyFormat.format(dblLowest));
		System.out.println("Average Sale Amount for the Day : " + currencyFormat.format(dblAverage));
		System.out.println("Total Employee Commission is    : " + currencyFormat.format(dblTotalCommission));
		System.out.println("Per Employee Commission Amount  : " + currencyFormat.format(dblEmployeeCommission));
		System.out.println("-----------------------------------------------");		
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

}		
