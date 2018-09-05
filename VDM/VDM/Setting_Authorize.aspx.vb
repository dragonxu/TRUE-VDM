Imports System.Data
Imports System.Data.SqlClient

Imports System.IO
Imports System.Reflection
Imports ClosedXML.Excel
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Setting_Authorize
    Inherits System.Web.UI.Page
    Dim BL As New VDM_BL

    Protected Property USER_ID As Integer
        Get
            Return Val(lblUser_ID.Attributes("USER_ID"))
        End Get
        Set(value As Integer)
            lblUser_ID.Attributes("USER_ID") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsNumeric(Session("USER_ID")) Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('กรุณาเข้าสู่ระบบ'); window.location.href='SignIn.aspx';", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            ResetPage(Nothing, Nothing)
        Else
            initFormPlugin()
        End If


    End Sub

    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Protected Sub ResetPage(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        BindList()
        ClearEditForm()
        pnlList.Visible = True
        pnlEdit.Visible = False
    End Sub

    Private Sub ClearEditForm()
        'USER_ID = 0 คือ Supper Admin
        USER_ID = -1

        txtLoginName.Text = ""
        txtPassword.Text = ""
        txtFirstName.Text = ""
        txtLastName.Text = ""
        chkActive.Checked = True

    End Sub

#Region "Download"

    Private Sub btnDownloadForm_Click(sender As Object, e As EventArgs) Handles btnDownloadForm.Click
        Try

            Dim appXL As Excel.Application
            Dim wbXl As Excel.Workbook
            Dim shXL As Excel.Worksheet
            Dim raXL As Excel.Range

            ' Start Excel and get Application object.
            appXL = CreateObject("Excel.Application")
            appXL.Visible = True

            ' Add a new workbook.
            wbXl = appXL.Workbooks.Add

            shXL = wbXl.ActiveSheet

            shXL.Name = "Authorize"
            Dim SQL As String = "SELECT USER_ID,LOGIN_NAME,PASSWORD,FIRST_NAME,LAST_NAME  FROM MS_USER ORDER BY USER_ID "
            Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
            Dim DT As New DataTable
            DA.Fill(DT)
            ' Add table headers going cell by cell.
            shXL.Cells(1, 1).Value = "USER_ID"
            shXL.Cells(1, 2).Value = "LOGIN_NAME"
            shXL.Cells(1, 3).Value = "PASSWORD"
            shXL.Cells(1, 4).Value = "FIRST_NAME"
            shXL.Cells(1, 5).Value = "LAST_NAME"

            ' Format as bold, vertical alignment = center.
            With shXL.Range("A1", "E1")
                .Font.Bold = True
                .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
            End With

            Dim cell(DT.Rows.Count - 1, 5) As String
            For i As Integer = 0 To DT.Rows.Count - 1
                cell(i, 0) = DT.Rows(i).Item("USER_ID")
                cell(i, 1) = DT.Rows(i).Item("LOGIN_NAME")
                cell(i, 2) = DT.Rows(i).Item("PASSWORD")
                cell(i, 3) = DT.Rows(i).Item("FIRST_NAME")
                cell(i, 4) = DT.Rows(i).Item("LAST_NAME")
            Next

            '' Fill A2:B6 with an array of values (First and Last Names).
            shXL.Range("A2", "E" & DT.Rows.Count + 1).Value = cell

            ' AutoFit columns A:D.
            raXL = shXL.Range("A1", "E1")
            raXL.EntireColumn.AutoFit()

            ' Make sure Excel is visible and give the user control
            ' of Excel's lifetime.
            appXL.Visible = True
            appXL.UserControl = True

            ' Release object references.
            raXL = Nothing
            shXL = Nothing
            wbXl = Nothing
            appXL.Quit()
            appXL = Nothing
            Exit Sub

        Catch ex As Exception
            Alert(Me.Page, ex.Message)
            Exit Sub
        End Try

    End Sub



#End Region

    Private Sub BindList()
        Dim SQL As String = "SELECT * FROM MS_USER ORDER BY USER_ID "
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        rptList.DataSource = DT
        rptList.DataBind()

        lblTotalList.Text = FormatNumber(DT.Rows.Count, 0)

        pnlList.Visible = True
        pnlEdit.Visible = False

    End Sub

    Private Sub rptList_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptList.ItemDataBound

        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim lblUserID As Label = e.Item.FindControl("lblUserID")
        Dim lblLoginName As Label = e.Item.FindControl("lblLoginName")
        Dim lblPassword As Label = e.Item.FindControl("lblPassword")
        'Dim lblFirstName As Label = e.Item.FindControl("lblFirstName")
        'Dim lblLastName As Label = e.Item.FindControl("lblLastName")

        Dim lblFullName As Label = e.Item.FindControl("lblFullName")

        Dim chkAvailable As CheckBox = e.Item.FindControl("chkAvailable")
        Dim btnToggle As Button = e.Item.FindControl("btnToggle")

        Dim btnEdit As Button = e.Item.FindControl("btnEdit")

        lblUserID.Text = e.Item.DataItem("USER_ID").ToString
        lblLoginName.Text = e.Item.DataItem("LOGIN_NAME").ToString
        lblPassword.Text = e.Item.DataItem("PASSWORD").ToString
        'lblFirstName.Text = e.Item.DataItem("FIRST_NAME").ToString
        'lblLastName.Text = e.Item.DataItem("LAST_NAME").ToString
        lblFullName.Text = e.Item.DataItem("FIRST_NAME").ToString & "  " & e.Item.DataItem("LAST_NAME").ToString


        chkAvailable.Checked = e.Item.DataItem("Active_Status")

        chkAvailable.Attributes("onClick") = "$('#" & btnToggle.ClientID & "').click();"


        btnToggle.CommandArgument = e.Item.DataItem("USER_ID")





        btnEdit.CommandArgument = e.Item.DataItem("USER_ID")
        Dim btnDelete As Button = e.Item.FindControl("btnDelete")
        btnDelete.CommandArgument = e.Item.DataItem("USER_ID")
        Dim btnPreDelete As HtmlInputButton = e.Item.FindControl("btnPreDelete")
        btnPreDelete.Attributes("onclick") = "if(confirm('ยืนยันลบ ?'))$('#" & btnDelete.ClientID & "').click();"

    End Sub

    Private Sub rptList_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptList.ItemCommand
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub
        Select Case e.CommandName
            Case "Edit"
                Dim SQL As String = "SELECT * FROM MS_USER WHERE USER_ID=" & e.CommandArgument
                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                Dim DT As New DataTable
                DA.Fill(DT)
                If DT.Rows.Count = 0 Then
                    Alert(Me.Page, "ไม่พบข้อมูล")
                    BindList()
                    Exit Sub
                End If
                ClearEditForm()
                pnlList.Visible = False
                pnlEdit.Visible = True
                USER_ID = e.CommandArgument

                lblEditMode.Text = "Edit"
                '--Detail
                txtLoginName.Text = DT.Rows(0).Item("LOGIN_NAME")
                txtPassword.Text = DT.Rows(0).Item("PASSWORD")
                txtFirstName.Text = DT.Rows(0).Item("FIRST_NAME")
                txtLastName.Text = DT.Rows(0).Item("LAST_NAME")
                chkActive.Checked = DT.Rows(0).Item("Active_Status")

            Case "ToggleStatus"
                USER_ID = e.CommandArgument
                Dim SQL As String = "UPDATE MS_USER Set Active_Status=CASE Active_Status WHEN 1 THEN 0 ELSE 1 END" & vbNewLine
                SQL &= " WHERE  USER_ID=" & USER_ID
                Dim Command As New SqlCommand
                Dim Conn As New SqlConnection(BL.ConnectionString)
                Try
                    Conn.Open()
                    With Command
                        .Connection = Conn
                        .CommandType = CommandType.Text
                        .CommandText = SQL
                        .ExecuteNonQuery()
                        .Dispose()
                    End With
                    Conn.Close()
                    Conn.Dispose()
                Catch ex As Exception
                    Alert(Me.Page, ex.Message.ToString())
                    Exit Sub
                End Try

                Alert(Me.Page, "บันทึกสำเร็จ")
                ResetPage(Nothing, Nothing)

            Case "Delete"
                Dim SQL As String = "DELETE FROM MS_USER" & vbNewLine
                SQL &= " WHERE USER_ID=" & e.CommandArgument
                BL.ExecuteNonQuery(SQL)

                BindList()
        End Select

    End Sub

    Private Sub btnUploadUser_Click(sender As Object, e As EventArgs) Handles btnUploadUser.Click
        Response.Redirect("Upload_User_Form.aspx")
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ClearEditForm()
        lblEditMode.Text = "Add new"
        '-----------------------------------
        pnlList.Visible = False
        pnlEdit.Visible = True
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If txtLoginName.Text = "" Then
            Alert(Me.Page, "กรอก Login Name")
            Exit Sub
        End If
        If txtPassword.Text = "" Then
            Alert(Me.Page, "กรอก Password")
            Exit Sub
        End If
        If txtFirstName.Text = "" Then
            Alert(Me.Page, "กรอก First Name")
            Exit Sub
        End If
        If txtLastName.Text = "" Then
            Alert(Me.Page, "กรอก Last Name")
            Exit Sub
        End If

        Dim SQL As String = "SELECT * FROM MS_USER WHERE Login_Name='" & txtLoginName.Text.Replace("'", "''") & "' AND Password='" & txtPassword.Text.Replace("'", "''") & "' AND USER_ID<>" & USER_ID
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            Alert(Me.Page, "ตรวจสอบ Login Name และ Password ")
            Exit Sub
        End If


        Dim DR As DataRow
        SQL = " SELECT * FROM MS_USER WHERE USER_ID=" & USER_ID
        DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        DT = New DataTable
        DA.Fill(DT)
        If DT.Rows.Count = 0 Then
            DR = DT.NewRow
            DT.Rows.Add(DR)
            USER_ID = GetNewID()
        Else
            DR = DT.Rows(0)
        End If

        DR("USER_ID") = USER_ID
        DR("LOGIN_NAME") = txtLoginName.Text.Trim
        DR("PASSWORD") = txtPassword.Text.Trim
        'DR("LDAP_USER") = ""
        DR("FIRST_NAME") = txtFirstName.Text
        DR("LAST_NAME") = txtLastName.Text
        'DR("EMAIL") = ""
        'DR("MOBILE_NO") = ""
        DR("Active_Status") = chkActive.Checked
        DR("Update_By") = Session("USER_ID")
        DR("Update_Time") = Now

        Dim cmd As New SqlCommandBuilder(DA)
        Try
            DA.Update(DT)
        Catch ex As Exception
            Alert(Me.Page, ex.Message)
            Exit Sub
        End Try

        Alert(Me.Page, "บันทึกสำเร็จ")
        ResetPage(Nothing, Nothing)


    End Sub

    Private Function GetNewID() As Integer
        Dim SQL As String = "SELECT IsNull(MAX(USER_ID),0)+1 FROM MS_USER "
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT.Rows(0).Item(0)
    End Function
End Class