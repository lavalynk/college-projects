<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLandscape
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.radPick = New System.Windows.Forms.RadioButton()
        Me.radDelivery = New System.Windows.Forms.RadioButton()
        Me.cboMulchType = New System.Windows.Forms.ComboBox()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.txtZip = New System.Windows.Forms.TextBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnTotal = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.lstDisplay = New System.Windows.Forms.ListBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPromoCode = New System.Windows.Forms.TextBox()
        Me.lblPromo = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtPromoCode)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.cboMulchType)
        Me.GroupBox1.Controls.Add(Me.txtQty)
        Me.GroupBox1.Controls.Add(Me.txtZip)
        Me.GroupBox1.Controls.Add(Me.txtName)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(323, 363)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Customer Info"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.radPick)
        Me.GroupBox2.Controls.Add(Me.radDelivery)
        Me.GroupBox2.Location = New System.Drawing.Point(76, 216)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(157, 98)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        '
        'radPick
        '
        Me.radPick.AutoSize = True
        Me.radPick.Checked = True
        Me.radPick.Location = New System.Drawing.Point(48, 28)
        Me.radPick.Name = "radPick"
        Me.radPick.Size = New System.Drawing.Size(63, 17)
        Me.radPick.TabIndex = 0
        Me.radPick.TabStop = True
        Me.radPick.Text = "Pick Up"
        Me.radPick.UseVisualStyleBackColor = True
        '
        'radDelivery
        '
        Me.radDelivery.AutoSize = True
        Me.radDelivery.Location = New System.Drawing.Point(48, 52)
        Me.radDelivery.Name = "radDelivery"
        Me.radDelivery.Size = New System.Drawing.Size(63, 17)
        Me.radDelivery.TabIndex = 1
        Me.radDelivery.Text = "Delivery"
        Me.radDelivery.UseVisualStyleBackColor = True
        '
        'cboMulchType
        '
        Me.cboMulchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMulchType.FormattingEnabled = True
        Me.cboMulchType.Items.AddRange(New Object() {"Black Shredded - $7.90", "Black Chips - $7.00", "Brown Shredded - $7.90", "Brown Chips - $7.00", "Red Shredded - $7.90", "Red Chips - $7.00"})
        Me.cboMulchType.Location = New System.Drawing.Point(119, 129)
        Me.cboMulchType.Name = "cboMulchType"
        Me.cboMulchType.Size = New System.Drawing.Size(174, 21)
        Me.cboMulchType.TabIndex = 2
        '
        'txtQty
        '
        Me.txtQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQty.Location = New System.Drawing.Point(119, 169)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(66, 20)
        Me.txtQty.TabIndex = 3
        '
        'txtZip
        '
        Me.txtZip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtZip.Location = New System.Drawing.Point(119, 90)
        Me.txtZip.Name = "txtZip"
        Me.txtZip.Size = New System.Drawing.Size(84, 20)
        Me.txtZip.TabIndex = 1
        '
        'txtName
        '
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Location = New System.Drawing.Point(119, 51)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(145, 20)
        Me.txtName.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 170)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Number of Bags:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(25, 131)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Mulch Type:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(38, 92)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Zip Code:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(53, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Name:"
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(110, 399)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(114, 23)
        Me.btnAdd.TabIndex = 0
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnTotal
        '
        Me.btnTotal.Location = New System.Drawing.Point(274, 399)
        Me.btnTotal.Name = "btnTotal"
        Me.btnTotal.Size = New System.Drawing.Size(114, 23)
        Me.btnTotal.TabIndex = 1
        Me.btnTotal.Text = "Total"
        Me.btnTotal.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(429, 399)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(114, 23)
        Me.btnClear.TabIndex = 2
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(581, 399)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(114, 23)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'lstDisplay
        '
        Me.lstDisplay.FormattingEnabled = True
        Me.lstDisplay.Location = New System.Drawing.Point(349, 19)
        Me.lstDisplay.Name = "lstDisplay"
        Me.lstDisplay.Size = New System.Drawing.Size(391, 355)
        Me.lstDisplay.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 344)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Promo Code:"
        '
        'txtPromoCode
        '
        Me.txtPromoCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPromoCode.Location = New System.Drawing.Point(85, 341)
        Me.txtPromoCode.Name = "txtPromoCode"
        Me.txtPromoCode.Size = New System.Drawing.Size(148, 20)
        Me.txtPromoCode.TabIndex = 11
        '
        'lblPromo
        '
        Me.lblPromo.AutoSize = True
        Me.lblPromo.Location = New System.Drawing.Point(93, 380)
        Me.lblPromo.Name = "lblPromo"
        Me.lblPromo.Size = New System.Drawing.Size(0, 13)
        Me.lblPromo.TabIndex = 6
        '
        'frmLandscape
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(754, 450)
        Me.Controls.Add(Me.lblPromo)
        Me.Controls.Add(Me.lstDisplay)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnTotal)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmLandscape"
        Me.Text = "VB Landscape Supplies"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtName As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents radPick As RadioButton
    Friend WithEvents radDelivery As RadioButton
    Friend WithEvents cboMulchType As ComboBox
    Friend WithEvents txtQty As TextBox
    Friend WithEvents txtZip As TextBox
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnTotal As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents btnExit As Button
    Friend WithEvents lstDisplay As ListBox
    Friend WithEvents txtPromoCode As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents lblPromo As Label
End Class
