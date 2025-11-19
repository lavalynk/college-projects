Public Class frmPassengerLogin
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim frmCustomerAddPassenger As New frmCustomerAddPassenger

        frmCustomerAddPassenger.ShowDialog()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim cmdValidateLogin As System.Data.OleDb.OleDbCommand
        Dim drSourceTable As System.Data.OleDb.OleDbDataReader
        Dim strLoginID As String = txtLogin.Text
        Dim strPassword As String = txtPassword.Text
        Dim dt As DataTable = New DataTable

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

            ' Call the stored procedure to validate the login
            cmdValidateLogin = New System.Data.OleDb.OleDbCommand("uspValidatePassengerLogin", m_conAdministrator)
            cmdValidateLogin.CommandType = CommandType.StoredProcedure

            ' Add parameters for the stored procedure
            cmdValidateLogin.Parameters.Add(New System.Data.OleDb.OleDbParameter("@PassengerLoginID", strLoginID))
            cmdValidateLogin.Parameters.Add(New System.Data.OleDb.OleDbParameter("@PassengerPassword", strPassword))

            ' Execute the command
            drSourceTable = cmdValidateLogin.ExecuteReader
            dt.Load(drSourceTable)

            ' Check if any rows are returned
            If dt.Rows.Count > 0 Then
                ' Successful login
                intPassenger = CInt(dt.Rows(0)("intPassengerID"))
                MessageBox.Show("Login successful! Passenger ID: " & intPassenger)

                ' Close the database connection
                CloseDatabaseConnection()

                ' Navigate to the Passenger Main Menu form
                Dim frmCustomerMain As New frmCustomerMain
                frmCustomerMain.ShowDialog()
                Me.Close()
            Else
                ' Login failed
                MessageBox.Show("ID and/or Password are not Valid")
            End If

            ' Clean up
            drSourceTable.Close()
            CloseDatabaseConnection()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class