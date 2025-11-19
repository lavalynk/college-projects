// --------------------------------------------------------------------------------
// File: Final.cpp
// Abstract: Executes a comprehensive suite of tests for the CSuperString class,
// covering constructors, conversions, operator overloads, and all public methods.
// --------------------------------------------------------------------------------

// Professor B - I appreciate you and your class!  Only the best!

// Sorry for all the submits... I keep changing stuff....  Forgot some hungarian notations.


// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include <iostream>
#include "CSuperString.h"
using namespace std;

// --------------------------------------------------------------------------------
// Prototypes
// --------------------------------------------------------------------------------
void VerifyConstructorCoverage();
void EvaluateConcatenationLogic();
void ExercisePlusFriendVariants();
void ToUpperCaseTests();
void ToLowerCaseTests();
void TrimLeft();
void TrimRight();
void TrimBothSides();
void Reverse();
void Left();
void Right();
void SubstringTests();
void FindCharacter();
void FindSubstringTests();
void ReplaceCharacter();
void ReplaceSubstring();
void InsertCharTests();
void InsertSubstringTests();
void SubscriptTests();
void ComparisonOperatorTests();
void ValidateConversionToTypes();
void DoubleCallTest();
void StreamOperatorTests();
void Test1();
void Test2();


// --------------------------------------------------------------------------------
// Name: main
// Abstract: Entry point for executing full CSuperString unit tests
// --------------------------------------------------------------------------------
int main()
{
    PrintBanner("CSuperString Test Suite");

    // ============================================================================
    // 1) Indexing & Comparisons 
    // ============================================================================
    PrintBanner("Segment 1: Indexing & Comparisons");
    // Confirms subscript operator [] retrieves and updates characters by index
    SubscriptTests();
    // Confirms comparison operators handle different types (char, const char*, object)
    ComparisonOperatorTests();

    // ============================================================================
    // 2) Core & Construction
    // ============================================================================
    PrintBanner("Segment 2: Core & Construction");
    // Validates constructors, default initialization, deep copying, and print formatting
    VerifyConstructorCoverage();
    // Confirms conversions from string to primitive types like bool, int, float, etc.
    ValidateConversionToTypes();

    // ============================================================================
    // 3) Edits / Mutations 
    // ============================================================================
    PrintBanner("Segment 3: Edits / Mutations");
    // Validates Replace(char, char) replaces all matching characters in the string
    ReplaceCharacter();
    // Validates Replace(substring, substring) swaps all matching substrings
    ReplaceSubstring();
    // Tests inserting a single character at various positions (beginning, middle, end)
    InsertCharTests();
    // Tests inserting substrings at various valid and out-of-bound indices
    InsertSubstringTests();

    // ============================================================================
    // 4) Streams
    // ============================================================================
    PrintBanner("Segment 4: Streams");
    // Verifies input and output of CSuperString through stream operators (cin/cout)
    StreamOperatorTests();

    // ============================================================================
    // 5) Transformations
    // ============================================================================
    PrintBanner("Segment 5: Transformations");
    // Ensures ToUpperCase returns an uppercase version without modifying the original
    ToUpperCaseTests();
    // Ensures ToLowerCase returns a lowercase version without modifying the original
    ToLowerCaseTests();
    // Tests TrimLeft removes only leading whitespace characters
    TrimLeft();
    // Tests TrimRight removes only trailing whitespace characters
    TrimRight();
    // Tests Trim removes whitespace from both the start and end of the string
    TrimBothSides();
    // Verifies Reverse returns the characters in backward order without altering original
    Reverse();
    // Tests Left() returns the first N characters from the string
    Left();
    // Tests Right() returns the last N characters from the string
    Right();
    // Tests Substring() slices a portion of the string from any valid index
    SubstringTests();

    // ============================================================================
    // 6) Building Strings (Operators) 
    // ============================================================================
    PrintBanner("Segment 6: Building Strings (Operators)");
    // Verifies += operator handles appending strings, characters, and other CSuperStrings
    EvaluateConcatenationLogic();
    // Checks friend-based + operator overloads for combining various string types
    ExercisePlusFriendVariants();

    // ============================================================================
    // 7) Queries
    // ============================================================================
    PrintBanner("Segment 7: Queries / Searching");
    // Locates individual character positions from beginning and end of the string
    FindCharacter();
    // Locates substrings and handles multiple occurrences and search starting index
    FindSubstringTests();

    // ============================================================================
    // 8) Edge / Smoke --- =)
    // ============================================================================
    PrintBanner("Segment 8: Edge & Smoke");
    // Detects any memory or result issues when calling return-value methods twice in same statement
    DoubleCallTest();
    // Executes a minimal test to ensure empty constructor works as expected
    Test1();
    // Tests complex substring replacement and confirms reassignment works correctly
    Test2();

    return 0;
}





