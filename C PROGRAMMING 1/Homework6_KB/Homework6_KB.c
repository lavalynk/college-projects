// --------------------------------------------------------------------------------
// Name: Keith Brock
// Class: C Programming 1 
// Abstract: Homework 6 - Various Problems
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
void InitializeArray(long alngValues[], long lngIndex);
void PopulateArray(long alngValues[], long lngIndex);
void PrintNumbersArray(long alngValues[], long lngIndex);
void CalculateArrayTotal(long alngValues[], long* plngTotal, long lngIndex);
void CalculateAverage(long alngValues[], long* plngAverageTotal, long* plngAverage, long lngIndex);
void CalculateMaxNumber(long alngValues[], long* plngMaxValue, long lngIndex);
void AddEvenPosition(long alngValues[], long* plngEvenPositionTotal, long lngIndex);
void AddEvenNumbers(long alngValues[], long* plngEvenTotal, long lngIndex);


// --------------------------------------------------------------------------------
// Name: main
// Abstract: This is where the program starts
// --------------------------------------------------------------------------------
int main()
{
	long lngTotal = 0;
	long lngAverageTotal = 0;
	long alngValues[5];
	long lngIndex = 0;
	long lngAverage = 0;
	long lngMaxValue = 0;
	long lngEvenPositionTotal = 0;
	long lngEvenTotal = 0;

	// 1 - Initialize Array
	InitializeArray(alngValues, lngIndex);
	// 2 - Populate Array
	PopulateArray(alngValues, lngIndex);
	// 3 - Print Array Values
	PrintNumbersArray(alngValues, lngIndex);
	// 4 - Print Total of Array
	CalculateArrayTotal(alngValues, &lngTotal, lngIndex);
	// 5 - Calculate Average of Array
	CalculateAverage(alngValues, &lngAverageTotal, &lngAverage, lngIndex);
	// 6 - Calculate Largest Number of Array
	CalculateMaxNumber(alngValues, &lngMaxValue, lngIndex);
	// 7 - Add All Even Positions of Array
	AddEvenPosition(alngValues, &lngEvenPositionTotal, lngIndex);
	// 8 - Add All Even Numbers of Array
	AddEvenNumbers(alngValues, &lngEvenTotal, lngIndex);

	system("pause");
}



// --------------------------------------------------------------------------------
// Name: InitializeArray
// Abstract: Initialize the Array
// --------------------------------------------------------------------------------
void InitializeArray(long alngValues[], long lngIndex)
{
	for (lngIndex = 0; lngIndex < lngARRAY_SIZE; lngIndex += 1)
	{
		alngValues[lngIndex] = 0;
	}
}



// --------------------------------------------------------------------------------
// Name: PopulateArray
// Abstract: Populates the Array
// --------------------------------------------------------------------------------
void PopulateArray(long alngValues[], long lngIndex)
{
	for (lngIndex = 0; lngIndex < lngARRAY_SIZE; lngIndex += 1)
	{
		printf("Enter Value %2ld: \n", lngIndex + 1);
		scanf(" %ld", &alngValues[lngIndex]);
	}
}



// --------------------------------------------------------------------------------
// Name: PrintNumbersArray
// Abstract: Print All Numbers In Array
// --------------------------------------------------------------------------------
void PrintNumbersArray(long alngValues[], long lngIndex)
{
	printf("\n");
	printf("--- Array Values ------------------------------------------\n");
	for (lngIndex = 0; lngIndex < lngARRAY_SIZE; lngIndex += 1)
	{
		printf("Value %2ld = %ld\n", lngIndex + 1,alngValues[lngIndex]);
	}
	printf("\n");
}



// --------------------------------------------------------------------------------
// Name: CalculateArrayTotal
// Abstract: Add all numbers of the array together.
// --------------------------------------------------------------------------------
void CalculateArrayTotal(long alngValues[], long* plngTotal, long lngIndex)
{	
	printf("--- Calculate Array Total----------------------------------\n");
	for (lngIndex = 0; lngIndex < lngARRAY_SIZE; lngIndex += 1)
	{
		*plngTotal += alngValues[lngIndex];
	}
	printf("Array Total: %ld\n", *plngTotal);
	printf("\n");
}



// --------------------------------------------------------------------------------
// Name: CalculateAverage
// Abstract: Calculate Average
// --------------------------------------------------------------------------------
void CalculateAverage(long alngValues[], long* plngAverageTotal, long* plngAverage, long lngIndex)
{
	// Calculate the array total and print.
	printf("--- Calculate Average -------------------------------------\n");
	for (lngIndex = 0; lngIndex < lngARRAY_SIZE; lngIndex += 1)
	{
		*plngAverageTotal += alngValues[lngIndex];
	}
	*plngAverage = (*plngAverageTotal / lngARRAY_SIZE);

	printf("Array Average: %ld\n", *plngAverage);
	printf("\n");
}



// --------------------------------------------------------------------------------
// Name: CalculateMaxNumber
// Abstract: Calculate The Max Number Entered
// --------------------------------------------------------------------------------
void CalculateMaxNumber(long alngValues[], long* plngMaxValue, long lngIndex)
{
	printf("--- Calculate Max Number ----------------------------\n");
	for (lngIndex = 0; lngIndex < lngARRAY_SIZE; lngIndex += 1)
	{
		if (*plngMaxValue < alngValues[lngIndex])
		{
			*plngMaxValue = alngValues[lngIndex];
		}
	}
	printf("Array Max Value: %ld\n", *plngMaxValue);
	printf("\n");
}



// --------------------------------------------------------------------------------
// Name: AddEvenPosition
// Abstract: Adding All Even Position Numbers
// --------------------------------------------------------------------------------
void AddEvenPosition(long alngValues[], long* plngEvenPositionTotal, long lngIndex)
{
	printf("--- Add Even Positions ------------------------------------\n");
	for (lngIndex = 0; lngIndex < lngARRAY_SIZE; lngIndex += 2)
	{
		*plngEvenPositionTotal += alngValues[lngIndex];
	}
	printf("Array Even Position Total: %ld\n", *plngEvenPositionTotal);
	printf("\n");
}



// --------------------------------------------------------------------------------
// Name: AddEvenNumbers
// Abstract: Adding All Even Numbers
// --------------------------------------------------------------------------------
void AddEvenNumbers(long alngValues[], long* plngEvenTotal, long lngIndex)
{
	printf("--- Add Even Numbers --------------------------------------\n");
	for (lngIndex = 0; lngIndex < lngARRAY_SIZE; lngIndex += 1)
	{
		if (alngValues[lngIndex] % 2 == 0)
		{
			*plngEvenTotal += alngValues[lngIndex];
		}
	}
	printf("Array Even Total: %ld\n", *plngEvenTotal);
	printf("\n");
}