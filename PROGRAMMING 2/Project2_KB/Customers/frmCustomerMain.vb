Public Class frmCustomerMain
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim frmCustomerUpdate As New frmCustomerUpdate

        frmCustomerUpdate.ShowDialog()

    End Sub

    Private Sub btnAddFlight_Click(sender As Object, e As EventArgs) Handles btnAddFlight.Click
        Dim frmCustomerAddFlight As New frmCustomerAddFlight

        frmCustomerAddFlight.ShowDialog()

    End Sub

    Private Sub btnPastFlights_Click(sender As Object, e As EventArgs) Handles btnPastFlights.Click
        Dim frmCustomerPastFlights As New frmCustomerPastFlights

        frmCustomerPastFlights.ShowDialog()
    End Sub

    Private Sub btnShowFuture_Click(sender As Object, e As EventArgs) Handles btnShowFuture.Click
        Dim frmCustomerFutureFlights As New frmCustomerFutureFlights

        frmCustomerFutureFlights.ShowDialog()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class