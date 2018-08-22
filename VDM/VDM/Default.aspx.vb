Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim FR As New BackEndInterface.Face_Recognition
        Dim Resp As BackEndInterface.Face_Recognition.Response = FR.Get_Result("1234", 3473824, 24837824, 23423422, 1)

    End Sub

End Class