from datetime import datetime

class BikeRental:
    def __init__(self, mountain_stock=0, road_stock=0, touring_stock=0):
        self.mountain_stock = mountain_stock
        self.road_stock = road_stock
        self.touring_stock = touring_stock
        self.total_bikes_rented = 0
        self.total_revenue = 0

    def rentBike(self, n, bike_type, rental_basis):
        if n <= 0:
            print("Number of bikes should be positive!")
            return None
        if bike_type == 'mountain':
            if n > self.mountain_stock:
                print("Sorry! We have currently {} mountain bikes available to rent.".format(self.mountain_stock))
                return None
            self.mountain_stock -= n
        elif bike_type == 'road':
            if n > self.road_stock:
                print("Sorry! We have currently {} road bikes available to rent.".format(self.road_stock))
                return None
            self.road_stock -= n
        elif bike_type == 'touring':
            if n > self.touring_stock:
                print("Sorry! We have currently {} touring bikes available to rent.".format(self.touring_stock))
                return None
            self.touring_stock -= n
        else:
            print("Invalid bike type.")
            return None
        
        now = datetime.now()
        self.total_bikes_rented += n
        return now

    def returnBike(self, request, bike_type, coupon=""):
        rentalTime, rentalBasis, numOfBikes = request
        bill = 0
        
        if rentalTime and rentalBasis and numOfBikes:
            if bike_type == 'mountain':
                self.mountain_stock += numOfBikes
            elif bike_type == 'road':
                self.road_stock += numOfBikes
            elif bike_type == 'touring':
                self.touring_stock += numOfBikes
            
            now = datetime.now()
            rentalPeriod = now - rentalTime
    
            # Calculate initial bill based on rental basis
            if rentalBasis == 1:
                bill = round(rentalPeriod.seconds / 3600) * 5 * numOfBikes
            elif rentalBasis == 2:
                bill = round(rentalPeriod.days) * 20 * numOfBikes
            elif rentalBasis == 3:
                bill = round(rentalPeriod.days / 7) * 60 * numOfBikes
            
            # Initialize discount multiplier
            discount_multiplier = 1.0
            
            # Apply family discount first, if applicable
            if 3 <= numOfBikes <= 5:
                print("You qualified for the Family Discount!")
                discount_multiplier -= 0.3  # Subtract 30% from multiplier
            
            # Apply coupon discount if applicable
            if coupon.endswith("BBP"):
                print("You entered a valid coupon!")
                discount_multiplier -= 0.1  # Subtract 10% from multiplier
            
            # Apply the calculated discount to the bill
            bill *= discount_multiplier
            
            # Update total revenue
            self.total_revenue += bill
            
            return bill
        else:
            print("Are you sure you rented a bike with us?")
            return None

    def displaystock(self):
        print("We have currently {} mountain bikes, {} road bikes, and {} touring bikes available to rent.".format(self.mountain_stock, self.road_stock, self.touring_stock))

    def endOfDay(self):
        print("Total bikes rented today: {}".format(self.total_bikes_rented))
        print("Total revenue collected today: ${}".format(self.total_revenue))

class Customer:
    def __init__(self, name, customer_id):
        """
        Our constructor method which instantiates a customer object.
        """
        self.name = name
        self.customer_id = customer_id
        self.bike_type = None
        self.bikes = 0
        self.rentalBasis = 0
        self.rentalTime = 0
        self.coupon = ""  # New attribute for the coupon code
    
    def requestBike(self):
        """
        Takes a request from the customer for the number and type of bikes.
        """
        while True:
            bike_type = input("Enter bike type (mountain/road/touring): ").lower()
            if bike_type in ['mountain', 'road', 'touring']:
                self.bike_type = bike_type
                break
            else:
                print("Invalid bike type. Please choose from mountain, road, or touring.")
        
        while True:
            bikes = input("How many bikes would you like to rent? ")
            try:
                bikes = int(bikes)
            except ValueError:
                print("That's not a positive integer!")
                continue
            if bikes < 1:
                print("Invalid input. Number of bikes should be greater than zero!")
                continue
            else:
                self.bikes = bikes
                break
        
        return self.bike_type, self.bikes
     
    def returnBike(self):
        """
        Allows customers to return their bikes to the rental shop.
        """
        if self.rentalBasis and self.rentalTime and self.bikes:
            return self.rentalTime, self.rentalBasis, self.bikes  
        else:
            return 0, 0, 0
