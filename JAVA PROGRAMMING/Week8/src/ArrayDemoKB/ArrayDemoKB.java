package ArrayDemoKB;

import java.io.BufferedReader;
import java.io.InputStreamReader;

/**
* Name: ArrayDemoKB
* Abstract: Array Demo Program
* @author Keith Brock
*/

public class ArrayDemoKB {
	/** 
	* Name: main
	* Abstract: This is where the program starts.
	*/
	public static void main(String[] astrCommandLine) 
	{
		// #1- Declare Array		
		int aintValues[];
		// #2 - Array Size
		aintValues = MakeArray();
		// #3 - Populate Array
		System.out.println("--------------------------------------");			
		System.out.println("Populate Array");
		System.out.println("--------------------------------------");
		PopulateArray(aintValues);
		System.out.println("--------------------------------------");			
		System.out.println("Print Array");		
		System.out.println("--------------------------------------");		
		// #4 - Print Array
		PrintArray(aintValues);
		System.out.println("--------------------------------------");			
		System.out.println("Average of Array");		
		System.out.println("--------------------------------------");		
		// #5 - Average of Array
		AverageArray(aintValues);
		System.out.println("--------------------------------------");			
		System.out.println("Maximum Value");		
		System.out.println("--------------------------------------");		
		// #6 - Calculate Maximum Value
		MaxArray(aintValues);
		System.out.println("--------------------------------------");			
		System.out.println("Add Value to End of Array");		
		System.out.println("--------------------------------------");		
		// #6 - Add Value to Back of Array 
		aintValues = AddValueToArray(aintValues, 5);				
		PrintArray(aintValues);		
		System.out.println("--------------------------------------");			
		System.out.println("Add Value to Front of Array");		
		System.out.println("--------------------------------------");		
		// #7 - Add Value to Front of Array
		aintValues = AddValueToFrontArray(aintValues, 10);		
		PrintArray(aintValues);
		System.out.println("--------------------------------------");			
		System.out.println("Insert Value Into Array");		
		System.out.println("--------------------------------------");
		aintValues = InsertValueIntoArray(aintValues, 42, 2);
		PrintArray(aintValues);		
		System.out.println("--------------------------------------");			
		System.out.println("Remove Value from Array");		
		System.out.println("--------------------------------------");
		aintValues = RemoveValueFromArray(aintValues, 2);
		PrintArray(aintValues);
		System.out.println("--------------------------------------");
		
	}
	

	// ----------------------------------------------------------------------
	// Name: MakeArray
	// Abstract: Makes the Array
	// ----------------------------------------------------------------------	
	private static int[] MakeArray()
	{
	    System.out.print("Enter the size of the array (0-100): ");
	    int intResult = ReadIntegerFromUser();  // Read the size from user

	    int[] aintValues = new int[intResult];

	    for (int intIndex = 0; intIndex < aintValues.length; intIndex++) {
	        aintValues[intIndex] = 0;  // Assign value to array
	        //System.out.println("aintValues[" + intIndex + "] = " + aintValues[intIndex]);  // Print value... debugging
	    }

	    return aintValues;
	}
	
	
	
	// ----------------------------------------------------------------------
	// Name: PopulateArray
	// Abstract: Populates the Array with Data
	// ----------------------------------------------------------------------		
	private static void PopulateArray(int[] aintValues)
	{
		int intIndex = 0;
		
		System.out.println("Enter values for the array:");
		
		for (intIndex = 0; intIndex <aintValues.length; intIndex += 1)
		{
			System.out.print("Enter value for element " + intIndex + ": ");
			aintValues[intIndex] = ReadIntegerFromUser();
		}

	}
	
	
	// ----------------------------------------------------------------------
	// Name: PrintArray
	// Abstract: Prints the Array
	// ----------------------------------------------------------------------		
	private static void PrintArray(int[] aintValues)
	{
		int intIndex = 0;
		
		for (intIndex = 0; intIndex < aintValues.length; intIndex += 1)
		{
			System.out.println("aintValues[" + intIndex + "] = " + aintValues[intIndex]);			
		}
	}
	
	

	// ----------------------------------------------------------------------
	// Name: AverageArray
	// Abstract: Calculates the Average of the Array
	// ----------------------------------------------------------------------		
	private static void AverageArray(int[] aintValues)
	{
	    if (aintValues.length == 0) {
	        System.out.println("The array is empty. No average.");
	        return;  // Exit the method since there's no value to find
	    }		
		int intIndex = 0;
		int intAddTotal = 0;
		float sngTotal = 0;
		
		
		for (intIndex = 0; intIndex < aintValues.length; intIndex += 1)
		{
			intAddTotal += aintValues[intIndex];
		}
		
		sngTotal = (float) intAddTotal / aintValues.length;
		
		System.out.printf("The average of the array is: %.2f%n", sngTotal);	
				
	}
	
	
	
