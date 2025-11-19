Public Class frmAttendantManagement
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim frmAttendantAdd As New frmAttendantAdd

        frmAttendantAdd.ShowDialog()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim frmAttendantDelete As New frmAttendantDelete

        frmAttendantDelete.ShowDialog()
    End Sub

    Private Sub btnAddFuture_Click(sender As Object, e As EventArgs) Handles btnAddFuture.Click
        Dim frmAttendantAddFutureFlights As New frmAttendantAddFutureFlights

        frmAttendantAddFutureFlights.ShowDialog()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub frmAttendantManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim frmAttendantUpdateAdmin As New frmAttendantUpdateAdmin

        frmAttendantUpdateAdmin.ShowDialog()

    End Sub
End Class