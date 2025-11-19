Public Class frmShow
    Private Sub frmShow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

            'Build the select statement using PK from name selected
            '
            strSelect = "SELECT TP.intPassengerID, TP.strFirstName, TP.strLastName, TP.strAddress, TP.strCity, TS.strState, TP.strZip, TP.strPhoneNumber, Tp.strEmail" &
                        " From TPassengers As TP" &
                        " Join TStates as TS" &
                        " On TP.intStateID = TS.intStateID"

            ' Retrieve all the records 
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader

            lstPassengers.Items.Clear()

            lstPassengers.Items.Add("=============================")

            While drSourceTable.Read()

                lstPassengers.Items.Add("  ")

                lstPassengers.Items.Add("First Name: " & vbTab & drSourceTable("strFirstName"))
                lstPassengers.Items.Add("Last Name: " & vbTab & drSourceTable("strLastName"))
                lstPassengers.Items.Add("Address: " & vbTab & vbTab & drSourceTable("strAddress"))
                lstPassengers.Items.Add("City: " & vbTab & vbTab & drSourceTable("strCity"))
                lstPassengers.Items.Add("State: " & vbTab & vbTab & drSourceTable("strState"))
                lstPassengers.Items.Add("Zip Code: " & vbTab & drSourceTable("strZip"))
                lstPassengers.Items.Add("Phone Number: " & vbTab & drSourceTable("strPhoneNumber"))
                lstPassengers.Items.Add("Email: " & vbTab & vbTab & drSourceTable("strEmail"))


                lstPassengers.Items.Add("  ")
                lstPassengers.Items.Add("=============================")

            End While
            CloseDatabaseConnection()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class