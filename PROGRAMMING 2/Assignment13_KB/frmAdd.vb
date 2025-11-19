Public Class frmAdd
    Private Sub frmAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        '  On the event Form Load, we are going to populate the comboboxes of City, State, and Race from 
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
        Dim strSelect As String
        Dim strInsert As String
        Dim strFirstName As String
        Dim strLastName As String
        Dim strAddress As String
        Dim strCity As String
        Dim intState As Integer
        Dim strZip As String
        Dim strPhone As String
        Dim strEmail As String

        Dim blnValidated As Boolean = True

        Dim cmdSelect As OleDb.OleDbCommand ' select command object
        Dim cmdInsert As OleDb.OleDbCommand ' insert command object
        Dim drSourceTable As OleDb.OleDbDataReader ' data reader for pulling info
        Dim intNextPrimaryKey As Integer ' holds next highest PK value
        Dim intRowsAffected As Integer  ' how many rows were affected when sql executed

        Call_Validation(blnValidated)

        If blnValidated Then
            Try
                strFirstName = txtFirstName.Text
                strLastName = txtLastName.Text
                strAddress = txtAddress.Text
                strCity = txtCity.Text
                intState = cboStates.SelectedValue
                strZip = txtZip.Text
                strPhone = txtPhone.Text
                strEmail = txtEmail.Text

                If OpenDatabaseConnectionSQLServer() = False Then

                    ' No, warn the user ...
                    MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                        "The application will now close.",
                                        Me.Text + " Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error)

                    ' and close the form/application
                    Me.Close()

                End If

                strSelect = "SELECT MAX(intPassengerID) + 1 AS intNextPrimaryKey " &
                                " FROM TPassengers"

                ' Execute command
                cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
                drSourceTable = cmdSelect.ExecuteReader

                ' Read result( highest ID )
                drSourceTable.Read()

                ' Null? (empty table)
                If drSourceTable.IsDBNull(0) = True Then

                    ' Yes, start numbering at 1
                    intNextPrimaryKey = 1

                Else

                    ' No, get the next highest ID
                    intNextPrimaryKey = CInt(drSourceTable("intNextPrimaryKey"))

                End If
                ' build insert statement (columns must match DB columns in name and the # of columns)

                strInsert = "INSERT INTO TPassengers (intPassengerID, strFirstName, strLastName, strAddress, strCity, intStateID, strZip, strPhoneNumber, strEmail) " &
                            "VALUES (" & intNextPrimaryKey & ", '" & strFirstName & "', '" & strLastName & "', '" & strAddress & "', '" & strCity & "', " & intState & ", '" & strZip & "', '" & strPhone & "', '" & strEmail & "')"

                'MessageBox.Show(strInsert)

                ' use insert command with sql string and connection object
                cmdInsert = New OleDb.OleDbCommand(strInsert, m_conAdministrator)

                ' execute query to insert data
                intRowsAffected = cmdInsert.ExecuteNonQuery()

                ' If not 0 insert successful
                If intRowsAffected > 0 Then
                    MessageBox.Show("Passenger has been added")    ' let user know success
                    ' close new player form
                End If


                CloseDatabaseConnection()       ' close connection if insert didn't work
                Close()



            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

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
                                    Validate_Email(blnValidated)
                                End If
                                If blnValidated Then
                                    Validate_State(blnValidated)
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