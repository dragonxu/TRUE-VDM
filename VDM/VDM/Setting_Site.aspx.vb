Imports System.Data.SqlClient
Public Class Setting_Site
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL

    Protected Property SITE_ID As Integer
        Get
            Return Val(txtCode.Attributes("Site_ID"))
        End Get
        Set(value As Integer)
            txtCode.Attributes("Site_ID") = value
        End Set
    End Property

    Private Property SITE_Icon As Byte()
        Get
            Try
                Return Session("SITE_Icon")
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
        Set(value As Byte())
            Session("SITE_Icon") = value

            If Not IsNothing(value) Then
                imgIcon.ImageUrl = "RenderImage.aspx?Mode=S&UID=SITE_Icon&t=" & Now.ToOADate.ToString.Replace(".", "")
            Else
                imgIcon.ImageUrl = "images/BlankIcon.png"
            End If

        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNumeric(Session("USER_ID")) Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('Please Login'); window.location.href='SignIn.aspx';", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            ResetPage(Nothing, Nothing)
            initFirstTimeJavascript()
        Else
            initFormPlugin()
        End If

    End Sub

    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Private Sub initFirstTimeJavascript()
        ImplementJavaNumericText(txtLAT, "center")
        ImplementJavaNumericText(txtLON, "center")
        imgIcon.Attributes("onclick") = "window.open(this.src);"
        ful.Attributes("onchange") = "$('#" & btnUpdateIcon.ClientID & "').click();"
    End Sub

    Private Sub BindList()
        Dim SQL As String = "SELECT * FROM VW_ALL_SITE ORDER BY SITE_CODE "
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

        Dim img As Image = e.Item.FindControl("img")

        Dim lblCode As Label = e.Item.FindControl("lblCode")
        Dim lblName As Label = e.Item.FindControl("lblName")
        Dim lblAddress As Label = e.Item.FindControl("lblAddress")
        Dim lblKiosk As Label = e.Item.FindControl("lblKiosk")

        img.ImageUrl = "RenderImage.aspx?Mode=D&Entity=Site&UID=" & e.Item.DataItem("Site_ID") & "&t=" & Now.ToOADate.ToString.Replace(".", "")

        lblCode.Text = e.Item.DataItem("Site_Code").ToString
        lblName.Text = e.Item.DataItem("Site_Name").ToString

        If Not IsDBNull(e.Item.DataItem("TUMBOL_NAME")) Then
            If e.Item.DataItem("PROVINCE_CODE").ToString = "10" Then
                lblAddress.Text &= "แขวง" & e.Item.DataItem("TUMBOL_NAME") & " "
            Else
                lblAddress.Text &= "ต." & e.Item.DataItem("TUMBOL_NAME") & " "
            End If
        End If
        If Not IsDBNull(e.Item.DataItem("AMPHUR_NAME")) Then
            If e.Item.DataItem("PROVINCE_CODE").ToString = "10" Then
                lblAddress.Text &= e.Item.DataItem("AMPHUR_NAME") & " "
            Else
                lblAddress.Text &= "อ." & e.Item.DataItem("AMPHUR_NAME") & " "
            End If
        End If
        If Not IsDBNull(e.Item.DataItem("PROVINCE_NAME")) Then
            lblAddress.Text &= e.Item.DataItem("PROVINCE_NAME") & " "
        End If

        lblKiosk.Text = FormatNumber(e.Item.DataItem("Total_Kiosk"), 0)

        Dim btnEdit As Button = e.Item.FindControl("btnEdit")
        Dim btnDelete As Button = e.Item.FindControl("btnDelete")


        btnEdit.CommandArgument = e.Item.DataItem("SITE_ID")
        btnDelete.CommandArgument = e.Item.DataItem("SITE_ID")

        Dim btnPreDelete As HtmlInputButton = e.Item.FindControl("btnPreDelete")
        btnPreDelete.Attributes("onclick") = "if(confirm('ยืนยันลบ " & lblName.Text & " ?'))$('#" & btnDelete.ClientID & "').click();"

    End Sub

    Private Sub rptList_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptList.ItemCommand
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Select Case e.CommandName
            Case "Edit"
                Dim SQL As String = "SELECT * FROM MS_Site WHERE SITE_ID=" & e.CommandArgument
                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                Dim DT As New DataTable
                DA.Fill(DT)

                If DT.Rows.Count = 0 Then
                    Alert(Me.Page, "ไม่พบข้อมูล")
                    BindList()
                    Exit Sub
                End If

                ClearEditForm()
                SITE_ID = e.CommandArgument
                lblEditMode.Text = "Edit"

                txtCode.Text = DT.Rows(0).Item("SITE_CODE").ToString
                txtName.Text = DT.Rows(0).Item("SITE_NAME").ToString
                '------------ Clear Province Location -----------
                BL.Bind_DDL_Province(ddlProvince, DT.Rows(0).Item("PROVINCE_CODE").ToString)
                BL.Bind_DDL_Amphur(ddlAmphur, DT.Rows(0).Item("PROVINCE_CODE").ToString, DT.Rows(0).Item("AMPHUR_CODE").ToString)
                BL.Bind_DDL_Tumbol(ddlTumbol, DT.Rows(0).Item("AMPHUR_CODE").ToString, DT.Rows(0).Item("TUMBOL_CODE").ToString)

                SITE_Icon = BL.Get_Site_Icon(SITE_ID)
                txtLAT.Text = DT.Rows(0).Item("LAT").ToString
                txtLON.Text = DT.Rows(0).Item("LON").ToString
                chkActive.Checked = DT.Rows(0).Item("Active_Status")

                pnlList.Visible = False
                pnlEdit.Visible = True

            Case "Delete"
                Dim SQL As String = "DELETE FROM MS_Site" & vbNewLine
                SQL &= " WHERE SITE_ID=" & e.CommandArgument
                BL.ExecuteNonQuery(SQL)
                BindList()
        End Select
    End Sub

    Protected Sub ResetPage(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        BindList()
        ClearEditForm()
        pnlList.Visible = True
        pnlEdit.Visible = False
    End Sub

    Private Sub ClearEditForm()
        SITE_ID = 0

        txtCode.Text = ""
        txtName.Text = ""
        '------------ Clear Province Location -----------
        BL.Bind_DDL_Province(ddlProvince)
        ddlProvince_SelectedIndexChanged(ddlProvince, Nothing)

        SITE_Icon = Nothing
        txtLAT.Text = ""
        txtLON.Text = ""

        chkActive.Checked = True
    End Sub

    Private Sub ddlProvince_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlProvince.SelectedIndexChanged
        Dim _id As String = "" : If ddlProvince.SelectedIndex > 0 Then _id = ddlProvince.Items(ddlProvince.SelectedIndex).Value
        BL.Bind_DDL_Amphur(ddlAmphur, _id)
        ddlAmphur_SelectedIndexChanged(sender, e)
    End Sub

    Private Sub ddlAmphur_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlAmphur.SelectedIndexChanged
        Dim _id As String = "" : If ddlAmphur.SelectedIndex > 0 Then _id = ddlAmphur.Items(ddlAmphur.SelectedIndex).Value
        BL.Bind_DDL_Tumbol(ddlTumbol, _id)
    End Sub

    Private Sub btnUpdateLogo_Click(sender As Object, e As EventArgs) Handles btnUpdateIcon.Click
        Try

            Dim C As New Converter
            Dim B As Byte() = C.StreamToByte(ful.FileContent)
            Dim img As System.Drawing.Image = System.Drawing.Image.FromStream(C.ByteToStream(B))
            SITE_Icon = B
        Catch ex As Exception
            Alert(Me.Page, "Support only image jpeg gif png\nAnd file size must not larger than 4MB")
            Exit Sub
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ClearEditForm()
        lblEditMode.Text = "Add new"
        '-----------------------------------
        pnlList.Visible = False
        pnlEdit.Visible = True
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtCode.Text = "" Then
            Alert(Me.Page, "กรอก Code")
            Exit Sub
        End If

        If txtName.Text = "" Then
            Alert(Me.Page, "กรอก Name")
            Exit Sub
        End If

        If IsNothing(SITE_Icon) Then
            Alert(Me.Page, "เลือก Icon")
            Exit Sub
        End If

        Dim SQL As String = "SELECT * FROM MS_Site WHERE SITE_CODE='" & txtCode.Text.Replace("'", "''") & "' AND SITE_ID<>" & SITE_ID
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            Alert(Me.Page, "Code ซ้ำ")
            Exit Sub
        End If

        SQL = "SELECT * FROM MS_Site WHERE SITE_NAME='" & txtName.Text.Replace("'", "''") & "' AND SITE_ID<>" & SITE_ID
        DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        DT = New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            Alert(Me.Page, "Name ซ้ำ")
            Exit Sub
        End If

        SQL = "SELECT * FROM MS_Site WHERE Site_ID=" & SITE_ID
        DT = New DataTable
        DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        Dim DR As DataRow
        If DT.Rows.Count = 0 Then
            DR = DT.NewRow
            DT.Rows.Add(DR)
            SITE_ID = GetNewID()
            DR("Site_ID") = SITE_ID
        Else
            DR = DT.Rows(0)
        End If

        DR("Site_Code") = txtCode.Text
        DR("Site_Name") = txtName.Text

        If ddlProvince.SelectedIndex > 0 Then
            DR("PROVINCE_CODE") = ddlProvince.Items(ddlProvince.SelectedIndex).Value
        Else
            DR("PROVINCE_CODE") = DBNull.Value
        End If
        If ddlAmphur.SelectedIndex > 0 Then
            DR("AMPHUR_CODE") = ddlAmphur.Items(ddlAmphur.SelectedIndex).Value
        Else
            DR("AMPHUR_CODE") = DBNull.Value
        End If
        If ddlTumbol.SelectedIndex > 0 Then
            DR("TUMBOL_CODE") = ddlTumbol.Items(ddlTumbol.SelectedIndex).Value
        Else
            DR("TUMBOL_CODE") = DBNull.Value
        End If

        If IsNumeric(txtLAT.Text) Then
            DR("LAT") = Val(txtLAT.Text)
        End If
        If IsNumeric(txtLON.Text) Then
            DR("LON") = Val(txtLON.Text)
        End If

        DR("Active_Status") = chkActive.Checked
        DR("Update_By") = Session("USER_ID")
        DR("Update_Time") = Now

        Dim cmd As New SqlCommandBuilder(DA)
        Try
            DA.Update(DT)
            BL.Save_Site_Icon(SITE_ID, SITE_Icon)
        Catch ex As Exception
            Alert(Me.Page, ex.Message)
            Exit Sub
        End Try

        Alert(Me.Page, "บันทึกสำเร็จ")
        ResetPage(Nothing, Nothing)
    End Sub

    Private Function GetNewID() As Integer
        Dim SQL As String = "SELECT IsNull(MAX(SITE_ID),0)+1 FROM MS_Site "
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT.Rows(0).Item(0)
    End Function


End Class