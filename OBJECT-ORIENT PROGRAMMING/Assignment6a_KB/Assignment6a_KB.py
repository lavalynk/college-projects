# Name:  Keith Brock
# Assignment # 6a

#--------------------------------------------------------------------------
#   Assignment # 6a Function Area
#--------------------------------------------------------------------------


# -----------------------------------------------------------------
# Function Name:        Validate Integer Input
# Function Purpose:     Validate any integer data
# -----------------------------------------------------------------

def Validate_Integer_Count(int_Count):
    try:
        int_Count = int(int_Count)
        if int_Count >= 0:
            global blnInputValidated
            blnInputValidated = True
        else:
            print("Integer Must Be Positive or Equal to Zero")
    except ValueError:
        int_Count = int(0)
        print("Integer Must be Numeric")
    return int_Count


# -----------------------------------------------------------------
# Function Name:        Validate Text Data
# Function Purpose:     Validate Any Text Data
# -----------------------------------------------------------------

def Validate_Text(strText):
    try:
        strText = str(strText).upper()
        if strText in ['Y', 'N']:
            global blnInputValidated 
            blnInputValidated  = True            
        else:
            print("Please enter Y or N.")
    except Exception as e:
        print("An error occurred:", e)
    return strText



# -----------------------------------------------------------------
# Function Name:        Calculate_Air
# Function Purpose:     Calculate Air Reimbursement
# -----------------------------------------------------------------

def Calculate_Air(int1,int2):
    if int1 > int2:
        return int2
    else:
        return int1



# -----------------------------------------------------------------
# Function Name: Calculate_Lodging
# Function Purpose: Calculate Lodging Per Diem
# -----------------------------------------------------------------

def Calculate_Lodging(int1, int2):
    return int1 * int2



# -----------------------------------------------------------------
# Function Name: Calculate_Food
# Function Purpose: Calculate Food Per Diem
# -----------------------------------------------------------------

def Calculate_Food(int1, int2):
    return int1 * int2

#--------------------------------------------------------------------------
# Assignment #6a Main Processing Area 
#--------------------------------------------------------------------------

#--------------------------------------------------------------------------
# Declare Variables
#--------------------------------------------------------------------------
strPosition = str("")
strRepeat = str('Y')
intDays = int(0)
intAir= float(0)
intLodging= float(0)
intFood = float(0)
blnInputValidated = bool(False)
dblAirTotal = float(0)
dblLodgingTotal = float(0)
dblFoodTotal = float(0)
dblReimbursement = float(0)
dblTotalCost = float(0)
blnRepeat = bool(True)
dblEmployeeResponsible = float(0)



#--------------------------------------------------------------------------
# Declare Get and Validate Input
#--------------------------------------------------------------------------

while strRepeat == 'Y':    

    while blnInputValidated is False:
        strPosition = input('Are you in Management?  (Y or N): ')
        strPosition = Validate_Text(strPosition).upper()    

    blnInputValidated = False

    while blnInputValidated is False:
        intDays = input('How many days are you staying?: ')
        intDays = Validate_Integer_Count(intDays)

    blnInputValidated = False

    while blnInputValidated is False:
        dblAir = input('What was your cost of AIR TRAVEL?: ')
        dblAir = Validate_Integer_Count(dblAir)

    blnInputValidated = False

    while blnInputValidated is False:
        dblLodging = input('What was your cost of LODGING?: ')
        dblLodging = Validate_Integer_Count(dblLodging)

    blnInputValidated = False

    while blnInputValidated is False:
        dblFood = input('What was your cost of FOOD?: ')
        dblFood = Validate_Integer_Count(dblFood)

    blnInputValidated = False



#--------------------------------------------------------------------------
# Perform Calculations
#--------------------------------------------------------------------------

    if strPosition == 'Y':
        dblAirTotal = Calculate_Air(dblAir,500)
        dblLodgingTotal = Calculate_Lodging(125, intDays)
        dblFoodTotal = Calculate_Food(75, intDays)
    else:
        dblAirTotal = Calculate_Air(dblAir,400)
        dblLodgingTotal = Calculate_Lodging(100, intDays)
        dblFoodTotal = Calculate_Food(50, intDays)

    dblReimbursement = dblAirTotal + dblLodgingTotal + dblFoodTotal
    dblTotalCost = dblAir + dblLodging + dblFood
    dblEmployeeResponsible = dblTotalCost - dblReimbursement



#--------------------------------------------------------------------------
# Display Output
#--------------------------------------------------------------------------

    print('Total Travel Costs: ${:,.2f}'.format(dblTotalCost))
    print('Total Amount Reimbursed: ${:,.2f}'.format(dblReimbursement))
    print('Total Employee Responsibility: ${:,.2f}'.format(dblEmployeeResponsible))

#Loop to next employee
    while blnInputValidated is False:
        strRepeat = input('Next Employee? (Y OR N): ')
        strRepeat = Validate_Text(strRepeat).upper() 

    blnInputValidated = False
    
