Public Class Form1
    Const dblPercentageReturn As Double = 0.1
    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Dim dblYearlySalary As Double

        Dim blnValidated As Boolean = True

        GetandValidate_Input(dblYearlySalary, blnValidated)

        If blnValidated Then
            Process_and_Display(dblYearlySalary)
        End If

    End Sub

    Private Sub GetandValidate_Input(ByRef dblYearlySalary As Double, ByRef blnValidated As Boolean)
        Validate_Name(blnValidated)
        If blnValidated Then
            Get_Validate_Salary(dblYearlySalary, blnValidated)
        End If
    End Sub

    Private Sub Validate_Name(ByRef blnValidated As Boolean)
        If txtName.Text = "" Then
            MessageBox.Show("Name is required")
            blnValidated = False
            txtName.Focus()

        End If
    End Sub

    Private Sub Get_Validate_Salary(ByRef dblYearlySalary As Double, ByRef blnValidated As Boolean)
        If Double.TryParse(txtSalary.Text, dblYearlySalary) Then
            If dblYearlySalary > 0 Then
            Else
                MessageBox.Show("Salary must be greater than 0")
                blnValidated = False
                txtSalary.Focus()
            End If
        End If
    End Sub

    Private Sub Process_and_Display(ByVal dblYearlySalary As Double)
        Dim dblTotalReturn As Double
        Dim dblPercent As Double
        Dim dblSavings As Double

        For intPercent = 5 To 15 Step 5

            dblPercent = intPercent / 100
            dblSavings = dblYearlySalary * dblPercent
            lstResults.Items.Add("")
            lstResults.Items.Add("Saving " & intPercent & "% of Annual Salary")

            For intYears = 10 To 40 Step 10
                dblTotalReturn = 0
                For intCounter = 1 To intYears

                    dblTotalReturn += ((dblTotalReturn) * dblPercentageReturn)
                    dblTotalReturn += dblSavings

                Next

                lstResults.Items.Add(vbTab & "After " & intYears & " Years")
                lstResults.Items.Add(vbTab & vbTab & "Potential Account Total is " & dblTotalReturn.ToString("c"))
            Next
        Next

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        lstResults.Items.Clear()

        txtName.Clear()
        txtSalary.Clear()

        txtName.Focus()
    End Sub
End Class
