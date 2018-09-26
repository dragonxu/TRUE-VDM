
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

    Protected Property Last_Tab As VDM_BL.UILanguage
        Get
            Return Val(txtCode.Attributes("Last_Tab"))
        End Get
        Set(value As VDM_BL.UILanguage)
            txtCode.Attributes("Last_Tab") = value
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
            imgIcon_TH.ImageUrl = "RenderImage.aspx?Mode=S&UID=PRODUCT_Logo_TH&LANG=" & VDM_BL.UILanguage.TH & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        End Set
    End Property

    Private Property PRODUCT_Logo_EN As Byte()
        Get
            Try
                Return Session("PRODUCT_Logo_EN")
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
        Set(value As Byte())
            Session("PRODUCT_Logo_EN") = value
            imgIcon_EN.ImageUrl = "RenderImage.aspx?Mode=S&UID=PRODUCT_Logo_EN&LANG=" & VDM_BL.UILanguage.EN & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        End Set
    End Property

    Private Property PRODUCT_Logo_CH As Byte()
        Get
            Try
                Return Session("PRODUCT_Logo_CH")
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
        Set(value As Byte())
            Session("PRODUCT_Logo_CH") = value
            imgIcon_CH.ImageUrl = "RenderImage.aspx?Mode=S&UID=PRODUCT_Logo_CH&LANG=" & VDM_BL.UILanguage.CN & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        End Set
    End Property

    Private Property PRODUCT_Logo_JP As Byte()
        Get
            Try
                Return Session("PRODUCT_Logo_JP")
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
        Set(value As Byte())
            Session("PRODUCT_Logo_JP") = value
            imgIcon_JP.ImageUrl = "RenderImage.aspx?Mode=S&UID=PRODUCT_Logo_JP&LANG=" & VDM_BL.UILanguage.JP & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        End Set
    End Property

    Private Property PRODUCT_Logo_KR As Byte()
        Get
            Try
                Return Session("PRODUCT_Logo_KR")
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
        Set(value As Byte())
            Session("PRODUCT_Logo_KR") = value
            imgIcon_KR.ImageUrl = "RenderImage.aspx?Mode=S&UID=PRODUCT_Logo_KR&LANG=" & VDM_BL.UILanguage.KR & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        End Set
    End Property

    Private Property PRODUCT_Logo_RS As Byte()
        Get
            Try
                Return Session("PRODUCT_Logo_RS")
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
        Set(value As Byte())
            Session("PRODUCT_Logo_RS") = value
            imgIcon_RS.ImageUrl = "RenderImage.aspx?Mode=S&UID=PRODUCT_Logo_RS&LANG=" & VDM_BL.UILanguage.RS & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        End Set
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNumeric(Session("USER_ID")) Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('กรุณาเข้าสู่ระบบ'); window.location.href='SignIn.aspx';", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            ResetPage(Nothing, Nothing)
            initFirstTimeJavascript()
        Else
            initFormPlugin()
        End If

    End Sub

