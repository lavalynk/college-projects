#--------------------------------------------------------------------------------------------------
#   Name:   Keith Brock
#
#   Class:  IT-102
#
#   Assignment 4
#
#--------------------------------------------------------------------------------------------------
strContinue = 'Y'

while strContinue == 'Y':

    strSeason = str(input("Are you staying in Off Season (O), Peak Season (P) or Standard Season (S)?  (Enter O, P, or S): "))
    strFirstName = str(input("What is your First Name?: "))
    strLastName = str(input("What is your Last Name?: "))
    strDays = str(input("How many days will you be staying with us?: "))
    strAARP = str(input("Do you have AARP or AAA? (Enter Y or N): "))
    strState = str(input("Are you from Florida? (Enter Y or N): "))
    dblTotal = float(0)
    dblTotalDiscount = float(0)
    dblTotalTax = float(0)
    dblGrandTotal = float(0)
    #Calculate Season Cost

    if strSeason == 'O':
        dblSeason = 50
    elif strSeason == 'P':
        dblSeason = 150
    else:
        dblSeason = 100

    #Calculate AARP or AAA Discount
    if strAARP == 'Y':
        dblDiscount = 0.025
    else:
        dblDiscount = 0
    
    #Calculate Florida Exemption
    if strState == 'Y':
        dblTax = 0
    else:
        dblTax = .10

    #Calculate Days Discount
    if 14 < int(strDays) < 30:
        dblDiscount += .05
    elif int(strDays) > 30:
        dblDiscount += .1

    dblTotal = dblSeason * int(strDays)
    dblTotalDiscount = dblTotal * dblDiscount
    dblTotalTax = (dblTotal - dblTotalDiscount) * dblTax
    dblGrandTotal = dblTotal - dblTotalDiscount + dblTotalTax

    print ("Total: ${:.2f}".format(dblTotal))
    print ("Discount: ${:.2f}".format(dblTotalDiscount))
    print ("Taxes: ${:.2f}".format(dblTotalTax))
    print ("The Total cost is: ${:.2f}".format(dblGrandTotal))

    strContinue = str(input("Another Rental? (Enter Y or N): "))
