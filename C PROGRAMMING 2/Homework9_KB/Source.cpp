// --------------------------------------------------------------------------------
// Abstract: Homework9 - Linked Lists - Reads numbers from a file and dynamically
// stores them in a list, prints those results, and deletes/clears that memory.
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// Includes – built-in libraries of functions
// --------------------------------------------------------------------------------
#include <iostream>
#include <fstream>
#include <time.h>
using namespace std;

// --------------------------------------------------------------------------------
// User Defined Types (UDT)
// --------------------------------------------------------------------------------
typedef struct udtNode
{
    int intValue;
    udtNode* pudtNextNode;
} udtNodeType;

// --------------------------------------------------------------------------------
// Prototypes
// --------------------------------------------------------------------------------
char* GetFileNameFromUser();
bool OpenFile(char* pstrFileName, ifstream& ifsDataFile);
udtNodeType* LoadNumbersIntoList(char* pstrFileName);
void PrintLinkedList(udtNodeType* pudtHeadNode);
void DeleteLinkedList(udtNodeType*& pudtHeadNode);


// --------------------------------------------------------------------------------
// Name: main
// Abstract: This is where the program starts operations.
// --------------------------------------------------------------------------------
int main()
{
    bool blnResult = false;
    ifstream ifsDataFile;
    char* pstrFileName = 0;
    udtNodeType* pudtHeadNode = 0;

    // Prompt user for file name
    pstrFileName = GetFileNameFromUser();

    // Try to open the file
    blnResult = OpenFile(pstrFileName, ifsDataFile);

    if (blnResult == true)
    {
        cout << "File opened successfully: " << pstrFileName << endl;

        pudtHeadNode = LoadNumbersIntoList(pstrFileName);
    }

    // Free dynamically allocated memory
    delete[] pstrFileName;
    pstrFileName = 0;

    // Print the linked list if it contains values
    if (pudtHeadNode != 0)
    {
        PrintLinkedList(pudtHeadNode);
    }

    // Delete the linked list (timing inside function)
    DeleteLinkedList(pudtHeadNode);

    // Makes sure the head pointer is set to 0
    cout << "After delete, head node pointer address: " << pudtHeadNode << endl;

    system("pause");

    return 0;
}



// --------------------------------------------------------------------------------
// Name: GetFileNameFromUser
// Abstract: Prompts the user for a file name and returns a dynamically allocated string.
// --------------------------------------------------------------------------------
char* GetFileNameFromUser()
{
    char strFileName[255] = ""; // Temporary storage

    // Prompt the user for input
    cout << "Enter the whole file name: ";
    cin >> strFileName;

    // Determine the length of the input string
    int intLength = 0;
    while (strFileName[intLength] != '\0')
    {
        intLength++;
    }

    // Allocate memory for the file name (+1 for null terminator)
    char* pstrFileName = new char[intLength + 1];

    // Copy the file name from the temporary array to dynamically allocated memory
    for (int i = 0; i <= intLength; i++)  // Include null terminator
    {
        pstrFileName[i] = strFileName[i];
    }

    return pstrFileName; // Return the dynamically allocated file name
}



// --------------------------------------------------------------------------------
// Name: OpenFile
// Abstract: Opens the specified file and returns a boolean indicating success.
// --------------------------------------------------------------------------------
bool OpenFile(char* pstrFileName, ifstream& ifsDataFile)
{
    bool blnResult = false;

    // Attempt to open the file for reading
    ifsDataFile.open(pstrFileName, ios::in);

    // Check if the file was opened successfully
    if (ifsDataFile.is_open() == true)
    {
        blnResult = true; // File opened successfully
    }
    else
    {
        cout << "Error opening the file: " << pstrFileName << endl;
    }

    return blnResult;
}



// --------------------------------------------------------------------------------
// Name: LoadNumbersIntoList
// Abstract: Reads numbers from a file and adds them to a linked list.
// --------------------------------------------------------------------------------
udtNodeType* LoadNumbersIntoList(char* pstrFileName)
{
    ifstream ifsDataFile;
    int intValue = 0;
    udtNodeType* pudtHeadNode = 0;
    udtNodeType* pudtCurrentNode = 0;

    // Open the file for reading
    ifsDataFile.open(pstrFileName, ios::in);

    if (!ifsDataFile.is_open())
    {
        cout << "Error opening the file: " << pstrFileName << endl;
        return 0;
    }

    // Read numbers from the file and add them to the linked list
    while (ifsDataFile >> intValue)
    {
        // Create a new node
        pudtCurrentNode = new udtNodeType;
        pudtCurrentNode->intValue = intValue;

        // Insert new node at the beginning of the list
        pudtCurrentNode->pudtNextNode = pudtHeadNode;
        pudtHeadNode = pudtCurrentNode;
    }

    ifsDataFile.close();
    return pudtHeadNode; // Return the head of the linked list
}




// --------------------------------------------------------------------------------
// Name: PrintLinkedList
// Abstract: Prints all values stored in the linked list.
// --------------------------------------------------------------------------------
void PrintLinkedList(udtNodeType* pudtHeadNode)
{
    udtNodeType* pudtCurrentNode = pudtHeadNode; // Start at the head
    int intIndex = 0;

    // Traverse the list and print values
    while (pudtCurrentNode != 0)
    {
        intIndex += 1; // Keep track of node index
        cout << "Value at node #" << intIndex << " is " << pudtCurrentNode->intValue << endl;

        // Move to the next node
        pudtCurrentNode = pudtCurrentNode->pudtNextNode;
    }
}



// --------------------------------------------------------------------------------
// Name: DeleteLinkedList
// Abstract: Deletes all nodes in the linked list and sets head pointer to 0.
// --------------------------------------------------------------------------------
void DeleteLinkedList(udtNodeType*& pudtHeadNode)
{
    udtNodeType* pudtNextNode = 0;

    // Start timing deletion
    clock_t clkStart = clock();

    while (pudtHeadNode != 0)
    {
        // Store pointer to next node before deleting current node
        pudtNextNode = pudtHeadNode->pudtNextNode;

        // Delete the current node
        delete pudtHeadNode;

        // Move to the next node
        pudtHeadNode = pudtNextNode;
    }

    // Stop timing
    clock_t clkStop = clock();

    // Display elapsed time for deleting the linked list
    cout << "Time it takes to delete the linked list: "
        << float(clkStop - clkStart) / CLOCKS_PER_SEC
        << " seconds" << endl;

    // Set head pointer to 0 to indicate the list is deleted
    pudtHeadNode = 0;
}