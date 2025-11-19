<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdmin
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
        Me.btnPilots = New System.Windows.Forms.Button()
        Me.btnAttendants = New System.Windows.Forms.Button()
        Me.btnStatistics = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnFutureFlights = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnPilots
        '
        Me.btnPilots.Location = New System.Drawing.Point(13, 13)
        Me.btnPilots.Name = "btnPilots"
        Me.btnPilots.Size = New System.Drawing.Size(160, 46)
        Me.btnPilots.TabIndex = 0
        Me.btnPilots.Text = "Manage Pilots"
        Me.btnPilots.UseVisualStyleBackColor = True
        '
        'btnAttendants
        '
        Me.btnAttendants.Location = New System.Drawing.Point(179, 13)
        Me.btnAttendants.Name = "btnAttendants"
        Me.btnAttendants.Size = New System.Drawing.Size(160, 46)
        Me.btnAttendants.TabIndex = 1
        Me.btnAttendants.Text = "Manage Attendants"
        Me.btnAttendants.UseVisualStyleBackColor = True
        '
        'btnStatistics
        '
        Me.btnStatistics.Location = New System.Drawing.Point(345, 13)
        Me.btnStatistics.Name = "btnStatistics"
        Me.btnStatistics.Size = New System.Drawing.Size(160, 46)
        Me.btnStatistics.TabIndex = 2
        Me.btnStatistics.Text = "Statistics"
        Me.btnStatistics.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(225, 117)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnFutureFlights
        '
        Me.btnFutureFlights.Location = New System.Drawing.Point(179, 65)
        Me.btnFutureFlights.Name = "btnFutureFlights"
        Me.btnFutureFlights.Size = New System.Drawing.Size(160, 46)
        Me.btnFutureFlights.TabIndex = 4
        Me.btnFutureFlights.Text = "Add Future Flight"
        Me.btnFutureFlights.UseVisualStyleBackColor = True
        '
        'frmAdmin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(518, 154)
        Me.Controls.Add(Me.btnFutureFlights)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnStatistics)
        Me.Controls.Add(Me.btnAttendants)
        Me.Controls.Add(Me.btnPilots)
        Me.Name = "frmAdmin"
        Me.Text = "Admin Main Menu"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnPilots As Button
    Friend WithEvents btnAttendants As Button
    Friend WithEvents btnStatistics As Button
    Friend WithEvents btnExit As Button
    Friend WithEvents btnFutureFlights As Button
End Class
