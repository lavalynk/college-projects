Public Class frmAttendantFutureFlights
    Private Sub frmAttendantFutureFlights_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim strSelect As String = ""
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

            ' Build the select statement to get the total miles flown
            strSelect = "SELECT SUM(TF.intMilesFlown) AS TotalMilesFlown " &
                    "FROM TFlights as TF " &
                    "JOIN TAttendantFlights as TAF ON TF.intFlightID = TAF.intFlightID " &
                    "JOIN TAttendants as TA ON TA.intAttendantID = TAF.intAttendantID " &
                    "WHERE TF.dtmFlightDate >= CAST(GETDATE() AS DATE) " &
                    "AND TA.intAttendantID = " & intAttendant

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
                        "FromAirport.strAirportCode AS strFromAirportCode, TAIR.strAirportCode AS strToAirportCode, TF.intMilesFlown, " &
                        "TPlanes.strPlaneNumber, TPlaneTypes.strPlaneType " &
                        "FROM TFlights as TF " &
                        "JOIN TAttendantFlights as TAF ON TF.intFlightID = TAF.intFlightID " &
                        "JOIN TAttendants as TA ON TA.intAttendantID = TAF.intAttendantID " &
                        "JOIN TAirports as FromAirport ON FromAirport.intAirportID = TF.intFromAirportID " &
                        "JOIN TAirports as TAIR ON TAIR.intAirportID = TF.intToAirportID " &
                        "JOIN TPlanes ON TPlanes.intPlaneID = TF.intPlaneID " &
                        "JOIN TPlaneTypes ON TPlaneTypes.intPlaneTypeID = TPlanes.intPlaneTypeID " &
                        "WHERE TF.dtmFlightDate >= CAST(GETDATE() AS DATE) " &
                        "AND TA.intAttendantID = " & intAttendant & " " &
                        "GROUP BY TF.intFlightID, TF.strFlightNumber, TF.dtmFlightDate, TF.dtmTimeofDeparture, TF.dtmTimeOfLanding, " &
                        "FromAirport.strAirportCode, TAIR.strAirportCode, TF.intMilesFlown, TPlanes.strPlaneNumber, TPlaneTypes.strPlaneType"

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
                lstFutureFlights.Items.Add("Plane Number: " & vbTab & drSourceTable("strPlaneNumber"))
                lstFutureFlights.Items.Add("Plane Type: " & vbTab & drSourceTable("strPlaneType"))
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
