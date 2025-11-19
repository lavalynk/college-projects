// --------------------------------------------------------------------------------
// File: CSuperString.cpp
// Purpose: Provides implementation for a custom dynamic string class.
// --------------------------------------------------------------------------------

#include "CSuperString.h"
#include <cstring>
#include <iostream>

using namespace std;

// --------------------------------------------------------------------------------
// Purpose: Default constructor initializing an empty string.
// --------------------------------------------------------------------------------
CSuperString::CSuperString()
{
    Initialize("");
}

// --------------------------------------------------------------------------------
// Purpose: Constructs a CSuperString from a C-style string.
// --------------------------------------------------------------------------------
CSuperString::CSuperString(const char* pstrInput)
{
    Initialize(pstrInput);
}

// --------------------------------------------------------------------------------
// Purpose: Creates a CSuperString from a boolean, converting to "true" or "false".
// --------------------------------------------------------------------------------
CSuperString::CSuperString(const bool blnValue)
{
    Initialize("");
    *this = blnValue ? "true" : "false";
}

// --------------------------------------------------------------------------------
// Purpose: Initializes a CSuperString from a single character.
// --------------------------------------------------------------------------------
CSuperString::CSuperString(const char chrInput)
{
    char strTemp[2] = { chrInput, 0 };
    Initialize(strTemp);
}

// --------------------------------------------------------------------------------
// Purpose: Converts a short integer to a string for initialization.
// --------------------------------------------------------------------------------
CSuperString::CSuperString(const short shtValue)
{
    char strTemp[8] = "";
    sprintf_s(strTemp, "%hd", shtValue);
    Initialize(strTemp);
}

// --------------------------------------------------------------------------------
// Purpose: Converts an integer to a string for initialization.
// --------------------------------------------------------------------------------
CSuperString::CSuperString(const int intValue)
{
    char strTemp[16] = "";
    sprintf_s(strTemp, "%d", intValue);
    Initialize(strTemp);
}

// --------------------------------------------------------------------------------
// Purpose: Converts a long integer to a string for initialization.
// --------------------------------------------------------------------------------
CSuperString::CSuperString(const long lngValue)
{
    char strTemp[32] = "";
    sprintf_s(strTemp, "%ld", lngValue);
    Initialize(strTemp);
}

// --------------------------------------------------------------------------------
// Purpose: Converts a float to scientific notation string for initialization.
// --------------------------------------------------------------------------------
CSuperString::CSuperString(const float fltValue)
{
    char strTemp[64] = "";
    sprintf_s(strTemp, "%e", fltValue);
    Initialize(strTemp);
}

// --------------------------------------------------------------------------------
// Purpose: Converts a double to scientific notation string for initialization.
// --------------------------------------------------------------------------------
CSuperString::CSuperString(const double dblValue)
{
    char strTemp[256] = "";
    sprintf_s(strTemp, "%e", dblValue);
    Initialize(strTemp);
}

// --------------------------------------------------------------------------------
// Purpose: Copy constructor for deep copying another CSuperString.
// --------------------------------------------------------------------------------
CSuperString::CSuperString(const CSuperString& ssSource)
{
    Initialize(ssSource.ToString());
}

// --------------------------------------------------------------------------------
// Purpose: Destructor to free allocated memory.
// --------------------------------------------------------------------------------
CSuperString::~CSuperString()
{
    CleanUp();
}

// --------------------------------------------------------------------------------
// Purpose: Returns the length of the stored string.
// --------------------------------------------------------------------------------
long CSuperString::Length() const
{
    return m_pstrSuperString ? (long)strlen(m_pstrSuperString) : 0;
}

// --------------------------------------------------------------------------------
// Purpose: Assigns a single character to the string.
// --------------------------------------------------------------------------------
void CSuperString::operator=(const char chrValue)
{
    char strTemp[2] = { chrValue, 0 };
    *this = strTemp;
}

// --------------------------------------------------------------------------------
// Purpose: Assigns a C-style string to this CSuperString.
// --------------------------------------------------------------------------------
void CSuperString::operator=(const char* pstrInput)
{
    if (m_pstrSuperString != pstrInput)
    {
        CleanUp();
        DeepCopy(pstrInput);
    }
}

