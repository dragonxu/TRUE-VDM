Public Class UC_Prepaid_Validate_Register
    Inherits System.Web.UI.UserControl

    Dim BackEndInterface As New BackEndInterface.Prepaid_Validate_Register

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btn_Request_Click(sender As Object, e As EventArgs) Handles btn_Request.Click
        BackEndInterface.Get_Result(key_type.Text, key_value.Text, id_number.Text, id_type.Text)

        Dim Response As New BackEndInterface.Prepaid_Validate_Register.Response
        If Not IsNothing(Response) Then

            status.Text = Response.status
            trx_id.Text = Response.trx_id
            process_instance.Text = Response.process_instance
            'response_data
            subscriber.Text = Response.response_data.subscriber
            sim.Text = Response.response_data.sim
            imsi.Text = Response.response_data.imsi
            sim_category.Text = Response.response_data.sim_category
            priceplan.Text = Response.response_data.priceplan
            company_code.Text = Response.response_data.company_code
            is_registered.Text = Response.response_data.is_registered
            firstname.Text = Response.response_data.firstname
            lastname.Text = Response.response_data.lastname

        End If
    End Sub

End Class