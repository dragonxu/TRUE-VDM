Public Class Device_Verify
    Inherits System.Web.UI.Page

    Private ReadOnly Property LANGUAGE As Integer
        Get
            Return Session("LANGUAGE")
        End Get
    End Property

    Private ReadOnly Property KO_ID As Integer
        Get
            Return Session("KO_ID")
        End Get
    End Property



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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlScanIDCard.Visible = True
            pnlFace_Recognition.Visible = False

            pnlModul_IDCard_Success.Visible = True

        Else

        End If



    End Sub

    Private Sub btnSkip_ScanIDCard_Click(sender As Object, e As EventArgs) Handles btnSkip_ScanIDCard.Click
        'pnlScanIDCard.Visible = False  เป็นการซื้อ Device แต่ไม่ Scan บัตร User กดเอง
        pnlScanIDCard.Visible = False
        Response.Redirect("Device_Payment.aspx?PRODUCT_ID=" & PRODUCT_ID)
        'width:400px;
    End Sub

    Private Sub btnStart_Take_Photos_Click(sender As Object, e As EventArgs) Handles btnStart_Take_Photos.Click
        pnlScanIDCard.Visible = False
        pnlModul_IDCard_Success.Visible = False
        pnlFace_Recognition.Visible = True

    End Sub

    Private Sub lnkClose_Click(sender As Object, e As EventArgs) Handles lnkClose.Click
        pnlModul_IDCard_Success.Visible = False
    End Sub


    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Home.aspx")
    End Sub

    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click
        Response.Redirect("Device_Shoping_Cart.aspx?PRODUCT_ID=" & PRODUCT_ID)
    End Sub
End Class