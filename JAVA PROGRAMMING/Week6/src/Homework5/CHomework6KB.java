package Homework5;

/**
* Name: StringIC1
* Abstract: Working with strings
* @author Keith Brock
*/
public class CHomework6KB
{
	/** 
	* Name: main
	* Abstract: This is where the program starts.
	*/
	public static void main( String astrCommandLine[ ] )
	{
		// declare the variable
		int intVowelCount = 0;
		char chrLetterIndex = 0;
		int intLetterCount = 0;
		String strBuffer;
		boolean blnResult = false;
		int intWordCount = 0;
		
		// Problem #1 – Count Vowels
		// call the CountVowelsInString method		
		System.out.print("-- Problem #1 - Count Vowels --\n");
		intVowelCount = CountVowelsInString("Keith Brock");
		System.out.print("Vowel Count =" + intVowelCount + "\n");
		System.out.println("");
		// Problem #2 - Find Letter
		System.out.print("-- Problem #2 - Find Letter In String --\n");
		chrLetterIndex = FindLetterInString("I Love Java", 'J');
		System.out.print("Letter Index = " + chrLetterIndex + "\n");
		System.out.println("");		
		// Problem #3 -- Count Letters In String
		System.out.print("-- Problem #3 - Count Specific Letter In String --\n");
		intLetterCount = CountLetterInString("I Love Java", 'A');
		System.out.print("Letter Count = " + intLetterCount + "\n");
		System.out.println("");		
		// Problem #4 -- Left
		System.out.print("-- Problem #4 - Left" + "\n");
		strBuffer = Left("I Love Java", 3);
		System.out.print("Left(3) = " + strBuffer + "\n");
		System.out.println("");		
		// Problem #5 -- Right
		System.out.print("-- Problem #5 - Right" + "\n");
		strBuffer = Right("I Love Java", 3);
		System.out.println("Right(3) = " + strBuffer);
		System.out.println("");		
		//Problem #6 -- VB Substring
		System.out.print("-- Problem #6 - VBSubString" + "\n");		
		strBuffer = VBSubString("I Love Java", 2, 4);
		System.out.println("VB SubString = " + strBuffer);
		System.out.println("");		
		//Problem #7 -- Java SubString
		System.out.println("-- Problem #7 - Java SubString");
		strBuffer = JavaSubString("I Love Java", 2, 6);
		System.out.println("Java SubString = " + strBuffer);
		System.out.println("");		
		// Problem #8 – CompareStrings
		System.out.println( "-- Problem #8 - CompareStrings" );		
		blnResult = CompareString( "I Love Java", "I love Java" );  // Should return Different
		if (blnResult == true)
		{
			System.out.println("Same");
		}
		else
		{
			System.out.println("Different");
		}
		System.out.println("");		
		// Problem #9 - Count Words
		System.out.println("-- Problem #9 - Count Words");
		intWordCount = CountWordsInString("  I  Love Java  Class  ");
		System.out.println("Word Count = " + intWordCount);
	}		
		

	/**
	 * method CountVowelsInString
	 * Abstract: Get Number of Vowels in String
	 * @param strSource
	 * @return VowelCount
	 */
	public static int CountVowelsInString( String strSource )
	{
		//declare the variables for VowelCount, CurrentLetter, Index
		int intVowelCount = 0;
		char chrCurrentLetter = 0;
		int intIndex = 0;					
		// Create a for loop to check each character and compare to see if it's an A, E, I, O or U
		// For Index = 0; Index less than length of the string; increment Index
		for(intIndex = 0; intIndex < strSource.length(); intIndex += 1)
		{
			// get the value of the current letter based on the Index
			 chrCurrentLetter = strSource.charAt(intIndex);
			// Convert to upper case
			 chrCurrentLetter = Character.toUpperCase(chrCurrentLetter);

		 
			
			//write case logic to check if the letter is A, E, I, O, or U - if so, add 1 to VowelCount
			switch(chrCurrentLetter)
			{
				case 'A':					
				case 'E': 					  
				case 'I': 					  
				case 'O': 					  
				case 'U': intVowelCount += 1;
						  break;
			}
		}
		return intVowelCount;		
	}
	
	
	
	/**
	 * method FindLetterInString
	 * Abstract: Find Letter In String
	 * @param strSource, chrChar
	 * @return Letter
	 */
	private static char FindLetterInString(String strSource, char chrLetter) {
		 int intIndex = 0;
		 char chrFound = 0;
		 
		 strSource = strSource.toLowerCase();
		 chrLetter = Character.toLowerCase(chrLetter);
		 		 
		 // Find the index of the first occurrence of the character
		 intIndex = strSource.indexOf(chrLetter);
		 
		 if (intIndex != -1)
		 {
			 chrFound = strSource.charAt(intIndex);
		 } else {
			 System.out.print("Chracter " + chrLetter + " not found.\n");
		 }

		 return chrFound;	 
	
	}

	
	// ----------------------------------------------------------------------
	// Name: CountLettersInString
	// Abstract: Counts the Letters in String
	// ----------------------------------------------------------------------	
	private static int CountLetterInString(String strSource, char chrLetter) {
		//Define Variables
		int intIndex = 0;
		int intCount = 0;
		
		strSource = strSource.toLowerCase();
		chrLetter = Character.toLowerCase(chrLetter);
		
		//Loop through the string to count chrLetter
		for (intIndex = 0; intIndex < strSource.length(); intIndex+=1)
		{
			if (strSource.charAt(intIndex) == chrLetter)
			{
				intCount += 1;
			}
		}
		//Return the total count
		return intCount;
	}	
	
	
	
