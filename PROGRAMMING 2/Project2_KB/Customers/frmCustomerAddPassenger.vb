Public Class frmCustomerAddPassenger
    Private Sub frmCustomerAddPassenger_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '
        '  On the event Form Load, we are going to populate the comboboxes of City and State from 
        '  the database
        '


        Try

            Dim strSelect As String = ""
            Dim cmdSelect As OleDb.OleDbCommand ' this will be used for our Select statement
            Dim drSourceTable As OleDb.OleDbDataReader ' this will be where our data is retrieved to           
            Dim dts As DataTable = New DataTable ' this is the table we will load from our reader for State

            ' open the DB
            If OpenDatabaseConnectionSQLServer() = False Then

                ' No, warn the user ...
                MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                    "The application will now close.",
                                    Me.Text + " Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)

                ' and close the form/application
                Me.Close()

            End If



            'load the State result set into the combobox.  For VB, we do this by binding the data to the combobox


            ' Build the select statement
            strSelect = "SELECT intStateID, strState FROM TStates"

            ' Retrieve all the records 
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader
            dts.Load(drSourceTable)


            'load the State result set into the combobox.  For VB, we do this by binding the data to the combobox


            cboStates.ValueMember = "intStateID"
            cboStates.DisplayMember = "strState"
            cboStates.DataSource = dts

            ' Clean up
            drSourceTable.Close()

            ' close the database connection
            CloseDatabaseConnection()

        Catch ex As Exception

            ' Log and display error message
            MessageBox.Show(ex.Message)

        End Try
    End Sub

    Private Sub btnAddPassenger_Click(sender As Object, e As EventArgs) Handles btnAddPassenger.Click

        ' Variables for new passenger data
        Dim strFirstName As String
        Dim strLastName As String
        Dim strAddress As String
        Dim strCity As String
        Dim intStateID As Integer
        Dim strZip As String
        Dim strPhoneNumber As String
        Dim strEmail As String
        Dim strPassengerLoginID As String
        Dim strPassword As String
        Dim dtmPassengerDOB As Date
        Dim intPassengerID As Integer
        Dim blnValidated As Boolean = True

        Dim cmdAddPassenger As New System.Data.OleDb.OleDbCommand()
        Dim intRowsAffected As Integer  ' Number of rows affected when SQL executed

        Try
            ' Validate data is entered
            Call_Validation(blnValidated)

            If blnValidated Then
                ' Put values into variables
                strFirstName = txtFirstName.Text
                strLastName = txtLastName.Text
                strAddress = txtAddress.Text
                strCity = txtCity.Text
                intStateID = CInt(cboStates.SelectedValue)
                strZip = txtZip.Text
                strPhoneNumber = txtPhone.Text
                strEmail = txtEmail.Text
                strPassengerLoginID = txtLogin.Text
                strPassword = txtPassword.Text
                dtmPassengerDOB = DateTime.Parse(txtDOB.Text)

                If OpenDatabaseConnectionSQLServer() = False Then
                    ' No, warn the user ...
                    MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                    "The application will now close.",
                                    Me.Text + " Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)

                    ' and close the form/application
                    Me.Close()
                End If

                ' Prepare the SQL command
                cmdAddPassenger = New System.Data.OleDb.OleDbCommand("uspAddPassenger", m_conAdministrator)
                cmdAddPassenger.CommandType = System.Data.CommandType.StoredProcedure

                ' Add parameters
                cmdAddPassenger.Parameters.Add(New System.Data.OleDb.OleDbParameter("@intPassengerID", System.Data.OleDb.OleDbType.Integer)).Direction = ParameterDirection.Output
                cmdAddPassenger.Parameters.Add(New System.Data.OleDb.OleDbParameter("@strFirstName", strFirstName))
                cmdAddPassenger.Parameters.Add(New System.Data.OleDb.OleDbParameter("@strLastName", strLastName))
                cmdAddPassenger.Parameters.Add(New System.Data.OleDb.OleDbParameter("@strAddress", strAddress))
                cmdAddPassenger.Parameters.Add(New System.Data.OleDb.OleDbParameter("@strCity", strCity))
                cmdAddPassenger.Parameters.Add(New System.Data.OleDb.OleDbParameter("@intStateID", intStateID))
                cmdAddPassenger.Parameters.Add(New System.Data.OleDb.OleDbParameter("@strZip", strZip))
                cmdAddPassenger.Parameters.Add(New System.Data.OleDb.OleDbParameter("@strPhoneNumber", strPhoneNumber))
                cmdAddPassenger.Parameters.Add(New System.Data.OleDb.OleDbParameter("@strEmail", strEmail))
                cmdAddPassenger.Parameters.Add(New System.Data.OleDb.OleDbParameter("@strPassengerLogin", strPassengerLoginID))
                cmdAddPassenger.Parameters.Add(New System.Data.OleDb.OleDbParameter("@strPassword", strPassword))
                cmdAddPassenger.Parameters.Add(New System.Data.OleDb.OleDbParameter("@dtmPassengerDOB", dtmPassengerDOB))

                ' Execute the command
                intRowsAffected = cmdAddPassenger.ExecuteNonQuery()

                ' Retrieve the output value
                intPassengerID = CInt(cmdAddPassenger.Parameters("@intPassengerID").Value)

                ' Close database connection
                CloseDatabaseConnection()

                ' Inform the user about the result
                If intPassengerID > 0 Then
                    MessageBox.Show("Insert successful. Passenger " & strFirstName & " " & strLastName & " has been added with Passenger ID: " & intPassengerID)
                Else
                    MessageBox.Show("Insert failed")
                End If

                ' Close the form
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub Call_Validation(ByRef blnValidated As Boolean)
        If blnValidated Then
            Validate_PassengerLoginID(blnValidated)
            If blnValidated Then
                Validate_PassengerPassword(blnValidated)
                If blnValidated Then
                    Validate_FirstName(blnValidated)
                    If blnValidated Then
                        Validate_LastName(blnValidated)
                        If blnValidated Then
                            Validate_Address(blnValidated)
                            If blnValidated Then
                                Validate_City(blnValidated)
                                If blnValidated Then
                                    Validate_Zip(blnValidated)
                                    If blnValidated Then
                                        Validate_Phone(blnValidated)
                                        If blnValidated Then
                                            Validate_Email(blnValidated)
                                            If blnValidated Then
                                                Validate_State(blnValidated)
                                                If blnValidated Then
                                                    Validate_PassengerDOB(blnValidated)
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub


    'Validation Checks
    Private Sub Validate_PassengerDOB(ByRef blnValidated As Boolean)
        txtDOB.Text = txtDOB.Text.Trim()

        If txtDOB.Text = String.Empty OrElse Not IsDate(txtDOB.Text) Then
            MessageBox.Show("Valid Date of Birth must exist. Format: MM/DD/YYYY")
            txtDOB.Focus()
            blnValidated = False
        Else
            Dim dtmPassengerDOB As DateTime
            If DateTime.TryParse(txtDOB.Text, dtmPassengerDOB) Then
                ' Ensure the date is not in the future
                If dtmPassengerDOB > DateTime.Now Then
                    MessageBox.Show("Date of Birth cannot be in the future.")
                    txtDOB.Focus()
                    blnValidated = False
                    ' Ensure the date is after 1900
                ElseIf dtmPassengerDOB.Year < 1900 Then
                    MessageBox.Show("Date of Birth must be after 1900.")
                    txtDOB.Focus()
                    blnValidated = False
                End If
            Else
                MessageBox.Show("Invalid Date. Format: MM/DD/YYYY")
                txtDOB.Focus()
                blnValidated = False
            End If
        End If
    End Sub

    Private Sub Validate_PassengerLoginID(ByRef blnValidated As Boolean)
        txtLogin.Text = txtLogin.Text.Trim()

        If txtLogin.Text = String.Empty Then
            MessageBox.Show("Passenger Login ID must exist.")
            txtLogin.Focus()
            blnValidated = False
        ElseIf txtLogin.Text.Length < 5 Then ' Example constraint: must be at least 5 characters
            MessageBox.Show("Passenger Login ID must be at least 5 characters long.")
            txtLogin.Focus()
            blnValidated = False
        End If
    End Sub

    Private Sub Validate_PassengerPassword(ByRef blnValidated As Boolean)
        txtPassword.Text = txtPassword.Text.Trim()

        If txtPassword.Text = String.Empty Then
            MessageBox.Show("Passenger Password must exist.")
            txtPassword.Focus()
            blnValidated = False
        ElseIf txtPassword.Text.Length < 8 Then ' Example constraint: must be at least 8 characters
            MessageBox.Show("Passenger Password must be at least 8 characters long.")
            txtPassword.Focus()
            blnValidated = False
        End If

    End Sub

    Private Sub Validate_FirstName(ByRef blnValidated As Boolean)
        txtFirstName.Text = txtFirstName.Text.Trim()

        If txtFirstName.Text = String.Empty Then
            MessageBox.Show("First Name Must Exist.")
            txtFirstName.Focus()
            blnValidated = False
        Else
            ' Check if the first name contains any numbers
            For Each intDigit As Char In "0123456789"
                If txtFirstName.Text.IndexOf(intDigit) <> -1 Then
                    MessageBox.Show("First Name must not contain numbers.")
                    txtFirstName.Focus()
                    blnValidated = False
                    Exit Sub
                End If
            Next
        End If
    End Sub

    Private Sub Validate_LastName(ByRef blnValidated As Boolean)
        txtLastName.Text = txtLastName.Text.Trim()

        If txtLastName.Text = String.Empty Then
            MessageBox.Show("Last Name Must Exist.")
            txtLastName.Focus()
            blnValidated = False
        Else
            ' Check if the first name contains any numbers
            For Each intDigit As Char In "0123456789"
                If txtLastName.Text.IndexOf(intDigit) <> -1 Then
                    MessageBox.Show("Last Name must not contain numbers.")
                    txtLastName.Focus()
                    blnValidated = False
                    Exit Sub
                End If
            Next
        End If
    End Sub

    Private Sub Validate_Address(ByRef blnValidated As Boolean)
        txtAddress.Text = txtAddress.Text.Trim()

        If txtAddress.Text = String.Empty Then
            MessageBox.Show("Address Must Exist.")
            txtAddress.Focus()
            blnValidated = False
        End If
    End Sub

    Private Sub Validate_City(ByRef blnValidated As Boolean)
        txtCity.Text = txtCity.Text.Trim()
        If txtCity.Text = String.Empty Then
            MessageBox.Show("City Must Exist.")
            txtCity.Focus()
            blnValidated = False
        Else
            ' Check if the first name contains any numbers
            For Each intDigit As Char In "0123456789"
                If txtCity.Text.IndexOf(intDigit) <> -1 Then
                    MessageBox.Show("City must not contain numbers.")
                    txtCity.Focus()
                    blnValidated = False
                    Exit Sub
                End If
            Next
        End If
    End Sub

    Private Sub Validate_Zip(ByRef blnValidated As Boolean)
        Dim strEnteredZip As String = txtZip.Text

        ' Check if the zip code is exactly 5 characters long
        If strEnteredZip.Length = 5 Then
            ' Check if all characters are digits using Integer.TryParse
            Dim zipNumber As Integer
            If Integer.TryParse(strEnteredZip, zipNumber) Then
                ' Zip code is valid
            Else
                MessageBox.Show("Invalid Zip Code. Zip code must be 5 digits.")
                txtZip.Focus()
                blnValidated = False
            End If
        Else
            MessageBox.Show("Invalid Zip Code. Zip code must be 5 digits.")
            txtZip.Focus()
            blnValidated = False
        End If
    End Sub

    Private Sub Validate_Phone(ByRef blnValidated As Boolean)
        Dim strPhone As String

        strPhone = txtPhone.Text.Trim()

        If IsNumeric(strPhone) And strPhone.Length = 10 Then
            'Phone valid
        Else
            MessageBox.Show("Please enter a valid 10 digit phone number.  Do not include - ( or ).")
            txtPhone.ResetText()
            txtPhone.Focus()
            blnValidated = False
        End If
    End Sub

    Private Sub Validate_Email(ByRef blnValidated As Boolean)
        ' Trim and check if the email text box is empty
        If txtEmail.Text.Trim() = String.Empty Then
            MessageBox.Show("Email must exist.")
            blnValidated = False
            Return
        End If
        ' Check if the email contains '@' symbol
        If txtEmail.Text.IndexOf("@") = -1 Then
            MessageBox.Show("Email must contain '@'.")
            blnValidated = False
            Return
        End If


    End Sub

    Private Sub Validate_State(ByRef blnValidated As Boolean)
        If cboStates.SelectedIndex = -1 Then
            MessageBox.Show("Please select a State from the list.")
            blnValidated = False
        Else
            'cboState is Validated
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub


End Class