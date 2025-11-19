Public Class frmPilotPastFlights
    Private Sub frmPilotPastFlights_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                    "JOIN TPilotFlights as TPF ON TF.intFlightID = TPF.intFlightID " &
                    "JOIN TPilots as TP ON TP.intPilotID = TPF.intPilotID " &
                    "WHERE TF.dtmFlightDate < CAST(GETDATE() AS DATE) " &
                    "AND TP.intPilotID = " & intPilot

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

            ' Build the select statement for past flights
            strSelect = "SELECT TF.intFlightID, TF.strFlightNumber, TF.dtmFlightDate, TF.dtmTimeofDeparture, TF.dtmTimeOfLanding, " &
                    "FA.strAirportCode AS strFromAirportCode, TA.strAirportCode AS strToAirportCode, TF.intMilesFlown, " &
                    "TPlanes.strPlaneNumber, TPlaneTypes.strPlaneType " &
                    "FROM TFlights as TF " &
                    "JOIN TPilotFlights as TPF ON TF.intFlightID = TPF.intFlightID " &
                    "JOIN TPilots as TP ON TP.intPilotID = TPF.intPilotID " &
                    "JOIN TAirports as FA ON FA.intAirportID = TF.intFromAirportID " &
                    "JOIN TAirports as TA ON TA.intAirportID = TF.intToAirportID " &
                    "JOIN TPlanes ON TPlanes.intPlaneID = TF.intPlaneID " &
                    "JOIN TPlaneTypes ON TPlaneTypes.intPlaneTypeID = TPlanes.intPlaneTypeID " &
                    "WHERE TF.dtmFlightDate < CAST(GETDATE() AS DATE) " &
                    "AND TP.intPilotID = " & intPilot & " " &
                    "GROUP BY TF.intFlightID, TF.strFlightNumber, TF.dtmFlightDate, TF.dtmTimeofDeparture, TF.dtmTimeOfLanding, " &
                    "FA.strAirportCode, TA.strAirportCode, TF.intMilesFlown, TPlanes.strPlaneNumber, TPlaneTypes.strPlaneType"

            ' Retrieve all the records 
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader()

            lstPastFlights.Items.Clear()

            lstPastFlights.Items.Add("=============================")

            While drSourceTable.Read()
                lstPastFlights.Items.Add("  ")
                lstPastFlights.Items.Add("Flight Number: " & vbTab & drSourceTable("strFlightNumber"))
                lstPastFlights.Items.Add("Flight Date: " & vbTab & drSourceTable("dtmFlightDate"))
                lstPastFlights.Items.Add("Time of Departure: " & vbTab & Format(CDate(drSourceTable("dtmTimeofDeparture")), "HH:mm"))
                lstPastFlights.Items.Add("Time of Landing: " & vbTab & Format(CDate(drSourceTable("dtmTimeOfLanding")), "HH:mm"))
                lstPastFlights.Items.Add("From Airport Code: " & vbTab & drSourceTable("strFromAirportCode"))
                lstPastFlights.Items.Add("To Airport Code: " & vbTab & drSourceTable("strToAirportCode"))
                lstPastFlights.Items.Add("Miles Flown: " & vbTab & drSourceTable("intMilesFlown"))
                lstPastFlights.Items.Add("Plane Number: " & vbTab & drSourceTable("strPlaneNumber"))
                lstPastFlights.Items.Add("Plane Type: " & vbTab & drSourceTable("strPlaneType"))
                lstPastFlights.Items.Add("  ")
                lstPastFlights.Items.Add("=============================")
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