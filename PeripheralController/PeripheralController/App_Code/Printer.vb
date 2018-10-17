Imports System
Imports System.Drawing.Printing
Imports System.Runtime.InteropServices

Public Class Printer

    '#Region " Property Variables "
    '    ''' <summary>
    '    ''' Property variable for the Font the user wishes to use
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private _font As Font

    '    ''' <summary>
    '    ''' Property variable for the text to be printed
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private _text As String
    '#End Region

    '#Region " Class Properties "
    '    ''' <summary>
    '    ''' Property to hold the text that is to be printed
    '    ''' </summary>
    '    ''' <value></value>
    '    ''' <returns>A string</returns>
    '    ''' <remarks></remarks>
    '    Public Property TextToPrint() As String
    '        Get
    '            Return _text
    '        End Get
    '        Set(ByVal Value As String)
    '            _text = Value
    '        End Set
    '    End Property

    '    ''' <summary>
    '    ''' Property to hold the font the users wishes to use
    '    ''' </summary>
    '    ''' <value></value>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Property PrinterFont() As Font
    '        ' Allows the user to override the default font
    '        Get
    '            Return _font
    '        End Get
    '        Set(ByVal Value As Font)
    '            _font = Value
    '        End Set
    '    End Property
    '#End Region

    '#Region " Class Constructors "
    '    ''' <summary>
    '    ''' Empty constructor
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Public Sub New()
    '        'Set the file stream
    '        MyBase.New()
    '        'Instantiate out Text property to an empty string
    '        _text = String.Empty
    '    End Sub

    '    ''' <summary>
    '    ''' Constructor to initialize our printing object
    '    ''' and the text it's supposed to be printing
    '    ''' </summary>
    '    ''' <param name="str">Text that will be printed</param>
    '    ''' <remarks></remarks>
    '    Public Sub New(ByVal str As String)
    '        'Set the file stream
    '        MyBase.New()
    '        'Set our Text property value
    '        _text = str
    '    End Sub
    '#End Region

    '#Region " OnBeginPrint "
    '    ''' <summary>
    '    ''' Override the default OnBeginPrint method of the PrintDocument Object
    '    ''' </summary>
    '    ''' <param name="e"></param>
    '    ''' <remarks></remarks>
    '    Protected Overrides Sub OnBeginPrint(ByVal e As Printing.PrintEventArgs)
    '        ' Run base code
    '        MyBase.OnBeginPrint(e)

    '        'Check to see if the user provided a font
    '        'if they didnt then we default to Times New Roman
    '        If _font Is Nothing Then
    '            'Create the font we need
    '            _font = New Font("Times New Roman", 10)
    '        End If

    '        '-------------- Set Page Setting Here (T-Custom)------------------
    '    End Sub
    '#End Region

    '#Region " OnPrintPage "
    '    ''' <summary>
    '    ''' Override the default OnPrintPage method of the PrintDocument
    '    ''' </summary>
    '    ''' <param name="e"></param>
    '    ''' <remarks>This provides the print logic for our document</remarks>
    '    Protected Overrides Sub OnPrintPage(ByVal e As Printing.PrintPageEventArgs)
    '        ' Run base code
    '        MyBase.OnPrintPage(e)

    '        'Declare local variables needed
    '        Static curChar As Integer
    '        Dim printHeight As Integer
    '        Dim printWidth As Integer
    '        Dim leftMargin As Integer
    '        Dim rightMargin As Integer
    '        Dim lines As Int32
    '        Dim chars As Int32

    '        'Set print area size and margins
    '        With MyBase.DefaultPageSettings
    '            printHeight = .PaperSize.Height - .Margins.Top - .Margins.Bottom
    '            printWidth = .PaperSize.Width - .Margins.Left - .Margins.Right
    '            leftMargin = .Margins.Left 'X
    '            rightMargin = .Margins.Top   'Y
    '        End With

    '        'Check if the user selected to print in Landscape mode
    '        'if they did then we need to swap height/width parameters
    '        If MyBase.DefaultPageSettings.Landscape Then
    '            Dim tmp As Integer
    '            tmp = printHeight
    '            printHeight = printWidth
    '            printWidth = tmp
    '        End If

    '        'Now we need to determine the total number of lines
    '        'we're going to be printing
    '        Dim numLines As Int32 = CInt(printHeight / PrinterFont.Height)

    '        'Create a rectangle printing are for our document
    '        Dim printArea As New RectangleF(leftMargin, rightMargin, printWidth, printHeight)

    '        'Use the StringFormat class for the text layout of our document
    '        Dim format As New StringFormat(StringFormatFlags.LineLimit)

    '        'Fit as many characters as we can into the print area      
    '        Try
    '            e.Graphics.MeasureString(_text.Substring(RemoveZeros(curChar)), PrinterFont, New SizeF(printWidth, printHeight), format, chars, lines)
    '        Catch ex As Exception
    '        End Try

    '        'Print the page
    '        Try
    '            e.Graphics.DrawString(_text.Substring(RemoveZeros(curChar)), PrinterFont, Brushes.Black, printArea, format)
    '        Catch ex As Exception
    '        End Try


    '        'Increase current char count
    '        curChar += chars

    '        'Detemine if there is more text to print, if
    '        'there is the tell the printer there is more coming
    '        If curChar < _text.Length Then
    '            e.HasMorePages = True
    '        Else
    '            e.HasMorePages = False
    '            curChar = 0
    '        End If


    '    End Sub
    '#End Region

    '#Region " OnBeginEnd"
    '    'Protected Overrides Sub OnEndPrint(e As PrintEventArgs)
    '    '    MyBase.OnEndPrint(e)

    '    '    MyBase.
    '    'End Sub
    '#End Region


    '#Region " RemoveZeros "
    '    ''' <summary>
    '    ''' Function to replace any zeros in the size to a 1
    '    ''' Zero's will mess up the printing area
    '    ''' </summary>
    '    ''' <param name="value">Value to check</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function RemoveZeros(ByVal value As Integer) As Integer
    '        'Check the value passed into the function,
    '        'if the value is a 0 (zero) then return a 1,
    '        'otherwise return the value passed in
    '        Select Case value
    '            Case 0
    '                Return 1
    '            Case Else
    '                Return value
    '        End Select
    '    End Function
    '#End Region

    Public Class DOCINFO
        <MarshalAs(UnmanagedType.LPStr)>
        Public pDocName As String
        <MarshalAs(UnmanagedType.LPStr)>
        Public pOutputFile As String
        <MarshalAs(UnmanagedType.LPStr)>
        Public pDataType As String
    End Class

    '<DllImport("winspool.drv", EntryPoint:="OpenPrinterA", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    'Private Shared Function OpenPrinter(ByVal pPrinterName As String, ByRef phPrinter As IntPtr, ByVal pDefault As IntPtr) As Boolean
    'End Function


    '<DllImport("winspool.drv", EntryPoint:="ClosePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    'Private Shared Function ClosePrinter(ByVal hPrinter As Int32) As Boolean
    'End Function

    '<DllImport("winspool.drv", CharSet:=CharSet.Unicode, ExactSpelling:=False, CallingConvention:=CallingConvention.StdCall)>
    'Public Function StartDocPrinter(hPrinter As IntPtr, Level As Integer, ByRef pDocInfo As DOCINFOA) As Long
    'End Function

    '  [DllImport("winspool.Drv", EntryPoint="StartDocPrinterA", SetLastError=true, CharSet=CharSet.Ansi, ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
    '  Public Static extern bool StartDocPrinter( IntPtr hPrinter, Int32 level,  [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

    '  [DllImport("winspool.Drv", EntryPoint="EndDocPrinter", SetLastError=true, ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
    '  Public Static extern bool EndDocPrinter(IntPtr hPrinter);

    '  [DllImport("winspool.Drv", EntryPoint="StartPagePrinter", SetLastError=true, ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
    '  Public Static extern bool StartPagePrinter(IntPtr hPrinter);

    '  [DllImport("winspool.Drv", EntryPoint="EndPagePrinter", SetLastError=true, ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
    '  Public Static extern bool EndPagePrinter(IntPtr hPrinter);

    '  [DllImport("winspool.Drv", EntryPoint="WritePrinter", SetLastError=true, ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
    '  Public Static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten );

    <DllImport("winspool.drv", CharSet:=CharSet.Unicode, ExactSpelling:=False, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function OpenPrinter(pPrinterName As String, ByRef phPrinter As IntPtr, pDefault As Integer) As Long
    End Function

    <DllImport("winspool.drv", CharSet:=CharSet.Unicode, ExactSpelling:=False, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function StartDocPrinter(hPrinter As IntPtr, Level As Integer, ByRef pDocInfo As DOCINFO) As Long
    End Function

    <DllImport("winspool.drv", CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function StartPagePrinter(hPrinter As IntPtr) As Long
    End Function

    <DllImport("winspool.drv", CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WritePrinter(hPrinter As IntPtr, data As String, buf As Integer, ByRefpcWritten As Integer) As Long
    End Function

    <DllImport("winspool.drv", CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function EndPagePrinter(hPrinter As IntPtr) As Long
    End Function

    <DllImport("winspool.drv", CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function EndDocPrinter(hPrinter As IntPtr) As Long
    End Function

    <DllImport("winspool.drv", CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function ClosePrinter(hPrinter As IntPtr) As Long
    End Function


    Private HandlePrinter As IntPtr
    Private ps As PrinterSettings

    Public Sub New()
        HandlePrinter = IntPtr.Zero
        ps = New PrinterSettings()
    End Sub

    Public Property PrinterName As String
        Get
            Return ps.PrinterName
        End Get
        Set
            ps.PrinterName = Value
        End Set
    End Property

    Public Function Open(ByVal DocName As String) As Boolean

        'see If printer Is already open
        If HandlePrinter <> IntPtr.Zero Then
            Return False
        End If

        ' opens the printer
        Dim risp As Boolean = OpenPrinter(ps.PrinterName, HandlePrinter, IntPtr.Zero)
        If Not risp Then Return False

        ' starts a print job
        Dim MyDocInfo As New DOCINFO()
        MyDocInfo.pDocName = DocName
        MyDocInfo.pOutputFile = Nothing
        MyDocInfo.pDataType = "RAW"

        If (StartDocPrinter(HandlePrinter, 1, MyDocInfo)) Then
            StartPagePrinter(HandlePrinter)  '/starts a page       
            Return True
        Else
            Return False
        End If
    End Function

    Public Function Close() As Boolean
        If HandlePrinter = IntPtr.Zero Then Return False
        If Not EndPagePrinter(HandlePrinter) Then Return False
        If Not EndDocPrinter(HandlePrinter) Then Return False
        If Not ClosePrinter(HandlePrinter) Then Return False
        HandlePrinter = IntPtr.Zero
        Return True
    End Function

    Public Function Print(outputstring As String) As Boolean
        If HandlePrinter = IntPtr.Zero Then Return False
        Dim buf As IntPtr = Marshal.StringToCoTaskMemAnsi(outputstring)
        Dim done As Int32 = 0
        Dim ok As Boolean = WritePrinter(HandlePrinter, buf, outputstring.Length, done)
        Marshal.FreeCoTaskMem(buf)
        Return ok
    End Function

End Class
