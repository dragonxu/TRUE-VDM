Imports CefSharp
Imports CefSharp.WinForms
Imports System.Net
Imports System.IO

Public Class FormMain

    Public ChromeBrowser As ChromiumWebBrowser

    Private Sub FormMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CefSharp.Cef.Shutdown()
    End Sub

    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Cursor.Hide()
        InitChromium()

        '------------- Start Product Controller------------
        Dim ProductThred As New Threading.Thread(AddressOf StartProductController)
        ProductThred.Priority = Threading.ThreadPriority.Normal
        ProductThred.IsBackground = True
        ProductThred.Start()

        '------------- Start SIM Dispenser------------

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
        Dim WebRequest As WebRequest = WebRequest.Create("http://localhost/ProductPicker.aspx?Mode=SetHome&callback=test")
        WebRequest.Method = "POST"
        WebRequest.Timeout = 5 * 60 * 1000 '5 นาที
        Dim WebResponse = WebRequest.GetResponse().GetResponseStream()
        Dim Reader As New StreamReader(WebResponse)
        Dim Result = Reader.ReadToEnd()
        Reader.Close()
        WebResponse.Close()
    End Sub
End Class
