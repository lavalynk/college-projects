Public Class Form1
    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        'Declaring Variables
        Dim intInput As Integer
        Dim intCounter As Integer = 1
        Dim intTotal As Integer = 0
        Dim blnValidated As Boolean = True

        If Integer.TryParse(txtNumber.Text, intInput) Then
            Get_Validate_Number(intInput, blnValidated)
            If blnValidated = True Then
                ' Loop to calculate the sum of the input.
                Do Until intCounter > intInput
                    intTotal += intCounter
                    intCounter += 1
                Loop
                ' Display Result
                MessageBox.Show("The sum of 1 to " & intInput & " is " & intTotal)
            End If
        Else
            MessageBox.Show("Please enter a valid integer.")
            txtNumber.Focus()
        End If
    End Sub

    Private Sub Get_Validate_Number(ByRef intInput As Integer, ByRef blnValidated As Boolean)
        ' Check to make sure that the input is not a negative number.
        If intInput >= 0 Then
            blnValidated = True
        Else
            ' Error message if the input is a negative number.
            MessageBox.Show("Please enter a non-negative integer.")
            blnValidated = False
            txtNumber.Focus()
        End If
    End Sub
End Class
