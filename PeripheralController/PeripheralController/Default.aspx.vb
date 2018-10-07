Public Class _Default
    Inherits System.Web.UI.Page

    Dim BL As New Core_BL
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim URL As String = BL.ServerRoot
        If URL.Substring(URL.Length - 1, 1) = "/" Then
            URL = URL.Substring(0, URL.Length - 1)
        End If
        URL = URL & "/Default.aspx?KO_ID=" & BL.KO_ID
        Response.Redirect(URL)
    End Sub

End Class