Public Class UC_CashReciever
    Inherits System.Web.UI.UserControl

    Dim BL As New Core_BL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindComport()
        End If
    End Sub

    Private Sub BindComport()
        Dim ports() As String = GetListComPort()

        ddlCash.Items.Clear()
        ddlCoin.Items.Clear()

        For i As Integer = 0 To ports.Count - 1
            ddlCash.Items.Add(ports(i))
            If ddlCash.Items(i).Text = BL.CashReciever_Port Then
                ddlCash.SelectedIndex = i
            End If

            ddlCoin.Items.Add(ports(i))
            If ddlCoin.Items(i).Text = BL.CoinReciever_Port Then
                ddlCoin.SelectedIndex = i
            End If
        Next
    End Sub

End Class