Imports VDM
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationManager

Public Class BBLPaymentGateway

    Public ServerRoot As String = AppSettings("ServerRoot").ToString

    Public BBL_MerchantID As String = AppSettings("BBL_MerchantID").ToString
    Public BBL_CurrentCode As String = AppSettings("BBL_CurrentCode").ToString
    Public BBL_PostPaymentURL As String = AppSettings("BBL_PostPaymentURL").ToString

    Dim BL As New VDM_BL

    Public Function Generate_OrderRef() As String ' Confirm จาก True

        Dim YY As String = Now.ToString("yy")
        Dim MM As String = Now.ToString("MM")
        Dim DD As String = Now.ToString("dd")

        Dim SQL As String = "SELECT ISNULL(MAX(CAST(RIGHT(orderRef,3) AS INT)),0)+1" & vbLf
        SQL &= " FROM BBL_Payment_REQ " & vbLf
        SQL &= " WHERE orderRef LIKE 'TRUE-VDM-" & YY & MM & DD & "%'"
        Dim DA As New SqlDataAdapter(SQL, BL.LogConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        Return "TRUE-VDM-" & YY & MM & DD & DT.Rows(0)(0).ToString.PadLeft(3, "0")

    End Function

End Class
