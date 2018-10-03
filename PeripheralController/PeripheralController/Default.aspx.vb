Public Class _Default
    Inherits System.Web.UI.Page

    Dim BL As New Core_BL
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim URL As String = (BL.ServerRoot & "/Default.aspx?KO_ID=" & BL.KO_ID).Replace("//", "/")
        Response.Redirect(URL)
    End Sub

End Class