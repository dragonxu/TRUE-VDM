Public Class Machine_Overview
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub lnkShift_Click(sender As Object, e As EventArgs) Handles lnkShift.Click
        Response.Redirect("Manage_OpenClose_Shift.aspx")
    End Sub
End Class