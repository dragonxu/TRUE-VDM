Imports System.Data
Imports System.Data.SqlClient

Public Class UC_Shift_Recieve
    Inherits System.Web.UI.UserControl

    Dim BL As New VDM_BL

#Region "Property"
    Public ReadOnly Property Total() As Integer
        Get
            Return FormatNumber(Val(lblSum.Text.Replace(",", "")), 0)
        End Get
    End Property

    Public ReadOnly Property Remain_coin() As Integer
        Get
            Return FormatNumber(coin1_Remain + coin5_Remain, 0)
        End Get
    End Property

    Public ReadOnly Property Remain_cash() As Integer
        Get
            Return FormatNumber(cash20_Remain + cash100_Remain + cash500_Remain + cash1000_Remain, 0)
        End Get
    End Property

    'Coin1
    Public ReadOnly Property coin1_Before() As Integer
        Get
            Return FormatNumber(Val(txt_coin1_Before.Text.Replace(",", "")), 0)
        End Get
    End Property
    Public ReadOnly Property coin1_Pick() As Integer
        Get
            Return FormatNumber(Val(txt_coin1_Pick.Text.Replace(",", "")), 0)
        End Get
    End Property
    Public ReadOnly Property coin1_Input() As Integer
        Get
            Return FormatNumber(Val(txt_coin1_Input.Text.Replace(",", "")), 0)
        End Get
    End Property
    Public ReadOnly Property coin1_Remain() As Integer
        Get
            Return FormatNumber(Val(txt_coin1_Remain.Text.Replace(",", "")), 0)
        End Get
    End Property


    'coin5
    Public ReadOnly Property coin5_Before() As Integer
        Get
            Return FormatNumber(Val(txt_coin5_Before.Text.Replace(",", "")), 0)
        End Get
    End Property
    Public ReadOnly Property coin5_Pick() As Integer
        Get
            Return FormatNumber(Val(txt_coin5_Pick.Text.Replace(",", "")), 0)
        End Get
    End Property
    Public ReadOnly Property coin5_Input() As Integer
        Get
            Return FormatNumber(Val(txt_coin5_Input.Text.Replace(",", "")), 0)
        End Get
    End Property
    Public ReadOnly Property coin5_Remain() As Integer
        Get
            Return FormatNumber(Val(txt_coin5_Remain.Text.Replace(",", "")), 0)
        End Get
    End Property

    'cash20
    Public ReadOnly Property cash20_Before() As Integer
        Get
            Return FormatNumber(Val(txt_cash20_Before.Text.Replace(",", "")), 0)
        End Get
    End Property
    Public ReadOnly Property cash20_Pick() As Integer
        Get
            Return FormatNumber(Val(txt_cash20_Pick.Text.Replace(",", "")), 0)
        End Get
    End Property
    Public ReadOnly Property cash20_Input() As Integer
        Get
            Return FormatNumber(Val(txt_cash20_Input.Text.Replace(",", "")), 0)
        End Get
    End Property
    Public ReadOnly Property cash20_Remain() As Integer
        Get
            Return FormatNumber(Val(txt_cash20_Remain.Text.Replace(",", "")), 0)
        End Get
    End Property

    'cash100
    Public ReadOnly Property cash100_Before() As Integer
        Get
            Return FormatNumber(Val(txt_cash100_Before.Text.Replace(",", "")), 0)
        End Get
    End Property
    Public ReadOnly Property cash100_Pick() As Integer
        Get
            Return FormatNumber(Val(txt_cash100_Pick.Text.Replace(",", "")), 0)
        End Get
    End Property
    Public ReadOnly Property cash100_Input() As Integer
        Get
            Return FormatNumber(Val(txt_cash100_Input.Text.Replace(",", "")), 0)
        End Get
    End Property
    Public ReadOnly Property cash100_Remain() As Integer
        Get
            Return FormatNumber(Val(txt_cash100_Remain.Text.Replace(",", "")), 0)
        End Get
    End Property

    'cash500
    Public ReadOnly Property cash500_Before() As Integer
        Get
            Return FormatNumber(Val(txt_cash500_Before.Text.Replace(",", "")), 0)
        End Get
    End Property
    Public ReadOnly Property cash500_Pick() As Integer
        Get
            Return FormatNumber(Val(txt_cash500_Pick.Text.Replace(",", "")), 0)
        End Get
    End Property
    Public ReadOnly Property cash500_Input() As Integer
        Get
            Return FormatNumber(Val(txt_cash500_Input.Text.Replace(",", "")), 0)
        End Get
    End Property
    Public ReadOnly Property cash500_Remain() As Integer
        Get
            Return FormatNumber(Val(txt_cash500_Remain.Text.Replace(",", "")), 0)
        End Get
    End Property

    'cash1000
    Public ReadOnly Property cash1000_Before() As Integer
        Get
            Return FormatNumber(Val(txt_cash1000_Before.Text.Replace(",", "")), 0)
        End Get
    End Property
    Public ReadOnly Property cash1000_Pick() As Integer
        Get
            Return FormatNumber(Val(txt_cash1000_Pick.Text.Replace(",", "")), 0)
        End Get
    End Property
    Public ReadOnly Property cash1000_Input() As Integer
        Get
            Return FormatNumber(Val(txt_cash1000_Input.Text.Replace(",", "")), 0)
        End Get
    End Property
    Public ReadOnly Property cash1000_Remain() As Integer
        Get
            Return FormatNumber(Val(txt_cash1000_Remain.Text.Replace(",", "")), 0)
        End Get
    End Property
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not IsNumeric(Session("USER_ID")) Then
        '    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('กรุณาเข้าสู่ระบบ'); window.location.href='Login.aspx';", True)
        '    Exit Sub
        'End If

        If Not IsPostBack Then
            ClearEditForm()
            SetTextbox()

        Else
            initFormPlugin()
        End If
    End Sub

    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Private Sub ClearEditForm()
        '--CoinIn
        'Coin1
        txt_coin1_Before.Text = ""
        txt_coin1_Pick.Text = ""
        txt_coin1_Input.Text = ""
        txt_coin1_Remain.Text = ""
        lbl_coin1_Amount.Text = ""
        'Coin5
        txt_coin5_Before.Text = ""
        txt_coin5_Pick.Text = ""
        txt_coin5_Input.Text = ""
        txt_coin5_Remain.Text = ""
        lbl_coin5_Amount.Text = ""


        '--CashIn
        'Cash20 
        txt_cash20_Before.Text = ""
        txt_cash20_Pick.Text = ""
        txt_cash20_Input.Text = ""
        txt_cash20_Remain.Text = ""
        lbl_cash20_Amount.Text = ""
        'Cash100
        txt_cash100_Before.Text = ""
        txt_cash100_Pick.Text = ""
        txt_cash100_Input.Text = ""
        txt_cash100_Remain.Text = ""
        lbl_cash100_Amount.Text = ""
        'Cash500 
        txt_cash500_Before.Text = ""
        txt_cash500_Pick.Text = ""
        txt_cash500_Input.Text = ""
        txt_cash500_Remain.Text = ""
        lbl_cash500_Amount.Text = ""
        'Cash1000
        txt_cash1000_Before.Text = ""
        txt_cash1000_Pick.Text = ""
        txt_cash1000_Input.Text = ""
        txt_cash1000_Remain.Text = ""
        lbl_cash1000_Amount.Text = ""

    End Sub

    Public Sub SetTextbox()
        'Coin1
        ModuleGlobal.ImplementJavaNumericText(txt_coin1_Before, "Center")
        ModuleGlobal.ImplementJavaNumericText(txt_coin1_Pick, "Center")
        ModuleGlobal.ImplementJavaNumericText(txt_coin1_Input, "Center")
        ModuleGlobal.ImplementJavaNumericText(txt_coin1_Remain, "Center")
        'Coin5
        ModuleGlobal.ImplementJavaNumericText(txt_coin5_Before, "Center")
        ModuleGlobal.ImplementJavaNumericText(txt_coin5_Pick, "Center")
        ModuleGlobal.ImplementJavaNumericText(txt_coin5_Input, "Center")
        ModuleGlobal.ImplementJavaNumericText(txt_coin5_Remain, "Center")
        'Cash20 
        ModuleGlobal.ImplementJavaNumericText(txt_cash20_Before, "Center")
        ModuleGlobal.ImplementJavaNumericText(txt_cash20_Pick, "Center")
        ModuleGlobal.ImplementJavaNumericText(txt_cash20_Input, "Center")
        ModuleGlobal.ImplementJavaNumericText(txt_cash20_Remain, "Center")
        'Cash100
        ModuleGlobal.ImplementJavaNumericText(txt_cash100_Before, "Center")
        ModuleGlobal.ImplementJavaNumericText(txt_cash100_Pick, "Center")
        ModuleGlobal.ImplementJavaNumericText(txt_cash100_Input, "Center")
        ModuleGlobal.ImplementJavaNumericText(txt_cash100_Remain, "Center")
        'Cash500
        ModuleGlobal.ImplementJavaNumericText(txt_cash500_Before, "Center")
        ModuleGlobal.ImplementJavaNumericText(txt_cash500_Pick, "Center")
        ModuleGlobal.ImplementJavaNumericText(txt_cash500_Input, "Center")
        ModuleGlobal.ImplementJavaNumericText(txt_cash500_Remain, "Center")
        'Cash1000
        ModuleGlobal.ImplementJavaNumericText(txt_cash1000_Before, "Center")
        ModuleGlobal.ImplementJavaNumericText(txt_cash1000_Pick, "Center")
        ModuleGlobal.ImplementJavaNumericText(txt_cash1000_Input, "Center")
        ModuleGlobal.ImplementJavaNumericText(txt_cash1000_Remain, "Center")
    End Sub

    Public Sub CurrentData()
        '--Current 
        Dim SQL As String = " SELECT * FROM TB_KIOSK_DEVICE WHERE KO_ID=" & BL.KioskID
        SQL &= " AND D_ID IN (" & VDM_BL.Device.Coin1 & "," & VDM_BL.Device.CoinIn & "," & VDM_BL.Device.CashIn & ")"
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            DT.DefaultView.RowFilter = "D_ID=" & VDM_BL.Device.CoinIn '& " AND Unit_Value=1"
            If DT.DefaultView.Count > 0 Then
                txt_coin1_Before.Text = FormatNumber(Val(DT.DefaultView(0).Item("Current_Qty")), 0)
            End If
            DT.DefaultView.RowFilter = "D_ID=" & VDM_BL.Device.CoinIn '& " AND Unit_Value=5"
            If DT.DefaultView.Count > 0 Then
                txt_coin5_Before.Text = FormatNumber(Val(DT.DefaultView(0).Item("Current_Qty")), 0)
            End If
            DT.DefaultView.RowFilter = "D_ID=" & VDM_BL.Device.CashIn '& " AND Unit_Value=20"
            If DT.DefaultView.Count > 0 Then
                txt_cash20_Before.Text = FormatNumber(Val(DT.DefaultView(0).Item("Current_Qty")), 0)
            End If
            DT.DefaultView.RowFilter = "D_ID=" & VDM_BL.Device.CashIn '& " AND Unit_Value=100"
            If DT.DefaultView.Count > 0 Then
                txt_cash100_Before.Text = FormatNumber(Val(DT.DefaultView(0).Item("Current_Qty")), 0)
            End If

            'DT.DefaultView.RowFilter = "D_ID=" & VDM_BL.Device.CashIn & " AND Unit_Value=500"
            'If DT.DefaultView.Count > 0 Then
            '    txt_cash500_Before.Text = FormatNumber(Val(DT.DefaultView(0).Item("Current_Qty")), 0)
            'End If
            'DT.DefaultView.RowFilter = "D_ID=" & VDM_BL.Device.CashIn & " AND Unit_Value=1000"
            'If DT.DefaultView.Count > 0 Then
            '    txt_cash1000_Before.Text = FormatNumber(Val(DT.DefaultView(0).Item("Current_Qty")), 0)
            'End If
        End If

    End Sub