#Region "Tab"
    Private Sub ClearTab()
        Tab_THAI.Attributes("class") = ""
        Tab_ENGLISH.Attributes("class") = ""
        Tab_CHINESE.Attributes("class") = ""
        Tab_JAPANESE.Attributes("class") = ""
        Tab_KOREAN.Attributes("class") = ""
        Tab_RUSSIAN.Attributes("class") = ""

        pnlTHAI.Visible = False
        pnlENGLISH.Visible = False
        pnlCHINESE.Visible = False
        pnlJAPANESE.Visible = False
        pnlKOREAN.Visible = False
        pnlRUSSIAN.Visible = False
    End Sub

    Private Sub lnkTab_THAI_Click(sender As Object, e As EventArgs) Handles lnkTab_THAI.Click
        ClearTab()
        Tab_THAI.Attributes("class") = "active"
        pnlTHAI.Visible = True
        Select Case Last_Tab
            Case VDM_BL.UILanguage.TH
                UC_Product_Spec_TH.BindList(UC_Product_Spec_TH.Current_Data, VDM_BL.UILanguage.TH)
            Case VDM_BL.UILanguage.EN
                UC_Product_Spec_TH.BindList(UC_Product_Spec_EN.Current_Data, VDM_BL.UILanguage.TH)
            Case VDM_BL.UILanguage.CN
                UC_Product_Spec_TH.BindList(UC_Product_Spec_CH.Current_Data, VDM_BL.UILanguage.TH)
            Case VDM_BL.UILanguage.JP
                UC_Product_Spec_TH.BindList(UC_Product_Spec_JP.Current_Data, VDM_BL.UILanguage.TH)
            Case VDM_BL.UILanguage.KR
                UC_Product_Spec_TH.BindList(UC_Product_Spec_KR.Current_Data, VDM_BL.UILanguage.TH)
            Case VDM_BL.UILanguage.RS
                UC_Product_Spec_TH.BindList(UC_Product_Spec_RS.Current_Data, VDM_BL.UILanguage.TH)
        End Select

        Last_Tab = VDM_BL.UILanguage.TH



    End Sub

    Private Sub lnkTab_ENGLISH_Click(sender As Object, e As EventArgs) Handles lnkTab_ENGLISH.Click
        ClearTab()
        Tab_ENGLISH.Attributes("class") = "active"
        pnlENGLISH.Visible = True
        Select Case Last_Tab
            Case VDM_BL.UILanguage.TH
                UC_Product_Spec_EN.BindList(UC_Product_Spec_TH.Current_Data, VDM_BL.UILanguage.EN)
            Case VDM_BL.UILanguage.EN
                UC_Product_Spec_EN.BindList(UC_Product_Spec_EN.Current_Data, VDM_BL.UILanguage.EN)
            Case VDM_BL.UILanguage.CN
                UC_Product_Spec_EN.BindList(UC_Product_Spec_CH.Current_Data, VDM_BL.UILanguage.EN)
            Case VDM_BL.UILanguage.JP
                UC_Product_Spec_EN.BindList(UC_Product_Spec_JP.Current_Data, VDM_BL.UILanguage.EN)
            Case VDM_BL.UILanguage.KR
                UC_Product_Spec_EN.BindList(UC_Product_Spec_KR.Current_Data, VDM_BL.UILanguage.EN)
            Case VDM_BL.UILanguage.RS
                UC_Product_Spec_EN.BindList(UC_Product_Spec_RS.Current_Data, VDM_BL.UILanguage.EN)
        End Select

        Last_Tab = VDM_BL.UILanguage.EN

    End Sub

    Private Sub lnkTab_CHINESE_Click(sender As Object, e As EventArgs) Handles lnkTab_CHINESE.Click
        ClearTab()
        Tab_CHINESE.Attributes("class") = "active"
        pnlCHINESE.Visible = True
        Select Case Last_Tab
            Case VDM_BL.UILanguage.TH
                UC_Product_Spec_CH.BindList(UC_Product_Spec_TH.Current_Data, VDM_BL.UILanguage.CN)
            Case VDM_BL.UILanguage.EN
                UC_Product_Spec_CH.BindList(UC_Product_Spec_EN.Current_Data, VDM_BL.UILanguage.CN)
            Case VDM_BL.UILanguage.CN
                UC_Product_Spec_CH.BindList(UC_Product_Spec_CH.Current_Data, VDM_BL.UILanguage.CN)
            Case VDM_BL.UILanguage.JP
                UC_Product_Spec_CH.BindList(UC_Product_Spec_JP.Current_Data, VDM_BL.UILanguage.CN)
            Case VDM_BL.UILanguage.KR
                UC_Product_Spec_CH.BindList(UC_Product_Spec_KR.Current_Data, VDM_BL.UILanguage.CN)
            Case VDM_BL.UILanguage.RS
                UC_Product_Spec_CH.BindList(UC_Product_Spec_RS.Current_Data, VDM_BL.UILanguage.CN)
        End Select
        Last_Tab = VDM_BL.UILanguage.CN
    End Sub

    Private Sub lnkTab_JAPANESE_Click(sender As Object, e As EventArgs) Handles lnkTab_JAPANESE.Click
        ClearTab()
        Tab_JAPANESE.Attributes("class") = "active"
        pnlJAPANESE.Visible = True
        Select Case Last_Tab
            Case VDM_BL.UILanguage.TH
                UC_Product_Spec_JP.BindList(UC_Product_Spec_TH.Current_Data, VDM_BL.UILanguage.JP)
            Case VDM_BL.UILanguage.EN
                UC_Product_Spec_JP.BindList(UC_Product_Spec_EN.Current_Data, VDM_BL.UILanguage.JP)
            Case VDM_BL.UILanguage.CN
                UC_Product_Spec_JP.BindList(UC_Product_Spec_CH.Current_Data, VDM_BL.UILanguage.JP)
            Case VDM_BL.UILanguage.JP
                UC_Product_Spec_JP.BindList(UC_Product_Spec_JP.Current_Data, VDM_BL.UILanguage.JP)
            Case VDM_BL.UILanguage.KR
                UC_Product_Spec_JP.BindList(UC_Product_Spec_KR.Current_Data, VDM_BL.UILanguage.JP)
            Case VDM_BL.UILanguage.RS
                UC_Product_Spec_JP.BindList(UC_Product_Spec_RS.Current_Data, VDM_BL.UILanguage.JP)
        End Select
        Last_Tab = VDM_BL.UILanguage.JP
    End Sub

    Private Sub lnkTab_KOREAN_Click(sender As Object, e As EventArgs) Handles lnkTab_KOREAN.Click
        ClearTab()
        Tab_KOREAN.Attributes("class") = "active"
        pnlKOREAN.Visible = True
        Select Case Last_Tab
            Case VDM_BL.UILanguage.TH
                UC_Product_Spec_KR.BindList(UC_Product_Spec_TH.Current_Data, VDM_BL.UILanguage.KR)
            Case VDM_BL.UILanguage.EN
                UC_Product_Spec_KR.BindList(UC_Product_Spec_EN.Current_Data, VDM_BL.UILanguage.KR)
            Case VDM_BL.UILanguage.CN
                UC_Product_Spec_KR.BindList(UC_Product_Spec_CH.Current_Data, VDM_BL.UILanguage.KR)
            Case VDM_BL.UILanguage.JP
                UC_Product_Spec_KR.BindList(UC_Product_Spec_JP.Current_Data, VDM_BL.UILanguage.KR)
            Case VDM_BL.UILanguage.KR
                UC_Product_Spec_KR.BindList(UC_Product_Spec_KR.Current_Data, VDM_BL.UILanguage.KR)
            Case VDM_BL.UILanguage.RS
                UC_Product_Spec_KR.BindList(UC_Product_Spec_RS.Current_Data, VDM_BL.UILanguage.KR)
        End Select
        Last_Tab = VDM_BL.UILanguage.KR
    End Sub

    Private Sub lnkTab_RUSSIAN_Click(sender As Object, e As EventArgs) Handles lnkTab_RUSSIAN.Click
        ClearTab()
        Tab_RUSSIAN.Attributes("class") = "active"
        pnlRUSSIAN.Visible = True

        Select Case Last_Tab
            Case VDM_BL.UILanguage.TH
                UC_Product_Spec_RS.BindList(UC_Product_Spec_TH.Current_Data, VDM_BL.UILanguage.RS)
            Case VDM_BL.UILanguage.EN
                UC_Product_Spec_RS.BindList(UC_Product_Spec_EN.Current_Data, VDM_BL.UILanguage.RS)
            Case VDM_BL.UILanguage.CN
                UC_Product_Spec_RS.BindList(UC_Product_Spec_CH.Current_Data, VDM_BL.UILanguage.RS)
            Case VDM_BL.UILanguage.JP
                UC_Product_Spec_RS.BindList(UC_Product_Spec_JP.Current_Data, VDM_BL.UILanguage.RS)
            Case VDM_BL.UILanguage.KR
                UC_Product_Spec_RS.BindList(UC_Product_Spec_KR.Current_Data, VDM_BL.UILanguage.RS)
            Case VDM_BL.UILanguage.RS
                UC_Product_Spec_RS.BindList(UC_Product_Spec_RS.Current_Data, VDM_BL.UILanguage.RS)
        End Select
        Last_Tab = VDM_BL.UILanguage.RS
    End Sub
