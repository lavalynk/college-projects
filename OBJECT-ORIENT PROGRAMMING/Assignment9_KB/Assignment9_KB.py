# -----------------------------------------------------------------
# Assignment Name:      Assignment 9 - With Class
# Name:                 Keith Brock
# -----------------------------------------------------------------
import Assignment9_KB_class

##########################################################
# Main 
##########################################################

lstStudents = []
lstGraduates = []
lstMaleGraduatesWithJobs = []
lstFemaleGraduatesWithJobs = []

# Create graduate objects
strFullName = input("Enter full name (first and last name separated by a space): ")
intAge = int(input("Enter age: "))
strGender = input("Enter gender (M/F): ")
dblGPA = float(input("Enter GPA: "))
intGradYear = int(input("Enter graduation year: "))
strJobStatus = input("Enter job status (Y/N): ")

try:
    student = Graduate(strFirstName, strLastName, intAge, strGender, dblGPA, intGradYear, strJobStatus)
    lstStudents.append(student)
    if strJobStatus == 'Y':
        lstGraduates.append(student)
        if strGender == 'M':
            lstMaleGraduatesWithJobs.append(student)
        elif strGender == 'F':
            lstFemaleGraduatesWithJobs.append(student)
except ValueError as e:
    print(e)

# Print statistics
print(Graduate1)
print("Average GPA of all Graduated Students: {:.2f}".format(Graduate1.get_grad_average_gpa()))
print("Average Age of Male Graduated Students: {:.2f}".format(Graduate1.get_grad_average_male_age()))
print("Average Age of Female Graduated Students: {:.2f}".format(Graduate1.get_grad_average_female_age()))
print("Total Number of Male Graduated Students who have jobs:", Graduate1.get_grad_male_job_count())
print("Total Number of Female Graduated Students who have jobs:", Graduate1.get_grad_female_job_count())
