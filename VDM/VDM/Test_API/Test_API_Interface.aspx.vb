Public Class Test_API_Interface
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            ClearForm()
        End If


    End Sub

    Private Sub ClearForm()
        pnl_face_recognition.Visible = False
        pnl_Prepaid_Validate_Register.Visible = False
        pnl_Generate_Order_Id.Visible = False
        pnl_Delete_File.Visible = False
        pnl_Save_File.Visible = False
        pnl_Service_Flow_Create.Visible = False
        pnl_Service_Flow_Finish.Visible = False
        pnl_Activity_Start.Visible = False
        pnl_Activity_End.Visible = False
        pnl_Get_Product_Info.Visible = False
        pnl_Prepaid_Register.Visible = False
        pnl_Validate_Serial.Visible = False
        pnl_Auto_Prepaid_Register.Visible = False
    End Sub

    Private Sub btnFace_Recognition_Click(sender As Object, e As EventArgs) Handles btnFace_Recognition.Click
        ClearForm()
        pnl_face_recognition.Visible = True
        Me.Page.Title = "Face_Recognition"
    End Sub
    Private Sub btn_Prepaid_Validate_Register_Click(sender As Object, e As EventArgs) Handles btn_Prepaid_Validate_Register.Click
        ClearForm()
        pnl_Prepaid_Validate_Register.Visible = True
        Me.Page.Title = "Prepaid_Validate_Register"
    End Sub
    Private Sub btn_Generate_Order_Id_Click(sender As Object, e As EventArgs) Handles btn_Generate_Order_Id.Click
        ClearForm()
        pnl_Generate_Order_Id.Visible = True
        Me.Page.Title = "Generate_Order_Id"
    End Sub

    Private Sub btn_Delete_File_Click(sender As Object, e As EventArgs) Handles btn_Delete_File.Click
        ClearForm()
        pnl_Delete_File.Visible = True
        Me.Page.Title = "Delete_File"
    End Sub

    Private Sub btn_Save_File_Click(sender As Object, e As EventArgs) Handles btn_Save_File.Click
        ClearForm()
        pnl_Save_File.Visible = True
        Me.Page.Title = "Save_File"
    End Sub

    Private Sub btn_Service_Flow_Create_Click(sender As Object, e As EventArgs) Handles btn_Service_Flow_Create.Click
        ClearForm()
        pnl_Service_Flow_Create.Visible = True
        Me.Page.Title = "Service_Flow_Create"
    End Sub

    Private Sub btn_Service_Flow_Finish_Click(sender As Object, e As EventArgs) Handles btn_Service_Flow_Finish.Click
        ClearForm()
        pnl_Service_Flow_Finish.Visible = True
        Me.Page.Title = "Service_Flow_Finish"
    End Sub

    Private Sub btn_Activity_Start_Click(sender As Object, e As EventArgs) Handles btn_Activity_Start.Click
        ClearForm()
        pnl_Activity_Start.Visible = True
        Me.Page.Title = "Activity_Start"
    End Sub

    Private Sub btn_Activity_End_Click(sender As Object, e As EventArgs) Handles btn_Activity_End.Click
        ClearForm()
        pnl_Activity_End.Visible = True
        Me.Page.Title = "Activity_End"
    End Sub

    Private Sub btn_Get_Product_Info_Click(sender As Object, e As EventArgs) Handles btn_Get_Product_Info.Click
        ClearForm()
        pnl_Get_Product_Info.Visible = True
        Me.Page.Title = "Get_Product_Info"
    End Sub

    Private Sub btn_Validate_Serial_Click(sender As Object, e As EventArgs) Handles btn_Validate_Serial.Click
        ClearForm()
        pnl_Validate_Serial.Visible = True
        Me.Page.Title = "Validate_Serial"
    End Sub

    Private Sub btn_Prepaid_Register_Click(sender As Object, e As EventArgs) Handles btn_Prepaid_Register.Click
        ClearForm()
        pnl_Prepaid_Register.Visible = True
        Me.Page.Title = "Prepaid_Register"
    End Sub

    Private Sub btn_Auto_Prepaid_Register_Click(sender As Object, e As EventArgs) Handles btn_Auto_Prepaid_Register.Click
        ClearForm()
        pnl_Auto_Prepaid_Register.Visible = True
        Me.Page.Title = "Auto_Prepaid_Register"
    End Sub
End Class