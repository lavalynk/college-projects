// --------------------------------------------------------------------------------
// Name: CTrainedDog
// Abstract: This class represents a trained dog which is inheriting from CDog. 
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Pre-compiler Directives
// --------------------------------------------------------------------------------
#pragma once		// Include this file only once even if referenced multiple times

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
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
	char* m_pstrBreed;

private:

	// --------------------------------------------------------------------------------
	// Methods
	// --------------------------------------------------------------------------------
public:
	CTrainedDog();

	CTrainedDog(const char* pstrName);
	CTrainedDog(const char* pstrName, int intAge);
	CTrainedDog(const char* pstrName, int intAge, float sngWeight);
	CTrainedDog(const char* pstrName, int intAge, float sngWeight, const char* pstrBreed);

	// Destructor
	~CTrainedDog();

	// Breed of Dog
	void SetBreed(const char* pstrNewBreed);
	const char* GetBreed() const;

	// Fetch
	void Fetch() const;

	// PlayDead
	void PlayDead() const;

	// Print
	void Print() const;

protected:
	void Initialize(const char* pstrName, int intAge, float sngWeight, const char* pstrBreed);
	void CleanUp();

private:

};
