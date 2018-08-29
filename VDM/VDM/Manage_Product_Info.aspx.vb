
Imports System.Data
Imports System.Data.SqlClient

Public Class Manage_Product_Info
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL
    Dim BackEndInterface As New BackEndInterface.Get_Product_Info

    Protected Property PRODUCT_ID As Integer
        Get
            Return Val(txtCode.Attributes("PRODUCT_ID"))
        End Get
        Set(value As Integer)
            txtCode.Attributes("PRODUCT_ID") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lnkUpload.Attributes("onClick") = "document.getElementById('" & FileUpload1.ClientID & "').click();"

    End Sub



End Class