# -----------------------------------------------------------------
# Assignment Name:      Assignment 6
# Name:                 Keith Brock
# -----------------------------------------------------------------



# -----------------------------------------------------------------
# Function Name:        DisplayInstructions
# Function Purpose:     This program will demonstrate how to make and
#                       use procedures in Python.
# -----------------------------------------------------------------

#	This program will demonstrate how to make and use procedures in Python...
#	In addition it will demonstrate how to pass values and variables into
#	a procedure as parameters.  It will demonstrate Python deals with ByRef and ByVal.  

#	Call the DisplayInstructions subroutine from main.

def DisplayInstructions():
    print('This program will demonstrate how to make and use procedures in Python...')
    print('In addition it will demonstrate how to pass values and variables into')
    print('a procedure as parameters.  It will demonstrate Python deals with ByRef and ByVal.')
    print(' ')



# -----------------------------------------------------------------
# Function Name:        Validate Integer Input
# Function Purpose:     Validate any integer data
# -----------------------------------------------------------------
def Validate_Integer_Count(int_Count):
   try:
        int_Count = int(int_Count)
        if(int_Count > 0):
            global strFlag
            strFlag = True
        else:
            print("Integer Must Be Positive")
   except ValueError:
        int_Count = int(0)
        print("Integer Must be Numeric")
   return int_Count



# -----------------------------------------------------------------
# Function Name:        Validate Text Data
# Function Purpose:     Validate Any Text Data
# -----------------------------------------------------------------

def Validate_Text_Love(strLove):
    try:
        strLove = str(strLove)
        if strLove.isalpha():
            global strLover
            strLover = True            
        else:
            print("Please enter a valid name with letters only.")     
    except Exception as e:
        print("An error occurred:", e)
    return strLove
        
        

# -----------------------------------------------------------------
# Function Name:        DisplayMessage
# Function Purpose:     This will display who you love the number of
#                       times entered.
# -----------------------------------------------------------------

def DisplayMessage(intCount):    
    for _ in range(intCount):    
        print('I Love ' + strLove + '\n')



# -----------------------------------------------------------------
# Function Name:        GetLargetValue
# Function Purpose:     This function will return the Largest of the
#                       two integers.
# -----------------------------------------------------------------

def GetLargerValue(intOne, intTwo):
    if intOne > intTwo:
        intLargest = intOne
    else:
        intLargest = intTwo
    return intLargest



# -----------------------------------------------------------------
# Function Name:        GetLargestValue
# Function Purpose:     Find the largest of seven integer values
# -----------------------------------------------------------------

def GetLargestValue(intValue1, intValue2, intValue3, intValue4, intValue5, intValue6, intValue7):
    intLargestValue = int(intValue1)

    if intValue2 > intLargestValue:
        intLargestValue = intValue2
    if intValue3 > intLargestValue:
        intLargestValue = intValue3
    if intValue4 > intLargestValue:
        intLargestValue = intValue4
    if intValue5 > intLargestValue:
        intLargestValue = intValue5
    if intValue6 > intLargestValue:
        intLargestValue = intValue6
    if intValue7 > intLargestValue:
        intLargestValue = intValue7

    return intLargestValue



# -----------------------------------------------------------------
# Function Name:        CalculateSphereVolume
# Function Purpose:     This function will return the volume of a 
#                       sphere.
# -----------------------------------------------------------------

def CalculateSphereVolume(intValue1):
    intRadius = intValue1 / 2                           
    intVolume = (4/3) * 3.14 * intRadius ** 3
    return intVolume
        


# -----------------------------------------------------------------
# Name:                 Controlling Main Code for Applications
# Purpose:              Controls the flow for Application
# -----------------------------------------------------------------

# declare all input, output, and other needed variables
intCount = int(0)
strLove = str("")
strFlag = bool(False)
strLover = bool(False)
strLarg = bool(False)
intOne = int(0)
intTwo = int(0)
intThree = int(0)
intFour = int(0)
intFive = int(0)
intSix = int(0)
intSeven = int(0)
intLarger = int(0)
intLargest = int(0)
intDiameter = int(0)
dblSphere = float(0)

DisplayInstructions()

#Getting Variables and Validating for Display Message
while strFlag is False:
   intCount = input("Enter number of times to repeat: ")
   intCount = Validate_Integer_Count (intCount) 

while strLover is False:
    strLove = input("Enter the person you love: ")    
    strLove = Validate_Text_Love(strLove)

DisplayMessage(intCount)

strFlag = bool(False)
#Getting Variables and Validating for GetLargerValue
while strFlag is False:
    intOne = input("Enter first integer: ") 
    intOne = Validate_Integer_Count(intOne)

strFlag = bool(False)

while strFlag is False:
    intTwo = input("Enter second integer: ") 
    intTwo = Validate_Integer_Count(intTwo)

intLarger = GetLargerValue(intOne, intTwo)

print(int(intLarger), ' is the largest integer entered.')

strFlag = bool(False)

#Getting Variables and Validating for GetLargestValue
while strFlag is False:
    intOne = input("Enter first integer: ") 
    intOne = Validate_Integer_Count(intOne)

strFlag = bool(False)

while strFlag is False:
    intTwo = input("Enter second integer: ") 
    intTwo = Validate_Integer_Count(intTwo)

strFlag = bool(False)

while strFlag is False:
    intThree = input("Enter third integer: ") 
    intThree = Validate_Integer_Count(intThree)

strFlag = bool(False)

while strFlag is False:
    intFour = input("Enter fourth integer: ") 
    intFour = Validate_Integer_Count(intFour)

strFlag = bool(False)

while strFlag is False:
    intFive = input("Enter fifth integer: ") 
    intFive = Validate_Integer_Count(intFive)

strFlag = bool(False)

while strFlag is False:
    intSix = input("Enter sixth integer: ") 
    intSix = Validate_Integer_Count(intSix)

strFlag = bool(False)

while strFlag is False:
    intSeven = input("Enter seventh integer: ") 
    intSeven = Validate_Integer_Count(intSeven)

intLargest = GetLargestValue(intOne, intTwo, intThree, intFour, intFive, intSix, intSeven)

print(intLargest, ' is the largest value entered into GetLargestValue.')

strFlag = bool(False)
#Getting Variables and Validating for CalculateSphereVolume.
while strFlag is False:
    intDiameter = input('Enter diameter: ')
    intDiameter = Validate_Integer_Count(intDiameter)

dblSphere = CalculateSphereVolume(intDiameter) 

print('The volume of the sphere with a diameter of {} is: {:.2f}.'.format(intDiameter, dblSphere))