// --------------------------------------------------------------------------------
// Purpose: Assigns another CSuperString to this one with deep copy.
// --------------------------------------------------------------------------------
void CSuperString::operator=(const CSuperString& ssInput)
{
    if (this != &ssInput)
    {
        CleanUp();
        DeepCopy(ssInput.ToString());
    }
}

// --------------------------------------------------------------------------------
// Purpose: Appends a C-style string to the current string.
// --------------------------------------------------------------------------------
void CSuperString::operator+=(const char* pstrAppend)
{
    // No-op if null or empty
    if (!pstrAppend || *pstrAppend == '\0')
        return;

    // Current base (treat nullptr as empty string)
    const char* base = m_pstrSuperString ? m_pstrSuperString : "";

    // Compute lengths
    long curLen = (long)strlen(base);
    long addLen = (long)strlen(pstrAppend);

    // Check for aliasing (appending from inside current buffer)
    bool overlaps = false;
    if (m_pstrSuperString)
    {
        const char* begin = m_pstrSuperString;
        const char* end = m_pstrSuperString + curLen;
        overlaps = (pstrAppend >= begin && pstrAppend <= end);
    }

    const char* src = pstrAppend;
    char* tempCopy = NULL;
    if (overlaps)
    {
        tempCopy = CloneString(pstrAppend);
        src = tempCopy;
        addLen = (long)strlen(src); // recalc length from clone
    }

    long totalLen = curLen + addLen;
    char* pNew = new char[totalLen + 1];

    // Copy base string
    if (curLen > 0)
        strcpy_s(pNew, totalLen + 1, base);
    else
        pNew[0] = '\0';

    // Append new content
    strcat_s(pNew, totalLen + 1, src);

    // Replace old buffer
    CleanUp();
    m_pstrSuperString = pNew;

    if (tempCopy) delete[] tempCopy;
}


// --------------------------------------------------------------------------------
// Purpose: Appends a single character to the current string.
// --------------------------------------------------------------------------------
void CSuperString::operator+=(const char chrAppend)
{
    char strTemp[2] = { chrAppend, 0 };
    *this += strTemp;
}

// --------------------------------------------------------------------------------
// Purpose: Appends another CSuperString to the current string.
// --------------------------------------------------------------------------------
void CSuperString::operator+=(const CSuperString& ssAppend)
{
    *this += ssAppend.ToString();
}

// --------------------------------------------------------------------------------
// Purpose: Combines two CSuperString objects into a new CSuperString.
// --------------------------------------------------------------------------------
CSuperString operator+(const CSuperString& ssLeft, const CSuperString& ssRight)
{
    CSuperString ssResult(ssLeft.ToString());
    ssResult += ssRight;
    return ssResult;
}

// --------------------------------------------------------------------------------
// Purpose: Combines a C-style string and a CSuperString into a new CSuperString.
// --------------------------------------------------------------------------------
CSuperString operator+(const char* pstrLeft, const CSuperString& ssRight)
{
    CSuperString ssResult(pstrLeft);
    ssResult += ssRight;
    return ssResult;
}

// --------------------------------------------------------------------------------
// Purpose: Combines a CSuperString and a C-style string into a new CSuperString.
// --------------------------------------------------------------------------------
CSuperString operator+(const CSuperString& ssLeft, const char* pstrRight)
{
    CSuperString ssResult(ssLeft.ToString());
    ssResult += pstrRight;
    return ssResult;
}

// --------------------------------------------------------------------------------
// Purpose: Finds the first occurrence of a character in the string.
// --------------------------------------------------------------------------------
long CSuperString::FindFirstIndexOf(const char chrTarget)
{
    long intResult = -1; // default if not found

    if (m_pstrSuperString)
    {
        for (long intIndex = 0; intIndex < Length(); intIndex++)
        {
            if (m_pstrSuperString[intIndex] == chrTarget)
            {
                intResult = intIndex;
                break; // stop at the first match
            }
        }
    }

    return intResult;
}


