Imports System.Data
Imports System.Data.SqlClient

Public Class Device_Category
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

        Dim DT As DataTable = BL.GetList_Current_Category_Kiosk(KO_ID)
        rptList.DataSource = DT
        rptList.DataBind()

    End Sub

    Private Sub rptList_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptList.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim img As Image = e.Item.FindControl("img")
        Dim lblCategory As Label = e.Item.FindControl("lblCategory")
        Dim btnCategory As HtmlAnchor = e.Item.FindControl("btnCategory")
        Dim btnSelect As Button = e.Item.FindControl("btnSelect")

        img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=Brand&UID=" & e.Item.DataItem("CAT_ID") & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        lblCategory.Text = e.Item.DataItem("CAT_NAME").ToString()
        btnCategory.Attributes("onclick") = "$('#" & btnSelect.ClientID & "').click();"


    End Sub

    Private Sub rptList_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptList.ItemCommand
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Select Case e.CommandName
            Case "Select"


        End Select
    End Sub
End Class