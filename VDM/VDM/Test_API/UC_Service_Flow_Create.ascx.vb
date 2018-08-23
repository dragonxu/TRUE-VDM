Public Class UC_Service_Flow_Create
    Inherits System.Web.UI.UserControl
    Dim BackEndInterface As New BackEndInterface.Service_Flow_Create
    Dim BE As New BackEndInterface.Service_Flow_Create

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btn_Request_Click(sender As Object, e As EventArgs) Handles btn_Request.Click
        BackEndInterface.Get_Result(
                                    orderId.Text,
                                    FlowType.Text,
                                    staffOpenShift.Text,
                                    thaiID.Text,
                                    subscriber.Text,
                                    shopCode.Text,
                                    customerName.Text,
                                    proposition.Text,
                                    pricePlan.Text,
                                    sim.Text,
                                    saleCode.Text,
                                    face_recognition_result.Text,
                                    is_identical.Text,
                                    confident_ratio.Text)

        Dim Response As New BackEndInterface.Service_Flow_Create.Response
        If Not IsNothing(Response) Then

            status.Text = Response.status

            Dim Display_messages As New BackEndInterface.Service_Flow_Create.Response.Display_Message
            message.Text = Display_messages.message
            message_type.Text = Display_messages.message_type
            en_message.Text = Display_messages.en_message
            th_message.Text = Display_messages.th_message

            trx_id.Text = Response.trx_id
            process_instance.Text = Response.process_instance

            'response_data
            'Dim Display_messages As New BackEndInterface.Service_Flow_Create.Response.Display_Message

            'THAI_ID


        End If
    End Sub
End Class