// --------------------------------------------------------------------------------
// Purpose: Finds the first occurrence of a character starting from a given index.
// --------------------------------------------------------------------------------
long CSuperString::FindFirstIndexOf(const char chrTarget, long lngStart)
{
    long intResult = -1; // default to not found

    if (m_pstrSuperString)
    {
        long lngLength = Length();

        if (lngStart < 0)
            lngStart = 0;

        if (lngStart < lngLength)
        {
            for (long intIndex = lngStart; intIndex < lngLength; intIndex++)
            {
                if (m_pstrSuperString[intIndex] == chrTarget)
                {
                    intIndex = intIndex;
                    break; // stop after finding the first match
                }
            }
        }
    }

    return intResult;
}


// --------------------------------------------------------------------------------
// Purpose: Finds the last occurrence of a character in the string.
// --------------------------------------------------------------------------------
long CSuperString::FindLastIndexOf(const char chrTarget)
{
    long intResult = -1; // default "not found"

    if (m_pstrSuperString)
    {
        for (long intIndex = Length() - 1; intIndex >= 0; intIndex--)
        {
            if (m_pstrSuperString[intIndex] == chrTarget)
            {
                intResult = intIndex;
                break; // stop after finding the last occurrence
            }
        }
    }

    return intResult;
}


// --------------------------------------------------------------------------------
// Purpose: Finds the first occurrence of a substring in the string.
// --------------------------------------------------------------------------------
long CSuperString::FindFirstIndexOf(const char* pstrTarget)
{
    long intResult = -1; // default "not found"

    if (m_pstrSuperString && pstrTarget)
    {
        const char* pstrFound = strstr(m_pstrSuperString, pstrTarget);
        if (pstrFound)
        {
            intResult = (long)(pstrFound - m_pstrSuperString);
        }
    }

    return intResult;
}


// --------------------------------------------------------------------------------
// Purpose: Finds the first occurrence of a substring starting from a given index.
// --------------------------------------------------------------------------------
long CSuperString::FindFirstIndexOf(const char* pstrTarget, long lngStart)
{
    long intResult = -1; // default to not found

    if (m_pstrSuperString && pstrTarget)
    {
        long lngLength = Length();
        if (lngStart < 0) lngStart = 0;

        if (lngStart < lngLength)
        {
            const char* pstrStart = m_pstrSuperString + lngStart;
            const char* pstrFound = strstr(pstrStart, pstrTarget);
            if (pstrFound)
            {
                intResult = (long)(pstrFound - m_pstrSuperString);
            }
        }
    }

    return intResult;
}


// --------------------------------------------------------------------------------
// Purpose: Finds the last occurrence of a substring in the string.
// --------------------------------------------------------------------------------
long CSuperString::FindLastIndexOf(const char* pstrTarget)
{
    long intResult = -1; // default: not found

    if (m_pstrSuperString && pstrTarget)
    {
        const char* pstrCurrent = m_pstrSuperString;
        const char* pstrLast = nullptr;

        while ((pstrCurrent = strstr(pstrCurrent, pstrTarget)))
        {
            pstrLast = pstrCurrent;
            pstrCurrent += 1;
        }

        if (pstrLast)
        {
            intResult = (long)(pstrLast - m_pstrSuperString);
        }
    }

    return intResult;
}


// --------------------------------------------------------------------------------
// Purpose: Returns a new string with all characters converted to uppercase.
// --------------------------------------------------------------------------------
const char* CSuperString::ToUpperCase()
{
    char* pstrResult = nullptr;
    long lngLength = 0;

    if (m_pstrSuperString)
    {
        lngLength = Length();
        pstrResult = new char[lngLength + 1];

        for (long intIndex = 0; intIndex < lngLength; intIndex++)
        {
            pstrResult[intIndex] = (char)toupper((unsigned char)m_pstrSuperString[intIndex]);
        }

        pstrResult[lngLength] = '\0';
    }

    return pstrResult; 
}


