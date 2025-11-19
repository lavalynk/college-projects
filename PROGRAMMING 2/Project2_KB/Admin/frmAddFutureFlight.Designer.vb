<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAddFutureFlight
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.txtFlightDate = New System.Windows.Forms.TextBox()
        Me.txtFlightNumber = New System.Windows.Forms.TextBox()
        Me.txtTimeofDeparture = New System.Windows.Forms.TextBox()
        Me.txtTimeofLanding = New System.Windows.Forms.TextBox()
        Me.cboDeparting = New System.Windows.Forms.ComboBox()
        Me.cboArriving = New System.Windows.Forms.ComboBox()
        Me.txtMiles = New System.Windows.Forms.TextBox()
        Me.txtPlaneID = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(61, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Flight Date:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(47, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Flight Number:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(27, 87)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Time of Departure:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(36, 121)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Time of Landing:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(33, 155)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(89, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Departing Airport:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(44, 189)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(78, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Arriving Airport:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(57, 223)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(65, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Miles Flown:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(71, 257)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(51, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Plane ID:"
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(62, 302)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(75, 23)
        Me.btnSubmit.TabIndex = 8
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(164, 302)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 9
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'txtFlightDate
        '
        Me.txtFlightDate.Location = New System.Drawing.Point(129, 19)
        Me.txtFlightDate.Name = "txtFlightDate"
        Me.txtFlightDate.Size = New System.Drawing.Size(100, 20)
        Me.txtFlightDate.TabIndex = 10
        '
        'txtFlightNumber
        '
        Me.txtFlightNumber.Location = New System.Drawing.Point(129, 49)
        Me.txtFlightNumber.Name = "txtFlightNumber"
        Me.txtFlightNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtFlightNumber.TabIndex = 11
        '
        'txtTimeofDeparture
        '
        Me.txtTimeofDeparture.Location = New System.Drawing.Point(129, 84)
        Me.txtTimeofDeparture.Name = "txtTimeofDeparture"
        Me.txtTimeofDeparture.Size = New System.Drawing.Size(100, 20)
        Me.txtTimeofDeparture.TabIndex = 12
        '
        'txtTimeofLanding
        '
        Me.txtTimeofLanding.Location = New System.Drawing.Point(128, 118)
        Me.txtTimeofLanding.Name = "txtTimeofLanding"
        Me.txtTimeofLanding.Size = New System.Drawing.Size(100, 20)
        Me.txtTimeofLanding.TabIndex = 13
        '
        'cboDeparting
        '
        Me.cboDeparting.FormattingEnabled = True
        Me.cboDeparting.Location = New System.Drawing.Point(128, 152)
        Me.cboDeparting.Name = "cboDeparting"
        Me.cboDeparting.Size = New System.Drawing.Size(121, 21)
        Me.cboDeparting.TabIndex = 14
        '
        'cboArriving
        '
        Me.cboArriving.FormattingEnabled = True
        Me.cboArriving.Location = New System.Drawing.Point(128, 186)
        Me.cboArriving.Name = "cboArriving"
        Me.cboArriving.Size = New System.Drawing.Size(121, 21)
        Me.cboArriving.TabIndex = 15
        '
        'txtMiles
        '
        Me.txtMiles.Location = New System.Drawing.Point(129, 218)
        Me.txtMiles.Name = "txtMiles"
        Me.txtMiles.Size = New System.Drawing.Size(100, 20)
        Me.txtMiles.TabIndex = 16
        '
        'txtPlaneID
        '
        Me.txtPlaneID.Location = New System.Drawing.Point(129, 252)
        Me.txtPlaneID.Name = "txtPlaneID"
        Me.txtPlaneID.Size = New System.Drawing.Size(100, 20)
        Me.txtPlaneID.TabIndex = 17
        '
        'frmAddFutureFlight
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(293, 347)
        Me.Controls.Add(Me.txtPlaneID)
        Me.Controls.Add(Me.txtMiles)
        Me.Controls.Add(Me.cboArriving)
        Me.Controls.Add(Me.cboDeparting)
        Me.Controls.Add(Me.txtTimeofLanding)
        Me.Controls.Add(Me.txtTimeofDeparture)
        Me.Controls.Add(Me.txtFlightNumber)
        Me.Controls.Add(Me.txtFlightDate)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmAddFutureFlight"
        Me.Text = "Add Future Flight"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents btnSubmit As Button
    Friend WithEvents btnExit As Button
    Friend WithEvents txtFlightDate As TextBox
    Friend WithEvents txtFlightNumber As TextBox
    Friend WithEvents txtTimeofDeparture As TextBox
    Friend WithEvents txtTimeofLanding As TextBox
    Friend WithEvents cboDeparting As ComboBox
    Friend WithEvents cboArriving As ComboBox
    Friend WithEvents txtMiles As TextBox
    Friend WithEvents txtPlaneID As TextBox
End Class
