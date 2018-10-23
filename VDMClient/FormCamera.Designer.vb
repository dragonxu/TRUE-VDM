<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormCamera
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCamera))
        Me.Player = New Accord.Controls.VideoSourcePlayer()
        Me.cmbcamera = New System.Windows.Forms.ComboBox()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.pbDetected = New System.Windows.Forms.PictureBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.pbStart = New System.Windows.Forms.PictureBox()
        Me.btnAgain = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.pbGet = New System.Windows.Forms.PictureBox()
        Me.TimerDetected = New System.Windows.Forms.Timer(Me.components)
        CType(Me.pbDetected, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbGet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Player
        '
        Me.Player.Location = New System.Drawing.Point(58, 140)
        Me.Player.Name = "Player"
        Me.Player.Size = New System.Drawing.Size(601, 375)
        Me.Player.TabIndex = 27
        Me.Player.Text = "Player"
        Me.Player.VideoSource = Nothing
        '
        'cmbcamera
        '
        Me.cmbcamera.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcamera.FormattingEnabled = True
        Me.cmbcamera.Location = New System.Drawing.Point(33, 20)
        Me.cmbcamera.Name = "cmbcamera"
        Me.cmbcamera.Size = New System.Drawing.Size(200, 26)
        Me.cmbcamera.TabIndex = 28
        Me.cmbcamera.Visible = False
        '
        'btnStart
        '
        Me.btnStart.FlatAppearance.BorderSize = 0
        Me.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStart.Image = CType(resources.GetObject("btnStart.Image"), System.Drawing.Image)
        Me.btnStart.Location = New System.Drawing.Point(259, 524)
        Me.btnStart.Margin = New System.Windows.Forms.Padding(0)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(200, 40)
        Me.btnStart.TabIndex = 29
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'pbDetected
        '
        Me.pbDetected.Location = New System.Drawing.Point(185, 150)
        Me.pbDetected.Name = "pbDetected"
        Me.pbDetected.Size = New System.Drawing.Size(347, 346)
        Me.pbDetected.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbDetected.TabIndex = 30
        Me.pbDetected.TabStop = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(668, 9)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(40, 40)
        Me.btnClose.TabIndex = 32
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'pbStart
        '
        Me.pbStart.Image = CType(resources.GetObject("pbStart.Image"), System.Drawing.Image)
        Me.pbStart.Location = New System.Drawing.Point(100, 69)
        Me.pbStart.Name = "pbStart"
        Me.pbStart.Size = New System.Drawing.Size(531, 50)
        Me.pbStart.TabIndex = 33
        Me.pbStart.TabStop = False
        '
        'btnAgain
        '
        Me.btnAgain.FlatAppearance.BorderSize = 0
        Me.btnAgain.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAgain.Image = CType(resources.GetObject("btnAgain.Image"), System.Drawing.Image)
        Me.btnAgain.Location = New System.Drawing.Point(185, 524)
        Me.btnAgain.Margin = New System.Windows.Forms.Padding(0)
        Me.btnAgain.Name = "btnAgain"
        Me.btnAgain.Size = New System.Drawing.Size(131, 40)
        Me.btnAgain.TabIndex = 34
        Me.btnAgain.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.FlatAppearance.BorderSize = 0
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Image = CType(resources.GetObject("btnOK.Image"), System.Drawing.Image)
        Me.btnOK.Location = New System.Drawing.Point(401, 524)
        Me.btnOK.Margin = New System.Windows.Forms.Padding(0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(131, 40)
        Me.btnOK.TabIndex = 35
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'pbGet
        '
        Me.pbGet.Image = CType(resources.GetObject("pbGet.Image"), System.Drawing.Image)
        Me.pbGet.Location = New System.Drawing.Point(273, 69)
        Me.pbGet.Name = "pbGet"
        Me.pbGet.Size = New System.Drawing.Size(181, 50)
        Me.pbGet.TabIndex = 36
        Me.pbGet.TabStop = False
        '
        'TimerDetected
        '
        Me.TimerDetected.Interval = 1000
        '
        'FormCamera
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(717, 594)
        Me.ControlBox = False
        Me.Controls.Add(Me.pbGet)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnAgain)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.cmbcamera)
        Me.Controls.Add(Me.pbDetected)
        Me.Controls.Add(Me.pbStart)
        Me.Controls.Add(Me.Player)
        Me.Font = New System.Drawing.Font("Arial Unicode MS", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormCamera"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TopMost = True
        CType(Me.pbDetected, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbGet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Player As Accord.Controls.VideoSourcePlayer
    Private WithEvents cmbcamera As ComboBox
    Friend WithEvents btnStart As Button
    Private WithEvents pbDetected As PictureBox
    Friend WithEvents btnClose As Button
    Friend WithEvents pbStart As PictureBox
    Friend WithEvents btnAgain As Button
    Friend WithEvents btnOK As Button
    Friend WithEvents pbGet As PictureBox
    Friend WithEvents TimerDetected As Timer
End Class
