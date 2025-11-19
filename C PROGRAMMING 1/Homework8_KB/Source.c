// --------------------------------------------------------------------------------
// Name: Keith Brock
// Class: SET-151
// Abstract: Homework 8 - Structure Stress
// --------------------------------------------------------------------------------
#define _CRT_SECURE_NO_WARNINGS



// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include<stdio.h>
#include<stdlib.h>
#include<string.h>



// --------------------------------------------------------------------------------
// Constants
// --------------------------------------------------------------------------------
const int intARRAY_SIZE = 100;
#define TRUE 1
#define FALSE 0



// --------------------------------------------------------------------------------
// User Defined Types (UDTs)
// --------------------------------------------------------------------------------
typedef int boolean;

typedef struct 
{
    long lngRecordID;
    char strFirstName[50];
    char strMiddleName[50];
    char strLastName[50];
    char strStreet[100];
    char strCity[50];
    char strState[50];
    char strZipCode[50];
} udtAddressType;



// --------------------------------------------------------------------------------
// Prototypes
// --------------------------------------------------------------------------------
void InitializeAddressList(udtAddressType audtAddressList[]);
    void InitializeAddress(udtAddressType* pudtAddress);

void PopulateAddressList(udtAddressType audtAddressList[]);
    int OpenInputFile(char strFileName[], FILE** ppfilInput);

void PrintAddressList(udtAddressType audtAddressList[]);
    void PrintAddress(int intIndex, udtAddressType udtAddress);

void StringCopy(char strDestination[], char strSource[]);

void ParseFullName(char strFullName[], udtAddressType* pudtAddress);

void Trim(char strSource[]);

boolean IsWhiteSpace(char chrLetterToCheck);



// --------------------------------------------------------------------------------
// Name: main
// Abstract: This is where the program starts.
// --------------------------------------------------------------------------------

void main()
{
    udtAddressType audtAddressList[100];

    InitializeAddressList(audtAddressList);
    PopulateAddressList(audtAddressList);
    PrintAddressList(audtAddressList);

    system("pause");
}

// --------------------------------------------------------------------------------
// Name: InitializeAddressList
// Abstract: Initialize all the addresses in the list
// --------------------------------------------------------------------------------
void InitializeAddressList(udtAddressType audtAddressList[])
{
    int intIndex = 0;
    for (intIndex = 0; intIndex < intARRAY_SIZE; intIndex += 1)
    {
        // Pass a single array element by pointer
        InitializeAddress(&audtAddressList[intIndex]);
    }
}




// --------------------------------------------------------------------------------
// Name: InitializeAddress
// Abstract: Set all the values to 0 or empty strings.
// --------------------------------------------------------------------------------
void InitializeAddress(udtAddressType* pudtAddress) 
{
    // Use -> or *( ). syntax. Both are functionally equivalent
    pudtAddress->lngRecordID = 0;
    StringCopy(pudtAddress->strFirstName, "");
    StringCopy(pudtAddress->strMiddleName, "");
    StringCopy(pudtAddress->strLastName, "");
    StringCopy(pudtAddress->strStreet, "");
    StringCopy(pudtAddress->strCity, "");
    StringCopy(pudtAddress->strState, "");
    StringCopy(pudtAddress->strZipCode, "");
}



