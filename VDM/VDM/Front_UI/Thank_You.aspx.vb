﻿Imports System.Data
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
            'Response.Cookies("KO_ID").Value = 1
            'Session("TXN_ID") = 83 ' Test True Money
            'Session("TXN_ID") = 901 ' Test Cash
            'Session("TXN_ID") = 196 ' CreditCard
            DT_CONTROL = UI_CONTROL()
            Bind_CONTROL()
            txtLocalControllerURL.Text = BL.LocalControllerURL
            '---------------- Change And Print Slip--------------
            Change()
            Print()
        End If

    End Sub

    Private Sub Print()

        Dim C As New Converter

        Dim SQL As String = "SELECT * FROM TB_SERVICE_TRANSACTION" & vbLf
        SQL &= "WHERE TXN_ID=" & TXN_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        If DT.Rows.Count = 0 OrElse IsDBNull(DT.Rows(0).Item("METHOD_ID")) Then Exit Sub

        Dim Script As String = "printSlip(" & TXN_ID & ");"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Print", Script, True)
    End Sub

    Private Sub Change()
        Dim SQL As String = ""
        SQL &= " SELECT ISNULL(MUST_CHANGE,0)-ISNULL(ACTUAL_CHANGE,0) REMAIN_CHANGE" & vbLf
        SQL &= " FROM TB_TRANSACTION_CASH TXN_CASH" & vbLf
        SQL &= " LEFT JOIN TB_TRANSACTION_CASH_CHANGE ACTUAL_CHANGE ON TXN_CASH.TXN_ID=ACTUAL_CHANGE.TXN_ID" & vbLf
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

    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Select_Language.aspx")
    End Sub

#Region "UI"
    Private ReadOnly Property UI_CONTROL As DataTable  '------------- เอาไว้ดึงข้อมูล UI ----------
        Get
            Try
                Return BL.GET_MS_UI_LANGUAGE(LANGUAGE)
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property
    Dim DT_CONTROL As DataTable
    Public Sub Bind_CONTROL()
        On Error Resume Next
        If LANGUAGE > VDM_BL.UILanguage.TH Then
            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_ThankYou.Text & "'"
            lblUI_ThankYou.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_ThankYou.Text)

        End If

        If LANGUAGE > VDM_BL.UILanguage.EN Then
            lblUI_ThankYou.CssClass = "UI"
        End If
    End Sub

#End Region

End Class