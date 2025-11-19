<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAttendantFutureFlights
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
        Me.lstFutureFlights = New System.Windows.Forms.ListBox()
        Me.lblMilesFlown = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(142, 356)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 15
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'lstFutureFlights
        '
        Me.lstFutureFlights.FormattingEnabled = True
        Me.lstFutureFlights.Location = New System.Drawing.Point(12, 47)
        Me.lstFutureFlights.Name = "lstFutureFlights"
        Me.lstFutureFlights.Size = New System.Drawing.Size(334, 303)
        Me.lstFutureFlights.TabIndex = 14
        '
        'lblMilesFlown
        '
        Me.lblMilesFlown.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMilesFlown.Location = New System.Drawing.Point(142, 11)
        Me.lblMilesFlown.Name = "lblMilesFlown"
        Me.lblMilesFlown.Size = New System.Drawing.Size(100, 23)
        Me.lblMilesFlown.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Total Miles Flown:"
        '
        'frmAttendantFutureFlights
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(359, 390)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.lstFutureFlights)
        Me.Controls.Add(Me.lblMilesFlown)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmAttendantFutureFlights"
        Me.Text = "Attendant Future Flights"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnExit As Button
    Friend WithEvents lstFutureFlights As ListBox
    Friend WithEvents lblMilesFlown As Label
    Friend WithEvents Label1 As Label
End Class
