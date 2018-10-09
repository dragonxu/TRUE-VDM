Imports System.IO
Imports System.Net

Public Class UC_Printer
    Inherits System.Web.UI.UserControl

    Dim BL As New Core_BL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindListPrinter()
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

    Private Sub BindListPrinter()
        Dim Printer As New Printer.Printer
        Dim _list As String() = Printer.GetList()

        ddlPrinter.Items.Clear()
        For i As Integer = 0 To _list.Count - 1
            ddlPrinter.Items.Add(_list(i))
            '------------- Select Default ------------
            If ddlPrinter.Items(i).Text = BL.Printer_Name Then
                ddlPrinter.SelectedIndex = i
            End If
        Next

    End Sub



    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim ThePrinter As New Printer.Printer
        Dim Contents As String() = txtContent.Text.Split(vbNewLine)
        Dim Lines As New List(Of Printer.Printer.ContentLine)
        Dim PrinterName As String = ddlPrinter.Items(ddlPrinter.SelectedIndex).Value
        Select Case ThePrinter.GetStatus(PrinterName)
            Case Printer.Printer.PrinterStatus.Offline
                lblResult.Text = "Printer is Offline"
                Exit Sub
            Case Printer.Printer.PrinterStatus.Unknow
                lblResult.Text = "Printer is Unknow"
                Exit Sub
        End Select
        Try
            '------- Transalate DataTable To Printer Contents--------
            For i As Integer = 0 To Contents.Count - 1
                Dim Line As New Printer.Printer.ContentLine
                With Line
                    .Text = Contents(i)
                    .ImagePath = ""
                    .FontSize = 10
                    .FontName = "FontA1x1"
                    .Bold = False
                    .IsColor = False
                    .ContentType = Printer.Printer.ContentType.Text
                End With
                Lines.Add(Line)
            Next
            ThePrinter.Print(PrinterName, Lines)
            lblResult.Text = "command sent"
        Catch ex As Exception
            lblResult.Text = ex.Message
        End Try
    End Sub




End Class