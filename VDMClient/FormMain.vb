Imports CefSharp
Imports CefSharp.WinForms
Imports System.Net
Imports System.IO
Imports System.Diagnostics

Public Class FormMain

    Public ChromeBrowser As ChromiumWebBrowser

    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        CheckForIllegalCrossThreadCalls = False
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
        AddHandler ChromeBrowser.AddressChanged, AddressOf ChromeBrowser_AddressChanged
    End Sub

    Private Sub ChromeBrowser_AddressChanged(sender As Object, e As AddressChangedEventArgs)
        If e.Address = "about:blank" Then
            On Error Resume Next
            'CefSharp.Cef.Shutdown()
            Application.Exit()
        End If
    End Sub

    Private Sub StartProductController()
        '--------------- Set Home --------------
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
