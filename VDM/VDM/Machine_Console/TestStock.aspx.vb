Imports VDM

Public Class TestStock
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        If Not IsPostBack Then
            ResetPage()
            initFirstTimeJavascript()
        Else
            initFormPlugin()
        End If

    End Sub

    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Private Sub initFirstTimeJavascript()
        ImplementJavaIntegerText(txtShelfWidth, True, 3000, "center")
        ImplementJavaIntegerText(txtShelfHeight, True, 3000, "center")
        ImplementJavaIntegerText(txtShelfDepth, True, 3000, "center")

        ImplementJavaIntegerText(txtFloorWidth, True, 3000, "center")
        ImplementJavaIntegerText(txtFloorHeight, True, 3000, "center")
        ImplementJavaIntegerText(txtFloorDepth, True, 3000, "center")
        ImplementJavaIntegerText(txtFloorY, True, 3000, "center")

        ImplementJavaIntegerText(txtSlotWidth, True, 3000, "center")
        ImplementJavaIntegerText(txtSlotHeight, True, 3000, "center")
        ImplementJavaIntegerText(txtSlotDepth, True, 3000, "center")
        ImplementJavaIntegerText(txtSlotX, True, 3000, "center")
        ImplementJavaIntegerText(txtSlotY, True, 3000, "center")
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
        pnlShelf.Visible = False
    End Sub

    Private Sub ClearFloorProperty()
        Shelf.Deselect_All()
        txtFloorWidth.Text = ""
        txtFloorHeight.Text = ""
        txtFloorDepth.Text = ""
        txtFloorY.Text = ""
        pnlFloor.Visible = False
    End Sub

    Private Sub ClearSlotProperty()
        Shelf.Deselect_All()
        txtSlotWidth.Text = ""
        txtSlotHeight.Text = ""
        txtSlotDepth.Text = ""
        txtSlotX.Text = ""
        txtSlotY.Text = ""
        '----------------- Containing Product --------------
        pnlSlot.Visible = False
    End Sub



#Region "Event"

    Private Sub Shelf_RequestEdit(Sender As UC_Product_Shelf) Handles Shelf.RequestEdit

        ClearShelfProperty()

        txtShelfWidth.Text = Shelf.SHELF_WIDTH
        txtShelfHeight.Text = Shelf.SHELF_HEIGHT
        txtShelfDepth.Text = Shelf.SHELF_DEPTH
        Shelf.IsSelected = True

        pnlShelf.Visible = True
    End Sub

    Private Sub btnCancelShelf_Click(sender As Object, e As EventArgs) Handles btnCancelShelf.Click
        ClearShelfProperty()
    End Sub

    Private Sub btnApplyShelf_Click(sender As Object, e As EventArgs) Handles btnApplyShelf.Click
        '--------------- Validate -----------------

        '--------------- Apply --------------------

    End Sub

    Private Sub btnClearShelf_Click(sender As Object, e As EventArgs) Handles btnClearShelf.Click

    End Sub

#End Region

End Class