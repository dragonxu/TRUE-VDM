Public Class UC_Machine_Console
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnLeft_Click(sender As Object, e As ImageClickEventArgs) Handles btnLeft.Click, btnRight.Click
        Response.Redirect("../Machine_Console/Login.aspx")
    End Sub
End Class