// --------------------------------------------------------------------------------
// Name: PopulateAddressList
// Abstract: Load the addresses from a file into the array.
// --------------------------------------------------------------------------------
void PopulateAddressList(udtAddressType audtAddressList[]) 
{
    // Declare a file pointer
    FILE* pfilInput = NULL;
    int intResultFlag = 0;
    char strBuffer[100];  // Buffer for temporary storage
    char chrLetter;
    int intCurrentAddress = 0;
    int charPos = 0;
    int intIndex = 0;

    // Try to open the file for reading
    intResultFlag = OpenInputFile("Addresses1.txt", &pfilInput);

    // Was the file opened?
    if (intResultFlag == TRUE) 
    {
        // Yes, read in records until end of file (EOF)
        while ((chrLetter = fgetc(pfilInput)) != EOF && intCurrentAddress < intARRAY_SIZE)
        {
            // Reset field and buffer positions for each new line
            charPos = 0;
            intIndex = 0;

            // Read and process one character at a time until a newline
            while (chrLetter != '\n' && chrLetter != EOF)
            {
                if (chrLetter == ',')
                {
                    // When a comma is encountered, finalize the current field and move to the next one
                    strBuffer[charPos] = '\0';  // Terminate the string
                    Trim(strBuffer);  // Remove leading and trailing whitespace

                    switch (intIndex) 
                    {
                    case 0:  // RecordID
                        audtAddressList[intCurrentAddress].lngRecordID = atol(strBuffer);
                        break;
                    case 1:  // Full Name (split into First, Middle, Last)
                        ParseFullName(strBuffer, &audtAddressList[intCurrentAddress]);
                        break;
                    case 2:  // Street
                        StringCopy(audtAddressList[intCurrentAddress].strStreet, strBuffer);
                        break;
                    case 3:  // City
                        StringCopy(audtAddressList[intCurrentAddress].strCity, strBuffer);
                        break;
                    case 4:  // State
                        StringCopy(audtAddressList[intCurrentAddress].strState, strBuffer);
                        break;
                    case 5:  // ZipCode
                        StringCopy(audtAddressList[intCurrentAddress].strZipCode, strBuffer);
                        break;
                    }
                    intIndex += 1;
                    charPos = 0;  // Reset buffer for the next field
                }
                else 
                {
                    // Store the character in the buffer if it's not a comma
                    strBuffer[charPos++] = chrLetter;
                }

                // Read the next character
                chrLetter = fgetc(pfilInput);
            }

            // Handle the last field (ZipCode) when newline is reached
            strBuffer[charPos] = '\0';
            Trim(strBuffer);  // Remove leading and trailing whitespace
            StringCopy(audtAddressList[intCurrentAddress].strZipCode, strBuffer);

            // Move to the next address
            intCurrentAddress += 1;
        }

        // Clean up
        fclose(pfilInput);
    }
}




// --------------------------------------------------------------------------------
// Name: OpenInputFile
// Abstract: Open the file for reading. Return true if successful.
// --------------------------------------------------------------------------------
int OpenInputFile(char strFileName[], FILE** ppfilInput)
{
    int intResultFlag = FALSE;

    // Open the file for reading
    *ppfilInput = fopen(strFileName, "rb");

    // Success?
    if (*ppfilInput != NULL) 
    {
        intResultFlag = TRUE;
    }
    else 
    {
        printf("Error opening %s for reading!!!\n", strFileName);
    }

    return intResultFlag;
}



// --------------------------------------------------------------------------------
// Name: PrintAddressList
// Abstract: Print all the addresses
// --------------------------------------------------------------------------------
void PrintAddressList(udtAddressType audtAddressList[])
{
    int intIndex = 0;
    printf("------------------------------------------------------------------------");

    for (intIndex = 0; intIndex < intARRAY_SIZE; intIndex += 1) 
    {
        if (audtAddressList[intIndex].lngRecordID != 0) {
            // Pass a single array element
            PrintAddress(intIndex, audtAddressList[intIndex]);
            printf("------------------------------------------------------------------------");
        }
    }
    printf("\n");
}



// --------------------------------------------------------------------------------
// Name: PrintAddress
// Abstract: Print all the structure field values
// --------------------------------------------------------------------------------
void PrintAddress(int intIndex, udtAddressType udtAddress) 
{
    printf("\n");
    //printf("Address #%2d ----------------------------------------\n", intIndex + 1);
    printf("\tRecord ID      : %ld\n", udtAddress.lngRecordID);
    printf("\tFirst Name     : %s\n", udtAddress.strFirstName);
    printf("\tMiddle Name    : %s\n", udtAddress.strMiddleName);
    printf("\tLast Name      : %s\n", udtAddress.strLastName);
    printf("\tStreet Address : %s\n", udtAddress.strStreet);
    printf("\tCity           : %s\n", udtAddress.strCity);
    printf("\tState          : %s\n", udtAddress.strState);
    printf("\tZipcode        : %s\n", udtAddress.strZipCode);
}



// --------------------------------------------------------------------------------
// Name: StringCopy
// Abstract: Copy the source to the destination
// --------------------------------------------------------------------------------
void StringCopy(char strDestination[], char strSource[]) 
{
    int intIndex = 0;

    // Copy each character
    while (strSource[intIndex] != '\0') 
    {
        strDestination[intIndex] = strSource[intIndex];
        intIndex += 1;
    }

    // Terminate
    strDestination[intIndex] = '\0';
}



