// --------------------------------------------------------------------------------
// Name: CTrainedDog
// Abstract: This class represents a trained dog, inheriting from CDog. 
// It includes additional trained behaviors such as Fetch and PlayDead.
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Pre-compiler Directives
// --------------------------------------------------------------------------------
#pragma once		// Include this file only once even if referenced multiple times

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include <stdlib.h>
#include <iostream>
#include "CDog.h"
using namespace std;

class CTrainedDog : public CDog
{
	// --------------------------------------------------------------------------------
	// Properties
	// --------------------------------------------------------------------------------
public:			// Never make public properties.  
				// Make protected or private with public get/set methods

protected:

private:

	// --------------------------------------------------------------------------------
	// Methods
	// --------------------------------------------------------------------------------
public:

	// Fetch
	void Fetch();

	// PlayDead
	void PlayDead();

protected:

private:

};
