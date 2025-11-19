Public Class frmStatistics
    Private Sub frmStatistics_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

            ' Total Number of Customers
            cmdSelect = New OleDb.OleDbCommand("uspGetTotalCustomers", m_conAdministrator)
            cmdSelect.CommandType = CommandType.StoredProcedure
            drSourceTable = cmdSelect.ExecuteReader()
            drSourceTable.Read()
            lblCustomers.Text = drSourceTable("TotalCustomers").ToString()
            drSourceTable.Close()

            ' Total Number of Flights Taken By All Customers
            cmdSelect = New OleDb.OleDbCommand("uspGetTotalFlights", m_conAdministrator)
            cmdSelect.CommandType = CommandType.StoredProcedure
            drSourceTable = cmdSelect.ExecuteReader()
            drSourceTable.Read()
            lblFlights.Text = drSourceTable("TotalFlights").ToString()
            drSourceTable.Close()

            ' Average Miles Flown For All Customers
            cmdSelect = New OleDb.OleDbCommand("uspGetAverageMiles", m_conAdministrator)
            cmdSelect.CommandType = CommandType.StoredProcedure
            drSourceTable = cmdSelect.ExecuteReader()
            drSourceTable.Read()
            lblAverage.Text = drSourceTable("AvgMiles").ToString()
            drSourceTable.Close()

            ' List Each Pilot and Their Total Miles Flown (excluding future flights, including pilots with no flights)
            cmdSelect = New OleDb.OleDbCommand("uspGetPilotsMiles", m_conAdministrator)
            cmdSelect.CommandType = CommandType.StoredProcedure
            drSourceTable = cmdSelect.ExecuteReader()
            lstPilots.Items.Clear()
            lstPilots.Items.Add("Pilots")
            lstPilots.Items.Add("======================================")
            While drSourceTable.Read()
                lstPilots.Items.Add(drSourceTable("PilotName") & " - " & drSourceTable("TotalMiles") & " miles")
            End While
            lstPilots.Items.Add("======================================")
            drSourceTable.Close()

            ' List Each Attendant and Their Total Miles Flown (excluding future flights, including attendants with no flights)
            cmdSelect = New OleDb.OleDbCommand("uspGetAttendantsMiles", m_conAdministrator)
            cmdSelect.CommandType = CommandType.StoredProcedure
            drSourceTable = cmdSelect.ExecuteReader()
            lstAttendants.Items.Clear()
            lstAttendants.Items.Add("Attendants")
            lstAttendants.Items.Add("======================================")
            While drSourceTable.Read()
                lstAttendants.Items.Add(drSourceTable("AttendantName") & " - " & drSourceTable("TotalMiles") & " miles")
            End While
            lstAttendants.Items.Add("======================================")
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
