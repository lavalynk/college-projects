// --------------------------------------------------------------------------------
// Name: CAnimal
// Abstract: Class method definitions for the CAnimal class.
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include "CAnimal.h"
#define _CRT_SECURE_NO_WARNINGS
#include <cstring>

// --------------------------------------------------------------------------------
// Name: SetName
// Abstract: Set the animal's name.
// --------------------------------------------------------------------------------
void CAnimal::SetName(const char* pstrNewName)
{
	if (pstrNewName != nullptr && strlen(pstrNewName) < 50)
	{
		strcpy_s(m_strName, pstrNewName);
	}
	else
	{
		m_strName[0] = '\0';
	}
}


// --------------------------------------------------------------------------------
// Name: GetName
// Abstract: Get the animal's name.
// --------------------------------------------------------------------------------
const char* CAnimal::GetName()
{
	return m_strName;
}


// --------------------------------------------------------------------------------
// Name: SetType
// Abstract: Set the animal's type.
// --------------------------------------------------------------------------------
void CAnimal::SetType(const char* pstrNewType)
{
	if (pstrNewType != nullptr && strlen(pstrNewType) < 50)
	{
		strcpy_s(m_strType, pstrNewType);
	}
	else
	{
		m_strType[0] = '\0';
	}
}


// --------------------------------------------------------------------------------
// Name: GetType
// Abstract: Get the animal's type.
// --------------------------------------------------------------------------------
const char* CAnimal::GetType()
{
	return m_strType;
}


// --------------------------------------------------------------------------------
// Name: MakeNoise
// Abstract: Base class placeholder for making noise.
// --------------------------------------------------------------------------------
void CAnimal::MakeNoise()
{
	cout << "(Generic animal noise)" << endl;
}
