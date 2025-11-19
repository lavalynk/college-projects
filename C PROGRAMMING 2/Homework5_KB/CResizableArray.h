// --------------------------------------------------------------------------------
// Name: CResizableArray
// Abstract: Implements a dynamically resizable array class with various insert, 
// retrieval, and removal functions.
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Pre-compiler Directives
// --------------------------------------------------------------------------------
#pragma once  

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include <iostream>
using namespace std;

class CResizableArray
{
	// --------------------------------------------------------------------------------
	// Properties
	// --------------------------------------------------------------------------------
public:

protected:

private:
	long m_lngArraySize;
	long* m_palngValues;

	// --------------------------------------------------------------------------------
	// Methods
	// --------------------------------------------------------------------------------
public:

	// Initialization
	void Initialize();

	// Size Management
	void SetSize(long lngNewSize);
	long GetSize();

	// Value Access
	void SetValueAt(long lngValue, long lngIndex);
	long GetValueAt(long lngIndex) const;

	// Insert Methods
	void AddValueToFront(long lngValue);
	void AddValueToEnd(long lngValue);
	void InsertValueAt(long lngValue, long lngIndex);

	// Remove Method
	void RemoveAt(long lngIndex);

	// Print Method
	void Print();
};
