Imports System.Data.SqlClient

Public Class MasterStaffConsole
    Inherits System.Web.UI.MasterPage
    Dim BL As New VDM_BL


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsNumeric(Session("USER_ID")) Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('Please Login'); window.location.href='Login.aspx';", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            LoginInfo()

            GetMachineInfo()

        End If
    End Sub

    Private Sub LoginInfo()
        If Not IsNumeric(Session("FULL_NAME")) Then
            lblLoginName.Text = Session("FULL_NAME")
        Else
            Session.Abandon()
            Response.Redirect("Login.aspx")
        End If

    End Sub

    Private Sub GetMachineInfo()
        Dim DT As DataTable = BL.GetList_Kiosk(BL.KioskID)
        If DT.Rows.Count > 0 Then
            lblMachine_Name.Text = DT.Rows(0).Item("KO_CODE").ToString()
            lblMachine_Location.Text = DT.Rows(0).Item("SITE_CODE").ToString()
            If DT.Rows(0).Item("ZONE").ToString() <> "" Then
                lblMachine_Zone.Text = DT.Rows(0).Item("ZONE").ToString()
            Else
                spanZone.Visible = False
            End If
        Else
            Session.Abandon()
            Response.Redirect("Login.aspx")
        End If


    End Sub

    Private Sub lnkLogout_ServerClick(sender As Object, e As EventArgs) Handles lnkLogout.ServerClick
        Session.Abandon()
        Response.Redirect("Login.aspx")
    End Sub
End Class