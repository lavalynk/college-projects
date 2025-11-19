// --------------------------------------------------------------------------------
// Name: CDragon
// Abstract: Class method definitions for the CDragon class.
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include "CDragon.h"
#include <iostream>
#define _CRT_SECURE_NO_WARNINGS

// --------------------------------------------------------------------------------
// Name: SetGold
// Abstract: Set the dragon's gold amount.
// --------------------------------------------------------------------------------
void CDragon::SetGold(int intNewGold)
{
	if (intNewGold >= 0)
	{
		m_intAmountOfGold = intNewGold;
	}
	else
	{
		m_intAmountOfGold = 0;
	}
}

// --------------------------------------------------------------------------------
// Name: GetGold
// Abstract: Get the dragon's gold amount.
// --------------------------------------------------------------------------------
int CDragon::GetGold()
{
	return m_intAmountOfGold;
}

// --------------------------------------------------------------------------------
// Name: MakeNoise
// Abstract: Override MakeNoise to represent roaring.
// --------------------------------------------------------------------------------
void CDragon::MakeNoise()
{
	cout << "ROAAAAAAAAAR!!" << endl;
}

// --------------------------------------------------------------------------------
// Name: BreatheFire
// Abstract: Dragon breathes fire.
// --------------------------------------------------------------------------------
void CDragon::BreatheFire()
{
	cout << GetName() << " breathes a massive wall of fire!" << endl;
}
