Imports Newtonsoft.Json
Imports System.Configuration.ConfigurationManager
Imports System.Net
Imports System.IO

Public Class TrueMoney

    Public ReadOnly Property URL As String
        Get
            Return AppSettings("TrueMoneyURL").ToString
        End Get
    End Property

    Public Function CreateRequest(ByVal URL As String) As WebRequest

        Dim C As New Converter
        Dim webReq As WebRequest = WebRequest.Create(URL)
        '--------------- Config Web Request ------------
        webReq.Method = "POST"
        webReq.Timeout = 10000
        webReq.Headers.Add("X-Correlation-Id", (New Guid()).ToString)
        'webReq.Headers.Add("X-API-Key", xxxxxxxxxxxxxxxxxxxxxxx)
        webReq.Headers.Add("X-API-Version", "1.0")
        'webReq.Headers.Add("Content-Signature", xxxxxxxxxxxxxxxxxxxxxxx)
        webReq.Headers.Add("TIMESTAMP", C.DateToEpoch(Now).ToString)
        webReq.Headers.Add("Content-type", "applicaton/json")
        Return webReq
    End Function

End Class
