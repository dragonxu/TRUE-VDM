Public Class UC_Delete_File
    Inherits System.Web.UI.UserControl

    Dim BackEndInterface As New BackEndInterface.Delete_File

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btn_Request_Click(sender As Object, e As EventArgs) Handles btn_Request.Click
        Try


            Dim Response As New BackEndInterface.Delete_File.Response
            Response = BackEndInterface.Get_Result(order_id_REQ.Text)
            If Not IsNothing(Response) Then
                trx_id.Text = Response.trx_id
                process_instance.Text = Response.process_instance
                'ref_id.Text = Response.ref_id
                order_id_RESP.Text = Response.order_id

            End If
        Catch ex As Exception
            lblErr_Msg.Text = ex.Message.ToString()
        End Try
    End Sub
End Class