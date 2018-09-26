Public Class Device_Shoping_Cart
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnConfirm_str_Click(sender As Object, e As EventArgs) Handles btnConfirm_str.Click
        Response.Redirect("Device_Verify.aspx")
    End Sub
End Class