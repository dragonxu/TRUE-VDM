Imports System.Data
Imports System.Data.SqlClient
Public Class SIM_Detail
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL

    Private ReadOnly Property LANGUAGE As Integer
        Get
            Return Session("LANGUAGE")
        End Get
    End Property

    Private ReadOnly Property KO_ID As Integer
        Get
            Return Session("KO_ID")
        End Get
    End Property
    Protected Property SIM_ID As Integer
        Get
            Try
                Return lblCode.Attributes("SIM_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(value As Integer)
            lblCode.Attributes("SIM_ID") = value
        End Set
    End Property
    Protected Property D_ID As Integer
        Get
            Try
                Return lblCode.Attributes("D_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(value As Integer)
            lblCode.Attributes("D_ID") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNumeric(Session("LANGUAGE")) Then
            Response.Redirect("Select_Language.aspx")
        End If

        If Not IsPostBack Then
            SIM_ID = Request.QueryString("SIM_ID")
            'BindList()
            BindDetail()
        Else
            initFormPlugin()
        End If
    End Sub

    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

#Region "rpt"

    'Private Sub BindList()
    '    Dim SIM As DataTable = BL.GetList_Current_SIM_Kiosk(KO_ID)
    '    If SIM.Rows.Count > 0 Then
    '        SIM.DefaultView.RowFilter = "SIM_ID=" & SIM_ID
    '        Dim DT_Current As DataTable = SIM.DefaultView.ToTable
    '        For i As Integer = 0 To SIM.Rows.Count - 1
    '            If SIM.Rows(i).Item("SIM_ID") = SIM_ID Then
    '                SIM.Rows.RemoveAt(i)
    '            End If
    '        Next

    '        DT_Current.Merge(SIM)
    '        rptPage.DataSource = DT_Current
    '        rptPage.DataBind()
    '    Else
    '        rptPage.DataSource = SIM
    '        rptPage.DataBind()
    '    End If
    'End Sub

    'Private Sub rptPage_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptPage.ItemDataBound

    '    If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

    '    Dim img As Image = e.Item.FindControl("img")
    '    Dim imgPrice As Image = e.Item.FindControl("imgPrice")
    '    Dim lblDISPLAY_NAME As Label = e.Item.FindControl("lblDISPLAY_NAME")
    '    Dim lblDesc As Label = e.Item.FindControl("lblDesc")
    '    Dim btnSelect_str As LinkButton = e.Item.FindControl("btnSelect_str")

    '    lblDISPLAY_NAME.Text = e.Item.DataItem("DISPLAY_NAME_" & BL.Get_Language_Code(LANGUAGE)).ToString()
    '    lblDesc.Text = e.Item.DataItem("DESCRIPTION_" & BL.Get_Language_Code(LANGUAGE)).ToString()

    '    Dim Package As String = BL.Get_SIM_Package_Picture_Path(e.Item.DataItem("SIM_ID"), LANGUAGE)
    '    If IO.File.Exists(Package) Then
    '        img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=SIM_PACKAGE&UID=" & e.Item.DataItem("SIM_ID") & "&LANG=" & LANGUAGE
    '    Else
    '        img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=SIM_PACKAGE&UID=" & e.Item.DataItem("SIM_ID") & "&LANG=" & VDM_BL.UILanguage.TH
    '    End If

    '    Dim Detail As String = BL.Get_SIM_Detail_Picture_Path(e.Item.DataItem("SIM_ID"), LANGUAGE)
    '    If IO.File.Exists(Detail) Then
    '        imgPrice.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=SIM_DETAIL&UID=" & e.Item.DataItem("SIM_ID") & "&LANG=" & LANGUAGE
    '    Else
    '        imgPrice.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=SIM_DETAIL&UID=" & e.Item.DataItem("SIM_ID") & "&LANG=" & VDM_BL.UILanguage.TH
    '    End If

    '    btnSelect_str.CommandArgument = e.Item.DataItem("SIM_ID")
    '    btnSelect_str.Attributes("D_ID") = IIf(Not IsDBNull(e.Item.DataItem("D_ID")), e.Item.DataItem("D_ID"), 0)

    'End Sub

    'Private Sub rptPage_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptPage.ItemCommand
    '    If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub
    '    Dim btnSelect_str As LinkButton = e.Item.FindControl("btnSelect_str")
    '    Select Case e.CommandName
    '        Case "Select"
    '            Response.Redirect("Device_Shoping_Cart.aspx?SIM_ID=" & btnSelect_str.CommandArgument & "&D_ID=" & btnSelect_str.Attributes("D_ID"))

    '    End Select
    'End Sub

#End Region
    Private Sub BindDetail()
        Dim DT As DataTable = BL.GetList_Current_SIM_Kiosk(KO_ID, SIM_ID)
        If DT.Rows.Count > 0 Then

            lblDISPLAY_NAME.Text = DT.Rows(0).Item("DISPLAY_NAME_" & BL.Get_Language_Code(LANGUAGE)).ToString()
            lblDesc.Text = DT.Rows(0).Item("DESCRIPTION_" & BL.Get_Language_Code(LANGUAGE)).ToString()
            D_ID = IIf(Not IsDBNull(DT.Rows(0).Item("D_ID")), DT.Rows(0).Item("D_ID"), 0)

            Dim Package As String = BL.Get_SIM_Package_Picture_Path(SIM_ID, LANGUAGE)
            If IO.File.Exists(Package) Then
                img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=SIM_PACKAGE&UID=" & SIM_ID & "&LANG=" & LANGUAGE
            Else
                img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=SIM_PACKAGE&UID=" & SIM_ID & "&LANG=" & VDM_BL.UILanguage.TH
            End If

            Dim Detail As String = BL.Get_SIM_Detail_Picture_Path(SIM_ID, LANGUAGE)
            If IO.File.Exists(Detail) Then
                imgPrice.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=SIM_DETAIL&UID=" & SIM_ID & "&LANG=" & LANGUAGE
            Else
                imgPrice.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=SIM_DETAIL&UID=" & SIM_ID & "&LANG=" & VDM_BL.UILanguage.TH
            End If

            'lblPrice_str.Text = ""
            If Not IsDBNull(DT.Rows(0).Item("PRICE")) Then
                lblPrice_Money.Text = FormatNumber(Val(DT.Rows(0).Item("PRICE")), 2)
                'lblCurrency_Str.Text = ""
            End If

        End If

    End Sub

    Private Sub btnSelect_str_Click(sender As Object, e As EventArgs) Handles btnSelect_str.Click
        Response.Redirect("Device_Shoping_Cart.aspx?SIM_ID=" & SIM_ID & "&D_ID=" & D_ID)
    End Sub

    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Home.aspx")
    End Sub

    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click
        Response.Redirect("SIM_List.aspx")
    End Sub
End Class