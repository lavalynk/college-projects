# -----------------------------------------------------------------
# Assignment Name:      Assignment 8 - With Class
# Name:                 Keith Brock
# -----------------------------------------------------------------
import Assignment8_KB_class as Student

# This would create first object from the Student class
Student1 = Student.Student("Bill", "Zara", "M", 3.25, 20)

# This would create second object from the Student class
Student2 = Student.Student("Betty", "Tara", "F", 2.25, 25)

# This would create third object from the Student class
Student3 = Student.Student("Sydney", "Nye", "F", 3.75, 30)

# This would create forth object from the Student class
Student4 = Student.Student("Jake", "Leedom", "M", 2.75, 35)

# This would create fifth object from the Student class
Student5 = Student.Student("Alex", "Jacobs", "M", 3.55, 21)

# Display the results
Student1.displayCount()
Student1.displayAvgGPA()
Student1.displayAvgMaleAge()
Student1.displayAvgFemaleAge()

print("Total Number of Male Students: ", Student.Student.intMaleCount)
print("Total Number of Female Students: ", Student.Student.intFemaleCount)