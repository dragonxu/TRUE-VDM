Public Class Device_Shoping_Cart
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

    Protected Property BRAND_ID As Integer
        Get
            Return Val(Request.QueryString("BRAND_ID"))
        End Get
        Set(value As Integer)
            Request.QueryString("BRAND_ID") = value
        End Set
    End Property

    Protected Property MODEL As String
        Get
            Return Request.QueryString("PRODUCT_ID")
        End Get
        Set(value As String)
            Request.QueryString("PRODUCT_ID") = value
        End Set
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnConfirm_str_Click(sender As Object, e As EventArgs) Handles btnConfirm_str.Click
        Response.Redirect("Device_Verify.aspx?PRODUCT_ID=" & PRODUCT_ID)
    End Sub


    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Home.aspx")
    End Sub

    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click
        'Response.Redirect("Device_Product_Detail.aspx?PRODUCT_ID=" & PRODUCT_ID & "&MODEL=" & MODEL & "&BRAND_ID=" & BRAND_ID)
        Response.Redirect("Device_Product_Detail.aspx?PRODUCT_ID=" & PRODUCT_ID)
    End Sub
End Class