// --------------------------------------------------------------------------------
// Purpose: Returns a new string with all characters converted to lowercase.
// --------------------------------------------------------------------------------
const char* CSuperString::ToLowerCase()
{
    char* pstrResult = nullptr;
    long lngLength = 0;

    if (m_pstrSuperString)
    {
        lngLength = Length();
        pstrResult = new char[lngLength + 1];

        for (long intIndex = 0; intIndex < lngLength; intIndex++)
        {
            pstrResult[intIndex] = (char)tolower((unsigned char)m_pstrSuperString[intIndex]);
        }

        pstrResult[lngLength] = '\0';
    }

    return pstrResult; 
}


// --------------------------------------------------------------------------------
// Purpose: Returns a new string with leading whitespace removed.
// --------------------------------------------------------------------------------
const char* CSuperString::TrimLeft()
{
    char* pstrResult = nullptr;
    long lngLength = 0;

    if (m_pstrSuperString)
    {
        const char* pstrStart = m_pstrSuperString;

        // Skip leading whitespace
        while (*pstrStart && isspace((unsigned char)*pstrStart))
        {
            pstrStart++;
        }

        lngLength = (long)strlen(pstrStart);
        pstrResult = new char[lngLength + 1];
        strcpy_s(pstrResult, lngLength + 1, pstrStart);
    }

    return pstrResult;
}


// --------------------------------------------------------------------------------
// Purpose: Returns a new string with trailing whitespace removed.
// --------------------------------------------------------------------------------
const char* CSuperString::TrimRight()
{
    char* pstrResult = nullptr;
    long lngNewLength = 0;

    if (m_pstrSuperString)
    {
        long lngLength = Length();
        const char* pstrEnd = m_pstrSuperString + lngLength - 1;

        // Move backwards over trailing whitespace
        while (pstrEnd >= m_pstrSuperString && isspace((unsigned char)*pstrEnd))
        {
            pstrEnd--;
        }

        lngNewLength = (long)(pstrEnd - m_pstrSuperString + 1);
        pstrResult = new char[lngNewLength + 1];
        strncpy_s(pstrResult, lngNewLength + 1, m_pstrSuperString, lngNewLength);
        pstrResult[lngNewLength] = '\0';
    }

    return pstrResult;
}


// --------------------------------------------------------------------------------
// Purpose: Returns a new string with both leading and trailing whitespace removed.
// --------------------------------------------------------------------------------
const char* CSuperString::Trim()
{
    char* pstrResult = nullptr;

    if (m_pstrSuperString)
    {
        const char* pstrStart = m_pstrSuperString;
        while (*pstrStart && isspace((unsigned char)*pstrStart))
        {
            pstrStart++;
        }

        long lngNewLength = 0;
        if (*pstrStart)
        {
            const char* pstrEnd = pstrStart + strlen(pstrStart) - 1;
            while (pstrEnd > pstrStart && isspace((unsigned char)*pstrEnd))
            {
                pstrEnd--;
            }
            lngNewLength = (long)(pstrEnd - pstrStart + 1);
        }

        pstrResult = new char[lngNewLength + 1];
        if (lngNewLength > 0)
        {
            strncpy_s(pstrResult, lngNewLength + 1, pstrStart, lngNewLength);
        }
        pstrResult[lngNewLength] = '\0';
    }

    return pstrResult;
}


// --------------------------------------------------------------------------------
// Purpose: Returns a new string with characters in reverse order.
// --------------------------------------------------------------------------------
const char* CSuperString::Reverse()
{
    char* pstrResult = nullptr;

    if (m_pstrSuperString)
    {
        long lngLength = Length();
        pstrResult = new char[lngLength + 1];

        for (long i = 0; i < lngLength; i++)
        {
            pstrResult[i] = m_pstrSuperString[lngLength - 1 - i];
        }

        pstrResult[lngLength] = '\0';
    }

    return pstrResult;
}


// --------------------------------------------------------------------------------
// Purpose: Returns a new string with the first N characters.
// --------------------------------------------------------------------------------
const char* CSuperString::Left(long lngCount)
{
    char* pstrResult = nullptr;

    if (m_pstrSuperString && lngCount > 0)
    {
        long lngLength = Length();

        if (lngCount > lngLength)
        {
            lngCount = lngLength;
        }

        pstrResult = new char[lngCount + 1];
        strncpy_s(pstrResult, lngCount + 1, m_pstrSuperString, lngCount);
        pstrResult[lngCount] = '\0';
    }

    return pstrResult;
}


