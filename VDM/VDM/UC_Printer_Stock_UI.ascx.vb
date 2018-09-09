Public Class UC_Printer_Stock_UI
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub BindPrinter(ByVal Current_Qty As Object, ByVal Warning_Qty As Object, ByVal Critical_Qty As Object, ByVal Max_Qty As Object)
        If IsNothing(Max_Qty) OrElse IsDBNull(Max_Qty) OrElse IsNothing(Critical_Qty) OrElse IsDBNull(Critical_Qty) OrElse IsNothing(Warning_Qty) OrElse IsDBNull(Warning_Qty) Then
            pnlPrinterStock.Visible = False
            pnlPrinterProgress.Visible = False
        Else
            If IsNothing(Current_Qty) OrElse IsDBNull(Current_Qty) Then Current_Qty = 0
            lblPrintPaper.Text = Current_Qty & " / " & Max_Qty
            barPrinterLevel.Width = Unit.Percentage((Current_Qty * 100 / Max_Qty))
            If Current_Qty < Critical_Qty Then
                pnlPrinterStock.CssClass = "row text-danger p-t"
                barPrinterLevel.CssClass = "progress-bar progress-bar-danger"
            ElseIf Current_Qty < Warning_Qty Then
                pnlPrinterStock.CssClass = "row text-warning p-t"
                barPrinterLevel.CssClass = "progress-bar progress-bar-warning"
            Else
                pnlPrinterStock.CssClass = "row text-success p-t"
                barPrinterLevel.CssClass = "progress-bar progress-bar-success"
            End If
        End If
    End Sub

End Class