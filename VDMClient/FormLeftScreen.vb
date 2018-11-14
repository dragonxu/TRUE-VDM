Imports System.IO
Public Class FormLeftScreen

    Public Property MainForm As FormMain

    Private Sub FormLeftScreen_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Play First File in folder Movie
        Dim Path As String = Application.StartupPath() & "\Movie\"
        Dim ExistFile As Boolean = False
        For i As Integer = 0 To Directory.GetFiles(Path).Count - 1
            Path = Directory.GetFiles(Path)(0)
            ExistFile = True
            Exit For
        Next
        If ExistFile Then
            Player.URL = Path
            Player.uiMode = "none"
            Player.settings.autoStart = True
            Player.settings.setMode("loop", True)
        Else
            Me.Close()
        End If

    End Sub

End Class