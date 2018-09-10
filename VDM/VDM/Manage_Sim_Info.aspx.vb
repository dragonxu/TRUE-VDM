
Imports System.Data
Imports System.Data.SqlClient

Public Class Manage_Sim_Info
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL
    Dim BackEndInterface As New BackEndInterface.Get_Product_Info

    Protected Property SIM_ID As Integer
        Get
            Return Val(txtCode.Attributes("SIM_ID"))
        End Get
        Set(value As Integer)
            txtCode.Attributes("SIM_ID") = value
        End Set
    End Property

    '-SIM-
#Region "SIM_PACKAGE"
    Private Property SIM_PACKAGE_Logo_TH As Byte()
        Get
            Try
                Return Session("SIM_PACKAGE_Logo_TH")
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
        Set(value As Byte())
            Session("SIM_PACKAGE_Logo_TH") = value
            imgIcon_TH.ImageUrl = "RenderImage.aspx?Mode=S&UID=SIM_PACKAGE_Logo_TH&LANG=" & VDM_BL.UILanguage.TH & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        End Set
    End Property

    Private Property SIM_PACKAGE_Logo_EN As Byte()
        Get
            Try
                Return Session("SIM_PACKAGE_Logo_EN")
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
        Set(value As Byte())
            Session("SIM_PACKAGE_Logo_EN") = value
            imgIcon_EN.ImageUrl = "RenderImage.aspx?Mode=S&UID=SIM_PACKAGE_Logo_EN&LANG=" & VDM_BL.UILanguage.EN & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        End Set
    End Property

    Private Property SIM_PACKAGE_Logo_CH As Byte()
        Get
            Try
                Return Session("SIM_PACKAGE_Logo_CH")
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
        Set(value As Byte())
            Session("SIM_PACKAGE_Logo_CH") = value
            imgIcon_CH.ImageUrl = "RenderImage.aspx?Mode=S&UID=SIM_PACKAGE_Logo_CH&LANG=" & VDM_BL.UILanguage.CN & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        End Set
    End Property

    Private Property SIM_PACKAGE_Logo_JP As Byte()
        Get
            Try
                Return Session("SIM_PACKAGE_Logo_JP")
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
        Set(value As Byte())
            Session("SIM_PACKAGE_Logo_JP") = value
            imgIcon_JP.ImageUrl = "RenderImage.aspx?Mode=S&UID=SIM_PACKAGE_Logo_JP&LANG=" & VDM_BL.UILanguage.JP & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        End Set
    End Property

    Private Property SIM_PACKAGE_Logo_KR As Byte()
        Get
            Try
                Return Session("SIM_PACKAGE_Logo_KR")
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
        Set(value As Byte())
            Session("SIM_PACKAGE_Logo_KR") = value
            imgIcon_KR.ImageUrl = "RenderImage.aspx?Mode=S&UID=SIM_PACKAGE_Logo_KR&LANG=" & VDM_BL.UILanguage.KR & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        End Set
    End Property

    Private Property SIM_PACKAGE_Logo_RS As Byte()
        Get
            Try
                Return Session("SIM_PACKAGE_Logo_RS")
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
        Set(value As Byte())
            Session("SIM_PACKAGE_Logo_RS") = value
            imgIcon_RS.ImageUrl = "RenderImage.aspx?Mode=S&UID=SIM_PACKAGE_Logo_RS&LANG=" & VDM_BL.UILanguage.RS & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        End Set
    End Property
#End Region

