// ====================================================================================
// Author: Keith Brock
// Course: C Programming 2
// Purpose: Source.cpp - Validates all essential methods in CNode, CStack, and CQueue 
//          through rigorous testing with long, double, and char using templates.
// ====================================================================================

// ====================================================================================
// Header Files
// ====================================================================================
#include <iostream>
#include "CStack.h"
#include "CQueue.h"

using namespace std;

// ====================================================================================
// Function Declarations
// ====================================================================================
template <typename GenericDataType>
void PassStackByValue(CStack<GenericDataType> clsStack);

template <typename GenericDataType>
void PassQueueByValue(CQueue<GenericDataType> clsQueue);

// ====================================================================================
// Function: main
// Description: Executes full-scale validation tests for templated structures
//              using long, double, and char data types.
// ====================================================================================
int main()
{
    cout << "==========================================" << endl;
    cout << "        Validating Stack & Queue          " << endl;
    cout << "==========================================" << endl;

    // ====================================================================================
    // Series 1: Testing with 'long' data type
    // ====================================================================================
    cout << "\n========================================\n";
    cout << "Testing with long\n";
    cout << "========================================\n";

    // Node validation with long
    CNode<long> longNode(100);
    cout << "Initialized node with: " << longNode.GetValue() << endl;
    longNode.SetValue(200);
    cout << "Updated node value to: " << longNode.GetValue() << endl;

    // Stack operations with long
    CStack<long> longStack;
    cout << "\nPushing onto stack: 10, 20, 30" << endl;
    longStack.Push(10);
    longStack.Push(20);
    longStack.Push(30);
    longStack.Print();
    cout << "Popped top value: " << longStack.Pop() << endl;
    longStack.Print();

    // Copy constructor test for stack (long)
    cout << "\nTesting stack copy via value passing..." << endl;
    PassStackByValue(longStack);
    cout << "Verifying original stack integrity:" << endl;
    longStack.Print();

    // Queue operations with long
    CQueue<long> longQueue;
    cout << "\nEnqueuing values: 10, 20, 30" << endl;
    longQueue.Push(10);
    longQueue.Push(20);
    longQueue.Push(30);
    longQueue.Print();
    cout << "\nDequeued front value: " << longQueue.Pop() << endl;
    longQueue.Print();

    // Copy constructor test for queue (long)
    cout << "\nTesting queue copy via value passing..." << endl;
    PassQueueByValue(longQueue);
    cout << "Verifying original queue integrity:" << endl;
    longQueue.Print();

    // ====================================================================================
    // Series 2: Testing with 'double' data type
    // ====================================================================================
    cout << "\n========================================\n";
    cout << "Testing with double\n";
    cout << "========================================\n";

    CNode<double> doubleNode(10.5);
    cout << "Initialized node with: " << doubleNode.GetValue() << endl;
    doubleNode.SetValue(20.5);
    cout << "Updated node value to: " << doubleNode.GetValue() << endl;

    CStack<double> doubleStack;
    cout << "\nPushing onto stack: 10.5, 20.5, 30.5" << endl;
    doubleStack.Push(10.5);
    doubleStack.Push(20.5);
    doubleStack.Push(30.5);
    doubleStack.Print();
    cout << "Popped top value: " << doubleStack.Pop() << endl;
    doubleStack.Print();

    cout << "\nTesting stack copy via value passing..." << endl;
    PassStackByValue(doubleStack);
    cout << "Verifying original stack integrity:" << endl;
    doubleStack.Print();

    CQueue<double> doubleQueue;
    cout << "\nEnqueuing values: 10.5, 20.5, 30.5" << endl;
    doubleQueue.Push(10.5);
    doubleQueue.Push(20.5);
    doubleQueue.Push(30.5);
    doubleQueue.Print();
    cout << "\nDequeued front value: " << doubleQueue.Pop() << endl;
    doubleQueue.Print();

    cout << "\nTesting queue copy via value passing..." << endl;
    PassQueueByValue(doubleQueue);
    cout << "Verifying original queue integrity:" << endl;
    doubleQueue.Print();

    // ====================================================================================
    // Series 3: Testing with 'char' data type
    // ====================================================================================
    cout << "\n========================================\n";
    cout << "Testing with char\n";
    cout << "========================================\n";

    CNode<char> charNode('A');
    cout << "Initialized node with: " << charNode.GetValue() << endl;
    charNode.SetValue('B');
    cout << "Updated node value to: " << charNode.GetValue() << endl;

    CStack<char> charStack;
    cout << "\nPushing onto stack: 'X', 'Y', 'Z'" << endl;
    charStack.Push('X');
    charStack.Push('Y');
    charStack.Push('Z');
    charStack.Print();
    cout << "Popped top value: " << charStack.Pop() << endl;
    charStack.Print();

    cout << "\nTesting stack copy via value passing..." << endl;
    PassStackByValue(charStack);
    cout << "Verifying original stack integrity:" << endl;
    charStack.Print();

    CQueue<char> charQueue;
    cout << "\nEnqueuing values: 'X', 'Y', 'Z'" << endl;
    charQueue.Push('X');
    charQueue.Push('Y');
    charQueue.Push('Z');
    charQueue.Print();
    cout << "\nDequeued front value: " << charQueue.Pop() << endl;
    charQueue.Print();

    cout << "\nTesting queue copy via value passing..." << endl;
    PassQueueByValue(charQueue);
    cout << "Verifying original queue integrity:" << endl;
    charQueue.Print();

    return 0;
}

// ====================================================================================
// Function: PassStackByValue
// Intent: Confirms the copy constructor functions properly by working with a duplicate
//         of the passed stack and modifying it.
// ====================================================================================
template <typename GenericDataType>
void PassStackByValue(CStack<GenericDataType> clsStack)
{
    cout << "Inside function - copied stack contents:" << endl;
    clsStack.Print();

    cout << "\nAppending new value to copied stack: ";
    if (typeid(GenericDataType) == typeid(char))
    {
        cout << "'M'" << endl;
        clsStack.Push('M');
    }
    else if (typeid(GenericDataType) == typeid(double))
    {
        cout << "99.9" << endl;
        clsStack.Push(99.9);
    }
    else
    {
        cout << "99" << endl;
        clsStack.Push(99);
    }

    clsStack.Print();
    cout << "\n(Original stack remains unaffected outside this function.)" << endl;
}

// ====================================================================================
// Function: PassQueueByValue
// Intent: Ensures the queue copy behaves as expected when modified internally.
// ====================================================================================
template <typename GenericDataType>
void PassQueueByValue(CQueue<GenericDataType> clsQueue)
{
    cout << "Inside function - copied queue contents:" << endl;
    clsQueue.Print();

    cout << "\nAppending new value to copied queue: ";
    if (typeid(GenericDataType) == typeid(char))
    {
        cout << "'M'" << endl;
        clsQueue.Push('M');
    }
    else if (typeid(GenericDataType) == typeid(double))
    {
        cout << "99.9" << endl;
        clsQueue.Push(99.9);
    }
    else
    {
        cout << "99" << endl;
        clsQueue.Push(99);
    }

    clsQueue.Print();
    cout << "\n(Original queue remains unaffected outside this function.)" << endl;
}
