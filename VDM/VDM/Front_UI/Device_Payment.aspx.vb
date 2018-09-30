﻿Public Class Device_Payment
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

        If Not IsPostBack Then
            pnlCash.Visible = False
            pnlCredit.Visible = False
            pnlTruemoney.Visible = False
        Else
            initFormPlugin()
        End If

    End Sub


    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Private Sub ClearForm()
        pnlCash.Visible = False
        pnlCredit.Visible = False
        pnlTruemoney.Visible = False
        'current
        lnkCash.Attributes("class") = ""
        lnkCredit.Attributes("class") = ""
        lnkTruemoney.Attributes("class") = ""
    End Sub

    Private Sub lnkCash_ServerClick(sender As Object, e As EventArgs) Handles lnkCash.ServerClick
        ClearForm()
        pnlCash.Visible = True
        lnkCash.Attributes("class") = "current"
    End Sub

    Private Sub lnkCredit_ServerClick(sender As Object, e As EventArgs) Handles lnkCredit.ServerClick
        ClearForm()
        pnlCredit.Visible = True
        lnkCredit.Attributes("class") = "current"
    End Sub

    Private Sub lnkTruemoney_ServerClick(sender As Object, e As EventArgs) Handles lnkTruemoney.ServerClick
        ClearForm()
        pnlTruemoney.Visible = True
        lnkTruemoney.Attributes("class") = "current"

    End Sub

    Private Sub btnSkip_Click(sender As Object, e As EventArgs) Handles btnSkip.Click
        Response.Redirect("Complete_Order.aspx?PRODUCT_ID=" & PRODUCT_ID)

    End Sub


    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Home.aspx")
    End Sub

    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click
        Response.Redirect("Device_Shoping_Cart.aspx?PRODUCT_ID=" & PRODUCT_ID)
    End Sub

End Class