
Imports System.Configuration.ConfigurationManager

Public Class Core_BL

    Public CoinDispense1_Port As String = AppSettings("CoinDispense1_Port").ToString
    Public CoinDispense2_Port As String = AppSettings("CoinDispense2_Port").ToString
    Public CoinDispense5_Port As String = AppSettings("CoinDispense5_Port").ToString
    Public CoinDispense10_Port As String = AppSettings("CoinDispense10_Port").ToString
    Public CashDispense20_Port As String = AppSettings("CashDispense20_Port").ToString
    Public CashDispense50_Port As String = AppSettings("CashDispense50_Port").ToString
    Public CashDispense100_Port As String = AppSettings("CashDispense100_Port").ToString
    Public CashDispense500_Port As String = AppSettings("CashDispense500_Port").ToString
    Public CashDispense1000_Port As String = AppSettings("CashDispense1000_Port").ToString
    Public CashReciever_Port As String = AppSettings("CashReciever_Port").ToString
    Public CoinReciever_Port As String = AppSettings("CoinReciever_Port").ToString
    Public Printer_Name As String = AppSettings("Printer_Name").ToString

End Class
