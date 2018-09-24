Public Class UC_Product_Floor
    Inherits System.Web.UI.UserControl

    Const MarginWidth As Integer = 2 ' Padding 10x2 + border 2x2
    Const MarginHeight As Integer = 2 ' Padding 10x2 + border 2x2

    Dim BL As New VDM_BL



    Public ReadOnly Property PixelPerMM As Double
        Get
            Return ParentShelf.PixelPerMM
        End Get
    End Property

    Public ReadOnly Property ParentShelf As UC_Product_Shelf
        Get
            Try
                Return Me.Parent.Parent.Parent.Parent
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property

    Public Property FLOOR_NAME As String
        Get
            Return FloorLabel.Text
        End Get
        Set(value As String)
            FloorLabel.Text = value
            '------------- Update SlotName -----------
            For i As Integer = 0 To Slots.Count - 1
                Slots(i).SLOT_NAME = value & "-" & (i + 1)
            Next
        End Set
    End Property

    '--------------- Calculate MM------------
    Public Property FLOOR_HEIGHT As Integer
        Get
            Return lblHeight.Text
        End Get
        Set(value As Integer)
            Floor.Height = Unit.Pixel((value * PixelPerMM) - MarginHeight)
            lblHeight.Text = value
            '----------- Update Scale --------
            lblY1.Text = value + POS_Y
            '-------------------- Default Layout---------------------
            Floor.Width = Unit.Pixel((ParentShelf.SHELF_WIDTH * PixelPerMM) - MarginWidth)
            Floor.Style("right") = "0"
            '------------- Update SlotHeight -----------
            For i As Integer = 0 To Slots.Count - 1
                Slots(i).SLOT_HEIGHT = value
            Next
        End Set
    End Property

    Public Property POS_Y As Integer
        Get
            Return lblY.Text
        End Get
        Set(value As Integer)
            Floor.Style("bottom") = ((value * PixelPerMM) - MarginHeight) & "px"
            lblY.Text = value
            '----------- Update Scale --------
            lblY1.Text = value + FLOOR_HEIGHT
            '-------------------- Default Layout---------------------
            Floor.Width = Unit.Pixel((ParentShelf.SHELF_WIDTH * PixelPerMM) - MarginWidth)
            Floor.Style("right") = "0"
            '------------- Update Slot POS_Y -----------
            For i As Integer = 0 To Slots.Count - 1
                Slots(i).POS_Y = value
            Next
        End Set
    End Property

    Public Property FLOOR_ID As Integer
        Get
            Return Floor.Attributes("FLOOR_ID")
        End Get
        Set(value As Integer)
            Floor.Attributes("FLOOR_ID") = value
        End Set
    End Property

    Public Property HighLight As UC_Product_Slot.HighLightMode
        Get
            Select Case True
                Case Floor.CssClass.IndexOf("highlightYellow") > -1
                    Return UC_Product_Slot.HighLightMode.YellowDotted
                Case Floor.CssClass.IndexOf("highlightGreen") > -1
                    Return UC_Product_Slot.HighLightMode.GreenSolid
                Case Floor.CssClass.IndexOf("highlightRed") > -1
                    Return UC_Product_Slot.HighLightMode.RedSolid
                Case Else
                    Return UC_Product_Slot.HighLightMode.None
            End Select
        End Get
        Set(value As UC_Product_Slot.HighLightMode)
            Floor.CssClass = RemoveTagCssClass(Floor.CssClass, "highlightYellow")
            Floor.CssClass = RemoveTagCssClass(Floor.CssClass, "highlightGreen")
            Floor.CssClass = RemoveTagCssClass(Floor.CssClass, "highlightRed")
            Select Case value
                Case UC_Product_Slot.HighLightMode.YellowDotted
                    Floor.CssClass &= " highlightYellow"
                Case UC_Product_Slot.HighLightMode.GreenSolid
                    Floor.CssClass &= " highlightGreen"
                Case UC_Product_Slot.HighLightMode.RedSolid
                    Floor.CssClass &= " highlightRed"
                Case UC_Product_Slot.HighLightMode.None
                    '-------- Donothing --------------
            End Select
        End Set
    End Property

    Public Property ShowFloorName As Boolean
        Get
            Return CaptionBlock.Visible
        End Get
        Set(value As Boolean)
            CaptionBlock.Visible = value
        End Set
    End Property

    Public Property ShowMenu As Boolean
        Get
            Return pnlMenu.Visible
        End Get
        Set(value As Boolean)
            pnlMenu.Visible = value
        End Set
    End Property

    Public Property ShowScale As Boolean
        Get
            Return pnlScale.Visible
        End Get
        Set(value As Boolean)
            pnlScale.Visible = value
        End Set
    End Property

    Public Function Slots() As List(Of UC_Product_Slot)
        Dim Result As New List(Of UC_Product_Slot)
        For Each Item In rptSlot.Items
            If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For
            Dim Slot As UC_Product_Slot = Item.FindControl("Slot")
            Result.Add(Slot)
        Next
        Return Result
    End Function

    Public Function AccessSlotFromName(ByVal SLOT_NAME As String) As UC_Product_Slot
        For i As Integer = 0 To Slots.Count - 1
            If Slots(i).SLOT_NAME = SLOT_NAME Then
                Return Slots(i)
            End If
        Next
        Return Nothing
    End Function

    Public Function SlotDatas() As DataTable

        Dim DT As New DataTable
        DT.Columns.Add("SLOT_ID", GetType(Integer))
        DT.Columns.Add("SLOT_NAME", GetType(String))
        DT.Columns.Add("POS_X", GetType(Integer))
        DT.Columns.Add("SLOT_WIDTH", GetType(Integer))
        DT.Columns.Add("PRODUCT_ID", GetType(Integer))
        DT.Columns.Add("PRODUCT_CODE", GetType(String))
        DT.Columns.Add("PRODUCT_QUANTITY", GetType(Integer))
        DT.Columns.Add("PRODUCT_LEVEL_PERCENT", GetType(String))
        DT.Columns.Add("PRODUCT_LEVEL_COLOR", GetType(Drawing.Color))
        DT.Columns.Add("QUANTITY_BAR_COLOR", GetType(Drawing.Color))
        DT.Columns.Add("HighLight", GetType(Integer))

        For Each Item In rptSlot.Items
            If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For
            Dim Slot As UC_Product_Slot = Item.FindControl("Slot")
            Dim DR As DataRow = DT.NewRow
            DR("SLOT_ID") = Slot.SLOT_ID
            DR("SLOT_NAME") = Slot.SLOT_NAME
            DR("POS_X") = Slot.POS_X
            DR("SLOT_WIDTH") = Slot.SLOT_WIDTH
            DR("PRODUCT_ID") = Slot.PRODUCT_ID
            DR("PRODUCT_CODE") = Slot.PRODUCT_CODE
            DR("PRODUCT_QUANTITY") = Slot.PRODUCT_QUANTITY
            DR("PRODUCT_LEVEL_PERCENT") = Slot.PRODUCT_LEVEL_PERCENT
            DR("PRODUCT_LEVEL_COLOR") = Slot.PRODUCT_LEVEL_COLOR
            DR("QUANTITY_BAR_COLOR") = Slot.QUANTITY_BAR_COLOR
            DR("HighLight") = Slot.HighLight
            DT.Rows.Add(DR)
        Next

        Return DT
    End Function

    Private Sub rptSlot_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptSlot.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim Slot As UC_Product_Slot = e.Item.FindControl("Slot")
        With Slot

            .SLOT_ID = e.Item.DataItem("SLOT_ID")
            .SLOT_NAME = e.Item.DataItem("SLOT_NAME")
            .POS_X = e.Item.DataItem("POS_X")
            .SLOT_WIDTH = e.Item.DataItem("SLOT_WIDTH")
            .PRODUCT_ID = e.Item.DataItem("PRODUCT_ID")
            .PRODUCT_CODE = e.Item.DataItem("PRODUCT_CODE")
            .PRODUCT_QUANTITY = e.Item.DataItem("PRODUCT_QUANTITY")
            .PRODUCT_LEVEL_PERCENT = e.Item.DataItem("PRODUCT_LEVEL_PERCENT")
            .PRODUCT_LEVEL_COLOR = e.Item.DataItem("PRODUCT_LEVEL_COLOR")
            .QUANTITY_BAR_COLOR = e.Item.DataItem("QUANTITY_BAR_COLOR")
            .HighLight = e.Item.DataItem("HighLight")
            '-------------------- Default Value ---------------
            .SLOT_HEIGHT = FLOOR_HEIGHT
            .POS_Y = POS_Y
        End With
    End Sub

    Public Sub DeselectAllSlot()
        For i As Integer = 0 To Slots.Count - 1
            Slots(i).HighLight = UC_Product_Slot.HighLightMode.None
        Next
    End Sub

    Public Sub AddSlot(ByVal SLOT_ID As Integer, ByVal SLOT_NAME As String, ByVal POS_X As Integer, ByVal SLOT_WIDTH As Integer,
                       ByVal PRODUCT_ID As Integer, ByVal PRODUCT_CODE As String, ByVal PRODUCT_QUANTITY As Integer, ByVal PRODUCT_LEVEL_PERCENT As String,
                       ByVal PRODUCT_LEVEL_COLOR As Drawing.Color, ByVal QUANTITY_BAR_COLOR As Drawing.Color, ByVal HighLight As UC_Product_Slot.HighLightMode)
        Dim DT As DataTable = SlotDatas()
        Dim DR As DataRow = DT.NewRow

        DR("SLOT_ID") = SLOT_ID
        DR("SLOT_NAME") = SLOT_NAME
        DR("POS_X") = POS_X
        DR("SLOT_WIDTH") = SLOT_WIDTH
        DR("PRODUCT_ID") = PRODUCT_ID
        DR("PRODUCT_CODE") = PRODUCT_CODE
        DR("PRODUCT_QUANTITY") = PRODUCT_QUANTITY
        DR("PRODUCT_LEVEL_PERCENT") = PRODUCT_LEVEL_PERCENT
        DR("PRODUCT_LEVEL_COLOR") = PRODUCT_LEVEL_COLOR
        DR("QUANTITY_BAR_COLOR") = QUANTITY_BAR_COLOR
        DR("HighLight") = HighLight

        DT.Rows.Add(DR)
        rptSlot.DataSource = DT
        rptSlot.DataBind()
    End Sub

    Public Sub AddSlot(ByVal SlotDataRow As DataRow)
        AddSlot(SlotDataRow("SLOT_ID"), SlotDataRow("SLOT_NAME"), SlotDataRow("POS_X"), SlotDataRow("SLOT_WIDTH"), SlotDataRow("PRODUCT_ID"),
                SlotDataRow("PRODUCT_CODE"), SlotDataRow("PRODUCT_QUANTITY"), SlotDataRow("PRODUCT_LEVEL_PERCENT"), SlotDataRow("PRODUCT_LEVEL_COLOR"), SlotDataRow("QUANTITY_BAR_COLOR"),
                SlotDataRow("HighLight"))
    End Sub

    Public Sub AddSlots(ByVal SlotsDataTable As DataTable)
        For i As Integer = 0 To SlotsDataTable.Rows.Count - 1
            AddSlot(SlotsDataTable.Rows(i))
        Next
    End Sub

    Public Sub RemoveSlot(ByVal SlotIndex As Integer)
        Dim DT As DataTable = SlotDatas()
        DT.Rows.RemoveAt(SlotIndex)
        rptSlot.DataSource = DT
        rptSlot.DataBind()
        For i As Integer = 0 To Slots.Count - 1
            Slots(i).SLOT_NAME = FLOOR_NAME & "-" & (i + 1)
        Next
    End Sub

    Public Sub ClearAllSlot()
        rptSlot.DataSource = Nothing
        rptSlot.DataBind()
    End Sub

    Public ReadOnly Property SelectedSlot As UC_Product_Slot
        Get
            For i As Integer = 0 To Slots.Count - 1
                If Slots(i).HighLight <> UC_Product_Slot.HighLightMode.None Then
                    Return Slots(i)
                End If
            Next
            Return Nothing
        End Get
    End Property

