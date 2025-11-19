// --------------------------------------------------------------------------------
// Name: CResizableArray.cpp
// Abstract: Implements a dynamically resizable array class using templates.
// --------------------------------------------------------------------------------

#ifndef CRESIZABLE_ARRAY_CPP
#define CRESIZABLE_ARRAY_CPP

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include "CResizableArray.h"

// --------------------------------------------------------------------------------
// Name: CResizableArray 
// Abstract: Default constructor - initializes an empty resizable array. --Homework11
// --------------------------------------------------------------------------------
template <typename GenericDataType>
CResizableArray<GenericDataType>::CResizableArray()
{
    Initialize(0, GenericDataType()); // Initialize with default value of GenericDataType
}



// --------------------------------------------------------------------------------
// Name: CResizableArray 
// Abstract: Constructor - initializes array with a given size, defaulting values to GenericDataType default.
// --------------------------------------------------------------------------------
template <typename GenericDataType>
CResizableArray<GenericDataType>::CResizableArray(long lngInitialSize)
{
    Initialize(lngInitialSize, GenericDataType()); // Use default constructor for the data type
}



// --------------------------------------------------------------------------------
// Name: CResizableArray 
// Abstract: Constructor - initializes array with a given size and default value.
// --------------------------------------------------------------------------------
template <typename GenericDataType>
CResizableArray<GenericDataType>::CResizableArray(long lngInitialSize, GenericDataType gdtDefaultValue)
{
    Initialize(lngInitialSize, gdtDefaultValue);
}



// --------------------------------------------------------------------------------
// Name: CResizableArray 
// Abstract: Copy constructor - creates a new instance as a copy of an existing instance.
// --------------------------------------------------------------------------------
template <typename GenericDataType>
CResizableArray<GenericDataType>::CResizableArray(const CResizableArray<GenericDataType>& clsOriginalToCopy)
{
    // Initialize with default values
    Initialize(0, GenericDataType());

    // Use the assignment operator to copy data
    *this = clsOriginalToCopy;
}



// --------------------------------------------------------------------------------
// Name: Initialize 
// Abstract: Sets class properties to default values and calls SetSize().
// --------------------------------------------------------------------------------
template <typename GenericDataType>
void CResizableArray<GenericDataType>::Initialize(long lngInitialSize, GenericDataType gdtDefaultValue)
{
    m_lngArraySize = 0;
    m_pagdtValues = 0;
    SetSize(lngInitialSize, gdtDefaultValue);
}



// --------------------------------------------------------------------------------
// Name: Destructor 
// Abstract: Calls CleanUp to free memory.
// --------------------------------------------------------------------------------
template <typename GenericDataType>
CResizableArray<GenericDataType>::~CResizableArray()
{
    CleanUp();
}



// --------------------------------------------------------------------------------
// Name: CleanUp 
// Abstract: Frees allocated memory and resets array properties.
// --------------------------------------------------------------------------------
template <typename GenericDataType>
void CResizableArray<GenericDataType>::CleanUp()
{
    SetSize(0, GenericDataType());
}



// --------------------------------------------------------------------------------
// Name: SetSize 
// Abstract: Resizes the array, setting new elements to the default value of GenericDataType.
// --------------------------------------------------------------------------------
template <typename GenericDataType>
void CResizableArray<GenericDataType>::SetSize(long lngNewSize)
{
    SetSize(lngNewSize, GenericDataType()); // Use the default value of GenericDataType
}



// --------------------------------------------------------------------------------
// Name: SetSize (Overloaded)
// Abstract: Resizes the array, preserving existing values and setting new ones to gdtDefaultValue.
// --------------------------------------------------------------------------------
template <typename GenericDataType>
void CResizableArray<GenericDataType>::SetSize(long lngNewSize, GenericDataType gdtDefaultValue)
{
    GenericDataType* pagdtNewValues = 0; // Pointer for the new array
    long lngIndex = 0;
    long lngStop = 0;

    // Boundary check for invalid sizes
    if (lngNewSize < 0)
    {
        lngNewSize = 0;
    }
    else if (lngNewSize > 100000)
    {
        lngNewSize = 100000;
    }

    // Check for changed array size
    if (lngNewSize != m_lngArraySize)
    {
        // Allocate space if necessary
        if (lngNewSize > 0)
        {
            pagdtNewValues = new GenericDataType[lngNewSize];
        }

        // Initialize new memory
        for (lngIndex = 0; lngIndex < lngNewSize; lngIndex += 1)
        {
            *(pagdtNewValues + lngIndex) = gdtDefaultValue;
        }

        // Preserve as many values as possible
        if (lngNewSize < m_lngArraySize)
        {
            lngStop = lngNewSize;
        }
        else
        {
            lngStop = m_lngArraySize;
        }

        // Copy values from old array to new array
        for (lngIndex = 0; lngIndex < lngStop; lngIndex += 1)
        {
            *(pagdtNewValues + lngIndex) = *(m_pagdtValues + lngIndex);
        }

        // Delete old array
        if (m_pagdtValues != 0)
        {
            delete[] m_pagdtValues;
            m_pagdtValues = 0;
        }

        // Assign new array and size
        m_pagdtValues = pagdtNewValues;
        m_lngArraySize = lngNewSize;
    }
}



