Imports System.Data.SqlClient

Public Class Manage_Machine
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL

    Dim SimList As DataTable
    Dim DeviceList As DataTable
    Dim StatusList As DataTable
    Dim ProductData As DataTable

    Private Property KO_ID As Integer
        Get
            Try
                Return ViewState("KO_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(value As Integer)
            ViewState("KO_ID") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNumeric(Session("USER_ID")) Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('Please Login'); window.location.href='SignIn.aspx';", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            ResetPage(Nothing, Nothing)
            initFirstTimeJavascript()
        Else
            initFormPlugin()
        End If

    End Sub

    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Private Sub initFirstTimeJavascript()

    End Sub

    Private Sub BindList()

        Dim DT As DataTable = BL.GetList_Kiosk
        DeviceList = BL.GetList_Kiosk_Device
        StatusList = BL.GetList_Device_Status

        rptList.DataSource = DT
        rptList.DataBind()

        lblTotalList.Text = FormatNumber(DT.Rows.Count, 0)

        pnlList.Visible = True
        pnlEdit.Visible = False

    End Sub

    Private Sub rptList_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptList.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim imgKioskIcon As Image = e.Item.FindControl("imgKioskIcon")
        Dim lblKioskCode As Label = e.Item.FindControl("lblKioskCode")
        Dim lblKOID As Label = e.Item.FindControl("lblKOID")
        Dim lblSite As Label = e.Item.FindControl("lblSite")
        Dim lblZone As Label = e.Item.FindControl("lblZone")
        Dim lblTotalProduct As Label = e.Item.FindControl("lblTotalProduct")

        Dim btnConsole As LinkButton = e.Item.FindControl("btnConsole")
        Dim btnEdit As LinkButton = e.Item.FindControl("btnEdit")
        Dim btnDelete As Button = e.Item.FindControl("btnDelete")

        If e.Item.DataItem("IsOnline") Then
            imgKioskIcon.ImageUrl = "images/Icon/koisk_ok.png"
        Else
            imgKioskIcon.ImageUrl = "images/Icon/koisk_ab.png"
        End If

        lblKioskCode.Text = e.Item.DataItem("KO_CODE").ToString
        lblKOID.Text = e.Item.DataItem("KO_ID").ToString
        lblSite.Text = e.Item.DataItem("SITE_CODE").ToString
        lblZone.Text = e.Item.DataItem("ZONE").ToString
        lblTotalProduct.Text = FormatNumber(e.Item.DataItem("Total_Product"), 0)

        '--------------- SIM Slot Level---------------------
        Dim SIM_STOCK As DataTable = BL.Get_Current_SIM_Stock(KO_ID)
        SIM_STOCK.Columns("CURRENT").ColumnName = "SLOT_NAME"
        BL.Bind_SIMDispenser_Stock(Dispenser, SIM_STOCK)

        '--------------- Peripheral UI ---------------
        DeviceList.DefaultView.RowFilter = "KO_ID=" & e.Item.DataItem("KO_ID")
        Dim Peripheral As UC_Peripheral_UI = e.Item.FindControl("Peripheral")
        Peripheral.BindPeripheral(DeviceList.DefaultView.ToTable.Copy, StatusList)

        '-----------Money Stock Level ---------------
        DeviceList.DefaultView.RowFilter = "KO_ID=" & e.Item.DataItem("KO_ID") & " AND Max_Qty IS NOT NULL AND Warning_Qty IS NOT NULL AND Critical_Qty IS NOT NULL"
        Dim MoneyStock As UC_MoneyStock_UI = e.Item.FindControl("MoneyStock")
        MoneyStock.BindMoneyStock(DeviceList.DefaultView.ToTable.Copy)

        '------------- Set Button By Authorize Level -----------
        btnConsole.CommandArgument = e.Item.DataItem("KO_ID")
        btnEdit.CommandArgument = e.Item.DataItem("KO_ID")
        btnDelete.CommandArgument = e.Item.DataItem("KO_ID")
        Dim btnPreDelete As HtmlAnchor = e.Item.FindControl("btnPreDelete")
        btnPreDelete.Attributes("onclick") = "if(confirm('ยืนยันลบ " & lblKioskCode.Text.Replace("'", "") & " ?'))$('#" & btnDelete.ClientID & "').click();"

    End Sub

    Private Sub rptList_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptList.ItemCommand
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim lblKioskCode As Label = e.Item.FindControl("lblKioskCode")

        Select Case e.CommandName
            Case "Console"

                Dim lblSite As Label = e.Item.FindControl("lblSite")
                Session("KO_ID") = e.CommandArgument
                Session("SHOP_CODE") = lblSite.Text
                'Response.Redirect("Machine_Console/Machine_Overview.aspx")
                Response.Redirect("Front_UI/Default.aspx?KO_ID=" & Session("KO_ID"))
            Case "Setting"

                Dim DT As DataTable = BL.GetList_Kiosk(e.CommandArgument)
                If DT.Rows.Count = 0 Then
                    Message_Toastr(lblKioskCode.Text.Replace("'", """") & " ไม่พบเครื่อง Vending ดังกล่าว!", ToastrMode.Warning, ToastrPositon.TopRight, Me.Page)
                    Exit Sub
                End If

                ClearEditForm()
                KO_ID = e.CommandArgument
                lblCode.Text = " : " & DT.Rows(0).Item("KO_Code") & " "
                txtCode.Text = DT.Rows(0).Item("KO_Code")
                BL.Bind_DDL_Site(ddlSite, DT.Rows(0).Item("SITE_ID"))
                txtZone.Text = DT.Rows(0).Item("ZONE").ToString
                chkActive.Checked = DT.Rows(0).Item("Active_Status")
                lblEditMode.Text = "Edit"

                pnlList.Visible = False
                pnlEdit.Visible = True

                '------------ Setting Shelf ---------------
                Shelf.KO_ID = KO_ID
                Shelf.BindData()

                '------------ Bind SIM Stock --------------
                Dispenser.KO_ID = KO_ID
                Dim STOCK As DataTable = BL.Get_Current_SIM_Stock(KO_ID)
                STOCK.Columns("CURRENT").ColumnName = "SLOT_NAME"
                BL.Bind_SIMDispenser_Stock(Dispenser, STOCK)


            Case "Delete"
                BL.Drop_Kiosk(e.CommandArgument)
                BindList()
                Message_Toastr("ลบ " & lblKioskCode.Text.Replace("'", """") & " สำเร็จ!", ToastrMode.Success, ToastrPositon.TopRight, Me.Page)
        End Select
    End Sub

    Protected Sub ResetPage(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        BindList()
    End Sub

    Private Sub ClearEditForm()
        KO_ID = 0

        lblCode.Text = " (New)"
        txtCode.Text = ""
        BL.Bind_DDL_Site(ddlSite)
        txtZone.Text = ""

        '---------------- Manage Shelf -------------------
        Shelf.PixelPerMM = 0.25
        Shelf.KO_ID = KO_ID
        Shelf.BindDefaultTemplateLayout()

        '------------------ Manage SIM Slot -----------------
        BindDefaultSIM()

        ''------- Manage Material Stock Control Level --------
        'BindMaterialStock()

        ''---------------- Manage Ads Rotator ----------------
        'BindAds()

        chkActive.Checked = True
        lblEditMode.Text = "Add"

        pnlList.Visible = True
        pnlEdit.Visible = False
    End Sub


    Private Sub BindDefaultSIM()
        Dispenser.ClearAllSlot()

        '--------------- Bind Layout ------------
        Dim SQL As String = "SELECT D.D_ID DEVICE_ID,D_Name SLOT_NAME,D.Max_Qty MAX_CAPACITY" & vbLf
        SQL &= " FROM MS_DEVICE D" & vbLf
        SQL &= " WHERE D.D_ID In (11,12,13) " & vbLf
        SQL &= " ORDER BY DEVICE_ID" & vbLf
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)

        If DT.Rows.Count = 0 Then Exit Sub

        For i As Integer = 0 To DT.Rows.Count - 1
            Dispenser.AddSlot(DT.Rows(i).Item("SLOT_NAME"), DT.Rows(i).Item("DEVICE_ID"), DT.Rows(i).Item("MAX_CAPACITY"), 0, "", 0, False, True,
                              Drawing.Color.Green, UC_Product_Slot.HighLightMode.None, 3)
        Next

        '----------- Bind Stock ------------
        DT = BL.Get_Current_SIM_Stock(KO_ID)
        DT.Columns("CURRENT").ColumnName = "SLOT_NAME"
        BL.Bind_SIMDispenser_Stock(Dispenser, DT)

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ClearEditForm()

        pnlList.Visible = False
        pnlEdit.Visible = True
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtCode.Text = "" Then
            Message_Toastr("กรอก Machine Code", ToastrMode.Warning, ToastrPositon.TopRight, Me.Page)
            Exit Sub
        End If

        If ddlSite.SelectedIndex < 1 Then
            Message_Toastr("เลือก Site", ToastrMode.Warning, ToastrPositon.TopRight, Me.Page)
            Exit Sub
        End If

        Dim DT As DataTable = BL.GetList_Kiosk()
        DT.DefaultView.RowFilter = "KO_Code='" & txtCode.Text.Replace("'", "''") & "' AND KO_ID<>" & KO_ID
        If DT.DefaultView.Count > 0 Then
            Message_Toastr("Kiosk Code ซ้ำ", ToastrMode.Warning, ToastrPositon.TopRight, Me.Page)
            Exit Sub
        End If

        Dim Sql As String = "SELECT * FROM MS_KIOSK WHERE KO_ID=" & KO_ID
        DT = New DataTable
        Dim DA As New SqlDataAdapter(Sql, BL.ConnectionString)
        DA.Fill(DT)
        Dim DR As DataRow
        If DT.Rows.Count = 0 Then
            DR = DT.NewRow
            DT.Rows.Add(DR)
            KO_ID = GetNewID()
            DR("KO_ID") = KO_ID
            DR("IsOnline") = False
        Else
            DR = DT.Rows(0)
        End If

        DR("SITE_ID") = ddlSite.Items(ddlSite.SelectedIndex).Value
        If txtZone.Text <> "" Then
            DR("ZONE") = txtZone.Text
        Else
            DR("ZONE") = DBNull.Value
        End If
        DR("KO_CODE") = txtCode.Text
        DR("Active_Status") = chkActive.Checked
        DR("Update_By") = Session("USER_ID")
        DR("Update_Time") = Now

        Dim cmd As New SqlCommandBuilder(DA)
        Try
            DA.Update(DT)
        Catch ex As Exception
            Message_Toastr(ex.Message, ToastrMode.Danger, ToastrPositon.TopRight, Me.Page)
            Exit Sub
        End Try

        '-------------- Save Product Shelf -------------
        Shelf.KO_ID = KO_ID
        If Not Shelf.SaveData Then
            Message_Toastr("ไม่สามารถบันทึก Layout ของ Product Shelf", ToastrMode.Danger, ToastrPositon.TopRight, Me.Page)
            Exit Sub
        End If

        '-------------- Save SIM Slot-------------
        Sql = "SELECT * FROM TB_KIOSK_DEVICE WHERE KO_ID=" & KO_ID
        DT = New DataTable
        DA = New SqlDataAdapter(Sql, BL.ConnectionString)
        DA.Fill(DT)
        For i As Integer = 0 To Dispenser.Slots.Count - 1
            DT.DefaultView.RowFilter = "D_ID=" & Dispenser.Slots(i).DEVICE_ID
            DR = DT.NewRow
            DR("KO_ID") = KO_ID
            DR("D_ID") = Dispenser.Slots(i).DEVICE_ID
            DR("Current_Qty") = 0
            DR("DT_ID") = 8
            DR("DS_ID") = 2
            DR("Update_Time") = Now
            DT.Rows.Add(DR)
            cmd = New SqlCommandBuilder(DA)
            DA.Update(DT)
        Next

        Message_Toastr("บันทึกสำเร็จ", ToastrMode.Success, ToastrPositon.TopRight, Me.Page)
        ResetPage(Nothing, Nothing)
    End Sub

    Private Function GetNewID() As Integer
        Dim SQL As String = "SELECT IsNull(MAX(KO_ID),0)+1 FROM MS_KIOSK "
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT.Rows(0).Item(0)
    End Function

End Class