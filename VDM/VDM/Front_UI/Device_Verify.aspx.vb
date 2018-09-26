Public Class Device_Verify
    Inherits System.Web.UI.Page

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
        Response.Redirect("Device_Payment.aspx")
    End Sub

    Private Sub btnStart_Take_Photos_Click(sender As Object, e As EventArgs) Handles btnStart_Take_Photos.Click
        pnlScanIDCard.Visible = False
        pnlModul_IDCard_Success.Visible = False
        pnlFace_Recognition.Visible = True

    End Sub

    Private Sub lnkClose_Click(sender As Object, e As EventArgs) Handles lnkClose.Click
        pnlModul_IDCard_Success.Visible = False
    End Sub
End Class