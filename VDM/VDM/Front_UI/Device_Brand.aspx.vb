
Imports System.Data
Imports System.Data.SqlClient

Public Class Device_Brand
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

    Protected Property Row_Total As Integer
        Get
            Try
                Return lblCode.Attributes("Row_Total")
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(value As Integer)
            lblCode.Attributes("Row_Total") = value
        End Set
    End Property
    Protected Property PageCount As Integer
        Get
            Try
                Return lblCode.Attributes("PageCount")
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(value As Integer)
            lblCode.Attributes("PageCount") = value
        End Set
    End Property
    Protected Property Row_Remain As Integer
        Get
            Try
                Return lblCode.Attributes("Row_Remain")
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(value As Integer)
            lblCode.Attributes("Row_Remain") = value
        End Set
    End Property
    Protected ReadOnly Property CAT_ID As Integer
        Get
            Try
                Return Request.QueryString("CAT_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNumeric(Session("LANGUAGE")) Then
            Response.Redirect("Select_Language.aspx")
        End If

        If Not IsPostBack Then
            DT_CONTROL = UI_CONTROL()
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


#End Region

    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Dim DT_Product_Model As New DataTable

#Region "rptProductList"
    Dim Show_Box As Integer = 9 'จำนวน กล่องที่ต้องการแสดง ต่อ 1 page
    Dim DT_Brand As DataTable
    Private Sub BindList()

        Dim DT As DataTable = BL.GetList_Current_Brand_Kiosk(KO_ID, CAT_ID)     '--brand ทั้งหมด
        Row_Total = Val(DT.Rows.Count)
        PageCount = Math.Ceiling(Val(Row_Total / Show_Box))
        Row_Remain = Row_Total Mod Show_Box

        DT_Brand = DT

        Dim DT_Page As New DataTable
        DT_Page.Columns.Add("Index")


        For i As Integer = 1 To PageCount
            'PageIndex = i
            DT_Page.Rows.Add(i)
        Next

        rptPage.DataSource = DT_Page
        rptPage.DataBind()



    End Sub

    Private Sub rptPage_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptPage.ItemDataBound
        Dim lblUI_Brands As Label = e.Item.FindControl("lblUI_Brands")
        If LANGUAGE > VDM_BL.UILanguage.EN Then
            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_Brands.Text & "'"
            lblUI_Brands.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_Brands.Text)
            lblUI_Brands.CssClass = "UI"
        End If

        'PageIndex

        Dim rptList As Repeater = e.Item.FindControl("rptList")
        AddHandler rptList.ItemDataBound, AddressOf rptList_ItemDataBound

        Dim Index As Integer = Val(e.Item.DataItem("Index"))
        Dim Brand As DataTable = DT_Brand
        Dim DT As New DataTable
        DT.Columns.Add("BRAND_ID")
        DT.Columns.Add("BRAND_NAME")
        Dim DR As DataRow
        If (e.Item.ItemIndex + 1) < PageCount And PageCount <> 1 And (e.Item.ItemIndex + 1) <> PageCount Then
            For i As Integer = (Index * Show_Box) - (Show_Box - 1) To Index * Show_Box
                DR = DT.NewRow
                DR("BRAND_ID") = Brand.Rows(i - 1).Item("BRAND_ID")
                DR("BRAND_NAME") = Brand.Rows(i - 1).Item("BRAND_NAME").ToString
                DT.Rows.Add(DR)
            Next
        Else
            For i As Integer = (Index * Show_Box) - (Show_Box - 1) To Row_Total
                DR = DT.NewRow
                DR("BRAND_ID") = Brand.Rows(i - 1).Item("BRAND_ID")
                DR("BRAND_NAME") = Brand.Rows(i - 1).Item("BRAND_NAME").ToString
                DT.Rows.Add(DR)
            Next
        End If

        rptList.DataSource = DT
        rptList.DataBind()

    End Sub

    Protected Sub rptList_ItemCreated(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim btnBrand As LinkButton = e.Item.FindControl("btnBrand")

        Dim scriptMan As ScriptManager = ScriptManager.GetCurrent(Page)
        scriptMan.RegisterAsyncPostBackControl(btnBrand)
    End Sub

    Protected Sub rptList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim img As Image = e.Item.FindControl("img")
        Dim lblBrand As Label = e.Item.FindControl("lblBrand")
        Dim btnBrand As LinkButton = e.Item.FindControl("btnBrand")

        img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=Brand&UID=" & e.Item.DataItem("BRAND_ID")
        lblBrand.Text = e.Item.DataItem("BRAND_NAME").ToString()
        btnBrand.CommandArgument = e.Item.DataItem("BRAND_ID")

    End Sub
    Protected Sub rptList_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs)
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub
        Select Case e.CommandName
            Case "Select"
                Response.Redirect("Product_List.aspx?BRAND_ID=" & e.CommandArgument & "&CAT_ID=" & CAT_ID)
        End Select
    End Sub

#End Region


    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Select_Menu.aspx")
    End Sub

    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click
        Response.Redirect("Select_Menu.aspx?CAT_ID=" & CAT_ID)
    End Sub
End Class