// ========================================================================================
// Name: VerifyConstructorCoverage
// Purpose: Runs parameterized construction tests for all supported data types including
// strings, primitives, and objects, to ensure CSuperString initializes correctly.
// ========================================================================================
void VerifyConstructorCoverage()
{
    CSuperString s1;
    CSuperString s2("Professor B is the Man!");
    CSuperString s3_true(true);
    CSuperString s3_false(false);
    CSuperString s4_char('C');
    CSuperString s5_shortMin((short)-32768);
    CSuperString s6_shortMax((short)32767);
    CSuperString s7_intMin((int)-2147483648);
    CSuperString s8_intMax((int)2147483647);
    CSuperString s9_longMin((long)-2147483648L);
    CSuperString s10_longMax((long)2147483647L);
    CSuperString s11_floatMin((float)1.17549e-38f);
    CSuperString s12_floatMax((float)3.40282e+38f);
    CSuperString s13_doubleMin((double)2.22507e-308);
    CSuperString s14_doubleMax((double)1.79769e+308);
    CSuperString s15_copy(s2);

    PrintBanner("Constructor Validation Output");

    cout << "  Default Constructor         : " << s1.ToString() << endl;
    cout << "  const char*                 : " << s2.ToString() << endl;
    cout << "  bool (true)                 : " << s3_true.ToString() << endl;
    cout << "  bool (false)                : " << s3_false.ToString() << endl;
    cout << "  char                        : " << s4_char.ToString() << endl;
    cout << "  short (min)                 : " << s5_shortMin.ToString() << endl;
    cout << "  short (max)                 : " << s6_shortMax.ToString() << endl;
    cout << "  int (min)                   : " << s7_intMin.ToString() << endl;
    cout << "  int (max)                   : " << s8_intMax.ToString() << endl;
    cout << "  long (min)                  : " << s9_longMin.ToString() << endl;
    cout << "  long (max)                  : " << s10_longMax.ToString() << endl;
    cout << "  float (min)                 : " << s11_floatMin.ToString() << endl;
    cout << "  float (max)                 : " << s12_floatMax.ToString() << endl;
    cout << "  double (min)                : " << s13_doubleMin.ToString() << endl;
    cout << "  double (max)                : " << s14_doubleMax.ToString() << endl;
    cout << "  Copy Constructor            : " << s15_copy.ToString() << endl;
    cout << endl << endl;
}



// ========================================================================================
// Name: ValidateConversionToTypes
// Purpose: Confirms accurate conversion from CSuperString to primitive data types,
//          including bool, short, int, long, float, and double using representative values.
// ========================================================================================
void ValidateConversionToTypes()
{
    PrintBanner("Type Conversion Tests");

    // --------------------------------------------------------------------------------
    // Boolean Conversion
    // --------------------------------------------------------------------------------
    CSuperString blnYes("true");
    CSuperString blnNo("false");
    cout << " Boolean: \"true\"                                 = " << (blnYes.ToBoolean() ? "true" : "false") << endl;
    cout << " Boolean: \"false\"                                = " << (blnNo.ToBoolean() ? "true" : "false") << endl;

    // --------------------------------------------------------------------------------
    // Short Conversion
    // --------------------------------------------------------------------------------
    CSuperString strShortLow("-12345");
    CSuperString strShortHigh("31000");
    cout << " Short conversion (\"-12345\")                     = " << strShortLow.ToShort() << endl;
    cout << " Short conversion (\"31000\")                      = " << strShortHigh.ToShort() << endl;

    // --------------------------------------------------------------------------------
    // Integer Conversion
    // --------------------------------------------------------------------------------
    CSuperString strIntSmall("-987654");
    CSuperString strIntLarge("1500000000");
    cout << " Integer conversion (\"-987654\")                  = " << strIntSmall.ToInteger() << endl;
    cout << " Integer conversion (\"1500000000\")               = " << strIntLarge.ToInteger() << endl;

    // --------------------------------------------------------------------------------
    // Long Conversion
    // --------------------------------------------------------------------------------
    CSuperString strLongA("2024080501");  
    CSuperString strLongB("-100000001");
    cout << " Long conversion (\"2024080501\")                  = " << strLongA.ToLong() << endl;
    cout << " Long conversion (\"-100000001\")                  = " << strLongB.ToLong() << endl;

    // --------------------------------------------------------------------------------
    // Float Conversion
    // --------------------------------------------------------------------------------
    CSuperString strFloatNormal("9.81");
    CSuperString strFloatSmall("2.5e-30");
    cout << " Float conversion (\"9.81\")                       = " << strFloatNormal.ToFloat() << endl;
    cout << " Float conversion (\"2.5e-30\")                    = " << strFloatSmall.ToFloat() << endl;

    // --------------------------------------------------------------------------------
    // Double Conversion
    // --------------------------------------------------------------------------------
    CSuperString strDoublePrecise("123456789.987654321");
    CSuperString strDoubleMassive("9.999999999999999e+307");
    cout << " Double conversion (\"123456789.98...\")           = " << strDoublePrecise.ToDouble() << endl;
    cout << " Double conversion (\"9.999e+307\")                = " << strDoubleMassive.ToDouble() << endl;

    cout << endl;
}




// ========================================================================================
// Name: EvaluateConcatenationLogic
// Purpose: Validates += operator overload behavior using literal strings, characters,
//          and CSuperString objects, ensuring proper dynamic memory management.
// ========================================================================================
void EvaluateConcatenationLogic()
{
    PrintBanner("Concatenation Operator Tests");

    // --------------------------------------------------------------------------------
    // Test 1: Add a single character. char +=
    // --------------------------------------------------------------------------------
    CSuperString ssString1("Fart");
    cout << "[1] Initial (char)             : \"" << ssString1 << "\"" << endl;
    ssString1 += '!';
    cout << "    After += '!'               : \"" << ssString1.ToString() << "\"\n" << endl;

    // --------------------------------------------------------------------------------
    // Test 2: Merge two CSuperString objects into a combined phrase
    // --------------------------------------------------------------------------------
    CSuperString ssString2("United States");
    CSuperString ssString2Append(" of America");
    cout << "[2] Initial (CSuperString)     : \"" << ssString2 << "\"" << endl;
    cout << "    Appending                  : \"" << ssString2Append << "\"" << endl;
    ssString2 += ssString2Append;
    cout << "    After += CSuperString      : \"" << ssString2.ToString() << "\"\n" << endl;

    // --------------------------------------------------------------------------------
    // Test 3: Combine two C-style strings to form Longhorn Steakhouse. const char* +=
    // --------------------------------------------------------------------------------
    CSuperString ssString3("Longhorn");
    cout << "[3] Initial (const char*)      : \"" << ssString3 << "\"" << endl;
    ssString3 += " Steakhouse";
    cout << "    After += \" Steakhouse\"     : \"" << ssString3.ToString() << "\"\n" << endl;
}




