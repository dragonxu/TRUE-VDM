Imports System.Data
Imports System.Data.SqlClient

Public Class Device_Product_Detail
    Inherits System.Web.UI.Page
    Dim BL As New VDM_BL

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

    Protected Property BRAND_ID As Integer
        Get
            Return Val(lblCode.Attributes("BRAND_ID"))
        End Get
        Set(value As Integer)
            lblCode.Attributes("BRAND_ID") = value
        End Set
    End Property

    Protected Property PRODUCT_ID As Integer
        Get
            Return Val(lblCode.Attributes("PRODUCT_ID"))
        End Get
        Set(value As Integer)
            lblCode.Attributes("PRODUCT_ID") = value
        End Set
    End Property

    Protected Property MODEL As String
        Get
            Return lblCode.Attributes("MODEL").ToString
        End Get
        Set(value As String)
            lblCode.Attributes("MODEL") = value
        End Set
    End Property

    Protected Property CAPACITY As String
        Get
            Return lblCode.Attributes("CAPACITY").ToString
        End Get
        Set(value As String)
            lblCode.Attributes("CAPACITY") = value
        End Set
    End Property

    Protected Property COLOR As String
        Get
            Return lblCode.Attributes("COLOR").ToString
        End Get
        Set(value As String)
            lblCode.Attributes("COLOR") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BRAND_ID = Request.QueryString("BRAND_ID")
            PRODUCT_ID = Request.QueryString("PRODUCT_ID")
            MODEL = Request.QueryString("MODEL").ToString

            BindList()
        Else
            initFormPlugin()
        End If




    End Sub

    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Dim DT_Product_Model As New DataTable

