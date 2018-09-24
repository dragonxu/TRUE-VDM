Imports System.Data
Imports System.Data.SqlClient
Imports VDM

Public Class TestScan
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL

    Public Property KO_ID As Integer
        Get
            Return pnlScanProduct.Attributes("KO_ID")
        End Get
        Set(value As Integer)
            pnlScanProduct.Attributes("KO_ID") = value
        End Set
    End Property

    Public Property SHOP_CODE As String
        Get
            Return pnlScanProduct.Attributes("SHOP_CODE")
        End Get
        Set(value As String)
            pnlScanProduct.Attributes("SHOP_CODE") = value
        End Set
    End Property

    Public ReadOnly Property VW_ALL_PRODUCT As DataTable
        Get
            If IsNothing(Session("VW_ALL_PRODUCT_" & MY_UNIQUE_ID)) Then
                Dim SQL As String = "SELECT * FROM VW_ALL_PRODUCT"
                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                Dim DT As New DataTable
                DA.Fill(DT)
                Session("VW_ALL_PRODUCT_" & MY_UNIQUE_ID) = DT
            End If
            Return Session("VW_ALL_PRODUCT_" & MY_UNIQUE_ID)
        End Get
    End Property

    Public Property PixelPerMM As Double
        Get
            Return Shelf.PixelPerMM
        End Get
        Set(value As Double)
            Shelf.PixelPerMM = value
        End Set
    End Property

    Private ReadOnly Property MY_UNIQUE_ID() As String
        Get
            If pnlScanProduct.Attributes("MY_UNIQUE_ID") = "" Then
                pnlScanProduct.Attributes("MY_UNIQUE_ID") = GenerateNewUniqueID() '---- Needed ---------
            End If
            Return pnlScanProduct.Attributes("MY_UNIQUE_ID")
        End Get
    End Property

    Public Property STOCK_DATA As DataTable
        Get
            If IsNothing(Session("STOCK_DATA_" & MY_UNIQUE_ID)) Then
                Dim SQL As String = "SELECT PRODUCT_ID,PRODUCT_CODE,PRODUCT_NAME,SERIAL_NO,SLOT_NAME RECENT,SLOT_NAME [CURRENT]" & vbLf
                SQL &= "FROM VW_CURRENT_PRODUCT_STOCK" & vbLf
                SQL &= "WHERE KO_ID=" & KO_ID
                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                Dim DT As New DataTable
                DA.Fill(DT)
                Session("STOCK_DATA_" & MY_UNIQUE_ID) = DT
            End If
            Return Session("STOCK_DATA_" & MY_UNIQUE_ID)
        End Get
        Set(value As DataTable)
            Session("STOCK_DATA_" & MY_UNIQUE_ID) = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Session("USER_ID") = 1 '--------------- For Test ---------------
            KO_ID = 1 '--------------- For Test ---------------
            BindData()
        Else
            initFormPlugin()
        End If

    End Sub

    Public Sub BindData()
        'MY_UNIQUE_ID = GenerateNewUniqueID()
        pnlScanProduct.Attributes("MY_UNIQUE_ID") = GenerateNewUniqueID()
        BindShelfLayout()
        SCAN_PRODUCT_ID = -1
        BindScanProduct()
        '-------- Left Side -------
        ResetProductSlot()
        BindShelfProduct()
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

    Private Sub BindShelfLayout() '------------ เรียกครั้งแรกครั้งเดียว ---------------

        BL.Bind_Product_Shelf_Layout(Shelf, KO_ID)

        '-------------- Configure ------------
        Shelf.ShowAddFloor = False
        Shelf.ShowEditShelf = False
        Shelf.HideFloorName()
        Shelf.HideFloorMenu()

        ConfigShelfLayout()
    End Sub

    Private Sub ConfigShelfLayout()
        '------------ Hide All Scale----------
        For i As Integer = 0 To Shelf.Slots.Count - 1
            With Shelf.Slots(i)
                .ShowScale = False
                .ShowProductCode = False
            End With
        Next
        For i As Integer = 0 To Shelf.Floors.Count - 1
            Shelf.Floors(i).ShowScale = False
        Next
        Shelf.ShowScale = False
    End Sub

    Private Sub btnZoomIn_Click(sender As Object, e As EventArgs) Handles btnZoomIn.Click
        PixelPerMM += 0.05
        ConfigShelfLayout()
        BindShelfProduct()
    End Sub

    Private Sub btnZoomOut_Click(sender As Object, e As EventArgs) Handles btnZoomOut.Click
        If PixelPerMM <= 0.1 Then Exit Sub
        PixelPerMM -= 0.05
        ConfigShelfLayout()
        BindShelfProduct()
    End Sub

    Private Sub btnZoomReset_Click(sender As Object, e As EventArgs) Handles btnZoomReset.Click
        PixelPerMM = 0.25
        ConfigShelfLayout()
        BindShelfProduct()
    End Sub

    Private Property SCAN_PRODUCT_ID As Integer
        Get
            For Each Item As RepeaterItem In rptProductTab.Items
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

            Dim Col() As String = {"PRODUCT_ID", "PRODUCT_NAME"}
            DT.DefaultView.RowFilter = "CURRENT IS NULL"
            DT = DT.DefaultView.ToTable(True, Col)

            rptProductTab.DataSource = DT
            rptProductTab.DataBind()
            DT.Dispose()

            '------------- Set UI Active-------------
            For Each Item As RepeaterItem In rptProductTab.Items
                If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For
                Dim lnk As LinkButton = Item.FindControl("lnk")
                Dim li As HtmlGenericControl = Item.FindControl("li")
                li.Attributes("class") = ""
                If lnk.CommandArgument = value Then
                    li.Attributes("class") = "active"
                End If
            Next

            SuggessProductSlot()

            '------------ Check Left Product Slot-------
            If SLOT_ID > 0 Then '------- Show Slot อยู่ -------
                If SLOT_PRODUCT_ID > 0 Then '------------- บรรจุ Product --------
                    If SLOT_PRODUCT_ID <> SCAN_PRODUCT_ID Then '----------- เป็นคนละ Product กัน----------
                        ResetProductSlot()
                    End If
                End If
            End If
        End Set
    End Property

