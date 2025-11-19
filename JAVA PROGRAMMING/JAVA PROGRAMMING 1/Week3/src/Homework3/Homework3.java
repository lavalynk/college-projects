package Homework3;

/**
* The MyFirstPgm program implements an application that
* simply displays constants and variables to the standard output.
*
* @author  Keith Brock
* @version 1.0
* @since   2024-09-09
*/

public class Homework3{
	/** This is Java's main method entry point of this program.
	 *  The program uses constants and variables.
	 */
	public static void main( String astrCommandLine[] ) 	{
		// --------------------------------------------------------------------------------
		// Problem 1: Age in seconds
		// --------------------------------------------------------------------------------
		// Constants/Variables
		final int intSECONDS_PER_DAY = 24 * 60 * 60;
		int intYearsOld = 0;
		int intDaysOld = 0;
		int intSecondsOld = 0;
		
		intYearsOld = 39;
		intDaysOld = 39 * 365;
		intSecondsOld = intDaysOld * intSECONDS_PER_DAY;
			
		// Output the Result
		System.out.print("#1 Problem: Age in Seconds.\n");
		System.out.print("-------------------------------------------\n");
		System.out.print(intYearsOld + " years old is " + intSecondsOld + " seconds.\n");
		System.out.print("\n");
		System.out.print("\n");		
		
		// --------------------------------------------------------------------------------
		// Problem 2: Volume of Sun, Earth and ratio of the two
		// --------------------------------------------------------------------------------
	    // Constants/Variables
	    int intRadiusSun = 432500; 
	    int intRadiusEarth = 3800; 
	    
	    // Calculate volume of the Sun
	    float sngVolumeSun = (float) ((4.0/3.0) * Math.PI * Math.pow(intRadiusSun, 3));
	    
	    // Calculate volume of the Earth
	    float sngVolumeEarth = (float) ((4.0/3.0) * Math.PI * Math.pow(intRadiusEarth, 3));
	    
	    // Calculate the ratio of Sun's volume to Earth's volume
	    float sngRatio = sngVolumeSun / sngVolumeEarth;
	    
	    // Output the results
	    System.out.print("#2 Problem: Ratio Sun/Earth\n");
	    System.out.print("-------------------------------------------\n");    
	    System.out.println("Volume of the Sun: " + sngVolumeSun + " cubic miles");
	    System.out.println("Volume of the Earth: " + sngVolumeEarth + " cubic miles");
	    System.out.println("Ratio of Sun's volume to Earth's volume: " + sngRatio);
		System.out.print("\n");
		System.out.print("\n");			    
	    
		// --------------------------------------------------------------------------------
		// Problem 3: Number of molecules in 2.45 quarts of water.
		// --------------------------------------------------------------------------------
		// Constants/Variables
		final int intGRAMS_PER_QUART = 950;
		final float sngMOLECULE_WEIGHT_GRAMS = 3E-23f;
		float sngQuartsOfWater = 2.45f;
		float sngTotalNumberOfGrams = 0;
		float sngTotalNumberOfMolecules = 0;
		
		sngTotalNumberOfGrams = sngQuartsOfWater * intGRAMS_PER_QUART;
		sngTotalNumberOfMolecules = sngTotalNumberOfGrams / sngMOLECULE_WEIGHT_GRAMS;
		
	    // Output the result
		System.out.print("#3 Problem: Number of molecules in 2.45 quarts of water.\n");
		System.out.print("-------------------------------------------\n");    		
	    System.out.println("Number of molecules in 2.45 quarts of water: " + sngTotalNumberOfMolecules);		
		System.out.println("\n");
		System.out.println("\n");		
		
		
	}
}

