Imports System.Data
Public Class PageNavigation
    Inherits System.Web.UI.UserControl

    Public Event PageChanged(ByVal Sender As PageNavigation)
    Public Event PageChanging(ByVal Sender As PageNavigation)

    Public TheRepeater As Repeater

    Public Property MaximunPageCount() As Integer
        Get
            Return btnFirst.Attributes("MaximunPageCount")
        End Get
        Set(ByVal value As Integer)
            btnFirst.Attributes("MaximunPageCount") = value
        End Set
    End Property

    Public Property PageSize() As Integer '-------------1
        Get
            Return btnFirst.Attributes("PageSize")
        End Get
        Set(ByVal value As Integer)
            btnFirst.Attributes("PageSize") = value
        End Set
    End Property

    Public Property SesssionSourceName() As String '-------------2
        Get
            Return btnFirst.Attributes("SourceName")
        End Get
        Set(ByVal value As String)
            btnFirst.Attributes("SourceName") = value
        End Set
    End Property

    Public ReadOnly Property Datasource() As DataTable '-------------3
        Get
            If SesssionSourceName = "" Then Return Nothing
            Return Session(SesssionSourceName)
        End Get
    End Property

    Public ReadOnly Property PageCount() As Integer '-------------4
        Get
            If IsNothing(Datasource) Then Return 0
            Dim Source As DataTable = Datasource.Copy
            Return Math.Ceiling(Source.Rows.Count / PageSize)
        End Get
    End Property

    Public Property CurrentPage() As Integer '-------------4
        Get
            Return btnFirst.Attributes("CurrentPage")
        End Get
        Set(ByVal value As Integer)
            If value > PageCount Then
                value = PageCount
            End If
            If value <= 0 Then
                value = 0
            End If
            btnFirst.Attributes("CurrentPage") = value
            RenderLayout()


        End Set
    End Property

    Protected Sub rptPage_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptPage.ItemCommand
        If e.Item.ItemType <> ListItemType.AlternatingItem And e.Item.ItemType <> ListItemType.Item Then Exit Sub
        Dim btnPage As LinkButton = e.Item.FindControl("btnPage")
        CurrentPage = btnPage.Text
    End Sub

    Protected Sub rptPage_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptPage.ItemDataBound
        If e.Item.ItemType <> ListItemType.AlternatingItem And e.Item.ItemType <> ListItemType.Item Then Exit Sub
        Dim btnPage As LinkButton = e.Item.FindControl("btnPage")
        Dim liPage As HtmlGenericControl = e.Item.FindControl("liPage")
        btnPage.Text = e.Item.DataItem("P")

        If e.Item.DataItem("P") = CurrentPage Then
            liPage.Attributes("class") = "active"
        Else
            liPage.Attributes("class") = ""
        End If
    End Sub

    Public Sub RenderLayout()

        RaiseEvent PageChanging(Me)

        '----------------- Set Pager---------------------
        If IsNothing(Datasource) OrElse Datasource.Rows.Count = 0 Then
            Me.Visible = False
            If Not IsNothing(TheRepeater) Then
                TheRepeater.DataSource = Nothing
                TheRepeater.DataBind()
            End If
            Exit Sub
        End If

        Dim Source As DataTable = Datasource.Copy

        Dim TotalRecord As Integer = Source.Rows.Count
        If CurrentPage = 0 Then
            CurrentPage = 1
            Exit Sub
        ElseIf CurrentPage > PageCount Then
            CurrentPage = PageCount
            Exit Sub
        End If

        Dim StartIndex As Integer = (CurrentPage - 1) * PageSize
        Dim EndIndex As Integer = StartIndex + PageSize - 1
        If EndIndex > TotalRecord - 1 Or TotalRecord = 0 Then EndIndex = TotalRecord - 1
        '-------------- Set Display Record-----------

        Dim NewSource As DataTable = Source.Copy
        NewSource.Rows.Clear()

        If StartIndex < 0 Then StartIndex = 0
        For i As Integer = StartIndex To EndIndex
            Dim DR As DataRow = NewSource.NewRow
            DR.ItemArray = Source.Rows(i).ItemArray
            NewSource.Rows.Add(DR)
        Next

        '-----------Render Page Number------------
        Dim TotalGroup As Integer = Math.Ceiling(PageCount / MaximunPageCount)
        Dim ThisGroup As Integer = Math.Ceiling(CurrentPage / MaximunPageCount)

        StartIndex = ((ThisGroup - 1) * MaximunPageCount) + 1
        EndIndex = StartIndex + MaximunPageCount - 1
        If EndIndex > PageCount Then EndIndex = PageCount
        Dim DT As New DataTable
        DT.Columns.Add("P")
        For i As Integer = StartIndex To EndIndex
            Dim DR As DataRow = DT.NewRow
            DR("P") = i
            DT.Rows.Add(DR)
        Next
        rptPage.DataSource = DT
        rptPage.DataBind()

        '-----------End Page Number------------

        '-------- Page Number Visible----------
        btnNext.Visible = CurrentPage < PageCount
        btnBack.Visible = CurrentPage > 1
        btnFirst.Visible = CurrentPage <> 1
        btnLast.Visible = CurrentPage <> PageCount
        '------- End Page Number Visible--------

        If IsNothing(TheRepeater) Then Exit Sub
        TheRepeater.DataSource = NewSource
        TheRepeater.DataBind()

        RaiseEvent PageChanged(Me)

        Me.Visible = PageCount > 1

    End Sub

#Region "Page Change"
    ' First Page
    Protected Sub btnFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFirst.Click
        CurrentPage = 1
    End Sub

    ' Next
    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        CurrentPage += 1
    End Sub

    ' Back
    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        CurrentPage -= 1
    End Sub

    ' last Page
    Protected Sub btnLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLast.Click
        CurrentPage = PageCount
    End Sub

#End Region
End Class