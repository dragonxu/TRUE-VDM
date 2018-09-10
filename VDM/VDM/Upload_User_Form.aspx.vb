Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Drawing
Imports System
Imports System.IO
Imports AjaxControlToolkit

Public Class Upload_User_Form
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL
    Dim CV As New Converter

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNumeric(Session("USER_ID")) Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('กรุณาเข้าสู่ระบบ'); window.location.href='SignIn.aspx';", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            pnlUpload.Visible = True
            pnlList.Visible = False
        Else
            initFormPlugin()
        End If
    End Sub

    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("Setting_Authorize.aspx")
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If Not ful_User.HasFile Then
            Alert(Me.Page, "Choose File")
            Exit Sub
        End If

        If Not ModuleGlobal.ExcelContentType(ful_User.PostedFile.ContentType) Then
            Alert(Me.Page, "เลือกประเภทไฟล์ที่กำหนดดังนี้  .xlsx")
            Exit Sub
        End If

        '-------------------------------------------------------------
        Dim F As New FileStructure
        F.Content = CV.StreamToByte(ful_User.PostedFile.InputStream)
        F.ContentType = ful_User.PostedFile.ContentType.ToLower
        F.FileName = Path.GetFileName(ful_User.PostedFile.FileName)
        Session("FILE_IMPORT") = F
        '-------------------------------------------------------------
        Dim FileName As String = "Temp/Authorize_" & Now.ToOADate.ToString.Replace(".", "")

        '--------- Save To Path------------
        F = Session("FILE_IMPORT")
        If Not IsNothing(F) AndAlso Not IsNothing(F.Content) Then
            Dim FS As FileStream = File.OpenWrite(Server.MapPath(FileName))
            FS.Write(F.Content, 0, F.Content.Length)
            FS.Close()
        End If
        Dim ExcelPath As String = Server.MapPath(FileName)
        Dim Conn As New System.Data.OleDb.OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & ExcelPath & "; Extended Properties=Excel 12.0;")
        Conn.Open()
        Dim DT As New DataTable

        Try
            Dim DA As New OleDbDataAdapter("SELECT * FROM `Authorize$`", Conn)
            DA.Fill(DT)

            DT.DefaultView.RowFilter = " USER_ID <> '' AND  LOGIN_NAME <> '' AND  PASSWORD <>'' AND  FIRST_NAME <>'' AND LAST_NAME<>'' "
            For i As Integer = 0 To DT.DefaultView.Count - 1
                Try
                    If Convert.ToInt32(DT.DefaultView(i).Item("USER_ID")) <= 0 Then
                        Alert(Me.Page, "ตรวจสอบ USER_ID เป็นตัวเลขเท่านั้น")
                        Exit Sub
                    End If
                Catch ex As Exception
                    Alert(Me.Page, "ตรวจสอบ USER_ID เป็นตัวเลขเท่านั้น")
                    Exit Sub
                End Try
            Next

            rptList.DataSource = DT.DefaultView.ToTable
            rptList.DataBind()
        Catch ex As Exception
            Alert(Me.Page, "ตั้งชื่อ Sheet ที่ต้องการนำเข้าเป็น  Authorize")
            Exit Sub
        End Try

        lblTotalList.Text = FormatNumber(DT.Rows.Count, 0)
        pnlUpload.Visible = False
        pnlList.Visible = True

        ful_User = Nothing
        Conn.Close()
        Try
            If File.Exists(Server.MapPath(FileName)) Then
                File.Delete(Server.MapPath(FileName))
            End If
        Catch : End Try

    End Sub

    Private Sub rptList_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptList.ItemDataBound

        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim lblUserID As Label = e.Item.FindControl("lblUserID")
        Dim lblLoginName As Label = e.Item.FindControl("lblLoginName")
        Dim lblPassword As Label = e.Item.FindControl("lblPassword")
        Dim lblFirstName As Label = e.Item.FindControl("lblFirstName")
        Dim lblLasrName As Label = e.Item.FindControl("lblLasrName")

        lblUserID.Text = e.Item.DataItem("USER_ID").ToString
        lblLoginName.Text = e.Item.DataItem("LOGIN_NAME").ToString
        lblPassword.Text = e.Item.DataItem("PASSWORD").ToString
        lblFirstName.Text = e.Item.DataItem("FIRST_NAME").ToString
        lblLasrName.Text = e.Item.DataItem("LAST_NAME").ToString

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Response.Redirect("Upload_User_Form.aspx")
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click

        ' Save Caption 
        Dim DT_User As DataTable = Current_Data()
        Dim DR As DataRow
        If DT_User.Rows.Count > 0 Then

            For i As Integer = 0 To DT_User.Rows.Count - 1
                Dim SQL As String = " SELECT * FROM MS_USER WHERE USER_ID=" & DT_User.Rows(i).Item("USER_ID")
                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                Dim DT As New DataTable
                DA.Fill(DT)
                If DT.Rows.Count = 0 Then
                    DR = DT.NewRow
                    DT.Rows.Add(DR)
                Else
                    DR = DT.Rows(0)
                End If
                DR("USER_ID") = DT_User.Rows(i).Item("USER_ID")
                DR("LOGIN_NAME") = DT_User.Rows(i).Item("LOGIN_NAME")
                DR("PASSWORD") = DT_User.Rows(i).Item("PASSWORD")
                'DR("LDAP_USER") = DT_User.Rows(i).Item("LDAP_USER")
                DR("FIRST_NAME") = DT_User.Rows(i).Item("FIRST_NAME")
                DR("LAST_NAME") = DT_User.Rows(i).Item("LAST_NAME")
                'DR("EMAIL") = DT_User.Rows(i).Item("EMAIL")
                'DR("MOBILE_NO") = DT_User.Rows(i).Item("MOBILE_NO")
                DR("Active_Status") = True
                DR("Update_By") = Session("USER_ID")
                DR("Update_Time") = Now

                Dim cmd As New SqlCommandBuilder(DA)
                Try
                    DA.Update(DT)
                Catch ex As Exception
                    Alert(Me.Page, ex.Message)
                    Exit Sub
                End Try
            Next

        End If

        Alert(Me.Page, "บันทึกสำเร็จ")

        pnlUpload.Visible = True
        pnlList.Visible = False

        Response.Redirect("Setting_Authorize.aspx")
    End Sub

    Public Function Current_Data() As DataTable

        Dim DT As New DataTable
        DT.Columns.Add("USER_ID")
        DT.Columns.Add("LOGIN_NAME")
        DT.Columns.Add("PASSWORD")
        DT.Columns.Add("FIRST_NAME")
        DT.Columns.Add("LAST_NAME")

        For Each rpt As RepeaterItem In rptList.Items
            If rpt.ItemType <> ListItemType.AlternatingItem And rpt.ItemType <> ListItemType.Item Then Continue For

            Dim lblUserID As Label = rpt.FindControl("lblUserID")
            Dim lblLoginName As Label = rpt.FindControl("lblLoginName")
            Dim lblPassword As Label = rpt.FindControl("lblPassword")
            Dim lblFirstName As Label = rpt.FindControl("lblFirstName")
            Dim lblLasrName As Label = rpt.FindControl("lblLasrName")


            Dim DR As DataRow = DT.NewRow

            DR("USER_ID") = lblUserID.Text
            DR("LOGIN_NAME") = lblLoginName.Text
            DR("PASSWORD") = lblPassword.Text
            DR("FIRST_NAME") = lblFirstName.Text
            DR("LAST_NAME") = lblLasrName.Text


            DT.Rows.Add(DR)
        Next


        Return DT
    End Function


End Class