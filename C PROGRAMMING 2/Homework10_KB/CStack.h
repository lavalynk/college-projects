// --------------------------------------------------------------------------------
// Name: CStack.h
// Abstract: Implements a Last-In-First-Out (LIFO) Stack using a linked list.
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Pre-compiler Directives
// --------------------------------------------------------------------------------
#pragma once

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include <iostream>
#include "CNode.h"
using namespace std;

class CStack
{
    // --------------------------------------------------------------------------------
    // Properties
    // --------------------------------------------------------------------------------
public:

protected:

private:
    CNode* m_pclsHeadNode; // Points to the top of the stack

    // --------------------------------------------------------------------------------
    // Methods
    // --------------------------------------------------------------------------------
public:
    // Constructors
    CStack();
    CStack(const CStack& clsOriginalToCopy);            // Deep copy with this copy constructors

    // Destructor
    ~CStack();                                          // Clean up any allocated memory

    // Operators
    void operator=(const CStack& clsOriginalToCopy);    // Deep copy

    // Stack Operations
    void Push(int intValue);                            // Add 1 value to the top of the stack
    int Pop();                                          // Remove 1 value from the top of the stack
    bool IsEmpty() const;                               // Return true if head node pointer is zero
    void Print() const;                                 // This will print the whole list. 

protected:
    // Helper Methods
    void Initialize();
    void CleanUp();
    void DeepCopy(const CStack& clsOriginalToCopy);

private:

};
