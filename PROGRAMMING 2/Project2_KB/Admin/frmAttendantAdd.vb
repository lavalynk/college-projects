Public Class frmAttendantAdd

    Private Sub frmAttendantAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    ' Validation Checks (similar to existing validation methods)
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


    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim cmdInsert As OleDb.OleDbCommand
        Dim intRowsAffected As Integer
        Dim blnValidated As Boolean = True

        Call_Validation(blnValidated)

        If blnValidated Then
            Try
                ' Open database connection
                If OpenDatabaseConnectionSQLServer() = False Then
                    MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                "The application will now close.",
                                Me.Text + " Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Me.Close()
                End If

                ' Prepare the SQL command
                cmdInsert = New OleDb.OleDbCommand("uspAddAttendant", m_conAdministrator)
                cmdInsert.CommandType = CommandType.StoredProcedure

                ' Add parameters
                cmdInsert.Parameters.Add(New OleDb.OleDbParameter("@strFirstName", txtFirstName.Text))
                cmdInsert.Parameters.Add(New OleDb.OleDbParameter("@strLastName", txtLastName.Text))
                cmdInsert.Parameters.Add(New OleDb.OleDbParameter("@strEmployeeID", txtEmployeeID.Text))
                cmdInsert.Parameters.Add(New OleDb.OleDbParameter("@dtmDateOfHire", DateTime.Parse(txtDateOfHire.Text)))
                cmdInsert.Parameters.Add(New OleDb.OleDbParameter("@dtmDateOfTermination", DateTime.Parse(txtDateOfTermination.Text)))
                cmdInsert.Parameters.Add(New OleDb.OleDbParameter("@strLoginID", txtLoginID.Text))
                cmdInsert.Parameters.Add(New OleDb.OleDbParameter("@strPassword", txtPassword.Text))

                ' Execute the command
                intRowsAffected = cmdInsert.ExecuteNonQuery()

                ' Inform the user about the result
                If intRowsAffected > 0 Then
                    MessageBox.Show("Insert successful. Attendant has been added.")
                Else
                    MessageBox.Show("Insert failed.")
                End If

                ' Close the database connection
                CloseDatabaseConnection()

                ' Close the form
                Me.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub




    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

End Class
