Public Class UC_Shift_Change
    Inherits System.Web.UI.UserControl

    Public Property Total As Integer

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
            Calculate_Total()

        End If

    End Sub

    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub


    Private Sub ClearEditForm()
        '--CoinIn
        txt_coin1_Before.Text = ""
        txt_coin1_Pick.Text =""
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
        If Val(lblSum.Text.Replace(",", "")) > 0 Then
            Total = FormatNumber(Val(lblSum.Text.Replace(",", "")), 0)
        End If
    End Sub

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
End Class