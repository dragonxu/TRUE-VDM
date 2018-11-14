Imports CefSharp
Imports CefSharp.WinForms
Imports System.Net
Imports System.IO
Imports System.Diagnostics

Public Class FormMain

    Public WithEvents ChromeBrowser As ChromiumWebBrowser
    'Dim StartURL As String = "http://119.46.96.185/Front_UI/Default.aspx?KO_ID=1" '********** Production Check '-********** 
    'Dim StartURL As String = "http://localhost:62820/Front_UI/Thank_You.aspx" '********** Production Check '-********** 
    Dim StartURL As String = "http://localhost:49211"

    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        CheckForIllegalCrossThreadCalls = False

        StartLeftMovie()

        LoadKeyboard()

        'Cursor.Hide() '********** Production Check '-********** 
        InitChromium()
        '------------- Start SIM Dispenser------------
        StartProductController() '********** Production Check '-********** 
    End Sub

    Public FormLeftPlayer As FormLeftScreen = Nothing
    Private Sub StartLeftMovie()

        If Screen.AllScreens.Count = 1 Then Exit Sub

        FormLeftPlayer = New FormLeftScreen

        FormLeftPlayer.MainForm = Me
        FormLeftPlayer.Show()
        FormLeftPlayer.Location = New Point(-1080, 0)
        FormLeftPlayer.WindowState = FormWindowState.Maximized
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
        On Error Resume Next
        If ChromeBrowser.GetBrowser.GetFrameCount = 2 Then
            If ChromeBrowser.GetFocusedFrame.Url.ToUpper.IndexOf(".bangkokbank.com".ToUpper) > -1 Then
                Keyboard.Show()
            End If
        End If
    End Sub

    'Dim LastLoadCam As DateTime = Now
    Dim Camera As FormCamera = Nothing
    Dim LastPrintTime As DateTime = Now '------------- Prevent Print 2 Copy
    Dim LastPrintURL As String = "" '------------- Prevent Print 2 Copy
    Private Sub ChromeBrowser_FrameLoadEnd(sender As Object, e As FrameLoadEndEventArgs) Handles ChromeBrowser.FrameLoadEnd

        Select Case True
            Case Not e.Frame.IsMain And e.Frame.Url.ToUpper.IndexOf(".bangkokbank.com".ToUpper) > -1
                Keyboard.Show()
            Case Not e.Frame.IsMain And e.Frame.Url.ToUpper.IndexOf("/CAMCAPTURE.ASPX") > -1
                If Not IsNothing(Camera) Then Exit Sub
                Camera = New FormCamera
                '------------- Get Alias -------------
                Camera.MainURL = ChromeBrowser.GetMainFrame.Url
                Dim urlPart As String() = e.Frame.Url.Split("?")
                Camera.aliasname = "คุณ"
                If urlPart.Count > 1 Then
                    Dim Params As String() = urlPart(1).Split("&")
                    For i As Integer = 0 To Params.Count - 1
                        If Params(i).ToLower.IndexOf("cusname=") > -1 Then
                            Camera.aliasname = Params(i).Split("=")(1)
                            Exit For
                        End If
                    Next
                End If

                Camera.MainForm = Me
                Camera.Show(Me)

            Case Not e.Frame.IsMain And e.Frame.Url.ToUpper.IndexOf("/PRINTCONTENT.ASPX") > -1

                Dim URL As String = e.Frame.Url

                '------------- Prevent Print 2 Copy
                If LastPrintURL = URL And DateDiff(DateInterval.Second, LastPrintTime, Now) < 5 Then
                    Exit Sub
                End If

                Dim C As New Converter
                Dim Data As Byte() = New WebClient().DownloadData(URL)
                Dim Content As String = C.ByteToString(Data, Converter.EncodeType._UTF8)

                If Content <> "" Then
                    Dim Printer As New Printer
                    Printer.Content = Content
                    Printer.Print()
                    LastPrintTime = Now '------------- Prevent Print 2 Copy
                    LastPrintURL = URL '------------- Prevent Print 2 Copy
                End If

            Case Else
                Keyboard.Hide()
                ClearCamera()

        End Select
    End Sub

    Private Sub ProductPickerCallBack(ByVal callback As String, ByVal Result As Boolean, ByVal Message As String)
        Dim Script As String = callback & "(" & Result.ToString.ToLower & ",'" & Message.Replace("'", "") & "');"
        ChromeBrowser.GetBrowser.MainFrame.ExecuteJavaScriptAsync(Script)
    End Sub

    Public Sub ClearCamera()
        If Not IsNothing(Camera) Then
            Camera.Close()
            Camera = Nothing
        End If
    End Sub

    Dim Keyboard As FormKeyboard = Nothing

    Private Sub LoadKeyboard()
        Keyboard = New FormKeyboard
        Keyboard.MainForm = Me
    End Sub


    Private Sub StartProductController()
        '--------------- Set Home --------------
        Dim url As String = StartURL & "/ProductPicker.aspx?Mode=SetHome&callback=test"
        Try
            Dim result As String = New WebClient().DownloadString(url)
        Catch : End Try
    End Sub

End Class