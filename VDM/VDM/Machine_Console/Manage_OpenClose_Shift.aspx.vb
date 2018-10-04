Imports System.Data.SqlClient

Public Class Manage_OpenClose_Shift
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL

    Private ReadOnly Property KO_ID As Integer
        Get
            Return Request.Cookies("KO_ID").Value
        End Get
    End Property

    Private ReadOnly Property SHOP_CODE As String
        Get
            Return Session("SHOP_CODE")
        End Get
    End Property

    Private Property SHIFT_Status As VDM_BL.ShiftStatus
        Get
            Try
                Return Session("SHIFT_Status")
            Catch ex As Exception
                Return VDM_BL.ShiftStatus.Unknown
            End Try
        End Get
        Set(value As VDM_BL.ShiftStatus)
            Session("SHIFT_Status") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNumeric(Session("USER_ID")) Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('กรุณาเข้าสู่ระบบ'); window.location.href='Login.aspx';", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            ClearForm()
            ClearMenu()

            ResetProductStock()
            ResetSIMStock()
            BineDataMenu()
            SetSummaryMenu()

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

        Select Case SHIFT_Status
            Case VDM_BL.ShiftStatus.Close
                lblMode.Text = "สั่ง Open Shift"
                lblMode.CssClass = "text-success"
                lnkConfirm.Text = "ยืนยัน Open Shift"
                lnkConfirm.CssClass = "btn btn-success btn-lg btn-block"
            Case VDM_BL.ShiftStatus.Open
                lblMode.Text = "สั่ง Close Shift"
                lblMode.CssClass = "text-danger"
                lnkConfirm.Text = "ยืนยัน Close Shift"
                lnkConfirm.CssClass = "btn btn-danger btn-lg btn-block"
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
        End If

    End Sub
    Private Sub BineDataMenu()
        UC_Shift_Change.Start_Menu()
        UC_Shift_Recieve.Start_Menu()
        UC_Shift_StockPaper.Start_Menu()
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
        '----Product 
        If Val(lbl_Product_Total.Text) > 0 Then
            divMenuStockProduct.Visible = True
            lbl_Product_Count.Text = lbl_Product_Total.Text & " รายการ"
        Else
            divMenuStockProduct.Visible = False
        End If
        '----SIM 
        If Val(lbl_SIM_Total.Text) > 0 Then
            divMenuStockSIM.Visible = True
            lbl_SIM_Count.Text = lbl_SIM_Total.Text & " รายการ"
        Else
            divMenuStockSIM.Visible = False
        End If
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
            '----Paper
            If UC_Shift_StockPaper.Validate Then
                Validate = True
            Else
                ClearMenu()
                lnkStockPaper_ServerClick(Nothing, Nothing)
                Exit Sub
            End If

            Try
                '----------- Get KO_CODE------------------
                Dim DT As DataTable = BL.GetList_Kiosk(KO_ID)
                Dim KO_CODE As String = DT.Rows(0).Item("KO_CODE")
                '------------ get shift id first----------
                Dim SQL As String = "EXEC dbo.SP_CURRENT_OPEN_SHIFT " & KO_ID
                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                DT = New DataTable
                DA.Fill(DT)

                If DT.Rows.Count = 0 Then '------------- ปิดอยู่ ต้องเปิด

                    '-------------- Get Shift CODE--------------
                    Dim SHIFT_Y As String = Now.Year
                    Dim SHIFT_M As String = Now.Month.ToString.PadLeft(2, "0")
                    Dim SHIFT_D As String = Now.Day.ToString.PadLeft(2, "0")
                    SQL = "SELECT ISNULL(MAX(SHIFT_N),0)+1 SHIFT_N FROM TB_SHIFT " & vbLf
                    SQL &= "WHERE KO_ID = " & KO_ID & vbLf
                    SQL &= " AND SHIFT_Y='" & SHIFT_Y & "'" & vbLf
                    SQL &= " AND SHIFT_M='" & SHIFT_M & "'" & vbLf
                    SQL &= " AND SHIFT_D='" & SHIFT_D & "'" & vbLf
                    DT = New DataTable
                    DA = New SqlDataAdapter(SQL, BL.ConnectionString)
                    DA.Fill(DT)
                    Dim SHIFT_N As Integer = DT.Rows(0)(0)

                    '------------ Create New SHIFT--------------
                    Session("SHIFT_ID") = BL.Get_NewID("TB_SHIFT", "SHIFT_ID")
                    '------------ Set Stock-------------
                    Product_Stock.SHIFT_STATUS = VDM_BL.ShiftStatus.Open
                    SIMStock.SHIFT_STATUS = VDM_BL.ShiftStatus.Open

                    DT = New DataTable
                    DA = New SqlDataAdapter("SELECT TOP 0 * FROM TB_SHIFT", BL.ConnectionString)
                    DA.Fill(DT)
                    Dim DR As DataRow = DT.NewRow
                    DT.Rows.Add(DR)
                    DR("SHIFT_ID") = Session("SHIFT_ID")
                    DR("KO_ID") = KO_ID
                    DR("SHIFT_Y") = SHIFT_Y
                    DR("SHIFT_M") = SHIFT_M
                    DR("SHIFT_D") = SHIFT_D
                    DR("KO_CODE") = KO_CODE
                    DR("SHIFT_N") = SHIFT_N
                    DR("Open_Time") = Now
                    DR("Close_Time") = DBNull.Value
                    DR("Open_By") = Session("USER_ID")
                    DR("Close_By") = DBNull.Value

                Else '------------- เปิดอยู่ ต้องปิด
                    Session("SHIFT_ID") = DT.Rows(0)("SHIFT_ID")
                    DT = New DataTable
                    DA = New SqlDataAdapter("SELECT * FROM TB_SHIFT WHERE SHIFT_ID=" & Session("SHIFT_ID"), BL.ConnectionString)
                    DA.Fill(DT)
                    Dim DR As DataRow = DT.Rows(0)
                    DR("KO_CODE") = KO_CODE
                    DR("Close_Time") = Now
                    DR("Close_By") = Session("USER_ID")
                    '------------ Set Stock-------------
                    Product_Stock.SHIFT_STATUS = VDM_BL.ShiftStatus.Close
                    SIMStock.SHIFT_STATUS = VDM_BL.ShiftStatus.Close
                End If
                Dim cmd As New SqlCommandBuilder(DA)
                DA.Update(DT)
                Product_Stock.SHIFT_ID = Session("SHIFT_ID")
                SIMStock.SHIFT_ID = Session("SHIFT_ID")
                Product_Stock.Save()
                SIMStock.Save()

                '----------- ไล่หาดูว่า Save ถูกมั้ย -----------
                ShiftChange_Success = UC_Shift_Change.Save
                ShiftRecieve_Success = UC_Shift_Recieve.Save
                ShiftStockPaper_Success = UC_Shift_StockPaper.Save


            Catch ex As Exception
                Alert(Me.Page, ex.Message)
                Exit Sub
            End Try


            '--Update TB_KIOSK_DEVICE
            UPDATE_DEVICE_Qty()
            '--สั่ง Open/Close Shift

            SHIFT_Status = Product_Stock.SHIFT_STATUS

            Alert(Me.Page, lblMode.Text & " แล้ว")
            Redirect(Me.Page, "Machine_Overview.aspx")


        Catch ex As Exception
            Alert(Me.Page, ex.Message)
            Exit Sub
        End Try



    End Sub


    Private Sub UPDATE_DEVICE_Qty()
        '----เงินทอน
        Dim Change As DataTable = UC_Shift_Change.Current_Data()
        Dim SQL As String = ""
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter
        Dim DR As DataRow
        Dim cmd As New SqlCommandBuilder
        If Change.Rows.Count > 0 Then
            For i As Integer = 0 To Change.Rows.Count - 1
                SQL = "Select * FROM TB_KIOSK_DEVICE "
                SQL &= " WHERE KO_ID=" & KO_ID & " And D_ID=" & Change.Rows(i).Item("D_ID")
                DT = New DataTable
                DA = New SqlDataAdapter(SQL, BL.ConnectionString)
                DA.Fill(DT)
                If DT.Rows.Count = 0 Then
                    DR = DT.NewRow
                    DT.Rows.Add(DR)
                    DR("KO_ID") = KO_ID
                    DR("D_ID") = Change.Rows(i).Item("D_ID")
                Else
                    DR = DT.Rows(0)
                End If

                DR("Current_Qty") = IIf(Val(Change.Rows(i).Item("Remain")) <> 0, Val(Change.Rows(i).Item("Remain")), DBNull.Value)
                DR("DT_ID") = Change.Rows(i).Item("DT_ID")
                DR("DS_ID") = BL.CheckDevice_Status(KO_ID, Change.Rows(i).Item("D_ID"))
                DR("Update_Time") = Now
                cmd = New SqlCommandBuilder(DA)
                DA.Update(DT)
            Next
        End If

        '----เงินรับ 
        '---------  CoinIn  
        SQL = "Select * FROM TB_KIOSK_DEVICE "
        SQL &= " WHERE KO_ID=" & KO_ID & " And D_ID=" & VDM_BL.Device.CoinIn
        DT = New DataTable
        DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        If DT.Rows.Count = 0 Then
            DR = DT.NewRow
            DT.Rows.Add(DR)
            DR("KO_ID") = KO_ID
            DR("D_ID") = VDM_BL.Device.CoinIn
        Else
            DR = DT.Rows(0)
        End If
        DR("Current_Qty") = Val(UC_Shift_Recieve.Remain_coin)
        DR("DT_ID") = VDM_BL.DeviceType.CoinIn
        DR("DS_ID") = BL.CheckDevice_Status(KO_ID, VDM_BL.Device.CoinIn)
        DR("Update_Time") = Now
        cmd = New SqlCommandBuilder(DA)
        DA.Update(DT)


        '----------------------------------------------------------
        '---------  CashIn

        SQL = "Select * FROM TB_KIOSK_DEVICE "
        SQL &= " WHERE KO_ID=" & KO_ID & " And D_ID=" & VDM_BL.Device.CashIn
        DT = New DataTable
        DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        If DT.Rows.Count = 0 Then
            DR = DT.NewRow
            DT.Rows.Add(DR)
            DR("KO_ID") = KO_ID
            DR("D_ID") = VDM_BL.Device.CashIn
        Else
            DR = DT.Rows(0)
        End If
        DR("Current_Qty") = Val(UC_Shift_Recieve.Remain_cash)
        DR("DT_ID") = VDM_BL.DeviceType.CashIn
        DR("DS_ID") = BL.CheckDevice_Status(KO_ID, VDM_BL.Device.CashIn)
        DR("Update_Time") = Now
        cmd = New SqlCommandBuilder(DA)
        DA.Update(DT)

        '----Product

        '----SIM


        '----Paper
        SQL = "Select * FROM TB_KIOSK_DEVICE "
        SQL &= " WHERE KO_ID=" & KO_ID & " And D_ID=" & VDM_BL.Device.Printer
        DT = New DataTable
        DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        If DT.Rows.Count = 0 Then
            DR = DT.NewRow
            DT.Rows.Add(DR)
            DR("KO_ID") = KO_ID
            DR("D_ID") = VDM_BL.Device.Printer
        Else
            DR = DT.Rows(0)
        End If
        DR("Current_Qty") = Val(UC_Shift_StockPaper.Total)
        DR("DT_ID") = VDM_BL.DeviceType.Printer
        DR("DS_ID") = BL.CheckDevice_Status(KO_ID, VDM_BL.Device.Printer)
        DR("Update_Time") = Now
        cmd = New SqlCommandBuilder(DA)
        DA.Update(DT)



        '--2   Cash 20
        '--3   Cash 100
        '--5   Coin 1
        '--6   Coin 5

        '--1   Cash In
        '--4   Coin In

        '10  Product Shelf
        'Product_Stock.SHIFT_ID =ระบุเพิ่ม
        'Product_Stock.SHIFT_STATUS =ระบุเพิ่ม
        'Product_Stock.Save() >> Return True/False

        '11  SIM Dispenser 1
        '12  SIM Dispenser 2
        '13  SIM Dispenser 3

        '--7   Printer


    End Sub

