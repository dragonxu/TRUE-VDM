Imports System.Data
Imports System.Data.SqlClient

Public Class Device_Shoping_Cart
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

#Region "PRODUCT"
    Protected Property PRODUCT_ID As Integer
        Get
            Try
                Return Request.QueryString("PRODUCT_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(value As Integer)
            Request.QueryString("PRODUCT_ID") = value
        End Set
    End Property
#End Region

#Region "SIM"

    Protected Property SIM_ID As Integer
        Get
            Try
                Return Request.QueryString("SIM_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(value As Integer)
            Request.QueryString("SIM_ID") = value
        End Set
    End Property

    Public Property Customer_IDCard As VDM_BL.Customer_IDCard
        Get
            If IsNothing(Session("Customer_IDCard")) Then
                Session("Customer_IDCard") = New VDM_BL.Customer_IDCard
            End If
            Return Session("Customer_IDCard")
        End Get
        Set(value As VDM_BL.Customer_IDCard)
            Session("Customer_IDCard") = value
        End Set
    End Property


    Public Property Customer_Passport As VDM_BL.Customer_Passport
        Get
            If IsNothing(Session("Customer_Passport")) Then
                Session("Customer_Passport") = New VDM_BL.Customer_Passport
            End If
            Return Session("Customer_Passport")
        End Get
        Set(value As VDM_BL.Customer_Passport)
            Session("Customer_Passport") = value
        End Set
    End Property

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsNumeric(Session("LANGUAGE")) Then
            Response.Redirect("Select_Language.aspx")
        End If

        If Not IsPostBack Then
            DT_CONTROL = UI_CONTROL()
            Bind_CONTROL()
            ClearForm()
            If PRODUCT_ID <> 0 Then
                BindProduct()
            Else
                BindSIM()
            End If
            txtLocalControllerURL.Text = BL.LocalControllerURL
        Else
            initFormPlugin()
        End If
    End Sub

    Private Sub initFormPlugin()
        'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

#Region "UI"
    Private ReadOnly Property UI_CONTROL As DataTable  '------------- เอาไว้ดึงข้อมูล UI ----------
        Get
            Try
                Return BL.GET_MS_UI_LANGUAGE(LANGUAGE)
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property
    Dim DT_CONTROL As DataTable
    Public Sub Bind_CONTROL()
        If LANGUAGE > VDM_BL.UILanguage.TH Then
            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_SHOPINGCART.Text & "'"
            lblUI_SHOPINGCART.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_SHOPINGCART.Text)
            'lblUI_SHOPINGCART.CssClass = "t-cart t-red true-b UI"

            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_PAYMENT.Text & "'"
            lblUI_PAYMENT.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_PAYMENT.Text)
            'lblUI_PAYMENT.CssClass = "t-payment true-b UI"

            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_COMPLETEORDER.Text & "'"
            lblUI_COMPLETEORDER.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_COMPLETEORDER.Text)
            'lblUI_COMPLETEORDER.CssClass = "t-complete true-b UI"

            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_Term.Text & "'"
            lblUI_Term.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_Term.Text)
            'lblUI_Term.CssClass = "UI"

            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblPrice_str.Text & "'"
            lblPrice_str.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblPrice_str.Text)
            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblCurrency_Str.Text & "'"
            lblCurrency_Str.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblCurrency_Str.Text)
            'lblPrice_str.CssClass = "UI"

            'lblCurrency_Str.CssClass = "UI"

            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_Accept.Text & "'"
            lblUI_Accept.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_Accept.Text)
            lblUI_Accept.CssClass = "UI"


            'btn
            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & btnConfirm.Text & "'"
            btnConfirm.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, btnConfirm.Text)

            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & btnVerify.Text & "'"
            btnVerify.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, btnVerify.Text)

        End If

        If LANGUAGE > VDM_BL.UILanguage.EN Then
            lblUI_SHOPINGCART.CssClass = "t-cart t-red true-b UI"
            lblUI_PAYMENT.CssClass = "t-payment true-b UI"
            lblUI_COMPLETEORDER.CssClass = "t-complete true-b UI"
            lblUI_Term.CssClass = "UI"
            lblPrice_str.CssClass = "UI"
            lblCurrency_Str.CssClass = "UI"
            lblUI_Accept.CssClass = "UI"
        End If
    End Sub

