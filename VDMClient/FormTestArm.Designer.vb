<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormTestArm
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
        Me.btnHome = New System.Windows.Forms.Button()
        Me.txtPOS = New System.Windows.Forms.TextBox()
        Me.btnPick = New System.Windows.Forms.Button()
        Me.btnOpenGate = New System.Windows.Forms.Button()
        Me.btnCloseGate = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnHome
        '
        Me.btnHome.Location = New System.Drawing.Point(125, 12)
        Me.btnHome.Name = "btnHome"
        Me.btnHome.Size = New System.Drawing.Size(75, 23)
        Me.btnHome.TabIndex = 0
        Me.btnHome.Text = "SetHome"
        Me.btnHome.UseVisualStyleBackColor = True
        '
        'txtPOS
        '
        Me.txtPOS.Location = New System.Drawing.Point(12, 53)
        Me.txtPOS.Name = "txtPOS"
        Me.txtPOS.Size = New System.Drawing.Size(100, 20)
        Me.txtPOS.TabIndex = 1
        '
        'btnPick
        '
        Me.btnPick.Location = New System.Drawing.Point(125, 50)
        Me.btnPick.Name = "btnPick"
        Me.btnPick.Size = New System.Drawing.Size(75, 23)
        Me.btnPick.TabIndex = 0
        Me.btnPick.Text = "GoPick"
        Me.btnPick.UseVisualStyleBackColor = True
        '
        'btnOpenGate
        '
        Me.btnOpenGate.Location = New System.Drawing.Point(125, 91)
        Me.btnOpenGate.Name = "btnOpenGate"
        Me.btnOpenGate.Size = New System.Drawing.Size(75, 23)
        Me.btnOpenGate.TabIndex = 0
        Me.btnOpenGate.Text = "OpenGate"
        Me.btnOpenGate.UseVisualStyleBackColor = True
        '
        'btnCloseGate
        '
        Me.btnCloseGate.Location = New System.Drawing.Point(125, 132)
        Me.btnCloseGate.Name = "btnCloseGate"
        Me.btnCloseGate.Size = New System.Drawing.Size(75, 23)
        Me.btnCloseGate.TabIndex = 0
        Me.btnCloseGate.Text = "CloseGate"
        Me.btnCloseGate.UseVisualStyleBackColor = True
        '
        'FormTestArm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(526, 261)
        Me.Controls.Add(Me.txtPOS)
        Me.Controls.Add(Me.btnCloseGate)
        Me.Controls.Add(Me.btnOpenGate)
        Me.Controls.Add(Me.btnPick)
        Me.Controls.Add(Me.btnHome)
        Me.Name = "FormTestArm"
        Me.Text = "FormTestArm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnHome As Button
    Friend WithEvents txtPOS As TextBox
    Friend WithEvents btnPick As Button
    Friend WithEvents btnOpenGate As Button
    Friend WithEvents btnCloseGate As Button
End Class
