// ------------------------------------------------------------------------------------------
// Name: Keith Brock
// Class: C Programming
// Abstract: Assignment 1 - To find the upper and lower limits of different data types.
// ------------------------------------------------------------------------------------------

// ------------------------------------------------------------------------------------------
// Includes
// ------------------------------------------------------------------------------------------
#include<stdio.h>
#include<stdlib.h>


// ------------------------------------------------------------------------------------------
// Constants
// ------------------------------------------------------------------------------------------

// ------------------------------------------------------------------------------------------
// Prototypes
// ------------------------------------------------------------------------------------------

// ------------------------------------------------------------------------------------------
// Name: Main
// Abstract: This is where the program starts
// ------------------------------------------------------------------------------------------
void main()
{
	// Declare Variables
	short shtMinimum = 0;
	short shtMaximum = 0;
	int intMinimum = 0;
	int intMaximum = 0;
	long lngMinimum = 0;
	long lngMaximum = 0;
	float sngMinimum = 0;
	float sngMaximum = 0;

	// Set to Min/Max Values
	shtMinimum = -32767;
	shtMaximum = 32766;
	intMinimum = -2147483647;
	intMaximum = 2147483646;
	lngMinimum = -2147483647;
	lngMaximum = 2147483646;
	sngMinimum = -3.402823466e38F;
	sngMaximum = 3.402823466e38F;

	//Short
	printf("Short Minimum and Maximum\n");
	printf("------------------------------------------------\n");
	printf("Short Minimum: %hd\n", shtMinimum); 
	printf("Short Maximum: %hd\n", shtMaximum);
	printf("\n");

	shtMinimum -= 1;
	shtMaximum += 1;

	printf("Confirmation\n");
	printf("Short Minimum: %hd\n", shtMinimum); 
	printf("Short Maximum: %hd\n", shtMaximum); //Numbers Inverted
	printf("\n");

	//Integer
	printf("Integer Minimum and Maximum\n");
	printf("------------------------------------------------\n");
	printf("Integer Minimum: %d\n", intMinimum); 
	printf("Integer Maximum: %d\n", intMaximum); 
	printf("\n");

	intMinimum -= 1;
	intMaximum += 1;

	printf("Confirmation\n");
	printf("Integer Minimum: %d\n", intMinimum); //Build Error Over -2147483647
	printf("Integer Maximum: %d\n", intMaximum); //Negative Number Over 2147483646
	printf("\n");

	//Long
	printf("Long Minimum and Maximum\n");
	printf("------------------------------------------------\n");
	printf("Long Minimum: %ld\n", lngMinimum); 
	printf("Long Maximum: %ld\n", lngMaximum); 
	printf("\n");

	lngMinimum -= 1;
	lngMaximum += 1;

	printf("Confirmation\n");
	printf("Long Minimum: %ld\n", lngMinimum); //Build Error Over -2147483647
	printf("Long Maximum: %ld\n", lngMaximum); //Negative Number Over 2147483646
	printf("\n");

	//Float
	printf("Float Minimum and Maximum\n");
	printf("------------------------------------------------\n");
	printf("Float Minimum: %f\n", sngMinimum);
	printf("Float Maximum: %f\n", sngMaximum);
	printf("\n");

	sngMinimum -= 1e32;
	sngMaximum += 1e32;

	printf("Confirmation\n");
	printf("Float Minimum: %f\n", sngMinimum);
	printf("Float Maximum: %f\n", sngMaximum); 
	printf("\n");

	system("pause");

}