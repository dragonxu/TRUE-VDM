Public Class Home
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsNumeric(Session("LANGUAGE")) Then
            Response.Redirect("Select_Language.aspx")
        End If

    End Sub

    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click
        Response.Redirect("Select_Language.aspx")
    End Sub

    Private Sub lnkDevice_Click(sender As Object, e As EventArgs) Handles lnkDevice.Click
        Response.Redirect("Device_Brand.aspx")
    End Sub

    Private Sub lnkSim_Click(sender As Object, e As EventArgs) Handles lnkSim.Click
        Response.Redirect("SIM_List.aspx")
    End Sub


End Class