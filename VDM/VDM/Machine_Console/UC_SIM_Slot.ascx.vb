Public Class UC_SIM_Slot
    Inherits System.Web.UI.UserControl

    Public Property Column12Style As Integer
        Get
            Dim Css As String = aContainer.CssClass
            For i As Integer = 1 To 12
                If ExistingCssClass(Css, "col-sm-" & i) Then Return i
            Next
            Return 0
        End Get
        Set(value As Integer)
            Dim Css As String = aContainer.CssClass
            For i As Integer = 1 To 12
                Css = RemoveTagCssClass(Css, "col-sm-" & i)
            Next
            aContainer.CssClass &= " col-sm-" & value
        End Set
    End Property

    Public ReadOnly Property ParentDispenser As UC_SIM_Dispenser
        Get
            Try
                Return Me.Parent.Parent.Parent
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property

    Public ReadOnly Property SIMS As List(Of UC_SIM)
        Get
            Dim Result As New List(Of UC_SIM)
            For Each Item In rptSIM.Items
                If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For
                Dim Slot As UC_SIM = Item.FindControl("SIM")
                Result.Add(Slot)
            Next
            Return Result
        End Get
    End Property

    Public ReadOnly Property SIM_Height As Double
        Get
            Return ParentDispenser.SIM_Height
        End Get
    End Property

    Public ReadOnly Property KO_ID As Integer
        Get
            Return ParentDispenser.KO_ID
        End Get
    End Property

    Public Property DEVICE_ID As Integer
        Get
            Return aContainer.Attributes("DEVICE_ID")
        End Get
        Set(value As Integer)
            aContainer.Attributes("DEVICE_ID") = value
        End Set
    End Property

    Public Property SLOT_ID As Integer
        Get
            Return lblID.Text
        End Get
        Set(value As Integer)
            lblID.Text = value
        End Set
    End Property

    Public Property SIM_ID As Integer
        Get
            Return pnlProfile.Attributes("SIM_ID")
        End Get
        Set(value As Integer)
            pnlProfile.Attributes("SIM_ID") = value
            imgSIM.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=SIM_PACKAGE&UID=" & value & "&LANG=1&DI=images/WhiteDot.png"
        End Set
    End Property

    Public Property SIM_CODE As String
        Get
            Return lblSIMCode.Text
        End Get
        Set(value As String)
            lblSIMCode.Text = value
        End Set
    End Property

    Public Property SIM_PRICE As String
        Get
            Return lblPrice.Text
        End Get
        Set(value As String)
            lblPrice.Text = value
        End Set
    End Property

    Public Property ShowSIMProfile As Boolean
        Get
            Return pnlProfile.Visible
        End Get
        Set(value As Boolean)
            pnlProfile.Visible = value
        End Set
    End Property

    Public Property ShowPointer As Boolean
        Get
            Return pnlPointer.Visible
        End Get
        Set(value As Boolean)
            pnlPointer.Visible = value
        End Set
    End Property

    Public Property PointerColor As Drawing.Color
        Get
            Return lblQuanity.ForeColor
        End Get
        Set(value As Drawing.Color)
            lblQuanity.ForeColor = value
        End Set
    End Property

    Public ReadOnly Property SIM_QUANTITY As Integer
        Get
            Try
                Return Val(lblQuanity.Text.Replace("items", "").Replace("item", "").Replace(" ", ""))
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Public Sub UpdateSIMQuantity()
        Dim QTY As Integer = rptSIM.Items.Count
        If QTY = 0 Then
            lblQuanity.Text = "< empty"
        ElseIf QTY = 1 Then
            lblQuanity.Text = "< 1 item"
        Else
            lblQuanity.Text = "<" & QTY & " items"
        End If
        pnlEmpty.Visible = QTY = 0
        pnlProfile.Visible = QTY > 0
        '---------------- Update Pointer Position ------------
        pnlPointer.Style("bottom") = ((QTY * SIM_Height) - 12) & "px"
    End Sub

    Public Property MAX_CAPACITY As Integer
        Get
            Return lblMaxQuantity.Text
        End Get
        Set(value As Integer)
            lblMaxQuantity.Text = value
            pnlSlot.Height = Unit.Pixel(value * SIM_Height)
        End Set
    End Property

    Public Property HighLight As UC_Product_Slot.HighLightMode
        Get
            Select Case True
                Case aContainer.CssClass.IndexOf("highlightYellow") > -1
                    Return UC_Product_Slot.HighLightMode.YellowDotted
                Case aContainer.CssClass.IndexOf("highlightGreen") > -1
                    Return UC_Product_Slot.HighLightMode.GreenSolid
                Case aContainer.CssClass.IndexOf("highlightRed") > -1
                    Return UC_Product_Slot.HighLightMode.RedSolid
                Case Else
                    Return UC_Product_Slot.HighLightMode.None
            End Select
        End Get
        Set(value As UC_Product_Slot.HighLightMode)
            aContainer.CssClass = RemoveTagCssClass(aContainer.CssClass, "highlightYellow")
            aContainer.CssClass = RemoveTagCssClass(aContainer.CssClass, "highlightGreen")
            aContainer.CssClass = RemoveTagCssClass(aContainer.CssClass, "highlightRed")
            Select Case value
                Case UC_Product_Slot.HighLightMode.YellowDotted
                    aContainer.CssClass &= " highlightYellow"
                Case UC_Product_Slot.HighLightMode.GreenSolid
                    aContainer.CssClass &= " highlightGreen"
                Case UC_Product_Slot.HighLightMode.RedSolid
                    aContainer.CssClass &= " highlightRed"
                Case UC_Product_Slot.HighLightMode.None
                    '-------- Donothing --------------
            End Select
        End Set
    End Property

    Public Sub ClearAllSIM()
        rptSIM.DataSource = Nothing
        rptSIM.DataBind()
    End Sub

    Public Sub RemoveSIM(ByVal SIMIndex As Integer)
        Dim DT As DataTable = SIMSDatas()
        DT.Rows.RemoveAt(SIMIndex)
        rptSIM.DataSource = DT
        rptSIM.DataBind()
    End Sub

    Public Sub RemoveSIM(ByVal SERIAL_NO As String)
        Dim DT As DataTable = SIMSDatas()
        SIMSDatas.DefaultView.RowFilter = "SERIAL_NO<>'" & SERIAL_NO.Replace("'", "''") & "'"
        DT = DT.DefaultView.ToTable
        rptSIM.DataSource = DT
        rptSIM.DataBind()
    End Sub

    Private Function SIMSDatas() As DataTable
        Dim DT As New DataTable
        DT.Columns.Add("SERIAL_NO", GetType(String))
        DT.Columns.Add("DISPLAY_COLOR", GetType(Drawing.Color))
        For Each Item In rptSIM.Items
            If Item.ItemType <> ListItemType.Item And Item.ItemType <> ListItemType.AlternatingItem Then Continue For

            Dim SIM As UC_SIM = Item.FindControl("SIM")
            Dim DR As DataRow = DT.NewRow
            DR("SERIAL_NO") = SIM.SERIAL_NO
            DR("DISPLAY_COLOR") = SIM.DISPLAY_COLOR
            DT.Rows.Add(DR)
        Next
        Return DT
    End Function

    Private Sub rptSIM_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptSIM.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim SIM As UC_SIM = e.Item.FindControl("SIM")
        With SIM
            .SERIAL_NO = e.Item.DataItem("SERIAL_NO")
            .DISPLAY_COLOR = e.Item.DataItem("DISPLAY_COLOR")
            .Update_Display()
        End With
        UpdateSIMQuantity()
    End Sub

    Public Sub AddSIM(ByVal SERIAL_NO As String, ByVal DISPLAY_COLOR As Drawing.Color)
        Dim DT As DataTable = SIMSDatas()
        Dim DR As DataRow = DT.NewRow
        DR("SERIAL_NO") = SERIAL_NO
        DR("DISPLAY_COLOR") = DISPLAY_COLOR
        DT.Rows.Add(DR)
        rptSIM.DataSource = DT
        rptSIM.DataBind()
    End Sub

    Public Sub AddSIM(ByVal SIMDataRow As DataRow)
        AddSIM(SIMDataRow("SERIAL_NO").ToString, SIMDataRow("DISPLAY_COLOR"))
    End Sub

    Public Sub AddSIM(ByVal SIMDataTable As DataTable)
        For i As Integer = 0 To SIMDataTable.Rows.Count - 1
            AddSIM(SIMDataTable.Rows(i))
        Next
    End Sub

#Region "Event"

    Public Event Selecting(ByRef Sender As UC_SIM_Slot)

    Private Sub aContainer_Click(sender As Object, e As EventArgs) Handles aContainer.Click
        RaiseEvent Selecting(Me)
    End Sub

#End Region

End Class