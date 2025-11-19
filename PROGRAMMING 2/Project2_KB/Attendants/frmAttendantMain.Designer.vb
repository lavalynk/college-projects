<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAttendantMain
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
        Me.btnFutureFlights = New System.Windows.Forms.Button()
        Me.btnPastFlights = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(162, 64)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnFutureFlights
        '
        Me.btnFutureFlights.Location = New System.Drawing.Point(267, 12)
        Me.btnFutureFlights.Name = "btnFutureFlights"
        Me.btnFutureFlights.Size = New System.Drawing.Size(123, 46)
        Me.btnFutureFlights.TabIndex = 2
        Me.btnFutureFlights.Text = "Show Future Flights"
        Me.btnFutureFlights.UseVisualStyleBackColor = True
        '
        'btnPastFlights
        '
        Me.btnPastFlights.Location = New System.Drawing.Point(138, 12)
        Me.btnPastFlights.Name = "btnPastFlights"
        Me.btnPastFlights.Size = New System.Drawing.Size(123, 46)
        Me.btnPastFlights.TabIndex = 1
        Me.btnPastFlights.Text = "Show Past Flights"
        Me.btnPastFlights.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(9, 12)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(123, 46)
        Me.btnUpdate.TabIndex = 0
        Me.btnUpdate.Text = "Update Attendant"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'frmAttendantMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(404, 100)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnFutureFlights)
        Me.Controls.Add(Me.btnPastFlights)
        Me.Controls.Add(Me.btnUpdate)
        Me.Name = "frmAttendantMain"
        Me.Text = "Attendant's Main Menu"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnExit As Button
    Friend WithEvents btnFutureFlights As Button
    Friend WithEvents btnPastFlights As Button
    Friend WithEvents btnUpdate As Button
End Class