#Region "Calculate"
    Public Sub Calculate_coin1()
        'คงเหลือ
        txt_coin1_Remain.Text = FormatNumber((Val(txt_coin1_Before.Text.Replace(",", "")) - Val(txt_coin1_Pick.Text.Replace(",", ""))) + Val(txt_coin1_Input.Text.Replace(",", "")), 0)
        'จำนวนเงิน
        lbl_coin1_Amount.Text = FormatNumber(Val(txt_coin1_Remain.Text.Replace(",", "")), 0)
    End Sub

    Public Sub Calculate_coin5()
        'คงเหลือ
        txt_coin5_Remain.Text = FormatNumber((Val(txt_coin5_Before.Text.Replace(",", "")) - Val(txt_coin5_Pick.Text.Replace(",", ""))) + Val(txt_coin5_Input.Text.Replace(",", "")), 0)
        'จำนวนเงิน
        lbl_coin5_Amount.Text = FormatNumber(Val(txt_coin5_Remain.Text.Replace(",", "")) * 5, 0)
    End Sub

    Public Sub Calculate_cash20()
        'คงเหลือ
        txt_cash20_Remain.Text = FormatNumber((Val(txt_cash20_Before.Text.Replace(",", "")) - Val(txt_cash20_Pick.Text.Replace(",", ""))) + Val(txt_cash20_Input.Text.Replace(",", "")), 0)
        'จำนวนเงิน
        lbl_cash20_Amount.Text = FormatNumber(Val(txt_cash20_Remain.Text.Replace(",", "")) * 20, 0)
    End Sub

    Public Sub Calculate_cash100()
        'คงเหลือ
        txt_cash100_Remain.Text = FormatNumber((Val(txt_cash100_Before.Text.Replace(",", "")) - Val(txt_cash100_Pick.Text.Replace(",", ""))) + Val(txt_cash100_Input.Text.Replace(",", "")), 0)
        'จำนวนเงิน
        lbl_cash100_Amount.Text = FormatNumber(Val(txt_cash100_Remain.Text.Replace(",", "")) * 100, 0)
    End Sub

    Public Sub Calculate_cash500()
        'คงเหลือ
        txt_cash500_Remain.Text = FormatNumber((Val(txt_cash500_Before.Text.Replace(",", "")) - Val(txt_cash500_Pick.Text.Replace(",", ""))) + Val(txt_cash500_Input.Text.Replace(",", "")), 0)
        'จำนวนเงิน
        lbl_cash500_Amount.Text = FormatNumber(Val(txt_cash500_Remain.Text.Replace(",", "")) * 500, 0)
    End Sub

    Public Sub Calculate_cash1000()
        'คงเหลือ
        txt_cash1000_Remain.Text = FormatNumber((Val(txt_cash1000_Before.Text.Replace(",", "")) - Val(txt_cash1000_Pick.Text.Replace(",", ""))) + Val(txt_cash1000_Input.Text.Replace(",", "")), 0)
        'จำนวนเงิน
        lbl_cash1000_Amount.Text = FormatNumber(Val(txt_cash1000_Remain.Text.Replace(",", "")) * 1000, 0)
    End Sub

    Public Sub Calculate_Total()
        lblSum.Text = FormatNumber(Val(lbl_coin1_Amount.Text.Replace(",", "")) + Val(lbl_coin5_Amount.Text.Replace(",", "")) + Val(lbl_cash20_Amount.Text.Replace(",", "")) + Val(lbl_cash100_Amount.Text.Replace(",", "")) + Val(lbl_cash500_Amount.Text.Replace(",", "")) + Val(lbl_cash1000_Amount.Text.Replace(",", "")), 0)
    End Sub