// --------------------------------------------------------------------------------
// Purpose: Returns a new string with the last N characters.
// --------------------------------------------------------------------------------
const char* CSuperString::Right(long lngCount)
{
    char* pstrResult = nullptr;

    if (m_pstrSuperString && lngCount > 0)
    {
        long lngLength = Length();

        if (lngCount > lngLength)
        {
            lngCount = lngLength;
        }

        const char* pstrStart = m_pstrSuperString + (lngLength - lngCount);

        pstrResult = new char[lngCount + 1];
        strncpy_s(pstrResult, lngCount + 1, pstrStart, lngCount);
        pstrResult[lngCount] = '\0';
    }

    return pstrResult;
}


// --------------------------------------------------------------------------------
// Purpose: Returns a substring starting at the given index with specified length.
// --------------------------------------------------------------------------------
const char* CSuperString::Substring(long lngStart, long lngLength)
{
    char* pstrResult = nullptr;

    if (m_pstrSuperString && lngLength > 0)
    {
        long lngTotalLength = Length();

        if (lngStart < 0)
        {
            lngStart = 0;
        }

        if (lngStart >= lngTotalLength)
        {
            pstrResult = new char[1];
            pstrResult[0] = '\0';
        }
        else
        {
            if (lngStart + lngLength > lngTotalLength)
            {
                lngLength = lngTotalLength - lngStart;
            }

            pstrResult = new char[lngLength + 1];
            strncpy_s(pstrResult, lngLength + 1, m_pstrSuperString + lngStart, lngLength);
            pstrResult[lngLength] = '\0';
        }
    }

    return pstrResult;
}


// --------------------------------------------------------------------------------
// Purpose: Returns a new string with all occurrences of a character replaced.
// --------------------------------------------------------------------------------
const char* CSuperString::Replace(char chrFind, char chrReplace)
{
    char* pstrResult = nullptr;

    if (m_pstrSuperString)
    {
        long lngLength = Length();
        pstrResult = new char[lngLength + 1];

        for (long i = 0; i < lngLength; i++)
        {
            pstrResult[i] = (m_pstrSuperString[i] == chrFind) ? chrReplace : m_pstrSuperString[i];
        }

        pstrResult[lngLength] = '\0';
    }

    return pstrResult;
}


// --------------------------------------------------------------------------------
// Purpose: Returns a new string with all occurrences of a substring replaced.
// --------------------------------------------------------------------------------
const char* CSuperString::Replace(const char* pstrFind, const char* pstrReplace)
{
    char* pstrResult = nullptr;
    if (m_pstrSuperString && pstrFind && pstrReplace)
    {
        long lngFindLen = (long)strlen(pstrFind);
        if (lngFindLen == 0)
            return CloneString(m_pstrSuperString);

        long lngReplaceLen = (long)strlen(pstrReplace);
        long lngOccurrences = 0;
        const char* pstrCurrent = m_pstrSuperString;
        while ((pstrCurrent = strstr(pstrCurrent, pstrFind)))
        {
            lngOccurrences++;
            pstrCurrent += lngFindLen;
        }

        long lngOriginalLen = (long)strlen(m_pstrSuperString);
        long lngNewLen = lngOriginalLen + (lngReplaceLen - lngFindLen) * lngOccurrences;
        pstrResult = new char[lngNewLen + 1];
        char* pstrDest = pstrResult;
        pstrCurrent = m_pstrSuperString;

        while (*pstrCurrent)
        {
            const char* pstrMatch = strstr(pstrCurrent, pstrFind);
            if (pstrMatch == pstrCurrent)
            {
                strcpy_s(pstrDest, lngNewLen + 1 - (pstrDest - pstrResult), pstrReplace);
                pstrDest += lngReplaceLen;
                pstrCurrent += lngFindLen;
            }
            else
            {
                *pstrDest++ = *pstrCurrent++;
            }
        }
        *pstrDest = 0;
    }
    return pstrResult;
}

