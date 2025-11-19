Public Class frmPilotAdd

    Private Sub frmPilotAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim strSelect As String = ""
        Dim cmdSelect As OleDb.OleDbCommand ' this will be used for our Select statement
        Dim drSourceTable As OleDb.OleDbDataReader ' this will be where our data is retrieved to
        Dim dt As DataTable = New DataTable ' this is the table we will load from our reader
        Dim dts As DataTable = New DataTable ' this is the table we will load from our reader for Pilot Roles

        Try
            If OpenDatabaseConnectionSQLServer() = False Then
                ' No, warn the user ...
                MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                    "The application will now close.",
                                    Me.Text + " Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                ' And close the form/application
                Me.Close()
            End If

            ' Build the select statement for pilot roles
            strSelect = "SELECT TPR.intPilotRoleID, TPR.strPilotRole FROM TPilotRoles as TPR"

            ' Retrieve all the records 
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader
            dts.Load(drSourceTable)

            ' Load the Pilot Role result set into the combobox
            cboPilotRole.ValueMember = "intPilotRoleID"
            cboPilotRole.DisplayMember = "strPilotRole"
            cboPilotRole.DataSource = dts

            ' Clean up
            drSourceTable.Close()

            ' Close the database connection
            CloseDatabaseConnection()

        Catch excError As Exception
            ' Log and display error message
            MessageBox.Show(excError.Message)
        End Try
    End Sub

    Private Sub Call_Validation(ByRef blnValidated As Boolean)
        If blnValidated Then
            Validate_LoginID(blnValidated)
            If blnValidated Then
                Validate_Password(blnValidated)
                If blnValidated Then
                    Validate_EmployeeID(blnValidated)
                    If blnValidated Then
                        Validate_FirstName(blnValidated)
                        If blnValidated Then
                            Validate_LastName(blnValidated)
                            If blnValidated Then
                                Validate_DateOfHire(blnValidated)
                                If blnValidated Then
                                    Validate_DateOfTermination(blnValidated)
                                    If blnValidated Then
                                        Validate_DateOfLicense(blnValidated)
                                        If blnValidated Then
                                            Validate_PilotRole(blnValidated)
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

    ' Validation Checks
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

    Private Sub Validate_EmployeeID(ByRef blnValidated As Boolean)
        txtEmployeeID.Text = txtEmployeeID.Text.Trim()

        If txtEmployeeID.Text = String.Empty Then
            MessageBox.Show("Employee ID must exist.")
            txtEmployeeID.Focus()
            blnValidated = False
        End If
    End Sub

    Private Sub Validate_DateOfHire(ByRef blnValidated As Boolean)
        txtDateOfHire.Text = txtDateOfHire.Text.Trim()

        If txtDateOfHire.Text = String.Empty OrElse Not IsDate(txtDateOfHire.Text) Then
            MessageBox.Show("Valid Date of Hire must exist. Format: MM/DD/YYYY")
            txtDateOfHire.Focus()
            blnValidated = False
            Return
        End If

        Dim dtmDateOfHire As DateTime
        If DateTime.TryParse(txtDateOfHire.Text, dtmDateOfHire) Then
            If dtmDateOfHire.Year <= 1900 Then
                MessageBox.Show("Date of Hire must be after 1900. Format: MM/DD/YYYY")
                txtDateOfHire.Focus()
                blnValidated = False
            End If
        Else
            MessageBox.Show("Invalid Date. Format: MM/DD/YYYY")
            txtDateOfHire.Focus()
            blnValidated = False
        End If
    End Sub

    Private Sub Validate_DateOfTermination(ByRef blnValidated As Boolean)
        txtDateOfTermination.Text = txtDateOfTermination.Text.Trim()

        If txtDateOfTermination.Text = String.Empty Then
            txtDateOfTermination.Text = "1/1/2099"
        ElseIf Not IsDate(txtDateOfTermination.Text) Then
            MessageBox.Show("Valid Date of Termination must be empty or a valid date. Format: MM/DD/YYYY")
            txtDateOfTermination.Focus()
            blnValidated = False
        End If
    End Sub

    Private Sub Validate_DateOfLicense(ByRef blnValidated As Boolean)
        txtDateOfLicense.Text = txtDateOfLicense.Text.Trim()

        If txtDateOfLicense.Text = String.Empty OrElse Not IsDate(txtDateOfLicense.Text) Then
            MessageBox.Show("Valid Date of License must exist. Format: MM/DD/YYYY")
            txtDateOfLicense.Focus()
            blnValidated = False
            Return
        End If

        Dim dtmDateOfLicense As DateTime
        If DateTime.TryParse(txtDateOfLicense.Text, dtmDateOfLicense) Then
            If dtmDateOfLicense.Year <= 1900 Then
                MessageBox.Show("Date of License must be after 1900. Format: MM/DD/YYYY")
                txtDateOfLicense.Focus()
                blnValidated = False
            End If
        Else
            MessageBox.Show("Invalid Date. Format: MM/DD/YYYY")
            txtDateOfLicense.Focus()
            blnValidated = False
        End If

    End Sub

    Private Sub Validate_PilotRole(ByRef blnValidated As Boolean)
        If cboPilotRole.SelectedIndex = -1 Then
            MessageBox.Show("Please select a Pilot Role from the list.")
            blnValidated = False
        End If
    End Sub

    Private Sub Validate_LoginID(ByRef blnValidated As Boolean)
        txtLoginID.Text = txtLoginID.Text.Trim()

        If txtLoginID.Text = String.Empty Then
            MessageBox.Show("Login ID must exist.")
            txtLoginID.Focus()
            blnValidated = False
        ElseIf txtLoginID.Text.Length < 5 Then ' Example constraint: must be at least 5 characters
            MessageBox.Show("Login ID must be at least 5 characters long.")
            txtLoginID.Focus()
            blnValidated = False
        End If
    End Sub

    Private Sub Validate_Password(ByRef blnValidated As Boolean)
        txtPassword.Text = txtPassword.Text.Trim()

        If txtPassword.Text = String.Empty Then
            MessageBox.Show("Password must exist.")
            txtPassword.Focus()
            blnValidated = False
        ElseIf txtPassword.Text.Length < 8 Then ' Example constraint: must be at least 8 characters
            MessageBox.Show("Password must be at least 8 characters long.")
            txtPassword.Focus()
            blnValidated = False
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        ' Variables for new pilot data
        Dim strFirstName As String
        Dim strLastName As String
        Dim strEmployeeID As String
        Dim dtmDateOfHire As Date
        Dim dtmDateOfTermination As Date
        Dim dtmDateOfLicense As Date
        Dim intPilotRoleID As Integer
        Dim intPKID As Integer
        Dim strLoginID As String = txtLoginID.Text
        Dim strPassword As String = txtPassword.Text
        Dim blnValidated As Boolean = True

        Dim cmdAddPilot As New System.Data.OleDb.OleDbCommand()
        Dim cmdAddEmployee As New System.Data.OleDb.OleDbCommand()
        Dim intRowsAffected As Integer  ' Number of rows affected when SQL executed

        Try
            ' Validate data is entered
            Call_Validation(blnValidated)

            If blnValidated Then
                ' Put values into variables
                strFirstName = txtFirstName.Text
                strLastName = txtLastName.Text
                strEmployeeID = txtEmployeeID.Text
                dtmDateOfHire = DateTime.Parse(txtDateOfHire.Text)
                dtmDateOfTermination = DateTime.Parse(txtDateOfTermination.Text)
                dtmDateOfLicense = DateTime.Parse(txtDateOfLicense.Text)
                intPilotRoleID = CInt(cboPilotRole.SelectedValue)

                If OpenDatabaseConnectionSQLServer() = False Then
                    ' No, warn the user ...
                    MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                    "The application will now close.",
                                    Me.Text + " Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)

                    ' and close the form/application
                    Me.Close()
                End If

                ' Prepare the SQL command for adding the pilot
                cmdAddPilot = New System.Data.OleDb.OleDbCommand("uspAddPilot", m_conAdministrator)
                cmdAddPilot.CommandType = System.Data.CommandType.StoredProcedure

                ' Add parameters for the pilot
                cmdAddPilot.Parameters.Add(New System.Data.OleDb.OleDbParameter("@intPilotID", System.Data.OleDb.OleDbType.Integer)).Direction = ParameterDirection.Output
                cmdAddPilot.Parameters.Add(New System.Data.OleDb.OleDbParameter("@strFirstName", strFirstName))
                cmdAddPilot.Parameters.Add(New System.Data.OleDb.OleDbParameter("@strLastName", strLastName))
                cmdAddPilot.Parameters.Add(New System.Data.OleDb.OleDbParameter("@strEmployeeID", strEmployeeID))
                cmdAddPilot.Parameters.Add(New System.Data.OleDb.OleDbParameter("@dtmDateOfHire", dtmDateOfHire))
                cmdAddPilot.Parameters.Add(New System.Data.OleDb.OleDbParameter("@dtmDateOfTermination", dtmDateOfTermination))
                cmdAddPilot.Parameters.Add(New System.Data.OleDb.OleDbParameter("@dtmDateOfLicense", dtmDateOfLicense))
                cmdAddPilot.Parameters.Add(New System.Data.OleDb.OleDbParameter("@intPilotRoleID", intPilotRoleID))

                ' Execute the command for adding the pilot
                intRowsAffected = cmdAddPilot.ExecuteNonQuery()

                ' Retrieve the output value
                intPKID = CInt(cmdAddPilot.Parameters("@intPilotID").Value)

                ' Prepare the SQL command for adding the employee
                cmdAddEmployee = New System.Data.OleDb.OleDbCommand("uspAddEmployee", m_conAdministrator)
                cmdAddEmployee.CommandType = System.Data.CommandType.StoredProcedure

                ' Add parameters for the employee
                cmdAddEmployee.Parameters.Add(New System.Data.OleDb.OleDbParameter("@intEmployeeKeyID", System.Data.OleDb.OleDbType.Integer)).Direction = ParameterDirection.Output
                cmdAddEmployee.Parameters.Add(New System.Data.OleDb.OleDbParameter("@strEmployeeLoginID", strLoginID))
                cmdAddEmployee.Parameters.Add(New System.Data.OleDb.OleDbParameter("@strEmployeePassword", strPassword))
                cmdAddEmployee.Parameters.Add(New System.Data.OleDb.OleDbParameter("@strEmployeeRole", "Pilot"))
                cmdAddEmployee.Parameters.Add(New System.Data.OleDb.OleDbParameter("@intEmployeeID", intPKID))

                ' Execute the command for adding the employee
                intRowsAffected = cmdAddEmployee.ExecuteNonQuery()

                ' Retrieve the output value for employee (not used here, but can be if needed)
                Dim intEmployeeKeyID As Integer = CInt(cmdAddEmployee.Parameters("@intEmployeeKeyID").Value)

                ' Close database connection
                CloseDatabaseConnection()

                ' Inform the user about the result
                If intPKID > 0 And intEmployeeKeyID > 0 Then
                    MessageBox.Show("Insert successful. Pilot " & strFirstName & " " & strLastName & " has been added with Pilot ID: " & intPKID)
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

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

End Class
