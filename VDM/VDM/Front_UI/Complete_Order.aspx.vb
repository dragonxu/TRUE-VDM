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

    Public ReadOnly Property PRODUCT_ID As Integer
        Get
            Return CInt(Request.QueryString("PRODUCT_ID"))
        End Get
    End Property

    Public ReadOnly Property SIM_ID As Integer
        Get
            Try
                Return Request.QueryString("SIM_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    'Private Property PRODUCT_NAME As String
    '    Get
    '        Try
    '            Return ViewState("PRODUCT_NAME")
    '        Catch ex As Exception
    '            Return ""
    '        End Try
    '    End Get
    '    Set(value As String)
    '        ViewState("PRODUCT_NAME") = value
    '    End Set
    'End Property

    'Private Property PRODUCT_CODE As String
    '    Get
    '        Try
    '            Return ViewState("PRODUCT_CODE")
    '        Catch ex As Exception
    '            Return ""
    '        End Try
    '    End Get
    '    Set(value As String)
    '        ViewState("PRODUCT_CODE") = value
    '    End Set
    'End Property

    'Private Property PRICE As Integer
    '    Get
    '        Try
    '            Return ViewState("PRICE")
    '        Catch ex As Exception
    '            Return 0
    '        End Try
    '    End Get
    '    Set(value As Integer)
    '        ViewState("PRICE") = value
    '    End Set
    'End Property

    'Private Property KO_CODE As String
    '    Get
    '        Try
    '            Return ViewState("KO_CODE")
    '        Catch ex As Exception
    '            Return ""
    '        End Try
    '    End Get
    '    Set(value As String)
    '        ViewState("KO_CODE") = value
    '    End Set
    'End Property

    'Private Property SITE_ID As Integer
    '    Get
    '        Try
    '            Return ViewState("SITE_ID")
    '        Catch ex As Exception
    '            Return 0
    '        End Try
    '    End Get
    '    Set(value As Integer)
    '        ViewState("SITE_ID") = value
    '    End Set
    'End Property

    'Private Property SITE_CODE As String
    '    Get
    '        Try
    '            Return ViewState("SITE_CODE")
    '        Catch ex As Exception
    '            Return ""
    '        End Try
    '    End Get
    '    Set(value As String)
    '        ViewState("SITE_CODE") = value
    '    End Set
    'End Property

    'Private Property SITE_NAME As String
    '    Get
    '        Try
    '            Return ViewState("SITE_NAME")
    '        Catch ex As Exception
    '            Return ""
    '        End Try
    '    End Get
    '    Set(value As String)
    '        ViewState("SITE_NAME") = value
    '    End Set
    'End Property

    'Private Property SHIFT_ID As Integer
    '    Get
    '        Try
    '            Return ViewState("SHIFT_ID")
    '        Catch ex As Exception
    '            Return 0
    '        End Try
    '    End Get
    '    Set(value As Integer)
    '        ViewState("SHIFT_ID") = value
    '    End Set
    'End Property

    Private ReadOnly Property METHOD_ID As VDM_BL.PaymentMethod
        Get
            Try
                Return Request.QueryString("METHOD_ID")
            Catch ex As Exception
                Return VDM_BL.PaymentMethod.UNKNOWN
            End Try
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

    End Sub

    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Select_Menu.aspx")
    End Sub

    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click
        Response.Redirect("Device_Payment.aspx?PRODUCT_ID=" & PRODUCT_ID)
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click

        '-------------------- Product_Picked_Up------------------

        '---------------------TB_TRANSACTION_PICK----------------
        Dim SQL As String = "SELECT Top 0 * FROM  TB_TRANSACTION_PICK" & vbLf
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        Dim DR As DataRow = DT.NewRow

        DR("TXN_ID") = TXN_ID
        DR("ITEM_NO") = 1
        DR("PRODUCT_ID") = PRODUCT_ID
        DR("SERIAL_NO") = SERIAL_NO
        DR("SLOT_ID") = SLOT_ID
        DR("SLOT_NAME") = SLOT_NAME
        DR("POS_ID") = POS_ID
        DR("TXN_TIME") = Now
        DT.Rows.Add(DR)
        Dim cmd As New SqlCommandBuilder(DA)
        DA.Update(DT)

        '---------------- Cut-Off Stock----------------
        BL.Drop_PRODUCT_STOCK_SERIAL(SLOT_ID, SERIAL_NO)
        BL.Save_Product_Movement_Log(SHIFT_ID, VDM_BL.ShiftStatus.OnGoing, PRODUCT_ID, SERIAL_NO, VDM_BL.StockMovementType.Sell, SLOT_NAME, SLOT_ID, "", 0, "ขายที่ตู้ " & SHOP_CODE & "-" & KO_CODE & " ไปเมื่อวันที่ " & Now.ToString("dd-MMM-yyyy") & " by SaleCode : " & SALE_CODE, SHIFT_OPEN_BY, Now)

        '---------------- Create Slip Content -----------
        Select Case METHOD_ID
            Case VDM_BL.PaymentMethod.CASH


            Case VDM_BL.PaymentMethod.TRUE_MONEY

        End Select
        '------------------ ไปหน้า พิมพ์ใบเสร็จและทอนเงิน ----------
        Response.Redirect("Thank_You.aspx")
    End Sub




End Class