// --------------------------------------------------------------------------------
// Purpose: Inserts a character at the specified index, returning a new string.
// --------------------------------------------------------------------------------
const char* CSuperString::Insert(const char chrInsert, long lngIndex)
{
    char* pstrResult = nullptr;
    if (m_pstrSuperString)
    {
        long lngLength = Length();
        if (lngIndex < 0) lngIndex = 0;
        if (lngIndex > lngLength) lngIndex = lngLength;

        pstrResult = new char[lngLength + 2];
        strncpy_s(pstrResult, lngLength + 2, m_pstrSuperString, lngIndex);
        pstrResult[lngIndex] = chrInsert;
        strcpy_s(pstrResult + lngIndex + 1, lngLength + 2 - (lngIndex + 1), m_pstrSuperString + lngIndex);
        pstrResult[lngLength + 1] = 0;
    }
    return pstrResult;
}

// --------------------------------------------------------------------------------
// Purpose: Inserts a substring at the specified index, returning a new string.
// --------------------------------------------------------------------------------
const char* CSuperString::Insert(const char* pstrInsert, long lngIndex)
{
    char* pstrResult = nullptr;

    const char* base = m_pstrSuperString ? m_pstrSuperString : "";
    const char* add = (pstrInsert && *pstrInsert) ? pstrInsert : "";

    long baseLen = (long)strlen(base);
    long addLen = (long)strlen(add);

    // Clamp index
    if (lngIndex < 0) lngIndex = 0;
    if (lngIndex > baseLen) lngIndex = baseLen;

    if (addLen == 0)
    {
        // No insertion — just clone the base
        pstrResult = CloneString(base);
    }
    else
    {
        long newLen = baseLen + addLen;
        pstrResult = new char[newLen + 1];

        // Copy first part up to index
        strncpy_s(pstrResult, newLen + 1, base, lngIndex);

        // Append inserted string
        strcpy_s(pstrResult + lngIndex, newLen + 1 - lngIndex, add);

        // Append remainder of base
        strcpy_s(pstrResult + lngIndex + addLen,
            newLen + 1 - (lngIndex + addLen),
            base + lngIndex);

        pstrResult[newLen] = '\0';
    }

    return pstrResult;
}


// --------------------------------------------------------------------------------
// Purpose: Provides read/write access to a character at the given index.
// --------------------------------------------------------------------------------
char& CSuperString::operator[](long lngIndex)
{
    static char chrDummy = 0;
    if (!m_pstrSuperString || Length() == 0)
        return chrDummy;
    if (lngIndex < 0) lngIndex = 0;
    if (lngIndex >= Length()) lngIndex = Length() - 1;
    return m_pstrSuperString[lngIndex];
}

// --------------------------------------------------------------------------------
// Purpose: Provides read-only access to a character at the given index.
// --------------------------------------------------------------------------------
const char& CSuperString::operator[](long lngIndex) const
{
    static char chrDummy = 0;
    if (!m_pstrSuperString || Length() == 0)
        return chrDummy;
    if (lngIndex < 0) lngIndex = 0;
    if (lngIndex >= Length()) lngIndex = Length() - 1;
    return m_pstrSuperString[lngIndex];
}

// --------------------------------------------------------------------------------
// Purpose: Outputs the CSuperString to an output stream (e.g., cout).
// --------------------------------------------------------------------------------
ostream& operator<<(ostream& os, const CSuperString& ssOut)
{
    if (ssOut.m_pstrSuperString)
        os << ssOut.m_pstrSuperString;
    return os;
}

// --------------------------------------------------------------------------------
// Purpose: Reads a single word into the CSuperString from an input stream (e.g., cin).
// --------------------------------------------------------------------------------
istream& operator>>(istream& is, CSuperString& ssIn)
{
    char strTemp[1024] = "";
    is >> strTemp;
    ssIn = strTemp;
    return is;
}

// --------------------------------------------------------------------------------
// Purpose: Compares two CSuperString objects for equality.
// --------------------------------------------------------------------------------
bool operator==(const CSuperString& ssLeft, const CSuperString& ssRight)
{
    return strcmp(ssLeft.ToString(), ssRight.ToString()) == 0;
}

