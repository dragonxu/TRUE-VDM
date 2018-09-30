Public Class Arm
    Inherits System.Web.UI.Page

    Private ReadOnly Property POS_ID
        Get
            Try
                Return Request.QueryString("POS_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim DT As New DataTable
        DT.Columns.Add("status", GetType(Boolean))
        DT.Columns.Add("message", GetType(String))
        Dim DR As DataRow = DT.NewRow
        DT.Rows.Add(DR)
        '--------------- Call API -------------
        Try

            DR("status") = True
            DR("message") = "success"
        Catch ex As Exception
            DR("status") = False
            DR("message") = ex.Message
        End Try
        Response.Write(SingleRowDataTableToJSON(DT))
    End Sub

End Class