Public Class UC_Activity_Start
    Inherits System.Web.UI.UserControl

    Dim BackEndInterface As New BackEndInterface.Activity_Start

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btn_Request_Click(sender As Object, e As EventArgs) Handles btn_Request.Click
        Try
            BackEndInterface.Get_Result(orderId.Text)

            Dim Response As New BackEndInterface.Activity_Start.Response
            If Not IsNothing(Response) Then
                trx_id.Text = Response.trx_id
                status.Text = Response.status
                process_instance.Text = Response.process_instance

                Dim Display_messages As New BackEndInterface.Activity_Start.Response.Display_Message
                message.Text = Display_messages.message
                message_type.Text = Display_messages.message_type
                en_message.Text = Display_messages.en_message
                th_message.Text = Display_messages.th_message

            End If
        Catch ex As Exception
            lblErr_Msg.Text = ex.Message.ToString()
        End Try
    End Sub

End Class