bool operator==(const CSuperString& ssLeft, const char* pstrRight)
{
    return strcmp(ssLeft.ToString(), pstrRight) == 0;
}

bool operator==(const CSuperString& ssLeft, const char chrRight)
{
    char strTemp[2] = { chrRight, 0 };
    return strcmp(ssLeft.ToString(), strTemp) == 0;
}

// --------------------------------------------------------------------------------
// Purpose: Checks if two CSuperString objects are not equal.
// --------------------------------------------------------------------------------
bool operator!=(const CSuperString& ssLeft, const CSuperString& ssRight)
{
    return !(ssLeft == ssRight);
}

bool operator!=(const CSuperString& ssLeft, const char* pstrRight)
{
    return !(ssLeft == pstrRight);
}

bool operator!=(const CSuperString& ssLeft, const char chrRight)
{
    return !(ssLeft == chrRight);
}

// --------------------------------------------------------------------------------
// Purpose: Checks if the left CSuperString is lexicographically less than the right.
// --------------------------------------------------------------------------------
bool operator<(const CSuperString& ssLeft, const CSuperString& ssRight)
{
    return strcmp(ssLeft.ToString(), ssRight.ToString()) < 0;
}

bool operator<(const CSuperString& ssLeft, const char* pstrRight)
{
    return strcmp(ssLeft.ToString(), pstrRight) < 0;
}

bool operator<(const CSuperString& ssLeft, const char chrRight)
{
    char strTemp[2] = { chrRight, 0 };
    return strcmp(ssLeft.ToString(), strTemp) < 0;
}

// --------------------------------------------------------------------------------
// Purpose: Checks if the left CSuperString is less than or equal to the right.
// --------------------------------------------------------------------------------
bool operator<=(const CSuperString& ssLeft, const CSuperString& ssRight)
{
    return strcmp(ssLeft.ToString(), ssRight.ToString()) <= 0;
}

bool operator<=(const CSuperString& ssLeft, const char* pstrRight)
{
    return strcmp(ssLeft.ToString(), pstrRight) <= 0;
}

bool operator<=(const CSuperString& ssLeft, const char chrRight)
{
    char strTemp[2] = { chrRight, 0 };
    return strcmp(ssLeft.ToString(), strTemp) <= 0;
}

// --------------------------------------------------------------------------------
// Purpose: Checks if the left CSuperString is greater than the right.
// --------------------------------------------------------------------------------
bool operator>(const CSuperString& ssLeft, const CSuperString& ssRight)
{
    return strcmp(ssLeft.ToString(), ssRight.ToString()) > 0;
}

bool operator>(const CSuperString& ssLeft, const char* pstrRight)
{
    return strcmp(ssLeft.ToString(), pstrRight) > 0;
}

bool operator>(const CSuperString& ssLeft, const char chrRight)
{
    char strTemp[2] = { chrRight, 0 };
    return strcmp(ssLeft.ToString(), strTemp) > 0;
}

// --------------------------------------------------------------------------------
// Purpose: Checks if the left CSuperString is greater than or equal to the right.
// --------------------------------------------------------------------------------
bool operator>=(const CSuperString& ssLeft, const CSuperString& ssRight)
{
    return strcmp(ssLeft.ToString(), ssRight.ToString()) >= 0;
}

bool operator>=(const CSuperString& ssLeft, const char* pstrRight)
{
    return strcmp(ssLeft.ToString(), pstrRight) >= 0;
}

bool operator>=(const CSuperString& ssLeft, const char chrRight)
{
    char strTemp[2] = { chrRight, 0 };
    return strcmp(ssLeft.ToString(), strTemp) >= 0;
}

// --------------------------------------------------------------------------------
// Purpose: Initializes the internal string to an empty state or from a source.
// --------------------------------------------------------------------------------
void CSuperString::Initialize(const char* pstrInput)
{
    m_pstrSuperString = nullptr;
    *this = pstrInput;
}

// --------------------------------------------------------------------------------
// Purpose: Performs a deep copy of a C-style string into this object.
// --------------------------------------------------------------------------------
void CSuperString::DeepCopy(const char* pstrInput)
{
    m_pstrSuperString = CloneString(pstrInput);
}

