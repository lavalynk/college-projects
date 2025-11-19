Public Class frmAddFutureFlight
    Dim intDepAirportID As Integer = 0
    Dim intArrAirportID As Integer = 0

    Private Sub frmAddFutureFlight_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulateAirports()
    End Sub

    Private Sub PopulateAirports()
        Dim strSelect As String = "SELECT intAirportID, strAirportCode FROM TAirports"
        Dim cmdSelect As OleDb.OleDbCommand
        Dim drSourceTable As OleDb.OleDbDataReader
        Dim dtAirports As DataTable = New DataTable

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

            ' Execute the select statement
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader
            dtAirports.Load(drSourceTable)

            ' Bind the data to the combo boxes
            cboDeparting.ValueMember = "intAirportID"
            cboDeparting.DisplayMember = "strAirportCode"
            cboDeparting.DataSource = dtAirports.Copy()

            cboArriving.ValueMember = "intAirportID"
            cboArriving.DisplayMember = "strAirportCode"
            cboArriving.DataSource = dtAirports.Copy()

            ' Clean up
            drSourceTable.Close()
            CloseDatabaseConnection()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub cboDeparting_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDeparting.SelectedIndexChanged
        If cboDeparting.SelectedValue IsNot Nothing Then
            FetchAirportID(cboDeparting.Text, True)
        End If
    End Sub

    Private Sub cboArriving_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboArriving.SelectedIndexChanged
        If cboArriving.SelectedValue IsNot Nothing Then
            FetchAirportID(cboArriving.Text, False)
        End If
    End Sub

    Private Sub FetchAirportID(strAirportCode As String, isDeparting As Boolean)
        Dim strSelect As String = "SELECT intAirportID FROM TAirports WHERE strAirportCode = '" & strAirportCode & "'"
        Dim cmdSelect As OleDb.OleDbCommand
        Dim drSourceTable As OleDb.OleDbDataReader

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

            ' Execute the select statement
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader()

            ' Set the corresponding airport ID
            If drSourceTable.Read() Then
                If isDeparting Then
                    intDepAirportID = Convert.ToInt32(drSourceTable("intAirportID"))
                Else
                    intArrAirportID = Convert.ToInt32(drSourceTable("intAirportID"))
                End If
            End If

            ' Clean up
            drSourceTable.Close()
            CloseDatabaseConnection()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Validations  
    Private Sub Validate_FlightDate(ByRef blnValidated As Boolean)
        txtFlightDate.Text = txtFlightDate.Text.Trim()

        ' Check if the date is empty or not a valid date
        If txtFlightDate.Text = String.Empty OrElse Not IsDate(txtFlightDate.Text) Then
            MessageBox.Show("Valid Flight Date must exist. Format: MM/DD/YYYY")
            txtFlightDate.Focus()
            blnValidated = False
        Else
            Dim dtmFlightDate As DateTime
            If DateTime.TryParse(txtFlightDate.Text, dtmFlightDate) Then
                ' Ensure the flight date is not in the past
                If dtmFlightDate < DateTime.Now.Date Then
                    MessageBox.Show("Flight Date cannot be in the past.")
                    txtFlightDate.Focus()
                    blnValidated = False
                ElseIf dtmFlightDate.Year < DateTime.Now.Year Then
                    MessageBox.Show("Flight Date must be in the current year or later.")
                    txtFlightDate.Focus()
                    blnValidated = False
                End If
            Else
                MessageBox.Show("Invalid Date. Format: MM/DD/YYYY")
                txtFlightDate.Focus()
                blnValidated = False
            End If
        End If
    End Sub

    Private Sub Validate_FlightNumber(ByRef blnValidated As Boolean)
        txtFlightNumber.Text = txtFlightNumber.Text.Trim()

        ' Check if the flight number is empty
        If txtFlightNumber.Text = String.Empty Then
            MessageBox.Show("Flight Number is required.")
            txtFlightNumber.Focus()
            blnValidated = False
            Exit Sub
        End If

        ' Try to parse the flight number as an integer
        Dim flightNumber As Integer
        If Not Integer.TryParse(txtFlightNumber.Text, flightNumber) Then
            MessageBox.Show("Flight Number must be a numeric value.")
            txtFlightNumber.Focus()
            blnValidated = False
            Exit Sub
        End If

        ' Check if the flight number is exactly 3 digits
        If txtFlightNumber.Text.Length <> 3 Then
            MessageBox.Show("Flight Number must be exactly 3 digits (e.g., 123).")
            txtFlightNumber.Focus()
            blnValidated = False
            Exit Sub
        End If
    End Sub

    Private Sub Validate_TimeOfDeparture(ByRef blnValidated As Boolean)
        txtTimeofDeparture.Text = txtTimeofDeparture.Text.Trim()

        ' Check if the time of departure is empty
        If txtTimeofDeparture.Text = String.Empty Then
            MessageBox.Show("Time of Departure is required.")
            txtTimeofDeparture.Focus()
            blnValidated = False
            Exit Sub
        End If

        ' Split the input into hours and minutes
        Dim timeParts() As String = txtTimeofDeparture.Text.Split(":"c)

        ' Check if the input has exactly two parts (HH and MM)
        If timeParts.Length <> 2 Then
            MessageBox.Show("Time of Departure must be in the format HH:MM (e.g., 14:30).")
            txtTimeofDeparture.Focus()
            blnValidated = False
            Exit Sub
        End If

        Dim intHours As Integer
        Dim intMinutes As Integer

        ' Validate hours
        If Not Integer.TryParse(timeParts(0), intHours) Then
            MessageBox.Show("Invalid hours entered. Please enter a number between 00 and 23.")
            txtTimeofDeparture.Focus()
            blnValidated = False
            Exit Sub
        End If

        If intHours < 0 OrElse intHours > 23 Then
            MessageBox.Show("Hours must be between 00 and 23.")
            txtTimeofDeparture.Focus()
            blnValidated = False
            Exit Sub
        End If

        ' Validate minutes
        If Not Integer.TryParse(timeParts(1), intMinutes) Then
            MessageBox.Show("Invalid minutes entered. Please enter a number between 00 and 59.")
            txtTimeofDeparture.Focus()
            blnValidated = False
            Exit Sub
        End If

        If intMinutes < 0 OrElse intMinutes > 59 Then
            MessageBox.Show("Minutes must be between 00 and 59.")
            txtTimeofDeparture.Focus()
            blnValidated = False
            Exit Sub
        End If

        ' If everything is fine, set the validated flag to True
        blnValidated = True
    End Sub

    Private Sub Validate_TimeOfLanding(ByRef blnValidated As Boolean)
        txtTimeofLanding.Text = txtTimeofLanding.Text.Trim()

        ' Check if the time of landing is empty
        If txtTimeofLanding.Text = String.Empty Then
            MessageBox.Show("Time of Landing is required.")
            txtTimeofLanding.Focus()
            blnValidated = False
            Exit Sub
        End If

        ' Split the input into hours and minutes
        Dim intColonPosition As Integer = txtTimeofLanding.Text.IndexOf(":")
        Dim strHours As String = txtTimeofLanding.Text.Substring(0, intColonPosition)
        Dim strMinutes As String = txtTimeofLanding.Text.Substring(intColonPosition + 1)

        ' Validate hours
        Dim intHours As Integer
        If Not Integer.TryParse(strHours, intHours) Then
            MessageBox.Show("Invalid hours entered. Please enter a number between 00 and 23.")
            txtTimeofLanding.Focus()
            blnValidated = False
            Exit Sub
        End If

        If intHours < 0 OrElse intHours > 23 Then
            MessageBox.Show("Hours must be between 00 and 23.")
            txtTimeofLanding.Focus()
            blnValidated = False
            Exit Sub
        End If

        ' Validate minutes
        Dim intMinutes As Integer
        If Not Integer.TryParse(strMinutes, intMinutes) Then
            MessageBox.Show("Invalid minutes entered. Please enter a number between 00 and 59.")
            txtTimeofLanding.Focus()
            blnValidated = False
            Exit Sub
        End If

        If intMinutes < 0 OrElse intMinutes > 59 Then
            MessageBox.Show("Minutes must be between 00 and 59.")
            txtTimeofLanding.Focus()
            blnValidated = False
            Exit Sub
        End If

        ' If everything is fine, set the validated flag to True
        blnValidated = True
    End Sub

    Private Sub Validate_Airports(ByRef blnValidated As Boolean)
        ' Trim any whitespace and get the selected values
        cboDeparting.Text = cboDeparting.Text.Trim()
        cboArriving.Text = cboArriving.Text.Trim()

        ' Check if the departing airport is the same as the arriving airport
        If cboDeparting.Text = cboArriving.Text Then
            MessageBox.Show("Departing airport cannot be the same as the arriving airport.")
            cboDeparting.Focus() ' Set focus back to the departing airport selection
            blnValidated = False
        Else
            blnValidated = True
        End If
    End Sub

    Private Sub Validate_MilesFlown(ByRef blnValidated As Boolean)
        Dim intMilesFlown As Integer

        ' Trim any whitespace from the input
        txtMiles.Text = txtMiles.Text.Trim()

        ' Check if the Miles Flown field is empty
        If txtMiles.Text = String.Empty Then
            MessageBox.Show("Miles Flown is required.")
            txtMiles.Focus()
            blnValidated = False
            Exit Sub
        End If

        ' Validate that the input is a numeric value
        If Not Integer.TryParse(txtMiles.Text, intMilesFlown) Then
            MessageBox.Show("Miles Flown must be a numeric value and whole number.")
            txtMiles.Focus()
            blnValidated = False
            Exit Sub
        End If

        ' Optionally, you can add a check to ensure miles flown is positive
        If intMilesFlown <= 0 Then
            MessageBox.Show("Miles Flown must be a positive number.")
            txtMiles.Focus()
            blnValidated = False
            Exit Sub
        End If

        ' If everything is fine, set the validated flag to True
        blnValidated = True
    End Sub

    Private Sub Validate_PlaneID(ByRef blnValidated As Boolean)
        Dim intPlaneID As Integer

        ' Trim any whitespace from the input
        txtPlaneID.Text = txtPlaneID.Text.Trim()

        ' Check if the Plane ID field is empty
        If txtPlaneID.Text = String.Empty Then
            MessageBox.Show("Plane ID is required.")
            txtPlaneID.Focus()
            blnValidated = False
            Exit Sub
        End If

        ' Validate that the input is a numeric value
        If Not Integer.TryParse(txtPlaneID.Text, intPlaneID) Then
            MessageBox.Show("Plane ID must be a numeric value.")
            txtPlaneID.Focus()
            blnValidated = False
            Exit Sub
        End If

        ' Validate that the Plane ID is within the allowed range
        If intPlaneID < 1 OrElse intPlaneID > 6 Then
            MessageBox.Show("Plane ID must be one of the following: 1, 2, 3, 4, 5, or 6.")
            txtPlaneID.Focus()
            blnValidated = False
            Exit Sub
        End If

        ' If everything is fine, set the validated flag to True
        blnValidated = True
    End Sub
    Private Sub Call_Validation(ByRef blnValidated As Boolean)
        ' Start with validating the flight date
        Validate_FlightDate(blnValidated)
        If blnValidated = False Then Exit Sub

        ' Validate the flight number
        Validate_FlightNumber(blnValidated)
        If blnValidated = False Then Exit Sub

        ' Validate time of departure
        Validate_TimeOfDeparture(blnValidated)
        If blnValidated = False Then Exit Sub

        ' Validate time of landing
        Validate_TimeOfLanding(blnValidated)
        If blnValidated = False Then Exit Sub

        ' Validate that departing and arriving airports are not the same
        Validate_Airports(blnValidated)
        If blnValidated = False Then Exit Sub

        ' Validate miles flown
        Validate_MilesFlown(blnValidated)
        If blnValidated = False Then Exit Sub

        ' Validate Plane ID
        Validate_PlaneID(blnValidated)
        If blnValidated = False Then Exit Sub
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim blnValidated As Boolean = True

        ' Call the validation routine
        Call_Validation(blnValidated)

        If blnValidated Then
            Try
                ' Open the database connection
                If OpenDatabaseConnectionSQLServer() = False Then
                    MessageBox.Show("Database connection error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                ' Prepare the command to call the stored procedure
                Dim cmdInsert As New OleDb.OleDbCommand("uspInsertFlight", m_conAdministrator)
                cmdInsert.CommandType = CommandType.StoredProcedure

                ' Add parameters to the command
                cmdInsert.Parameters.AddWithValue("@FlightNumber", txtFlightNumber.Text.Trim())
                cmdInsert.Parameters.AddWithValue("@FlightDate", CDate(txtFlightDate.Text))
                cmdInsert.Parameters.AddWithValue("@TimeOfDeparture", TimeSpan.Parse(txtTimeofDeparture.Text))
                cmdInsert.Parameters.AddWithValue("@TimeOfLanding", TimeSpan.Parse(txtTimeofLanding.Text))
                cmdInsert.Parameters.AddWithValue("@DepartingAirportID", intDepAirportID)
                cmdInsert.Parameters.AddWithValue("@ArrivingAirportID", intArrAirportID)
                cmdInsert.Parameters.AddWithValue("@MilesFlown", Integer.Parse(txtMiles.Text))
                cmdInsert.Parameters.AddWithValue("@PlaneID", Integer.Parse(txtPlaneID.Text))

                ' Execute the command
                cmdInsert.ExecuteNonQuery()

                ' Close the database connection
                CloseDatabaseConnection()

                ' Provide feedback to the user
                MessageBox.Show("Flight successfully added to the database.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                ' Handle any errors that may have occurred
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                blnValidated = False
            Finally
                ' Ensure the connection is closed
                If m_conAdministrator IsNot Nothing AndAlso m_conAdministrator.State = ConnectionState.Open Then
                    CloseDatabaseConnection()
                End If
                If blnValidated = True Then
                    Me.Close()
                End If
            End Try
        End If

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
