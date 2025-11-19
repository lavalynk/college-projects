// --------------------------------------------------------------------------------
// Name: Keith Brock
// Class: SET-151
// Abstract: Homework 11 
// --------------------------------------------------------------------------------
#define _CRT_SECURE_NO_WARNINGS

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <math.h>

// --------------------------------------------------------------------------------
// Prototypes
// --------------------------------------------------------------------------------
int getArraySize();
float* allocateAndInitializeArray(int intArraySize);
void clearInputBuffer();
void populateArray(float* paintValues, int intSize);
void printArray(float* paintValues, int intSize);
float findMax(float* paintValues, int intSize);
float findMin(float* paintValues, int intSize);
float calculateTotal(float* paintValues, int intSize);
float calculateAverage(float dblTotal, int intSize);
float calculateStdDev(float* paintValues, int intSize, float dblAverage);
void AddValueToEnd(float** ppaintValues, int* pintArraySize, float valueToAdd);
void AddValueToFront(float** ppaintValues, int* pintArraySize, float valueToAdd);
void InsertValueAt(float** ppaintValues, int* pintArraySize, float valueToInsert, int intInsertIndex);
void RemoveAt(float** ppaintValues, int* pintArraySize, int removeIndex);

// --------------------------------------------------------------------------------
// Name: main
// Abstract: This is where the program starts.
// --------------------------------------------------------------------------------
int main()
{
    int intArraySize = 0;
    float* paintValues = 0;
    float dblTotalSum = 0.0;
    float dblMax = 0.0;
    float dblMin = 0.0;
    float dblAverage = 0.0;
    float dblStandardDeviation = 0.0;

    // Step 1: Prompt for array size
    intArraySize = getArraySize();

    // Step 2: Allocate memory & Initialize
    paintValues = allocateAndInitializeArray(intArraySize);

    // Step 3: Populate array
    populateArray(paintValues, intArraySize);

    // Step 4: Print array
    printArray(paintValues, intArraySize);

    // Test add/remove functions
    AddValueToEnd(&paintValues, &intArraySize, 1000.0);
    AddValueToFront(&paintValues, &intArraySize, 2000.0);
    InsertValueAt(&paintValues, &intArraySize, 3000.0, 2);
    RemoveAt(&paintValues, &intArraySize, 1);

    printf("\nArray After Modifications:\n");
    printArray(paintValues, intArraySize);

    // Step 5: Find and display statistics
    dblMax = findMax(paintValues, intArraySize);
    dblMin = findMin(paintValues, intArraySize);
    dblTotalSum = calculateTotal(paintValues, intArraySize);
    dblAverage = calculateAverage(dblTotalSum, intArraySize);
    dblStandardDeviation = calculateStdDev(paintValues, intArraySize, dblAverage);

    printf("\nArray Statistics:\n");
    printf("------------------------------------------------------------\n");
    printf("Maximum Value: %.2f\n", dblMax);
    printf("Minimum Value: %.2f\n", dblMin);
    printf("Total Sum: %.2f\n", dblTotalSum);
    printf("Average: %.2f\n", dblAverage);
    printf("Standard Deviation: %.2f\n", dblStandardDeviation);

    // Free allocated memory
    free(paintValues);

    system("Pause");
    return 0;
}



// --------------------------------------------------------------------------------
// Name: getArraySize
// Abstract: Gets the Size of the Array via User Input
// --------------------------------------------------------------------------------
int getArraySize()
{
    int intArraySize = 0;
    bool blnValid = false;

    while (!blnValid)
    {
        printf("Enter the array size (1 to 100000): ");

        // Check if input is a valid integer
        if (scanf("%d", &intArraySize) == 1) {
            if (intArraySize >= 1 && intArraySize <= 100000) {
                blnValid = true;  // Valid size entered, exit loop
            }
            else {
                printf("Invalid size! Please enter a number between 1 and 100000.\n");
            }
        }
        else {
            printf("Invalid input! Please enter an integer.\n");
            clearInputBuffer();  // Clear the buffer for invalid input
        }
    }

    return intArraySize;
}



