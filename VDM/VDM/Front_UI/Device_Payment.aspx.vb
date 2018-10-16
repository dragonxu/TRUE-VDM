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
            Return txtPaid.Text
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            txtLocalControllerURL.Text = BL.LocalControllerURL

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

    Private Sub ClearForm()

        'PAYMENT_METHOD = VDM_BL.PaymentMethod.UNKNOWN

        pnlCash.Visible = False
        pnlCredit.Visible = False
        pnlTruemoney.Visible = False
        pnlSelectChoice.Visible = True
        'current
        lnkCash.Attributes("class") = ""
        lnkCredit.Attributes("class") = ""
        lnkTruemoney.Attributes("class") = ""
        txtBarcode.Text = ""

        '----------------------- Stop Focus Barcode ----------------------
        Dim Script As String = "stopFocusBarcode();" & vbLf
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
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CashPayment", Script, True)
    End Sub

    Private Sub lnkCredit_ServerClick(sender As Object, e As EventArgs) Handles lnkCredit.ServerClick
        ClearForm()

        pnlSelectChoice.Visible = False
        pnlCredit.Visible = True
        lnkCredit.Attributes("class") = "current"

        '-------------- Set JS Leyboard -----------------
        paymentGatewayWindow.Attributes("onload") = "impletmentKeyboard()"

        Dim url As String = "Payment_Gateway_Start.aspx?amount=" & PRODUCT_COST & ".00&PRODUCT_ID=" & PRODUCT_ID
        paymentGatewayWindow.Src = url

    End Sub

    Private Sub lnkCloseCredit_Click(sender As Object, e As EventArgs) Handles lnkCloseCredit.Click
        pnlCredit.Visible = False
    End Sub

    Private Sub btnCreditComplete_Click(sender As Object, e As EventArgs) Handles btnCreditComplete.Click
        '--------------- Save Transaction Requested---------------
        Dim Sql As String = "SELECT TOP 1 * FROM TB_TRANSACTION_CREDITCARD WHERE TXN_ID=" & TXN_ID
        Dim DA As New SqlDataAdapter(Sql, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Dim DR As DataRow
        If DT.Rows.Count = 0 Then
            DR = DT.NewRow
            DT.Rows.Add(DR)
            '    DR("TXN_ID") = TXN_ID
            '    DR("ITEM_NO") = 1
            '    DR("SLIP_YEAR") = xxxxxxxxxxxxxxxxx
            '    DR("SLIP_MONTH") = xxxxxxxxxxxxxxxxx
            '    DR("SLIP_DAY") = xxxxxxxxxxxxxxxxx
            '    DR("SLIP_NO") = xxxxxxxxxxxxxxxxx
            '    DR("SLIP_CONTENT") = xxxxxxxxxxxxxxxxx
            '    DR("PRODUCT_ID") = PRODUCT_ID
            '    DR("IS_SERIAL") = xxxxxxxxxxxxxxxxx
            '    DR("UNIT_PRICE") = xxxxxxxxxxxxxxxxx
            '    DR("QUANTITY") = 1
            '    DR("TOTAL_PRICE") = xxxxxxxxxxxxxxxxx
            '    DR("BBL_REQ_ID") = xxxxxxxxxxxxxxxxx
            '    DR("BBL_ORDER_Y") = xxxxxxxxxxxxxxxxx
            '    DR("BBL_ORDER_M") = xxxxxxxxxxxxxxxxx
            '    DR("BBL_ORDER_D") = xxxxxxxxxxxxxxxxx
            '    DR("BBL_ORDER_N") = xxxxxxxxxxxxxxxxx
            '    DR("BBL_REQ_TIME") = xxxxxxxxxxxxxxxxx
            '    DR("BBL_RESP_TIME") = xxxxxxxxxxxxxxxxx
            '    DR("TXN_TIME") = Now
        Else
            DR = DT.Rows(0)
        End If
    End Sub


    Private Sub lnkTruemoney_ServerClick(sender As Object, e As EventArgs) Handles lnkTruemoney.ServerClick
        ClearForm()

        pnlSelectChoice.Visible = False
        pnlTruemoney.Visible = True
        lnkTruemoney.Attributes("class") = "current"

        '----------------------- Set Alway Focus Barcode ----------------------
        Dim Script As String = "txtBarcode='" & txtBarcode.ClientID & "';" & vbLf
        Script &= "startFocusBarcode();"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "focusBarcodeReader", Script, True)
    End Sub

    Private Sub btnBarcode_Click(sender As Object, e As EventArgs) Handles btnBarcode.Click
        ' Call True Money
        If txtBarcode.Text = "" Then Exit Sub
        Dim Barcode As String = txtBarcode.Text
        txtBarcode.Text = ""

        Dim TMN As New TrueMoney
        '------------------- Get Parameter --------------
        Dim DT As DataTable = BL.GetList_Kiosk(KO_ID)
        Dim ShopCode As String = DT.Rows(0).Item("SITE_CODE")
        DT = BL.Get_Product_Info_From_ID(PRODUCT_ID)
        Dim Amount As Integer = DT.Rows(0).Item("PRICE")
        Dim ISV As String = TMN.Generate_ISV(ShopCode)
        Dim PaymentDescription As String = "TRUE-VDM-" & ISV
        '-------------------- เรียก------------------------
        Dim RESP As TrueMoney.Response = TMN.GetResult(TXN_ID, ISV, Amount, Barcode, PaymentDescription, ShopCode)
        '---------------- ตรวจสอบผลลัพธ์ -------------------
        If RESP.status.code.ToLower <> "success" Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "TrueMoneyError", "$('#lnkTrueMoney').click();", True)
            txtBarcode.Text = ""
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

        '---------------- Goto Next Page ----------------
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
            PRODUCT_COST = DT.Rows(0).Item("PRICE")
            txtRequire.Text = FormatNumber(DT.Rows(0).Item("PRICE"), 0)
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
        PRODUCT_COST = DT.Rows(0).Item("PRICE")
        txtRequire.Text = FormatNumber(DT.Rows(0).Item("PRICE"), 0)
        CASH_PAID = 0
        IS_SERIAL = True
        '---------------- Update Cost And Money --------------
    End Sub
