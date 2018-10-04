Imports CefSharp
Imports CefSharp.WinForms

Public Class FormMain

    Public ChromeBrowser As ChromiumWebBrowser

    Private Sub FormMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CefSharp.Cef.Shutdown()
    End Sub

    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Cursor.Hide()
        InitChromium()
    End Sub

    Private Sub InitChromium()
        Dim settings As New CefSettings
        CefSharp.Cef.Initialize(settings)
        ChromeBrowser = New ChromiumWebBrowser("about:blank")
        Me.Controls.Add(ChromeBrowser)
        ChromeBrowser.Dock = DockStyle.Fill
        ChromeBrowser.Load("http://localhost/Default.aspx")
    End Sub
End Class
