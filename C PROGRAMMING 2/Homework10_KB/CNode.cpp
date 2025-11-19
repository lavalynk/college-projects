// --------------------------------------------------------------------------------
// Name: CNode.cpp
// Abstract: Implements a linked list node that stores an integer and a pointer 
// to the next node. Includes constructors, assignment operator, and accessor methods.
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include "CNode.h"

// --------------------------------------------------------------------------------
// Name: CNode
// Abstract: Default constructor - initializes the node with default values.
// --------------------------------------------------------------------------------
CNode::CNode()
{
    Initialize();
}


// --------------------------------------------------------------------------------
// Name: CNode
// Abstract: Constructor - initializes the node with a given value.
// --------------------------------------------------------------------------------
CNode::CNode(int intValue)
{
    Initialize(intValue);
}


// --------------------------------------------------------------------------------
// Name: CNode
// Abstract: Constructor - initializes the node with a value and next pointer.
// --------------------------------------------------------------------------------
CNode::CNode(int intValue, CNode* pclsNextNode)
{
    Initialize(intValue, pclsNextNode);
}


// --------------------------------------------------------------------------------
// Name: CNode
// Abstract: Copy Constructor - performs a shallow copy of another node.
// --------------------------------------------------------------------------------
CNode::CNode(const CNode& clsOriginalToCopy)
{
    ShallowCopy(clsOriginalToCopy);
}


// --------------------------------------------------------------------------------
// Name: ~CNode
// Abstract: Destructor - Calls CleanUp().
// --------------------------------------------------------------------------------
CNode::~CNode()
{
    CleanUp();
}


// --------------------------------------------------------------------------------
// Name: Operator=
// Abstract: Assignment operator - performs a shallow copy of another node.
// --------------------------------------------------------------------------------
void CNode::operator=(const CNode& clsOriginalToCopy)
{
    if (this != &clsOriginalToCopy) 
    {
        CleanUp();
        ShallowCopy(clsOriginalToCopy);
    }
}


// --------------------------------------------------------------------------------
// Name: ShallowCopy
// Abstract: Copies values from the original node (shallow copy).
// --------------------------------------------------------------------------------
void CNode::ShallowCopy(const CNode& clsOriginalToCopy)
{
    m_intValue = clsOriginalToCopy.m_intValue;
    m_pclsNextNode = clsOriginalToCopy.m_pclsNextNode; 
}


// --------------------------------------------------------------------------------
// Name: Initialize
// Abstract: Sets initial values for the node.
// --------------------------------------------------------------------------------
void CNode::Initialize(int intValue, CNode* pclsNextNode)
{
    m_intValue = intValue;
    m_pclsNextNode = pclsNextNode;
}


// --------------------------------------------------------------------------------
// Name: CleanUp
// Abstract: Resets node values (currently does nothing).
// --------------------------------------------------------------------------------
void CNode::CleanUp()
{
    // No dynamic memory allocation in CNode, so nothing to clean up
}


// --------------------------------------------------------------------------------
// Name: SetValue
// Abstract: Sets the value of the node.
// --------------------------------------------------------------------------------
void CNode::SetValue(int intNewValue)
{
    m_intValue = intNewValue;
}


// --------------------------------------------------------------------------------
// Name: GetValue
// Abstract: Returns the value of the node.
// --------------------------------------------------------------------------------
int CNode::GetValue() const
{
    return m_intValue;
}


// --------------------------------------------------------------------------------
// Name: SetNextNode
// Abstract: Sets the next node pointer.
// --------------------------------------------------------------------------------
void CNode::SetNextNode(CNode* pclsNextNode)
{
    m_pclsNextNode = pclsNextNode;
}


// --------------------------------------------------------------------------------
// Name: GetNextNode
// Abstract: Returns the next node pointer.
// --------------------------------------------------------------------------------
CNode* CNode::GetNextNode() const
{
    return m_pclsNextNode;
}