#End Region

    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Private Sub initFirstTimeJavascript()
        'THAI
        imgIcon_TH.Attributes("onclick") = "window.open(this.src);"
        ful_TH.Attributes("onchange") = "$('#" & btnUpdateLogo.ClientID & "').click();"
        'ENGLISH
        imgIcon_EN.Attributes("onclick") = "window.open(this.src);"
        ful_EN.Attributes("onchange") = "$('#" & btnUpdateLogo.ClientID & "').click();"
        'CHINESE
        imgIcon_CH.Attributes("onclick") = "window.open(this.src);"
        ful_CH.Attributes("onchange") = "$('#" & btnUpdateLogo.ClientID & "').click();"
        'JAPANESE
        imgIcon_JP.Attributes("onclick") = "window.open(this.src);"
        ful_JP.Attributes("onchange") = "$('#" & btnUpdateLogo.ClientID & "').click();"
        'KOREAN
        imgIcon_KR.Attributes("onclick") = "window.open(this.src);"
        ful_KR.Attributes("onchange") = "$('#" & btnUpdateLogo.ClientID & "').click();"
        'RUSSIAN
        imgIcon_RS.Attributes("onclick") = "window.open(this.src);"
        ful_RS.Attributes("onchange") = "$('#" & btnUpdateLogo.ClientID & "').click();"

    End Sub

    Protected Sub ResetPage(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        BindList()
        ClearEditForm()
        pnlList.Visible = True
        pnlEdit.Visible = False
    End Sub

    Private Sub ClearEditForm()
        PRODUCT_ID = 0
        ModuleGlobal.ImplementJavaIntegerText(txtCode, False, Nothing, "Left")
        txtCode.Text = ""
        rdIsSerial_Yes.Checked = False
        rdIsSerial_No.Checked = False
        BL.Bind_DDL_Brand(ddlBrand)
        ddlBrand.SelectedIndex = 0
        txtModel.Text = ""
        rdRequireReceive_Yes.Checked = False
        rdRequireReceive_No.Checked = False
        ModuleGlobal.ImplementJavaMoneyText(txtPrice)
        Tab_THAI.Attributes("class") = "active"

        'THAI
        txtDisplayName_TH.Text = ""
        txtDescription_TH.Text = ""
        PRODUCT_Logo_TH = Nothing

        'ENGLISH
        txtDisplayName_EN.Text = ""
        txtDescription_EN.Text = ""
        PRODUCT_Logo_EN = Nothing

        'CHINESE
        txtDisplayName_CH.Text = ""
        txtDESCRIPTION_CN.Text = ""
        PRODUCT_Logo_CH = Nothing

        'JAPANESE
        txtDisplayName_JP.Text = ""
        txtDescription_JP.Text = ""
        PRODUCT_Logo_JP = Nothing

        'KOREAN
        txtDisplayName_KR.Text = ""
        txtDescription_KR.Text = ""
        PRODUCT_Logo_KR = Nothing

        'RUSSIAN
        txtDisplayName_RS.Text = ""
        txtDescription_RS.Text = ""
        PRODUCT_Logo_RS = Nothing

        '---SPEC
        Dim SQL_SPEC As String = "SELECT * FROM VW_PRODUCT_SPEC WHERE 0=1 "
        Dim DA_SPEC As New SqlDataAdapter(SQL_SPEC, BL.ConnectionString)
        Dim DT_SPEC As New DataTable
        DA_SPEC.Fill(DT_SPEC)
        UC_Product_Spec_TH.BindList(DT_SPEC, VDM_BL.UILanguage.TH)
        UC_Product_Spec_EN.BindList(DT_SPEC, VDM_BL.UILanguage.EN)
        UC_Product_Spec_CH.BindList(DT_SPEC, VDM_BL.UILanguage.CN)
        UC_Product_Spec_JP.BindList(DT_SPEC, VDM_BL.UILanguage.JP)
        UC_Product_Spec_KR.BindList(DT_SPEC, VDM_BL.UILanguage.KR)
        UC_Product_Spec_RS.BindList(DT_SPEC, VDM_BL.UILanguage.RS)

        chkActive.Checked = True

        ClearTab()
        Tab_THAI.Attributes("class") = "active"
        pnlTHAI.Visible = True
    End Sub

    Private Sub BindList()
        Dim SQL As String = "SELECT * FROM VW_ALL_PRODUCT ORDER BY PRODUCT_CODE "
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        'rptList.DataSource = DT
        'rptList.DataBind()

        lblTotalList.Text = FormatNumber(DT.Rows.Count, 0)

        pnlList.Visible = True
        pnlEdit.Visible = False

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

        img.ImageUrl = "RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & e.Item.DataItem("PRODUCT_ID") & "&LANG=" & VDM_BL.UILanguage.TH & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        lblProductCode.Text = e.Item.DataItem("PRODUCT_CODE").ToString
        lblDisplayName.Text = e.Item.DataItem("DISPLAY_NAME_TH").ToString
        imgBrand.ImageUrl = "RenderImage.aspx?Mode=D&Entity=Brand&UID=" & e.Item.DataItem("BRAND_ID")
        lblModel.Text = e.Item.DataItem("MODEL").ToString
        If Not IsDBNull(e.Item.DataItem("Price")) Then
            lblPrice.Text = FormatNumber(e.Item.DataItem("Price"), 2)
        End If
        'ImageActive

        btnEdit.CommandArgument = e.Item.DataItem("PRODUCT_ID")

        Dim btnDelete As Button = e.Item.FindControl("btnDelete")
        btnDelete.CommandArgument = e.Item.DataItem("PRODUCT_ID")

        Dim btnPreDelete As HtmlInputButton = e.Item.FindControl("btnPreDelete")
        btnPreDelete.Attributes("onclick") = "if(confirm('ยืนยันลบ ?'))$('#" & btnDelete.ClientID & "').click();"
        'btnPreDelete.Attributes("onclick") = "If(confirm('ยืนยันลบ ?'))$('#" & btnDelete.ClientID & "').click();"

        Dim chkAvailable As CheckBox = e.Item.FindControl("chkAvailable")
        chkAvailable.Checked = e.Item.DataItem("Active_Status")

    End Sub

    Private Sub rptList_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptList.ItemCommand
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Select Case e.CommandName
            Case "Edit"
                '--ดึงข้อมูล Product
                Dim SQL As String = "SELECT * FROM VW_ALL_PRODUCT WHERE PRODUCT_ID=" & e.CommandArgument
                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                Dim DT As New DataTable
                DA.Fill(DT)
                If DT.Rows.Count = 0 Then
                    Alert(Me.Page, "ไม่พบข้อมูล")
                    BindList()
                    Exit Sub
                End If

                ClearEditForm()
                pnlList.Visible = False
                pnlEdit.Visible = True
                PRODUCT_ID = e.CommandArgument
                lblEditMode.Text = "Edit"

                '--Detail
                txtCode.Text = DT.Rows(0).Item("PRODUCT_CODE").ToString
                txtModel.Text = DT.Rows(0).Item("MODEL").ToString

                If DT.Rows(0).Item("IS_SERIAL") Then
                    rdIsSerial_Yes.Checked = True
                Else
                    rdIsSerial_No.Checked = False
                End If

                If Not IsDBNull(DT.Rows(0).Item("BRAND_ID")) Then
                    BL.Bind_DDL_Brand(ddlBrand, DT.Rows(0).Item("BRAND_ID"))
                End If
                'If DT.Rows(0).Item("REQUIRE_RECEIVE_FORM") Then
                '    rdRequireReceive_Yes.Checked = True
                'Else
                '    rdRequireReceive_No.Checked = False
                'End If

                ModuleGlobal.ImplementJavaMoneyText(txtPrice)
                If Not IsDBNull(DT.Rows(0).Item("PRICE")) Then
                    txtPrice.Text = FormatNumber(DT.Rows(0).Item("PRICE"), 2)
                End If

                '---SPEC
                Dim SQL_SPEC As String = "SELECT * FROM VW_PRODUCT_SPEC WHERE PRODUCT_ID=" & e.CommandArgument & "  AND SPEC_ID IS NOT NULL ORDER BY SEQ "
                Dim DA_SPEC As New SqlDataAdapter(SQL_SPEC, BL.ConnectionString)
                Dim DT_SPEC As New DataTable
                DA_SPEC.Fill(DT_SPEC)

                '==THAI==
                txtDisplayName_TH.Text = DT.Rows(0).Item("DISPLAY_NAME_TH").ToString()
                txtDescription_TH.Text = DT.Rows(0).Item("DESCRIPTION_TH").ToString()
                PRODUCT_Logo_TH = BL.Get_Product_Picture(PRODUCT_ID, VDM_BL.UILanguage.TH)


                UC_Product_Spec_TH.BindList(DT_SPEC, VDM_BL.UILanguage.TH)
                Last_Tab = VDM_BL.UILanguage.TH

                '==ENGLISH==
                txtDisplayName_EN.Text = DT.Rows(0).Item("DISPLAY_NAME_EN").ToString()
                txtDescription_EN.Text = DT.Rows(0).Item("DESCRIPTION_EN").ToString()
                PRODUCT_Logo_EN = BL.Get_Product_Picture(PRODUCT_ID, VDM_BL.UILanguage.EN)
                UC_Product_Spec_EN.BindList(DT_SPEC, VDM_BL.UILanguage.EN)


                '==CHINESE==
                txtDisplayName_CH.Text = DT.Rows(0).Item("DISPLAY_NAME_CN").ToString()
                txtDESCRIPTION_CN.Text = DT.Rows(0).Item("DESCRIPTION_CN").ToString()
                PRODUCT_Logo_CH = BL.Get_Product_Picture(PRODUCT_ID, VDM_BL.UILanguage.CN)
                UC_Product_Spec_CH.BindList(DT_SPEC, VDM_BL.UILanguage.CN)


                '==JAPANESE==
                txtDisplayName_JP.Text = DT.Rows(0).Item("DISPLAY_NAME_JP").ToString()
                txtDescription_JP.Text = DT.Rows(0).Item("DESCRIPTION_JP").ToString()
                PRODUCT_Logo_JP = BL.Get_Product_Picture(PRODUCT_ID, VDM_BL.UILanguage.JP)
                UC_Product_Spec_JP.BindList(DT_SPEC, VDM_BL.UILanguage.JP)


                '==KOREAN==
                txtDisplayName_KR.Text = DT.Rows(0).Item("DISPLAY_NAME_KR").ToString()
                txtDescription_KR.Text = DT.Rows(0).Item("DESCRIPTION_KR").ToString()
                PRODUCT_Logo_KR = BL.Get_Product_Picture(PRODUCT_ID, VDM_BL.UILanguage.KR)
                UC_Product_Spec_KR.BindList(DT_SPEC, VDM_BL.UILanguage.KR)


                '==RUSSIAN==
                txtDisplayName_RS.Text = DT.Rows(0).Item("DISPLAY_NAME_RS").ToString()
                txtDescription_RS.Text = DT.Rows(0).Item("DESCRIPTION_RS").ToString()
                PRODUCT_Logo_RS = BL.Get_Product_Picture(PRODUCT_ID, VDM_BL.UILanguage.RS)
                UC_Product_Spec_RS.BindList(DT_SPEC, VDM_BL.UILanguage.RS)


                chkActive.Checked = DT.Rows(0).Item("Active_Status")

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



#Region "Upload_Logo"

    Private Sub btnUpdateLogo_Click(sender As Object, e As EventArgs) Handles btnUpdateLogo.Click
        Try
            Dim C As New Converter
            Dim B As Byte()
            Select Case Last_Tab
                Case VDM_BL.UILanguage.TH
                    B = C.StreamToByte(ful_TH.FileContent)
                Case VDM_BL.UILanguage.EN
                    B = C.StreamToByte(ful_EN.FileContent)
                Case VDM_BL.UILanguage.CN
                    B = C.StreamToByte(ful_CH.FileContent)
                Case VDM_BL.UILanguage.JP
                    B = C.StreamToByte(ful_JP.FileContent)
                Case VDM_BL.UILanguage.KR
                    B = C.StreamToByte(ful_KR.FileContent)
                Case VDM_BL.UILanguage.RS
                    B = C.StreamToByte(ful_RS.FileContent)
            End Select
            Dim img As System.Drawing.Image = System.Drawing.Image.FromStream(C.ByteToStream(B))

            Select Case Last_Tab
                Case VDM_BL.UILanguage.TH
                    PRODUCT_Logo_TH = B
                Case VDM_BL.UILanguage.EN
                    PRODUCT_Logo_EN = B
                Case VDM_BL.UILanguage.CN
                    PRODUCT_Logo_CH = B
                Case VDM_BL.UILanguage.JP
                    PRODUCT_Logo_JP = B
                Case VDM_BL.UILanguage.KR
                    PRODUCT_Logo_KR = B
                Case VDM_BL.UILanguage.RS
                    PRODUCT_Logo_RS = B
            End Select
        Catch ex As Exception
            Alert(Me.Page, "Support only image jpeg gif png\nAnd file size must not larger than 4MB")
            Exit Sub
        End Try
    End Sub

#End Region

#Region "ADDNewProduct"

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ClearEditForm()
        lblEditMode.Text = "Add new"
        '-----------------------------------
        pnlList.Visible = False
        pnlEdit.Visible = True

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtCode.Text = "" Then
            Alert(Me.Page, "กรอก Code")
            Exit Sub
        End If

        If rdIsSerial_No.Checked = False And rdIsSerial_Yes.Checked = False Then
            Alert(Me.Page, "เลือก Is serial")
            Exit Sub
        End If

        If ddlBrand.SelectedIndex = 0 Then
            Alert(Me.Page, "เลือก Brand")
            Exit Sub
        End If
        If txtModel.Text = "" Then
            Alert(Me.Page, "กรอก Model")
            Exit Sub
        End If
        'If rdRequireReceive_No.Checked = False And rdRequireReceive_Yes.Checked = False Then
        '    Alert(Me.Page, "เลือก Require Receive Form")
        '    Exit Sub
        'End If

        Try
            If Convert.ToDecimal(txtPrice.Text.Trim) <= 0 Then
                Alert(Me.Page, "กรอก Price")
                Exit Sub
            End If
        Catch ex As Exception
            Alert(Me.Page, "กรอก Price")
            Exit Sub
        End Try

        'Validate เฉพาะ TH
        'TH
        Dim Alert_Language As String = ""

        Alert_Language = "Specification THAI : "
        If txtDisplayName_TH.Text = "" Then
            Alert(Me.Page, Alert_Language & "กรอก Display Name")
            Exit Sub
        End If

        If txtDescription_TH.Text = "" Then
            Alert(Me.Page, Alert_Language & "กรอก Description")
            Exit Sub
        End If

        If IsNothing(PRODUCT_Logo_TH) Then
            Alert(Me.Page, Alert_Language & "เลือก Logo ")
            Exit Sub
        End If

        '-- Check Caption 

        Dim DT_Spec As New DataTable
        Select Case Last_Tab
            Case VDM_BL.UILanguage.TH
                DT_Spec = UC_Product_Spec_TH.Current_Data()
            Case VDM_BL.UILanguage.EN
                DT_Spec = UC_Product_Spec_EN.Current_Data()
            Case VDM_BL.UILanguage.CN
                DT_Spec = UC_Product_Spec_CH.Current_Data()
            Case VDM_BL.UILanguage.JP
                DT_Spec = UC_Product_Spec_JP.Current_Data()
            Case VDM_BL.UILanguage.KR
                DT_Spec = UC_Product_Spec_KR.Current_Data()
            Case VDM_BL.UILanguage.RS
                DT_Spec = UC_Product_Spec_RS.Current_Data()
        End Select

        DT_Spec.DefaultView.RowFilter = "SPEC_ID<=0"
        If DT_Spec.DefaultView.Count > 0 Then
            Alert(Me.Page, "" & "เลือก Spec ให้ครบ ")
            Exit Sub
        End If

        DT_Spec.DefaultView.RowFilter = "DESCRIPTION_TH ='' OR DESCRIPTION_TH IS NULL "
        If DT_Spec.DefaultView.Count > 0 Then
            Alert(Me.Page, "SPEC " & "กรอก Descript ภาษาไทย ให้ครบ ")
            Exit Sub
        End If

        If DT_Spec.Rows.Count > 0 Then
            For i As Integer = 0 To DT_Spec.Rows.Count - 1
                DT_Spec.DefaultView.RowFilter = "SPEC_ID=" & DT_Spec.Rows(i).Item("SPEC_ID")
                If DT_Spec.DefaultView.Count > 1 Then
                    Alert(Me.Page, "Spec ซ้ำ ")
                    Exit Sub
                End If
            Next
        End If

        Dim SQL As String = "SELECT * FROM MS_Product WHERE PRODUCT_CODE='" & txtCode.Text.Replace("'", "''") & "' AND PRODUCT_ID<>" & PRODUCT_ID
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            Alert(Me.Page, "Code ซ้ำ")
            Exit Sub
        End If

        SQL = "SELECT * FROM MS_Product WHERE DISPLAY_NAME_TH='" & txtDisplayName_TH.Text.Replace("'", "''") & "' AND PRODUCT_ID<>" & PRODUCT_ID
        DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        DT = New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            Alert(Me.Page, "Display Name Thai ซ้ำ")
            Exit Sub
        End If


        'Product
        SQL = "SELECT * FROM MS_Product WHERE PRODUCT_ID=" & PRODUCT_ID
        DT = New DataTable
        DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        Dim DR As DataRow
        If DT.Rows.Count = 0 Then
            DR = DT.NewRow
            DT.Rows.Add(DR)
            PRODUCT_ID = GetNewID()
            DR("PRODUCT_ID") = PRODUCT_ID
        Else
            DR = DT.Rows(0)
        End If
        DR("PRODUCT_CODE") = txtCode.Text
        DR("BRAND_ID") = ddlBrand.SelectedValue
        DR("MODEL") = txtModel.Text
        DR("DISPLAY_NAME_TH") = IIf(txtDisplayName_TH.Text <> "", txtDisplayName_TH.Text, DBNull.Value)
        DR("DISPLAY_NAME_EN") = IIf(txtDisplayName_EN.Text <> "", txtDisplayName_EN.Text, DBNull.Value)
        DR("DISPLAY_NAME_CN") = IIf(txtDisplayName_CH.Text <> "", txtDisplayName_CH.Text, DBNull.Value)
        DR("DISPLAY_NAME_JP") = IIf(txtDisplayName_JP.Text <> "", txtDisplayName_JP.Text, DBNull.Value)
        DR("DISPLAY_NAME_KR") = IIf(txtDisplayName_KR.Text <> "", txtDisplayName_KR.Text, DBNull.Value)
        DR("DISPLAY_NAME_RS") = IIf(txtDisplayName_RS.Text <> "", txtDisplayName_RS.Text, DBNull.Value)
        DR("DESCRIPTION_TH") = IIf(txtDescription_TH.Text <> "", txtDescription_TH.Text, DBNull.Value)
        DR("DESCRIPTION_EN") = IIf(txtDescription_EN.Text <> "", txtDescription_EN.Text, DBNull.Value)
        DR("DESCRIPTION_CN") = IIf(txtDESCRIPTION_CN.Text <> "", txtDESCRIPTION_CN.Text, DBNull.Value)
        DR("DESCRIPTION_JP") = IIf(txtDescription_JP.Text <> "", txtDescription_JP.Text, DBNull.Value)
        DR("DESCRIPTION_KR") = IIf(txtDescription_KR.Text <> "", txtDescription_KR.Text, DBNull.Value)
        DR("DESCRIPTION_RS") = IIf(txtDescription_RS.Text <> "", txtDescription_RS.Text, DBNull.Value)
        DR("IS_SERIAL") = IIf(rdIsSerial_Yes.Checked, True, False)
        DR("PRICE") = txtPrice.Text.Replace(",", "")
        DR("Active_Status") = chkActive.Checked
        DR("Update_By") = Session("USER_ID")
        DR("Update_Time") = Now

        Dim cmd As New SqlCommandBuilder(DA)
        Try
            DA.Update(DT)
            BL.Save_Product_Picture(PRODUCT_ID, VDM_BL.UILanguage.TH, PRODUCT_Logo_TH)
            If Not IsNothing(PRODUCT_Logo_EN) Then
                BL.Save_Product_Picture(PRODUCT_ID, VDM_BL.UILanguage.EN, PRODUCT_Logo_EN)
            End If
            If Not IsNothing(PRODUCT_Logo_CH) Then
                BL.Save_Product_Picture(PRODUCT_ID, VDM_BL.UILanguage.CN, PRODUCT_Logo_CH)
            End If
            If Not IsNothing(PRODUCT_Logo_JP) Then
                BL.Save_Product_Picture(PRODUCT_ID, VDM_BL.UILanguage.JP, PRODUCT_Logo_JP)
            End If
            If Not IsNothing(PRODUCT_Logo_KR) Then
                BL.Save_Product_Picture(PRODUCT_ID, VDM_BL.UILanguage.KR, PRODUCT_Logo_KR)
            End If
            If Not IsNothing(PRODUCT_Logo_RS) Then
                BL.Save_Product_Picture(PRODUCT_ID, VDM_BL.UILanguage.RS, PRODUCT_Logo_RS)
            End If

        Catch ex As Exception
            Alert(Me.Page, ex.Message)
            Exit Sub
        End Try

        ' Save Caption 
        If DT_Spec.Rows.Count > 0 Then
            SQL = " DELETE FROM MS_Product_Spec " & vbNewLine
            SQL &= " WHERE PRODUCT_ID=" & PRODUCT_ID
            BL.ExecuteNonQuery(SQL)
            SQL = " SELECT * FROM MS_Product_Spec WHERE 0=1 "
            DT = New DataTable
            DA = New SqlDataAdapter(SQL, BL.ConnectionString)
            DA.Fill(DT)
            If DT.Rows.Count = 0 Then
                For i As Integer = 0 To DT_Spec.Rows.Count - 1
                    DR = DT.NewRow
                    DT.Rows.Add(DR)
                    DR("PRODUCT_ID") = PRODUCT_ID
                    DR("SEQ") = i + 1
                    DR("SPEC_ID") = DT_Spec.Rows(i).Item("SPEC_ID")
                    DR("DESCRIPTION_TH") = IIf(DT_Spec.Rows(i).Item("DESCRIPTION_TH").ToString.Trim <> "", DT_Spec.Rows(i).Item("DESCRIPTION_TH").ToString.Trim, DBNull.Value)
                    DR("DESCRIPTION_EN") = IIf(DT_Spec.Rows(i).Item("DESCRIPTION_EN").ToString.Trim <> "", DT_Spec.Rows(i).Item("DESCRIPTION_EN").ToString.Trim, DBNull.Value)
                    DR("DESCRIPTION_CN") = IIf(DT_Spec.Rows(i).Item("DESCRIPTION_CN").ToString.Trim <> "", DT_Spec.Rows(i).Item("DESCRIPTION_CN").ToString.Trim, DBNull.Value)
                    DR("DESCRIPTION_JP") = IIf(DT_Spec.Rows(i).Item("DESCRIPTION_JP").ToString.Trim <> "", DT_Spec.Rows(i).Item("DESCRIPTION_JP").ToString.Trim, DBNull.Value)
                    DR("DESCRIPTION_KR") = IIf(DT_Spec.Rows(i).Item("DESCRIPTION_KR").ToString.Trim <> "", DT_Spec.Rows(i).Item("DESCRIPTION_KR").ToString.Trim, DBNull.Value)
                    DR("DESCRIPTION_RS") = IIf(DT_Spec.Rows(i).Item("DESCRIPTION_RS").ToString.Trim <> "", DT_Spec.Rows(i).Item("DESCRIPTION_RS").ToString.Trim, DBNull.Value)
                Next
                cmd = New SqlCommandBuilder(DA)
                Try
                    DA.Update(DT)
                Catch ex As Exception
                    Alert(Me.Page, ex.Message)
                    Exit Sub
                End Try
            End If
        End If
        Alert(Me.Page, "บันทึกสำเร็จ")
        ResetPage(Nothing, Nothing)

    End Sub


    Private Function GetNewID() As Integer
        Dim SQL As String = "SELECT IsNull(MAX(PRODUCT_ID),0)+1 FROM MS_Product "
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT.Rows(0).Item(0)
    End Function


#End Region


#Region "Get Product TSM"
    Private Sub btnGetProductTSM_Click(sender As Object, e As EventArgs) Handles btnGetProductTSM.Click
        If txtCode.Text = "" Then
            Alert(Me.Page, "กรอก Code")
            Exit Sub
        End If

        Try

            '-----ดึงข้อมูล check ข้อมูล 

            '-----แสดงข้อมูลใน form
            Dim Response As New BackEndInterface.Get_Product_Info.Response
            Response = BackEndInterface.Get_Result(txtCode.Text)
            If Not IsNothing(Response) Then

                If Response.Product.IS_SIM.ToUpper = "TRUE" Then
                    Alert(Me.Page, txtCode.Text & "Is Sim ดำเนินการหน้า Manage Sim Info ")
                    Exit Sub
                End If

                If Response.Product.IS_SERIAL.ToUpper = "TRUE" Then
                    rdIsSerial_Yes.Checked = True
                Else
                    rdIsSerial_No.Checked = False
                End If

                ModuleGlobal.ImplementJavaMoneyText(txtPrice)
                If Not IsDBNull(Response.Price) Then
                    txtPrice.Text = FormatNumber(Response.Price, 2)
                End If

                txtDisplayName_TH.Text = Response.Product.DISPLAY_NAME
                txtDescription_TH.Text = Response.Product.DESCRIPTION



                '''Captions
                'If Response.Captions.Count > 0 Then
                '    SEQ.Text = Response.Captions(0).SEQ
                '    PRODUCT_CODE.Text = Response.Captions(0).PRODUCT_CODE
                '    CAPTION_CODE.Text = Response.Captions(0).CAPTION_CODE
                '    CAPTION_DESC.Text = Response.Captions(0).CAPTION_DESC
                '    DETAIL.Text = Response.Captions(0).DETAIL
                'End If


            End If

        Catch ex As Exception
            Alert(Me.Page, "ไม่มีข้อมูล Product Code ใน TSM")
            Exit Sub
        End Try



    End Sub
#End Region


End Class