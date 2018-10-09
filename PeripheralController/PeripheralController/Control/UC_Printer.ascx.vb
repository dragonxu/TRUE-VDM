Imports System.IO
Imports System.Net

Public Class UC_Printer
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub

    Public Enum PrintContentType
        Text = 1
        Image = 2
    End Enum

    Public Function GEN_DEFAULT_PRINT_FORMAT() As DataTable
        Dim DT As New DataTable
        DT.Columns.Add("Text", GetType(String))
        DT.Columns.Add("ImagePath", GetType(String))
        DT.Columns.Add("FontSize", GetType(Single))
        DT.Columns.Add("FontName", GetType(String))
        DT.Columns.Add("Bold", GetType(Boolean))
        DT.Columns.Add("IsColor", GetType(Boolean))
        DT.Columns.Add("ContentType", GetType(PrintContentType))
        DT.TableName = "PrintContent"
        Return DT
    End Function


    Public Function GEN_DEFAULT_SLIP_HEADER() As DataTable
        Dim Content As DataTable = GEN_DEFAULT_PRINT_FORMAT()
        Content.Rows.Add("   บริษัท ทรู ดิสทริบิวชั่น แอนด์ เซลส์ จำกัด")
        Content.Rows.Add("18 อาคารทรูทาวเวอร์ ถ.รัชดาภิเษก แขวงห้วยขวาง")
        Content.Rows.Add("    เขตห้วยขวาง กรุงเทพมหานคร 10310")
        Content.Rows.Add(" ")
        Content.Rows.Add(" ")
        Return Content
    End Function

    Private Sub BindListPrinter()
        'ddlPrinter.Items.Clear()
        'Dim Printer As New Printer.Printer

        'Dim Content As DataTable = GEN_DEFAULT_PRINT_FORMAT()
        'Content.Rows.Add("   บริษัท ทรู ดิสทริบิวชั่น แอนด์ เซลส์ จำกัด")
        'Content.Rows.Add("18 อาคารทรูทาวเวอร์ ถ.รัชดาภิเษก แขวงห้วยขวาง")
        'Content.Rows.Add("    เขตห้วยขวาง กรุงเทพมหานคร 10310")
        'Content.Rows.Add(" ")
        'Content.Rows.Add(" ")

        '------------ Set Default Parameter
        'Set_Default_Print_Content_Style(Content)

    End Sub

    Public Sub Set_Default_Print_Content_Style(ByRef DT As DataTable)
        For i As Integer = 0 To DT.Rows.Count - 1
            Dim DR As DataRow = DT.Rows(i)
            If IsDBNull(DR("Text")) Then
                DR("Text") = ""
            End If
            If IsDBNull(DR("ImagePath")) Then
                DR("ImagePath") = ""
            End If
            If IsDBNull(DR("FontSize")) Then
                DR("FontSize") = 10
            End If
            If IsDBNull(DR("FontName")) Then
                DR("FontName") = "FontA1x1"
            End If
            If IsDBNull(DR("Bold")) Then
                DR("Bold") = False
            End If
            If IsDBNull(DR("IsColor")) Then
                DR("IsColor") = False
            End If
            If IsDBNull(DR("ContentType")) Then
                DR("ContentType") = PrintContentType.Text
            End If
        Next
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim DT As DataTable = GEN_DEFAULT_SLIP_HEADER()
        postXMLData("http://localhost/Print.aspx?Mode=Print", DatatableToXML(DT))
    End Sub

    Public Function DatatableToXML(ByVal DT As DataTable) As String
        Using Writer As New StringWriter()
            DT.WriteXml(Writer)
            Return Writer.ToString()
        End Using
    End Function

    Public Function postXMLData(ByVal URL As String, ByVal XMLString As String) As String
        Dim request As HttpWebRequest = DirectCast(WebRequest.Create(URL), HttpWebRequest)
        Dim bytes As Byte()
        bytes = System.Text.Encoding.UTF8.GetBytes(XMLString)
        request.ContentType = "text/xml"

        request.ContentLength = bytes.Length
        request.Method = "POST"
        Dim requestStream As Stream = request.GetRequestStream()
        requestStream.Write(bytes, 0, bytes.Length)
        requestStream.Close()
        Dim resp As HttpWebResponse
        Try
            resp = DirectCast(request.GetResponse(), HttpWebResponse)
            If resp.StatusCode = HttpStatusCode.OK Then
                Dim responseStream As Stream = resp.GetResponseStream()
                Dim responseStr As String = New StreamReader(responseStream).ReadToEnd()
                Return responseStr
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

End Class