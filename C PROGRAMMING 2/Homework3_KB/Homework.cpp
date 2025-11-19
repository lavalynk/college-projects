// --------------------------------------------------------------------------------  
// Name: Homework#3 - Encapsulation and Inheritance  
// Abstract: Tests the CDog and CTrainedDog classes.  
// --------------------------------------------------------------------------------  
// Checklist:
// Were you able to complete and run the assignment successfully ?
// Yes.
// If something isn’t working, how far did you get ?
// Everything seems to be working okay.
// --------------------------------------------------------------------------------  
// Includes  
// --------------------------------------------------------------------------------  
#include <iostream>  
#include "CDog.h"  
#include "CTrainedDog.h"  
using namespace std;

// --------------------------------------------------------------------------------  
// Name: main  
// Abstract: Entry point for testing encapsulation and inheritance.  
// --------------------------------------------------------------------------------  
int main()
{
    // --------------------------------------------------------------------------------  
    // Step 1: Test CDog  
    // --------------------------------------------------------------------------------  

    // Test 1: Small weight.  
    CDog clsZephyr;
    clsZephyr.SetName("Zephyr");
    clsZephyr.SetWeight(10.5f);
    cout << "Name : " << clsZephyr.GetName() << endl;
    cout << "Weight : " << clsZephyr.GetWeight() << endl << endl;

    // Test 2: Large weight.  
    CDog clsNimbus;
    clsNimbus.SetName("Nimbus");
    clsNimbus.SetWeight(20.0f);
    cout << "Name : " << clsNimbus.GetName() << endl;
    cout << "Weight : " << clsNimbus.GetWeight() << endl << endl;

    // Test 3: Invalid name (too long) and invalid weight.  
    CDog clsCerberus;
    clsCerberus.SetName("CerberusTheThreeHeadedGuardianOfTheUnderworldAlpha");
    clsCerberus.SetWeight(-130.0f);
    cout << "Name : " << clsCerberus.GetName() << endl;
    cout << "Weight : " << clsCerberus.GetWeight() << endl << endl;

    // --------------------------------------------------------------------------------  
    // Step 2: Test CTrainedDog (Inheritance)  
    // --------------------------------------------------------------------------------  

    // Test 1: Trained Dog with small weight.  
    CTrainedDog clsTrainedEcho;
    clsTrainedEcho.SetName("Echo");
    clsTrainedEcho.SetWeight(8.0f);
    cout << "Name : " << clsTrainedEcho.GetName() << endl;
    cout << "Weight : " << clsTrainedEcho.GetWeight() << endl;
    clsTrainedEcho.Bark();
    clsTrainedEcho.Fetch();
    clsTrainedEcho.PlayDead();
    cout << endl;

    // Test 2: Trained Dog with large weight.  
    CTrainedDog clsTrainedAtlas;
    clsTrainedAtlas.SetName("Atlas");
    clsTrainedAtlas.SetWeight(25.0f);
    cout << "Name : " << clsTrainedAtlas.GetName() << endl;
    cout << "Weight : " << clsTrainedAtlas.GetWeight() << endl;
    clsTrainedAtlas.Bark();
    clsTrainedAtlas.Fetch();
    clsTrainedAtlas.PlayDead();
    cout << endl;

    // Test 3: Trained Dog with invalid weight.  
    CTrainedDog clsTrainedPhantom;
    clsTrainedPhantom.SetName("Phantom");
    clsTrainedPhantom.SetWeight(-5.0f);
    cout << "Name : " << clsTrainedPhantom.GetName() << endl;
    cout << "Weight : " << clsTrainedPhantom.GetWeight() << endl;
    clsTrainedPhantom.Bark();
    clsTrainedPhantom.Fetch();
    clsTrainedPhantom.PlayDead();
    cout << endl;

    // --------------------------------------------------------------------------------  
    // End of program  
    // --------------------------------------------------------------------------------  
    return 0;
}
