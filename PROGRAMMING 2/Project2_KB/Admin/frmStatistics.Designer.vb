<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStatistics
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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lstPilots = New System.Windows.Forms.ListBox()
        Me.lstAttendants = New System.Windows.Forms.ListBox()
        Me.lblCustomers = New System.Windows.Forms.Label()
        Me.lblFlights = New System.Windows.Forms.Label()
        Me.lblAverage = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(272, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(138, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Total Number of Customers:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(176, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(234, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Total Number of Flights Taken By All Customers:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(218, 100)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(192, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Average Miles Flown For All Customers:"
        '
        'lstPilots
        '
        Me.lstPilots.FormattingEnabled = True
        Me.lstPilots.Location = New System.Drawing.Point(39, 150)
        Me.lstPilots.Name = "lstPilots"
        Me.lstPilots.Size = New System.Drawing.Size(343, 251)
        Me.lstPilots.TabIndex = 3
        '
        'lstAttendants
        '
        Me.lstAttendants.FormattingEnabled = True
        Me.lstAttendants.Location = New System.Drawing.Point(419, 150)
        Me.lstAttendants.Name = "lstAttendants"
        Me.lstAttendants.Size = New System.Drawing.Size(343, 251)
        Me.lstAttendants.TabIndex = 4
        '
        'lblCustomers
        '
        Me.lblCustomers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCustomers.Location = New System.Drawing.Point(432, 21)
        Me.lblCustomers.Name = "lblCustomers"
        Me.lblCustomers.Size = New System.Drawing.Size(132, 23)
        Me.lblCustomers.TabIndex = 5
        '
        'lblFlights
        '
        Me.lblFlights.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFlights.Location = New System.Drawing.Point(432, 57)
        Me.lblFlights.Name = "lblFlights"
        Me.lblFlights.Size = New System.Drawing.Size(132, 23)
        Me.lblFlights.TabIndex = 6
        '
        'lblAverage
        '
        Me.lblAverage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAverage.Location = New System.Drawing.Point(432, 97)
        Me.lblAverage.Name = "lblAverage"
        Me.lblAverage.Size = New System.Drawing.Size(132, 23)
        Me.lblAverage.TabIndex = 7
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(360, 407)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 0
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'frmStatistics
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.lblAverage)
        Me.Controls.Add(Me.lblFlights)
        Me.Controls.Add(Me.lblCustomers)
        Me.Controls.Add(Me.lstAttendants)
        Me.Controls.Add(Me.lstPilots)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmStatistics"
        Me.Text = "FlyMe2theMoon Statistics"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lstPilots As ListBox
    Friend WithEvents lstAttendants As ListBox
    Friend WithEvents lblCustomers As Label
    Friend WithEvents lblFlights As Label
    Friend WithEvents lblAverage As Label
    Friend WithEvents btnExit As Button
End Class
