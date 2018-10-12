Imports System.Data.SqlClient
Public Class Payment_Gateway_Start
    Inherits System.Web.UI.Page

#Region "ส่วนที่เหมือนกันหมดทุกหน้า"

    Dim BL As New VDM_BL
    Dim BBL As New BBLPaymentGateway

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

    Private ReadOnly Property Req_amount As String
        Get
            Return Request.QueryString("amount").ToString
        End Get
    End Property

    Private ReadOnly Property rderRef As String
        Get
            Return Request.QueryString("orderRef").ToString
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        merchantId.Text = BBL.BBL_MerchantID
        amount.Text = Req_amount
        orderRef.Text = BBL.Generate_OrderRef
        currCode.Text = BBL.BBL_CurrentCode


        Dim REQ_ID As Integer = BL.Get_NewID_Log("BBL_Payment_REQ", "REQ_ID")
        successUrl.Text = BL.ServerRoot & "/FRONT_UI/Payment_Gateway_Success.aspx?REQ_ID=" & REQ_ID
        failUrl.Text = BL.ServerRoot & "/FRONT_UI/Payment_Gateway_Fail.aspx?REQ_ID=" & REQ_ID
        cancelUrl.Text = BL.ServerRoot & "/FRONT_UI/Payment_Gateway_Cancel.aspx?REQ_ID=" & REQ_ID

        '--------------- Get ProductInfo for remark-------------
        Dim DT As DataTable = BL.Get_Product_Info_From_ID(PRODUCT_ID)
        Dim _desc As String = ""
        If DT.Rows.Count > 0 Then
            _desc &= DT.Rows(0).Item("PRODUCT_CODE").ToString & " " & DT.Rows(0).Item("DISPLAY_NAME_TH").ToString
        End If
        DT = BL.GetList_Kiosk(KO_ID)
        If DT.Rows.Count > 0 Then
            _desc &= " at " & DT.Rows(0).Item("SITE_NAME").ToString & " on " & Now.ToString("dd-MMM-yyyy")
        End If
        Dim SQL As String = "SELECT dbo.FN_TXN_CODE(SITE_CODE,KO_ID,TXN_Y,TXN_M,TXN_D,TXN_N) TXN_CODE" & vbLf
        SQL &= " FROM TB_SERVICE_TRANSACTION WHERE TXN_ID=" & TXN_ID
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        DT = New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            _desc &= " ,TXN : " & DT.Rows(0).Item(0)
        End If
        If _desc = "" Then _desc = "-"
        remark.Text = _desc

        '--------------- Save Log --------------
        SQL = "SELECT TOP 0 * FROM BBL_Payment_REQ"
        DA = New SqlDataAdapter(SQL, BL.LogConnectionString)
        DT = New DataTable
        DA.Fill(DT)
        Dim DR As DataRow = DT.NewRow
        DR("REQ_ID") = REQ_ID
        DR("orderRef") = orderRef.Text
        DR("amount") = Req_amount
        DR("currCode") = currCode.Text
        DR("lang") = lang.Text
        DR("cancelURL") = cancelUrl.Text
        DR("failURL") = failUrl.Text
        DR("successURL") = successUrl.Text
        DR("merchantID") = merchantId.Text
        DR("payType") = payType.Text
        DR("payMethod") = "UPOP"
        DR("remark") = remark.Text
        DR("REQ_Time") = Now
        DT.Rows.Add(DR)
        Dim cmd As New SqlCommandBuilder(DA)
        DA.Update(DT)

        '--------------- Set Auto Link----------------
        form1.Action = BBL.BBL_PostPaymentURL

        Dim Script As String = "LinkGateway(" & REQ_ID & ");"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Gateway", Script, True)
    End Sub

End Class