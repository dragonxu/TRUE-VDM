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

        '--------Check Existing TXN-----------
        Dim SQL As String = "SELECT * FROM TB_SERVICE_TRANSACTION WHERE TXN_ID=" & TXN_ID
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        If DT.Rows.Count = 0 Then Exit Sub


        Dim METHOD_ID As VDM_BL.PaymentMethod = DT.Rows(0).Item("METHOD_ID")

        If METHOD_ID = VDM_BL.PaymentMethod.CASH Then
            ChangeIfRemain()
        End If

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
        DR("BEFORE_QUANTITY") = BEFORE_QUANTITY
        DR("DIFF_QUANTITY") = 1

        'TB_KIOSK_DEVICE

    End Sub

    Private Sub ChangeIfRemain()

        '------------ Check Remain Change ----------
        Dim SQL As String = ""
        SQL &= " SELECT PD.TOTAL_PRICE,CASH_PAID,ISNULL(CASH_CHANGE,0) CASH_CHANGE" & vbLf
        SQL &= "FROM TB_SERVICE_TRANSACTION TXN " & vbLf
        SQL &= "LEFT JOIN TB_BUY_PRODUCT PD ON TXN.TXN_ID=PD.TXN_ID" & vbLf

        SQL &= "WHERE TXN.TXN_ID=" & TXN_ID & vbLf
        SQL &= "AND METHOD_ID=" & VDM_BL.PaymentMethod.CASH & vbLf
        SQL &= "AND CASH_PAID-ISNULL(CASH_CHANGE,0)<>PD.TOTAL_PRICE" & vbLf
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        If DT.Rows.Count = 0 Then
            SQL = "UPDATE TB_SERVICE_TRANSACTION SET CASH_CHANGE=0 WHERE TXN_ID=" & TXN_ID
            BL.ExecuteNonQuery(SQL)
            Exit Sub
        End If

        Dim Remain As Integer = DT.Rows(0).Item("CASH_PAID") - DT.Rows(0).Item("TOTAL_PRICE")

        '------------ Call hardware ---------------
        SQL &= "SELECT KD.D_ID,D.Unit_Value,KD.Current_Qty" & vbLf
        SQL &= "FROM TB_KIOSK_DEVICE KD " & vbLf
        SQL &= "INNER JOIN MS_DEVICE D ON KD.D_ID=D.D_ID" & vbLf
        SQL &= "		AND D.Active_Status=1" & vbLf
        SQL &= "WHERE KD.KO_ID=" & KO_ID & " AND D.DT_ID IN (" & VDM_BL.DeviceType.CoinOut & "," & VDM_BL.DeviceType.CashOut & ")  AND KD.Current_Qty>0" & vbLf
        SQL &= "ORDER BY D.Unit_Value DESC"
        DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        DT = New DataTable
        DA.Fill(DT)
        Dim TB_KIOSK_DEVICE As DataTable = DT.Copy

        Dim TMP As New DataTable
        TMP.Columns.Add("Unit_Value", GetType(Integer))
        TMP.Columns.Add("Current_Qty", GetType(Integer))

        While Remain > 0 And DT.Rows.Count > 0
            Dim CashToPay As Integer = NextToPay(Remain, DT.Copy)
            If CashToPay = 0 Then
                Exit While
            Else
                TMP.Rows.Add(CashToPay, 1)
                Remain -= CashToPay
                DT.DefaultView.RowFilter = "Unit_Value=" & CashToPay
                DT.DefaultView(0).Item("Current_Qty") -= 1
                If DT.DefaultView(0).Item("Current_Qty") = 0 Then
                    DT.DefaultView(0).Row.Delete()
                End If
            End If
        End While
        '----------- Distinct Result ------------
        Dim col() As String = {"Unit_Value"}
        TMP.DefaultView.RowFilter = ""
        Dim Result As DataTable = TMP.DefaultView.ToTable(True, col)
        Result.Columns.Add("Current_Qty", GetType(Integer))
        For i As Integer = 0 To Result.Rows.Count - 1
            Result.Rows(i).Item("Current_Qty") = TMP.Compute("COUNT(Current_Qty)", "Unit_Value=" & Result.Rows(i).Item("Unit_Value"))
        Next
        '------------ Update Money Stock -----------
        For i As Integer = 0 To Result.Rows.Count - 1
            Dim Unit_Value As Integer = Result.Rows(i).Item("Unit_Value")
            Dim Current_Qty As Integer = Result.Rows(i).Item("Current_Qty")
            Dim Script As String = ""
            Select Case Unit_Value
                Case 20, 50, 100, 500, 1000
                    Script = "cashDispense(" & Unit_Value & "," & Current_Qty & ");"
                Case 1, 2, 5, 10
                    Script = "coinDispense(" & Unit_Value & "," & Current_Qty & ");"
            End Select
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Dispense" & Unit_Value, Script, True)

            '------------- ตัด Stock Money --------------------
            TB_KIOSK_DEVICE.DefaultView.RowFilter = "Unit_Value=" & Unit_Value
            Dim D_ID As Integer = TB_KIOSK_DEVICE.DefaultView(0).Item("D_ID")
            TB_KIOSK_DEVICE.DefaultView.RowFilter = "'"
            BL.UPDATE_KIOSK_DEVICE_STOCK(KO_ID, TXN_ID, D_ID, -Current_Qty)
        Next
        '-------------- Update Already Change-----------
        Result.Columns.Add("Total", GetType(Integer), "Unit_Value*Current_Qty")
        Dim Total As Object = Result.Compute("SUM(Total)", "")
        If Not IsDBNull(Total) Then
            SQL = "UPDATE TB_SERVICE_TRANSACTION SET CASH_CHANGE=" & Total & " WHERE TXN_ID=" & TXN_ID
            BL.ExecuteNonQuery(SQL)
        Else
            SQL = "UPDATE TB_SERVICE_TRANSACTION SET CASH_CHANGE=0 WHERE TXN_ID=" & TXN_ID
            BL.ExecuteNonQuery(SQL)
        End If
        '------------- Check If Not Enough Change----------
        PrintSlipNotEnoughMoney(Total, Remain)
    End Sub

    Private Function NextToPay(ByVal Amount As Integer, ByVal MoneyStock As DataTable) As Integer
        Dim Result As Object = DBNull.Value
        Result = MoneyStock.Compute("MAX(Unit_Value)", "Unit_Value<=" & Amount)
        If Not IsDBNull(Result) Then
            Return Result
        Else
            Return 0
        End If
        Return Result
    End Function

    Private Sub PrintSlipNotEnoughMoney(ByVal Change As Integer, ByVal Remain As Integer)

        Dim SQL As String = "SELECT * FROM TB_SERVICE_TRANSACTION WHERE TXN_ID=" & TXN_ID
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        Dim DeleteCount As Integer = 3
        For i As Integer = DT.Rows.Count - 1 To 0 Step -1
            DT.Rows(i).Delete()
            DeleteCount -= 1
            If DeleteCount = 0 Then Exit For
        Next

        Dim C As New Converter
        Dim Content As DataTable = C.XMLToDatatable(DT.Rows(0).Item("SLIP_CONTENT").ToString)
        DT.Columns.Add("Text", GetType(String))
        DT.Columns.Add("ImagePath", GetType(String))
        DT.Columns.Add("FontSize", GetType(Single))
        DT.Columns.Add("FontName", GetType(String))
        DT.Columns.Add("Bold", GetType(Boolean))
        DT.Columns.Add("IsColor", GetType(Boolean))
        DT.Columns.Add("ContentType", GetType(VDM_BL.PrintContentType))

        Dim DR As DataRow = Content.NewRow
        DT.Rows.Add("***** CHANGE REMAIN : " & FormatNumber(Remain, 2) & " *********")
        DT.Rows.Add(" ")
        DT.Rows.Add("ขอบคุณที่ใช้บริการ")
        '----------------- Ads ----------------

        '----------------- Ads ----------------
        DT.Rows.Add("__________________________________________")

        '--------------- Update Print Content -----------
        BL.Set_Default_Print_Content_Style(DT)
        DT.Rows(0).Item("SLIP_CONTENT") = C.DatatableToXML(DT)
        Dim cmd As New SqlCommandBuilder(DA)
        DA.Update(DT)

        Dim Printer As New Printer.Print
        Printer.CallPrintAsync(DT)
    End Sub

    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Select_Language.aspx")
    End Sub



End Class