// ========================================================================================
// Name: ExercisePlusFriendVariants
// Purpose: Verifies the behavior of friend-based operator+ overloads with different
//          parameter combinations using Spanish greetings as examples.
// ========================================================================================
void ExercisePlusFriendVariants()
{
    PrintBanner("String Addition Friend Tests");
    // --------------------------------------------------------------------------------
    // Test 1: CSuperString + CSuperString — Combine words to say "Buenos Dias"
    // --------------------------------------------------------------------------------
    CSuperString ssSuperString1("Buenos");
    CSuperString ssSuperString2(" Dias");
    cout << "[1] Buenos + Dias: CSuperString + CSuperString" << endl;
    cout << "    Left Operand   : \"" << ssSuperString1 << "\"" << endl;
    cout << "    Right Operand  : \"" << ssSuperString2 << "\"" << endl;
    CSuperString ssSuperString3 = ssSuperString1 + ssSuperString2;
    cout << "    Result         : \"" << ssSuperString3.ToString() << "\"\n" << endl;

    // --------------------------------------------------------------------------------
    // Test 2: const char* + CSuperString — Combine literal with object to say "Buenos Tardes"
    // --------------------------------------------------------------------------------
    cout << "[2] Buenos + Tardes: const char* + CSuperString" << endl;
    CSuperString ssSuperString4 = "Buenas" + CSuperString(" Tardes");
    cout << "    Result         : \"" << ssSuperString4.ToString() << "\"\n" << endl;

    // --------------------------------------------------------------------------------
    // Test 3: CSuperString + const char* — Combine object with literal to say "Buenos Noches"
    // --------------------------------------------------------------------------------
    cout << "[3] Buenos + Noches: CSuperString + const char*" << endl;
    CSuperString ssSuperString5("Buenos");
    CSuperString ssSuperString6 = ssSuperString5 + " Noches";
    cout << "    Left Operand   : \"" << ssSuperString5 << "\"" << endl;
    cout << "    Result         : \"" << ssSuperString6.ToString() << "\"\n" << endl;
}



// ========================================================================================
// Name: FindCharacter
// Purpose: Verifies FindFirstIndexOf and FindLastIndexOf for various characters,
//          including case sensitivity, nonexistent characters, and offset-based searches.
// ========================================================================================
void FindCharacter()
{
    PrintBanner("Character Search Index Tests");

    CSuperString ssSearch("Mississippi River");
    ssSearch.Print("Target String: \"Mississippi River\"");

    // --------------------------------------------------------------------------------
    // [1]: FindFirstIndexOf('s', 5) — 's' search starting further into string
    // --------------------------------------------------------------------------------
    cout << "[1]: First index of 's' starting at position 5   = "
        << ssSearch.FindFirstIndexOf('s', 5) << "\n" << endl;

    // --------------------------------------------------------------------------------
    // [2]: FindFirstIndexOf('i') — First occurrence of lowercase i
    // --------------------------------------------------------------------------------
    cout << "[2]: First index of 'i'                          = "
        << ssSearch.FindFirstIndexOf('i') << "\n" << endl;

    // --------------------------------------------------------------------------------
    // [3]: FindFirstIndexOf('z') — Character not found
    // --------------------------------------------------------------------------------
    cout << "[3]: First index of 'z' (nonexistent)            = "
        << ssSearch.FindFirstIndexOf('z') << "\n" << endl;

    // --------------------------------------------------------------------------------
    // [4]: FindFirstIndexOf('i', 7) — 'i' search after earlier matches
    // --------------------------------------------------------------------------------
    cout << "[4]: First index of 'i' starting at position 7   = "
        << ssSearch.FindFirstIndexOf('i', 7) << "\n" << endl;

    // --------------------------------------------------------------------------------
    // [5]: FindFirstIndexOf('M') — Case sensitivity test
    // --------------------------------------------------------------------------------
    cout << "[5]: First index of 'M'                          = "
        << ssSearch.FindFirstIndexOf('M') << "\n" << endl;

    // --------------------------------------------------------------------------------
    // [6]: FindLastIndexOf('s') — Last occurrence of lowercase s
    // --------------------------------------------------------------------------------
    cout << "[6]: Last index of 's'                           = "
        << ssSearch.FindLastIndexOf('s') << "\n" << endl;

    // --------------------------------------------------------------------------------
    // [7]: FindLastIndexOf('i') — Last occurrence of lowercase i
    // --------------------------------------------------------------------------------
    cout << "[7]: Last index of 'i'                           = "
        << ssSearch.FindLastIndexOf('i') << "\n" << endl;

    // --------------------------------------------------------------------------------
    // [8]: FindFirstIndexOf('s') — First match of lowercase s
    // --------------------------------------------------------------------------------
    cout << "[8]: First index of 's'                          = "
        << ssSearch.FindFirstIndexOf('s') << "\n" << endl;

    // --------------------------------------------------------------------------------
    // [9]: FindFirstIndexOf('i', 50) — Start index out of bounds
    // --------------------------------------------------------------------------------
    cout << "[9]: First index of 'i' starting at 50 (invalid) = "
        << ssSearch.FindFirstIndexOf('i', 50) << "\n" << endl;

    cout << endl << endl;
}




