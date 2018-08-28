Public Class MS_Product
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lnkUpload.Attributes("onClick") = "document.getElementById('" & FileUpload1.ClientID & "').click();"

    End Sub

End Class