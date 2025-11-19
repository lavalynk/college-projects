Public Class frmCustomerAddFlight
    Dim intFlightID As Integer = 0

    Private Sub frmCustomerAddFlight_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim strSelect As String = ""
        Dim cmdSelect As OleDb.OleDbCommand
        Dim drSourceTable As OleDb.OleDbDataReader
        Dim dt As DataTable = New DataTable

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

            ' Bind the result to the ComboBox but do not select any item
            cboFuture.ValueMember = "intFlightID"
            cboFuture.DisplayMember = "strFlightNumber"
            cboFuture.DataSource = dt
            cboFuture.SelectedIndex = -1  ' Ensure no flight is selected initially

            ' Hide radio buttons, labels, and the seat ComboBox initially
            radReservedSeat.Visible = False
            radDesignatedSeat.Visible = False
            lblReservedSeatCost.Visible = False
            lblDesignatedSeatCost.Visible = False
            cboSeat.Visible = False

            ' Close the database connection
            drSourceTable.Close()
            CloseDatabaseConnection()
            btnSubmit.Enabled = False
        Catch excError As Exception
            ' Log and display error message
            MessageBox.Show(excError.Message)
        End Try
    End Sub

    Private Sub cboFuture_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFuture.SelectedIndexChanged
        ' Check if a flight is selected
        If cboFuture.SelectedIndex <> -1 AndAlso cboFuture.SelectedValue IsNot Nothing Then
            ' Fetch intFlightID and update flight info label
            FetchFlightID()
            ' Load available seats for the selected flight
            LoadAvailableSeats()
            btnSubmit.Enabled = True
            ' Show radio buttons, cost labels, and seat label
            radReservedSeat.Visible = True
            radDesignatedSeat.Visible = True
            lblReservedSeatCost.Visible = True
            lblDesignatedSeatCost.Visible = True
            radReservedSeat.Checked = True
            cboSeat.Visible = True
            lblSeat.Text = "Seat:"
            CalculateCosts()
        Else
            ' Reset the class-level variable if no flight is selected
            intFlightID = 0
            lblFlightInfo.Text = String.Empty ' Clear the label if no flight is selected

            ' Hide the radio buttons and labels if no flight is selected
            radReservedSeat.Visible = False
            radDesignatedSeat.Visible = False
            lblReservedSeatCost.Visible = True
            lblDesignatedSeatCost.Visible = True
            cboSeat.Visible = False
            lblSeat.Text = ""
        End If
    End Sub

    Private Sub FetchFlightID()
        Dim strSelect As String = "SELECT F.intFlightID, A1.strAirportCode AS FromAirport, A2.strAirportCode AS ToAirport, F.dtmFlightDate " &
                              "FROM TFlights F " &
                              "JOIN TAirports A1 ON F.intFromAirportID = A1.intAirportID " &
                              "JOIN TAirports A2 ON F.intToAirportID = A2.intAirportID " &
                              "WHERE F.strFlightNumber = '" & cboFuture.Text & "'"
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

            ' Prepare the SQL statement
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)

            ' Execute the query
            drSourceTable = cmdSelect.ExecuteReader()

            ' Set the intFlightID to the value from the database and update the lblFlightInfo label
            If drSourceTable.Read() Then
                intFlightID = Convert.ToInt32(drSourceTable("intFlightID"))
                Dim fromAirport As String = drSourceTable("FromAirport").ToString()
                Dim toAirport As String = drSourceTable("ToAirport").ToString()
                Dim flightDate As DateTime = Convert.ToDateTime(drSourceTable("dtmFlightDate"))

                ' Update the lblFlightInfo label with the flight details
                lblFlightInfo.Text = $"From: {fromAirport} To: {toAirport} Date: {flightDate.ToShortDateString()}"
                lblFlightInfo.TextAlign = ContentAlignment.MiddleCenter
            Else
                intFlightID = 0 ' Reset if not found
                lblFlightInfo.Text = String.Empty ' Clear the label if no flight is selected
            End If

            ' Clean up
            drSourceTable.Close()
            CloseDatabaseConnection()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub




    Private Sub radReservedSeat_CheckedChanged(sender As Object, e As EventArgs) Handles radReservedSeat.CheckedChanged
        If radReservedSeat.Checked Then
            ' Show the seat selection ComboBox
            radReservedSeat.Visible = True
            radDesignatedSeat.Visible = True
            lblReservedSeatCost.Visible = True
            lblDesignatedSeatCost.Visible = True
            cboSeat.Visible = True
            lblSeat.Text = "Seat:"
            LoadAvailableSeats()
        End If
    End Sub

    Private Sub radDesignatedSeat_CheckedChanged(sender As Object, e As EventArgs) Handles radDesignatedSeat.CheckedChanged
        If radDesignatedSeat.Checked Then
            ' Hide the seat selection ComboBox
            lblReservedSeatCost.Visible = True
            lblDesignatedSeatCost.Visible = True
            cboSeat.Visible = False
            lblSeat.Text = " "
        End If
    End Sub

    Private Sub LoadAvailableSeats()
        Dim strSelect As String
        Dim cmdSelect As OleDb.OleDbCommand
        Dim drSourceTable As OleDb.OleDbDataReader
        Dim dtSeats As DataTable = New DataTable

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

            ' Build the select statement to get available seats for the selected flight
            strSelect = "SELECT TS.intSeatID, TS.strSeat " &
                        "FROM TSeats TS " &
                        "WHERE TS.intSeatID NOT IN (SELECT FP.intSeatID FROM TFlightPassengers FP WHERE FP.intFlightID = " & intFlightID & ")"

            ' Execute the select statement
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader
            dtSeats.Load(drSourceTable)

            ' Bind the result to the ComboBox
            cboSeat.ValueMember = "intSeatID"
            cboSeat.DisplayMember = "strSeat"
            cboSeat.DataSource = dtSeats

            ' Clean up
            drSourceTable.Close()
            CloseDatabaseConnection()
        Catch excError As Exception
            ' Log and display error message
            MessageBox.Show(excError.Message)
        End Try
    End Sub

    Private Sub CalculateCosts()
        Dim dblBaseCost As Double = 250.0
        Dim dblReservedSeatCost As Double = 0
        Dim dblTotalMiles As Double = GetFlightMiles()
        Dim intPassengerCount As Integer = GetPassengerCount()
        Dim strPlaneType As String = GetPlaneType()
        Dim strDestination As String = GetDestination()
        Dim intPassengerAge As Integer = GetPassengerAge()
        Dim intPreviousFlights As Integer = GetPreviousFlightsCount()
        Dim dblDiscount As Double = 1.0

        ' Apply base adjustments
        If dblTotalMiles > 750 Then
            dblBaseCost += 50
        End If
        If intPassengerCount > 8 Then
            dblBaseCost += 100
        ElseIf intPassengerCount < 4 Then
            dblBaseCost -= 50
        End If
        If strPlaneType = "Airbus A350" Then
            dblBaseCost += 35
        ElseIf strPlaneType = "Boeing 747-8" Then
            dblBaseCost -= 25
        End If
        If strDestination = "MIA" Then
            dblBaseCost += 15
        End If

        ' Check if the reserved seat option is selected
        If radReservedSeat.Checked Then
            dblReservedSeatCost = 125
        End If

        ' Calculate the discount percentage
        If intPassengerAge >= 65 Then
            dblDiscount -= 0.2
        ElseIf intPassengerAge <= 5 Then
            dblDiscount -= 0.65
        End If

        If intPreviousFlights > 10 Then
            dblDiscount -= 0.2
        ElseIf intPreviousFlights > 5 Then
            dblDiscount -= 0.1
        End If

        ' Calculate the total costs for both options
        Dim dblTotalReservedSeatCost As Double = (dblBaseCost + dblReservedSeatCost) * dblDiscount
        Dim dblTotalDesignatedSeatCost As Double = dblBaseCost * dblDiscount

        ' Display the costs next to each radio button
        lblReservedSeatCost.Text = dblTotalReservedSeatCost.ToString("C2")
        lblDesignatedSeatCost.Text = dblTotalDesignatedSeatCost.ToString("C2")
    End Sub


    Private Function GetFlightMiles() As Integer
        Dim strSelect As String = "SELECT intMilesFlown FROM TFlights WHERE intFlightID = " & intFlightID
        Dim cmdSelect As OleDb.OleDbCommand
        Dim drSourceTable As OleDb.OleDbDataReader
        Dim intMiles As Integer = 0

        Try
            ' Open database connection
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return 0
            End If

            ' Check if the connection is open
            If m_conAdministrator Is Nothing OrElse m_conAdministrator.State <> ConnectionState.Open Then
                MessageBox.Show("Connection is not open.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return 0
            End If

            ' Prepare the select command
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)

            ' Execute the query
            drSourceTable = cmdSelect.ExecuteReader()

            ' Check if the query executed successfully
            If drSourceTable Is Nothing Then
                MessageBox.Show("No data returned by the query.", "Debug Info")
                Return 0
            End If

            ' Retrieve the miles
            If drSourceTable.Read() Then
                intMiles = Convert.ToInt32(drSourceTable("intMilesFlown"))
            Else
                MessageBox.Show("No miles found for Flight ID: " & intFlightID.ToString(), "Debug Info")
            End If

            ' Clean up
            drSourceTable.Close()
            CloseDatabaseConnection()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Exception")
        End Try

        Return intMiles
    End Function



    Private Function GetPassengerCount() As Integer
        Dim strSelect As String = "SELECT COUNT(intPassengerID) AS PassengerCount FROM TFlightPassengers WHERE intFlightID = " & intFlightID
        Dim cmdSelect As OleDb.OleDbCommand
        Dim drSourceTable As OleDb.OleDbDataReader
        Dim intPassengerCount As Integer = 0

        Try
            ' Open database connection
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return 0
            End If

            ' Prepare the select command
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)

            ' Execute the query
            drSourceTable = cmdSelect.ExecuteReader()

            If drSourceTable.Read() Then
                intPassengerCount = Convert.ToInt32(drSourceTable("PassengerCount"))
            End If

            ' Clean up
            drSourceTable.Close()
            CloseDatabaseConnection()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return intPassengerCount
    End Function

    Private Function GetPlaneType() As String
        Dim strSelect As String = "SELECT PT.strPlaneType FROM TPlanes P " &
                              "JOIN TPlaneTypes PT ON P.intPlaneTypeID = PT.intPlaneTypeID " &
                              "WHERE P.intPlaneID = (SELECT intPlaneID FROM TFlights WHERE intFlightID = " & intFlightID & ")"
        Dim cmdSelect As OleDb.OleDbCommand
        Dim drSourceTable As OleDb.OleDbDataReader
        Dim strPlaneType As String = String.Empty

        Try
            ' Open database connection
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return String.Empty
            End If

            ' Prepare the select command
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)

            ' Execute the query
            drSourceTable = cmdSelect.ExecuteReader()

            If drSourceTable.Read() Then
                strPlaneType = drSourceTable("strPlaneType").ToString()
            End If

            ' Clean up
            drSourceTable.Close()
            CloseDatabaseConnection()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return strPlaneType
    End Function

    Private Function GetDestination() As String
        Dim strSelect As String = "SELECT A.strAirportCode FROM TAirports A " &
                              "JOIN TFlights F ON A.intAirportID = F.intToAirportID " &
                              "WHERE F.intFlightID = " & intFlightID
        Dim cmdSelect As OleDb.OleDbCommand
        Dim drSourceTable As OleDb.OleDbDataReader
        Dim strDestination As String = String.Empty

        Try
            ' Open database connection
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return String.Empty
            End If

            ' Prepare the select command
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)

            ' Execute the query
            drSourceTable = cmdSelect.ExecuteReader()

            If drSourceTable.Read() Then
                strDestination = drSourceTable("strAirportCode").ToString()
            End If

            ' Clean up
            drSourceTable.Close()
            CloseDatabaseConnection()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return strDestination
    End Function

    Private Function GetPassengerAge() As Integer
        Dim strSelect As String = "SELECT FLOOR(DATEDIFF(DAY, dtmPassengerDOB, GETDATE()) / 365.25) AS Age " &
                              "FROM TPassengers WHERE intPassengerID = " & intPassenger
        Dim cmdSelect As OleDb.OleDbCommand
        Dim drSourceTable As OleDb.OleDbDataReader
        Dim intAge As Integer = 0

        Try
            ' Open database connection
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return 0
            End If

            ' Prepare the select command
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)

            ' Execute the query
            drSourceTable = cmdSelect.ExecuteReader()

            If drSourceTable.Read() Then
                intAge = Convert.ToInt32(drSourceTable("Age"))
            End If

            ' Clean up
            drSourceTable.Close()
            CloseDatabaseConnection()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return intAge
    End Function
    Private Function GetPreviousFlightsCount() As Integer
        Dim strSelect As String = "SELECT COUNT(*) AS PreviousFlights FROM TFlightPassengers FP " &
                              "JOIN TFlights F ON FP.intFlightID = F.intFlightID " &
                              "WHERE FP.intPassengerID = " & intPassenger & " AND F.dtmFlightDate < GETDATE()"
        Dim cmdSelect As OleDb.OleDbCommand
        Dim drSourceTable As OleDb.OleDbDataReader
        Dim intPreviousFlights As Integer = 0

        Try
            ' Open database connection
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return 0
            End If

            ' Prepare the select command
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)

            ' Execute the query
            drSourceTable = cmdSelect.ExecuteReader()

            If drSourceTable.Read() Then
                intPreviousFlights = Convert.ToInt32(drSourceTable("PreviousFlights"))
            End If

            ' Clean up
            drSourceTable.Close()
            CloseDatabaseConnection()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return intPreviousFlights
    End Function

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim flightPassengerID As Integer = GetNextFlightPassengerID()
        Dim flightID As Integer = intFlightID
        Dim passengerID As Integer = intPassenger
        Dim seatID As Integer
        Dim flightCost As Decimal

        ' Determine the selected seat and cost
        If radReservedSeat.Checked Then
            seatID = CType(cboSeat.SelectedValue, Integer)
            flightCost = Decimal.Parse(lblReservedSeatCost.Text, Globalization.NumberStyles.Currency)
        ElseIf radDesignatedSeat.Checked Then
            seatID = GetFirstAvailableSeatID()
            flightCost = Decimal.Parse(lblDesignatedSeatCost.Text, Globalization.NumberStyles.Currency)
        Else
            MessageBox.Show("Please select a seat option.")
            Return
        End If

        ' Confirm booking
        If MessageBox.Show("Are you sure you want to book this flight?", "Confirm Booking", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            InsertFlightPassenger(flightPassengerID, flightID, passengerID, seatID, flightCost)
            MessageBox.Show("Booking successful")
        End If

        Me.Close()
    End Sub

    Private Function GetFirstAvailableSeatID() As Integer
        Dim strSelect As String = "SELECT TOP 1 TS.intSeatID " &
                                  "FROM TSeats TS " &
                                  "WHERE TS.intSeatID NOT IN (SELECT FP.intSeatID FROM TFlightPassengers FP WHERE FP.intFlightID = " & intFlightID & ")"
        Dim cmdSelect As OleDb.OleDbCommand
        Dim drSourceTable As OleDb.OleDbDataReader
        Dim intSeatID As Integer = 0

        Try
            ' Open database connection
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return 0
            End If

            ' Prepare the select command
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)

            ' Execute the query
            drSourceTable = cmdSelect.ExecuteReader()

            If drSourceTable.Read() Then
                intSeatID = Convert.ToInt32(drSourceTable("intSeatID"))
            End If

            ' Clean up
            drSourceTable.Close()
            CloseDatabaseConnection()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return intSeatID
    End Function

    Private Sub InsertFlightPassenger(flightPassengerID As Integer, flightID As Integer, passengerID As Integer, seatID As Integer, flightCost As Decimal)
        Dim strInsert As String = "INSERT INTO TFlightPassengers (intFlightPassengerID, intFlightID, intPassengerID, intSeatID, decFlightCost) " &
                              "VALUES (" & flightPassengerID & ", " & flightID & ", " & passengerID & ", " & seatID & ", " & flightCost & ")"
        Dim cmdInsert As OleDb.OleDbCommand

        Try
            ' Open database connection
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Execute the insert command
            cmdInsert = New OleDb.OleDbCommand(strInsert, m_conAdministrator)
            cmdInsert.ExecuteNonQuery()

            ' Clean up
            CloseDatabaseConnection()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function GetNextFlightPassengerID() As Integer
        Dim strSelect As String = "SELECT ISNULL(MAX(intFlightPassengerID), 0) + 1 AS NextFlightPassengerID FROM TFlightPassengers"
        Dim cmdSelect As OleDb.OleDbCommand
        Dim drSourceTable As OleDb.OleDbDataReader
        Dim intNextID As Integer = 1

        Try
            ' Open database connection
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return intNextID
            End If

            ' Execute the select command
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader()

            If drSourceTable.Read() Then
                intNextID = Convert.ToInt32(drSourceTable("NextFlightPassengerID"))
            End If

            ' Clean up
            drSourceTable.Close()
            CloseDatabaseConnection()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return intNextID
    End Function

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
