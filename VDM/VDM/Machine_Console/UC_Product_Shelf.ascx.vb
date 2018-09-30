﻿Public Class UC_Product_Shelf
    Inherits System.Web.UI.UserControl

    Const MarginWidth As Integer = 26 ' Padding 10x2 + border 3x2
    Const MarginHeight As Integer = 26 ' Padding 10x2 + border 3x2

    Public Property PixelPerMM As Double
        Get
            Return Shelf.Attributes("PixelPerMM")
        End Get
        Set(value As Double)
            Shelf.Attributes("PixelPerMM") = value

            '----------------- ReDisplay------------
            SHELF_WIDTH = SHELF_WIDTH
            SHELF_HEIGHT = SHELF_HEIGHT
            Dim TMP As DataTable = FloorDatas()
            rptFloor.DataSource = TMP
            rptFloor.DataBind()
        End Set
    End Property

    Public Property SHELF_ID As Integer
        Get
            Return Shelf.Attributes("SHELF_ID")
        End Get
        Set(value As Integer)
            Shelf.Attributes("SHELF_ID") = value
        End Set
    End Property

    Public Property SHELF_WIDTH As Integer '--------------- mm Unit ---------------
        Get
            Return lblWidth.Text
        End Get
        Set(value As Integer)
            Shelf.Width = Unit.Pixel((value * PixelPerMM) + MarginWidth)
            lnkEdit.Width = Shelf.Width
            lnkAddFloor.Width = Shelf.Width
            lblWidth.Text = value
            '---------------- Update All Floors Size ---------
            For i As Integer = 0 To Floors.Count - 1
                '---------------- Default Layout --------------
                Floors(i).FLOOR_HEIGHT = Floors(i).FLOOR_HEIGHT
            Next
        End Set
    End Property

    Public Property SHELF_HEIGHT As Integer '--------------- mm Unit ---------------
        Get
            Return lblHeight.Text
        End Get
        Set(value As Integer)
            Shelf.Height = Unit.Pixel((value * PixelPerMM) + MarginHeight)
            lblHeight.Text = value
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

    Public Property ShowScale As Boolean
        Get
            Return pnlScale.Visible
        End Get
        Set(value As Boolean)
            pnlScale.Visible = value
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

    Public Property HighLight As UC_Product_Slot.HighLightMode
        Get
            Select Case True
                Case Shelf.CssClass.IndexOf("highlightYellow") > -1
                    Return UC_Product_Slot.HighLightMode.YellowDotted
                Case Shelf.CssClass.IndexOf("highlightGreen") > -1
                    Return UC_Product_Slot.HighLightMode.GreenSolid
                Case Shelf.CssClass.IndexOf("highlightRed") > -1
                    Return UC_Product_Slot.HighLightMode.RedSolid
                Case Else
                    Return UC_Product_Slot.HighLightMode.None
            End Select
        End Get
        Set(value As UC_Product_Slot.HighLightMode)
            Shelf.CssClass = RemoveTagCssClass(Shelf.CssClass, "highlightYellow")
            Shelf.CssClass = RemoveTagCssClass(Shelf.CssClass, "highlightGreen")
            Shelf.CssClass = RemoveTagCssClass(Shelf.CssClass, "highlightRed")
            Select Case value
                Case UC_Product_Slot.HighLightMode.YellowDotted
                    Shelf.CssClass &= " highlightYellow"
                Case UC_Product_Slot.HighLightMode.GreenSolid
                    Shelf.CssClass &= " highlightGreen"
                Case UC_Product_Slot.HighLightMode.RedSolid
                    Shelf.CssClass &= " highlightRed"
                Case UC_Product_Slot.HighLightMode.None
                    '-------- Donothing --------------
            End Select
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '------- Reset Shelf Size ----------
            SHELF_WIDTH = SHELF_WIDTH
            SHELF_HEIGHT = SHELF_HEIGHT
        End If
    End Sub

    Public Sub setDefautShelfFeature()
        '-------------- Configure ------------
        ShowAddFloor = True
        ShowEditShelf = True
        ShowFloorName()
        ShowFloorMenu()
        '------------ Hide All Scale----------
        For i As Integer = 0 To Slots.Count - 1
            With Slots(i)
                .ShowScale = True
                .ShowProductCode = True
            End With
        Next
        For i As Integer = 0 To Floors.Count - 1
            Floors(i).ShowScale = True
        Next
        ShowScale = True
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

    Public Function AccessFloorFromName(ByVal FLOOR_NAME As String) As UC_Product_Floor
        For i As Integer = 0 To Floors.Count - 1
            If Floors(i).FLOOR_NAME = FLOOR_NAME Then
                Return Floors(i)
            End If
        Next
        Return Nothing
    End Function

    Public Function AccessSlotFromName(ByVal SLOT_NAME As String) As UC_Product_Slot
        For i As Integer = 0 To Slots.Count - 1
            If Slots(i).SLOT_NAME = SLOT_NAME Then
                Return Slots(i)
            End If
        Next
        Return Nothing
    End Function

    Private Function FloorDatas() As DataTable
        Dim DT As New DataTable

        DT.Columns.Add("FLOOR_ID", GetType(Integer))
        DT.Columns.Add("FLOOR_HEIGHT", GetType(Integer))
        DT.Columns.Add("POS_Y", GetType(Integer))
        DT.Columns.Add("HighLight", GetType(Integer))
        DT.Columns.Add("ShowFloorName", GetType(Boolean))
        DT.Columns.Add("ShowMenu", GetType(Boolean))
        DT.Columns.Add("SlotDatas", GetType(DataTable))

        For Each Item In rptFloor.Items
            If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For

            Dim Floor As UC_Product_Floor = Item.FindControl("Floor")
            Dim DR As DataRow = DT.NewRow
            DR("FLOOR_ID") = Floor.FLOOR_ID
            DR("FLOOR_HEIGHT") = Floor.FLOOR_HEIGHT
            DR("POS_Y") = Floor.POS_Y
            DR("HighLight") = Floor.HighLight
            DR("ShowFloorName") = Floor.ShowFloorName
            DR("ShowMenu") = Floor.ShowMenu
            DR("SlotDatas") = Floor.SlotDatas
            DT.Rows.Add(DR)
        Next

        Return DT
    End Function

    Public Sub ShowFloorName()
        For i As Integer = 0 To Floors.Count - 1
            Floors(i).ShowFloorName = True
        Next
    End Sub


    Public Sub HideFloorName()
        For i As Integer = 0 To Floors.Count - 1
            Floors(i).ShowFloorName = False
        Next
    End Sub

    Public Sub ShowFloorMenu()
        For i As Integer = 0 To Floors.Count - 1
            Floors(i).ShowMenu = True
        Next
    End Sub

    Public Sub HideFloorMenu()
        For i As Integer = 0 To Floors.Count - 1
            Floors(i).ShowMenu = False
        Next
    End Sub

    Private Sub rptFloor_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptFloor.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim Floor As UC_Product_Floor = e.Item.FindControl("Floor")
        With Floor
            .FLOOR_ID = e.Item.DataItem("FLOOR_ID")
            .FLOOR_HEIGHT = e.Item.DataItem("FLOOR_HEIGHT")
            .POS_Y = e.Item.DataItem("POS_Y")
            .HighLight = e.Item.DataItem("HighLight")
            .ShowFloorName = e.Item.DataItem("ShowFloorName")
            .ShowMenu = e.Item.DataItem("ShowMenu")
            If Not IsDBNull(e.Item.DataItem("SlotDatas")) And Not IsNothing(e.Item.DataItem("SlotDatas")) Then
                Dim SlotDatas As DataTable = e.Item.DataItem("SlotDatas")
                .AddSlots(SlotDatas)
            End If
            '---------------- Update Relate Slot Name---------
            .FLOOR_NAME = Chr(Asc("A") + e.Item.ItemIndex)
        End With
    End Sub

    Public Function Slots() As List(Of UC_Product_Slot)
        Dim Result As New List(Of UC_Product_Slot)
        For Each Item In rptFloor.Items
            If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For
            Dim Floor As UC_Product_Floor = Item.FindControl("Floor")
            For i As Integer = 0 To Floor.Slots.Count - 1
                Result.Add(Floor.Slots(i))
            Next
        Next
        Return Result
    End Function

    Public Sub ResetDimension()
        SHELF_WIDTH = 1900
        SHELF_HEIGHT = 1900
        SHELF_DEPTH = 1200
    End Sub

    Public Sub Deselect_All_Slot()
        For i As Integer = 0 To Floors.Count - 1
            For j As Integer = 0 To Floors(i).Slots.Count - 1
                Floors(i).Slots(j).HighLight = UC_Product_Slot.HighLightMode.None
            Next
        Next
    End Sub

    Public Sub Deselect_All_Floor()
        For i As Integer = 0 To Floors.Count - 1
            Floors(i).HighLight = UC_Product_Slot.HighLightMode.None
        Next
    End Sub

    Public Sub Deselect_All()
        HighLight = UC_Product_Slot.HighLightMode.None
        Deselect_All_Floor()
        Deselect_All_Slot()
    End Sub

    Public Sub AddFloor(ByVal FLOOR_ID As Integer, ByVal FLOOR_HEIGHT As Integer, ByVal POS_Y As Integer, ByVal HighLight As UC_Product_Slot.HighLightMode, ByVal ShowFloorName As Boolean, ByVal SlotDatas As DataTable, ByVal ShowMenu As Boolean, ByVal AddToIndex As Integer)
        Dim DT As DataTable = FloorDatas()

        Dim DR As DataRow = DT.NewRow
        DR("FLOOR_ID") = FLOOR_ID
        DR("FLOOR_HEIGHT") = FLOOR_HEIGHT
        DR("POS_Y") = POS_Y
        DR("HighLight") = HighLight
        DR("ShowFloorName") = ShowFloorName
        DR("ShowMenu") = ShowMenu
        DR("SlotDatas") = SlotDatas

        DT.Rows.InsertAt(DR, AddToIndex)
        rptFloor.DataSource = DT
        rptFloor.DataBind()
    End Sub

    Public Sub AddFloor(ByVal FloorData As DataRow)
        AddFloor(FloorData("FLOOR_ID"), FloorData("FLOOR_HEIGHT"), FloorData("POS_Y"),
                  FloorData("HighLight"), FloorData("ShowFloorName"), FloorData("ShowMenu"), FloorData("SlotDatas"), 0)
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

    Public ReadOnly Property SelectedFloor As UC_Product_Floor
        Get
            For i As Integer = 0 To Floors.Count - 1
                If Floors(i).HighLight <> UC_Product_Slot.HighLightMode.None Then
                    Return Floors(i)
                End If
            Next
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property SelectedSlot As UC_Product_Slot
        Get
            For i As Integer = 0 To Floors.Count - 1
                If Not IsNothing(Floors(i).SelectedSlot) Then
                    Return Floors(i).SelectedSlot
                End If
            Next
            Return Nothing
        End Get
    End Property

