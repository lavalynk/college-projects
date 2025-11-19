// --------------------------------------------------------------------------------
// Name: Keith Brock
// Class: C Programming 2
// Abstract: Tests all major methods in CNode, CStack, and CQueue. 
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include "CStack.h"
#include "CQueue.h"

// --------------------------------------------------------------------------------
// Function Prototypes
// --------------------------------------------------------------------------------
void TestCNode();
void TestStack();
void TestQueue();
void PassStackByValue(CStack clsStack);
void PassQueueByValue(CQueue clsQueue);

// --------------------------------------------------------------------------------
// Name: main
// Abstract: Calls all test functions.
// --------------------------------------------------------------------------------
int main()
{
    cout << "--------------------------------------------------" << endl;
    cout << " Testing CNode, CStack, and CQueue " << endl;
    cout << "--------------------------------------------------" << endl << endl;

    TestCNode();
    cout << endl << "--------------------------------------------------" << endl << endl;
    TestStack();
    cout << endl << "--------------------------------------------------" << endl << endl;
    TestQueue();

    return 0;
}



// --------------------------------------------------------------------------------
// Name: TestCNode
// Abstract: Tests basic functionality of CNode, including SetValue and GetValue.
// --------------------------------------------------------------------------------
void TestCNode()
{
    cout << "--------------------------------------------------" << endl;
    cout << " TESTING CNode " << endl;
    cout << "--------------------------------------------------" << endl;

    // Create a node with initial value 10.
    CNode clsNode(10);
    cout << "Created node with value: " << clsNode.GetValue() << endl;

    // Testing SetValue.
    clsNode.SetValue(99);
    cout << "After SetValue(99), node now has value: " << clsNode.GetValue() << endl;

    // Test SetValue again.
    clsNode.SetValue(42);
    cout << "After SetValue(42), node now has value: " << clsNode.GetValue() << endl;

    cout << "CNode Test Completed!" << endl;
}



// --------------------------------------------------------------------------------
// Name: TestStack
// Abstract: Tests all functions of CStack, including copy constructor.
// --------------------------------------------------------------------------------
void TestStack()
{
    cout << "--------------------------------------------------" << endl;
    cout << " TESTING CStack (LIFO Stack) " << endl;
    cout << "--------------------------------------------------" << endl;

    // Create an empty stack.
    CStack clsStack;

    // Test IsEmpty() before pushing values.
    cout << "Checking if stack is empty before pushing values: ";
    cout << (clsStack.IsEmpty() ? "Yes (Stack is empty)" : "No (Stack is NOT empty)") << endl;

    // Test Push().
    cout << "Pushing values onto stack: 10, 20, 30" << endl;
    clsStack.Push(10);
    clsStack.Push(20);
    clsStack.Push(30);
    clsStack.Print();

    // Test IsEmpty() after pushing values.
    cout << "Checking if stack is empty after pushing values: ";
    cout << (clsStack.IsEmpty() ? "Yes (Stack is empty)" : "No (Stack is NOT empty)") << endl;

    // Test Pop.
    cout << "Popping top value from stack..." << endl;
    cout << "Popped: " << clsStack.Pop() << endl;
    clsStack.Print();

    // Test Copy Constructor.
    cout << "Testing Copy Constructor by passing CStack by value..." << endl;
    cout << "Original stack before passing:" << endl;
    clsStack.Print();
    PassStackByValue(clsStack);  // calls function to test Copy Constructor.
    cout << "Original stack after function call (should remain unchanged):" << endl;
    clsStack.Print();

    // Test assignment operator.
    cout << "Testing Assignment Operator..." << endl;
    CStack clsStackCopy;
    clsStackCopy = clsStack;
    cout << "Copy of Stack after assignment (should match original):" << endl;

    // Tests Print().
    clsStackCopy.Print();

    // Pop all remaining elements.
    cout << "Popping all remaining values from stack..." << endl;
    while (!clsStack.IsEmpty())
    {
        cout << "Popped: " << clsStack.Pop() << endl;
        clsStack.Print();
    }

    // Test IsEmpty() after popping all elements.
    cout << "Checking if stack is empty after popping all values: ";
    cout << (clsStack.IsEmpty() ? "Yes (Stack is empty)" : "No (Stack is NOT empty)") << endl;

    cout << "Stack Test Completed!" << endl;
}



