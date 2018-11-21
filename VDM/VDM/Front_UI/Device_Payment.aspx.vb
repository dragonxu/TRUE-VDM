Imports System.Data.SqlClient
Imports System.Net
Public Class Device_Payment
    Inherits System.Web.UI.Page



#Region "ส่วนที่เหมือนกันหมดทุกหน้า"

    Dim BL As New VDM_BL

    Private ReadOnly Property KO_ID As Integer '------------- เอาไว้เรียกใช้ง่ายๆ ----------
        Get
            Try
                Return Request.Cookies("KO_ID").Value
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Private Property LANGUAGE As VDM_BL.UILanguage '-------- หน้าอื่นๆต้องเป็น ReadOnly --------
        Get
            Return Session("LANGUAGE")
        End Get
        Set(value As VDM_BL.UILanguage)
            Session("LANGUAGE") = value
        End Set
    End Property

    Public ReadOnly Property TXN_ID As Integer
        Get
            Return Session("TXN_ID")
        End Get
    End Property

#End Region

    Private ReadOnly Property PRODUCT_ID As Integer
        Get
            If IsNumeric(Request.QueryString("PRODUCT_ID")) Then
                Return Request.QueryString("PRODUCT_ID")
            Else
                Return 0
            End If
        End Get
    End Property

    Private Property CASH_PAID As Integer
        Get
            Return txtPaid.Text.Replace(",", "").Replace(".", "")
        End Get
        Set(value As Integer)
            txtPaid.Text = value
        End Set
    End Property

    Private Property PRODUCT_COST As Integer
        Get
            Return CInt(Replace(txtCost.Text, ",", ""))
        End Get
        Set(value As Integer)
            txtCost.Text = FormatNumber(value, 0)
        End Set
    End Property

    Private Property IS_SERIAL As Boolean
        Get
            Try
                Return ViewState("IS_SERIAL")
            Catch ex As Exception
                Return False
            End Try
        End Get
        Set(value As Boolean)
            ViewState("IS_SERIAL") = value
        End Set
    End Property


#Region "SIM"

    Private ReadOnly Property SIM_ID As Integer
        Get
            If IsNumeric(Request.QueryString("SIM_ID")) Then
                Return Request.QueryString("SIM_ID")
            Else
                Return 0
            End If
        End Get
    End Property


