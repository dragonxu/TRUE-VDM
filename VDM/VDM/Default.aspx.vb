Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim PD As New BackEndInterface.Generate_Order_Id
        Dim Resp As BackEndInterface.Generate_Order_Id.Response = PD.Get_Result("80000011")

    End Sub

End Class