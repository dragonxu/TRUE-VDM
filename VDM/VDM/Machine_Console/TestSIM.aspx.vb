Public Class TestSIM
    Inherits System.Web.UI.Page




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dispenser.AddSlot(1, 1, 150, 1, 237862312, 59, False, True, Drawing.Color.Green, UC_Product_Slot.HighLightMode.None, 3)
        Dispenser.AddSlot(1, 2, 150, 1, 237862317, 39, False, True, Drawing.Color.Red, UC_Product_Slot.HighLightMode.None, 3)
        Dispenser.AddSlot(1, 3, 150, 2, 795384758, 49, False, True, Drawing.Color.Blue, UC_Product_Slot.HighLightMode.None, 3)
    End Sub

End Class