
Imports System.Configuration.ConfigurationManager

Public Class Core_BL

    Public CoinIn_Port As String = AppSettings("CoinIn_Port").ToString
    Public CashIn_Port As String = AppSettings("CashIn_Port").ToString

End Class
