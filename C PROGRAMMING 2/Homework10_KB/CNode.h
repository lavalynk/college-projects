// --------------------------------------------------------------------------------
// Name: CNode.h
// Abstract: Implements a linked list node that stores an integer and a pointer 
// to the next node. Includes constructors, assignment operator, and accessor methods.
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Pre-compiler Directives
// --------------------------------------------------------------------------------
#pragma once

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include <iostream>
using namespace std;

class CNode
{
    // --------------------------------------------------------------------------------
    // Properties
    // --------------------------------------------------------------------------------
public:

protected:

private:
    int m_intValue;        // Holds the value of the node
    CNode* m_pclsNextNode; // Pointer to the next node

    // --------------------------------------------------------------------------------
    // Methods
    // --------------------------------------------------------------------------------
public:
    // Constructors
    CNode();
    CNode(int intValue);
    CNode(int intValue, CNode* pclsNextNode);
    CNode(const CNode& clsOriginalToCopy);  // Copy Constructor (Shallow Copy)

    // Destructor
    ~CNode();

    // Assignment Operator
    void operator=(const CNode& clsOriginalToCopy);  // Shallow Copy Assignment

    // Set/Get Value
    void SetValue(int intNewValue);
    int GetValue() const;

    // Set/Get Next Node
    void SetNextNode(CNode* pclsNextNode);
    CNode* GetNextNode() const;

protected:
    // Helper Methods
    void Initialize(int intValue = 0, CNode* pclsNextNode = 0);
    void CleanUp();
    void ShallowCopy(const CNode& clsOriginalToCopy);

private:

};