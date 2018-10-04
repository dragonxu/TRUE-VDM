Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Printer

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class Print
    Inherits System.Web.Services.WebService

    Dim BL As New Core_BL

    <WebMethod()>
    Public Function Print(ByVal Contents As DataTable) As DataTable


        '------------- Generate Result ------------
        Dim Result = New DataTable
        Result.TableName = "PrintResult"
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
                Return Result
            Case Printer.Printer.PrinterStatus.Unknow
                DR("status") = False
                DR("message") = "Printer Unknow Status"
                Return Result
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

        Return Result
    End Function


End Class