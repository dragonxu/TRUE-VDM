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
        Response.Write(callBackFunction & "" & Print().ToString.ToLower & ");")
    End Sub

    'Private Sub Print()

    '    'Dim Content As String = "สาขาที่ : xiwefweoru owurow(ewrjweir)" & vbLf
    '    'Content &= "ใบยืนยันการรับชำระ         23748923748928324" & vbLf
    '    'Content &= "sasdsadas	xxxs-sdasd-sdasd-dsaa" & vbLf
    '    'Content &= "_________________________________" & vbLf
    '    'Content &= "รายการสินค้า" & vbLf
    '    'Content &= " " & vbLf
    '    'Content &= "1. 45645645             5000.00" & vbLf
    '    'Content &= "eirouewoirwkeruiowuriow uewuiroew" & vbLf
    '    'Content &= "S/N : 3897589345784378  " & vbLf
    '    'Content &= " " & vbLf
    '    'Content &= "_________________________________" & vbLf
    '    'Content &= "TRUE MONEY              25,000.00" & vbLf
    '    'Content &= " " & vbLf
    '    'Content &= "ISV : 3894723842 24 242 432" & vbLf
    '    'Content &= "PAYMENT ID : 23432 4234 2342 4324 2342" & vbLf
    '    'Content &= "PAYMENT CODE : 234 42 42342 4242342" & vbLf
    '    'Content &= "_________________________________" & vbLf
    '    'Content &= " " & vbLf
    '    'Content &= "ขอบคุณที่ใช้บริการ" & vbLf
    '    ''----------------- Ads ----------------

    '    ''----------------- Ads ----------------
    '    'Content &= "_________________________________" & vbLf

    '    Dim Printer As New Printer
    '    Printer.PrinterFont = New Drawing.Font("Verdana", 9)

    '    'Dim settings As New System.Drawing.Printing.PrinterSettings
    '    '--------------- Set PaperWidth ------------
    '    '--------------- Set Margin ------------
    '    Printer.DefaultPageSettings.Margins = New Drawing.Printing.Margins(0, 0, 0, 0)
    '    Printer.TextToPrint = Content
    '    Printer.Print()

    '    'Dim Reader = New IO.StreamReader(Request.InputStream)
    '    'Request.ContentEncoding = Encoding.UTF8
    '    'Request.InputStream.Seek(0, IO.SeekOrigin.Begin)
    '    'Reader.DiscardBufferedData()
    '    'Dim XML As String = XDocument.Load(Reader).ToString

    '    'Dim C As New Converter
    '    'Dim BL As New Core_BL
    '    'Dim Contents As DataTable = C.XMLToDatatable(XML)

    '    ''------------- Generate Result ------------
    '    'Dim Result = New DataTable
    '    'Result.Columns.Add("status", GetType(Boolean))
    '    'Result.Columns.Add("message", GetType(String))
    '    'Dim DR As DataRow = Result.NewRow
    '    'Result.Rows.Add(DR)

    '    '------------- Send Contents To Printer------------
    '    'Dim ThePrinter As New Printer.Printer

    '    ''------------- Check Current Status ---------------
    '    'Select Case ThePrinter.GetStatus(BL.Printer_Name)
    '    '    Case Printer.Printer.PrinterStatus.Offline
    '    '        DR("status") = False
    '    '        DR("message") = "Printer Offline"
    '    '        Response.Write(SingleRowDataTableToJSON(Result))
    '    '        Response.End()
    '    '        Exit Sub
    '    '    Case Printer.Printer.PrinterStatus.Unknow
    '    '        DR("status") = False
    '    '        DR("message") = "Printer Unknow Status"
    '    '        Response.Write(SingleRowDataTableToJSON(Result))
    '    '        Response.End()
    '    '        Exit Sub
    '    'End Select


    '    'Dim Lines As New List(Of Printer.Printer.ContentLine)
    '    'Try
    '    '    '------- Transalate DataTable To Printer Contents--------
    '    '    For i As Integer = 0 To Contents.Rows.Count - 1
    '    '        Dim Line As New Printer.Printer.ContentLine
    '    '        With Line
    '    '            .Text = Contents.Rows(i).Item("Text").ToString
    '    '            .ImagePath = Contents.Rows(i).Item("ImagePath").ToString
    '    '            .FontSize = Contents.Rows(i).Item("FontSize")
    '    '            .FontName = Contents.Rows(i).Item("FontName")
    '    '            .Bold = Contents.Rows(i).Item("Bold")
    '    '            .IsColor = Contents.Rows(i).Item("IsColor")
    '    '            .ContentType = CType(Contents.Rows(i).Item("ContentType"), Printer.Printer.ContentType)
    '    '        End With
    '    '        Lines.Add(Line)
    '    '    Next
    '    '    ThePrinter.Print(BL.Printer_Name, Lines)
    '    '    DR("status") = True
    '    '    DR("message") = "success"
    '    'Catch ex As Exception
    '    '    DR("status") = False
    '    '    DR("message") = ex.Message
    '    'End Try
    '    'Response.Write(SingleRowDataTableToJSON(Result))
    '    'Response.End()
    'End Sub

    Private Function Print() As Boolean

        '-------------Get Post Content------------
        Dim C As New Converter
        Dim Reader As IO.Stream = Request.InputStream
        'Request.ContentEncoding = Encoding.UTF8
        'Request.InputStream.Seek(0, IO.SeekOrigin.Begin)
        'Reader.DiscardBufferedData()
        'Dim Content As String = Reader.ToString
        Dim Content As String = C.ByteToString(C.StreamToByte(Reader), Converter.EncodeType._UTF8)

        Dim Printer As New Printer
        Printer.PrinterFont = New Drawing.Font("Verdana", 9)
        'Dim settings As New System.Drawing.Printing.PrinterSettings
        '--------------- Set PaperWidth ------------
        '--------------- Set Margin ------------
        Printer.DefaultPageSettings.Margins = New Drawing.Printing.Margins(0, 0, 0, 0)
        Printer.TextToPrint = Content
        Printer.Print()

        Return True
    End Function

End Class