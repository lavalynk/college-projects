// --------------------------------------------------------------------------------
// Name: Keith Brock
// Class: C Programming 1 
// Abstract: Homework 7 - Various Problems
// --------------------------------------------------------------------------------
#define _CRT_SECURE_NO_WARNINGS
// --------------------------------------------------------------------------------
// Includes – built-in libraries of functions
// --------------------------------------------------------------------------------
#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include <math.h>

// --------------------------------------------------------------------------------
// Constants
// --------------------------------------------------------------------------------
const long lngARRAY_SIZE = 5;

// --------------------------------------------------------------------------------
// User Defined Types (UDT)
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Prototypes
// --------------------------------------------------------------------------------
// Problem #1: StringLength
int StringLength(char strSource[]);

// Problem #2: CopyString
void CopyString(char strDestination[], char strSource[]);

// Problem #3: FindFirstOccurrence
int FindFirstOccurrence(char strSource[], char chrLetterToFind);

// Problem #4: FindFirstOccurrenceInsensitive
int FindFirstOccurrenceInsensitive(char strSource[], char chrLetterToFind);

// Problem #5: AppendString
void AppendString(char strDestination[], char strSource[]);

// Problem #6: CopyReverse
void CopyReverse(char strDestination[], char strSource[]);

// Problem #7: CopyUpperCase
void CopyUpperCase(char strDestination[], char strSource[]);

// Problem #8: CopySubString
void CopySubString(char strDestination[], char strSource[], int intStartIndex, int intLength);

// Problem #9: WordCount
int WordCount(char strSource[]);

// --------------------------------------------------------------------------------
// Name: main
// Abstract: This is where the program starts
// --------------------------------------------------------------------------------
int main()
{
	char strSource[50] = "I Love Star Trek";
	char strSource2[50] = " Battles";
	int intLength = 0;
	int intLengths = 4;
	int intStartIndex = 7;
	char strDestination[50] = "";
	char chrLetterToFind = 'T';
	int intFoundIndex = 0;
	int intWordCount = 0;

	// Problem #1: String Length
	intLength = StringLength(strSource);
	printf("Problem #1: String Length: %d\n", intLength);
	printf("\n");

	// Problem #2: Copy String
	CopyString(strDestination, strSource);
	printf("Problem #2: CopyString: %s\n", strDestination);
	printf("\n");

	// Problem #3: Find First Occurrence (Case Sensitive)
	intFoundIndex = FindFirstOccurrence(strSource, chrLetterToFind);
	printf("Problem #3: FindFirstOccurrence: %d Index\n", intFoundIndex);
	printf("\n");

	// Problem #4: Find First Occurrence (Case Insensitive)
	intFoundIndex = FindFirstOccurrenceInsensitive(strSource, chrLetterToFind);
	printf("Problem #4: FindFirstOccurrenceInsensitive: %d Index\n", intFoundIndex);
	printf("\n");

	// Problem #5: Append String
	AppendString(strDestination, strSource2);
	printf("Problem #5: AppendString: %s\n", strDestination);
	printf("\n");

	// Problem #6: Copy String In Reverse
	CopyReverse(strDestination, strSource);
	printf("Problem #6: Copy String in Reverse: %s\n", strDestination);
	printf("\n");

	// Problem #7: Copy to Upper Case
	CopyUpperCase(strDestination, strSource);
	printf("Problem #7: Copy String to Upper Case: %s\n ", strDestination);
	printf("\n");

	// Problem #8: Copy Substring
	CopySubString(strDestination, strSource, intStartIndex, intLengths);
	printf("Problem #8: Copy SubString: %s\n", strDestination);
	printf("\n");

	// Problem #9: Count Words
	intWordCount = WordCount(strSource);
	printf("Problem #9: Count Words: %d\n", intWordCount);
	printf("\n");

	system("pause");
}

//
//
// --------------------------------------------------------------------------------
// Name: StringLength
// Abstract: Return the Length of String
// --------------------------------------------------------------------------------
int StringLength(char strSource[ ])
{
	int intLength = 0;

		while (strSource[intLength] != '\0')
		{
			intLength += 1;
		}

	return intLength;
}


// --------------------------------------------------------------------------------
// Name: Copy String
// Abstract: Copy the String
// --------------------------------------------------------------------------------
void CopyString(char strDestination[ ], char strSource[ ]) 
{
	int intIndex = 0;
	while (strSource[intIndex] != '\0') {
		strDestination[intIndex] = strSource[intIndex];
		intIndex += 1;
	}
	strDestination[intIndex] = '\0'; // Add null terminator
}



