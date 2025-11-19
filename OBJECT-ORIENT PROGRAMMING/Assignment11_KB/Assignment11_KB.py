# -----------------------------------------------------------------
# Assignment Name:      Assignment 11
# Name:                 Keith Brock
# -----------------------------------------------------------------

import GraduateClass as Grad

# ------------------------------------------------------------------
# Assignment 11 Main Processing Area
# ------------------------------------------------------------------

# Previous Assignment

# Create Graduate objects
test1 = Grad.Graduate("Emily", "Johnson", 22, "F", 3.6, 2024, "Y")
test2 = Grad.Graduate("Michael", "Williams", 27, "M", 3.2, 2025, "N")
test3 = Grad.Graduate("Sarah", "Brown", 23, "F", 3.9, 2023, "Y")
test4 = Grad.Graduate("David", "Jones", 26, "M", 2.7, 2024, "N")
test5 = Grad.Graduate("Anna", "Garcia", 24, "F", 3.8, 2026, "Y")

# Previous Assignment
# Print the number of graduates
print("There are {} graduates".format(Grad.Graduate.total_graduated_students()))

# Previous Assignment
print("Average GPA: {:.2f}".format(Grad.Graduate.averageGPA()))
print("Average age of students by gender: ", Grad.Graduate.averageAgeByGender())
print("Number of students with a job by gender: ", Grad.Graduate.totalMaleFemaleWithJobs())
print("Total number of graduated students: ", Grad.Graduate.total_graduated_students())

# Testing the new calculateGPA method
student = Grad.Graduate("Test", "Student", 22, "M", 3.5, 2023, "N")
print("Calculated GPA for ('A', 3): {:.2f}".format(student.calculateGPA('A', 3)))
print("Calculated GPA for ('A', 3, 'B', 4): {:.2f}".format(student.calculateGPA('A', 3, 'B', 4)))
print("Calculated GPA for ('A', 3, 'B', 4, 'C', 3): {:.2f}".format(student.calculateGPA('A', 3, 'B', 4, 'C', 3)))
print("Calculated GPA for ('A', 3, 'B', 4, 'C', 3, 'D', 2): {:.2f}".format(student.calculateGPA('A', 3, 'B', 4, 'C', 3, 'D', 2)))
print("Calculated GPA for ('A', 3, 'B', 4, 'C', 3, 'D', 2, 'B', 4): {:.2f}".format(student.calculateGPA('A', 3, 'B', 4, 'C', 3, 'D', 2, 'B', 4)))


