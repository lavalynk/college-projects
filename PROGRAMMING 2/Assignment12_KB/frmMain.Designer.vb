<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.btnPassengers = New System.Windows.Forms.Button()
        Me.btnPilot = New System.Windows.Forms.Button()
        Me.btnAttendant = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnPassengers
        '
        Me.btnPassengers.Location = New System.Drawing.Point(13, 13)
        Me.btnPassengers.Name = "btnPassengers"
        Me.btnPassengers.Size = New System.Drawing.Size(245, 130)
        Me.btnPassengers.TabIndex = 0
        Me.btnPassengers.Text = "Show Passengers"
        Me.btnPassengers.UseVisualStyleBackColor = True
        '
        'btnPilot
        '
        Me.btnPilot.Location = New System.Drawing.Point(264, 13)
        Me.btnPilot.Name = "btnPilot"
        Me.btnPilot.Size = New System.Drawing.Size(245, 130)
        Me.btnPilot.TabIndex = 1
        Me.btnPilot.Text = "Show Pilot Flights"
        Me.btnPilot.UseVisualStyleBackColor = True
        '
        'btnAttendant
        '
        Me.btnAttendant.Location = New System.Drawing.Point(13, 149)
        Me.btnAttendant.Name = "btnAttendant"
        Me.btnAttendant.Size = New System.Drawing.Size(245, 130)
        Me.btnAttendant.TabIndex = 2
        Me.btnAttendant.Text = "Show Attendant Flights"
        Me.btnAttendant.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(264, 149)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(245, 130)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(521, 294)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnAttendant)
        Me.Controls.Add(Me.btnPilot)
        Me.Controls.Add(Me.btnPassengers)
        Me.Name = "frmMain"
        Me.Text = "Airport Information"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnPassengers As Button
    Friend WithEvents btnPilot As Button
    Friend WithEvents btnAttendant As Button
    Friend WithEvents btnExit As Button
End Class
