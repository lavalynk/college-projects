# -----------------------------------------------------------------
# Assignment Name:      Assignment 11
# Name:                 Keith Brock
# -----------------------------------------------------------------

# ------------------------------------------------------------------
# Assignment 11 Class Area
# ------------------------------------------------------------------

class Person:
	_intPeople = int(0)
	
	def __init__(self, strFirstName, strLastName, intAge, strGender):
		self.strFirstName = strFirstName
		self.strLastName = strLastName
		self.intAge = intAge
		self.strGender = strGender
		
		Person._intPeople += 1
	
	def __str__(self):
		return "There are " + str(Person._intPeople) + " people."
	
	def __repr__(self):
		return "First name: " + self._strFirstName + ", Last name: " + self._strLastName + ", Age: " + str(self._intAge) + ", Gender: " + self._strGender

	
	@property
	def strFirstName(self):
		return self._strFirstName
	
	@strFirstName.setter
	def strFirstName(self, strInput):
		if(str(strInput).isdecimal() == False):
			self._strFirstName = strInput
		else:
			self._strFirstName = ""
			raise Exception("First name has to have letters. The value of first name was: {}".format(strInput))

	
	@property
	def strLastName(self):
		return self._strLastName
	
	@strLastName.setter
	def strLastName(self, strInput):
		if(str(strInput).isdecimal() == False):
			self._strLastName = strInput
		else:
			self._strLastName = ""
			raise Exception("Last name has to have letters. The value of last name was: {}".format(strInput))

	
	@property
	def intAge(self):
		return self._intAge
	
	@intAge.setter
	def intAge(self, intInput):
		if(intInput < 0):
			raise Exception("Age has to be greater than 0. The value of age was: {}".format(intInput))
			self._intAge = 0
		else:
			self._intAge = intInput

	@property
	def strGender(self):
		return self._strGender
	
	@strGender.setter
	def strGender(self, strInput):
		strInput = strInput.upper()
		if(strInput == "F"):
			self._strGender = strInput
		elif(strInput == "M"):
			self._strGender = strInput
		else:
			self._strGender = ""
			raise Exception("Gender has to be an F or an M. The value of gender was: {}".format(strInput))