#End Region

#Region "Cash"

    'จ่ายครบ
    Private Sub btnCashCompleted_Click(sender As Object, e As EventArgs) Handles btnCashCompleted.Click
        UpdateCashCompleted()
    End Sub

    Private Sub btnCashTimeout_Click(sender As Object, e As EventArgs) Handles btnCashTimeout.Click
        '------------If pay some amount
        'If CASH_PAID > 0 Then UpdateCashProblem()
        Alert(Me.Page, txtCashProblem.Text)
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Refresh", "location.href=location.href;", True)
    End Sub

    Private Sub btnCashProblem_Click(sender As Object, e As EventArgs) Handles btnCashProblem.Click
        'UpdateCashProblem()
        'Alert(Me.Page, txtCashProblem.Text)
    End Sub

    Private Sub UpdateCashCompleted()

        '--------------- Update TB_SERVICE_TRANSACTION
        Dim SQL As String = "UPDATE TB_SERVICE_TRANSACTION SET METHOD_ID=" & VDM_BL.PaymentMethod.CASH & vbLf
        SQL &= " WHERE TXN_ID=" & TXN_ID
        BL.ExecuteNonQuery(SQL)

        '--------------- Insert TB_TRANSACTION_CASH
        SQL = "SELECT TOP 1 * FROM TB_TRANSACTION_CASH" & vbLf
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

        ''--------------------------------
        Response.Redirect("Complete_Order.aspx?PRODUCT_ID=" & PRODUCT_ID & "&SIM_ID=" & SIM_ID)

    End Sub

    Private Sub UpdateCashProblem()

    End Sub






#End Region

End Class