// --------------------------------------------------------------------------------
// Name: Keith Brock
// Class: SET-151
// Abstract: Final Project
// --------------------------------------------------------------------------------
#define _CRT_SECURE_NO_WARNINGS



// --------------------------------------------------------------------------------
// Includes
// --------------------------------------------------------------------------------
#include<stdio.h>
#include<stdbool.h>
#include<stdlib.h>
#include<string.h>



// --------------------------------------------------------------------------------
// Constants
// --------------------------------------------------------------------------------
const int intARRAY_SIZE = 100;
const int intPOVERTY_THRESHOLDS[] = { 12000, 18000, 25000, 30000, 40000 };
#define TRUE 1
#define FALSE 0


// --------------------------------------------------------------------------------
// User Defined Types (UDTs)
// --------------------------------------------------------------------------------

typedef struct 
{
    char strDate[11]; // Format: YYYY-MM-DD
    char strState[10]; // Ohio or Kentucky
    char strCounty[10];
    char strRace[20];
    int intHouseholdSize; // Must be > 0
    double dblYearlyIncome; // Must be > 0
} udtSurveyData;



// --------------------------------------------------------------------------------
// Prototypes
// --------------------------------------------------------------------------------
void WriteSurveyData();
void DisplayMode();
int LoadDataFromFile(udtSurveyData surveyDataArray[], int* pintTotalSurveys);
void DisplayTotalHouseholds(const udtSurveyData surveyDataArray[], int intTotalSurveys);
void DisplayTotalByCounty(const udtSurveyData surveyDataArray[], int intTotalSurveys);
void DisplayTotalByRace(const udtSurveyData surveyDataArray[], int intTotalSurveys);
void DisplayAverageIncome(const udtSurveyData surveyDataArray[], int intTotalSurveys);
void DisplayAverageIncomeByCountyAndState(const udtSurveyData surveyDataArray[], int intTotalSurveys);
void DisplayAverageIncomeByRace(const udtSurveyData surveyDataArray[], int intTotalSurveys);
void DisplayPovertyPercentage(const udtSurveyData surveyDataArray[], int intTotalSurveys);
void DisplayPovertyPercentageByCountyAndState(const udtSurveyData surveyDataArray[], int intTotalSurveys);
void DisplayPovertyPercentageByRace(const udtSurveyData surveyDataArray[], int intTotalSurveys);
int IsBelowPoverty(int intHouseholdSize, double dblYearlyIncome);



// --------------------------------------------------------------------------------
// Main Function
// --------------------------------------------------------------------------------
int main()
{
    int intChoice = 0;

    printf("Census Bureau Application\n");
    printf("1. Write Mode\n");
    printf("2. Display Mode\n");
    printf("3. Exit\n");
    printf("Choose an Option: ");
    scanf("%d", &intChoice);

    switch (intChoice)
    {
    case 1:
        WriteSurveyData();
        break;
    case 2:
        DisplayMode();
        break;
    case 3:
        return 0;
        break;
    default:
        printf("Invalid Option.\n");
        break;
    }
    system("pause");
    return 0;
}



