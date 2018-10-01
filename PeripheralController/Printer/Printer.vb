Imports System.Management
Imports PrinterClassDll

Public Class Printer

    Public Enum PrinterStatus
        Unknow = -1
        Online = 1
        Offline = 0
    End Enum

    Public Function GetList() As String()
        Dim Result As String() = {}
        Try
            Dim scope As New ManagementScope("\root\cimv2")
            scope.Connect()
            Dim searcher As New ManagementObjectSearcher("SELECT * FROM Win32_Printer")
            Dim MyPrinter As String = ""
            For Each printer As ManagementObject In searcher.[Get]()
                PushArray_String(Result, printer("Name").ToString().ToLower())
            Next
        Catch ex As Exception
        End Try
        Return Result
    End Function

    Public Function GetStatus(ByVal PrinterName As String) As PrinterStatus
        Try
            Dim scope As New ManagementScope("\root\cimv2")
            scope.Connect()
            Dim searcher As New ManagementObjectSearcher("SELECT * FROM Win32_Printer")
            For Each printer As ManagementObject In searcher.[Get]()
                Dim _tmp As String = printer("Name").ToString().ToLower()
                If PrinterName.ToLower = _tmp Then
                    If printer("WorkOffline") Then
                        Return PrinterStatus.Offline
                    Else
                        Return PrinterStatus.Online
                    End If
                End If
            Next
        Catch ex As Exception : End Try
        Return PrinterStatus.Unknow
    End Function

    Public Function PrintImage(ByVal PrinterName As String, ByVal FileName As String) As Object
        Dim Result As Object = Nothing
        Try
            Dim prn As New PrinterClassDll.Win32Print
            prn.SetPrinterName(PrinterName)
            Result = prn.PrintImage(FileName)
        Catch ex As Exception
        End Try
        Return Result
    End Function

    Public Enum ContentType
        Text = 1
        Image = 2
    End Enum

    Public Class ContentLine
        Public Text As String
        Public ImagePath As String
        Public FontSize As Single
        Public FontName As String
        Public Bold As Boolean
        Public IsColor As Boolean
        Public ContentType As ContentType
    End Class

    Public Function PrintText(ByVal PrinterName As String, ByVal Lines As List(Of ContentLine)) As Boolean
        Try
            Dim prn As New PrinterClassDll.Win32Print
            prn.SetPrinterName(PrinterName)
            For i As Integer = 0 To Lines.Count - 1
                Dim Line As ContentLine = Lines(i)
                Try
                    Select Case Line.ContentType
                        Case ContentType.Text
                            prn.SetDeviceFont(Line.FontSize, Line.FontName, Line.Bold, Line.IsColor)
                            prn.PrintText(Line.Text)
                        Case ContentType.Image
                            prn.PrintImage(Line.ImagePath)
                    End Select
                Catch : End Try
            Next
            prn.EndDoc()
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function

    Public Sub PushArray_String(ByRef TheArray() As String, ByVal AppendedValue As String)
        Array.Resize(TheArray, TheArray.Length + 1)
        TheArray(TheArray.Length - 1) = AppendedValue
    End Sub

End Class
