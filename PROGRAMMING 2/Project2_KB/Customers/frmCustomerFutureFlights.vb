Public Class frmCustomerFutureFlights
    Private Sub frmCustomerFutureFlights_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

            ' Build the select statement to get the total miles flown for future flights
            strSelect = "SELECT SUM(TF.intMilesFlown) AS TotalMilesFlown " &
                    "FROM TFlights as TF " &
                    "JOIN TFlightPassengers as TFP ON TF.intFlightID = TFP.intFlightID " &
                    "JOIN TPassengers as TP ON TP.intPassengerID = TFP.intPassengerID " &
                    "WHERE TF.dtmFlightDate >= CAST(GETDATE() AS DATE) " &
                    "AND TP.intPassengerID = " & intPassenger

            ' MessageBox.Show(strSelect) ' Uncomment this line to display the query for debugging

            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader()

            ' Read the result (total miles flown)
            drSourceTable.Read()

            ' Check for null (no matching rows)
            If drSourceTable.IsDBNull(0) Then
                ' No rows found, set total miles flown to 0
                lblMilesFlown.Text = "0"
            Else
                ' Rows found, set the total miles flown
                lblMilesFlown.Text = drSourceTable("TotalMilesFlown").ToString()
            End If

            ' Close the data reader
            drSourceTable.Close()

            ' Build the select statement for future flights
            strSelect = "SELECT TF.intFlightID, TF.strFlightNumber, TF.dtmFlightDate, TF.dtmTimeofDeparture, TF.dtmTimeOfLanding, " &
                        "FA.strAirportCode AS strFromAirportCode, TA.strAirportCode AS strToAirportCode, TF.intMilesFlown, TS.strSeat " &
                        "FROM TFlights as TF " &
                        "JOIN TFlightPassengers as TFP ON TF.intFlightID = TFP.intFlightID " &
                        "JOIN TPassengers as TP ON TP.intPassengerID = TFP.intPassengerID " &
                        "JOIN TAirports as FA ON FA.intAirportID = TF.intFromAirportID " &
                        "JOIN TAirports as TA ON TA.intAirportID = TF.intToAirportID " &
                        "JOIN TSeats as TS ON TFP.intSeatID = TS.intSeatID " &
                        "WHERE TF.dtmFlightDate >= CAST(GETDATE() AS DATE) " &
                        "AND TP.intPassengerID = " & intPassenger & " " &
                        "GROUP BY TF.intFlightID, TF.strFlightNumber, TF.dtmFlightDate, TF.dtmTimeofDeparture, TF.dtmTimeOfLanding, " &
                        "FA.strAirportCode, TA.strAirportCode, TF.intMilesFlown, TS.strSeat"

            ' Retrieve all the records 
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader()

            lstFutureFlights.Items.Clear()

            lstFutureFlights.Items.Add("=============================")

            While drSourceTable.Read()
                lstFutureFlights.Items.Add("  ")
                lstFutureFlights.Items.Add("Flight Number: " & vbTab & drSourceTable("strFlightNumber"))
                lstFutureFlights.Items.Add("Flight Date: " & vbTab & drSourceTable("dtmFlightDate"))
                lstFutureFlights.Items.Add("Time of Departure: " & vbTab & Format(CDate(drSourceTable("dtmTimeofDeparture")), "HH:mm"))
                lstFutureFlights.Items.Add("Time of Landing: " & vbTab & Format(CDate(drSourceTable("dtmTimeOfLanding")), "HH:mm"))
                lstFutureFlights.Items.Add("From Airport Code: " & vbTab & drSourceTable("strFromAirportCode"))
                lstFutureFlights.Items.Add("To Airport Code: " & vbTab & drSourceTable("strToAirportCode"))
                lstFutureFlights.Items.Add("Miles Flown: " & vbTab & drSourceTable("intMilesFlown"))
                lstFutureFlights.Items.Add("Seat: " & vbTab & vbTab & drSourceTable("strSeat"))
                lstFutureFlights.Items.Add("  ")
                lstFutureFlights.Items.Add("=============================")
            End While

            ' Close the data reader
            drSourceTable.Close()
            ' Close the database connection
            CloseDatabaseConnection()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class