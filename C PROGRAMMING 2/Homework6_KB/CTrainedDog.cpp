// --------------------------------------------------------------------------------
// Name: CTrainedDog
// Abstract: Class method definitions for the CTrainedDog class.
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#define _CRT_SECURE_NO_WARNINGS

#include "CTrainedDog.h"
#include <iostream>
#include <cstring>

using namespace std;


// --------------------------------------------------------------------------------
// Name: Constructor
// Abstract: Default constructor
// --------------------------------------------------------------------------------
CTrainedDog::CTrainedDog() : CDog()
{
    Initialize("", 0, 0.0f, "");
}


// --------------------------------------------------------------------------------
// Name: Constructor
// Abstract: Parameterized constructor (Name only)
// --------------------------------------------------------------------------------
CTrainedDog::CTrainedDog(const char* pstrName) : CDog(pstrName)
{
    Initialize(pstrName, 0, 0.0f, "");
}


// --------------------------------------------------------------------------------
// Name: Constructor
// Abstract: Parameterized constructor (Name & Age)
// --------------------------------------------------------------------------------
CTrainedDog::CTrainedDog(const char* pstrName, int intAge) : CDog(pstrName, intAge)
{
    Initialize(pstrName, intAge, 0.0f, "");
}


// --------------------------------------------------------------------------------
// Name: Constructor
// Abstract: Parameterized constructor (Name, Age, Weight)
// --------------------------------------------------------------------------------
CTrainedDog::CTrainedDog(const char* pstrName, int intAge, float sngWeight) : CDog(pstrName, intAge, sngWeight)
{
    Initialize(pstrName, intAge, sngWeight, "");
}


// --------------------------------------------------------------------------------
// Name: Constructor
// Abstract: Parameterized constructor (Name, Age, Weight, Breed)
// --------------------------------------------------------------------------------
CTrainedDog::CTrainedDog(const char* pstrName, int intAge, float sngWeight, const char* pstrBreed) : CDog(pstrName, intAge, sngWeight)
{
    Initialize(pstrName, intAge, sngWeight, pstrBreed);
}


// --------------------------------------------------------------------------------
// Name: Initialize
// Abstract: Sets all properties with proper boundary checks.
// --------------------------------------------------------------------------------
void CTrainedDog::Initialize(const char* pstrName, int intAge, float sngWeight, const char* pstrBreed)
{
    m_pstrBreed = 0;  

    SetBreed(pstrBreed);
}


// --------------------------------------------------------------------------------
// Name: Destructor
// Abstract: Calls CleanUp to free memory
// --------------------------------------------------------------------------------
CTrainedDog::~CTrainedDog()
{
    CleanUp();
}


// --------------------------------------------------------------------------------
// Name: CleanUp
// Abstract: Calls DeleteString for breed only (not name, which is handled by CDog).
// --------------------------------------------------------------------------------
void CTrainedDog::CleanUp()
{
    DeleteString(m_pstrBreed);
}


// --------------------------------------------------------------------------------
// Name: SetBreed
// Abstract: Sets the breed, allocates memory and caps to 49 characters.
// --------------------------------------------------------------------------------
void CTrainedDog::SetBreed(const char* pstrNewBreed)
{
    if (pstrNewBreed == nullptr)
    {
        return;
    }

    int intLength = strlen(pstrNewBreed);
    if (intLength > 49)
    {
        intLength = 49; 
    }

    DeleteString(m_pstrBreed);

    m_pstrBreed = new char[intLength + 1];

    strncpy(m_pstrBreed, pstrNewBreed, intLength);
    m_pstrBreed[intLength] = '\0'; 
}


// --------------------------------------------------------------------------------
// Name: GetBreed
// Abstract: Returns the breed (constant method).
// --------------------------------------------------------------------------------
const char* CTrainedDog::GetBreed() const
{
    return m_pstrBreed;
}


// --------------------------------------------------------------------------------
// Name: Fetch
// Abstract: Makes the trained dog fetching an object.
// --------------------------------------------------------------------------------
void CTrainedDog::Fetch() const
{
    cout << "Fetching the tasty stick!" << endl;
}


// --------------------------------------------------------------------------------
// Name: PlayDead
// Abstract: Makes the trained dog playing dead.
// --------------------------------------------------------------------------------
void CTrainedDog::PlayDead() const
{
    cout << "*BANG* You got me!  I'm dead!  *wink*" << endl;
}


// --------------------------------------------------------------------------------
// Name: Print
// Abstract: Prints all properties and calls all methods.
// --------------------------------------------------------------------------------
void CTrainedDog::Print() const
{
    CDog::Print();

    cout << "Breed: " << GetBreed() << endl;

    Fetch();
    PlayDead();
}
