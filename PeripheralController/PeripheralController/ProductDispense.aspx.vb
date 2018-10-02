Public Class ProductDispense
    Inherits System.Web.UI.Page

    Private ReadOnly Property POS_ID As Integer
        Get
            Return Request.QueryString("POS_ID")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class