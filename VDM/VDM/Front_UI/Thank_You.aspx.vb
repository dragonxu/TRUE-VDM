Imports System.Data
Imports System.Data.SqlClient

Public Class Thank_You
    Inherits System.Web.UI.Page


#Region "ส่วนที่เหมือนกันหมดทุกหน้า"
    Dim BL As New VDM_BL

    Private ReadOnly Property KO_ID As Integer '------------- เอาไว้เรียกใช้ง่ายๆ ----------
        Get
            Try
                Return Request.Cookies("KO_ID").Value
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Private Property LANGUAGE As VDM_BL.UILanguage '-------- หน้าอื่นๆต้องเป็น ReadOnly --------
        Get
            Return Session("LANGUAGE")
        End Get
        Set(value As VDM_BL.UILanguage)
            Session("LANGUAGE") = value
        End Set
    End Property

    Public ReadOnly Property TXN_ID As Integer
        Get
            Return Session("TXN_ID")
        End Get
    End Property

#End Region

    Public ReadOnly Property PRODUCT_ID As Integer
        Get
            Return CInt(Request.QueryString("PRODUCT_ID"))
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        BL.Update_Service_Transaction(TXN_ID, Me.Page) '-------------- Update ทุกหน้า ------------
        If Not IsPostBack Then
            PrintSlipAndChange()
        End If

    End Sub

    Private Sub PrintSlipAndChange()
        Dim SQL As String = "SELECT * FROM TB_SERVICE_TRANSACTION WHERE TXN_ID=" & TXN_ID
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        Dim C As New Converter
        Dim Slip As DataTable = C.XMLToDatatable(DT.Rows(0).Item("SLIP_CONTENT".ToString))

        '----------- Print ----------
        Dim Printer As New Printer.Print
        Printer.CallPrintAsync(Slip)

        '------------ Update Printer Stock -----------
        BL.UPDATE_KIOSK_DEVICE_STOCK(KO_ID, TXN_ID, VDM_BL.Device.Printer, -1)
        Dim DR As DataRow = Nothing
        'TB_TRANSACTION_STOCK
        Dim BEFORE_QUANTITY As Object = DBNull.Value
        SQL = "SELECT Current_Qty FROM TB_KIOSK_DEVICE WHERE KO_ID=" & KO_ID & " AND D_ID=" & VDM_BL.Device.Printer
        DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        DT = New DataTable
        DA.Fill(DT)

        SQL = "SELECT * FROM TB_TRANSACTION_STOCK WHERE TXN_ID=" & TXN_ID & " AND D_ID=" & VDM_BL.DeviceType.Printer
        DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        DR("TXN_ID") = TXN_ID
        DR("D_ID") = VDM_BL.DeviceType.Printer
        DR("Unit_Value") = DBNull.Value
        DR("BEFORE_QUANTITY")=BEFORE_QUANTITY
        DR("DIFF_QUANTITY") = 1

        'TB_KIOSK_DEVICE
        If DT.Rows(0).Item("METHOD_ID") = VDM_BL.PaymentMethod.CASH AndAlso Not IsDBNull(DT.Rows(0).Item("CASH_CHANGE")) AndAlso DT.Rows(0).Item("CASH_CHANGE") > 0 Then
            Change(DT.Rows(0).Item("CASH_CHANGE"))
        End If
    End Sub

    Private Sub Change(ByVal Amount As Integer)

        '------------ Call hardware ------------

        '------------ Update Money Stock -----------
        'TB_TRANSACTION_STOCK
        'TB_KIOSK_DEVICE
    End Sub

    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Select_Language.aspx")
    End Sub



End Class