// --------------------------------------------------------------------------------
// Name: WriteSurveyData
// Abstract: Collect and validate survey data from the user and write it to a file.
// --------------------------------------------------------------------------------
void WriteSurveyData()
{
    // Declare variables
    FILE* pfilOutput = NULL;
    int intResultFlag = 0;
    udtSurveyData udtData;  // Survey data structure

    // Try to open the file for appending
    intResultFlag = OpenOutputFile("Census.csv", &pfilOutput);

    // Was the file opened?
    if (intResultFlag == TRUE)
    {
        // Collect survey date
        printf("Enter survey date (MM-DD-YYYY): ");
        scanf("%s", udtData.strDate);

        // Collect state
        printf("Select state (1: Ohio, 2: Kentucky): ");
        int intStateChoice;
        scanf("%d", &intStateChoice);
        strcpy(udtData.strState, (intStateChoice == 1) ? "Ohio" : "Kentucky");

        // Collect county based on state
        printf("Select county:\n");
        if (intStateChoice == 1)
        {
            printf("1: Hamilton\n2: Butler\n");
            int intCountyChoice;
            scanf("%d", &intCountyChoice);
            strcpy(udtData.strCounty, (intCountyChoice == 1) ? "Hamilton" : "Butler");
        }
        else
        {
            printf("1: Boone\n2: Kenton\n");
            int intCountyChoice;
            scanf("%d", &intCountyChoice);
            strcpy(udtData.strCounty, (intCountyChoice == 1) ? "Boone" : "Kenton");
        }

        // Collect race
        printf("Select race (1: Caucasian, 2: African American, 3: Hispanic, 4: Asian, 5: Other): ");
        int intRaceChoice;
        scanf("%d", &intRaceChoice);
        const char* strRaces[] = { "Caucasian", "African American", "Hispanic", "Asian", "Other" };
        strcpy(udtData.strRace, strRaces[intRaceChoice - 1]);

        // Collect household size
        do
        {
            printf("Enter household size (must be > 0): ");
            scanf("%d", &udtData.intHouseholdSize);
        } while (udtData.intHouseholdSize <= 0);

        // Collect yearly income
        do
        {
            printf("Enter yearly income (must be > 0): ");
            scanf("%lf", &udtData.dblYearlyIncome);
        } while (udtData.dblYearlyIncome <= 0);

        // Write data to file in CSV format
        fprintf(pfilOutput, "%s,%s,%s,%s,%d,%.2f\n",
            udtData.strDate, udtData.strState, udtData.strCounty, udtData.strRace, udtData.intHouseholdSize, udtData.dblYearlyIncome);

        // Flush to ensure data is written to disk
       fflush(pfilOutput);

        printf("Data successfully written to Census.csv.\n");
        printf("\n");
        
        // Clean up
        fclose(pfilOutput);
    }
    else
    {
        printf("Failed to open file for writing.\n");
        printf("\n");
    }
    main();
}



// --------------------------------------------------------------------------------
// Name: DisplayMode
// Abstract: Load survey data from a file and display statistics based on user input.
// --------------------------------------------------------------------------------
void DisplayMode() 
{
    udtSurveyData surveyDataArray[100]; // Fixed-size array for survey data
    int intTotalSurveys = 0;            // Counter for total surveys loaded
    char chUserChoice = '\0';           // User's menu selection as a letter
    bool blnLoop = true;

    while (blnLoop == true)
    {
        // Attempt to load survey data from the file
        if (LoadDataFromFile(surveyDataArray, &intTotalSurveys) == 1) {
            // Present the user with menu options for display actions
            printf("\n");
            printf("Select an action (A-I):\n");
            printf("A: Total Households Surveyed\n");
            printf("B: Total Households Surveyed per County\n");
            printf("C: Total Households Surveyed per Race\n");
            printf("D: Average Household Income\n");
            printf("E: Average Household Income by County and State\n");
            printf("F: Average Household Income by Race\n");
            printf("G: Percentage below Poverty\n");
            printf("H: Percentage below Poverty by County and State\n");
            printf("I: Percentage below Poverty by Race\n");
            printf("X: Exit\n");
            printf("Enter your choice: ");
            scanf(" %c", &chUserChoice);
            printf("\n");

            // Perform the selected action
            // I debated using toUpper function for this, but just had it not accept it instead.
            switch (chUserChoice) 
            {
            case 'A':
                DisplayTotalHouseholds(surveyDataArray, intTotalSurveys);
                break;
            case 'B':
                DisplayTotalByCounty(surveyDataArray, intTotalSurveys);
                break;
            case 'C':
                DisplayTotalByRace(surveyDataArray, intTotalSurveys);
                break;
            case 'D':
                DisplayAverageIncome(surveyDataArray, intTotalSurveys);
                break;
            case 'E':
                DisplayAverageIncomeByCountyAndState(surveyDataArray, intTotalSurveys);
                break;
            case 'F':
                DisplayAverageIncomeByRace(surveyDataArray, intTotalSurveys);
                break;
            case 'G':
                DisplayPovertyPercentage(surveyDataArray, intTotalSurveys);
                break;
            case 'H':
                DisplayPovertyPercentageByCountyAndState(surveyDataArray, intTotalSurveys);
                break;
            case 'I':
                DisplayPovertyPercentageByRace(surveyDataArray, intTotalSurveys);
                break;
            case 'X':
                printf("\n");
                main();
                break;
            default:
                printf("Invalid choice. Please select a valid option (A-I or X).\n");
            }
        }
        else 
        {
            printf("Failed to load data from the file.\n");
        }
        printf("\n");
        system("pause");
    }
}



