Public Class frmEmployeeLogin
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim strLoginID As String = txtLogin.Text.Trim()
        Dim strPassword As String = txtPassword.Text.Trim()
        Dim cmdSelect As New OleDb.OleDbCommand
        Dim drSourceTable As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim objParam As OleDb.OleDbParameter

        Try
            ' Validate input
            If String.IsNullOrEmpty(strLoginID) OrElse String.IsNullOrEmpty(strPassword) Then
                MessageBox.Show("Please enter both Login ID and Password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Open database connection
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error. The application will now close.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
            End If

            ' Call stored procedure to check login
            cmdSelect = New OleDb.OleDbCommand("uspValidateEmployeeLogin", m_conAdministrator)
            cmdSelect.CommandType = CommandType.StoredProcedure

            ' Add parameters
            objParam = cmdSelect.Parameters.Add("@LoginID", OleDb.OleDbType.VarChar)
            objParam.Value = strLoginID
            objParam = cmdSelect.Parameters.Add("@Password", OleDb.OleDbType.VarChar)
            objParam.Value = strPassword

            ' Execute the command
            drSourceTable = cmdSelect.ExecuteReader()
            dt.Load(drSourceTable)

            ' Close the database connection
            CloseDatabaseConnection()

            ' Check if login was successful
            If dt.Rows.Count = 0 Then
                MessageBox.Show("ID and/or Password are not valid.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Retrieve user role and ID
            Dim strEmployeeRole As String = CStr(dt.Rows(0)("strEmployeeRole"))
            Dim intEmployeeID As Integer = CInt(dt.Rows(0)("intEmployeeID"))

            ' Set the global variable based on role
            Select Case strEmployeeRole.ToLower()
                Case "admin"
                    frmAdmin.ShowDialog()
                Case "pilot"
                    intPilot = intEmployeeID
                    frmPilotMain.ShowDialog()
                Case "attendant"
                    intAttendant = intEmployeeID
                    frmAttendantMain.ShowDialog()
                Case Else
                    MessageBox.Show("Unknown role. Access denied.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Select

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

End Class