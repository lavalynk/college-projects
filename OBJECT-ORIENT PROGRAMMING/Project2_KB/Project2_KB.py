# Name:  Keith Brock
# Assignment # Project 2

#--------------------------------------------------------------------------
#   Import Data
#--------------------------------------------------------------------------


from datetime import datetime, timedelta
from clsBikeRental import BikeRental, Customer



#--------------------------------------------------------------------------
#   Project 2 # Function Area
#--------------------------------------------------------------------------

#--------------------------------------------------------------------------
#1. Function Name: validateNonNegativeInteger
#2. Function Description: Makes sure that the integer is 0 or greater.
#--------------------------------------------------------------------------


def validateNonNegativeInteger(value):
    """
    Validates that the input value is a non-negative integer.
    """
    try:
        intValue = int(value)
        if intValue >= 0:
            return True
        else:
            print("Invalid input. The number must be 0 or greater.")
            return False
    except ValueError:
        print("Invalid input. The number must be a non-negative integer.")
        return False



#--------------------------------------------------------------------------
#1. Function Name: validateName
#2. Function Description: Makes sure it is a string and valid name.
#--------------------------------------------------------------------------

def validateName(strName):
    """
    Validates that the name contains only letters and spaces.
    """
    if strName.replace(" ", "").isalpha():
        return True
    else:
        print("Invalid name. Name must contain only letters and spaces.")
        return False



#--------------------------------------------------------------------------
#1. Function Name: validateCustomerID
#2. Function Description: Makes sure the customer id is a postive integer.
#--------------------------------------------------------------------------

def validateCustomerID(intCustomerID):
    """
    Validates that the customer ID is a positive integer.
    """
    try:
        intCustomerID = int(intCustomerID)
        if intCustomerID > 0:
            return True
        else:
            print("Invalid customer ID. It must be a positive integer.")
            return False
    except ValueError:
        print("Invalid customer ID. It must be a number.")
        return False



#--------------------------------------------------------------------------
#1. Function Name: isCustomerIDUnique
#2. Function Description: Searches list for duplicate ID.
#--------------------------------------------------------------------------

def isCustomerIDUnique(customers, intCustomerID):
    """
    Validates that the customer ID is unique.
    """
    for customer in customers:
        if customer.customer_id == intCustomerID:
            print("Customer ID already in use. Please choose a different ID.")
            return False
    return True



#--------------------------------------------------------------------------
# Project 2 # Main Processing Area
#--------------------------------------------------------------------------

print("Welcome to Bob's Bike Rental Shop!")

while True:
    strMountainBikes = input("Enter the initial number of mountain bikes: ")
    if validateNonNegativeInteger(strMountainBikes):
        intMountainBikes = int(strMountainBikes)
        break

while True:
    strRoadBikes = input("Enter the initial number of road bikes: ")
    if validateNonNegativeInteger(strRoadBikes):
        intRoadBikes = int(strRoadBikes)
        break

while True:
    strTouringBikes = input("Enter the initial number of touring bikes: ")
    if validateNonNegativeInteger(strTouringBikes):
        intTouringBikes = int(strTouringBikes)
        break

shop = BikeRental()
shop.stock['mountain'] = intMountainBikes
shop.stock['road'] = intRoadBikes
shop.stock['touring'] = intTouringBikes

# Creating a customers list.
customers = []

#--------------------------------------------------------------------------
# Preload some customers with different rental times and coupon information
#--------------------------------------------------------------------------
    
# Customer 1: Rented a mountain bike 4 hours ago (hourly rental) with coupon
customer1 = Customer(1, "Alice")
customer1.bike_type = "mountain"
customer1.bikes = 1
customer1.rentalBasis = 1  # hourly
customer1.rentalTime = datetime.now() + timedelta(hours=-4)
customer1.coupon = "SUMMER21BBP"
customers.append(customer1)

# Customer 2: Rented a road bike 23 hours ago (hourly rental) without coupon but family discount
customer2 = Customer(2, "Bob")
customer2.bike_type = "road"
customer2.bikes = 4
customer2.rentalBasis = 1  # hourly
customer2.rentalTime = datetime.now() + timedelta(hours=-23)
customer2.coupon = ""
customers.append(customer2)

