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

    Private Sub Document_PrintPage(sender As Object, e As PrintPageEventArgs) Handles Document.PrintPage

        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        e.PageSettings.Landscape = False

        'Dim printHeight As Integer
        'Dim printWidth As Integer
        'Dim leftMargin As Integer
        'Dim rightMargin As Integer

        ''Set print area size and margins
        'With Document.DefaultPageSettings
        '    printHeight = .PaperSize.Height - .Margins.Top - .Margins.Bottom
        '    printWidth = .PaperSize.Width - .Margins.Left - .Margins.Right
        '    leftMargin = .Margins.Left 'X
        '    rightMargin = .Margins.Top   'Y
        'End With

        ''Now we need to determine the total number of lines
        ''we're going to be printing
        'Dim numLines As Integer = CInt(printHeight / PrintFont.Height)

        ''Create a rectangle printing are for our document
        'Dim printArea As New RectangleF(leftMargin, rightMargin, printWidth, printHeight)

        'Use the StringFormat class for the text layout of our document
        Dim format As New StringFormat(StringFormatFlags.NoClip)

        ''Fit as many characters as we can into the print area    
        'e.Graphics.MeasureString(TextBox1.Text, PrintFont, New SizeF(printWidth, printHeight), format)

        'Print the page
        'e.Graphics.DrawString(TextBox1.Text, PrintFont, Brushes.Black, printArea, format)
        e.Graphics.DrawString(Content, PrintFont, Brushes.Black, 0, 0, format) ', format)

        e.HasMorePages = False

    End Sub

    Public Sub Print()
        Document.Print()
    End Sub


End Class
