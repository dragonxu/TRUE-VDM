
Imports System.Configuration.ConfigurationManager

Public Class Core_BL

    Public KO_ID As String = AppSettings("KO_ID").ToString
    Public ServerRoot As String = AppSettings("ServerRoot").ToString

    Public Dispense1_Port As String = AppSettings("Dispense1_Port").ToString
    Public Dispense2_Port As String = AppSettings("Dispense2_Port").ToString
    Public Dispense5_Port As String = AppSettings("Dispense5_Port").ToString
    Public Dispense10_Port As String = AppSettings("Dispense10_Port").ToString
    Public Dispense20_Port As String = AppSettings("Dispense20_Port").ToString
    Public Dispense50_Port As String = AppSettings("Dispense50_Port").ToString
    Public Dispense100_Port As String = AppSettings("Dispense100_Port").ToString
    Public Dispense500_Port As String = AppSettings("Dispense500_Port").ToString
    Public Dispense1000_Port As String = AppSettings("Dispense1000_Port").ToString
    Public CashReciever_Port As String = AppSettings("CashReciever_Port").ToString
    Public CoinReciever_Port As String = AppSettings("CoinReciever_Port").ToString
    Public Printer_Name As String = AppSettings("Printer_Name").ToString

End Class
