Imports System.Data.SqlClient

Public Class UC_CommonUI
    Inherits System.Web.UI.UserControl

    Dim BL As New VDM_BL

    Public ReadOnly Property KO_ID
        Get
            Try
                Return Request.Cookies("KO_ID").Value
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Public ReadOnly Property TXN_ID As Integer
        Get
            Try
                Return Session("TXN_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Exit Sub

        If KO_ID = 0 Then
            Response.Redirect("../Machine_Console/Login.aspx")
            Response.End()
        End If

        If TXN_ID > 0 Then
            BL.Update_Service_Transaction(TXN_ID, Me.Page) '-------------- Update ทุกหน้า ------------
        End If
        '---------------- Check Shift Open ----------------
        Dim DT As New DataTable
        Dim SQL As String = "EXEC dbo.SP_CURRENT_OPEN_SHIFT " & KO_ID
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        If DT.Rows.Count = 0 Then
            '------------- Shift ยังไม่เปิด -------------
            Response.Redirect("../Machine_Console/Login.aspx?KO_ID=" & KO_ID)
            Response.End()
        End If

    End Sub

End Class