Public Class DisableCash
    Inherits System.Web.UI.Page

    Dim BL As New Core_BL

    Dim Cash As CashReceiver.CashReceiver = Nothing
    Dim Coin As CoinReciever.CoinReciever = Nothing

    Private ReadOnly Property callBackFunction As String
        Get
            Try
                Return Request.QueryString("callback")
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Cash = New CashReceiver.CashReceiver
        Coin = New CoinReciever.CoinReciever

        Cash.SetPort(BL.CashReciever_Port)
        Coin.SetPort(BL.CoinReciever_Port)

        Try
            Cash.Close()
        Catch : End Try

        Try
            Coin.Close()
        Catch : End Try

    End Sub

    Private Sub callBack()
        Try
            Cash.Close()
        Catch : End Try
        Try
            Coin.Close()
        Catch : End Try

        Dim Script As String = callBackFunction & "('success');"
        Response.Write(Script)
        Response.End()

    End Sub

End Class