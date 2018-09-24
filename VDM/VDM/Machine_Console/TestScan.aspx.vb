Imports System.Data
Imports System.Data.SqlClient

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
                BindExistingStock() '------------- Get From Database -------------
            End If
            Return Session("STOCK_DATA_" & MY_UNIQUE_ID)
        End Get
        Set(value As DataTable)
            Session("STOCK_DATA_" & MY_UNIQUE_ID) = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            KO_ID = 1 '--------------- For Test ---------------
            BindShelf()
            SCAN_PRODUCT_ID = -1
            BindScanProduct()
        Else
            initFormPlugin()
        End If

    End Sub

#Region "CheckButton"

    Const CheckScanCss As String = "btn btn-success btn-icon-icon btn-sm mr5"
    Const CheckSlotCss As String = "btn btn-primary btn-icon-icon btn-sm mr5"
    Const UncheckCss As String = "btn btn-default btn-icon-icon btn-sm mr5"
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

    Private Sub BindShelf() '------------ เรียกครั้งแรกครั้งเดียว ---------------
        PixelPerMM = 0.25
        BL.Bind_Product_Shelf(Shelf, KO_ID)

        '-------------- Configure ------------
        Shelf.ShowAddFloor = False
        Shelf.ShowEditShelf = False
        Shelf.HideFloorName()
        Shelf.HideFloorMenu()

        '------------ Hide All Scale----------
        For i As Integer = 0 To Shelf.Slots.Count - 1
            Shelf.Slots(i).ShowScale = False
        Next
        For i As Integer = 0 To Shelf.Floors.Count - 1
            Shelf.Floors(i).ShowScale = False
        Next
        Shelf.ShowScale = False
    End Sub

    Private Sub BindExistingStock()
        Dim SQL As String = "SELECT PRODUCT_ID,PRODUCT_CODE,PRODUCT_NAME,SERIAL_NO,SLOT_NAME RECENT,SLOT_NAME [CURRENT]" & vbLf
        SQL &= "FROM VW_CURRENT_PRODUCT_STOCK" & vbLf
        SQL &= "WHERE KO_ID=" & KO_ID
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        STOCK_DATA = DT
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
            DT.DefaultView.RowFilter = ""
            DT = DT.DefaultView.ToTable(True, Col)

            rptProductTab.DataSource = DT
            rptProductTab.DataBind()
            DT.Dispose()

            For Each Item As RepeaterItem In rptProductTab.Items
                If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For
                Dim lnk As LinkButton = Item.FindControl("lnk")
                Dim li As HtmlGenericControl = Item.FindControl("li")
                li.Attributes("class") = ""
                If lnk.CommandArgument = value Then
                    li.Attributes("class") = "active"
                End If
            Next
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

    Private Sub ResetSlot()

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
        pnlSlotCapacity.Visible = False
        levelProduct.Visible = False
        lblFreeSpace.Text = "-"
        lblMaxSpace.Text = "-"

        SetSlotCheck(chkSlot, False)
        chkSlot.Visible = False
        rptSlotProduct.DataSource = Nothing
        rptSlotProduct.DataBind()
        btnMoveRight.Visible = False

        pnlSlot.Visible = False
        Shelf.Visible = True
    End Sub

#End Region

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

    Private Sub rptScan_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptScan.ItemDataBound

        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                Dim chk As LinkButton = e.Item.FindControl("chk")
                Dim lblCode As Label = e.Item.FindControl("lblCode")
                Dim lblSerial As Label = e.Item.FindControl("lblSerial")
                Dim lblRecent As Label = e.Item.FindControl("lblRecent")

                SetScanCheck(chk, False) '---------- Default Uncheck--------
                lblCode.Text = e.Item.DataItem("PRODUCT_CODE").ToString
                lblSerial.Text = e.Item.DataItem("SERIAL_NO").ToString
                If IsDBNull(e.Item.DataItem("RECENT")) Then
                    lblRecent.Text = "SCAN"
                    lblRecent.ForeColor = Drawing.Color.Green
                Else
                    lblRecent.Text = e.Item.DataItem("RECENT")
                    lblRecent.ForeColor = Drawing.Color.Blue
                End If

            Case ListItemType.Footer
                btnMoveLeft.Visible = rptScan.ItemType.Count > 0
        End Select

    End Sub

    Private Sub rptScan_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptScan.ItemCommand
        Select Case e.CommandName
            Case "Check"
                Dim chk As LinkButton = e.Item.FindControl("chk")
                SetScanCheck(chk, Not IsButtonCheck(chk))

                '------------ Update chkScan------------
                SetScanCheck(chkScan, False)
                For Each Item As RepeaterItem In rptScan.Items
                    If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For
                    Dim _chk As LinkButton = Item.FindControl("chk")
                    If Not IsButtonCheck(_chk) Then Exit Sub
                Next
                SetScanCheck(chkScan, True)

        End Select
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
        End Select

    End Sub

    Private Sub BindScanProduct()

        ResetScanProductTab()

        Dim PRODUCT_ID As Integer = SCAN_PRODUCT_ID
        If PRODUCT_ID = -1 Then Exit Sub

        Dim DT As DataTable = BL.Get_Product_Info_From_ID(PRODUCT_ID)
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

    Private Sub btnSeeShelf_Click(sender As Object, e As EventArgs) Handles btnSeeShelf.Click

    End Sub
End Class