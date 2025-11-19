// --------------------------------------------------------------------------------
// Name: CDog
// Abstract: Class method definitions for the CDog class.
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include "CDog.h"
#include <iostream>
#include <cstring>
#define _CRT_SECURE_NO_WARNINGS

// --------------------------------------------------------------------------------
// Name: SetBreed
// Abstract: Set the dog's breed.
// --------------------------------------------------------------------------------
void CDog::SetBreed(const char* pstrNewBreed)
{
	if (pstrNewBreed != nullptr && strlen(pstrNewBreed) < 50)
	{
		strcpy_s(m_strBreed, pstrNewBreed);
	}
	else
	{
		m_strBreed[0] = '\0';
	}
}


// --------------------------------------------------------------------------------
// Name: GetBreed
// Abstract: Get the dog's breed.
// --------------------------------------------------------------------------------
const char* CDog::GetBreed()
{
	return m_strBreed;
}


// --------------------------------------------------------------------------------
// Name: MakeNoise
// Abstract: Override MakeNoise to represent barking.
// --------------------------------------------------------------------------------
void CDog::MakeNoise()
{
	cout << "RUFF! RUFF!" << endl;
}


// --------------------------------------------------------------------------------
// Name: Fetch
// Abstract: Dog fetches an object.
// --------------------------------------------------------------------------------
void CDog::Fetch()
{
	cout << GetName() << " is fetching the baseball!" << endl;
}
