<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnCalculate = New System.Windows.Forms.Button()
        Me.lstInput = New System.Windows.Forms.ListBox()
        Me.lstOutput = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(119, 269)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 0
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnCalculate
        '
        Me.btnCalculate.Enabled = False
        Me.btnCalculate.Location = New System.Drawing.Point(442, 269)
        Me.btnCalculate.Name = "btnCalculate"
        Me.btnCalculate.Size = New System.Drawing.Size(75, 23)
        Me.btnCalculate.TabIndex = 1
        Me.btnCalculate.Text = "Statistics"
        Me.btnCalculate.UseVisualStyleBackColor = True
        '
        'lstInput
        '
        Me.lstInput.FormattingEnabled = True
        Me.lstInput.Location = New System.Drawing.Point(12, 12)
        Me.lstInput.Name = "lstInput"
        Me.lstInput.Size = New System.Drawing.Size(308, 251)
        Me.lstInput.TabIndex = 3
        '
        'lstOutput
        '
        Me.lstOutput.FormattingEnabled = True
        Me.lstOutput.Location = New System.Drawing.Point(326, 12)
        Me.lstOutput.Name = "lstOutput"
        Me.lstOutput.Size = New System.Drawing.Size(308, 251)
        Me.lstOutput.TabIndex = 4
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(645, 300)
        Me.Controls.Add(Me.lstOutput)
        Me.Controls.Add(Me.lstInput)
        Me.Controls.Add(Me.btnCalculate)
        Me.Controls.Add(Me.btnAdd)
        Me.Name = "Form1"
        Me.Text = "Rainfall Calculator"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnAdd As Button
    Friend WithEvents btnCalculate As Button
    Friend WithEvents lstInput As ListBox
    Friend WithEvents lstOutput As ListBox
End Class