#Region "rptProductList"

    Private Sub BindList()

        Dim DT As DataTable = BL.GetList_Product_Model(MODEL, KO_ID, PRODUCT_ID)

        If DT.Rows.Count > 0 Then
            DT_Product_Model = DT   ' Product ที่จัดกลุ่ม Model  เดียวกัน

            'แสดง Product Default ครั้งแรก
            img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & PRODUCT_ID & "&LANG=" & LANGUAGE & "&t=" & Now.ToOADate.ToString.Replace(".", "")
            lblDISPLAY_NAME.Text = DT.Rows(0).Item("DISPLAY_NAME_" & BL.Get_Language_Code(LANGUAGE)).ToString()

            'Warranty
            'pnlSPEC_Warranty.
            Dim DT_Warranty As DataTable = BL.GetList_Product_Spec_Warranty(PRODUCT_ID, KO_ID, LANGUAGE)
            If DT_Product_Model.Rows.Count > 0 Then
                lblSPEC_Warranty.Text = DT_Warranty.Rows(0).Item("SPEC_NAME").ToString()
                lblDESCRIPTION_Warranty.Text = DT_Warranty.Rows(0).Item("DESCRIPTION").ToString()
            End If

            'lblDescription_Header.Text = ""  ' จาก Master แปล
            lblDescription_Detail.Text = DT.Rows(0).Item("DESCRIPTION_" & BL.Get_Language_Code(LANGUAGE)).ToString()

            'lblPrice_str.Text = ""
            If Not IsDBNull(DT.Rows(0).Item("PRICE")) Then
                lblPrice_Money.Text = FormatNumber(Val(DT.Rows(0).Item("PRICE")), 2)
                'lblCurrency_Str.Text = ""
            End If

            '--Set Current Select
            Dim SQL_Active As String = ""
            SQL_Active &= "  Select DISTINCT PRODUCT_ID,PRODUCT_CODE,PRODUCT_NAME,KO_ID " & vbLf
            SQL_Active &= "        ,SPEC_ID,SEQ " & vbLf
            SQL_Active &= "        ,SPEC_NAME_" & BL.Get_Language_Code(LANGUAGE).ToString() & ",DESCRIPTION_" & BL.Get_Language_Code(LANGUAGE).ToString()
            SQL_Active &= "        ,CAT_ID,MODEL       " & vbLf
            SQL_Active &= "    From VDM.dbo.VW_CURRENT_PRODUCT_SPEC " & vbLf
            SQL_Active &= "    Where PRODUCT_ID =" & PRODUCT_ID & " And SPEC_ID In (" & VDM_BL.Spec.Capacity & "," & VDM_BL.Spec.Color & ") " & vbLf

            Dim DA As New SqlDataAdapter(SQL_Active, BL.ConnectionString)
            Dim DT_Active As New DataTable
            DA.Fill(DT_Active)
            If DT_Active.Rows.Count > 0 Then
                For i As Integer = 0 To DT_Active.Rows.Count - 1
                    If DT_Active.Rows(i).Item("SPEC_ID") = VDM_BL.Spec.Color Then
                        COLOR = DT_Active.Rows(i).Item("DESCRIPTION_" & BL.Get_Language_Code(LANGUAGE)).ToString()
                    End If
                    If DT_Active.Rows(i).Item("SPEC_ID") = VDM_BL.Spec.Capacity Then
                        CAPACITY = DT_Active.Rows(i).Item("DESCRIPTION_" & BL.Get_Language_Code(LANGUAGE)).ToString()
                    End If
                Next
            End If

            Dim DT_Capacity As DataTable = BL.GetList_Product_Spec_Capacity(MODEL, KO_ID, Val(DT.Rows(0).Item("CAT_ID").ToString()), LANGUAGE)
            rptCapacity.DataSource = DT_Capacity
            rptCapacity.DataBind()

            Dim DT_Spec As DataTable = BL.GetList_Product_Spec_Other(DT.Rows(0).Item("PRODUCT_ID"), KO_ID, LANGUAGE)
            rptSpec.DataSource = DT_Spec
            rptSpec.DataBind()

            Dim DT_Color As DataTable = BL.GetList_Product_Spec_Color(MODEL, KO_ID, LANGUAGE)
            rptColor.DataSource = DT_Color
            rptColor.DataBind()



        End If

    End Sub



    'Private Sub rptProductList_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptProductList.ItemDataBound
    '    If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

    '    Dim img As Image = e.Item.FindControl("img")

    '    Dim lblDISPLAY_NAME As Label = e.Item.FindControl("lblDISPLAY_NAME")

    '    'Warranty
    '    Dim pnlSPEC_Warranty As Panel = e.Item.FindControl("pnlSPEC_Warranty")          'ถ้าไม่มี Warranty pnlSPEC_Warranty=false
    '    Dim lblSPEC_Warranty As Label = e.Item.FindControl("lblSPEC_Warranty")
    '    Dim lblDESCRIPTION_Warranty As Label = e.Item.FindControl("lblDESCRIPTION_Warranty")

    '    'Description
    '    Dim lblDescription_Header As Label = e.Item.FindControl("lblDescription_Header")
    '    Dim lblDescription_Detail As Label = e.Item.FindControl("lblDescription_Detail")

    '    'Price
    '    Dim lblPrice_str As Label = e.Item.FindControl("lblPrice_str")
    '    Dim lblPrice_Money As Label = e.Item.FindControl("lblPrice_Money")
    '    Dim lblCurrency_Str As Label = e.Item.FindControl("lblCurrency_Str")

    '    Dim btnSelect_str As LinkButton = e.Item.FindControl("btnSelect_str")

    '    lblDISPLAY_NAME.Text = e.Item.DataItem("DISPLAY_NAME_" & BL.Get_Language_Code(LANGUAGE)).ToString()
    '    img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & e.Item.DataItem("PRODUCT_ID") & "&LANG=" & LANGUAGE & "&t=" & Now.ToOADate.ToString.Replace(".", "")

    '    'Warranty
    '    'pnlSPEC_Warranty.
    '    Dim DT_Warranty As DataTable = BL.GetList_Product_Spec_Warranty(PRODUCT_ID, KO_ID, LANGUAGE)
    '    If DT_Product_Model.Rows.Count > 0 Then
    '        lblSPEC_Warranty.Text = DT_Warranty.Rows(0).Item("SPEC_NAME").ToString()
    '        lblDESCRIPTION_Warranty.Text = DT_Warranty.Rows(0).Item("DESCRIPTION").ToString()
    '    End If

    '    'lblDescription_Header.Text = ""  ' จาก Master แปล
    '    lblDescription_Detail.Text = e.Item.DataItem("DESCRIPTION_" & BL.Get_Language_Code(LANGUAGE)).ToString()

    '    'lblPrice_str.Text = ""
    '    If Not IsDBNull(e.Item.DataItem("PRICE")) Then
    '        lblPrice_Money.Text = FormatNumber(Val(e.Item.DataItem("PRICE")), 2)
    '        'lblCurrency_Str.Text = ""

    '    End If


    '    '--------
    '    Dim rptCapacity As Repeater = e.Item.FindControl("rptCapacity")
    '    AddHandler rptCapacity.ItemDataBound, AddressOf rptCapacity_ItemDataBound

    '    Dim rptSpec As Repeater = e.Item.FindControl("rptSpec")
    '    AddHandler rptSpec.ItemDataBound, AddressOf rptSpec_ItemDataBound
    '    Dim rptColor As Repeater = e.Item.FindControl("rptColor")
    '    AddHandler rptColor.ItemDataBound, AddressOf rptColor_ItemDataBound

    '    Dim DT_Capacity As DataTable = BL.GetList_Product_Spec_Capacity(MODEL, KO_ID, Val(e.Item.DataItem("CAT_ID").ToString()), LANGUAGE)
    '    rptCapacity.DataSource = DT_Capacity
    '    rptCapacity.DataBind()

    '    Dim DT_Spec As DataTable = BL.GetList_Product_Spec_Other(e.Item.DataItem("PRODUCT_ID"), KO_ID, LANGUAGE)
    '    rptSpec.DataSource = DT_Spec
    '    rptSpec.DataBind()

    '    Dim DT_Color As DataTable = BL.GetList_Product_Spec_Color(MODEL, KO_ID, LANGUAGE)
    '    rptColor.DataSource = DT_Color
    '    rptColor.DataBind()
    'End Sub


