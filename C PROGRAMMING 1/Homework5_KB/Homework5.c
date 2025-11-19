// --------------------------------------------------------------------------------
// Name: Keith Brock
// Class: C Programming 1 
// Abstract: Homework 5 - Various Problems
// --------------------------------------------------------------------------------

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
//const long lngARRAY_SIZE = 100; // This is just an example - you can delete this line

// --------------------------------------------------------------------------------
// User Defined Types (UDT)
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Prototypes
// --------------------------------------------------------------------------------
	void DisplayInstructions();
	void DisplayMessage(int intPrintCount);
	int GetLargerValue(int intValue1, int intValue2);
	int GetLargestValue(int intValue1, int intValue2, int intValue3, int intValue4, int intValue5, int intValue6, int intValue7);
	float CalculateSphereVolume(int intDiameter);
	void PassIntByPointer(int* pintValue1);
	float SolvePythagoreanTheorem(float* sngA, float* sngB, float* sngC);
	int FindQuadraticRoots(int intA, int intB, int intC, float* psngRoot1, float* psngRoot2);
	void DisplayFirst20Factorials();

// --------------------------------------------------------------------------------
// Name: main
// Abstract: This is where the program starts
// --------------------------------------------------------------------------------
int main()
{
	int intLargerValue = 0;
	int intLargestValue = 0;
	float sngVolume = 0;
	int intValue = 100;
	float sngA = 9;
	float sngB = 12;
	float sngC = 0;
	int intA = -1;
	int intB = 3;
	int intC = -2;
	float sngRoot1 = 0;
	float sngRoot2 = 0;
	int intResult = 0;

	//Problem #1 - Display Instructions
	printf("Problem #1 - Display Instructions\n");
	DisplayInstructions();
	printf("\n");

	//Problem #2 - Print message X times
	printf("Problem #2 - Print Message X Times\n");
	DisplayMessage(10);
	printf("\n");

	//Problem #3 - GetLargerValue
	printf("Problem #3 - GetLargerValue\n");
	intLargerValue = GetLargerValue(5, 10);
	printf("The larger value is: %d\n", intLargerValue);
	printf("\n");

	//Probelm #4 - GetLargestValue
	printf("Problem #4 - GetLargestValue\n");
	intLargestValue = GetLargestValue(1, 4, 7, 355, 5, 150, 2);
	printf("The largest value is: %d\n", intLargestValue);
	printf("\n");

	//Problem #5 - CalculateSphereVolume
	printf("Problem #5 - CalculateSphereVolume\n");
	sngVolume = CalculateSphereVolume(10);
	printf("The Volume of a Sphere is: %.2f\n", sngVolume);
	printf("\n");

	//Problem #6 - PassIntByPointer
	printf("Problem #6 - PassIntByPointer\n");
	printf("Original value before calling PassIntByPointer: %d\n", intValue);
	PassIntByPointer(&intValue);
	printf("Value after calling PassIntByPointer: %d\n", intValue);
	printf("\n");

	//Problem #7 - SolvePythagoreanTheorem
	printf("Problem #7 - SolvePythagoreanTheorem\n");
	SolvePythagoreanTheorem(&sngA, &sngB, &sngC);
	printf("A = %f, B = %f, C = %f\n", sngA, sngB, sngC);
	printf("\n");

	//Problem #8 - FindQuadraticRoots
	printf("Problem #8 - FindQuadraticRoots\n");
	intResult = FindQuadraticRoots(intA, intB, intC, &sngRoot1, &sngRoot2);
	if (intResult == 1)
	{
		printf("Two real roots:  Root1 = %.2f, Root2 = %.2f\n", sngRoot1, sngRoot2);
	}
	else if (intResult == 0)
	{
		printf("One real root:  Root = %.2f\n", sngRoot1);
	}
	else
	{
		printf("No roots in real numbers.\n");
	}
	printf("\n");

	//Problem #8 - DisplayFirst20Factorials
	printf("Problem #9 - DisplayFirst20Factorials\n");
	DisplayFirst20Factorials();

	system("pause");
}



// --------------------------------------------------------------------------------
// Name: DisplayInstructions
// Abstract: Tell the user what's going on
// --------------------------------------------------------------------------------
void DisplayInstructions()
{
	printf("This program will demonstrate how to makeand use procedures in C.\n");
	printf("In addition it will demonstrate how to pass valuesand variables into\n");
	printf("a procedure as parameters. It will demonstrate how to return a value\n");
	printf("from a function using the return keyword. It will demonstrate how to\n");
	printf("emulate passing variables by reference using pointers.\n");
}



