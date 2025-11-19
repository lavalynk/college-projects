<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerMain
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
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnAddFlight = New System.Windows.Forms.Button()
        Me.btnPastFlights = New System.Windows.Forms.Button()
        Me.btnShowFuture = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(13, 13)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(174, 47)
        Me.btnUpdate.TabIndex = 0
        Me.btnUpdate.Text = "Update Customer"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnAddFlight
        '
        Me.btnAddFlight.Location = New System.Drawing.Point(193, 13)
        Me.btnAddFlight.Name = "btnAddFlight"
        Me.btnAddFlight.Size = New System.Drawing.Size(174, 47)
        Me.btnAddFlight.TabIndex = 1
        Me.btnAddFlight.Text = "Add Flight"
        Me.btnAddFlight.UseVisualStyleBackColor = True
        '
        'btnPastFlights
        '
        Me.btnPastFlights.Location = New System.Drawing.Point(373, 13)
        Me.btnPastFlights.Name = "btnPastFlights"
        Me.btnPastFlights.Size = New System.Drawing.Size(174, 47)
        Me.btnPastFlights.TabIndex = 2
        Me.btnPastFlights.Text = "Show Past Flights"
        Me.btnPastFlights.UseVisualStyleBackColor = True
        '
        'btnShowFuture
        '
        Me.btnShowFuture.Location = New System.Drawing.Point(553, 13)
        Me.btnShowFuture.Name = "btnShowFuture"
        Me.btnShowFuture.Size = New System.Drawing.Size(174, 47)
        Me.btnShowFuture.TabIndex = 3
        Me.btnShowFuture.Text = "Show Future Flights"
        Me.btnShowFuture.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(332, 66)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'frmCustomerMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(732, 94)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnShowFuture)
        Me.Controls.Add(Me.btnPastFlights)
        Me.Controls.Add(Me.btnAddFlight)
        Me.Controls.Add(Me.btnUpdate)
        Me.Name = "frmCustomerMain"
        Me.Text = "Customer Main Menu"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnUpdate As Button
    Friend WithEvents btnAddFlight As Button
    Friend WithEvents btnPastFlights As Button
    Friend WithEvents btnShowFuture As Button
    Friend WithEvents btnExit As Button
End Class
