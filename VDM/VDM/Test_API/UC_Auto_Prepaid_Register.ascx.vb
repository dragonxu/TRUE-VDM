Public Class UC_Auto_Prepaid_Register
    Inherits System.Web.UI.UserControl

    Dim BackEndInterface As New BackEndInterface.Register

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btn_Request_Click(sender As Object, e As EventArgs) Handles btn_Request.Click

        'Dim DT_Customer_Info As New DataTable

        'DT_Customer_Info.Columns.Add("CUS_ID")
        'DT_Customer_Info.Columns.Add("CUS_TITLE")
        'DT_Customer_Info.Columns.Add("CUS_NAME")
        'DT_Customer_Info.Columns.Add("CUS_SURNAME")
        'DT_Customer_Info.Columns.Add("NAT_CODE")
        'DT_Customer_Info.Columns.Add("CUS_GENDER")
        'DT_Customer_Info.Columns.Add("CUS_BIRTHDATE")
        'DT_Customer_Info.Columns.Add("CUS_PID")
        'DT_Customer_Info.Columns.Add("CUS_PASSPORT_ID")
        'DT_Customer_Info.Columns.Add("CUS_PASSPORT_START")
        'DT_Customer_Info.Columns.Add("CUS_PASSPORT_EXPIRE")
        'DT_Customer_Info.Rows.Add(1, "นางสาว", "ธัญลักษณ์", "เจ๊ะหล้า", "TH", 2, "1990-02-05", "1939900150601", DBNull.Value, "2018-02-05", "2022-02-05")

        Dim Response As New BackEndInterface.Register
        Dim ResponseCommand As New BackEndInterface.Register.Command_Result
        Dim Result As Boolean = False
        Try
            Result = BackEndInterface.Get_Result(Face_cust_certificate.Text, Face_cust_capture.Text, SIM_Serial.Text, KO_ID.Text, USER_ID.Text, TXN_ID.Text)

            If Result Then
                ReturnResult.Text = "True"
            Else
                ReturnResult.Text = "False"
            End If
            status.Text = ResponseCommand.Status
            Message.Text = ResponseCommand.Message

        Catch ex As Exception

            status.Text = ResponseCommand.Status
            Message.Text = ResponseCommand.Message
        End Try






    End Sub
End Class