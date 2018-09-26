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

    Public Property SIM_Height As Integer
        Get
            Return lblProperty.Attributes("SIM_Height")
        End Get
        Set(value As Integer)
            lblProperty.Attributes("SIM_Height") = value
            '-------------
        End Set
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub rpt_Slot_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptSlot.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim Slot As UC_SIM_Slot = e.Item.FindControl("Slot")

    End Sub
End Class