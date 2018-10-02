Public Class FrontUI_Default
    Inherits System.Web.UI.Page

    Private ReadOnly Property KO_ID As Integer
        Get
            Try
                Return Request.QueryString("KO_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    '-------------------- หน้า Portal เอาไว้เรียกเปิดโปรแกรมจาก Localhost ----------------
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If KO_ID = 0 Then
            Response.End()
        Else
            Response.Cookies("KO_ID").Value = KO_ID
            Response.Cookies("KO_ID").Expires = DateAdd(DateInterval.Year, 1, Now)
            Response.Redirect("Select_Language.aspx")
        End If
    End Sub

End Class