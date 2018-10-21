Imports CefSharp
Imports CefSharp.WinForms
Imports System.Net
Imports System.IO
Imports System.Diagnostics

Public Class FormMain

    Public WithEvents ChromeBrowser As ChromiumWebBrowser
    Dim StartURL As String = "http://localhost:62820/Front_UI/Default.aspx?KO_ID=1" '********** Production Check '-********** 
    'Dim StartURL As String = "http://localhost"

    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        CheckForIllegalCrossThreadCalls = False

        LoadKeyboard()
        LoadCamera()

        'Cursor.Hide() '********** Production Check '-********** 
        InitChromium()
        '------------- Start SIM Dispenser------------
        'StartProductController() '********** Production Check '-********** 
    End Sub

    Private Sub InitChromium()
        Dim settings As New CefSettings
        CefSharp.Cef.Initialize(settings)
        ChromeBrowser = New ChromiumWebBrowser("about:blank")
        Me.Controls.Add(ChromeBrowser)
        ChromeBrowser.Dock = DockStyle.Fill
        ChromeBrowser.Load(StartURL)
        AddHandler ChromeBrowser.AddressChanged, AddressOf ChromeBrowser_AddressChanged
        AddHandler ChromeBrowser.FrameLoadEnd, AddressOf ChromeBrowser_FrameLoadEnd
    End Sub

    Private Sub ChromeBrowser_AddressChanged(sender As Object, e As AddressChangedEventArgs)
        If e.Address = "about:blank" Then
            On Error Resume Next
            Application.Exit()
        End If
    End Sub

    Private Sub ChromeBrowser_Click(sender As Object, e As EventArgs) Handles ChromeBrowser.Click
        If ChromeBrowser.GetBrowser.GetFrameCount = 2 Then
            If ChromeBrowser.GetFocusedFrame.Url.IndexOf("psipay.bangkokbank") > -1 Then
                Keyboard.Show()
            End If
        End If
    End Sub

    Private Sub ChromeBrowser_FrameLoadEnd(sender As Object, e As FrameLoadEndEventArgs) Handles ChromeBrowser.FrameLoadEnd
        Select Case True
            Case Not e.Frame.IsMain And e.Frame.Url.IndexOf("psipay.bangkokbank") > -1
                Keyboard.Show()
            Case Not e.Frame.IsMain And e.Frame.Url.ToUpper.IndexOf("/CAMCAPTURE.ASPX") > -1
                Camera.Show()
            Case Else
                Keyboard.Hide()
                Camera.Hide()
        End Select
    End Sub

    Dim Keyboard As FormKeyboard = Nothing
    Dim Camera As FormCamera = Nothing

    Private Sub LoadKeyboard()
        Keyboard = New FormKeyboard
        Keyboard.MainForm = Me
    End Sub

    Private Sub LoadCamera()
        Camera = New FormCamera
        Camera.MainForm = Me
    End Sub

    Private Sub StartProductController()
        '--------------- Set Home --------------
        Dim url As String = StartURL & "/ProductPicker.aspx?Mode=SetHome&callback=test"
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
