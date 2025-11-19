// --------------------------------------------------------------------------------
// Name: Keith Brock
// Class: C Programming 1 
// Abstract: Project 1
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
// Globals
// --------------------------------------------------------------------------------
double dblGrossTotal = 0;
double dblNetTotal = 0;

// --------------------------------------------------------------------------------
// Constants
// --------------------------------------------------------------------------------
 
// --------------------------------------------------------------------------------
// User Defined Types (UDT)
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Prototypes
// --------------------------------------------------------------------------------
//void DisplayInstructions();
void GetYearsWorked(int* pintYearsWorked);
void GatherInputs(int* pintYearsWorked, double* pdblPreviousPurchases, char* pcharPosition, double* pdblCurrentPurchases);
void GetPreviousPurchases(double* pdblPreviousPurchases);
void GetEmployeeRole(char* pchrPosition);
void GetCurrentPurchases(double* pdblCurrentPurchases);
void CalculateEmpDiscountPercent(int intYearsWorked, char chrEmployeePosition, double* pdblEmployeeDiscountPercent);
void CalculateYTDDiscount(double dblPreviousPurchases, double dblEmployeeDiscountPercent, double* dblYTDDiscount);
void CalculateCurrentDiscount(double dblEmployeeDiscountPercent, double dblCurrentPurchase, double dblYTDDiscount, double* pdblEmployeeDiscount);
void ClearAllVariables(char* pchrPosition);
void CalculateTotals(double dblCurrentPurchase, double dblEmployeeDiscount, double* dblTotalPrice, double* dblGrossTotal, double* dblNetTotal);
void DisplayOutput(double dblEmployeeDiscountPercent, double dblYTDDiscount, double dblCurrentPurchase, double dblEmployeeDiscount, double dblTotalPrice);
void NextEmployee(char* pchrAnotherEmployee);
void PrintDailyTotals();

// --------------------------------------------------------------------------------
// Name: main
// Abstract: This is where the program starts
// --------------------------------------------------------------------------------
int main()
{	
	char chrAnotherEmployee = 'Y';
	int intYearsWorked = 0;
	double dblPreviousPurchases = 0;
	char chrPosition = 'E';							// 'M' = Manager, 'E' = Employee
	double dblCurrentPurchase = 0;
	double dblEmployeeDiscountPercent = 0;
	double dblYTDDiscount = 0;
	double dblEmployeeDiscount = 0;
	double dblTotalPrice = 0;

	
	do 
	{
	// Collect Input
		ClearAllVariables(&chrPosition);
		GatherInputs(&intYearsWorked, &dblPreviousPurchases, &chrPosition, &dblCurrentPurchase);
		CalculateEmpDiscountPercent(intYearsWorked, chrPosition, &dblEmployeeDiscountPercent);
		CalculateYTDDiscount(dblPreviousPurchases, dblEmployeeDiscountPercent, &dblYTDDiscount);
		CalculateCurrentDiscount(dblEmployeeDiscountPercent, dblCurrentPurchase, dblYTDDiscount, &dblEmployeeDiscount);
		CalculateTotals(dblCurrentPurchase, dblEmployeeDiscount, &dblTotalPrice, &dblGrossTotal, &dblNetTotal);
		DisplayOutput(dblEmployeeDiscountPercent, dblYTDDiscount, dblCurrentPurchase, dblEmployeeDiscount, dblTotalPrice);
		NextEmployee(&chrAnotherEmployee);
	} while (chrAnotherEmployee == 'Y' || chrAnotherEmployee == 'y');
	
	PrintDailyTotals();	
}



// --------------------------------------------------------------------------------
// Name: ClearAllVariables
// Abstract: Clears all Variables
// --------------------------------------------------------------------------------
void ClearAllVariables(char* pchrPosition)
{
	*pchrPosition = ' ';							// 'M' = Manager, 'E' = Employee	
}



// --------------------------------------------------------------------------------
// Name: GatherInputs
// Abstract: Gets employee information from the user
// --------------------------------------------------------------------------------
void GatherInputs(int* pintYearsWorked, double* pdblPreviousPurchases, char* pchrPosition, double* pdblCurrentPurchases)
{
	GetYearsWorked(pintYearsWorked);
	GetPreviousPurchases(pdblPreviousPurchases);
	GetEmployeeRole(pchrPosition);
	GetCurrentPurchases(pdblCurrentPurchases);
}

