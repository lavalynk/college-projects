class Person:
    'Common base class for all persons'

    def __init__(self, strFullName, intAge, strGender):
        self.strFullName = strFullName
        self.intAge = intAge
        self.strGender = strGender

    @property
    def strFullName(self):
        return self.__strFullName

    @strFullName.setter
    def strFullName(self, value):
        if " " not in value:
            raise ValueError("Full name must include a first and last name separated by a space.")
        self.__strFullName = value

    def getFirstName(self):
        return self.__strFullName.split(" ")[0]

    def getLastName(self):
        return self.__strFullName.split(" ")[1]

    @property
    def intAge(self):
        return self.__intAge

    @intAge.setter
    def intAge(self, value):
        try:
            value = int(value)
            if value <= 0:
                raise ValueError
        except:
            raise ValueError("Age must be a positive integer.")
        self.__intAge = value

    @property
    def strGender(self):
        return self.__strGender

    @strGender.setter
    def strGender(self, value):
        if value not in ('M', 'F'):
            raise ValueError("Gender must be 'M' or 'F'.")
        self.__strGender = value

class Student(Person):
    def __init__(self, strFullName, intAge, strGender, dblGPA):
        super().__init__(strFullName, intAge, strGender)
        self.dblGPA = dblGPA

    @property
    def dblGPA(self):
        return self.__dblGPA

    @dblGPA.setter
    def dblGPA(self, value):
        try:
            value = float(value)
        except ValueError:
            raise ValueError("GPA must be a float between 0 and 4.0.")
        if not (0 <= value <= 4.0):
            raise ValueError("GPA must be a float between 0 and 4.0.")
        self.__dblGPA = value

    @staticmethod
    def totalStudents(lstStudents):
        count = 0
        for _ in lstStudents:
            count += 1
        return count

    @staticmethod
    def averageGPA(lstStudents):
        total_gpa = 0
        count = 0
        for student in lstStudents:
            total_gpa += student.dblGPA
            count += 1
        return total_gpa / count if count > 0 else 0

    @staticmethod
    def averageAgeByGender(lstStudents, strGender):
        total_age = 0
        count = 0
        for student in lstStudents:
            if student.strGender == strGender:
                total_age += student.intAge
                count += 1
        return total_age / count if count > 0 else 0


class Graduate(Student):
    intGradStudentCount = 0
    intGradJobMaleCount = 0
    intGradJobFemaleCount = 0
    dblGradGPATotal = 0.0
    intGradMaleAgeTotal = 0
    intGradFemaleAgeTotal = 0
    intGradMaleCount = 0
    intGradFemaleCount = 0

    def __init__(self, strFirstName, strLastName, intAge, strGender, dblGPA, intGraduationYear, strJobStatus):
        Student.__init__(self, strFirstName, strLastName, intAge, strGender, dblGPA)
        self.intGraduationYear = intGraduationYear
        self.strJobStatus = strJobStatus
        Graduate.intGradStudentCount += 1
        Graduate.dblGradGPATotal += dblGPA
        
        if strGender == 'Male':
            Graduate.intGradMaleCount += 1
            Graduate.intGradMaleAgeTotal += intAge
            if strJobStatus == 'Y':
                Graduate.intGradJobMaleCount += 1
        elif strGender == 'Female':
            Graduate.intGradFemaleCount += 1
            Graduate.intGradFemaleAgeTotal += intAge
            if strJobStatus == 'Y':
                Graduate.intGradJobFemaleCount += 1

    def get_grad_average_gpa(self):
        if Graduate.intGradStudentCount > 0:
            return Graduate.dblGradGPATotal / Graduate.intGradStudentCount
        return 0

    def get_grad_average_male_age(self):
        if Graduate.intGradMaleCount > 0:
            return Graduate.intGradMaleAgeTotal / Graduate.intGradMaleCount
        return 0

    def get_grad_average_female_age(self):
        if Graduate.intGradFemaleCount > 0:
            return Graduate.intGradFemaleAgeTotal / Graduate.intGradFemaleCount
        return 0

    def get_grad_male_job_count(self):
        return Graduate.intGradJobMaleCount

    def get_grad_female_job_count(self):
        return Graduate.intGradJobFemaleCount

def __str__(self):
    return "Total Number of Graduated Students: " + str(Graduate.intGradStudentCount)
