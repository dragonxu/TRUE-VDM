Public Class UC_SIM
    Inherits System.Web.UI.UserControl

    Dim BL As New VDM_BL

    Public ReadOnly Property ItemIndex As Integer
        Get
            Return ParentSlot.SIMS.IndexOf(Me)
        End Get
    End Property

    Public ReadOnly Property SIM_Height As Double
        Get
            Return ParentSlot.SIM_Height
        End Get
    End Property

    Public Property SERIAL_NO As String
        Get
            Return lblSERIAL.Text
        End Get
        Set(value As String)
            lblSERIAL.Text = value
            pnlSIM.ToolTip = value
        End Set
    End Property

    Public Property DISPLAY_COLOR As Drawing.Color
        Get
            Try
                Return pnlSIM.BackColor
            Catch ex As Exception
                Return Drawing.Color.Empty
            End Try
        End Get
        Set(value As Drawing.Color)
            pnlSIM.BackColor = value
        End Set
    End Property

    Public ReadOnly Property ParentSlot As UC_SIM_Slot
        Get
            Try
                Return Me.Parent.Parent.Parent.Parent.Parent
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property

    Public Sub Update_Display()
        pnlSIM.Style.Item("bottom") = (ItemIndex * SIM_Height) & "px"
        pnlSIM.Height = Unit.Pixel(SIM_Height)
        DISPLAY_COLOR = DISPLAY_COLOR
    End Sub

    Public ReadOnly Property SIM_ID As Integer
        Get
            Return ParentSlot.SIM_ID
        End Get
    End Property

    Public ReadOnly Property SIM_CODE As String
        Get
            Return ParentSlot.SIM_CODE
        End Get
    End Property

    Public ReadOnly Property SIM_PRICE As String
        Get
            Return ParentSlot.SIM_PRICE
        End Get
    End Property

End Class