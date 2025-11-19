// ====================================================================================
// Author: Keith Brock
// Class: C Programming 2
// File: CNode.h
// Purpose: Defines a templated linked list node that stores a generic value and
//          a pointer to the next node in the sequence.
// ====================================================================================

#ifndef CNODE_H
#define CNODE_H

// ====================================================================================
// Includes
// ====================================================================================
#include <iostream>
using namespace std;

// ====================================================================================
// Template Class: CNode
// Summary       : Generic node for use in singly linked data structures.
// ====================================================================================
template <typename GenericDataType>
class CNode
{
    // ====================================================================================
    // Public Interface - Methods and Members accessible from outside
    // ====================================================================================
public:
    // -----------------------------
    // Constructors and Destructor
    // -----------------------------
    CNode();                                                                 // Default constructor
    CNode(GenericDataType gdtValue);                                         // Value constructor
    CNode(GenericDataType gdtValue, CNode<GenericDataType>* pclsNextNode);  // Full constructor
    CNode(const CNode<GenericDataType>& clsOriginalToCopy);                 // Copy constructor
    ~CNode();                                                                // Destructor

    // -----------------------------
    // Assignment Operator
    // -----------------------------
    void operator=(const CNode<GenericDataType>& clsOriginalToCopy);        // Overloaded =

    // -----------------------------
    // Getters and Setters
    // -----------------------------
    void SetValue(GenericDataType gdtNewValue);                              // Set node value
    GenericDataType GetValue() const;                                        // Get node value

    void SetNextNode(CNode<GenericDataType>* pclsNextNode);                 // Link to next node
    CNode<GenericDataType>* GetNextNode() const;                            // Access next node

    // ====================================================================================
    // Protected Members - Internal Utility Methods
    // ====================================================================================
protected:
    void Initialize(GenericDataType gdtValue = GenericDataType(), CNode<GenericDataType>* pclsNextNode = 0);
    void CleanUp();                                                          // Clean internal state
    void ShallowCopy(const CNode<GenericDataType>& clsOriginalToCopy);      // Copy contents

    // ====================================================================================
    // Private Members - Core Data
    // ====================================================================================
private:
    GenericDataType m_gdtValue;              // Stored value (templated type)
    CNode* m_pclsNextNode;                   // Pointer to next node in list
};

// Include implementation
#include "CNode.cpp"

#endif // CNODE_H
