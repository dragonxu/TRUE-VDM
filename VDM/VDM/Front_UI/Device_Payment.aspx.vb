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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            ClearForm()
            If PRODUCT_ID = 0 Then
                Response.Redirect("Device_Brand.aspx")
                Exit Sub
            End If
            BindProductInfo()
        Else
            initFormPlugin()
        End If
    End Sub


    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Private Sub ClearForm()
        pnlCash.Visible = False
        pnlCredit.Visible = False
        pnlTruemoney.Visible = False
        'current
        lnkCash.Attributes("class") = ""
        lnkCredit.Attributes("class") = ""
        lnkTruemoney.Attributes("class") = ""

        '----------------------- Set Alway Focus Barcode ----------------------
        Dim Script As String = "stopFocusBarcode();" & vbLf
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "clearBarcode", Script, True)

    End Sub

    Private Sub BindProductInfo()
        Dim DT As DataTable = BL.Get_Product_Info_From_ID(PRODUCT_ID)

        txtCost.Text = DT.Rows(0).Item("PRICE")
        txtRequire.Text = DT.Rows(0).Item("PRICE")
        txtPaid.Text = 0

    End Sub

    Private Sub lnkCash_ServerClick(sender As Object, e As EventArgs) Handles lnkCash.ServerClick
        ClearForm()
        pnlCash.Visible = True
        lnkCash.Attributes("class") = "current"
        '----------------เริ่มรับชำระ --------------
        txtCost.Text = 1000
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CashPayment", "RequireCash();", True)

    End Sub

    Private Sub lnkCredit_ServerClick(sender As Object, e As EventArgs) Handles lnkCredit.ServerClick
        ClearForm()
        pnlCredit.Visible = True
        lnkCredit.Attributes("class") = "current"
    End Sub

    Private Sub lnkTruemoney_ServerClick(sender As Object, e As EventArgs) Handles lnkTruemoney.ServerClick
        ClearForm()
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
        Dim RESP As TrueMoney.Response = TMN.GetResult(ISV, Amount, Barcode, PaymentDescription, ShopCode)
        '---------------- ตรวจสอบผลลัพธ์ -------------------
        If RESP.status.code.ToLower <> "success" Then
            'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "TMN", Alert(Me.);)
            Alert(Me.Page, RESP.status.message)
            Exit Sub
        End If
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
        Response.Redirect("Home.aspx")
    End Sub

    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click
        Response.Redirect("Device_Shoping_Cart.aspx?PRODUCT_ID=" & PRODUCT_ID)
    End Sub


End Class