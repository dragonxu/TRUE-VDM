Imports System.Data.SqlClient

Public Class Machine_Overview
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL

    Private ReadOnly Property KO_ID As Integer
        Get
            Return Request.Cookies("KO_ID").Value
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNumeric(Session("USER_ID")) Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('กรุณาเข้าสู่ระบบ'); window.location.href='Login.aspx';", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            SetMachineInfo()
            SetProductShelf()
            BindSIM()
        Else

        End If


    End Sub

    Private Sub SetProductShelf()
        Shelf.PixelPerMM = 0.25
        Shelf.KO_ID = KO_ID
        Shelf.BindData()
    End Sub

    Private Sub btnResetProduct_Click(sender As Object, e As EventArgs) Handles btnResetProduct.Click
        Shelf.ResetProperty()
        SetProductShelf()
    End Sub

    Private Sub btnSaveProduct_Click(sender As Object, e As EventArgs) Handles btnSaveProduct.Click
        Shelf.KO_ID = KO_ID
        If Not Shelf.SaveData Then
            Message_Toastr("ไม่สามารถบันทึก Layout ของ Product Shelf", ToastrMode.Danger, ToastrPositon.TopRight, Me.Page)
        Else
            Message_Toastr("บันทึกสำเร็จ", ToastrMode.Success, ToastrPositon.TopRight, Me.Page)
            Shelf.ResetProperty()
            SetProductShelf()
        End If
    End Sub

    Private Sub BindSIM()
        Dispenser.KO_ID = KO_ID
        BL.Bind_SIMDispenser_Layout(Dispenser, KO_ID, 4)
        Dim STOCK_Data As DataTable = BL.Get_Current_SIM_Stock(KO_ID)
        STOCK_Data.Columns("CURRENT").ColumnName = "SLOT_NAME"
        BL.Bind_SIMDispenser_Stock(Dispenser, STOCK_Data)
        For i As Integer = 0 To Dispenser.Slots.Count - 1
            Dispenser.Slots(i).Column12Style = 2
        Next
    End Sub

    Private Sub SetMachineInfo()
        '---Peripheral UI ---------------
        Dim DeviceList As DataTable
        Dim StatusList As DataTable
        DeviceList = BL.GetList_Kiosk_Device(KO_ID)
        StatusList = BL.GetList_Device_Status
        UC_Peripheral_UI.BindPeripheral(DeviceList, StatusList)

        '---Stock
        DeviceList.DefaultView.RowFilter = " Max_Qty IS NOT NULL AND Warning_Qty IS NOT NULL AND Critical_Qty IS NOT NULL"
        UC_MoneyStock_UI.BindMoneyStock(DeviceList.DefaultView.ToTable.Copy)

        '---Product


    End Sub

    Private Sub lnkShift_Click(sender As Object, e As EventArgs) Handles lnkShift.Click
        Response.Redirect("Manage_OpenClose_Shift.aspx")
    End Sub


End Class