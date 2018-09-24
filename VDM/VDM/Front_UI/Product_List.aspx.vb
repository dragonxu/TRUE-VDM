Imports System.Data
Imports System.Data.SqlClient

Public Class Product_List
    Inherits System.Web.UI.Page
    Dim BL As New VDM_BL

    Private ReadOnly Property KO_ID As Integer
        Get
            Return Session("KO_ID")
        End Get
    End Property

    Protected Property BRAND_ID As Integer
        Get
            Return Val(lblBrand_ID.Attributes("BRAND_ID"))
        End Get
        Set(value As Integer)
            lblBrand_ID.Attributes("BRAND_ID") = value
        End Set
    End Property

    Protected Property CAT_ID As Integer
        Get
            Return Val(lblBrand_ID.Attributes("CAT_ID"))
        End Get
        Set(value As Integer)
            lblBrand_ID.Attributes("CAT_ID") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            BRAND_ID = Request.QueryString("BRAND_ID")
            BindList()
            BindList_Box()
        Else
            initFormPlugin()
        End If
    End Sub
    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Private Sub BindList()

        Dim SQL As String = ""
        SQL &= " Select DISTINCT VW_ALL_PRODUCT.MODEL,VW_ALL_PRODUCT.BRAND_ID,VW_ALL_PRODUCT.BRAND_CODE,VW_ALL_PRODUCT.BRAND_NAME,VW_ALL_PRODUCT.CAT_ID " & vbLf
        SQL &= " From VW_ALL_PRODUCT " & vbLf
        SQL &= " INNER Join VW_CURRENT_PRODUCT_STOCK ON VW_CURRENT_PRODUCT_STOCK.PRODUCT_ID= VW_ALL_PRODUCT.PRODUCT_ID " & vbLf
        SQL &= " WHERE VW_CURRENT_PRODUCT_STOCK.KO_ID = " & KO_ID & "  AND  VW_ALL_PRODUCT.BRAND_ID=" & BRAND_ID
        If CAT_ID > 0 Then
            SQL &= " And VW_ALL_PRODUCT.CAT_ID = " & CAT_ID
        End If
        SQL &= " ORDER BY VW_ALL_PRODUCT.CAT_ID , VW_ALL_PRODUCT.MODEL "
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        'lblTotalList.Text = FormatNumber(DT.Rows.Count, 0)

        Session("Manage_Product_Info") = DT
        Pager.SesssionSourceName = "Manage_Product_Info"
        Pager.RenderLayout()

    End Sub

    Protected Sub Pager_PageChanging(ByVal Sender As PageNavigation) Handles Pager.PageChanging
        Pager.TheRepeater = rptList
    End Sub

    Private Sub rptList_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptList.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim img As Image = e.Item.FindControl("img")
        Dim imgBrand As Image = e.Item.FindControl("imgBrand")
        Dim lblProductCode As Label = e.Item.FindControl("lblProductCode")
        Dim lblModel As Label = e.Item.FindControl("lblModel")
        Dim lblDisplayName As Label = e.Item.FindControl("lblDisplayName")
        Dim lblCountSpec As Label = e.Item.FindControl("lblCountSpec")
        Dim lblPrice As Label = e.Item.FindControl("lblPrice")
        Dim btnEdit As Button = e.Item.FindControl("btnEdit")

        'img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & e.Item.DataItem("PRODUCT_ID") & "&LANG=" & VDM_BL.UILanguage.TH & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        'lblProductCode.Text = e.Item.DataItem("PRODUCT_CODE").ToString
        'lblDisplayName.Text = e.Item.DataItem("DISPLAY_NAME_TH").ToString
        'imgBrand.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=Brand&UID=" & e.Item.DataItem("BRAND_ID")
        lblModel.Text = e.Item.DataItem("MODEL").ToString
        'If Not IsDBNull(e.Item.DataItem("Price")) Then
        '    lblPrice.Text = FormatNumber(e.Item.DataItem("Price"), 2)
        'End If
        'ImageActive

        'btnEdit.CommandArgument = e.Item.DataItem("PRODUCT_ID")

    End Sub

    Private Sub rptList_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptList.ItemCommand
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Select Case e.CommandName
            Case "Edit"


            Case "Delete"
                Dim SQL As String = "DELETE FROM MS_Product_Spec" & vbNewLine
                SQL &= " WHERE PRODUCT_ID=" & e.CommandArgument
                BL.ExecuteNonQuery(SQL)

                SQL = "DELETE FROM MS_Product" & vbNewLine
                SQL &= " WHERE PRODUCT_ID=" & e.CommandArgument
                BL.ExecuteNonQuery(SQL)
                BindList()

        End Select



    End Sub





    Private Sub BindList_Box()

        Dim SQL As String = ""
        SQL &= " Select DISTINCT VW_ALL_PRODUCT.MODEL,VW_ALL_PRODUCT.BRAND_ID,VW_ALL_PRODUCT.BRAND_CODE,VW_ALL_PRODUCT.BRAND_NAME,VW_ALL_PRODUCT.CAT_ID " & vbLf
        SQL &= " From VW_ALL_PRODUCT " & vbLf
        SQL &= " INNER Join VW_CURRENT_PRODUCT_STOCK ON VW_CURRENT_PRODUCT_STOCK.PRODUCT_ID= VW_ALL_PRODUCT.PRODUCT_ID " & vbLf
        SQL &= " WHERE VW_CURRENT_PRODUCT_STOCK.KO_ID = " & KO_ID & "  AND  VW_ALL_PRODUCT.BRAND_ID=" & BRAND_ID
        If CAT_ID > 0 Then
            SQL &= " And VW_ALL_PRODUCT.CAT_ID = " & CAT_ID
        End If
        SQL &= " ORDER BY VW_ALL_PRODUCT.CAT_ID , VW_ALL_PRODUCT.MODEL "
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        rptBox.DataSource = DT
        rptBox.DataBind()

    End Sub

    Private Sub rptBox_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptBox.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim img As Image = e.Item.FindControl("img")
        Dim lblBrand As Label = e.Item.FindControl("lblBrand")
        Dim btnBrand As HtmlAnchor = e.Item.FindControl("btnBrand")
        Dim btnSelect As Button = e.Item.FindControl("btnSelect")
        Dim Product_Default As DataTable = BL.Get_Show_Default_Product(e.Item.DataItem("MODEL").ToString())
        Dim Product_ID_Default As Integer = 0
        If Product_Default.Rows.Count > 0 Then
            Product_ID_Default = Val(Product_Default.Rows(0).Item("PRODUCT_ID"))
        End If
        img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & Product_ID_Default & "&LANG=" & VDM_BL.UILanguage.TH & "&t=" & Now.ToOADate.ToString.Replace(".", "")

        'img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=Brand&UID=" & e.Item.DataItem("BRAND_ID") & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        lblBrand.Text = e.Item.DataItem("MODEL").ToString()
        btnBrand.Attributes("onclick") = "$('#" & btnSelect.ClientID & "').click();"
        btnSelect.CommandArgument = e.Item.DataItem("BRAND_ID")

    End Sub

    Private Sub rptBox_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptBox.ItemCommand
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub
        Dim btnSelect As Button = e.Item.FindControl("btnSelect")
        Select Case e.CommandName
            Case "Select"
                Response.Redirect("Product_List.aspx?BRAND_ID=" & btnSelect.CommandArgument)

        End Select
    End Sub


End Class