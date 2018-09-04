
Imports System.Data
Imports System.Data.SqlClient

Public Class UC_Product_Spec
    Inherits System.Web.UI.UserControl

    Dim BL As New VDM_BL

    Protected Property Language As VDM_BL.UILanguage
        Get
            Return Val(txtCode.Attributes("Language"))
        End Get
        Set(value As VDM_BL.UILanguage)
            txtCode.Attributes("Language") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

        Else
            initFormPlugin()
        End If

    End Sub
    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

#Region "Caption"

    Public Sub BindList(ByRef DT As DataTable, ByVal Lang As VDM_BL.UILanguage)
        'เงื่อนไข แสดง ตาราง Language
        Language = Lang

        If DT.Rows.Count > 0 Then
            rptCaptionList.DataSource = DT
            rptCaptionList.DataBind()
        Else

            rptCaptionList.DataSource = Nothing
            rptCaptionList.DataBind()
        End If



    End Sub

    Private Sub rptCaptionList_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptCaptionList.ItemDataBound

        Dim ddlSpec_TH As DropDownList = e.Item.FindControl("ddlSpec_TH")
        Dim ddlSpec_EN As DropDownList = e.Item.FindControl("ddlSpec_EN")
        Dim ddlSpec_CH As DropDownList = e.Item.FindControl("ddlSpec_CH")
        Dim ddlSpec_JP As DropDownList = e.Item.FindControl("ddlSpec_JP")
        Dim ddlSpec_KR As DropDownList = e.Item.FindControl("ddlSpec_KR")
        Dim ddlSpec_RS As DropDownList = e.Item.FindControl("ddlSpec_RS")

        Dim txtDescription_TH As TextBox = e.Item.FindControl("txtDescription_TH")
        Dim txtDescription_EN As TextBox = e.Item.FindControl("txtDescription_EN")
        Dim txtDescription_CH As TextBox = e.Item.FindControl("txtDescription_CH")
        Dim txtDescription_JP As TextBox = e.Item.FindControl("txtDescription_JP")
        Dim txtDescription_KR As TextBox = e.Item.FindControl("txtDescription_KR")
        Dim txtDescription_RS As TextBox = e.Item.FindControl("txtDescription_RS")

        'Dim btnDelete As Button = e.Item.FindControl("btnDelete")
        Dim lnkDelete As LinkButton = e.Item.FindControl("lnkDelete")


        If Not IsDBNull(e.Item.DataItem("SPEC_ID")) Then
            BL.Bind_DDL_Spec(ddlSpec_TH, VDM_BL.UILanguage.TH, e.Item.DataItem("SPEC_ID"), True)
            BL.Bind_DDL_Spec(ddlSpec_EN, VDM_BL.UILanguage.EN, e.Item.DataItem("SPEC_ID"), True)
            BL.Bind_DDL_Spec(ddlSpec_CH, VDM_BL.UILanguage.CN, e.Item.DataItem("SPEC_ID"), True)
            BL.Bind_DDL_Spec(ddlSpec_JP, VDM_BL.UILanguage.JP, e.Item.DataItem("SPEC_ID"), True)
            BL.Bind_DDL_Spec(ddlSpec_KR, VDM_BL.UILanguage.KR, e.Item.DataItem("SPEC_ID"), True)
            BL.Bind_DDL_Spec(ddlSpec_RS, VDM_BL.UILanguage.RS, e.Item.DataItem("SPEC_ID"), True)

        Else
            BL.Bind_DDL_Spec(ddlSpec_TH, VDM_BL.UILanguage.TH, Nothing, True)
            BL.Bind_DDL_Spec(ddlSpec_EN, VDM_BL.UILanguage.EN, Nothing, True)
            BL.Bind_DDL_Spec(ddlSpec_CH, VDM_BL.UILanguage.CN, Nothing, True)
            BL.Bind_DDL_Spec(ddlSpec_JP, VDM_BL.UILanguage.JP, Nothing, True)
            BL.Bind_DDL_Spec(ddlSpec_KR, VDM_BL.UILanguage.KR, Nothing, True)
            BL.Bind_DDL_Spec(ddlSpec_RS, VDM_BL.UILanguage.RS, Nothing, True)
        End If

        txtDescription_TH.Text = e.Item.DataItem("DESCRIPTION_TH").ToString
        txtDescription_EN.Text = e.Item.DataItem("DESCRIPTION_EN").ToString
        txtDescription_CH.Text = e.Item.DataItem("DESCRIPTION_CH").ToString
        txtDescription_JP.Text = e.Item.DataItem("DESCRIPTION_JP").ToString
        txtDescription_KR.Text = e.Item.DataItem("DESCRIPTION_KR").ToString
        txtDescription_RS.Text = e.Item.DataItem("DESCRIPTION_RS").ToString

        ddlSpec_TH.Visible = False
        ddlSpec_EN.Visible = False
        ddlSpec_CH.Visible = False
        ddlSpec_JP.Visible = False
        ddlSpec_KR.Visible = False
        ddlSpec_RS.Visible = False
        txtDescription_TH.Visible = False
        txtDescription_EN.Visible = False
        txtDescription_CH.Visible = False
        txtDescription_JP.Visible = False
        txtDescription_KR.Visible = False
        txtDescription_RS.Visible = False

        Select Case Language
            Case VDM_BL.UILanguage.TH
                ddlSpec_TH.Visible = True
                txtDescription_TH.Visible = True

            Case VDM_BL.UILanguage.EN
                ddlSpec_EN.Visible = True
                txtDescription_EN.Visible = True

            Case VDM_BL.UILanguage.CN
                ddlSpec_CH.Visible = True
                txtDescription_CH.Visible = True

            Case VDM_BL.UILanguage.JP
                ddlSpec_JP.Visible = True
                txtDescription_JP.Visible = True

            Case VDM_BL.UILanguage.KR
                ddlSpec_KR.Visible = True
                txtDescription_KR.Visible = True

            Case VDM_BL.UILanguage.RS
                ddlSpec_RS.Visible = True
                txtDescription_RS.Visible = True
        End Select



        Dim btnSpecDelete As Button = e.Item.FindControl("btnSpecDelete")
        btnSpecDelete.CommandArgument = e.Item.DataItem("SPEC_ID")

        Dim btnSpecPreDelete As HtmlInputButton = e.Item.FindControl("btnSpecPreDelete")
        btnSpecPreDelete.Attributes("onclick") = "if(confirm('ยืนยันลบ ?'))$('#" & btnSpecDelete.ClientID & "').click();"



    End Sub

    Private Sub rptCaptionList_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptCaptionList.ItemCommand

        Dim ddlSpec_TH As DropDownList = e.Item.FindControl("ddlSpec_TH")
        Dim ddlSpec_EN As DropDownList = e.Item.FindControl("ddlSpec_EN")
        Dim ddlSpec_CH As DropDownList = e.Item.FindControl("ddlSpec_CH")
        Dim ddlSpec_JP As DropDownList = e.Item.FindControl("ddlSpec_JP")
        Dim ddlSpec_KR As DropDownList = e.Item.FindControl("ddlSpec_KR")
        Dim ddlSpec_RS As DropDownList = e.Item.FindControl("ddlSpec_RS")

        Dim txtDescription_TH As TextBox = e.Item.FindControl("txtDescription_TH")
        Dim txtDescription_EN As TextBox = e.Item.FindControl("txtDescription_EN")
        Dim DT As New DataTable
        Select Case e.CommandName

            Case "Delete"

                DT = New DataTable
                DT = Current_Data()
                DT.Rows.RemoveAt(e.Item.ItemIndex)
                rptCaptionList.DataSource = DT
                rptCaptionList.DataBind()

        End Select

    End Sub


    Private Sub btnAddCapion_TH_Click(sender As Object, e As EventArgs) Handles btnAddCapion_TH.Click

        Dim DT As DataTable = Current_Data()
        Dim DR As DataRow
        DR = DT.NewRow
        DR("SPEC_ID") = 0
        DR("SEQ") = DT.Rows.Count + 1
        DT.Rows.Add(DR)

        rptCaptionList.DataSource = DT
        rptCaptionList.DataBind()

    End Sub


    Public Function BindAllList() As DataTable
        Dim DT As DataTable = Current_Data()
        Return DT
    End Function


    Public Function Current_Data() As DataTable

        Dim DT As New DataTable
        DT.Columns.Add("SPEC_ID", GetType(Integer))
        DT.Columns.Add("SEQ", GetType(Integer))

        DT.Columns.Add("SPEC_ID_TH")
        DT.Columns.Add("SPEC_ID_EN")
        DT.Columns.Add("SPEC_ID_CH")
        DT.Columns.Add("SPEC_ID_JP")
        DT.Columns.Add("SPEC_ID_KR")
        DT.Columns.Add("SPEC_ID_RS")

        DT.Columns.Add("DESCRIPTION_TH")
        DT.Columns.Add("DESCRIPTION_EN")
        DT.Columns.Add("DESCRIPTION_CH")
        DT.Columns.Add("DESCRIPTION_JP")
        DT.Columns.Add("DESCRIPTION_KR")
        DT.Columns.Add("DESCRIPTION_RS")

        DT.Columns.Add("IsNew_Row", GetType(Boolean))

        For Each rpt As RepeaterItem In rptCaptionList.Items
            If rpt.ItemType <> ListItemType.AlternatingItem And rpt.ItemType <> ListItemType.Item Then Continue For

            Dim ddlSpec_TH As DropDownList = rpt.FindControl("ddlSpec_TH")
            Dim ddlSpec_EN As DropDownList = rpt.FindControl("ddlSpec_EN")
            Dim ddlSpec_CH As DropDownList = rpt.FindControl("ddlSpec_CH")
            Dim ddlSpec_JP As DropDownList = rpt.FindControl("ddlSpec_JP")
            Dim ddlSpec_KR As DropDownList = rpt.FindControl("ddlSpec_KR")
            Dim ddlSpec_RS As DropDownList = rpt.FindControl("ddlSpec_RS")

            Dim txtDescription_TH As TextBox = rpt.FindControl("txtDescription_TH")
            Dim txtDescription_EN As TextBox = rpt.FindControl("txtDescription_EN")
            Dim txtDescription_CH As TextBox = rpt.FindControl("txtDescription_CH")
            Dim txtDescription_JP As TextBox = rpt.FindControl("txtDescription_JP")
            Dim txtDescription_KR As TextBox = rpt.FindControl("txtDescription_KR")
            Dim txtDescription_RS As TextBox = rpt.FindControl("txtDescription_RS")
            'Dim btnDelete As Button = rpt.FindControl("btnDelete")
            'Dim lnkDelete As LinkButton = rpt.FindControl("lnkDelete")
            Dim btnSpecDelete As Button = rpt.FindControl("btnSpecDelete")

            Dim DR As DataRow = DT.NewRow
            If ddlSpec_TH.SelectedIndex > 0 Then
                DR("SPEC_ID") = ddlSpec_TH.SelectedValue
            Else
                DR("SPEC_ID") = 0
            End If

            DR("SEQ") = rpt.ItemIndex + 1
            DR("DESCRIPTION_TH") = txtDescription_TH.Text
            DR("DESCRIPTION_EN") = txtDescription_EN.Text
            DR("DESCRIPTION_CH") = txtDescription_CH.Text
            DR("DESCRIPTION_JP") = txtDescription_JP.Text
            DR("DESCRIPTION_KR") = txtDescription_KR.Text
            DR("DESCRIPTION_RS") = txtDescription_RS.Text

            DR("IsNew_Row") = btnSpecDelete.CommandArgument = 0

            DT.Rows.Add(DR)
        Next


        Return DT
    End Function

    Protected Sub ddlSpec_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim ddlSpec As DropDownList = sender
        Dim rpt As RepeaterItem = ddlSpec.Parent

        Dim ddlSpec_TH As DropDownList = DirectCast(rpt.FindControl("ddlSpec_TH"), DropDownList)
        Dim ddlSpec_EN As DropDownList = DirectCast(rpt.FindControl("ddlSpec_EN"), DropDownList)
        Dim ddlSpec_CH As DropDownList = DirectCast(rpt.FindControl("ddlSpec_CH"), DropDownList)
        Dim ddlSpec_JP As DropDownList = DirectCast(rpt.FindControl("ddlSpec_JP"), DropDownList)
        Dim ddlSpec_KR As DropDownList = DirectCast(rpt.FindControl("ddlSpec_KR"), DropDownList)
        Dim ddlSpec_RS As DropDownList = DirectCast(rpt.FindControl("ddlSpec_RS"), DropDownList)

        Dim txtDescription_TH As TextBox = DirectCast(rpt.FindControl("txtDescription_TH"), TextBox)
        Dim txtDescription_EN As TextBox = DirectCast(rpt.FindControl("txtDescription_EN"), TextBox)
        Dim txtDescription_CH As TextBox = DirectCast(rpt.FindControl("txtDescription_CH"), TextBox)
        Dim txtDescription_JP As TextBox = DirectCast(rpt.FindControl("txtDescription_JP"), TextBox)
        Dim txtDescription_KR As TextBox = DirectCast(rpt.FindControl("txtDescription_KR"), TextBox)
        Dim txtDescription_RS As TextBox = DirectCast(rpt.FindControl("txtDescription_RS"), TextBox)

        If ddlSpec.SelectedValue <> -1 Then

            ddlSpec_TH.SelectedValue = ddlSpec.SelectedValue
            ddlSpec_EN.SelectedValue = ddlSpec.SelectedValue
            ddlSpec_CH.SelectedValue = ddlSpec.SelectedValue
            ddlSpec_JP.SelectedValue = ddlSpec.SelectedValue
            ddlSpec_KR.SelectedValue = ddlSpec.SelectedValue
            ddlSpec_RS.SelectedValue = ddlSpec.SelectedValue


            If ddlSpec.SelectedIndex > 0 Then
                Dim SQL As String = "SELECT * FROM MS_Spec WHERE SPEC_ID=" & ddlSpec.SelectedValue & " AND IS_QUALITATIVE=1"
                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                Dim DT As New DataTable
                DA.Fill(DT)
                If DT.Rows.Count > 0 Then

                    ModuleGlobal.ImplementJavaNumericText(txtDescription_TH)
                    ModuleGlobal.ImplementJavaNumericText(txtDescription_EN)
                    ModuleGlobal.ImplementJavaNumericText(txtDescription_CH)
                    ModuleGlobal.ImplementJavaNumericText(txtDescription_JP)
                    ModuleGlobal.ImplementJavaNumericText(txtDescription_KR)
                    ModuleGlobal.ImplementJavaNumericText(txtDescription_RS)

                End If
            End If
        Else

            '--คืนค่า ddl  หลังจากกด เพิ่ม Spec 
            Try
                If ddlSpec_TH.SelectedValue <> -1 Then
                    ddlSpec.SelectedValue = ddlSpec_TH.SelectedValue
                ElseIf ddlSpec_EN.SelectedValue <> -1 Then
                    ddlSpec.SelectedValue = ddlSpec_EN.SelectedValue
                ElseIf ddlSpec_CH.SelectedValue <> -1 Then
                    ddlSpec.SelectedValue = ddlSpec_CH.SelectedValue
                ElseIf ddlSpec_JP.SelectedValue <> -1 Then
                    ddlSpec.SelectedValue = ddlSpec_JP.SelectedValue
                ElseIf ddlSpec_KR.SelectedValue <> -1 Then
                    ddlSpec.SelectedValue = ddlSpec_KR.SelectedValue
                ElseIf ddlSpec_RS.SelectedValue <> -1 Then
                    ddlSpec.SelectedValue = ddlSpec_RS.SelectedValue
                Else
                    ddlSpec.SelectedIndex = 0
                End If
            Catch ex As Exception
                ddlSpec.SelectedIndex = 0
            End Try


            '------Dialog Add Spec
            pnlModal.Visible = True
            '--Clear--
            txtSpec_THAI.Text = ""
            txtSpec_ENGLISH.Text = ""
            txtSpec_CHINESE.Text = ""
            txtSpec_JAPANESE.Text = ""
            txtSpec_KOREAN.Text = ""
            txtSpec_RUSSIAN.Text = ""
            chkIS_QUALITATIVE.Checked = False
        End If

        Current_Data()

    End Sub

    Protected Sub txtDescription_TextChanged(sender As Object, e As EventArgs)
        Dim txtDescription As TextBox = sender
        Dim rpt As RepeaterItem = txtDescription.Parent

        Dim txtDescription_TH As TextBox = DirectCast(rpt.FindControl("txtDescription_TH"), TextBox)
        Dim txtDescription_EN As TextBox = DirectCast(rpt.FindControl("txtDescription_EN"), TextBox)
        Dim txtDescription_CH As TextBox = DirectCast(rpt.FindControl("txtDescription_CH"), TextBox)
        Dim txtDescription_JP As TextBox = DirectCast(rpt.FindControl("txtDescription_JP"), TextBox)
        Dim txtDescription_KR As TextBox = DirectCast(rpt.FindControl("txtDescription_KR"), TextBox)
        Dim txtDescription_RS As TextBox = DirectCast(rpt.FindControl("txtDescription_RS"), TextBox)

        Dim ddlSpec_TH As DropDownList = DirectCast(rpt.FindControl("ddlSpec_TH"), DropDownList)
        If ddlSpec_TH.SelectedIndex > 0 Then
            Dim SQL As String = "SELECT * FROM MS_Spec WHERE SPEC_ID=" & ddlSpec_TH.SelectedValue & " AND IS_QUALITATIVE=1"
            Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
            Dim DT As New DataTable
            DA.Fill(DT)
            If DT.Rows.Count > 0 Then
                txtDescription_TH.Text = txtDescription.Text
                txtDescription_EN.Text = txtDescription.Text
                txtDescription_CH.Text = txtDescription.Text
                txtDescription_JP.Text = txtDescription.Text
                txtDescription_KR.Text = txtDescription.Text
                txtDescription_RS.Text = txtDescription.Text
            End If
        End If

        Current_Data()

    End Sub

    'Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
    '    Dim lnk As LinkButton = sender
    '    Dim rpt As RepeaterItem = lnk.Parent.Parent

    '    Dim lnkDelete As LinkButton = DirectCast(rpt.FindControl("lnkDelete"), LinkButton)

    '    Dim DT As New DataTable
    '    DT = Current_Data()
    '    DT.Rows.RemoveAt(rpt.ItemIndex)
    '    rptCaptionList.DataSource = DT
    '    rptCaptionList.DataBind()

    '    initFormPlugin()
    'End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        pnlModal.Visible = False
    End Sub

    Private Sub btnOKSpec_Click(sender As Object, e As EventArgs) Handles btnOKSpec.Click
        If txtSpec_THAI.Text = "" Then
            Alert(Me.Page, "กรอก Thai")
            Exit Sub
        End If

        If txtSpec_ENGLISH.Text = "" Then
            Alert(Me.Page, "กรอก English")
            Exit Sub
        End If

        If txtSpec_CHINESE.Text = "" Then
            Alert(Me.Page, "กรอก Cninese")
            Exit Sub
        End If

        If txtSpec_JAPANESE.Text = "" Then
            Alert(Me.Page, "กรอก Japanese")
            Exit Sub
        End If

        If txtSpec_KOREAN.Text = "" Then
            Alert(Me.Page, "กรอก Korean")
            Exit Sub
        End If

        If txtSpec_RUSSIAN.Text = "" Then
            Alert(Me.Page, "กรอก Russian")
            Exit Sub
        End If

        Dim SQL As String = "SELECT * FROM MS_Spec WHERE SPEC_NAME_TH='" & txtSpec_THAI.Text.Replace("'", "''") & "'  "
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            Alert(Me.Page, "Display Name Thai ซ้ำ")
            Exit Sub
        End If

        'Product
        SQL = "SELECT * FROM MS_Spec WHERE 0=1"
        DT = New DataTable
        DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        Dim DR As DataRow
        If DT.Rows.Count = 0 Then
            DR = DT.NewRow
            DT.Rows.Add(DR)
            DR("SPEC_ID") = GetNewID()
        Else
            DR = DT.Rows(0)
        End If
        DR("SPEC_NAME_TH") = txtSpec_THAI.Text
        DR("SPEC_NAME_EN") = txtSpec_ENGLISH.Text
        DR("SPEC_NAME_CH") = txtSpec_CHINESE.Text
        DR("SPEC_NAME_JP") = txtSpec_JAPANESE.Text
        DR("SPEC_NAME_KR") = txtSpec_KOREAN.Text
        DR("SPEC_NAME_RS") = txtSpec_RUSSIAN.Text
        DR("Active_Status") = True
        DR("IS_QUALITATIVE") = chkIS_QUALITATIVE.Checked
        Dim cmd As New SqlCommandBuilder(DA)
        Try
            DA.Update(DT)
        Catch ex As Exception
            Alert(Me.Page, ex.Message)
            Exit Sub
        End Try
        Alert(Me.Page, "บันทึกสำเร็จ")
        pnlModal.Visible = False
        Dim DT_Current As DataTable = Current_Data()
        rptCaptionList.DataSource = DT_Current
        rptCaptionList.DataBind()
    End Sub


#End Region


    Private Function GetNewID() As Integer
        Dim SQL As String = "SELECT IsNull(MAX(SPEC_ID),0)+1 FROM MS_Spec "
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT.Rows(0).Item(0)
    End Function


End Class