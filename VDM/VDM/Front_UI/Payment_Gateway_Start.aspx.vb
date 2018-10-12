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

        successUrl.Text = BL.ServerRoot & "/xxxxxxxxxxxxxx"
        failUrl.Text = BL.ServerRoot & "/xxxxxxxxxxxxxx"
        cancelUrl.Text = BL.ServerRoot & "/xxxxxxxxxxxxxx"

        form1.Action = BBL.BBL_PostPaymentURL

        Dim Script As String = "document.getElementById('" & btnOK.ClientID & "').click();"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "go", Script, True)
    End Sub

End Class