#End Region

#Region "rptCapacity"
    Private Sub rptCapacity_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptCapacity.ItemDataBound
        If e.Item.ItemType <> ListItemType.AlternatingItem And e.Item.ItemType <> ListItemType.Item Then Exit Sub
        Dim lnkCapacity As LinkButton = e.Item.FindControl("lnkCapacity")
        lnkCapacity.Text = e.Item.DataItem("DESCRIPTION").ToString + e.Item.DataItem("Unit").ToString
        'btnCapacity.CommandArgument = e.Item.DataItem("CAT_ID")

        If e.Item.DataItem("DESCRIPTION").ToString = CAPACITY Then
            lnkCapacity.Attributes("class") = "btu active true-bs"
        Else
            lnkCapacity.Attributes("class") = "btu true-bs"
        End If


    End Sub


#End Region

#Region "rptSpec"
    Private Sub rptSpec_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptSpec.ItemDataBound
        If e.Item.ItemType <> ListItemType.AlternatingItem And e.Item.ItemType <> ListItemType.Item Then Exit Sub

        Dim lblSPEC_NAME As Label = e.Item.FindControl("lblSPEC_NAME")
        Dim lblDESCRIPTION As Label = e.Item.FindControl("lblDESCRIPTION")

        lblSPEC_NAME.Text = e.Item.DataItem("SPEC_NAME").ToString
        lblDESCRIPTION.Text = e.Item.DataItem("DESCRIPTION").ToString


    End Sub



#End Region

#Region "rptColor"
    Private Sub rptColor_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptColor.ItemDataBound
        If e.Item.ItemType <> ListItemType.AlternatingItem And e.Item.ItemType <> ListItemType.Item Then Exit Sub
        Dim img As Image = e.Item.FindControl("img")
        Dim lblColor As Label = e.Item.FindControl("lblColor")
        Dim lnkColor As LinkButton = e.Item.FindControl("lnkColor")

        img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & e.Item.DataItem("PRODUCT_ID") & "&LANG=" & LANGUAGE & "&t=" & Now.ToOADate.ToString.Replace(".", "")

        lblColor.Text = e.Item.DataItem("DESCRIPTION").ToString
        If e.Item.DataItem("DESCRIPTION").ToString = CAPACITY Then
            lnkColor.Attributes("class") = "btu active true-bs"
        Else
            lnkColor.Attributes("class") = "btu true-bs"
        End If

    End Sub
    Protected Sub rptColor_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs)

    End Sub


#End Region


    'Private Sub btnSelect_str_Click(sender As Object, e As EventArgs) Handles btnSelect_str.Click
    '    Response.Redirect("Device_Shoping_Cart.aspx")
    'End Sub

End Class