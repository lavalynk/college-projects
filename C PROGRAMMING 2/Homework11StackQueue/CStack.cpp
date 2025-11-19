// ====================================================================================
// Author: Keith Brock
// Class: C Programming 2
// File: CStack.cpp
// Purpose: Implements a templated LIFO (Last-In-First-Out) stack using linked nodes.
// ====================================================================================

#ifndef CSTACK_CPP
#define CSTACK_CPP

// ====================================================================================
// Includes
// ====================================================================================
#include "CStack.h"

// ====================================================================================
// Constructor: CStack
// Summary   : Initializes an empty stack.
// ====================================================================================
template <typename GenericDataType>
CStack<GenericDataType>::CStack()
{
    Initialize();
}

// ====================================================================================
// Copy Constructor: CStack
// Summary   : Performs a deep copy of another stack.
// ====================================================================================
template <typename GenericDataType>
CStack<GenericDataType>::CStack(const CStack<GenericDataType>& clsOriginalToCopy)
{
    Initialize();
    DeepCopy(clsOriginalToCopy);
}

// ====================================================================================
// Destructor: ~CStack
// Summary   : Deletes all dynamically allocated nodes in the stack.
// ====================================================================================
template <typename GenericDataType>
CStack<GenericDataType>::~CStack()
{
    CleanUp();
}

// ====================================================================================
// Operator Overload: =
// Summary   : Deep copies another stack during assignment.
// ====================================================================================
template <typename GenericDataType>
void CStack<GenericDataType>::operator=(const CStack<GenericDataType>& clsOriginalToCopy)
{
    if (this != &clsOriginalToCopy)
    {
        CleanUp();
        DeepCopy(clsOriginalToCopy);
    }
}

// ====================================================================================
// Method: Initialize
// Summary: Sets the stack to an empty state.
// ====================================================================================
template <typename GenericDataType>
void CStack<GenericDataType>::Initialize()
{
    m_pclsHeadNode = 0;
}

// ====================================================================================
// Method: CleanUp
// Summary: Frees all memory allocated in the stack.
// ====================================================================================
template <typename GenericDataType>
void CStack<GenericDataType>::CleanUp()
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
}

// ====================================================================================
// Method: DeepCopy
// Summary: Creates an exact deep copy of another stack.
// ====================================================================================
template <typename GenericDataType>
void CStack<GenericDataType>::DeepCopy(const CStack<GenericDataType>& clsOriginalToCopy)
{
    if (clsOriginalToCopy.m_pclsHeadNode != 0)
    {
        m_pclsHeadNode = new CNode<GenericDataType>(clsOriginalToCopy.m_pclsHeadNode->GetValue());
        CNode<GenericDataType>* pclsSourceNode = clsOriginalToCopy.m_pclsHeadNode->GetNextNode();
        CNode<GenericDataType>* pclsNewNode = m_pclsHeadNode;

        while (pclsSourceNode != 0)
        {
            pclsNewNode->SetNextNode(new CNode<GenericDataType>(pclsSourceNode->GetValue()));
            pclsNewNode = pclsNewNode->GetNextNode();
            pclsSourceNode = pclsSourceNode->GetNextNode();
        }
    }
}

// ====================================================================================
// Method: Push
// Summary: Adds a new element to the top of the stack.
// ====================================================================================
template <typename GenericDataType>
void CStack<GenericDataType>::Push(GenericDataType gdtValue)
{
    CNode<GenericDataType>* pclsNewNode = new CNode<GenericDataType>(gdtValue, m_pclsHeadNode);
    m_pclsHeadNode = pclsNewNode;
}

// ====================================================================================
// Method: Pop
// Summary: Removes and returns the top value from the stack.
//          Returns default-initialized value if stack is empty.
// ====================================================================================
template <typename GenericDataType>
GenericDataType CStack<GenericDataType>::Pop()
{
    GenericDataType gdtValue = GenericDataType();
    CNode<GenericDataType>* pclsOldHead = 0;

    if (!IsEmpty())
    {
        gdtValue = m_pclsHeadNode->GetValue();
        pclsOldHead = m_pclsHeadNode;
        m_pclsHeadNode = m_pclsHeadNode->GetNextNode();
        delete pclsOldHead;
    }

    return gdtValue;
}

// ====================================================================================
// Method: IsEmpty
// Summary: Checks whether the stack contains any elements.
// ====================================================================================
template <typename GenericDataType>
bool CStack<GenericDataType>::IsEmpty() const
{
    return (m_pclsHeadNode == 0);
}

// ====================================================================================
// Method: Print
// Summary: Outputs the stack's contents from top to bottom.
// ====================================================================================
template <typename GenericDataType>
void CStack<GenericDataType>::Print() const
{
    CNode<GenericDataType>* pclsCurrentNode = m_pclsHeadNode;

    cout << "Stack contents (top to bottom):" << endl;

    while (pclsCurrentNode != 0)
    {
        cout << pclsCurrentNode->GetValue() << endl;
        pclsCurrentNode = pclsCurrentNode->GetNextNode();
    }

    cout << endl;
}

#endif // CSTACK_CPP