	// ----------------------------------------------------------------------
	// Name: MaxArray
	// Abstract: Calculates the Maximum Value
	// ----------------------------------------------------------------------		
	private static void MaxArray(int[] aintValues)
	{
	    if (aintValues.length == 0) {
	        System.out.println("The array is empty. No maximum value.");
	        return;
	    }
	    
		int intIndex = 0;
		int intMax = aintValues[intIndex];
		
		for (intIndex = 1 ; intIndex < aintValues.length; intIndex += 1)
		{
			if (intMax < aintValues[intIndex])
			{
				intMax = aintValues[intIndex];
			}
		}
		System.out.println("The maximum value in the array is: " + intMax);
	}
	
	
	
	// ----------------------------------------------------------------------
	// Name: AddValueToArray
	// Abstract: Adds a value to the end of array
	// ----------------------------------------------------------------------		
	private static int[] AddValueToArray(int[] aintValues, int intNewValue)
	{
		int intIndex = 0;
		int[] aintArray = new int[aintValues.length + 1];
		
		for (intIndex = 0; intIndex < aintValues.length; intIndex += 1 )
		{
			aintArray[intIndex] =  aintValues[intIndex];			
		}
		
		aintArray[aintValues.length] = intNewValue;
		
		return aintArray;
	}
	
	
	
	// ----------------------------------------------------------------------
	// Name: AddValueToFrontArray
	// Abstract: Adds a value to the front of the array
	// ----------------------------------------------------------------------		
	private static int[] AddValueToFrontArray(int[] aintValues, int intNewValue)
	{
		int intIndex = 0;
		int[] aintArray =  new int[aintValues.length + 1];
		
		aintArray[0] = intNewValue;
		
		for (intIndex = 0; intIndex < aintValues.length; intIndex += 1 )
		{
			aintArray[intIndex + 1] =  aintValues[intIndex];			
		}
		
		return aintArray;
	}
	
	
	// ----------------------------------------------------------------------
	// Name: InsertValueIntoArray
	// Abstract: Inserts Value Into Position in Array
	// ----------------------------------------------------------------------
	private static int[] InsertValueIntoArray(int[] aintValues, int intNewValue, int intInsertIndex)
	{
		int intIndex = 0;
		
		if (intInsertIndex < 0) 
		{
			intInsertIndex = 0;
		} else if (intInsertIndex > aintValues.length) {
			intInsertIndex = aintValues.length;
		}
		
		int[] aintArray = new int[aintValues.length + 1];
		
		for (intIndex = 0; intIndex < intInsertIndex; intIndex += 1)
		{
			aintArray[intIndex] = aintValues[intIndex];
		}
		
		aintArray[intInsertIndex] = intNewValue;
		
		for (intIndex = intInsertIndex; intIndex < aintValues.length; intIndex += 1)
		{
			aintArray[intIndex+1] = aintValues[intIndex];
		}
		return aintArray;
	}
	
	

	// ----------------------------------------------------------------------
	// Name: RemoveValueFromArray
	// Abstract: Remove a Value from the Array
	// ----------------------------------------------------------------------
	private static int[] RemoveValueFromArray(int[] aintValues, int intRemoveIndex)
	{
		int intIndex = 0;
	    if (intRemoveIndex < 0) {
	        intRemoveIndex = 0;
	    } else if (intRemoveIndex >= aintValues.length) {
	        intRemoveIndex = aintValues.length - 1;
	    }

	    int[] aintArray = new int[aintValues.length - 1];

	    for (intIndex = 0; intIndex < intRemoveIndex; intIndex++) {
	        aintArray[intIndex] = aintValues[intIndex];
	    }

	    for (intIndex = intRemoveIndex + 1; intIndex < aintValues.length; intIndex++) {
	        aintArray[intIndex - 1] = aintValues[intIndex];
	    }

	    return aintArray;
	}
	
	
	
	// ----------------------------------------------------------------------
	// Name: ReadFloatFromUser
	// Abstract: Reads Float Input
	// ----------------------------------------------------------------------		
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

	

	
	// ----------------------------------------------------------------------
	// Name: ReadIntegerFromUser
	// Abstract: Reads Integer Input
	// ----------------------------------------------------------------------	
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

