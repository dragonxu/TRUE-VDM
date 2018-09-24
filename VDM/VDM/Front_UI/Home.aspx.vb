Public Class Home
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub lnkDevice_Click(sender As Object, e As EventArgs) Handles lnkDevice.Click

        Response.Redirect("Device_Brand.aspx")
    End Sub
End Class