// --------------------------------------------------------------------------------
// Name: clearInputBuffer
// Abstract: Prevents looping with invalid input on getArraySize
// --------------------------------------------------------------------------------
void clearInputBuffer()
{
    int intChar;
    while ((intChar = getchar()) != '\n' && intChar != EOF)
    {
        // Discard invalid input
    }
}



// --------------------------------------------------------------------------------
// Name: allocateAndInitializeArray
// Abstract: Allocates Memory, and Initializes Array to Zero
// --------------------------------------------------------------------------------
float* allocateAndInitializeArray(int intArraySize)
{
    // Allocate memory for the array
    float* paintValues = (float*)malloc(sizeof(float) * intArraySize);

    // Check if memory allocation failed
    if (paintValues == NULL) {
        printf("Memory allocation failed!\n");
        return NULL;
    }

    // Initialize array to zero
    for (int intIndex = 0; intIndex < intArraySize; intIndex++)
    {
        paintValues[intIndex] = 0.0;
    }

    return paintValues;  // Return the pointer to the allocated array
}



// --------------------------------------------------------------------------------
// Name: populateArray
// Abstract: Populates Array
// --------------------------------------------------------------------------------
void populateArray(float* paintValues, int intSize)
{
    printf("Enter %d values for the array:\n", intSize);
    int intIndex = 0;
    for (intIndex = 0; intIndex < intSize; intIndex++)
    {
        printf("Value for element %d: ", intIndex + 1);
        scanf(" %f", &paintValues[intIndex]);
    }
}



// --------------------------------------------------------------------------------
// Name: printArray
// Abstract: Prints the Array with Formatting!
// --------------------------------------------------------------------------------
void printArray(float* paintValues, int intSize)
{
    printf("\nArray Contents:\n");
    printf("------------------------------------------------------------\n");
    int intIndex = 0;
    for (intIndex = 0; intIndex < intSize; intIndex+=1)
    {
        printf("Location [ %3d ] = %8.2f\n", intIndex + 1, paintValues[intIndex]);
    }
    printf("\n");
}



// --------------------------------------------------------------------------------
// Name: findMax
// Abstract: Finds Max Number in Array
// --------------------------------------------------------------------------------
float findMax(float* paintValues, int intSize)
{
    float dblMax = paintValues[0];
    int intIndex = 0;

    for (intIndex = 1; intIndex < intSize; intIndex+=1)
    {
        if (paintValues[intIndex] > dblMax) {
            dblMax = paintValues[intIndex];
        }
    }
    return dblMax;
}



// --------------------------------------------------------------------------------
// Name: findMin
// Abstract: Finds Min Number in Array
// --------------------------------------------------------------------------------
float findMin(float* paintValues, int intSize)
{
    float dblMin = paintValues[0];
    int intIndex = 0;
    for (intIndex = 1; intIndex < intSize; intIndex+=1)
    {
        if (paintValues[intIndex] < dblMin) {
            dblMin = paintValues[intIndex];
        }
    }
    return dblMin;
}



// --------------------------------------------------------------------------------
// Name: calculateTotal
// Abstract: Adds all Numbers in Array Together
// --------------------------------------------------------------------------------
float calculateTotal(float* paintValues, int intSize)
{
    float dblTotal = 0.0;
    int intIndex = 0;

    for (intIndex = 0; intIndex < intSize; intIndex+=1)
    {
        dblTotal += paintValues[intIndex];
    }
    return dblTotal;
}



// --------------------------------------------------------------------------------
// Name: calculateAverage
// Abstract: Calculates Average
// --------------------------------------------------------------------------------
float calculateAverage(float dblTotal, int intSize)
{
    return dblTotal / intSize;
}



// --------------------------------------------------------------------------------
// Name: calculateStdDev
// Abstract: Calculates the Standard Deviation
// --------------------------------------------------------------------------------
float calculateStdDev(float* paintValues, int intSize, float dblAverage)
{
    float dblSumSqDiff = 0.0;
    int intIndex = 0;
    for (intIndex = 0; intIndex < intSize; intIndex+=1)
    {
        dblSumSqDiff += (paintValues[intIndex] - dblAverage) * (paintValues[intIndex] - dblAverage);
    }
    return sqrt(dblSumSqDiff / intSize);
}