#End Region


    Private Sub ClearForm()
        Customer_IDCard = Nothing
        Customer_Passport = Nothing

        pnlProduct.Visible = PRODUCT_ID <> 0
        btnConfirm.Visible = PRODUCT_ID <> 0

        btnVerify.Visible = SIM_ID <> 0
        clickIDCard.Visible = SIM_ID <> 0

        chkActive.Checked = False
    End Sub

#Region "PRODUCT"

    Private Sub BindProduct()
        Dim SQL As String = ""
        SQL &= "  SELECT * FROM VW_CURRENT_PRODUCT_DETAIL WHERE PRODUCT_ID=" & PRODUCT_ID & " AND KO_ID=" & KO_ID
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            Dim Path As String = BL.Get_Product_Picture_Path(PRODUCT_ID, LANGUAGE)
            If IO.File.Exists(Path) Then
                img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & PRODUCT_ID & "&LANG=" & LANGUAGE
            Else
                img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & PRODUCT_ID & "&LANG=" & VDM_BL.UILanguage.TH
            End If
            lblDISPLAY_NAME.Text = DT.Rows(0).Item("DISPLAY_NAME_" & BL.Get_Language_Code(LANGUAGE)).ToString()

            'lblPrice_str.Text = ""
            If Not IsDBNull(DT.Rows(0).Item("PRICE")) Then
                lblPrice_Money.Text = FormatNumber(Val(DT.Rows(0).Item("PRICE")), 2)
                'lblCurrency_Str.Text = ""
            End If

        End If

        Dim SQL_Active As String = ""
        SQL_Active &= "  Select DISTINCT PRODUCT_ID,PRODUCT_CODE,PRODUCT_NAME,KO_ID " & vbLf
        SQL_Active &= "        ,SPEC_ID,SEQ " & vbLf
        SQL_Active &= "        ,SPEC_NAME_" & BL.Get_Language_Code(LANGUAGE).ToString() & ",DESCRIPTION_" & BL.Get_Language_Code(LANGUAGE).ToString()
        SQL_Active &= "        ,CAT_ID,MODEL       " & vbLf
        SQL_Active &= "    From VW_CURRENT_PRODUCT_SPEC " & vbLf
        SQL_Active &= "    Where PRODUCT_ID =" & PRODUCT_ID & " And SPEC_ID In (" & VDM_BL.Spec.Capacity & "," & VDM_BL.Spec.Color & ") " & vbLf
        SQL_Active &= "    AND KO_ID=" & KO_ID

        DA = New SqlDataAdapter(SQL_Active, BL.ConnectionString)
        Dim DT_Active As New DataTable
        DA.Fill(DT_Active)
        If DT_Active.Rows.Count > 0 Then
            For i As Integer = 0 To DT_Active.Rows.Count - 1
                If DT_Active.Rows(i).Item("SPEC_ID") = VDM_BL.Spec.Color Then
                    lblColor.Text = DT_Active.Rows(i).Item("DESCRIPTION_" & BL.Get_Language_Code(LANGUAGE)).ToString()
                End If
                pnlCapacity.Visible = False
                If DT_Active.Rows(i).Item("SPEC_ID") = VDM_BL.Spec.Capacity Then
                    Dim Unit As String = ""
                    Select Case IIf(Not IsDBNull(DT.Rows(0).Item("CAT_ID")), DT.Rows(0).Item("CAT_ID"), 0)
                        Case VDM_BL.Category.Accessories
                            Unit = "mbps"
                        Case Else
                            Unit = "GB"
                    End Select
                    lblCapacity.Text = DT_Active.Rows(i).Item("DESCRIPTION_" & BL.Get_Language_Code(LANGUAGE)).ToString() & "" & Unit
                    pnlCapacity.Visible = True
                End If
            Next
        End If
    End Sub

#End Region

#Region "SIM"
    Private Sub BindSIM()
        Dim DT As DataTable = BL.GetList_Current_SIM_Kiosk(KO_ID, SIM_ID)
        If DT.Rows.Count > 0 Then
            Dim Path As String = BL.Get_Product_Picture_Path(PRODUCT_ID, LANGUAGE)
            If IO.File.Exists(Path) Then
                img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=SIM_PACKAGE&UID=" & SIM_ID & "&LANG=" & LANGUAGE
            Else
                img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=SIM_PACKAGE&UID=" & SIM_ID & "&LANG=" & VDM_BL.UILanguage.TH
            End If
            lblDISPLAY_NAME.Text = DT.Rows(0).Item("DISPLAY_NAME_" & BL.Get_Language_Code(LANGUAGE)).ToString()

            'lblPrice_str.Text = "ยอดชำระ"
            If Not IsDBNull(DT.Rows(0).Item("PRICE")) Then
                lblPrice_Money.Text = FormatNumber(Val(DT.Rows(0).Item("PRICE")), 2)
                'lblCurrency_Str.Text = ""
            End If
        End If

    End Sub
