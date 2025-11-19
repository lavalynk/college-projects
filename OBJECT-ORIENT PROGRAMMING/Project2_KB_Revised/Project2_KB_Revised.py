#--------------------------------------------------------------------------
# Name:  Keith Brock
# Assignment # Project 2
#--------------------------------------------------------------------------
#   Import Data
#--------------------------------------------------------------------------

from datetime import datetime, timedelta
from BikeRental import BikeRental, Customer

#--------------------------------------------------------------------------
#   Project 2 # Function Area
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

def validateName(strName):
    """
    Validates that the name contains only letters and spaces.
    """
    if strName.replace(" ", "").isalpha():
        return True
    else:
        print("Invalid name. Name must contain only letters and spaces.")
        return False

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

#--------------------------------------------------------------------------
# Input bike stock for each type
#--------------------------------------------------------------------------

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

#--------------------------------------------------------------------------
# Initialize the BikeRental shop
#--------------------------------------------------------------------------

shop = BikeRental(mountain_stock=intMountainBikes, road_stock=intRoadBikes, touring_stock=intTouringBikes)

#--------------------------------------------------------------------------
# Creating a customers list.
#--------------------------------------------------------------------------

customers = []

#--------------------------------------------------------------------------
# Preload some customers with different rental times and coupon information
#--------------------------------------------------------------------------
    
# Test Case 1: Rent One Mountain Bike on an Hourly Basis with Coupon
customer1 = Customer("Alice", 1)
customer1.bike_type = "mountain"
customer1.bikes = 1
customer1.rentalBasis = 1  # 1 = hourly
customer1.rentalTime = datetime.now() - timedelta(hours=4)
customer1.coupon = "DISCOUNT10BBP"
customers.append(customer1)

# Test Case 2: Rent Four Road Bikes on a Daily Basis (Family Discount Applied, No Coupon)
customer2 = Customer("Bob", 2)
customer2.bike_type = "road"
customer2.bikes = 4  # Eligible for family discount
customer2.rentalBasis = 2  # 2 = daily
customer2.rentalTime = datetime.now() - timedelta(days=3)
customer2.coupon = ""
customers.append(customer2)

# Test Case 3: Rent Two Touring Bikes on a Weekly Basis with Coupon
customer3 = Customer("Charlie", 3)
customer3.bike_type = "touring"
customer3.bikes = 2
customer3.rentalBasis = 3  # 3 = weekly
customer3.rentalTime = datetime.now() - timedelta(weeks=2)
customer3.coupon = "WEEKLY20BBP"
customers.append(customer3)

# Test Case 4: Rent Five Mountain Bikes on an Hourly Basis (Family Discount Applied with Coupon)
customer4 = Customer("Diana", 4)
customer4.bike_type = "mountain"
customer4.bikes = 5  # Eligible for family discount
customer4.rentalBasis = 1  # 1 = hourly
customer4.rentalTime = datetime.now() - timedelta(hours=5)
customer4.coupon = "FAMILYBBP"
customers.append(customer4)

#--------------------------------------------------------------------------
# Menu Setup
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
        #--------------------------------------------------------------------------
        # New Customer Rental Process
        #--------------------------------------------------------------------------
        
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

        customer = Customer(strName, intCustomerID)
            
        while True:
            bike_type = input("Enter bike type (mountain/road/touring): ").lower()
            if bike_type in ['mountain', 'road', 'touring']:
                break
            else:
                print("Invalid bike type. Please choose from mountain, road, or touring.")

        while True:
            bikes = input("Enter the number of bikes: ")
            if validateNonNegativeInteger(bikes):
                bikes = int(bikes)
                break

        customer.bike_type = bike_type
        customer.bikes = bikes

        while True:
            strRentalType = input("Enter Rental Type (hourly/daily/weekly): ").lower()
            if strRentalType in ['hourly', 'daily', 'weekly']:
                if strRentalType == 'hourly':
                    customer.rentalBasis = 1
                elif strRentalType == 'daily':
                    customer.rentalBasis = 2
                elif strRentalType == 'weekly':
                    customer.rentalBasis = 3
                customer.rentalTime = shop.rentBike(customer.bikes, customer.bike_type, strRentalType)
                break
            else:
                print("Invalid rental type. Please choose from hourly, daily, or weekly.")
        
        # Prompt for coupon code
        while True:
            strCoupon = input("Enter coupon code (leave blank if none): ").strip()
            if not strCoupon:  # If user enters nothing, assume no coupon
                customer.coupon = ""
                break
            elif strCoupon.endswith("BBP"):
                customer.coupon = strCoupon
                break
            else:
                print("Invalid coupon code.")

        customers.append(customer)

    elif strChoice == '2':
        #--------------------------------------------------------------------------
        # Rental Return Process
        #--------------------------------------------------------------------------
        
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
            bill = shop.returnBike(customer.returnBike(), customer.bike_type, customer.coupon)
            if bill is not None:
                print(" ")
                print("Invoice:")
                print("Name of Customer: {}".format(customer.name))
                print("Type of Bike: {}".format(customer.bike_type))
                print("Number of Bikes Rented: {}".format(customer.bikes))
                print("Final Total: ${:.2f}".format(bill))
            customers.remove(customer)
        else:
            print("Customer not found.")

    elif strChoice == '3':
        #--------------------------------------------------------------------------
        # Show Inventory
        #--------------------------------------------------------------------------
        shop.displaystock()

    elif strChoice == '4':
        #--------------------------------------------------------------------------
        # End of Day Summary
        #--------------------------------------------------------------------------
        shop.endOfDay()

    elif strChoice == '5':
        #--------------------------------------------------------------------------
        # Exit Program
        #--------------------------------------------------------------------------
        print("Exiting program...")
        break

    else:
        print("Invalid choice. Please try again.")
