// --------------------------------------------------------------------------------
// Name: CDog
// Abstract: Class method definitions for CDog.h
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include "CDog.h"


// --------------------------------------------------------------------------------
// Name: Constructor
// Abstract: Default constructor
// --------------------------------------------------------------------------------
CDog::CDog()
{
	Initialize("", 0, 0);
}


// --------------------------------------------------------------------------------
// Name: Constructor
// Abstract: Name only
// --------------------------------------------------------------------------------
CDog::CDog(const char* pstrName)
{
	Initialize(pstrName, 0, 0.0f);
}


// --------------------------------------------------------------------------------
// Name: Constructor
// Abstract: Name & Age
// --------------------------------------------------------------------------------
CDog::CDog(const char* pstrName, int intAge)
{
	Initialize(pstrName, intAge, 0.0f);
}


// --------------------------------------------------------------------------------
// Name: Constructor
// Abstract: Name, Age, Weight
// --------------------------------------------------------------------------------
CDog::CDog(const char* pstrName, int intAge, float sngWeight)
{
	Initialize(pstrName, intAge, sngWeight);
}


// --------------------------------------------------------------------------------
// Name: Initialize
// Abstract: Sets all properties.
// --------------------------------------------------------------------------------
void CDog::Initialize(const char* pstrName, int intAge, float sngWeight)
{
	m_pstrName = 0; 

	SetName(pstrName);
	SetAge(intAge);
	SetWeight(sngWeight);
}


// --------------------------------------------------------------------------------
// Name: Destructor
// Abstract: CleanUp to free memory.
// --------------------------------------------------------------------------------
CDog::~CDog()
{
	CleanUp();
}


// --------------------------------------------------------------------------------
// Name: CleanUp
// Abstract: Calls DeleteString for all char* properties.
// --------------------------------------------------------------------------------
void CDog::CleanUp()
{
	DeleteString(m_pstrName);
}


// --------------------------------------------------------------------------------
// Name: DeleteString
// Abstract: Deletes a dynamically allocated string and sets pointer to null.
// --------------------------------------------------------------------------------
void CDog::DeleteString(char*& pstrTarget)
{
	if (pstrTarget != nullptr)
	{
		delete[] pstrTarget;
		pstrTarget = nullptr; 
	}
}


// --------------------------------------------------------------------------------
// Name: SetName
// Abstract: Sets the name, allocates memory and caps to 49 characters.
// --------------------------------------------------------------------------------
void CDog::SetName(const char* pstrNewName)
{
	if (pstrNewName == nullptr)
	{
		return;
	}

	int intLength = strlen(pstrNewName);

	if (intLength > 49)
	{
		intLength = 49; 
	}

	DeleteString(m_pstrName);

	m_pstrName = new char[intLength + 1];

	strncpy(m_pstrName, pstrNewName, intLength);
	m_pstrName[intLength] = '\0';
}



// --------------------------------------------------------------------------------
// Name: GetName
// Abstract: Returns the name (constant method).
// --------------------------------------------------------------------------------
const char* CDog::GetName() const
{
	return m_pstrName;
}


// --------------------------------------------------------------------------------
// Name: SetAge
// Abstract: Sets the age, clipping negative values to 0.
// --------------------------------------------------------------------------------
void CDog::SetAge(int intNewAge)
{
	if (intNewAge < 0)
	{
		m_intAge = 0;
	}
	else
	{
		m_intAge = intNewAge;
	}
}


// --------------------------------------------------------------------------------
// Name: GetAge
// Abstract: Returns the age (constant method).
// --------------------------------------------------------------------------------
int CDog::GetAge() const
{
	return m_intAge;
}


// --------------------------------------------------------------------------------
// Name: SetWeight
// Abstract: Set the weight, ensuring valid input.
// --------------------------------------------------------------------------------
void CDog::SetWeight(float sngNewWeight)
{
	if (sngNewWeight < 0.0f)
	{
		m_sngWeight = 0.0f;
	}
	else
	{
		m_sngWeight = sngNewWeight;
	}
}


// --------------------------------------------------------------------------------
// Name: GetWeight
// Abstract: Returns the weight (constant method).
// --------------------------------------------------------------------------------
float CDog::GetWeight() const
{
	return m_sngWeight;
}


/// --------------------------------------------------------------------------------
// Name: Bark
// Abstract: Makes the dog bark based on weight (constant method).
// --------------------------------------------------------------------------------
void CDog::Bark() const
{
	if (m_sngWeight < 15.0f)
	{
		cout << "Yip! yip! yip!" << endl;
	}
	else
	{
		cout << "Woof! Woof!" << endl;
	}
}


// --------------------------------------------------------------------------------
// Name: Print
// Abstract: Prints all properties and calls all methods (constant).
// --------------------------------------------------------------------------------
void CDog::Print() const
{
	cout << "Name: " << GetName() << endl;
	cout << "Age: " << GetAge() << endl;
	cout << "Weight: " << GetWeight() << endl;
	Bark();
}