// --------------------------------------------------------------------------------
// Purpose: Creates a duplicate of a C-style string.
// --------------------------------------------------------------------------------
char* CSuperString::CloneString(const char* pstrInput)
{
    char* pstrCopy = nullptr;
    if (pstrInput)
    {
        long lngLength = (long)strlen(pstrInput);
        pstrCopy = new char[lngLength + 1];
        strcpy_s(pstrCopy, lngLength + 1, pstrInput);
    }
    else
    {
        pstrCopy = new char[1];
        pstrCopy[0] = 0;
    }
    return pstrCopy;
}

// --------------------------------------------------------------------------------
// Purpose: Frees allocated memory for the internal string.
// --------------------------------------------------------------------------------
void CSuperString::CleanUp()
{
    DeleteString(m_pstrSuperString);
}

// --------------------------------------------------------------------------------
// Purpose: Deletes a string and sets its pointer to null.
// --------------------------------------------------------------------------------
void CSuperString::DeleteString(char*& pstrInput)
{
    if (pstrInput)
    {
        delete[] pstrInput;
        pstrInput = nullptr;
    }
}

// --------------------------------------------------------------------------------
// Purpose: Returns the internal C-style string.
// --------------------------------------------------------------------------------
const char* CSuperString::ToString() const
{
    return m_pstrSuperString;
}

// --------------------------------------------------------------------------------
// Purpose: Converts the string to a boolean value ("true" or "1" returns true).
// --------------------------------------------------------------------------------
bool CSuperString::ToBoolean()
{
    bool blnResult = false;

    if (m_pstrSuperString)
    {
        if (_stricmp(m_pstrSuperString, "true") == 0 || strcmp(m_pstrSuperString, "1") == 0)
        {
            blnResult = true;
        }
    }

    return blnResult;
}


// --------------------------------------------------------------------------------
// Purpose: Converts the string to a short integer.
// --------------------------------------------------------------------------------
short CSuperString::ToShort()
{
    if (m_pstrSuperString)
    {
        return (short)strtol(m_pstrSuperString, nullptr, 10);
    }
    return 0;
}

// --------------------------------------------------------------------------------
// Purpose: Converts the string to an integer.
// --------------------------------------------------------------------------------
int CSuperString::ToInteger()
{
    if (m_pstrSuperString)
    {
        return (int)strtol(m_pstrSuperString, nullptr, 10);
    }
    return 0;
}

// --------------------------------------------------------------------------------
// Purpose: Converts the string to a long integer.
// --------------------------------------------------------------------------------
long CSuperString::ToLong()
{
    if (m_pstrSuperString)
    {
        return strtol(m_pstrSuperString, nullptr, 10);
    }
    return 0;
}

// --------------------------------------------------------------------------------
// Purpose: Converts the string to a float value.
// --------------------------------------------------------------------------------
float CSuperString::ToFloat()
{
    if (m_pstrSuperString)
    {
        return (float)strtod(m_pstrSuperString, nullptr);
    }
    return 0.0f;
}

// --------------------------------------------------------------------------------
// Purpose: Converts the string to a double value.
// --------------------------------------------------------------------------------
double CSuperString::ToDouble()
{
    if (m_pstrSuperString)
    {
        return strtod(m_pstrSuperString, nullptr);
    }
    return 0.0;
}

// --------------------------------------------------------------------------------
// Purpose: Prints the string with a caption to the console.
// --------------------------------------------------------------------------------
void CSuperString::Print(const char* pstrCaption) const
{
    cout << endl << pstrCaption << endl;
    cout << "--------------------------------------------" << endl;
    if (Length() > 0)
        cout << m_pstrSuperString << endl;
    else
        cout << "-Empty-" << endl;
    cout << endl;
}

// --------------------------------------------------------------------------------
// Purpose: Prints a simple section banner with a custom title.
// --------------------------------------------------------------------------------
void PrintBanner(const char* pstrTitle)
{
    cout << "==============================================================================\n";
    cout << pstrTitle << "\n";
    cout << "==============================================================================\n\n";
}