#Region "Slot Panel"
    Public Property SLOT_ID As Integer
        Get
            Return lblSlotName.Attributes("SLOT_ID")
        End Get
        Set(value As Integer)
            lblSlotName.Attributes("SLOT_ID") = value
        End Set
    End Property

    Private Sub ResetProductSlot()

        Shelf.Deselect_All()
        SLOT_ID = 0
        SLOT_PRODUCT_ID = 0

        lblSlotName.Text = ""
        imgSlot_Product.ImageUrl = "../images/TransparentDot.png" '"../RenderImage.aspx?Mode=D&UID=0&Entity=Product&LANG=1"
        lblSlot_ProductCode.Text = "-"
        lblSlot_ProductName.Text = ""
        lblSlot_ProductDesc.Text = ""
        tagSlot_Empty.Visible = True
        imgSlot_Brand.Visible = False
        imgSlot_Brand.ImageUrl = "../images/TransparentDot.png" '"../RenderImage.aspx?Mode=D&UID=0&Entity=Brand&LANG=1"

        pnlSlotProductSize.Visible = False
        lblSlot_Product_Width.Text = "-"
        lblSlot_Product_Height.Text = "-"
        lblSlot_Product_Depth.Text = "-"
        lblSlot_Width.Text = "-"
        lblSlot_Height.Text = "-"
        lblSlot_Depth.Text = "-"
        lblSlotQuantity.Text = "-"
        pnlSlotCapacity.Visible = False
        levelProduct.Visible = False
        lblFreeSpace.Text = "-"
        lblMaxSpace.Text = "-"

        SetSlotCheck(chkSlot, False)
        chkSlot.Visible = False
        rptSlot.DataSource = Nothing
        rptSlot.DataBind()
        btnMoveRight.Visible = False

        pnlSlot.Visible = False
        Shelf.Visible = True
        pnlZoom.Visible = True
    End Sub

    Private Sub BindShelfProduct()

        Dim DT As DataTable = STOCK_DATA.Copy
        DT.Columns("CURRENT").ColumnName = "SLOT_NAME"
        BL.Bind_Product_Shelf_Stock(Shelf, DT, VW_ALL_PRODUCT)

    End Sub

    Private Property SLOT_PRODUCT_ID As Integer
        Get
            Return pnlSlot.Attributes("PRODUCT_ID")
        End Get
        Set(value As Integer)
            pnlSlot.Attributes("PRODUCT_ID") = value
        End Set
    End Property

    Private Sub Shelf_SlotSelecting(ByRef Sender As UC_Product_Slot) Handles Shelf.SlotSelecting
        If Sender.PRODUCT_ID > 0 Then '----------- ไม่ว่าง
            If SCAN_PRODUCT_ID > 0 Then
                If Sender.PRODUCT_ID <> SCAN_PRODUCT_ID Then
                    SCAN_PRODUCT_ID = Sender.PRODUCT_ID
                    BindScanProduct()
                End If
            End If
        End If
        BindSlotProduct(Sender)
    End Sub

    Private Sub btnSeeShelf_Click(sender As Object, e As EventArgs) Handles btnSeeShelf.Click
        ResetProductSlot()
        SuggessProductSlot()
    End Sub

    Private Sub BindSlotProduct(ByVal SLOT_ID As Integer)
        Dim SLOTS As List(Of UC_Product_Slot) = Shelf.Slots
        Dim SLOT As UC_Product_Slot = Nothing
        For s As Integer = 0 To SLOTS.Count - 1
            If SLOTS(s).SLOT_ID = Me.SLOT_ID Then
                SLOT = SLOTS(s)
                Exit For
            End If
        Next
        BindSlotProduct(SLOT)
    End Sub

    Private Sub BindSlotProduct(ByRef Sender As UC_Product_Slot)

        ResetProductSlot()

        SLOT_ID = Sender.SLOT_ID
        lblSlotName.Text = Sender.SLOT_NAME
        lblSlot_Width.Text = Sender.SLOT_WIDTH
        lblSlot_Height.Text = Sender.SLOT_HEIGHT
        lblSlot_Depth.Text = Shelf.SHELF_DEPTH

        VW_ALL_PRODUCT.DefaultView.RowFilter = "PRODUCT_ID=" & Sender.PRODUCT_ID

        If VW_ALL_PRODUCT.DefaultView.Count > 0 Then
            Dim DV As DataRowView = VW_ALL_PRODUCT.DefaultView(0)

            SLOT_PRODUCT_ID = DV("PRODUCT_ID")

            imgSlot_Product.ImageUrl = "../RenderImage.aspx?Mode=D&UID=" & Sender.PRODUCT_ID & "&Entity=Product&LANG=1&DI=images/TransparentDot.png"
            lblSlot_ProductCode.Text = DV("PRODUCT_CODE")
            lblSlot_ProductName.Text = DV("DISPLAY_NAME_TH")
            lblSlot_ProductDesc.Text = DV("DESCRIPTION_TH").ToString
            tagSlot_Empty.Visible = False
            imgSlot_Brand.Visible = True
            imgSlot_Brand.ImageUrl = "../RenderImage.aspx?Mode=D&UID=" & DV("BRAND_ID").ToString & "&Entity=Brand&LANG=1&DI=images/TransparentDot.png"

            lblSlotQuantity.Text = "<small>X</small> " & Sender.PRODUCT_QUANTITY

            pnlSlotProductSize.Visible = True
            If Not IsDBNull(DV("WIDTH")) Then
                lblSlot_Product_Width.Text = DV("WIDTH")
            End If
            If Not IsDBNull(DV("HEIGHT")) Then
                lblSlot_Product_Height.Text = DV("HEIGHT")
            End If
            If Not IsDBNull(DV("DEPTH")) Then
                lblSlot_Product_Depth.Text = DV("DEPTH")
                pnlSlotCapacity.Visible = True
                levelProduct.Visible = True

                Dim MaxQuantity As Integer = Math.Floor(Shelf.SHELF_DEPTH / DV("DEPTH"))
                lblMaxSpace.Text = MaxQuantity
                lblFreeSpace.Text = MaxQuantity - Sender.PRODUCT_QUANTITY
                levelProduct.Width = Unit.Percentage(Sender.PRODUCT_QUANTITY * 100 / MaxQuantity)
            End If
        End If

        SetSlotCheck(chkSlot, False)

        Dim DT As DataTable = STOCK_DATA.Copy
        DT.DefaultView.RowFilter = "CURRENT='" & lblSlotName.Text & "'"
        DT = DT.DefaultView.ToTable

        rptSlot.DataSource = DT
        rptSlot.DataBind()

        btnMoveRight.Visible = DT.Rows.Count > 0
        chkSlot.Visible = DT.Rows.Count > 0

        pnlSlot.Visible = True
        Shelf.Visible = False
        pnlZoom.Visible = False
    End Sub

    Private Sub chkSlot_Click(sender As Object, e As EventArgs) Handles chkSlot.Click
        Dim Checked As Boolean = IsButtonCheck(chkSlot)
        For Each Item As RepeaterItem In rptSlot.Items
            If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For
            Dim chk As LinkButton = Item.FindControl("chk")
            SetSlotCheck(chk, Not Checked)
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
        Dim PRODUCT_ID As Integer = SLOT_PRODUCT_ID
        Dim SLOT_ID As Integer = Me.SLOT_ID
        'Dim SLOTS As List(Of UC_Product_Slot) = Shelf.Slots
        'Dim SLOT As UC_Product_Slot = Nothing
        'For s As Integer = 0 To SLOTS.Count - 1
        '    If SLOTS(s).SLOT_ID = SLOT_ID Then
        '        SLOT = SLOTS(s)
        '        Exit For
        '    End If
        'Next

        '----------- Bind Right Side ----------
        SCAN_PRODUCT_ID = PRODUCT_ID
        BindScanProduct()

        '----------- Bind LEFT Side ----------
        BindShelfProduct()
        BindSlotProduct(SLOT_ID)

    End Sub

    Private Sub SuggessProductSlot()

        Shelf.Deselect_All()
        Dim Slots As List(Of UC_Product_Slot) = Shelf.Slots
        Dim PRODUCT_ID As Integer = SCAN_PRODUCT_ID
        For s As Integer = 0 To Slots.Count - 1
            If SLOT_CAN_RECIEVE_PRODUCT(Slots(s), PRODUCT_ID) Then
                Slots(s).HighLight = UC_Product_Slot.HighLightMode.GreenSolid
            End If
        Next

    End Sub

    Private Function SLOT_CAN_RECIEVE_PRODUCT(ByRef SLOT As UC_Product_Slot, ByVal PRODUCT_ID As Integer) As Boolean

        '------------- Get Product Info -----------
        VW_ALL_PRODUCT.DefaultView.RowFilter = "PRODUCT_ID=" & PRODUCT_ID
        If VW_ALL_PRODUCT.DefaultView.Count = 0 Then Return False

        Dim PRODUCT_WIDTH As Integer = 0
        Dim PRODUCT_HEIGHT As Integer = 0
        Dim PRODUCT_DEPTH As Integer = 0

        Dim DV As DataRowView = VW_ALL_PRODUCT.DefaultView(0)
        If Not IsDBNull(DV("WIDTH")) Then PRODUCT_WIDTH = DV("WIDTH")
        If Not IsDBNull(DV("HEIGHT")) Then PRODUCT_HEIGHT = DV("HEIGHT")
        If Not IsDBNull(DV("DEPTH")) Then PRODUCT_DEPTH = DV("DEPTH")

        If SLOT.PRODUCT_QUANTITY <= 0 Then '---- Slot ว่าง -----
            Return PRODUCT_WIDTH <= SLOT.SLOT_WIDTH And PRODUCT_HEIGHT <= SLOT.FLOOR_HEIGHT
        ElseIf SLOT.PRODUCT_ID = PRODUCT_ID And PRODUCT_DEPTH > 0 Then  '----------- Contained Same Product --------------
            Dim MaxQuantity As Integer = Math.Floor(Shelf.SHELF_DEPTH / PRODUCT_DEPTH)
            Return SLOT.PRODUCT_QUANTITY < MaxQuantity
        ElseIf SLOT.PRODUCT_ID = PRODUCT_ID Then
            Return True
        Else '----------- Contained Other Product --------------
            Return False
        End If

    End Function

