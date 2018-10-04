Imports System.Data
Imports System.Data.SqlClient

Public Class UC_Shift_Recieve
    Inherits System.Web.UI.UserControl

    Dim BL As New VDM_BL

    Private ReadOnly Property KO_ID As Integer
        Get
            Return Request.Cookies("KO_ID").Value
        End Get
    End Property

#Region "Property"
    Public ReadOnly Property Total() As Integer
        Get
            Return FormatNumber(Val(Sum_coin) + Val(Sum_cash), 0)
        End Get
    End Property

    Public ReadOnly Property Remain_coin() As Integer
        Get
            Return FormatNumber(Val(lbl_RemainCoinIn.Text), 0)
        End Get
    End Property

    Public ReadOnly Property Sum_coin() As Integer
        Get
            Return FormatNumber(Val(lbl_SumCoinIn.Text), 0)
        End Get
    End Property
    Public ReadOnly Property Remain_cash() As Integer
        Get
            Return FormatNumber(Val(lbl_RemainCashIn.Text), 0)
        End Get
    End Property
    Public ReadOnly Property Sum_cash() As Integer
        Get
            Return FormatNumber(Val(lbl_SumCashIn.Text), 0)
        End Get
    End Property
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNumeric(Session("USER_ID")) Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('กรุณาเข้าสู่ระบบ'); window.location.href='Login.aspx';", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            ClearEditForm()
            BindListCoinIn()
            Current_DataCoinIn()
            BindListCashIn()
            Current_DataCashIn()

        Else
            initFormPlugin()
        End If
    End Sub

    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

