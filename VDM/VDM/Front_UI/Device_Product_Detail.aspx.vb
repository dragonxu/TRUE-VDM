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

    Protected Property BRAND_ID As Integer
        Get
            Try
                Return lblCode.Attributes("BRAND_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(value As Integer)
            lblCode.Attributes("BRAND_ID") = value
        End Set
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
            If Request.QueryString("MODEL") = Nothing Then
                Dim SQL As String = ""
                SQL &= "  SELECT * FROM VW_CURRENT_PRODUCT_DETAIL WHERE PRODUCT_ID=" & PRODUCT_ID
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

            CAPACITY = ""
            COLOR = ""

            BindPage_Color()


        Else
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Scoll", "$(document).ready(function () {$('.detail-slider').slick({infinite: true,slidesToShow: 1,slidesToScroll: 1,}); });", True)

        End If
    End Sub

    Dim DT_Color_Page As DataTable

    Private Sub BindPage_Color()

        Dim SQL As String = ""
        SQL &= "  SELECT DISTINCT  SPEC_ID  "
        SQL &= "  , SPEC_NAME_" & BL.Get_Language_Code(LANGUAGE) & " SPEC_NAME"
        SQL &= "  , dbo.FN_Clean_Line_ITem(DESCRIPTION_" & BL.Get_Language_Code(LANGUAGE) & ") DESCRIPTION_COLOR"

        SQL &= "  FROM VW_CURRENT_PRODUCT_SPEC " & vbLf
        SQL &= "  WHERE KO_ID=" & KO_ID
        SQL &= "        AND MODEL='" & MODEL & "'" & vbLf
        SQL &= "        AND SPEC_ID IN (" & VDM_BL.Spec.Color & ") "


        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        DT_Color_Page = DT
        rptProductList.DataSource = DT
        rptProductList.DataBind()

        'rptProductList.DataSource = DT_Color
        'rptProductList.DataBind()
    End Sub



    Dim Default_Color As String = ""
    Dim Default_Capacity As String = ""
    Dim Default_Product_ID As Integer = 0

    Private Sub rptProductList_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptProductList.ItemDataBound
        Dim img As Image = e.Item.FindControl("img")

        Dim lblDISPLAY_NAME As Label = e.Item.FindControl("lblDISPLAY_NAME")
        Dim lblDesc As Label = e.Item.FindControl("lblDesc")

        'Warranty
        Dim pnlSPEC_Warranty As Panel = e.Item.FindControl("pnlSPEC_Warranty")          'ถ้าไม่มี Warranty pnlSPEC_Warranty=false
        Dim lblSPEC_Warranty As Label = e.Item.FindControl("lblSPEC_Warranty")
        Dim lblDESCRIPTION_Warranty As Label = e.Item.FindControl("lblDESCRIPTION_Warranty")

        'Description
        Dim lblDescription_Header As Label = e.Item.FindControl("lblDescription_Header")
        Dim lblDescription_Detail As Label = e.Item.FindControl("lblDescription_Detail")

        'Price
        Dim lblPrice_str As Label = e.Item.FindControl("lblPrice_str")
        Dim lblPrice_Money As Label = e.Item.FindControl("lblPrice_Money")
        Dim lblCurrency_Str As Label = e.Item.FindControl("lblCurrency_Str")

        Dim btnSelect_str As LinkButton = e.Item.FindControl("btnSelect_str")

        btnSelect_str.Attributes("DESCRIPTION_COLOR") = e.Item.DataItem("DESCRIPTION_COLOR").ToString()

        Default_Color = e.Item.DataItem("DESCRIPTION_COLOR").ToString()


        '-Capacity
        Dim rptCapacity As Repeater = e.Item.FindControl("rptCapacity")
        AddHandler rptCapacity.ItemDataBound, AddressOf rptCapacity_ItemDataBound

        Dim DT_Capacity As DataTable = BL.GetProduct_Choice(MODEL, Default_Color, "", CAT_ID, KO_ID, LANGUAGE)
        If DT_Capacity.Rows.Count > 0 Then
            Default_Capacity = IIf(Not IsDBNull(DT_Capacity.Rows(0).Item("DESCRIPTION_CAPACITY")), DT_Capacity.Rows(0).Item("DESCRIPTION_CAPACITY"), "")

            If Not IsDBNull(DT_Capacity.Rows(0).Item("DESCRIPTION_CAPACITY")) Then
                rptCapacity.DataSource = DT_Capacity
                rptCapacity.DataBind()
            End If

        End If


        '-หา Product ID ที่จะแสดง Item แรก 
        Default_Product_ID = BL.GetProduct_ID_Select(MODEL, e.Item.DataItem("DESCRIPTION_COLOR").ToString(), Default_Capacity, KO_ID, LANGUAGE)
        If Default_Product_ID = 0 Then
            ' หา ID ใหม่


        End If

        '-Color
        Dim rptColor As Repeater = e.Item.FindControl("rptColor")
        AddHandler rptColor.ItemDataBound, AddressOf rptColor_ItemDataBound
        rptColor.DataSource = DT_Color_Page
        rptColor.DataBind()


        '-Detail
        Dim DT As DataTable = BL.GetList_Product_Model(MODEL, KO_ID, Default_Product_ID)
        If DT.Rows.Count > 0 Then
            Dim Path As String = BL.Get_Product_Picture_Path(Default_Product_ID, LANGUAGE)
            If IO.File.Exists(Path) Then
                img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & Default_Product_ID & "&LANG=" & LANGUAGE
            Else
                img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & Default_Product_ID & "&LANG=" & VDM_BL.UILanguage.TH
            End If
            lblDISPLAY_NAME.Text = DT.Rows(0).Item("DISPLAY_NAME_" & BL.Get_Language_Code(LANGUAGE)).ToString()

            'Warranty
            'pnlSPEC_Warranty.
            Dim DT_Warranty As DataTable = BL.GetList_Product_Spec_Warranty(Default_Product_ID, KO_ID, LANGUAGE)
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



            Dim DT_Spec As DataTable = BL.GetList_Product_Spec_Other(Default_Product_ID, KO_ID, LANGUAGE)
            Dim rptSpec As Repeater = e.Item.FindControl("rptSpec")
            AddHandler rptSpec.ItemDataBound, AddressOf rptSpec_ItemDataBound
            rptSpec.DataSource = DT_Spec
            rptSpec.DataBind()
        End If


        btnSelect_str.CommandArgument = Default_Product_ID



    End Sub

    Private Sub rptProductList_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptProductList.ItemCommand
        Dim btnSelect_str As LinkButton = e.Item.FindControl("btnSelect_str")
        Response.Redirect("Device_Shoping_Cart.aspx?PRODUCT_ID=" & btnSelect_str.CommandArgument)

    End Sub




#Region "Capacity"
    Protected Sub rptCapacity_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        If e.Item.ItemType <> ListItemType.AlternatingItem And e.Item.ItemType <> ListItemType.Item Then Exit Sub
        Dim lnkCapacity As LinkButton = e.Item.FindControl("lnkCapacity")
        lnkCapacity.Text = e.Item.DataItem("DESCRIPTION_CAPACITY").ToString + e.Item.DataItem("Unit").ToString
        lnkCapacity.Attributes("DESCRIPTION_CAPACITY") = e.Item.DataItem("DESCRIPTION_CAPACITY").ToString()
        lnkCapacity.Attributes("DESCRIPTION_COLOR") = Default_Color

        If e.Item.DataItem("DESCRIPTION_CAPACITY").ToString = Default_Capacity Then
            lnkCapacity.Style("color") = "#FFF"
            lnkCapacity.Style("background") = "#47464B"
        Else
            lnkCapacity.Style("color") = "#47464B"
            lnkCapacity.Style("background") = "#FFF"
        End If



    End Sub

    Protected Sub rptCapacity_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs)

        Dim Repeater As Repeater = sender
        Dim rptProductList As RepeaterItem = Repeater.NamingContainer


        'Dim rpt As RepeaterCommandEventArgs = sender
        'Dim rptProductList As RepeaterItem = rpt.Item

        Dim lnkCapacity As LinkButton = e.Item.FindControl("lnkCapacity")

        'Dim rptProductList As Repeater = e.Item.FindControl("rptProductList")

        '----rptProductList---
        Dim img As Image = DirectCast(rptProductList.FindControl("ddlSpec_TH"), Image)

        Dim lblDISPLAY_NAME As Label = DirectCast(rptProductList.FindControl("lblDISPLAY_NAME"), Label)
        Dim lblDesc As Label = DirectCast(rptProductList.FindControl("lblDesc"), Label)

        'Warranty
        Dim pnlSPEC_Warranty As Panel = DirectCast(rptProductList.FindControl("pnlSPEC_Warranty"), Panel)         'ถ้าไม่มี Warranty pnlSPEC_Warranty=false
        Dim lblSPEC_Warranty As Label = DirectCast(rptProductList.FindControl("lblSPEC_Warranty"), Label)
        Dim lblDESCRIPTION_Warranty As Label = DirectCast(rptProductList.FindControl("lblDESCRIPTION_Warranty"), Label)

        'Description
        Dim lblDescription_Header As Label = DirectCast(rptProductList.FindControl("lblDescription_Header"), Label)
        Dim lblDescription_Detail As Label = DirectCast(rptProductList.FindControl("lblDescription_Detail"), Label)

        'Price
        Dim lblPrice_str As Label = DirectCast(rptProductList.FindControl("lblPrice_str"), Label)
        Dim lblPrice_Money As Label = DirectCast(rptProductList.FindControl("lblPrice_Money"), Label)
        Dim lblCurrency_Str As Label = DirectCast(rptProductList.FindControl("lblCurrency_Str"), Label)

        Dim btnSelect_str As LinkButton = DirectCast(rptProductList.FindControl("btnSelect_str"), LinkButton)
        '----rptProductList---





        '-Detail
        Dim P_ID As Integer = 0
        P_ID = BL.GetProduct_ID_Select(MODEL, btnSelect_str.Attributes("DESCRIPTION_COLOR").ToString(), lnkCapacity.Attributes("DESCRIPTION_CAPACITY").ToString, KO_ID, LANGUAGE)

        Dim DT As DataTable = BL.GetList_Product_Model(MODEL, KO_ID, P_ID)
        If DT.Rows.Count > 0 Then
            'Dim Path As String = BL.Get_Product_Picture_Path(P_ID, LANGUAGE)
            'If IO.File.Exists(Path) Then
            '    img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & P_ID & "&LANG=" & LANGUAGE
            'Else
            '    img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & P_ID & "&LANG=" & VDM_BL.UILanguage.TH
            'End If
            lblDISPLAY_NAME.Text = DT.Rows(0).Item("DISPLAY_NAME_" & BL.Get_Language_Code(LANGUAGE)).ToString()

            'Warranty
            'pnlSPEC_Warranty.
            Dim DT_Warranty As DataTable = BL.GetList_Product_Spec_Warranty(P_ID, KO_ID, LANGUAGE)
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


            Dim rptSpec As Repeater = DirectCast(rptProductList.FindControl("rptSpec"), Repeater)

            Dim DT_Spec As DataTable = BL.GetList_Product_Spec_Other(P_ID, KO_ID, LANGUAGE)
            'Dim rptSpec As Repeater = e.Item.FindControl("rptSpec")
            AddHandler rptSpec.ItemDataBound, AddressOf rptSpec_ItemDataBound
            rptSpec.DataSource = DT_Spec
            rptSpec.DataBind()
        End If

        '-Capacity
        Dim rptCapacity As Repeater = sender
        AddHandler rptCapacity.ItemDataBound, AddressOf rptCapacity_ItemDataBound

        Dim DT_Capacity As DataTable = BL.GetProduct_Choice(MODEL, btnSelect_str.Attributes("DESCRIPTION_COLOR").ToString(), "", CAT_ID, KO_ID, LANGUAGE)
        If DT_Capacity.Rows.Count > 0 Then
            Default_Capacity = lnkCapacity.Attributes("DESCRIPTION_CAPACITY").ToString
        End If
        rptCapacity.DataSource = DT_Capacity
        rptCapacity.DataBind()


        btnSelect_str.CommandArgument = P_ID
    End Sub

