// --------------------------------------------------------------------------------
// Name: CQueue.cpp
// Abstract: Implements a First-In-First-Out (FIFO) Queue using a linked list.
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include "CQueue.h"

// --------------------------------------------------------------------------------
// Name: CQueue
// Abstract: Default constructor - Initializes the queue as empty.
// --------------------------------------------------------------------------------
CQueue::CQueue()
{
    Initialize();
}


// --------------------------------------------------------------------------------
// Name: CQueue
// Abstract: Copy constructor - Performs a deep copy of an existing queue.
// --------------------------------------------------------------------------------
CQueue::CQueue(const CQueue& clsOriginalToCopy)
{
    Initialize();  // Set up an empty queue.
    DeepCopy(clsOriginalToCopy);  // Copy the original queue.
}


// --------------------------------------------------------------------------------
// Name: ~CQueue
// Abstract: Destructor - Deletes all nodes in the queue.
// --------------------------------------------------------------------------------
CQueue::~CQueue()
{
    CleanUp();
}


// --------------------------------------------------------------------------------
// Name: operator=
// Abstract: Assignment operator - Performs a deep copy of another queue.
// --------------------------------------------------------------------------------
void CQueue::operator=(const CQueue& clsOriginalToCopy)
{
    if (this != &clsOriginalToCopy) // Avoid self-assignment
    {
        CleanUp();  // Delete old contents.
        DeepCopy(clsOriginalToCopy);  // Copy the new contents.
    }
}


// --------------------------------------------------------------------------------
// Name: Initialize
// Abstract: Sets the queue to an empty state.
// --------------------------------------------------------------------------------
void CQueue::Initialize()
{
    m_pclsHeadNode = 0;
    m_pclsTailNode = 0;
}


// --------------------------------------------------------------------------------
// Name: CleanUp
// Abstract: Deletes all nodes in the queue.
// --------------------------------------------------------------------------------
void CQueue::CleanUp()
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
    m_pclsTailNode = 0;
}


// --------------------------------------------------------------------------------
// Name: DeepCopy
// Abstract: Copies all nodes from another queue.
// --------------------------------------------------------------------------------
void CQueue::DeepCopy(const CQueue& clsOriginalToCopy)
{
    if (clsOriginalToCopy.m_pclsHeadNode != 0)
    {
        // Create the first node
        m_pclsHeadNode = new CNode(clsOriginalToCopy.m_pclsHeadNode->GetValue());
        m_pclsTailNode = m_pclsHeadNode;

        CNode* pclsSourceNode = clsOriginalToCopy.m_pclsHeadNode->GetNextNode();

        // Copy remaining nodes
        while (pclsSourceNode != 0)
        {
            m_pclsTailNode->SetNextNode(new CNode(pclsSourceNode->GetValue()));
            m_pclsTailNode = m_pclsTailNode->GetNextNode();
            pclsSourceNode = pclsSourceNode->GetNextNode();
        }
    }
}


// --------------------------------------------------------------------------------
// Name: Push
// Abstract: Adds a new value to the end of the queue.
// --------------------------------------------------------------------------------
void CQueue::Push(int intValue)
{
    CNode* pclsNewNode = new CNode(intValue);

    // If queue is empty, new node is both head and tail
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


// --------------------------------------------------------------------------------
// Name: Pop
// Abstract: Removes and returns the front value of the queue.
// --------------------------------------------------------------------------------
int CQueue::Pop()
{
    int intValue = 0;
    CNode* pclsOldHead = 0;

    if (!IsEmpty())
    {
        intValue = m_pclsHeadNode->GetValue();
        pclsOldHead = m_pclsHeadNode;
        m_pclsHeadNode = m_pclsHeadNode->GetNextNode();
        delete pclsOldHead;

        // If queue is now empty, reset tail pointer
        if (m_pclsHeadNode == 0)
        {
            m_pclsTailNode = 0;
        }
    }

    return intValue;
}


// --------------------------------------------------------------------------------
// Name: IsEmpty
// Abstract: Returns true if the queue is empty.
// --------------------------------------------------------------------------------
bool CQueue::IsEmpty() const
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
// Abstract: Prints the queue without modifying it.
// --------------------------------------------------------------------------------
void CQueue::Print() const
{
    CNode* pclsCurrentNode = m_pclsHeadNode;
    cout << "Queue contents (front to back):" << endl;

    while (pclsCurrentNode != 0)
    {
        cout << pclsCurrentNode->GetValue() << " ";
        pclsCurrentNode = pclsCurrentNode->GetNextNode();
    }
    cout << endl;
}