// ========================================================================================
// Name: FindSubstringTests
// Purpose: Validates FindFirstIndexOf and FindLastIndexOf for substring detection,
//          including repeated phrases, offset searches, and not-found cases.
// ========================================================================================
void FindSubstringTests()
{
    PrintBanner("Substring Search Index Tests");

    CSuperString ssSearch("watermelon waterfall watered down the walkway");
    ssSearch.Print("Target String: \"watermelon waterfall watered down the walkway\"");

    // --------------------------------------------------------------------------------
    // [1]: FindFirstIndexOf("water") — First 'water'
    // --------------------------------------------------------------------------------
    cout << "[1] First index of \"waterfall\"                   : "
        << ssSearch.FindFirstIndexOf("waterfall") << "\n" << endl;

    // --------------------------------------------------------------------------------
    // [2]: FindFirstIndexOf(\"water\", 10) — Skips the first 'water'
    // --------------------------------------------------------------------------------
    cout << "[2] First index of \"water\" from index 10         : "
        << ssSearch.FindFirstIndexOf("water", 10) << "\n" << endl;

    // --------------------------------------------------------------------------------
    // [3]: FindLastIndexOf(\"water\") — Final match of 'water'
    // --------------------------------------------------------------------------------
    cout << "[3] Last index of \"watered\"                      : "
        << ssSearch.FindLastIndexOf("watered") << "\n" << endl;

    // --------------------------------------------------------------------------------
    // [4]: FindFirstIndexOf(\"melon\") — Unique middle word
    // --------------------------------------------------------------------------------
    cout << "[4] First index of \"melon\"                       : "
        << ssSearch.FindFirstIndexOf("melon") << "\n" << endl;

    // --------------------------------------------------------------------------------
    // [5]: FindFirstIndexOf(\"walk\") — Search partial word match
    // --------------------------------------------------------------------------------
    cout << "[5] First index of \"dow\"                         : "
        << ssSearch.FindFirstIndexOf("dow") << "\n" << endl;

    // --------------------------------------------------------------------------------
    // [6]: FindLastIndexOf(\"walk\") — Last instance of 'walk'
    // --------------------------------------------------------------------------------
    cout << "[6] Last index of \"walk\"                         : "
        << ssSearch.FindLastIndexOf("walk") << "\n" << endl;

    // --------------------------------------------------------------------------------
    // [7]: FindFirstIndexOf(\"banana\") — Nonexistent word
    // --------------------------------------------------------------------------------
    cout << "[7] First index of \"jake\"                        : "
        << ssSearch.FindFirstIndexOf("jake") << "\n" << endl;

    // --------------------------------------------------------------------------------
    // [8]: FindFirstIndexOf(\"fall\") — Inside of 'waterfall'
    // --------------------------------------------------------------------------------
    cout << "[8] First index of \"fall\"                        : "
        << ssSearch.FindFirstIndexOf("fall") << "\n" << endl;

    cout << endl << endl;
}



// ========================================================================================
// Name: ToUpperCaseTests
// Purpose: Validates that ToUpperCase() returns a new uppercase string copy
//          and does not modify the original unless reassigned.
// ========================================================================================
void ToUpperCaseTests()
{
    PrintBanner("Uppercase Conversion Test");

    CSuperString ssQuote("one ring to rule them all");

    // [1] Preview uppercase version (temporary)
    const char* pstrUpper = ssQuote.ToUpperCase();
    cout << "[1] Shouted Preview            : \"" << pstrUpper << "\"" << endl;
    delete[] pstrUpper;

    // [2] Apply uppercase version to the object
    pstrUpper = ssQuote.ToUpperCase();
    ssQuote = pstrUpper;
    delete[] pstrUpper;

    ssQuote.Print("[2] Final After Reassignment");
    cout << endl;
}



// ========================================================================================
// Name: ToLowerCaseTests
// Purpose: Validates that ToLowerCase() returns a lowercase copy
//          and preserves the original unless reassigned.
// ========================================================================================
void ToLowerCaseTests()
{
    PrintBanner("Lowercase Conversion Test");

    CSuperString ssBattleCry("YOU SHALL NOT PASS");

    // [1] View lowercase version before applying
    const char* pstrLower = ssBattleCry.ToLowerCase();
    cout << "[1] Whispered Preview          : \"" << pstrLower << "\"" << endl;
    delete[] pstrLower;

    // [2] Store lowercase version into the object
    pstrLower = ssBattleCry.ToLowerCase();
    ssBattleCry = pstrLower;
    delete[] pstrLower;

    ssBattleCry.Print("[2] Final After Reassignment");
    cout << endl;
}


// ========================================================================================
// Name: TrimLeft
// Purpose: Verifies that TrimLeft() removes leading whitespace from the string
//          and does not modify the original unless explicitly reassigned.
// ========================================================================================
void TrimLeft()
{
    PrintBanner("TrimLeft()");
 
    CSuperString strOriginal("          One ring to rule them all.");
    CSuperString strResult;

    // Initial display of original string
    cout << "[1] String before trimming:" << endl;
    strOriginal.Print("     ORIGINAL:");

    // Get trimmed version without applying it
    const char* pstrLeftTrimmed = strOriginal.TrimLeft();
    cout << "[2] Left-trimmed (temporary result)           : \"" << pstrLeftTrimmed << "\"" << endl;

    strResult = pstrLeftTrimmed;
    delete[] pstrLeftTrimmed;

    // Check if trimming changed anything
    if (strResult != strOriginal)
    {
        cout << "[3] Trimmed result is different from original." << endl;
        strResult.Print("     AFTER TRIM:");
    }
    else
    {
        cout << "[3] No difference detected — TrimLeft may have failed!" << endl;
    }

    cout << endl << endl;
}