// --------------------------------------------------------------------------------
// Name: GetSize 
// Abstract: Returns the size of the array.
// --------------------------------------------------------------------------------
template <typename GenericDataType>
long CResizableArray<GenericDataType>::GetSize() const
{
    return m_lngArraySize;
}



// --------------------------------------------------------------------------------
// Name: AddValueToFront 
// Abstract: Adds a value to the beginning of the array.
// --------------------------------------------------------------------------------
template <typename GenericDataType>
void CResizableArray<GenericDataType>::AddValueToFront(GenericDataType gdtValueToAdd)
{
    InsertValueAt(gdtValueToAdd, 0);
}



// --------------------------------------------------------------------------------
// Name: AddValueToEnd 
// Abstract: Adds a value to the end of the array.
// --------------------------------------------------------------------------------
template <typename GenericDataType>
void CResizableArray<GenericDataType>::AddValueToEnd(GenericDataType gdtValueToAdd)
{
    InsertValueAt(gdtValueToAdd, GetSize());
}



// --------------------------------------------------------------------------------
// Name: Assignment Operator 
// Abstract: Copies the contents of another CResizableArray object into this one.
// --------------------------------------------------------------------------------
template <typename GenericDataType>
void CResizableArray<GenericDataType>::operator=(const CResizableArray<GenericDataType>& clsOriginalToCopy)
{
    // Self-assignment check: Compare instance addresses
    if (this != &clsOriginalToCopy)
    {
        // No, perform deep copy
        CleanUp();
        DeepCopy(clsOriginalToCopy);
    }
}



// --------------------------------------------------------------------------------
// Name: Operator[] 
// Abstract: Overloads subscript operator for non-const access.
// --------------------------------------------------------------------------------
template <typename GenericDataType>
GenericDataType& CResizableArray<GenericDataType>::operator[](long lngIndex)
{
    if (lngIndex < 0)
    {
        // Clip to first index
        lngIndex = 0;
    }
    else if (lngIndex > m_lngArraySize - 1)
    {
        // Clip to last index
        lngIndex = m_lngArraySize - 1;
    }
    return m_pagdtValues[lngIndex];
}



// --------------------------------------------------------------------------------
// Name: Operator[] (Const) 
// Abstract: Overloads subscript operator for read-only access.
// --------------------------------------------------------------------------------
template <typename GenericDataType>
const GenericDataType& CResizableArray<GenericDataType>::operator[](long lngIndex) const
{
    if (lngIndex < 0)
    {
        // Clip to first index
        lngIndex = 0;
    }
    else if (lngIndex > m_lngArraySize - 1)
    {
        // Clip to last index
        lngIndex = m_lngArraySize - 1;
    }
    return m_pagdtValues[lngIndex];
}



// --------------------------------------------------------------------------------
// Name: Operator+= 
// Abstract: Overloads the += operator to append all elements of another array.
// --------------------------------------------------------------------------------
template <typename GenericDataType>
void CResizableArray<GenericDataType>::operator+=(const CResizableArray<GenericDataType>& clsOriginalToAppend)
{
    if (clsOriginalToAppend.m_lngArraySize > 0)
    {
        // Store current size
        long lngOldSize = m_lngArraySize;

        // Expand the array
        SetSize(m_lngArraySize + clsOriginalToAppend.m_lngArraySize);

        // Copy elements from clsOriginalToAppend to the expanded array
        for (long lngIndex = 0; lngIndex < clsOriginalToAppend.m_lngArraySize; lngIndex += 1)
        {
            (*this)[lngOldSize + lngIndex] = clsOriginalToAppend[lngIndex];
        }
    }
}



// --------------------------------------------------------------------------------
// Name: Operator+ 
// Abstract: Overloads the + operator to combine two arrays into a new one.
// --------------------------------------------------------------------------------
template <typename GenericDataType>
CResizableArray<GenericDataType> CResizableArray<GenericDataType>::operator+(const CResizableArray<GenericDataType>& clsRight) const
{
    CResizableArray<GenericDataType> clsResult(m_lngArraySize + clsRight.m_lngArraySize);

    // Copy elements from the left side array (this)
    for (long lngIndex = 0; lngIndex < m_lngArraySize; lngIndex += 1)
    {
        clsResult[lngIndex] = (*this)[lngIndex];
    }

    // Copy elements from the right side array (clsRight)
    for (long lngIndex = 0; lngIndex < clsRight.m_lngArraySize; lngIndex += 1)
    {
        clsResult[m_lngArraySize + lngIndex] = clsRight[lngIndex];
    }

    return clsResult;
}