#Region "SIM_DETAIL"
    Private Property SIM_DETAIL_Logo_TH As Byte()
        Get
            Try
                Return Session("SIM_DETAIL_Logo_TH")
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
        Set(value As Byte())
            Session("SIM_DETAIL_Logo_TH") = value
            imgIcon_TH_Detail.ImageUrl = "RenderImage.aspx?Mode=S&UID=SIM_DETAIL_Logo_TH&LANG=" & VDM_BL.UILanguage.TH & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        End Set
    End Property

    Private Property SIM_DETAIL_Logo_EN As Byte()
        Get
            Try
                Return Session("SIM_DETAIL_Logo_EN")
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
        Set(value As Byte())
            Session("SIM_DETAIL_Logo_EN") = value
            imgIcon_EN_Detail.ImageUrl = "RenderImage.aspx?Mode=S&UID=SIM_DETAIL_Logo_EN&LANG=" & VDM_BL.UILanguage.EN & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        End Set
    End Property

    Private Property SIM_DETAIL_Logo_CH As Byte()
        Get
            Try
                Return Session("SIM_DETAIL_Logo_CH")
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
        Set(value As Byte())
            Session("SIM_DETAIL_Logo_CH") = value
            imgIcon_CH_Detail.ImageUrl = "RenderImage.aspx?Mode=S&UID=SIM_DETAIL_Logo_CH&LANG=" & VDM_BL.UILanguage.CN & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        End Set
    End Property

    Private Property SIM_DETAIL_Logo_JP As Byte()
        Get
            Try
                Return Session("SIM_DETAIL_Logo_JP")
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
        Set(value As Byte())
            Session("SIM_DETAIL_Logo_JP") = value
            imgIcon_JP_Detail.ImageUrl = "RenderImage.aspx?Mode=S&UID=SIM_DETAIL_Logo_JP&LANG=" & VDM_BL.UILanguage.JP & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        End Set
    End Property

    Private Property SIM_DETAIL_Logo_KR As Byte()
        Get
            Try
                Return Session("SIM_DETAIL_Logo_KR")
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
        Set(value As Byte())
            Session("SIM_DETAIL_Logo_KR") = value
            imgIcon_KR_Detail.ImageUrl = "RenderImage.aspx?Mode=S&UID=SIM_DETAIL_Logo_KR&LANG=" & VDM_BL.UILanguage.KR & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        End Set
    End Property

    Private Property SIM_DETAIL_Logo_RS As Byte()
        Get
            Try
                Return Session("SIM_DETAIL_Logo_RS")
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
        Set(value As Byte())
            Session("SIM_DETAIL_Logo_RS") = value
            imgIcon_RS_Detail.ImageUrl = "RenderImage.aspx?Mode=S&UID=SIM_DETAIL_Logo_RS&LANG=" & VDM_BL.UILanguage.RS & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        End Set
    End Property

#End Region

    Protected Property Current_Tab As VDM_BL.UILanguage
        Get
            Return Val(txtCode.Attributes("Current_Tab"))
        End Get
        Set(value As VDM_BL.UILanguage)
            txtCode.Attributes("Current_Tab") = value
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

    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Private Sub initFirstTimeJavascript()
#Region "SIM_PACKAGE"


        'THAI
        imgIcon_TH.Attributes("onclick") = "window.open(this.src);"
        ful_TH.Attributes("onchange") = "$('#" & btnUpdate_SimPackage_Logo.ClientID & "').click();"
        'ENGLISH
        imgIcon_EN.Attributes("onclick") = "window.open(this.src);"
        ful_EN.Attributes("onchange") = "$('#" & btnUpdate_SimPackage_Logo.ClientID & "').click();"
        'CHINESE
        imgIcon_CH.Attributes("onclick") = "window.open(this.src);"
        ful_CH.Attributes("onchange") = "$('#" & btnUpdate_SimPackage_Logo.ClientID & "').click();"
        'JAPANESE
        imgIcon_JP.Attributes("onclick") = "window.open(this.src);"
        ful_JP.Attributes("onchange") = "$('#" & btnUpdate_SimPackage_Logo.ClientID & "').click();"
        'KOREAN
        imgIcon_KR.Attributes("onclick") = "window.open(this.src);"
        ful_KR.Attributes("onchange") = "$('#" & btnUpdate_SimPackage_Logo.ClientID & "').click();"
        'RUSSIAN
        imgIcon_RS.Attributes("onclick") = "window.open(this.src);"
        ful_RS.Attributes("onchange") = "$('#" & btnUpdate_SimPackage_Logo.ClientID & "').click();"

