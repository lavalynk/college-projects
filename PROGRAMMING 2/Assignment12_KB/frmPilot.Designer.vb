<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPilot
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
        Me.btnExit = New System.Windows.Forms.Button()
        Me.cboPilot = New System.Windows.Forms.ComboBox()
        Me.lstPilot = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(271, 341)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(106, 36)
        Me.btnExit.TabIndex = 0
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'cboPilot
        '
        Me.cboPilot.FormattingEnabled = True
        Me.cboPilot.Location = New System.Drawing.Point(154, 18)
        Me.cboPilot.Name = "cboPilot"
        Me.cboPilot.Size = New System.Drawing.Size(341, 21)
        Me.cboPilot.TabIndex = 1
        '
        'lstPilot
        '
        Me.lstPilot.FormattingEnabled = True
        Me.lstPilot.Location = New System.Drawing.Point(154, 45)
        Me.lstPilot.Name = "lstPilot"
        Me.lstPilot.Size = New System.Drawing.Size(341, 290)
        Me.lstPilot.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(57, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Pilot's Last Name:"
        '
        'frmPilot
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(605, 405)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lstPilot)
        Me.Controls.Add(Me.cboPilot)
        Me.Controls.Add(Me.btnExit)
        Me.Name = "frmPilot"
        Me.Text = "Pilot's Information"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnExit As Button
    Friend WithEvents cboPilot As ComboBox
    Friend WithEvents lstPilot As ListBox
    Friend WithEvents Label1 As Label
End Class
