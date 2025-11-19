// --------------------------------------------------------------------------------
// Name: CDog
// Abstract: This class represents a dog with properties for name and weight, and 
// includes methods for getting/setting and Barking.
// --------------------------------------------------------------------------------


// --------------------------------------------------------------------------------
// Pre-compiler Directives
// --------------------------------------------------------------------------------
#pragma once		// Include this file only once even if referenced multiple times
#define _CRT_SECURE_NO_WARNINGS  // To address the issue with using strcpy.

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include <stdlib.h>
#include <iostream>
#include <cstring>
using namespace std;

class CDog
{
	// --------------------------------------------------------------------------------
	// Properties
	// --------------------------------------------------------------------------------
public:			// Never make public properties.  
				// Make protected or private with public get/set methods

protected:

	char m_strName[50];
	float m_sngWeight;

private:

	// --------------------------------------------------------------------------------
	// Methods
	// --------------------------------------------------------------------------------
public:

	// Name
	void SetName(const char* pstrNewName);
	char* GetName();

	// Weight
	void SetWeight(float sngNewWeight);
	float GetWeight();

	// Bark Method
	void Bark();

protected:

private:

};
