﻿
Public Class Test_TMN_Payment
    Inherits System.Web.UI.Page

    Dim TMN As New TrueMoney

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ImplementJavaIntegerText(Amount, False,, "left")
            Amount.Text = 10
            Invoice_NO.Text = TMN.Generate_ISV(shopCode.Text)
            CustomerQRCode.Focus()
        End If
        lblError.Text = ""
    End Sub

    Private Sub btnGen_Click(sender As Object, e As EventArgs) Handles btnGen.Click
        Invoice_NO.Text = TMN.Generate_ISV(shopCode.Text)
    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click

        Dim Resp As TrueMoney.Response = TMN.GetResult(0, Invoice_NO.Text, Amount.Text, CustomerQRCode.Text, PaymentDescription.Text, shopCode.Text)
        RequestString.Text = Resp.Request.PostString
        X_API_Key.Text = Resp.Request.X_API_Key
        X_API_Version.Text = Resp.Request.X_API_Version
        Content_Signature.Text = Resp.Request.Content_Signature
        TIMESTAMP.Text = Resp.Request.TIMESTAMP
        Content_type.Text = Resp.Request.Content_type

        ResponseString.Text = Resp.JSONString
        '-------------- Connection Message ------------
        If Resp.ConnectionMessage = "Success" Then
            lblError.ForeColor = Drawing.Color.Green
        Else
            lblError.ForeColor = Drawing.Color.Red
        End If
        lblError.Text = Resp.ConnectionMessage

    End Sub


End Class