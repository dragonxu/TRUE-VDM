Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Public Class Report_Product_Stock
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
        'If Not IsNumeric(Session("USER_ID")) Then
        '    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('กรุณาเข้าสู่ระบบ'); window.location.href='SignIn.aspx';", True)
        '    Exit Sub
        'End If
        If Not IsPostBack Then
            ClearForm()
            BindData()
        Else
            initFormPlugin()
        End If

    End Sub
    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub
    Private Sub ClearForm()
        ddlService.SelectedIndex = 0
        BL.Bind_DDlShop_Code(ddlShop_Name)
    End Sub

    Private Sub BindData()
        lblHeader.Text = "รายงานสินค้าคงเครื่อง "
        Dim Sql As String = ""

        Sql &= " Select * " & vbLf
        Sql &= " FROM VW_RPT_STOCK " & vbLf
        Sql &= " WHERE 1=1 " & vbLf
        '---SEARCH--
        If ddlService.SelectedIndex > 0 Then
            Sql &= " AND ITEM_TYPE='" & ddlService.SelectedItem.Text & "' " & vbLf
            lblHeader.Text &= " ประเภท " & ddlService.SelectedItem.Text
        Else
            lblHeader.Text &= " ทั้งหมดแยกตาม Product และ SIM "
        End If

        If ddlShop_Name.SelectedIndex > 0 Then
            Sql &= " And SITE_CODE ='" & ddlShop_Name.SelectedValue & "' " & vbLf
            lblHeader.Text &= " สาขา " & ddlShop_Name.SelectedItem.Text
        Else
            lblHeader.Text &= " ทุกสาขา"
        End If
        If ddlKiosk_Code.SelectedIndex > 0 Then
            Sql &= " AND KO_CODE='" & ddlKiosk_Code.SelectedItem.ToString & "' " & vbLf
            lblHeader.Text &= " Kiosk :" & ddlKiosk_Code.SelectedItem.ToString
        Else
            lblHeader.Text &= ""
        End If

        'PRODUCT
        If txtSearchProduct_SIM.Text <> "" Then
            Sql &= " AND ( PRODUCT_NAME LIKE '%" & txtSearchProduct_SIM.Text & "%'  " & vbLf
            Sql &= " OR PRODUCT_CODE LIKE '%" & txtSearchProduct_SIM.Text & "%'  )" & vbLf

            lblHeader.Text &= " " & txtSearchProduct_SIM.Text
        End If
        If txtSearchSerial_No.Text <> "" Then
            Sql &= " AND SERIAL_NO LIKE '%" & txtSearchSerial_No.Text & "%' " & vbLf
            lblHeader.Text &= " Serial No :" & txtSearchSerial_No.Text
        End If

        Sql &= " ORDER BY SITE_CODE,KO_ID,ITEM_TYPE,PRODUCT_ID,ORDER_NO " & vbLf
        Dim DA As New SqlDataAdapter(Sql, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            lblHeader.Text &= " พบ " & FormatNumber(DT.Rows.Count, 0) & " รายการ"
        Else
            lblHeader.Text &= " ไม่พบรายการ"
        End If


        Session("Report_Product_Output") = DT
        Pager.SesssionSourceName = "Report_Product_Output"
        Pager.RenderLayout()

    End Sub
    Protected Sub Pager_PageChanging(ByVal Sender As PageNavigation) Handles Pager.PageChanging
        Pager.TheRepeater = rptData
    End Sub


    Private Sub rptData_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptData.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim lblSHOP_CODE As Label = e.Item.FindControl("lblSHOP_CODE")
        Dim lblKIOSK_CODE As Label = e.Item.FindControl("lblKIOSK_CODE")
        Dim lblSERVICE As Label = e.Item.FindControl("lblSERVICE")
        Dim lblPRODUCT As Label = e.Item.FindControl("lblPRODUCT")
        Dim lblSERIAL_NO As Label = e.Item.FindControl("lblSERIAL_NO")
        Dim lblORDER As Label = e.Item.FindControl("lblORDER")

        lblSHOP_CODE.Text = e.Item.DataItem("SITE_CODE").ToString
        lblKIOSK_CODE.Text = e.Item.DataItem("KO_CODE").ToString
        lblSERVICE.Text = e.Item.DataItem("ITEM_TYPE").ToString
        lblPRODUCT.Text = e.Item.DataItem("PRODUCT_CODE").ToString & ": " & e.Item.DataItem("PRODUCT_NAME").ToString
        lblSERIAL_NO.Text = e.Item.DataItem("SERIAL_NO").ToString
        lblORDER.Text = e.Item.DataItem("ORDER_NO").ToString

    End Sub

    Private Sub ddlShop_Name_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlShop_Name.SelectedIndexChanged
        If ddlShop_Name.SelectedIndex > 0 Then
            BL.Bind_DDlKiosk_Code(ddlKiosk_Code, ddlShop_Name.SelectedValue)
        Else
            BL.Bind_DDlKiosk_Code(ddlKiosk_Code)
        End If
        BindData()
    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        BindData()
    End Sub


End Class