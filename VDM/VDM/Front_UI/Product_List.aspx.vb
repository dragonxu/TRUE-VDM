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
            BindList_Box()
        Else
            initFormPlugin()
        End If
    End Sub
    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
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
        Dim lblProduct As Label = e.Item.FindControl("lblProduct")
        Dim btnBrand As HtmlAnchor = e.Item.FindControl("btnBrand")
        Dim btnSelect As Button = e.Item.FindControl("btnSelect")
        Dim Product_Default As DataTable = BL.Get_Show_Default_Product(e.Item.DataItem("MODEL").ToString())
        Dim Product_ID_Default As Integer = 0
        If Product_Default.Rows.Count > 0 Then
            Product_ID_Default = Val(Product_Default.Rows(0).Item("PRODUCT_ID"))
        End If
        img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & Product_ID_Default & "&LANG=" & VDM_BL.UILanguage.TH & "&t=" & Now.ToOADate.ToString.Replace(".", "")

        'img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=Brand&UID=" & e.Item.DataItem("BRAND_ID") & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        lblProduct.Text = e.Item.DataItem("MODEL").ToString()
        lblProduct.Attributes("PRODUCT_ID") = Product_ID_Default
        btnBrand.Attributes("onclick") = "$('#" & btnSelect.ClientID & "').click();"
        btnSelect.CommandArgument = Product_ID_Default

    End Sub

    Private Sub rptBox_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptBox.ItemCommand
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub
        Dim btnSelect As Button = e.Item.FindControl("btnSelect")
        Select Case e.CommandName
            Case "Select"
                Dim SQL As String = ""
                SQL &= " SELECT * FROM VW_ALL_PRODUCT WHERE PRODUCT_ID=" & btnSelect.CommandArgument
                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                Dim DT As New DataTable
                DA.Fill(DT)

                If DT.Rows.Count > 0 Then
                    Response.Redirect("Device_Product_Detail.aspx?PRODUCT_ID=" & btnSelect.CommandArgument & "&BRAND_ID=" & BRAND_ID & "&MODEL=" & DT.Rows(0).Item("MODEL").ToString)
                End If

        End Select
    End Sub


End Class