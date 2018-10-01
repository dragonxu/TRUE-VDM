Imports System.Data
Imports System.Data.SqlClient
Imports VDM

Public Class UC_SIM_Stock
    Inherits System.Web.UI.UserControl

    Dim BL As New VDM_BL

    Public Property KO_ID As Integer
        Get
            Return btnSeeDispenser.Attributes("KO_ID")
        End Get
        Set(value As Integer)
            btnSeeDispenser.Attributes("KO_ID") = value
        End Set
    End Property

    Public Property SHOP_CODE As String
        Get
            Return btnSeeDispenser.Attributes("SHOP_CODE")
        End Get
        Set(value As String)
            btnSeeDispenser.Attributes("SHOP_CODE") = value
        End Set
    End Property

    Public Property SHIFT_ID As Integer
        Get
            Return btnSeeDispenser.Attributes("SHIFT_ID")
        End Get
        Set(value As Integer)
            btnSeeDispenser.Attributes("SHIFT_ID") = value
        End Set
    End Property

    Public Property SHIFT_STATUS As VDM_BL.ShiftStatus
        Get
            Return btnSeeDispenser.Attributes("SHIFT_STATUS")
        End Get
        Set(value As VDM_BL.ShiftStatus)
            btnSeeDispenser.Attributes("SHIFT_STATUS") = value
        End Set
    End Property

    Public ReadOnly Property SLOT_CAPACITY As Integer
        Get
            Return Dispenser.SLOT_CAPACITY
        End Get
    End Property

    Public ReadOnly Property SIMDispenser As UC_SIM_Dispenser
        Get
            Return Dispenser
        End Get
    End Property

    Public ReadOnly Property VW_ALL_SIM As DataTable
        Get
            If IsNothing(Session("VW_ALL_SIM")) Then
                Dim SQL As String = "SELECT * FROM VW_ALL_SIM"
                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                Dim DT As New DataTable
                DA.Fill(DT)
                Session("VW_ALL_SIM") = DT
            End If
            Return Session("VW_ALL_SIM")
        End Get
    End Property

    Private ReadOnly Property MY_UNIQUE_ID() As String
        Get
            If btnSeeDispenser.Attributes("MY_UNIQUE_ID") = "" Then
                btnSeeDispenser.Attributes("MY_UNIQUE_ID") = GenerateNewUniqueID() '---- Needed ---------
            End If
            Return btnSeeDispenser.Attributes("MY_UNIQUE_ID")
        End Get
    End Property

    Public Property STOCK_DATA As DataTable
        Get
            If IsNothing(Session("STOCK_DATA_" & MY_UNIQUE_ID)) Then
                Session("STOCK_DATA_" & MY_UNIQUE_ID) = BL.Get_Current_SIM_Stock(KO_ID)
            End If
            Return Session("STOCK_DATA_" & MY_UNIQUE_ID)
        End Get
        Set(value As DataTable)
            Session("STOCK_DATA_" & MY_UNIQUE_ID) = value
        End Set
    End Property

    Public Property SIM_Height As Integer
        Get
            Return SIMDispenser.SIM_Height
        End Get
        Set(value As Integer)
            SIMDispenser.SIM_Height = value
        End Set
    End Property

    Public ReadOnly Property BarcodeClientID As String
        Get
            Return txtBarcode.ClientID
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            BindData()
        Else
            initFormPlugin()
        End If

    End Sub

    Public Sub BindData()
        'MY_UNIQUE_ID = GenerateNewUniqueID()
        STOCK_DATA = Nothing

        SCAN_SIM_ID = -1
        BindScanSIM()
        '-------- Left Side -------
        BindDispenserLayout()
        ResetSIMSlot()
        BindDispenserSIM()
    End Sub

#Region "CheckButton"

    Const CheckScanCss As String = "btn btn-success btn-icon-icon btn-sm"
    Const CheckSlotCss As String = "btn btn-primary btn-icon-icon btn-sm"
    Const UncheckCss As String = "btn btn-default btn-icon-icon btn-sm"
    Const CheckedText As String = "<i class='icon-check'></i>"
    Const UncheckedText As String = "<i class='icon-close'></i>"

    Private Function IsButtonCheck(ByRef btn As LinkButton) As Boolean
        Return btn.Text = CheckedText
    End Function

    Private Sub SetScanCheck(ByRef btn As LinkButton, ByVal Checked As Boolean)
        If Checked Then
            btn.CssClass = CheckScanCss
            btn.Text = CheckedText
        Else
            btn.CssClass = UncheckCss
            btn.Text = UncheckedText
        End If
    End Sub

    Private Sub SetSlotCheck(ByRef btn As LinkButton, ByVal Checked As Boolean)
        If Checked Then
            btn.CssClass = CheckSlotCss
            btn.Text = CheckedText
        Else
            btn.CssClass = UncheckCss
            btn.Text = UncheckedText
        End If
    End Sub
