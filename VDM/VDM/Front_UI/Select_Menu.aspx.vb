Imports System.Data
Imports System.Data.SqlClient
Public Class Select_Menu
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL
#Region "ส่วนที่เหมือนกันหมดทุกหน้า"
    Private ReadOnly Property KO_ID As Integer '------------- เอาไว้เรียกใช้ง่ายๆ ----------
        Get
            Try
                Return Request.Cookies("KO_ID").Value
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Private ReadOnly Property LANGUAGE As VDM_BL.UILanguage '------- ต้องเป็น ReadOnly --------
        Get
            Try
                Return Session("LANGUAGE")
            Catch ex As Exception
                Return 0
            End Try

        End Get
    End Property

    Private ReadOnly Property TXN_ID As Integer
        Get
            Try
                Return Session("TXN_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property


#End Region

    Private ReadOnly Property QTY_Device As Integer '------------- เอาไว้เรียกใช้ง่ายๆ ----------
        Get
            Try
                Dim DT As DataTable = Session("DT_QTY_Product")
                Dim QTY As Integer = 0
                For i As Integer = 0 To DT.Rows.Count - 1
                    If VDM_BL.Category.Accessories <> DT.Rows(i).Item("CAT_ID") Then
                        QTY = 1
                    End If
                Next
                Return QTY
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property
    Private ReadOnly Property QTY_Accessories As Integer '------------- เอาไว้เรียกใช้ง่ายๆ ----------
        Get
            Try
                Dim DT As DataTable = Session("DT_QTY_Product")
                Dim QTY As Integer = 0
                For i As Integer = 0 To DT.Rows.Count - 1
                    If VDM_BL.Category.Accessories = DT.Rows(i).Item("CAT_ID") Then
                        QTY = 1
                    End If
                Next
                Return QTY
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property
    Private ReadOnly Property QTY_SIM As Integer '------------- เอาไว้เรียกใช้ง่ายๆ ----------
        Get
            Try
                Dim DT As DataTable = BL.GetList_Current_SIM_Kiosk(KO_ID)
                Dim QTY As Integer = 0
                If DT.Rows.Count > 0 Then QTY = 1
                Return QTY
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsNumeric(Session("LANGUAGE")) Then
            Response.Redirect("Select_Language.aspx")
        End If

        BindProduct()
        lnkDevice.Visible = False
        lnkAccessories.Visible = False
        lnkSim.Visible = False
        If QTY_Device > 0 Then lnkDevice.Visible = True
        If QTY_Accessories > 0 Then lnkAccessories.Visible = True
        If QTY_SIM > 0 Then lnkSim.Visible = True

        If QTY_Device + QTY_Accessories + QTY_SIM = 1 Then
            If QTY_Device > 0 Then
                Response.Redirect("Device_Brand.aspx?CAT_ID=" & VDM_BL.Category.Mobile)
            ElseIf QTY_Accessories > 0 Then
                Response.Redirect("Device_Brand.aspx?CAT_ID=" & VDM_BL.Category.Accessories)
            ElseIf QTY_SIM > 0 Then
                Response.Redirect("SIM_List.aspx")
            End If
        End If

        DT_CONTROL = UI_CONTROL()
        Bind_CONTROL()
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
        If LANGUAGE > VDM_BL.UILanguage.EN Then
            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_Device.Text & "'"
            lblUI_Device.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_Device.Text)
            'DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_Device_Desc.Text & "'"
            'lblUI_Device_Desc.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_Device_Desc.Text)

            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_Accessories.Text & "'"
            lblUI_Accessories.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_Accessories.Text)
            'DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_Accessories_Desc.Text & "'"
            'lblUI_Accessories_Desc.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_Accessories_Desc.Text)

            DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_SIM.Text & "'"
            lblUI_SIM.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_SIM.Text)
            'DT_CONTROL.DefaultView.RowFilter = "DISPLAY_TH='" & lblUI_SIM_Desc.Text & "'"
            'lblUI_SIM_Desc.Text = IIf(DT_CONTROL.DefaultView.Count > 0, DT_CONTROL.DefaultView(0).Item("DISPLAY").ToString, lblUI_SIM_Desc.Text)

            lblUI_Device.CssClass = "UI"
            lblUI_Accessories.CssClass = "UI"
            lblUI_SIM.CssClass = "UI"

        End If
    End Sub

#End Region

    Public Sub BindProduct()
        Dim SQL As String = ""
        SQL &= " Select  DISTINCT ISNULL(VW_ALL_PRODUCT.CAT_ID ,1) CAT_ID  " & vbLf
        SQL &= " From VW_ALL_PRODUCT  " & vbLf
        SQL &= " INNER Join VW_CURRENT_PRODUCT_STOCK On VW_CURRENT_PRODUCT_STOCK.PRODUCT_ID= VW_ALL_PRODUCT.PRODUCT_ID  " & vbLf
        SQL &= " WHERE VW_CURRENT_PRODUCT_STOCK.KO_ID =" & KO_ID & "   " & vbLf
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Session("DT_QTY_Product") = DT
    End Sub


    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click
        Response.Redirect("Select_Language.aspx")
    End Sub

    Private Sub lnkDevice_Click(sender As Object, e As EventArgs) Handles lnkDevice.Click
        Response.Redirect("Device_Brand.aspx?CAT_ID=" & VDM_BL.Category.Mobile)
    End Sub
    Private Sub lnkAccessories_Click(sender As Object, e As EventArgs) Handles lnkAccessories.Click
        Response.Redirect("Device_Brand.aspx?CAT_ID=" & VDM_BL.Category.Accessories)
    End Sub

    Private Sub lnkSim_Click(sender As Object, e As EventArgs) Handles lnkSim.Click
        Response.Redirect("SIM_List.aspx")
    End Sub


End Class