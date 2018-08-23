Public Class UC_Face_Recognition
    Inherits System.Web.UI.UserControl

    Dim BackEndInterface As New BackEndInterface.Face_Recognition

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btn_Request_Click(sender As Object, e As EventArgs) Handles btn_Request.Click
        Try


            BackEndInterface.Get_Result(partner_code.Text, id_number.Text, face_recog_cust_certificate.Text, face_recog_cust_capture.Text, seq.Text)

            Dim Response As New BackEndInterface.Face_Recognition.Response
            If Not IsNothing(Response) Then

                status.Text = Response.status
                trx_id.Text = Response.trx_id
                process_instance.Text = Response.process_instance

                ' response_data
                face_recognition_result.Text = Response.response_data.face_recognition_result
                face_recognition_message.Text = Response.response_data.face_recognition_message
                over_max_allow.Text = Response.response_data.over_max_allow
                over_max_allow_message.Text = Response.response_data.over_max_allow_message
                is_identical.Text = Response.response_data.is_identical
                confident_ratio.Text = Response.response_data.confident_ratio
                face_recog_cust_certificate_id.Text = Response.response_data.face_recog_cust_certificate_id
                face_recog_cust_capture_id.Text = Response.response_data.face_recog_cust_capture_id

            End If
        Catch ex As Exception
            lblErr_Msg.Text = ex.Message.ToString()
        End Try
    End Sub

End Class