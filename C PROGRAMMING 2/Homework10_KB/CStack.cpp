// --------------------------------------------------------------------------------
// Name: CStack.cpp
// Abstract: Implements a Last-In-First-Out (LIFO) Stack using a linked list.
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include "CStack.h"

// --------------------------------------------------------------------------------
// Name: CStack
// Abstract: Default constructor - Initializes the stack as empty.
// --------------------------------------------------------------------------------
CStack::CStack()
{
    Initialize();
}


// --------------------------------------------------------------------------------
// Name: CStack
// Abstract: Copy constructor - Performs a deep copy of an existing stack.
// --------------------------------------------------------------------------------
CStack::CStack(const CStack& clsOriginalToCopy)
{
    Initialize();  // Set up an empty stack.
    DeepCopy(clsOriginalToCopy);  // Copy the original stack.
}


// --------------------------------------------------------------------------------
// Name: ~CStack
// Abstract: Destructor - Deletes all nodes in the stack.
// --------------------------------------------------------------------------------
CStack::~CStack()
{
    CleanUp();
}


// --------------------------------------------------------------------------------
// Name: operator=
// Abstract: Assignment operator - Performs a deep copy of another stack.
// --------------------------------------------------------------------------------
void CStack::operator=(const CStack& clsOriginalToCopy)
{
    if (this != &clsOriginalToCopy) // Avoid self-assignment
    {
        CleanUp(); // Delete old contents.
        DeepCopy(clsOriginalToCopy); // Copy the new contents.
    }
}


// --------------------------------------------------------------------------------
// Name: Initialize
// Abstract: Sets the stack to an empty state.
// --------------------------------------------------------------------------------
void CStack::Initialize()
{
    m_pclsHeadNode = 0;
}


// --------------------------------------------------------------------------------
// Name: CleanUp
// Abstract: Deletes all nodes in the stack.
// --------------------------------------------------------------------------------
void CStack::CleanUp()
{
    CNode* pclsCurrentNode = m_pclsHeadNode;
    CNode* pclsNextNode = 0;

    while (pclsCurrentNode != 0)
    {
        pclsNextNode = pclsCurrentNode->GetNextNode();
        delete pclsCurrentNode;
        pclsCurrentNode = pclsNextNode;
    }

    m_pclsHeadNode = 0;
}


// --------------------------------------------------------------------------------
// Name: DeepCopy
// Abstract: Copies all nodes from another stack.
// --------------------------------------------------------------------------------
void CStack::DeepCopy(const CStack& clsOriginalToCopy)
{
    if (clsOriginalToCopy.m_pclsHeadNode != 0)
    {
        // Create the first node
        m_pclsHeadNode = new CNode(clsOriginalToCopy.m_pclsHeadNode->GetValue());
        CNode* pclsSourceNode = clsOriginalToCopy.m_pclsHeadNode->GetNextNode();
        CNode* pclsNewNode = m_pclsHeadNode;

        // Copy remaining nodes
        while (pclsSourceNode != 0)
        {
            pclsNewNode->SetNextNode(new CNode(pclsSourceNode->GetValue()));
            pclsNewNode = pclsNewNode->GetNextNode();
            pclsSourceNode = pclsSourceNode->GetNextNode();
        }
    }
}


// --------------------------------------------------------------------------------
// Name: Push
// Abstract: Adds a new value to the top of the stack.
// --------------------------------------------------------------------------------
void CStack::Push(int intValue)
{
    CNode* pclsNewNode = new CNode(intValue, m_pclsHeadNode);
    m_pclsHeadNode = pclsNewNode;
}


// --------------------------------------------------------------------------------
// Name: Pop
// Abstract: Removes and returns the top value of the stack.
// --------------------------------------------------------------------------------
int CStack::Pop()
{
    int intValue = 0;
    CNode* pclsOldHead = 0;

    if (!IsEmpty())
    {
        intValue = m_pclsHeadNode->GetValue();
        pclsOldHead = m_pclsHeadNode;
        m_pclsHeadNode = m_pclsHeadNode->GetNextNode();
        delete pclsOldHead;
    }

    return intValue;
}


// --------------------------------------------------------------------------------
// Name: IsEmpty
// Abstract: Returns true if the queue is empty.
// --------------------------------------------------------------------------------
bool CStack::IsEmpty() const
{
    bool blnIsEmpty = false;

    // Empty?
    if (m_pclsHeadNode == 0)
    {
        // Yes
        blnIsEmpty = true;
    }

    return blnIsEmpty;
}


// --------------------------------------------------------------------------------
// Name: Print
// Abstract: Prints the stack without modifying it.
// --------------------------------------------------------------------------------
void CStack::Print() const
{
    CNode* pclsCurrentNode = m_pclsHeadNode;
    cout << "Stack contents (top to bottom):" << endl;

    while (pclsCurrentNode != 0)
    {
        cout << pclsCurrentNode->GetValue() << endl;
        pclsCurrentNode = pclsCurrentNode->GetNextNode();
    }
    cout << endl;
}