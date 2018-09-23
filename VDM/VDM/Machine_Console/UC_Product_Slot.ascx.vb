Public Class UC_Product_Slot
    Inherits System.Web.UI.UserControl

    Const MarginWidth As Integer = 2 ' Padding 10x2 + border 2x2
    Const MarginHeight As Integer = 2 ' Padding 10x2 + border 2x2

    Dim BL As New VDM_BL

    Public Enum HighLightMode
        None = 0
        YellowDotted = 1
        GreenSolid = 2
        RedSolid = 3
    End Enum

    Public ReadOnly Property PixelPerMM As Double
        Get
            Return ParentFloor.PixelPerMM
        End Get
    End Property

    Public ReadOnly Property ParentFloor As UC_Product_Floor
        Get
            Try
                Return Me.Parent.Parent.Parent.Parent
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property

    '--------------- Calculate MM------------
    Public Property SLOT_WIDTH As Integer
        Get
            Return lblWidth.Text
        End Get
        Set(value As Integer)
            Slot.Width = Unit.Pixel((value * PixelPerMM) - MarginWidth)
            lblWidth.Text = value
            pnlCoor.Style("left") = ((value * PixelPerMM) - MarginWidth) & "px"
            '------------- Update Relate Scale -----------
            lblXT.Text = POS_X + value
        End Set
    End Property

    Public Property SLOT_HEIGHT As Integer
        Get
            Return lblHeight.Text
        End Get
        Set(value As Integer)
            lblHeight.Text = value
            '------------- Update Relate Scale -----------
            lblYT.Text = POS_Y + value
        End Set
    End Property

    Public ReadOnly Property FLOOR_HEIGHT As Integer
        Get
            Return ParentFloor.FLOOR_HEIGHT
        End Get
    End Property

    Public Property POS_X As Integer
        Get
            Return lblXB.Text
        End Get
        Set(value As Integer)
            Slot.Style.Item("right") = (value * PixelPerMM) & "px"
            lblXB.Text = value
            '------------- Update Relate Scale -----------
            lblXT.Text = value + SLOT_WIDTH
        End Set
    End Property

    Public Property POS_Y As Integer
        Get
            Return lblYB.Text
        End Get
        Set(value As Integer)
            lblYB.Text = value
            '------------- Update Relate Scale -----------
            lblYT.Text = value + SLOT_HEIGHT
        End Set
    End Property

    Public Property SLOT_ID As Integer
        Get
            Return Slot.Attributes("SLOT_ID")
        End Get
        Set(value As Integer)
            Slot.Attributes("SLOT_ID") = value
        End Set
    End Property

    Public Property HighLight As HighLightMode
        Get
            Select Case True
                Case Slot.CssClass.IndexOf("highlightYellow") > -1
                    Return HighLightMode.YellowDotted
                Case Slot.CssClass.IndexOf("highlightGreen") > -1
                    Return HighLightMode.GreenSolid
                Case Slot.CssClass.IndexOf("highlightRed") > -1
                    Return HighLightMode.RedSolid
                Case Else
                    Return HighLightMode.None
            End Select
        End Get
        Set(value As HighLightMode)
            Slot.CssClass = RemoveTagCssClass(Slot.CssClass, "highlightYellow")
            Slot.CssClass = RemoveTagCssClass(Slot.CssClass, "highlightGreen")
            Slot.CssClass = RemoveTagCssClass(Slot.CssClass, "highlightRed")
            Select Case value
                Case HighLightMode.YellowDotted
                    Slot.CssClass &= " highlightYellow"
                Case HighLightMode.GreenSolid
                    Slot.CssClass &= " highlightGreen"
                Case HighLightMode.RedSolid
                    Slot.CssClass &= " highlightRed"
                Case HighLightMode.None
                    '-------- Donothing --------------
            End Select
        End Set
    End Property

    Public Property SLOT_NAME As String
        Get
            Return lblName.Text
        End Get
        Set(value As String)
            lblName.Text = value
        End Set
    End Property

    Public Property PRODUCT_CODE As String
        Get
            Return lblCode.Text
        End Get
        Set(value As String)
            lblCode.Text = value
        End Set
    End Property

    Public Property ShowScale As Boolean
        Get
            Return pnlScale.Visible
        End Get
        Set(value As Boolean)
            pnlScale.Visible = value
        End Set
    End Property

    Public Property ShowMask As Boolean
        Get
            Return mask.Visible
        End Get
        Set(value As Boolean)
            mask.Visible = value
        End Set
    End Property

    Public Property MaskContent As String
        Get
            Return mask_content.InnerHtml
        End Get
        Set(value As String)
            mask_content.InnerHtml = value
        End Set
    End Property



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Slot.Attributes("onClick") = "document.getElementById('" & btnSelect.ClientID & "').click();"
    End Sub

#Region "Product"
    Public Property PRODUCT_LEVEL_PERCENT As String
        Get
            Return QuantityLevel.Style("height")
        End Get
        Set(value As String)
            QuantityLevel.Style("height") = value
        End Set
    End Property

    Public Property PRODUCT_QUANTITY As Integer
        Get
            Return Val(lblQuantity.Text.Replace("x", "").Replace("X", "").Replace(" ", ""))
        End Get
        Set(value As Integer)
            lblQuantity.Text = "X " & value
        End Set
    End Property

    Public Property PRODUCT_LEVEL_COLOR As Drawing.Color
        Get
            Return QuantityLevel.BackColor
        End Get
        Set(value As Drawing.Color)
            QuantityLevel.BackColor = value
            lblQuantity.ForeColor = value
        End Set
    End Property

    Public Property QUANTITY_BAR_COLOR As Drawing.Color
        Get
            Return QuantityBar.BackColor
        End Get
        Set(value As Drawing.Color)
            QuantityBar.BackColor = value
        End Set
    End Property


    Public Property PRODUCT_ID As Integer
        Get
            Return QuantityBar.Attributes("PRODUCT_ID")
        End Get
        Set(value As Integer)
            If value <> 0 Then
                Slot.Style("background-image") = "url('../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & value & "&LANG=1')"
            Else
                Slot.Style.Remove("background-image")
            End If
            QuantityBar.Attributes("PRODUCT_ID") = value
        End Set
    End Property

    Public Property ShowQuantity As Boolean
        Get
            Return QuantityBar.Visible
        End Get
        Set(value As Boolean)
            lblQuantity.Visible = value
            QuantityBar.Visible = value
        End Set
    End Property

#End Region

#Region "Event"
    Public Event Selecting(ByRef Sender As UC_Product_Slot)

    Private Sub Slot_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        RaiseEvent Selecting(Me)
    End Sub
#End Region

End Class