// --------------------------------------------------------------------------------
// Name: GetYearsWorked
// Abstract: Get years from the user and clears the input.
// --------------------------------------------------------------------------------
void GetYearsWorked(int* pintYearsWorked)
{
	int intYearsWorked = 0;

	do
	{
		if (intYearsWorked <= 0)
		{
			printf("Enter number of years employed (must be > 0): ");
			scanf(" %d", &intYearsWorked);	
			
			if (intYearsWorked == 0)
			{
				printf("Invalid input.  Please enter a positive whole number.\n");
				fseek(stdin, 0, SEEK_END);
			}
		}
	} while (intYearsWorked <= 0);

	*pintYearsWorked = intYearsWorked;
}



// --------------------------------------------------------------------------------
// Name: GetPreviousPurchases
// Abstract: Get Previous Purchases from the user and clears the input.
// --------------------------------------------------------------------------------
void GetPreviousPurchases(double* pdblPreviousPurchases)
{
	int dblPreviousPurchases = 0;

	do
	{
		if (dblPreviousPurchases <= 0)
		{
			printf("How much has the employee spent this year (before the discount)? $");
			scanf(" %d", &dblPreviousPurchases);

			if (dblPreviousPurchases == 0)
			{
				printf("Invalid input.  Please enter a positive number.\n");
				fseek(stdin, 0, SEEK_END);
			}
		}
	} while (dblPreviousPurchases <= 0);

	*pdblPreviousPurchases = dblPreviousPurchases;
}



// --------------------------------------------------------------------------------
// Name: GetEmployeeRole
// Abstract: Asks if you are a Manager or Employee
// --------------------------------------------------------------------------------
void GetEmployeeRole(char* pchrPosition)
{
	char chrRole = ' ';

	do
	{
		printf("Enter employee status (M for Manager, E for Employee): ");
		scanf(" %c", &chrRole);
		if (chrRole != 'E' && chrRole != 'M')
		{
			printf("Invalid input.  Must be 'M' or 'E'.\n");
			fseek(stdin, 0, SEEK_END);
		}
	} while (chrRole != 'E' && chrRole != 'M');

	*pchrPosition = chrRole;
}



// --------------------------------------------------------------------------------
// Name: GetCurrentPurchases
// Abstract: Get Current Purchases from the user and clears the input.
// --------------------------------------------------------------------------------
void GetCurrentPurchases(double* pdblCurrentPurchases)
{
	int dblCurrentPurchases = 0;

	do
	{
		if (dblCurrentPurchases <= 0)
		{
			printf("What is the total of the current purchase? $");
			scanf(" %d", &dblCurrentPurchases);

			if (dblCurrentPurchases == 0)
			{
				printf("Invalid input.  Please enter a positive number.\n");
				fseek(stdin, 0, SEEK_END);
			}
		}
	} while (dblCurrentPurchases <= 0);

	*pdblCurrentPurchases = dblCurrentPurchases;
}



// --------------------------------------------------------------------------------
// Name: CalculateEmployeeDiscountPercent
// Abstract: Calculates Employee Discount %
// --------------------------------------------------------------------------------
void CalculateEmpDiscountPercent(int intYearsWorked, char chrEmployeePosition, double* pdblEmployeeDiscountPercent)
{
	double dblBaseDiscount = 0.0;  // Base discount for employees
	double dblManagerBonus = 0.0;  // Additional discount for managers

	// Assign base discount according to years worked
	if (intYearsWorked > 15)
	{
		dblBaseDiscount = 0.30;  
	}
	else if (intYearsWorked > 10)
	{
		dblBaseDiscount = 0.25;  
	}
	else if (intYearsWorked > 6)
	{
		dblBaseDiscount = 0.20;  
	}
	else if (intYearsWorked > 3)
	{
		dblBaseDiscount = 0.14;  
	}
	else
	{
		dblBaseDiscount = 0.10;  
	}

	if (chrEmployeePosition == 'M')
	{
		dblManagerBonus = 0.10;  // Managers get an additional 10% discount
	}

	// Combine base discount with manager bonus (if applicable)
	*pdblEmployeeDiscountPercent = dblBaseDiscount + dblManagerBonus;
}



