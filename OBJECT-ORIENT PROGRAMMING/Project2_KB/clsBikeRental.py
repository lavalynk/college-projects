from datetime import datetime, timedelta

class BikeRental:
    def __init__(self, stock=0):
        """
        Our constructor class that instantiates bike rental shop.
        """
        self.stock = {
            'mountain': stock,
            'road': stock,
            'touring': stock
        }
        self.total_bikes_rented = 0
        self.daily_revenue = 0

    def displaystock(self):
        """
        Displays the bikes currently available for rent in the shop.
        """
        print("We have currently {} mountain bikes, {} road bikes, and {} touring bikes available to rent.".format(
            self.stock['mountain'], self.stock['road'], self.stock['touring']))
        return self.stock

    def rentBikeOnHourlyBasis(self, bike_type, n):
        """
        Rents a bike on hourly basis to a customer.
        """
        if n <= 0:
            print("Number of bikes should be positive!")
            return None
        elif n > self.stock[bike_type]:
            print("Sorry! We have currently {} {} bikes available to rent.".format(self.stock[bike_type], bike_type))
            return None
        else:
            now = datetime.now()
            print("You have rented {} {} bike(s) on hourly basis today at {} hours.".format(n, bike_type, now.hour))
            print("You will be charged $5 for each hour per bike.")
            print("We hope that you enjoy our service.")
            self.stock[bike_type] -= n
            self.total_bikes_rented += n
            return now      

    def rentBikeOnDailyBasis(self, bike_type, n):
        """
        Rents a bike on daily basis to a customer.
        """
        if n <= 0:
            print("Number of bikes should be positive!")
            return None
        elif n > self.stock[bike_type]:
            print("Sorry! We have currently {} {} bikes available to rent.".format(self.stock[bike_type], bike_type))
            return None
        else:
            now = datetime.now()
            print("You have rented {} {} bike(s) on daily basis today at {} hours.".format(n, bike_type, now.hour))
            print("You will be charged $20 for each day per bike.")
            print("We hope that you enjoy our service.")
            self.stock[bike_type] -= n
            self.total_bikes_rented += n
            return now

    def rentBikeOnWeeklyBasis(self, bike_type, n):
        """
        Rents a bike on weekly basis to a customer.
        """
        if n <= 0:
            print("Number of bikes should be positive!")
            return None
        elif n > self.stock[bike_type]:
            print("Sorry! We have currently {} {} bikes available to rent.".format(self.stock[bike_type], bike_type))
            return None        
        else:
            now = datetime.now()
            print("You have rented {} {} bike(s) on weekly basis today at {} hours.".format(n, bike_type, now.hour))
            print("You will be charged $60 for each week per bike.")
            print("We hope that you enjoy our service.")
            self.stock[bike_type] -= n
            self.total_bikes_rented += n
            return now

    def returnBike(self, request, customer_name, coupon):
        """
        1. Accept a rented bike from a customer
        2. Replenishes the inventory
        3. Return a bill and details for the invoice
        """
        rentalTime, rentalBasis, numOfBikes, bike_type = request
        bill = 0
        total_before_discount = 0
        discount = 0
        if rentalTime and rentalBasis and numOfBikes:
            self.stock[bike_type] += numOfBikes
            now = datetime.now()
            rentalPeriod = now - rentalTime

            # Convert rentalPeriod to total hours, days, and weeks
            total_hours = rentalPeriod.total_seconds() // 3600
            total_days = total_hours // 24
            total_weeks = total_days // 7

            rental_duration = "{} hours".format(total_hours) if rentalBasis == 1 else \
                              "{} days".format(total_days) if rentalBasis == 2 else \
                              "{} weeks".format(total_weeks)

            # Billing calculation
            if rentalBasis == 1:
                total_before_discount = total_hours * 5 * numOfBikes
            elif rentalBasis == 2:
                total_before_discount = total_days * 20 * numOfBikes
            elif rentalBasis == 3:
                total_before_discount = total_weeks * 60 * numOfBikes

            # Family discount calculation
            if 3 <= numOfBikes <= 5:
                discount += total_before_discount * 0.3
                bill = total_before_discount * 0.7
            else:
                bill = total_before_discount

            # Coupon discount calculation
            if coupon.endswith("BBP"):
                discount += bill * 0.1
                bill = bill * 0.9

            print("Thanks for returning your bike. Hope you enjoyed our service!")
            self.daily_revenue += round(bill, 2)
            invoice = {
                "customer_name": customer_name,
                "bike_type": bike_type,
                "num_of_bikes": numOfBikes,
                "total_time": rental_duration,
                "total_before_discount": round(total_before_discount, 2),
                "discount": round(discount, 2),
                "final_total": round(bill, 2)
            }
            return invoice
        else:
            print("Are you sure you rented a bike with us?")
            return None

class Customer:
    def __init__(self, intCustomerID, strName):
        """
        Our constructor method which instantiates various customer objects.
        """
        self.customer_id = intCustomerID
        self.name = strName
        self.bikes = 0
        self.bike_type = ""
        self.rentalBasis = 0
        self.rentalTime = 0
        self.bill = 0
        self.coupon = ""

    def see_available_bikes(self, shop):
        """
        Displays available bikes at the shop.
        """
        shop.displaystock()

    def requestBike(self):
        """
        Takes a request from the customer for the number of bikes and bike type.
        """
        while True:
            bikes = input("How many bikes would you like to rent? ")
            if bikes.isdigit() and int(bikes) > 0:
                self.bikes = int(bikes)
                break
            else:
                print("Invalid input. Number of bikes should be a positive integer.")

        while True:
            bike_type = input("What type of bike would you like to rent (mountain, road, touring)? ").lower()
            if bike_type in ['mountain', 'road', 'touring']:
                self.bike_type = bike_type
                break
            else:
                print("Invalid bike type. Please choose from mountain, road, or touring.")
                
        return self.bikes, self.bike_type

    def returnBike(self):
        """
        Allows customers to return their bikes to the rental shop.
        """
        if self.rentalBasis and self.rentalTime and self.bikes:
            return self.rentalTime, self.rentalBasis, self.bikes, self.bike_type
        else:
            return 0, 0, 0, ""
