Public Class Test_API_Interface
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
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


    End Sub

    Private Sub btnFace_Recognition_Click(sender As Object, e As EventArgs) Handles btnFace_Recognition.Click
        ClearForm()
        pnl_face_recognition.Visible = True
    End Sub
    Private Sub btn_Prepaid_Validate_Register_Click(sender As Object, e As EventArgs) Handles btn_Prepaid_Validate_Register.Click
        ClearForm()
        pnl_Prepaid_Validate_Register.Visible = True
    End Sub
    Private Sub btn_Generate_Order_Id_Click(sender As Object, e As EventArgs) Handles btn_Generate_Order_Id.Click
        ClearForm()
        pnl_Generate_Order_Id.Visible = True
    End Sub

    Private Sub btn_Delete_File_Click(sender As Object, e As EventArgs) Handles btn_Delete_File.Click
        ClearForm()
        pnl_Delete_File.Visible = True
    End Sub

    Private Sub btn_Save_File_Click(sender As Object, e As EventArgs) Handles btn_Save_File.Click
        ClearForm()
        pnl_Save_File.Visible = True
    End Sub

    Private Sub btn_Service_Flow_Create_Click(sender As Object, e As EventArgs) Handles btn_Service_Flow_Create.Click
        ClearForm()
        pnl_Service_Flow_Create.Visible = True
    End Sub

    Private Sub btn_Service_Flow_Finish_Click(sender As Object, e As EventArgs) Handles btn_Service_Flow_Finish.Click
        ClearForm()
        pnl_Service_Flow_Finish.Visible = True
    End Sub

    Private Sub btn_Activity_Start_Click(sender As Object, e As EventArgs) Handles btn_Activity_Start.Click
        ClearForm()
        pnl_Activity_Start.Visible = True
    End Sub

    Private Sub btn_Activity_End_Click(sender As Object, e As EventArgs) Handles btn_Activity_End.Click
        ClearForm()
        pnl_Activity_End.Visible = True
    End Sub

    Private Sub btn_Get_Product_Info_Click(sender As Object, e As EventArgs) Handles btn_Get_Product_Info.Click
        ClearForm()
        pnl_Get_Product_Info.Visible = True
    End Sub


    Private Sub btn_Prepaid_Register_Click(sender As Object, e As EventArgs) Handles btn_Prepaid_Register.Click
        ClearForm()
        pnl_Prepaid_Register.Visible = True
    End Sub

End Class