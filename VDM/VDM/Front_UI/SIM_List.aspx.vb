Imports System.Data
Imports System.Data.SqlClient
Public Class SIM_List
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL

#Region "ส่วนที่เหมือนกันหมดทุกหน้า"
    Private ReadOnly Property KO_ID As Integer '------------- เอาไว้เรียกใช้ง่ายๆ ----------
        Get
            Try
                Return Request.Cookies("KO_ID").Value
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Private ReadOnly Property LANGUAGE As VDM_BL.UILanguage '------- ต้องเป็น ReadOnly --------
        Get
            Try
                Return Session("LANGUAGE")
            Catch ex As Exception
                Return 0
            End Try

        End Get
    End Property

    Private ReadOnly Property TXN_ID As Integer
        Get
            Try
                Return Session("TXN_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            DT_CONTROL = UI_CONTROL()
            Bind_CONTROL()
            BindList()
        Else
            initFormPlugin()
        End If


    End Sub

#Region "UI"
    Private ReadOnly Property UI_CONTROL As DataTable  '------------- เอาไว้ดึงข้อมูล UI ----------
        Get
            Try
                Return BL.GET_MS_UI_LANGUAGE(LANGUAGE)
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property
    Dim DT_CONTROL As DataTable
    Public Sub Bind_CONTROL()
        If LANGUAGE > VDM_BL.UILanguage.EN Then
            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_SIM.Text & "'"
            lblUI_SIM.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_SIM.Text)

            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_TOPUP.Text & "'"
            lblUI_TOPUP.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_TOPUP.Text)

            lblUI_SIM.CssClass = "UI"
            lblUI_TOPUP.CssClass = "UI"
        End If
    End Sub

#End Region
    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Private Sub BindList()

        Dim DT As DataTable = BL.GetList_Current_SIM_Kiosk(KO_ID)     '--SIM ทั้งหมด
        rptList.DataSource = DT
        rptList.DataBind()
    End Sub

    Private Sub rptList_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptList.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim img As Image = e.Item.FindControl("img")
        Dim btnSIM As HtmlAnchor = e.Item.FindControl("btnSIM")
        Dim btnSelect As Button = e.Item.FindControl("btnSelect")

        Dim Path As String = BL.Get_SIM_Package_Picture_Path(e.Item.DataItem("SIM_ID"), LANGUAGE)
        If IO.File.Exists(Path) Then
            img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=SIM_PACKAGE&UID=" & e.Item.DataItem("SIM_ID") & "&LANG=" & LANGUAGE
        Else
            img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=SIM_PACKAGE&UID=" & e.Item.DataItem("SIM_ID") & "&LANG=" & VDM_BL.UILanguage.TH
        End If

        btnSIM.Attributes("onclick") = "$('#" & btnSelect.ClientID & "').click();"
        btnSelect.CommandArgument = e.Item.DataItem("SIM_ID")
    End Sub
    Private Sub rptList_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptList.ItemCommand
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub
        Dim btnSelect As Button = e.Item.FindControl("btnSelect")
        Select Case e.CommandName
            Case "Select"
                Response.Redirect("SIM_Detail.aspx?SIM_ID=" & btnSelect.CommandArgument)

        End Select

    End Sub
    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Select_Menu.aspx")
    End Sub


End Class