#End Region

    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Select_Menu.aspx")
    End Sub

    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click

        If PRODUCT_ID <> 0 Then
            Response.Redirect("Device_Product_Detail.aspx?PRODUCT_ID=" & PRODUCT_ID)
        Else
            Response.Redirect("SIM_Detail.aspx?SIM_ID=" & SIM_ID)
        End If
    End Sub

    Private Sub chkActive_CheckedChanged(sender As Object, e As EventArgs) Handles chkActive.CheckedChanged
        If chkActive.Checked Then
            If LANGUAGE <= VDM_BL.UILanguage.EN Then
                btnConfirm.CssClass = "order true-m"
                btnVerify.CssClass = "order true-m"
            Else
                btnConfirm.CssClass = "order true-m UI"
                btnVerify.CssClass = "order true-m UI"
            End If

        Else
            If LANGUAGE <= VDM_BL.UILanguage.EN Then
                btnConfirm.CssClass = "order true-m default"
                btnVerify.CssClass = "order true-m default"
            Else
                btnConfirm.CssClass = "order true-m default UI"
                btnVerify.CssClass = "order true-m default UI"
            End If

        End If

    End Sub

    Private Sub btnConfirm_ServerClick(sender As Object, e As EventArgs) Handles btnConfirm.Click
        If Not chkActive.Checked Then Exit Sub
        'ใบเสร็จรับเงินฉบับจริง
        Dim Script As String = "$(""#lnkSlip"").click();" & vbNewLine
        Script &= " requestSlip();"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Slip", Script, True)

    End Sub

    Private Sub btnNextSlip_ServerClick(sender As Object, e As EventArgs) Handles btnNextSlip.ServerClick
        If PRODUCT_ID <> 0 Then
            Response.Redirect("Device_Payment.aspx?PRODUCT_ID=" & PRODUCT_ID)
        Else

            Dim Script As String = "$(""#btnVerifySlip"").click();" & vbNewLine
            Script &= " requestVerifySlip();"
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "VerifySlip", Script, True)

        End If
    End Sub


    Private Sub btnVerify_Click(sender As Object, e As EventArgs) Handles btnVerify.Click
        If Not chkActive.Checked Then Exit Sub

        '--ใบเสร็จรับเงินฉบับจริง
        Dim Script As String = "$(""#lnkSlip"").click();" & vbNewLine
        Script &= " requestSlip();"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Slip", Script, True)


        'Dim Script As String = "$(""#clickIDCard"").click();" & vbNewLine
        'If LANGUAGE = VDM_BL.UILanguage.TH Then
        '    Script &= " requestIDCard();"
        'Else
        '    Script &= " requestPassport();"
        'End If
        'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "StartScan", Script, True)
    End Sub

    Private Sub btnVerifySlip_Click(sender As Object, e As EventArgs) Handles btnVerifySlip.Click
        Dim Script As String = "$(""#clickIDCard"").click();" & vbNewLine
        If LANGUAGE = VDM_BL.UILanguage.TH Then
            Script &= " requestIDCard();"
        Else
            Script &= " requestPassport();"
        End If

        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "StartScan", Script, True)
    End Sub

    Private Sub btnKeepInfo_Click(sender As Object, e As EventArgs) Handles btnKeepInfo.Click
        Customer_IDCard = Nothing
        Customer_Passport = Nothing
        Dim C As New Converter

        If LANGUAGE = VDM_BL.UILanguage.TH Then
            Dim cus As New VDM_BL.Customer_IDCard

            cus.Citizenid = id_Citizenid.Text
            If id_Citizenid.Text = "" Or id_Th_Firstname.Text = "" Or id_Th_Lastname.Text = "" Then
                '--------------Error ----------------
                ResponseCannotReadIDCard()
                Exit Sub
            End If
            cus.Th_Prefix = id_Th_Prefix.Text
            cus.Th_Firstname = id_Th_Firstname.Text
            cus.Th_Middlename = id_Th_Middlename.Text
            cus.Th_Lastname = id_Th_Lastname.Text
            cus.En_Prefix = id_En_Prefix.Text
            cus.En_Firstname = id_En_Firstname.Text
            cus.En_Middlename = id_En_Middlename.Text
            cus.En_Lastname = id_En_Lastname.Text
            cus.Sex = id_Sex.Text
            If id_Birthday.Text <> "" Then
                cus.Birthday = C.DateToString(id_Birthday.Text, "yyyy-MM-dd")
            Else
                '--------------Error ----------------
                ResponseCannotReadIDCard()
                Exit Sub
            End If
            cus.Address = id_Address.Text
            cus.addrHouseNo = id_addrHouseNo.Text
            cus.addrVillageNo = id_addrVillageNo.Text
            cus.addrLane = id_addrLane.Text
            cus.addrRoad = id_addrRoad.Text
            cus.addrTambol = id_addrTambol.Text
            cus.addrAmphur = id_addrAmphur.Text
            cus.addrProvince = id_addrProvince.Text
            If id_Issue.Text <> "" Then
                cus.Issue = C.DateToString(id_Issue.Text, "yyyy-MM-dd")
            Else
                '--------------Error ----------------
                ResponseCannotReadIDCard()
                Exit Sub
            End If
            cus.Issuer = id_Issuer.Text

            'If id_Expire.Text <> "" Then
            '    cus.Expire = C.DateToString(id_Expire.Text, "yyyy-MM-dd")
            '    If DateDiff(DateInterval.Day, C.StringToDate(id_Expire.Text, "yyyy-MM-dd"), Now) > 0 Then
            Dim _cultureEnInfo As New Globalization.CultureInfo("en-US")
            Dim Expire As DateTime = Convert.ToDateTime(id_Expire.Text, _cultureEnInfo)
            If id_Expire.Text <> "" Then
                cus.Expire = C.DateToString(id_Expire.Text, "yyyy-MM-dd")
                If DateDiff(DateInterval.Day, C.StringToDate((Expire.ToString("yyyy", _cultureEnInfo)) & "-" & Expire.ToString("MM-dd", _cultureEnInfo), "yyyy-MM-dd"), Now) > 0 Then

                    ResponseIDCardExpired()
                    Exit Sub
                End If
            Else
                '--------------Error ----------------
                ResponseCannotReadIDCard()
                Exit Sub
            End If
            cus.Photo = id_Photo.Text

            Customer_IDCard = cus
            '--------------------- save ลง Database : TB_CUSTOMER_DOC----------------
            BL.SAVE_CUSTOMER_IDCard(Customer_IDCard, TXN_ID)
        Else
            Dim cus As New VDM_BL.Customer_Passport

            If pass_FirstName.Text = "" Or pass_LastName.Text = "" Or pass_DocType.Text = "" Or
                pass_Nationality.Text = "" Or pass_PassportNo.Text = "" Or pass_DateOfBirth.Text = "" Or
                pass_Expire.Text = "" Or pass_PersonalID.Text = "" Or pass_IssueCountry.Text = "" Or pass_Photo.Text = "" Then
                '--------------Error ----------------
                ResponseCannotReadPassport()
                Exit Sub
            End If

            Dim _cultureEnInfo As New Globalization.CultureInfo("en-US")
            Dim Expire As DateTime = Convert.ToDateTime(pass_Expire.Text, _cultureEnInfo)
            If pass_Expire.Text <> "" Then
                If DateDiff(DateInterval.Day, C.StringToDate((Expire.ToString("yyyy", _cultureEnInfo)) & "-" & Expire.ToString("MM-dd", _cultureEnInfo), "yyyy-MM-dd"), Now) > 0 Then
                    ResponseIDCardExpired()
                    Exit Sub
                End If
            Else
                '--------------Error ----------------
                ResponseCannotReadIDCard()
                Exit Sub
            End If


            cus.FirstName = pass_FirstName.Text
            cus.MiddleName = pass_MiddleName.Text
            cus.LastName = pass_LastName.Text
            cus.DocType = pass_DocType.Text
            cus.Nationality = pass_Nationality.Text
            cus.PassportNo = pass_PassportNo.Text
            cus.DateOfBirth = pass_DateOfBirth.Text
            cus.Sex = pass_Sex.Text
            cus.Expire = pass_Expire.Text

            cus.PersonalID = pass_PersonalID.Text
            cus.IssueCountry = pass_IssueCountry.Text
            cus.MRZ = pass_MRZ.Text.Replace("&lt", "<")
            cus.Photo = pass_Photo.Text
            Customer_Passport = cus
            '--------------------- save ลง Database : TB_CUSTOMER_DOC----------------
            BL.SAVE_CUSTOMER_Passport(cus, TXN_ID)
        End If

        '------------- Clear Textbox Improve Postback Performance--------------
        id_Citizenid.Text = ""
        id_Th_Prefix.Text = ""
        id_Th_Firstname.Text = ""
        id_Th_Middlename.Text = ""
        id_Th_Lastname.Text = ""
        id_En_Prefix.Text = ""
        id_En_Firstname.Text = ""
        id_En_Middlename.Text = ""
        id_En_Lastname.Text = ""
        id_Sex.Text = ""
        id_Birthday.Text = ""
        id_Address.Text = ""
        id_addrHouseNo.Text = ""
        id_addrVillageNo.Text = ""
        id_addrLane.Text = ""
        id_addrRoad.Text = ""
        id_addrTambol.Text = ""
        id_addrAmphur.Text = ""
        id_addrProvince.Text = ""
        id_Issue.Text = ""
        id_Issuer.Text = ""
        id_Expire.Text = ""
        id_Photo.Text = ""

        pass_FirstName.Text = ""
        pass_MiddleName.Text = ""
        pass_LastName.Text = ""
        pass_DocType.Text = ""
        pass_Nationality.Text = ""
        pass_PassportNo.Text = ""
        pass_DateOfBirth.Text = ""
        pass_Sex.Text = ""
        pass_Expire.Text = ""
        pass_PersonalID.Text = ""
        pass_IssueCountry.Text = ""
        pass_MRZ.Text = ""
        pass_Photo.Text = ""

        '------------------- Next to Camera Scan -------------
        Dim Script As String = "triggerCamera();"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "ToCamera", Script, True)

    End Sub

    Private Sub ResponseIDCardExpired()
        Dim Script As String = ""
        Script &= "$('#idCardCrossReason').html('ขออภัย บัตรของท่านหมดอายุ');" & vbLf
        Script &= "$('#clickIDCardCross').click();" & vbLf
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "IDCardExpired", Script, True)
    End Sub

    Private Sub ResponseCannotReadIDCard()
        Dim Script As String = ""
        Script &= "$('#idCardAlertReason').html('ขออภัย ไม่สามารถอ่านบัตรประชาชนของท่านได้');" & vbLf
        Script &= "$('#clickIDCardAlert').click();" & vbLf
        ' Auto ReScan
        Script &= "setTimeOut(function(){ $(""#clickIDCard"").click();requestIDCard();},1000);" & vbNewLine

        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CannotReadIDCard", Script, True)
    End Sub

    Private Sub ResponseCannotReadPassport()
        Dim Script As String = ""
        Script &= "$('#passportAlertReason').html('Cannot read your document');" & vbLf
        Script &= "$('#clickPassportAlert').click();" & vbLf
        ' Auto ReScan
        Script &= "setTimeOut(function(){ $(""#clickIDCard"").click();requestIDCard();},1000);" & vbNewLine

        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CannotReadPassport", Script, True)
    End Sub

    Private Sub ResponsePassportExpired()
        Dim Script As String = ""
        Script &= "$('#passportCrossReason').html('Your document has expired');" & vbLf
        Script &= "$('#clickPassportCross').click();" & vbLf
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "PassportExpired", Script, True)
    End Sub


    Private Sub btnPostCam_Click(sender As Object, e As EventArgs) Handles btnPostCam.Click
        If txtCamData.Text = "" Then Exit Sub

        If LANGUAGE = VDM_BL.UILanguage.TH Then
            Customer_IDCard.FaceCamera = txtCamData.Text
            BL.SAVE_CUSTOMER_IDCard(Customer_IDCard, TXN_ID)
        Else
            Customer_Passport.FaceCamera = txtCamData.Text
            BL.SAVE_CUSTOMER_Passport(Customer_Passport, TXN_ID)
        End If
        Response.Redirect("Device_Verify.aspx?SIM_ID=" & SIM_ID)
    End Sub


End Class