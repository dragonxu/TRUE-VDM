Imports System.Data
Imports System.Data.SqlClient
Public Class UC_Shift_StockPaper
    Inherits System.Web.UI.UserControl
    Dim BL As New VDM_BL

    Private ReadOnly Property KO_ID As Integer
        Get
            Return Session("KO_ID")
        End Get
    End Property

#Region "Property"


    Public ReadOnly Property Total() As Integer
        Get
            Return FormatNumber(Val(txtRemainPaper.Text.Replace(",", "")), 0)
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
            CurrentData()
        Else
            initFormPlugin()
        End If
    End Sub
    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Private Sub ClearEditForm()
        txtMaxPaper.Text = ""
        txtRemainPaper.Text = ""


    End Sub

    Public Sub SetTextbox()

        ModuleGlobal.ImplementJavaNumericText(txtRemainPaper, "Center")
    End Sub

    Function GetDeviceInfo() As DataTable
        Dim SQL As String = " SELECT * FROM MS_DEVICE WHERE D_ID=" & VDM_BL.Device.Printer
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

    Public Sub CurrentData()
        '--Current 

        Dim SQL As String = " SELECT * FROM MS_DEVICE WHERE D_ID=" & VDM_BL.Device.Printer
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            If Not IsDBNull(DT.Rows(0).Item("Max_Qty")) Then
                txtMaxPaper.Text = FormatNumber(Val(DT.Rows(0).Item("Max_Qty")), 0)
            End If
        End If

        SQL = " SELECT * FROM TB_KIOSK_DEVICE WHERE KO_ID=" & KO_ID & " AND D_ID=" & VDM_BL.Device.Printer
        DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        DT = New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            If Not IsDBNull(DT.Rows(0).Item("Current_Qty")) Then
                txtRemainPaper.Text = FormatNumber(Val(DT.Rows(0).Item("Current_Qty")), 0)
            End If
        End If

    End Sub

    Private Sub btnInput_Full_Click(sender As Object, e As EventArgs) Handles btnInput_Full.Click

        If txtMaxPaper.Text <> "" Then
            txtRemainPaper.Text = FormatNumber(Val(txtMaxPaper.Text.Replace(",", "")), 0)
        Else
            Dim SQL As String = " SELECT * FROM MS_DEVICE WHERE D_ID=" & VDM_BL.Device.Printer
            Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
            Dim DT As New DataTable
            DA.Fill(DT)

            'Dim SQL As String = " SELECT * FROM TB_KIOSK_DEVICE WHERE KO_ID=" & BL.KioskID & " AND D_ID=" & VDM_BL.Device.Printer
            'Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
            'Dim DT As New DataTable
            'DA.Fill(DT)
            If DT.Rows.Count > 0 Then
                If Not IsDBNull(DT.Rows(0).Item("Max_Qty")) Then
                    txtRemainPaper.Text = FormatNumber(Val(DT.Rows(0).Item("Max_Qty")), 0)
                End If
            Else
                Alert(Me.Page, "ตรวจสอบ Printer Device ")
                Exit Sub
            End If
        End If


    End Sub


    Function Validate() As Boolean
        Dim result As Boolean = True
        If Val(txtMaxPaper.Text) < Val(txtRemainPaper.Text) Then
            Alert(Me.Page, "ตรวจสอบจำนวนที่พิมพ์ได้")
            result = False
        End If
        Return result
    End Function

    Function Save() As Boolean

        Dim result As Boolean = False

        'save Device
        Dim SQL As String = "SELECT * FROM TB_SHIFT_STOCK "
        SQL &= " WHERE SHIFT_ID=" & Session("SHIFT_ID") & " AND D_ID=" & VDM_BL.Device.Printer
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        Dim DR As DataRow

        If DT.Rows.Count = 0 Then
            DR = DT.NewRow
            DT.Rows.Add(DR)

        Else
            DR = DT.Rows(0)
        End If
        DR("SHIFT_ID") = Session("SHIFT_ID")
        DR("D_ID") = VDM_BL.Device.Printer
        DR("Unit_Value") = 0
        Select Case Session("SHIFT_Status")
            Case VDM_BL.ShiftStatus.Close
                DR("CLOSE_BEFORE") = IIf(txtMaxPaper.Text <> "", txtMaxPaper.Text.Replace(",", ""), DBNull.Value)
                DR("CLOSE_FINAL") = IIf(txtRemainPaper.Text <> "", txtRemainPaper.Text.Replace(",", ""), DBNull.Value)
            Case VDM_BL.ShiftStatus.Open
                DR("OPEN_BEFORE") = IIf(txtMaxPaper.Text <> "", txtMaxPaper.Text.Replace(",", ""), DBNull.Value)
                DR("OPEN_FINAL") = IIf(txtRemainPaper.Text <> "", txtRemainPaper.Text.Replace(",", ""), DBNull.Value)
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

End Class