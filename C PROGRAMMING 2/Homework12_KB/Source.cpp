// ------------------------------------------------------------------------------
// FILE: Homework12_KB
// ABSTRACT: Uses recursion to compute sequence sums, factorials, and Fibonacci
// values. Demonstrates classic recursive techniques in C.
// ------------------------------------------------------------------------------

#include <stdio.h>
#include <stdlib.h>

// ------------------------------------------------------------------------------
// Function prototypes
// ------------------------------------------------------------------------------
int  AddNumbers1To100Recursively(int intIndex);
int  AddNumbersInRange(int intStart, int intStop);
long long Factorial(int intNumber);
void DisplayFactorials1To20(void);
int  FindFibonacci(int intNumber);

// ------------------------------------------------------------------------------
// main: Execute all recursive demonstrations
// ------------------------------------------------------------------------------
int main(void)
{
    int intTotal;
    long long llngFactorial;
    int intFibonacci;

    // --------------------------------------------------------------------------
    // Part 1: Recursive Summation Examples
    // --------------------------------------------------------------------------
    printf("\n----------------------------------------------\n");
    printf("PART 1: ADDING WITH RECURSION\n");
    printf("----------------------------------------------\n");
    intTotal = AddNumbers1To100Recursively(0);
    printf("Sum from 1 to 100 = %d\n", intTotal);
    printf("Sum from 5 to 10  = %d\n", AddNumbersInRange(5, 10));
    printf("Sum from 50 to 75 = %d\n", AddNumbersInRange(50, 75));
    printf("Sum from 100 to 100 = %d\n", AddNumbersInRange(100, 100));

    // --------------------------------------------------------------------------
    // Part 2: Factorial Calculations
    // --------------------------------------------------------------------------
    printf("\n----------------------------------------------\n");
    printf("PART 2: FACTORIAL TESTS\n");
    printf("----------------------------------------------\n");
    llngFactorial = Factorial(5);
    printf("5!  = %lld\n", llngFactorial);
    printf("25! = %lld (max safe)\n", Factorial(25));
    printf("26! = %lld (overflow)\n", Factorial(26));

    // --------------------------------------------------------------------------
    // Part 3: Factorial Table 1–20
    // --------------------------------------------------------------------------
    printf("\n----------------------------------------------\n");
    printf("PART 3: FACTORIALS FROM 1 TO 20\n");
    printf("----------------------------------------------\n");
    DisplayFactorials1To20();

    // --------------------------------------------------------------------------
    // Part 4: Fibonacci Sequence
    // --------------------------------------------------------------------------
    printf("\n----------------------------------------------\n");
    printf("PART 4: FIBONACCI TEST CASES\n");
    printf("----------------------------------------------\n");
    intFibonacci = FindFibonacci(5);
    printf("F(5) = %d\n", intFibonacci);
    intFibonacci = FindFibonacci(10);
    printf("F(10) = %d\n", intFibonacci);
    intFibonacci = FindFibonacci(46);
    printf("The largest number I could use without overflowing the datatype is 46.\n");
    printf("F(46) = %d\n", intFibonacci);
    printf("Going any higher than 46 overflows the data type.\n");

    system("pause");
    return 0;
}

//================================================================================
// Recursion Limit Notes
//================================================================================
// MAXIMUM RECURSION TESTED: 3575 – cannot go higher without crashing.
// SYSTEM MEMORY: 32 GB RAM
//================================================================================

//================================================================================
// Factorial Limits Notes
//================================================================================
// MAXIMUM FACTORIAL: 25!
// - Using long long, the largest safe factorial before overflow is 25!.
// - 26! causes overflow with long long.
//================================================================================

//================================================================================
// Fibonacci Limits Notes
//================================================================================
// HIGHEST STABLE FIBONACCI INDEX: 46
// Beyond this point, integer overflow corrupts outputs
// and deep recursion results in drastic slowdowns.
//================================================================================

// ------------------------------------------------------------------------------
// Recursively accumulates numbers 1 through 100
// ------------------------------------------------------------------------------
int AddNumbers1To100Recursively(int intIndex)
{
    int sum = intIndex + 1;
    if (intIndex + 1 < 100)
    {
        sum += AddNumbers1To100Recursively(intIndex + 1);
    }
    return sum;
}

// ------------------------------------------------------------------------------
// Recursively sums all integers from start to stop (inclusive)
// ------------------------------------------------------------------------------
int AddNumbersInRange(int intStart, int intStop)
{
    if (intStart > intStop)
        return 0;
    return intStart + AddNumbersInRange(intStart + 1, intStop);
}

// ------------------------------------------------------------------------------
// Recursive factorial: returns n! for n >= 0
// ------------------------------------------------------------------------------
long long Factorial(int intNumber)
{
    return (intNumber <= 1) ? 1LL : intNumber * Factorial(intNumber - 1);
}

// ------------------------------------------------------------------------------
// Prints factorials for 1 to 20 in a single column
// ------------------------------------------------------------------------------
void DisplayFactorials1To20(void)
{
    printf(" %-16s    %-16s\n", "n! = value", "n! = value");
    for (int i = 1; i <= 10; ++i)
    {
        long long left = Factorial(i);
        long long right = Factorial(i + 10);
        printf("%2d! = %-10lld    %2d! = %-10lld\n", i, left, i + 10, right);
    }
}


// ------------------------------------------------------------------------------
// Recursive Fibonacci: F(1)=F(2)=1, F(n)=F(n-1)+F(n-2) for n>2
// ------------------------------------------------------------------------------
int FindFibonacci(int intNumber)
{
    return (intNumber <= 2) ? 1 : FindFibonacci(intNumber - 1) + FindFibonacci(intNumber - 2);
}