#End Region

    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Private Sub BindDispenserLayout() '------------ เรียกครั้งแรกครั้งเดียว ---------------
        BL.Bind_SIMDispenser_Layout(Dispenser, KO_ID)
        ConfigDispenserLayout()
    End Sub

    Private Sub ConfigDispenserLayout()

    End Sub

    Private Property SCAN_SIM_ID As Integer
        Get
            For Each Item As RepeaterItem In rptSIMType.Items
                If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For
                Dim lnk As LinkButton = Item.FindControl("lnk")
                Dim li As HtmlGenericControl = Item.FindControl("li")
                If li.Attributes("class") = "active" Then
                    Return lnk.CommandArgument
                End If
            Next
            Return -1
        End Get
        Set(value As Integer)
            Dim DT As DataTable = STOCK_DATA.Copy '------------- Bind Database Automatically-------

            lblTotalScan.Text = DT.Compute("COUNT(SERIAL_NO)", "CURRENT IS NULL") & " ชิ้น"
            Dim Col() As String = {"SIM_ID", "PRODUCT_NAME"}
            DT.DefaultView.RowFilter = "CURRENT IS NULL"
            DT = DT.DefaultView.ToTable(True, Col)

            rptSIMType.DataSource = DT
            rptSIMType.DataBind()
            DT.Dispose()

            '------------- Set UI Active-------------
            For Each Item As RepeaterItem In rptSIMType.Items
                If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For
                Dim lnk As LinkButton = Item.FindControl("lnk")
                Dim li As HtmlGenericControl = Item.FindControl("li")
                li.Attributes("class") = ""
                If lnk.CommandArgument = value Then
                    li.Attributes("class") = "active"
                End If
            Next

            SuggessSIMSlot()

            '------------ Check Left Product Slot-------
            If DEVICE_ID > 0 Then '------- Show Slot อยู่ -------
                If SLOT_SIM_ID > 0 Then '------------- บรรจุ Product --------
                    If SLOT_SIM_ID <> SCAN_SIM_ID Then '----------- เป็นคนละ Product กัน----------
                        ResetSIMSlot()
                    End If
                End If
            End If


            ImplementDragDrop()
        End Set
    End Property

#Region "Slot Panel"

    Public Property DEVICE_ID As Integer '-------- Slected Slot 
        Get
            Return lblSlotID.Attributes("DEVICE_ID")
        End Get
        Set(value As Integer)
            lblSlotID.Attributes("DEVICE_ID") = value
        End Set
    End Property

    Public Property SLOT_NAME As String '-------- Slected Slot 
        Get
            Return lblSlotID.Text
        End Get
        Set(value As String)
            lblSlotID.Text = value
        End Set
    End Property

    Private Sub ResetSIMSlot()
        Dispenser.Deselect_All_Slot()
        DEVICE_ID = 0
        SLOT_NAME = ""
        SLOT_SIM_ID = 0

        '----------- Clear Panel Slot-------------
        imgSlot.ImageUrl = "../images/TransparentDot.png"
        lblSlot_SIMCode.Text = "-"
        lblSlot_SIMName.Text = ""
        tagSlot_Empty.Visible = True


        lblSlot_Quantity.Text = "-"
        pnlSlotCapacity.Visible = False
        levelSIM.Visible = False
        lblFreeSpace.Text = "-"
        lblMaxSpace.Text = "-"

        SetSlotCheck(chkSlot, False)
        chkSlot.Visible = False
        rptSlot.DataSource = Nothing
        rptSlot.DataBind()
        btnMoveRight.Visible = False

        pnlSlotSIM.Visible = False
        Dispenser.Visible = True

    End Sub

    Private Sub BindDispenserSIM()

        Dim DT As DataTable = STOCK_DATA.Copy
        DT.Columns("CURRENT").ColumnName = "SLOT_NAME"
        BL.Bind_SIMDispenser_Stock(Dispenser, DT, VW_ALL_SIM)

        ImplementDragDrop()
    End Sub

    Private Property SLOT_SIM_ID As Integer
        Get
            Return pnlSlotSIM.Attributes("SIM_ID")
        End Get
        Set(value As Integer)
            pnlSlotSIM.Attributes("SIM_ID") = value
        End Set
    End Property

    Private Sub Dispenser_Selecting(ByRef Sender As UC_SIM_Slot) Handles Dispenser.Selecting
        If Sender.SIM_ID > 0 Then '----------- ไม่ว่าง
            If SCAN_SIM_ID > 0 Then
                If Sender.SIM_ID <> SCAN_SIM_ID Then
                    SCAN_SIM_ID = Sender.SIM_ID
                    BindScanSIM()
                End If
            End If
        End If
        BindSlotSIM(Sender)
    End Sub

    Private Sub BindSlotSIM(ByVal DEVICE_ID As Integer)
        Dim SLOTS As List(Of UC_SIM_Slot) = Dispenser.Slots
        Dim SLOT As UC_SIM_Slot = Nothing
        For s As Integer = 0 To SLOTS.Count - 1
            If SLOTS(s).DEVICE_ID = DEVICE_ID Then
                SLOT = SLOTS(s)
                Exit For
            End If
        Next
        BindSlotSIM(SLOT)
    End Sub

    Private Sub BindSlotSIM(ByRef Sender As UC_SIM_Slot)

        ResetSIMSlot()
        DEVICE_ID = Sender.DEVICE_ID
        SLOT_NAME = Sender.SLOT_NAME

        VW_ALL_SIM.DefaultView.RowFilter = "SIM_ID=" & Sender.SIM_ID

        If VW_ALL_SIM.DefaultView.Count > 0 Then
            Dim DV As DataRowView = VW_ALL_SIM.DefaultView(0)

            SLOT_SIM_ID = DV("SIM_ID")

            imgSlot.ImageUrl = "../RenderImage.aspx?Mode=D&UID=" & Sender.SIM_ID & "&Entity=SIM_Package&LANG=1" '&DI=images/TransparentDot.png"
            lblSlot_SIMCode.Text = DV("PRODUCT_CODE")
            lblSlot_SIMName.Text = DV("DISPLAY_NAME_TH")
            tagSlot_Empty.Visible = False

            lblSlot_Quantity.Text = "<small>X</small> " & Sender.SIM_QUANTITY

            pnlSlotCapacity.Visible = True
            levelSIM.Visible = True

            Dim MaxQuantity As Integer = Sender.MAX_CAPACITY
            lblMaxSpace.Text = MaxQuantity
            lblFreeSpace.Text = MaxQuantity - Sender.SIM_QUANTITY
            levelSIM.Width = Unit.Percentage(Sender.SIM_QUANTITY * 100 / MaxQuantity)

        End If

        SetSlotCheck(chkSlot, False)

        Dim DT As DataTable = STOCK_DATA.Copy
        DT.DefaultView.RowFilter = "CURRENT='" & SLOT_NAME & "'"
        DT = DT.DefaultView.ToTable

        rptSlot.DataSource = DT
        rptSlot.DataBind()

        btnMoveRight.Visible = DT.Rows.Count > 0
        chkSlot.Visible = DT.Rows.Count > 0

        pnlSlotSIM.Visible = True
        Dispenser.Visible = False

        ImplementDragDrop()
    End Sub

    Private Sub chkSlot_Click(sender As Object, e As EventArgs) Handles chkSlot.Click
        Dim Checked As Boolean = IsButtonCheck(chkSlot)
        For Each Item As RepeaterItem In rptSlot.Items
            If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For
            Dim chk As LinkButton = Item.FindControl("chk")
            SetSlotCheck(chk, Not Checked)
            '------------- HighLight All Row-------------
            Dim tr As HtmlTableRow = Item.FindControl("tr")
            If Not Checked Then
                tr.Attributes("class") = "bg-info text-white"
            Else
                tr.Attributes("class") = ""
            End If

        Next
        SetSlotCheck(chkSlot, Not Checked)
    End Sub

    Private Function LeftSelectedList() As List(Of DataRow)
        '----------------- Create Strucure ----------
        Dim Result As New List(Of DataRow)
        For Each Item As RepeaterItem In rptSlot.Items
            If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For
            Dim chk As LinkButton = Item.FindControl("chk")
            If IsButtonCheck(chk) Then
                Dim lblSerial As Label = Item.FindControl("lblSerial")
                STOCK_DATA.DefaultView.RowFilter = "SERIAL_NO='" & lblSerial.Attributes("SERIAL_NO").Replace("'", "''") & "'"
                If STOCK_DATA.DefaultView.Count > 0 Then
                    Result.Add(STOCK_DATA.DefaultView(0).Row)
                End If
            End If
        Next
        Return Result
    End Function

    Private Sub btnMoveRight_Click(sender As Object, e As EventArgs) Handles btnMoveRight.Click
        Dim MoveList As List(Of DataRow) = LeftSelectedList()
        If MoveList.Count = 0 Then
            Message_Toastr("เลือกรายการที่ย้ายออก", ToastrMode.Warning, ToastrPositon.TopRight, Me.Page)
            Exit Sub
        End If

        '------------ Update Data -------------
        For i As Integer = 0 To MoveList.Count - 1
            Dim DR As DataRow = MoveList(i)
            DR("CURRENT") = DBNull.Value
        Next

        '----------- Bind Left Side ----------
        Dim SIM_ID As Integer = SLOT_SIM_ID
        Dim DEVICE_ID As Integer = Me.DEVICE_ID
        '----------- Bind Right Side ----------
        SCAN_SIM_ID = SIM_ID
        BindScanSIM()

        '----------- Bind LEFT Side ----------
        BindDispenserSIM()
        BindSlotSIM(DEVICE_ID)

    End Sub

    Private Sub SuggessSIMSlot()
        Dispenser.Deselect_All_Slot()
        Dim Slots As List(Of UC_SIM_Slot) = Dispenser.Slots
        Dim PRODUCT_ID As Integer = SCAN_SIM_ID
        For s As Integer = 0 To Slots.Count - 1
            If SLOT_CAN_RECIEVE_SIM(Slots(s), PRODUCT_ID) Then
                Slots(s).HighLight = UC_Product_Slot.HighLightMode.GreenSolid
            End If
        Next
    End Sub

    Private Function SLOT_CAN_RECIEVE_SIM(ByRef SLOT As UC_SIM_Slot, ByVal SIM_ID As Integer) As Boolean
        Return CALCULATE_SLOT_FREE_SPACE_FOR_SIM(SLOT, SIM_ID) > 0
    End Function

    Private Function CALCULATE_SLOT_FREE_SPACE_FOR_SIM(ByRef SLOT As UC_SIM_Slot, ByVal SIM_ID As Integer) As Integer

        VW_ALL_SIM.DefaultView.RowFilter = "SIM_ID=" & SIM_ID
        If VW_ALL_SIM.DefaultView.Count = 0 Then Return 0

        If SLOT.SIM_ID = 0 Then
            Return SLOT.MAX_CAPACITY
        ElseIf SLOT.SIM_ID = SIM_ID Then
            Return SLOT.MAX_CAPACITY - SLOT.SIM_QUANTITY
        Else
            Return 0
        End If
    End Function