// --------------------------------------------------------------------------------
// Name: LoadDataFromFile
// Abstract: Reads survey data from "Census.csv" into an array and counts surveys.
// --------------------------------------------------------------------------------
int LoadDataFromFile(udtSurveyData surveyDataArray[], int* pintTotalSurveys) 
{
    FILE* pfilInput = fopen("Census.csv", "r");
    if (pfilInput == NULL) {
        return 0; // Failed to open file
    }

    *pintTotalSurveys = 0; // Initialize the count

    // Read data from the file into the array
    // I used [^,] to handle the commas.
    // The == 6 because there are 6 fields.
    while (fscanf(pfilInput, "%10[^,],%10[^,],%10[^,],%20[^,],%d,%lf\n",
        surveyDataArray[*pintTotalSurveys].strDate,
        surveyDataArray[*pintTotalSurveys].strState,
        surveyDataArray[*pintTotalSurveys].strCounty,
        surveyDataArray[*pintTotalSurveys].strRace,
        &surveyDataArray[*pintTotalSurveys].intHouseholdSize,
        &surveyDataArray[*pintTotalSurveys].dblYearlyIncome) == 6) 
    {

        // Increment the count for each successfully read record
        (*pintTotalSurveys)++;

        // Stop reading if we exceed the array size
        if (*pintTotalSurveys >= 100)
        {
            printf("Maximum survey limit reached. Some data may not have been loaded.\n");
            break;
        }
    }

    fclose(pfilInput);
    return 1; // Successfully loaded data
}



// --------------------------------------------------------------------------------
// Name: OpenOutputFile
// Abstract: Open the file for writing/appending. Return true if successful.
// --------------------------------------------------------------------------------
int OpenOutputFile(char strFileName[], FILE** ppfilOutput)
{
    int intResultFlag = FALSE;

    // Open the file for appending
    *ppfilOutput = fopen(strFileName, "a");

    // Success?
    if (*ppfilOutput != NULL)
    {
        intResultFlag = TRUE;
    }
    else
    {
        printf("Error opening %s for writing!!!\n", strFileName);
    }

    return intResultFlag;
}



// --------------------------------------------------------------------------------
// Name: DisplayTotalHouseholds
// Abstract: Displays the total number of households surveyed.
// --------------------------------------------------------------------------------
void DisplayTotalHouseholds(const udtSurveyData surveyDataArray[], int intTotalSurveys) 
{
    printf("Total Households Surveyed: %d\n", intTotalSurveys);
}



// --------------------------------------------------------------------------------
// Name: DisplayTotalByCounty
// Abstract: Displays the total number of households surveyed for each county.
// --------------------------------------------------------------------------------
void DisplayTotalByCounty(const udtSurveyData surveyDataArray[], int intTotalSurveys)
{
    int intHamilton = 0;
    int intButler = 0;
    int intBoone = 0;
    int intKenton = 0;
    int intIndex = 0;

    // I'm using strcmp to compare the contents of two strings...
    // Should I have built a function for it instead?
    // I probably should have had it store the number instead of name.  Probably would have made this whole project easier.

    for (intIndex = 0; intIndex < intTotalSurveys; intIndex += 1)
    {
        if (strcmp(surveyDataArray[intIndex].strCounty, "Hamilton") == 0)
        {
            intHamilton += 1;
        }
        else if (strcmp(surveyDataArray[intIndex].strCounty, "Butler") == 0)
        {
            intButler += 1;
        }
        else if (strcmp(surveyDataArray[intIndex].strCounty, "Boone") == 0)
        {
            intBoone += 1;
        }
        else if (strcmp(surveyDataArray[intIndex].strCounty, "Kenton") == 0)
        {
            intKenton += 1;
        }
    }


    printf("Total Households by County:\n");
    printf("Hamilton: %d\n", intHamilton);
    printf("Butler: %d\n", intButler);
    printf("Boone: %d\n", intBoone);
    printf("Kenton: %d\n", intKenton);
}



