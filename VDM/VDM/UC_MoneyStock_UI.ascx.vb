Public Class UC_MoneyStock_UI
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub BindMoneyStock(ByVal DT As DataTable)
        rptMoneyStock.DataSource = DT
        rptMoneyStock.DataBind()
    End Sub

    Private Sub rptMoneyStock_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptMoneyStock.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim divContainer As HtmlGenericControl = e.Item.FindControl("divContainer")
        Dim lblName As Label = e.Item.FindControl("lblName")
        Dim lblLevel As Label = e.Item.FindControl("lblLevel")
        Dim progress As HtmlGenericControl = e.Item.FindControl("progress")

        '---------------- Control Level----------------
        Dim Max_Qty As Integer = e.Item.DataItem("Max_Qty")
        Dim Warning_Qty As Integer = e.Item.DataItem("Warning_Qty")
        Dim Critical_Qty As Integer = e.Item.DataItem("Critical_Qty")
        Dim Current_Qty As Integer = 0
        If Not IsDBNull(e.Item.DataItem("Current_Qty")) Then Current_Qty = e.Item.DataItem("Current_Qty")


        lblName.Text = e.Item.DataItem("D_Name").ToString

        '--------------- Set Control Level -----------------------
        If Current_Qty < 0 Then Current_Qty = 0
        If Current_Qty > Max_Qty Then Current_Qty = Max_Qty
        lblLevel.Text = Current_Qty & " / " & Max_Qty
        progress.Style("width") = (Current_Qty * 100 / Max_Qty) & "%"
        Dim Direction As Integer = -1
        If Not IsDBNull(e.Item.DataItem("Movement_Direction")) Then
            Direction = e.Item.DataItem("Movement_Direction")
        End If
        Select Case Direction
            Case 1
                If Current_Qty >= Critical_Qty Then
                    divContainer.Attributes("class") = "row m-a-0 text-danger"
                    progress.Attributes("class") = "progress-bar progress-bar-danger"
                ElseIf Current_Qty >= Warning_Qty Then
                    divContainer.Attributes("class") = "row m-a-0 text-warning"
                    progress.Attributes("class") = "progress-bar progress-bar-warning"
                Else
                    divContainer.Attributes("class") = "row m-a-0 text-success"
                    progress.Attributes("class") = "progress-bar progress-bar-success"
                End If
            Case -1
                If Current_Qty <= Critical_Qty Then
                    divContainer.Attributes("class") = "row m-a-0 text-danger"
                    progress.Attributes("class") = "progress-bar progress-bar-danger"
                ElseIf Current_Qty <= Warning_Qty Then
                    divContainer.Attributes("class") = "row m-a-0 text-warning"
                    progress.Attributes("class") = "progress-bar progress-bar-warning"
                Else
                    divContainer.Attributes("class") = "row m-a-0 text-success"
                    progress.Attributes("class") = "progress-bar progress-bar-success"
                End If
        End Select
    End Sub

End Class