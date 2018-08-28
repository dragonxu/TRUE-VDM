Imports System.Data
Imports System.Data.SqlClient

Public Class SignIn
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblError.Visible = False

        If Not IsPostBack Then
            txtUser.Focus()
        End If

    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim SQL As String = "SELECT * FROM MS_USER WHERE LOGIN_NAME='" & txtUser.Text.Replace("'", "''") & "' AND PASSWORD='" & txtPass.Text.Replace("'", "''") & "'"
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        If DT.Rows.Count = 0 Then
            lblError.Visible = True
        ElseIf Not DT.Rows(0).Item("Active_Status") Then
            Alert(Me.Page, "คุณไม่มีสิทธิ์เข้าสู่ระบบ")
        Else
            Session("USER_ID") = DT.Rows(0).Item("USER_ID")
            Session("LOGIN_NAME") = DT.Rows(0).Item("LOGIN_NAME").ToString
            Session("FULL_NAME") = DT.Rows(0).Item("FIRST_NAME").ToString & " " & DT.Rows(0).Item("LAST_NAME").ToString

            Response.Redirect("Default.aspx")
        End If

    End Sub

End Class