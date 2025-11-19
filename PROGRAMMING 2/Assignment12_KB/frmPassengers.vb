Public Class frmPassengers
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try

            Dim strSelect As String = ""
            Dim cmdSelect As OleDb.OleDbCommand            ' this will be used for our Select statement
            Dim drSourceTable As OleDb.OleDbDataReader     ' this will be where our result set will 
            Dim dt As DataTable = New DataTable            ' this is the table we will load from our reader


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

            ' Build the select statement
            strSelect = "SELECT * FROM TPassengers"

            ' Retrieve all the records 
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader

            'loop through result set and display in Listbox

            lstResults.Items.Add("Roster of All Passengers")
            lstResults.Items.Add("=============================")

            While drSourceTable.Read()

                lstResults.Items.Add("  ")

                lstResults.Items.Add("First Name: " & vbTab & drSourceTable("strFirstName"))
                lstResults.Items.Add("Last Name: " & vbTab & drSourceTable("strLastName"))
                lstResults.Items.Add("Address: " & vbTab & vbTab & drSourceTable("strAddress"))
                lstResults.Items.Add("City: " & vbTab & vbTab & drSourceTable("strCity"))
                'lstResults.Items.Add("State: " & vbTab & vbTab & drSourceTable("strState"))

                lstResults.Items.Add("  ")
                lstResults.Items.Add("=============================")

            End While


            ' Clean up
            drSourceTable.Close()

            ' close the database connection
            CloseDatabaseConnection()

        Catch ex As Exception

            ' Log and display error message
            MessageBox.Show(ex.Message)

        End Try

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

End Class
