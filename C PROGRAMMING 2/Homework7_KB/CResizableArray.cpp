// --------------------------------------------------------------------------------
// Name: CResizableArray
// Abstract: A simple resizable array class with basic operations.
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include "CResizableArray.h"

// --------------------------------------------------------------------------------
// Name: CResizableArray
// Abstract: Default constructor – makes an empty array.
// --------------------------------------------------------------------------------
CResizableArray::CResizableArray()
{
    Initialize(0, 0);
}

// --------------------------------------------------------------------------------
// Name: CResizableArray
// Abstract: Constructor – makes an array of given size, all values start at 0.
// --------------------------------------------------------------------------------
CResizableArray::CResizableArray(long lngInitialSize)
{
    Initialize(lngInitialSize, 0);
}

// --------------------------------------------------------------------------------
// Name: CResizableArray
// Abstract: Constructor – makes an array of given size, all values start at given default.
// --------------------------------------------------------------------------------
CResizableArray::CResizableArray(long lngInitialSize, long lngDefaultValue)
{
    Initialize(lngInitialSize, lngDefaultValue);
}

// --------------------------------------------------------------------------------
// Name: Copy Constructor
// Abstract: Creates a new instance as a copy
// --------------------------------------------------------------------------------
CResizableArray::CResizableArray(const CResizableArray& clsOriginalToCopy)
{
    // Initialize
    Initialize(0, 0);

    // Use the assignment operator to copy data
    *this = clsOriginalToCopy;
}

// --------------------------------------------------------------------------------
// Name: Initialize
// Abstract: Sets up internal state and calls SetSize.
// --------------------------------------------------------------------------------
void CResizableArray::Initialize(long lngInitialSize, long lngDefaultValue)
{
    m_lngArraySize = 0;
    m_palngValues = nullptr;  // no data yet

    SetSize(lngInitialSize, lngDefaultValue);
}

// --------------------------------------------------------------------------------
// Name: ~CResizableArray
// Abstract: Destructor – cleans up any allocated memory.
// --------------------------------------------------------------------------------
CResizableArray::~CResizableArray()
{
    CleanUp();
}

// --------------------------------------------------------------------------------
// Name: CleanUp
// Abstract: Frees memory and resets to empty.
// --------------------------------------------------------------------------------
void CResizableArray::CleanUp()
{
    SetSize(0, 0);
}

// --------------------------------------------------------------------------------
// Name: SetSize
// Abstract: Changes array size, new spots get 0.
// --------------------------------------------------------------------------------
void CResizableArray::SetSize(long lngNewSize)
{
    SetSize(lngNewSize, 0);
}

// --------------------------------------------------------------------------------
// Name: SetSize (overload)
// Abstract: Changes array size, new spots get defaultValue.
// --------------------------------------------------------------------------------
void CResizableArray::SetSize(long lngNewSize, long lngDefaultValue)
{
    if (lngNewSize < 0)
    {
        cout << "Error: size must be non-negative\n";
        return;
    }

    long* palngNewValues = new long[lngNewSize];

    for (long i = 0; i < lngNewSize; i++)
    {
        if (i < m_lngArraySize)
            palngNewValues[i] = m_palngValues[i];
        else
            palngNewValues[i] = lngDefaultValue;
    }

    delete[] m_palngValues;
    m_palngValues = palngNewValues;
    m_lngArraySize = lngNewSize;
}

// --------------------------------------------------------------------------------
// Name: GetSize
// Abstract: Returns how many elements are in the array.
// --------------------------------------------------------------------------------
long CResizableArray::GetSize() const
{
    return m_lngArraySize;
}



// --------------------------------------------------------------------------------
// Name: AddValueToFront
// Abstract: Inserts a value at the front of the array.
// --------------------------------------------------------------------------------
void CResizableArray::AddValueToFront(long lngValue)
{
    SetSize(m_lngArraySize + 1);
    for (long i = m_lngArraySize - 1; i > 0; i--)
        m_palngValues[i] = m_palngValues[i - 1];
    m_palngValues[0] = lngValue;
}

// --------------------------------------------------------------------------------
// Name: AddValueToEnd
// Abstract: Appends a value at the end of the array.
// --------------------------------------------------------------------------------
void CResizableArray::AddValueToEnd(long lngValue)
{
    SetSize(m_lngArraySize + 1);
    m_palngValues[m_lngArraySize - 1] = lngValue;
}

// --------------------------------------------------------------------------------
// Name: Assignment Operator
// Abstract: Copies the contents of another CResizableArray object into this one.
// --------------------------------------------------------------------------------
void CResizableArray::operator = (const CResizableArray& clsOriginalToCopy)
{
    // Self assignment? Compare instance addresses
    if (this != &clsOriginalToCopy)
    {
        // No, copy
        CleanUp();
        DeepCopy(clsOriginalToCopy);
    }
}


