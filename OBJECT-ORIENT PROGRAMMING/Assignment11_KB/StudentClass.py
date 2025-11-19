# -----------------------------------------------------------------
# Assignment Name:      Assignment 11
# Name:                 Keith Brock
# -----------------------------------------------------------------

import PersonClass as Person


# ------------------------------------------------------------------
# Assignment 11 Class Area
# ------------------------------------------------------------------

class Student(Person.Person):
    _intFemales = 0
    _intMales = 0
    _dblTotalGPA = 0.0
    _dblTotalFemaleAge = 0.0
    _dblTotalMaleAge = 0.0

    def __init__(self, strFirstName, strLastName, intAge, strGender, dblGPA):
        Person.Person.__init__(self, strFirstName, strLastName, intAge, strGender)
        self.dblGPA = dblGPA

        Student._dblTotalGPA += self._dblGPA
        if strGender == "F":
            Student._intFemales += 1
            Student._dblTotalFemaleAge += intAge
        elif strGender == "M":
            Student._intMales += 1
            Student._dblTotalMaleAge += intAge

    def __str__(self):
        return Person.Person.__str__(self) + ", GPA: " + str(self.dblGPA)

    def __repr__(self):
        return Person.Person.__repr__(self) + ", GPA: " + str(self.dblGPA)

    @property
    def dblGPA(self):
        return self._dblGPA

    @dblGPA.setter
    def dblGPA(self, value):
        if not (0.0 <= value <= 4.0):
            raise ValueError("GPA must be between 0 and 4.0")
        self._dblGPA = value

    @staticmethod
    def numOfStudentsByGender():
        return "Females: " + str(Student._intFemales) + ", Males: " + str(Student._intMales)

    @staticmethod
    def averageGPA():
        if Person.Person._intPeople == 0:
            return 0
        return Student._dblTotalGPA / Person.Person._intPeople

    @staticmethod
    def averageAgeOfStudentsByGender():
        avg_female_age = Student._dblTotalFemaleAge / Student._intFemales if Student._intFemales > 0 else 0
        avg_male_age = Student._dblTotalMaleAge / Student._intMales if Student._intMales > 0 else 0
        return "Females: " + str(avg_female_age) + ", Males: " + str(avg_male_age)

    @staticmethod
    def grade_to_gpa(grade):
        if grade == 'A':
            return 4.0
        elif grade == 'B':
            return 3.0
        elif grade == 'C':
            return 2.0
        elif grade == 'D':
            return 1.0
        elif grade == 'F':
            return 0.0
        else:
            raise ValueError("Invalid grade")


    def calculateGPA(self, *args):
        if len(args) % 2 != 0 or len(args) > 10:
            raise ValueError("Invalid number of arguments. Provide up to 5 pairs of grades and credit hours.")

        total_points = 0
        total_credits = 0

        for i in range(0, len(args), 2):
            grade = args[i]
            hours = args[i + 1]
            total_points += self.grade_to_gpa(grade) * hours
            total_credits += hours

        dblGPA = total_points / total_credits if total_credits != 0 else 0
        return dblGPA