#Region "CoinIn"

    Private Sub BindListCoinIn()

        Dim DT As DataTable = BL.GetCoinIn_List()
        DT.Columns.Add("D_ID", GetType(Integer))
        DT.Columns.Add("DT_ID", GetType(Integer))
        DT.Columns.Add("Current_Qty")
        DT.Columns.Add("Pick")
        DT.Columns.Add("Input")
        DT.Columns.Add("Remain")

        rptListCoinIn.DataSource = DT
        rptListCoinIn.DataBind()

    End Sub
    Private Sub rptListCoinIn_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptListCoinIn.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim img As HtmlImage = e.Item.FindControl("img")
        Dim lbl_Before As Label = e.Item.FindControl("lbl_Before")
        Dim txt_Pick As TextBox = e.Item.FindControl("txt_Pick")
        Dim btn_Pick_Full As Button = e.Item.FindControl("btn_Pick_Full")
        Dim txt_Input As TextBox = e.Item.FindControl("txt_Input")
        Dim btn_Input_Full As Button = e.Item.FindControl("btn_Input_Full")
        Dim lbl_Remain As Label = e.Item.FindControl("lbl_Remain")
        Dim lbl_Amount As Label = e.Item.FindControl("lbl_Amount")

        ModuleGlobal.ImplementJavaNumericText(txt_Pick, "Center")
        ModuleGlobal.ImplementJavaNumericText(txt_Input, "Center")

        img.Attributes("Src") = "../" & e.Item.DataItem("Icon_Green").ToString

        If Not IsDBNull(e.Item.DataItem("Unit_Value")) Then
            Dim Before As Integer = Val(BL.GetKiosk_Current_QTY(KO_ID, VDM_BL.Device.CoinIn, e.Item.DataItem("Unit_Value")))
            If Before > 0 Then
                lbl_Before.Text = FormatNumber(Before, 0)
            End If
        End If



        If Not IsDBNull(e.Item.DataItem("Pick")) Then
            txt_Pick.Text = FormatNumber(Val(e.Item.DataItem("Pick")), 0)
        End If
        If Not IsDBNull(e.Item.DataItem("Input")) Then
            txt_Input.Text = FormatNumber(Val(e.Item.DataItem("Input")), 0)
        End If
        lbl_Remain.Attributes("Unit_Value") = Val(e.Item.DataItem("Unit_Value"))
        lbl_Remain.Attributes("DT_ID") = VDM_BL.DeviceType.CoinIn

        lbl_Remain.Text = FormatNumber((Val(lbl_Before.Text.Replace(",", "")) - Val(txt_Pick.Text.Replace(",", ""))) + Val(txt_Input.Text.Replace(",", "")), 0)
        lbl_Amount.Text = FormatNumber(Val(lbl_Remain.Text.Replace(",", "")) * Val(e.Item.DataItem("Unit_Value")), 0)

        btn_Pick_Full.CommandArgument = VDM_BL.Device.CoinIn
        btn_Input_Full.CommandArgument = VDM_BL.Device.CoinIn

    End Sub
    Private Sub rptListCoinIn_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptListCoinIn.ItemCommand
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim lbl_Before As Label = e.Item.FindControl("lbl_Before")
        Dim txt_Pick As TextBox = e.Item.FindControl("txt_Pick")
        Dim btn_Pick_Full As Button = e.Item.FindControl("btn_Pick_Full")
        Dim txt_Input As TextBox = e.Item.FindControl("txt_Input")
        Dim btn_Input_Full As Button = e.Item.FindControl("btn_Input_Full")
        Dim lbl_Remain As Label = e.Item.FindControl("lbl_Remain")
        Dim lbl_Amount As Label = e.Item.FindControl("lbl_Amount")

        Select Case e.CommandName
            Case "Pick_Full"
                txt_Pick.Text = FormatNumber(Val(lbl_Before.Text.Replace(",", "")), 0)

                lbl_Remain.Text = FormatNumber((Val(lbl_Before.Text.Replace(",", "")) - Val(txt_Pick.Text.Replace(",", ""))) + Val(txt_Input.Text.Replace(",", "")), 0)
                lbl_Amount.Text = FormatNumber(Val(lbl_Remain.Text.Replace(",", "")) * Val(lbl_Remain.Attributes("Unit_Value")), 0)

            Case "Input_Full"
                Dim SQL As String = " SELECT * FROM MS_DEVICE WHERE D_ID=" & Val(btn_Input_Full.CommandArgument)
                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                Dim DT As New DataTable
                DA.Fill(DT)
                If DT.Rows.Count > 0 Then
                    txt_Input.Text = FormatNumber(Val(DT.Rows(0).Item("Max_Qty")), 0)
                Else
                    Alert(Me.Page, "ตรวจสอบ Device " & Val(lbl_Remain.Attributes("Unit_Value")) & "")
                    Exit Sub
                End If

                lbl_Remain.Text = FormatNumber((Val(lbl_Before.Text.Replace(",", "")) - Val(txt_Pick.Text.Replace(",", ""))) + Val(txt_Input.Text.Replace(",", "")), 0)
                lbl_Amount.Text = FormatNumber(Val(lbl_Remain.Text.Replace(",", "")) * Val(lbl_Remain.Attributes("Unit_Value")), 0)


        End Select

        Current_DataCoinIn()

    End Sub
    Protected Sub txt_TextChanged_CoinIn(sender As Object, e As EventArgs)
        Dim txt As TextBox = sender
        Dim rpt As RepeaterItem = txt.Parent

        Dim lbl_Before As Label = DirectCast(rpt.FindControl("lbl_Before"), Label)
        Dim txt_Pick As TextBox = DirectCast(rpt.FindControl("txt_Pick"), TextBox)
        Dim btn_Pick_Full As Button = DirectCast(rpt.FindControl("btn_Pick_Full"), Button)
        Dim txt_Input As TextBox = DirectCast(rpt.FindControl("txt_Input"), TextBox)
        Dim btn_Input_Full As Button = DirectCast(rpt.FindControl("btn_Input_Full"), Button)
        Dim lbl_Remain As Label = DirectCast(rpt.FindControl("lbl_Remain"), Label)
        Dim lbl_Amount As Label = DirectCast(rpt.FindControl("lbl_Amount"), Label)

        lbl_Remain.Text = FormatNumber((Val(lbl_Before.Text.Replace(",", "")) - Val(txt_Pick.Text.Replace(",", ""))) + Val(txt_Input.Text.Replace(",", "")), 0)
        lbl_Amount.Text = FormatNumber(Val(lbl_Remain.Text.Replace(",", "")) * Val(lbl_Remain.Attributes("Unit_Value")), 0)

        Current_DataCoinIn()

    End Sub
    Public Function Current_DataCoinIn() As DataTable

        Dim DT As New DataTable
        DT.Columns.Add("D_ID", GetType(Integer))
        DT.Columns.Add("DT_ID", GetType(Integer))
        DT.Columns.Add("Current_Qty")
        DT.Columns.Add("Pick")
        DT.Columns.Add("Input")
        DT.Columns.Add("Remain")
        DT.Columns.Add("Unit_Value")

        Dim Sum As Integer = 0
        Dim Remain As Integer = 0

        For Each rpt As RepeaterItem In rptListCoinIn.Items
            If rpt.ItemType <> ListItemType.AlternatingItem And rpt.ItemType <> ListItemType.Item Then Continue For

            Dim lbl_Before As Label = rpt.FindControl("lbl_Before")
            Dim txt_Pick As TextBox = rpt.FindControl("txt_Pick")
            Dim btn_Pick_Full As Button = rpt.FindControl("btn_Pick_Full")
            Dim txt_Input As TextBox = rpt.FindControl("txt_Input")
            Dim btn_Input_Full As Button = rpt.FindControl("btn_Input_Full")
            Dim lbl_Remain As Label = rpt.FindControl("lbl_Remain")
            Dim lbl_Amount As Label = rpt.FindControl("lbl_Amount")

            Dim DR As DataRow = DT.NewRow

            DR("D_ID") = btn_Pick_Full.CommandArgument
            DR("Current_Qty") = Val(lbl_Before.Text.Replace(",", ""))
            DR("Pick") = Val(txt_Pick.Text.Replace(",", ""))
            DR("Input") = Val(txt_Input.Text.Replace(",", ""))
            DR("Remain") = Val(lbl_Remain.Text.Replace(",", ""))
            DR("DT_ID") = lbl_Remain.Attributes("DT_ID")
            DR("Unit_Value") = lbl_Remain.Attributes("Unit_Value")
            DT.Rows.Add(DR)
            Sum = Sum + Val(lbl_Amount.Text.Replace(",", ""))
            Remain = Remain + Val(lbl_Remain.Text.Replace(",", ""))
        Next
        lbl_SumCoinIn.Text = Val(Sum)
        lbl_RemainCoinIn.Text = Val(Remain)

        lblSum.Text = FormatNumber(Total, 0)
        Return DT
    End Function