// ========================================================================================
// Name: TrimRight
// Purpose: Verifies that TrimRight() removes trailing whitespace from the string
//          and does not modify the original unless explicitly reassigned.
// ========================================================================================
void TrimRight()
{
    PrintBanner("TrimRight()");
    
    CSuperString strSource("Speak, friend, and enter.     ");
    CSuperString strModified;

    cout << "[1] Original text with spaces at the end:" << endl;
    strSource.Print("     BEFORE TRIM:");

    // Allocate trimmed version (but don’t assign yet)
    const char* pstrTrimmed = strSource.TrimRight();
    cout << "[2] Result from TrimRight() (temporary)       : \"" << pstrTrimmed << "\"" << endl;

    strModified = pstrTrimmed;
    delete[] pstrTrimmed;

    // Conditional comparison output
    if (strModified != strSource)
    {
        cout << "[3] Result differs from original (as expected)" << endl;
        strModified.Print("     AFTER TRIM:");
    }
    else
    {
        cout << "[3] No difference detected — TrimRight failed!" << endl;
    }

    cout << endl << endl;
}



// ========================================================================================
// Name: TrimBothSides
// Purpose: Verifies that Trim() removes whitespace from both ends of the string
//          and preserves the original unless reassigned.
// ========================================================================================
void TrimBothSides()
{
    PrintBanner("Trim() Tests");

    CSuperString strFull("   Even the smallest person can change the course of the future.   ");

    // Display the original with whitespace
    cout << "[1] Full string with padding:" << endl;
    strFull.Print("     ORIGINAL:");

    // Use Trim() but do not alter original
    const char* pszTrimmed = strFull.Trim();
    cout << "[2] Trim() output (no reassignment)            : \"" << pszTrimmed << "\"" << endl;

    // Preserve result into new CSuperString and show difference
    CSuperString strClipped(pszTrimmed);
    delete[] pszTrimmed;

    // Reassign trimmed string back to original
    strFull = strClipped;
    cout << "[3] After reassignment (spaces removed):" << endl;
    strFull.Print("     FINAL:");

    cout << endl << endl;
}




// ========================================================================================
// Name: Reverse
// Purpose: Verifies that Reverse() creates a reversed copy of a single-word string
//          and does not affect the original unless reassigned.
// ========================================================================================
void Reverse()
{
    PrintBanner("Reverse()");

    CSuperString strWizard("Gandalf");

    // Display starting string
    strWizard.Print("[1] Starting value");

    // Generate reversed version without affecting original
    const char* pstrBackwards = strWizard.Reverse();
    cout << "[2] Reverse() output (unchanged original)       : \"" << pstrBackwards << "\"" << endl;
    delete[] pstrBackwards;

    // Apply reversal back into original string
    const char* pstrAgain = strWizard.Reverse();
    strWizard = pstrAgain;
    delete[] pstrAgain;

    // Display updated string after reassignment
    strWizard.Print("[3] After applying reversed result");

    cout << endl << endl;
}



// ========================================================================================
// Name: Left
// Purpose: Validates that Left() returns the first number of characters from the string
//          without modifying the original unless reassigned.
// ========================================================================================
void Left()
{
    PrintBanner("Left(): First N Characters");

    CSuperString strLOTR("There and Back Again");

    // --------------------------------------------------------------------------------
    // [1] Extract full string using oversized length
    // --------------------------------------------------------------------------------
    const char* pstrResult1 = strLOTR.Left(100);
    cout << "[1] Left(100)                              -> \"" << pstrResult1 << "\"" << endl;
    delete[] pstrResult1;

    // --------------------------------------------------------------------------------
    // [2] Extract 8 characters
    // --------------------------------------------------------------------------------
    const char* pstrResult2 = strLOTR.Left(8);
    cout << "[2] Left(8)                                -> \"" << pstrResult2 << "\"" << endl;
    delete[] pstrResult2;

    // --------------------------------------------------------------------------------
    // [3] Extract 4 characters
    // --------------------------------------------------------------------------------
    const char* pstrResult3 = strLOTR.Left(4);
    cout << "[3] Left(4)                                -> \"" << pstrResult3 << "\"" << endl;
    delete[] pstrResult3;

    // --------------------------------------------------------------------------------
    // [4] Confirm original string was not modified
    // --------------------------------------------------------------------------------
    strLOTR.Print("[4] After all Left() calls (original unchanged)");

    cout << endl << endl;
}



// ========================================================================================
// Name: Right
// Purpose: Ensures Right() returns the last number of characters of the string,
//          and the original remains unchanged unless reassigned.
// ========================================================================================
void Right()
{
    PrintBanner("Right(): Last N Characters");

    CSuperString strLOTR("Minas Tirith The White City");

    // --------------------------------------------------------------------------------
    // [1] Get last 15 characters
    // --------------------------------------------------------------------------------
    const char* pstrResult1 = strLOTR.Right(15);
    cout << "[1] Right(15)                               -> \"" << pstrResult1 << "\"" << endl;
    delete[] pstrResult1;

    // --------------------------------------------------------------------------------
    // [2] Get last 4 characters
    // --------------------------------------------------------------------------------
    const char* pstrResult2 = strLOTR.Right(4);
    cout << "[2] Right(4)                                -> \"" << pstrResult2 << "\"" << endl;
    delete[] pstrResult2;

    // --------------------------------------------------------------------------------
    // [3] Request more than full length
    // --------------------------------------------------------------------------------
    const char* pstrResult3 = strLOTR.Right(100);
    cout << "[3] Right(100)                              -> \"" << pstrResult3 << "\" (clamped to full length)" << endl;
    delete[] pstrResult3;

    // --------------------------------------------------------------------------------
    // [4] Confirm original string unchanged
    // --------------------------------------------------------------------------------
    strLOTR.Print("[4] After all Right() calls (original unchanged)");

    cout << endl << endl;
}




