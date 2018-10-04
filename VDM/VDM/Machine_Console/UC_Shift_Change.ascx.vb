Imports System.Data
Imports System.Data.SqlClient

Public Class UC_Shift_Change
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
            Return FormatNumber(Val(lblSum.Text.Replace(",", "")), 0)
        End Get
    End Property



#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNumeric(Session("USER_ID")) Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('กรุณาเข้าสู่ระบบ'); window.location.href='Login.aspx';", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            BindList()
            Current_Data()
        Else
            initFormPlugin()
        End If

    End Sub

    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Public Sub Start_Menu()
        BindList()
        Current_Data()
    End Sub

    Private Sub BindList()
        Dim SQL As String = " " & vbLf
        SQL &= " Select MS_DEVICE.D_ID " & vbLf
        SQL &= "      ,D_Name,D_Fullname,MS_DEVICE.DT_ID,Unit_Value " & vbLf
        SQL &= "      ,Max_Qty,Warning_Qty,Critical_Qty " & vbLf
        SQL &= " 	  ,TB_KIOSK_DEVICE.Current_Qty " & vbLf
        SQL &= "      ,Icon_White,Icon_Green,Icon_Red " & vbLf
        SQL &= "      ,D_Order,Active_Status " & vbLf

        SQL &= "      ,NULL Pick " & vbLf
        SQL &= "      ,NULL Input " & vbLf

        SQL &= "  From MS_DEVICE  " & vbLf
        SQL &= "  Left Join TB_KIOSK_DEVICE ON TB_KIOSK_DEVICE.D_ID=MS_DEVICE.D_ID  And KO_ID=" & KO_ID & vbLf
        SQL &= "  WHERE MS_DEVICE.D_ID IN (" & VDM_BL.Device.Coin1 & "," & VDM_BL.Device.Coin5 & "," & VDM_BL.Device.Cash20 & "," & VDM_BL.Device.Cash100 & ")"
        SQL &= "  AND Active_Status=1 "
        SQL &= "  ORDER BY Unit_Value "
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        rptList.DataSource = DT
        rptList.DataBind()
        Current_Data()
    End Sub
    Private Sub rptList_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptList.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim img As HtmlImage = e.Item.FindControl("img")
        Dim lbl_Before As Label = e.Item.FindControl("lbl_Before")
        Dim txt_Pick As TextBox = e.Item.FindControl("txt_Pick")
        Dim btn_Pick_Full As Button = e.Item.FindControl("btn_Pick_Full")
        Dim txt_Input As TextBox = e.Item.FindControl("txt_Input")
        Dim btn_Input_Full As Button = e.Item.FindControl("btn_Input_Full")
        Dim lbl_Remain As Label = e.Item.FindControl("lbl_Remain")
        Dim lbl_Amount As Label = e.Item.FindControl("lbl_Amount")
        Dim imgAlert As Image = e.Item.FindControl("imgAlert")


        ModuleGlobal.ImplementJavaNumericText(txt_Pick, "Center")
        ModuleGlobal.ImplementJavaNumericText(txt_Input, "Center")


        img.Attributes("Src") = "../" & e.Item.DataItem("Icon_Green").ToString
        If Not IsDBNull(e.Item.DataItem("Current_Qty")) Then
            lbl_Before.Text = FormatNumber(Val(e.Item.DataItem("Current_Qty")), 0)
        End If
        If Not IsDBNull(e.Item.DataItem("Pick")) Then
            txt_Pick.Text = FormatNumber(Val(e.Item.DataItem("Pick")), 0)
        End If
        If Not IsDBNull(e.Item.DataItem("Input")) Then
            txt_Input.Text = FormatNumber(Val(e.Item.DataItem("Input")), 0)
        End If
        lbl_Remain.Attributes("Unit_Value") = Val(e.Item.DataItem("Unit_Value"))
        lbl_Remain.Attributes("DT_ID") = Val(e.Item.DataItem("DT_ID"))

        lbl_Remain.Text = FormatNumber((Val(lbl_Before.Text.Replace(",", "")) - Val(txt_Pick.Text.Replace(",", ""))) + Val(txt_Input.Text.Replace(",", "")), 0)
        lbl_Amount.Text = FormatNumber(Val(lbl_Remain.Text.Replace(",", "")) * Val(e.Item.DataItem("Unit_Value")), 0)

        Dim SQL As String = " SELECT * FROM MS_DEVICE WHERE D_ID=" & Val(btn_Input_Full.CommandArgument)
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            If Val(lbl_Remain.Text.Replace(",", "")) > Val(DT.Rows(0).Item("Max_Qty")) Then
                lbl_Amount.Style("color") = "red"
                imgAlert.Visible = True
            Else
                lbl_Amount.Style("color") = "black"
                imgAlert.Visible = False
            End If

        End If


        btn_Pick_Full.CommandArgument = e.Item.DataItem("D_ID")
        btn_Input_Full.CommandArgument = e.Item.DataItem("D_ID")

    End Sub
    Private Sub rptList_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptList.ItemCommand
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim lbl_Before As Label = e.Item.FindControl("lbl_Before")
        Dim txt_Pick As TextBox = e.Item.FindControl("txt_Pick")
        Dim btn_Pick_Full As Button = e.Item.FindControl("btn_Pick_Full")
        Dim txt_Input As TextBox = e.Item.FindControl("txt_Input")
        Dim btn_Input_Full As Button = e.Item.FindControl("btn_Input_Full")
        Dim lbl_Remain As Label = e.Item.FindControl("lbl_Remain")
        Dim lbl_Amount As Label = e.Item.FindControl("lbl_Amount")
        Dim imgAlert As Image = e.Item.FindControl("imgAlert")

        Dim Max_Qty As Integer = 0
        Dim SQL As String = " SELECT * FROM MS_DEVICE WHERE D_ID=" & Val(btn_Input_Full.CommandArgument)
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            Max_Qty = Val(DT.Rows(0).Item("Max_Qty"))
        End If

        Select Case e.CommandName
            Case "Pick_Full"
                txt_Pick.Text = FormatNumber(Val(lbl_Before.Text.Replace(",", "")), 0)

                lbl_Remain.Text = FormatNumber((Val(lbl_Before.Text.Replace(",", "")) - Val(txt_Pick.Text.Replace(",", ""))) + Val(txt_Input.Text.Replace(",", "")), 0)
                lbl_Amount.Text = FormatNumber(Val(lbl_Remain.Text.Replace(",", "")) * Val(lbl_Remain.Attributes("Unit_Value")), 0)

            Case "Input_Full"

                If DT.Rows.Count > 0 Then

                    txt_Input.Text = FormatNumber(Val(DT.Rows(0).Item("Max_Qty")), 0)
                Else
                    Alert(Me.Page, "ตรวจสอบ Device " & Val(lbl_Remain.Attributes("Unit_Value")) & "")
                    Exit Sub
                End If

                lbl_Remain.Text = FormatNumber((Val(lbl_Before.Text.Replace(",", "")) - Val(txt_Pick.Text.Replace(",", ""))) + Val(txt_Input.Text.Replace(",", "")), 0)
                lbl_Amount.Text = FormatNumber(Val(lbl_Remain.Text.Replace(",", "")) * Val(lbl_Remain.Attributes("Unit_Value")), 0)


        End Select

        If Max_Qty > 0 Then
            If Val(lbl_Remain.Text.Replace(",", "")) > Val(Max_Qty) Then
                lbl_Amount.Style("color") = "red"
                imgAlert.Visible = True
            Else
                lbl_Amount.Style("color") = "black"
                imgAlert.Visible = False
            End If
        End If



        Current_Data()

    End Sub

    Protected Sub txt_TextChanged(sender As Object, e As EventArgs)
        Dim txt As TextBox = sender
        Dim rpt As RepeaterItem = txt.Parent

        Dim lbl_Before As Label = DirectCast(rpt.FindControl("lbl_Before"), Label)
        Dim txt_Pick As TextBox = DirectCast(rpt.FindControl("txt_Pick"), TextBox)
        Dim btn_Pick_Full As Button = DirectCast(rpt.FindControl("btn_Pick_Full"), Button)
        Dim txt_Input As TextBox = DirectCast(rpt.FindControl("txt_Input"), TextBox)
        Dim btn_Input_Full As Button = DirectCast(rpt.FindControl("btn_Input_Full"), Button)
        Dim lbl_Remain As Label = DirectCast(rpt.FindControl("lbl_Remain"), Label)
        Dim lbl_Amount As Label = DirectCast(rpt.FindControl("lbl_Amount"), Label)
        Dim imgAlert As Image = DirectCast(rpt.FindControl("imgAlert"), Image)

        lbl_Remain.Text = FormatNumber((Val(lbl_Before.Text.Replace(",", "")) - Val(txt_Pick.Text.Replace(",", ""))) + Val(txt_Input.Text.Replace(",", "")), 0)
        lbl_Amount.Text = FormatNumber(Val(lbl_Remain.Text.Replace(",", "")) * Val(lbl_Remain.Attributes("Unit_Value")), 0)

        Dim SQL As String = " SELECT * FROM MS_DEVICE WHERE D_ID=" & Val(btn_Input_Full.CommandArgument)
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            If Val(lbl_Remain.Text.Replace(",", "")) > Val(DT.Rows(0).Item("Max_Qty")) Then
                lbl_Amount.Style("color") = "red"
                imgAlert.Visible = True
            Else
                lbl_Amount.Style("color") = "black"
                imgAlert.Visible = False
            End If

        End If



        Current_Data()

    End Sub

    Public Function Current_Data() As DataTable

        Dim DT As New DataTable
        DT.Columns.Add("D_ID", GetType(Integer))
        DT.Columns.Add("DT_ID", GetType(Integer))
        DT.Columns.Add("Current_Qty")
        DT.Columns.Add("Pick")
        DT.Columns.Add("Input")
        DT.Columns.Add("Remain")
        DT.Columns.Add("Unit_Value")

        Dim Sum As Integer = 0
        For Each rpt As RepeaterItem In rptList.Items
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
        Next
        lblSum.Text = FormatNumber(Val(Sum), 0)
        Return DT
    End Function


    Function Validate() As Boolean
        Dim result As Boolean = True
        Dim DT As DataTable = Current_Data()
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

            Dim DT_Data As DataTable = Current_Data()
            If DT_Data.Rows.Count > 0 Then
                For i As Integer = 0 To DT_Data.Rows.Count - 1
                    'save Device 
                    Dim SQL As String = "SELECT * FROM TB_SHIFT_STOCK "
                    SQL &= " WHERE SHIFT_ID=" & Session("SHIFT_ID") & " AND D_ID=" & DT_Data.Rows(i).Item("D_ID")
                    Dim DT As New DataTable
                    Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                    DA.Fill(DT)
                    Dim DR As DataRow
                    If DT.Rows.Count = 0 Then
                        DR = DT.NewRow
                        DT.Rows.Add(DR)
                        DR("SHIFT_ID") = Session("SHIFT_ID")
                        DR("D_ID") = DT_Data.Rows(i).Item("D_ID")
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
                    Dim cmd As New SqlCommandBuilder(DA)
                    DA.Update(DT)
                Next
            End If
            result = True
        Catch ex As Exception
            Alert(Me.Page, ex.Message)
        End Try

        Return result
    End Function


    Private Sub SavePREVIOUS_SHIFT()
        'Dim SQL As String = "SELECT * FROM TB_SHIFT_STOCK "
        'SQL &= " WHERE SHIFT_ID=" & Session("SHIFT_ID") & " AND D_ID=" & DT_Data.Rows(i).Item("D_ID")
        'Dim DT As New DataTable
        'Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        'DA.Fill(DT)
        'Dim DR As DataRow

        '      Select Case TOP 1 SHIFT_ID     
        'From TB_SHIFT
        'Where KO_ID = 1 And SHIFT_ID < 2 - -current shiftddd
        'ORDER BY SHIFT_ID DESC
    End Sub

End Class