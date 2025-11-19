<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAttendant
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
        Me.lstAttendant = New System.Windows.Forms.ListBox()
        Me.cboAttendant = New System.Windows.Forms.ComboBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(117, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Attendant's Last Name:"
        '
        'lstAttendant
        '
        Me.lstAttendant.FormattingEnabled = True
        Me.lstAttendant.Location = New System.Drawing.Point(139, 39)
        Me.lstAttendant.Name = "lstAttendant"
        Me.lstAttendant.Size = New System.Drawing.Size(341, 290)
        Me.lstAttendant.TabIndex = 6
        '
        'cboAttendant
        '
        Me.cboAttendant.FormattingEnabled = True
        Me.cboAttendant.Location = New System.Drawing.Point(139, 12)
        Me.cboAttendant.Name = "cboAttendant"
        Me.cboAttendant.Size = New System.Drawing.Size(341, 21)
        Me.cboAttendant.TabIndex = 5
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(256, 335)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(106, 36)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'frmAttendant
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(620, 387)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lstAttendant)
        Me.Controls.Add(Me.cboAttendant)
        Me.Controls.Add(Me.btnExit)
        Me.Name = "frmAttendant"
        Me.Text = "Flight Attendant's Information"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents lstAttendant As ListBox
    Friend WithEvents cboAttendant As ComboBox
    Friend WithEvents btnExit As Button
End Class
