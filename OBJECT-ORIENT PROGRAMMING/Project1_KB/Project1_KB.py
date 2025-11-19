# -----------------------------------------------------------------
# Assignment Name:      Project 1
# Name:                 Keith Brock
# -----------------------------------------------------------------


# -----------------------------------------------------------------
# Function Name:        Validate String Input
# Function Purpose:     Validate any String data
# -----------------------------------------------------------------

def Validate_String_Input( str_Input ):
   str_Input = str_Input.upper()
   if(str_Input == 'M') or (str_Input == 'E'):
      global blnValidated
      blnValidated = True
   else:
      print("Employee must be 'M' or 'E'")
   
   return str_Input



# -----------------------------------------------------------------
# Function Name:        Validate String Input
# Function Purpose:     Validate Repeat Data
# -----------------------------------------------------------------

def Validate_Repeat_Input( str_Input ):
   str_Input = str_Input.upper()    
   if(str_Input == 'Y') or (str_Input == 'N'):
      global blnValidated
      blnValidated = True
   else:
      print("You must reply Y or N.")
   
   return str_Input



# -----------------------------------------------------------------
# Function Name:        Validate Integer Input
# Function Purpose:     Validate any integer data
# -----------------------------------------------------------------

def Validate_Integer_Input( int_Input ):
   try:
        int_Input = int(int_Input)
        if(int_Input >= 0):
          global blnValidated
          blnValidated = True
        else:
            print("Input Must Be 0 or More")
   except ValueError:
        int_Input = int(0)
        print("Input Must be Numeric With No Decimals")
   return int_Input



# -----------------------------------------------------------------
# Function Name:        Validate Float Input
# Function Purpose:     Validate any Float data
# -----------------------------------------------------------------

def Validate_Float_Input( flt_Input ):
   try:
        flt_Input = float(flt_Input)
        if(flt_Input >= 0):
          global blnValidated
          blnValidated = True
        else:
            print("Input Must Be 0 or More")
   except ValueError:
        flt_Input = int(0)
        print("Input Must be Numeric")
   return flt_Input



# -----------------------------------------------------------------
# Function Name:        Calculate_Discount
# Function Purpose:     Calculate the Employee's Discount
# -----------------------------------------------------------------

def Calculate_Discount(dblInput1, strInput2):
    dblDiscount = float(0)

    if dblInput1 > 15:
        dblDiscount = 0.40
    else:
        if dblInput1 > 10:
            dblDiscount = 0.35
        else:
            if dblInput1 > 6:
                dblDiscount = 0.30
            else:
                if dblInput1 > 3:
                    dblDiscount = 0.24
                else:
                    if dblInput1 > 0:
                        dblDiscount = 0.20

    if strInput2 == "E":   
        dblDiscount -= .1

    return dblDiscount



# -----------------------------------------------------------------
# Function Name:        Calculate_Previous_Discount
# Function Purpose:     Calculate the Previous Employee's Discount
# -----------------------------------------------------------------

def Calculate_Previous_Discount(dblOldPurchase, dblDiscount):
    dblPreviousDiscount = dblOldPurchase * dblDiscount

    if dblPreviousDiscount > 200:
        dblPreviousDiscount = 200

    return dblPreviousDiscount



# -----------------------------------------------------------------
# Function Name:        Calculate_Total_Discount
# Function Purpose:     Calculate the Employee's Discount
# -----------------------------------------------------------------
def Calculate_Total_Discount(dblDiscount, dblTodayPurchase, dblPreviousDiscount):
    dblMaxDiscount = 200 - dblPreviousDiscount
    dblDiscountMath = dblTodayPurchase * dblDiscount
    
    if dblDiscountMath < dblMaxDiscount:
        dblTotalDiscount = dblDiscountMath
    else:
        dblTotalDiscount = dblMaxDiscount
    
    return dblTotalDiscount        



# -----------------------------------------------------------------
# Function Name:        Calculate_Grand_Total
# Function Purpose:     Calculate the Grand Total
# -----------------------------------------------------------------

def Calculate_Grand_Total(dblTotal, dblDiscount):
    return dblTotal - dblDiscount



# -----------------------------------------------------------------
# Function Name:        Display Totals
# Function Purpose:     Will display totals
# -----------------------------------------------------------------

def Display_Totals(dblDiscount, dblPreviousDiscount, dblTodayPurchase, dblTodayDiscount, dblGrandTotal):
    strDiscount = "{:.2f}".format(dblDiscount * 100)
    strPreviousDiscount = "{:.2f}".format(dblPreviousDiscount)
    strTodayTotal = "{:.2f}".format(dblTodayPurchase)
    strTodayDiscount = "{:.2f}".format(dblTodayDiscount)
    strGrandTotal = "{:.2f}".format(dblGrandTotal)
    
    print("The Discount Percentage is: " + strDiscount + " %.")
    print("The Previous Discount is: $" + strPreviousDiscount + ".")
    print("The total for today's purchase before discount: $" + strTodayTotal)
    print("The discount amount is: $" + strTodayDiscount)
    print("The final total amount is $" + strGrandTotal)



# -----------------------------------------------------------------
# Name:                 Controlling Main Code for Applications
# Purpose:              Controls the flow for the Project
# -----------------------------------------------------------------

intNumberofYears = int(0)
dblPreviousPurchases = float(0)
strRole = str("")
dblTodayPurchase = float(0)
dblDiscount = float(0)
dblPreviousDiscount = float(0)
dblDiscountDollar = float(0)
dblGrandTotal = float(0)
dblSessionTotal = float(0)
dblSessionFinalTotal = float(0)
strRepeat = "Y"

while strRepeat == "Y":
    blnValidated = bool(False)

    while blnValidated is False:
        intNumberofYears = input("How many years have you worked for the company?: ")
        intNumberofYears = Validate_Integer_Input(intNumberofYears)

    blnValidated = bool(False)

    while blnValidated is False:
        dblPreviousPurchases = input("How much were your previous purchases?: ")
        dblPreviousPurchases = Validate_Float_Input(dblPreviousPurchases)

    blnValidated = bool(False)

    while blnValidated is False:
        strRole = input("Are you a manager or an employee? (M or E): ")
        strRole = Validate_String_Input(strRole)

    blnValidated = bool(False)

    while blnValidated is False:
        dblTodayPurchase = input("How much is today's purchase?: ")
        dblTodayPurchase = Validate_Float_Input(dblTodayPurchase)

    dblSessionTotal += dblTodayPurchase

    #call function to figure out Discount
    dblDiscount = Calculate_Discount(intNumberofYears, strRole)
    dblPreviousDiscount = Calculate_Previous_Discount(dblPreviousPurchases, dblDiscount)
    dblDiscountDollar = Calculate_Total_Discount(dblDiscount, dblTodayPurchase, dblPreviousDiscount)
    dblGrandTotal = Calculate_Grand_Total(dblTodayPurchase, dblDiscountDollar)
    dblSessionFinalTotal += dblGrandTotal

    # call function to format and display Totals
    Display_Totals(dblDiscount, dblPreviousDiscount, dblTodayPurchase, dblDiscountDollar, dblGrandTotal)

    blnValidated = bool(False)

    while blnValidated is False:
        print(" ")
        strRepeat = input("Next Employee? (Y or N): ")
        strRepeat = Validate_Repeat_Input(strRepeat)
        print(" ")

print("Total Before Discount: ${:.2f}".format(dblSessionTotal))
print("Total After Discount: ${:.2f}".format(dblSessionFinalTotal))




