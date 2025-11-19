Public Class frmAdmin
    Private Sub btnPilots_Click(sender As Object, e As EventArgs) Handles btnPilots.Click
        Dim frmPilotManagement As New frmPilotManagement

        frmPilotManagement.ShowDialog()

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnAttendants_Click(sender As Object, e As EventArgs) Handles btnAttendants.Click
        Dim frmAttendantManagement As New frmAttendantManagement

        frmAttendantManagement.ShowDialog()

    End Sub

    Private Sub btnStatistics_Click(sender As Object, e As EventArgs) Handles btnStatistics.Click
        Dim frmStatistics As New frmStatistics

        frmStatistics.ShowDialog()
    End Sub

    Private Sub btnFutureFlights_Click(sender As Object, e As EventArgs) Handles btnFutureFlights.Click
        Dim frmAddFutureFlight As New frmAddFutureFlight

        frmAddFutureFlight.ShowDialog()
    End Sub
End Class