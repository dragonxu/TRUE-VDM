Imports System.Data.SqlClient

Public Class Complete_Order
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

    Private ReadOnly Property TXN_ID As Integer
        Get
            Return Session("TXN_ID")
        End Get
    End Property

#End Region

#Region "Page Property"

    Public Property POS_ID As Integer
        Get
            Try
                Return CInt(txtPosID.Text)
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(value As Integer)
            txtPosID.Text = value
        End Set
    End Property

    Private Property SLOT_ID As Integer
        Get
            Try
                Return txtSlotID.Text
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(value As Integer)
            txtSlotID.Text = value
        End Set
    End Property

    Private Property SLOT_NAME As String
        Get
            Return txtSlotName.Text
        End Get
        Set(value As String)
            txtSlotName.Text = value
        End Set
    End Property

    Private Property SERIAL_NO As String
        Get
            Return txtSerial.Text
        End Get
        Set(value As String)
            txtSerial.Text = value
        End Set
    End Property

    Private Property Result_Status As String
        Get
            Return txtStatus.Text
        End Get
        Set(value As String)
            txtStatus.Text = value
        End Set
    End Property

    Private Property Result_Message As String
        Get
            Return txtMessage.Text
        End Get
        Set(value As String)
            txtMessage.Text = value
        End Set
    End Property

    Private Property SIM_SERIAL As String
        Get
            Return txtBarcode.Text
        End Get
        Set(value As String)
            txtBarcode.Text = value
        End Set
    End Property

    Public ReadOnly Property PRODUCT_ID As Integer
        Get
            If IsNumeric(Request.QueryString("PRODUCT_ID")) Then
                Return Request.QueryString("PRODUCT_ID")
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property SIM_ID As Integer
        Get
            If IsNumeric(Request.QueryString("SIM_ID")) Then
                Return Request.QueryString("SIM_ID")
            Else
                Return 0
            End If
        End Get
    End Property


    Private Property SLIP_CODE As String
        Get
            Try
                Return ViewState("SLIP_CODE")
            Catch ex As Exception
                Return ""
            End Try
        End Get
        Set(value As String)
            ViewState("SLIP_CODE") = value
        End Set
    End Property

    Private Property SHIFT_ID As Integer
        Get
            Try
                Return ViewState("SHIFT_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(value As Integer)
            ViewState("SHIFT_ID") = value
        End Set
    End Property

    Private Property KO_CODE As String
        Get
            Try
                Return ViewState("KO_CODE")
            Catch ex As Exception
                Return ""
            End Try
        End Get
        Set(value As String)
            ViewState("KO_CODE") = value
        End Set
    End Property

    Private Property SHOP_CODE As String
        Get
            Try
                Return ViewState("SHOP_CODE")
            Catch ex As Exception
                Return ""
            End Try
        End Get
        Set(value As String)
            ViewState("SHOP_CODE") = value
        End Set
    End Property

    Private Property SALE_CODE As String
        Get
            Try
                Return ViewState("SALE_CODE")
            Catch ex As Exception
                Return ""
            End Try
        End Get
        Set(value As String)
            ViewState("SALE_CODE") = value
        End Set
    End Property

    Private Property SHIFT_OPEN_BY As Integer
        Get
            Try
                Return ViewState("SHIFT_OPEN_BY")
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(value As Integer)
            ViewState("SHIFT_OPEN_BY") = value
        End Set
    End Property

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            initFirstTimeScript()
            '----------------------- Stop Focus Barcode ----------------------
            Dim Script As String = "stopFocusBarcode();" & vbLf
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "clearBarcode", Script, True)
            '----------------------- Stop SIM ----------------------

            txtLocalControllerURL.Text = BL.LocalControllerURL

            Dim DT As DataTable = BL.Get_Kiosk_Current_Shift(KO_ID)
            If DT.Rows.Count > 0 Then
                SHIFT_ID = DT.Rows(0).Item("SHIFT_ID")
                SALE_CODE = DT.Rows(0).Item("Open_EMP_ID").ToString
                SHIFT_OPEN_BY = DT.Rows(0).Item("Open_By")
            End If
            DT = BL.GetList_Kiosk(KO_ID)
            If DT.Rows.Count > 0 Then
                KO_CODE = DT.Rows(0).Item("KO_CODE")
                SHOP_CODE = DT.Rows(0).Item("SITE_CODE")
            End If

            If PRODUCT_ID > 0 Then
                PickUpProduct()
            Else
                PickUpSIM()
            End If

        End If

    End Sub

    Private Sub initFirstTimeScript()
        Dim Script = "breakSIMSlot(); stopFocusBarcode(); $('#btnLeaveFocus').focus();"
        txtBarcode.Attributes("onchange") = Script
    End Sub

    Private Sub PickUpProduct()

        Dim DT As DataTable = BL.Get_Next_Product_To_Pick(KO_ID, PRODUCT_ID)
        If DT.Rows.Count = 0 Then

        Else
            POS_ID = DT.Rows(0).Item("POS_ID")
            SLOT_ID = DT.Rows(0).Item("SLOT_ID")
            SLOT_NAME = DT.Rows(0).Item("SLOT_NAME")
            SERIAL_NO = DT.Rows(0).Item("SERIAL_NO")
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "PickProduct", "pickProduct();", True)
        End If
    End Sub

    Private Sub PickUpSIM()
        Dim DT As DataTable = BL.Get_Next_SIM_To_Pick(KO_ID, SIM_ID)
        If DT.Rows.Count = 0 Then

        Else
            SLOT_ID = DT.Rows(0).Item("SLOT_ID")
            SLOT_NAME = DT.Rows(0).Item("SLOT_NAME")
            Dim Script As String = "txtBarcode='" & txtBarcode.ClientID & "';" & vbLf
            Script &= "startFocusBarcode();" & vbLf
            Script &= "pullSIM();" & vbLf
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "PickSIM", Script, True)
        End If

    End Sub

    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Select_Menu.aspx")
    End Sub

    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click
        Response.Redirect("Device_Payment.aspx?PRODUCT_ID=" & PRODUCT_ID & "&SIM_ID=" & SIM_ID)
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click

        '-------------------- Product_Picked_Up------------------

        '---------------------TB_TRANSACTION_PICK----------------
        Dim SQL As String = "SELECT * FROM  TB_TRANSACTION_PICK WHERE TXN_ID=" & TXN_ID & vbLf
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        Dim DR As DataRow
        If DT.Rows.Count = 0 Then
            DR = DT.NewRow
            DT.Rows.Add(DR)
            DR("TXN_ID") = TXN_ID
            DR("ITEM_NO") = 1
        Else
            DR = DT.Rows(0)
        End If

        DR("SLOT_NAME") = SLOT_NAME
        DR("TXN_TIME") = Now

        Dim D_ID As Integer = 0
        If PRODUCT_ID <> 0 Then
            DR("PRODUCT_ID") = PRODUCT_ID
            DR("SLOT_ID") = SLOT_ID
            DR("POS_ID") = POS_ID
            DR("SERIAL_NO") = SERIAL_NO
        Else
            DR("SERIAL_NO") = SIM_SERIAL
            DR("SIM_ID") = SIM_ID
            Dim ST As DataTable = BL.Get_SIM_SLOT_INFO(KO_ID)
            ST.DefaultView.RowFilter = "SLOT_NAME='" & SLOT_NAME & "'"
            If ST.DefaultView.Count > 0 Then
                D_ID = ST.DefaultView(0).Item("D_ID")
                DR("SLOT_ID") = D_ID
            End If
            ST.Dispose()
        End If

        Try
            Dim cmd As New SqlCommandBuilder(DA)
            DA.Update(DT)
        Catch ex As Exception
            Alert(Me.Page, ex.Message)
            'Response.End()
            Exit Sub
        End Try


        '---------------- Cut-Off Stock----------------
        If PRODUCT_ID > 0 Then '------------ Product---------------
            BL.Drop_PRODUCT_STOCK_SERIAL(SLOT_ID, SERIAL_NO)
            BL.Save_Product_Movement_Log(SHIFT_ID, VDM_BL.ShiftStatus.OnGoing, PRODUCT_ID, SERIAL_NO, VDM_BL.StockMovementType.Sell, SLOT_NAME, SLOT_ID, "", 0, "ขายที่ตู้ " & SHOP_CODE & "-" & KO_CODE & " ไปเมื่อวันที่ " & Now.ToString("dd-MMM-yyyy") & " by SaleCode : " & SALE_CODE, SHIFT_OPEN_BY, Now)
        Else '------------ SIM---------------
            Try
                BL.Drop_SIM_SERIAL(KO_ID, D_ID, SIM_SERIAL)
                BL.Save_SIM_Movement_Log(SHIFT_ID, VDM_BL.ShiftStatus.OnGoing, SIM_ID, SERIAL_NO, VDM_BL.StockMovementType.Sell, SLOT_NAME, D_ID, "", 0, "ขายที่ตู้ " & SHOP_CODE & "-" & KO_CODE & " ไปเมื่อวันที่ " & Now.ToString("dd-MMM-yyyy") & " by SaleCode : " & SALE_CODE, SHIFT_OPEN_BY, Now)
            Catch ex As Exception
                Alert(Me.Page, ex.Message)
                'Response.End()
                Exit Sub
            End Try

        End If

        Response.Redirect("Thank_You.aspx")
    End Sub

    Private Sub btnBarcode_Click(sender As Object, e As EventArgs) Handles btnBarcode.Click ' ได้ Barcode หยุดเอาไว้ก่อน

        Dim Script As String = " sendSIMValidation();" & vbLf

        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "BreakSIM", Script, True)
    End Sub

    Private Sub btnValidatePrepaid_Click(sender As Object, e As EventArgs) Handles btnValidatePrepaid.Click
        ' ------------- Call Validate Prepaid -------------

        '----------- validate Success -----------------
        Dim Script As String = "sendSIMToCustomer();" & vbLf
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "ForwardSIM", Script, True)
    End Sub
End Class