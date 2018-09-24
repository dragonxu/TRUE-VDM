Imports System.Data
Imports System.Data.SqlClient

Public Class Device_Brand
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL

    Private ReadOnly Property KO_ID As Integer
        Get
            Return Session("KO_ID")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            BindList()
        Else
            initFormPlugin()
        End If

    End Sub
    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Private Sub BindList()

        Dim DT As DataTable = BL.GetList_Current_Brand_Kiosk(KO_ID)
        rptList.DataSource = DT
        rptList.DataBind()

    End Sub

    Private Sub rptList_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptList.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim img As Image = e.Item.FindControl("img")
        Dim lblBrand As Label = e.Item.FindControl("lblBrand")
        Dim btnBrand As HtmlAnchor = e.Item.FindControl("btnBrand")
        Dim btnSelect As Button = e.Item.FindControl("btnSelect")

        img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=Brand&UID=" & e.Item.DataItem("BRAND_ID") & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        lblBrand.Text = e.Item.DataItem("BRAND_NAME").ToString()
        btnBrand.Attributes("onclick") = "$('#" & btnSelect.ClientID & "').click();"
        btnSelect.CommandArgument = e.Item.DataItem("BRAND_ID")

    End Sub

    Private Sub rptList_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptList.ItemCommand
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub
        Dim btnSelect As Button = e.Item.FindControl("btnSelect")
        Select Case e.CommandName
            Case "Select"
                Response.Redirect("Product_List.aspx?BRAND_ID=" & btnSelect.CommandArgument)

        End Select
    End Sub
End Class