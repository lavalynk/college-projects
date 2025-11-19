// --------------------------------------------------------------------------------
// Name: Keith Brock
// Class: C Programming 2 SET-252-400
// Abstract: Homework 3
// --------------------------------------------------------------------------------

#include <iostream>
using namespace std; // Enables cleaner use of standard I/O functions

// --------------------------------------------------------------------------------
// Function Prototypes
// --------------------------------------------------------------------------------
void MakeArray(long*& palngValues, long& lngArraySize);
void PopulateArray(long* palngValues, long lngArraySize);
void PrintArray(long* palngValues, long lngArraySize);
void DeleteArray(long*& palngValues);

// --------------------------------------------------------------------------------
// Name: main
// Abstract: Coordinates function calls for creating, populating, displaying,
// and cleaning up.
// --------------------------------------------------------------------------------
int main()
{
    long* palngValues = 0; // Pointer that will reference the dynamic array
    long lngArraySize = 0; // Size of the array

    MakeArray(palngValues, lngArraySize);       // Request size, allocate memory
    PopulateArray(palngValues, lngArraySize);   // Fill array with values
    PrintArray(palngValues, lngArraySize);      // Display the array contents
    DeleteArray(palngValues);                   // Free memory and reset pointer

    // Show that the pointer has been nulled out
    cout << endl; //Just using this instead of \n.  Nothing to see here....
    cout << "Pointer is: " << palngValues << endl;

    return 0;
}

// --------------------------------------------------------------------------------
// Name: MakeArray
// Abstract: Prompts the user for a valid size and dynamically allocates memory.
// --------------------------------------------------------------------------------
void MakeArray(long*& palngValues, long& lngArraySize)
{
    do {
        cout << "Enter array size (1 to 100): ";
        cin >> lngArraySize;
    } while (lngArraySize < 1 || lngArraySize > 100);

    palngValues = new long[lngArraySize];

    // Initialize all values to 0 using pointer arithmetic
    for (long intIndex = 0; intIndex < lngArraySize; intIndex++) {
        *(palngValues + intIndex) = 0;
    }
}


// --------------------------------------------------------------------------------
// Name: PopulateArray
// Abstract: Fills each element in the array with user provided input.
// --------------------------------------------------------------------------------
void PopulateArray(long* palngValues, long lngArraySize)
{
    for (long intIndex = 0; intIndex < lngArraySize; intIndex++) {
        cout << "Enter value for index [" << intIndex << "]: ";
        cin >> *(palngValues + intIndex);
    }
}

// --------------------------------------------------------------------------------
// Name: PrintArray
// Abstract: Displays each element and its location in the array.
// --------------------------------------------------------------------------------
void PrintArray(long* palngValues, long lngArraySize)
{
    for (long intIndex = 0; intIndex < lngArraySize; intIndex++) {
        cout << "Location [ " << intIndex << " ] = " << *(palngValues + intIndex) << endl;
    }
}

// --------------------------------------------------------------------------------
// Name: DeleteArray
// Abstract: Deallocates the memory and clears the pointer.
// --------------------------------------------------------------------------------
void DeleteArray(long*& palngValues)
{
    delete[] palngValues; 
    palngValues = 0;      // Reset pointer to null to prevent dangling reference
}
