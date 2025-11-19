#--------------------------------------------------------------------------------------------------
#   Name:   Keith Brock
#
#   Class:  IT-102
#
#   Assignment 5b - For Loop
#
#--------------------------------------------------------------------------------------------------

strHourly = str(input("Hourly Rate (Enter as 0.00): "))
strRaise = str(input("Raise Percentage (Enter as 0.00): "))
dblSalary = float(strHourly) * 40  * 52
dblRaise = float(strRaise) / 100

print ('Year 1 : ${:.2f}'.format(dblSalary))
dblTotal = float(0)

for intCount in range(2,11):
    dblSalary = dblSalary + (dblSalary * dblRaise)
    print ('Year', intCount, ': ${:.2f}'.format(dblSalary))
    intCount+=1
    