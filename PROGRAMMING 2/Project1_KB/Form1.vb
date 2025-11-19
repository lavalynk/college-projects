'--------------------------------------------------------------------------------------------------
'   Name:   Keith Brock
'
'   Class:  IT-102
'
'   Project 1
'
'--------------------------------------------------------------------------------------------------

Public Class Form1
    'Defining Class Level Variables
    Dim strStateCounty() As String = {"Hamilton, Ohio", "Butler, Ohio", "Boone, Kentucky", "Kenton, Kentucky"}
    Dim strState() As String
    Dim strCounty() As String
    Dim strIncome() As String
    Dim strHousehold() As String
    Dim intCount As Integer = 0

    Private Sub btnSubmit_Click_1(sender As Object, e As EventArgs) Handles btnSubmit.Click
        'Declare Variables
        Dim blnValidated As Boolean = True
        Dim dblIncome As Double = 0
        Dim intHousehold As Integer

        Validate_Input(dblIncome, intHousehold, blnValidated)

        If blnValidated Then
            Dim strStateCounty As String
            strStateCounty = cboReside.SelectedItem.ToString()

            Dim intIndex As Integer

            intIndex = strStateCounty.IndexOf(",")

            If intIndex <> -1 Then
                Dim strCurrentCounty As String = strStateCounty.Substring(0, intIndex).Trim()
                Dim strCurrentState As String = strStateCounty.Substring(intIndex + 1).Trim()


                'Redim the array to add new elements
                ReDim Preserve strCounty(intCount)
                ReDim Preserve strState(intCount)
                ReDim Preserve strIncome(intCount)
                ReDim Preserve strHousehold(intCount)

                strCounty(intCount) = strCurrentCounty
                strState(intCount) = strCurrentState
                strIncome(intCount) = dblIncome
                strHousehold(intCount) = intHousehold

                intCount += 1

                txtIncome.ResetText()
                cboReside.SelectedIndex = -1
                txtNumber.ResetText()

                If intCount > 0 Then
                    btnTotalHouse.Enabled = True
                    btnPoverty.Enabled = True
                    btnAverage.Enabled = True
                End If
            End If
        End If

    End Sub

    Private Sub Validate_Input(ByRef dblIncome As Double, ByRef intHousehold As Integer, ByRef blnValidated As Boolean)
        'Validate Data
        Validate_Reside(blnValidated)
        If blnValidated Then
            Validate_Income(dblIncome, blnValidated)
            If blnValidated Then
                Validate_Household(intHousehold, blnValidated)
            End If
        End If
    End Sub

    Private Sub Validate_Reside(ByRef blnValidated As Boolean)
        If cboReside.SelectedIndex = -1 Then
            MessageBox.Show("Please select a State/County from the list.")
            blnValidated = False
        Else
            'cboReside is Validated
        End If
    End Sub

    Private Sub Validate_Income(ByRef dblIncome As Double, ByRef blnValidated As Boolean)
        If Double.TryParse(txtIncome.Text, dblIncome) Then
            If dblIncome > 0 Then
                ' Size is valid
            Else
                MessageBox.Show("Income Must Be Greater Than 0.")
                txtIncome.Focus()
                blnValidated = False
            End If
        Else
            MessageBox.Show("Income Must Be Numeric.")
            txtIncome.Focus()
            blnValidated = False
        End If
    End Sub

    Private Sub Validate_Household(ByRef intHousehold As Integer, ByRef blnValidated As Boolean)
        If Integer.TryParse(txtNumber.Text, intHousehold) Then
            If intHousehold > 0 Then
                'Household is valid.
            Else
                MessageBox.Show("Family Size Must Be Greater Than 0.")
                txtNumber.Focus()
                blnValidated = False
            End If
        Else
            MessageBox.Show("Family Size Must Be A Whole Number And Numeric.")
            txtNumber.Focus()
            blnValidated = False
        End If
    End Sub

    Private Sub btnTotalHouse_Click(sender As Object, e As EventArgs) Handles btnTotalHouse.Click
        'Define Variables
        Dim intOhio As Integer = 0
        Dim intKentucky As Integer = 0
        Dim intHamilton As Integer = 0
        Dim intButler As Integer = 0
        Dim intBoone As Integer = 0
        Dim intKenton As Integer = 0
        Dim strStates As String
        Dim strCounties As String

        lstResults.Items.Clear()

        For Each strStates In strState
            If strStates = "Ohio" Then
                intOhio += 1
            Else
                If strStates = "Kentucky" Then
                    intKentucky += 1
                End If
            End If
        Next

        For Each strCounties In strCounty
            If strCounties = "Hamilton" Then
                intHamilton += 1
            Else
                If strCounties = "Butler" Then
                    intButler += 1
                Else
                    If strCounties = "Boone" Then
                        intBoone += 1
                    Else
                        If strCounties = "Kenton" Then
                            intKenton += 1
                        End If
                    End If
                End If
            End If
        Next

        'Display Output
        lstResults.Items.Add("Number of Households Surveyed")
        lstResults.Items.Add("----------------------------------------------------------------")
        lstResults.Items.Add("Ohio: " & intOhio)
        lstResults.Items.Add("    " & "Hamilton: " & intHamilton)
        lstResults.Items.Add("    " & "Butler: " & intButler)
        lstResults.Items.Add("Kentucky: " & intKentucky)
        lstResults.Items.Add("    " & "Boone: " & intBoone)
        lstResults.Items.Add("    " & "Kenton: " & intKenton)


    End Sub

    Private Sub btnAverage_Click(sender As Object, e As EventArgs) Handles btnAverage.Click
        'Define Variables
        Dim intOhio As Integer = 0
        Dim intKentucky As Integer = 0
        Dim intHamilton As Integer = 0
        Dim intButler As Integer = 0
        Dim intBoone As Integer = 0
        Dim intKenton As Integer = 0
        Dim dblOhioTotal As Double = 0
        Dim dblKentuckyTotal As Double = 0
        Dim dblHamilton As Double = 0
        Dim dblButler As Double = 0
        Dim dblBoone As Double = 0
        Dim dblKenton As Double = 0


        lstResults.Items.Clear()

        For intCounter As Integer = 0 To strState.Length - 1
            If strState(intCounter) = "Ohio" Then
                intOhio += 1
                dblOhioTotal += strIncome(intCounter)
            Else
                If strState(intCounter) = "Kentucky" Then
                    intKentucky += 1
                    dblKentuckyTotal += strIncome(intCounter)
                End If
            End If
        Next

        For intCounter As Integer = 0 To strCounty.Length - 1
            If strCounty(intCounter) = "Butler" Then
                intButler += 1
                dblButler += strIncome(intCounter)
            Else
                If strCounty(intCounter) = "Hamilton" Then
                    intHamilton += 1
                    dblHamilton += strIncome(intCounter)
                Else
                    If strCounty(intCounter) = "Boone" Then
                        intBoone += 1
                        dblBoone += strIncome(intCounter)
                    Else
                        If strCounty(intCounter) = "Kenton" Then
                            intKenton += 1
                            dblKenton += strIncome(intCounter)
                        End If
                    End If
                End If
            End If
        Next

        'Display Output
        dblOhioTotal = Calculate_Ohio_Average(dblOhioTotal, intOhio)
        dblKentuckyTotal = Calculate_Kentucky_Average(dblKentuckyTotal, intKentucky)
        dblHamilton = Calculate_Hamilton_Average(dblHamilton, intHamilton)
        dblButler = Calculate_Butler_Average(dblButler, intButler)
        dblBoone = Calculate_Boone_Average(dblBoone, intBoone)
        dblKenton = Calculate_Kenton_Average(dblKenton, intKenton)

        lstResults.Items.Add("Average Household Income")
        lstResults.Items.Add("----------------------------------------------------------------")
        lstResults.Items.Add("Ohio: " & dblOhioTotal.ToString("C"))
        lstResults.Items.Add("    Hamilton: " & dblHamilton.ToString("C"))
        lstResults.Items.Add("    Butler: " & dblButler.ToString("C"))
        lstResults.Items.Add("Kentucky: " & dblKentuckyTotal.ToString("C"))
        lstResults.Items.Add("    Boone: " & dblBoone.ToString("C"))
        lstResults.Items.Add("    Kenton: " & dblKenton.ToString("C"))
    End Sub

    Private Sub btnPoverty_Click(sender As Object, e As EventArgs) Handles btnPoverty.Click
        Dim intOhio As Integer = 0
        Dim intKentucky As Integer = 0
        Dim intHamilton As Integer = 0
        Dim intButler As Integer = 0
        Dim intBoone As Integer = 0
        Dim intKenton As Integer = 0
        Dim intOhioTotal As Integer = 0
        Dim intKentuckyTotal As Integer = 0
        Dim intHamiltonTotal As Double = 0
        Dim intButlerTotal As Double = 0
        Dim intBooneTotal As Double = 0
        Dim intKentonTotal As Double = 0


        lstResults.Items.Clear()

        For intCounter As Integer = 0 To strState.Length - 1
            If strState(intCounter) = "Ohio" Then
                intOhio += 1
                ' Checking poverty for Ohio
                If strHousehold(intCounter) > 5 AndAlso strIncome(intCounter) < 35000 Then
                    intOhioTotal += 1
                ElseIf strHousehold(intCounter) >= 4 AndAlso strIncome(intCounter) < 25000 Then
                    intOhioTotal += 1
                ElseIf (strHousehold(intCounter) = 2 Or strHousehold(intCounter) = 3) AndAlso strIncome(intCounter) < 10000 Then
                    intOhioTotal += 1
                End If
            ElseIf strState(intCounter) = "Kentucky" Then
                intKentucky += 1
                ' Checking poverty for Kentucky
                If strHousehold(intCounter) > 5 AndAlso strIncome(intCounter) < 35000 Then
                    intKentuckyTotal += 1
                ElseIf strHousehold(intCounter) >= 4 AndAlso strIncome(intCounter) < 25000 Then
                    intKentuckyTotal += 1
                ElseIf (strHousehold(intCounter) = 2 Or strHousehold(intCounter) = 3) AndAlso strIncome(intCounter) < 10000 Then
                    intKentuckyTotal += 1
                End If
            End If
        Next

        For intCounter As Integer = 0 To strCounty.Length - 1
            If strCounty(intCounter) = "Hamilton" Then
                intHamilton += 1
                ' Checking poverty for Hamilton
                If strHousehold(intCounter) > 5 AndAlso strIncome(intCounter) < 35000 Then
                    intHamiltonTotal += 1
                ElseIf strHousehold(intCounter) >= 4 AndAlso strIncome(intCounter) < 25000 Then
                    intHamiltonTotal += 1
                ElseIf (strHousehold(intCounter) = 2 Or strHousehold(intCounter) = 3) AndAlso strIncome(intCounter) < 10000 Then
                    intHamiltonTotal += 1
                End If
            ElseIf strCounty(intCounter) = "Butler" Then
                intButler += 1
                ' Checking poverty for Butler
                If strHousehold(intCounter) > 5 AndAlso strIncome(intCounter) < 35000 Then
                    intButlerTotal += 1
                ElseIf strHousehold(intCounter) >= 4 AndAlso strIncome(intCounter) < 25000 Then
                    intButlerTotal += 1
                ElseIf (strHousehold(intCounter) = 2 Or strHousehold(intCounter) = 3) AndAlso strIncome(intCounter) < 10000 Then
                    intButlerTotal += 1
                End If
            ElseIf strCounty(intCounter) = "Boone" Then
                intBoone += 1
                ' Checking poverty for Boone
                If strHousehold(intCounter) > 5 AndAlso strIncome(intCounter) < 35000 Then
                    intBooneTotal += 1
                ElseIf strHousehold(intCounter) >= 4 AndAlso strIncome(intCounter) < 25000 Then
                    intBooneTotal += 1
                ElseIf (strHousehold(intCounter) = 2 Or strHousehold(intCounter) = 3) AndAlso strIncome(intCounter) < 10000 Then
                    intBooneTotal += 1
                End If
            ElseIf strCounty(intCounter) = "Kenton" Then
                intKenton += 1
                ' Checking poverty for Kenton
                If strHousehold(intCounter) > 5 AndAlso strIncome(intCounter) < 35000 Then
                    intKentonTotal += 1
                ElseIf strHousehold(intCounter) >= 4 AndAlso strIncome(intCounter) < 25000 Then
                    intKentonTotal += 1
                ElseIf (strHousehold(intCounter) = 2 Or strHousehold(intCounter) = 3) AndAlso strIncome(intCounter) < 10000 Then
                    intKentonTotal += 1
                End If
            End If
        Next

        'Display Outputs
        lstResults.Items.Add("Percent Households in Poverty")
        lstResults.Items.Add("----------------------------------------------------------------")
        If intOhioTotal > 0 Then
            lstResults.Items.Add("Ohio: " & ((intOhioTotal / intOhio) * 100).ToString("F2") & "%")
        Else
            lstResults.Items.Add("Ohio: 0" & "%")
        End If
        If intHamiltonTotal > 0 Then
            lstResults.Items.Add("    Hamilton: " & ((intHamiltonTotal / intHamilton) * 100).ToString("F2") & "%")
        Else
            lstResults.Items.Add("    Hamilton: 0")
        End If
        If intButlerTotal > 0 Then
            lstResults.Items.Add("    Butler: " & ((intButlerTotal / intButler) * 100).ToString("F2") & "%")
        Else
            lstResults.Items.Add("    Butler: 0" & "%")
        End If
        If intKentuckyTotal > 0 Then
            lstResults.Items.Add("Kentucky: " & ((intKentuckyTotal / intKentucky) * 100).ToString("F2") & "%")
        Else
            lstResults.Items.Add("Kentucky: 0" & "%")
        End If
        If intBooneTotal > 0 Then
            lstResults.Items.Add("    Boone: " & ((intBooneTotal / intBoone) * 100).ToString("F2") & "%")
        Else
            lstResults.Items.Add("    Boone: 0" & "%")
        End If
        If intKentonTotal > 0 Then
            lstResults.Items.Add("    Kenton: " & ((intKentonTotal / intKenton) * 100).ToString("F2") & "%")
        Else
            lstResults.Items.Add("    Kenton: 0" & "%")
        End If
    End Sub

    'Calculations
    Private Function Calculate_Ohio_Average(ByVal dblOhioTotal As Double, ByVal intOhio As Integer)
        Dim dblAverage As Double

        If dblOhioTotal > 0 Then
            dblAverage = dblOhioTotal / intOhio
        Else
            dblAverage = 0
        End If

        Return dblAverage
    End Function

    Private Function Calculate_Kentucky_Average(ByVal dblKentuckyTotal As Double, ByVal intKentucky As Integer)
        Dim dblAverage As Double

        If dblKentuckyTotal > 0 Then
            dblAverage = dblKentuckyTotal / intKentucky
        Else
            dblAverage = 0
        End If

        Return dblAverage
    End Function

    Private Function Calculate_Hamilton_Average(ByVal dblHamilton As Double, ByVal intHamilton As Integer)
        Dim dblAverage As Double

        If dblHamilton > 0 Then
            dblAverage = dblHamilton / intHamilton
        Else
            dblAverage = 0
        End If

        Return dblAverage
    End Function

    Private Function Calculate_Butler_Average(ByVal dblButler As Double, ByVal intButler As Integer)
        Dim dblAverage As Double

        If dblButler > 0 Then
            dblAverage = dblButler / intButler
        Else
            dblAverage = 0
        End If

        Return dblAverage
    End Function

    Private Function Calculate_Boone_Average(ByVal dblBoone As Double, ByVal intBoone As Integer)
        Dim dblAverage As Double

        If dblBoone > 0 Then
            dblAverage = dblBoone / intBoone
        Else
            dblAverage = 0
        End If

        Return dblAverage
    End Function

    Private Function Calculate_Kenton_Average(ByVal dblKenton As Double, ByVal intKenton As Integer)
        Dim dblAverage As Double

        If dblKenton > 0 Then
            dblAverage = dblKenton / intKenton
        Else
            dblAverage = 0
        End If

        Return dblAverage
    End Function

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Close the program.
        Me.Close()
    End Sub
End Class
