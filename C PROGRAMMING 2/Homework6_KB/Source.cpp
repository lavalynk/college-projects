// --------------------------------------------------------------------------------
// Name: Homework 6 - Overloading and Const
// Abstract: Tests the CDog and CTrainedDog classes.
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include <iostream>
#include "CDog.h"
#include "CTrainedDog.h"

using namespace std;

// --------------------------------------------------------------------------------
// Name: main
// Abstract: Entry point for testing CDog and CTrainedDog classes.
// --------------------------------------------------------------------------------
int main()
{
    // --------------------------------------------------------------------------------
    // Step 1: Test CDog
    // --------------------------------------------------------------------------------

    // Create a CDog instance
    CDog clsRegularDog("Goliath", 5, 5.0f);

    // Print details
    cout << "Printing CDog:" << endl;
    clsRegularDog.Print();
    cout << endl;

    // --------------------------------------------------------------------------------
    // Step 2: Test CTrainedDog
    // --------------------------------------------------------------------------------

    // Create a CTrainedDog instance
    CTrainedDog clsTrainedDog("Frost", 3, 35.0f, "American Pitbull Terrier");

    // Print details
    cout << "Printing CTrainedDog:" << endl;
    clsTrainedDog.Print();
    cout << endl;

    return 0;
}