// ========================================================================================
// Name: SubstringTests
// Purpose: Validates Substring() retrieves a slice of the string given a starting index
//          and length, clamps if needed, and preserves the original string.
// ========================================================================================
void SubstringTests()
{
    PrintBanner("Substring() Tests");

    CSuperString strMagic("Hogwarts School of Witchcraft and Wizardry");

    // --------------------------------------------------------------------------------
    // [1] Display original string
    // --------------------------------------------------------------------------------
    strMagic.Print("[1] Original string");

    // --------------------------------------------------------------------------------
    // [2] Extract "School" from index 9
    // --------------------------------------------------------------------------------
    const char* pstrResult1 = strMagic.Substring(9, 6);
    cout << "[2] Substring(9, 6)                          -> \"" << pstrResult1 << "\"\n" << endl;
    delete[] pstrResult1;

    // --------------------------------------------------------------------------------
    // [3] Extract "Hogwarts" from start
    // --------------------------------------------------------------------------------
    const char* pstrResult2 = strMagic.Substring(0, 8);
    cout << "[3] Substring(0, 8)                          -> \"" << pstrResult2 << "\"\n" << endl;
    delete[] pstrResult2;

    // --------------------------------------------------------------------------------
    // [4] Extract "Witchcraft" from index 19
    // --------------------------------------------------------------------------------
    const char* pstrResult3 = strMagic.Substring(19, 10);
    cout << "[4] Substring(19, 10)                        -> \"" << pstrResult3 << "\"\n" << endl;
    delete[] pstrResult3;

    // --------------------------------------------------------------------------------
    // [5] Clamp result starting at 29
    // --------------------------------------------------------------------------------
    const char* pstrResult4 = strMagic.Substring(29, 13);
    cout << "[5] Substring(29, 13)                        -> \"" << pstrResult4 << "\" (clamped)\n" << endl;
    delete[] pstrResult4;

    // --------------------------------------------------------------------------------
    // [6] Out-of-range starting index
    // --------------------------------------------------------------------------------
    const char* pstrResult5 = strMagic.Substring(99, 5);
    cout << "[6] Substring(99, 5)                         -> \"" << pstrResult5 << "\" (invalid start)\n" << endl;
    delete[] pstrResult5;

    // --------------------------------------------------------------------------------
    // [7] Confirm original is unchanged
    // --------------------------------------------------------------------------------
    strMagic.Print("[7] After all Substring() calls (original unchanged)");

    cout << endl << endl;
}



// --------------------------------------------------------------------------------
// Name: ReplaceCharacter
// Abstract: Tests Replace(char, char) replaces all matching characters.
// --------------------------------------------------------------------------------
void ReplaceCharacter()
{
    PrintBanner("Replace Character");

    CSuperString strMagic("All we have to decide is what to do with the time that is given us.");

    // --------------------------------------------------------------------------------
    // [1] Print the original string
    // --------------------------------------------------------------------------------
    strMagic.Print("[1] Original string");

    // --------------------------------------------------------------------------------
    // [2] Replace 'o' with '%'
    // --------------------------------------------------------------------------------
    cout << "[2] Replace 'o' with '%'" << endl;
    const char* pstrResult1 = strMagic.Replace('o', '%');
    cout << "    Result: [" << pstrResult1 << "]\n" << endl;
    delete[] pstrResult1;

    // --------------------------------------------------------------------------------
    // [3] Replace 'x' with '*' (not found)
    // --------------------------------------------------------------------------------
    cout << "[3] Replace 'x' with '*' (no characters replaced)" << endl;
    const char* pstrResult2 = strMagic.Replace('x', '*');
    cout << "    Result: [" << pstrResult2 << "] (no change)\n" << endl;
    delete[] pstrResult2;

    // --------------------------------------------------------------------------------
    // [4] Print original string again to confirm it's unchanged
    // --------------------------------------------------------------------------------
    strMagic.Print("[4] String after Replace() calls (should be unchanged)");

    // --------------------------------------------------------------------------------
    // [5] Reassign result of Replace(' ', '_') to object
    // --------------------------------------------------------------------------------
    cout << "[5] Reassign result of Replace(' ', '_') to object" << endl;
    const char* pstrResult3 = strMagic.Replace(' ', '_');
    strMagic = pstrResult3;
    delete[] pstrResult3;
    strMagic.Print("[6] String after reassignment (spaces replaced with underscores)");

    cout << endl;
    cout << endl;
}



