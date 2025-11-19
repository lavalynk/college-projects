Public Class frmAttendantAddFutureFlights
    Private Sub frmAttendantAddFutureFlights_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim strSelect As String = ""
        Dim cmdSelect As OleDb.OleDbCommand
        Dim drSourceTable As OleDb.OleDbDataReader
        Dim dt As DataTable = New DataTable
        Dim dtAttendant As DataTable = New DataTable

        Try
            ' Open database connection
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                "The application will now close.",
                                Me.Text + " Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
                Return
            End If

            ' Build the select statement to get future flights
            strSelect = "SELECT intFlightID, strFlightNumber FROM TFlights WHERE dtmFlightDate > GETDATE()"

            ' Execute the select statement
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader
            dt.Load(drSourceTable)

            ' Bind the result to the ComboBox
            cboFuture.ValueMember = "intFlightID"
            cboFuture.DisplayMember = "strFlightNumber"
            cboFuture.DataSource = dt

            ' Clean up
            drSourceTable.Close()

            ' Build the select statement to get available attendants
            strSelect = "SELECT intAttendantID, strFirstName + ' ' + strLastName as AttendantName FROM TAttendants"

            ' Retrieve all the records 
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader

            ' Load table from data reader
            dtAttendant.Load(drSourceTable)

            ' Bind the result to the ComboBox
            cboAttendant.ValueMember = "intAttendantID"
            cboAttendant.DisplayMember = "AttendantName"
            cboAttendant.DataSource = dtAttendant

            CloseDatabaseConnection()

        Catch excError As Exception
            ' Log and display error message
            MessageBox.Show(excError.Message)
        End Try
    End Sub

    Private Function GetNextAttendantFlightID() As Integer
        Dim strSelect As String = "SELECT MAX(intAttendantFlightID) + 1 FROM TAttendantFlights"
        Dim cmdSelect As OleDb.OleDbCommand
        Dim drSourceTable As OleDb.OleDbDataReader
        Dim intNextID As Integer = 1

        Try
            ' Open database connection
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                "The application will now close.",
                                Me.Text + " Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
                Return intNextID
            End If

            ' Execute the select statement
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader

            If drSourceTable.Read() AndAlso Not IsDBNull(drSourceTable(0)) Then
                intNextID = drSourceTable(0)
            End If

            ' Clean up
            drSourceTable.Close()
            CloseDatabaseConnection()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return intNextID
    End Function

    Private Sub BookFlight(attendantFlightID As Integer, flightID As Integer, attendantID As Integer)
        Dim strInsert As String
        Dim cmdInsert As OleDb.OleDbCommand
        Dim intRowsAffected As Integer

        Try
            ' Open database connection
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                "The application will now close.",
                                Me.Text + " Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
                Return
            End If

            ' Build the insert statement
            strInsert = "INSERT INTO TAttendantFlights (intAttendantFlightID, intFlightID, intAttendantID) " &
                        "VALUES (" & attendantFlightID & ", " & flightID & ", " & attendantID & ")"

            ' Execute the insert statement
            cmdInsert = New OleDb.OleDbCommand(strInsert, m_conAdministrator)
            intRowsAffected = cmdInsert.ExecuteNonQuery()

            ' Provide feedback to the user
            If intRowsAffected = 1 Then
                MessageBox.Show("Booking successful")
            Else
                MessageBox.Show("Booking failed")
            End If

            ' Clean up
            CloseDatabaseConnection()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim intFlightID As Integer = CType(cboFuture.SelectedValue, Integer)
        Dim intAttendantID As Integer = CType(cboAttendant.SelectedValue, Integer)
        Dim intNextAttendantFlightID As Integer

        If MessageBox.Show("Are you sure you want to book this flight?", "Confirm Booking", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            intNextAttendantFlightID = GetNextAttendantFlightID()
            BookFlight(intNextAttendantFlightID, intFlightID, intAttendantID)
        End If

        Me.Close()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
