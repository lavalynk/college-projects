// ====================================================================================
// Author: Keith Brock
// Class: C Programming 2
// File: CQueue.h
// Purpose: Defines a templated FIFO Queue structure using linked nodes.
//          Supports standard queue operations and dynamic memory handling.
// ====================================================================================

#ifndef CQUEUE_H
#define CQUEUE_H

// ====================================================================================
// Includes
// ====================================================================================
#include <iostream>
#include "CNode.h"
using namespace std;

// ====================================================================================
// Template Class: CQueue
// Summary       : Implements a First-In-First-Out (FIFO) Queue with deep copy support.
// ====================================================================================
template <typename GenericDataType>
class CQueue
{
    // ====================================================================================
    // Public Members – Constructor, Destructor, Core Operations
    // ====================================================================================
public:
    // -----------------------------
    // Constructors & Destructor
    // -----------------------------
    CQueue();                                                               // Default constructor
    CQueue(const CQueue<GenericDataType>& clsOriginalToCopy);              // Copy constructor
    ~CQueue();                                                              // Destructor

    // -----------------------------
    // Assignment Operator
    // -----------------------------
    void operator=(const CQueue<GenericDataType>& clsOriginalToCopy);      // Deep copy via =

    // -----------------------------
    // Core Queue Operations
    // -----------------------------
    void Push(GenericDataType gdtValue);                                    // Add to rear
    GenericDataType Pop();                                                  // Remove from front
    bool IsEmpty() const;                                                   // Check if queue is empty
    void Print() const;                                                     // Display contents

    // ====================================================================================
    // Protected Helpers – Internal Utility Methods
    // ====================================================================================
protected:
    void Initialize();                                                      // Setup initial state
    void CleanUp();                                                         // Deallocate memory
    void DeepCopy(const CQueue<GenericDataType>& clsOriginalToCopy);       // Copy contents from another queue

    // ====================================================================================
    // Private Members – Internal Data Pointers
    // ====================================================================================
private:
    CNode<GenericDataType>* m_pclsHeadNode;  // Points to front (dequeue point)
    CNode<GenericDataType>* m_pclsTailNode;  // Points to rear (enqueue point)
};

// Implementation file inclusion
#include "CQueue.cpp"

#endif // CQUEUE_H
