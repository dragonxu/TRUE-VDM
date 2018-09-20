Imports System.Data.SqlClient

Public Class Machine_Overview
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL

    Private ReadOnly Property KO_ID As Integer
        Get
            Return Session("KO_ID")
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
            Alert(Me.Page, "ไม่สามารถบันทึก Layout ของ Product Shelf")
        Else
            Alert(Me.Page, "บันทึกสำเร็จ")
            Shelf.ResetProperty()
            SetProductShelf()
        End If
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