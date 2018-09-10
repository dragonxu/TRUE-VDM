Public Class UC_Peripheral_UI
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Dim StatusList As DataTable
    Public Sub BindPeripheral(ByVal Device As DataTable, ByVal StatusTemplate As DataTable)
        StatusList = StatusTemplate
        rptDevice.DataSource = Device
        rptDevice.DataBind()
    End Sub

    Private Sub rptDevice_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptDevice.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim spanDevice As HtmlGenericControl = e.Item.FindControl("spanDevice")
        Dim iconDevice As Image = e.Item.FindControl("iconDevice")
        Dim lblDeviceName As Label = e.Item.FindControl("lblDeviceName")

        lblDeviceName.Text = e.Item.DataItem("D_Name").ToString
        spanDevice.Attributes("Title") = e.Item.DataItem("D_Name").ToString
        iconDevice.ImageUrl = e.Item.DataItem("Icon_White")

        If IsDBNull(e.Item.DataItem("DS_ID")) Then
            spanDevice.Attributes("class") = "btn btn-default"
        Else
            StatusList.DefaultView.RowFilter = "DT_ID=" & e.Item.DataItem("DT_ID") & " AND DS_ID=" & e.Item.DataItem("DS_ID")
            If StatusList.DefaultView.Count > 0 AndAlso Not IsDBNull(StatusList.DefaultView(0).Item("IsProblem")) Then
                If StatusList.DefaultView(0).Item("IsProblem") Then
                    spanDevice.Attributes("class") = "btn btn-danger"
                Else
                    spanDevice.Attributes("class") = "btn btn-success"
                End If
            Else
                spanDevice.Attributes("class") = "btn btn-default"
            End If
        End If
    End Sub

End Class