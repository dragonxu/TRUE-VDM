﻿Imports System.Data.SqlClient

Public Class UC_Kiosk_Shelf
    Inherits System.Web.UI.UserControl

    Dim BL As New VDM_BL

    Public Property KO_ID As Integer
        Get
            Return pnlShelf.Attributes("KO_ID")
        End Get
        Set(value As Integer)
            pnlShelf.Attributes("KO_ID") = value
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ResetProperty()
            initFirstTimeJavascript()
        Else
            initFormPlugin()
        End If
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

    Public Sub ResetProperty()
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
            Alert(Page, "กรอกขนาดให้ครบ")
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
            Alert(Page, "กรอกขนาดให้ครบ")
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
            Alert(Page, "กรอกขนาดให้ครบ")
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

#Region "DataManagement"

    Public Sub BindData()

        BL.Bind_Product_Shelf(Shelf, KO_ID)

        'Shelf.ResetDimension()
        'Shelf.ClearAllFloor()
        ''--------------- BindShelf ----------------
        'Dim SQL As String = "SELECT * FROM TB_PRODUCT_SHELF WHERE KO_ID=" & KO_ID
        'Dim DT As New DataTable
        'Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        'DA.Fill(DT)
        'If DT.Rows.Count = 0 Then Exit Sub

        'Shelf.SHELF_ID = DT.Rows(0).Item("SHELF_ID")
        'Shelf.SHELF_DEPTH = DT.Rows(0).Item("DEPTH")
        'Shelf.SHELF_WIDTH = DT.Rows(0).Item("WIDTH")
        'Shelf.SHELF_HEIGHT = DT.Rows(0).Item("HEIGHT")

        ''------------ Bind Floor -----------------
        'SQL = "SELECT FLOOR_ID,HEIGHT,POS_Y," & vbLf
        'SQL &= "0 HighLight,CAST(1 AS BIT) ShowFloorName,CAST(1 AS BIT) ShowMenu,NULL SlotDatas " & vbLf
        'SQL &= "FROM TB_PRODUCT_FLOOR" & vbLf
        'SQL &= "WHERE SHELF_ID=" & Shelf.SHELF_ID & " ORDER BY FLOOR_ID"
        'DT = New DataTable
        'DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        'DA.Fill(DT)

        'For f As Integer = 0 To DT.Rows.Count - 1

        '    Dim FLOOR_ID As Integer = DT.Rows(f).Item("FLOOR_ID")
        '    Dim FLOOR_HEIGHT As Integer = DT.Rows(f).Item("HEIGHT")
        '    Dim POS_Y As Integer = DT.Rows(f).Item("POS_Y")

        '    Shelf.AddFloor(FLOOR_ID, FLOOR_HEIGHT, POS_Y, False, True, Nothing, True, f)
        '    Dim Floor As UC_Product_Floor = Shelf.Floors(f)
        '    '-------------- Bind Slot --------------
        '    SQL = "SELECT * FROM TB_PRODUCT_SLOT WHERE FLOOR_ID=" & FLOOR_ID
        '    Dim ST = New DataTable
        '    DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        '    DA.Fill(ST)

        '    For s As Integer = 0 To ST.Rows.Count - 1
        '        Dim SLOT_ID As Integer = ST.Rows(s).Item("SLOT_ID")
        '        Dim POS_X As Integer = ST.Rows(s).Item("POS_X")
        '        Dim SLOT_WIDTH As Integer = ST.Rows(s).Item("WIDTH")
        '        Floor.AddSlot(SLOT_ID, Chr(Asc("A") + f) & "-" & s + 1, POS_X, SLOT_WIDTH, 0, "", 0, "", Drawing.Color.White, Drawing.Color.White, False)
        '        Dim Slot As UC_Product_Slot = Floor.Slots(s)
        '        '------------------- Bind Product----------------
        '        'Slot.PRODUCT_ID = xxxxxxxxxxxxx
        '        'Slot.PRODUCT_CODE = xxxxxxxxxxxxxxx
        '        'Slot.PRODUCT_QUANTITY = xxxxxxxxxxxxxxxxx
        '        'Slot.PRODUCT_LEVEL_PERCENT = xxxxxxxxxxxxxxxxxx
        '        'Slot.PRODUCT_LEVEL_COLOR = xxxxxxxxxxxxxxxxxx
        '        'Slot.QUANTITY_BAR_COLOR = xxxxxxxxxxxxxxxx
        '    Next

        'Next

    End Sub

    Public Function SaveData() As Boolean

        If KO_ID = 0 Then
            Return False
        End If

        If Not SaveShelf() Then Return False
        If Not SaveFloor() Then Return False
        If Not SaveSlot() Then Return False
        If Not SaveProductSerial() Then Return False

        Return True

        '----------------TB_PRODUCT_SERIAL_STOCK-----------------

        Return True
    End Function

    Private Function SaveShelf() As Boolean
        '---------------- PRODUCT_SHELF ---------------
        Dim SQL As String = "SELECT * FROM TB_PRODUCT_SHELF WHERE KO_ID=" & KO_ID
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        Dim DR As DataRow
        If DT.Rows.Count = 0 Then
            DR = DT.NewRow
            DT.Rows.Add(DR)
            Shelf.SHELF_ID = BL.GetNewPrimaryID("TB_PRODUCT_SHELF", "SHELF_ID")
            DR("SHELF_ID") = Shelf.SHELF_ID
            DR("KO_ID") = KO_ID
        Else
            DR = DT.Rows(0)
            Shelf.SHELF_ID = DT.Rows(0).Item("SHELF_ID")
        End If
        DR("WIDTH") = Shelf.SHELF_WIDTH
        DR("HEIGHT") = Shelf.SHELF_HEIGHT
        DR("DEPTH") = Shelf.SHELF_DEPTH

        Dim cmd As New SqlCommandBuilder(DA)
        Try
            DA.Update(DT)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Function SaveFloor() As Boolean

        Dim Sql As String = "SELECT * FROM TB_PRODUCT_FLOOR WHERE SHELF_ID=" & Shelf.SHELF_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(Sql, BL.ConnectionString)
        DA.Fill(DT)

        Dim Floors As List(Of UC_Product_Floor) = Shelf.Floors
        '-------------- Remove UnUsed Floor ------------------
        For i As Integer = 0 To DT.Rows.Count - 1
            Dim FLOOR_ID As Integer = DT.Rows(i).Item("FLOOR_ID")
            Dim Found As Boolean = False
            For f As Integer = 0 To Floors.Count - 1
                If Floors(f).FLOOR_ID = FLOOR_ID Then
                    Found = True
                    Exit For
                End If
            Next
            If Not Found Then
                BL.Drop_PRODUCT_FLOOR(Shelf.SHELF_ID, FLOOR_ID)
            End If
        Next
        If Floors.Count = 0 Then Return True
        '------------- Reload Floor From Database -----------------
        DT = New DataTable
        DA.Fill(DT)
        '-------------- Add New/Update Floor ------------------------
        For i As Integer = 0 To Floors.Count - 1
            DT.DefaultView.RowFilter = "FLOOR_ID=" & Floors(i).FLOOR_ID
            Dim DR As DataRow
            If DT.DefaultView.Count = 0 Then
                DR = DT.NewRow
                Floors(i).FLOOR_ID = BL.GetNewPrimaryID("TB_PRODUCT_FLOOR", "FLOOR_ID") '----------- Update Floor ID-------
                DR("FLOOR_ID") = Floors(i).FLOOR_ID
                DR("SHELF_ID") = Shelf.SHELF_ID
                DT.Rows.Add(DR)
            Else
                DR = DT.DefaultView(0).Row
            End If
            DR("POS_Y") = Floors(i).POS_Y
            DR("HEIGHT") = Floors(i).FLOOR_HEIGHT
        Next
        Dim cmd As New SqlCommandBuilder(DA)
        Try
            DA.Update(DT)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Function SaveSlot() As Boolean

        Dim Floors As List(Of UC_Product_Floor) = Shelf.Floors
        Dim floorList As String = ""
        For i As Integer = 0 To Floors.Count - 1
            floorList &= Floors(i).FLOOR_ID & ","
        Next
        If floorList <> "" Then
            floorList = floorList.Substring(0, floorList.Length - 1)
        Else
            Return True '---------- End All Process -------------
        End If
        Dim Sql As String = "SELECT * FROM TB_PRODUCT_SLOT WHERE FLOOR_ID IN (" & floorList & ")"
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(Sql, BL.ConnectionString)
        DA.Fill(DT)
        '-------------- Remove UnUsed Slot ------------------
        For i As Integer = DT.Rows.Count - 1 To 0 Step -1
            Dim FLOOR_ID As Integer = DT.Rows(i).Item("FLOOR_ID")
            Dim SLOT_ID As Integer = DT.Rows(i).Item("SLOT_ID")
            Dim Found As Boolean = False
            For f As Integer = 0 To Floors.Count - 1
                If Floors(f).FLOOR_ID = FLOOR_ID Then
                    Dim Slots As List(Of UC_Product_Slot) = Floors(f).Slots
                    For s As Integer = 0 To Slots.Count - 1
                        If Slots(s).SLOT_ID = SLOT_ID Then
                            Found = True
                            Exit For
                        End If
                    Next
                End If
                If Found Then Exit For
            Next
            If Not Found Then
                BL.Drop_PRODUCT_SLOT(FLOOR_ID, SLOT_ID)
            End If
        Next
        '------------- Reload Slot From Database -----------------
        DT = New DataTable
        DA.Fill(DT)
        '-------------- Add New/Update Slot ------------------------
        For f As Integer = 0 To Floors.Count - 1

            Dim Slots As List(Of UC_Product_Slot) = Floors(f).Slots

            For s As Integer = 0 To Slots.Count - 1
                DT.DefaultView.RowFilter = "FLOOR_ID=" & Floors(f).FLOOR_ID & " AND SLOT_ID=" & Slots(s).SLOT_ID
                Dim DR As DataRow
                If DT.DefaultView.Count = 0 Then
                    DR = DT.NewRow
                    Slots(s).SLOT_ID = BL.GetNewPrimaryID("TB_PRODUCT_SLOT", "SLOT_ID") '----------- Update Slot ID-------
                    DR("SLOT_ID") = Slots(s).SLOT_ID
                    DR("FLOOR_ID") = Floors(f).FLOOR_ID
                    DT.Rows.Add(DR)
                Else
                    DR = DT.DefaultView(0).Row
                End If
                DR("POS_X") = Slots(s).POS_X
                DR("WIDTH") = Slots(s).SLOT_WIDTH
                DR("Update_Time") = Now

                Dim Product As DataTable = BL.Get_Product_Info_From_ID(Slots(s).PRODUCT_ID)
                '----------- Product Containing---------------
                If Product.Rows.Count = 0 Or Slots(s).PRODUCT_QUANTITY < 1 Then
                    DR("PRODUCT_ID") = DBNull.Value
                    DR("QUANTITY") = DBNull.Value
                    BL.Drop_PRODUCT_STOCK_SERIAL(Slots(s).SLOT_ID)
                Else
                    DR("PRODUCT_ID") = Slots(s).PRODUCT_ID
                    DR("QUANTITY") = Slots(s).PRODUCT_QUANTITY
                    If Not Product.Rows(0).Item("IS_SERIAL") Then
                        BL.Drop_PRODUCT_STOCK_SERIAL(Slots(s).SLOT_ID)
                    End If
                End If

                Dim cmd As New SqlCommandBuilder(DA)
                Try
                    DA.Update(DT)
                    DT.AcceptChanges()
                Catch ex As Exception
                    Return False
                End Try

            Next
        Next


        Return True

    End Function

    Private Function SaveProductSerial() As Boolean

        Return True
    End Function

#End Region

End Class