#Region "Scan Product Stock"

    Private Sub ResetProductStock() '--------- Call First Time -------------
        Product_Stock.KO_ID = KO_ID
        Product_Stock.SHOP_CODE = SHOP_CODE
        Product_Stock.BindData()
        UpdateProductSummary()
    End Sub

    Private Sub UpdateProductSummary()
        Dim STOCK_DATA As DataTable = Product_Stock.STOCK_DATA.Copy
        STOCK_DATA.Columns("CURRENT").ColumnName = "SLOT_NAME"
        BL.Bind_Product_Shelf_Layout(Kiosk_Shelf, KO_ID)
        BL.Bind_Product_Shelf_Stock(Kiosk_Shelf, STOCK_DATA)
        Kiosk_Shelf.HideFloorMenu()
        Kiosk_Shelf.HideFloorName()
        Kiosk_Shelf.ShowAddFloor = False
        Kiosk_Shelf.ShowEditShelf = False
        Kiosk_Shelf.ShowScale = False
        For i As Integer = 0 To Kiosk_Shelf.Slots.Count - 1
            Kiosk_Shelf.Slots(i).ShowScale = False
        Next
        For i As Integer = 0 To Kiosk_Shelf.Floors.Count - 1
            Kiosk_Shelf.Floors(i).ShowScale = False
        Next

        '----------Quantity--------------
        lbl_Product_Total.Text = STOCK_DATA.Compute("COUNT(SERIAL_NO)", "")
        lbl_Product_In.Text = STOCK_DATA.Compute("COUNT(SERIAL_NO)", "RECENT Is NULL And SLOT_NAME Is Not NULL")
        lbl_Product_Out.Text = STOCK_DATA.Compute("COUNT(SERIAL_NO)", "RECENT Is Not NULL And SLOT_NAME Is NULL")
        lbl_Product_Move.Text = STOCK_DATA.Compute("COUNT(SERIAL_NO)", "RECENT Is Not NULL And SLOT_NAME Is Not NULL And SLOT_NAME<>RECENT")
        Dim EmptySlot As Integer = 0
        For i As Integer = 0 To Product_Stock.Product_Shelf.Slots.Count - 1
            If Product_Stock.Product_Shelf.Slots(i).PRODUCT_ID = 0 Then EmptySlot += 1
        Next
        lbl_Product_Empty.Text = EmptySlot

        If lbl_Product_Total.Text = "0" Then lbl_Product_Total.Text = "-"
        If lbl_Product_In.Text = "0" Then lbl_Product_In.Text = "-"
        If lbl_Product_Out.Text = "0" Then lbl_Product_Out.Text = "-"
        If lbl_Product_Move.Text = "0" Then lbl_Product_Move.Text = "-"
        If lbl_Product_Empty.Text = "0" Then lbl_Product_Empty.Text = "-"

    End Sub

    Private Sub btnManageProductStock_Click(sender As Object, e As EventArgs) Handles btnManageProductStock.Click
        pnlScanProduct.Visible = True
        Dim Script As String = "txtBarcode='" & Product_Stock.BarcodeClientID & "';" & vbLf
        Script &= "startFocusBarcode();"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "focusBarcodeReader", Script, True)
        '---------------- DragDropEvent-----------
        Product_Stock.ImplementDragDrop()
    End Sub

    Private Sub CloseScanProduct_Click(sender As Object, e As EventArgs) Handles btnCloseScanProduct.Click, lnkCloseScanProduct.Click
        pnlScanProduct.Visible = False
        UpdateProductSummary()
    End Sub

    Private Sub btnResetScanProduct_Click(sender As Object, e As EventArgs) Handles btnResetScanProduct.Click
        ResetProductStock()
    End Sub
