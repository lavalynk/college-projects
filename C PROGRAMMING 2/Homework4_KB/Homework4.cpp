// --------------------------------------------------------------------------------
// Name: Homework4.cpp
// Abstract: Implements polymorphism using an array of CAnimal.
// --------------------------------------------------------------------------------
// Checklist:
// Were you able to complete and run the assignment successfully ?
// Yes.
// If something isn’t working, how far did you get ?
// Everything seems fine.
// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include <iostream>
#include "CDog.h"
#include "CCow.h"
#include "CDragon.h"
using namespace std;

// --------------------------------------------------------------------------------
// Name: main
// Abstract: Iterates over a CAnimal pointer array to invoke each animal's behavior.
// --------------------------------------------------------------------------------
int main()
{
	// ----------------------------------------------------------------------------
	// Step 2 - Create Child Instances
	// Abstract: Instantiate each animal subclass with specific data
	// ----------------------------------------------------------------------------

	CDog clsBuster;
	clsBuster.SetName("Buster");
	clsBuster.SetType("Dog");
	clsBuster.SetBreed("Pug");

	CCow clsDaisy;
	clsDaisy.SetName("Daisy");
	clsDaisy.SetType("Cow");
	clsDaisy.SetColor("Black");

	CDragon clsSmaug;
	clsSmaug.SetName("Tiamat");
	clsSmaug.SetType("Dragon");
	clsSmaug.SetGold(5000);

	// ----------------------------------------------------------------------------
	// Step 3 - Populate Zoo Array
	// Abstract: Store CAnimal pointers (and nulls) in a fixed-size array
	// Question for Mr. B - Would nullptr been also acceptable here?
	// ----------------------------------------------------------------------------

	CAnimal* paclsZoo[5];

	paclsZoo[0] = (CAnimal*)&clsBuster;
	paclsZoo[1] = NULL;
	paclsZoo[2] = (CAnimal*)&clsDaisy;
	paclsZoo[3] = NULL;
	paclsZoo[4] = (CAnimal*)&clsSmaug;

	// ----------------------------------------------------------------------------
	// Step 4 - Base Class Polymorphism
	// Abstract: Call base-class methods polymorphically on each non-null entry
	// ----------------------------------------------------------------------------

	cout << "=========== Zoo Animal Sounds ===========" << endl;

	cout << endl;

	int intIndex = 0;

	for (intIndex = 0; intIndex < 5; intIndex += 1)
	{
		if (paclsZoo[intIndex] != 0)
		{

			cout << "Name: " << paclsZoo[intIndex]->GetName() << endl;
			cout << "Type: " << paclsZoo[intIndex]->GetType() << endl;
			paclsZoo[intIndex]->MakeNoise();
			cout << endl;
		}
	}

	// ----------------------------------------------------------------------------
	// Step 5 - Derived Class Actions
	// Abstract: Downcast and invoke subclass-specific methods based on type
	// ----------------------------------------------------------------------------

	cout << "=========== Unique Animal Actions ===========" << endl;

	cout << endl;

	for (int intIndex = 0; intIndex < 5; intIndex += 1)
	{
		if (paclsZoo[intIndex] != 0)
		{

			// Dog
			if (strcmp(paclsZoo[intIndex]->GetType(), "Dog") == 0)
			{
				((CDog*)paclsZoo[intIndex])->Fetch();
			}

			// Cow
			if (strcmp(paclsZoo[intIndex]->GetType(), "Cow") == 0)
			{
				((CCow*)paclsZoo[intIndex])->Graze();
			}

			// Dragon
			if (strcmp(paclsZoo[intIndex]->GetType(), "Dragon") == 0)
			{
				((CDragon*)paclsZoo[intIndex])->BreatheFire();
			}
			cout << endl;
		}
	}
	// --------------------------------------------------------------------------------
	// End of program
	// --------------------------------------------------------------------------------
	return 0;
}
