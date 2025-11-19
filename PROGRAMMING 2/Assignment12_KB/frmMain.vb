'--------------------------------------------------------------------------------------------------
'   Name:   Keith Brock
'
'   Class:  IT-102
'
'   Assignment 12
'
'--------------------------------------------------------------------------------------------------
Public Class frmMain
    Private Sub btnPassengers_Click(sender As Object, e As EventArgs) Handles btnPassengers.Click
        Dim frmPassengers As New frmPassengers

        frmPassengers.ShowDialog()
    End Sub

    Private Sub btnPilot_Click(sender As Object, e As EventArgs) Handles btnPilot.Click
        Dim frmPilot As New frmPilot

        frmPilot.ShowDialog()
    End Sub

    Private Sub btnAttendant_Click(sender As Object, e As EventArgs) Handles btnAttendant.Click
        Dim frmAttendant As New frmAttendant

        frmAttendant.ShowDialog()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class