#Region "Event"

    Public Event RequestEdit(ByRef Sender As UC_Product_Shelf)
    Public Event RequestAddFloor(ByRef Sender As UC_Product_Shelf)
    Public Event RequestAddFloorAfter(ByVal Index As Integer)
    Public Event RequestEditFloor(ByRef Sender As UC_Product_Floor)
    'Public Event RequestRemoveFloorAt(ByVal Index As Integer)
    'Public Event RequestClearFloorSlot(ByVal Sender As UC_Product_Floor)
    'Public Event RequestClearFloorProduct(ByVal Sender As UC_Product_Floor)
    Public Event RequestAddSlot(ByRef Sender As UC_Product_Floor)
    Public Event SlotSelecting(ByRef Sender As UC_Product_Slot)

    Private Sub lnkEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkEdit.Click
        RaiseEvent RequestEdit(Me)
    End Sub

    Private Sub lnkAddFloor_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkAddFloor.Click
        RaiseEvent RequestAddFloor(Me)
    End Sub

    Protected Sub Floor_RequestAddFloor(ByVal Sender As UC_Product_Floor)
        RaiseEvent RequestAddFloorAfter(Floors.IndexOf(Sender))
    End Sub

    Protected Sub Floor_RequestEdit(ByVal Sender As UC_Product_Floor)
        RaiseEvent RequestEditFloor(Sender)
    End Sub

    Protected Sub Floor_RequestRemove(ByVal Sender As UC_Product_Floor)
        'RaiseEvent RequestRemoveFloorAt(Floors.IndexOf(Sender))
        Dim DT As DataTable = FloorDatas()
        DT.Rows.RemoveAt(Floors.IndexOf(Sender))
        rptFloor.DataSource = DT
        rptFloor.DataBind()
    End Sub

    Protected Sub Floor_RequestClearSlot(ByVal Sender As UC_Product_Floor)
        'RaiseEvent RequestClearFloorSlot(Sender)
        Dim DT As DataTable = FloorDatas()
        Dim DR As DataRow = DT.Rows(Floors.IndexOf(Sender))
        DR("SlotDatas") = DBNull.Value
        rptFloor.DataSource = DT
        rptFloor.DataBind()
    End Sub

    Protected Sub Floor_RequestClearProduct(ByVal Sender As UC_Product_Floor)
        'RaiseEvent RequestClearFloorProduct(Sender)
        Dim DT As DataTable = FloorDatas()

        For floor As Integer = 0 To DT.Rows.Count - 1
            If IsDBNull(DT.Rows(floor).Item("SlotDatas")) Then Continue For
            Dim SlotDatas As DataTable = DT.Rows(floor).Item("SlotDatas")

            For slot As Integer = 0 To SlotDatas.Rows.Count - 1
                SlotDatas.Rows(slot).Item("PRODUCT_ID") = 0
                SlotDatas.Rows(slot).Item("PRODUCT_CODE") = ""
                SlotDatas.Rows(slot).Item("PRODUCT_QUANTITY") = 0
                SlotDatas.Rows(slot).Item("PRODUCT_LEVEL_PERCENT") = ""
                SlotDatas.Rows(slot).Item("PRODUCT_LEVEL_COLOR") = Drawing.Color.White
                SlotDatas.Rows(slot).Item("QUANTITY_BAR_COLOR") = Drawing.Color.Green
            Next
        Next

        rptFloor.DataSource = DT
        rptFloor.DataBind()
    End Sub

    Protected Sub Floor_RequestAddSlot(ByVal Sender As UC_Product_Floor)
        RaiseEvent RequestAddSlot(Sender)
    End Sub

    Protected Sub Slot_Selecting(ByVal Sender As UC_Product_Slot)
        RaiseEvent SlotSelecting(Sender)
    End Sub

#End Region
End Class