#Region "Event"

    Public Event RequestEdit(ByVal Sender As UC_Product_Floor)
    Public Event RequestAddFloor(ByVal Sender As UC_Product_Floor)
    Public Event RequestAddSlot(ByVal Sender As UC_Product_Floor)
    Public Event RequestClearSlot(ByVal Sender As UC_Product_Floor)
    Public Event RequestClearProduct(ByVal Sender As UC_Product_Floor)
    Public Event RequestRemove(ByVal Sender As UC_Product_Floor)
    Public Event SlotSelecting(ByVal Sender As UC_Product_Slot)

    Private Sub mnuFloorSetting_Click(sender As Object, e As EventArgs) Handles mnuFloorSetting.Click, CaptionBlock.Click
        RaiseEvent RequestEdit(Me)
    End Sub

    Private Sub mnuAddFloor_Click(sender As Object, e As EventArgs) Handles mnuAddFloor.Click
        RaiseEvent RequestAddFloor(Me)
    End Sub

    Private Sub mnuAddSlot_Click(sender As Object, e As EventArgs) Handles mnuAddSlot.Click
        RaiseEvent RequestAddSlot(Me)
    End Sub

    Private Sub mnuClearAllSlot_Click(sender As Object, e As EventArgs) Handles mnuClearAllSlot.Click
        RaiseEvent RequestClearSlot(Me)
    End Sub

    Private Sub mnuClearAllProduct_Click(sender As Object, e As EventArgs) Handles mnuClearAllProduct.Click
        RaiseEvent RequestClearProduct(Me)
    End Sub

    Private Sub mnuRemoveFloor_Click(sender As Object, e As EventArgs) Handles mnuRemoveFloor.Click
        RaiseEvent RequestRemove(Me)
    End Sub

    Protected Sub Slot_Selecting(ByRef Sender As UC_Product_Slot)
        RaiseEvent SlotSelecting(Sender)
    End Sub

#End Region

End Class