#End Region

#Region "TextChanged"


    Private Sub txt_coin1_TextChanged(sender As Object, e As EventArgs) Handles txt_coin1_Before.TextChanged, txt_coin1_Pick.TextChanged, txt_coin1_Input.TextChanged
        Calculate_coin1()
        Calculate_Total()
    End Sub

    Private Sub txt_coin5_TextChanged(sender As Object, e As EventArgs) Handles txt_coin5_Before.TextChanged, txt_coin5_Pick.TextChanged, txt_coin5_Input.TextChanged
        Calculate_coin5()
        Calculate_Total()
    End Sub

    Private Sub txt_cash20_TextChanged(sender As Object, e As EventArgs) Handles txt_cash20_Before.TextChanged, txt_cash20_Pick.TextChanged, txt_cash20_Input.TextChanged
        Calculate_cash20()
        Calculate_Total()
    End Sub

    Private Sub txt_cash100_TextChanged(sender As Object, e As EventArgs) Handles txt_cash100_Before.TextChanged, txt_cash100_Pick.TextChanged, txt_cash100_Input.TextChanged
        Calculate_cash100()
        Calculate_Total()
    End Sub

    Private Sub txt_cash500_TextChanged(sender As Object, e As EventArgs) Handles txt_cash500_Before.TextChanged, txt_cash500_Pick.TextChanged, txt_cash500_Input.TextChanged
        Calculate_cash500()
        Calculate_Total()
    End Sub


    Private Sub txt_cash1000_TextChanged(sender As Object, e As EventArgs) Handles txt_cash1000_Before.TextChanged, txt_cash1000_Pick.TextChanged, txt_cash1000_Input.TextChanged
        Calculate_cash1000()
        Calculate_Total()
    End Sub

