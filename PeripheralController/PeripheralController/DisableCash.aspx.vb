Public Class DisableCash
    Inherits System.Web.UI.Page

    Dim BL As New Core_BL

    Private ReadOnly Property CashReceiver As CashReceiver.CashReceiver
        Get
            If IsNothing(Application("CashReceiver")) Then
                '----------- Init Object And Connect---------
                Dim _cash As New CashReceiver.CashReceiver
                _cash.SetPort(BL.CashReciever_Port)

                Try
                    _cash.Close()
                Catch ex As Exception
                End Try

                Threading.Thread.Sleep(100)

                Application.Lock()
                Application("CashReceiver") = _cash
                Application.UnLock()

            End If
            Return Application("CashReceiver")
        End Get
    End Property

    Private ReadOnly Property CoinReceiver As CoinReciever.CoinReciever
        Get
            If IsNothing(Application("CoinReceiver")) Then
                '----------- Init Object And Connect---------
                Dim _coin As New CoinReciever.CoinReciever
                _coin.SetPort(BL.CoinReciever_Port)

                Try
                    _coin.Close()
                Catch ex As Exception
                End Try

                Threading.Thread.Sleep(100)

                Application.Lock()
                Application("CoinReceiver") = _coin
                Application.UnLock()

            End If
            Return Application("CoinReceiver")
        End Get
    End Property

    Private ReadOnly Property callBackFunction As String
        Get
            If Not IsNothing(Request.QueryString("callback")) Then
                Return Request.QueryString("callback")
            Else
                Return ""
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            CashReceiver.Close()
        Catch : End Try

        Try
            CoinReceiver.Close()
        Catch : End Try

    End Sub

    Private Sub callBack()
        Try
            CashReceiver.Close()
        Catch : End Try
        Try
            CoinReceiver.Close()
        Catch : End Try

        Dim Script As String = callBackFunction & "('success');"
        Response.Write(Script)
        Response.End()

    End Sub

End Class