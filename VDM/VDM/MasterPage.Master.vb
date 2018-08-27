Public Class MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not IsNothing(Session("STAFF_NAME")) Then
                lblUserName.Text = Session("STAFF_NAME").ToString
            End If
            SetupMenu()
        End If
    End Sub

    Private Sub SetupMenu()
        Select Case Session("Role")
            Case CMPG_BL.StaffRole.Administrator
                mnu_Dashboard.Visible = True
                mnu_Report.Visible = True
                mnu_Management.Visible = True
                mnu_Setting.Visible = True
            Case CMPG_BL.StaffRole.Viewer
                mnu_Dashboard.Visible = True
                mnu_Report.Visible = True
                mnu_Management.Visible = False
                mnu_Setting.Visible = False
                '------------ Custom Report ------------
                mnu_Report_Cus_TXN.Visible = False
                badge_Report.InnerHtml = 3
        End Select

    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Session.Abandon()
        Response.Redirect("SignIn.aspx")
    End Sub
End Class