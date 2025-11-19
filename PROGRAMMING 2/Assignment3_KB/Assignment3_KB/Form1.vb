Public Class Form1
    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        'Declaring Variables
        Dim intInput As Integer
        Dim intCounter As Integer
        Dim intTotal As Integer

        intInput = txtNumber.Text

        'Parse the input to an integer.
        If Integer.TryParse(txtNumber.Text, intInput) Then
            'Check to make sure that the input is not a negative number.
            If intInput >= 0 Then
                'Loop to calculate the sum of the input.
                Do Until intCounter > intInput
                    intTotal += intCounter
                    intCounter += 1
                Loop
                'Display Result
                MessageBox.Show("The sum of 1 to " & intInput & " is " & intTotal)
            Else
                'Error message if the input is a negative number.
                MessageBox.Show("Please enter a non negative integer.")

            End If
        Else
            'Error message if the input is not a valid integer.
            MessageBox.Show("Please enter a valid integer.")
        End If
    End Sub
End Class
