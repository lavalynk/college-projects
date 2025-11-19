Public Class frmPilotUpdate

    Private Sub frmPilotUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmdSelect As OleDb.OleDbCommand ' This will be used for our Select statement
        Dim drSourceTable As OleDb.OleDbDataReader ' This will be where our data is retrieved to
        Dim dts As DataTable = New DataTable ' This is the table we will load from our reader for Pilot Roles

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

            ' Call the stored procedure to get pilot roles
            cmdSelect = New OleDb.OleDbCommand("uspGetPilotRoles", m_conAdministrator)
            cmdSelect.CommandType = CommandType.StoredProcedure

            ' Retrieve all the records 
            drSourceTable = cmdSelect.ExecuteReader
            dts.Load(drSourceTable)

            ' Load the Pilot Role result set into the combobox
            cboPilotRole.ValueMember = "intPilotRoleID"
            cboPilotRole.DisplayMember = "strPilotRole"
            cboPilotRole.DataSource = dts

            ' Clean up
            drSourceTable.Close()

            ' Load pilot data into the text fields
            LoadPilotData()

            ' Close the database connection
            CloseDatabaseConnection()

        Catch excError As Exception
            ' Log and display error message
            MessageBox.Show(excError.Message)
        End Try
    End Sub

    Private Sub LoadPilotData()
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

            ' Call the stored procedure to get pilot data
            cmdSelect = New OleDb.OleDbCommand("uspGetPilotData", m_conAdministrator)
            cmdSelect.CommandType = CommandType.StoredProcedure

            ' Add the parameter for the stored procedure
            objParam = cmdSelect.Parameters.Add("@pilotID", OleDb.OleDbType.Integer)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = intPilot

            ' Retrieve the record
            drSourceTable = cmdSelect.ExecuteReader
            dt.Load(drSourceTable)

            ' Populate text fields with pilot data
            If dt.Rows.Count > 0 Then
                txtFirstName.Text = dt.Rows(0)("strFirstName").ToString()
                txtLastName.Text = dt.Rows(0)("strLastName").ToString()
                cboPilotRole.SelectedValue = dt.Rows(0)("intPilotRoleID").ToString()
                txtEmployeeID.Text = dt.Rows(0)("strEmployeeID").ToString()
                txtDateOfHire.Text = CDate(dt.Rows(0)("dtmDateofHire")).ToString("MM/dd/yyyy")
                txtDateOfTermination.Text = CDate(dt.Rows(0)("dtmDateofTermination")).ToString("MM/dd/yyyy")
                txtDateOfLicense.Text = CDate(dt.Rows(0)("dtmDateofLicense")).ToString("MM/dd/yyyy")
                txtLoginID.Text = dt.Rows(0)("strEmployeeLoginID").ToString()
                txtPassword.Text = dt.Rows(0)("strEmployeePassword").ToString()
            Else
                MessageBox.Show("No pilot data found.")
            End If

            ' Clean up
            drSourceTable.Close()
            CloseDatabaseConnection()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Call_Validation(ByRef blnValidated As Boolean)
        If blnValidated Then
            Validate_LoginID(blnValidated)
            If blnValidated Then
                Validate_Password(blnValidated)
                If blnValidated Then
                    Validate_FirstName(blnValidated)
                    If blnValidated Then
                        Validate_LastName(blnValidated)
                        If blnValidated Then
                            Validate_EmployeeID(blnValidated)
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
        End If
    End Sub

    Private Sub Validate_Password(ByRef blnValidated As Boolean)
        txtPassword.Text = txtPassword.Text.Trim()

        If txtPassword.Text = String.Empty Then
            MessageBox.Show("Password must exist.")
            txtPassword.Focus()
            blnValidated = False
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim strFirstName As String
        Dim strLastName As String
        Dim strEmployeeID As String
        Dim dtmDateOfHire As Date
        Dim dtmDateOfTermination As Date
        Dim dtmDateOfLicense As Date
        Dim intPilotRoleID As Integer
        Dim strLoginID As String = txtLoginID.Text
        Dim strPassword As String = txtPassword.Text
        Dim blnValidated As Boolean = True

        Dim cmdUpdatePilot As New System.Data.OleDb.OleDbCommand()

        Try
            ' Validate data is entered
            Call_Validation(blnValidated)

            If blnValidated Then
                ' Assign values to variables
                strFirstName = txtFirstName.Text
                strLastName = txtLastName.Text
                strEmployeeID = txtEmployeeID.Text
                dtmDateOfHire = DateTime.Parse(txtDateOfHire.Text)
                dtmDateOfTermination = DateTime.Parse(txtDateOfTermination.Text)
                dtmDateOfLicense = DateTime.Parse(txtDateOfLicense.Text)

                ' Determine intPilotRoleID based on selected text
                Select Case cboPilotRole.Text
                    Case "Co-Pilot"
                        intPilotRoleID = 1
                    Case "Captain"
                        intPilotRoleID = 2
                    Case Else
                        MessageBox.Show("Unknown role selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                End Select

                ' Open the database connection
                If OpenDatabaseConnectionSQLServer() = False Then
                    MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                "The application will now close.",
                                Me.Text + " Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Me.Close()
                End If

                ' Prepare the SQL command
                cmdUpdatePilot = New System.Data.OleDb.OleDbCommand("uspUpdatePilot", m_conAdministrator)
                cmdUpdatePilot.CommandType = System.Data.CommandType.StoredProcedure

                ' Add parameters
                cmdUpdatePilot.Parameters.Add(New System.Data.OleDb.OleDbParameter("@intPilotID", intPilot))
                cmdUpdatePilot.Parameters.Add(New System.Data.OleDb.OleDbParameter("@strFirstName", strFirstName))
                cmdUpdatePilot.Parameters.Add(New System.Data.OleDb.OleDbParameter("@strLastName", strLastName))
                cmdUpdatePilot.Parameters.Add(New System.Data.OleDb.OleDbParameter("@strEmployeeID", strEmployeeID))
                cmdUpdatePilot.Parameters.Add(New System.Data.OleDb.OleDbParameter("@dtmDateOfHire", dtmDateOfHire))
                cmdUpdatePilot.Parameters.Add(New System.Data.OleDb.OleDbParameter("@dtmDateOfTermination", dtmDateOfTermination))
                cmdUpdatePilot.Parameters.Add(New System.Data.OleDb.OleDbParameter("@dtmDateOfLicense", dtmDateOfLicense))
                cmdUpdatePilot.Parameters.Add(New System.Data.OleDb.OleDbParameter("@intPilotRoleID", intPilotRoleID))
                cmdUpdatePilot.Parameters.Add(New System.Data.OleDb.OleDbParameter("@strEmployeeLoginID", strLoginID))
                cmdUpdatePilot.Parameters.Add(New System.Data.OleDb.OleDbParameter("@strEmployeePassword", strPassword))

                ' Execute the command
                cmdUpdatePilot.ExecuteNonQuery()

                ' Close database connection
                CloseDatabaseConnection()

                ' Provide feedback to the user without relying on the row count
                MessageBox.Show("Update successful. Pilot " & strFirstName & " " & strLastName & " has been updated.")

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
