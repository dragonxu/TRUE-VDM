Imports System.Data.SqlClient
Public Class FrontUI_Default
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL

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
            Response.Write("Invalid Kiosk Info")
            Response.End()
            Exit Sub
        End If

        Response.Cookies("KO_ID").Value = KO_ID
        Response.Cookies("KO_ID").Expires = DateAdd(DateInterval.Year, 1, Now)

        '-------------- Get Shift Info-------------
        Dim SQL As String = "EXEC dbo.SP_CURRENT_OPEN_SHIFT " & KO_ID
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        If DT.Rows.Count = 0 Then
            Response.Redirect("../Machine_Console/Login.aspx?KO_ID=" & KO_ID)
        Else

            SQL = "SELECT * FROM MS_KIOSK_POSTER WHERE KO_ID=" & KO_ID
            DA = New SqlDataAdapter(SQL, BL.ConnectionString)
            DT = New DataTable
            DA.Fill(DT)
            If DT.Rows.Count > 0 Then
                Response.Cookies("POSTER_TYPE").Value = DT.Rows(0).Item("POSTER_TYPE").ToString

            Else
                Response.Cookies("POSTER_TYPE").Value = DT.Rows(0).Item("POSTER_TYPE").ToString

            End If


            Response.Redirect("Select_Language.aspx")
        End If


    End Sub

End Class