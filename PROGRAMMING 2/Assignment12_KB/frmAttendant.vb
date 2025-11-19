Public Class frmAttendant
    Private Sub cboAttendant_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboAttendant.SelectedIndexChanged
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
            strSelect = "Select TF.strFlightNumber, TF.dtmFlightDate, TF.dtmTimeofDeparture, TF.dtmTimeOfLanding" &
                        " From TAttendants As TA" &
                        " Join TAttendantFlights As TAF" &
                        " On TA.intAttendantID = TAF.intAttendantID" &
                        " Join TFlights as TF" &
                        " On TF.intFlightID = TAF.intFlightID" &
                        " Where TA.intAttendantID = " & cboAttendant.SelectedValue

            ' Retrieve all the records 
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader

            lstAttendant.Items.Clear()

            lstAttendant.Items.Add("=============================")

            While drSourceTable.Read()

                lstAttendant.Items.Add("  ")

                lstAttendant.Items.Add("Flight Number: " & vbTab & drSourceTable("strFlightNumber"))
                lstAttendant.Items.Add("Flight Date: " & vbTab & drSourceTable("dtmFlightDate"))
                lstAttendant.Items.Add("Departure Time: " & vbTab & drSourceTable("dtmTimeofDeparture"))
                lstAttendant.Items.Add("Arrival Time: " & vbTab & drSourceTable("dtmTimeOfLanding"))


                lstAttendant.Items.Add("  ")
                lstAttendant.Items.Add("=============================")

            End While
            CloseDatabaseConnection()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmAttendant_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim strSelect As String = ""
        Dim cmdSelect As OleDb.OleDbCommand ' this will be used for our Select statement
        Dim drSourceTable As OleDb.OleDbDataReader ' this will be where our data is retrieved to
        Dim dts As DataTable = New DataTable ' this is the table we will load from our reader
        '

        Try


            ' open the DB this is in module
            If OpenDatabaseConnectionSQLServer() = False Then

                ' No, warn the user ...
                MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                "The application will now close.",
                                Me.Text + " Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)

                ' and close the form/application
                Me.Close()

            End If


            ' Build the select statement to obtain Cities
            strSelect = "SELECT intAttendantID, strLastName FROM TAttendants"

            ' Retrieve all the records 
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader
            dts.Load(drSourceTable)

            'load the Cities result set into the combobox.  For VB, we do this by binding the data to the combobox

            cboAttendant.ValueMember = "intAttendantID"
            cboAttendant.DisplayMember = "strLastName"
            cboAttendant.DataSource = dts


            ' Clean up
            drSourceTable.Close()

            ' close the database connection
            CloseDatabaseConnection()

        Catch excError As Exception

            ' Log and display error message
            MessageBox.Show(excError.Message)

        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class