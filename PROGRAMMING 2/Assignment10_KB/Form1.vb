'--------------------------------------------------------------------------------------------------
'   Name:   Keith Brock
'
'   Class:  IT-102
'
'   Assignment 10
'
'--------------------------------------------------------------------------------------------------

Public Class Form1
    'Defining Class Level Variables
    Dim strZipCode() As String = {"45202", "45203", "45204", "45205", "45206", "45207"}
    Dim strZipCodes() As String
    Dim intFamilySize() As Integer
    Dim intCount As Integer = 0

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        'Declare Variables
        Dim blnValidated As Boolean = True
        Dim intSize As Integer

        'Validate Input
        Validate_Input(intSize, blnValidated)

        If blnValidated Then
            ReDim Preserve strZipCodes(intCount)
            ReDim Preserve intFamilySize(intCount)

            strZipCodes(intCount) = txtZip.Text
            intFamilySize(intCount) = intSize

            intCount += 1

            txtZip.ResetText()
            txtHouseSize.ResetText()
            txtHouseSize.Focus()
        End If
    End Sub

    Private Sub Validate_Input(ByRef intSize As Integer, ByRef blnValidated As Boolean)
        Validate_Size(intSize, blnValidated)
        If blnValidated Then
            Validate_Zip(blnValidated)
        End If
    End Sub

    Private Sub Validate_Size(ByRef intSize As Integer, ByRef blnValidated As Boolean)
        If Integer.TryParse(txtHouseSize.Text, intSize) Then
            If intSize > 0 Then
                ' Size is valid
            Else
                MessageBox.Show("Family Size Must Be Greater Than 0.")
                txtHouseSize.Focus()
                blnValidated = False
            End If
        Else
            MessageBox.Show("Family Size Must Be Numeric.")
            txtHouseSize.Focus()
            blnValidated = False
        End If
    End Sub

    Private Sub Validate_Zip(ByRef blnValidated As Boolean)
        Dim strEnteredZip As String = txtZip.Text

        If strZipCode.Contains(strEnteredZip) Then
            ' Zip code is valid
        Else
            MessageBox.Show("Invalid Zip Code.")
            txtZip.Focus()
            blnValidated = False
        End If
    End Sub

    Private Sub btnStats_Click(sender As Object, e As EventArgs) Handles btnStats.Click
        Dim dblMean As Double
        Dim dblMedian As Double
        'Resets the List...  just in case someone hits it twice......
        lstTotals.Items.Clear()

        If intCount = 0 Then
            MessageBox.Show("No data to display statistics.")
            Return
        End If

        dblMean = Calculate_Mean(intFamilySize)
        dblMedian = Calculate_Median(intFamilySize, strZipCodes)

        ' Calculate and Display Statistics
        lstTotals.Items.Add("The mean family size of all zip codes collected: " & dblMean)
        lstTotals.Items.Add("The median family size of all zip codes collected: " & dblMedian)

        ' Calculate and Display Means for Each Zip Code
        For Each strZip In strZipCode
            Dim dblMeanZip As Double = Calculate_Mean_Zip(strZip)
            If dblMeanZip <> -1 Then
                lstTotals.Items.Add("The mean family size for zip code " & strZip & ": " & dblMeanZip)
            End If
        Next

        ' Calculate and Display Medians for Each Zip Code
        For Each strZip In strZipCode
            Dim dblMedianZip As Double = Calculate_Median_Zip(strZip)
            If dblMedianZip <> -1 Then
                lstTotals.Items.Add("The median family size for zip code " & strZip & ": " & dblMedianZip)
            End If
        Next
    End Sub

    Private Function Calculate_Mean(intFamilySize() As Integer)
        'Calculates Mean
        Dim intSum As Integer = 0
        For i As Integer = 0 To intFamilySize.Length - 1
            intSum += intFamilySize(i)
        Next
        Return intSum / intFamilySize.Length
    End Function

    Private Function Calculate_Median(intFamilySize() As Integer, strZipCodes() As String)
        'Sorting Array and Keeping the Zip Codes aligned with the Sort.
        Array.Sort(intFamilySize, strZipCodes)
        Dim intMiddle As Integer = intFamilySize.Length \ 2
        Dim dblMedian As Double

        If intFamilySize.Length Mod 2 = 0 Then
            dblMedian = (intFamilySize(intMiddle - 1) + intFamilySize(intMiddle)) / 2.0
        Else
            dblMedian = intFamilySize(intMiddle)
        End If

        Return dblMedian
    End Function
    Private Function Calculate_Median_Zip(ByVal strZip As String) As Double
        Dim intZipFamilySizes() As Integer = {}
        Dim intZipCount As Integer = 0
        Dim dblMedian As Double

        ' Collect family sizes for the current zip code
        For intCounter As Integer = 0 To intCount - 1
            If strZipCodes(intCounter) = strZip Then
                ReDim Preserve intZipFamilySizes(intZipCount)
                intZipFamilySizes(intZipCount) = intFamilySize(intCounter)
                intZipCount += 1
            End If
        Next

        ' Calculate the median for the zip code
        If intZipCount > 0 Then
            Array.Sort(intZipFamilySizes)
            Dim intMiddle As Integer = intZipFamilySizes.Length \ 2
            If intZipFamilySizes.Length = intMiddle * 2 Then
                dblMedian = (intZipFamilySizes(intMiddle - 1) + intZipFamilySizes(intMiddle)) / 2.0
            Else
                dblMedian = intZipFamilySizes(intMiddle)
            End If
        Else
            dblMedian = -1
        End If

        Return dblMedian
    End Function
    Private Function Calculate_Mean_Zip(ByVal strZip As String)
        Dim intZipFamilySizes() As Integer = {}
        Dim intZipCount As Integer = 0
        Dim dblMean As Double

        ' Collect family sizes for the current zip code
        For intCounter As Integer = 0 To intCount - 1
            If strZipCodes(intCounter) = strZip Then
                ReDim Preserve intZipFamilySizes(intZipCount)
                intZipFamilySizes(intZipCount) = intFamilySize(intCounter)
                intZipCount += 1
            End If
        Next

        ' Calculate the mean for the zip code
        If intZipCount > 0 Then
            Dim intSum As Integer = 0
            For i As Integer = 0 To intZipFamilySizes.Length - 1
                intSum += intZipFamilySizes(i)
            Next
            dblMean = intSum / intZipFamilySizes.Length
        Else
            dblMean = -1
        End If

        Return dblMean
    End Function

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the program.
        Me.Close()
    End Sub
End Class