#End Region

#Region "SIM_DETAIL"

        'THAI
        imgIcon_TH_Detail.Attributes("onclick") = "window.open(this.src);"
        ful_TH_Detail.Attributes("onchange") = "$('#" & btnUpdate_SimDetail_Logo.ClientID & "').click();"
        'ENGLISHs
        imgIcon_EN_Detail.Attributes("onclick") = "window.open(this.src);"
        ful_EN_Detail.Attributes("onchange") = "$('#" & btnUpdate_SimDetail_Logo.ClientID & "').click();"
        'CHINESE
        imgIcon_CH_Detail.Attributes("onclick") = "window.open(this.src);"
        ful_CH_Detail.Attributes("onchange") = "$('#" & btnUpdate_SimDetail_Logo.ClientID & "').click();"
        'JAPANESE
        imgIcon_JP_Detail.Attributes("onclick") = "window.open(this.src);"
        ful_JP_Detail.Attributes("onchange") = "$('#" & btnUpdate_SimDetail_Logo.ClientID & "').click();"
        'KOREAN
        imgIcon_KR_Detail.Attributes("onclick") = "window.open(this.src);"
        ful_KR_Detail.Attributes("onchange") = "$('#" & btnUpdate_SimDetail_Logo.ClientID & "').click();"
        'RUSSIAN
        imgIcon_RS_Detail.Attributes("onclick") = "window.open(this.src);"
        ful_RS_Detail.Attributes("onchange") = "$('#" & btnUpdate_SimDetail_Logo.ClientID & "').click();"

