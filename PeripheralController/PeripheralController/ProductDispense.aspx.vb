Public Class ProductDispense
    Inherits System.Web.UI.Page

    Dim BL As New Core_BL
    Dim Result As DataTable

    Private ReadOnly Property POS_ID As Integer
        Get
            Return Request.QueryString("POS_ID")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Result = New DataTable
        Result.Columns.Add("amount", GetType(Integer))
        Result.Columns.Add("status", GetType(Boolean))
        Result.Columns.Add("message", GetType(String))

        '-------------- Hardcode For Success ---------------
        Dim DR As DataRow = Result.NewRow
        DR("status") = True
        DR("message") = "success"
        Result.Rows.Add(DR)
        Response.Write(SingleRowDataTableToJSON(Result))
        Response.End()
    End Sub

End Class