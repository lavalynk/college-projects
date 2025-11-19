// --------------------------------------------------------------------------------
// Name: CResizableArray
// Abstract: Implements a dynamically resizable array class with insertion, retrieval, 
// and removal functions.
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include "CResizableArray.h"


// --------------------------------------------------------------------------------
// Name: Initialize
// Abstract: Sets array size and pointer to 0.
// --------------------------------------------------------------------------------
void CResizableArray::Initialize()
{
	m_lngArraySize = 0;
	m_palngValues = nullptr;
}


// --------------------------------------------------------------------------------
// Name: SetSize
// Abstract: Resizes the array, saves values and sets new ones to 0.
// --------------------------------------------------------------------------------
void CResizableArray::SetSize(long lngNewSize)
{
	if (lngNewSize < 0)
	{
		cout << "Error: Negative size not allowed.\n";
		return;
	}

	long* palngNewArray = new long[lngNewSize];

	// Copy existing data and set new values to 0
	for (long intIndex = 0; intIndex < lngNewSize; intIndex += 1)
	{
		if (intIndex < m_lngArraySize)
		{
			palngNewArray[intIndex] = m_palngValues[intIndex]; // Copy old value
		}
		else
		{
			palngNewArray[intIndex] = 0; // Explicitly initialize new elements
		}
	}

	// Free old memory
	delete[] m_palngValues;

	// Assign new array
	m_palngValues = palngNewArray;
	m_lngArraySize = lngNewSize;
}


// --------------------------------------------------------------------------------
// Name: GetSize
// Abstract: Returns the size of the array.
// --------------------------------------------------------------------------------
long CResizableArray::GetSize()
{
	return m_lngArraySize;
}


// --------------------------------------------------------------------------------
// Name: SetValueAt
// Abstract: Sets a value at a specific index with clipping.
// --------------------------------------------------------------------------------
void CResizableArray::SetValueAt(long lngValue, long lngIndex)
{
	// Clip index to valid range
	if (lngIndex < 0)
	{
		lngIndex = 0;
	}
	else if (lngIndex >= m_lngArraySize)
	{
		lngIndex = m_lngArraySize - 1;
	}

	// Set value
	m_palngValues[lngIndex] = lngValue;
}


// --------------------------------------------------------------------------------
// Name: GetValueAt
// Abstract: Returns the value at a specific index withclipping.
// --------------------------------------------------------------------------------
long CResizableArray::GetValueAt(long lngIndex) const
{
	// Clip index to valid range
	if (lngIndex < 0)
	{
		lngIndex = 0;
	}
	else if (lngIndex >= m_lngArraySize)
	{
		lngIndex = m_lngArraySize - 1;
	}

	// Return value
	return m_palngValues[lngIndex];
}


// --------------------------------------------------------------------------------
// Name: AddValueToFront
// Abstract: Adds a value to the beginning of the array.
// --------------------------------------------------------------------------------
void CResizableArray::AddValueToFront(long lngValue)
{
	SetSize(m_lngArraySize + 1);

	// Shift elements right
	for (long intIndex = m_lngArraySize - 1; intIndex > 0; intIndex -= 1)
	{
		m_palngValues[intIndex] = m_palngValues[intIndex - 1];
	}

	m_palngValues[0] = lngValue;
}


// --------------------------------------------------------------------------------
// Name: AddValueToEnd
// Abstract: Adds a value to the end of the array.
// --------------------------------------------------------------------------------
void CResizableArray::AddValueToEnd(long lngValue)
{
	SetSize(m_lngArraySize + 1);
	m_palngValues[m_lngArraySize - 1] = lngValue;
}


// --------------------------------------------------------------------------------
// Name: InsertValueAt
// Abstract: Inserts a value at a specific index, shifting elements right.
// --------------------------------------------------------------------------------
void CResizableArray::InsertValueAt(long lngValue, long lngIndex)
{
	if (lngIndex < 0)
	{
		lngIndex = 0;
	}
	else if (lngIndex > m_lngArraySize)
	{
		lngIndex = m_lngArraySize;
	}

	SetSize(m_lngArraySize + 1);

	// Shift elements right
	for (long intIndex = m_lngArraySize - 1; intIndex > lngIndex; intIndex -= 1)
	{
		m_palngValues[intIndex] = m_palngValues[intIndex - 1];
	}

	m_palngValues[lngIndex] = lngValue;
}


// --------------------------------------------------------------------------------
// Name: RemoveAt
// Abstract: Removes an element and shifts remaining values left.
// --------------------------------------------------------------------------------
void CResizableArray::RemoveAt(long lngIndex)
{
	if (lngIndex < 0)
	{
		lngIndex = 0;
	}
	else if (lngIndex >= m_lngArraySize)
	{
		lngIndex = m_lngArraySize - 1;
	}

	// Shift elements left
	for (long intIndex = lngIndex; intIndex < m_lngArraySize - 1; intIndex += 1)
	{
		m_palngValues[intIndex] = m_palngValues[intIndex + 1];
	}

	SetSize(m_lngArraySize - 1);
}


// --------------------------------------------------------------------------------
// Name: Print
// Abstract: Displays the array contents in the required format.
// --------------------------------------------------------------------------------
void CResizableArray::Print()
{
	cout << "=============================================" << endl;
	for (long intIndex = 0; intIndex < m_lngArraySize; intIndex++)
	{
		cout << "Value at index" << intIndex << ": " << m_palngValues[intIndex] << endl;
	}
	cout << endl;
}