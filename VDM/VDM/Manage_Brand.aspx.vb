
Imports System.Data
Imports System.Data.SqlClient

Public Class Setting_Brand
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL

    Protected Property BRAND_ID As Integer
        Get
            Return Val(txtCode.Attributes("BRAND_ID"))
        End Get
        Set(value As Integer)
            txtCode.Attributes("BRAND_ID") = value
        End Set
    End Property

    Private Property BRAND_Logo As Byte()
        Get
            Try
                Return Session("BRAND_Logo")
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
        Set(value As Byte())
            Session("BRAND_Logo") = value
            imgIcon.ImageUrl = "RenderImage.aspx?Mode=S&UID=BRAND_Logo&t=" & Now.ToOADate.ToString.Replace(".", "")
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNumeric(Session("USER_ID")) Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('กรุณาเข้าสู่ระบบ'); window.location.href='SignIn.aspx';", True)
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
        imgIcon.Attributes("onclick") = "window.open(this.src);"
        ful.Attributes("onchange") = "$('#" & btnUpdateLogo.ClientID & "').click();"
    End Sub

    Private Sub BindList()
        Dim SQL As String = "SELECT * FROM VW_ALL_BRAND ORDER BY BRAND_CODE "
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
        Dim lblProduct As Label = e.Item.FindControl("lblProduct")

        img.ImageUrl = "RenderImage.aspx?Mode=D&Entity=Brand&UID=" & e.Item.DataItem("BRAND_ID") & "&t=" & Now.ToOADate.ToString.Replace(".", "")

        lblCode.Text = e.Item.DataItem("BRAND_CODE").ToString
        lblName.Text = e.Item.DataItem("BRAND_NAME").ToString
        lblProduct.Text = FormatNumber(e.Item.DataItem("Total_Product"), 0)

        Dim btnEdit As Button = e.Item.FindControl("btnEdit")
        Dim btnDelete As Button = e.Item.FindControl("btnDelete")


        btnEdit.CommandArgument = e.Item.DataItem("BRAND_ID")
        btnDelete.CommandArgument = e.Item.DataItem("BRAND_ID")

        Dim btnPreDelete As HtmlInputButton = e.Item.FindControl("btnPreDelete")
        btnPreDelete.Attributes("onclick") = "If(confirm('ยืนยันลบ " & lblName.Text & " ?'))$('#" & btnDelete.ClientID & "').click();"


    End Sub

    Private Sub rptList_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptList.ItemCommand
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Select Case e.CommandName
            Case "Edit"
                Dim SQL As String = "SELECT * FROM MS_Brand WHERE BRAND_ID=" & e.CommandArgument
                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                Dim DT As New DataTable
                DA.Fill(DT)

                If DT.Rows.Count = 0 Then
                    Alert(Me.Page, "ไม่พบข้อมูล")
                    BindList()
                    Exit Sub
                End If

                ClearEditForm()
                BRAND_ID = e.CommandArgument
                lblEditMode.Text = "Edit"

                txtCode.Text = DT.Rows(0).Item("BRAND_CODE").ToString
                txtName.Text = DT.Rows(0).Item("BRAND_NAME").ToString
                BRAND_Logo = BL.Get_Brand_Logo(BRAND_ID)
                chkActive.Checked = DT.Rows(0).Item("Active_Status")

                pnlList.Visible = False
                pnlEdit.Visible = True

            Case "Delete"
                Dim SQL As String = "DELETE FROM MS_Brand" & vbNewLine
                SQL &= " WHERE BRAND_ID=" & e.CommandArgument
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
        BRAND_ID = 0
        txtName.Text = ""
        txtCode.Text = ""
        BRAND_Logo = Nothing
        chkActive.Checked = True
    End Sub

    Private Sub btnUpdateLogo_Click(sender As Object, e As EventArgs) Handles btnUpdateLogo.Click
        Try
            Dim C As New Converter
            Dim B As Byte() = C.StreamToByte(ful.FileContent)
            Dim img As System.Drawing.Image = System.Drawing.Image.FromStream(C.ByteToStream(B))
            BRAND_Logo = B
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

        If IsNothing(BRAND_Logo) Then
            Alert(Me.Page, "เลือก Logo")
            Exit Sub
        End If

        Dim SQL As String = "SELECT * FROM MS_Brand WHERE BRAND_CODE='" & txtCode.Text.Replace("'", "''") & "' AND BRAND_ID<>" & BRAND_ID
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            Alert(Me.Page, "Code ซ้ำ")
            Exit Sub
        End If

        SQL = "SELECT * FROM MS_Brand WHERE BRAND_NAME='" & txtName.Text.Replace("'", "''") & "' AND BRAND_ID<>" & BRAND_ID
        DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        DT = New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            Alert(Me.Page, "Name ซ้ำ")
            Exit Sub
        End If

        SQL = "SELECT * FROM MS_Brand WHERE BRAND_ID=" & BRAND_ID
        DT = New DataTable
        DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        Dim DR As DataRow
        If DT.Rows.Count = 0 Then
            DR = DT.NewRow
            DT.Rows.Add(DR)
            BRAND_ID = GetNewID()
            DR("BRAND_ID") = BRAND_ID
        Else
            DR = DT.Rows(0)
        End If
        DR("BRAND_CODE") = txtCode.Text
        DR("BRAND_NAME") = txtName.Text

        DR("Active_Status") = chkActive.Checked
        DR("Update_By") = Session("USER_ID")
        DR("Update_Time") = Now

        Dim cmd As New SqlCommandBuilder(DA)
        Try
            DA.Update(DT)
            BL.Save_Brand_Logo(BRAND_ID, BRAND_Logo)
        Catch ex As Exception
            Alert(Me.Page, ex.Message)
            Exit Sub
        End Try

        Alert(Me.Page, "บันทึกสำเร็จ")
        ResetPage(Nothing, Nothing)
    End Sub

    Private Function GetNewID() As Integer
        Dim SQL As String = "SELECT IsNull(MAX(BRAND_ID),0)+1 FROM MS_Brand "
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT.Rows(0).Item(0)
    End Function

End Class