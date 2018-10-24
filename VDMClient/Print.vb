Imports System.Drawing
Imports System.Drawing.Printing

Public Class Printer

    WithEvents Document As New PrintDocument

    Public Content As String = ""

    Public PrinterName As String = ""
    Public PrintFont As New Font("Arial Unicode MS", 9, FontStyle.Regular, GraphicsUnit.Point)
    Dim Brush As New SolidBrush(Color.Black)

    Private Sub Document_BeginPrint(sender As Object, e As PrintEventArgs) Handles Document.BeginPrint
        '-------------- Setting Page -------------
        Document.DefaultPageSettings.Landscape = False
        Document.PrinterSettings.Copies = 1
        If PrinterName <> "" Then
            Document.PrinterSettings.PrinterName = PrinterName
        End If
        'Dim PaperSize As New PaperSize
        'PaperSize.Width = 500
        'PaperSize.Height = 1600
        'Document.DefaultPageSettings.PaperSize = PaperSize
        Document.DefaultPageSettings.Margins.Left = 0
        Document.DefaultPageSettings.Margins.Right = 0
        Document.DefaultPageSettings.Margins.Top = 0
        Document.DefaultPageSettings.Margins.Bottom = 0
    End Sub

    Dim TotalLine As Integer = 0
    Dim CurrentLine As Integer = 0
    Dim LinePerPage As Integer = 32
    Dim PrintLines As String() = {}
    Private Sub Document_PrintPage(sender As Object, e As PrintPageEventArgs) Handles Document.PrintPage

        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        e.PageSettings.Landscape = False


        While PrintLines.Count > 0 And CurrentLine < LinePerPage

            Dim line As String = PrintLines(0)
            Dim s As SizeF = e.Graphics.MeasureString(line, PrintFont)
            Dim y As Integer = s.Height * CurrentLine
            e.Graphics.DrawString(line, PrintFont, Brushes.Black, 0, y)
            'Dim _temp As String() = PrintLines.ToArray()
            PrintLines = RemoveFirstElement(PrintLines)
            CurrentLine += 1
        End While

        If PrintLines.Count > 0 Then
            e.HasMorePages = True
            CurrentLine = 0
        Else
            e.HasMorePages = False
        End If

    End Sub

    Private Function RemoveFirstElement(ByVal Source As String()) As String()
        Dim Temp As String() = {}
        For i As Integer = 1 To Source.Count - 1
            Array.Resize(Temp, Temp.Count + 1)
            Temp(Temp.Count - 1) = Source(i)
        Next
        Return Temp
    End Function

    Public Sub Print()
        PrintLines = Content.Split(vbLf)
        TotalLine = PrintLines.Count
        CurrentLine = 0
        Document.Print()
    End Sub


End Class
