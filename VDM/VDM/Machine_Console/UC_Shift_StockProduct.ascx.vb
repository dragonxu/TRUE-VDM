Imports VDM

Public Class UC_Shift_StockProduct
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then
            ResetPage()
            initFirstTimeJavascript()
        Else
            initFormPlugin()
        End If

        'Dim Slot As UC_Product_Slot = Shelf.Floors(2).Slots(1)
        'Slot.PRODUCT_ID = 1
        'Slot.PRODUCT_QUANTITY = 10
        'Slot.PRODUCT_LEVEL_PERCENT = "40%"
        'Slot.QUANTITY_BAR_COLOR = Drawing.Color.FromName("#eeeeee")
        'Slot.PRODUCT_CODE = 310023
        'Slot.PRODUCT_LEVEL_COLOR = Drawing.Color.Green


    End Sub


    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Private Sub initFirstTimeJavascript()
        ImplementJavaOnlyNumberText(txtShelfWidth, "center")
        ImplementJavaOnlyNumberText(txtShelfHeight, "center")
        ImplementJavaOnlyNumberText(txtShelfDepth, "center")

        ImplementJavaOnlyNumberText(txtFloorWidth, "center")
        ImplementJavaOnlyNumberText(txtFloorHeight, "center")
        ImplementJavaOnlyNumberText(txtFloorDepth, "center")
        ImplementJavaOnlyNumberText(txtFloorY, "center")

        ImplementJavaOnlyNumberText(txtSlotWidth, "center")
        ImplementJavaOnlyNumberText(txtSlotHeight, "center")
        ImplementJavaOnlyNumberText(txtSlotDepth, "center")
        ImplementJavaOnlyNumberText(txtSlotX, "center")
        ImplementJavaOnlyNumberText(txtSlotY, "center")
    End Sub

    Private Sub ResetPage()
        ClearShelfProperty()
        ClearFloorProperty()
        ClearSlotProperty()
    End Sub

    Private Sub ClearShelfProperty()
        Shelf.Deselect_All()
        txtShelfWidth.Text = ""
        txtShelfHeight.Text = ""
        txtShelfDepth.Text = ""

        btnClearShelf.Visible = True

        pnlShelf.Visible = False
        pnlFloor.Visible = False
        pnlSlot.Visible = False
    End Sub

    Private Sub ClearFloorProperty()
        Shelf.Deselect_All()

        lblFloorName.Text = ""
        txtFloorWidth.Text = ""
        txtFloorHeight.Text = ""
        txtFloorDepth.Text = ""
        txtFloorY.Text = ""

        btnRemoveFloor.Visible = True

        pnlShelf.Visible = False
        pnlFloor.Visible = False
        pnlSlot.Visible = False
    End Sub

    Private Sub ClearSlotProperty()
        Shelf.Deselect_All()

        lblSlotName.Text = ""
        txtSlotWidth.Text = ""
        txtSlotHeight.Text = ""
        txtSlotDepth.Text = ""
        txtSlotX.Text = ""
        txtSlotY.Text = ""
        '----------------- Containing Product --------------

        btnRemoveSlot.Visible = True

        pnlShelf.Visible = False
        pnlFloor.Visible = False
        pnlSlot.Visible = False
    End Sub



#Region "Event"

#Region "Shelf"

    Private Sub Shelf_RequestEdit(ByRef Sender As UC_Product_Shelf) Handles Shelf.RequestEdit

        ClearShelfProperty()

        txtShelfWidth.Text = Shelf.SHELF_WIDTH
        txtShelfHeight.Text = Shelf.SHELF_HEIGHT
        txtShelfDepth.Text = Shelf.SHELF_DEPTH
        Shelf.HighLight = UC_Product_Slot.HighLightMode.YellowDotted

        btnClearShelf.Visible = Shelf.Floors.Count > 0
        pnlShelf.Visible = True
    End Sub

    Private Sub btnCloseShelf_Click(sender As Object, e As EventArgs) Handles btnCloseShelf.Click
        ClearShelfProperty()
    End Sub

    Private Sub btnApplyShelf_Click(sender As Object, e As EventArgs) Handles btnApplyShelf.Click
        '--------------- Validate -----------------
        If txtShelfWidth.Text = "" Or txtShelfWidth.Text = "0" Or
            txtShelfHeight.Text = "" Or txtShelfHeight.Text = "0" Or
            txtShelfDepth.Text = "" Or txtShelfDepth.Text = "0" Then
            Alert(Me.Page, "กรอกขนาดให้ครบ")
            Exit Sub
        End If
        '--------------- Apply --------------------
        Shelf.SHELF_WIDTH = txtShelfWidth.Text
        Shelf.SHELF_HEIGHT = txtShelfHeight.Text
        Shelf.SHELF_DEPTH = txtShelfDepth.Text

        'ClearShelfProperty()
    End Sub

    Private Sub btnClearShelf_Click(sender As Object, e As EventArgs) Handles btnClearShelf.Click
        Shelf.ClearAllFloor()
        ClearShelfProperty()
    End Sub

