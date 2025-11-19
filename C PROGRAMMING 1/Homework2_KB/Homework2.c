// ------------------------------------------------------------------------------------------
// Name: Keith Brock
// Class: C Programming
// Abstract: Homework 2  
// ------------------------------------------------------------------------------------------
#define _CRT_SECURE_NO_WARNINGS

// ------------------------------------------------------------------------------------------
// Includes
// ------------------------------------------------------------------------------------------
#include <stdio.h>
#include <stdlib.h>
#include <conio.h>


// ------------------------------------------------------------------------------------------
// Constants
// ------------------------------------------------------------------------------------------
const long lngARRAY_SIZE = 100;

// ------------------------------------------------------------------------------------------
// Prototypes
// ------------------------------------------------------------------------------------------

// ------------------------------------------------------------------------------------------
// Name: Main
// Abstract: This is where the program starts
// ------------------------------------------------------------------------------------------
int main()
{
	// ------------------------------------------------------------------------------------------
	// Problem #1 – Print number in decimal and exponential notation.
	// ------------------------------------------------------------------------------------------
	float sngUserValue = 0;
	printf("Problem #1 - Enter a decimal number: \n");
	scanf("%f", &sngUserValue);

	printf("The number in decimal format is %f\n", sngUserValue);
	printf("and in exponential format is %e\n", sngUserValue);
	printf("\n");
	printf("\n");

	// ------------------------------------------------------------------------------------------
	// Problem #2 – Display age in seconds
	// ------------------------------------------------------------------------------------------
	//Do the math: 365 days/year * 24 hours/day * 60 minutes/hour * 60 seconds/minute

	float sngAgeInYears = 0;
	float sngSecondsPerYear = 365 * 24 * 60 * 60;
	float sngAgeInSeconds = 0;

	printf("Problem 2 - What is your age in years (e.g. 4.5)?\n");
	scanf("%f", &sngAgeInYears);

	sngAgeInSeconds = sngAgeInYears * sngSecondsPerYear;

	printf("Your age in seconds is %f\n", sngAgeInSeconds);
	printf("\n");
	printf("\n");

	// ------------------------------------------------------------------------------------------
	// Problem #3 – Display number of water molecules in defined quarts of water.
	// ------------------------------------------------------------------------------------------
	float sngMassOfWaterMolecule = 3.0e-23;
	float sngMassOfQuart = 950.0;
	float sngQuartsOfWater = 0;
	float sngNumberOfMolecules = 0;

	printf("Problem 3 - How many quarts of water are there?\n");
	scanf("%f", &sngQuartsOfWater);
	
	sngNumberOfMolecules = (sngQuartsOfWater * sngMassOfQuart) / sngMassOfWaterMolecule;
	printf("The number of water molecules in %f quarts of water is %e molecules.", sngQuartsOfWater, sngNumberOfMolecules);
	printf("\n");
	printf("\n");

	// ------------------------------------------------------------------------------------------
	// Problem #4 – Display number of water molecules in defined quarts of water.
	// ------------------------------------------------------------------------------------------
	int intNumberOne = 0;
	int intNumberTwo = 0;
	int intNumberThree = 0;
	int intNumberFour = 0;
	int intLargestNumber = 0;

	printf("Problem 4 - You will be entering in four whole numbers.\n");

	printf("Enter the first whole number:\n");
	scanf("%d", &intNumberOne);

	printf("Enter the second whole number:\n");
	scanf("%d", &intNumberTwo);

	printf("Enter the third whole number:\n");
	scanf("%d", &intNumberThree);

	printf("Enter the fourth whole number:\n");
	scanf("%d", &intNumberFour);

	intLargestNumber = intNumberOne;

	////Start comparing numbers to find out the largest number.

	if (intNumberTwo > intLargestNumber)
	{
		intLargestNumber = intNumberTwo;
	}
	if (intNumberThree > intLargestNumber)
	{
		intLargestNumber = intNumberThree;
	}
	if (intNumberFour > intLargestNumber)
	{
		intLargestNumber = intNumberFour;
	}

	printf("The largest of the four numbers is: %d\n", intLargestNumber);
	printf("\n");
	printf("\n");

	// ------------------------------------------------------------------------------------------
	// Problem #5 – Determine if entered year is a leap year.
	// ------------------------------------------------------------------------------------------

	int intInputYear = 0;
	int intLeapHundred = 400;
	int intLeapYear = 4;
	
	printf("Problem 5 - Please enter a whole number between 1500 and 2200 to determine the leap year: \n");
	scanf("%d", &intInputYear);

	if (intInputYear < 1500 || intInputYear > 2200)
	{
		printf("Invalid Year");
	}

	intLeapHundred = intInputYear % intLeapHundred;
	intLeapYear = intInputYear % intLeapYear;

	if (intLeapYear == 0)
	{
		if (intLeapHundred == 0)
		{
			printf("Yes, %d is a leap year.\n", intInputYear);
		}
		else
		{
			printf("No, %d is not a leap year.\n", intInputYear);
		}
	}
	else
	{
		printf("No, %d is not a leap year.\n", intInputYear);
	}
	
	printf("\n");
	printf("\n");
	 
	// ------------------------------------------------------------------------------------------
	// Problem #6 – Determine Employee Net Pay
	// ------------------------------------------------------------------------------------------
	float sngHourlyPayRate = 0;
	float sngHoursWorked = 0;
	float sngGrossPay = 0;
	float sngNetPay = 0;
	float sngTaxes = 0;

	printf("Problem 6 - Enter your hourly pay rate:\n");
	scanf("%f", &sngHourlyPayRate);
	printf("Enter your total hours: \n");
	scanf("%f", &sngHoursWorked);

	// Check if both values are positive
	if (sngHourlyPayRate > 0 && sngHoursWorked > 0)
	{
		// Calculate the gross pay
		sngGrossPay = sngHourlyPayRate * sngHoursWorked;
		// Calculate the taxes based on the gross pay
		if (sngGrossPay <= 300) {
			sngTaxes = sngGrossPay * 0.15; // 15% tax on the first $300
		}
		else if (sngGrossPay <= 450) {
			sngTaxes = (300 * 0.15) + ((sngGrossPay - 300) * 0.20); // 15% on first $300, 20% on the next $150
		}
		else {
			sngTaxes = (300 * 0.15) + (150 * 0.20) + ((sngGrossPay - 450) * 0.25); // 15% on first $300, 20% on the next $150, 25% on the rest
		}

		// Calculate net pay
		sngNetPay = sngGrossPay - sngTaxes;

		// Output the results
		printf("Gross Pay: $%f\n", sngGrossPay);
		printf("Taxes: $%f\n", sngTaxes);
		printf("Net Pay: $%f\n", sngNetPay);
	}
	else {
		printf("Either the pay rate or hours worked is a negative number.\n");
	}
	return 0;
}