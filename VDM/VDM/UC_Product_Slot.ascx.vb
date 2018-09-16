Public Class UC_Product_Slot
    Inherits System.Web.UI.UserControl

    Dim BL As New VDM_BL

    Public Property PixelPerMM As Double
        Get
            Return Slot.Attributes("PixelPerMM")
        End Get
        Set(value As Double)
            Slot.Attributes("PixelPerMM") = value
        End Set
    End Property

    Public ReadOnly Property ParentFloor As UC_Product_Floor
        Get
            Try
                Return Me.Parent.Parent.Parent
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property

    '--------------- Calculate MM------------
    Public Property SLOT_WIDTH As Integer
        Get
            'Return Slot.Width.Value / PixelPerMM
            Return Slot.Attributes("SLOT_WIDTH")
        End Get
        Set(value As Integer)
            Slot.Width = Unit.Pixel(value * PixelPerMM)
            Slot.Attributes("SLOT_WIDTH") = value
        End Set
    End Property

    Public ReadOnly Property FLOOR_HEIGHT As Integer
        Get
            Return ParentFloor.FLOOR_HEIGHT
        End Get
    End Property

    Public Property POS_X As Integer
        Get
            'Return CInt(Slot.Style.Item("left").Replace("px", "") / PixelPerMM)
            Return Slot.Attributes("POS_X")
        End Get
        Set(value As Integer)
            Slot.Style.Item("left") = (value * PixelPerMM) & "px"
            Slot.Attributes("POS_X") = value
        End Set
    End Property

    Public ReadOnly Property POS_Y As Integer
        Get
            Return ParentFloor.POS_Y
        End Get
    End Property

    Public Property SLOT_ID As Integer
        Get
            Return Slot.Attributes("SLOT_ID")
        End Get
        Set(value As Integer)
            Slot.Attributes("SLOT_ID") = value
        End Set
    End Property

    Public Property IsSelected As Boolean
        Get
            Return Slot.CssClass = "machine_slot selected"
        End Get
        Set(value As Boolean)
            If value Then
                Slot.CssClass = "machine_slot selected"
            Else
                Slot.CssClass = "machine_slot"
            End If
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub



    Public Property IsViewOnly As Boolean
        Get
            Return Slot.Enabled
        End Get
        Set(value As Boolean)
            Slot.Enabled = value
        End Set
    End Property

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
            lblQuantity.BackColor = value
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
                QuantityBar.Visible = True
                Slot.Style("background-image") = "url('../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & value & "&LANG=1')"
            Else
                QuantityBar.Visible = False
                Slot.Style("background-image") = ""
            End If
            QuantityBar.Attributes("PRODUCT_ID") = value
        End Set
    End Property

#End Region

#Region "Event"
    Public Event RequestEdit(ByVal Sender As UC_Product_Slot)

    Private Sub Slot_Click(sender As Object, e As EventArgs) Handles Slot.Click
        RaiseEvent RequestEdit(Me)
    End Sub
#End Region

End Class