﻿Public Class UC_Service_Flow_Finish
    Inherits System.Web.UI.UserControl

    Dim BackEndInterface As New BackEndInterface.Service_Flow_Finish

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btn_Request_Click(sender As Object, e As EventArgs) Handles btn_Request.Click
        Try


            Dim Response As New BackEndInterface.Service_Flow_Finish.Response
            Response = BackEndInterface.Get_Result(orderId.Text)
            If Not IsNothing(Response) Then

                status.Text = Response.status

                message.Text = Response.display_messages(0).message
                message_type.Text = Response.display_messages(0).message_type
                en_message.Text = Response.display_messages(0).en_message
                th_message.Text = Response.display_messages(0).th_message

                trx_id.Text = Response.trx_id
                process_instance.Text = Response.process_instance

                'response_data
                'THAI_ID.Text = Response.response_data.data.THAI_ID
                'LANG.Text = Response.response_data.data.LANG

                flow_id.Text = Response.response_data.flow_id
                flow_name.Text = Response.response_data.flow_name
                create_date.Text = Response.response_data.create_date
                create_by.Text = Response.response_data.create_by
                end_date.Text = Response.response_data.end_date
                end_by.Text = Response.response_data.end_by
            End If
        Catch ex As Exception
            lblErr_Msg.Text = ex.Message.ToString()
        End Try
    End Sub
End Class