#End Region

#Region "Floor"

    Private Sub Shelf_RequestAddFloor(ByRef Sender As UC_Product_Shelf) Handles Shelf.RequestAddFloor
        ClearFloorProperty()

        '----------- Inherited Property --------
        txtFloorWidth.Text = Shelf.SHELF_WIDTH
        txtFloorDepth.Text = Shelf.SHELF_DEPTH
        '-------- Set Floor Name -----------
        lblFloorName.Text = Chr(Asc("A") + Shelf.Floors.Count) & " (new) "

        btnRemoveFloor.Visible = False
        Shelf.HighLight = UC_Product_Slot.HighLightMode.YellowDotted
        pnlFloor.Visible = True
    End Sub

    Private Sub Shelf_RequestAddFloorAfter(ByVal Index As Integer) Handles Shelf.RequestAddFloorAfter
        ClearFloorProperty()
        '----------- Inherited Property --------
        txtFloorWidth.Text = Shelf.SHELF_WIDTH
        txtFloorDepth.Text = Shelf.SHELF_DEPTH
        '-------- Set Floor Name -----------
        lblFloorName.Text = Chr(Asc("A") + Index + 1) & " (new)"

        btnRemoveFloor.Visible = False
        Shelf.HighLight = UC_Product_Slot.HighLightMode.YellowDotted
        Shelf.Floors(Index).HighLight = UC_Product_Slot.HighLightMode.YellowDotted
        pnlFloor.Visible = True
    End Sub

    Private Sub Shelf_RequestEditFloor(ByRef Sender As UC_Product_Floor) Handles Shelf.RequestEditFloor
        ClearFloorProperty()
        '----------- Inherited Property --------
        txtFloorWidth.Text = Shelf.SHELF_WIDTH
        txtFloorDepth.Text = Shelf.SHELF_DEPTH

        txtFloorHeight.Text = Sender.FLOOR_HEIGHT
        txtFloorY.Text = Sender.POS_Y
        '-------- Set Floor Name -----------
        lblFloorName.Text = Sender.FLOOR_NAME

        Sender.HighLight = UC_Product_Slot.HighLightMode.YellowDotted
        pnlFloor.Visible = True
    End Sub

    Private Sub btnRemoveFloor_Click(sender As Object, e As EventArgs) Handles btnRemoveFloor.Click
        Shelf.RemoveFloor(Shelf.Floors.IndexOf(Shelf.SelectedFloor))
        ClearFloorProperty()
    End Sub

    Private Sub btnApplyFloor_Click(sender As Object, e As EventArgs) Handles btnApplyFloor.Click
        '--------------- Validate -----------------
        If txtFloorHeight.Text = "" Or txtFloorHeight.Text = "0" Or
           txtFloorY.Text = "" Then
            Alert(Me.Page, "กรอกขนาดให้ครบ")
            Exit Sub
        End If
        '--------------- Apply --------------------
        Select Case True
            Case Shelf.HighLight <> UC_Product_Slot.HighLightMode.None And Not IsNothing(Shelf.SelectedFloor) '---------- Add Floor After ----------
                Shelf.AddFloor(0, txtFloorHeight.Text, txtFloorY.Text, False, True, Nothing, True, Shelf.Floors.IndexOf(Shelf.SelectedFloor) + 1)
                ClearFloorProperty()
            Case Shelf.HighLight <> UC_Product_Slot.HighLightMode.None  '---------- Add Floor --------------
                Shelf.AddFloor(0, txtFloorHeight.Text, txtFloorY.Text, False, True, Nothing, True, Shelf.Floors.Count)
                ClearFloorProperty()
            Case Not IsNothing(Shelf.SelectedFloor) '---------- Edit Floor ----------
                Shelf.SelectedFloor.FLOOR_HEIGHT = txtFloorHeight.Text
                Shelf.SelectedFloor.POS_Y = txtFloorY.Text
        End Select
        '
    End Sub

    Private Sub btnCloseFloor_Click(sender As Object, e As EventArgs) Handles btnCloseFloor.Click
        ClearFloorProperty()
    End Sub

