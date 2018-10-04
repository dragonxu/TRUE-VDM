Imports System.Data.SqlClient

Public Class Login
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL
    Private ReadOnly Property KO_ID As Integer
        Get
            Try
                Return Request.QueryString("KO_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblError.Visible = False

        If Not IsPostBack Then
            SetButtonBack()
            txtUser.Focus()
        End If
    End Sub

    Private Sub SetButtonBack()
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter("EXEC dbo.SP_CURRENT_OPEN_SHIFT " & KO_ID, BL.ConnectionString)
        DA.Fill(DT)
        btnBack.Visible = DT.Rows.Count > 0
        If Not btnBack.Visible Then
            btnLogin.Style("margin-top") = "30px"
        End If
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click

        If KO_ID = 0 Then
            Alert(Me.Page, "คุณต้อง Login จากหน้าเครื่อง Vending หรือ Management Console")
            Exit Sub
        End If

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

            Response.Cookies("KO_ID").Value = KO_ID
            DT = BL.GetList_Kiosk(KO_ID)
            Session("SHOP_CODE") = DT.Rows(0).Item("SITE_CODE")

            Response.Redirect("Machine_Overview.aspx")
        End If

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("../Front_UI/Default.aspx?KO_ID=" & KO_ID)
    End Sub
End Class