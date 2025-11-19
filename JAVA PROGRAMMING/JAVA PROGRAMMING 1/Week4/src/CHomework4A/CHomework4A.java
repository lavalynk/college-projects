package CHomework4A;

/**
* 
* 
*
* @author  Keith Brock
* @version 1.0
* @since   2024-09-10
*/

public class CHomework4A{
	/** This is Java's main method entry point of this program.
	 *  The program uses constants and variables.
	 */
	public static void main( String astrCommandLine[] ) 	{
		// --------------------------------------------------------------------------------
		// Write a loop that will print all the whole numbers from 1 to 100
		// --------------------------------------------------------------------------------
		
		int intIndex = 0;
		int intCount = 0;
		int intOdd = 0;
		int intEven = 0;
		int intThird = 0;
		
		System.out.print("Problem # 1\n");
		System.out.print("--------------------------------\n");
		for(intIndex = 1; intIndex <= 100; intIndex +=1)
		{
			System.out.print(intIndex + " ");
			if (intIndex % 10 == 0)
			{
				System.out.print("\n");
			}
		}
		
		System.out.print("\n");
		System.out.print("\n");		
		// --------------------------------------------------------------------------------
		// Write a loop that will add all the whole numbers from 1 to 1000.
		// --------------------------------------------------------------------------------		
		
		System.out.print("Problem # 2\n");
		System.out.print("--------------------------------\n");
		
		for (intIndex =1; intIndex <= 1000; intIndex+=1)
		{
			intCount += intIndex;
		}
		
		System.out.print("The sum of all added whole numbers 1 to 1000 is: " + intCount + "\n");
		
		System.out.print("\n");
		System.out.print("\n");		
		
		// --------------------------------------------------------------------------------
		// Write a loop that will add ODD numbers from 1 to 300 and print only the total.
		// --------------------------------------------------------------------------------		
		System.out.print("Problem # 3\n");
		System.out.print("--------------------------------\n");
		
		for (intIndex = 1; intIndex <= 300; intIndex+=2)
		{
			intOdd += intIndex;
		}
		System.out.print("The sum off all ODD numbers 1 to 300 is: " + intOdd + "\n");
		
		System.out.print("\n");
		System.out.print("\n");		
		
		// --------------------------------------------------------------------------------
		// Write a loop that will add EVEN numbers from 2 to 146 and print only the total.
		// --------------------------------------------------------------------------------
		System.out.print("Problem # 4\n");
		System.out.print("--------------------------------\n");
		
		for (intIndex = 2; intIndex <= 146; intIndex+=2)
		{
			intEven += intIndex;
		}
		
		System.out.print("The sum off all EVEN numbers 2 to 146 is: " + intEven + "\n");
		
		System.out.print("\n");
		System.out.print("\n");	
		
		// --------------------------------------------------------------------------------
		// Write a loop that will add every 3rd number from 2000 to 6 and print only the total.
		// --------------------------------------------------------------------------------
		System.out.print("Problem # 5\n");
		System.out.print("--------------------------------\n");
		
		for (intIndex = 2000; intIndex >= 6; intIndex-=3)
		{
			intThird += intIndex;
		}
		System.out.print("The sum of every 3rd number from 2000 to 6 is: " + intThird + "\n");
		
		System.out.print("\n");
		System.out.print("\n");	
	}
}

