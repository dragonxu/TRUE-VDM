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

        Dim BBL_ORDER_Y As String = Now.ToString("yy")
        Dim BBL_ORDER_M As String = Now.ToString("MM")
        Dim BBL_ORDER_D As String = Now.ToString("dd")

        Dim SQL As String = "SELECT ISNULL(MAX(CAST(BBL_ORDER_N AS INT)),0)+1 " & vbLf
        SQL &= "FROM TB_TRANSACTION_CREDITCARD WHERE BBL_ORDER_Y='" & BBL_ORDER_Y & "' AND BBL_ORDER_M='" & BBL_ORDER_M & "' AND BBL_ORDER_D='" & BBL_ORDER_D & "'"
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        Dim BBL_ORDER_N As String = DT.Rows(0).Item(0).ToString.PadLeft(3, "0")
        Dim REQ_ID As Integer = BL.Get_NewID_Log("BBL_Payment_REQ", REQ_ID)

        Return "TRUE-VDM-" & BBL_ORDER_Y & BBL_ORDER_M & BBL_ORDER_D & BBL_ORDER_N

    End Function

End Class