# Customer 3: Rented a touring bike 4 days ago (daily rental) with coupon
customer3 = Customer(3, "Charlie")
customer3.bike_type = "touring"
customer3.bikes = 3
customer3.rentalBasis = 2  # daily
customer3.rentalTime = datetime.now() + timedelta(days=-4)
customer3.coupon = "WINTER21BBP"
customers.append(customer3)

# Customer 4: Rented a mountain bike 14 days ago (weekly rental) without coupon
customer4 = Customer(4, "Diana")
customer4.bike_type = "mountain"
customer4.bikes = 4
customer4.rentalBasis = 3  # weekly
customer4.rentalTime = datetime.now() + timedelta(days=-14)
customer4.coupon = ""
customers.append(customer4)


#--------------------------------------------------------------------------
#   Menu Setup
#--------------------------------------------------------------------------

while True:
    print(" ")
    print("1. New Customer Rental")
    print("2. Rental Return")
    print("3. Show Inventory")
    print("4. End of Day")
    print("5. Exit Program")
    print(" ")
    strChoice = input("Enter your choice: ")

    if strChoice == '1':
        while True:
            strCustomerID = input("Enter Customer ID: ")
            if validateCustomerID(strCustomerID):
                intCustomerID = int(strCustomerID)
                if isCustomerIDUnique(customers, intCustomerID):
                    break

        while True:
            strName = input("Enter Customer Name: ")
            if validateName(strName):
                break

        customer = Customer(intCustomerID, strName)
            
        customer.see_available_bikes(shop)
        customer.requestBike()

        strCoupon = ""
        strHasCoupon = input("Do you have a coupon? (Y/N): ").upper()
        if strHasCoupon == 'Y':
            while True:
                strCoupon = input("Enter your coupon code: ")
                if strCoupon.endswith("BBP"):
                    break
                else:
                    print("Invalid coupon code. It must end with 'BBP'.")

        customer.coupon = strCoupon
            
        while True:
            strRentalType = input("Enter Rental Type (hourly/daily/weekly): ").lower()
            if strRentalType in ['hourly', 'daily', 'weekly']:
                if strRentalType == 'hourly':
                    customer.rentalBasis = 1
                    customer.rentalTime = shop.rentBikeOnHourlyBasis(customer.bike_type, customer.bikes)
                elif strRentalType == 'daily':
                    customer.rentalBasis = 2
                    customer.rentalTime = shop.rentBikeOnDailyBasis(customer.bike_type, customer.bikes)
                elif strRentalType == 'weekly':
                    customer.rentalBasis = 3
                    customer.rentalTime = shop.rentBikeOnWeeklyBasis(customer.bike_type, customer.bikes)
                break
            else:
                print("Invalid rental type. Please choose from hourly, daily, or weekly.")
                    
        customers.append(customer)

    elif strChoice == '2':
        while True:
            strCustomerID = input("Enter Customer ID: ")
            if validateCustomerID(strCustomerID):
                intCustomerID = int(strCustomerID)
                break

        customer = None
#--------------------------------------------------------------------------
#   Loop to go through each CustomerID to find match.
#--------------------------------------------------------------------------
        for c in customers:
            if c.customer_id == intCustomerID:
                customer = c
                break

        if customer:
            request = customer.returnBike()
            invoice = shop.returnBike(request, customer.name, customer.coupon)
            if invoice:
                print("\nInvoice:")
                print("Name of Customer: {}".format(invoice["customer_name"]))
                print("Type of Bike: {}".format(invoice["bike_type"]))
                print("Number of Bikes Rented: {}".format(invoice["num_of_bikes"]))
                print("Total Time of Rental: {}".format(invoice["total_time"]))
                print("Total Before Discount: ${:.2f}".format(invoice["total_before_discount"]))
                print("Discount: ${:.2f}".format(invoice["discount"]))
                print("Final Total: ${:.2f}".format(invoice["final_total"]))
            customers.remove(customer)
        else:
            print("Customer not found.")

    elif strChoice == '3':
        shop.displaystock()

    elif strChoice == '4':
        print("Total bikes rented today: {}".format(shop.total_bikes_rented))
        print("Total daily revenue collected: ${:.2f}".format(shop.daily_revenue))

    elif strChoice == '5':
        print("Exiting program...")
        break

    else:
        print("Invalid choice. Please try again.")

