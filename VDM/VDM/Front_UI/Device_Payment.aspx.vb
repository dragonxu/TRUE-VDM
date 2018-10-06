Imports System.Data.SqlClient

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

    Public ReadOnly Property PRODUCT_ID As Integer
        Get
            Try
                Return CInt(Request.QueryString("PRODUCT_ID"))
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Private Property PAYMENT_METHOD As VDM_BL.PaymentMethod
        Get
            Try
                Return ViewState("PAYMENT_METHOD")
            Catch ex As Exception
                Return VDM_BL.PaymentMethod.UNKNOWN
            End Try
        End Get
        Set(value As VDM_BL.PaymentMethod)
            ViewState("PAYMENT_METHOD") = value
        End Set
    End Property
    '    Get
    '        Dim amount As String() = {"1", "2", "5", "10", "20", "50", "100", "500", "1000"}
    '        Dim result As Integer = 0
    '        For i As Integer = 0 To amount.Count - 1
    '            result += CInt(CType(pnlCash.FindControl("txt" & amount(i)), TextBox).Text)
    '        Next
    '        Return result
    '    End Get
    'End Property

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

    Private Property SLOT_NAME As String
        Get
            Try
                Return ViewState("SLOT_NAME")
            Catch ex As Exception
                Return ""
            End Try

        End Get
        Set(value As String)
            ViewState("SLOT_NAME") = value
        End Set
    End Property

    Private Property SLOT_ID As Integer
        Get
            Try
                Return ViewState("SLOT_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(value As Integer)
            ViewState("SLOT_ID") = value
        End Set
    End Property

    Private Property POS_ID As Integer
        Get
            Try
                Return ViewState("POS_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(value As Integer)
            ViewState("POS_ID") = value
        End Set
    End Property

    Private Property SERIAL_NO As String
        Get
            Try
                Return ViewState("SERIAL_NO")
            Catch ex As Exception
                Return ""
            End Try
        End Get
        Set(value As String)
            ViewState("SERIAL_NO") = value
        End Set
    End Property


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

    'Protected Property D_ID As Integer
    '    Get
    '        Try
    '            Return Request.QueryString("D_ID")
    '        Catch ex As Exception
    '            Return 0
    '        End Try
    '    End Get
    '    Set(value As Integer)
    '        Request.QueryString("D_ID") = value
    '    End Set
    'End Property

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            ClearForm()

            If PRODUCT_ID = 0 Then
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

        PAYMENT_METHOD = VDM_BL.PaymentMethod.UNKNOWN

        pnlCash.Visible = False
        pnlCredit.Visible = False
        pnlTruemoney.Visible = False
        pnlSelectChoice.Visible = True
        'current
        lnkCash.Attributes("class") = ""
        lnkCredit.Attributes("class") = ""
        lnkTruemoney.Attributes("class") = ""

        '----------------------- Stop Focus Barcode ----------------------
        Dim Script As String = "stopFocusBarcode();" & vbLf
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "clearBarcode", Script, True)

    End Sub

    Private Sub lnkCash_ServerClick(sender As Object, e As EventArgs) Handles lnkCash.ServerClick
        ClearForm()

        PAYMENT_METHOD = VDM_BL.PaymentMethod.CASH

        pnlSelectChoice.Visible = False
        pnlCash.Visible = True
        lnkCash.Attributes("class") = "current"
        '----------------เริ่มรับชำระ --------------
        Dim Script As String = "RequireCash(); "
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CashPayment", Script, True)
    End Sub

    Private Sub lnkCredit_ServerClick(sender As Object, e As EventArgs) Handles lnkCredit.ServerClick
        ClearForm()

        PAYMENT_METHOD = VDM_BL.PaymentMethod.CREDIT_CARD

        pnlSelectChoice.Visible = False
        pnlCredit.Visible = True
        lnkCredit.Attributes("class") = "current"
    End Sub

    Private Sub lnkTruemoney_ServerClick(sender As Object, e As EventArgs) Handles lnkTruemoney.ServerClick
        ClearForm()

        PAYMENT_METHOD = VDM_BL.PaymentMethod.TRUE_MONEY

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
            Alert(Me.Page, RESP.status.message)
            Exit Sub
        End If

        '---------------- Gen SlipNo --------------------

        '---------------- Save Service Transaction-------
        Dim SQL As String = "SELECT * FROM TB_SERVICE_TRANSACTION WHERE TXN_ID=" & TXN_ID
        DT = New DataTable
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        DT.Rows(0).Item("METHOD_ID") = VDM_BL.PaymentMethod.TRUE_MONEY
        DT.Rows(0).Item("TMN_REQ_ID") = RESP.REQ_ID
        DT.Rows(0).Item("TMN_ISV") = RESP.Request.isv_payment_ref
        DT.Rows(0).Item("TMN_REQUEST_AMOUNT") = RESP.Request.request_amount
        DT.Rows(0).Item("TMN_STATUS_CODE") = RESP.status.code
        DT.Rows(0).Item("TMN_PAYMENT_ID") = RESP.data.payment_id
        DT.Rows(0).Item("TMN_RESP_TIME") = Now
        Dim cmd As New SqlCommandBuilder(DA)
        DA.Update(DT)

        '---------------- Goto Next Page ----------------
        Response.Redirect("Complete_Order.aspx?PRODUCT_ID=" & PRODUCT_ID)

    End Sub

    Private Sub btnSkip_Click(sender As Object, e As EventArgs) Handles btnSkip.Click
        Response.Redirect("Complete_Order.aspx?PRODUCT_ID=" & PRODUCT_ID)
    End Sub


    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Select_Menu.aspx")
    End Sub

    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click
        Response.Redirect("Device_Shoping_Cart.aspx?PRODUCT_ID=" & PRODUCT_ID)
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
            txtPaid.Text = 0
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
    End Sub
#End Region

#Region "Cash"
    'จ่ายครบ
    Private Sub btnCashCompleted_Click(sender As Object, e As EventArgs) Handles btnCashCompleted.Click
        '------------------- Update Information -----------------
        UpdateCashCompleted()
    End Sub

    Private Sub btnCashTimeout_Click(sender As Object, e As EventArgs) Handles btnCashTimeout.Click
        '------------If pay some amount
        If CASH_PAID > 0 Then
            UpdateCashProblem()
        End If
        Alert(Me.Page, txtCashProblem.Text)
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Refresh", "location.href=location.href;", True)
    End Sub

    Private Sub btnCashProblem_Click(sender As Object, e As EventArgs) Handles btnCashProblem.Click
        UpdateCashProblem()
        Alert(Me.Page, txtCashProblem.Text)
    End Sub

    Private Sub UpdateCashCompleted()

        Dim OrderInfo As DataTable = BL.Commit_Product_Order(TXN_ID, KO_ID, PRODUCT_ID)
        If OrderInfo.Rows(0).Item("IsProblem") Then
            txtCashProblem.Text = OrderInfo.Rows(0).Item("ProblemDetail").ToString
            UpdateCashProblem()
            Exit Sub
        End If

        SERIAL_NO = OrderInfo.Rows(0).Item("SERIAL_NO").ToString
        POS_ID = OrderInfo.Rows(0).Item("POS_ID")
        SLOT_ID = OrderInfo.Rows(0).Item("SLOT_ID")

        '------------------------- Update TB_SERVICE_TRANSACTION ------------------------
        Dim SQL As String = "SELECT * FROM TB_SERVICE_TRANSACTION WHERE TXN_ID=" & TXN_ID
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        DT.Rows(0).Item("METHOD_ID") = PAYMENT_METHOD
        DT.Rows(0).Item("CASH_PAID") = CASH_PAID
        'DT.Rows(0).Item("CASH_CHANGE") = DBNull.Value
        DT.Rows(0).Item("CASH_PROBLEM") = False
        DT.Rows(0).Item("CASH_PROBLEM_DETAIL") = ""
        Dim cmd As New SqlCommandBuilder(DA)
        DA.Update(DT)
        '--------------------------------
        Response.Redirect("Complete_Order.aspx?PRODUCT_ID=" & PRODUCT_ID & "&POS_ID=" & POS_ID & "&SERIAL_NO=" & SERIAL_NO & "&SLOT_ID=" & SLOT_ID)

    End Sub

    Private Sub UpdateCashProblem()

        Dim OrderInfo As DataTable = BL.Commit_Product_Order(TXN_ID, KO_ID, PRODUCT_ID)

        Dim SQL As String = "SELECT * FROM TB_SERVICE_TRANSACTION WHERE TXN_ID=" & TXN_ID
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        DT.Rows(0).Item("METHOD_ID") = PAYMENT_METHOD
        DT.Rows(0).Item("CASH_PAID") = CASH_PAID
        'DT.Rows(0).Item("CASH_CHANGE") = DBNull.Value
        DT.Rows(0).Item("CASH_PROBLEM") = True
        DT.Rows(0).Item("CASH_PROBLEM_DETAIL") = txtCashProblem.Text
        Dim cmd As New SqlCommandBuilder(DA)
        DA.Update(DT)
    End Sub


#End Region

End Class