#End Region


#Region "Color"
    Protected Sub rptColor_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        If e.Item.ItemType <> ListItemType.AlternatingItem And e.Item.ItemType <> ListItemType.Item Then Exit Sub
        Dim img As Image = e.Item.FindControl("img")
        Dim lblColor As Label = e.Item.FindControl("lblColor")
        Dim lnkColor As LinkButton = e.Item.FindControl("lnkColor")


        Dim btnSelect As Button = e.Item.FindControl("btnSelect")

        'Dim btnColor As HtmlAnchor = e.Item.FindControl("btnColor")
        'btnColor.Attributes("onclick") = "$('#" & btnSelect.ClientID & "').click();"
        Dim P_ID As Integer = 0
        P_ID = BL.GetProduct_ID_Select(MODEL, e.Item.DataItem("DESCRIPTION_COLOR").ToString(), Default_Capacity, KO_ID, LANGUAGE)


        Dim Path As String = BL.Get_Product_Picture_Path(P_ID, LANGUAGE)
        If IO.File.Exists(Path) Then
            img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & P_ID & "&LANG=" & LANGUAGE
        Else
            img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & P_ID & "&LANG=" & VDM_BL.UILanguage.TH
        End If
        lblColor.Text = e.Item.DataItem("DESCRIPTION_COLOR").ToString

        Dim pnlSelect As Panel = e.Item.FindControl("pnlSelect")
        Dim BoxIndex As HtmlGenericControl = e.Item.FindControl("BoxIndex")
        If e.Item.DataItem("DESCRIPTION_COLOR").ToString = Default_Color Then
            pnlSelect.Attributes("class") = "select-color-active"
        Else
            pnlSelect.Attributes("class") = "select-color"
        End If
        lnkColor.CommandArgument = e.Item.DataItem("DESCRIPTION_COLOR").ToString()
    End Sub
#End Region

#Region "rptSpec"

    Protected Sub rptSpec_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        If e.Item.ItemType <> ListItemType.AlternatingItem And e.Item.ItemType <> ListItemType.Item Then Exit Sub

        Dim lblSPEC_NAME As Label = e.Item.FindControl("lblSPEC_NAME")
        Dim lblDESCRIPTION As Label = e.Item.FindControl("lblDESCRIPTION")
        Dim lblDesc As Label = e.Item.FindControl("lblDesc")

        lblSPEC_NAME.Text = e.Item.DataItem("SPEC_NAME").ToString
        'lblDESCRIPTION.Text = e.Item.DataItem("DESCRIPTION").ToString

        lblDesc.Text = e.Item.DataItem("DESCRIPTION").ToString
    End Sub



#End Region


    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Select_Menu.aspx")
    End Sub

    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click
        Response.Redirect("Product_List.aspx?BRAND_ID=" & BRAND_ID)
    End Sub

End Class