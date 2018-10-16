
Imports System.Data.SqlClient

Public Class Payment_Gateway_Success
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL

    Private ReadOnly Property REQ_ID As Integer
        Get
            If Not IsNothing(Request.QueryString("REQ_ID")) Then
                Return Val(Request.QueryString("REQ_ID"))
            Else
                Return 0
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If REQ_ID <> 0 Then
            '-------------------- Save RESP Log ----------------------
            Dim SQL As String = "SELECT TOP 1 * FROM BBL_PAYMENT_RESP WHERE REQ_ID=" & REQ_ID
            Dim DA As New SqlDataAdapter(SQL, BL.LogConnectionString)
            Dim DT As New DataTable
            DA.Fill(DT)
            Dim DR As DataRow
            If DT.Rows.Count = 0 Then
                DR = DT.NewRow
                DR("REQ_ID") = REQ_ID
                DT.Rows.Add(DR)
            Else
                DR = DT.Rows(0)
            End If
            DR("paymentStatus") = "cancel"
            DR("RESP_param") = Request.QueryString.ToString
            DR("RESP_Time") = Now
            Dim cmd As New SqlCommandBuilder(DA)
            DA.Update(DT)
        End If

        '---------------- Close Parent ----------------

    End Sub

End Class