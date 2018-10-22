Public Class Print
    Inherits System.Web.UI.Page

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
        Response.Write(callBackFunction & "(" & Print().ToString.ToLower & ");")
    End Sub

    Private Function Print() As Boolean

        '-------------Get Post Content------------
        Dim C As New Converter
        Dim Reader As IO.Stream = Request.InputStream
        Dim Content As String = C.ByteToString(C.StreamToByte(Reader), Converter.EncodeType._UTF8)

        If Content = "" AndAlso Not IsNothing(Request.QueryString("Content")) AndAlso Request.QueryString("Content") <> "" Then
            Content = Request.QueryString("Content")
        End If

        Dim Printer As New Printer
        Printer.Content = Content
        Printer.Print()

        Response.Write(callBackFunction & "('true);")

        Return True
    End Function

End Class