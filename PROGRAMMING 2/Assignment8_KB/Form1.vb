'--------------------------------------------------------------------------------------------------
'   Name:   Keith Brock
'
'   Class:  IT-102
'
'   Assignment 9
'
'--------------------------------------------------------------------------------------------------

Public Class Form1
    Dim dblRain(11) As Double
    Dim strMonth() As String = {"January", "Feburary", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"}
    Dim intCounter As Integer = 0
    Dim strMaximum As String = String.Empty
    Dim strMinimum As String = String.Empty

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        LoadRainfall()
    End Sub

    Private Sub LoadRainfall()
        Dim strAmount As String
        Dim strInputMonth As String

        strInputMonth = strMonth(intCounter)
        strAmount = InputBox("Please enter rainfall for the month of " & strInputMonth & ".")

        If Double.TryParse(strAmount, dblRain(intCounter)) Then
            If strAmount < 0 Then
                MessageBox.Show("Please enter a positive integer.")
            Else
                lstInput.Items.Add(strInputMonth & " - " & dblRain(intCounter) & " inches.")
                intCounter += 1
            End If
        Else
            MessageBox.Show("Please enter numbers only.")
        End If

        If intCounter > 11 Then
            btnAdd.Enabled = False
            btnCalculate.Enabled = True
        End If
    End Sub

    Private Function GetTotal() As Double
        Dim dblTotal As Double = 0
        ' loop through array and add all hours up
        For intIndex As Integer = 0 To dblRain.Length - 1
            dblTotal += dblRain(intIndex)
        Next

        ' return total hours
        Return dblTotal
    End Function

    Private Function GetMaximum() As String
        Dim dblMaximum As Double = dblRain(0)

        For intIndex As Integer = 0 To dblRain.Length - 1
            If dblRain(intIndex) > dblMaximum Then
                dblMaximum = dblRain(intIndex)
                strMaximum = strMonth(intIndex)
            End If
        Next

        Return strMaximum
    End Function

    Private Function GetMinimum() As String
        Dim dblMinimum As Double = dblRain(0)
        strMinimum = strMonth(0)

        For intIndex As Integer = 1 To intCounter - 1
            If dblRain(intIndex) < dblMinimum Then
                dblMinimum = dblRain(intIndex)
                strMinimum = strMonth(intIndex)
            End If
        Next

        Return strMinimum
    End Function

    Private Function GetAverage() As Double
        Dim dblAverage As Double

        dblAverage = GetTotal() / dblRain.Length

        Return dblAverage
    End Function

    Private Function GetSeasonWithMostRainfall() As String
        Dim dblSeasonRain(3) As Double
        ' Calculate total rainfall for each season
        dblSeasonRain(0) = dblRain(11) + dblRain(0) + dblRain(1) ' Winter
        dblSeasonRain(1) = dblRain(2) + dblRain(3) + dblRain(4) ' Spring
        dblSeasonRain(2) = dblRain(5) + dblRain(6) + dblRain(7) ' Summer
        dblSeasonRain(3) = dblRain(8) + dblRain(9) + dblRain(10) ' Fall

        ' Find the season with the most rainfall
        Dim intMaxSeasonIndex As Integer = 0
        For intIndex As Integer = 1 To dblSeasonRain.Length - 1
            If dblSeasonRain(intIndex) > dblSeasonRain(intMaxSeasonIndex) Then
                intMaxSeasonIndex = intIndex
            End If
        Next

        Dim strSeason() As String = {"Winter", "Spring", "Summer", "Fall"}

        Return "Season with most rainfall is " & strSeason(intMaxSeasonIndex) & " with " & dblSeasonRain(intMaxSeasonIndex).ToString("F2") & " inches."
    End Function

    Private Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click
        'calculations
        Dim dblTotalDisplay As Double = GetTotal()
        Dim strMaximumDisplay As String = GetMaximum()
        Dim strMinimumDisplay As String = GetMinimum()
        Dim dblAverage As Double = GetAverage()
        Dim strSeasonWithMostRainfall As String = GetSeasonWithMostRainfall()

        DisplayTotals(dblTotalDisplay, strMaximumDisplay, strMinimumDisplay, dblAverage, strSeasonWithMostRainfall)
        btnCalculate.Enabled = False
    End Sub

    Private Sub DisplayTotals(ByVal dblTotalDisplay As Double, ByVal strMaximumDisplay As String, ByVal strMinimumDisplay As String, ByVal dblAverage As Double, ByVal strSeasonwithMostrainfall As String)
        'display total rainfall
        lstOutput.Items.Add("Total: " & dblTotalDisplay.ToString("F2") & " inches.")
        'display max total
        lstOutput.Items.Add("Greatest Rainfall: " & strMaximumDisplay)
        'display minimum total
        lstOutput.Items.Add("Least Rainfall: " & strMinimumDisplay)
        'display average
        lstOutput.Items.Add("Average: " & dblAverage.ToString("F2") & " inches.")
        'display season with most rainfall
        lstOutput.Items.Add(strSeasonwithMostrainfall)
    End Sub
End Class
