<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAttendantAddFutureFlights
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboAttendant = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.cboFuture = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(189, 116)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(38, 74)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Attendant:"
        '
        'cboAttendant
        '
        Me.cboAttendant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAttendant.FormattingEnabled = True
        Me.cboAttendant.Location = New System.Drawing.Point(100, 71)
        Me.cboAttendant.Name = "cboAttendant"
        Me.cboAttendant.Size = New System.Drawing.Size(221, 21)
        Me.cboAttendant.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Flight Selection:"
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(108, 116)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(75, 23)
        Me.btnSubmit.TabIndex = 2
        Me.btnSubmit.Text = "Add Flight"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'cboFuture
        '
        Me.cboFuture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFuture.FormattingEnabled = True
        Me.cboFuture.Location = New System.Drawing.Point(100, 26)
        Me.cboFuture.Name = "cboFuture"
        Me.cboFuture.Size = New System.Drawing.Size(221, 21)
        Me.cboFuture.TabIndex = 0
        '
        'frmAttendantAddFutureFlights
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(352, 180)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboAttendant)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.cboFuture)
        Me.Name = "frmAttendantAddFutureFlights"
        Me.Text = "Assign Attendant to Flight"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnExit As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents cboAttendant As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnSubmit As Button
    Friend WithEvents cboFuture As ComboBox
End Class
