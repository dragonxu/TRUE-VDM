Public Class UC_SIM_Dispenser
    Inherits System.Web.UI.UserControl

    Public Property KO_ID As Integer
        Get
            Return lblProperty.Attributes("KO_ID")
        End Get
        Set(value As Integer)
            lblProperty.Attributes("KO_ID") = value
        End Set
    End Property

    Public Property SHOP_CODE As String
        Get
            Return lblProperty.Attributes("SHOP_CODE")
        End Get
        Set(value As String)
            lblProperty.Attributes("SHOP_CODE") = value
        End Set
    End Property


    Public Property SIM_Height As Integer
        Get
            Return lblProperty.Attributes("SIM_Height")
        End Get
        Set(value As Integer)
            lblProperty.Attributes("SIM_Height") = value
            '-------------Update Children ---------
            For i As Integer = 0 To Slots.Count - 1
                Slots(i).MAX_CAPACITY = Slots(i).MAX_CAPACITY
                Slots(i).UpdateSIMQuantity()
                For j As Integer = 0 To Slots(i).SIMS.Count - 1
                    Slots(i).SIMS(j).Update_Display()
                Next
            Next
        End Set
    End Property

    Public ReadOnly Property SLOT_CAPACITY As Integer
        Get
            Return lblProperty.Attributes("SLOT_CAPACITY")
        End Get
    End Property

    Public ReadOnly Property Slots As List(Of UC_SIM_Slot)
        Get
            Dim Result As New List(Of UC_SIM_Slot)
            For Each Item In rptSlot.Items
                If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For
                Dim Slot As UC_SIM_Slot = Item.FindControl("Slot")
                Result.Add(Slot)
            Next
            Return Result
        End Get
    End Property

    Public ReadOnly Property SIMS As List(Of UC_SIM)
        Get
            Dim Result As New List(Of UC_SIM)
            For i As Integer = 0 To Slots.Count - 1
                For j As Integer = 0 To Slots(j).SIMS.Count - 1
                    Result.Add(Slots(i).SIMS(j))
                Next
            Next
            Return Result
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Function SlotDatas() As DataTable
        Dim DT As New DataTable
        '---------------- Config ----------------
        DT.Columns.Add("SLOT_NAME", GetType(String))
        DT.Columns.Add("DEVICE_ID", GetType(Integer))
        DT.Columns.Add("MAX_CAPACITY", GetType(Integer))
        '---------------- Data-------------------
        DT.Columns.Add("SIM_ID", GetType(Integer))
        DT.Columns.Add("SIM_CODE", GetType(String))
        DT.Columns.Add("SIM_PRICE", GetType(String))
        DT.Columns.Add("ShowSIMProfile", GetType(Boolean))
        DT.Columns.Add("ShowPointer", GetType(Boolean))
        DT.Columns.Add("PointerColor", GetType(Drawing.Color))
        DT.Columns.Add("HighLight", GetType(UC_Product_Slot.HighLightMode))
        DT.Columns.Add("Column12Style", GetType(Integer))

        For Each Item In rptSlot.Items
            If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For

            Dim Slot As UC_SIM_Slot = Item.FindControl("Slot")
            Dim DR As DataRow = DT.NewRow
            DR("SLOT_NAME") = Slot.SLOT_NAME
            DR("DEVICE_ID") = Slot.DEVICE_ID
            DR("MAX_CAPACITY") = Slot.MAX_CAPACITY
            DR("SIM_ID") = Slot.SIM_ID
            DR("SIM_CODE") = Slot.SIM_CODE
            DR("SIM_PRICE") = Slot.SIM_PRICE
            DR("ShowSIMProfile") = Slot.ShowSIMProfile
            DR("ShowPointer") = Slot.ShowPointer
            DR("PointerColor") = Slot.PointerColor
            DR("HighLight") = Slot.HighLight
            DR("Column12Style") = Slot.Column12Style

            DT.Rows.Add(DR)
        Next
        Return DT
    End Function

    Public Sub ShowSIMProfile()
        For i As Integer = 0 To Slots.Count - 1
            Slots(i).ShowSIMProfile = True
        Next
    End Sub

    Public Sub HideSIMProfile()
        For i As Integer = 0 To Slots.Count - 1
            Slots(i).ShowSIMProfile = False
        Next
    End Sub

    Public Sub ShowPointer()
        For i As Integer = 0 To Slots.Count - 1
            Slots(i).ShowPointer = False
        Next
    End Sub

    Public Sub HidePointer()
        For i As Integer = 0 To Slots.Count - 1
            Slots(i).ShowPointer = False
        Next
    End Sub

    Public Sub Deselect_All_Slot()
        For i As Integer = 0 To Slots.Count - 1
            Slots(i).HighLight = UC_Product_Slot.HighLightMode.None
        Next
    End Sub

    Private Sub rpt_Slot_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptSlot.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim Slot As UC_SIM_Slot = e.Item.FindControl("Slot")

        Slot.SLOT_NAME = e.Item.DataItem("SLOT_NAME")
        Slot.DEVICE_ID = e.Item.DataItem("DEVICE_ID")
        Slot.MAX_CAPACITY = e.Item.DataItem("MAX_CAPACITY")
        Slot.SIM_ID = e.Item.DataItem("SIM_ID")
        Slot.SIM_CODE = e.Item.DataItem("SIM_CODE")
        Slot.SIM_PRICE = e.Item.DataItem("SIM_PRICE")
        Slot.ShowSIMProfile = e.Item.DataItem("ShowSIMProfile")
        Slot.ShowPointer = e.Item.DataItem("ShowPointer")
        Slot.PointerColor = e.Item.DataItem("PointerColor")
        Slot.HighLight = e.Item.DataItem("HighLight")
        Slot.Column12Style = e.Item.DataItem("Column12Style")

    End Sub

    Public Sub AddSlot(ByVal SLOT_NAME As String, ByVal DEVICE_ID As Integer, ByVal MAX_CAPACITY As Integer,
                       ByVal SIM_ID As Integer, ByVal SIM_CODE As String, ByVal SIM_PRICE As String,
                       ByVal ShowSIMProfile As Boolean, ByVal ShowPointer As Boolean, ByVal PointerColor As Drawing.Color,
                       ByVal HighLight As UC_Product_Slot.HighLightMode, ByVal Column12Style As Integer)

        Dim DT As DataTable = SlotDatas()
        Dim DR As DataRow = DT.NewRow
        DR("SLOT_NAME") = SLOT_NAME
        DR("DEVICE_ID") = DEVICE_ID
        DR("MAX_CAPACITY") = MAX_CAPACITY
        DR("SIM_ID") = SIM_ID
        DR("SIM_CODE") = SIM_CODE
        DR("SIM_PRICE") = SIM_PRICE
        DR("ShowSIMProfile") = ShowSIMProfile
        DR("ShowPointer") = ShowPointer
        DR("PointerColor") = PointerColor
        DR("HighLight") = HighLight
        DR("Column12Style") = Column12Style
        DT.Rows.Add(DR)

        rptSlot.DataSource = DT
        rptSlot.DataBind()
    End Sub

    Public Sub AddSlot(ByVal SlotData As DataRow)
        AddSlot(SlotData("SLOT_ID"), SlotData("DEVICE_ID"), SlotData("MAX_CAPACITY"), SlotData("SIM_ID"), SlotData("SIM_CODE"), SlotData("SIM_PRICE"),
                  SlotData("ShowSIMProfile"), SlotData("ShowPointer"), SlotData("PointerColor"), SlotData("HighLight"), SlotData("Column12Style"))
    End Sub

    Public Sub AddSlot(ByVal SlotDatas As DataTable)
        For i As Integer = 0 To SlotDatas.Rows.Count - 1
            AddSlot(SlotDatas.Rows(i))
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

    Public ReadOnly Property SelectedSlot As UC_SIM_Slot
        Get
            For i As Integer = 0 To Slots.Count - 1
                If Slots(i).HighLight <> UC_Product_Slot.HighLightMode.None Then
                    Return Slots(i)
                End If
            Next
            Return Nothing
        End Get
    End Property

    Public Function GET_DEVICE_ID_FROM_SLOT_NAME(ByVal SLOT_NAME As String) As Integer
        For i As Integer = 0 To Slots.Count - 1
            If Slots(i).SLOT_NAME = SLOT_NAME Then
                Return Slots(i).DEVICE_ID
            End If
        Next
        Return -1
    End Function

#Region "Event"
    Public Event Selecting(ByRef Sender As UC_SIM_Slot)
    Protected Sub Slot_Selecting(ByRef Sender As UC_SIM_Slot)
        RaiseEvent Selecting(Sender)
    End Sub
#End Region

End Class