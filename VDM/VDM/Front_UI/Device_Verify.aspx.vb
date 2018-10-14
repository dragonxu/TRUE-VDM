Public Class Device_Verify
    Inherits System.Web.UI.Page
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

    Private ReadOnly Property SIM_ID As Integer
        Get
            If IsNumeric(Request.QueryString("SIM_ID")) Then
                Return Request.QueryString("SIM_ID")
            Else
                Return 0
            End If
        End Get
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
        'pnlScanIDCard.Visible = False
        Response.Redirect("Device_Payment.aspx?SIM_ID=" & SIM_ID)
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
        Response.Redirect("Select_Menu.aspx")
    End Sub

    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click
        Response.Redirect("SIM_Detail.aspx?SIM_ID=" & SIM_ID)
    End Sub
End Class