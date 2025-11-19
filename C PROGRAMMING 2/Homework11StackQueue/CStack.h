// ====================================================================================
// Author: Keith Brock
// Class: C Programming 2
// File: CStack.h
// Purpose: Declares a templated LIFO (Last-In-First-Out) stack data structure.
//          Uses linked nodes to allow dynamic memory management and unlimited size.
// ====================================================================================

#ifndef CSTACK_H
#define CSTACK_H

// ====================================================================================
// Includes
// ====================================================================================
#include <iostream>
#include "CNode.h"
using namespace std;

// ====================================================================================
// Template Class: CStack
// Summary       : Implements a stack using singly linked nodes.
// ====================================================================================
template <typename GenericDataType>
class CStack
{
    // ====================================================================================
    // Public Members – External Interface
    // ====================================================================================
public:
    // -----------------------------
    // Constructors & Destructor
    // -----------------------------
    CStack();                                                              // Default constructor
    CStack(const CStack<GenericDataType>& clsOriginalToCopy);             // Copy constructor
    ~CStack();                                                             // Destructor

    // -----------------------------
    // Assignment Operator
    // -----------------------------
    void operator=(const CStack<GenericDataType>& clsOriginalToCopy);     // Deep copy via =

    // -----------------------------
    // Stack Operations
    // -----------------------------
    void Push(GenericDataType gdtValue);                                   // Push to top
    GenericDataType Pop();                                                 // Pop from top
    bool IsEmpty() const;                                                  // Check for emptiness
    void Print() const;                                                    // Output contents

    // ====================================================================================
    // Protected Helpers – Internal Utility Logic
    // ====================================================================================
protected:
    void Initialize();                                                     // Set initial state
    void CleanUp();                                                        // Deallocate memory
    void DeepCopy(const CStack<GenericDataType>& clsOriginalToCopy);      // Copy all elements

    // ====================================================================================
    // Private Members – Data Pointer
    // ====================================================================================
private:
    CNode<GenericDataType>* m_pclsHeadNode;  // Pointer to the top node (LIFO)
};

// Link implementation source
#include "CStack.cpp"

#endif // CSTACK_H
