Public Class FormCreditCard


    Public Property MainForm As FormMain

    Public Sub ShowPaymentForm(ByVal URL As String)
        Browser.Url = New Uri(URL)
        MainForm.Keyboard.TargetFrom = Me
        MainForm.Keyboard.Show()
        btnClose.BringToFront()
        Me.Show()
        Me.BringToFront()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        HidePaymentForm()
    End Sub

    Public Sub HidePaymentForm()
        Me.Hide()
        MainForm.Keyboard.Hide()
        MainForm.LastCreditTime = Now
    End Sub

    Private Sub Browser_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles Browser.DocumentCompleted
        Select Case True
            Case e.Url.ToString.ToUpper.IndexOf("Payment_Gateway_Cancel.aspx".ToUpper) > -1
                '------------------ Eveluate MainForm Cencel Script
                HidePaymentForm()
                MainForm.RaiseCancelCreditScript()
            Case e.Url.ToString.ToUpper.IndexOf("Payment_Gateway_Fail.aspx".ToUpper) > -1
                HidePaymentForm()
                '------------------ Eveluate MainForm Cencel Script
                MainForm.RaiseFailCreditScript()
            Case e.Url.ToString.ToUpper.IndexOf("Payment_Gateway_Success.aspx".ToUpper) > -1
                HidePaymentForm()
                '------------------- Sent REQ_ID --------------
                Dim urlPart As String() = e.Url.ToString.Split("?")
                If urlPart.Count > 1 Then
                    Dim Params As String() = urlPart(1).Split("&")
                    For i As Integer = 0 To Params.Count - 1
                        If Params(i).Split("=")(0).ToUpper = "REQ_ID" Then
                            MainForm.RaiseSuccessCreditScript(Params(i).Split("=")(1))
                        End If
                    Next
                End If
        End Select

    End Sub


End Class