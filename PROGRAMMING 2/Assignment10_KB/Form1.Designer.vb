<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.btnStats = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.txtHouseSize = New System.Windows.Forms.TextBox()
        Me.txtZip = New System.Windows.Forms.TextBox()
        Me.lstTotals = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(53, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Household Size:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(84, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Zip Code:"
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(63, 117)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(75, 23)
        Me.btnSubmit.TabIndex = 2
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'btnStats
        '
        Me.btnStats.Location = New System.Drawing.Point(144, 117)
        Me.btnStats.Name = "btnStats"
        Me.btnStats.Size = New System.Drawing.Size(75, 23)
        Me.btnStats.TabIndex = 3
        Me.btnStats.Text = "Statistics"
        Me.btnStats.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(107, 146)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'txtHouseSize
        '
        Me.txtHouseSize.Location = New System.Drawing.Point(144, 40)
        Me.txtHouseSize.Name = "txtHouseSize"
        Me.txtHouseSize.Size = New System.Drawing.Size(100, 20)
        Me.txtHouseSize.TabIndex = 5
        '
        'txtZip
        '
        Me.txtZip.Location = New System.Drawing.Point(143, 79)
        Me.txtZip.Name = "txtZip"
        Me.txtZip.Size = New System.Drawing.Size(100, 20)
        Me.txtZip.TabIndex = 6
        '
        'lstTotals
        '
        Me.lstTotals.FormattingEnabled = True
        Me.lstTotals.Location = New System.Drawing.Point(325, 12)
        Me.lstTotals.Name = "lstTotals"
        Me.lstTotals.Size = New System.Drawing.Size(357, 290)
        Me.lstTotals.TabIndex = 7
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(722, 324)
        Me.Controls.Add(Me.lstTotals)
        Me.Controls.Add(Me.txtZip)
        Me.Controls.Add(Me.txtHouseSize)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnStats)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "Family Size Statistics by Zip Code"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btnSubmit As Button
    Friend WithEvents btnStats As Button
    Friend WithEvents btnExit As Button
    Friend WithEvents txtHouseSize As TextBox
    Friend WithEvents txtZip As TextBox
    Friend WithEvents lstTotals As ListBox
End Class