	// ----------------------------------------------------------------------
	// Name: Left
	// Abstract: Saves the First xx amount of letters in String
	// ----------------------------------------------------------------------	
	private static String Left(String strSource, int intLength) {
		String strResult = "";
		
		if (intLength < 0)
		{
			strResult = "Not Valid.";
		} else if (intLength > strSource.length()){
			strResult = strSource.substring(0, strSource.length());			
		} else {
			strResult = strSource.substring(0, intLength);
		}
		return strResult;
	}
	

	
	// ----------------------------------------------------------------------
	// Name: Right
	// Abstract: Saves the last xx amount of letters in String
	// ----------------------------------------------------------------------
	private static String Right(String strSource, int intLength) {
		String strRight = "";
		int intIndex = 0;
		int intStart = 0;
		
		//Boundary Checking
		if (intLength < 0)
		{
			intLength = 0; //If negative, set length to 0.
		}
		if (intLength > strSource.length())
		{
			intLength = strSource.length();
		}
		
		intStart = strSource.length() - intLength;
		
		for (intIndex = intStart; intIndex < strSource.length(); intIndex += 1)
		{
			strRight += strSource.charAt(intIndex);
		}		
		return strRight;
	}
	
	
	// ----------------------------------------------------------------------
	// Name: VBSubString
	// Abstract: Counts the Words in String
	// ----------------------------------------------------------------------	

	private static String VBSubString(String strSource, int intStartIndex, int intLength) 
	{
		String strResult = "";
		int intIndex = 0;
		
		//Boundary check
		if (intStartIndex < 0)
		{
			intStartIndex = 0;
		}
		if (intLength < 0)
		{
			intLength = 0;		
		}
		
		if (intStartIndex>= strSource.length()) {
			strResult = "";
		}

		if (intStartIndex + intLength > strSource.length())
		{
			intLength = strSource.length() - intStartIndex;
		}
		
		for (intIndex = intStartIndex; intIndex < intStartIndex + intLength; intIndex+=1)
		{
			strResult += strSource.charAt(intIndex);
		}
		
		return strResult;
	}

	
	
	// ----------------------------------------------------------------------
	// Name: JavaSubString
	// Abstract: Strange Java Substring-ness
	// ----------------------------------------------------------------------	
	private static String JavaSubString(String strSource, int intStartIndex, int intStopIndex) {
		String strSubString = " ";
		int intIndex = 0;
		
		if (intStartIndex < 0)
		{
			intStartIndex = 0;
		}
		
		if (intStartIndex > strSource.length())
		{
			intStartIndex = strSource.length();
		}
		
		if (intStopIndex < 0)
		{
			intStopIndex = 0;
		}
		
		if (intStopIndex > strSource.length())
		{
			intStopIndex = strSource.length();
		}
		
		if (intStartIndex > intStopIndex)
		{
			strSubString = " ";
		}
		
		for (intIndex = intStartIndex; intIndex < intStopIndex; intIndex += 1)
		{
			strSubString += strSource.charAt(intIndex);
		}
		
		return strSubString;
	}

	// ----------------------------------------------------------------------
	// Name: CompareStrings
	// Abstract: Do I look the same to you?
	// ----------------------------------------------------------------------
	public static boolean CompareString( String strLeft, String strRight )
	{
		//declare boolean StringsAreTheSame and default to true
		boolean blnStringsAreTheSame = true;
		//declare LeftLength
		int intLeftLength = strLeft.length( );
		//declare RightLength
		int intRightLength = strRight.length( );
		//declare Index
		int intIndex = 0;

		// Get lengths for both strings


		
		// Same length? - if LeftLength is not equal to RightLength?
		if( intLeftLength != intRightLength )
		{
			// No, strings are different
			blnStringsAreTheSame = false;
		}
		else
		{
			// Yes, the length is the same, continue to check and loop through ... 
			//for Index is zero; Index is less than LeftLength; increment Index
			for( intIndex = 0; intIndex < intLeftLength; intIndex +=1 )
			{
				// Same character? - check if left character is not equal to right character?
				if( strLeft.charAt( intIndex ) != strRight.charAt( intIndex ) )
				{
					// No, strings are different
					blnStringsAreTheSame = false;
					
					// Stop looping - get out since you found a difference
					break;
				}
			}
		}
		//return StringsAreTheSame
		return blnStringsAreTheSame;
	}
	
	// ----------------------------------------------------------------------
	// Name: CountWordsInString
	// Abstract: Counts the Words in String
	// ----------------------------------------------------------------------
	private static int CountWordsInString(String strSource) {
		strSource = strSource.trim();
		
		String[] strWords = strSource.split("\\s+");
		
		return strWords.length;		
	}	
}	
	


