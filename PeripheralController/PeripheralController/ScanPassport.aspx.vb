
Public Class ScanPassport
    Inherits System.Web.UI.Page

    Dim BL As New Core_BL
    Dim ResultFileName As String = BL.PassportScanPath & "\Scanned.bmp"

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
            Case "Stop"
                StopScan()
        End Select
    End Sub

    Private MRZ As String = ""

    Private Sub Scan()
        Dim EndWait As DateTime = DateAdd(DateInterval.Second, TimeOut, Now)
        Dim LastModify As DateTime = IO.File.GetLastWriteTime(BL.PassportScanPath & "\MRZ.txt")
        StartScan()
        '------------- รอจนกว่าจะScan -------
        While MRZ = "" And Now < EndWait
            Threading.Thread.Sleep(100)
            If IO.File.GetLastWriteTime(BL.PassportScanPath & "\MRZ.txt") <> LastModify Then
                MRZ = ReadMRZ()
            End If
        End While
        ''------------- สิ้นทุดการรอคอย -------------
        Try : StopScan() : Catch : End Try
        callBack()
    End Sub

    Private Sub StartScan()
        '------------ Just Write Text file ------
        Dim C As New Converter
        Dim B As Byte() = C.StringToByte("start", Converter.EncodeType._UTF8)
        Dim S As IO.Stream = IO.File.Open(BL.PassportScanPath & "\command.txt", IO.FileMode.OpenOrCreate, IO.FileAccess.Write, IO.FileShare.ReadWrite)
        S.Write(B, 0, B.Length)
        S.Close()
    End Sub

    Private Sub StopScan()
        '------------ Just Write Text file ------
        Dim C As New Converter
        Dim B As Byte() = C.StringToByte("stop", Converter.EncodeType._UTF8)
        Dim S As IO.Stream = IO.File.Open(BL.PassportScanPath & "\command.txt", IO.FileMode.OpenOrCreate, IO.FileAccess.Write, IO.FileShare.ReadWrite)
        S.Write(B, 0, B.Length)
        S.Close()
    End Sub

    Private Function ReadMRZ() As String
        Dim C As New Converter
        Dim S As IO.Stream = IO.File.Open(BL.PassportScanPath & "\MRZ.txt", IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite)
        Dim B() As Byte = C.StreamToByte(S)
        Return C.ByteToString(B, Converter.EncodeType._UTF8)
    End Function

    Private Sub callBack()

        'Split MRZ
        Dim ThePassport As New Passport
        ThePassport = ThePassport.MRZToCusInfo(MRZ)

        Dim C As New Converter
        Dim ImagePath As String = BL.PassportScanPath & "\Scanned.bmp"
        If IO.File.Exists(ImagePath) Then
            Dim S As IO.Stream = IO.File.Open(ImagePath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite)
            Dim Img As Drawing.Image = Drawing.Image.FromStream(S)
            S.Close()
            '------------Scale Down Image -----------
            Img = ScaleImage(Img, Img.Height / 3, Img.Width / 3)
            Img.Save(ImagePath)
            ThePassport.Photo = C.ImageToBlob(Img)

        End If

        Dim Script As String = callBackFunction & "("

        Script &= "'" & ThePassport.FirstName.Replace("'", "") & "',"
        Script &= "'" & ThePassport.MiddleName.Replace("'", "") & "',"
        Script &= "'" & ThePassport.LastName.Replace("'", "") & "',"
        Script &= "'" & ThePassport.DocType.Replace("'", "") & "',"
        Script &= "'" & ThePassport.Nationality.Replace("'", "") & "',"
        Script &= "'" & ThePassport.PassportNo.Replace("'", "") & "',"
        Script &= "'" & ThePassport.DateOfBirth.Replace("'", "") & "',"
        Script &= "'" & ThePassport.Sex.Replace("'", "") & "',"
        Script &= "'" & ThePassport.Expire.Replace("'", "") & "',"
        Script &= "'" & ThePassport.PersonalID.Replace("'", "") & "',"
        Script &= "'" & ThePassport.IssueCountry.Replace("'", "") & "',"
        Script &= "'" & ThePassport.MRZ.Replace("'", "") & "',"
        Script &= "'" & ThePassport.Photo.Replace("'", "") & "');"
        Dim Temp As Long = ThePassport.Photo.Length
        Response.Write(Script)
    End Sub


End Class