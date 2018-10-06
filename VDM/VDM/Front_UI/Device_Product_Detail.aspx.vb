Imports System.Data
Imports System.Data.SqlClient

Public Class Device_Product_Detail
    Inherits System.Web.UI.Page
    Dim BL As New VDM_BL

#Region "ส่วนที่เหมือนกันหมดทุกหน้า"
    Private ReadOnly Property KO_ID As Integer '------------- เอาไว้เรียกใช้ง่ายๆ ----------
        Get
            Try
                Return Request.Cookies("KO_ID").Value
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Private ReadOnly Property LANGUAGE As VDM_BL.UILanguage '------- ต้องเป็น ReadOnly --------
        Get
            Try
                Return Session("LANGUAGE")
            Catch ex As Exception
                Return 0
            End Try

        End Get
    End Property

    Private ReadOnly Property TXN_ID As Integer
        Get
            Try
                Return Session("TXN_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

#End Region

    Protected ReadOnly Property BRAND_ID As Integer
        Get
            Try
                Return Request.QueryString("BRAND_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Protected Property PRODUCT_ID As Integer
        Get
            Try
                Return lblCode.Attributes("PRODUCT_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(value As Integer)
            lblCode.Attributes("PRODUCT_ID") = value
        End Set
    End Property

    Protected Property CAT_ID As Integer
        Get
            Try
                Return lblCode.Attributes("CAT_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(value As Integer)
            lblCode.Attributes("CAT_ID") = value
        End Set
    End Property

    Protected Property MODEL As String
        Get
            Try
                Return lblCode.Attributes("MODEL")
            Catch ex As Exception
                Return ""
            End Try
        End Get
        Set(value As String)
            lblCode.Attributes("MODEL") = value
        End Set
    End Property

    Protected Property CAPACITY As String
        Get
            Try
                Return lblCode.Attributes("CAPACITY")
            Catch ex As Exception
                Return ""
            End Try
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
        If Not IsNumeric(Session("LANGUAGE")) Then
            Response.Redirect("Select_Language.aspx")
        End If

        If Not IsPostBack Then
            PRODUCT_ID = Request.QueryString("PRODUCT_ID")
            'If Request.QueryString("MODEL") = Nothing Then

            Dim SQL As String = ""
            SQL &= "  SELECT * FROM VW_CURRENT_PRODUCT_DETAIL WHERE PRODUCT_ID=" & PRODUCT_ID
            Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
            Dim DT_Para As New DataTable
            DA.Fill(DT_Para)
            If DT_Para.Rows.Count > 0 Then
                MODEL = DT_Para.Rows(0).Item("MODEL")
                'BRAND_ID = DT_Para.Rows(0).Item("BRAND_ID")
            End If

            'Else
            '    BRAND_ID = Request.QueryString("BRAND_ID")
            '    MODEL = Request.QueryString("MODEL").ToString
            'End If

            CAPACITY = ""
            COLOR = ""
            BindDetail()
            '--เข้ามาครั้งแรก Default สี ความจุ--
            BindFirst(CAT_ID)

        Else
            initFormPlugin()
        End If

    End Sub

    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Scoll", "$(document).ready(function () {$('.card-slider').slick({infinite: true,slidesToShow: 3,slidesToScroll: 1,}); });", True)



    End Sub

    Dim DT_Product_Model As New DataTable

#Region "rptProductList"

    Private Sub BindDetail()

        Dim DT As DataTable = BL.GetList_Product_Model(MODEL, KO_ID, PRODUCT_ID)

        If DT.Rows.Count > 0 Then
            DT_Product_Model = DT   ' Product ที่จัดกลุ่ม Model  เดียวกัน

            'แสดง Product Default ครั้งแรก
            'chack pic
            Dim Path As String = BL.Get_Product_Picture_Path(PRODUCT_ID, LANGUAGE)
            If IO.File.Exists(Path) Then
                img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & PRODUCT_ID & "&LANG=" & LANGUAGE
            Else
                img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & PRODUCT_ID & "&LANG=" & VDM_BL.UILanguage.TH
            End If
            lblDISPLAY_NAME.Text = DT.Rows(0).Item("DISPLAY_NAME_" & BL.Get_Language_Code(LANGUAGE)).ToString()

            'Warranty
            'pnlSPEC_Warranty.
            Dim DT_Warranty As DataTable = BL.GetList_Product_Spec_Warranty(PRODUCT_ID, KO_ID, LANGUAGE)
            If DT_Warranty.Rows.Count > 0 Then
                lblSPEC_Warranty.Text = DT_Warranty.Rows(0).Item("SPEC_NAME").ToString()
                lblDESCRIPTION_Warranty.Text = DT_Warranty.Rows(0).Item("DESCRIPTION").ToString()
            End If

            lblDesc.Text = DT.Rows(0).Item("DESCRIPTION_" & BL.Get_Language_Code(LANGUAGE)).ToString()

            'lblPrice_str.Text = ""
            If Not IsDBNull(DT.Rows(0).Item("PRICE")) Then
                lblPrice_Money.Text = FormatNumber(Val(DT.Rows(0).Item("PRICE")), 2)
                'lblCurrency_Str.Text = ""
            End If

            ''--เข้ามาครั้งแรก Default สี ความจุ-- 
            CAT_ID = IIf(Not IsDBNull(DT.Rows(0).Item("CAT_ID")), DT.Rows(0).Item("CAT_ID"), 0)

            Dim DT_Spec As DataTable = BL.GetList_Product_Spec_Other(DT.Rows(0).Item("PRODUCT_ID"), KO_ID, LANGUAGE)
            rptSpec.DataSource = DT_Spec
            rptSpec.DataBind()
        End If

    End Sub

    Private Sub BindFirst(ByRef Catagory As Integer)
        CAT_ID = Catagory
        '--เข้ามาครั้งแรก--
        '--Set Current Select
        Dim SQL_Active As String = ""
        SQL_Active &= "  Select DISTINCT PRODUCT_ID,PRODUCT_CODE,PRODUCT_NAME,KO_ID " & vbLf
        SQL_Active &= "        ,SPEC_ID,SEQ  " & vbLf
        SQL_Active &= "        ,SPEC_NAME_" & BL.Get_Language_Code(LANGUAGE).ToString() & ",DESCRIPTION_" & BL.Get_Language_Code(LANGUAGE).ToString()
        SQL_Active &= "        ,DESCRIPTION_TH COLOR_TH       " & vbLf
        SQL_Active &= "        ,CAT_ID,MODEL       " & vbLf
        SQL_Active &= "    From VW_CURRENT_PRODUCT_SPEC " & vbLf
        SQL_Active &= "    Where PRODUCT_ID =" & PRODUCT_ID & " And SPEC_ID In (" & VDM_BL.Spec.Capacity & "," & VDM_BL.Spec.Color & ") " & vbLf

        Dim DA As New SqlDataAdapter(SQL_Active, BL.ConnectionString)
        Dim DT_Active As New DataTable
        DA.Fill(DT_Active)
        If DT_Active.Rows.Count > 0 Then
            For i As Integer = 0 To DT_Active.Rows.Count - 1
                If DT_Active.Rows(i).Item("SPEC_ID") = VDM_BL.Spec.Color Then
                    COLOR = DT_Active.Rows(i).Item("COLOR_TH")
                End If
                If DT_Active.Rows(i).Item("SPEC_ID") = VDM_BL.Spec.Capacity Then
                    CAPACITY = DT_Active.Rows(i).Item("DESCRIPTION_" & BL.Get_Language_Code(LANGUAGE)).ToString()
                End If
            Next
        End If

        'ทั้งหมด
        Dim DT_Capacity As DataTable = BL.GetList_Product_Spec_Capacity(MODEL, KO_ID, CAT_ID, LANGUAGE)
        rptCapacity.DataSource = DT_Capacity
        rptCapacity.DataBind()

        'ทั้งหมด
        Dim DT_Color As DataTable = BL.GetProduct_Choice(MODEL, "", CAPACITY, CAT_ID, KO_ID, LANGUAGE)

        'Dim DT_Color As DataTable = BL.GetList_Product_Spec_Color(MODEL, KO_ID, LANGUAGE)
        rptColor.DataSource = DT_Color
        rptColor.DataBind()

    End Sub

#End Region

#Region "rptCapacity"
    Private Sub rptCapacity_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptCapacity.ItemDataBound
        If e.Item.ItemType <> ListItemType.AlternatingItem And e.Item.ItemType <> ListItemType.Item Then Exit Sub
        Dim lnkCapacity As LinkButton = e.Item.FindControl("lnkCapacity")
        lnkCapacity.Text = e.Item.DataItem("DESCRIPTION_CAPACITY").ToString + e.Item.DataItem("Unit").ToString
        lnkCapacity.Attributes("DESCRIPTION_CAPACITY") = e.Item.DataItem("DESCRIPTION_CAPACITY").ToString()

        If e.Item.DataItem("DESCRIPTION_CAPACITY").ToString = CAPACITY Then
            lnkCapacity.Style("color") = "#FFF"
            lnkCapacity.Style("background") = "#47464B"
        Else
            lnkCapacity.Style("color") = "#47464B"
            lnkCapacity.Style("background") = "#FFF"
        End If


    End Sub

    Private Sub rptCapacity_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptCapacity.ItemCommand
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub
        Dim lnkCapacity As LinkButton = e.Item.FindControl("lnkCapacity")
        Select Case e.CommandName
            Case "Select"
                CAPACITY = lnkCapacity.Attributes("DESCRIPTION_CAPACITY").ToString
                'MODEL COLOR CAPACITY

                PRODUCT_ID = BL.GetProduct_ID_Select(MODEL, COLOR, CAPACITY, KO_ID, LANGUAGE)
                If PRODUCT_ID = 0 Then
                    'ทั้งหมด
                    Dim DT_Color As DataTable = BL.GetProduct_Choice(MODEL, "", CAPACITY, CAT_ID, KO_ID, LANGUAGE)
                    If DT_Color.Rows.Count > 0 Then
                        COLOR = DT_Color.Rows(0).Item("COLOR_TH").ToString()
                    End If
                    rptColor.DataSource = DT_Color
                    rptColor.DataBind()
                End If
                PRODUCT_ID = BL.GetProduct_ID_Select(MODEL, COLOR, CAPACITY, KO_ID, LANGUAGE)

                BindDetail()

                Dim DT_Capacity As DataTable = BL.GetList_Product_Spec_Capacity(MODEL, KO_ID, CAT_ID, LANGUAGE)
                rptCapacity.DataSource = DT_Capacity
                rptCapacity.DataBind()
        End Select
    End Sub

#End Region

#Region "rptSpec"
    Private Sub rptSpec_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptSpec.ItemDataBound
        If e.Item.ItemType <> ListItemType.AlternatingItem And e.Item.ItemType <> ListItemType.Item Then Exit Sub

        Dim lblSPEC_NAME As Label = e.Item.FindControl("lblSPEC_NAME")
        Dim lblDESCRIPTION As Label = e.Item.FindControl("lblDESCRIPTION")
        Dim lblDesc As Label = e.Item.FindControl("lblDesc")
        'Dim txtarea As HtmlTextArea = e.Item.FindControl("txtarea")
        'txtarea.InnerText = e.Item.DataItem("DESCRIPTION").ToString

        lblSPEC_NAME.Text = e.Item.DataItem("SPEC_NAME").ToString
        'lblDESCRIPTION.Text = e.Item.DataItem("DESCRIPTION").ToString

        lblDesc.Text = e.Item.DataItem("DESCRIPTION").ToString
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
        Dim Path As String = BL.Get_Product_Picture_Path(e.Item.DataItem("PRODUCT_ID"), LANGUAGE)
        If IO.File.Exists(Path) Then
            img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & e.Item.DataItem("PRODUCT_ID") & "&LANG=" & LANGUAGE
        Else
            img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & e.Item.DataItem("PRODUCT_ID") & "&LANG=" & VDM_BL.UILanguage.TH
        End If
        lblColor.Text = e.Item.DataItem("DESCRIPTION_COLOR").ToString

        Dim pnlSelect As Panel = e.Item.FindControl("pnlSelect")
        Dim BoxIndex As HtmlGenericControl = e.Item.FindControl("BoxIndex")
        'If e.Item.DataItem("COLOR_TH").ToString = COLOR Then
        '    pnlSelect.Attributes("class") = "select-color-active"
        'Else
        '    pnlSelect.Attributes("class") = "select-color"
        'End If
        lnkColor.CommandArgument = e.Item.DataItem("COLOR_TH").ToString()
    End Sub

    Protected Sub rptColor_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs)
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub
        Dim lblColor As Label = e.Item.FindControl("lblColor")
        Dim lnkColor As LinkButton = e.Item.FindControl("lnkColor")
        Dim img As Image = e.Item.FindControl("img")
        Select Case e.CommandName
            Case "Select"
                COLOR = lnkColor.CommandArgument
                'MODEL COLOR CAPACITY
                'img.Attributes("class") = "btn-active "
                PRODUCT_ID = BL.GetProduct_ID_Select(MODEL, COLOR, CAPACITY, KO_ID, LANGUAGE)
                If PRODUCT_ID = 0 Then
                    'ทั้งหมด
                    Dim DT_Capacity As DataTable = BL.GetProduct_Choice(MODEL, COLOR, "", CAT_ID, KO_ID, LANGUAGE)
                    If DT_Capacity.Rows.Count > 0 Then
                        CAPACITY = IIf(Not IsDBNull(DT_Capacity.Rows(0).Item("DESCRIPTION_CAPACITY")), DT_Capacity.Rows(0).Item("DESCRIPTION_CAPACITY"), "")
                    End If

                End If

                Dim DT_Color As DataTable = BL.GetProduct_Choice(MODEL, "", CAPACITY, CAT_ID, KO_ID, LANGUAGE)
                'DT_Color.Columns.Add("Order")
                'If DT_Color.Rows.Count > 3 Then
                '    Dim selectIndex As Integer = e.Item.ItemIndex
                '    For i As Integer = 0 To DT_Color.Rows.Count - 1
                '        If selectIndex = i Then
                '            DT_Color.Rows(i).Item("Order") = 0
                '        ElseIf i < selectIndex Then
                '            DT_Color.Rows(i).Item("Order") = i + DT_Color.Rows.Count
                '        Else
                '            DT_Color.Rows(i).Item("Order") = DT_Color.Rows.Count - i
                '        End If

                '    Next
                '    DT_Color.DefaultView.Sort = "Order ASC"
                '    rptColor.DataSource = DT_Color.DefaultView.ToTable
                '    rptColor.DataBind()
                'Else
                '    rptColor.DataSource = DT_Color
                '    rptColor.DataBind()
                'End If
                'rptColor.DataSource = DT_Color
                'rptColor.DataBind()
                PRODUCT_ID = BL.GetProduct_ID_Select(MODEL, COLOR, CAPACITY, KO_ID, LANGUAGE)

                BindDetail()

        End Select
    End Sub

    Private Sub btnSelect_str_Click(sender As Object, e As EventArgs) Handles btnSelect_str.Click
        Response.Redirect("Device_Shoping_Cart.aspx?PRODUCT_ID=" & PRODUCT_ID)
    End Sub

#End Region



    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Select_Menu.aspx")
    End Sub

    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click
        Response.Redirect("Product_List.aspx?BRAND_ID=" & BRAND_ID)
    End Sub


End Class