
Public Class ScanPassport
    Inherits System.Web.UI.Page

    Dim BL As New Core_BL
    Dim ResultFileName As String = BL.PassportScanPath & "\Scanned.bmp"

    Private ReadOnly Property Scanner As QuantumPassport.Scanner
        Get
            If IsNothing(Application("PassportScanner")) Then
                '----------- Init Object And Connect---------
                Dim _scanner As New QuantumPassport.Scanner()
                _scanner.FilePath = BL.PassportScanPath

                Threading.Thread.Sleep(200)

                Application.Lock()
                Application("PassportScanner") = _scanner
                Application.UnLock()

            End If
            Return Application("PassportScanner")
        End Get
    End Property

    Private ReadOnly Property Mode As String
        Get
            If Not IsNothing(Request.QueryString("Mode")) Then
                Return Request.QueryString("Mode").ToString
            Else
                Return ""
            End If
        End Get
    End Property

    Private ReadOnly Property TimeOut As Integer '---------- Second -----------
        Get
            If IsNumeric(Request.QueryString("timeout")) Then
                Return CInt(Request.QueryString("timeout"))
            Else
                Return 0
            End If
        End Get
    End Property

    Private ReadOnly Property callBackFunction As String
        Get
            If Not IsNothing(Request.QueryString("callback")) Then
                Return Request.QueryString("callback")
            Else
                Return ""
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Select Case Mode
            Case "Scan"
                Scan()
        End Select
    End Sub

    Private MRZ As String = ""
    Private Sub Scan()
        AddHandler Scanner.GetResult, AddressOf Scanner_GetResult
        Scanner.StartScan()
        Dim EndWait As DateTime = DateAdd(DateInterval.Second, TimeOut, Now)

        Scanner.ManualScan()
        '------------- รอจนกว่าจะScan -------
        While MRZ = "" And Now < EndWait
            Threading.Thread.Sleep(200)
        End While
        '------------- ไม่ Scan สักที -------------
        Try : Scanner.CloseScanner() : Catch : End Try
        callBack()
    End Sub

    Protected Sub Scanner_GetResult(ByVal sender As Object, ByVal e As System.EventArgs)
        MRZ = Scanner.ReadMRZ
    End Sub

    Private Sub callBack()
        Response.Write(callBackFunction & "('" & MRZ & "');")
    End Sub

End Class