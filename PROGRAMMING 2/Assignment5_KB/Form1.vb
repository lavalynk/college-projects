'--------------------------------------------------------------------------------------------------
'   Name:   Keith Brock
'
'   Class:  IT-102
'
'   Assignment 5
'
'--------------------------------------------------------------------------------------------------

Public Class Form1
    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Dim blnValidated As Boolean = True
        Dim dblSalary As Double
        Dim dblGrowth As Double

        Get_Validate_Name(blnValidated)
        If blnValidated Then
            Get_Validate_Salary(dblSalary, blnValidated)
            If blnValidated Then
                Get_Validate_Growth(dblGrowth, blnValidated)
            End If
        End If

        If blnValidated Then
            Process_and_Display(dblSalary, dblGrowth)
        End If
    End Sub

    Private Sub Process_and_Display(ByVal dblSalary As Double, ByVal dblGrowth As Double)
        lstRetirement.Items.Clear()

        lstRetirement.Items.Add("Saving 5% of Annual Salary")
        Call_Salary(0.05, dblSalary, 10, dblGrowth)
        Call_Salary(0.05, dblSalary, 20, dblGrowth)
        Call_Salary(0.05, dblSalary, 30, dblGrowth)
        Call_Salary(0.05, dblSalary, 40, dblGrowth)
        lstRetirement.Items.Add("")
        lstRetirement.Items.Add("Saving 10% of Annual Salary")
        Call_Salary(0.1, dblSalary, 10, dblGrowth)
        Call_Salary(0.1, dblSalary, 20, dblGrowth)
        Call_Salary(0.1, dblSalary, 30, dblGrowth)
        Call_Salary(0.1, dblSalary, 40, dblGrowth)
        lstRetirement.Items.Add("")
        lstRetirement.Items.Add("Saving 15% of Annual Salary")
        Call_Salary(0.15, dblSalary, 10, dblGrowth)
        Call_Salary(0.15, dblSalary, 20, dblGrowth)
        Call_Salary(0.15, dblSalary, 30, dblGrowth)
        Call_Salary(0.15, dblSalary, 40, dblGrowth)
    End Sub

    Private Sub Call_Salary(ByVal dblPercentage As Double, ByVal dblSalary As Double, ByVal intYears As Integer, ByVal dblGrowth As Double)
        Dim intCounter As Integer
        Dim dblSaved As Double
        Dim dblTotal As Double = 0

        dblGrowth = dblGrowth / 100
        dblSaved = dblSalary * dblPercentage

        lstRetirement.Items.Add(vbTab & "After " & intYears.ToString() & " Years")


        For intCounter = 1 To intYears
            dblTotal *= (1 + dblGrowth)
            dblTotal += dblSaved
        Next

        lstRetirement.Items.Add(vbTab & vbTab & "Potential Account Total is " & dblTotal.ToString("c"))
    End Sub


    Private Sub Get_Validate_Name(ByRef blnValidated As Boolean)
        'Validation Check on Name.
        If txtName.Text = String.Empty Then
            MessageBox.Show("Name Must Exist.")
            txtName.Focus()
            blnValidated = False
        End If
    End Sub

    Private Sub Get_Validate_Salary(ByRef dblSalary As Double, ByRef blnValidated As Boolean)
        'Validation Check on Salary
        If Double.TryParse(txtSalary.Text, dblSalary) Then
            If dblSalary <= 0 Then
                MessageBox.Show("Salary must be greater than 0.")
                blnValidated = False
                txtSalary.Focus()
            End If
        Else
            MessageBox.Show("Salary must be Numeric.")
            blnValidated = False
            txtSalary.Focus()
        End If
    End Sub

    Private Sub Get_Validate_Growth(ByRef dblGrowth As Double, ByRef blnValidated As Boolean)
        If Double.TryParse(txtGrowth.Text, dblGrowth) Then
            If dblGrowth <= 0 Then
                MessageBox.Show("Growth must be greater than 0.")
                blnValidated = False
                txtGrowth.Focus()
            End If
        Else
            MessageBox.Show("Growth must be Numeric.")
            blnValidated = False
            txtGrowth.Focus()
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtName.ResetText()
        txtSalary.ResetText()
        txtGrowth.ResetText()
        lstRetirement.Items.Clear()
        txtName.Focus()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