// --------------------------------------------------------------------------------
// Name: Operator[]
// Abstract: Overloads subscript operator
// --------------------------------------------------------------------------------
long& CResizableArray::operator[](long lngIndex)
{
    if (lngIndex < 0)
    {
        // Clip to first Index
        lngIndex = 0;
    }
    else if (lngIndex > m_lngArraySize - 1)
    {
        // Clip to last Index
        lngIndex = m_lngArraySize - 1;
    }
    return m_palngValues[lngIndex];
}


// --------------------------------------------------------------------------------
// Name: Operator[] (const)
// Abstract: Overloads subscript operator for read-only access
// --------------------------------------------------------------------------------
const long& CResizableArray::operator[](long lngIndex) const
{
    if (lngIndex < 0)
    {
        // Clip to first index
        lngIndex = 0;
    }
    else if (lngIndex > m_lngArraySize - 1)
    {
        // Clip to last Index
        lngIndex = m_lngArraySize - 1;
    }
    return m_palngValues[lngIndex];
}


// --------------------------------------------------------------------------------
// Name: Operator+=
// Abstract: Overloads the += operator to append all elements of another array.
// --------------------------------------------------------------------------------
void CResizableArray::operator+=(const CResizableArray& clsOriginalToAppend)
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
CResizableArray CResizableArray::operator+(const CResizableArray& clsRight) const
{
    CResizableArray clsResult(m_lngArraySize + clsRight.m_lngArraySize);

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
void CResizableArray::DeepCopy(const CResizableArray& clsOriginalToCopy)
{
    // Clean up existing data before copying
    CleanUp();

    // Copy size
    m_lngArraySize = clsOriginalToCopy.m_lngArraySize;

    // Allocate new memory
    if (m_lngArraySize > 0)
    {
        m_palngValues = new long[m_lngArraySize];

        // Copy values from source array
        for (long lngIndex = 0; lngIndex < m_lngArraySize; lngIndex += 1)
        {
            (*this)[lngIndex] = clsOriginalToCopy[lngIndex];
        }
    }
}

// --------------------------------------------------------------------------------
// Name: InsertValueAt
// Abstract: Inserts value at index, shifts others right.
// --------------------------------------------------------------------------------
void CResizableArray::InsertValueAt(long lngValueToInsert, long lngInsertAtIndex)
{
    long* palngNewValues = 0;
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
    palngNewValues = new long[GetSize() + 1];

    // Copy 1st half of values from old array into new array
    for (lngIndex = 0; lngIndex < lngInsertAtIndex; lngIndex += 1)
    {
        *(palngNewValues + lngIndex) = *(m_palngValues + lngIndex);
    }

    // Insert value
    *(palngNewValues + lngInsertAtIndex) = lngValueToInsert;

    // Copy 2nd half of values from old array into new array
    for (lngIndex = lngInsertAtIndex; lngIndex < GetSize(); lngIndex += 1)
    {
        *(palngNewValues + lngIndex + 1) = *(m_palngValues + lngIndex);
    }

    // Delete old array
    delete[] m_palngValues;
    m_palngValues = 0;

    // Assign new array
    m_palngValues = palngNewValues;

    // Assign size
    m_lngArraySize += 1;
}

// --------------------------------------------------------------------------------
// Name: RemoveAt
// Abstract: Removes element at index, shifts others left.
// --------------------------------------------------------------------------------
void CResizableArray::RemoveAt(long lngRemoveAtIndex)
{
    long* palngNewValues = 0;
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
        palngNewValues = new long[GetSize() - 1];

        // Copy 1st half of values from old array into new array
        for (lngIndex = 0; lngIndex < lngRemoveAtIndex; lngIndex += 1)
        {
            *(palngNewValues + lngIndex) = *(m_palngValues + lngIndex);
        }

        // Copy 2nd half of values from old array into new array
        for (lngIndex = lngRemoveAtIndex + 1; lngIndex < GetSize(); lngIndex += 1)
        {
            *(palngNewValues + lngIndex - 1) = *(m_palngValues + lngIndex);
        }

        // Delete old array
        delete[] m_palngValues;
        m_palngValues = 0;

        // Assign new array
        m_palngValues = palngNewValues;

        // Assign size
        m_lngArraySize -= 1;
    }
}

// --------------------------------------------------------------------------------
// Name: Print
// Abstract: Print all the values
// --------------------------------------------------------------------------------
void CResizableArray::Print() const
{
    Print("Print Array");
}

// --------------------------------------------------------------------------------
// Name: Print
// Abstract: Displays the array contents with a caption using operator[]
// --------------------------------------------------------------------------------
void CResizableArray::Print(const char* pstrCaption) const
{
    long lngIndex = 0;

    cout << endl;
    cout << pstrCaption << endl;
    cout << "--------------------------------------" << endl;

    if (m_lngArraySize > 0)
    {
        for (lngIndex = 0; lngIndex < GetSize(); lngIndex += 1)
        {
            cout << "Location[ " << (lngIndex + 1) << " ] = "
                << (*this)[lngIndex] << endl;  // Use operator[]
        }
    }
    else
    {
        cout << "-Empty array" << endl;
    }

    cout << endl;
}
