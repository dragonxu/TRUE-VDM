﻿Imports System.Data
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
            Return Val(lblCode.Attributes("BRAND_ID"))
        End Get
        Set(value As Integer)
            lblCode.Attributes("BRAND_ID") = value
        End Set
    End Property

    Protected Property CAT_ID As Integer
        Get
            Return Val(lblCode.Attributes("CAT_ID"))
        End Get
        Set(value As Integer)
            lblCode.Attributes("CAT_ID") = value
        End Set
    End Property


    Protected Property Row_Total As Integer
        Get
            Return Val(lblCode.Attributes("Row_Total"))
        End Get
        Set(value As Integer)
            lblCode.Attributes("Row_Total") = value
        End Set
    End Property
    Protected Property PageCount As Integer
        Get
            Return Val(lblCode.Attributes("PageCount"))
        End Get
        Set(value As Integer)
            lblCode.Attributes("PageCount") = value
        End Set
    End Property
    Protected Property Row_Remain As Integer
        Get
            Return Val(lblCode.Attributes("Row_Remain"))
        End Get
        Set(value As Integer)
            lblCode.Attributes("Row_Remain") = value
        End Set
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            BRAND_ID = Request.QueryString("BRAND_ID")
            BindList()
        Else
            initFormPlugin()
        End If
    End Sub
    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub


    Dim Show_Box As Integer = 9 'จำนวน กล่องที่ต้องการแสดง ต่อ 1 page
    Dim DT_Product As DataTable
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
        'Dim DT As DataTable = BL.GetList_Current_Brand_Kiosk(KO_ID)     '--brand ทั้งหมด
        Row_Total = Val(DT.Rows.Count)
        PageCount = Math.Ceiling(Val(Row_Total / Show_Box))
        Row_Remain = Row_Total Mod Show_Box

        DT_Product = DT

        Dim DT_Page As New DataTable
        DT_Page.Columns.Add("Index")

        For i As Integer = 1 To PageCount
            DT_Page.Rows.Add(i)

        Next

        rptPage.DataSource = DT_Page
        rptPage.DataBind()

    End Sub

    'Private Sub rptBox_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptBox.ItemDataBound
    '    If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

    '    Dim img As Image = e.Item.FindControl("img")
    '    Dim lblProduct As Label = e.Item.FindControl("lblProduct")
    '    Dim btnBrand As HtmlAnchor = e.Item.FindControl("btnBrand")
    '    Dim btnSelect As Button = e.Item.FindControl("btnSelect")
    '    Dim Product_Default As DataTable = BL.Get_Show_Default_Product(e.Item.DataItem("MODEL").ToString())
    '    Dim Product_ID_Default As Integer = 0
    '    If Product_Default.Rows.Count > 0 Then
    '        Product_ID_Default = Val(Product_Default.Rows(0).Item("PRODUCT_ID"))
    '    End If
    '    img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & Product_ID_Default & "&LANG=" & VDM_BL.UILanguage.TH & "&t=" & Now.ToOADate.ToString.Replace(".", "")

    '    'img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=Brand&UID=" & e.Item.DataItem("BRAND_ID") & "&t=" & Now.ToOADate.ToString.Replace(".", "")
    '    lblProduct.Text = e.Item.DataItem("MODEL").ToString()
    '    lblProduct.Attributes("PRODUCT_ID") = Product_ID_Default
    '    btnBrand.Attributes("onclick") = "$('#" & btnSelect.ClientID & "').click();"
    '    btnSelect.CommandArgument = Product_ID_Default


    '    img.Style("box-shadow") = "8px 0px 8px 0px rgba(0,0,0,0.30);"
    'End Sub

    'Private Sub rptBox_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptBox.ItemCommand
    '    If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub
    '    Dim btnSelect As Button = e.Item.FindControl("btnSelect")
    '    Select Case e.CommandName
    '        Case "Select"
    '            Dim SQL As String = ""
    '            SQL &= " SELECT * FROM VW_ALL_PRODUCT WHERE PRODUCT_ID=" & btnSelect.CommandArgument
    '            Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
    '            Dim DT As New DataTable
    '            DA.Fill(DT)

    '            If DT.Rows.Count > 0 Then
    '                Response.Redirect("Device_Product_Detail.aspx?PRODUCT_ID=" & btnSelect.CommandArgument & "&BRAND_ID=" & BRAND_ID & "&MODEL=" & DT.Rows(0).Item("MODEL").ToString)
    '            End If

    '    End Select
    'End Sub

    Private Sub rptPage_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptPage.ItemDataBound

        'PageIndex
        Dim img As Image = e.Item.FindControl("img")
        Dim rptList As Repeater = e.Item.FindControl("rptList")
        AddHandler rptList.ItemDataBound, AddressOf rptList_ItemDataBound

        Dim Index As Integer = Val(e.Item.DataItem("Index"))
        Dim Product As DataTable = DT_Product
        Dim DT As New DataTable
        DT.Columns.Add("PRODUCT_ID")
        DT.Columns.Add("MODEL")
        Dim DR As DataRow

        '--หา Product Default
        Dim Product_Default As DataTable
        Dim Product_ID_Default As Integer = 0

        If (e.Item.ItemIndex + 1) < PageCount And PageCount <> 1 And (e.Item.ItemIndex + 1) <> PageCount Then
            For i As Integer = (Index * Show_Box) - (Show_Box - 1) To Index * Show_Box
                DR = DT.NewRow

                Product_Default = BL.Get_Show_Default_Product(Product.Rows(i - 1).Item("MODEL").ToString)
                If Product_Default.Rows.Count > 0 Then
                    Product_ID_Default = Val(Product_Default.Rows(0).Item("PRODUCT_ID"))
                End If

                DR("PRODUCT_ID") = Product_ID_Default
                DR("MODEL") = Product.Rows(i - 1).Item("MODEL").ToString
                DT.Rows.Add(DR)
            Next
        Else
            For i As Integer = (Index * Show_Box) - (Show_Box - 1) To Row_Total
                DR = DT.NewRow

                Product_Default = BL.Get_Show_Default_Product(Product.Rows(i - 1).Item("MODEL").ToString)
                If Product_Default.Rows.Count > 0 Then
                    Product_ID_Default = Val(Product_Default.Rows(0).Item("PRODUCT_ID"))
                End If

                DR("PRODUCT_ID") = Product_ID_Default
                DR("MODEL") = Product.Rows(i - 1).Item("MODEL").ToString
                DT.Rows.Add(DR)
            Next
        End If

        'ภาพแบรนด์
        img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=Brand&UID=" & BRAND_ID & "&t=" & Now.ToOADate.ToString.Replace(".", "")

        rptList.DataSource = DT
        rptList.DataBind()

    End Sub

    Protected Sub rptList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim img As Image = e.Item.FindControl("img")
        Dim lblProduct As Label = e.Item.FindControl("lblProduct")
        Dim btnProduct As HtmlAnchor = e.Item.FindControl("btnProduct")
        Dim btnSelect As Button = e.Item.FindControl("btnSelect")
        img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & e.Item.DataItem("PRODUCT_ID") & "&LANG=" & VDM_BL.UILanguage.TH & "&t=" & Now.ToOADate.ToString.Replace(".", "")

        lblProduct.Text = e.Item.DataItem("MODEL").ToString()
        btnProduct.Attributes("onclick") = "$('#" & btnSelect.ClientID & "').click();"
        btnSelect.CommandArgument = e.Item.DataItem("PRODUCT_ID")

        'If (e.Item.ItemIndex + 1) Mod 3 = 0 Or CountRow = e.Item.ItemIndex + 1 Then
        '    btnBrand.Attributes("class") = "Last"
        'End If
    End Sub

    Protected Sub rptList_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs)
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub
        Dim btnSelect As Button = e.Item.FindControl("btnSelect")
        Dim lblProduct As Label = e.Item.FindControl("lblProduct")
        Select Case e.CommandName
            Case "Select"
                Response.Redirect("Device_Product_Detail.aspx?PRODUCT_ID=" & btnSelect.CommandArgument & "&BRAND_ID=" & BRAND_ID & "&MODEL=" & lblProduct.Text)

        End Select
    End Sub




    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Home.aspx")
    End Sub

    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click
        Response.Redirect("Device_Brand.aspx")
    End Sub
End Class