// --------------------------------------------------------------------------------
// Name: ReplaceSubstring
// Abstract: Tests Replace(const char*, const char*) replaces all matches.
// --------------------------------------------------------------------------------
void ReplaceSubstring()
{
    PrintBanner("Replace(const char*, const char*) Tests");

    CSuperString strMagic("Happiness can be found even in the darkest of times, if one only remembers to turn on the light.");

    // [1] Print the original string
    strMagic.Print("[1] Original string");

    // [2] Replace "darkest" with "brightest"
    cout << "[2] Replace \"darkest\" with \"brightest\"" << endl;
    const char* pstrResult1 = strMagic.Replace("darkest", "brightest");
    cout << "    Result: [" << pstrResult1 << "]\n" << endl;
    delete[] pstrResult1;

    // [3] Replace "light" with "switch"
    cout << "[3] Replace \"light\" with \"switch\"" << endl;
    const char* pstrResult2 = strMagic.Replace("light", "switch");
    cout << "    Result: [" << pstrResult2 << "]\n" << endl;
    delete[] pstrResult2;

    // [4] Replace "xyz" with "abc" (not found)
    cout << "[4] Replace \"xyz\" with \"abc\" (no matches)" << endl;
    const char* pstrResult3 = strMagic.Replace("xyz", "abc");
    cout << "    Result: [" << pstrResult3 << "]\n" << endl;
    delete[] pstrResult3;

    // [5] Print original string again to confirm it's unchanged
    strMagic.Print("[5] String after Replace() calls (should be unchanged)");

    // [6] Reassign result of Replace(" ", "_") to object
    cout << "[6] Reassign result of Replace(\" \", \"_\") to object" << endl;
    const char* pstrResult4 = strMagic.Replace(" ", "_");
    strMagic = pstrResult4;
    delete[] pstrResult4;
    strMagic.Print("[7] String after reassignment (spaces replaced with underscores)");

    cout << endl;
    cout << endl;
}



// --------------------------------------------------------------------------------
// Name: InsertCharTests
// Abstract: Tests inserting a character at various positions.
// --------------------------------------------------------------------------------
void InsertCharTests()
{
    PrintBanner("Insert(char, index) Tests");

    CSuperString strMagic("Dumbledore");

    // [1] Print the original string
    strMagic.Print("[1] Original string");

    // [2] Insert at beginning
    const char* pstrTest1 = strMagic.Insert('!', 0);
    cout << "[2] Insert '!' at index 0 (beginning)" << endl;
    cout << "    Result: [" << pstrTest1 << "]\n" << endl;
    delete[] pstrTest1;

    // [3] Insert in the middle
    const char* pstrTest2 = strMagic.Insert('@', 4);
    cout << "[3] Insert '@' at index 4 (middle)" << endl;
    cout << "    Result: [" << pstrTest2 << "]\n" << endl;
    delete[] pstrTest2;

    // [4] Insert beyond string length
    const char* pstrTest3 = strMagic.Insert('#', 100);
    cout << "[4] Insert '#' at index 100 (should clamp to end)" << endl;
    cout << "    Result: [" << pstrTest3 << "] (clamped)\n" << endl;
    delete[] pstrTest3;

    // [5] Confirm object is unchanged
    strMagic.Print("[5] String after Insert() calls (should be unchanged)");

    // [6] Reassign result of Insert('-', 3) to object
    cout << "[6] Reassign result of Insert('-', 3) to object" << endl;
    const char* pstrTest4 = strMagic.Insert('-', 3);
    strMagic = pstrTest4;
    delete[] pstrTest4;
    strMagic.Print("[7] String after reassignment (inserted hyphen at index 3)");

    cout << endl;
    cout << endl;
}


// --------------------------------------------------------------------------------
// Name: InsertSubstringTests
// Abstract: Tests inserting a substring at different locations.
// --------------------------------------------------------------------------------
void InsertSubstringTests()
{
    PrintBanner("Insert(const char*, index) Tests");

    CSuperString strHogwarts("I solemnly swear");

    // [1] Print the original string
    strHogwarts.Print("[1] Original string");

    // [2] Insert " that I am up to no good" at index 16 (end)
    const char* pstrTest1 = strHogwarts.Insert(" that I am up to no good", 16);
    cout << "[2] Insert \" that I am up to no good\" at index 16" << endl;
    cout << "    Result: [" << pstrTest1 << "]\n" << endl;
    delete[] pstrTest1;

    // [3] Insert " really" at index 2
    const char* pstrTest2 = strHogwarts.Insert(" really", 2);
    cout << "[3] Insert \" really\" at index 2" << endl;
    cout << "    Result: [" << pstrTest2 << "]\n" << endl;
    delete[] pstrTest2;

    // [4] Insert "--" at index 0
    const char* pstrTest3 = strHogwarts.Insert("--", 0);
    cout << "[4] Insert \"--\" at index 0 (beginning)" << endl;
    cout << "    Result: [" << pstrTest3 << "]\n" << endl;
    delete[] pstrTest3;

    // [5] Insert "!" at index 100 (clamp to end)
    const char* pstrTest4 = strHogwarts.Insert("!", 100);
    cout << "[5] Insert \"!\" at index 100 (should clamp to end)" << endl;
    cout << "    Result: [" << pstrTest4 << "]\n" << endl;
    delete[] pstrTest4;

    // [6] Confirm object is unchanged
    strHogwarts.Print("[6] String after Insert() calls (should be unchanged)");

    // [7] Reassign result of Insert(" not", 1) to object
    cout << "[7] Reassign result of Insert(\" not\", 1) to object" << endl;
    const char* pstrTest5 = strHogwarts.Insert(" not", 1);
    strHogwarts = pstrTest5;
    delete[] pstrTest5;
    strHogwarts.Print("[8] String after reassignment (inserted ' not' at index 1)");

    cout << endl;
    cout << endl;
}