// --------------------------------------------------------------------------------
// Name: DeepCopy 
// Abstract: Copies the contents of another CResizableArray object into this one.
// --------------------------------------------------------------------------------
template <typename GenericDataType>
void CResizableArray<GenericDataType>::DeepCopy(const CResizableArray<GenericDataType>& clsOriginalToCopy)
{
    // Clean up existing data before copying
    CleanUp();

    // Copy size
    m_lngArraySize = clsOriginalToCopy.m_lngArraySize;

    // Allocate new memory
    if (m_lngArraySize > 0)
    {
        m_pagdtValues = new GenericDataType[m_lngArraySize];

        // Copy values from source array
        for (long lngIndex = 0; lngIndex < m_lngArraySize; lngIndex += 1)
        {
            (*this)[lngIndex] = clsOriginalToCopy[lngIndex];
        }
    }
}



// --------------------------------------------------------------------------------
// Name: InsertValueAt 
// Abstract: Inserts a value at a specific index, shifting elements right.
// --------------------------------------------------------------------------------
template <typename GenericDataType>
void CResizableArray<GenericDataType>::InsertValueAt(GenericDataType gdtValueToInsert, long lngInsertAtIndex)
{
    GenericDataType* pagdtNewValues = 0;
    long lngIndex = 0;

    if (lngInsertAtIndex < 0)
    {
        lngInsertAtIndex = 0;
    }
    else if (lngInsertAtIndex > m_lngArraySize)
    {
        lngInsertAtIndex = m_lngArraySize;
    }

    // Make a new array of size one larger
    pagdtNewValues = new GenericDataType[GetSize() + 1];

    // Copy 1st half of values from old array into new array
    for (lngIndex = 0; lngIndex < lngInsertAtIndex; lngIndex += 1)
    {
        *(pagdtNewValues + lngIndex) = *(m_pagdtValues + lngIndex);
    }

    // Insert value
    *(pagdtNewValues + lngInsertAtIndex) = gdtValueToInsert;

    // Copy 2nd half of values from old array into new array
    for (lngIndex = lngInsertAtIndex; lngIndex < GetSize(); lngIndex += 1)
    {
        *(pagdtNewValues + lngIndex + 1) = *(m_pagdtValues + lngIndex);
    }

    // Delete old array
    delete[] m_pagdtValues;
    m_pagdtValues = 0;

    // Assign new array
    m_pagdtValues = pagdtNewValues;

    // Assign size
    m_lngArraySize += 1;
}



// --------------------------------------------------------------------------------
// Name: RemoveAt 
// Abstract: Removes an element and shifts remaining values left.
// --------------------------------------------------------------------------------
template <typename GenericDataType>
void CResizableArray<GenericDataType>::RemoveAt(long lngRemoveAtIndex)
{
    GenericDataType* pagdtNewValues = 0;
    long lngIndex = 0;

    if (GetSize() > 0)
    {
        if (lngRemoveAtIndex < 0)
        {
            lngRemoveAtIndex = 0;
        }
        else if (lngRemoveAtIndex > m_lngArraySize - 1)
        {
            lngRemoveAtIndex = m_lngArraySize - 1;
        }

        // Make a new array of size one smaller
        pagdtNewValues = new GenericDataType[GetSize() - 1];

        // Copy 1st half of values from old array into new array
        for (lngIndex = 0; lngIndex < lngRemoveAtIndex; lngIndex += 1)
        {
            *(pagdtNewValues + lngIndex) = *(m_pagdtValues + lngIndex);
        }

        // Copy 2nd half of values from old array into new array
        for (lngIndex = lngRemoveAtIndex + 1; lngIndex < GetSize(); lngIndex += 1)
        {
            *(pagdtNewValues + lngIndex - 1) = *(m_pagdtValues + lngIndex);
        }

        // Delete old array
        delete[] m_pagdtValues;
        m_pagdtValues = 0;

        // Assign new array
        m_pagdtValues = pagdtNewValues;

        // Assign size
        m_lngArraySize -= 1;
    }
}



// --------------------------------------------------------------------------------
// Name: Print 
// Abstract: Print all the values.
// --------------------------------------------------------------------------------
template <typename GenericDataType>
void CResizableArray<GenericDataType>::Print() const
{
    Print("Print Array");
}



// --------------------------------------------------------------------------------
// Name: Print 
// Abstract: Displays the array contents with a caption using operator[].
// --------------------------------------------------------------------------------
template <typename GenericDataType>
void CResizableArray<GenericDataType>::Print(const char* pstrCaption) const
{
    long lngIndex = 0;

    // Caption
    cout << endl;
    cout << pstrCaption << endl;
    cout << "--------------------------------------------------------------------------------------------" << endl;
    // Check if array has elements
    if (m_lngArraySize > 0)
    {
        // Loop through each element in the array
        for (lngIndex = 0; lngIndex < GetSize(); lngIndex += 1)
        {
            // Print the current index and it's value
            cout << "Location[ " << (lngIndex + 1) << " ] = "
                << (*this)[lngIndex] << endl;  // Use operator[] access value
        }
    }
    else
    {
        // If empty array, print  message indicating that.
        cout << "-Empty array" << endl;
    }

    cout << endl;
}


#endif // CRESIZABLE_ARRAY_CPP