Public Class TestSIM
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SIMStock.KO_ID = 1
            SIMStock.SHOP_CODE = "TestShop1"
            SIMStock.BindData()

            Dim Script As String = "txtBarcode='" & SIMStock.BarcodeClientID & "';" & vbLf
            Script &= "startFocusBarcode();"
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "focusBarcodeReader", Script, True)
            Session("USER_ID") = 1
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        SIMStock.BindData()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If SIMStock.Save() Then
            Message_Toastr("Save success", ToastrMode.Success, ToastrPositon.TopRight, Me.Page)
        End If
    End Sub
End Class