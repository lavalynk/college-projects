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
#include <stdlib.h>
#include <iostream>
using namespace std;

class CResizableArray
{
	// --------------------------------------------------------------------------------
	// Properties
	// --------------------------------------------------------------------------------
public:

protected:
	long m_lngArraySize;
	long* m_palngValues;
private:


	// --------------------------------------------------------------------------------
	// Methods
	// --------------------------------------------------------------------------------
public:
	// Constructors
	CResizableArray();
	CResizableArray(long lngInitialSize);
	CResizableArray(long lngInitialSize, long lngDefaultValue);
	CResizableArray(const CResizableArray& clsOriginalToCopy);

	// Destructor
	~CResizableArray();

	// Operators
	void operator = (const CResizableArray& clsOriginalToCopy);

	// Overload Subscript [ ] Operators
	long& operator[](long lngIndex);
	const long& operator[](long lngIndex) const;

	// Overload += Operator
	void operator+=(const CResizableArray& clsOriginalToAppend);

	// Overload + Operator
	CResizableArray operator+(const CResizableArray& clsRight) const;

	// Set/Get Size
	void SetSize(long lngNewSize);
	void SetSize(long lngNewSize, long lngDefaultValue);
	long GetSize() const;

	// Add to Front/End
	void AddValueToFront(long lngValueToAdd);
	void AddValueToEnd(long lngValueToAdd);

	// Insert At
	void InsertValueAt(long lngValueToInsert, long lngInsertAtIndex);

	// Remove At
	void RemoveAt(long lngRemoveAtIndex);

	// Print 
	void Print() const;
	void Print(const char* pstrCaption) const;

protected:
	void Initialize(long lngInitialSize, long lngDefaultValue);
	void CleanUp();
	void DeepCopy(const CResizableArray& clsSource);

private:

};
