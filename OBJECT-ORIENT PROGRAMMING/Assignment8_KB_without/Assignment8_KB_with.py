# -----------------------------------------------------------------
# Assignment Name:      Assignment 8 - With Class
# Name:                 Keith Brock
# -----------------------------------------------------------------
import Assignment8_KB_class as Student
# -----------------------------------------------------------------
# Function Name:        Validate Integer Input
# Function Purpose:     Validate any integer data
# -----------------------------------------------------------------
def Validate_Integer_Count(intCount):
    try:
        intCount = int(intCount)
        if intCount > 0:
            return True, intCount
        else:
            print("Integer Must Be Positive")
    except ValueError:
        print("Integer Must be Numeric")
    return False, 0

# -----------------------------------------------------------------
# Function Name:        Validate Float Input
# Function Purpose:     Validate any Float data
# -----------------------------------------------------------------
def Validate_Float_Input(dblInput):
    try:
        dblInput = float(dblInput)
        if dblInput >= 0:
            return True, dblInput
        else:
            print("Input Must Be 0 or More")
    except ValueError:
        print("Input Must be Numeric")
    return False, 0.0

# -----------------------------------------------------------------
# Function Name:        Validate Text Data
# Function Purpose:     Validate Any Text Data
# -----------------------------------------------------------------
def Validate_Text_Input(strInput):
    try:
        strInput = str(strInput)
        if strInput.isalpha():
            return True, strInput
        else:
            print("Please enter a valid name with letters only.")     
    except Exception as e:
        print("An error occurred:", e)
    return False, ""

# -----------------------------------------------------------------
# Function Name:        Validate Gender Input
# Function Purpose:     Validate String data for M or F
# -----------------------------------------------------------------
def Validate_Gender_Input(strInput):
    strInput = strInput.upper()
    if strInput == 'M' or strInput == 'F':
        return True, strInput
    else:
        print("Gender must be 'M' or 'F'")
    return False, ""

# -----------------------------------------------------------------
# Name:                 Controlling Main Code for Applications
# Purpose:              Controls the flow for the Project
# -----------------------------------------------------------------
intCount = 0
blnValidated = False

while intCount < 5:
    
    blnValidated = False

    while not blnValidated:
        strFirstName = input("What is your First Name?: ")
        blnValidated, strFirstName = Validate_Text_Input(strFirstName)

    blnValidated = False

    while not blnValidated:
        strLastName = input("What is your Last Name?: ")
        blnValidated, strLastName = Validate_Text_Input(strLastName)

    blnValidated = False

    while not blnValidated:
        strGender = input("What is your Gender? (M OR F): ")
        blnValidated, strGender = Validate_Gender_Input(strGender)

    blnValidated = False

    while not blnValidated:
        dblGPA = input("What is your GPA?: ")
        blnValidated, dblGPA = Validate_Float_Input(dblGPA)

    blnValidated = False

    while not blnValidated:
        intAge = input("What is your age?: ")
        blnValidated, intAge = Validate_Integer_Count(intAge)

    if intCount == 0:
        student1 = Student(strFirstName, strLastName, strGender, dblGPA, intAge)
    elif intCount == 1:
        student2 = Student(strFirstName, strLastName, strGender, dblGPA, intAge)
    elif intCount == 2:
        student3 = Student(strFirstName, strLastName, strGender, dblGPA, intAge)
    elif intCount == 3:
        student4 = Student(strFirstName, strLastName, strGender, dblGPA, intAge)
    elif intCount == 4:
        student5 = Student(strFirstName, strLastName, strGender, dblGPA, intAge)

    intCount+=1

student1.displayCount()
student1.displayAvgGPA()
student1.displayAvgMaleAge()
student1.displayAvgFemaleAge()

print("Total Number of Male Students: ", Student.intMaleCount)
print("Total Number of Female Students: ", Student.intFemaleCount)