#End Region

#Region "CashIn"

    Private Sub BindListCashIn()

        Dim DT As DataTable = BL.GetCashIn_List()
        DT.Columns.Add("D_ID", GetType(Integer))
        DT.Columns.Add("DT_ID", GetType(Integer))
        DT.Columns.Add("Current_Qty")
        DT.Columns.Add("Pick")
        DT.Columns.Add("Input")
        DT.Columns.Add("Remain")

        rptListCashIn.DataSource = DT
        rptListCashIn.DataBind()

    End Sub
    Private Sub rptListCashIn_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptListCashIn.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim img As HtmlImage = e.Item.FindControl("img")
        Dim lbl_Before As Label = e.Item.FindControl("lbl_Before")
        Dim txt_Pick As TextBox = e.Item.FindControl("txt_Pick")
        Dim btn_Pick_Full As Button = e.Item.FindControl("btn_Pick_Full")
        Dim txt_Input As TextBox = e.Item.FindControl("txt_Input")
        Dim btn_Input_Full As Button = e.Item.FindControl("btn_Input_Full")
        Dim lbl_Remain As Label = e.Item.FindControl("lbl_Remain")
        Dim lbl_Amount As Label = e.Item.FindControl("lbl_Amount")

        ModuleGlobal.ImplementJavaNumericText(txt_Pick, "Center")
        ModuleGlobal.ImplementJavaNumericText(txt_Input, "Center")

        img.Attributes("Src") = "../" & e.Item.DataItem("Icon_Green").ToString

        If Not IsDBNull(e.Item.DataItem("Unit_Value")) Then
            Dim Before As Integer = Val(BL.GetKiosk_Current_QTY(KO_ID, VDM_BL.Device.CashIn, e.Item.DataItem("Unit_Value")))
            If Before > 0 Then
                lbl_Before.Text = FormatNumber(Before, 0)
            End If
        End If



        If Not IsDBNull(e.Item.DataItem("Pick")) Then
            txt_Pick.Text = FormatNumber(Val(e.Item.DataItem("Pick")), 0)
        End If
        If Not IsDBNull(e.Item.DataItem("Input")) Then
            txt_Input.Text = FormatNumber(Val(e.Item.DataItem("Input")), 0)
        End If
        lbl_Remain.Attributes("Unit_Value") = Val(e.Item.DataItem("Unit_Value"))
        lbl_Remain.Attributes("DT_ID") = VDM_BL.DeviceType.CashIn

        lbl_Remain.Text = FormatNumber((Val(lbl_Before.Text.Replace(",", "")) - Val(txt_Pick.Text.Replace(",", ""))) + Val(txt_Input.Text.Replace(",", "")), 0)
        lbl_Amount.Text = FormatNumber(Val(lbl_Remain.Text.Replace(",", "")) * Val(e.Item.DataItem("Unit_Value")), 0)

        btn_Pick_Full.CommandArgument = VDM_BL.Device.CashIn
        btn_Input_Full.CommandArgument = VDM_BL.Device.CashIn

    End Sub
    Private Sub rptListCashIn_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptListCashIn.ItemCommand
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim lbl_Before As Label = e.Item.FindControl("lbl_Before")
        Dim txt_Pick As TextBox = e.Item.FindControl("txt_Pick")
        Dim btn_Pick_Full As Button = e.Item.FindControl("btn_Pick_Full")
        Dim txt_Input As TextBox = e.Item.FindControl("txt_Input")
        Dim btn_Input_Full As Button = e.Item.FindControl("btn_Input_Full")
        Dim lbl_Remain As Label = e.Item.FindControl("lbl_Remain")
        Dim lbl_Amount As Label = e.Item.FindControl("lbl_Amount")

        Select Case e.CommandName
            Case "Pick_Full"
                txt_Pick.Text = FormatNumber(Val(lbl_Before.Text.Replace(",", "")), 0)

                lbl_Remain.Text = FormatNumber((Val(lbl_Before.Text.Replace(",", "")) - Val(txt_Pick.Text.Replace(",", ""))) + Val(txt_Input.Text.Replace(",", "")), 0)
                lbl_Amount.Text = FormatNumber(Val(lbl_Remain.Text.Replace(",", "")) * Val(lbl_Remain.Attributes("Unit_Value")), 0)

            Case "Input_Full"
                Dim SQL As String = " SELECT * FROM MS_DEVICE WHERE D_ID=" & Val(btn_Input_Full.CommandArgument)
                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                Dim DT As New DataTable
                DA.Fill(DT)
                If DT.Rows.Count > 0 Then
                    txt_Input.Text = FormatNumber(Val(DT.Rows(0).Item("Max_Qty")), 0)
                Else
                    Alert(Me.Page, "ตรวจสอบ Device " & Val(lbl_Remain.Attributes("Unit_Value")) & "")
                    Exit Sub
                End If

                lbl_Remain.Text = FormatNumber((Val(lbl_Before.Text.Replace(",", "")) - Val(txt_Pick.Text.Replace(",", ""))) + Val(txt_Input.Text.Replace(",", "")), 0)
                lbl_Amount.Text = FormatNumber(Val(lbl_Remain.Text.Replace(",", "")) * Val(lbl_Remain.Attributes("Unit_Value")), 0)


        End Select

        Current_DataCashIn()

    End Sub
    Protected Sub txt_TextChanged_CashIn(sender As Object, e As EventArgs)
        Dim txt As TextBox = sender
        Dim rpt As RepeaterItem = txt.Parent

        Dim lbl_Before As Label = DirectCast(rpt.FindControl("lbl_Before"), Label)
        Dim txt_Pick As TextBox = DirectCast(rpt.FindControl("txt_Pick"), TextBox)
        Dim btn_Pick_Full As Button = DirectCast(rpt.FindControl("btn_Pick_Full"), Button)
        Dim txt_Input As TextBox = DirectCast(rpt.FindControl("txt_Input"), TextBox)
        Dim btn_Input_Full As Button = DirectCast(rpt.FindControl("btn_Input_Full"), Button)
        Dim lbl_Remain As Label = DirectCast(rpt.FindControl("lbl_Remain"), Label)
        Dim lbl_Amount As Label = DirectCast(rpt.FindControl("lbl_Amount"), Label)

        lbl_Remain.Text = FormatNumber((Val(lbl_Before.Text.Replace(",", "")) - Val(txt_Pick.Text.Replace(",", ""))) + Val(txt_Input.Text.Replace(",", "")), 0)
        lbl_Amount.Text = FormatNumber(Val(lbl_Remain.Text.Replace(",", "")) * Val(lbl_Remain.Attributes("Unit_Value")), 0)

        Current_DataCashIn()

    End Sub
    Public Function Current_DataCashIn() As DataTable

        Dim DT As New DataTable
        DT.Columns.Add("D_ID", GetType(Integer))
        DT.Columns.Add("DT_ID", GetType(Integer))
        DT.Columns.Add("Current_Qty")
        DT.Columns.Add("Pick")
        DT.Columns.Add("Input")
        DT.Columns.Add("Remain")
        DT.Columns.Add("Unit_Value")

        Dim Sum As Integer = 0
        Dim Remain As Integer = 0
        For Each rpt As RepeaterItem In rptListCashIn.Items
            If rpt.ItemType <> ListItemType.AlternatingItem And rpt.ItemType <> ListItemType.Item Then Continue For

            Dim lbl_Before As Label = rpt.FindControl("lbl_Before")
            Dim txt_Pick As TextBox = rpt.FindControl("txt_Pick")
            Dim btn_Pick_Full As Button = rpt.FindControl("btn_Pick_Full")
            Dim txt_Input As TextBox = rpt.FindControl("txt_Input")
            Dim btn_Input_Full As Button = rpt.FindControl("btn_Input_Full")
            Dim lbl_Remain As Label = rpt.FindControl("lbl_Remain")
            Dim lbl_Amount As Label = rpt.FindControl("lbl_Amount")

            Dim DR As DataRow = DT.NewRow

            DR("D_ID") = btn_Pick_Full.CommandArgument
            DR("Current_Qty") = Val(lbl_Before.Text.Replace(",", ""))
            DR("Pick") = Val(txt_Pick.Text.Replace(",", ""))
            DR("Input") = Val(txt_Input.Text.Replace(",", ""))
            DR("Remain") = Val(lbl_Remain.Text.Replace(",", ""))
            DR("DT_ID") = lbl_Remain.Attributes("DT_ID")
            DR("Unit_Value") = lbl_Remain.Attributes("Unit_Value")
            DT.Rows.Add(DR)
            Sum = Sum + Val(lbl_Amount.Text.Replace(",", ""))
            Remain = Remain + Val(lbl_Remain.Text.Replace(",", ""))
        Next
        lbl_SumCoinIn.Text = Val(Sum)
        lbl_RemainCoinIn.Text = Val(Remain)

        lblSum.Text = FormatNumber(Total, 0)
        Return DT
    End Function

