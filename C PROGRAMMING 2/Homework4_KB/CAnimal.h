// --------------------------------------------------------------------------------
// Name: CAnimal
// Abstract: This class represents a generic animal with properties for name and type.
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Pre-compiler Directives
// --------------------------------------------------------------------------------
#pragma once  // Include this file only once even if referenced multiple times

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include <iostream>
#include <cstring> 
#define _CRT_SECURE_NO_WARNINGS
using namespace std;

class CAnimal
{
	// --------------------------------------------------------------------------------
	// Properties
	// --------------------------------------------------------------------------------
public:			// Never make public properties.
				// Make protected or private with public get/set methods

protected:
	char m_strName[50];
	char m_strType[50];

private:

	// --------------------------------------------------------------------------------
	// Methods
	// --------------------------------------------------------------------------------
public:

	// Name
	void SetName(const char* pstrNewName);
	const char* GetName();

	// Type
	void SetType(const char* pstrNewType);
	const char* GetType();

	// MakeNoise 
	virtual void MakeNoise();

protected:

private:

};
