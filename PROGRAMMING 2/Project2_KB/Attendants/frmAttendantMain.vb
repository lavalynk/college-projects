Public Class frmAttendantMain
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim frmAttendantUpdate As New frmAttendantUpdate

        frmAttendantUpdate.ShowDialog()
    End Sub

    Private Sub btnPastFlights_Click(sender As Object, e As EventArgs) Handles btnPastFlights.Click
        Dim frmAttendantPastFlights As New frmAttendantPastFlights

        frmAttendantPastFlights.ShowDialog()
    End Sub

    Private Sub btnFutureFlights_Click(sender As Object, e As EventArgs) Handles btnFutureFlights.Click
        Dim frmAttendantFutureFlights As New frmAttendantFutureFlights

        frmAttendantFutureFlights.ShowDialog()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

End Class