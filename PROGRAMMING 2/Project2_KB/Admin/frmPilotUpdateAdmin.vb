Public Class frmPilotUpdateAdmin

    Private Sub frmPilotUpdateAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set intPilot to 0 initially
        intPilot = 0

        Try
            ' Open the database connection
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                "The application will now close.",
                                Me.Text + " Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
                Return
            End If

            ' Load the Pilot Selection combo box
            Dim cmdSelectPilots As New OleDb.OleDbCommand("SELECT intPilotID, strFirstName + ' ' + strLastName AS strPilotName FROM TPilots", m_conAdministrator)
            cmdSelectPilots.CommandType = CommandType.Text

            ' Ensure the connection is explicitly set
            cmdSelectPilots.Connection = m_conAdministrator

            Dim drSourcePilots As OleDb.OleDbDataReader = cmdSelectPilots.ExecuteReader()
            Dim dtPilots As DataTable = New DataTable
            dtPilots.Load(drSourcePilots)

            ' Insert a blank row to represent no selection initially
            Dim newRow As DataRow = dtPilots.NewRow()
            newRow("intPilotID") = 0
            newRow("strPilotName") = "-- Select a Pilot --"
            dtPilots.Rows.InsertAt(newRow, 0)

            cboPilotSelection.ValueMember = "intPilotID"
            cboPilotSelection.DisplayMember = "strPilotName"
            cboPilotSelection.DataSource = dtPilots
            btnUpdate.Enabled = False

            drSourcePilots.Close()

            ' Load the Pilot Role combo box
            Dim cmdSelectRoles As New OleDb.OleDbCommand("SELECT intPilotRoleID, strPilotRole FROM TPilotRoles", m_conAdministrator)
            cmdSelectRoles.CommandType = CommandType.Text

            ' Ensure the connection is explicitly set
            cmdSelectRoles.Connection = m_conAdministrator

            Dim drSourceRoles As OleDb.OleDbDataReader = cmdSelectRoles.ExecuteReader()
            Dim dtRoles As DataTable = New DataTable
            dtRoles.Load(drSourceRoles)

            cboPilotRole.ValueMember = "intPilotRoleID"
            cboPilotRole.DisplayMember = "strPilotRole"
            cboPilotRole.DataSource = dtRoles

            drSourceRoles.Close()

        Catch excError As Exception
            MessageBox.Show(excError.Message)
        Finally
            ' Close the database connection
            CloseDatabaseConnection()
        End Try
    End Sub

    Private Sub cboPilotSelection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPilotSelection.SelectedIndexChanged
        ' Update intPilot with the selected pilot ID
        intPilot = CInt(cboPilotSelection.SelectedValue)

        ' If a valid pilot is selected (not the initial "-- Select a Pilot --")
        If intPilot > 0 Then
            LoadPilotData()
            btnUpdate.Enabled = True
        Else
            ' Clear the form if no valid pilot is selected
            ClearForm()
        End If
    End Sub

    Private Sub LoadPilotData()
        Dim cmdSelect As OleDb.OleDbCommand
        Dim drSourceTable As OleDb.OleDbDataReader
        Dim dt As DataTable = New DataTable

        Try
            ' Open the database connection
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                "The application will now close.",
                                Me.Text + " Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
                Return
            End If

            ' Retrieve the pilot data for the selected pilot
            cmdSelect = New OleDb.OleDbCommand("uspGetPilotData", m_conAdministrator)
            cmdSelect.CommandType = CommandType.StoredProcedure
            cmdSelect.Parameters.Add(New OleDb.OleDbParameter("@pilotID", intPilot))

            drSourceTable = cmdSelect.ExecuteReader()
            dt.Load(drSourceTable)

            ' Populate the form fields with pilot data
            If dt.Rows.Count > 0 Then
                txtFirstName.Text = dt.Rows(0)("strFirstName").ToString()
                txtLastName.Text = dt.Rows(0)("strLastName").ToString()
                cboPilotRole.SelectedValue = dt.Rows(0)("intPilotRoleID")
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

    Private Sub ClearForm()
        txtFirstName.Clear()
        txtLastName.Clear()
        txtEmployeeID.Clear()
        txtDateOfHire.Clear()
        txtDateOfTermination.Clear()
        txtDateOfLicense.Clear()
        txtLoginID.Clear()
        txtPassword.Clear()
        cboPilotRole.SelectedIndex = -1
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        ' Variables for updated pilot data
        Dim strFirstName As String
        Dim strLastName As String
        Dim strEmployeeID As String
        Dim dtmDateOfHire As Date
        Dim dtmDateOfTermination As Date
        Dim dtmDateOfLicense As Date
        Dim intPilotRoleID As Integer
        Dim strLoginID As String = txtLoginID.Text ' Login ID
        Dim strPassword As String = txtPassword.Text ' Password
        Dim intRowsAffected As Integer
        Dim blnValidated As Boolean = True

        Dim cmdUpdatePilot As New System.Data.OleDb.OleDbCommand()

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

                ' Prepare the SQL command
                cmdUpdatePilot = New System.Data.OleDb.OleDbCommand("uspUpdatePilots", m_conAdministrator)
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
                intRowsAffected = cmdUpdatePilot.ExecuteNonQuery()

                ' Close database connection
                CloseDatabaseConnection()

                ' Inform the user about the result
                If intRowsAffected > 0 Then
                    MessageBox.Show("Update successful. Pilot " & strFirstName & " " & strLastName & " has been updated.")
                Else
                    MessageBox.Show("Update failed")
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
                                    If blnValidated Then
                                        Validate_LoginID(blnValidated)
                                        If blnValidated Then
                                            Validate_Password(blnValidated)
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

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

End Class