#End Region

    Private Sub rptSIM_ItemCreated(sender As Object, e As RepeaterItemEventArgs) Handles rptSlot.ItemCreated, rptScan.ItemCreated
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim chk As LinkButton = e.Item.FindControl("chk")
        Dim del As LinkButton = e.Item.FindControl("del")

        Dim scriptMan As ScriptManager = ScriptManager.GetCurrent(Page)
        scriptMan.RegisterAsyncPostBackControl(chk)
        scriptMan.RegisterAsyncPostBackControl(del)
    End Sub

    Private Sub rptSIM_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptSlot.ItemDataBound, rptScan.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim chk As LinkButton = e.Item.FindControl("chk")
        Dim lblSerial As Label = e.Item.FindControl("lblSerial")
        Dim lblRecent As Label = e.Item.FindControl("lblRecent")
        Dim del As LinkButton = e.Item.FindControl("del")

        SetScanCheck(chk, False) '---------- Default Uncheck--------
        If IsFormatGUID(e.Item.DataItem("SERIAL_NO").ToString) Then
            lblSerial.Text = "-"
        Else
            lblSerial.Text = e.Item.DataItem("SERIAL_NO")
        End If
        lblSerial.Attributes("SERIAL_NO") = e.Item.DataItem("SERIAL_NO").ToString

        If IsDBNull(e.Item.DataItem("RECENT")) Then
            lblRecent.Text = "SCAN"
            lblRecent.ForeColor = Drawing.Color.Green
            del.Visible = True
        Else
            lblRecent.Text = e.Item.DataItem("RECENT")
            lblRecent.ForeColor = Drawing.Color.Blue
            del.Visible = False
        End If

        Dim tr As HtmlTableRow = e.Item.FindControl("tr")
        tr.Attributes("onclick") = "document.getElementById('" & chk.ClientID & "').click();"

    End Sub

    Private Sub rptSlot_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptSlot.ItemCommand
        Select Case e.CommandName
            Case "Check"
                Dim chk As LinkButton = e.Item.FindControl("chk")
                SetScanCheck(chk, Not IsButtonCheck(chk))

                '------------- HighLight All Row-------------
                Dim tr As HtmlTableRow = e.Item.FindControl("tr")
                If IsButtonCheck(chk) Then
                    tr.Attributes("class") = "bg-info text-white"
                Else
                    tr.Attributes("class") = ""
                End If

                '------------ Update Header------------
                SetSlotCheck(chkScan, False)
                For Each Item As RepeaterItem In rptScan.Items
                    If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For
                    Dim _chk As LinkButton = Item.FindControl("chk")
                    If Not IsButtonCheck(_chk) Then Exit Sub
                Next
                SetSlotCheck(chkScan, True)


            Case "Delete"
                Dim lblSerial As Label = e.Item.FindControl("lblSerial")
                STOCK_DATA.DefaultView.RowFilter = "SERIAL_NO='" & lblSerial.Attributes("SERIAL_NO").Replace("'", "''") & "'"
                If STOCK_DATA.DefaultView.Count > 0 Then
                    STOCK_DATA.DefaultView(0).Row.Delete()
                End If
                STOCK_DATA.DefaultView.RowFilter = ""
                '-------------- Refresh -----------
                BindSlotSIM(DEVICE_ID)
        End Select
    End Sub

    Private Sub rptScan_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptScan.ItemCommand
        Select Case e.CommandName
            Case "Check"
                Dim chk As LinkButton = e.Item.FindControl("chk")
                SetScanCheck(chk, Not IsButtonCheck(chk))

                '------------- HighLight All Row-------------
                Dim tr As HtmlTableRow = e.Item.FindControl("tr")
                If IsButtonCheck(chk) Then
                    tr.Attributes("class") = "bg-success text-white"
                Else
                    tr.Attributes("class") = ""
                End If

                '------------ Update Header------------
                SetScanCheck(chkScan, False)
                For Each Item As RepeaterItem In rptScan.Items
                    If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For
                    Dim _chk As LinkButton = Item.FindControl("chk")
                    If Not IsButtonCheck(_chk) Then Exit Sub
                Next
                SetScanCheck(chkScan, True)


            Case "Delete"
                Dim lblSerial As Label = e.Item.FindControl("lblSerial")
                STOCK_DATA.DefaultView.RowFilter = "SERIAL_NO='" & lblSerial.Attributes("SERIAL_NO").Replace("'", "''") & "'"
                If STOCK_DATA.DefaultView.Count > 0 Then
                    STOCK_DATA.DefaultView(0).Row.Delete()
                End If
                STOCK_DATA.DefaultView.RowFilter = ""
                '-------------- Refresh -----------
                SCAN_SIM_ID = SCAN_SIM_ID
                BindScanSIM()
        End Select
    End Sub