// --------------------------------------------------------------------------------
// Name: FindFirstOccurrence
// Abstract: Find First Occurrence (Case Sensitive)
// --------------------------------------------------------------------------------
int FindFirstOccurrence(char strSource[ ], char chrLetterToFind)
{
	int intIndex = 0;
	int intResult = -1;

	while (strSource[intIndex] != '\0')
	{
		if (strSource[intIndex] == chrLetterToFind)
		{
			intResult = intIndex; // Store the index of the found character
			break;             // Exit the loop once the letter is found
		}
		intIndex += 1;
	}

	return intResult; // Single return point at the end
}


// --------------------------------------------------------------------------------
// Name: FindFirstOccurrenceInsensitive
// Abstract: Find First Occurrence (Case Insensitive)
// --------------------------------------------------------------------------------
int FindFirstOccurrenceInsensitive(char strSource[], char chrLetterToFind)
{
	int intIndex = 0;
	int intResult = -1;

	while (strSource[intIndex] != '\0')
	{
		if (strSource[intIndex] == chrLetterToFind || strSource[intIndex] == (chrLetterToFind ^ 32))
		{
			intResult = intIndex; // Store the index of the found character
			break;             // Exit the loop once the letter is found
		}
		intIndex += 1;
	}

	return intResult; // Single return point at the end
}


// --------------------------------------------------------------------------------
// Name: AppendString
// Abstract: Append Source String to Destination String
// --------------------------------------------------------------------------------
void AppendString(char strDestination[], char strSource[])
{
	int intIndex = 0;
	int intIndex2 = 0;

	while (strDestination[intIndex] != '\0')
	{
		intIndex += 1;
	}

	while (strSource[intIndex2] != '\0')
	{
		strDestination[intIndex] = strSource[intIndex2];
		intIndex += 1;
		intIndex2 += 1;
	}
	strDestination[intIndex] = '\0';
}



// --------------------------------------------------------------------------------
// Name: CopyReverse
// Abstract: Copy String In Reverse
// --------------------------------------------------------------------------------
void CopyReverse(char strDestination[], char strSource[])
{
	int intLength = StringLength(strSource);
	int intIndex = 0;

	for (intIndex = 0; intIndex < intLength; intIndex += 1)
	{
		strDestination[intIndex] = strSource[intLength - intIndex - 1];
	}
	strDestination[intIndex] = '\0';
}



// --------------------------------------------------------------------------------
// Name: CopyUpperCase
// Abstract: Copy String to All Upper Case
// --------------------------------------------------------------------------------
void CopyUpperCase(char strDestination[], char strSource[])
{
	int intIndex = 0;

	while (strSource[intIndex] != '\0')
	{
		if (strSource[intIndex] >= 'a' && strSource[intIndex] <= 'z')
		{
			strDestination[intIndex] = strSource[intIndex] - 32;
		}
		else
		{
			strDestination[intIndex] = strSource[intIndex];
		}
		intIndex += 1;
	}
	strDestination[intIndex] = '\0';
}



// --------------------------------------------------------------------------------
// Name: CopySubString
// Abstract: Copy Sub String
// --------------------------------------------------------------------------------
void CopySubString(char strDestination[], char strSource[], int intStartIndex, int intLengths)
{
	int intIndex = 0;

	for (intIndex = 0; intIndex < intLengths && strSource[intStartIndex + intIndex] != '\0'; intIndex += 1)
	{
		strDestination[intIndex] = strSource[intStartIndex + intIndex];
	}
	strDestination[intIndex] = '\0';
}



// --------------------------------------------------------------------------------
// Name: WordCount
// Abstract: Counts Words in String
// --------------------------------------------------------------------------------
int WordCount(char strSource[])
{
	int intIndex = 0;
	int intWordCount = 0;
	int intInWord = 0;

	while (strSource[intIndex] != '\0')
	{
		if (strSource[intIndex] == ' ' || strSource[intIndex] == '\t' || strSource[intIndex] == '\n')
		{
			intInWord = 0;
		}
		else if (intInWord == 0)
		{
			intInWord = 1;
			intWordCount += 1;
		}
		intIndex += 1;
	}
	return intWordCount;
}