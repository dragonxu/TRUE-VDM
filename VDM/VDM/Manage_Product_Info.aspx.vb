
Imports System.Data
Imports System.Data.SqlClient

Public Class Manage_Product_Info
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL
    Dim BackEndInterface As New BackEndInterface.Get_Product_Info

    Protected Property PRODUCT_ID As Integer
        Get
            Return Val(txtCode.Attributes("PRODUCT_ID"))
        End Get
        Set(value As Integer)
            txtCode.Attributes("PRODUCT_ID") = value
        End Set
    End Property

    Protected Property IS_SIM As Integer
        Get
            Return Val(txtCode.Attributes("PRODUCT_ID"))
        End Get
        Set(value As Integer)
            txtCode.Attributes("PRODUCT_ID") = value
        End Set
    End Property

    Private Property PRODUCT_Logo_TH As Byte()
        Get
            Try
                Return Session("PRODUCT_Logo_TH")
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
        Set(value As Byte())
            Session("PRODUCT_Logo_TH") = value
            imgIcon_TH.ImageUrl = "RenderImage.aspx?Mode=S&UID=PRODUCT_Logo_TH&t=" & Now.ToOADate.ToString.Replace(".", "")
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'lnkUpload.Attributes("onClick") = "document.getElementById('" & FileUpload1.ClientID & "').click();"

        If Not IsPostBack Then
            ResetPage(Nothing, Nothing)
            initFirstTimeJavascript()
        Else
            initFormPlugin()
        End If

    End Sub


    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Private Sub initFirstTimeJavascript()
        'THAI
        imgIcon_TH.Attributes("onclick") = "window.open(this.src);"
        ful_TH.Attributes("onchange") = "$('#" & btnUpdateLogo_TH.ClientID & "').click();"

        'ENGLISH
        'CHINESE
        'JAPANESE
        'KOREAN
        'RUSSIAN


    End Sub

    Protected Sub ResetPage(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        BindList()
        ClearEditForm()
        'pnlList.Visible = True
        'pnlEdit.Visible = False
    End Sub

    Private Sub ClearEditForm()
        PRODUCT_ID = 0

        txtCode.Text = ""
        rdIsSerial_Yes.Checked = False
        rdIsSerial_No.Checked = False
        'BL.DDL
        ddlBrand.SelectedIndex = 0
        rdRequireReceive_Yes.Checked = False
        rdRequireReceive_No.Checked = False
        ModuleGlobal.ImplementJavaMoneyText(txtPrice)
        Tab_THAI.Attributes("class") = "active"

        'THAI
        txtDisplayName_TH.Text = ""
        txtDescription_TH.Text = ""
        rptCaptionList_TH.DataSource = Nothing
        rptCaptionList_TH.DataBind()
        PRODUCT_Logo_TH = Nothing

        'ENGLISH
        txtDisplayName_EN.Text = ""
        txtDescription_EN.Text = ""
        rptCaptionList_EN.DataSource = Nothing
        rptCaptionList_EN.DataBind()
        'PRODUCT_Logo = Nothing

        'CHINESE
        txtDisplayName_CN.Text = ""
        txtDescription_CN.Text = ""
        rptCaptionList_CN.DataSource = Nothing
        rptCaptionList_CN.DataBind()
        'PRODUCT_Logo = Nothing

        'JAPANESE
        txtDisplayName_JP.Text = ""
        txtDescription_JP.Text = ""
        rptCaptionList_JP.DataSource = Nothing
        rptCaptionList_JP.DataBind()
        'PRODUCT_Logo = Nothing

        'KOREAN
        txtDisplayName_KR.Text = ""
        txtDescription_KR.Text = ""
        rptCaptionList_KR.DataSource = Nothing
        rptCaptionList_KR.DataBind()
        'PRODUCT_Logo = Nothing

        'RUSSIAN
        txtDisplayName_RS.Text = ""
        txtDescription_RS.Text = ""
        rptCaptionList_RS.DataSource = Nothing
        rptCaptionList_RS.DataBind()
        'PRODUCT_Logo = Nothing


        chkActive.Checked = True
    End Sub

    Private Sub BindList()
        Dim SQL As String = "SELECT * FROM VW_ALL_PRODUCT ORDER BY PRODUCT_CODE "
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        rptList.DataSource = DT
        rptList.DataBind()

        lblTotalList.Text = FormatNumber(DT.Rows.Count, 0)

        'pnlList.Visible = True
        'pnlEdit.Visible = False
    End Sub

    Private Sub rptList_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptList.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim img As Image = e.Item.FindControl("img")
        Dim lblProductCode As Label = e.Item.FindControl("lblProductCode")
        Dim lblDisplayName As Label = e.Item.FindControl("lblDisplayName")
        Dim lblCountSpec As Label = e.Item.FindControl("lblCountSpec")
        Dim lblPrice As Label = e.Item.FindControl("lblPrice")
        Dim ImageActive As Image = e.Item.FindControl("ImageActive")
        Dim btnEdit As Button = e.Item.FindControl("btnEdit")
        Dim btnDelete As Button = e.Item.FindControl("btnDelete")

        img.ImageUrl = "RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & e.Item.DataItem("PRODUCT_ID") & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        lblProductCode.Text = e.Item.DataItem("PRODUCT_CODE").ToString
        lblDisplayName.Text = e.Item.DataItem("DISPLAY_NAME_TH").ToString
        'lblCountSpec.Text = "" - --------------------
        lblPrice.Text = FormatNumber(e.Item.DataItem("Price"), 2)
        'ImageActive

        btnEdit.CommandArgument = e.Item.DataItem("PRODUCT_ID")
        btnDelete.CommandArgument = e.Item.DataItem("PRODUCT_ID")
        Dim btnPreDelete As HtmlInputButton = e.Item.FindControl("btnPreDelete")
        btnPreDelete.Attributes("onclick") = "If(confirm('ยืนยันลบ " & lblProductCode.Text & " ?'))$('#" & btnDelete.ClientID & "').click();"

    End Sub

    Private Sub rptList_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptList.ItemCommand
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Select Case e.CommandName
            Case "Edit"
                '--ดึงข้อมูล Product
                Dim SQL As String = "SELECT * FROM MS_Product WHERE PRODUCT_ID=" & e.CommandArgument
                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                Dim DT As New DataTable
                DA.Fill(DT)
                If DT.Rows.Count = 0 Then
                    Alert(Me.Page, "ไม่พบข้อมูล")
                    BindList()
                    Exit Sub
                End If

                ClearEditForm()
                PRODUCT_ID = e.CommandArgument
                lblEditMode.Text = "Edit"

                '--Detail
                txtCode.Text = DT.Rows(0).Item("PRODUCT_CODE").ToString
                rdIsSerial_Yes.Checked = IIf(DT.Rows(0).Item("IS_SERIAL") = 1, True, False)
                rdIsSerial_No.Checked = IIf(DT.Rows(0).Item("IS_SERIAL") = 1, True, False)
                If Not IsDBNull(DT.Rows(0).Item("BRAND_CODE")) Then
                    BL.Bind_DDL_Brand(ddlBrand, DT.Rows(0).Item("BRAND_CODE"))
                End If
                rdRequireReceive_Yes.Checked = IIf(DT.Rows(0).Item("REQUIRE_RECEIVE_FORM") = 1, True, False)
                rdRequireReceive_No.Checked = IIf(DT.Rows(0).Item("REQUIRE_RECEIVE_FORM") = 1, True, False)

                ModuleGlobal.ImplementJavaMoneyText(txtPrice)
                If Not IsDBNull(DT.Rows(0).Item("PRICE")) Then
                    txtPrice.Text = FormatNumber(DT.Rows(0).Item("PRICE"), 2)
                End If

                '---SPEC
                Dim SQL_SPEC As String = "SELECT * FROM VW_PRODUCT_SPEC WHERE PRODUCT_ID=" & e.CommandArgument & " ORDER BY SEQ "
                Dim DA_SPEC As New SqlDataAdapter(SQL_SPEC, BL.ConnectionString)
                Dim DT_SPEC As New DataTable
                DA_SPEC.Fill(DT_SPEC)

                Dim DT_SPEC_TH As New DataTable
                Dim DT_SPEC_EN As New DataTable
                Dim DT_SPEC_CN As New DataTable
                Dim DT_SPEC_JP As New DataTable
                Dim DT_SPEC_KR As New DataTable
                Dim DT_SPEC_RS As New DataTable

                If DT_SPEC.Rows.Count > 0 Then
                    '--Filter หาข้อมูลแต่ละภาษา
                    DT_SPEC.DefaultView.RowFilter = "SPEC_NAME_TH IS NOT NULL AND DESCRIPTION_TH IS NOT NULL "
                    DT_SPEC_TH = DT_SPEC.DefaultView.ToTable
                    DT_SPEC.DefaultView.RowFilter = "SPEC_NAME_EN IS NOT NULL AND DESCRIPTION_EN IS NOT NULL "
                    DT_SPEC_EN = DT_SPEC.DefaultView.ToTable
                    DT_SPEC.DefaultView.RowFilter = "SPEC_NAME_CN IS NOT NULL AND DESCRIPTION_CN IS NOT NULL "
                    DT_SPEC_CN = DT_SPEC.DefaultView.ToTable

                    DT_SPEC.DefaultView.RowFilter = "SPEC_NAME_JP IS NOT NULL AND DESCRIPTION_JP IS NOT NULL "
                    DT_SPEC_JP = DT_SPEC.DefaultView.ToTable
                    DT_SPEC.DefaultView.RowFilter = "SPEC_NAME_KR IS NOT NULL AND DESCRIPTION_KR IS NOT NULL "
                    DT_SPEC_KR = DT_SPEC.DefaultView.ToTable
                    DT_SPEC.DefaultView.RowFilter = "SPEC_NAME_RS IS NOT NULL AND DESCRIPTION_RS IS NOT NULL "
                    DT_SPEC_RS = DT_SPEC.DefaultView.ToTable
                End If

                '==THAI==
                txtDisplayName_TH.Text = DT.Rows(0).Item("DISPLAY_NAME_TH").ToString()
                txtDescription_TH.Text = DT.Rows(0).Item("DESCRIPTION_TH").ToString()
                PRODUCT_Logo_TH = BL.Get_Product_Picture(PRODUCT_ID, VDM_BL.UILanguage.TH)
                If DT_SPEC_TH.Rows.Count > 0 Then
                    rptCaptionList_TH.DataSource = DT_SPEC_TH
                    rptCaptionList_TH.DataBind()
                End If

                '==ENGLISH==
                txtDisplayName_EN.Text = DT.Rows(0).Item("DISPLAY_NAME_EN").ToString()
                txtDescription_EN.Text = DT.Rows(0).Item("DESCRIPTION_EN").ToString()
                'PRODUCT_Logo_EN = BL.Get_Product_Picture(PRODUCT_ID, VDM_BL.UILanguage.EN)
                If DT_SPEC_EN.Rows.Count > 0 Then
                    rptCaptionList_EN.DataSource = DT_SPEC_EN
                    rptCaptionList_EN.DataBind()
                End If

                '==CHINESE==
                txtDisplayName_CN.Text = DT.Rows(0).Item("DISPLAY_NAME_CN").ToString()
                txtDescription_CN.Text = DT.Rows(0).Item("DESCRIPTION_CN").ToString()
                'PRODUCT_Logo_CN = BL.Get_Product_Picture(PRODUCT_ID, VDM_BL.UILanguage.CN)
                If DT_SPEC_CN.Rows.Count > 0 Then
                    rptCaptionList_CN.DataSource = DT_SPEC_CN
                    rptCaptionList_CN.DataBind()
                End If

                '==JAPANESE==
                txtDisplayName_JP.Text = DT.Rows(0).Item("DISPLAY_NAME_JP").ToString()
                txtDescription_JP.Text = DT.Rows(0).Item("DESCRIPTION_JP").ToString()
                'PRODUCT_Logo_JP = BL.Get_Product_Picture(PRODUCT_ID, VDM_BL.UILanguage.JP)
                If DT_SPEC_JP.Rows.Count > 0 Then
                    rptCaptionList_JP.DataSource = DT_SPEC_JP
                    rptCaptionList_JP.DataBind()
                End If

                '==KOREAN==
                txtDisplayName_KR.Text = DT.Rows(0).Item("DISPLAY_NAME_KP").ToString()
                txtDescription_KR.Text = DT.Rows(0).Item("DESCRIPTION_KP").ToString()
                'PRODUCT_Logo_KR = BL.Get_Product_Picture(PRODUCT_ID, VDM_BL.UILanguage.KR)
                If DT_SPEC_KR.Rows.Count > 0 Then
                    rptCaptionList_KR.DataSource = DT_SPEC_KR
                    rptCaptionList_KR.DataBind()
                End If

                '==RUSSIAN==
                txtDisplayName_RS.Text = DT.Rows(0).Item("DISPLAY_NAME_RS").ToString()
                txtDescription_RS.Text = DT.Rows(0).Item("DESCRIPTION_RS").ToString()
                'PRODUCT_Logo_RS = BL.Get_Product_Picture(PRODUCT_ID, VDM_BL.UILanguage.RS)
                If DT_SPEC_RS.Rows.Count > 0 Then
                    rptCaptionList_RS.DataSource = DT_SPEC_RS
                    rptCaptionList_RS.DataBind()
                End If

                chkActive.Checked = DT.Rows(0).Item("Active_Status")

            Case "Delete"
                'MS_Product_PIC
                'MS_Product_Custom
                'MS_Product_Spec
                'MS_Product

        End Select



    End Sub

#Region "TH"
    Private Sub rptCaptionList_TH_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptCaptionList_TH.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim ddlSpec As DropDownList = e.Item.FindControl("ddlSpec")
        Dim txtCaptionDescription As TextBox = e.Item.FindControl("txtCaptionDescription")

        Dim btnDelete As Button = e.Item.FindControl("btnDelete")

        If Not IsDBNull(e.Item.DataItem("SPEC_ID")) Then
            BL.Bind_DDL_Spec(ddlSpec, VDM_BL.UILanguage.TH, e.Item.DataItem("SPEC_ID"))
        Else
            BL.Bind_DDL_Spec(ddlSpec, VDM_BL.UILanguage.TH)
        End If
        txtCaptionDescription.Text = e.Item.DataItem("DESCRIPTION_TH").ToString
        btnDelete.CommandArgument = e.Item.DataItem("PRODUCT_ID")
        Dim btnPreDelete As HtmlInputButton = e.Item.FindControl("btnPreDelete")
        btnPreDelete.Attributes("onclick") = "If(confirm('ยืนยันลบ " & " ?'))$('#" & btnDelete.ClientID & "').click();"
    End Sub
    Private Sub rptCaptionList_TH_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptCaptionList_TH.ItemCommand

    End Sub
#End Region

#Region "EN"

    Private Sub rptCaptionList_EN_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptCaptionList_EN.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim ddlSpec As DropDownList = e.Item.FindControl("ddlSpec")
        Dim txtCaptionDescription As TextBox = e.Item.FindControl("txtCaptionDescription")

        Dim btnDelete As Button = e.Item.FindControl("btnDelete")

        If Not IsDBNull(e.Item.DataItem("SPEC_ID")) Then
            BL.Bind_DDL_Spec(ddlSpec, VDM_BL.UILanguage.EN, e.Item.DataItem("SPEC_ID"))
        Else
            BL.Bind_DDL_Spec(ddlSpec, VDM_BL.UILanguage.EN)
        End If
        txtCaptionDescription.Text = e.Item.DataItem("DESCRIPTION_EN").ToString
        btnDelete.CommandArgument = e.Item.DataItem("PRODUCT_ID")
        Dim btnPreDelete As HtmlInputButton = e.Item.FindControl("btnPreDelete")
        btnPreDelete.Attributes("onclick") = "If(confirm('ยืนยันลบ " & " ?'))$('#" & btnDelete.ClientID & "').click();"
    End Sub

    Private Sub rptCaptionList_EN_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptCaptionList_EN.ItemCommand

    End Sub

#End Region



End Class