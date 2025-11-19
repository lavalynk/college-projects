// --------------------------------------------------------------------------------
// Name: Keith Brock
// Class: C Programming 1 
// Abstract: Homework 7.5 - Roman Numerals
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
//const long lngARRAY_SIZE = 5;

// --------------------------------------------------------------------------------
// User Defined Types (UDT)
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Prototypes
// --------------------------------------------------------------------------------
void ConvertDecimalNumberToRomanNumerals(char strRomanNumerals[], int intUserValue);
void ConvertThousands(char strRomanNumerals[], int intUserValue);
void ConvertHundreds(char strRomanNumerals[], int intUserValue);
void ConvertTens(char strRomanNumerals[], int intUserValue);
void ConvertOnes(char strRomanNumerals[], int intUserValue);
void StringConcatenate(char strRomanNumerals[], const char* chrRoman);
void DisplayFirst50RomanNumerals();
int GetValidUserInput();



// --------------------------------------------------------------------------------
// Name: main
// Abstract: This is where the program starts
// --------------------------------------------------------------------------------
int main()
{
	char strRomanNumerals[50] = " ";
	int intUserValue = 0;
	char chrChoice = ' ';

	while (chrChoice != 'c' && chrChoice != 'C')
	{
		printf("Menu Options:\n");
		printf("A. Display the first 50 Roman Numerals\n");
		printf("B. Convert a number to Roman Numerals\n");
		printf("C. Exit\n");
		printf("Enter your selection (A, B, or C): ");
		scanf(" %c", &chrChoice);

		if (chrChoice == 'A' || chrChoice == 'a')
		{
			printf("\n");
			DisplayFirst50RomanNumerals();
			chrChoice = ' ';
			printf("\n");
			system("pause");
			printf("\n");
		}
		else if (chrChoice == 'B' || chrChoice == 'b')
		{
			intUserValue = GetValidUserInput();
			printf("\n");
			strRomanNumerals[0] = '\0';
			ConvertDecimalNumberToRomanNumerals(strRomanNumerals, intUserValue);
			printf("%d = %s\n", intUserValue, strRomanNumerals);
			chrChoice = ' ';
			printf("\n");
			system("pause");
			printf("\n");
		}
		else if (chrChoice == 'C' || chrChoice == 'c')
		{
			printf("\n");
			printf("Exiting the program.\n");
			break;
		}
		else
		{
			printf("Invalid choice.  Please enter A, B, or C.\n");
		}
	}

	return 0;
}



// --------------------------------------------------------------------------------
// Name: DisplayFirst50RomanNumerals
// Abstract: Displays the First 50 Roman Numerals
// --------------------------------------------------------------------------------
void DisplayFirst50RomanNumerals() {
	int intIndex = 0;
	char chrLeftColumn[15] = "";  
	char chrRightColumn[15] = ""; 

	printf("Number/Numeral\t\tNumber/Numeral\n");
	for (intIndex = 1; intIndex <= 25; intIndex++) {
		ConvertDecimalNumberToRomanNumerals(chrLeftColumn, intIndex);
		ConvertDecimalNumberToRomanNumerals(chrRightColumn, intIndex + 25);
		printf("%d = %-10s\t\t%d = %s\n", intIndex, chrLeftColumn, intIndex + 25, chrRightColumn);

		// Reset the columns before the next iteration
		chrLeftColumn[0] = '\0';
		chrRightColumn[0] = '\0';
	}
}


// --------------------------------------------------------------------------------
// Name: ConvertToRomanNumerals
// Abstract: Convert Number to Roman Numeral
// --------------------------------------------------------------------------------
void ConvertDecimalNumberToRomanNumerals(char strRomanNumerals[], int intUserValue) {
	ConvertThousands(strRomanNumerals, intUserValue);
	ConvertHundreds(strRomanNumerals, intUserValue);
	ConvertTens(strRomanNumerals, intUserValue);
	ConvertOnes(strRomanNumerals, intUserValue);
}


// --------------------------------------------------------------------------------
// Name: StringConcatenate
// Abstract: Concatenate String
// --------------------------------------------------------------------------------
void StringConcatenate(char strRomanNumerals[], const char* chrRoman) {
	int intIndex = 0;
	int intIndex2 = 0;

	while (strRomanNumerals[intIndex] != '\0')
	{
		intIndex += 1;
	}

	while (chrRoman[intIndex2] != '\0')
	{
		strRomanNumerals[intIndex] = chrRoman[intIndex2];
		intIndex += 1;
		intIndex2 += 1;
	}

	strRomanNumerals[intIndex] = '\0';
}