#End Region

    Private Sub rptProduct_ItemCreated(sender As Object, e As RepeaterItemEventArgs) Handles rptSlot.ItemCreated, rptScan.ItemCreated
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim chk As LinkButton = e.Item.FindControl("chk")
        Dim del As LinkButton = e.Item.FindControl("del")

        Dim scriptMan As ScriptManager = ScriptManager.GetCurrent(Page)
        scriptMan.RegisterAsyncPostBackControl(chk)
        scriptMan.RegisterAsyncPostBackControl(del)

    End Sub

    Private Sub rptProduct_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptScan.ItemDataBound, rptSlot.ItemDataBound
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

    End Sub

    Private Sub rptScan_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptScan.ItemCommand
        Select Case e.CommandName
            Case "Check"
                Dim chk As LinkButton = e.Item.FindControl("chk")
                SetScanCheck(chk, Not IsButtonCheck(chk))
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
                BindScanProduct()
        End Select
    End Sub

    Private Sub rptSlot_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptSlot.ItemCommand
        Select Case e.CommandName
            Case "Check"
                Dim chk As LinkButton = e.Item.FindControl("chk")
                SetSlotCheck(chk, Not IsButtonCheck(chk))
                '------------ Update Header------------
                SetSlotCheck(chkSlot, False)
                For Each Item As RepeaterItem In rptSlot.Items
                    If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For
                    Dim _chk As LinkButton = Item.FindControl("chk")
                    If Not IsButtonCheck(_chk) Then Exit Sub
                Next
                SetSlotCheck(chkSlot, True)
            Case "Delete"
                Dim lblSerial As Label = e.Item.FindControl("lblSerial")
                STOCK_DATA.DefaultView.RowFilter = "SERIAL_NO='" & lblSerial.Attributes("SERIAL_NO").Replace("'", "''") & "'"
                If STOCK_DATA.DefaultView.Count > 0 Then
                    STOCK_DATA.DefaultView(0).Row.Delete()
                End If
                STOCK_DATA.DefaultView.RowFilter = ""
                '-------------- Refresh -----------
                BindSlotProduct(SLOT_ID)
        End Select
    End Sub

