// --------------------------------------------------------------------------------
// Name: CDog
// Abstract: This class shows a dog with properties for name and weight, and 
// includes methods for getting, setting and for barking
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

	char* m_pstrName;
	int m_intAge;
	float m_sngWeight;

private:

	// --------------------------------------------------------------------------------
	// Methods
	// --------------------------------------------------------------------------------
public:

	CDog();

	CDog(const char* pstrName);
	CDog(const char* pstrName, int intAge);
	CDog(const char* pstrName, int intAge, float sngWeight);

	~CDog();

	// Name of Dog
	void SetName(const char* pstrNewName);
	const char* GetName() const;

	// Age of Dog
	void SetAge(int intNewAge);
	int GetAge() const;

	// Weight of Dog
	void SetWeight(float sngNewWeight);
	float GetWeight() const;

	// Bark Method
	void Bark() const;

	// Print Method
	void Print() const;

protected:
	void Initialize(const char* pstrName, int intAge, float sngWeight);
	void CleanUp();
	void DeleteString(char*& pstrTarget);
private:

};