// --------------------------------------------------------------------------------
// Name: DisplayMessage
// Abstract: Print the message X number of times
// --------------------------------------------------------------------------------
void DisplayMessage(int intPrintCount)
{
	int intIndex = 0;

		for (intIndex = 1; intIndex <= intPrintCount; intIndex += 1)
		{
			printf("I love my wife.\n");
		}
}



// --------------------------------------------------------------------------------
// Name: GetLargerValue
// Abstract: Return the larger of the two values
// --------------------------------------------------------------------------------
int GetLargerValue(int intValue1, int intValue2)
{
	int intLargerValue = 0;

	if (intValue1 > intValue2)
	{
		intLargerValue = intValue1;
	}
	else
	{
		intLargerValue = intValue2;
	}

	return intLargerValue;
}



// --------------------------------------------------------------------------------
// Name: GetLargestValue
// Abstract: Return the larger of the seven values
// --------------------------------------------------------------------------------
int GetLargestValue(int intValue1, int intValue2, int intValue3, int intValue4, int intValue5, int intValue6, int intValue7)
{
	int intLargestValue = intValue1;

	if (intValue2 > intLargestValue)
	{
		intLargestValue = intValue2;
	}
	if (intValue3 > intLargestValue)
	{
		intLargestValue = intValue3;
	}
	if (intValue4 > intLargestValue)
	{
		intLargestValue = intValue4;
	}
	if (intValue5 > intLargestValue)
	{
		intLargestValue = intValue5;
	}
	if (intValue6 > intLargestValue)
	{
		intLargestValue = intValue6;
	}
	if (intValue7 > intLargestValue)
	{
		intLargestValue = intValue7;
	}

	return intLargestValue;
}



// --------------------------------------------------------------------------------
// Name: CalculateSphereVolume
// Abstract: Calculate the Sphere Volume of a Diameter
// --------------------------------------------------------------------------------
float CalculateSphereVolume(int intDiameter)
{
	float sngRadius = intDiameter / 2.0F;
	float sngVolume = (4.0f / 3.0f) * 3.1415927f * sngRadius * sngRadius * sngRadius;

	return sngVolume;		
}



// --------------------------------------------------------------------------------
// Name: PassIntByPointer
// Abstract: Modify the value stored at the pointer
// --------------------------------------------------------------------------------
void PassIntByPointer(int* pintValue1)
{
	*pintValue1 = 50;
}



// --------------------------------------------------------------------------------
// Name: SolvePythagoreanTheorem 
// Abstract: Solve the Pythagorean Theorem
// --------------------------------------------------------------------------------
float SolvePythagoreanTheorem(float* sngA, float* sngB, float* sngC)
{
	if (*sngA == 0 )
	{
		*sngA = sqrt(pow(*sngC, 2) - pow(*sngB, 2));
	}

	else if (*sngB == 0)
	{
		*sngB = sqrt(pow(*sngC, 2) - pow(*sngA, 2));
	}

	else if (*sngC == 0)
	{
		*sngC = sqrt(pow(*sngA, 2) + pow(*sngB, 2));
	}
	else
	{
		printf("Invalid input.\n");
	}
}



// --------------------------------------------------------------------------------
// Name: FindQuadraticRoots
// Abstract: Solves for the real roots of a quadratic equation using the quadratic formula.
// --------------------------------------------------------------------------------
int FindQuadraticRoots(int intA, int intB, int intC, float* psngRoot1, float* psngRoot2)
{
	int intDiscriminant = intB * intB - 4 * intA * intC;
	int intResult = 0;

	if (intDiscriminant > 0)
	{
		*psngRoot1 = (-intB + sqrt(intDiscriminant)) / (2 * intA);
		*psngRoot2 = (-intB - sqrt(intDiscriminant)) / (2 * intA);
		intResult = 1;
	}
	else if (intDiscriminant == 0)
	{
		*psngRoot1 = *psngRoot2 = -intB / (2 * intA);
		intResult = 0;
	}
	else
	{
		intResult = -1;
	}
	return intResult;
}



// --------------------------------------------------------------------------------
// Name: DisplayFirst20Factorials
// Abstract: Displays the first 20 factorials in two columns.
// --------------------------------------------------------------------------------
void DisplayFirst20Factorials() {
	int intIndex = 0;

	long long lngFactorial = 1; 

	printf("N\tN!\n");

	for (intIndex = 1; intIndex <= 20; intIndex++) {
		lngFactorial *= intIndex;  
		printf("%d\t%llu\n", intIndex, lngFactorial);
	}
}
