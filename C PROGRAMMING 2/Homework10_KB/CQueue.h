// --------------------------------------------------------------------------------
// Name: CQueue.h
// Abstract: Implements a First-In-First-Out (FIFO) Queue using a linked list.
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

class CQueue
{
    // --------------------------------------------------------------------------------
    // Properties
    // --------------------------------------------------------------------------------
public:

protected:

private:
    CNode* m_pclsHeadNode; // Points to the front of the queue
    CNode* m_pclsTailNode; // Points to the end of the queue

    // --------------------------------------------------------------------------------
    // Methods
    // --------------------------------------------------------------------------------
public:
    // Constructors
    CQueue();
    CQueue(const CQueue& clsOriginalToCopy);          // Deep copy with this copy constructor

    // Destructor
    ~CQueue();                                        // Clean up any allocated memory

    // Operators
    void operator=(const CQueue& clsOriginalToCopy);  // Deep Copy

    // Queue Operations
    void Push(int intValue);                          // Add 1 to the end of the queue
    int Pop();                                        // Remove 1 from the front of the queue
    bool IsEmpty() const;                             // Return true if head node is zero
    void Print() const;                               // This will print the whole list. 

protected:
    // Helper Methods
    void Initialize();
    void CleanUp();
    void DeepCopy(const CQueue& clsOriginalToCopy);

private:

};