#End Region
    End Sub

    Protected Sub ResetPage(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        BindList()
        ClearEditForm()
        pnlList.Visible = True
        pnlEdit.Visible = False
    End Sub


    Private Sub BindList()
        Dim SQL As String = "SELECT * FROM MS_SIM ORDER BY PRODUCT_CODE "
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        rptList.DataSource = DT
        rptList.DataBind()

        lblTotalList.Text = FormatNumber(DT.Rows.Count, 0)

        pnlList.Visible = True
        pnlEdit.Visible = False
    End Sub

    Private Sub rptList_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptList.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim img As Image = e.Item.FindControl("img")
        Dim lblProductCode As Label = e.Item.FindControl("lblProductCode")
        Dim lblPackageName As Label = e.Item.FindControl("lblPackageName")
        Dim lblPrice As Label = e.Item.FindControl("lblPrice")
        Dim btnEdit As Button = e.Item.FindControl("btnEdit")

        img.ImageUrl = "RenderImage.aspx?Mode=D&Entity=SIM_PACKAGE&UID=" & e.Item.DataItem("SIM_ID") & "&LANG=" & VDM_BL.UILanguage.TH & "&t=" & Now.ToOADate.ToString.Replace(".", "")
        lblProductCode.Text = e.Item.DataItem("PRODUCT_CODE").ToString
        lblPackageName.Text = e.Item.DataItem("DISPLAY_NAME_TH").ToString

        If Not IsDBNull(e.Item.DataItem("Price")) Then
            lblPrice.Text = FormatNumber(e.Item.DataItem("Price"), 2)
        End If

        btnEdit.CommandArgument = e.Item.DataItem("SIM_ID")
        Dim btnDelete As Button = e.Item.FindControl("btnDelete")
        btnDelete.CommandArgument = e.Item.DataItem("SIM_ID")

        Dim btnPreDelete As HtmlInputButton = e.Item.FindControl("btnPreDelete")
        btnPreDelete.Attributes("onclick") = "if(confirm('ยืนยันลบ ?'))$('#" & btnDelete.ClientID & "').click();"

        Dim chkAvailable As CheckBox = e.Item.FindControl("chkAvailable")
        chkAvailable.Checked = e.Item.DataItem("Active_Status")
    End Sub

    Private Sub rptList_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptList.ItemCommand
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub
        Select Case e.CommandName
            Case "Edit"
                Dim SQL As String = "SELECT * FROM MS_SIM WHERE SIM_ID=" & e.CommandArgument
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
                SIM_ID = e.CommandArgument
                lblEditMode.Text = "Edit"
                '--Detail
                txtCode.Text = DT.Rows(0).Item("PRODUCT_CODE").ToString

                ModuleGlobal.ImplementJavaMoneyText(txtPrice)
                If Not IsDBNull(DT.Rows(0).Item("PRICE")) Then
                    txtPrice.Text = FormatNumber(DT.Rows(0).Item("PRICE"), 2)
                End If

                '==THAI==
                txtDisplayName_TH.Text = DT.Rows(0).Item("DISPLAY_NAME_TH").ToString()
                txtDescription_TH.Text = DT.Rows(0).Item("DESCRIPTION_TH").ToString()
                SIM_PACKAGE_Logo_TH = BL.Get_SIM_Package_Picture(SIM_ID, VDM_BL.UILanguage.TH)
                SIM_DETAIL_Logo_TH = BL.Get_SIM_Detail_Picture(SIM_ID, VDM_BL.UILanguage.TH)

                '==ENGLISH==
                txtDisplayName_EN.Text = DT.Rows(0).Item("DISPLAY_NAME_EN").ToString()
                txtDescription_EN.Text = DT.Rows(0).Item("DESCRIPTION_EN").ToString()
                SIM_PACKAGE_Logo_EN = BL.Get_SIM_Package_Picture(SIM_ID, VDM_BL.UILanguage.EN)
                SIM_DETAIL_Logo_EN = BL.Get_SIM_Detail_Picture(SIM_ID, VDM_BL.UILanguage.EN)

                '==CHINESE==
                txtDisplayName_CH.Text = DT.Rows(0).Item("DISPLAY_NAME_CH").ToString()
                txtDescription_CH.Text = DT.Rows(0).Item("DESCRIPTION_CH").ToString()
                SIM_PACKAGE_Logo_CH = BL.Get_SIM_Package_Picture(SIM_ID, VDM_BL.UILanguage.CN)
                SIM_DETAIL_Logo_CH = BL.Get_SIM_Detail_Picture(SIM_ID, VDM_BL.UILanguage.CN)

                '==JAPANESE==
                txtDisplayName_JP.Text = DT.Rows(0).Item("DISPLAY_NAME_JP").ToString()
                txtDescription_JP.Text = DT.Rows(0).Item("DESCRIPTION_JP").ToString()
                SIM_PACKAGE_Logo_JP = BL.Get_SIM_Package_Picture(SIM_ID, VDM_BL.UILanguage.JP)
                SIM_DETAIL_Logo_JP = BL.Get_SIM_Detail_Picture(SIM_ID, VDM_BL.UILanguage.JP)

                '==KOREAN==
                txtDisplayName_KR.Text = DT.Rows(0).Item("DISPLAY_NAME_KR").ToString()
                txtDescription_KR.Text = DT.Rows(0).Item("DESCRIPTION_KR").ToString()
                SIM_PACKAGE_Logo_KR = BL.Get_SIM_Package_Picture(SIM_ID, VDM_BL.UILanguage.KR)
                SIM_DETAIL_Logo_KR = BL.Get_SIM_Detail_Picture(SIM_ID, VDM_BL.UILanguage.KR)

                '==RUSSIAN==
                txtDisplayName_RS.Text = DT.Rows(0).Item("DISPLAY_NAME_RS").ToString()
                txtDescription_RS.Text = DT.Rows(0).Item("DESCRIPTION_RS").ToString()
                SIM_PACKAGE_Logo_RS = BL.Get_SIM_Package_Picture(SIM_ID, VDM_BL.UILanguage.RS)
                SIM_DETAIL_Logo_RS = BL.Get_SIM_Detail_Picture(SIM_ID, VDM_BL.UILanguage.RS)

                chkActive.Checked = DT.Rows(0).Item("Active_Status")

            Case "Delete"
                Dim SQL As String = "DELETE FROM MS_SIM" & vbNewLine
                SQL &= " WHERE SIM_ID=" & e.CommandArgument
                BL.ExecuteNonQuery(SQL)

                BindList()

        End Select




    End Sub

    Private Sub ClearEditForm()
        SIM_ID = 0
        ModuleGlobal.ImplementJavaIntegerText(txtCode, False, Nothing, "Left")
        txtCode.Text = ""
        ModuleGlobal.ImplementJavaMoneyText(txtPrice)
        Tab_THAI.Attributes("class") = "active"

        'THAI
        txtDisplayName_TH.Text = ""
        txtDescription_TH.Text = ""
        SIM_PACKAGE_Logo_TH = Nothing
        SIM_DETAIL_Logo_TH = Nothing

        'ENGLISH
        txtDisplayName_EN.Text = ""
        txtDescription_EN.Text = ""
        SIM_PACKAGE_Logo_EN = Nothing
        SIM_DETAIL_Logo_EN = Nothing

        'CHINESE
        txtDisplayName_CH.Text = ""
        txtDescription_CH.Text = ""
        SIM_PACKAGE_Logo_CH = Nothing
        SIM_DETAIL_Logo_CH = Nothing

        'JAPANESE
        txtDisplayName_JP.Text = ""
        txtDescription_JP.Text = ""
        SIM_PACKAGE_Logo_JP = Nothing
        SIM_DETAIL_Logo_JP = Nothing

        'KOREAN
        txtDisplayName_KR.Text = ""
        txtDescription_KR.Text = ""
        SIM_PACKAGE_Logo_KR = Nothing
        SIM_DETAIL_Logo_KR = Nothing

        'RUSSIAN
        txtDisplayName_RS.Text = ""
        txtDescription_RS.Text = ""
        SIM_PACKAGE_Logo_RS = Nothing
        SIM_DETAIL_Logo_RS = Nothing

        chkActive.Checked = True
        ClearTab()
        Tab_THAI.Attributes("class") = "active"
        pnlTHAI.Visible = True
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
        Current_Tab = VDM_BL.UILanguage.TH
    End Sub

    Private Sub lnkTab_ENGLISH_Click(sender As Object, e As EventArgs) Handles lnkTab_ENGLISH.Click
        ClearTab()
        Tab_ENGLISH.Attributes("class") = "active"
        pnlENGLISH.Visible = True
        Current_Tab = VDM_BL.UILanguage.EN

    End Sub

    Private Sub lnkTab_CHINESE_Click(sender As Object, e As EventArgs) Handles lnkTab_CHINESE.Click
        ClearTab()
        Tab_CHINESE.Attributes("class") = "active"
        pnlCHINESE.Visible = True
        Current_Tab = VDM_BL.UILanguage.CN

    End Sub

    Private Sub lnkTab_JAPANESE_Click(sender As Object, e As EventArgs) Handles lnkTab_JAPANESE.Click
        ClearTab()
        Tab_JAPANESE.Attributes("class") = "active"
        pnlJAPANESE.Visible = True
        Current_Tab = VDM_BL.UILanguage.JP
    End Sub

    Private Sub lnkTab_KOREAN_Click(sender As Object, e As EventArgs) Handles lnkTab_KOREAN.Click
        ClearTab()
        Tab_KOREAN.Attributes("class") = "active"
        pnlKOREAN.Visible = True
        Current_Tab = VDM_BL.UILanguage.KR
    End Sub

    Private Sub lnkTab_RUSSIAN_Click(sender As Object, e As EventArgs) Handles lnkTab_RUSSIAN.Click
        ClearTab()
        Tab_RUSSIAN.Attributes("class") = "active"
        pnlRUSSIAN.Visible = True
        Current_Tab = VDM_BL.UILanguage.RS
    End Sub
#End Region


#Region "Upload_Logo"

    Private Sub btnUpdateLogo_Click(sender As Object, e As EventArgs) Handles btnUpdate_SimPackage_Logo.Click
        Try
            Dim C As New Converter
            Dim B As Byte()

            Select Case Current_Tab
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
            Select Case Current_Tab
                Case VDM_BL.UILanguage.TH
                    SIM_PACKAGE_Logo_TH = B
                Case VDM_BL.UILanguage.EN
                    SIM_PACKAGE_Logo_EN = B
                Case VDM_BL.UILanguage.CN
                    SIM_PACKAGE_Logo_CH = B
                Case VDM_BL.UILanguage.JP
                    SIM_PACKAGE_Logo_JP = B
                Case VDM_BL.UILanguage.KR
                    SIM_PACKAGE_Logo_KR = B
                Case VDM_BL.UILanguage.RS
                    SIM_PACKAGE_Logo_RS = B
            End Select
        Catch ex As Exception
            Alert(Me.Page, "Support only image jpeg gif png\nAnd file size must not larger than 4MB")
            Exit Sub
        End Try
    End Sub


    Private Sub btnUpdateLogo_TH_Detail_Click(sender As Object, e As EventArgs) Handles btnUpdate_SimDetail_Logo.Click
        Try
            Dim C As New Converter
            Dim B As Byte()

            Select Case Current_Tab
                Case VDM_BL.UILanguage.TH
                    B = C.StreamToByte(ful_TH_Detail.FileContent)
                Case VDM_BL.UILanguage.EN
                    B = C.StreamToByte(ful_EN_Detail.FileContent)
                Case VDM_BL.UILanguage.CN
                    B = C.StreamToByte(ful_CH_Detail.FileContent)
                Case VDM_BL.UILanguage.JP
                    B = C.StreamToByte(ful_JP_Detail.FileContent)
                Case VDM_BL.UILanguage.KR
                    B = C.StreamToByte(ful_KR_Detail.FileContent)
                Case VDM_BL.UILanguage.RS
                    B = C.StreamToByte(ful_RS_Detail.FileContent)
            End Select

            Dim img As System.Drawing.Image = System.Drawing.Image.FromStream(C.ByteToStream(B))
            Select Case Current_Tab
                Case VDM_BL.UILanguage.TH
                    SIM_DETAIL_Logo_TH = B
                Case VDM_BL.UILanguage.EN
                    SIM_DETAIL_Logo_EN = B
                Case VDM_BL.UILanguage.CN
                    SIM_DETAIL_Logo_CH = B
                Case VDM_BL.UILanguage.JP
                    SIM_DETAIL_Logo_JP = B
                Case VDM_BL.UILanguage.KR
                    SIM_DETAIL_Logo_KR = B
                Case VDM_BL.UILanguage.RS
                    SIM_DETAIL_Logo_RS = B
            End Select
        Catch ex As Exception
            Alert(Me.Page, "Support only image jpeg gif png\nAnd file size must not larger than 4MB")
            Exit Sub
        End Try
    End Sub



#End Region

#Region "ADDNewPackage"
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

        Alert_Language = "Package THAI : "
        If txtDisplayName_TH.Text = "" Then
            Alert(Me.Page, Alert_Language & "กรอก Package Name")
            Exit Sub
        End If

        If txtDescription_TH.Text = "" Then
            Alert(Me.Page, Alert_Language & "กรอก Description")
            Exit Sub
        End If

        If IsNothing(SIM_PACKAGE_Logo_TH) Then
            Alert(Me.Page, Alert_Language & "เลือก Logo package ")
            Exit Sub
        End If

        If IsNothing(SIM_DETAIL_Logo_TH) Then
            Alert(Me.Page, Alert_Language & "เลือก Package detail ")
            Exit Sub
        End If

        Dim SQL As String = "SELECT * FROM MS_SIM WHERE PRODUCT_CODE='" & txtCode.Text.Replace("'", "''") & "' AND SIM_ID<>" & SIM_ID
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            Alert(Me.Page, "Code ซ้ำ")
            Exit Sub
        End If
        SQL = "SELECT * FROM MS_SIM WHERE DISPLAY_NAME_TH='" & txtDisplayName_TH.Text.Replace("'", "''") & "' AND SIM_ID<>" & SIM_ID
        DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        DT = New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            Alert(Me.Page, "Package Name Thai ซ้ำ")
            Exit Sub
        End If

        'SIM
        SQL = "SELECT * FROM MS_SIM WHERE SIM_ID=" & SIM_ID
        DT = New DataTable
        DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        Dim DR As DataRow
        If DT.Rows.Count = 0 Then
            DR = DT.NewRow
            DT.Rows.Add(DR)
            SIM_ID = GetNewID()
            DR("SIM_ID") = SIM_ID
        Else
            DR = DT.Rows(0)
        End If
        DR("PRODUCT_CODE") = txtCode.Text
        DR("DISPLAY_NAME_TH") = IIf(txtDisplayName_TH.Text <> "", txtDisplayName_TH.Text, DBNull.Value)
        DR("DISPLAY_NAME_EN") = IIf(txtDisplayName_EN.Text <> "", txtDisplayName_EN.Text, DBNull.Value)
        DR("DISPLAY_NAME_CH") = IIf(txtDisplayName_CH.Text <> "", txtDisplayName_CH.Text, DBNull.Value)
        DR("DISPLAY_NAME_JP") = IIf(txtDisplayName_JP.Text <> "", txtDisplayName_JP.Text, DBNull.Value)
        DR("DISPLAY_NAME_KR") = IIf(txtDisplayName_KR.Text <> "", txtDisplayName_KR.Text, DBNull.Value)
        DR("DISPLAY_NAME_RS") = IIf(txtDisplayName_RS.Text <> "", txtDisplayName_RS.Text, DBNull.Value)
        DR("DESCRIPTION_TH") = IIf(txtDescription_TH.Text <> "", txtDescription_TH.Text, DBNull.Value)
        DR("DESCRIPTION_EN") = IIf(txtDescription_EN.Text <> "", txtDescription_EN.Text, DBNull.Value)
        DR("DESCRIPTION_CH") = IIf(txtDescription_CH.Text <> "", txtDescription_CH.Text, DBNull.Value)
        DR("DESCRIPTION_JP") = IIf(txtDescription_JP.Text <> "", txtDescription_JP.Text, DBNull.Value)
        DR("DESCRIPTION_KR") = IIf(txtDescription_KR.Text <> "", txtDescription_KR.Text, DBNull.Value)
        DR("DESCRIPTION_RS") = IIf(txtDescription_RS.Text <> "", txtDescription_RS.Text, DBNull.Value)
        DR("IS_SERIAL") = True
        DR("PRICE") = txtPrice.Text.Replace(",", "")
        DR("Active_Status") = chkActive.Checked
        DR("Update_By") = Session("USER_ID")
        DR("Update_Time") = Now
        Dim cmd As New SqlCommandBuilder(DA)
        Try
            DA.Update(DT)
            '--SIM PACKAGE
            BL.Save_SIM_Package_Picture(SIM_ID, VDM_BL.UILanguage.TH, SIM_PACKAGE_Logo_TH)
            If Not IsNothing(SIM_PACKAGE_Logo_EN) Then
                BL.Save_SIM_Package_Picture(SIM_ID, VDM_BL.UILanguage.EN, SIM_PACKAGE_Logo_EN)
            End If
            If Not IsNothing(SIM_PACKAGE_Logo_CH) Then
                BL.Save_SIM_Package_Picture(SIM_ID, VDM_BL.UILanguage.CN, SIM_PACKAGE_Logo_CH)
            End If
            If Not IsNothing(SIM_PACKAGE_Logo_JP) Then
                BL.Save_SIM_Package_Picture(SIM_ID, VDM_BL.UILanguage.JP, SIM_PACKAGE_Logo_JP)
            End If
            If Not IsNothing(SIM_PACKAGE_Logo_KR) Then
                BL.Save_SIM_Package_Picture(SIM_ID, VDM_BL.UILanguage.KR, SIM_PACKAGE_Logo_KR)
            End If
            If Not IsNothing(SIM_PACKAGE_Logo_RS) Then
                BL.Save_SIM_Package_Picture(SIM_ID, VDM_BL.UILanguage.RS, SIM_PACKAGE_Logo_RS)
            End If

            '--SIM DETAIL
            BL.Save_SIM_Detail_Picture(SIM_ID, VDM_BL.UILanguage.TH, SIM_DETAIL_Logo_TH)
            If Not IsNothing(SIM_DETAIL_Logo_EN) Then
                BL.Save_SIM_Detail_Picture(SIM_ID, VDM_BL.UILanguage.EN, SIM_DETAIL_Logo_EN)
            End If
            If Not IsNothing(SIM_DETAIL_Logo_CH) Then
                BL.Save_SIM_Detail_Picture(SIM_ID, VDM_BL.UILanguage.CN, SIM_DETAIL_Logo_CH)
            End If
            If Not IsNothing(SIM_DETAIL_Logo_JP) Then
                BL.Save_SIM_Detail_Picture(SIM_ID, VDM_BL.UILanguage.JP, SIM_DETAIL_Logo_JP)
            End If
            If Not IsNothing(SIM_DETAIL_Logo_KR) Then
                BL.Save_SIM_Detail_Picture(SIM_ID, VDM_BL.UILanguage.KR, SIM_DETAIL_Logo_KR)
            End If
            If Not IsNothing(SIM_DETAIL_Logo_RS) Then
                BL.Save_SIM_Detail_Picture(SIM_ID, VDM_BL.UILanguage.RS, SIM_DETAIL_Logo_RS)
            End If
        Catch ex As Exception
            Alert(Me.Page, ex.Message)
            Exit Sub
        End Try

        Alert(Me.Page, "บันทึกสำเร็จ")
        ResetPage(Nothing, Nothing)

    End Sub
#End Region


    Private Function GetNewID() As Integer
        Dim SQL As String = "SELECT IsNull(MAX(SIM_ID),0)+1 FROM MS_SIM "
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT.Rows(0).Item(0)
    End Function

#Region "Get SIM TSM"
    Private Sub btnGetSIMTSM_Click(sender As Object, e As EventArgs) Handles btnGetSIMTSM.Click
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

                If Response.Product.IS_SIM.ToUpper = "FALSE" Then
                    Alert(Me.Page, txtCode.Text & "Is Product ดำเนินการหน้า Manage Product Info ")
                    Exit Sub
                End If

                ModuleGlobal.ImplementJavaMoneyText(txtPrice)
                If Not IsDBNull(Response.Price) Then
                    txtPrice.Text = FormatNumber(Response.Price, 2)
                End If

                txtDisplayName_TH.Text = Response.Product.DISPLAY_NAME
                txtDescription_TH.Text = Response.Product.DESCRIPTION


            End If

        Catch ex As Exception
            Alert(Me.Page, "ไม่มีข้อมูล SIM Code ใน TSM")
            Exit Sub
        End Try



    End Sub
#End Region


End Class