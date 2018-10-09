Public Class Print
    Inherits System.Web.UI.Page

    Private ReadOnly Property Mode
        Get
            Try
                Return Request.QueryString("Mode")
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Select Case Mode
            Case "Print"
                Print()
            Case "GetList"
                '------------- Send Contents To Printer------------
                Dim ThePrinter As New Printer.Printer
                Dim _list As String() = ThePrinter.GetList
                For i As Integer = 0 To _list.Count - 1
                    Response.Write(_list(i) & "<br/>")
                Next
                Response.End()
        End Select

    End Sub

    Private Sub Print()
        'If Request.ContentType <> "application/x-www-form-urlencoded; charset=UTF-8" Then Exit Sub
        'Throw New HttpException(500, "Unexpected Content-Type")

        Dim Reader = New IO.StreamReader(Request.InputStream)
        Request.ContentEncoding = Encoding.UTF8
        Request.InputStream.Seek(0, IO.SeekOrigin.Begin)
        Reader.DiscardBufferedData()
        Dim XML As String = XDocument.Load(Reader).ToString

        Dim C As New Converter
        Dim BL As New Core_BL
        Dim Contents As DataTable = C.XMLToDatatable(XML)

        '------------- Generate Result ------------
        Dim Result = New DataTable
        Result.Columns.Add("status", GetType(Boolean))
        Result.Columns.Add("message", GetType(String))
        Dim DR As DataRow = Result.NewRow
        Result.Rows.Add(DR)

        '------------- Send Contents To Printer------------
        Dim ThePrinter As New Printer.Printer

        '------------- Check Current Status ---------------
        Select Case ThePrinter.GetStatus(BL.Printer_Name)
            Case Printer.Printer.PrinterStatus.Offline
                DR("status") = False
                DR("message") = "Printer Offline"
                Response.Write(SingleRowDataTableToJSON(Result))
                Response.End()
                Exit Sub
            Case Printer.Printer.PrinterStatus.Unknow
                DR("status") = False
                DR("message") = "Printer Unknow Status"
                Response.Write(SingleRowDataTableToJSON(Result))
                Response.End()
                Exit Sub
        End Select


        Dim Lines As New List(Of Printer.Printer.ContentLine)
        Try
            '------- Transalate DataTable To Printer Contents--------
            For i As Integer = 0 To Contents.Rows.Count - 1
                Dim Line As New Printer.Printer.ContentLine
                With Line
                    .Text = Contents.Rows(i).Item("Text").ToString
                    .ImagePath = Contents.Rows(i).Item("ImagePath").ToString
                    .FontSize = Contents.Rows(i).Item("FontSize")
                    .FontName = Contents.Rows(i).Item("FontName")
                    .Bold = Contents.Rows(i).Item("Bold")
                    .IsColor = Contents.Rows(i).Item("IsColor")
                    .ContentType = CType(Contents.Rows(i).Item("ContentType"), Printer.Printer.ContentType)
                End With
                Lines.Add(Line)
            Next
            ThePrinter.Print(BL.Printer_Name, Lines)
            DR("status") = True
            DR("message") = "success"
        Catch ex As Exception
            DR("status") = False
            DR("message") = ex.Message
        End Try
        Response.Write(SingleRowDataTableToJSON(Result))
        Response.End()
    End Sub

End Class