#Region "Scan"


    Private Sub ResetScanProductTab() '------------- Reset เฉพาะ pnlProduct ที่เลือก ----------

        txtBarcode.Text = ""
        pnlScan.Visible = False
        imgScan_Product.ImageUrl = "../images/TransparentDot.png" ' "../RenderImage.aspx?Mode=D&UID=0&Entity=Product&LANG=1"
        lblScan_ProductCode.Text = "-"
        lblScan_ProductName.Text = ""
        lblScan_ProductDesc.Text = ""
        imgScan_Brand.ImageUrl = "../images/TransparentDot.png" '"../RenderImage.aspx?Mode=D&UID=0&Entity=Brand&LANG=1"
        lblScan_Product_Width.Text = "-"
        lblScan_Product_Height.Text = "-"
        lblScan_Product_Depth.Text = "-"
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
        Next
        SetScanCheck(chkScan, Not Checked)
    End Sub

    Private Sub rptProductTab_ItemCreated(sender As Object, e As RepeaterItemEventArgs) Handles rptProductTab.ItemCreated
        Dim lnk As LinkButton = e.Item.FindControl("lnk")
        Dim scriptMan As ScriptManager = ScriptManager.GetCurrent(Page)
        scriptMan.RegisterAsyncPostBackControl(lnk)
    End Sub

    Private Sub rptProductTab_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptProductTab.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim lnk As LinkButton = e.Item.FindControl("lnk")
        Dim img As Image = e.Item.FindControl("img")
        Dim lblName As Label = e.Item.FindControl("lblName")

        lnk.CommandArgument = e.Item.DataItem("PRODUCT_ID")
        img.ImageUrl = "../RenderImage.aspx?Mode=D&UID=" & e.Item.DataItem("PRODUCT_ID") & "&Entity=Product&LANG=1&DI=images/TransparentDot.png"

        '-------------- Get Total Product ---------------
        Dim DT As DataTable = STOCK_DATA.Copy
        DT.DefaultView.RowFilter = "PRODUCT_ID=" & e.Item.DataItem("PRODUCT_ID") & " AND CURRENT IS NULL"
        Dim Total As Integer = DT.DefaultView.Count
        DT.Dispose()

        lblName.Text = e.Item.DataItem("PRODUCT_NAME").ToString & " <b>(" & Total & ")</b>"
    End Sub

    Private Sub rptProductTab_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptProductTab.ItemCommand
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Select Case e.CommandName
            Case "Select"
                SCAN_PRODUCT_ID = e.CommandArgument
                BindScanProduct()
                SuggessProductSlot()
        End Select

    End Sub

    Private Sub BindScanProduct()

        ResetScanProductTab()

        Dim PRODUCT_ID As Integer = SCAN_PRODUCT_ID
        If PRODUCT_ID = -1 Then Exit Sub

        VW_ALL_PRODUCT.DefaultView.RowFilter = "PRODUCT_ID=" & PRODUCT_ID
        Dim DT As DataTable = VW_ALL_PRODUCT.DefaultView.ToTable
        If DT.Rows.Count = 0 Then
            Message_Toastr("ไม่พบข้อมูล Product", ToastrMode.Danger, ToastrPositon.TopLeft, Me.Page)
            Exit Sub
        End If

        pnlScan.Visible = True
        imgScan_Product.ImageUrl = "../RenderImage.aspx?Mode=D&UID=" & PRODUCT_ID & "&Entity=Product&LANG=1&DI=images/TransparentDot.png"
        lblScan_ProductCode.Text = DT.Rows(0).Item("PRODUCT_CODE").ToString
        lblScan_ProductName.Text = DT.Rows(0).Item("DISPLAY_NAME_TH").ToString
        lblScan_ProductDesc.Text = DT.Rows(0).Item("DESCRIPTION_TH").ToString
        imgScan_Brand.ImageUrl = "../RenderImage.aspx?Mode=D&UID=" & DT.Rows(0).Item("BRAND_ID") & "&Entity=Brand&LANG=1&DI=images/TransparentDot.png"

        If Not IsDBNull(DT.Rows(0).Item("WIDTH")) Then
            lblScan_Product_Width.Text = DT.Rows(0).Item("WIDTH")
        End If
        If Not IsDBNull(DT.Rows(0).Item("HEIGHT")) Then
            lblScan_Product_Height.Text = DT.Rows(0).Item("HEIGHT")
        End If
        If Not IsDBNull(DT.Rows(0).Item("DEPTH")) Then
            lblScan_Product_Depth.Text = DT.Rows(0).Item("DEPTH")
        End If

        DT = STOCK_DATA.Copy
        DT.DefaultView.RowFilter = "PRODUCT_ID=" & PRODUCT_ID & " AND CURRENT IS NULL"
        DT = DT.DefaultView.ToTable

        rptScan.DataSource = DT
        rptScan.DataBind()

        btnMoveLeft.Visible = DT.Rows.Count > 0

    End Sub

    Private Sub btnBarcode_Click(sender As Object, e As EventArgs) Handles btnBarcode.Click
        Dim Barcode As String = txtBarcode.Text
        txtBarcode.Text = ""
        If Barcode = "" Then Exit Sub

        Dim ScanResult As VDM_BL.BarcodeScanResult = BL.Get_Product_Barcode_scan_Result(SHOP_CODE, Barcode)

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
        DR("PRODUCT_ID") = ScanResult.PRODUCT_ID
        DR("PRODUCT_CODE") = ScanResult.PRODUCT_CODE
        DR("PRODUCT_NAME") = ScanResult.DISPLAY_NAME
        If ScanResult.SERIAL_NO = "" Then
            ScanResult.SERIAL_NO = GenerateNewUniqueID()
        End If
        DR("SERIAL_NO") = ScanResult.SERIAL_NO
        DR("RECENT") = DBNull.Value
        DR("CURRENT") = DBNull.Value
        STOCK_DATA.Rows.Add(DR)

        SCAN_PRODUCT_ID = ScanResult.PRODUCT_ID '---------- Update Tab And Quantity ----------
        BindScanProduct()

        If ScanResult.IS_SERIAL Then
            Message_Toastr(ScanResult.DISPLAY_NAME & " : " & ScanResult.SERIAL_NO, ToastrMode.Success, ToastrPositon.Top, Me.Page)
        Else
            Message_Toastr(ScanResult.PRODUCT_CODE & " : " & ScanResult.DISPLAY_NAME, ToastrMode.Success, ToastrPositon.Top, Me.Page)
        End If

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

        If SLOT_ID <= 0 Then
            Message_Toastr("เลือก SLOT ที่จะเติม", ToastrMode.Warning, ToastrPositon.TopRight, Me.Page)
            btnSeeShelf_Click(Nothing, Nothing)
            Exit Sub
        End If

        Dim SLOTS As List(Of UC_Product_Slot) = Shelf.Slots
        Dim SLOT As UC_Product_Slot = Nothing
        For s As Integer = 0 To SLOTS.Count - 1
            If SLOTS(s).SLOT_ID = SLOT_ID Then
                SLOT = SLOTS(s)
                Exit For
            End If
        Next

        If IsNothing(SLOT) Then
            Message_Toastr("เลือก SLOT ที่จะเติม", ToastrMode.Warning, ToastrPositon.TopRight, Me.Page)
            btnSeeShelf_Click(Nothing, Nothing)
            Exit Sub
        End If

        If Not SLOT_CAN_RECIEVE_PRODUCT(SLOT, SCAN_PRODUCT_ID) Then
            Message_Toastr("Slot จะต้องบรรจุสินค้าชนิดเดียวกัน<br>และต้องมีพื้นที่เพียงพอ<br>กรุณาตรวจสอบอีกครั้ง", ToastrMode.Warning, ToastrPositon.TopRight, Me.Page, 8000)
            Exit Sub
        End If

        ''----------  Check Available Quantity ------------
        Dim PRODUCT_DEPTH As Integer = lblScan_Product_Depth.Text
        If PRODUCT_DEPTH > 0 Then

            Dim MaxQuantity As Integer = Math.Floor(Shelf.SHELF_DEPTH / PRODUCT_DEPTH)
            Dim AvailableQuantity As Integer = MaxQuantity - SLOT.PRODUCT_QUANTITY

            If MoveList.Count > AvailableQuantity Then
                Message_Toastr("Slot นี้สามารถใส่สินค้าได้ " & AvailableQuantity & " ชิ้นเท่านั้น", ToastrMode.Warning, ToastrPositon.TopRight, Me.Page)
                Exit Sub
            End If
        End If

        '------------ Update Data -------------
        For i As Integer = 0 To MoveList.Count - 1
            Dim DR As DataRow = MoveList(i)
            DR("CURRENT") = SLOT.SLOT_NAME
        Next

        '----------- Bind Right Side ----------
        SCAN_PRODUCT_ID = SCAN_PRODUCT_ID
        BindScanProduct()
        '----------- Bind Left Side ----------
        BindShelfProduct()
        BindSlotProduct(SLOT)
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

            Dim PRODUCT_ID As Integer = DR("PRODUCT_ID")
            Dim PRODUCT_CODE As String = DR("PRODUCT_CODE")
            Dim PRODUCT_NAME As String = DR("PRODUCT_NAME")
            Dim SERIAL_NO As String = DR("SERIAL_NO")


            If Not IsDBNull(DR("RECENT")) And Not IsDBNull(DR("CURRENT")) Then

                '-------------------- Not Changed------------------
                If DR("RECENT") = DR("CURRENT") Then Continue For

                '------------------ Move Other Slot ---------------
                Dim SLOT_FROM As Integer = Shelf.AccessSlotFromName(DR("RECENT")).SLOT_ID
                Dim SLOT_TO As Integer = Shelf.AccessSlotFromName(DR("CURRENT")).SLOT_ID
                SQL = "SELECT * FROM TB_PRODUCT_SERIAL WHERE SERIAL_NO='" & SERIAL_NO.Replace("'", "''") & "' AND SLOT_ID=" & SLOT_FROM
                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                Dim DT As New DataTable
                DA.Fill(DT)
                If DT.Rows.Count > 0 Then
                    DT.Rows(0).Item("SLOT_ID") = SLOT_TO
                    '------------------ Save Stock ---------------
                    Dim cmd As New SqlCommandBuilder(DA)
                    DA.Update(DT)
                    '------------------ Keep Log ---------------
                    BL.Save_Product_Movement_Log(PRODUCT_ID, SERIAL_NO, VDM_BL.StockMovementType.ChangeSlot, DR("RECENT"), DR("CURRENT"), "ย้าย Slot จาก " & DR("CURRENT") & " ไปยัง " & DR("CURRENT"), Session("USER_ID"), Now)
                End If

            ElseIf Not IsDBNull(DR("RECENT")) And IsDBNull(DR("CURRENT")) Then
                '--------------- Move Out / CheckOut Stock --------
                Dim SLOT_FROM As Integer = Shelf.AccessSlotFromName(DR("RECENT")).SLOT_ID
                SQL = "SELECT * FROM TB_PRODUCT_SERIAL WHERE SERIAL_NO='" & SERIAL_NO.Replace("'", "''") & "' AND SLOT_ID=" & SLOT_FROM
                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                Dim DT As New DataTable
                DA.Fill(DT)
                If DT.Rows.Count > 0 Then
                    DT.Rows(0).Delete()
                    '------------------ Save Stock ---------------
                    Dim cmd As New SqlCommandBuilder(DA)
                    DA.Update(DT)
                    '------------------ Keep Log ---------------
                    BL.Save_Product_Movement_Log(PRODUCT_ID, SERIAL_NO, VDM_BL.StockMovementType.CheckOut, DR("RECENT"), "Stores หลัก", "ย้ายออกจาก " & KO_Code & " ช่อง " & DR("RECENT") & " ไปยัง Store หลัก", Session("USER_ID"), Now)
                End If

            ElseIf IsDBNull(DR("RECENT")) And Not IsDBNull(DR("CURRENT")) Then
                '----------------- CheckIn By Scan ----------------
                Dim SLOT_TO As Integer = Shelf.AccessSlotFromName(DR("CURRENT")).SLOT_ID
                SQL = "SELECT TOP 0 * FROM TB_PRODUCT_SERIAL"
                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                Dim DT As New DataTable
                DA.Fill(DT)
                Dim R As DataRow = DT.NewRow
                R("SLOT_ID") = SLOT_TO
                R("PRODUCT_ID") = PRODUCT_ID
                R("SERIAL_NO") = SERIAL_NO
                R("CheckIn_Time") = Now
                DT.Rows.Add(R)
                '------------------ Save Stock ---------------
                Dim cmd As New SqlCommandBuilder(DA)
                DA.Update(DT)
                '------------------ Keep Log ---------------
                BL.Save_Product_Movement_Log(PRODUCT_ID, SERIAL_NO, VDM_BL.StockMovementType.CheckIn, "Stores หลัก", DR("CURRENT"), "สแกนของเข้าตู้ช่อง " & DR("CURRENT"), Session("USER_ID"), Now)

            Else 'If IsDBNull(DR("RECENT")) And IsDBNull(DR("CURRENT")) Then
                '----------- Do Nothing / Scan But Unmanaged---------------

                '------------------ Keep Log ---------------
                BL.Save_Product_Movement_Log(PRODUCT_ID, SERIAL_NO, VDM_BL.StockMovementType.CheckIn, "Stores หลัก", KO_Code, "สแกนของแต่ไม่เอาเข้าตู้", Session("USER_ID"), Now)
            End If
        Next

        BindData()
        Return True
    End Function


    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        BindData()
    End Sub

    Public Event Confirm(ByVal Sender As Object)
    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        RaiseEvent Confirm(Me)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Save()
    End Sub



#End Region

End Class