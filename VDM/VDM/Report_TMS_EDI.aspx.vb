Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Public Class Report_TMS_EDI
    Inherits System.Web.UI.Page
    Dim BL As New VDM_BL
    Dim CV As New Converter
    Protected Property USER_ID As Integer
        Get
            Return Val(lblUser_ID.Attributes("USER_ID"))
        End Get
        Set(value As Integer)
            lblUser_ID.Attributes("USER_ID") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        BindData()
    End Sub

    Private Sub BindData()

        Dim Sql As String = "SELECT * FROM VW_RPT_TMS_EDI "
        Dim DA As New SqlDataAdapter(Sql, BL.LogConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        rptData.DataSource = DT
        rptData.DataBind()

    End Sub


    Private Sub rptData_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptData.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim tdMode As HtmlTableCell = e.Item.FindControl("tdMode")
        Dim lblTime As Label = e.Item.FindControl("lblTime")
        Dim lblService As Label = e.Item.FindControl("lblService")
        Dim lblShop_Name As Label = e.Item.FindControl("lblShop_Name")
        Dim lblCash As Label = e.Item.FindControl("lblCash")
        Dim lblTMN_Wallet As Label = e.Item.FindControl("lblTMN_Wallet")
        Dim lblCredit_Card As Label = e.Item.FindControl("lblCredit_Card")
        Dim lblQtyTotal As Label = e.Item.FindControl("lblQtyTotal")
        Dim lblValue As Label = e.Item.FindControl("lblValue")


        'tdMode.InnerText = ""
        'lblTime.Text = ""

        'lblService
        'lblShop_Name
        'lblCash
        'lblTMN_Wallet
        'lblCredit_Card
        'lblQtyTotal
        'lblValue


    End Sub
    Private Sub rptData_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptData.ItemCommand

    End Sub

End Class