#End Region

    Private ReadOnly Property CREDIT_CARD_REQ_ID As Integer
        Get
            Return Val(txtCreditReq.Text)
        End Get
    End Property

    Private Function UNX() As String
        Return Now.ToOADate.ToString.Replace(".", "")
    End Function


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            If IsNothing(Session("ssCashTimeout")) Then Session("ssCashTimeout") = 0

            txtLocalControllerURL.Text = BL.LocalControllerURL
            DT_CONTROL = UI_CONTROL()
            DT_CONTROL_POPUP = UI_CONTROL_POPUP()
            Bind_CONTROL()
            ClearForm()

            If PRODUCT_ID = 0 And SIM_ID = 0 Then
                Response.Redirect("Device_Brand.aspx")
                Exit Sub
            End If

            If PRODUCT_ID <> 0 Then
                BindProduct()
            Else
                BindSIM()
            End If

        Else
            initFormPlugin()
        End If
    End Sub

    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
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
    Private ReadOnly Property UI_CONTROL_POPUP As DataTable  '------------- เอาไว้ดึงข้อมูล UI ----------
        Get
            Try
                Return BL.GET_MS_UI_CONTROL_POPUP(LANGUAGE)
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property
    Dim DT_CONTROL_POPUP As DataTable

    Public Sub Bind_CONTROL()
        On Error Resume Next
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

            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblPrice_str.Text & "'"
            lblPrice_str.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblPrice_str.Text)
            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblCurrency_Str.Text & "'"
            lblCurrency_Str.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblCurrency_Str.Text)
            'lblPrice_str.CssClass = "UI"
            'lblCurrency_Str.CssClass = "UI"

            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_Cash.Text & "'"
            lblUI_Cash.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_Cash.Text)
            'lblUI_Cash.Attributes("class") = "true-l UI"

            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_Credit.Text & "'"
            lblUI_Credit.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_Credit.Text)
            'lblUI_Credit.Attributes("class") = "true-l UI"

            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_Debit.Text & "'"
            lblUI_Debit.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_Debit.Text)
            'lblUI_Debit.Attributes("class") = "UI"

            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_TrueMoney.Text & "'"
            lblUI_TrueMoney.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_TrueMoney.Text)
            'lblUI_TrueMoney.Attributes("class") = "UI"

            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_SelectPayment.Text & "'"
            lblUI_SelectPayment.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_SelectPayment.Text)
            'lblUI_SelectPayment.Attributes("class") = "UI"

            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_ByCash.Text & "'"
            lblUI_ByCash.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_ByCash.Text)
            'lblUI_ByCash.Attributes("class") = "UI"

            'Remark Cash
            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_remark1.Text & "'"
            lblUI_remark1.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_remark1.Text)
            'lblUI_remark1.Style("font-size") = "30px"
            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_remark2.Text & "'"
            lblUI_remark2.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_remark2.Text)
            'lblUI_remark2.Style("font-size") = "30px"
            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_remark3.Text & "'"
            lblUI_remark3.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_remark3.Text)
            'lblUI_remark3.Style("font-size") = "30px"
            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_remark4.Text & "'"
            lblUI_remark4.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_remark4.Text)
            'lblUI_remark4.Style("font-size") = "30px"
            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_remark5.Text & "'"
            lblUI_remark5.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_remark5.Text)
            'lblUI_remark5.Attributes("class") = "UI"
            'Paymeny
            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_paymentcompleted.Text & "'"
            lblUI_paymentcompleted.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_paymentcompleted.Text)
            'lblUI_paymentcompleted.Attributes("class") = "UI"
            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_Remain.Text & "'"
            lblUI_Remain.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_Remain.Text)
            'lblUI_Remain.Attributes("class") = "UI"

            lblUI_Currency_Str1.Text = lblCurrency_Str.Text
            lblUI_Currency_Str2.Text = lblCurrency_Str.Text
            'lblUI_Currency_Str1.Attributes("class") = "UI"
            'lblUI_Currency_Str2.Attributes("class") = "UI"
            'Time
            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_Time.Text & "'"
            lblUI_Time.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_Time.Text)
            'lblUI_Time.Attributes("class") = "UI"


            'TrueMoney
            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_ByTrueMoney.Text & "'"
            lblUI_ByTrueMoney.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_ByTrueMoney.Text)
            'lblUI_ByTrueMoney.Attributes("class") = "UI"
            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_TrueMoney_Step1.Text & "'"
            lblUI_TrueMoney_Step1.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_TrueMoney_Step1.Text)
            'lblUI_TrueMoney_Step1.Attributes("class") = "UI"
            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_TrueMoney_Step2.Text & "'"
            lblUI_TrueMoney_Step2.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_TrueMoney_Step2.Text)
            'lblUI_TrueMoney_Step2.Attributes("class") = "UI"

            lblUI_paymentcompleted.Attributes("class") = "UI-EN"
            lblUI_Remain.Attributes("class") = "UI-EN"
            divpaymentcompleted.Attributes("class") = "col-md-6"
            divMoney.Attributes("class") = "col-md-4"

            '===UI_CONTROL_POPUP===
            DT_CONTROL_POPUP.DefaultView.RowFilter = "CONTROL_ID='lblTrueMoneyError_Header' "
            lblTrueMoneyError_Header.Text = IIf(DT_CONTROL_POPUP.DefaultView.Count > 0, DT_CONTROL_POPUP.DefaultView(0).Item("DISPLAY").ToString, lblTrueMoneyError_Header.Text)
            DT_CONTROL_POPUP.DefaultView.RowFilter = "CONTROL_ID='lblTrueMoneyError_Detail' "
            lblTrueMoneyError_Detail.Text = IIf(DT_CONTROL_POPUP.DefaultView.Count > 0, DT_CONTROL_POPUP.DefaultView(0).Item("DISPLAY").ToString, lblTrueMoneyError_Detail.Text)
            DT_CONTROL_POPUP.DefaultView.RowFilter = "CONTROL_ID='lblTrueMoneyError_btn' "
            lblTrueMoneyError_btn.Text = IIf(DT_CONTROL_POPUP.DefaultView.Count > 0, DT_CONTROL_POPUP.DefaultView(0).Item("DISPLAY").ToString, lblTrueMoneyError_btn.Text)

            DT_CONTROL_POPUP.DefaultView.RowFilter = "CONTROL_ID='lblCreditCardError_Header' "
            lblCreditCardError_Header.Text = IIf(DT_CONTROL_POPUP.DefaultView.Count > 0, DT_CONTROL_POPUP.DefaultView(0).Item("DISPLAY").ToString, lblCreditCardError_Header.Text)
            DT_CONTROL_POPUP.DefaultView.RowFilter = "CONTROL_ID='lblCreditCardError_Detail' "
            lblCreditCardError_Detail.Text = IIf(DT_CONTROL_POPUP.DefaultView.Count > 0, DT_CONTROL_POPUP.DefaultView(0).Item("DISPLAY").ToString, lblCreditCardError_Detail.Text)
            DT_CONTROL_POPUP.DefaultView.RowFilter = "CONTROL_ID='lblCreditCardError_btn' "
            lblCreditCardError_btn.Text = IIf(DT_CONTROL_POPUP.DefaultView.Count > 0, DT_CONTROL_POPUP.DefaultView(0).Item("DISPLAY").ToString, lblCreditCardError_btn.Text)

            DT_CONTROL_POPUP.DefaultView.RowFilter = "CONTROL_ID='lblCashTimeOut_Header' "
            lblCashTimeOut_Header.Text = IIf(DT_CONTROL_POPUP.DefaultView.Count > 0, DT_CONTROL_POPUP.DefaultView(0).Item("DISPLAY").ToString, lblCashTimeOut_Header.Text)
            DT_CONTROL_POPUP.DefaultView.RowFilter = "CONTROL_ID='lblCashTimeOut_Detail' "
            lblCashTimeOut_Detail.Text = IIf(DT_CONTROL_POPUP.DefaultView.Count > 0, DT_CONTROL_POPUP.DefaultView(0).Item("DISPLAY").ToString, lblCashTimeOut_Detail.Text)
            DT_CONTROL_POPUP.DefaultView.RowFilter = "CONTROL_ID='lblCashTimeOut_btn' "
            lblCashTimeOut_btn.Text = IIf(DT_CONTROL_POPUP.DefaultView.Count > 0, DT_CONTROL_POPUP.DefaultView(0).Item("DISPLAY").ToString, lblCashTimeOut_btn.Text)

            DT_CONTROL_POPUP.DefaultView.RowFilter = "CONTROL_ID='lblCashTimeOut_Header2' "
            lblCashTimeOut_Header2.Text = IIf(DT_CONTROL_POPUP.DefaultView.Count > 0, DT_CONTROL_POPUP.DefaultView(0).Item("DISPLAY").ToString, lblCashTimeOut_Header2.Text)
            DT_CONTROL_POPUP.DefaultView.RowFilter = "CONTROL_ID='lblCashTimeOut_Detail2' "
            lblCashTimeOut_Detail2.Text = IIf(DT_CONTROL_POPUP.DefaultView.Count > 0, DT_CONTROL_POPUP.DefaultView(0).Item("DISPLAY").ToString, lblCashTimeOut_Detail2.Text)
            DT_CONTROL_POPUP.DefaultView.RowFilter = "CONTROL_ID='lblCashTimeOut_btn2' "
            lblCashTimeOut_btn2.Text = IIf(DT_CONTROL_POPUP.DefaultView.Count > 0, DT_CONTROL_POPUP.DefaultView(0).Item("DISPLAY").ToString, lblCashTimeOut_btn2.Text)

            If LANGUAGE <> VDM_BL.UILanguage.EN Then
                '    lblUI_paymentcompleted.Attributes("class") = "UI-EN"
                '    lblUI_Remain.Attributes("class") = "UI-EN"
                '    divpaymentcompleted.Attributes("class") = "col-md-6"
                '    divMoney.Attributes("class") = "col-md-4"
                'Else
                lblUI_paymentcompleted.Attributes("class") = "UI"
                lblUI_Remain.Attributes("class") = "UI"
            End If
        End If
        If LANGUAGE = VDM_BL.UILanguage.TH Then
            'lblUI_Credit.Attributes("class") = "true-l UI"
            'lblUI_Debit.Attributes("class") = "true-l UI"
        End If

        If LANGUAGE > VDM_BL.UILanguage.EN Then
            lblUI_Debit.Attributes("Style") = "font-size: 30px;margin-top: 30px;"
            lblUI_SHOPINGCART.CssClass = "t-cart t-red true-b UI"
            lblUI_PAYMENT.CssClass = "t-payment  t-red  true-b UI"
            lblUI_COMPLETEORDER.CssClass = "t-complete true-b UI"
            lblPrice_str.CssClass = "UI"
            lblCurrency_Str.CssClass = "UI"
            lblUI_Cash.Attributes("class") = "UI"
            lblUI_Credit.Attributes("class") = "true-l UI"
            lblUI_Debit.Attributes("class") = "true-l UI"
            lblUI_Credit.Style("font-size") = "10px"
            lblUI_Debit.Attributes("style") = "font-size: 10px;"

            lblUI_TrueMoney.Attributes("class") = "true-l UI"
            lblUI_SelectPayment.Attributes("class") = "UI"
            lblUI_ByCash.Attributes("class") = "UI"
            lblUI_remark1.Style("font-size") = "30px"
            lblUI_remark2.Style("font-size") = "30px"
            lblUI_remark3.Style("font-size") = "30px"
            lblUI_remark4.Style("font-size") = "30px"
            lblUI_remark5.Attributes("class") = "UI"

            lblUI_Currency_Str1.Attributes("class") = "UI"
            lblUI_Currency_Str2.Attributes("class") = "UI"
            lblUI_Time.Attributes("class") = "UI"
            lblUI_ByTrueMoney.Attributes("class") = "UI"
            lblUI_TrueMoney_Step1.Attributes("class") = "UI"
            lblUI_TrueMoney_Step2.Attributes("class") = "UI"

            '===UI_CONTROL_POPUP===
            lblTrueMoneyError_Header.Style("font-size") = "35px"
            lblTrueMoneyError_Detail.Style("font-size") = "30px"
            lblTrueMoneyError_btn.Style("font-size") = "30px"

            lblCreditCardError_Header.Style("font-size") = "35px"
            lblCreditCardError_Detail.Style("font-size") = "30px"
            lblCreditCardError_btn.Style("font-size") = "30px"

            lblCashTimeOut_Header.Style("font-size") = "35px"
            lblCashTimeOut_Detail.Style("font-size") = "30px"
            lblCashTimeOut_btn.Style("font-size") = "30px"

            lblCashTimeOut_Header2.Style("font-size") = "35px"
            lblCashTimeOut_Detail2.Style("font-size") = "30px"
            lblCashTimeOut_btn2.Style("font-size") = "30px"

        Else
            lblUI_Credit.Style("font-size") = "30px"
            lblUI_Debit.Attributes("style") = "font-size: 30px;margin-top: 30px;"
        End If

    End Sub