// --------------------------------------------------------------------------------
// Name: ConvertThousands
// Abstract: Convert the thousands place
// --------------------------------------------------------------------------------
void ConvertThousands(char strRomanNumerals[], int intUserValue) {
	int intThousands = (intUserValue % 10000) / 1000;

	switch (intThousands) {
	case 1: 
		StringConcatenate(strRomanNumerals, "M");
		break;
	case 2: 
		StringConcatenate(strRomanNumerals, "MM"); 
		break;
	case 3: 
		StringConcatenate(strRomanNumerals, "MMM"); 
		break;
	}
}

// --------------------------------------------------------------------------------
// Name: ConvertHundreds
// Abstract: Convert the hundreds place
// --------------------------------------------------------------------------------
void ConvertHundreds(char strRomanNumerals[], int intUserValue) {
	int intHundreds = (intUserValue % 1000) / 100;

	switch (intHundreds) {
	case 9: 
		StringConcatenate(strRomanNumerals, "CM");
		break;
	case 8: 
		StringConcatenate(strRomanNumerals, "DCCC"); 
		break;
	case 7: 
		StringConcatenate(strRomanNumerals, "DCC"); 
		break;
	case 6: 
		StringConcatenate(strRomanNumerals, "DC"); 
		break;
	case 5: 
		StringConcatenate(strRomanNumerals, "D"); 
		break;
	case 4: 
		StringConcatenate(strRomanNumerals, "CD"); 
		break;
	case 3: 
		StringConcatenate(strRomanNumerals, "CCC"); 
		break;
	case 2: 
		StringConcatenate(strRomanNumerals, "CC"); 
		break;
	case 1: 
		StringConcatenate(strRomanNumerals, "C"); 
		break;
	}
}

// --------------------------------------------------------------------------------
// Name: ConvertTens
// Abstract: Convert the tens place
// --------------------------------------------------------------------------------
void ConvertTens(char strRomanNumerals[], int intUserValue) {
	int intTens = (intUserValue % 100) / 10;

	switch (intTens) {
	case 9: 
		StringConcatenate(strRomanNumerals, "XC"); 
		break;
	case 8: 
		StringConcatenate(strRomanNumerals, "LXXX"); 
		break;
	case 7: 
		StringConcatenate(strRomanNumerals, "LXX"); 
		break;
	case 6: 
		StringConcatenate(strRomanNumerals, "LX"); 
		break;
	case 5: 
		StringConcatenate(strRomanNumerals, "L"); 
		break;
	case 4: 
		StringConcatenate(strRomanNumerals, "XL"); 
		break;
	case 3: 
		StringConcatenate(strRomanNumerals, "XXX"); 
		break;
	case 2: 
		StringConcatenate(strRomanNumerals, "XX"); 
		break;
	case 1: 
		StringConcatenate(strRomanNumerals, "X"); 
		break;
	}
}

// --------------------------------------------------------------------------------
// Name: ConvertOnes
// Abstract: Convert the ones place
// --------------------------------------------------------------------------------
void ConvertOnes(char strRomanNumerals[], int intUserValue) {
	int intOnes = intUserValue % 10;

	switch (intOnes) {
	case 9: 
		StringConcatenate(strRomanNumerals, "IX"); 
		break;
	case 8: 
		StringConcatenate(strRomanNumerals, "VIII"); 
		break;
	case 7: 
		StringConcatenate(strRomanNumerals, "VII"); 
		break;
	case 6: 
		StringConcatenate(strRomanNumerals, "VI"); 
		break;
	case 5: 
		StringConcatenate(strRomanNumerals, "V"); 
		break;
	case 4: 
		StringConcatenate(strRomanNumerals, "IV"); 
		break;
	case 3: 
		StringConcatenate(strRomanNumerals, "III"); 
		break;
	case 2: 
		StringConcatenate(strRomanNumerals, "II");
		break;
	case 1: 
		StringConcatenate(strRomanNumerals, "I"); 
		break;
	}
}

// --------------------------------------------------------------------------------
// Name: GetValidUserInput
// Abstract: Get a valid input from the user between 1 and 3999
// --------------------------------------------------------------------------------
int GetValidUserInput() {
	int intUserValue = 0;
	do {
		printf("Enter a number between 1 and 3999: ");
		scanf("%d", &intUserValue);
	} while (intUserValue < 1 || intUserValue > 3999);

	return intUserValue;
}