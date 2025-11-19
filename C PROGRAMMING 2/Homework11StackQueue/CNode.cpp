// ====================================================================================
// Author: Keith Brock
// Class: C Programming 2
// File: CNode.cpp
// Purpose: Implements a templated linked list node that holds a value of any type
//          along with a pointer to the next node in the chain.
// ====================================================================================

#ifndef CNODE_CPP
#define CNODE_CPP

// ====================================================================================
// Includes
// ====================================================================================
#include "CNode.h"

// ====================================================================================
// Function: CNode (Default Constructor)
// Summary : Initializes the node with default values (null pointer, uninitialized value).
// ====================================================================================
template <typename GenericDataType>
CNode<GenericDataType>::CNode()
{
    Initialize();
}

// ====================================================================================
// Function: CNode (Value Constructor)
// Summary : Initializes the node with a specific value and a null next pointer.
// ====================================================================================
template <typename GenericDataType>
CNode<GenericDataType>::CNode(GenericDataType gdtValue)
{
    Initialize(gdtValue);
}

// ====================================================================================
// Function: CNode (Full Constructor)
// Summary : Initializes the node with a value and pointer to the next node.
// ====================================================================================
template <typename GenericDataType>
CNode<GenericDataType>::CNode(GenericDataType gdtValue, CNode<GenericDataType>* pclsNextNode)
{
    Initialize(gdtValue, pclsNextNode);
}

// ====================================================================================
// Function: CNode (Copy Constructor)
// Summary : Performs a shallow copy of the given node.
// ====================================================================================
template <typename GenericDataType>
CNode<GenericDataType>::CNode(const CNode<GenericDataType>& clsOriginalToCopy)
{
    ShallowCopy(clsOriginalToCopy);
}

// ====================================================================================
// Function: ~CNode (Destructor)
// Summary : Handles cleanup (though not necessary here since there's no dynamic memory).
// ====================================================================================
template <typename GenericDataType>
CNode<GenericDataType>::~CNode()
{
    CleanUp();
}

// ====================================================================================
// Operator: = (Assignment Operator)
// Summary : Assigns values from another node using a shallow copy, avoiding self-assignment.
// ====================================================================================
template <typename GenericDataType>
void CNode<GenericDataType>::operator=(const CNode<GenericDataType>& clsOriginalToCopy)
{
    if (this != &clsOriginalToCopy)
    {
        CleanUp();
        ShallowCopy(clsOriginalToCopy);
    }
}

// ====================================================================================
// Function: ShallowCopy
// Summary : Copies the data and pointer from another node (no deep allocation).
// ====================================================================================
template <typename GenericDataType>
void CNode<GenericDataType>::ShallowCopy(const CNode<GenericDataType>& clsOriginalToCopy)
{
    m_gdtValue = clsOriginalToCopy.m_gdtValue;
    m_pclsNextNode = clsOriginalToCopy.m_pclsNextNode;
}

// ====================================================================================
// Function: Initialize
// Summary : Sets up node data and link pointer.
// ====================================================================================
template <typename GenericDataType>
void CNode<GenericDataType>::Initialize(GenericDataType gdtValue, CNode<GenericDataType>* pclsNextNode)
{
    m_gdtValue = gdtValue;
    m_pclsNextNode = pclsNextNode;
}

// ====================================================================================
// Function: CleanUp
// Summary : Placeholder for future cleanup needs. No memory allocation here.
// ====================================================================================
template <typename GenericDataType>
void CNode<GenericDataType>::CleanUp()
{
    // No dynamic memory used, so nothing to clean up
}

// ====================================================================================
// Function: SetValue
// Summary : Updates the value stored in the node.
// ====================================================================================
template <typename GenericDataType>
void CNode<GenericDataType>::SetValue(GenericDataType gdtNewValue)
{
    m_gdtValue = gdtNewValue;
}

// ====================================================================================
// Function: GetValue
// Summary : Retrieves the stored value from the node.
// ====================================================================================
template <typename GenericDataType>
GenericDataType CNode<GenericDataType>::GetValue() const
{
    return m_gdtValue;
}

// ====================================================================================
// Function: SetNextNode
// Summary : Updates the pointer to the next node in the list.
// ====================================================================================
template <typename GenericDataType>
void CNode<GenericDataType>::SetNextNode(CNode<GenericDataType>* pclsNextNode)
{
    m_pclsNextNode = pclsNextNode;
}

// ====================================================================================
// Function: GetNextNode
// Summary : Returns the pointer to the next node.
// ====================================================================================
template <typename GenericDataType>
CNode<GenericDataType>* CNode<GenericDataType>::GetNextNode() const
{
    return m_pclsNextNode;
}

#endif // CNODE_CPP
