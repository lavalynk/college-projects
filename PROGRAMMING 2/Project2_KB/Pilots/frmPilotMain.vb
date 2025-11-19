Public Class frmPilotMain
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim frmPilotUpdate As New frmPilotUpdate

        frmPilotUpdate.ShowDialog()

    End Sub

    Private Sub btnPastFlights_Click(sender As Object, e As EventArgs) Handles btnPastFlights.Click
        Dim frmPilotPastFlights As New frmPilotPastFlights

        frmPilotPastFlights.ShowDialog()

    End Sub
    Private Sub btnFutureFlights_Click(sender As Object, e As EventArgs) Handles btnFutureFlights.Click
        Dim frmPilotFutureFlights As New frmPilotFutureFlight

        frmPilotFutureFlights.ShowDialog()

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub


End Class