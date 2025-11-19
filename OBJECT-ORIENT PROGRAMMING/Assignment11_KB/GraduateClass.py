# -----------------------------------------------------------------
# Assignment Name:      Assignment 11
# Name:                 Keith Brock
# -----------------------------------------------------------------


# ------------------------------------------------------------------
# Assignment 11 Class Area
# ------------------------------------------------------------------



import StudentClass as Stud

class Graduate(Stud.Student):
    intFemaleJobs = 0
    intMaleJobs = 0

    def __init__(self, strFirstName, strLastName, intAge, strGender, dblGPA, intGraduationYear, strJob):
        Stud.Student.__init__(self, strFirstName, strLastName, intAge, strGender, dblGPA)
        self.intGraduationYear = intGraduationYear
        self.strJob = strJob

        if strGender == "F" and strJob == "Y":
            Graduate.intFemaleJobs += 1
        elif strGender == "M" and strJob == "Y":
            Graduate.intMaleJobs += 1

    def __str__(self):
        return "There are " + str(Graduate.total_graduated_students()) + " graduates."

    @property
    def intGraduationYear(self):
        return self._intGraduationYear

    @intGraduationYear.setter
    def intGraduationYear(self, value):
        self._intGraduationYear = value

    @property
    def strJob(self):
        return self._strJob

    @strJob.setter
    def strJob(self, value):
        self._strJob = value

    @staticmethod
    def total_graduated_students():
        return Stud.Student._intFemales + Stud.Student._intMales

    @staticmethod
    def averageGPA():
        return Stud.Student.averageGPA()

    @staticmethod
    def averageAgeByGender():
        return Stud.Student.averageAgeOfStudentsByGender()

    @staticmethod
    def totalMaleFemaleWithJobs():
        return "Females with jobs: " + str(Graduate.intFemaleJobs) + ", Males with jobs: " + str(Graduate.intMaleJobs)