#Region "Scane Panel"

    Private Sub ResetScanSIMTab() '------------- Reset เฉพาะ pnlProduct ที่เลือก ----------
        txtBarcode.Text = ""
        pnlScanSIM.Visible = False
        imgScan.ImageUrl = "../images/TransparentDot.png" ' "../RenderImage.aspx?Mode=D&UID=0&Entity=Product&LANG=1"
        lblScan_SIMCode.Text = "-"
        lblScan_SIMName.Text = ""
        SetScanCheck(chkScan, False)
        rptScan.DataSource = Nothing
        rptScan.DataBind()
        btnMoveLeft.Visible = False
    End Sub

    Private Sub chkScan_Click(sender As Object, e As EventArgs) Handles chkScan.Click
        Dim Checked As Boolean = IsButtonCheck(chkScan)

        For Each Item As RepeaterItem In rptScan.Items
            If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For
            Dim chk As LinkButton = Item.FindControl("chk")
            SetScanCheck(chk, Not Checked)
            '------------- HighLight All Row-------------
            Dim tr As HtmlTableRow = Item.FindControl("tr")
            If Not Checked Then
                tr.Attributes("class") = "bg-success text-white"
            Else
                tr.Attributes("class") = ""
            End If

        Next
        SetScanCheck(chkScan, Not Checked)
    End Sub

    Private Sub btnSeeDispenser_Click(sender As Object, e As EventArgs) Handles btnSeeDispenser.Click
        ResetSIMSlot()
        BindDispenserSIM()
        SuggessSIMSlot()

    End Sub

    Private Sub rptSIMType_ItemCreated(sender As Object, e As RepeaterItemEventArgs) Handles rptSIMType.ItemCreated
        Dim lnk As LinkButton = e.Item.FindControl("lnk")
        Dim scriptMan As ScriptManager = ScriptManager.GetCurrent(Page)
        scriptMan.RegisterAsyncPostBackControl(lnk)
    End Sub

    Private Sub rptSIMType_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptSIMType.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim lnk As LinkButton = e.Item.FindControl("lnk")
        Dim img As Image = e.Item.FindControl("img")
        Dim lblName As Label = e.Item.FindControl("lblName")


        lnk.CommandArgument = e.Item.DataItem("SIM_ID")
        img.ImageUrl = "../RenderImage.aspx?Mode=D&UID=" & e.Item.DataItem("SIM_ID") & "&Entity=SIM_Package&LANG=1&DI=images/TransparentDot.png"

        '-------------- Get Total Product ---------------
        Dim DT As DataTable = STOCK_DATA.Copy
        DT.DefaultView.RowFilter = "SIM_ID=" & e.Item.DataItem("SIM_ID") & " AND CURRENT IS NULL"
        Dim Total As Integer = DT.DefaultView.Count
        DT.Dispose()

        lblName.Text = e.Item.DataItem("PRODUCT_NAME").ToString & " <b>(" & Total & ")</b>"

    End Sub

    Private Sub rptSIMType_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptSIMType.ItemCommand
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Select Case e.CommandName
            Case "Select"
                SCAN_SIM_ID = e.CommandArgument
                BindScanSIM()
                SuggessSIMSlot()
        End Select

    End Sub

    Private Sub BindScanSIM()

        ResetScanSIMTab()

        Dim SIM_ID As Integer = SCAN_SIM_ID
        If SIM_ID = -1 Then Exit Sub

        VW_ALL_SIM.DefaultView.RowFilter = "SIM_ID=" & SIM_ID
        Dim DT As DataTable = VW_ALL_SIM.DefaultView.ToTable
        If DT.Rows.Count = 0 Then
            Message_Toastr("ไม่พบข้อมูล SIM", ToastrMode.Danger, ToastrPositon.TopLeft, Me.Page)
            Exit Sub
        End If

        pnlScanSIM.Visible = True
        imgScan.ImageUrl = "../RenderImage.aspx?Mode=D&UID=" & SIM_ID & "&Entity=SIM_Package&LANG=1" '&DI=images/TransparentDot.png"
        lblScan_SIMCode.Text = DT.Rows(0).Item("PRODUCT_CODE").ToString
        lblScan_SIMName.Text = DT.Rows(0).Item("DISPLAY_NAME_TH").ToString

        DT = STOCK_DATA.Copy
        DT.DefaultView.RowFilter = "SIM_ID=" & SIM_ID & " AND CURRENT IS NULL"
        DT = DT.DefaultView.ToTable

        rptScan.DataSource = DT
        rptScan.DataBind()

        btnMoveLeft.Visible = DT.Rows.Count > 0

        ImplementDragDrop()
    End Sub

    Private Sub btnBarcode_Click(sender As Object, e As EventArgs) Handles btnBarcode.Click
        Dim Barcode As String = txtBarcode.Text
        txtBarcode.Text = ""
        If Barcode = "" Then Exit Sub

        Dim ScanResult As VDM_BL.BarcodeScanResult = BL.Get_SIM_Barcode_Scan_Result(SHOP_CODE, Barcode)

        If Not ScanResult.Result Then '--------Also Check Duplicate From Database ------------
            Message_Toastr(ScanResult.Message, ToastrMode.Warning, ToastrPositon.Top, Me.Page)
            Exit Sub
        End If

        '-------------- Check Duplication In STOCK_DATA--------------
        STOCK_DATA.DefaultView.RowFilter = "SERIAL_NO='" & ScanResult.SERIAL_NO.Replace("'", "''") & "'"
        If STOCK_DATA.DefaultView.Count > 0 Then
            Message_Toastr("Serial No ซ้ำ", ToastrMode.Warning, ToastrPositon.Top, Me.Page)
            STOCK_DATA.DefaultView.RowFilter = ""
            Exit Sub
        End If
        STOCK_DATA.DefaultView.RowFilter = ""

        Dim DR As DataRow = STOCK_DATA.NewRow
        DR("SIM_ID") = ScanResult.PRODUCT_ID
        DR("PRODUCT_CODE") = ScanResult.PRODUCT_CODE
        DR("PRODUCT_NAME") = ScanResult.DISPLAY_NAME
        If ScanResult.SERIAL_NO = "" Then
            ScanResult.SERIAL_NO = GenerateNewUniqueID()
        End If
        DR("SERIAL_NO") = ScanResult.SERIAL_NO
        DR("RECENT") = DBNull.Value
        DR("CURRENT") = DBNull.Value
        STOCK_DATA.Rows.Add(DR)

        SCAN_SIM_ID = ScanResult.PRODUCT_ID '---------- Update Tab And Quantity ----------
        BindScanSIM()

        Message_Toastr(ScanResult.DISPLAY_NAME & " " & ScanResult.DISPLAY_NAME & " : " & ScanResult.SERIAL_NO, ToastrMode.Success, ToastrPositon.Top, Me.Page)

    End Sub

    Private Function RightSelectedList() As List(Of DataRow)
        '----------------- Create Strucure ----------
        Dim Result As New List(Of DataRow)
        For Each Item As RepeaterItem In rptScan.Items
            If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For
            Dim chk As LinkButton = Item.FindControl("chk")
            If IsButtonCheck(chk) Then
                Dim lblSerial As Label = Item.FindControl("lblSerial")
                STOCK_DATA.DefaultView.RowFilter = "SERIAL_NO='" & lblSerial.Attributes("SERIAL_NO").Replace("'", "''") & "'"
                If STOCK_DATA.DefaultView.Count > 0 Then
                    Result.Add(STOCK_DATA.DefaultView(0).Row)
                End If
            End If
        Next
        Return Result
    End Function

    Private Sub btnMoveLeft_Click(sender As Object, e As EventArgs) Handles btnMoveLeft.Click
        Dim MoveList As List(Of DataRow) = RightSelectedList()
        If MoveList.Count = 0 Then
            Message_Toastr("เลือกรายการที่จะเติม", ToastrMode.Warning, ToastrPositon.TopRight, Me.Page)
            Exit Sub
        End If

        If DEVICE_ID <= 0 Then
            Message_Toastr("เลือก SLOT ที่จะเติม", ToastrMode.Warning, ToastrPositon.TopRight, Me.Page)
            'btn(Nothing, Nothing)
            Exit Sub
        End If

        Dim SLOTS As List(Of UC_SIM_Slot) = Dispenser.Slots
        Dim SLOT As UC_SIM_Slot = Nothing
        For s As Integer = 0 To SLOTS.Count - 1
            If SLOTS(s).DEVICE_ID = DEVICE_ID Then
                SLOT = SLOTS(s)
                Exit For
            End If
        Next

        If IsNothing(SLOT) Then
            Message_Toastr("เลือก SLOT ที่จะเติม", ToastrMode.Warning, ToastrPositon.TopRight, Me.Page)
            btnSeeDispenser_Click(Nothing, Nothing)
            Exit Sub
        End If

        If Not SLOT_CAN_RECIEVE_SIM(SLOT, SCAN_SIM_ID) Then
            Message_Toastr("Slot จะต้องบรรจุสินค้าชนิดเดียวกัน<br>และต้องมีพื้นที่เพียงพอ<br>กรุณาตรวจสอบอีกครั้ง", ToastrMode.Warning, ToastrPositon.TopRight, Me.Page, 8000)
            Exit Sub
        End If

        ''----------  Check Available Quantity ------------
        Dim FreeSpace As Integer = CALCULATE_SLOT_FREE_SPACE_FOR_SIM(SLOT, SCAN_SIM_ID)
        If FreeSpace = 0 Then
            Message_Toastr("Slot ไม่สามารถรับสินค้าได้", ToastrMode.Warning, ToastrPositon.TopRight, Me.Page)
            Exit Sub
        ElseIf MoveList.Count > FreeSpace Then
            Message_Toastr("Slot นี้สามารถใส่สินค้าได้ " & FreeSpace & " ชิ้นเท่านั้น", ToastrMode.Warning, ToastrPositon.TopRight, Me.Page)
            Exit Sub
        End If

        '------------ Update Data -------------
        For i As Integer = 0 To MoveList.Count - 1
            Dim DR As DataRow = MoveList(i)
            DR("CURRENT") = SLOT.SLOT_NAME
        Next

        '----------- Bind Right Side ----------
        SCAN_SIM_ID = SCAN_SIM_ID
        BindScanSIM()
        '----------- Bind Left Side ----------
        BindDispenserSIM()
        BindSlotSIM(SLOT)
    End Sub
