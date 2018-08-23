Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim PD As New BackEndInterface.Get_Product_Info
        Dim Resp As BackEndInterface.Get_Product_Info.Response = PD.Get_Result("")

    End Sub

End Class