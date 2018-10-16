Imports CefSharp
Imports CefSharp.WinForms
Imports System.Net
Imports System.IO
Imports System.Diagnostics

Public Class FormMain

    Public ChromeBrowser As ChromiumWebBrowser
    Dim StartURL As String = "http://localhost:62820/Front_UI/Default.aspx?KO_ID=1" '********** Production Check '-********** 
    'Dim StartURL As String = "http://localhost"

    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        CheckForIllegalCrossThreadCalls = False

        LoadKeyboard()

        'Cursor.Hide() '********** Production Check '-********** 
        InitChromium()
        '------------- Start SIM Dispenser------------
        StartProductController() '********** Production Check '-********** 
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
            'CefSharp.Cef.Shutdown()
            Application.Exit()
        End If
    End Sub


    Private Sub ChromeBrowser_FrameLoadEnd(sender As Object, e As FrameLoadEndEventArgs)

        Select Case True
            Case Not e.Frame.IsMain And e.Frame.Url.IndexOf("psipay.bangkokbank") > -1
                Keyboard.Show()
            Case Else
                Keyboard.Hide()
        End Select

    End Sub

    Dim Keyboard As FormKeyboard = Nothing

    Private Sub LoadKeyboard()
        Keyboard = New FormKeyboard
        Keyboard.MainForm = Me
    End Sub

    'Private Sub ShowKeyboard()
    '    Keyboard.Show()
    'End Sub

    'Private Sub HideKeyboard()
    '    Try
    '        Keyboard.Hide()
    '    Catch ex As Exception
    '    End Try
    'End Sub

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