#End Region


    Private Sub ClearForm()

        'PAYMENT_METHOD = VDM_BL.PaymentMethod.UNKNOWN

        pnlCash.Visible = False
        pnlTruemoney.Visible = False
        pnlSelectChoice.Visible = True
        'current
        lnkCash.Attributes("class") = ""
        lnkCredit.Attributes("class") = ""
        lnkTruemoney.Attributes("class") = ""
        txtBarcode.Text = ""

        txtPaid.Attributes("ReadOnly") = "true"

        '----------------------- Stop Focus Barcode ----------------------
        Dim Script As String = "stopFocusBarcode();" & vbLf
        Script &= "hideKeyboard();" & vbLf
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "clearBarcode", Script, True)
        '----------------------- Stop Cash In ----------------------
        CloseCashReciever()
    End Sub

    Private Sub CloseCashReciever()
        Dim Script As String = "disableCashIn();" & vbLf
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CloseCashReciever", Script, True)
    End Sub

    Private Sub lnkCash_ServerClick(sender As Object, e As EventArgs) Handles lnkCash.ServerClick
        ClearForm()

        pnlSelectChoice.Visible = False
        pnlCash.Visible = True
        lnkCash.Attributes("class") = "current"
        '----------------เริ่มรับชำระ --------------
        Dim Script As String = "RequireCash(); "
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CashPayment" & UNX(), Script, True)
    End Sub

    Private Sub lnkCredit_ServerClick(sender As Object, e As EventArgs) Handles lnkCredit.ServerClick
        ClearForm()

        pnlSelectChoice.Visible = False
        lnkCredit.Attributes("class") = "current"

        'Dim url As String = "Payment_Gateway_Start.aspx?amount=" & PRODUCT_COST & ".00&PRODUCT_ID=" & PRODUCT_ID

        Dim url As String = "Payment_Gateway_Init.aspx?amount=" & PRODUCT_COST & ".00&PRODUCT_ID=" & PRODUCT_ID & "&TXN_ID=" & TXN_ID
        Dim Script As String = "$('#paymentGatewayWindow').attr('src','" & url & "'); "

        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "PayCredit" & UNX(), Script, True)

    End Sub

    Private Sub btnCloseCredit_Click(sender As Object, e As EventArgs) Handles btnCloseCredit.Click
        ClearForm()
    End Sub

    Private Sub btnCreditComplete_Click(sender As Object, e As EventArgs) Handles btnCreditComplete.Click

        '--------------- Update TB_SERVICE_TRANSACTION
        Dim SQL As String = "UPDATE TB_SERVICE_TRANSACTION SET METHOD_ID=" & VDM_BL.PaymentMethod.CREDIT_CARD & vbLf
        SQL &= " WHERE TXN_ID=" & TXN_ID
        BL.ExecuteNonQuery(SQL)

        '--------------- Save Transaction Requested---------------
        SQL = "SELECT TOP 1 * FROM TB_TRANSACTION_CREDITCARD WHERE TXN_ID=" & TXN_ID
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Dim DR As DataRow
        If DT.Rows.Count = 0 Then
            DR = DT.NewRow
            DT.Rows.Add(DR)
            DR("TXN_ID") = TXN_ID
            DR("ITEM_NO") = 1
        Else
            DR = DT.Rows(0)
        End If

        DR("SLIP_YEAR") = Now.Year.ToString.Substring(2, 2)
        DR("SLIP_MONTH") = Now.Month.ToString.PadLeft(2, "0")
        DR("SLIP_DAY") = Now.Day.ToString.PadLeft(2, "0")
        DR("SLIP_NO") = BL.Get_New_Confirmation_Slip_No
        DR("SLIP_CONTENT") = DBNull.Value
        If PRODUCT_ID <> 0 Then
            DR("PRODUCT_ID") = PRODUCT_ID
        Else
            DR("SIM_ID") = SIM_ID
        End If
        DR("IS_SERIAL") = IS_SERIAL
        DR("UNIT_PRICE") = PRODUCT_COST
        DR("QUANTITY") = 1
        DR("TOTAL_PRICE") = PRODUCT_COST
        DR("BBL_REQ_ID") = CREDIT_CARD_REQ_ID

        SQL = "SELECT orderRef,REQ_Time,RESP_Time" & vbLf
        SQL &= " FROM BBL_Payment_REQ REQ " & vbLf
        SQL &= " INNER JOIN BBL_PAYMENT_RESP RESP ON REQ.REQ_ID=RESP.REQ_ID" & vbLf
        SQL &= " WHERE REQ.REQ_ID=" & CREDIT_CARD_REQ_ID & vbLf
        Dim BA As New SqlDataAdapter(SQL, BL.LogConnectionString)
        Dim BT As New DataTable
        BA.Fill(BT)
        If BT.Rows.Count > 0 Then
            DR("BBL_ORDER_REF") = BT.Rows(0).Item("orderRef").ToString
            DR("BBL_REQ_TIME") = BT.Rows(0).Item("REQ_Time")
            DR("BBL_RESP_TIME") = BT.Rows(0).Item("RESP_Time")
        End If
        DR("TXN_TIME") = Now

        Dim cmd As New SqlCommandBuilder(DA)
        DA.Update(DT)

        ''-------------------------------- Go To Pick ---------------------
        Response.Redirect("Complete_Order.aspx?PRODUCT_ID=" & PRODUCT_ID & "&SIM_ID=" & SIM_ID)
    End Sub


    Private Sub lnkTruemoney_ServerClick(sender As Object, e As EventArgs) Handles lnkTruemoney.ServerClick
        ClearForm()

        pnlSelectChoice.Visible = False
        pnlTruemoney.Visible = True
        lnkTruemoney.Attributes("class") = "current"

        '----------------------- Set Alway Focus Barcode ----------------------
        Dim Script As String = "txtBarcode='" & txtBarcode.ClientID & "';" & vbLf
        Script &= "startFocusBarcode();"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "focusBarcodeReader" & UNX(), Script, True)
    End Sub

    Private Sub btnBarcode_Click(sender As Object, e As EventArgs) Handles btnBarcode.Click
        ' Call True Money

        Dim Barcode As String = txtBarcode.Text

        If Barcode = "" Then Exit Sub
        txtBarcode.Text = ""
        lblTMNPaymentCode.Text = ""

        Dim TMN As New TrueMoney
        '------------------- Get Parameter --------------
        Dim DT As DataTable = BL.GetList_Kiosk(KO_ID)
        Dim ShopCode As String = DT.Rows(0).Item("SITE_CODE")
        Dim SITE_NAME_TH As String = DT.Rows(0).Item("SITE_NAME_TH").ToString
        DT = BL.Get_Product_Info_From_ID(PRODUCT_ID)
        Dim Amount As Integer = DT.Rows(0).Item("PRICE")
        Dim ISV As String = TMN.Generate_ISV(ShopCode)
        Dim PaymentDescription As String = SITE_NAME_TH

        '-------------------- เรียก------------------------
        Dim RESP As TrueMoney.Response = TMN.GetResult(TXN_ID, ISV, Amount, Barcode, PaymentDescription, ShopCode)

        '---------------- ตรวจสอบผลลัพธ์ -------------------
        If RESP.status.code.ToLower <> "success" Then
            lblTMNPaymentCode.Text = RESP.Request.payment_code
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "TrueMoneyError" & Now.ToOADate.ToString.Replace(".", ""), "$('#lnkTrueMoneyError').click();", True)
            Exit Sub
        End If
        '---------------- UPDATE Payment Method --------------------
        Dim SQL As String = "UPDATE TB_SERVICE_TRANSACTION SET METHOD_ID=" & VDM_BL.PaymentMethod.TRUE_MONEY & vbLf
        SQL &= "WHERE TXN_ID=" & TXN_ID
        BL.ExecuteNonQuery(SQL)

        SQL = "SELECT * FROM TB_TRANSACTION_TMN WHERE TXN_ID=" & TXN_ID
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        DT = New DataTable
        DA.Fill(DT)
        Dim DR As DataRow
        If DT.Rows.Count = 0 Then
            DR = DT.NewRow
            DT.Rows.Add(DR)
            DR("TXN_ID") = TXN_ID
        Else
            DR = DT.Rows(0)
        End If

        DR("ITEM_NO") = 1
        DR("SLIP_YEAR") = Now.Year.ToString.Substring(2, 2)
        DR("SLIP_MONTH") = Now.Month.ToString.PadLeft(2, "0")
        DR("SLIP_DAY") = Now.Day.ToString.PadLeft(2, "0")
        DR("SLIP_NO") = BL.Get_New_Confirmation_Slip_No
        DR("SLIP_CONTENT") = DBNull.Value
        If PRODUCT_ID <> 0 Then
            DR("PRODUCT_ID") = PRODUCT_ID
        Else
            DR("SIM_ID") = SIM_ID
        End If
        DR("IS_SERIAL") = IS_SERIAL
        DR("UNIT_PRICE") = PRODUCT_COST
        DR("QUANTITY") = 1
        DR("TOTAL_PRICE") = PRODUCT_COST
        DR("TMN_REQ_ID") = RESP.REQ_ID
        DR("TMN_ISV") = RESP.Request.isv_payment_ref
        DR("TMN_REQUEST_AMOUNT") = RESP.Request.request_amount
        DR("TMN_STATUS_CODE") = RESP.status.code
        DR("TMN_PAYMENT_ID") = RESP.data.payment_id
        DR("TMN_PAYMENT_CODE") = RESP.Request.payment_code
        DR("TMN_RESP_TIME") = Now
        DR("TXN_TIME") = Now

        Dim cmd As New SqlCommandBuilder(DA)
        DA.Update(DT)

        '---------------- Goto Pick ----------------
        Response.Redirect("Complete_Order.aspx?PRODUCT_ID=" & PRODUCT_ID & "&SIM_ID=" & SIM_ID)

    End Sub

    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        CloseCashReciever()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Redirect", "window.location.href='Select_Menu.aspx';", True)
    End Sub

    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click
        CloseCashReciever()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Redirect", "window.location.href='" & "Device_Shoping_Cart.aspx?PRODUCT_ID=" & PRODUCT_ID & "';", True)
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
            PRODUCT_COST = Val(DT.Rows(0).Item("PRICE").ToString)
            txtRequire.Text = FormatNumber(PRODUCT_COST, 0)
            CASH_PAID = 0
            IS_SERIAL = DT.Rows(0).Item("IS_SERIAL")
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
        End If
        PRODUCT_COST = Val(DT.Rows(0).Item("PRICE").ToString)
        txtRequire.Text = FormatNumber(PRODUCT_COST, 0)
        CASH_PAID = 0
        IS_SERIAL = True
        '---------------- Update Cost And Money --------------
    End Sub
