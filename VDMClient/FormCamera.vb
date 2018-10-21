Public Class FormCamera

    Public Property MainForm As FormMain

    Private Sub FormCamera_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.DoubleBuffered = True
        Me.BackColor = Color.White
    End Sub


End Class