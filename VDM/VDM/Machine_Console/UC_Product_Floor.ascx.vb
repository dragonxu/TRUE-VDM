Public Class UC_Product_Floor
    Inherits System.Web.UI.UserControl

    Dim BL As New VDM_BL

    Public Property PixelPerMM As Double
        Get
            Return Floor.Attributes("PixelPerMM")
        End Get
        Set(value As Double)
            Floor.Attributes("PixelPerMM") = value
        End Set
    End Property

    Public ReadOnly Property ParentShelf As UC_Product_Shelf
        Get
            Try
                Return Me.Parent.Parent.Parent
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
        End Set
    End Property

    '--------------- Calculate MM------------
    Public Property FLOOR_HEIGHT As Integer
        Get
            'Return Floor.Height.Value / PixelPerMM
            Return Floor.Attributes("FLOOR_HEIGHT")
        End Get
        Set(value As Integer)
            Floor.Height = Unit.Pixel(value * PixelPerMM)
            Floor.Attributes("FLOOR_HEIGHT") = value
        End Set
    End Property

    Public Property POS_Y As Integer
        Get
            Return Floor.Attributes("POS_Y")
        End Get
        Set(value As Integer)
            Floor.Attributes("POS_Y") = value
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

    Public Property IsSelected As Boolean
        Get
            Return Floor.CssClass = "machine_floor selected"
        End Get
        Set(value As Boolean)
            If value Then
                Floor.CssClass = "machine_floor selected"
            Else
                Floor.CssClass = "machine_floor"
            End If
        End Set
    End Property

    Public Property IsViewOnly As Boolean
        Get
            Return Not pnlMenu.Visible
        End Get
        Set(value As Boolean)
            pnlMenu.Visible = Not value
        End Set
    End Property

    Public Property ShowFloorLabel As Boolean
        Get
            Return CaptionBlock.Visible
        End Get
        Set(value As Boolean)
            CaptionBlock.Visible = value
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

    Public Function Slots(ByVal Index As Integer) As UC_Product_Slot
        Return rptSlot.Items(Index).FindControl("Slot")
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
        DT.Columns.Add("IsSelected", GetType(Boolean))
        DT.Columns.Add("IsViewOnly", GetType(Boolean))
        DT.Columns.Add("PixelPerMM", GetType(Double))

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
            DR("IsSelected") = Slot.IsSelected
            DR("IsViewOnly") = Slot.IsViewOnly
            DR("PixelPerMM") = Slot.PixelPerMM
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
            .IsSelected = e.Item.DataItem("IsSelected")
            .IsViewOnly = e.Item.DataItem("IsViewOnly")
            .PixelPerMM = e.Item.DataItem("PixelPerMM")
        End With
    End Sub

    Public Sub DeselectAllSlot()
        For i As Integer = 0 To Slots.Count - 1
            Slots(i).IsSelected = False
        Next
    End Sub

    Public Sub AddSlot(ByVal SLOT_ID As Integer, ByVal SLOT_NAME As String, ByVal POS_X As Integer, ByVal SLOT_WIDTH As Integer,
                       ByVal PRODUCT_ID As Integer, ByVal PRODUCT_CODE As String, ByVal PRODUCT_QUANTITY As Integer, ByVal PRODUCT_LEVEL_PERCENT As String,
                       ByVal PRODUCT_LEVEL_COLOR As Drawing.Color, ByVal QUANTITY_BAR_COLOR As Drawing.Color, ByVal IsSelected As Boolean, ByVal IsViewOnly As Boolean, ByVal PixelPerMM As Double)
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
        DR("IsSelected") = IsSelected
        DR("IsViewOnly") = IsViewOnly
        DR("PixelPerMM") = PixelPerMM

        DT.Rows.Add(DR)
        rptSlot.DataSource = DT
        rptSlot.DataBind()
    End Sub

    Public Sub AddSlot(ByVal SlotDataRow As DataRow)
        AddSlot(SlotDataRow("SLOT_ID"), SlotDataRow("SLOT_NAME"), SlotDataRow("POS_X"), SlotDataRow("SLOT_WIDTH"), SlotDataRow("PRODUCT_ID"),
                SlotDataRow("PRODUCT_CODE"), SlotDataRow("PRODUCT_QUANTITY"), SlotDataRow("PRODUCT_LEVEL_PERCENT"), SlotDataRow("PRODUCT_LEVEL_COLOR"), SlotDataRow("QUANTITY_BAR_COLOR"),
                SlotDataRow("IsSelected"), SlotDataRow("IsViewOnly"), SlotDataRow("PixelPerMM"))
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
    End Sub

    Public Sub ClearAllSlot()
        rptSlot.DataSource = Nothing
        rptSlot.DataBind()
    End Sub



#Region "Event"
    Public Event RequestEdit(ByVal Sender As UC_Product_Floor)
    Public Event RequestAddFloor(ByVal Sender As UC_Product_Floor)
    Public Event RequestAddSlot(ByVal Sender As UC_Product_Floor)
    Public Event RequestClearSlot(ByVal Sender As UC_Product_Floor)
    Public Event RequestClearProduct(ByVal Sender As UC_Product_Floor)
    Public Event RequestRemove(ByVal Sender As UC_Product_Floor)

    Private Sub mnuFloorSetting_Click(sender As Object, e As EventArgs) Handles mnuFloorSetting.Click
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

#End Region

End Class