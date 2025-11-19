// ====================================================================================
// Author: Keith Brock
// Class: C Programming 2
// File: CQueue.cpp
// Purpose: Implements a templated FIFO (First-In-First-Out) queue using linked nodes.
// ====================================================================================

#ifndef CQUEUE_CPP
#define CQUEUE_CPP

// ====================================================================================
// Includes
// ====================================================================================
#include "CQueue.h"

// ====================================================================================
// Constructor: CQueue
// Summary   : Initializes an empty queue.
// ====================================================================================
template <typename GenericDataType>
CQueue<GenericDataType>::CQueue()
{
    Initialize();
}

// ====================================================================================
// Copy Constructor: CQueue
// Summary   : Creates a deep copy of an existing queue.
// ====================================================================================
template <typename GenericDataType>
CQueue<GenericDataType>::CQueue(const CQueue<GenericDataType>& clsOriginalToCopy)
{
    Initialize();
    DeepCopy(clsOriginalToCopy);
}

// ====================================================================================
// Destructor: ~CQueue
// Summary   : Releases all dynamically allocated memory in the queue.
// ====================================================================================
template <typename GenericDataType>
CQueue<GenericDataType>::~CQueue()
{
    CleanUp();
}

// ====================================================================================
// Operator Overload: =
// Summary   : Performs a deep copy of another queue during assignment.
// ====================================================================================
template <typename GenericDataType>
void CQueue<GenericDataType>::operator=(const CQueue<GenericDataType>& clsOriginalToCopy)
{
    if (this != &clsOriginalToCopy)
    {
        CleanUp();
        DeepCopy(clsOriginalToCopy);
    }
}

// ====================================================================================
// Method: Initialize
// Summary: Resets head and tail pointers to represent an empty queue.
// ====================================================================================
template <typename GenericDataType>
void CQueue<GenericDataType>::Initialize()
{
    m_pclsHeadNode = 0;
    m_pclsTailNode = 0;
}

// ====================================================================================
// Method: CleanUp
// Summary: Frees all nodes in the queue to avoid memory leaks.
// ====================================================================================
template <typename GenericDataType>
void CQueue<GenericDataType>::CleanUp()
{
    CNode<GenericDataType>* pclsCurrentNode = m_pclsHeadNode;
    CNode<GenericDataType>* pclsNextNode = 0;

    while (pclsCurrentNode != 0)
    {
        pclsNextNode = pclsCurrentNode->GetNextNode();
        delete pclsCurrentNode;
        pclsCurrentNode = pclsNextNode;
    }

    m_pclsHeadNode = 0;
    m_pclsTailNode = 0;
}

// ====================================================================================
// Method: DeepCopy
// Summary: Duplicates every node from the source queue into the current queue.
// ====================================================================================
template <typename GenericDataType>
void CQueue<GenericDataType>::DeepCopy(const CQueue<GenericDataType>& clsOriginalToCopy)
{
    if (clsOriginalToCopy.m_pclsHeadNode != 0)
    {
        m_pclsHeadNode = new CNode<GenericDataType>(clsOriginalToCopy.m_pclsHeadNode->GetValue());
        m_pclsTailNode = m_pclsHeadNode;

        CNode<GenericDataType>* pclsSourceNode = clsOriginalToCopy.m_pclsHeadNode->GetNextNode();

        while (pclsSourceNode != 0)
        {
            m_pclsTailNode->SetNextNode(new CNode<GenericDataType>(pclsSourceNode->GetValue()));
            m_pclsTailNode = m_pclsTailNode->GetNextNode();
            pclsSourceNode = pclsSourceNode->GetNextNode();
        }
    }
}

// ====================================================================================
// Method: Push
// Summary: Adds a new value to the rear of the queue.
// ====================================================================================
template <typename GenericDataType>
void CQueue<GenericDataType>::Push(GenericDataType gdtValue)
{
    CNode<GenericDataType>* pclsNewNode = new CNode<GenericDataType>(gdtValue);

    if (IsEmpty())
    {
        m_pclsHeadNode = pclsNewNode;
        m_pclsTailNode = pclsNewNode;
    }
    else
    {
        m_pclsTailNode->SetNextNode(pclsNewNode);
        m_pclsTailNode = pclsNewNode;
    }
}

// ====================================================================================
// Method: Pop
// Summary: Removes and returns the front value of the queue.
//          If the queue is empty, returns default-initialized value.
// ====================================================================================
template <typename GenericDataType>
GenericDataType CQueue<GenericDataType>::Pop()
{
    GenericDataType gdtValue = GenericDataType();
    CNode<GenericDataType>* pclsOldHead = 0;

    if (!IsEmpty())
    {
        gdtValue = m_pclsHeadNode->GetValue();
        pclsOldHead = m_pclsHeadNode;
        m_pclsHeadNode = m_pclsHeadNode->GetNextNode();
        delete pclsOldHead;

        if (m_pclsHeadNode == 0)
            m_pclsTailNode = 0;
    }

    return gdtValue;
}

// ====================================================================================
// Method: IsEmpty
// Summary: Checks whether the queue has no elements.
// ====================================================================================
template <typename GenericDataType>
bool CQueue<GenericDataType>::IsEmpty() const
{
    return (m_pclsHeadNode == 0);
}

// ====================================================================================
// Method: Print
// Summary: Displays the current queue contents without altering its structure.
// ====================================================================================
template <typename GenericDataType>
void CQueue<GenericDataType>::Print() const
{
    CNode<GenericDataType>* pclsCurrentNode = m_pclsHeadNode;
    cout << "Queue contents (front to back):" << endl;

    while (pclsCurrentNode != 0)
    {
        cout << pclsCurrentNode->GetValue() << " ";
        pclsCurrentNode = pclsCurrentNode->GetNextNode();
    }

    cout << endl;
}

#endif // CQUEUE_CPP
