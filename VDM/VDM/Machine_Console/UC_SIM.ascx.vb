Public Class UC_SIM
    Inherits System.Web.UI.UserControl

    Dim BL As New VDM_BL

    Public Property SERIAL_NO As String
        Get
            Return lblSERIAL.Text
        End Get
        Set(value As String)
            lblSERIAL.Text = value
        End Set
    End Property

    Public Property ItemIndex As Integer
        Get
            Return pnlSIM.Style.Item("bottom").Replace("px", "")
        End Get
        Set(value As Integer)

        End Set
    End Property

End Class