// --------------------------------------------------------------------------------
// Name: DisplayTotalByRace
// Abstract: Displays the total number of households surveyed for each race.
// --------------------------------------------------------------------------------
void DisplayTotalByRace(const udtSurveyData surveyDataArray[], int intTotalSurveys) 
{
    int intCaucasian = 0;
    int intAfricanAmerican = 0;
    int intHispanic = 0;
    int intAsian = 0;
    int intOther = 0;
    int intIndex = 0;

    for (intIndex = 0; intIndex < intTotalSurveys; intIndex += 1)
    {
        if (strcmp(surveyDataArray[intIndex].strRace, "Caucasian") == 0) 
        {
            intCaucasian += 1;
        }
        else if (strcmp(surveyDataArray[intIndex].strRace, "African American") == 0) 
        {
            intAfricanAmerican += 1;
        }
        else if (strcmp(surveyDataArray[intIndex].strRace, "Hispanic") == 0) 
        {
            intHispanic += 1;
        }
        else if (strcmp(surveyDataArray[intIndex].strRace, "Asian") == 0) 
        {
            intAsian += 1;
        }
        else if (strcmp(surveyDataArray[intIndex].strRace, "Other") == 0) 
        {
            intOther += 1;
        }
    }


    printf("Total Households by Race:\n");
    printf("Caucasian: %d\n", intCaucasian);
    printf("African American: %d\n", intAfricanAmerican);
    printf("Hispanic: %d\n", intHispanic);
    printf("Asian: %d\n", intAsian);
    printf("Other: %d\n", intOther);
}



// --------------------------------------------------------------------------------
// Name: DisplayAverageIncome
// Abstract: Calculates and displays the average household income.
// --------------------------------------------------------------------------------
void DisplayAverageIncome(const udtSurveyData surveyDataArray[], int intTotalSurveys) 
{
    double dblTotalIncome = 0;
    int intIndex = 0;
    for (intIndex = 0; intIndex < intTotalSurveys; intIndex += 1)
    {
        dblTotalIncome += surveyDataArray[intIndex].dblYearlyIncome;
    }

    printf("Average Household Income: %.2f\n", dblTotalIncome / intTotalSurveys);
}



// --------------------------------------------------------------------------------
// Name: DisplayAverageIncomeByCountyAndState
// Abstract: Displays the average household income by county and state.
// --------------------------------------------------------------------------------
void DisplayAverageIncomeByCountyAndState(const udtSurveyData surveyDataArray[], int intTotalSurveys)
{
    double dblHamilton = 0;
    double dblButler = 0;
    double dblBoone = 0;
    double dblKenton = 0;
    int intHamiltonCount = 0;
    int intButlerCount = 0;
    int intBooneCount = 0;
    int intKentonCount = 0;
    int intIndex = 0;

    for (intIndex = 0; intIndex < intTotalSurveys; intIndex += 1) {
        if (strcmp(surveyDataArray[intIndex].strCounty, "Hamilton") == 0) {
            dblHamilton += surveyDataArray[intIndex].dblYearlyIncome;
            intHamiltonCount += 1;
        }
        else if (strcmp(surveyDataArray[intIndex].strCounty, "Butler") == 0) {
            dblButler += surveyDataArray[intIndex].dblYearlyIncome;
            intButlerCount += 1;
        }
        else if (strcmp(surveyDataArray[intIndex].strCounty, "Boone") == 0) {
            dblBoone += surveyDataArray[intIndex].dblYearlyIncome;
            intBooneCount += 1;
        }
        else if (strcmp(surveyDataArray[intIndex].strCounty, "Kenton") == 0) {
            dblKenton += surveyDataArray[intIndex].dblYearlyIncome;
            intKentonCount += 1;
        }
    }

    printf("Average Household Income by County:\n");
    if (intHamiltonCount > 0) {
        printf("Hamilton: %.2f\n", dblHamilton / intHamiltonCount);
    }
    if (intButlerCount > 0) {
        printf("Butler: %.2f\n", dblButler / intButlerCount);
    }
    if (intBooneCount > 0) {
        printf("Boone: %.2f\n", dblBoone / intBooneCount);
    }
    if (intKentonCount > 0) {
        printf("Kenton: %.2f\n", dblKenton / intKentonCount);
    }

}



