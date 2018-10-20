Imports ThaiNationalIDCard
Public Class ScanIDCard
    Inherits System.Web.UI.Page


    Dim BL As New Core_BL

    Private ReadOnly Property CardReader As ThaiIDCard
        Get
            If IsNothing(Application("ThaiCardReader")) Then
                '----------- Init Object And Connect---------
                Dim _reader As New ThaiIDCard

                Threading.Thread.Sleep(100)

                Application.Lock()
                Application("ThaiCardReader") = _reader
                Application.UnLock()

            End If
            Return Application("ThaiCardReader")
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

    Private ReadOnly Property callBackErrorFunction As String
        Get
            If Not IsNothing(Request.QueryString("callbackError")) Then
                Return Request.QueryString("callbackError")
            Else
                Return ""
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        WaitToRead()
    End Sub

    Dim PersonalInfo As Personal = Nothing
    Private Sub WaitToRead()


        Dim EndWait As DateTime = DateAdd(DateInterval.Second, TimeOut, Now)

        Dim ReaderList As String() = CardReader.GetReaders
        If Array.IndexOf(ReaderList, BL.IDCardReader) = -1 Then
            ErrorMessage = "No card Reader found"
            callBackError()
            Exit Sub
        End If
        '------------- รอจนกว่าจะScan -------
        While IsNothing(PersonalInfo) And Now < EndWait
            Try
                PersonalInfo = CardReader.readAllPhoto
            Catch : End Try
            Threading.Thread.Sleep(100)
        End While
        '------------- สิ้นทุดการรอคอย -------------
        If IsNothing(PersonalInfo) Then
            callBackError()
        Else
            callBack()
        End If

    End Sub

    Private ErrorMessage As String = ""

    Private Sub callBackError()
        Dim Script As String = callBackErrorFunction & "('" & ErrorMessage.Replace("'", "") & "');"
        Response.Write(Script)
        Response.End()
    End Sub

    Private Sub callBack()
        Dim C As New Converter
        Dim Script As String = callBackFunction & "("
        With PersonalInfo
            Script &= "'" & .Citizenid.Replace("'", "") & "',"
            Script &= "'" & .Th_Prefix.Replace("'", "") & "',"
            Script &= "'" & .Th_Firstname.Replace("'", "") & "',"
            Script &= "'" & .Th_Middlename.Replace("'", "") & "',"
            Script &= "'" & .Th_Lastname.Replace("'", "") & "',"
            Script &= "'" & .En_Prefix.Replace("'", "") & "',"
            Script &= "'" & .En_Firstname.Replace("'", "") & "',"
            Script &= "'" & .En_Middlename.Replace("'", "") & "',"
            Script &= "'" & .En_Lastname.Replace("'", "") & "',"
            Script &= "'" & .Sex.Replace("'", "") & "',"
            Script &= "'" & .Birthday.ToString("yyyy-MM-dd") & "',"
            Script &= "'" & .Address.Replace("'", "") & "',"
            Script &= "'" & .addrHouseNo.Replace("'", "") & "',"
            Script &= "'" & .addrVillageNo.Replace("'", "") & "',"
            Script &= "'" & .addrLane.Replace("'", "") & "',"
            Script &= "'" & .addrRoad.Replace("'", "") & "',"
            Script &= "'" & .addrTambol.Replace("'", "") & "',"
            Script &= "'" & .addrAmphur.Replace("'", "") & "',"
            Script &= "'" & .addrProvince.Replace("'", "") & "',"
            Script &= "'" & .Issue.ToString("yyyy-MM-dd") & "',"
            Script &= "'" & .Issuer.Replace("'", "") & "',"
            Script &= "'" & .Expire.ToString("yyyy-MM-dd") & "',"

            Dim Img As Drawing.Image = Drawing.Image.FromStream(C.ByteToStream(.PhotoRaw))

            Script &= "'" & C.ImageToBlob(Img) & "');"
        End With
        Response.Write(Script)
        Response.End()
    End Sub


End Class