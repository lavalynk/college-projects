'--------------------------------------------------------------------------------------------------
'   Name:   Keith Brock
'
'   Class:  IT-102
'
'   Assignment 6
'
'--------------------------------------------------------------------------------------------------

Public Class Form1
    Private Sub btnVowels_Click(sender As Object, e As EventArgs) Handles btnVowels.Click
        Dim strSentence As String
        Dim intCount As Integer = 0
        Dim intVowelCount As Integer

        strSentence = txtSentence.Text.ToUpper

        Do Until intCount = strSentence.Length
            If strSentence.Substring(intCount, 1) = "A" Or strSentence.Substring(intCount, 1) = "E" Or strSentence.Substring(intCount, 1) = "I" Or strSentence.Substring(intCount, 1) = "O" Or strSentence.Substring(intCount, 1) = "U" Then
                intVowelCount += 1
            End If
            intCount += 1
        Loop

        MessageBox.Show("Vowels: " & intVowelCount)
    End Sub
    Private Sub btnWords_Click(sender As Object, e As EventArgs) Handles btnWords.Click
        Dim strSentence As String = txtSentence.Text.Trim()
        Dim intIndex As Integer = 0
        Dim intIndex2 As Integer = 0
        Dim wordCount As Integer = 0

        Do
            intIndex = strSentence.IndexOf(" ", intIndex2)
            If intIndex <> -1 Then

                wordCount += 1
                intIndex2 = intIndex + 1
            Else
                If intIndex2 < strSentence.Length Then
                    wordCount += 1
                End If
            End If
        Loop Until intIndex = -1

        MessageBox.Show("Word Count: " & wordCount)
    End Sub
    Private Sub btnBreak_Click(sender As Object, e As EventArgs) Handles btnBreak.Click
        Dim strRecord As String
        Dim strField1 As String
        Dim strField2 As String
        Dim strField3 As String
        Dim strField4 As String
        Dim strField5 As String
        Dim strField6 As String
        Dim intIndex As Integer

        strRecord = txtRecord.Text

        intIndex = strRecord.IndexOf(",")
        If intIndex <> -1 Then
            strField1 = strRecord.Substring(0, intIndex)
            strRecord = strRecord.Substring(intIndex + 1)
        Else
            strField1 = strRecord
            strRecord = ""
        End If

        intIndex = strRecord.IndexOf(",")
        If intIndex <> -1 Then
            strField2 = strRecord.Substring(0, intIndex)
            strRecord = strRecord.Substring(intIndex + 1)
        Else
            strField2 = strRecord
            strRecord = ""
        End If

        intIndex = strRecord.IndexOf(",")
        If intIndex <> -1 Then
            strField3 = strRecord.Substring(0, intIndex)
            strRecord = strRecord.Substring(intIndex + 1)
        Else
            strField3 = strRecord
            strRecord = ""
        End If

        intIndex = strRecord.IndexOf(",")
        If intIndex <> -1 Then
            strField4 = strRecord.Substring(0, intIndex)
            strRecord = strRecord.Substring(intIndex + 1)
        Else
            strField4 = strRecord
            strRecord = ""
        End If

        intIndex = strRecord.IndexOf(",")
        If intIndex <> -1 Then
            strField5 = strRecord.Substring(0, intIndex)
            strRecord = strRecord.Substring(intIndex + 1)
        Else
            strField5 = strRecord
            strRecord = ""
        End If

        intIndex = strRecord.IndexOf(",")
        If intIndex <> -1 Then
            strField6 = strRecord.Substring(0, intIndex)
            strRecord = strRecord.Substring(intIndex + 1)
        Else
            strField6 = strRecord
            strRecord = ""
        End If

        txtField1.Text = strField1
        txtField2.Text = strField2
        txtField3.Text = strField3
        txtField4.Text = strField4
        txtField5.Text = strField5
        txtField6.Text = strField6
        txtRecord.ResetText()
    End Sub

    Private Sub btnFormat_Click(sender As Object, e As EventArgs) Handles btnFormat.Click
        Dim strPhone As String

        strPhone = txtPhone.Text

        If IsNumeric(strPhone) And strPhone.Length = 10 Then
            strPhone = strPhone.Insert(0, "(")
            strPhone = strPhone.Insert(4, ") ")
            strPhone = strPhone.Insert(9, "-")
            lblFormatPhone.Text = strPhone
        Else
            MessageBox.Show("Please enter a valid 10 digit phone number.  Do not include dashes.")
            txtPhone.ResetText()
            txtPhone.Focus()

        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtSentence.ResetText()
        txtRecord.ResetText()
        txtField1.ResetText()
        txtField2.ResetText()
        txtField3.ResetText()
        txtField4.ResetText()
        txtField5.ResetText()
        txtField6.ResetText()
        txtPhone.ResetText()
        lblFormatPhone.ResetText()
        txtSentence.Focus()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub


End Class