#End Region

#Region "Command"
    Public Function Save() As Boolean

        Dim result As Boolean = False
        Dim KT As DataTable = BL.GetList_Kiosk(KO_ID)
        Dim KO_Code As String = KT.Rows(0).Item("KO_CODE")

        Dim SQL As String = ""
        For i As Integer = 0 To STOCK_DATA.Rows.Count - 1

            Dim DR As DataRow = STOCK_DATA.Rows(i)

            Dim SIM_ID As Integer = DR("SIM_ID")
            Dim PRODUCT_CODE As String = DR("PRODUCT_CODE")
            Dim PRODUCT_NAME As String = DR("PRODUCT_NAME")
            Dim SERIAL_NO As String = DR("SERIAL_NO")

            If Not IsDBNull(DR("RECENT")) And Not IsDBNull(DR("CURRENT")) Then

                '-------------------- Not Changed------------------
                If DR("RECENT") = DR("CURRENT") Then Continue For
                '------------------ Move Other Slot ---------------
                Dim SLOT_FROM As Integer = Dispenser.GET_DEVICE_ID_FROM_SLOT_NAME(DR("RECENT"))
                Dim SLOT_TO As Integer = Dispenser.GET_DEVICE_ID_FROM_SLOT_NAME(DR("CURRENT"))
                SQL = "SELECT * FROM TB_SIM_SERIAL WHERE SERIAL_NO='" & SERIAL_NO.Replace("'", "''") & "' AND D_ID=" & SLOT_FROM

                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                Dim DT As New DataTable
                DA.Fill(DT)
                If DT.Rows.Count > 0 Then
                    DT.Rows(0).Item("D_ID") = SLOT_TO
                    '------------------ Save Stock ---------------
                    Dim cmd As New SqlCommandBuilder(DA)
                    DA.Update(DT)
                    '------------------ Keep Log ---------------
                    BL.Save_SIM_Movement_Log(SHIFT_ID, SHIFT_STATUS, SIM_ID, SERIAL_NO, VDM_BL.StockMovementType.ChangeSlot, DR("RECENT"), SLOT_FROM, DR("CURRENT"), SLOT_TO, "ย้าย Slot จาก " & DR("CURRENT") & " ไปยัง " & DR("CURRENT"), Session("USER_ID"), Now)
                End If

            ElseIf Not IsDBNull(DR("RECENT")) And IsDBNull(DR("CURRENT")) Then

                '--------------- Move Out / CheckOut Stock --------
                Dim SLOT_FROM As Integer = Dispenser.GET_DEVICE_ID_FROM_SLOT_NAME(DR("RECENT"))
                SQL = "SELECT * FROM TB_SIM_SERIAL WHERE SERIAL_NO='" & SERIAL_NO.Replace("'", "''") & "' AND D_ID=" & SLOT_FROM
                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                Dim DT As New DataTable
                DA.Fill(DT)
                If DT.Rows.Count > 0 Then
                    DT.Rows(0).Delete()
                    '------------------ Save Stock ---------------
                    Dim cmd As New SqlCommandBuilder(DA)
                    DA.Update(DT)
                    '------------------ Keep Log ---------------
                    BL.Save_SIM_Movement_Log(SHIFT_ID, SHIFT_STATUS, SIM_ID, SERIAL_NO, VDM_BL.StockMovementType.CheckOut, DR("RECENT"), SLOT_FROM, "Stores หลัก", 0, "ย้ายออกจาก " & KO_Code & " ช่อง " & DR("RECENT") & " ไปยัง Store หลัก", Session("USER_ID"), Now)
                End If

            ElseIf IsDBNull(DR("RECENT")) And Not IsDBNull(DR("CURRENT")) Then
                '----------------- CheckIn By Scan ----------------
                Dim SLOT_TO As Integer = Dispenser.GET_DEVICE_ID_FROM_SLOT_NAME(DR("CURRENT"))
                SQL = "SELECT TOP 0 * FROM TB_SIM_SERIAL"
                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                Dim DT As New DataTable
                DA.Fill(DT)
                Dim R As DataRow = DT.NewRow
                R("KO_ID") = KO_ID
                R("D_ID") = SLOT_TO
                R("SIM_ID") = SIM_ID
                R("SERIAL_NO") = SERIAL_NO
                R("CheckIn_Time") = Now
                R("CheckIn_By") = Session("USER_ID")
                DT.Rows.Add(R)
                '------------------ Save Stock ---------------
                Dim cmd As New SqlCommandBuilder(DA)
                DA.Update(DT)
                '------------------ Keep Log ---------------
                BL.Save_SIM_Movement_Log(SHIFT_ID, SHIFT_STATUS, SIM_ID, SERIAL_NO, VDM_BL.StockMovementType.CheckIn, "Stores หลัก", 0, DR("CURRENT"), SLOT_TO, "สแกนของเข้าตู้ช่อง " & DR("CURRENT"), Session("USER_ID"), Now)

            Else 'If IsDBNull(DR("RECENT")) And IsDBNull(DR("CURRENT")) Then
                '----------- Do Nothing / Scan But Unmanaged---------------

                '------------------ Keep Log ---------------
                BL.Save_SIM_Movement_Log(SHIFT_ID, SHIFT_STATUS, SIM_ID, SERIAL_NO, VDM_BL.StockMovementType.CheckIn, "Stores หลัก", 0, KO_Code, 0, "สแกนของแต่ไม่เอาเข้าตู้", Session("USER_ID"), Now)
            End If
        Next

        BindData()
        Return True

    End Function

