Public Class frmPilotManagement
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim frmPilotAdd As New frmPilotAdd

        frmPilotAdd.ShowDialog()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim frmPilotDelete As New frmPilotDelete

        frmPilotDelete.ShowDialog()
    End Sub

    Private Sub btnAddFuture_Click(sender As Object, e As EventArgs) Handles btnAddFuture.Click
        Dim frmPilotShowFutureFlights As New frmPilotShowFutureFlights

        frmPilotShowFutureFlights.ShowDialog()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim frmPilotUpdateAdmin As New frmPilotUpdateAdmin

        frmPilotUpdateAdmin.ShowDialog()
    End Sub
End Class