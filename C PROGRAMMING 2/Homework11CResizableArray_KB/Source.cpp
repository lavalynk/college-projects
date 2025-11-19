// ====================================================================================
// Author: Keith Brock
// Class: C Programming 2
// File: Source.cpp
// Purpose: Exercises the CResizableArray class using long, double, and char data types.
//          Includes full test coverage for core functionality and deep copy validation.
// ====================================================================================

#include <iostream>
#include "CResizableArray.h"
using namespace std;

// ====================================================================================
// Function: main
// Summary : Entry point for validating CResizableArray functionality with multiple types.
// ====================================================================================
int main()
{
    // ====================================================================================
    // Section: long
    // ====================================================================================
    cout << "--------------------------------------------------------------------------------------------" << endl;
    cout << "Testing CResizableArray with long" << endl;
    cout << "--------------------------------------------------------------------------------------------" << endl;

    CResizableArray<long> longArray(5, 100);
    cout << "\nInitialized with 5 elements set to 100:\n";
    longArray.Print("Long Array - Initial Values");

    cout << "\nAppending 200 to the end:\n";
    longArray.AddValueToEnd(200);
    longArray.Print("After Adding 200 to End");

    cout << "\nPrepending 50 to the front:\n";
    longArray.AddValueToFront(50);
    longArray.Print("After Adding 50 to Front");

    cout << "\nInserting 75 at index 3:\n";
    longArray.InsertValueAt(75, 3);
    longArray.Print("After Inserting 75 at Index 3");

    cout << "\nRemoving value at index 2:\n";
    longArray.RemoveAt(2);
    longArray.Print("After Removing Index 2");

    cout << "\nIncreasing array size to 7:\n";
    longArray.SetSize(7);
    longArray.Print("After Expanding to Size 7");

    cout << "\nTesting boundary handling with invalid index [-1]:\n";
    longArray[-1] = 999;
    longArray.Print("After Setting 999 at Index -1");

    cout << "Testing boundary handling with oversized index [9999]:\n";
    longArray[9999] = 777;
    longArray.Print("After Setting 777 at Index 9999");

    cout << "Getting value at clipped index [-1]: " << longArray[-1] << endl;
    cout << "Getting value at clipped index [9999]: " << longArray[9999] << endl;

    cout << "\nConcatenation Test with += operator (long):\n";
    CResizableArray<long> longRA1(3, 5);
    CResizableArray<long> longRA2(2, 9);
    longRA1.Print("longRA1 (Before +=)");
    longRA2.Print("longRA2 (Before +=)");
    longRA1 += longRA2;
    longRA1.Print("After += Operation");

    cout << "\nConcatenation Test with + operator (long):\n";
    CResizableArray<long> longCombined = longRA1 + longRA2;
    longCombined.Print("After + Operation");

    // ====================================================================================
    // Section: double
    // ====================================================================================
    cout << "\n--------------------------------------------------------------------------------------------\n";
    cout << "Testing CResizableArray with double\n";
    cout << "--------------------------------------------------------------------------------------------\n";

    CResizableArray<double> doubleArray(5, 10.5);
    cout << "\nInitialized with 5 elements set to 10.5:\n";
    doubleArray.Print("Double Array - Initial Values");

    cout << "\nAppending 20.75:\n";
    doubleArray.AddValueToEnd(20.75);
    doubleArray.Print("After Adding 20.75 to End");

    cout << "\nPrepending 5.25:\n";
    doubleArray.AddValueToFront(5.25);
    doubleArray.Print("After Adding 5.25 to Front");

    cout << "\nInserting 15.5 at index 2:\n";
    doubleArray.InsertValueAt(15.5, 2);
    doubleArray.Print("After Inserting 15.5 at Index 2");

    cout << "\nRemoving value at index 3:\n";
    doubleArray.RemoveAt(3);
    doubleArray.Print("After Removing Index 3");

    cout << "\nIncreasing size to 7:\n";
    doubleArray.SetSize(7);
    doubleArray.Print("After Expanding to Size 7");

    cout << "\nClipping test: Setting value at index -1:\n";
    doubleArray[-1] = 99.99;
    doubleArray.Print("After Setting 99.99 at Index -1");

    cout << "Clipping test: Setting value at index 9999:\n";
    doubleArray[9999] = 77.77;
    doubleArray.Print("After Setting 77.77 at Index 9999");

    cout << "Get clipped value [-1]: " << doubleArray[-1] << endl;
    cout << "Get clipped value [9999]: " << doubleArray[9999] << endl;

    cout << "\nConcatenation Test with += operator (double):\n";
    CResizableArray<double> doubleRA1(3, 3.14);
    CResizableArray<double> doubleRA2(2, 2.71);
    doubleRA1.Print("doubleRA1 (Before +=)");
    doubleRA2.Print("doubleRA2 (Before +=)");
    doubleRA1 += doubleRA2;
    doubleRA1.Print("After += Operation");

    cout << "\nConcatenation Test with + operator (double):\n";
    CResizableArray<double> doubleCombined = doubleRA1 + doubleRA2;
    doubleCombined.Print("After + Operation");

    // ====================================================================================
    // Section: char
    // ====================================================================================
    cout << "\n--------------------------------------------------------------------------------------------\n";
    cout << "Testing CResizableArray with char\n";
    cout << "--------------------------------------------------------------------------------------------\n";

    CResizableArray<char> charArray(5, '*');
    cout << "\nInitialized with 5 asterisks:\n";
    charArray.Print("Char Array - Initial Values");

    cout << "\nAppending 'A' to end:\n";
    charArray.AddValueToEnd('A');
    charArray.Print("After Adding 'A' to End");

    cout << "\nPrepending 'B' to front:\n";
    charArray.AddValueToFront('B');
    charArray.Print("After Adding 'B' to Front");

    cout << "\nInserting 'C' at index 2:\n";
    charArray.InsertValueAt('C', 2);
    charArray.Print("After Inserting 'C' at Index 2");

    cout << "\nRemoving element at index 3:\n";
    charArray.RemoveAt(3);
    charArray.Print("After Removing Index 3");

    cout << "\nIncreasing array size to 7:\n";
    charArray.SetSize(7);
    charArray.Print("After Expanding to Size 7");

    cout << "\nBoundary overwrite at index -1:\n";
    charArray[-1] = 'Z';
    charArray.Print("After Setting 'Z' at Index -1");

    cout << "Boundary overwrite at index 9999:\n";
    charArray[9999] = 'Y';
    charArray.Print("After Setting 'Y' at Index 9999");

    cout << "Read clipped value [-1]: " << charArray[-1] << endl;
    cout << "Read clipped value [9999]: " << charArray[9999] << endl;

    cout << "\nConcatenation Test with += operator (char):\n";
    CResizableArray<char> charRA1(3, 'X');
    CResizableArray<char> charRA2(2, 'Y');
    charRA1.Print("charRA1 (Before +=)");
    charRA2.Print("charRA2 (Before +=)");
    charRA1 += charRA2;
    charRA1.Print("After += Operation");

    cout << "\nConcatenation Test with + operator (char):\n";
    CResizableArray<char> charCombined = charRA1 + charRA2;
    charCombined.Print("After + Operation");

    // ====================================================================================
    // Section: Deep Copy Tests
    // ====================================================================================
    cout << "\n--------------------------------------------------------------------------------------------\n";
    cout << "Testing DeepCopy Method\n";
    cout << "--------------------------------------------------------------------------------------------\n";

    // --- Long ---
    CResizableArray<long> longCopyArray = longArray;
    cout << "\n[Long] Before modifying original:\n";
    longArray.Print("Original Long Array");
    longCopyArray.Print("Copied Long Array");

    cout << "\nModifying original (index 0 -> 500)...\n";
    longArray[0] = 500;
    longArray.Print("Modified Original");
    longCopyArray.Print("Copied (Should Remain Unchanged)");

    // --- Double ---
    CResizableArray<double> doubleCopyArray = doubleArray;
    cout << "\n[Double] Before modifying original:\n";
    doubleArray.Print("Original Double Array");
    doubleCopyArray.Print("Copied Double Array");

    cout << "\nModifying original (index 0 -> 123.12)...\n";
    doubleArray[0] = 123.12;
    doubleArray.Print("Modified Original");
    doubleCopyArray.Print("Copied (Should Remain Unchanged)");

    // --- Char ---
    CResizableArray<char> charCopyArray = charArray;
    cout << "\n[Char] Before modifying original:\n";
    charArray.Print("Original Char Array");
    charCopyArray.Print("Copied Char Array");

    cout << "\nModifying original (index 0 -> 'L')...\n";
    charArray[0] = 'L';
    charArray.Print("Modified Original");
    charCopyArray.Print("Copied (Should Remain Unchanged)");

    return 0;
}
