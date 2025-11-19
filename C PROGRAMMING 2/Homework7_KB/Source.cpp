// --------------------------------------------------------------------------------
// Name: Homework8 (disguised as Homework7)
// Abstract: Tests the CResizableArray class functionality by performing insertions,
// removals, boundary tests, invalid index handling, and memory management.
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include <iostream>
#include "CResizableArray.h"
using namespace std;

// --------------------------------------------------------------------------------
// Name: main
// Abstract: Entry point for testing CResizableArray.
// --------------------------------------------------------------------------------
int main()
{
    // --------------------------------------------------------------------------------
    // Step 1: Constructor Tests
    // --------------------------------------------------------------------------------
    cout << "Testing Default Constructor (Size 0)\n";
    CResizableArray clsArrayDefault;
    clsArrayDefault.Print("Default Constructor Output");

    cout << "Testing Constructor with Initial Size 5 (Default Values 0)\n";
    CResizableArray clsArraySizeOnly(5);
    clsArraySizeOnly.Print("Constructor with Size 5");

    cout << "Testing Constructor with Initial Size 5 and Default Value 9\n";
    CResizableArray clsArraySizeAndValue(5, 9);
    clsArraySizeAndValue.Print("Constructor with Size 5 and Default Value 9");

    // --------------------------------------------------------------------------------
    // Step 2: Insert & Remove Tests
    // --------------------------------------------------------------------------------
    cout << "Testing Insert into Empty Array\n";
    CResizableArray clsEmpty;
    clsEmpty.AddValueToEnd(100);
    clsEmpty.Print("After inserting 100 into an empty array");

    cout << "Testing Remove from Empty Array\n";
    clsEmpty.RemoveAt(0);
    clsEmpty.Print("After trying to remove from an empty array");

    // --------------------------------------------------------------------------------
    // Step 3: Destructor & Memory Cleanup
    // --------------------------------------------------------------------------------
    cout << "Testing Destructor and Memory Cleanup\n";
    {
        CResizableArray clsTemp(3, 7);
        clsTemp.Print("Temp Array (3 elements, all set to 7)");
    }
    cout << "Temp Array should be destroyed, and memory freed.\n";

    // --------------------------------------------------------------------------------
    // Step 4: Operator[] Tests - Setting and Getting Values
    // --------------------------------------------------------------------------------
    CResizableArray clsArray(4);
    clsArray[0] = 2;
    clsArray[1] = 4;
    clsArray[2] = 6;
    clsArray[3] = 8;
    clsArray.Print("Initial Values");

    // --------------------------------------------------------------------------------
    // Step 5: Resizing Tests
    // --------------------------------------------------------------------------------
    clsArray.SetSize(5);
    clsArray.Print("After Expanding to Size 5");

    clsArray.AddValueToFront(10);
    clsArray.Print("After Adding 10 to Front");

    clsArray.InsertValueAt(5, 2);
    clsArray.Print("After Inserting 5 at Index 2");

    clsArray.RemoveAt(2);
    clsArray.Print("After Removing Index 2");

    clsArray.AddValueToEnd(20);
    clsArray.Print("After Adding 20 to End");

    // --------------------------------------------------------------------------------
    // Step 6: Boundary Checks using `operator[]`
    // --------------------------------------------------------------------------------
    cout << "Boundary Check: Setting value at index -1 (Should clip to 0)\n";
    clsArray[-1] = 99;
    clsArray.Print("After Setting 99 at Index -1");

    cout << "Boundary Check: Setting value at index 9999 (Should clip to last index)\n";
    clsArray[9999] = 77;
    clsArray.Print("After Setting 77 at Index 9999");

    cout << "Boundary Check: Getting value at index -1 (Should return index 0)\n";
    cout << "Returned Value: " << clsArray[-1] << endl;

    cout << "Boundary Check: Getting value at index 9999 (Should return last index)\n";
    cout << "Returned Value: " << clsArray[9999] << endl;

    // --------------------------------------------------------------------------------
    // Step 7: DeepCopy Test
    // --------------------------------------------------------------------------------
    cout << "\nTesting DeepCopy Method\n";
    CResizableArray clsCopyArray = clsArray;
    clsCopyArray.Print("Copied Array after DeepCopy");

    clsArray[0] = 123;  // Modify original
    cout << "After modifying original array, copied array should remain unchanged:\n";
    clsCopyArray.Print("Copied Array after original modification");

    // --------------------------------------------------------------------------------
    // Step 8: Operator += Test (Appending Arrays)
    // --------------------------------------------------------------------------------
    cout << "Testing += Operator (Appending Arrays)\n";
    CResizableArray clsRA1(3, 5);  // [5, 5, 5]
    CResizableArray clsRA2(2, 9);  // [9, 9]

    cout << "Before += Operation:\n";
    clsRA1.Print("clsRA1 (Before)");
    clsRA2.Print("clsRA2 (Before)");

    clsRA1 += clsRA2;  // Append clsRA2 to clsRA1
    clsRA1.Print("After += Operation (clsRA1 += clsRA2)");

    // --------------------------------------------------------------------------------
    // Step 9: Operator + Test (Combining Arrays)
    // --------------------------------------------------------------------------------
    cout << "Testing + Operator (Combining Arrays)\n";
    CResizableArray clsRA_First(3, 1);  // [1, 1, 1]
    CResizableArray clsRA_Second(4, 2); // [2, 2, 2, 2]

    cout << "Before + Operation:\n";
    clsRA_First.Print("clsRA_First (Before)");
    clsRA_Second.Print("clsRA_Second (Before)");

    CResizableArray clsRA_Combined = clsRA_First + clsRA_Second;
    clsRA_Combined.Print("After + Operation (clsRA_Combined = clsRA_First + clsRA_Second)");

    cout << "Ensuring original arrays remain unchanged:\n";
    clsRA_First.Print("clsRA_First (After)");
    clsRA_Second.Print("clsRA_Second (After)");

    return 0;
}
