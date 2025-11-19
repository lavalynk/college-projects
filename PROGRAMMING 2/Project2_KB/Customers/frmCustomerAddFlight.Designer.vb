<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCustomerAddFlight
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
        Me.cboFuture = New System.Windows.Forms.ComboBox()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.radReservedSeat = New System.Windows.Forms.RadioButton()
        Me.gboxSeating = New System.Windows.Forms.GroupBox()
        Me.lblSeat = New System.Windows.Forms.Label()
        Me.lblDesignatedSeatCost = New System.Windows.Forms.Label()
        Me.cboSeat = New System.Windows.Forms.ComboBox()
        Me.lblReservedSeatCost = New System.Windows.Forms.Label()
        Me.radDesignatedSeat = New System.Windows.Forms.RadioButton()
        Me.lblFlightInfo = New System.Windows.Forms.Label()
        Me.gboxSeating.SuspendLayout()
        Me.SuspendLayout()
        '
        'cboFuture
        '
        Me.cboFuture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFuture.FormattingEnabled = True
        Me.cboFuture.Location = New System.Drawing.Point(102, 12)
        Me.cboFuture.Name = "cboFuture"
        Me.cboFuture.Size = New System.Drawing.Size(221, 21)
        Me.cboFuture.TabIndex = 0
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(107, 206)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(75, 23)
        Me.btnSubmit.TabIndex = 2
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Flight Selection:"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(188, 206)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'radReservedSeat
        '
        Me.radReservedSeat.AutoSize = True
        Me.radReservedSeat.Location = New System.Drawing.Point(88, 29)
        Me.radReservedSeat.Name = "radReservedSeat"
        Me.radReservedSeat.Size = New System.Drawing.Size(96, 17)
        Me.radReservedSeat.TabIndex = 4
        Me.radReservedSeat.TabStop = True
        Me.radReservedSeat.Text = "Reserved Seat"
        Me.radReservedSeat.UseVisualStyleBackColor = True
        '
        'gboxSeating
        '
        Me.gboxSeating.Controls.Add(Me.lblSeat)
        Me.gboxSeating.Controls.Add(Me.lblDesignatedSeatCost)
        Me.gboxSeating.Controls.Add(Me.cboSeat)
        Me.gboxSeating.Controls.Add(Me.lblReservedSeatCost)
        Me.gboxSeating.Controls.Add(Me.radDesignatedSeat)
        Me.gboxSeating.Controls.Add(Me.radReservedSeat)
        Me.gboxSeating.Location = New System.Drawing.Point(44, 67)
        Me.gboxSeating.Name = "gboxSeating"
        Me.gboxSeating.Size = New System.Drawing.Size(266, 133)
        Me.gboxSeating.TabIndex = 5
        Me.gboxSeating.TabStop = False
        Me.gboxSeating.Text = "Seating Information:"
        '
        'lblSeat
        '
        Me.lblSeat.AutoSize = True
        Me.lblSeat.Location = New System.Drawing.Point(63, 91)
        Me.lblSeat.Name = "lblSeat"
        Me.lblSeat.Size = New System.Drawing.Size(0, 13)
        Me.lblSeat.TabIndex = 7
        '
        'lblDesignatedSeatCost
        '
        Me.lblDesignatedSeatCost.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDesignatedSeatCost.Location = New System.Drawing.Point(6, 52)
        Me.lblDesignatedSeatCost.Name = "lblDesignatedSeatCost"
        Me.lblDesignatedSeatCost.Size = New System.Drawing.Size(79, 20)
        Me.lblDesignatedSeatCost.TabIndex = 7
        '
        'cboSeat
        '
        Me.cboSeat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSeat.FormattingEnabled = True
        Me.cboSeat.Location = New System.Drawing.Point(124, 87)
        Me.cboSeat.Name = "cboSeat"
        Me.cboSeat.Size = New System.Drawing.Size(95, 21)
        Me.cboSeat.TabIndex = 6
        '
        'lblReservedSeatCost
        '
        Me.lblReservedSeatCost.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblReservedSeatCost.Location = New System.Drawing.Point(6, 27)
        Me.lblReservedSeatCost.Name = "lblReservedSeatCost"
        Me.lblReservedSeatCost.Size = New System.Drawing.Size(79, 20)
        Me.lblReservedSeatCost.TabIndex = 6
        '
        'radDesignatedSeat
        '
        Me.radDesignatedSeat.AutoSize = True
        Me.radDesignatedSeat.Location = New System.Drawing.Point(88, 53)
        Me.radDesignatedSeat.Name = "radDesignatedSeat"
        Me.radDesignatedSeat.Size = New System.Drawing.Size(162, 17)
        Me.radDesignatedSeat.TabIndex = 5
        Me.radDesignatedSeat.TabStop = True
        Me.radDesignatedSeat.Text = "Designated Seat at Check In"
        Me.radDesignatedSeat.UseVisualStyleBackColor = True
        '
        'lblFlightInfo
        '
        Me.lblFlightInfo.Location = New System.Drawing.Point(17, 41)
        Me.lblFlightInfo.Name = "lblFlightInfo"
        Me.lblFlightInfo.Size = New System.Drawing.Size(334, 23)
        Me.lblFlightInfo.TabIndex = 6
        '
        'frmCustomerAddFlight
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(363, 242)
        Me.Controls.Add(Me.lblFlightInfo)
        Me.Controls.Add(Me.gboxSeating)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.cboFuture)
        Me.Name = "frmCustomerAddFlight"
        Me.Text = "Book Flight"
        Me.gboxSeating.ResumeLayout(False)
        Me.gboxSeating.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cboFuture As ComboBox
    Friend WithEvents btnSubmit As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents btnExit As Button
    Friend WithEvents radReservedSeat As RadioButton
    Friend WithEvents gboxSeating As GroupBox
    Friend WithEvents lblDesignatedSeatCost As Label
    Friend WithEvents lblReservedSeatCost As Label
    Friend WithEvents radDesignatedSeat As RadioButton
    Friend WithEvents lblSeat As Label
    Friend WithEvents cboSeat As ComboBox
    Friend WithEvents lblFlightInfo As Label
End Class
