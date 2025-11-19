// --------------------------------------------------------------------------------
// Name: CCow
// Abstract: Class method definitions for the CCow class.
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include "CCow.h"
#include <iostream>
#include <cstring>
#define _CRT_SECURE_NO_WARNINGS


// --------------------------------------------------------------------------------
// Name: SetColor
// Abstract: Set the cow's color.
// --------------------------------------------------------------------------------
void CCow::SetColor(const char* pstrNewColor)
{
	if (pstrNewColor != nullptr && strlen(pstrNewColor) < 50)
	{
		strcpy_s(m_strColor, pstrNewColor);
	}
	else
	{
		m_strColor[0] = '\0';
	}
}


// --------------------------------------------------------------------------------
// Name: GetColor
// Abstract: Get the cow's color.
// --------------------------------------------------------------------------------
const char* CCow::GetColor()
{
	return m_strColor;
}


// --------------------------------------------------------------------------------
// Name: MakeNoise
// Abstract: Override MakeNoise to represent mooing.
// --------------------------------------------------------------------------------
void CCow::MakeNoise()
{
	cout << "Mooooooooooooooooooooooooooooooooo!" << endl;
}


// --------------------------------------------------------------------------------
// Name: Graze
// Abstract: Cow grazing action.
// --------------------------------------------------------------------------------
void CCow::Graze()
{
	cout << GetName() << " is grazing in the field." << endl;
}
