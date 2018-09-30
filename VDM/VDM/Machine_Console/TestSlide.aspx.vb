Public Class TestSlide
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Script As String = ""
        Dim Max As Integer = 20
        Dim Unit_Value As Integer = 5
        Dim Currentt_Value As Integer = 15

        Script &= "initSlider('" & range_5.ClientID & "'," & Max & "," & Unit_Value & "," & Currentt_Value & "," & "" & txt_Score.ClientID & ");" & vbLf
        'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "slider", Script, True)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Slid", Script)
    End Sub

    Private Sub link_ServerClick(sender As Object, e As EventArgs) Handles link.ServerClick
        Dim value As String = txt_Score.Text
        Dim valuess As String = range_5.Value

    End Sub
End Class