#End Region
#Region "Slot"

    Private Sub Shelf_RequestAddSlot(ByRef Sender As UC_Product_Floor) Handles Shelf.RequestAddSlot
        ClearSlotProperty()
        '----------- Inherited Property --------
        txtSlotDepth.Text = Sender.ParentShelf.SHELF_DEPTH
        txtSlotHeight.Text = Sender.FLOOR_HEIGHT
        txtSlotY.Text = Sender.POS_Y

        lblSlotName.Text = (Sender.FLOOR_NAME & "-" & (Sender.Slots.Count + 1)) & " (new)"

        btnRemoveSlot.Visible = False

        Sender.HighLight = UC_Product_Slot.HighLightMode.YellowDotted '-------- Select Floor -----------
        pnlSlot.Visible = True
    End Sub

    Private Sub Shelf_SlotSelecting(ByRef Sender As UC_Product_Slot) Handles Shelf.SlotSelecting
        ClearSlotProperty()
        '----------- Inherited Property --------
        txtSlotDepth.Text = Sender.ParentFloor.ParentShelf.SHELF_DEPTH
        txtSlotHeight.Text = Sender.FLOOR_HEIGHT
        txtSlotY.Text = Sender.POS_Y

        txtSlotWidth.Text = Sender.SLOT_WIDTH
        txtSlotX.Text = Sender.POS_X

        lblSlotName.Text = Sender.SLOT_NAME

        btnRemoveSlot.Visible = True

        Sender.HighLight = UC_Product_Slot.HighLightMode.YellowDotted '-------- Select Slot -----------
        pnlSlot.Visible = True
    End Sub

    Private Sub btnRemoveSlot_Click(sender As Object, e As EventArgs) Handles btnRemoveSlot.Click
        Dim Slot As UC_Product_Slot = Shelf.SelectedSlot
        Dim Floor As UC_Product_Floor = Shelf.SelectedSlot.ParentFloor
        Floor.RemoveSlot(Floor.Slots.IndexOf(Slot))

        '---------- Rename Other Slot -------------
        For i As Integer = 0 To Floor.Slots.Count - 1
            Floor.Slots(i).SLOT_NAME = Floor.FLOOR_NAME & "-" & i + 1
        Next

        ClearSlotProperty()
    End Sub

    Private Sub btnCloseSlot_Click(sender As Object, e As EventArgs) Handles btnCloseSlot.Click
        ClearSlotProperty()
    End Sub

    Private Sub btnApplySlot_Click(sender As Object, e As EventArgs) Handles btnApplySlot.Click
        '--------------- Validate -----------------
        If txtSlotWidth.Text = "" Or txtSlotWidth.Text = "0" Or
           txtSlotX.Text = "" Then
            Alert(Me.Page, "กรอกขนาดให้ครบ")
            Exit Sub
        End If
        '--------------- Apply --------------------
        Select Case True
            Case Not IsNothing(Shelf.SelectedFloor) '----------- Add Slot -------------
                Dim SlotName As String = Shelf.SelectedFloor.FLOOR_NAME & "-" & Shelf.SelectedFloor.Slots.Count + 1
                Shelf.SelectedFloor.AddSlot(0, SlotName, txtSlotX.Text, txtSlotWidth.Text,
                                            0, "", 0, "", Drawing.Color.FromName("#f0f0f0"),
                                            Drawing.Color.Green, False)
                ClearSlotProperty()
            Case Not IsNothing(Shelf.SelectedSlot) '---------- Edit Slot -----------
                With Shelf.SelectedSlot
                    .POS_X = txtSlotX.Text
                    .SLOT_WIDTH = txtSlotWidth.Text
                End With
        End Select

    End Sub

    Private Sub btnMoveToSlot_Click(sender As Object, e As EventArgs) Handles btnMoveToSlot.Click

    End Sub

#End Region
#End Region


End Class