// --------------------------------------------------------------------------------
// Name: SubscriptTests
// Abstract: Tests subscript operator [] for character access/modification.
// --------------------------------------------------------------------------------
void SubscriptTests()
{
    PrintBanner("Subscript Operator Tests ");

    CSuperString strBuffer("Azkaban");

    // [1] Initial state
    strBuffer.Print("[1] Original string");

    // [2] Read characters by index
    cout << "[2] Read characters using subscript operator" << endl;
    cout << "    strBuffer[0]      -> " << strBuffer[0] << endl;
    cout << "    strBuffer[3]      -> " << strBuffer[3] << endl;
    cout << "    strBuffer[100]    -> " << strBuffer[100] << " (clipped to last)\n" << endl;

    // [3] Modify characters using subscript operator
    cout << "[3] Modify index 0 to 'R'" << endl;
    strBuffer[0] = 'R';
    strBuffer.Print("After modifying index 0 to 'R'");

    cout << "[4] Modify index 99 to 'X' (out-of-bounds, should affect last char)" << endl;
    strBuffer[99] = 'X';
    strBuffer.Print("After modifying index 99 to 'X' (clipped to end)");

    cout << endl;
    cout << endl;
}




// --------------------------------------------------------------------------------
// Name: StreamOperatorTests
// Abstract: Tests << and >> operators for input/output of CSuperString.
// --------------------------------------------------------------------------------
void StreamOperatorTests()
{
    PrintBanner("Stream Operator Tests (<< and >>)");

    CSuperString strStream;

    // [1] Input using >> operator
    cout << "[1] Input Test - Enter a magical keyword (e.g., Alohomora or any other word...): ";
    cin >> strStream;

    // [2] Output using << operator
    cout << "[2] Output Test - You entered: " << strStream << endl;

    cout << endl;
    cout << endl;
}



// --------------------------------------------------------------------------------
// Name: ComparisonOperatorTests
// Abstract: Tests ==, !=, <, <=, >, >= operators with all supported types.
// --------------------------------------------------------------------------------
void ComparisonOperatorTests()
{
    PrintBanner("Comparison Operator Tests");

    CSuperString ssOne("Hogwarts");
    CSuperString ssTwo("Azkaban");
    CSuperString ssThree("Hogwarts");

    // --------------------------------------------------------------------------------
    // [1] Show initial values
    // --------------------------------------------------------------------------------
    cout << "[1] Initial Values:" << endl;
    cout << "    ssOne: " << ssOne << endl;
    cout << "    ssTwo: " << ssTwo << endl;
    cout << "    ssThree: " << ssThree << "\n" << endl;

    // --------------------------------------------------------------------------------
    // [2] Comparisons between CSuperString objects
    // --------------------------------------------------------------------------------
    cout << "[2] Object vs Object Comparisons:" << endl;
    cout << "    ssOne == ssThree       - " << (ssOne == ssThree) << endl;
    cout << "    ssOne != ssTwo         - " << (ssOne != ssTwo) << endl;
    cout << "    ssOne <  ssTwo         - " << (ssOne < ssTwo) << endl;
    cout << "    ssTwo >  ssThree       - " << (ssTwo > ssThree) << endl;
    cout << "    ssOne <= ssThree       - " << (ssOne <= ssThree) << endl;
    cout << "    ssTwo >= ssOne         - " << (ssTwo >= ssOne) << "\n" << endl;

    // --------------------------------------------------------------------------------
    // [3] Comparisons with const char*
    // --------------------------------------------------------------------------------
    cout << "[3] Object vs const char* Comparisons:" << endl;
    cout << "    ssOne == \"Hogwarts\"     - " << (ssOne == "Hogwarts") << endl;
    cout << "    ssOne != \"Durmstrang\"   - " << (ssOne != "Durmstrang") << "\n" << endl;

    // --------------------------------------------------------------------------------
    // [4] Comparisons with char
    // --------------------------------------------------------------------------------
    cout << "[4] Object vs char Comparisons:" << endl;
    cout << "    ssOne == 'H'         - " << (ssOne == 'H') << endl;
    cout << "    ssTwo <  'Z'         - " << (ssTwo < 'Z') << endl;

    cout << endl;
    cout << endl;
}



// --------------------------------------------------------------------------------
// Name: DoubleCallTest
// Abstract: Tests double use of method return values on same line (no memory leak).
// --------------------------------------------------------------------------------
void DoubleCallTest()
{
    PrintBanner("Double Call Test: Left(x) Evaluation");

    CSuperString ssTest = "Expelliarmus";

    cout << "Original string: " << ssTest << endl;

    // Perform two calls in same line — test allocation/deallocation
    const char* pstrLeft3 = ssTest.Left(3);
    const char* pstrLeft6 = ssTest.Left(6);
    printf("Left(3): %s, Left(6): %s\n", pstrLeft3, pstrLeft6);
    delete[] pstrLeft3;
    delete[] pstrLeft6;

    cout << endl;
    cout << endl;
}



// --------------------------------------------------------------------------------
// Name: Final Tests
// --------------------------------------------------------------------------------

void Test1()
{
    PrintBanner("Final Boss: Test #1");

    CSuperString ssTest;
    cout << "Test #1: " << ssTest << endl;

    cout << endl;
    cout << endl;
}

void Test2()
{
    PrintBanner("Final Boss: Test #2");

    CSuperString ssTest = "I Love Star Wars and I Love Star Trek";
    const char* pstrResult = ssTest.Replace("Love", "Really Love Love");
    ssTest = pstrResult;
    delete[] pstrResult;
    cout << "Test #2: " << ssTest << endl;

    cout << endl;
    cout << endl;
}
#pragma endregion