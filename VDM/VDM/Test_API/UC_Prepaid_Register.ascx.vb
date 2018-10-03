Public Class UC_Prepaid_Register
    Inherits System.Web.UI.UserControl
    Dim BackEndInterface As New BackEndInterface.Prepaid_Register

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btn_Request_Click(sender As Object, e As EventArgs) Handles btn_Request.Click
        Try
            Dim Response As New BackEndInterface.Prepaid_Register.Response
            Response = BackEndInterface.Get_Result(OrderID.Text,
                customer_gender.SelectedValue,
                customer_title.Text,
                customer_language.Text,
                customer_title_code.Text,
                customer_firstname.Text,
                customer_lastname.Text,
                customer_birthdate.Text,
                customer_id_number.Text,
                customer_id_expire_date.Text,
                address_number.Text,
                address_moo.Text,
                address_village.Text,
                address_street.Text,
                address_soi.Text,
                address_district.Text,
                address_province.Text,
                address_building_name.Text,
                address_building_room.Text,
                address_building_floor.Text,
                sddress_sub_district.Text,
                address_zip.Text,
                shopCode.Text,
            shopName.Text,
            sale_code.Text,
                mat_code.Text,
                mat_desc.Text,
                sim_serial.Text,
                require_print_form.SelectedValue,
                price_plan.Text,
                subscriber.Text,
                is_registered.SelectedValue,
                sub_activity.SelectedValue,
                company_code.Text)


            If Not IsNothing(Response) Then

                status.Text = Response.status
                trx_id.Text = Response.trx_id
                process_instance.Text = Response.process_instance
                'response_data.Text = Response.response_data

                '------------------------------------------------------------
                'display_messages
                message.Text = Response.display_messages(0).message
                'message_code.Text = Response.display_messages(0).message_code
                message_type.Text = Response.display_messages(0).message_type
                en_message.Text = Response.display_messages(0).en_message
                th_message.Text = Response.display_messages(0).th_message
                'technical_message.Text = Response.display_messages(0).technical_message
                'fault
                'name.Text = Response.fault.name
                'code.Text = Response.fault.code
                'messagefault.Text = Response.fault.message
                'detailed_message.Text = Response.fault.detailed_message

                ''Respobse ที่ได้
                '{
                '  "status" : "SUCCESSFUL",
                '  "display-messages" : [ {
                '    "message" : "Order 18100200TLR030020021 successful submitted.",
                '    "message-type" : "INFORMATION",
                '    "en-message" : "Order 18100200TLR030020021 successful submitted.",
                '    "th-message" : "รายการคำขอเลขที่ 18100200TLR030020021 ได้รับข้อมูลเรียบร้อยแล้ว"
                '  } ],
                '  "trx-id" : "4370IPDCBVCJL",
                '  "process-instance" : "tmsapnpr1 (instance: SFF_node3)"
                '}
            End If
        Catch ex As Exception
            lblErr_Msg.Text = ex.Message.ToString()
        End Try
    End Sub
End Class