#End Region

#Region "SIM Stock"

    Private Sub ResetSIMStock() '--------- Call First Time -------------
        SIMStock.KO_ID = KO_ID
        SIMStock.SHOP_CODE = SHOP_CODE
        SIMStock.BindData()
        UpdateSIMSummary()
    End Sub

    Private Sub UpdateSIMSummary()
        Dim STOCK_DATA As DataTable = SIMStock.STOCK_DATA.Copy
        STOCK_DATA.Columns("CURRENT").ColumnName = "SLOT_NAME"
        BL.Bind_SIMDispenser_Layout(SIMDispenser, KO_ID)
        BL.Bind_SIMDispenser_Stock(SIMDispenser, STOCK_DATA)

        '----------Quantity--------------
        lbl_SIM_Total.Text = STOCK_DATA.Compute("COUNT(SERIAL_NO)", "")
        lbl_SIM_In.Text = STOCK_DATA.Compute("COUNT(SERIAL_NO)", "RECENT IS NULL AND SLOT_NAME IS NOT NULL")
        lbl_SIM_Out.Text = STOCK_DATA.Compute("COUNT(SERIAL_NO)", "RECENT IS NOT NULL AND SLOT_NAME IS NULL")
        lbl_SIM_Move.Text = STOCK_DATA.Compute("COUNT(SERIAL_NO)", "RECENT IS NOT NULL AND SLOT_NAME IS NOT NULL AND SLOT_NAME<>RECENT")
        Dim EmptySlot As Integer = 0
        For i As Integer = 0 To SIMStock.SIMDispenser.Slots.Count - 1
            If SIMStock.SIMDispenser.Slots(i).SIM_ID = 0 Then EmptySlot += 1
        Next
        lbl_SIM_Empty.Text = EmptySlot

        If lbl_SIM_Total.Text = "0" Then lbl_SIM_Total.Text = "-"
        If lbl_SIM_In.Text = "0" Then lbl_SIM_In.Text = "-"
        If lbl_SIM_Out.Text = "0" Then lbl_SIM_Out.Text = "-"
        If lbl_SIM_Move.Text = "0" Then lbl_SIM_Move.Text = "-"
        If lbl_SIM_Empty.Text = "0" Then lbl_SIM_Empty.Text = "-"
    End Sub

    Private Sub btnManageSIMStock_Click(sender As Object, e As EventArgs) Handles btnManageSIMStock.Click
        pnlScanSIM.Visible = True
        Dim Script As String = "txtBarcode='" & SIMStock.BarcodeClientID & "';" & vbLf
        Script &= "startFocusBarcode();"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "focusBarcodeReader", Script, True)
        '---------------- DragDropEvent-----------
        SIMStock.ImplementDragDrop()
    End Sub

    Private Sub btnCloseScanSIM_Click(sender As Object, e As EventArgs) Handles btnCloseScanSIM.Click, lnkCloseScanProduct.Click
        pnlScanSIM.Visible = False
        UpdateSIMSummary()
    End Sub

    Private Sub btnResetScanSIM_Click(sender As Object, e As EventArgs) Handles btnResetScanSIM.Click
        ResetSIMStock()
    End Sub

#End Region

End Class