// --------------------------------------------------------------------------------
// Name: CalculateYTDDiscount
// Abstract: Calculates YTD Discounts
// --------------------------------------------------------------------------------
void CalculateYTDDiscount(double dblPreviousPurchases, double dblEmployeeDiscountPercent, double* dblYTDDiscount)
{
	// Calculate the YTD discount
	*dblYTDDiscount = dblPreviousPurchases * dblEmployeeDiscountPercent;

	// Ensure that the YTD discount does not exceed $200
	if (*dblYTDDiscount > 200.00)
	{
		*dblYTDDiscount = 200.00;  // Cap the YTD discount at $200
	}
}



// --------------------------------------------------------------------------------
// Name: CalculateCurrentDiscount
// Abstract: Calculates Current Discount
// --------------------------------------------------------------------------------
void CalculateCurrentDiscount(double dblEmployeeDiscountPercent, double dblCurrentPurchase, double dblYTDDiscount, double* pdblEmployeeDiscount)
{
	double dblAllowableDiscount = 200.00;  // Maximum allowable discount
	double dblCurrentPurchaseDiscount = dblEmployeeDiscountPercent * dblCurrentPurchase;

	// Check if YTD discount has already reached or exceeded the limit
	if (dblYTDDiscount >= dblAllowableDiscount)
	{
		dblCurrentPurchaseDiscount = 0;  // No discount if YTD discount exceeds $200
	}
	else
	{
		// Calculate how much discount is left until the $200 limit
		dblAllowableDiscount -= dblYTDDiscount;

		// Cap the discount if the current purchase discount exceeds the allowable limit
		if (dblCurrentPurchaseDiscount > dblAllowableDiscount)
		{
			dblCurrentPurchaseDiscount = dblAllowableDiscount;
		}
	}

	// Assign the calculated discount to the output parameter
	*pdblEmployeeDiscount = dblCurrentPurchaseDiscount;
}




// --------------------------------------------------------------------------------
// Name: CalculateTotals
// Abstract: Calculates Current Totals
// --------------------------------------------------------------------------------
void CalculateTotals(double dblCurrentPurchase, double dblEmployeeDiscount, double* dblTotalPrice, double* dblGrossTotal, double* dblNetTotal)
{
	*dblTotalPrice = dblCurrentPurchase - dblEmployeeDiscount;

	*dblGrossTotal += dblCurrentPurchase;

	*dblNetTotal += *dblTotalPrice;
}



// --------------------------------------------------------------------------------
// Name: DisplayOutput
// Abstract: Displays the Output
// --------------------------------------------------------------------------------
void DisplayOutput(double dblEmployeeDiscountPercent, double dblYTDDiscount, double dblCurrentPurchase, double dblEmployeeDiscount, double dblTotalPrice)
{
	int intDiscountPercent = dblEmployeeDiscountPercent * 100;
	printf("\n-----------------------------------------------------------\n");
	printf("Current Purchase:\n");
	printf("-----------------------------------------------------------\n");
	printf("Employee Discount: %d%%\n", intDiscountPercent);
	printf("YTD Discount: $%.2f\n", dblYTDDiscount);
	printf("Purchase total today before discount: $%.2f\n", dblCurrentPurchase);
	printf("Discount for today's purchase: $%.2f\n", dblEmployeeDiscount);
	printf("Total with discount: $%.2f\n", dblTotalPrice);
	printf("-----------------------------------------------------------\n");
	printf("\n");
}

// --------------------------------------------------------------------------------
// Name: NextEmployee
// Abstract: Determines if there is more employees to enter.
// --------------------------------------------------------------------------------
void NextEmployee(char* pchrAnotherEmployee)
{
	char chrNext = ' ';

	do
	{
		printf("Is there another employee? (Y/N): ");
		scanf(" %c", &chrNext);
		if (chrNext != 'Y' && chrNext != 'N')
		{
			printf("Invalid input.  Must be 'Y' or 'N'.\n");
			fseek(stdin, 0, SEEK_END);
		}
	} while (chrNext != 'Y' && chrNext != 'N');

	printf("-----------------------------------------------------------\n");

	*pchrAnotherEmployee = chrNext;
}



// --------------------------------------------------------------------------------
// Name: PrintDailyTotals
// Abstract: Print the Daily Totals After The Loop using Global Variables
// --------------------------------------------------------------------------------
void PrintDailyTotals()
{
	printf("Total Before Discounts For The Day: $%.2f\n", dblGrossTotal);
	printf("Total After Discounts Applied: $%.2f\n", dblNetTotal);
	printf("-----------------------------------------------------------\n");
}