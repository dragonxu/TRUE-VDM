
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNumeric(Session("LANGUAGE")) Then
            Response.Redirect("Select_Language.aspx")
        End If

        If Not IsPostBack Then
            BindList()
        Else
            initFormPlugin()
        End If

    End Sub

    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Dim DT_Product_Model As New DataTable

#Region "rptProductList"
    Dim Show_Box As Integer = 9 'จำนวน กล่องที่ต้องการแสดง ต่อ 1 page
    Dim DT_Brand As DataTable
    Private Sub BindList()

        Dim DT As DataTable = BL.GetList_Current_Brand_Kiosk(KO_ID)     '--brand ทั้งหมด
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

    Protected Sub rptList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim img As Image = e.Item.FindControl("img")
        Dim lblBrand As Label = e.Item.FindControl("lblBrand")
        Dim btnBrand As HtmlAnchor = e.Item.FindControl("btnBrand")
        Dim btnSelect As Button = e.Item.FindControl("btnSelect")

        img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=Brand&UID=" & e.Item.DataItem("BRAND_ID")
        lblBrand.Text = e.Item.DataItem("BRAND_NAME").ToString()
        btnBrand.Attributes("onclick") = "$('#" & btnSelect.ClientID & "').click();"
        btnSelect.CommandArgument = e.Item.DataItem("BRAND_ID")

        'If (e.Item.ItemIndex + 1) Mod 3 = 0 Or CountRow = e.Item.ItemIndex + 1 Then
        '    btnBrand.Attributes("class") = "Last"
        'End If
    End Sub
    Protected Sub rptList_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs)
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub
        Dim btnSelect As Button = e.Item.FindControl("btnSelect")
        Select Case e.CommandName
            Case "Select"
                Response.Redirect("Product_List.aspx?BRAND_ID=" & btnSelect.CommandArgument)

        End Select
    End Sub



#End Region


    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Select_Menu.aspx")
    End Sub



End Class