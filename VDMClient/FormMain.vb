Imports CefSharp
Imports CefSharp.WinForms
Imports System.Net
Imports System.IO
Imports System.Diagnostics

Public Class FormMain

    Public ChromeBrowser As ChromiumWebBrowser

    Private Sub FormMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CefSharp.Cef.Shutdown()
    End Sub

    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        Cursor.Hide()
        InitChromium()

        '------------- Start SIM Dispenser------------
        StartProductController()

    End Sub

    Private Sub InitChromium()
        Dim settings As New CefSettings
        CefSharp.Cef.Initialize(settings)
        ChromeBrowser = New ChromiumWebBrowser("about:blank")
        Me.Controls.Add(ChromeBrowser)
        ChromeBrowser.Dock = DockStyle.Fill
        ChromeBrowser.Load("http://localhost/Default.aspx")


    End Sub

    Private Sub StartProductController()

        Dim url As String = "http://localhost/ProductPicker.aspx?Mode=SetHome&callback=test"

        ' Using WebRequest
        'Dim request As WebRequest = WebRequest.Create(url)
        'Dim response As WebResponse = request.GetResponse()
        'Dim result As String = New StreamReader(response.GetResponseStream()).ReadToEnd()
        '' Using WebClient
        Try
            Dim result As String = New WebClient().DownloadString(url)
        Catch : End Try


    End Sub
End Class
