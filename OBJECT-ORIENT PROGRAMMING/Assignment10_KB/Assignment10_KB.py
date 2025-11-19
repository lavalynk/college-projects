# -----------------------------------------------------------------
# Assignment Name:      Assignment 10
# Name:                 Keith Brock
# -----------------------------------------------------------------

class Person:
    intPerson_Counter = int(0) 

    def __init__(self, strFirstName, strLastName, intAge, strGender):
        self.first_name = strFirstName
        self.last_name = strLastName
        self.age = intAge
        self.gender = strGender

    @property
    def first_name(self):
        return self._first_name

    @first_name.setter
    def first_name(self, value):
        if not value.isalpha():
            raise ValueError("First name must be alphabetic")
        self._first_name = value

    @property
    def last_name(self):
        return self._last_name

    @last_name.setter
    def last_name(self, value):
        if not value.isalpha():
            raise ValueError("Last name must be alphabetic")
        self._last_name = value

    @property
    def age(self):
        return self._age

    @age.setter
    def age(self, value):
        if not (0 <= value <= 120):
            raise ValueError("Age must be between 0 and 120")
        self._age = value

    @property
    def gender(self):
        return self._gender

    @gender.setter
    def gender(self, value):
        if value not in ['Male', 'Female']:
            raise ValueError("Gender must be 'Male' or 'Female'")
        self._gender = value
          
        Person.intPerson_Counter += 1


class Student(Person):
    intMale_Counter = int(0)
    intFemale_Counter = int(0)
    dblAccumulated_GPA = float(0)
    intAccumulated_Male_Age = int(0)
    intAccumulated_Female_Age = int(0)

    def __init__(self, strFirstName, strLastName, intAge, strGender, dblGPA):
        Person.__init__(self, strFirstName, strLastName, intAge, strGender)
        self.gpa = dblGPA

    @property
    def gpa(self):
        return self._gpa

    @gpa.setter
    def gpa(self, value):
        if not (0.0 <= value <= 4.0):
            raise ValueError("GPA must be between 0.0 and 4.0")
        self._gpa = value

        Student.dblAccumulated_GPA += self.gpa

    def Total_Students(self):
        if self.gender == "Male":
            Student.intMale_Counter += 1
        elif self.gender == "Female":
            Student.intFemale_Counter += 1

    def AccumulateAge(self):
        if self.gender == "Male":
            Student.intAccumulated_Male_Age += self.age
        elif self.gender == "Female":
            Student.intAccumulated_Female_Age += self.age

    def Calculate_Average_GPA():
        if Person.intPerson_Counter == 0:
            return 0
        dblAverageGPA = Student.dblAccumulated_GPA / Person.intPerson_Counter
        return '{:2,.2f}'.format(dblAverageGPA)

    def Calculate_Average_Male_Age():
        if Student.intMale_Counter > 0:
            dblAverageMaleAge = Student.intAccumulated_Male_Age / Student.intMale_Counter
        else:
            dblAverageMaleAge = 0
        return '{:2,.2f}'.format(dblAverageMaleAge)

    def Calculate_Average_Female_Age():
        if Student.intFemale_Counter > 0:
            dblAverageFemaleAge = Student.intAccumulated_Female_Age / Student.intFemale_Counter
        else:
            dblAverageFemaleAge = 0
        return '{:2,.2f}'.format(dblAverageFemaleAge)


class Graduate(Student):
    intEmployed_Male_Counter = int(0)
    intEmployed_Female_Counter = int(0)

    def __init__(self, strFirstName, strLastName, intAge, strGender, dblGPA, intGraduationYear, strJobStatus):
        Student.__init__(self, strFirstName, strLastName, intAge, strGender, dblGPA)
        self.graduation_year = intGraduationYear
        self.job_status = strJobStatus
        self.AccumulateAge()
        self.Total_Students()
        
        self.strTotalGraduates = "Total number of graduates: " + str(Person.intPerson_Counter)

    @property
    def job_status(self):
        return self._job_status

    @job_status.setter
    def job_status(self, value):
        if value not in ['Y', 'N']:
            raise ValueError("Job status must be 'Y' or 'N'")
        self._job_status = value

    @property
    def graduation_year(self):
        return self._graduation_year

    @graduation_year.setter
    def graduation_year(self, value):
        if value <= 1900:
            raise ValueError("Graduation year must be greater than 1900")
        self._graduation_year = value

    def Total_Students(self):
        super().Total_Students()
        if self.job_status == "Y":
            if self.gender == "Male":
                Graduate.intEmployed_Male_Counter += 1
            elif self.gender == "Female":
                Graduate.intEmployed_Female_Counter += 1

    def __str__(self):
        return self.strTotalGraduates


def Display_Outputs(Student):
    print("Average GPA of Graduated Students: ", str(Graduate.Calculate_Average_GPA()))
    print("Total Male Graduated Students:", str(Graduate.intMale_Counter))
    print("Total Female Graduated Students:", str(Graduate.intFemale_Counter))
    print("Average Age of Graduated Male Students: ", str(Graduate.Calculate_Average_Male_Age()))
    print("Average Age of Graduated Female Students: ", str(Graduate.Calculate_Average_Female_Age()))
    print(str(Graduate.intEmployed_Male_Counter), " Male Graduates have jobs")
    print(str(Graduate.intEmployed_Female_Counter), " Female Graduates have jobs")


###################################################################
#  Main Routine
###################################################################


# Collect Graduates (5 times)

G1 = Graduate("John", "Doe", 30, "Male", 3.8, 2012, "Y")
G2 = Graduate("Emily", "Smith", 28, "Female", 3.2, 2014, "N")
G3 = Graduate("Michael", "Johnson", 26, "Male", 3.9, 2016, "Y")
G4 = Graduate("Sarah", "Williams", 24, "Female", 3.5, 2018, "Y")
G5 = Graduate("Daniel", "Brown", 22, "Male", 3.4, 2020, "N")

# Display number of Total Graduates
print(G5)  # This prints new_Graduate object of Graduate Class.
           # It includes dunder __str__ function, which prints total number of students.
# Display Outputs
Display_Outputs(Graduate)
