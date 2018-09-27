Public Class Thank_You
    Inherits System.Web.UI.Page


    Private ReadOnly Property LANGUAGE As Integer
        Get
            Return Session("LANGUAGE")
        End Get
    End Property

    Private ReadOnly Property KO_ID As Integer
        Get
            Return Session("KO_ID")
        End Get
    End Property

    Protected Property PRODUCT_ID As Integer
        Get
            Return Val(Request.QueryString("PRODUCT_ID"))
        End Get
        Set(value As Integer)
            Request.QueryString("PRODUCT_ID") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Select_Language.aspx")
    End Sub



End Class