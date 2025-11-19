// ------------------------------------------------------------------------------------------
// Name: Keith Brock
// Class: C Programming
// Abstract: Homework 3 - Introduction to Loops
// ------------------------------------------------------------------------------------------
#define _CRT_SECURE_NO_WARNINGS

// ------------------------------------------------------------------------------------------
// Includes
// ------------------------------------------------------------------------------------------
#include<stdio.h>
#include<stdlib.h>
#include<math.h>

// ------------------------------------------------------------------------------------------
// Constants
// ------------------------------------------------------------------------------------------

// ------------------------------------------------------------------------------------------
// Prototypes
// ------------------------------------------------------------------------------------------

// ------------------------------------------------------------------------------------------
// Name: main
// Abstract: This is where the program starts
// ------------------------------------------------------------------------------------------
void main()
{
	int intIndex = 1;
	int intTotal = 0;
	int intTotals = 0;
	int intTotalEven = 0;
	int intTotalThird = 0;
	int intUserValue = 0;
	int intMultiples = 0;
	int intTotalSquare = 0;
	int intGradeCount = 0;
	float sngGrades = 0;
	float sngFinalGrade = 0;
	float sngSquareRoot = 0;
	char strLetter;
	char strLowerCaseLetter;

	// --------------------------------------------------------------------------------
	// Problem #1 – Print all the whole numbers from 1 to 100.
	// --------------------------------------------------------------------------------
	printf("Problem #1 - Print all the whole numbers from 1 to 100.\n");
	printf("\n");
	for (intIndex = 1; intIndex <= 100; intIndex += 1)
	{
		printf("%d, ", intIndex);

		// Move to a new line every 10 numbers
		if (intIndex % 10 == 0)
		{
			printf("\n");
		}
	}
	printf("\n");
	printf("\n");


	// --------------------------------------------------------------------------------
	// Problem #2 – Write a loop that will add all the whole numbers from 1 and 100 and print the total.
	// --------------------------------------------------------------------------------
	
	printf("Problem #2 - Add all the whole numbers from 1 to 100 and print the total.\n");

	for (intIndex = 1; intIndex <= 100; intIndex += 1)
	{
		intTotal += intIndex;
	}

	printf("The sum of whole numbers 1 to 100 is: %d.\n", intTotal);
	
	printf("\n");
	printf("\n");


	// --------------------------------------------------------------------------------
	// Problem #3 – Write a loop that will add all the ODD numbers 7 to 313 and print the total.
	// --------------------------------------------------------------------------------
	printf("Problem #3 - Add all the ODD whole numbers from 7 to 313 and print the total.\n");

	for (intIndex = 7; intIndex <= 313; intIndex += 2)
	{
		intTotals += intIndex;
	}

	printf("The sum of ODD whole numbers 7 to 313 is: %d.\n", intTotals);

	printf("\n");
	printf("\n");
	

	// --------------------------------------------------------------------------------
	// Problem #4 – Write a loop that will add all the EVEN numbers -2 to -146 and print the total.
	// --------------------------------------------------------------------------------
	printf("Problem #4 - Add all the EVEN whole numbers from -2 to -146 and print the total.\n");

	for (intIndex = -2; intIndex >= -146; intIndex -= 2)
	{
		intTotalEven += intIndex;
	}

	printf("The sum of EVEN whole numbers -2 to -146 is: %d\n", intTotalEven);

	printf("\n");
	printf("\n");

	// --------------------------------------------------------------------------------
	// Problem #5 – Write a loop that will add every 3rd number from 2000 and -60 and print the total.
	// --------------------------------------------------------------------------------
	printf("Problem #5 - Add every 3rd number from 2000 and -60 and print the total.\n");

	for (intIndex = 2000; intIndex >= -60; intIndex -= 3)
	{
		intTotalThird += intIndex;
	}

	printf("The sum of every 3rd number from 2000 and -60 is: %d.\n", intTotalThird);

	printf("\n");
	printf("\n");

	// --------------------------------------------------------------------------------
	// Problem #6 – Prompt user for number 1 through 100 and then get square root.
	// --------------------------------------------------------------------------------
	printf("Problem #6 - Write a loop that will prompt the user to enter a number from 1 to 100.\n");
	printf("The program will continue to loop until a number within that range is entered.  After\n");
	printf("the loop, print out the square root of the number.\n");
	printf("\n");

	do
	{
		printf("Please enter a number 1 through 100: ");
		scanf("%d", &intUserValue);
	} while (intUserValue < 1 || intUserValue > 100);

	for (intIndex = 1; intIndex <= intUserValue; intIndex += 1)
	{
		intTotalSquare += intIndex;
	}

	sngSquareRoot = sqrt(intTotalSquare);
	printf("The square root of %d is %lf\n", intTotalSquare, sngSquareRoot);
	printf("\n");
	printf("\n");

	// --------------------------------------------------------------------------------
	// Problem #7 – Average Grades and figure out letter grade.
	// --------------------------------------------------------------------------------
	printf("Problem #7 - Write a loop that you enter test scores.  Program will loop until -1 is entered.\n");
	printf("The program will then figure out your grade.\n");
	printf("\n");

	printf("Please enter a grade between 0 and 100.  Enter -1 to Exit and Receive Grade: ");
	scanf("%d", &intUserValue);

	while (intUserValue != -1)
	{
		if (intUserValue >= 0 && intUserValue <= 100)
		{
			sngGrades += intUserValue;
			intGradeCount += 1;
		}
		else
		{
			printf("Invalid input.  Please enter a grade between 0 and 100.\n");
		}

		//Ask for the next grade
		printf("Please enter a grade between 0 and 100.  Enter -1 to Exit and Receive Grade: ");
		scanf("%d", &intUserValue);
	}

	if (intGradeCount > 0) 
	{
		sngFinalGrade = sngGrades / intGradeCount;		

		if (sngFinalGrade >= 90)
		{
			printf("The average was %.2lf and the letter grade is A.\n", sngFinalGrade);
		}
		else if (sngFinalGrade >= 80 && sngFinalGrade < 90)
		{
			printf("The average was %.2lf and the letter grade is B.\n", sngFinalGrade);
		}
		else if (sngFinalGrade >= 70 && sngFinalGrade < 80)
		{
			printf("The average was %.2lf and the letter grade is C.\n", sngFinalGrade);
		}
		else if (sngFinalGrade >= 60 && sngFinalGrade < 70)
		{
			printf("The average was %2.lf and the letter grade is D.\n", sngFinalGrade);
		}
		else if (sngFinalGrade < 60)
		{
			printf("The average was %2.lf and the letter grade is F.\n", sngFinalGrade);
		}
	}
	printf("\n");
	printf("\n");

	// --------------------------------------------------------------------------------
	// Problem #8 – Add all multiples of 3 and 5 up to 1000.
	// --------------------------------------------------------------------------------
	printf("Problem #8 - This is going to calculate and print the sum of all the multiples\n");
	printf("of 3 or 5 that is below 1000.\n");

	for (intIndex = 0; intIndex < 1000; intIndex += 1)
	{
		if (intIndex % 3 == 0 || intIndex % 5 == 0)
		{
			intMultiples += intIndex;
		}
	}
	printf("The sum of all multiples of 3 or 5 below 1000 is: %d\n", intMultiples);
	printf("\n");
	printf("\n");

	// --------------------------------------------------------------------------------
	// Problem #9 – Prompt the user for a letter and return it in lowercase and upper case.
	// --------------------------------------------------------------------------------
	printf("Problem #9 - This program will return an entered letter in both upper case\n");
	printf("and in lower case.\n");
	printf("\n");

	// Loop to prompt the user for a valid uppercase letter
	do
	{
		printf("Enter an uppercase letter: \n");
		scanf(" %c", &strLetter);

		if (strLetter < 'A' || strLetter > 'Z')
		{
			printf("Invalid input. ");
		}
	} while (strLetter < 'A' || strLetter > 'Z');
	
	//Convert the uppercase letter to lowercase
	strLowerCaseLetter = strLetter + ('a' - 'A');

	//Print Output
	printf("Here is the ltter in uppercase: %c and in lowercase: %c.\n", strLetter, strLowerCaseLetter);

	system("pause");
}