// --------------------------------------------------------------------------------
// Name: DisplayAverageIncomeByRace
// Abstract: Displays the average household income for each race.
// --------------------------------------------------------------------------------
void DisplayAverageIncomeByRace(const udtSurveyData surveyDataArray[], int intTotalSurveys) 
{
    double dblCaucasian = 0;
    double dblAfricanAmerican = 0;
    double dblHispanic = 0;
    double dblAsian = 0;
    double dblOther = 0;
    int intCaucasianCount = 0;
    int intAfricanAmericanCount = 0;
    int intHispanicCount = 0;
    int intAsianCount = 0;
    int intOtherCount = 0;
    int intIndex = 0;

    for (intIndex = 0; intIndex < intTotalSurveys; intIndex += 1) 
    {
        if (strcmp(surveyDataArray[intIndex].strRace, "Caucasian") == 0) 
        {
            dblCaucasian += surveyDataArray[intIndex].dblYearlyIncome;
            intCaucasianCount += 1;
        }
        else if (strcmp(surveyDataArray[intIndex].strRace, "African American") == 0)
        {
            dblAfricanAmerican += surveyDataArray[intIndex].dblYearlyIncome;
            intAfricanAmericanCount += 1;
        }
        else if (strcmp(surveyDataArray[intIndex].strRace, "Hispanic") == 0)
        {
            dblHispanic += surveyDataArray[intIndex].dblYearlyIncome;
            intHispanicCount += 1;
        }
        else if (strcmp(surveyDataArray[intIndex].strRace, "Asian") == 0) 
        {
            dblAsian += surveyDataArray[intIndex].dblYearlyIncome;
            intAsianCount += 1;
        }
        else if (strcmp(surveyDataArray[intIndex].strRace, "Other") == 0) 
        {
            dblOther += surveyDataArray[intIndex].dblYearlyIncome;
            intOtherCount += 1;
        }
    }


    printf("Average Household Income by Race:\n");
    if (intCaucasianCount > 0)
    {
        printf("Caucasian: %.2f\n", dblCaucasian / intCaucasianCount);
    }
    if (intAfricanAmericanCount > 0) 
    {
        printf("African American: %.2f\n", dblAfricanAmerican / intAfricanAmericanCount);
    }
    if (intHispanicCount > 0) 
    {
        printf("Hispanic: %.2f\n", dblHispanic / intHispanicCount);
    }
    if (intAsianCount > 0) 
    {
        printf("Asian: %.2f\n", dblAsian / intAsianCount);
    }
    if (intOtherCount > 0) 
    {
        printf("Other: %.2f\n", dblOther / intOtherCount);
    }

}



// --------------------------------------------------------------------------------
// Name: DisplayPovertyPercentage
// Abstract: Calculates and displays the percentage of households below poverty.
// --------------------------------------------------------------------------------
void DisplayPovertyPercentage(const udtSurveyData surveyDataArray[], int intTotalSurveys) 
{
    int intBelowPoverty = 0;
    int intIndex = 0;

    for (intIndex = 0; intIndex < intTotalSurveys; intIndex += 1)
    {
        if (IsBelowPoverty(surveyDataArray[intIndex].intHouseholdSize, surveyDataArray[intIndex].dblYearlyIncome)) 
        {
            intBelowPoverty += 1;
        }
    }


    printf("Percentage Below Poverty: %.2f%%\n", (intBelowPoverty / (double)intTotalSurveys) * 100);
}



