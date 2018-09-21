Public Class TestScan
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Shelf.KO_ID = 1
            Shelf.BindData()
        End If

    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        Message_Toastr("Testsettsetsete", ToastrMode.Info, ToastrPositon.TopRight, Me.Page, 4000)
    End Sub
End Class