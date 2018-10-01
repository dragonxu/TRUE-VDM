Public Class Select_Language
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Session("KO_ID") = 1

    End Sub

    Private Sub TH_ServerClick(sender As Object, e As EventArgs) Handles TH.ServerClick
        Session("LANGUAGE") = VDM_BL.UILanguage.TH
        Response.Redirect("home.aspx")
    End Sub
    Private Sub EN_ServerClick(sender As Object, e As EventArgs) Handles EN.ServerClick
        Session("LANGUAGE") = VDM_BL.UILanguage.EN
        Response.Redirect("home.aspx")
    End Sub

    Private Sub CN_ServerClick(sender As Object, e As EventArgs) Handles CN.ServerClick
        Session("LANGUAGE") = VDM_BL.UILanguage.CN
        Response.Redirect("home.aspx")
    End Sub

    Private Sub JP_ServerClick(sender As Object, e As EventArgs) Handles JP.ServerClick
        Session("LANGUAGE") = VDM_BL.UILanguage.JP
        Response.Redirect("home.aspx")
    End Sub

    Private Sub KR_ServerClick(sender As Object, e As EventArgs) Handles KR.ServerClick
        Session("LANGUAGE") = VDM_BL.UILanguage.KR
        Response.Redirect("home.aspx")
    End Sub

    Private Sub RU_ServerClick(sender As Object, e As EventArgs) Handles RU.ServerClick
        Session("LANGUAGE") = VDM_BL.UILanguage.RS
        Response.Redirect("home.aspx")
    End Sub

    Private Sub Check_SHIFT()
        '--update Transaction


        '--ตรวจสอบ SHIFT_ID ปิด ? ถ้าปิด ให้ response ไปยังหน้า Manage_Console



    End Sub

End Class