#End Region

#Region "DragDrop"

    '---------------- ทุกครั้งที่ View เปลี่ยน ----------------
    Public Sub ImplementDragDrop()

        ConfigDragDropListener(btnDropListener, txtDragType, txtDragArg, txtDropType, txtDropArg, Me.Page)

        Dim Slots As List(Of UC_SIM_Slot) = Dispenser.Slots

        '--------------------------- Drag from Event --------------------------
        '------------ Drag SCAN GROUP --------------
        For Each Item As RepeaterItem In rptSIMType.Items
            If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For
            Dim lnk As LinkButton = Item.FindControl("lnk")
            ImplementObjectDragable(lnk, "SCAN_SIM", lnk.CommandArgument)
        Next
        ImplementObjectDragable(imgScan, "SCAN_SIM", SCAN_SIM_ID)

        '------------ Drag SCAN ITEM ---------------
        For Each Item As RepeaterItem In rptScan.Items
            If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For
            Dim lblSerial As Label = Item.FindControl("lblSerial")
            Dim tr As HtmlTableRow = Item.FindControl("tr")
            ImplementObjectDragable(tr, "SCAN_SERIAL", lblSerial.Attributes("SERIAL_NO"))
        Next

        '------------ Drag SLOT SIM SERIAL-------
        For Each Item As RepeaterItem In rptSlot.Items
            If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For
            Dim lblSerial As Label = Item.FindControl("lblSerial")
            Dim tr As HtmlTableRow = Item.FindControl("tr")
            ImplementObjectDragable(tr, "SLOT_SERIAL", lblSerial.Attributes("SERIAL_NO"))
        Next

        '------------ Drag SLOT---------------------
        For i As Integer = 0 To Slots.Count - 1
            ImplementObjectDragable(Slots(i).TAG, "SLOT", Slots(i).SLOT_NAME)
            ImplementObjectDragable(Slots(i).ICON, "SLOT", Slots(i).SLOT_NAME)
        Next
        ImplementObjectDragable(imgSlot, "SLOT", SLOT_NAME)

        '--------------------------- Drop to Event ---------------------------
        '------------ DROP TO SCAN -------------
        ImplementObjectDropable(pnlScan, "SCAN", "")

        '------------ DROP TO SLOT -------------
        ImplementObjectDropable(pnlSlotSIM, "SLOT", SLOT_NAME)
        For i As Integer = 0 To Slots.Count - 1
            ImplementObjectDropable(Slots(i).TAG, "SLOT", Slots(i).SLOT_NAME)
        Next

    End Sub

    Private ReadOnly Property DragType As String
        Get
            Return txtDragType.Text
        End Get
    End Property

    Private ReadOnly Property DragArg As String
        Get
            Return txtDragArg.Text
        End Get
    End Property

    Private ReadOnly Property DropType As String
        Get
            Return txtDropType.Text
        End Get
    End Property

    Private ReadOnly Property DropArg As String
        Get
            Return txtDropArg.Text
        End Get
    End Property

    Private Sub btnDropListener_Click(sender As Object, e As EventArgs) Handles btnDropListener.Click
        Select Case DropType
            Case "SCAN"
                Select Case DragType
                    Case "SLOT"
                        '----------- ลากมาจาก
                        Dim slot_name As String = DragArg
                        STOCK_DATA.DefaultView.RowFilter = "CURRENT='" & slot_name & "'"
                        For i As Integer = STOCK_DATA.DefaultView.Count - 1 To 0 Step -1
                            STOCK_DATA.DefaultView(i).Row.Item("CURRENT") = DBNull.Value
                        Next
                        STOCK_DATA.DefaultView.RowFilter = ""
                        SCAN_SIM_ID = SCAN_SIM_ID
                        BindScanSIM()
                        If DEVICE_ID > 0 Then
                            BindSlotSIM(DEVICE_ID) '----------- ถ้า Show หน้า SLOT Info ก็ Update
                        Else
                            BindDispenserSIM() '----------- ถ้า Show หน้า SLOT Layout ก็ Update
                        End If

                    Case "SIM_SERIAL"
                        '--------ลากมาจากหน้าเลือก Repeater Slot ------------
                        Dim serial_no As String = DragArg
                        STOCK_DATA.DefaultView.RowFilter = "SERIAL_NO='" & serial_no.Replace("'", "''") & "'"
                        Dim targetRow As DataRow = STOCK_DATA.DefaultView(0).Row
                        Dim SelectedList As List(Of DataRow) = LeftSelectedList()

                        If SelectedList.IndexOf(targetRow) = -1 Then
                            '------------ ย้าย Row เดียว ----------
                            targetRow.Item("CURRENT") = DBNull.Value
                        Else
                            '------------ ย้าย ทั้ง Set ----------
                            For i As Integer = 0 To SelectedList.Count - 1
                                SelectedList(i).Item("CURRENT") = DBNull.Value
                            Next
                        End If

                        STOCK_DATA.DefaultView.RowFilter = ""
                        SCAN_SIM_ID = SCAN_SIM_ID
                        BindScanSIM()
                        BindDispenserSIM()
                        If DEVICE_ID > 0 Then
                            BindSlotSIM(DEVICE_ID) '----------- ถ้า Show หน้า SLOT Info ก็ Update
                        Else
                            BindDispenserSIM() '----------- ถ้า Show หน้า SLOT Layout ก็ Update
                        End If
                End Select
            Case "SLOT"

                Dim Target As UC_SIM_Slot = Dispenser.GET_SLOT_FROM_SLOT_NAME(DropArg)

                Select Case DragType
                    Case "SLOT"
                        Dim source As UC_SIM_Slot = Dispenser.GET_SLOT_FROM_SLOT_NAME(DragArg)
                        If Equals(Target, source) Then Exit Sub
                        If source.SIM_ID = 0 Then Exit Sub

                        If Not SLOT_CAN_RECIEVE_SIM(Target, source.SIM_ID) Then
                            Message_Toastr("Slot จะต้องบรรจุสินค้าชนิดเดียวกัน<br>และต้องมีพื้นที่เพียงพอ<br>กรุณาตรวจสอบอีกครั้ง", ToastrMode.Warning, ToastrPositon.TopRight, Me.Page, 8000)
                            Exit Sub
                        End If
                        STOCK_DATA.DefaultView.RowFilter = "CURRENT='" & source.SLOT_NAME & "'"
                        For i As Integer = STOCK_DATA.DefaultView.Count - 1 To 0 Step -1
                            STOCK_DATA.DefaultView(i).Row.Item("CURRENT") = Target.SLOT_NAME
                        Next

                        STOCK_DATA.DefaultView.RowFilter = ""
                        SCAN_SIM_ID = SCAN_SIM_ID
                        BindScanSIM()
                        BindDispenserSIM()

                    Case "SCAN_SIM"
                        Dim SIM_ID As Integer = DragArg
                        If Not SLOT_CAN_RECIEVE_SIM(Target, SIM_ID) Then
                            Message_Toastr("Slot จะต้องบรรจุสินค้าชนิดเดียวกัน<br>และต้องมีพื้นที่เพียงพอ<br>กรุณาตรวจสอบอีกครั้ง", ToastrMode.Warning, ToastrPositon.TopRight, Me.Page, 8000)
                            Exit Sub
                        End If
                        STOCK_DATA.DefaultView.RowFilter = "SIM_ID =" & SIM_ID & " AND CURRENT IS NULL"
                        For i As Integer = STOCK_DATA.DefaultView.Count - 1 To 0 Step -1
                            STOCK_DATA.DefaultView(i).Row.Item("CURRENT") = Target.SLOT_NAME
                        Next

                        STOCK_DATA.DefaultView.RowFilter = ""
                        SCAN_SIM_ID = SCAN_SIM_ID
                        BindScanSIM()
                        BindDispenserSIM()
                        If DEVICE_ID > 0 Then
                            BindSlotSIM(DEVICE_ID) '----------- ถ้า Show หน้า SLOT Info ก็ Update
                        Else
                            BindDispenserSIM() '----------- ถ้า Show หน้า SLOT Layout ก็ Update
                        End If

                    Case "SCAN_SERIAL"
                        Dim serial_no As String = DragArg
                        Dim slot_name As String = DropArg

                        STOCK_DATA.DefaultView.RowFilter = "SERIAL_NO='" & serial_no.Replace("'", "''") & "'"
                        Dim targetRow As DataRow = STOCK_DATA.DefaultView(0).Row
                        Dim SelectedList As List(Of DataRow) = RightSelectedList()
                        If SelectedList.IndexOf(targetRow) = -1 Then
                            '------------ ย้าย Row เดียว ----------
                            targetRow.Item("CURRENT") = slot_name
                        Else
                            '------------ ย้าย ทั้ง Set ----------
                            For i As Integer = 0 To SelectedList.Count - 1
                                SelectedList(i).Item("CURRENT") = slot_name
                            Next
                        End If

                        STOCK_DATA.DefaultView.RowFilter = ""
                        SCAN_SIM_ID = SCAN_SIM_ID
                        BindScanSIM()
                        BindDispenserSIM()
                        If DEVICE_ID > 0 Then
                            BindSlotSIM(DEVICE_ID) '----------- ถ้า Show หน้า SLOT Info ก็ Update
                        Else
                            BindDispenserSIM() '----------- ถ้า Show หน้า SLOT Layout ก็ Update
                        End If
                End Select
        End Select
    End Sub
#End Region


End Class