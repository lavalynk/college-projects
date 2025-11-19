Public Class frmAttendantUpdate
    Private Sub frmAttendantUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

            ' Load attendant data into the text fields
            LoadAttendantData()

            ' Close the database connection
            CloseDatabaseConnection()

        Catch excError As Exception
            ' Log and display error message
            MessageBox.Show(excError.Message)
        End Try
    End Sub

    Private Sub LoadAttendantData()
        Dim strSelect As String
        Dim cmdSelect As OleDb.OleDbCommand
        Dim drSourceTable As OleDb.OleDbDataReader
        Dim dt As DataTable = New DataTable

        Try
            ' Build the select statement to get attendant data from both TAttendants and TEmployees
            strSelect = "SELECT A.intAttendantID, A.strFirstName, A.strLastName, A.strEmployeeID, " &
                    "A.dtmDateofHire, A.dtmDateofTermination, E.strEmployeeLoginID, E.strEmployeePassword, E.strEmployeeRole " &
                    "FROM TAttendants AS A " &
                    "JOIN TEmployees AS E ON A.intAttendantID = E.intEmployeeID " &
                    "WHERE A.intAttendantID = " & intAttendant & " AND E.strEmployeeRole = 'Attendant'"

            ' Retrieve the record
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader
            dt.Load(drSourceTable)

            ' Populate text fields with attendant data
            If dt.Rows.Count > 0 Then
                txtFirstName.Text = dt.Rows(0)("strFirstName").ToString()
                txtLastName.Text = dt.Rows(0)("strLastName").ToString()
                txtEmployeeID.Text = dt.Rows(0)("strEmployeeID").ToString()
                txtDateOfHire.Text = CDate(dt.Rows(0)("dtmDateofHire")).ToString("MM/dd/yyyy")
                txtDateOfTermination.Text = CDate(dt.Rows(0)("dtmDateofTermination")).ToString("MM/dd/yyyy")
                txtLoginID.Text = dt.Rows(0)("strEmployeeLoginID").ToString()
                txtPassword.Text = dt.Rows(0)("strEmployeePassword").ToString()
            Else
                MessageBox.Show("No attendant data found.")
            End If

            ' Clean up
            drSourceTable.Close()

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
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    ' Validation Checks
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

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim strSelectAttendant As String
        Dim strSelectEmployee As String
        Dim strFirstName As String
        Dim strLastName As String
        Dim strEmployeeID As String
        Dim dtmDateOfHire As DateTime
        Dim dtmDateOfTermination As DateTime
        Dim strLoginID As String = txtLoginID.Text
        Dim strPassword As String = txtPassword.Text
        Dim intRowsAffected As Integer
        Dim blnValidated As Boolean = True
        Dim cmdUpdateAttendant As OleDb.OleDbCommand
        Dim cmdUpdateEmployee As OleDb.OleDbCommand

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
                strEmployeeID = txtEmployeeID.Text
                dtmDateOfHire = DateTime.Parse(txtDateOfHire.Text)
                dtmDateOfTermination = DateTime.Parse(txtDateOfTermination.Text)

                ' Update the attendant details
                strSelectAttendant = "UPDATE TAttendants SET " &
                    "strFirstName = '" & strFirstName & "', " &
                    "strLastName = '" & strLastName & "', " &
                    "strEmployeeID = '" & strEmployeeID & "', " &
                    "dtmDateofHire = '" & dtmDateOfHire.ToString("yyyy-MM-dd") & "', " &
                    "dtmDateofTermination = '" & dtmDateOfTermination.ToString("yyyy-MM-dd") & "' " &
                    "WHERE intAttendantID = " & intAttendant

                ' Update the employee login details
                strSelectEmployee = "UPDATE TEmployees SET " &
                    "strEmployeeLoginID = '" & strLoginID & "', " &
                    "strEmployeePassword = '" & strPassword & "', " &
                    "strEmployeeRole = 'Attendant' " &
                    "WHERE intEmployeeID = " & intAttendant

                ' Make the connections
                cmdUpdateAttendant = New OleDb.OleDbCommand(strSelectAttendant, m_conAdministrator)
                cmdUpdateEmployee = New OleDb.OleDbCommand(strSelectEmployee, m_conAdministrator)

                ' Update the rows with execute the statements
                intRowsAffected = cmdUpdateAttendant.ExecuteNonQuery()
                intRowsAffected += cmdUpdateEmployee.ExecuteNonQuery()

                ' Let the user know what happened 
                If intRowsAffected >= 2 Then
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
End Class
