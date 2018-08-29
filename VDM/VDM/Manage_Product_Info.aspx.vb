
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
        txtDisplayName_RS.Text = ""
        txtDescription_RS.Text = ""
        rptCaptionList_RS.DataSource = Nothing
        rptCaptionList_RS.DataBind()
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
        lblProductCode.Text = ""
        lblDisplayName.Text = ""
        lblCountSpec.Text = ""
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

                '---SELECT SPEC
                Dim SQL_SPEC As String = "SELECT * FROM VW_PRODUCT_SPEC WHERE PRODUCT_ID=" & e.CommandArgument
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
                End If

                'THAI
                txtDisplayName_TH.Text = DT.Rows(0).Item("DISPLAY_NAME_TH").ToString()
                txtDescription_TH.Text = DT.Rows(0).Item("DESCRIPTION_TH").ToString()
                If DT_SPEC_TH.Rows.Count > 0 Then
                    rptCaptionList_TH.DataSource = DT_SPEC_TH
                    rptCaptionList_TH.DataBind()
                End If

                PRODUCT_Logo_TH = Nothing

                'ENGLISH
                txtDisplayName_RS.Text = ""
                txtDescription_RS.Text = ""
                rptCaptionList_RS.DataSource = Nothing
                rptCaptionList_RS.DataBind()
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



                '--Caption




            Case "Delete"
                'MS_Product_PIC
                'MS_Product_Custom
                'MS_Product_Spec
                'MS_Product


        End Select



    End Sub

    'Function Get_Product_Caption(ByVal Language As VDM_BL.UILanguage) As DataTable

    '    Dim DT As New DataTable
    '    Dim SQL As String = "SELECT * FROM MS_Product "

    '    'Select Case BL.Get_Language_Code(Language)
    '    '    Case "TH"

    '    '    Case "EN"

    '    '    Case "CN"

    '    '    Case "JP"

    '    '    Case "KR"

    '    '    Case "RS"

    '    '    Case Else
    '    'End Select

    '    Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
    '        Dim DT As New DataTable
    '        DA.Fill(DT)


    '        Return DT
    'End Function


End Class