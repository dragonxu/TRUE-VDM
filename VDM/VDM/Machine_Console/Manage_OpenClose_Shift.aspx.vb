Imports System.Data.SqlClient

Public Class Manage_OpenClose_Shift
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not IsNumeric(Session("USER_ID")) Then
        '    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('กรุณาเข้าสู่ระบบ'); window.location.href='Login.aspx';", True)
        '    Exit Sub
        'End If

        If Not IsPostBack Then
            ClearForm()
            ClearMenu()
        Else
            initFormPlugin()
            pnlbtn.Visible = True
        End If
    End Sub


    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Private Sub ClearForm()
        '--เงินทอน
        divMenuChange.Visible = False
        'เงินรับ
        divMenuRecieve.Visible = False
        'Stock สินค้า
        divMenuStockProduct.Visible = False
        'Stock Sim
        divMenuStockSIM.Visible = False
        'Stock พิมพ์
        divMenuStockPaper.Visible = False
        lblConfirm.Text = ""

        Select Case Session("SHIFT_Status")
            Case VDM_BL.ShiftStatus.Close
                lblConfirm.Text = "Close Shift / ยืนยัน Close Shift"
            Case VDM_BL.ShiftStatus.Open
                lblConfirm.Text = "Open Shift / ยืนยัน Open Shift"
        End Select

    End Sub


    Private Sub ClearMenu()
        MenuChange.Attributes("class") = ""
        MenuRecieve.Attributes("class") = ""
        MenuStockProduct.Attributes("class") = ""
        MenuStockSIM.Attributes("class") = ""
        MenuStockPaper.Attributes("class") = ""

        '--เงินทอน
        pnlChange.Visible = False
        'เงินรับ
        pnlRecieve.Visible = False
        'Stock สินค้า
        pnlStockProduct.Visible = False
        'Stock Sim
        pnlStockSIM.Visible = False
        'Stock พิมพ์
        pnlStockPaper.Visible = False
        pnlbtn.Visible = False
        lnkOK.Visible = True
    End Sub

    Private Sub SetNextForm()

        If pnlChange.Visible Then
            ClearMenu()
            lnkRecieve_ServerClick(Nothing, Nothing)
        ElseIf pnlRecieve.Visible Then
            ClearMenu()
            lnkStockProduct_ServerClick(Nothing, Nothing)
        ElseIf pnlStockProduct.Visible Then
            ClearMenu()
            lnkStockSIM_ServerClick(Nothing, Nothing)
        ElseIf pnlStockSIM.Visible Then
            ClearMenu()
            lnkStockPaper_ServerClick(Nothing, Nothing)

            'ElseIf pnlStockPaper.Visible Then

        End If

    End Sub

    Private Sub SetSummaryMenu()
        '----แสดงสรุปแต่ละเมนู หลังจากกด Next

        '----เงินทอน
        If Val(UC_Shift_Change.Total) > 0 Then
            divMenuChange.Visible = True
            lbl_Change_Amount.Text = FormatNumber(UC_Shift_Change.Total, 0)
        Else
            divMenuChange.Visible = False
        End If
        '----เงินรับ 
        If Val(UC_Shift_Recieve.Total) > 0 Then
            divMenuRecieve.Visible = True
            lbl_Recieve_Amount.Text = FormatNumber(UC_Shift_Recieve.Total, 0)
        Else
            divMenuRecieve.Visible = False
        End If
        ''----Product 
        'If Val(UC_Shift_StockProduct.Total) > 0 Then
        '    divMenuStockProduct.Visible = True
        '    lbl_Product_Count.Text = ""
        'Else
        '    divMenuStockProduct.Visible = False
        'End If
        ''----SIM 
        'If Val(UC_Shift_StockSIM.Total) > 0 Then
        '    divMenuStockSIM.Visible = True
        '    lbl_SIM_Count.Text = ""
        'Else
        '    divMenuStockSIM.Visible = False
        'End If
        '----Paper 
        If Val(UC_Shift_StockPaper.Total) > 0 Then
            divMenuStockPaper.Visible = True
            lbl_Paper_Count.Text = FormatNumber(UC_Shift_StockPaper.Total, 0)
        Else
            divMenuStockPaper.Visible = False
        End If
    End Sub
    Private Sub lnkChange_ServerClick(sender As Object, e As EventArgs) Handles lnkChange.ServerClick
        ClearMenu()
        MenuChange.Attributes("class") = "active"
        pnlChange.Visible = True
        pnlbtn.Visible = True
        SetSummaryMenu()

    End Sub

    Private Sub lnkRecieve_ServerClick(sender As Object, e As EventArgs) Handles lnkRecieve.ServerClick
        ClearMenu()
        MenuRecieve.Attributes("class") = "active"
        pnlRecieve.Visible = True
        pnlbtn.Visible = True
        SetSummaryMenu()

    End Sub

    Private Sub lnkStockProduct_ServerClick(sender As Object, e As EventArgs) Handles lnkStockProduct.ServerClick
        ClearMenu()
        MenuStockProduct.Attributes("class") = "active"
        pnlStockProduct.Visible = True
        pnlbtn.Visible = True
        SetSummaryMenu()


    End Sub

    Private Sub lnkStockSIM_ServerClick(sender As Object, e As EventArgs) Handles lnkStockSIM.ServerClick
        ClearMenu()
        MenuStockSIM.Attributes("class") = "active"
        pnlStockSIM.Visible = True
        pnlbtn.Visible = True
        SetSummaryMenu()


    End Sub


    Private Sub lnkStockPaper_ServerClick(sender As Object, e As EventArgs) Handles lnkStockPaper.ServerClick
        ClearMenu()
        MenuStockPaper.Attributes("class") = "active"
        pnlStockPaper.Visible = True
        pnlbtn.Visible = True
        lnkOK.Visible = False
        SetSummaryMenu()



    End Sub

    Private Sub lnkBack_ServerClick(sender As Object, e As EventArgs) Handles lnkBack.ServerClick
        Response.Redirect("Machine_Overview.aspx")
    End Sub

    Private Sub lnkOK_Click(sender As Object, e As EventArgs) Handles lnkOK.Click
        '---Action
        SetSummaryMenu()   '----แสดงจำนวนเงิน ที่ทำแต่ละเมนู


        '---ClickNext
        SetNextForm()

    End Sub

    Private Sub lnkConfirm_Click(sender As Object, e As EventArgs) Handles lnkConfirm.Click
        Dim Validate As Boolean = False
        Dim ShiftChange_Success As Boolean = False
        Dim ShiftRecieve_Success As Boolean = False
        Dim ShiftStockPaper_Success As Boolean = False

        Try
            '--validate
            '----เงินทอน
            If UC_Shift_Change.Validate Then
                Validate = True
            Else
                ClearMenu()
                lnkChange_ServerClick(Nothing, Nothing)
                Exit Sub
            End If
            '----เงินรับ 
            If UC_Shift_Recieve.Validate Then
                Validate = True
            Else
                ClearMenu()
                lnkRecieve_ServerClick(Nothing, Nothing)
                Exit Sub
            End If
            '----Product

            '----SIM

            '----Paper
            If UC_Shift_StockPaper.Validate Then
                Validate = True
            Else
                ClearMenu()
                lnkStockPaper_ServerClick(Nothing, Nothing)
                Exit Sub
            End If


            Try
                '--Save
                If Validate Then
                    ShiftChange_Success = UC_Shift_Change.Save
                    ShiftRecieve_Success = UC_Shift_Recieve.Save

                    ShiftStockPaper_Success = UC_Shift_StockPaper.Save
                End If
            Catch ex As Exception
                Alert(Me.Page, ex.Message)
                Exit Sub
            End Try


            '--Update TB_KIOSK_DEVICE Current,Status sp
            '--สั่ง Open/Close Shift

            Alert(Me.Page, "บันทึกสำเร็จ")


        Catch ex As Exception
            Alert(Me.Page, ex.Message)
            Exit Sub
        End Try



    End Sub
End Class