// --------------------------------------------------------------------------------
// Name: AddValueToEnd
// Abstract: Adds a value to the end of a dynamically allocated array.
// --------------------------------------------------------------------------------
void AddValueToEnd(float** ppaintValues, int* pintArraySize, float dblValueToAdd) {
    float* adblTempArray = (float*)malloc((*pintArraySize + 1) * sizeof(float));
    int intIndex = 0;

    if (adblTempArray == NULL) {
        printf("Memory allocation failed for AddValueToEnd!\n");
        return;
    }

    for (intIndex = 0; intIndex < *pintArraySize; intIndex += 1) {
        adblTempArray[intIndex] = (*ppaintValues)[intIndex];
    }
    // I did some research on this Error C6011 - It seems that I don't need to do anything?  Or I can make sure that malloc returned something other than NULL...  Ignoring them
    // for now because it doesn't seem to be effecting the program from running.
    // Revisited this because it errors out if I don't have something to put in the array... unsure.  Check back.

    adblTempArray[*pintArraySize] = dblValueToAdd;
    free(*ppaintValues);
    *ppaintValues = adblTempArray;
    (*pintArraySize)+= 1;
}



// --------------------------------------------------------------------------------
// Name: AddValueToFront
// Abstract: Adds a value to the front of a dynamically allocated array.
// --------------------------------------------------------------------------------
void AddValueToFront(float** ppaintValues, int* pintArraySize, float valueToAdd) {
    float* adblTempArray = (float*)malloc((*pintArraySize + 1) * sizeof(float));
    int intIndex = 0;

    adblTempArray[0] = valueToAdd;

    for (int iintIndex = 0; intIndex < *pintArraySize; intIndex+= 1) {
        adblTempArray[intIndex + 1] = (*ppaintValues)[intIndex];
    }
    free(*ppaintValues);
    *ppaintValues = adblTempArray;
    (*pintArraySize)+= 1;
}



// --------------------------------------------------------------------------------
// Name: InsertValueAt
// Abstract: Inserts a value at a specified index in a dynamically allocated array.
// --------------------------------------------------------------------------------
void InsertValueAt(float** ppaintValues, int* pintArraySize, float valueToInsert, int intInsertIndex) {
    // Check for valid index
    if (intInsertIndex < 0 || intInsertIndex > *pintArraySize) {
        printf("Invalid index: %d\n", intInsertIndex);
        return;
    }

    // Allocate a new array with one extra element
    float* adblTempArray = (float*)malloc((*pintArraySize + 1) * sizeof(float));
    if (adblTempArray == NULL) {
        printf("Memory allocation failed for InsertValueAt!\n");
        return;
    }

    // Copy elements up to the insertion index
    int intIndex = 0;
    for (intIndex = 0; intIndex < intInsertIndex; intIndex += 1) {
        adblTempArray[intIndex] = (*ppaintValues)[intIndex];
    }

    // Insert the new value at the specified index
    adblTempArray[intInsertIndex] = valueToInsert;

    // Copy the remaining elements after the insertion point
    for (intIndex = intInsertIndex; intIndex < *pintArraySize; intIndex += 1) {
        adblTempArray[intIndex + 1] = (*ppaintValues)[intIndex];
    }

    // Free the old array and assign the new array back to the original pointer
    free(*ppaintValues);
    *ppaintValues = adblTempArray;
    *pintArraySize += 1;
}





// --------------------------------------------------------------------------------
// Name: RemoveAt
// Abstract: Removes a value at a specified index from a dynamically allocated array.
// --------------------------------------------------------------------------------
void RemoveAt(float** ppaintValues, int* pintArraySize, int intRemoveIndex) {
    int intIndex = 0;
    int intIndex2 = 0;

    if (intRemoveIndex < 0 || intRemoveIndex >= *pintArraySize) {
        printf("Invalid index\n");
        return;
    }
    float* adblTempArray = (float*)malloc((*pintArraySize - 1) * sizeof(float));
    for (int intIndex = 0, intIndex2 = 0; intIndex < *pintArraySize; intIndex += 1) {
        if (intIndex != intRemoveIndex) {
            adblTempArray[intIndex2++] = (*ppaintValues)[intIndex];
        }
    }
    free(*ppaintValues);
    *ppaintValues = adblTempArray;
    (*pintArraySize)-= 1;
}