// --------------------------------------------------------------------------------
// Name: IsBelowPoverty
// Abstract: Determines if a household is below the poverty threshold.
// --------------------------------------------------------------------------------
int IsBelowPoverty(int intHouseholdSize, double dblYearlyIncome) 
{
    if (intHouseholdSize > 0 && intHouseholdSize <= 5) 
    {
        return dblYearlyIncome < intPOVERTY_THRESHOLDS[intHouseholdSize - 1];
    }
    else if (intHouseholdSize > 5) 
    {
        return dblYearlyIncome < intPOVERTY_THRESHOLDS[4];
    }
    return 0;
}



// --------------------------------------------------------------------------------
// Name: DisplayPovertyPercentageByCountyAndState
// Abstract: Displays the percentage of households below poverty for each county and state.
// --------------------------------------------------------------------------------
void DisplayPovertyPercentageByCountyAndState(const udtSurveyData surveyDataArray[], int intTotalSurveys) 
{
    int intOhioCount = 0;
    int intKentuckyCount = 0;
    int intOhioBelowPoverty = 0;
    int intKentuckyBelowPoverty = 0;
    int intHamiltonCount = 0;
    int intButlerCount = 0;
    int intBooneCount = 0;
    int intKentonCount = 0;
    int intHamiltonBelowPoverty = 0;
    int intButlerBelowPoverty = 0;
    int intBooneBelowPoverty = 0;
    int intKentonBelowPoverty = 0;
    int intIndex = 0;

    for (intIndex = 0; intIndex < intTotalSurveys; intIndex += 1) 
    {
        if (strcmp(surveyDataArray[intIndex].strState, "Ohio") == 0) 
        {
            intOhioCount++;
            if (IsBelowPoverty(surveyDataArray[intIndex].intHouseholdSize, surveyDataArray[intIndex].dblYearlyIncome))
            {
                intOhioBelowPoverty++;
            }
            if (strcmp(surveyDataArray[intIndex].strCounty, "Hamilton") == 0)
            {
                intHamiltonCount++;
                if (IsBelowPoverty(surveyDataArray[intIndex].intHouseholdSize, surveyDataArray[intIndex].dblYearlyIncome))
                {
                    intHamiltonBelowPoverty++;
                }
            }
            else if (strcmp(surveyDataArray[intIndex].strCounty, "Butler") == 0)
            {
                intButlerCount++;
                if (IsBelowPoverty(surveyDataArray[intIndex].intHouseholdSize, surveyDataArray[intIndex].dblYearlyIncome))
                {
                    intButlerBelowPoverty++;
                }
            }
        }
        else if (strcmp(surveyDataArray[intIndex].strState, "Kentucky") == 0) 
        {
            intKentuckyCount++;
            if (IsBelowPoverty(surveyDataArray[intIndex].intHouseholdSize, surveyDataArray[intIndex].dblYearlyIncome))
            {
                intKentuckyBelowPoverty++;
            }
            if (strcmp(surveyDataArray[intIndex].strCounty, "Boone") == 0)
            {
                intBooneCount++;
                if (IsBelowPoverty(surveyDataArray[intIndex].intHouseholdSize, surveyDataArray[intIndex].dblYearlyIncome))
                {
                    intBooneBelowPoverty++;
                }
            }
            else if (strcmp(surveyDataArray[intIndex].strCounty, "Kenton") == 0)
            {
                intKentonCount++;
                if (IsBelowPoverty(surveyDataArray[intIndex].intHouseholdSize, surveyDataArray[intIndex].dblYearlyIncome)) 
                {
                    intKentonBelowPoverty++;
                }
            }
        }
    }

    printf("Percentage Below Poverty by County and State:\n");
    // Is it bad coding to not include braces because there is only one statement?
    if (intOhioCount > 0) {
        printf("Ohio: %.2f%%\n", (intOhioBelowPoverty / (double)intOhioCount) * 100);
    }
    if (intHamiltonCount > 0) {
        printf("\tHamilton: %.2f%%\n", (intHamiltonBelowPoverty / (double)intHamiltonCount) * 100);
    }
    if (intButlerCount > 0) {
        printf("\tButler: %.2f%%\n", (intButlerBelowPoverty / (double)intButlerCount) * 100);
    }
    if (intKentuckyCount > 0) {
        printf("Kentucky: %.2f%%\n", (intKentuckyBelowPoverty / (double)intKentuckyCount) * 100);
    }
    if (intBooneCount > 0) {
        printf("\tBoone: %.2f%%\n", (intBooneBelowPoverty / (double)intBooneCount) * 100);
    }
    if (intKentonCount > 0) {
        printf("\tKenton: %.2f%%\n", (intKentonBelowPoverty / (double)intKentonCount) * 100);
    }
}



