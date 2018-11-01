Public Class UC_Validate_Serial
    Inherits System.Web.UI.UserControl

    Dim BackEndInterface As New BackEndInterface.Validate_Serial

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then ClearResponse()
    End Sub

    Private Sub btn_Request_Click(sender As Object, e As EventArgs) Handles btn_Request.Click

        ClearResponse()
        Try
            Dim Response As New BackEndInterface.Validate_Serial.Response
            Response = BackEndInterface.Get_Result(Shop.Text, Serial.Text)
            If Not IsNothing(Response) Then
                If Response.ReturnValues.Count = 2 Then
                    CODE.Text = Response.ReturnValues(0).ToString
                    IS_SIM.Text = Response.ReturnValues(1).ToString
                End If
                IsError.Text = Response.IsError
                ErrorMessage.Text = Response.ErrorMessage
                IsNotTransaction.Text = Response.IsNotTransaction.ToString


                If Not IsNothing(Response.errCode) Then
                    errCode.Text = Response.errCode
                End If
                If Not IsNothing(Response.errMsg) Then
                    errMsgtrx_ID.Text = Response.errMsg.trx_id
                    errMsgstatus.Text = Response.errMsg.status
                    errMsgprocess_instance.Text = Response.errMsg.process_instance
                    If Not IsNothing(Response.errMsg.fault) Then
                        faultname.Text = Response.errMsg.fault.name
                        faultcode.Text = Response.errMsg.fault.code
                        faultmessage.Text = Response.errMsg.fault.message
                        faultdetailed_message.Text = Response.errMsg.fault.detailed_message
                    End If
                End If
                lbljson.Text = Response.JSONString

            End If

        Catch ex As Exception
            lblErr_Msg.Text = ex.Message
        End Try
    End Sub

    Private Sub ClearResponse()
        CODE.Text = ""
        IS_SIM.Text = ""
        IsError.Text = ""
        ErrorMessage.Text = ""
        IsNotTransaction.Text = ""
    End Sub
End Class