#End Region



#Region "btn Full"

    Function GetDeviceInfo(ByVal D_ID As Integer) As DataTable
        Dim SQL As String = " SELECT * FROM MS_DEVICE WHERE D_ID=" & Val(D_ID)
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

    'coin1
    Private Sub btn_coin1_Pick_Full_Click(sender As Object, e As EventArgs) Handles btn_coin1_Pick_Full.Click
        txt_coin1_Pick.Text = FormatNumber(Val(txt_coin1_Before.Text.Replace(",", "")), 0)
        txt_coin1_TextChanged(Nothing, Nothing)
    End Sub
    Private Sub btn_coin1_Input_Full_Click(sender As Object, e As EventArgs) Handles btn_coin1_Input_Full.Click
        Dim DT As DataTable = GetDeviceInfo(VDM_BL.Device.CoinIn)
        If DT.Rows.Count > 0 Then
            txt_coin1_Input.Text = FormatNumber(Val(DT.Rows(0).Item("Max_Qty")), 0)
            txt_coin1_TextChanged(Nothing, Nothing)
        Else
            Alert(Me.Page, "ตรวจสอบ Device coin1")
            Exit Sub
        End If
    End Sub

    'coin5
    Private Sub btn_coin5_Pick_Full_Click(sender As Object, e As EventArgs) Handles btn_coin5_Pick_Full.Click
        txt_coin5_Pick.Text = FormatNumber(Val(txt_coin5_Before.Text.Replace(",", "")), 0)
        txt_coin5_TextChanged(Nothing, Nothing)
    End Sub
    Private Sub btn_coin5_Input_Full_Click(sender As Object, e As EventArgs) Handles btn_coin5_Input_Full.Click
        Dim DT As DataTable = GetDeviceInfo(VDM_BL.Device.CoinIn)
        If DT.Rows.Count > 0 Then
            txt_coin5_Input.Text = FormatNumber(Val(DT.Rows(0).Item("Max_Qty")), 0)
            txt_coin5_TextChanged(Nothing, Nothing)
        Else
            Alert(Me.Page, "ตรวจสอบ Device coin5")
            Exit Sub
        End If
    End Sub

    'cash20
    Private Sub btn_cash20_Pick_Full_Click(sender As Object, e As EventArgs) Handles btn_cash20_Pick_Full.Click
        txt_cash20_Pick.Text = FormatNumber(Val(txt_cash20_Before.Text.Replace(",", "")), 0)
        txt_cash20_TextChanged(Nothing, Nothing)
    End Sub
    Private Sub btn_cash20_Input_Full_Click(sender As Object, e As EventArgs) Handles btn_cash20_Input_Full.Click
        Dim DT As DataTable = GetDeviceInfo(VDM_BL.Device.CashIn)
        If DT.Rows.Count > 0 Then
            txt_cash20_Input.Text = FormatNumber(Val(DT.Rows(0).Item("Max_Qty")), 0)
            txt_cash20_TextChanged(Nothing, Nothing)
        Else
            Alert(Me.Page, "ตรวจสอบ Device Cash20")
            Exit Sub
        End If
    End Sub

    'cash100
    Private Sub btn_cash100_Pick_Full_Click(sender As Object, e As EventArgs) Handles btn_cash100_Pick_Full.Click
        txt_cash100_Pick.Text = FormatNumber(Val(txt_cash100_Before.Text.Replace(",", "")), 0)
        txt_cash100_TextChanged(Nothing, Nothing)
    End Sub
    Private Sub btn_cash100_Input_Full_Click(sender As Object, e As EventArgs) Handles btn_cash100_Input_Full.Click
        Dim DT As DataTable = GetDeviceInfo(VDM_BL.Device.CashIn)
        If DT.Rows.Count > 0 Then
            txt_cash100_Input.Text = FormatNumber(Val(DT.Rows(0).Item("Max_Qty")), 0)
            txt_cash100_TextChanged(Nothing, Nothing)
        Else
            Alert(Me.Page, "ตรวจสอบ Device Cash100")
            Exit Sub
        End If
    End Sub

    'cash500
    Private Sub btn_cash500_Pick_Full_Click(sender As Object, e As EventArgs) Handles btn_cash500_Pick_Full.Click
        txt_cash500_Pick.Text = FormatNumber(Val(txt_cash500_Before.Text.Replace(",", "")), 0)
        txt_cash500_TextChanged(Nothing, Nothing)
    End Sub
    Private Sub btn_cash500_Input_Full_Click(sender As Object, e As EventArgs) Handles btn_cash500_Input_Full.Click
        Dim DT As DataTable = GetDeviceInfo(VDM_BL.Device.CashIn)
        If DT.Rows.Count > 0 Then
            txt_cash500_Input.Text = FormatNumber(Val(DT.Rows(0).Item("Max_Qty")), 0)
            txt_cash500_TextChanged(Nothing, Nothing)
        Else
            Alert(Me.Page, "ตรวจสอบ Device Cash500")
            Exit Sub
        End If
    End Sub
    'cash1000
    Private Sub btn_cash1000_Pick_Full_Click(sender As Object, e As EventArgs) Handles btn_cash1000_Pick_Full.Click
        txt_cash1000_Pick.Text = FormatNumber(Val(txt_cash1000_Before.Text.Replace(",", "")), 0)
        txt_cash1000_TextChanged(Nothing, Nothing)
    End Sub
    Private Sub btn_cash1000_Input_Full_Click(sender As Object, e As EventArgs) Handles btn_cash1000_Input_Full.Click
        Dim DT As DataTable = GetDeviceInfo(VDM_BL.Device.CashIn)
        If DT.Rows.Count > 0 Then
            txt_cash1000_Input.Text = FormatNumber(Val(DT.Rows(0).Item("Max_Qty")), 0)
            txt_cash1000_TextChanged(Nothing, Nothing)
        Else
            Alert(Me.Page, "ตรวจสอบ Device Cash1000")
            Exit Sub
        End If
    End Sub

