// --------------------------------------------------------------------------------
// Name: Homework5.cpp
// Abstract: Verifies CResizableArray operations by performing various insertions,
//           deletions, size adjustments, and index boundary tests.
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include <iostream>
#include "CResizableArray.h"
using namespace std;

// --------------------------------------------------------------------------------
// Name: main
// Abstract: Executes tests on the CResizableArray class.
// --------------------------------------------------------------------------------
int main()
{
    // --------------------------------------------------------------------------------
    // Step 1: Create array and assign starting values
    // --------------------------------------------------------------------------------
    CResizableArray clsArray;
    clsArray.Initialize();

    clsArray.SetSize(4);
    clsArray.SetValueAt(2, 0);
    clsArray.SetValueAt(4, 1);
    clsArray.SetValueAt(6, 2);
    clsArray.SetValueAt(8, 3);

    cout << "Begin values" << endl;
    clsArray.Print();

    // --------------------------------------------------------------------------------
    // Step 2: Increase size to accommodate more elements
    // --------------------------------------------------------------------------------
    clsArray.SetSize(5);
    cout << "After set size to 5" << endl;
    clsArray.Print();

    // --------------------------------------------------------------------------------
    // Step 3: Insert element at the front of the array
    // --------------------------------------------------------------------------------
    clsArray.AddValueToFront(10);
    cout << "After add 10 to the front" << endl;
    clsArray.Print();

    // --------------------------------------------------------------------------------
    // Step 4: Place a value in the middle of the array
    // --------------------------------------------------------------------------------
    clsArray.InsertValueAt(5, 2);
    cout << "After insert 5 at index 2" << endl;
    clsArray.Print();

    // --------------------------------------------------------------------------------
    // Step 5: Delete an element from a specific index
    // --------------------------------------------------------------------------------
    clsArray.RemoveAt(2);
    cout << "After remove value at index 2" << endl;
    clsArray.Print();

    // --------------------------------------------------------------------------------
    // Step 6: Append a new value at the end of the array
    // --------------------------------------------------------------------------------
    clsArray.AddValueToEnd(20);
    cout << "After add 20 to the end" << endl;
    clsArray.Print();

    // --------------------------------------------------------------------------------
    // Step 7: Test index boundaries for setting and getting values
    // --------------------------------------------------------------------------------
    cout << "Setting value at index -1. Should adjust to 0" << endl;
    clsArray.SetValueAt(159, -1); // Clip to index 0
    clsArray.Print();

    cout << "Setting value at index 9999.  Should adjust to last index" << endl;
    clsArray.SetValueAt(42, 9999); // Clip to last index
    clsArray.Print();

    cout << "Getting value at index -1.  Should return index 0" << endl;
    cout << "Returned Value: " << clsArray.GetValueAt(-1) << endl;

    cout << "Getting value at index 9999.  Should return last index" << endl;
    cout << "Returned Value: " << clsArray.GetValueAt(9999) << endl;

    // --------------------------------------------------------------------------------
    // End of program
    // --------------------------------------------------------------------------------
    return 0;
}