// --------------------------------------------------------------------------------
// Name: PassStackByValue
// Abstract: Tests copy constructor by passing CStack by value and modifying it.
// --------------------------------------------------------------------------------
void PassStackByValue(CStack clsStack)
{
    cout << "Copy of Stack inside function (should match original):" << endl;
    clsStack.Print();

    // Modify the stack with Push().
    cout << "Modifying copied stack inside function by pushing 99..." << endl;
    clsStack.Push(99);
    clsStack.Print();

    cout << "(Original stack should remain unchanged after function call.)" << endl;
}



// --------------------------------------------------------------------------------
// Name: TestQueue
// Abstract: Tests all functions of CQueue, including copy constructor.
// --------------------------------------------------------------------------------
void TestQueue()
{
    cout << "--------------------------------------------------" << endl;
    cout << " TESTING CQueue (FIFO Queue) " << endl;
    cout << "--------------------------------------------------" << endl;

    // Create an empty queue.
    CQueue clsQueue;

    // Test IsEmpty() before pushing values.
    cout << "Checking if queue is empty before pushing values: ";
    cout << (clsQueue.IsEmpty() ? "Yes (Queue is empty)" : "No (Queue is NOT empty)") << endl;

    // Test Push.
    cout << "Pushing values into queue: 10, 20, 30" << endl;
    clsQueue.Push(10);
    clsQueue.Push(20);
    clsQueue.Push(30);
    clsQueue.Print();

    // Test IsEmpty() after pushing values.
    cout << "Checking if queue is empty after pushing values: ";
    cout << (clsQueue.IsEmpty() ? "Yes (Queue is empty)" : "No (Queue is NOT empty)") << endl;

    // Test Pop.
    cout << endl << "Popping front value from queue..." << endl;
    cout << "Popped: " << clsQueue.Pop() << endl;
    clsQueue.Print();

    // Test Copy Constructor by passing by value.
    cout << endl << "Testing Copy Constructor by passing CQueue by value..." << endl;
    cout << "Original queue before passing:" << endl;
    clsQueue.Print();
    PassQueueByValue(clsQueue);  // Calls function test for Copy Constructor.
    cout << "Original queue after function call (should remain unchanged):" << endl;
    clsQueue.Print();

    // Test assignment operator.
    cout << endl << "Testing Assignment Operator..." << endl;
    CQueue clsQueueCopy;
    clsQueueCopy = clsQueue;
    cout << "Copy of Queue after assignment (should match original):" << endl;
    clsQueueCopy.Print();

    // Pop all remaining elements.
    cout << "Popping all remaining values from queue..." << endl;
    while (!clsQueue.IsEmpty())
    {
        cout << "Popped: " << clsQueue.Pop() << endl;
        clsQueue.Print();
    }

    // Test IsEmpty() after popping all elements.
    cout << "Checking if queue is empty after popping all values: ";
    cout << (clsQueue.IsEmpty() ? "Yes" : "No") << endl;

    cout << "Queue Test Completed!" << endl;
}



// --------------------------------------------------------------------------------
// Name: PassQueueByValue
// Abstract: Tests copy constructor by passing CQueue by value and modifying it.
// --------------------------------------------------------------------------------
void PassQueueByValue(CQueue clsQueue)
{
    cout << endl << "Copy of Queue inside function (should match original):" << endl;
    clsQueue.Print();

    cout << endl << "Modifying copied queue inside function by pushing 99..." << endl;
    clsQueue.Push(99);
    clsQueue.Print();

    cout << endl << "(Original queue should remain unchanged after function call.)" << endl;
}