#End Region

    Function Validate() As Boolean
        Dim result As Boolean = True
        If Val(txt_coin1_Before.Text) < Val(txt_coin1_Pick.Text) Then
            Alert(Me.Page, "ตรวจสอบจำนวนเงินเอาออก 1 บาท")
            result = False
        End If

        If Val(txt_coin5_Before.Text) < Val(txt_coin5_Pick.Text) Then
            Alert(Me.Page, "ตรวจสอบจำนวนเงินเอาออก 5 บาท")
            result = False
        End If

        If Val(txt_cash20_Before.Text) < Val(txt_cash20_Pick.Text) Then
            Alert(Me.Page, "ตรวจสอบจำนวนเงินเอาออก 20 บาท")
            result = False
        End If

        If Val(txt_cash100_Before.Text) < Val(txt_cash100_Pick.Text) Then
            Alert(Me.Page, "ตรวจสอบจำนวนเงินเอาออก 100 บาท")
            result = False
        End If

        If Val(txt_cash500_Before.Text) < Val(txt_cash500_Pick.Text) Then
            Alert(Me.Page, "ตรวจสอบจำนวนเงินเอาออก 500 บาท")
            result = False
        End If

        If Val(txt_cash1000_Before.Text) < Val(txt_cash1000_Pick.Text) Then
            Alert(Me.Page, "ตรวจสอบจำนวนเงินเอาออก 1000 บาท")
            result = False
        End If

        Return result
    End Function

    Function Save() As Boolean
        Dim result As Boolean = False
        Try
            'save Device
            'VDM_BL.Device.Coin1
            Dim SQL As String = "SELECT * FROM TB_SHIFT_STOCK "
            SQL &= " WHERE SHIFT_ID=" & Session("SHIFT_ID") & " AND D_ID=" & VDM_BL.Device.CoinIn & " AND Unit_Value=1"
            Dim DT As New DataTable
            Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
            DA.Fill(DT)

            Dim DR As DataRow
            If DT.Rows.Count = 0 Then
                DR = DT.NewRow
                DT.Rows.Add(DR)
                DR("SHIFT_ID") = Session("SHIFT_ID")
                DR("D_ID") = VDM_BL.Device.CoinIn
                DR("Unit_Value") = 1
            Else
                DR = DT.Rows(0)
            End If
            Select Case Session("SHIFT_Status")
                Case VDM_BL.ShiftStatus.Close
                    DR("CLOSE_BEFORE") = IIf(txt_coin1_Before.Text <> "", txt_coin1_Before.Text.Replace(",", ""), DBNull.Value)
                    DR("CLOSE_FINAL") = IIf(txt_coin1_Remain.Text <> "", txt_coin1_Remain.Text.Replace(",", ""), DBNull.Value)
                Case VDM_BL.ShiftStatus.Open
                    DR("OPEN_BEFORE") = IIf(txt_coin1_Before.Text <> "", txt_coin1_Before.Text.Replace(",", ""), DBNull.Value)
                    DR("OPEN_FINAL") = IIf(txt_coin1_Remain.Text <> "", txt_coin1_Remain.Text.Replace(",", ""), DBNull.Value)
            End Select
            Dim cmd As New SqlCommandBuilder(DA)
            DA.Update(DT)

            'VDM_BL.Device.Coin5
            SQL = "SELECT * FROM TB_SHIFT_STOCK "
            SQL &= " WHERE SHIFT_ID=" & Session("SHIFT_ID") & " AND D_ID=" & VDM_BL.Device.CoinIn & " AND Unit_Value=5"
            DT = New DataTable
            DA = New SqlDataAdapter(SQL, BL.ConnectionString)
            DA.Fill(DT)
            If DT.Rows.Count = 0 Then
                DR = DT.NewRow
                DT.Rows.Add(DR)
                DR("SHIFT_ID") = Session("SHIFT_ID")
                DR("D_ID") = VDM_BL.Device.CoinIn
                DR("Unit_Value") = 5
            Else
                DR = DT.Rows(0)
            End If
            Select Case Session("SHIFT_Status")
                Case VDM_BL.ShiftStatus.Close
                    DR("CLOSE_BEFORE") = IIf(txt_coin5_Before.Text <> "", txt_coin5_Before.Text.Replace(",", ""), DBNull.Value)
                    DR("CLOSE_FINAL") = IIf(txt_coin5_Remain.Text <> "", txt_coin5_Remain.Text.Replace(",", ""), DBNull.Value)
                Case VDM_BL.ShiftStatus.Open
                    DR("OPEN_BEFORE") = IIf(txt_coin5_Before.Text <> "", txt_coin5_Before.Text.Replace(",", ""), DBNull.Value)
                    DR("OPEN_FINAL") = IIf(txt_coin5_Remain.Text <> "", txt_coin5_Remain.Text.Replace(",", ""), DBNull.Value)
            End Select
            cmd = New SqlCommandBuilder(DA)
            DA.Update(DT)

            'VDM_BL.Device.Cash20
            SQL = "SELECT * FROM TB_SHIFT_STOCK "
            SQL &= " WHERE SHIFT_ID=" & Session("SHIFT_ID") & " AND D_ID=" & VDM_BL.Device.CashIn & " AND Unit_Value=20"
            DT = New DataTable
            DA = New SqlDataAdapter(SQL, BL.ConnectionString)
            DA.Fill(DT)
            If DT.Rows.Count = 0 Then
                DR = DT.NewRow
                DT.Rows.Add(DR)
                DR("SHIFT_ID") = Session("SHIFT_ID")
                DR("D_ID") = VDM_BL.Device.CashIn
                DR("Unit_Value") = 20
            Else
                DR = DT.Rows(0)
            End If
            Select Case Session("SHIFT_Status")
                Case VDM_BL.ShiftStatus.Close
                    DR("CLOSE_BEFORE") = IIf(txt_cash20_Before.Text <> "", txt_cash20_Before.Text.Replace(",", ""), DBNull.Value)
                    DR("CLOSE_FINAL") = IIf(txt_cash20_Remain.Text <> "", txt_cash20_Remain.Text.Replace(",", ""), DBNull.Value)
                Case VDM_BL.ShiftStatus.Open
                    DR("OPEN_BEFORE") = IIf(txt_cash20_Before.Text <> "", txt_cash20_Before.Text.Replace(",", ""), DBNull.Value)
                    DR("OPEN_FINAL") = IIf(txt_cash20_Remain.Text <> "", txt_cash20_Remain.Text.Replace(",", ""), DBNull.Value)
            End Select
            cmd = New SqlCommandBuilder(DA)
            DA.Update(DT)

            'VDM_BL.Device.Cash100
            SQL = "SELECT * FROM TB_SHIFT_STOCK "
            SQL &= " WHERE SHIFT_ID=" & Session("SHIFT_ID") & " AND D_ID=" & VDM_BL.Device.CashIn & " AND Unit_Value=100"
            DT = New DataTable
            DA = New SqlDataAdapter(SQL, BL.ConnectionString)
            DA.Fill(DT)
            If DT.Rows.Count = 0 Then
                DR = DT.NewRow
                DT.Rows.Add(DR)
                DR("SHIFT_ID") = Session("SHIFT_ID")
                DR("D_ID") = VDM_BL.Device.CashIn
                DR("Unit_Value") = 100
            Else
                DR = DT.Rows(0)
            End If
            Select Case Session("SHIFT_Status")
                Case VDM_BL.ShiftStatus.Close
                    DR("CLOSE_BEFORE") = IIf(txt_cash100_Before.Text <> "", txt_cash100_Before.Text.Replace(",", ""), DBNull.Value)
                    DR("CLOSE_FINAL") = IIf(txt_cash100_Remain.Text <> "", txt_cash100_Remain.Text.Replace(",", ""), DBNull.Value)
                Case VDM_BL.ShiftStatus.Open
                    DR("OPEN_BEFORE") = IIf(txt_cash100_Before.Text <> "", txt_cash100_Before.Text.Replace(",", ""), DBNull.Value)
                    DR("OPEN_FINAL") = IIf(txt_cash100_Remain.Text <> "", txt_cash100_Remain.Text.Replace(",", ""), DBNull.Value)
            End Select
            cmd = New SqlCommandBuilder(DA)
            DA.Update(DT)

            'VDM_BL.Device.Cash500
            SQL = "SELECT * FROM TB_SHIFT_STOCK "
            SQL &= " WHERE SHIFT_ID=" & Session("SHIFT_ID") & " AND D_ID=" & VDM_BL.Device.CashIn & " AND Unit_Value=500"
            DT = New DataTable
            DA = New SqlDataAdapter(SQL, BL.ConnectionString)
            DA.Fill(DT)
            If DT.Rows.Count = 0 Then
                DR = DT.NewRow
                DT.Rows.Add(DR)
                DR("SHIFT_ID") = Session("SHIFT_ID")
                DR("D_ID") = VDM_BL.Device.CashIn
                DR("Unit_Value") = 500
            Else
                DR = DT.Rows(0)
            End If
            Select Case Session("SHIFT_Status")
                Case VDM_BL.ShiftStatus.Close
                    DR("CLOSE_BEFORE") = IIf(txt_cash500_Before.Text <> "", txt_cash500_Before.Text.Replace(",", ""), DBNull.Value)
                    DR("CLOSE_FINAL") = IIf(txt_cash500_Remain.Text <> "", txt_cash500_Remain.Text.Replace(",", ""), DBNull.Value)
                Case VDM_BL.ShiftStatus.Open
                    DR("OPEN_BEFORE") = IIf(txt_cash500_Before.Text <> "", txt_cash500_Before.Text.Replace(",", ""), DBNull.Value)
                    DR("OPEN_FINAL") = IIf(txt_cash500_Remain.Text <> "", txt_cash500_Remain.Text.Replace(",", ""), DBNull.Value)
            End Select

            'VDM_BL.Device.Cash1000
            SQL = "SELECT * FROM TB_SHIFT_STOCK "
            SQL &= " WHERE SHIFT_ID=" & Session("SHIFT_ID") & " AND D_ID=" & VDM_BL.Device.CashIn & " AND Unit_Value=1000"
            DT = New DataTable
            DA = New SqlDataAdapter(SQL, BL.ConnectionString)
            DA.Fill(DT)
            If DT.Rows.Count = 0 Then
                DR = DT.NewRow
                DT.Rows.Add(DR)
                DR("SHIFT_ID") = Session("SHIFT_ID")
                DR("D_ID") = VDM_BL.Device.CashIn
                DR("Unit_Value") = 1000
            Else
                DR = DT.Rows(0)
            End If
            Select Case Session("SHIFT_Status")
                Case VDM_BL.ShiftStatus.Close
                    DR("CLOSE_BEFORE") = IIf(txt_cash1000_Before.Text <> "", txt_cash1000_Before.Text.Replace(",", ""), DBNull.Value)
                    DR("CLOSE_FINAL") = IIf(txt_cash1000_Remain.Text <> "", txt_cash1000_Remain.Text.Replace(",", ""), DBNull.Value)
                Case VDM_BL.ShiftStatus.Open
                    DR("OPEN_BEFORE") = IIf(txt_cash1000_Before.Text <> "", txt_cash1000_Before.Text.Replace(",", ""), DBNull.Value)
                    DR("OPEN_FINAL") = IIf(txt_cash1000_Remain.Text <> "", txt_cash1000_Remain.Text.Replace(",", ""), DBNull.Value)
            End Select
            cmd = New SqlCommandBuilder(DA)
            DA.Update(DT)
            result = True
        Catch ex As Exception
            Alert(Me.Page, ex.Message)
        End Try

        Return result
    End Function


End Class