Public Class frmCustomerUpdate

    Private Sub frmCustomerUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmdSelect As OleDb.OleDbCommand ' This will be used for our Select statement
        Dim drSourceTable As OleDb.OleDbDataReader ' This will be where our data is retrieved to
        Dim dts As DataTable = New DataTable ' This is the table we will load from our reader for State
        Dim objParam As OleDb.OleDbParameter ' This will be used to add parameters needed for stored procedures

        Try
            ' Open the database connection
            If OpenDatabaseConnectionSQLServer() = False Then
                ' Warn the user if the connection fails...
                MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                    "The application will now close.",
                                    Me.Text + " Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                ' And close the form/application
                Me.Close()
            End If

            ' Call the stored procedure to get states
            cmdSelect = New OleDb.OleDbCommand("uspGetStates", m_conAdministrator)
            cmdSelect.CommandType = CommandType.StoredProcedure

            ' Retrieve all the records 
            drSourceTable = cmdSelect.ExecuteReader
            dts.Load(drSourceTable)

            ' Bind the state result set to the combo box
            cboStates.ValueMember = "intStateID"
            cboStates.DisplayMember = "strState"
            cboStates.DataSource = dts

            ' Clean up
            drSourceTable.Close()

            ' Load passenger data into the text fields
            LoadPassengerData()

            ' Close the database connection
            CloseDatabaseConnection()

        Catch excError As Exception
            ' Log and display error message
            MessageBox.Show(excError.Message)
        End Try
    End Sub

    Private Sub LoadPassengerData()
        Dim cmdSelect As OleDb.OleDbCommand
        Dim drSourceTable As OleDb.OleDbDataReader
        Dim dt As DataTable = New DataTable
        Dim objParam As OleDb.OleDbParameter

        Try
            ' Open the database connection
            If OpenDatabaseConnectionSQLServer() = False Then
                ' Warn the user if the connection fails...
                MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                "The application will now close.",
                                Me.Text + " Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                ' And close the form/application
                Me.Close()
            End If

            ' Call the stored procedure to get passenger data
            cmdSelect = New OleDb.OleDbCommand("uspGetPassengerData", m_conAdministrator)
            cmdSelect.CommandType = CommandType.StoredProcedure

            ' Add the parameter for the stored procedure
            objParam = cmdSelect.Parameters.Add("@passengerID", OleDb.OleDbType.Integer)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = intPassenger

            ' Retrieve the record
            drSourceTable = cmdSelect.ExecuteReader
            dt.Load(drSourceTable)

            ' Populate text fields with passenger data
            If dt.Rows.Count > 0 Then
                txtFirstName.Text = dt.Rows(0)("strFirstName").ToString()
                txtLastName.Text = dt.Rows(0)("strLastName").ToString()
                txtAddress.Text = dt.Rows(0)("strAddress").ToString()
                txtCity.Text = dt.Rows(0)("strCity").ToString()
                cboStates.SelectedValue = dt.Rows(0)("intStateID")
                txtZip.Text = dt.Rows(0)("strZip").ToString()
                txtPhone.Text = dt.Rows(0)("strPhoneNumber").ToString()
                txtEmail.Text = dt.Rows(0)("strEmail").ToString()
                txtLogin.Text = dt.Rows(0)("strPassengerLogin").ToString()
                txtPassword.Text = dt.Rows(0)("strPassword").ToString()
                txtDOB.Text = CDate(dt.Rows(0)("dtmPassengerDOB")).ToString("MM/dd/yyyy")
            Else
                MessageBox.Show("No passenger data found.")
            End If

            ' Clean up
            drSourceTable.Close()
            CloseDatabaseConnection()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim strFirstName As String
        Dim strLastName As String
        Dim strAddress As String
        Dim strCity As String
        Dim intState As Integer
        Dim strPhoneNumber As String
        Dim strEmail As String
        Dim strZip As String
        Dim strLogin As String
        Dim strPassword As String
        Dim dtmDOB As DateTime
        Dim intRowsAffected As Integer
        Dim blnValidated As Boolean = True
        Dim cmdUpdate As OleDb.OleDbCommand

        Call_Validation(blnValidated)

        If blnValidated Then
            Try
                ' Open database connection
                If OpenDatabaseConnectionSQLServer() = False Then
                    ' No, warn the user ...
                    MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                "The application will now close.",
                                Me.Text + " Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ' And close the form/application
                    Me.Close()
                End If

                strFirstName = txtFirstName.Text
                strLastName = txtLastName.Text
                strAddress = txtAddress.Text
                strCity = txtCity.Text
                intState = cboStates.SelectedValue
                strZip = txtZip.Text
                strEmail = txtEmail.Text
                strPhoneNumber = txtPhone.Text
                strLogin = txtLogin.Text
                strPassword = txtPassword.Text
                dtmDOB = DateTime.Parse(txtDOB.Text)

                ' Call the stored procedure to update passenger data
                cmdUpdate = New OleDb.OleDbCommand("uspUpdatePassenger", m_conAdministrator)
                cmdUpdate.CommandType = CommandType.StoredProcedure

                ' Add parameters for the stored procedure
                cmdUpdate.Parameters.Add(New OleDb.OleDbParameter("@passengerID", intPassenger))
                cmdUpdate.Parameters.Add(New OleDb.OleDbParameter("@strFirstName", strFirstName))
                cmdUpdate.Parameters.Add(New OleDb.OleDbParameter("@strLastName", strLastName))
                cmdUpdate.Parameters.Add(New OleDb.OleDbParameter("@strAddress", strAddress))
                cmdUpdate.Parameters.Add(New OleDb.OleDbParameter("@strCity", strCity))
                cmdUpdate.Parameters.Add(New OleDb.OleDbParameter("@intStateID", intState))
                cmdUpdate.Parameters.Add(New OleDb.OleDbParameter("@strZip", strZip))
                cmdUpdate.Parameters.Add(New OleDb.OleDbParameter("@strPhoneNumber", strPhoneNumber))
                cmdUpdate.Parameters.Add(New OleDb.OleDbParameter("@strEmail", strEmail))
                cmdUpdate.Parameters.Add(New OleDb.OleDbParameter("@strPassengerLogin", strLogin))
                cmdUpdate.Parameters.Add(New OleDb.OleDbParameter("@strPassword", strPassword))
                cmdUpdate.Parameters.Add(New OleDb.OleDbParameter("@dtmPassengerDOB", dtmDOB))

                ' Execute the command
                intRowsAffected = cmdUpdate.ExecuteNonQuery()

                ' Let the user know what happened 
                If intRowsAffected = 1 Then
                    MessageBox.Show("Update successful")
                Else
                    MessageBox.Show("Update failed")
                End If

                ' Close the database connection
                CloseDatabaseConnection()

                Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub


    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
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

    ' Validation Methods
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
            ' Check if the last name contains any numbers
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
            ' Check if the city name contains any numbers
            For Each intDigit As Char In "0123456789"
                If txtCity.Text.IndexOf(intDigit) <> -1 Then
                    MessageBox.Show("City name must not contain numbers.")
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
        Dim strPhone As String = txtPhone.Text

        If IsNumeric(strPhone) AndAlso strPhone.Length = 10 Then
            'Phone valid
        Else
            MessageBox.Show("Please enter a valid 10 digit phone number.  Do not include - ( or ).")
            txtPhone.ResetText()
            txtPhone.Focus()
            blnValidated = False
        End If
    End Sub

    Private Sub Validate_Email(ByRef blnValidated As Boolean)
        If txtEmail.Text = String.Empty Then
            MessageBox.Show("Email must exist.")
            txtEmail.Focus()
            blnValidated = False
        ElseIf txtEmail.Text.IndexOf("@") = -1 Then
            MessageBox.Show("Email must contain '@'.")
            txtEmail.Focus()
            blnValidated = False
        End If
    End Sub

    Private Sub Validate_State(ByRef blnValidated As Boolean)
        If cboStates.SelectedIndex = -1 Then
            MessageBox.Show("Please select a State from the list.")
            blnValidated = False
        End If
    End Sub

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
End Class