// --------------------------------------------------------------------------------
// Name: ParseFullName
// Abstract: Makes Full Name
// --------------------------------------------------------------------------------
void ParseFullName(char strFullName[], udtAddressType* pudtAddress) 
{
    int charPos = 0, wordNum = 0, wordPos = 0;
    char strWord[50];

    // Initialize all name fields to empty
    StringCopy(pudtAddress->strFirstName, "");
    StringCopy(pudtAddress->strMiddleName, "");
    StringCopy(pudtAddress->strLastName, "");

    // Process each character in the full name string
    while (strFullName[charPos] != '\0') 
    {
        if (strFullName[charPos] == ' ') 
        {
            // When we encounter a space, end the current word and assign it
            if (wordPos > 0) {
                strWord[wordPos] = '\0';  // End the word with a null terminator

                if (wordNum == 0) 
                {
                    StringCopy(pudtAddress->strFirstName, strWord);  // First word is the first name
                }
                else if (wordNum == 1) 
                {
                    StringCopy(pudtAddress->strMiddleName, strWord);  // Second word is the middle name
                }
                else if (wordNum == 2) 
                {
                    StringCopy(pudtAddress->strLastName, strWord);  // Third word is the last name
                }

                wordNum++;
                wordPos = 0;  // Reset for the next word
            }
        }
        else {
            // If not a space, keep accumulating the word
            strWord[wordPos++] = strFullName[charPos];
        }

        charPos++;
    }

    // After exiting the loop, handle the last word (since there’s no space at the end)
    if (wordPos > 0)
    {
        strWord[wordPos] = '\0';  // End the word with a null terminator

        if (wordNum == 0) 
        {
            StringCopy(pudtAddress->strFirstName, strWord);  // First word is the first name
        }
        else if (wordNum == 1) 
        {
            StringCopy(pudtAddress->strLastName, strWord);  // If two words, the second is the last name
        }
        else if (wordNum == 2) 
        {
            StringCopy(pudtAddress->strLastName, strWord);  // If three words, the third is the last name
        }
    }
}



// --------------------------------------------------------------------------------
// Name: Trim
// Abstract: Remove leading and trailing whitespace (space, tab or newline)
// --------------------------------------------------------------------------------
void Trim(char strSource[])
{

    int intIndex = 0;
    int intFirstNonWhitespaceIndex = -1;
    int intLastNonWhitespaceIndex = 0;
    int intSourceIndex = 0;
    int intDestinationIndex = 0;

    // Default first non-whitespace character index to end of string in case string is all whitespace
    // Bug fix.  Not in video.
    intFirstNonWhitespaceIndex = StringLength(strSource);

    // Find first non-whitespace character
    while (strSource[intIndex] != 0)
    {
        // Non-whitespace character?
        if (IsWhiteSpace(strSource[intIndex]) == FALSE)
        {
            // Yes, save the index
            intFirstNonWhitespaceIndex = intIndex;

            // Stop searching!
            break;
        }

        // Next character
        intIndex += 1;
    }

    // Find the last non-whitespace character
    while (strSource[intIndex] != 0)
    {
        // Non-whitespace character?
        if (IsWhiteSpace(strSource[intIndex]) == FALSE)
        {
            // Yes, save the index
            intLastNonWhitespaceIndex = intIndex;
        }

        // Next character
        intIndex += 1;
    }

    // Any non-whitepsace characters?
    if (intFirstNonWhitespaceIndex >= 0)
    {
        // Yes, copy everything in between
        for (intSourceIndex = intFirstNonWhitespaceIndex; intSourceIndex <= intLastNonWhitespaceIndex; intSourceIndex += 1)
        {
            // Copy next character
            strSource[intDestinationIndex] = strSource[intSourceIndex];

            intDestinationIndex += 1;
        }
    }

    // Terminate 
    strSource[intDestinationIndex] = 0;
}


// --------------------------------------------------------------------------------
// Name: StringLength
// Abstract: Return the string length
// --------------------------------------------------------------------------------
int StringLength(char strSource[])
{
    int intIndex = 0;
    int intLength = 0;

    // Pre-test because string may be empty
    while (strSource[intIndex] != 0)
    {
        intIndex += 1;
    }

    intLength = intIndex;

    return intLength;
}



// --------------------------------------------------------------------------------
// Name: IsWhiteSpace
// Abstract: Return true if letter is a space, tab, newline or carriage return
// --------------------------------------------------------------------------------
boolean IsWhiteSpace(char chrLetterToCheck)
{
    boolean blnIsWhiteSpace = FALSE;

    // Space
    if (chrLetterToCheck == ' ') blnIsWhiteSpace = TRUE;

    // Tab
    if (chrLetterToCheck == '\t') blnIsWhiteSpace = TRUE;

    // Carriarge return
    if (chrLetterToCheck == '\r') blnIsWhiteSpace = TRUE;

    // Line feed
    if (chrLetterToCheck == '\n') blnIsWhiteSpace = TRUE;

    return blnIsWhiteSpace;
}