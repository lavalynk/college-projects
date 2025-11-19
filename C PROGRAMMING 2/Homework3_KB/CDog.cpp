// --------------------------------------------------------------------------------
// Name: CDog
// Abstract: Class method definitions for CDog.h
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include "CDog.h"


// --------------------------------------------------------------------------------
// Name: SetName
// Abstract: Set the name.
// --------------------------------------------------------------------------------
void CDog::SetName(const char* pstrNewName)
{
	// Ensure the name is not null and within bounds
	if (pstrNewName != nullptr && strlen(pstrNewName) < 50)
	{
		strcpy(m_strName, pstrNewName);
	}
	else
	{
		m_strName[0] = '\0';
	}
}


// --------------------------------------------------------------------------------
// Name: GetName
// Abstract: Get the name.
// --------------------------------------------------------------------------------
char* CDog::GetName() 
{
	return m_strName;
}


// --------------------------------------------------------------------------------
// Name: SetWeight
// Abstract: Set the weight, ensuring valid input.
// --------------------------------------------------------------------------------
void CDog::SetWeight(float sngNewWeight)
{
	// If the weight is negative, change it to 0.0
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
// Abstract: Get the weight.
// --------------------------------------------------------------------------------
float CDog::GetWeight()
{
	return m_sngWeight;
}


// --------------------------------------------------------------------------------
// Name: Bark
// Abstract: Make the dog bark based on weight.
// --------------------------------------------------------------------------------
void CDog::Bark()
{
	if (m_sngWeight < 15.0f)
	{
		cout << "Yip, Yip, Yip" << endl;
	}
	else 
	{
		cout << "Woof, Woof" << endl;
	}
}