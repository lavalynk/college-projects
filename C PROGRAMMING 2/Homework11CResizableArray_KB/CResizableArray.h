// ====================================================================================
// Author: Keith Brock
// Class: C Programming 2
// File: CResizableArray.h
// Purpose: Declares a generic, dynamically resizable array using C++ templates.
//          Supports insertion, removal, expansion, and operator overloading.
// ====================================================================================

#ifndef CRESIZABLE_ARRAY_H
#define CRESIZABLE_ARRAY_H

// ====================================================================================
// Includes
// ====================================================================================
#include <iostream>
using namespace std;

// ====================================================================================
// Template Class: CResizableArray
// Summary       : A flexible, type-agnostic array implementation with dynamic resizing.
// ====================================================================================
template <typename GenericDataType>
class CResizableArray
{
    // ====================================================================================
    // Public Interface – Constructors, Mutators, Accessors, Operators
    // ====================================================================================
public:
    // -----------------------------
    // Constructors & Destructor
    // -----------------------------
    CResizableArray();                                                                // Default constructor
    CResizableArray(long lngInitialSize);                                             // Size-only constructor
    CResizableArray(long lngInitialSize, GenericDataType gdtDefaultValue);           // Size + default value
    CResizableArray(const CResizableArray<GenericDataType>& clsOriginalToCopy);       // Copy constructor
    ~CResizableArray();                                                               // Destructor

    // -----------------------------
    // Assignment & Operators
    // -----------------------------
    void operator=(const CResizableArray<GenericDataType>& clsOriginalToCopy);        // Assignment
    GenericDataType& operator[](long lngIndex);                                       // Subscript (write)
    const GenericDataType& operator[](long lngIndex) const;                           // Subscript (read)
    void operator+=(const CResizableArray<GenericDataType>& clsOriginalToAppend);     // Append
    CResizableArray<GenericDataType> operator+(const CResizableArray<GenericDataType>& clsRight) const; // Combine

    // -----------------------------
    // Resizing & Info
    // -----------------------------
    void SetSize(long lngNewSize);                                                    // Resize
    void SetSize(long lngNewSize, GenericDataType gdtDefaultValue);                   // Resize + fill
    long GetSize() const;                                                             // Retrieve size

    // -----------------------------
    // Insertion/Removal Methods
    // -----------------------------
    void AddValueToFront(GenericDataType gdtValueToAdd);                              // Add to front
    void AddValueToEnd(GenericDataType gdtValueToAdd);                                // Add to end
    void InsertValueAt(GenericDataType gdtValueToInsert, long lngInsertAtIndex);      // Insert at index
    void RemoveAt(long lngRemoveAtIndex);                                             // Remove at index

    // -----------------------------
    // Display
    // -----------------------------
    void Print() const;                                                               // Print contents
    void Print(const char* pstrCaption) const;                                        // Print with caption

    // ====================================================================================
    // Protected Members – Internal Utility
    // ====================================================================================
protected:
    void Initialize(long lngInitialSize, GenericDataType gdtDefaultValue);            // Initialization logic
    void CleanUp();                                                                   // Deallocate memory
    void DeepCopy(const CResizableArray<GenericDataType>& clsSource);                 // Copy from another

    // ====================================================================================
    // Private Members – Internal Data
    // ====================================================================================
protected:
    long m_lngArraySize;                                                              // Logical size of array
    GenericDataType* m_pagdtValues;                                                   // Pointer to dynamic array
};

// ====================================================================================
// Include Implementation File
// ====================================================================================
#include "CResizableArray.cpp"

#endif // CRESIZABLE_ARRAY_H
