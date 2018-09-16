Public Class UC_Product_Shelf
    Inherits System.Web.UI.UserControl

    Public Property PixelPerMM As Double
        Get
            Return Shelf.Attributes("PixelPerMM")
        End Get
        Set(value As Double)
            Shelf.Attributes("PixelPerMM") = value
        End Set
    End Property

    Public Property SHELF_WIDTH As Integer '--------------- mm Unit ---------------
        Get
            Return Shelf.Attributes("SHELF_WIDTH")
        End Get
        Set(value As Integer)
            Shelf.Width = Unit.Pixel(value * PixelPerMM)
            lnkEdit.Width = Shelf.Width
            lnkAddFloor.Width = Shelf.Width
            Shelf.Attributes("SHELF_WIDTH") = value
        End Set
    End Property

    Public Property SHELF_HEIGHT As Integer '--------------- mm Unit ---------------
        Get
            Return Shelf.Attributes("SHELF_HEIGHT")
        End Get
        Set(value As Integer)
            Shelf.Height = Unit.Pixel(value * PixelPerMM)
            Shelf.Attributes("SHELF_HEIGHT") = value
        End Set
    End Property

    Public Property SHELF_DEPTH As Integer '--------------- mm Unit ---------------
        Get
            Return Shelf.Attributes("SHELF_DEPTH")
        End Get
        Set(value As Integer)
            Shelf.Attributes("SHELF_DEPTH") = value
        End Set
    End Property

    Public Property ShowAddFloor As Boolean
        Get
            Return lnkAddFloor.Visible
        End Get
        Set(value As Boolean)
            lnkAddFloor.Visible = value
        End Set
    End Property

    Public Property ShowEditShelf As Boolean
        Get
            Return lnkEdit.Visible
        End Get
        Set(value As Boolean)
            lnkEdit.Visible = value
        End Set
    End Property

    Public Property IsSelected As Boolean
        Get
            Return Shelf.CssClass = "machine_stock btn-shadow selected"
        End Get
        Set(value As Boolean)
            If value Then
                Shelf.CssClass = "machine_stock btn-shadow selected"
            Else
                Shelf.CssClass = "machine_stock btn-shadow"
            End If
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function Floors() As List(Of UC_Product_Floor)
        Dim Result As New List(Of UC_Product_Floor)
        For Each Item In rptFloor.Items
            If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For
            Dim Floor As UC_Product_Floor = Item.FindControl("Floor")
            Result.Add(Floor)
        Next
        Return Result
    End Function

    Public Function Floors(ByVal Index As Integer) As UC_Product_Floor
        Return rptFloor.Items(Index).FindControl("Floor")
    End Function

    Private Function FloorDatas() As DataTable
        Dim DT As New DataTable

        DT.Columns.Add("FLOOR_ID", GetType(Integer))
        DT.Columns.Add("FLOOR_HEIGHT", GetType(Integer))
        DT.Columns.Add("POS_Y", GetType(Integer))
        DT.Columns.Add("IsSelected", GetType(Boolean))
        DT.Columns.Add("IsViewOnly", GetType(Boolean))
        DT.Columns.Add("ShowFloorLabel", GetType(Boolean))
        DT.Columns.Add("PixelPerMM", GetType(Double))
        DT.Columns.Add("SlotDatas", GetType(DataTable))

        For Each Item In rptFloor.Items
            If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For

            Dim Floor As UC_Product_Floor = Item.FindControl("Floor")
            Dim DR As DataRow = DT.NewRow
            DR("FLOOR_ID") = Floor.FLOOR_ID
            DR("FLOOR_HEIGHT") = Floor.FLOOR_HEIGHT
            DR("POS_Y") = Floor.POS_Y
            DR("IsSelected") = Floor.IsSelected
            DR("IsViewOnly") = Floor.IsViewOnly
            DR("ShowFloorLabel") = Floor.ShowFloorLabel
            DR("PixelPerMM") = Floor.PixelPerMM
            DR("SlotDatas") = Floor.SlotDatas
            DT.Rows.Add(DR)
        Next

        Return DT
    End Function

    Private Sub rptFloor_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptFloor.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim Floor As UC_Product_Floor = e.Item.FindControl("Floor")
        With Floor
            .FLOOR_ID = e.Item.DataItem("FLOOR_ID")
            .FLOOR_NAME = Chr(Asc("A") + e.Item.ItemIndex)
            .FLOOR_HEIGHT = e.Item.DataItem("FLOOR_HEIGHT")
            .POS_Y = e.Item.DataItem("POS_Y")
            .IsSelected = e.Item.DataItem("IsSelected")
            .IsViewOnly = e.Item.DataItem("IsViewOnly")
            .ShowFloorLabel = e.Item.DataItem("ShowFloorLabel")
            .PixelPerMM = e.Item.DataItem("PixelPerMM")
            If Not IsDBNull(e.Item.DataItem("SlotDatas")) And Not IsNothing(e.Item.DataItem("SlotDatas")) Then
                Dim SlotDatas As DataTable = e.Item.DataItem("SlotDatas")
                .AddSlots(SlotDatas)
            End If
        End With
    End Sub

    Public Sub Deselect_All_Slot()
        For i As Integer = 0 To Floors.Count - 1
            For j As Integer = 0 To Floors(i).Slots.Count - 1
                Floors(i).Slots(j).IsSelected = False
            Next
        Next
    End Sub

    Public Sub Deselect_All_Floor()
        For i As Integer = 0 To Floors.Count - 1
            Floors(i).IsSelected = False
        Next
    End Sub

    Public Sub Deselect_All()
        IsSelected = False
        Deselect_All_Floor()
        Deselect_All_Slot()
    End Sub

    Public Sub AddFloor(ByVal FLOOR_ID As Integer, ByVal FLOOR_HEIGHT As Integer, ByVal POS_Y As Integer, ByVal IsSelected As Boolean, ByVal IsViewOnly As Boolean, ByVal ShowFloorLabel As Boolean, ByVal PixelPerMM As Double, ByVal SlotDatas As DataTable)
        Dim DT As DataTable = FloorDatas()

        Dim DR As DataRow = DT.NewRow
        DR("FLOOR_ID") = FLOOR_ID
        DR("FLOOR_HEIGHT") = FLOOR_HEIGHT
        DR("POS_Y") = POS_Y
        DR("IsSelected") = IsSelected
        DR("IsViewOnly") = IsViewOnly
        DR("ShowFloorLabel") = ShowFloorLabel
        DR("PixelPerMM") = PixelPerMM
        DR("SlotDatas") = SlotDatas
        DT.Rows.Add(DR)
        rptFloor.DataSource = DT
        rptFloor.DataBind()
    End Sub

    Public Sub AddFloor(ByVal FloorData As DataRow)
        AddFloor(FloorData("FLOOR_ID"), FloorData("FLOOR_HEIGHT"), FloorData("POS_Y"),
                  FloorData("IsSelected"), FloorData("IsViewOnly"), FloorData("ShowFloorLabel"), FloorData("PixelPerMM"), FloorData("SlotDatas"))
    End Sub

    Public Sub AddFloors(ByVal FloorsDataTable As DataTable)
        For i As Integer = 0 To FloorsDataTable.Rows.Count - 1
            AddFloor(FloorsDataTable.Rows(i))
        Next
    End Sub

    Public Sub RemoveFloor(ByVal FloorIndex As Integer)
        Dim DT As DataTable = FloorDatas()
        DT.Rows.RemoveAt(FloorIndex)
        rptFloor.DataSource = DT
        rptFloor.DataBind()
    End Sub

    Public Sub ClearAllFloor()
        rptFloor.DataSource = Nothing
        rptFloor.DataBind()
    End Sub



#Region "Event"

    Public Event RequestEdit(ByVal Sender As UC_Product_Shelf)
    Public Event RequestAddFloor(ByVal Sender As UC_Product_Shelf)

    Private Sub lnkEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkEdit.Click
        RaiseEvent RequestEdit(Me)
    End Sub

    Private Sub lnkAddFloor_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkAddFloor.Click
        RaiseEvent RequestAddFloor(Me)
    End Sub

#End Region
End Class