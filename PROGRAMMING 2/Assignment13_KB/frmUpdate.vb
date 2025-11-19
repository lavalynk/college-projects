Public Class frmUpdate
    Private Sub frmUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim strSelect As String = ""
        Dim cmdSelect As OleDb.OleDbCommand ' this will be used for our Select statement
        Dim drSourceTable As OleDb.OleDbDataReader ' this will be where our data is retrieved to
        Dim dt As DataTable = New DataTable ' this is the table we will load from our reader
        Dim dts As DataTable = New DataTable ' this is the table we will load from our reader for State

        Try
            ' loop through the textboxes and clear them in case they have data in them after a delete
            For Each cntrl As Control In Controls
                If TypeOf cntrl Is TextBox Then
                    cntrl.Text = String.Empty
                End If
            Next
            If OpenDatabaseConnectionSQLServer() = False Then

                ' No, warn the user ...
                MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                "The application will now close.",
                                Me.Text + " Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)

                ' and close the form/application
                Me.Close()

            End If

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


            ' Build the select statement
            strSelect = "SELECT intPassengerID, strFirstName + ' ' + strLastName as PassengerName FROM TPassengers"


            ' Retrieve all the records 
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader

            ' load table from data reader
            dt.Load(drSourceTable)

            ' Add the item to the combo box. We need the player ID associated with the name so 
            ' when we click on the name we can then use the ID to pull the rest of the players data.
            ' We are binding the column name to the combo box display and value members. 
            cboPassenger.ValueMember = "intPassengerID"
            cboPassenger.DisplayMember = "PassengerName"
            cboPassenger.DataSource = dt

            ' Select the first item in the list by default
            If cboPassenger.Items.Count > 0 Then cboPassenger.SelectedIndex = 0

            ' Clean up
            drSourceTable.Close()

            ' close the database connection
            CloseDatabaseConnection()

        Catch excError As Exception
            ' Log and display error message
            MessageBox.Show(excError.Message)
        End Try

    End Sub

    Private Sub cboPassenger_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPassenger.SelectedIndexChanged
        'this Sub() Is called anytime the selected item Is changed in the combo box.

        Dim strSelect As String = ""
        Dim strName As String = ""
        Dim cmdSelect As OleDb.OleDbCommand ' this will be used for our Select statement
        Dim drSourceTable As OleDb.OleDbDataReader ' this will be where our data is retrieved to
        Dim dt As DataTable = New DataTable ' this is the table we will load from our reader

        Try


            ' open the database this is in module
            If OpenDatabaseConnectionSQLServer() = False Then

                ' No, warn the user ...
                MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                    "The application will now close.",
                                    Me.Text + " Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)

                ' and close the form/application
                Me.Close()

            End If

            ' Build the select statement using PK from name selected
            strSelect = "SELECT strFirstName, strLastName, strAddress, strCity, intStateID, strZip, strPhoneNumber, strEmail " &
                        " FROM TPassengers Where intPassengerID = " & cboPassenger.SelectedValue

            ' Retrieve all the records 
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader

            drSourceTable.Read()


            ' populate the text boxes with the data
            txtFirstName.Text = drSourceTable("strFirstName")
            txtLastName.Text = drSourceTable("strLastName")
            txtAddress.Text = drSourceTable("strAddress")
            txtCity.Text = drSourceTable("strCity")
            cboStates.SelectedValue = drSourceTable("intStateID")
            txtZip.Text = drSourceTable("strZip")
            txtPhone.Text = drSourceTable("strPhoneNumber")
            txtEmail.Text = drSourceTable("strEmail")

            ' close the database connection
            CloseDatabaseConnection()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try



    End Sub
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim strSelect As String
        Dim strFirstName As String
        Dim strLastName As String
        Dim strAddress As String
        Dim strCity As String
        Dim intState As Integer
        Dim strPhoneNumber As String
        Dim strEmail As String
        Dim strZip As String
        Dim intRowsAffected As Integer
        Dim blnValidated As Boolean = True
        Dim cmdUpdate As OleDb.OleDbCommand
        Call_Validation(blnValidated)

        If blnValidated Then
            Try
                ' open database this is in module
                If OpenDatabaseConnectionSQLServer() = False Then
                    ' No, warn the user ...
                    MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                "The application will now close.",
                                Me.Text + " Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ' and close the form/application
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

                ' Build the select statement using PK from name selected
                strSelect = "UPDATE TPassengers SET " &
                        "strFirstName = '" & strFirstName & "', " &
                        "strLastName = '" & strLastName & "', " &
                        "strAddress = '" & strAddress & "', " &
                        "strCity = '" & strCity & "', " &
                        "intStateID = " & intState & ", " &
                        "strZip = '" & strZip & "', " &
                        "strPhoneNumber = '" & strPhoneNumber & "', " &
                        "strEmail = '" & strEmail & "' " &
                        "WHERE intPassengerID = " & cboPassenger.SelectedValue.ToString

                ' make the connection
                cmdUpdate = New OleDb.OleDbCommand(strSelect, m_conAdministrator)

                ' Update the row with execute the statement
                intRowsAffected = cmdUpdate.ExecuteNonQuery()

                ' have to let the user know what happened 
                If intRowsAffected = 1 Then
                    MessageBox.Show("Update successful")
                Else
                    MessageBox.Show("Update failed")
                End If

                ' close the database connection
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
                                    Validate_State(blnValidated)
                                End If
                                If blnValidated Then
                                    Validate_Email(blnValidated)
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If

    End Sub
    'Validation Checks
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
                    MessageBox.Show("First Name must not contain numbers.")
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
        If txtCity.Text = String.Empty Then
            MessageBox.Show("City Must Exist.")
            txtCity.Focus()
            blnValidated = False
        Else
            ' Check if the first name contains any numbers
            For Each intDigit As Char In "0123456789"
                If txtLastName.Text.IndexOf(intDigit) <> -1 Then
                    MessageBox.Show("First Name must not contain numbers.")
                    txtLastName.Focus()
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

        strPhone = txtPhone.Text

        If IsNumeric(strPhone) And strPhone.Length = 10 Then
            'Phone valid
        Else
            MessageBox.Show("Please enter a valid 10 digit phone number.  Do not include - ( or ).")
            txtPhone.ResetText()
            txtPhone.Focus()
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
        Else
            'cboState is Validated
        End If
    End Sub
End Class