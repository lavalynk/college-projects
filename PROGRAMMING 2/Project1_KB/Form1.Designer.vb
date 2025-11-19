<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.cboReside = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtIncome = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNumber = New System.Windows.Forms.TextBox()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnTotalHouse = New System.Windows.Forms.Button()
        Me.btnAverage = New System.Windows.Forms.Button()
        Me.btnPoverty = New System.Windows.Forms.Button()
        Me.lstResults = New System.Windows.Forms.ListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cboReside
        '
        Me.cboReside.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReside.FormattingEnabled = True
        Me.cboReside.Items.AddRange(New Object() {"Hamilton, Ohio", "Butler, Ohio", "Boone, Kentucky", "Kenton, Kentucky"})
        Me.cboReside.Location = New System.Drawing.Point(191, 41)
        Me.cboReside.Name = "cboReside"
        Me.cboReside.Size = New System.Drawing.Size(121, 21)
        Me.cboReside.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(40, 69)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(131, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Household Yearly Income:"
        '
        'txtIncome
        '
        Me.txtIncome.Location = New System.Drawing.Point(191, 68)
        Me.txtIncome.Name = "txtIncome"
        Me.txtIncome.Size = New System.Drawing.Size(121, 20)
        Me.txtIncome.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(59, 98)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(112, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Number in Household:"
        '
        'txtNumber
        '
        Me.txtNumber.Location = New System.Drawing.Point(191, 94)
        Me.txtNumber.Name = "txtNumber"
        Me.txtNumber.Size = New System.Drawing.Size(121, 20)
        Me.txtNumber.TabIndex = 4
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(101, 136)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(178, 23)
        Me.btnSubmit.TabIndex = 5
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(101, 253)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(177, 23)
        Me.btnExit.TabIndex = 6
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnTotalHouse
        '
        Me.btnTotalHouse.Enabled = False
        Me.btnTotalHouse.Location = New System.Drawing.Point(101, 165)
        Me.btnTotalHouse.Name = "btnTotalHouse"
        Me.btnTotalHouse.Size = New System.Drawing.Size(178, 23)
        Me.btnTotalHouse.TabIndex = 7
        Me.btnTotalHouse.Text = "Total Households Surveyed"
        Me.btnTotalHouse.UseVisualStyleBackColor = True
        '
        'btnAverage
        '
        Me.btnAverage.Enabled = False
        Me.btnAverage.Location = New System.Drawing.Point(101, 195)
        Me.btnAverage.Name = "btnAverage"
        Me.btnAverage.Size = New System.Drawing.Size(178, 23)
        Me.btnAverage.TabIndex = 8
        Me.btnAverage.Text = "Average Household Income"
        Me.btnAverage.UseVisualStyleBackColor = True
        '
        'btnPoverty
        '
        Me.btnPoverty.Enabled = False
        Me.btnPoverty.Location = New System.Drawing.Point(102, 224)
        Me.btnPoverty.Name = "btnPoverty"
        Me.btnPoverty.Size = New System.Drawing.Size(177, 23)
        Me.btnPoverty.TabIndex = 9
        Me.btnPoverty.Text = "Percent Households in Poverty"
        Me.btnPoverty.UseVisualStyleBackColor = True
        '
        'lstResults
        '
        Me.lstResults.FormattingEnabled = True
        Me.lstResults.Location = New System.Drawing.Point(344, 23)
        Me.lstResults.Name = "lstResults"
        Me.lstResults.Size = New System.Drawing.Size(359, 251)
        Me.lstResults.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(98, 42)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "State/County:"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(721, 306)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lstResults)
        Me.Controls.Add(Me.btnPoverty)
        Me.Controls.Add(Me.btnAverage)
        Me.Controls.Add(Me.btnTotalHouse)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.txtNumber)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtIncome)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboReside)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cboReside As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtIncome As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtNumber As TextBox
    Friend WithEvents btnSubmit As Button
    Friend WithEvents btnExit As Button
    Friend WithEvents btnTotalHouse As Button
    Friend WithEvents btnAverage As Button
    Friend WithEvents btnPoverty As Button
    Friend WithEvents lstResults As ListBox
    Friend WithEvents Label3 As Label
End Class
