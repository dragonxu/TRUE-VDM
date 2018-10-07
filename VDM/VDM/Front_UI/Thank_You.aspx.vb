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


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            PrintSlip()
            Change()
        End If
        '---------------- Change And Print Slip--------------

        'BL.Update_Service_Transaction(TXN_ID, Me.Page) '-------------- Update ทุกหน้า ------------
        'If Not IsPostBack Then
        '    PrintSlipAndChange()
        'End If

    End Sub

    Private Sub PrintSlip()

    End Sub


    Private Sub Change()
        Dim SQL As String = ""
        SQL &= " SELECT ISNULL(MUST_CHANGE,0)-ISNULL(ACTUAL_CHANGE,0) REMAIN_CHANGE" & vbLf
        Sql &= " FROM TB_TRANSACTION_CASH TXN_CASH" & vbLf
        Sql &= " LEFT JOIN TB_TRANSACTION_CASH_CHANGE ACTUAL_CHANGE ON TXN_CASH.TXN_ID=ACTUAL_CHANGE.TXN_ID" & vbLf
        SQL &= " WHERE TXN_CASH.TXN_ID=" & TXN_ID & " AND" & vbLf
        SQL &= " ISNULL(MUST_CHANGE,0)-ISNULL(ACTUAL_CHANGE,0)>0" & vbLf
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        '------------- ไม่ต้องทอน ----------
        If DT.Rows.Count = 0 Then Exit Sub

        '------------- ต้องทอน ------------
        Dim MUST_CHANGE As Integer = DT.Rows(0)(0)

        '------------ CHECK MONEY_STOCK---------------
        SQL = "SELECT KD.D_ID,D.Unit_Value,KD.Current_Qty" & vbLf
        SQL &= "FROM TB_KIOSK_DEVICE KD " & vbLf
        SQL &= "INNER JOIN MS_DEVICE D ON KD.D_ID=D.D_ID" & vbLf
        SQL &= "		AND D.Active_Status=1" & vbLf
        SQL &= "WHERE KD.KO_ID=" & KO_ID & " AND D.DT_ID IN (" & VDM_BL.DeviceType.CoinOut & "," & VDM_BL.DeviceType.CashOut & ")" & vbLf
        SQL &= "And KD.Current_Qty > 0" & vbLf
        SQL &= "ORDER BY D.Unit_Value DESC"
        DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim MONEY_STOCK As New DataTable
        DA.Fill(MONEY_STOCK)

        Dim TMP As DataTable = BL.Calculate_Change_Quantity(MUST_CHANGE, MONEY_STOCK.Copy)

        Dim col() As String = {"Unit_Value"}
        Dim PAYLIST As DataTable = TMP.DefaultView.ToTable(True, col)
        PAYLIST.Columns.Add("Current_Qty", GetType(Integer))
        PAYLIST.Columns.Add("Total", GetType(Integer), "Unit_Value*Current_Qty")
        For i As Integer = 0 To PAYLIST.Rows.Count - 1
            PAYLIST.Rows(i).Item("Current_Qty") = TMP.Compute("SUM(Current_Qty)", "Unit_Value=" & PAYLIST.Rows(i).Item("Unit_Value"))
        Next
        PAYLIST.DefaultView.RowFilter = ""


        Dim ACTUAL_CHANGE As Object = PAYLIST.Compute("SUM(Total)", "")
        If IsDBNull(ACTUAL_CHANGE) Then ACTUAL_CHANGE = 0
        Dim REMAIN_CHANGE As Integer = MUST_CHANGE - ACTUAL_CHANGE

        '------------- Call Hardware -------------
        For i As Integer = 0 To PAYLIST.Rows.Count - 1
            Dim Script As String = ""
            Dim Unit_Value As Integer = PAYLIST.Rows(i).Item("Unit_Value")
            Dim Current_Qty As Integer = PAYLIST.Rows(i).Item("Current_Qty")
            Select Case Unit_Value
                Case 20, 50, 100, 500, 1000
                    Script = "cashDispense(" & Unit_Value & "," & Current_Qty & ");"
                Case 1, 2, 5, 10
                    Script = "coinDispense(" & Unit_Value & "," & Current_Qty & ");"
            End Select
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Dispense" & Unit_Value, Script, True)
        Next

        '---------------------TB_TRANSACTION_CASH_CHANGE------ Completed Transaction----------------------
        SQL = "SELECT * FROM TB_TRANSACTION_CASH_CHANGE" & vbLf
        SQL &= "WHERE TXN_ID=" & TXN_ID
        DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        DT = New DataTable
        DA.Fill(DT)

        Dim DR As DataRow
        If DT.Rows.Count = 0 Then
            DR = DT.NewRow
            DR("TXN_ID") = TXN_ID
            DT.Rows.Add(DR)
        Else
            DR = DT.Rows(0)
        End If
        DR("ACTUAL_CHANGE") = ACTUAL_CHANGE
        DR("REMAIN_CHANGE") = REMAIN_CHANGE
        DR("TXN_TIME") = Now
        Dim cmd As New SqlCommandBuilder(DA)
        DA.Update(DT)

        '-------------------UPDATE MONEY STOCK----------------
        For i As Integer = 0 To PAYLIST.Rows.Count - 1
            MONEY_STOCK.DefaultView.RowFilter = "Unit_Value=" & PAYLIST.Rows(i).Item("Unit_Value")
            If MONEY_STOCK.DefaultView.Count > 0 Then
                BL.UPDATE_KIOSK_DEVICE_TRANSACTION_STOCK(KO_ID, TXN_ID, MONEY_STOCK.DefaultView(0).Item("D_ID"), PAYLIST.Rows(i).Item("Current_Qty"))
            End If
        Next
        '------------------- Completed------------------------

    End Sub

    Private Sub PrintSlipAndChange()

        ''--------Check Existing TXN-----------
        'Dim SQL As String = "Select * FROM TB_SERVICE_TRANSACTION WHERE TXN_ID=" & TXN_ID
        'Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        'Dim DT As New DataTable
        'DA.Fill(DT)

        'If DT.Rows.Count = 0 Then Exit Sub


        'Dim METHOD_ID As VDM_BL.PaymentMethod = DT.Rows(0).Item("METHOD_ID")

        'If METHOD_ID = VDM_BL.PaymentMethod.CASH Then
        '    ChangeIfRemain()
        'End If

        'Dim C As New Converter
        'Dim Slip As DataTable = C.XMLToDatatable(DT.Rows(0).Item("SLIP_CONTENT".ToString))

        ''----------- Print ----------
        'Dim Printer As New Printer.Print
        'Printer.CallPrintAsync(Slip)

        ''------------ Update Printer Stock -----------
        'BL.UPDATE_KIOSK_DEVICE_STOCK(KO_ID, TXN_ID, VDM_BL.Device.Printer, -1)


        'Dim DR As DataRow = Nothing
        ''TB_TRANSACTION_STOCK
        'Dim BEFORE_QUANTITY As Object = DBNull.Value
        'SQL = "Select Current_Qty FROM TB_KIOSK_DEVICE WHERE KO_ID=" & KO_ID & " And D_ID=" & VDM_BL.Device.Printer
        'DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        'DT = New DataTable
        'DA.Fill(DT)

        'SQL = "Select * FROM TB_TRANSACTION_STOCK WHERE TXN_ID=" & TXN_ID & " And D_ID=" & VDM_BL.DeviceType.Printer
        'DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        'DA.Fill(DT)
        'DR("TXN_ID") = TXN_ID
        'DR("D_ID") = VDM_BL.DeviceType.Printer
        'DR("Unit_Value") = DBNull.Value
        'DR("BEFORE_QUANTITY") = BEFORE_QUANTITY
        'DR("DIFF_QUANTITY") = 1

        ''TB_KIOSK_DEVICE

    End Sub

    'Private Sub ChangeIfRemain()

    '    '------------ Check Remain Change ----------
    '    Dim SQL As String = ""
    '    SQL &= " Select PD.TOTAL_PRICE,CASH_PAID,ISNULL(CASH_CHANGE,0) CASH_CHANGE" & vbLf
    '    SQL &= "FROM TB_SERVICE_TRANSACTION TXN " & vbLf
    '    SQL &= "LEFT JOIN TB_BUY_PRODUCT PD On TXN.TXN_ID=PD.TXN_ID" & vbLf

    '    SQL &= "WHERE TXN.TXN_ID=" & TXN_ID & vbLf
    '    SQL &= "And METHOD_ID=" & VDM_BL.PaymentMethod.CASH & vbLf
    '    SQL &= "And CASH_PAID-ISNULL(CASH_CHANGE,0)<>PD.TOTAL_PRICE" & vbLf
    '    Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
    '    Dim DT As New DataTable
    '    DA.Fill(DT)
    '    If DT.Rows.Count = 0 Then
    '        SQL = "UPDATE TB_SERVICE_TRANSACTION Set CASH_CHANGE=0 WHERE TXN_ID=" & TXN_ID
    '        BL.ExecuteNonQuery(SQL)
    '        Exit Sub
    '    End If

    '    Dim Remain As Integer = DT.Rows(0).Item("CASH_PAID") - DT.Rows(0).Item("TOTAL_PRICE")

    '    '------------ Call hardware ---------------
    '    SQL &= "Select KD.D_ID,D.Unit_Value,KD.Current_Qty" & vbLf
    '    SQL &= "FROM TB_KIOSK_DEVICE KD " & vbLf
    '    SQL &= "INNER JOIN MS_DEVICE D On KD.D_ID=D.D_ID" & vbLf
    '    SQL &= "		And D.Active_Status=1" & vbLf
    '    SQL &= "WHERE KD.KO_ID=" & KO_ID & " And D.DT_ID In (" & VDM_BL.DeviceType.CoinOut & "," & VDM_BL.DeviceType.CashOut & ")  And KD.Current_Qty>0" & vbLf
    '    SQL &= "ORDER BY D.Unit_Value DESC"
    '    DA = New SqlDataAdapter(SQL, BL.ConnectionString)
    '    DT = New DataTable
    '    DA.Fill(DT)
    '    Dim TB_KIOSK_DEVICE As DataTable = DT.Copy

    '    Dim TMP As New DataTable
    '    TMP.Columns.Add("Unit_Value", GetType(Integer))
    '    TMP.Columns.Add("Current_Qty", GetType(Integer))

    '    While Remain > 0 And DT.Rows.Count > 0
    '        Dim CashToPay As Integer = NextToPay(Remain, DT.Copy)
    '        If CashToPay = 0 Then
    '            Exit While
    '        Else
    '            TMP.Rows.Add(CashToPay, 1)
    '            Remain -= CashToPay
    '            DT.DefaultView.RowFilter = "Unit_Value=" & CashToPay
    '            DT.DefaultView(0).Item("Current_Qty") -= 1
    '            If DT.DefaultView(0).Item("Current_Qty") = 0 Then
    '                DT.DefaultView(0).Row.Delete()
    '            End If
    '        End If
    '    End While
    '    '----------- Distinct Result ------------
    '    Dim col() As String = {"Unit_Value"}
    '    TMP.DefaultView.RowFilter = ""
    '    Dim Result As DataTable = TMP.DefaultView.ToTable(True, col)
    '    Result.Columns.Add("Current_Qty", GetType(Integer))
    '    For i As Integer = 0 To Result.Rows.Count - 1
    '        Result.Rows(i).Item("Current_Qty") = TMP.Compute("COUNT(Current_Qty)", "Unit_Value=" & Result.Rows(i).Item("Unit_Value"))
    '    Next
    '    '------------ Update Money Stock -----------
    '    For i As Integer = 0 To Result.Rows.Count - 1
    '        Dim Unit_Value As Integer = Result.Rows(i).Item("Unit_Value")
    '        Dim Current_Qty As Integer = Result.Rows(i).Item("Current_Qty")
    '        Dim Script As String = ""
    '        Select Case Unit_Value
    '            Case 20, 50, 100, 500, 1000
    '                Script = "cashDispense(" & Unit_Value & "," & Current_Qty & ");"
    '            Case 1, 2, 5, 10
    '                Script = "coinDispense(" & Unit_Value & "," & Current_Qty & ");"
    '        End Select
    '        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Dispense" & Unit_Value, Script, True)

    '        '------------- ตัด Stock Money --------------------
    '        TB_KIOSK_DEVICE.DefaultView.RowFilter = "Unit_Value=" & Unit_Value
    '        Dim D_ID As Integer = TB_KIOSK_DEVICE.DefaultView(0).Item("D_ID")
    '        TB_KIOSK_DEVICE.DefaultView.RowFilter = "'"
    '        BL.UPDATE_KIOSK_DEVICE_STOCK(KO_ID, TXN_ID, D_ID, -Current_Qty)
    '    Next
    '    '-------------- Update Already Change-----------
    '    Result.Columns.Add("Total", GetType(Integer), "Unit_Value*Current_Qty")
    '    Dim Total As Object = Result.Compute("SUM(Total)", "")
    '    If Not IsDBNull(Total) Then
    '        SQL = "UPDATE TB_SERVICE_TRANSACTION SET CASH_CHANGE=" & Total & " WHERE TXN_ID=" & TXN_ID
    '        BL.ExecuteNonQuery(SQL)
    '    Else
    '        SQL = "UPDATE TB_SERVICE_TRANSACTION SET CASH_CHANGE=0 WHERE TXN_ID=" & TXN_ID
    '        BL.ExecuteNonQuery(SQL)
    '    End If
    '    '------------- Check If Not Enough Change----------
    '    PrintSlipNotEnoughMoney(Total, Remain)
    'End Sub


    'Private Sub PrintSlipNotEnoughMoney(ByVal Change As Integer, ByVal Remain As Integer)

    '    Dim SQL As String = "SELECT * FROM TB_SERVICE_TRANSACTION WHERE TXN_ID=" & TXN_ID
    '    Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
    '    Dim DT As New DataTable
    '    DA.Fill(DT)

    '    Dim DeleteCount As Integer = 3
    '    For i As Integer = DT.Rows.Count - 1 To 0 Step -1
    '        DT.Rows(i).Delete()
    '        DeleteCount -= 1
    '        If DeleteCount = 0 Then Exit For
    '    Next

    '    Dim C As New Converter
    '    Dim Content As DataTable = C.XMLToDatatable(DT.Rows(0).Item("SLIP_CONTENT").ToString)
    '    DT.Columns.Add("Text", GetType(String))
    '    DT.Columns.Add("ImagePath", GetType(String))
    '    DT.Columns.Add("FontSize", GetType(Single))
    '    DT.Columns.Add("FontName", GetType(String))
    '    DT.Columns.Add("Bold", GetType(Boolean))
    '    DT.Columns.Add("IsColor", GetType(Boolean))
    '    DT.Columns.Add("ContentType", GetType(VDM_BL.PrintContentType))

    '    Dim DR As DataRow = Content.NewRow
    '    DT.Rows.Add("***** CHANGE REMAIN : " & FormatNumber(Remain, 2) & " *********")
    '    DT.Rows.Add(" ")
    '    DT.Rows.Add("ขอบคุณที่ใช้บริการ")
    '    '----------------- Ads ----------------

    '    '----------------- Ads ----------------
    '    DT.Rows.Add("__________________________________________")

    '    '--------------- Update Print Content -----------
    '    BL.Set_Default_Print_Content_Style(DT)
    '    DT.Rows(0).Item("SLIP_CONTENT") = C.DatatableToXML(DT)
    '    Dim cmd As New SqlCommandBuilder(DA)
    '    DA.Update(DT)

    '    Dim Printer As New Printer.Print
    '    Printer.CallPrintAsync(DT)
    'End Sub

    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Select_Language.aspx")
    End Sub



End Class