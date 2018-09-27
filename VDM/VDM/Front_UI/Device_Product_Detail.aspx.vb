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
            If Request.QueryString("MODEL") = Nothing Then
                Dim SQL As String = ""
                SQL &= "  SELECT  FROM VW_CURRENT_PRODUCT_DETAIL WHERE PRODUCT_ID=" & PRODUCT_ID
                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                Dim DT_Para As New DataTable
                DA.Fill(DT_Para)
                If DT_Para.Rows.Count > 0 Then
                    MODEL = DT_Para.Rows(0).Item("MODEL")
                    BRAND_ID = DT_Para.Rows(0).Item("BRAND_ID")
                End If

            Else
                BRAND_ID = Request.QueryString("BRAND_ID")
                MODEL = Request.QueryString("MODEL").ToString
            End If
            PRODUCT_ID = Request.QueryString("PRODUCT_ID")
            CAPACITY = ""
            COLOR = ""


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
            If DT_Warranty.Rows.Count > 0 Then
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



#End Region

#Region "rptCapacity"
    Private Sub rptCapacity_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptCapacity.ItemDataBound
        If e.Item.ItemType <> ListItemType.AlternatingItem And e.Item.ItemType <> ListItemType.Item Then Exit Sub
        Dim lnkCapacity As LinkButton = e.Item.FindControl("lnkCapacity")
        lnkCapacity.Text = e.Item.DataItem("DESCRIPTION").ToString + e.Item.DataItem("Unit").ToString
        'btnCapacity.CommandArgument = e.Item.DataItem("CAT_ID")

        'If e.Item.DataItem("DESCRIPTION").ToString = CAPACITY Then
        '    lnkCapacity.Attributes("class") = "btu active true-bs"
        'Else
        '    lnkCapacity.Attributes("class") = "btu true-bs"
        'End If

        'lnkCapacity.Style("color") = "#FFF"
        'lnkCapacity.Style("background") = "##47464B"


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

        Dim btnColor As HtmlAnchor = e.Item.FindControl("btnColor")
        Dim btnSelect As Button = e.Item.FindControl("btnSelect")
        btnColor.Attributes("onclick") = "$('#" & btnSelect.ClientID & "').click();"

        img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & e.Item.DataItem("PRODUCT_ID") & "&LANG=" & LANGUAGE & "&t=" & Now.ToOADate.ToString.Replace(".", "")

        lblColor.Text = e.Item.DataItem("DESCRIPTION").ToString
        'If e.Item.DataItem("DESCRIPTION").ToString = COLOR Then
        '    lnkColor.Attributes("class") = "btu active true-bs"
        '    'img.Attributes("CssClass") = "btn-active"
        'Else
        '    lnkColor.Attributes("class") = "btu true-bs"
        'End If

        lnkColor.CommandArgument = e.Item.DataItem("DESCRIPTION").ToString()
    End Sub

    Protected Sub rptColor_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs)
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub
        Dim lblColor As Label = e.Item.FindControl("lblColor")
        Dim img As Image = e.Item.FindControl("img")
        Select Case e.CommandName
            Case "Select"
                COLOR = lblColor.Text
                'MODEL COLOR CAPACITY
                'img.Attributes("class") = "btn-active "
                PRODUCT_ID = BL.GetProduct_ID_Select(MODEL, COLOR, CAPACITY, KO_ID, LANGUAGE)
                BindList()
        End Select
    End Sub

    Private Sub btnSelect_str_Click(sender As Object, e As EventArgs) Handles btnSelect_str.Click
        Response.Redirect("Device_Shoping_Cart.aspx?PRODUCT_ID=" & PRODUCT_ID & "&MODEL=" & MODEL & "&BRAND_ID=" & BRAND_ID)
    End Sub

#End Region



    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Home.aspx")
    End Sub

    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click
        Response.Redirect("Product_List.aspx?BRAND_ID=" & BRAND_ID)
    End Sub

End Class