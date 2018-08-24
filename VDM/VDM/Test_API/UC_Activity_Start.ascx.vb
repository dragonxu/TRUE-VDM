Public Class UC_Activity_Start
    Inherits System.Web.UI.UserControl

    Dim BackEndInterface As New BackEndInterface.Activity_Start

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btn_Request_Click(sender As Object, e As EventArgs) Handles btn_Request.Click
        Try

            Dim Response As New BackEndInterface.Activity_Start.Response
            Response = BackEndInterface.Get_Result(orderId.Text)
            If Not IsNothing(Response) Then
                trx_id.Text = Response.trx_id
                status.Text = Response.status
                process_instance.Text = Response.process_instance

                message.Text = Response.display_messages(0).message
                message_type.Text = Response.display_messages(0).message_type
                en_message.Text = Response.display_messages(0).en_message
                th_message.Text = Response.display_messages(0).th_message

            End If
        Catch ex As Exception
            lblErr_Msg.Text = ex.Message.ToString()
        End Try
    End Sub

End Class