<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDelete
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboPassengers = New System.Windows.Forms.ComboBox()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(239, 100)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(1)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(119, 44)
        Me.btnExit.TabIndex = 7
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(40, 43)
        Me.Label1.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Passenger to Delete:"
        '
        'cboPassengers
        '
        Me.cboPassengers.FormattingEnabled = True
        Me.cboPassengers.Location = New System.Drawing.Point(160, 43)
        Me.cboPassengers.Margin = New System.Windows.Forms.Padding(1)
        Me.cboPassengers.Name = "cboPassengers"
        Me.cboPassengers.Size = New System.Drawing.Size(235, 21)
        Me.cboPassengers.TabIndex = 5
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(77, 100)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(1)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(119, 44)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "Delete Passenger"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'frmDelete
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(438, 174)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboPassengers)
        Me.Controls.Add(Me.btnDelete)
        Me.Name = "frmDelete"
        Me.Text = "Delete Passengers"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnExit As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents cboPassengers As ComboBox
    Friend WithEvents btnDelete As Button
End Class