#End Region


    Private Sub ClearEditForm()
        lbl_SumCoinIn.Text = ""
        lbl_RemainCoinIn.Text = ""

        lbl_SumCashIn.Text = ""
        lbl_RemainCashIn.Text = ""
        lblSum.Text = ""
    End Sub



    Function Validate() As Boolean

        Dim result As Boolean = True
        Dim DT As DataTable = Current_DataCoinIn()
        If DT.Rows.Count > 0 Then
            For i As Integer = 0 To DT.Rows.Count - 1
                If Val(DT.Rows(i).Item("Current_Qty")) < Val(DT.Rows(i).Item("Pick")) Then
                    Alert(Me.Page, "ตรวจสอบจำนวนเงินเอาออก " & DT.Rows(i).Item("Unit_Value") & " บาท")
                    result = False
                End If

            Next
        End If

        DT = Current_DataCashIn()
        If DT.Rows.Count > 0 Then
            For i As Integer = 0 To DT.Rows.Count - 1
                If Val(DT.Rows(i).Item("Current_Qty")) < Val(DT.Rows(i).Item("Pick")) Then
                    Alert(Me.Page, "ตรวจสอบจำนวนเงินเอาออก " & DT.Rows(i).Item("Unit_Value") & " บาท")
                    result = False
                End If
            Next
        End If
        Return result
    End Function

    Function Save() As Boolean


        Dim result As Boolean = False
        Try
            Dim SQL As String
            Dim DT As New DataTable
            Dim DA As New SqlDataAdapter
            Dim DR As DataRow
            Dim cmd As New SqlCommandBuilder(DA)
            'CoinIn
            Dim DT_Data As DataTable = Current_DataCoinIn()
            If DT_Data.Rows.Count > 0 Then
                For i As Integer = 0 To DT_Data.Rows.Count - 1
                    SQL = "SELECT * FROM TB_SHIFT_STOCK "
                    SQL &= " WHERE SHIFT_ID=" & Session("SHIFT_ID") & " AND D_ID=" & VDM_BL.Device.CoinIn & " AND Unit_Value=" & DT_Data.Rows(i).Item("Unit_Value")
                    DT = New DataTable
                    DA = New SqlDataAdapter(SQL, BL.ConnectionString)
                    DA.Fill(DT)
                    If DT.Rows.Count = 0 Then
                        DR = DT.NewRow
                        DT.Rows.Add(DR)
                        DR("SHIFT_ID") = Session("SHIFT_ID")
                        DR("D_ID") = VDM_BL.Device.CoinIn
                        DR("Unit_Value") = DT_Data.Rows(i).Item("Unit_Value")
                    Else
                        DR = DT.Rows(0)
                    End If
                    Select Case Session("SHIFT_Status")
                        Case VDM_BL.ShiftStatus.Close
                            DR("CLOSE_BEFORE") = IIf(Val(DT_Data.Rows(i).Item("Current_Qty")) <> 0, Val(DT_Data.Rows(i).Item("Current_Qty")), DBNull.Value)
                            DR("CLOSE_FINAL") = IIf(Val(DT_Data.Rows(i).Item("Remain")) <> 0, Val(DT_Data.Rows(i).Item("Remain")), DBNull.Value)
                        Case VDM_BL.ShiftStatus.Open
                            DR("OPEN_BEFORE") = IIf(Val(DT_Data.Rows(i).Item("Current_Qty")) <> 0, Val(DT_Data.Rows(i).Item("Current_Qty")), DBNull.Value)
                            DR("OPEN_FINAL") = IIf(Val(DT_Data.Rows(i).Item("Remain")) <> 0, Val(DT_Data.Rows(i).Item("Remain")), DBNull.Value)
                    End Select
                    cmd = New SqlCommandBuilder(DA)
                    DA.Update(DT)
                Next
            End If

            'CashIn
            DT_Data = Current_DataCashIn()
            If DT_Data.Rows.Count > 0 Then
                For i As Integer = 0 To DT_Data.Rows.Count - 1
                    SQL = "SELECT * FROM TB_SHIFT_STOCK "
                    SQL &= " WHERE SHIFT_ID=" & Session("SHIFT_ID") & " AND D_ID=" & VDM_BL.Device.CashIn & " AND Unit_Value=" & DT_Data.Rows(i).Item("Unit_Value")
                    DT = New DataTable
                    DA = New SqlDataAdapter(SQL, BL.ConnectionString)
                    DA.Fill(DT)
                    If DT.Rows.Count = 0 Then
                        DR = DT.NewRow
                        DT.Rows.Add(DR)
                        DR("SHIFT_ID") = Session("SHIFT_ID")
                        DR("D_ID") = VDM_BL.Device.CashIn
                        DR("Unit_Value") = DT_Data.Rows(i).Item("Unit_Value")
                    Else
                        DR = DT.Rows(0)
                    End If
                    Select Case Session("SHIFT_Status")
                        Case VDM_BL.ShiftStatus.Close
                            DR("CLOSE_BEFORE") = IIf(Val(DT_Data.Rows(i).Item("Current_Qty")) <> 0, Val(DT_Data.Rows(i).Item("Current_Qty")), DBNull.Value)
                            DR("CLOSE_FINAL") = IIf(Val(DT_Data.Rows(i).Item("Remain")) <> 0, Val(DT_Data.Rows(i).Item("Remain")), DBNull.Value)
                        Case VDM_BL.ShiftStatus.Open
                            DR("OPEN_BEFORE") = IIf(Val(DT_Data.Rows(i).Item("Current_Qty")) <> 0, Val(DT_Data.Rows(i).Item("Current_Qty")), DBNull.Value)
                            DR("OPEN_FINAL") = IIf(Val(DT_Data.Rows(i).Item("Remain")) <> 0, Val(DT_Data.Rows(i).Item("Remain")), DBNull.Value)
                    End Select
                    cmd = New SqlCommandBuilder(DA)
                    DA.Update(DT)
                Next
            End If

            result = True
        Catch ex As Exception
            Alert(Me.Page, ex.Message)
        End Try

        Return result
    End Function


End Class