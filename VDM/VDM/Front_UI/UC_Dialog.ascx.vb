Public Class UC_Dialog
    Inherits System.Web.UI.UserControl
    Dim BL As New VDM_BL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Public Sub Set_Dialog(ByVal HeaderMessage As String, ByVal ImageMode As String, ByVal DetailMessage As String, ByVal StrButton As String, Optional ByRef IsVisible As Boolean = False)
        lbl_HeaderMessage.Text = HeaderMessage.ToString
        imgAlert.Attributes("src") = ImageMode.ToString()
        lbl_DetailMessage.Text = DetailMessage.ToString
        btnClose_Dialog.Text = StrButton.ToString
        pnlModul.Visible = IsVisible
    End Sub

    Private Sub btnClose_Dialog_Click(sender As Object, e As EventArgs) Handles btnClose_Dialog.Click
        pnlModul.Visible = False
    End Sub
End Class