// --------------------------------------------------------------------------------
// Name: DisplayPovertyPercentageByRace
// Abstract: Displays the percentage of households below poverty for each race.
// --------------------------------------------------------------------------------
void DisplayPovertyPercentageByRace(const udtSurveyData surveyDataArray[], int intTotalSurveys) {
    int intCaucasianCount = 0;
    int intAfricanAmericanCount = 0;
    int intHispanicCount = 0;
    int intAsianCount = 0;
    int intOtherCount = 0;
    int intCaucasianBelowPoverty = 0;
    int intAfricanAmericanBelowPoverty = 0;
    int intHispanicBelowPoverty = 0;
    int intAsianBelowPoverty = 0;
    int intOtherBelowPoverty = 0;
    int intIndex = 0;

    for (intIndex = 0; intIndex < intTotalSurveys; intIndex += 1)
    {
        if (strcmp(surveyDataArray[intIndex].strRace, "Caucasian") == 0)
        {
            intCaucasianCount += 1;
            if (IsBelowPoverty(surveyDataArray[intIndex].intHouseholdSize, surveyDataArray[intIndex].dblYearlyIncome))
            {
                intCaucasianBelowPoverty += 1;
            }
        }
        else if (strcmp(surveyDataArray[intIndex].strRace, "African American") == 0)
        {
            intAfricanAmericanCount += 1;
            if (IsBelowPoverty(surveyDataArray[intIndex].intHouseholdSize, surveyDataArray[intIndex].dblYearlyIncome))
            {
                intAfricanAmericanBelowPoverty += 1;
            }
        }
        else if (strcmp(surveyDataArray[intIndex].strRace, "Hispanic") == 0)
        {
            intHispanicCount += 1;
            if (IsBelowPoverty(surveyDataArray[intIndex].intHouseholdSize, surveyDataArray[intIndex].dblYearlyIncome))
            {
                intHispanicBelowPoverty += 1;
            }
        }
        else if (strcmp(surveyDataArray[intIndex].strRace, "Asian") == 0)
        {
            intAsianCount += 1;
            if (IsBelowPoverty(surveyDataArray[intIndex].intHouseholdSize, surveyDataArray[intIndex].dblYearlyIncome))
            {
                intAsianBelowPoverty += 1;
            }
        }
        else if (strcmp(surveyDataArray[intIndex].strRace, "Other") == 0)
        {
            intOtherCount += 1;
            if (IsBelowPoverty(surveyDataArray[intIndex].intHouseholdSize, surveyDataArray[intIndex].dblYearlyIncome))
            {
                intOtherBelowPoverty += 1;
            }
        }
    }


    printf("Percentage Below Poverty by Race:\n");
    if (intCaucasianCount > 0) 
    {
        printf("Caucasian: %.2f%%\n", (intCaucasianBelowPoverty / (double)intCaucasianCount) * 100);
    }
    if (intAfricanAmericanCount > 0)
    {
        printf("African American: %.2f%%\n", (intAfricanAmericanBelowPoverty / (double)intAfricanAmericanCount) * 100);
    }
    if (intHispanicCount > 0) 
    {
        printf("Hispanic: %.2f%%\n", (intHispanicBelowPoverty / (double)intHispanicCount) * 100);
    }
    if (intAsianCount > 0) 
    {
        printf("Asian: %.2f%%\n", (intAsianBelowPoverty / (double)intAsianCount) * 100);
    }
    if (intOtherCount > 0) 
    {
        printf("Other: %.2f%%\n", (intOtherBelowPoverty / (double)intOtherCount) * 100);
    }

}
