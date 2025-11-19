'--------------------------------------------------------------------------------------------------
'   Name:   Keith Brock
'
'   Class:  IT-102
'
'   Assignment 7
'
'--------------------------------------------------------------------------------------------------


Public Class frmLandscape

    'Defining Class Level Variables
    Dim dblTotal As Double
    Dim blnSetUp As Boolean = True
    Dim dblZip As Double

    'Defining Constants
    Const shtTax As Double = 0.07

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        Dim dblQty As Double = 0
        Dim blnValidated As Boolean = True
        Dim strMulchType As String
        Dim dblPrice As Double

        'Data Validation and Get Inputs
        If blnValidated = True Then
            Call Get_Validate_Name(blnValidated)
            If blnValidated = True Then
                Call Get_Validate_Zip(blnValidated)
                If blnValidated = True Then
                    Call Get_Validate_Delivery(blnValidated)
                    If blnValidated = True Then
                        Call Get_Validate_ComboBox(blnValidated)
                        If blnValidated = True Then
                            Call Get_Validate_Qty(dblQty, blnValidated)
                        End If
                    End If
                End If
            End If
        End If

        If blnValidated = True Then
            'Adding Mulch to List
            strMulchType = Call_MulchType()
            dblPrice = Call_MulchPrice(dblQty)
            dblTotal += dblPrice
            Display_Mulch_Outputs(strMulchType, dblPrice)
            txtQty.ResetText()
            cboMulchType.SelectedIndex = -1
        End If

    End Sub

    Private Sub Add_Header()
        'Adds the first line of the listbox.  Makes sure it only enters once using a class variable.
        If blnSetUp = True Then
            lstDisplay.Items.Add("Mulch Type" + vbTab +vbTab + vbTab + "Bags" + vbTab + vbTab + "Total")
            lstDisplay.Items.Add("")
            blnSetUp = False
        End If
    End Sub

    Private Sub Get_Validate_Delivery(ByRef blnValidated As Boolean)
        Dim strZip As String = txtZip.Text
        Dim intCount As Integer = 0

        ' Delivery Validation
        If radDelivery.Checked Then
            If strZip.Substring(intCount, 3) = "410" Or strZip.Substring(intCount, 3) = "400" Then
                blnValidated = True
            Else
                MessageBox.Show("Invalid Zip Code for Delivery")
                blnValidated = False
            End If
        End If
    End Sub
    Private Sub Validate_Promo(ByRef blnPromo As Boolean, ByRef blnValidated As Boolean)
        Dim strString1 As String

        strString1 = txtPromoCode.Text.Trim().ToUpper()

        If strString1 = String.Empty Then
            blnValidated = True
            blnPromo = False
        Else
            If strString1.Contains(" ") Then
                MessageBox.Show("Promo Code Cannot Contain Spaces")
                lblPromo.ResetText()
                blnValidated = False
                blnPromo = False
            Else
                If strString1.Length = 7 Then
                    If strString1.Substring(2, 3) = "X1F" Or strString1.Substring(2, 3) = "C2P" Then
                        lblPromo.ResetText()
                        blnValidated = True
                        blnPromo = True
                    Else
                        lblPromo.Text = "Promo Code is invalid and no discount was applied."
                        lblPromo.ForeColor = Color.Red
                        blnValidated = False
                        blnPromo = False
                    End If
                Else
                    MessageBox.Show("Promo Codes Must Be 7 Alphanumeric Characters Long.")
                    lblPromo.ResetText()
                    blnValidated = False
                    blnPromo = False
                End If
            End If
        End If
    End Sub

    Private Function Call_Discount() As Double
        Dim dblDiscount As Double = 0
        Dim strString1 As String = txtPromoCode.Text.Trim().ToUpper()

        If strString1.Substring(2, 3) = "X1F" Then
            dblDiscount = 0.1
        ElseIf strString1.Substring(2, 3) = "C2P" Then
            dblDiscount = 0.2
        End If


        Return dblDiscount
    End Function
    Private Function Call_Delivery(ByVal dblZip As Double)
        Dim strZip As String
        Dim intCount As Integer = 0

        strZip = txtZip.Text.Trim()

        ' Determines Delivery Amount
        Dim shtDelivery As Short = 0

        If radDelivery.Checked Then
            If strZip.Substring(intCount, 3) = "410" Then
                shtDelivery = 18
            ElseIf strZip.Substring(intCount, 3) = "400" Then
                shtDelivery = 25
            End If
        End If

        Return shtDelivery
    End Function
    Private Function Call_MulchType() As String
        ' Determines Mulch Type
        Dim strMulchType As String = ""
        Dim strString2 As String

        strString2 = cboMulchType.Text

        ' Insert "Gold" into the mulch type if it is Black Shredded or Black Chip
        If strString2.Contains("Black Shredded") Then
            strString2 = strString2.Replace("Black Shredded", "Black Gold Shredded")
        ElseIf strString2.Contains("Black Chip") Then
            strString2 = strString2.Replace("Black Chip", "Black Gold Chip")
        End If

        ' Find the position of the " - " extract the mulch type
        Dim intString1 As Integer = strString2.IndexOf(" - ")
        If intString1 <> -1 Then
            strMulchType = strString2.Substring(0, intString1)
        Else
            strMulchType = strString2
        End If

        Return strMulchType
    End Function


    Private Function Call_MulchPrice(ByVal dblQty As Double) As Double
        ' Determines Mulch Price
        Dim strMulchType As String = cboMulchType.Text
        Dim dblMulchPrice As Double = 0
        Dim intString1 As Integer = strMulchType.IndexOf(" - $")

        If intString1 <> -1 Then
            ' Extract the price part of the string and convert it to a double
            Dim priceString As String = strMulchType.Substring(intString1 + 4)
            Double.TryParse(priceString, dblMulchPrice)
        End If

        Return dblMulchPrice * dblQty
    End Function

    Private Sub Get_Validate_Zip(ByRef blnValidated As Boolean)
        ' Validate Zip
        Dim strZip As String = txtZip.Text
        Dim dblZip As Double

        If Double.TryParse(strZip, dblZip) Then
            ' Check if zip code is greater than 0 and has exactly 5 digits
            If dblZip > 0 And strZip.Length = 5 Then
                blnValidated = True
            Else
                MessageBox.Show("Zip Code must be greater than 0 and must contain 5 digits.")
                blnValidated = False
                txtZip.Focus()
            End If
        Else
            MessageBox.Show("Zip Code must Exist and be Numeric.")
            blnValidated = False
            txtZip.Focus()
        End If
    End Sub
    Private Sub Get_Validate_ComboBox(ByRef blnValidated As Boolean)
        'Validate ComboBox
        If cboMulchType.Text = String.Empty Then
            MessageBox.Show("Mulch Must Be Selected.")
            cboMulchType.Focus()
            blnValidated = False
        End If
    End Sub
    Private Sub Get_Validate_Qty(ByRef dblQty As Double, ByRef blnValidated As Boolean)
        'Validate Quantity
        If Double.TryParse(txtQty.Text, dblQty) Then
            If dblQty < 0 Then
                MessageBox.Show("Quantity must be greater than 0.")
                blnValidated = False
                txtQty.Focus()
            End If
        Else
            MessageBox.Show("Quantity must Exist and be Numeric.")
            blnValidated = False
            txtQty.Focus()
        End If
    End Sub

    Private Sub Get_Validate_Name(ByRef blnValidated As Boolean)
        'Validate Name
        If txtName.Text = String.Empty Then
            MessageBox.Show("Name Must Exist.")
            txtName.Focus()
            blnValidated = False
        End If
    End Sub

    Private Sub Display_Mulch_Outputs(ByVal strMulchType As String, ByVal dblPrice As Double)
        Dim strString1 As String

        'Adds mulch line and displays output.
        Add_Header()

        strString1 = txtQty.Text

        If strMulchType.Contains("Gold") Then
            If strMulchType.Contains("Shredded") Then
                lstDisplay.Items.Add(strMulchType & vbTab & vbTab & txtQty.Text & vbTab & vbTab & dblPrice.ToString("c"))
            Else
                lstDisplay.Items.Add(strMulchType & vbTab & vbTab & vbTab & txtQty.Text & vbTab & vbTab & dblPrice.ToString("c"))
            End If
        Else
                lstDisplay.Items.Add(strMulchType & vbTab & vbTab & vbTab & txtQty.Text & vbTab & vbTab & dblPrice.ToString("c"))
        End If

    End Sub

    Private Function Calculate_Tax() As Double
        'Calculates Tax
        Return dblTotal * shtTax
    End Function

    Private Sub btnTotal_Click(sender As Object, e As EventArgs) Handles btnTotal.Click
        ' Calculates Totals
        Dim shtDelivery As Short
        Dim dblGrand As Double
        Dim blnValidated = True
        Dim blnPromo = True
        Dim dblDiscountPercent As Double = 0

        lblPromo.ResetText()
        If blnValidated = True Then
            Call Get_Validate_Delivery(blnValidated)
            If blnValidated = True Then
                Validate_Promo(blnPromo, blnValidated)
                If blnPromo = True Then
                    dblDiscountPercent = Call_Discount()
                End If
                If blnValidated = True Then
                    shtDelivery = Call_Delivery(Double.Parse(txtZip.Text))
                    dblGrand = Calculate_Grand_Total(dblDiscountPercent, shtDelivery)
                    Display_Total_Outputs(shtDelivery, dblDiscountPercent, dblGrand, blnPromo)
                End If
            End If
        End If
    End Sub

    Private Function Calculate_Grand_Total(ByVal dblDiscountPercent As Double, ByVal shtDelivery As Short) As Double
        ' Calculate the discount amount
        Dim dblDiscountTotal As Double = dblTotal * dblDiscountPercent


        ' Calculate the subtotal after discount
        Dim dblSubtotal As Double = dblTotal - dblDiscountTotal


        ' Calculate the tax on the discounted subtotal
        Dim dblTax As Double = dblSubtotal * shtTax


        ' Calculate the grand total including tax and delivery charges
        Dim dblGrandTotal As Double = dblSubtotal + dblTax
        If radDelivery.Checked Then
            dblGrandTotal += shtDelivery
        End If

        Return dblGrandTotal
    End Function



    Private Sub Display_Total_Outputs(ByVal shtDelivery As Short, ByVal dblDiscountPercent As Double, ByVal dblGrand As Double, ByVal blnPromo As Boolean)
        ' Displays Outputs
        Dim dblDiscountTotal As Double = dblTotal * dblDiscountPercent
        Dim dblTax As Double = (dblTotal - dblDiscountTotal) * shtTax

        ' Added Validation Check to make sure zipcode/pickup/delivery method didn't change to obscure data.
        ' Without it there was a possibility an employee could abuse the system.
        Dim blnValidated As Boolean = True
        If blnValidated = True Then
            Call Get_Validate_Name(blnValidated)
            If blnValidated = True Then
                Call Get_Validate_Zip(blnValidated)
            End If
            If blnValidated = True Then
                Call Get_Validate_Delivery(blnValidated)
            End If
        End If

        If blnValidated = True Then
            lstDisplay.Items.Add("=====================================================")
            lstDisplay.Items.Add("Total for all bags:" & vbTab & vbTab & dblTotal.ToString("c"))
            If blnPromo = True Then
                lstDisplay.Items.Add("Discount:" & vbTab & vbTab & vbTab & dblDiscountTotal.ToString("c"))
            End If
            lstDisplay.Items.Add("Tax:" & vbTab & vbTab & vbTab & dblTax.ToString("c"))
            If radDelivery.Checked Then
                lstDisplay.Items.Add("Delivery:" & vbTab & vbTab & vbTab & shtDelivery.ToString("c"))
            End If
            lstDisplay.Items.Add("")
            lstDisplay.Items.Add("Grand Total:" & vbTab & vbTab & dblGrand.ToString("c"))
        End If
    End Sub



    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtName.ResetText()
        txtQty.ResetText()
        txtZip.ResetText()
        lblPromo.ResetText()
        txtPromoCode.ResetText()
        lstDisplay.Items.Clear()
        radPick.Checked = True
        dblTotal = 0
        blnSetUp = True
        txtName.Focus()

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
