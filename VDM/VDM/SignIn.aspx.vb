Imports System.Data
Imports System.Data.SqlClient

Public Class SignIn
    Inherits System.Web.UI.Page

    'Dim BL As New CMPG_BL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblError.Visible = False

        If Not IsPostBack Then
            txtUser.Focus()
        End If

    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click

        'Dim SQL As String = "SELECT * FROM TB_Staff WHERE UserName='" & txtUser.Text.Replace("'", "''") & "' AND Password='" & txtPass.Text.Replace("'", "''") & "'"
        'Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        'Dim DT As New DataTable
        'DA.Fill(DT)

        'If DT.Rows.Count = 0 Then
        '    lblError.Visible = True
        'Else
        '    Session("STAFF_ID") = DT.Rows(0).Item("STAFF_ID")
        '    Session("USER_NAME") = DT.Rows(0).Item("UserName")
        '    Session("STAFF_NAME") = DT.Rows(0).Item("STAFF_NAME")
        '    Session("Role") = DT.Rows(0).Item("Role_ID")

        '    If Session("Role") = CMPG_BL.StaffRole.Viewer Then
        '        SQL = "SELECT Site_ID FROM TB_Staff_Site WHERE STAFF_ID=" & Session("STAFF_ID")
        '        DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        '        DT = New DataTable
        '        DA.Fill(DT)
        '        Session("Available_Site") = DT
        '    End If

        '    Response.Redirect("Default.aspx")
        '    End If
    End Sub
End Class