<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPilotPastFlights
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
        Me.lstPastFlights = New System.Windows.Forms.ListBox()
        Me.lblMilesFlown = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(142, 351)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 7
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'lstPastFlights
        '
        Me.lstPastFlights.FormattingEnabled = True
        Me.lstPastFlights.Location = New System.Drawing.Point(12, 42)
        Me.lstPastFlights.Name = "lstPastFlights"
        Me.lstPastFlights.Size = New System.Drawing.Size(334, 303)
        Me.lstPastFlights.TabIndex = 6
        '
        'lblMilesFlown
        '
        Me.lblMilesFlown.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMilesFlown.Location = New System.Drawing.Point(142, 6)
        Me.lblMilesFlown.Name = "lblMilesFlown"
        Me.lblMilesFlown.Size = New System.Drawing.Size(100, 23)
        Me.lblMilesFlown.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Total Miles Flown:"
        '
        'frmPilotPastFlights
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(363, 384)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.lstPastFlights)
        Me.Controls.Add(Me.lblMilesFlown)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmPilotPastFlights"
        Me.Text = "Pilot Past Flights"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnExit As Button
    Friend WithEvents lstPastFlights As ListBox
    Friend WithEvents lblMilesFlown As Label
    Friend WithEvents Label1 As Label
End Class
