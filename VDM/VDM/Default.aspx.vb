Imports VDM.BackEndInterface

Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim PD As New Get_Product_Info
        Dim Resp As Get_Product_Info.Response = PD.Get_Result("3000070264")

    End Sub

End Class