#End Region

#Region "Cash"

    'จ่ายครบ
    Private Sub btnCashCompleted_Click(sender As Object, e As EventArgs) Handles btnCashCompleted.Click
        UpdateCashPayment()
        ''-------------------------------- Go To Pick ----------------------------------------
        Response.Redirect("Complete_Order.aspx?PRODUCT_ID=" & PRODUCT_ID & "&SIM_ID=" & SIM_ID)
    End Sub

    Private Sub btnCashTimeout_Click(sender As Object, e As EventArgs) Handles btnCashTimeout.Click
        '------------If pay some amount
        If CASH_PAID > 0 Then
            UpdateCashProblem()
            '------- ResponseRedirect ไปหน้า Problem Slip ------------
        Else
            Dim Script As String = "$(""#lnkCashTimeOut"").click();" & vbNewLine
            Script &= " showCashTimeOut();"
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CashTimeOut" & UNX(), Script, True)


            Session("ssCashTimeout") = Session("ssCashTimeout") + 1
            If Session("ssCashTimeout") > 2 Then '--ปล่อยให้ Timeout ครบ 2 ครั้งโดยไม่หยอดให้กลับหน้าหลักเลย
                Response.Redirect("Select_Language.aspx")
            End If

        End If
    End Sub

    '------------ จ่ายไม่ครบแล้ว Timeout ----------------
    Private Sub btnCashProblem_Click(sender As Object, e As EventArgs) Handles btnCashProblem.Click
        UpdateCashProblem()
    End Sub

    Private Sub UpdateCashPayment()

        '--------------- Update TB_SERVICE_TRANSACTION
        Dim SQL As String = "UPDATE TB_SERVICE_TRANSACTION SET METHOD_ID=" & VDM_BL.PaymentMethod.CASH & vbLf
        SQL &= " WHERE TXN_ID=" & TXN_ID
        BL.ExecuteNonQuery(SQL)

        '--------------- Insert TB_TRANSACTION_CASH
        SQL = "SELECT * FROM TB_TRANSACTION_CASH" & vbLf
        SQL &= " WHERE TXN_ID=" & TXN_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        Dim DR As DataRow
        If DT.Rows.Count = 0 Then
            DR = DT.NewRow
            DR("TXN_ID") = TXN_ID
            DR("ITEM_NO") = 1
            DT.Rows.Add(DR)
        Else
            DR = DT.Rows(0)
        End If
        DR("SLIP_YEAR") = Now.Year.ToString.Substring(2, 2)
        DR("SLIP_MONTH") = Now.Month.ToString.PadLeft(2, "0")
        DR("SLIP_DAY") = Now.Day.ToString.PadLeft(2, "0")
        DR("SLIP_NO") = BL.Get_New_Confirmation_Slip_No
        DR("SLIP_CONTENT") = DBNull.Value
        If PRODUCT_ID <> 0 Then
            DR("PRODUCT_ID") = PRODUCT_ID
        Else
            DR("SIM_ID") = SIM_ID
        End If
        DR("IS_SERIAL") = IS_SERIAL
        DR("UNIT_PRICE") = PRODUCT_COST
        DR("QUANTITY") = 1
        DR("TOTAL_PRICE") = PRODUCT_COST
        DR("PAID") = CASH_PAID
        DR("MUST_CHANGE") = CASH_PAID - PRODUCT_COST


        DR("TXN_TIME") = Now

        Dim cmd As New SqlCommandBuilder(DA)
        DA.Update(DT)
        '------------------- Update Money In Stock -------------
        If CInt(txt1.Text) > 0 Then
            BL.UPDATE_KIOSK_DEVICE_TRANSACTION_STOCK(KO_ID, TXN_ID, VDM_BL.Device.Coin1, CInt(txt1.Text))
        End If
        If CInt(txt2.Text) > 0 Then
            BL.UPDATE_KIOSK_DEVICE_TRANSACTION_STOCK(KO_ID, TXN_ID, VDM_BL.Device.Coin2, CInt(txt2.Text))
        End If
        If CInt(txt5.Text) > 0 Then
            BL.UPDATE_KIOSK_DEVICE_TRANSACTION_STOCK(KO_ID, TXN_ID, VDM_BL.Device.Coin5, CInt(txt5.Text))
        End If
        If CInt(txt10.Text) > 0 Then
            BL.UPDATE_KIOSK_DEVICE_TRANSACTION_STOCK(KO_ID, TXN_ID, VDM_BL.Device.Coin10, CInt(txt10.Text))
        End If
        If CInt(txt20.Text) > 0 Then
            BL.UPDATE_KIOSK_DEVICE_TRANSACTION_STOCK(KO_ID, TXN_ID, VDM_BL.Device.Cash20, CInt(txt20.Text))
        End If
        If CInt(txt50.Text) > 0 Then
            BL.UPDATE_KIOSK_DEVICE_TRANSACTION_STOCK(KO_ID, TXN_ID, VDM_BL.Device.Cash50, CInt(txt50.Text))
        End If
        If CInt(txt100.Text) > 0 Then
            BL.UPDATE_KIOSK_DEVICE_TRANSACTION_STOCK(KO_ID, TXN_ID, VDM_BL.Device.Cash100, CInt(txt100.Text))
        End If
        If CInt(txt500.Text) > 0 Then
            BL.UPDATE_KIOSK_DEVICE_TRANSACTION_STOCK(KO_ID, TXN_ID, VDM_BL.Device.Cash500, CInt(txt500.Text))
        End If
        If CInt(txt1000.Text) > 0 Then
            BL.UPDATE_KIOSK_DEVICE_TRANSACTION_STOCK(KO_ID, TXN_ID, VDM_BL.Device.Cash1000, CInt(txt1000.Text))
        End If

    End Sub

    Private Sub UpdateCashProblem()
        '-------------------- Update Stock -----------------------------
        UpdateCashPayment()
        '-------------------- Keep Transaction Log ---------------------
        Dim SQL As String = "SELECT TOP 1 * FROM TB_SERVICE_TRANSACTION_PROBLEM WHERE TXN_ID=" & TXN_ID
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Dim DR As DataRow = DT.NewRow
        DR("TXN_ID") = TXN_ID
        DR("ITEM_NO") = 1
        If PRODUCT_ID > 0 Then
            DR("PRODUCT_ID") = PRODUCT_ID
        Else
            DR("SIM_ID") = SIM_ID
        End If
        DR("SERIAL_NO") = DBNull.Value
        DR("TOTAL_PRICE") = PRODUCT_COST
        DR("CASH_PAID") = CASH_PAID
        DR("CASH_CHANGE") = 0
        DR("PROBLEM_DETAIL") = txtCashProblem.Text
        DR("SLIP_YEAR") = Now.Year.ToString.Substring(2, 2)
        DR("SLIP_MONTH") = Now.Month.ToString.PadLeft(2, "0")
        DR("SLIP_DAY") = Now.Day.ToString.PadLeft(2, "0")
        DR("SLIP_NO") = BL.Get_New_Confirmation_Slip_No
        DR("TXN_TIME") = Now
        DT.Rows.Add(DR)
        Dim cmd As New SqlCommandBuilder(DA)
        DA.Update(DT)
        '----------------- Redirect To Problem Page --------------------
        'Response.Redirect("Transaction_Problem.aspx?TXN_ID=" & TXN_ID)
    End Sub


#End Region

End Class