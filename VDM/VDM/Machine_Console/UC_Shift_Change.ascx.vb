Imports System.Data
Imports System.Data.SqlClient

Public Class UC_Shift_Change
    Inherits System.Web.UI.UserControl

    Dim BL As New VDM_BL

#Region "Property"

    Public ReadOnly Property Total() As Integer
        Get
            Return FormatNumber(Val(lblSum.Text.Replace(",", "")), 0)
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

    End Sub

    Public Sub SetData()

        Dim SQL As String = ""
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
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

    Public Sub Calculate_Total()
        lblSum.Text = FormatNumber(Val(lbl_coin1_Amount.Text.Replace(",", "")) + Val(lbl_coin5_Amount.Text.Replace(",", "")) + Val(lbl_cash20_Amount.Text.Replace(",", "")) + Val(lbl_cash100_Amount.Text.Replace(",", "")), 0)
    End Sub
#End Region

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







    Function Validate() As Boolean
        Dim result As Boolean = True
        If Val(txt_coin1_Before.Text) < Val(txt_coin1_Pick.Text) Then
            Alert(Me.Page, "ตรวจสอบจำนวน เอาออก coin1")
            result = False
        End If

        If Val(txt_coin5_Before.Text) < Val(txt_coin5_Pick.Text) Then
            Alert(Me.Page, "ตรวจสอบจำนวน เอาออก coin5")
            result = False
        End If

        If Val(txt_cash20_Before.Text) < Val(txt_cash20_Pick.Text) Then
            Alert(Me.Page, "ตรวจสอบจำนวน เอาออก cash20")
            result = False
        End If

        If Val(txt_cash100_Before.Text) < Val(txt_cash100_Pick.Text) Then
            Alert(Me.Page, "ตรวจสอบจำนวน เอาออก")
            result = False
        End If

        Return result
    End Function

    Function Save() As Boolean
        Dim result As Boolean = False
        'clear Device 
        Dim SQL As String = "DELETE FROM TB_SHIFT_STOCK" & vbNewLine
        SQL &= " WHERE SHIFT_ID=" & Session("SHIFT_ID") & " AND D_ID IN (" & VDM_BL.DeviceType.Coin1 & "," & VDM_BL.DeviceType.Coin5 & "," & VDM_BL.DeviceType.Cash20 & "," & VDM_BL.DeviceType.Cash100 & ")"
        BL.ExecuteNonQuery(SQL)

        'save Device
        SQL = "SELECT * FROM TB_SHIFT_STOCK WHERE SHIFT_ID=" & Session("SHIFT_ID")
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)

        Dim DR As DataRow
        'VDM_BL.DeviceType.Coin1
        DR = DT.NewRow
        DT.Rows.Add(DR)
        DR("SHIFT_ID") = Session("SHIFT_ID")
        DR("D_ID") = VDM_BL.DeviceType.Coin1
        DR("Unit_Value") = 1
        Select Case Session("SHIFT_Status")
            Case VDM_BL.ShiftStatus.Close
                DR("CLOSE_BEFORE") = IIf(txt_coin1_Before.Text <> "", txt_coin1_Before.Text.Replace(",", ""), DBNull.Value)
                DR("CLOSE_FINAL") = IIf(txt_coin1_Remain.Text <> "", txt_coin1_Remain.Text.Replace(",", ""), DBNull.Value)
            Case VDM_BL.ShiftStatus.Open
                DR("OPEN_BEFORE") = IIf(txt_coin1_Before.Text <> "", txt_coin1_Before.Text.Replace(",", ""), DBNull.Value)
                DR("OPEN_FINAL") = IIf(txt_coin1_Remain.Text <> "", txt_coin1_Remain.Text.Replace(",", ""), DBNull.Value)
        End Select

        'VDM_BL.DeviceType.Coin5
        DR = DT.NewRow
        DT.Rows.Add(DR)
        DR("SHIFT_ID") = Session("SHIFT_ID")
        DR("D_ID") = VDM_BL.DeviceType.Coin5
        DR("Unit_Value") = 5
        Select Case Session("SHIFT_Status")
            Case VDM_BL.ShiftStatus.Close
                DR("CLOSE_BEFORE") = IIf(txt_coin5_Before.Text <> "", txt_coin5_Before.Text.Replace(",", ""), DBNull.Value)
                DR("CLOSE_FINAL") = IIf(txt_coin5_Remain.Text <> "", txt_coin5_Remain.Text.Replace(",", ""), DBNull.Value)
            Case VDM_BL.ShiftStatus.Open
                DR("OPEN_BEFORE") = IIf(txt_coin5_Before.Text <> "", txt_coin5_Before.Text.Replace(",", ""), DBNull.Value)
                DR("OPEN_FINAL") = IIf(txt_coin5_Remain.Text <> "", txt_coin5_Remain.Text.Replace(",", ""), DBNull.Value)
        End Select

        'VDM_BL.DeviceType.Cash20
        DR = DT.NewRow
        DT.Rows.Add(DR)
        DR("SHIFT_ID") = Session("SHIFT_ID")
        DR("D_ID") = VDM_BL.DeviceType.Cash20
        DR("Unit_Value") = 20
        Select Case Session("SHIFT_Status")
            Case VDM_BL.ShiftStatus.Close
                DR("CLOSE_BEFORE") = IIf(txt_cash20_Before.Text <> "", txt_cash20_Before.Text.Replace(",", ""), DBNull.Value)
                DR("CLOSE_FINAL") = IIf(txt_cash20_Remain.Text <> "", txt_cash20_Remain.Text.Replace(",", ""), DBNull.Value)
            Case VDM_BL.ShiftStatus.Open
                DR("OPEN_BEFORE") = IIf(txt_cash20_Before.Text <> "", txt_cash20_Before.Text.Replace(",", ""), DBNull.Value)
                DR("OPEN_FINAL") = IIf(txt_cash20_Remain.Text <> "", txt_cash20_Remain.Text.Replace(",", ""), DBNull.Value)
        End Select

        'VDM_BL.DeviceType.Cash100
        DR = DT.NewRow
        DT.Rows.Add(DR)
        DR("SHIFT_ID") = Session("SHIFT_ID")
        DR("D_ID") = VDM_BL.DeviceType.Cash100
        DR("Unit_Value") = 100
        Select Case Session("SHIFT_Status")
            Case VDM_BL.ShiftStatus.Close
                DR("CLOSE_BEFORE") = IIf(txt_cash100_Before.Text <> "", txt_cash100_Before.Text.Replace(",", ""), DBNull.Value)
                DR("CLOSE_FINAL") = IIf(txt_cash100_Remain.Text <> "", txt_cash100_Remain.Text.Replace(",", ""), DBNull.Value)
            Case VDM_BL.ShiftStatus.Open
                DR("OPEN_BEFORE") = IIf(txt_cash100_Before.Text <> "", txt_cash100_Before.Text.Replace(",", ""), DBNull.Value)
                DR("OPEN_FINAL") = IIf(txt_cash100_Remain.Text <> "", txt_cash100_Remain.Text.Replace(",", ""), DBNull.Value)
        End Select
        Dim cmd As New SqlCommandBuilder(DA)
        Try
            DA.Update(DT)
            result = True
        Catch ex As Exception
            Alert(Me.Page, ex.Message)

        End Try



        Return result
    End Function

    Function GetDeviceInfo(ByVal D_ID As Integer) As DataTable
        Dim SQL As String = " SELECT * FROM MS_DEVICE WHERE D_ID=" & D_ID
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
        Dim DT As DataTable = GetDeviceInfo(VDM_BL.DeviceType.Coin1)
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
        Dim DT As DataTable = GetDeviceInfo(VDM_BL.DeviceType.Coin5)
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
        Dim DT As DataTable = GetDeviceInfo(VDM_BL.DeviceType.Cash20)
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
        Dim DT As DataTable = GetDeviceInfo(VDM_BL.DeviceType.Cash100)
        If DT.Rows.Count > 0 Then
            txt_cash100_Input.Text = FormatNumber(Val(DT.Rows(0).Item("Max_Qty")), 0)
            txt_cash100_TextChanged(Nothing, Nothing)
        Else
            Alert(Me.Page, "ตรวจสอบ Device Cash100")
            Exit Sub
        End If
    End Sub


End Class