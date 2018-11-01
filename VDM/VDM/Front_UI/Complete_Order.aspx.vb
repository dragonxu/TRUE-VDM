Imports System.Data
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

    Public Property Customer_IDCard As VDM_BL.Customer_IDCard
        Get
            If IsNothing(Session("Customer_IDCard")) Then
                Session("Customer_IDCard") = New VDM_BL.Customer_IDCard
            End If
            Return Session("Customer_IDCard")
        End Get
        Set(value As VDM_BL.Customer_IDCard)
            Session("Customer_IDCard") = value
        End Set
    End Property


    Public Property Customer_Passport As VDM_BL.Customer_Passport
        Get
            If IsNothing(Session("Customer_Passport")) Then
                Session("Customer_Passport") = New VDM_BL.Customer_Passport
            End If
            Return Session("Customer_Passport")
        End Get
        Set(value As VDM_BL.Customer_Passport)
            Session("Customer_Passport") = value
        End Set
    End Property

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            initFirstTimeScript()
            '----------------------- Stop SIM ----------------------
            '----------------- For Test ----------------
            'Session("TXN_ID") = 499
            'SIM_SERIAL = "896600401500000620"
            'Dim Result As BackEndInterface.Register.Command_Result = fn_Register()

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
                If IsNothing(Session("ssPickUpSIM")) Then Session("ssPickUpSIM") = 0
                PickUpSIM()
            End If

        End If

    End Sub

    Private Sub initFirstTimeScript()
        'txtBarcode.Attributes("onchange") = "leaveBarcode();"
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
            txtBarcode.Text = ""
            Dim Script As String = "pullSIM();" & vbLf
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

        Dim Script As String = "$('#btnFocus').focus(); sendSIMValidation();" & vbLf

        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "BreakSIM", Script, True)
    End Sub

    Private Sub btnValidatePrepaid_Click(sender As Object, e As EventArgs) Handles btnValidatePrepaid.Click
        ' ------------- Call Validate Prepaid -------------
        If fn_Register.Status Then
            Dim SQL_Update As String = "UPDATE TB_SERVICE_TRANSACTION SET  TSM_Result='" & "SIM_Serial:" & SIM_SERIAL & ": " & fn_Register.Message & "' WHERE TXN_ID=" & TXN_ID
            BL.ExecuteNonQuery(SQL_Update)
            '----------- validate Success -----------------
            Dim Script As String = "sendSIMToCustomer();" & vbLf
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "ForwardSIM", Script, True)

        Else
            '----------- validate Unsuccess -----------------แอมเพิ่ม
            Dim Message As String = fn_Register.Message
            If Mid(Message, InStr(Message, "INVALID SERIAL"), "INVALID SERIAL".Length) = "INVALID SERIAL" Then
                Session("ssPickUpSIM") = Session("ssPickUpSIM") + 1
                If Session("ssPickUpSIM") < 3 Then '--ปล่อยให้ Register SIM ครบ 3 ครั้งโดยไม่ ไม่ได้ Error เลย
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Refresh", "location.href=location.href;", True)
                    Exit Sub
                End If
            End If

            '---------ถ้าไม่ผ่าน แสดง Dialog และออก Slip Error
            Dim SQL_Update As String = "UPDATE TB_SERVICE_TRANSACTION SET  TSM_Result='" & "SIM_Serial:" & SIM_SERIAL & ": " & fn_Register.Message & "' WHERE TXN_ID=" & TXN_ID
            BL.ExecuteNonQuery(SQL_Update)
            lblMSG.Text = fn_Register.Message.ToString
            Dim Script As String = "$(""#clickCannotRegister"").click();"
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "register", Script, True)
        End If

    End Sub

    Dim BackEndInterface As New BackEndInterface.Register


    'Private Function PerpaidRegister(ByVal CUS_TITLE As String,
    '                                ByVal CUS_NAME As String,
    '                                ByVal CUS_SURNAME As String,
    '                                ByVal NAT_CODE As String,
    '                                ByVal CUS_GENDER As String,
    '                                ByVal CUS_BIRTHDATE As String,
    '                                ByVal CUS_PID As String,
    '                                ByVal CUS_PASSPORT_ID As String,
    '                                ByVal CUS_PASSPORT_EXPIRE As String,
    '                                ByVal Base64_Certificate As String,
    '                                ByVal Base64_capture As String,
    '                                ByVal face_recognition_result As String,
    '                                ByVal is_identical As String,
    '                                ByVal confident_ratio As String,
    '                                ByVal address_number As String,
    '                                ByVal address_moo As String,
    '                                ByVal address_village As String,
    '                                ByVal address_street As String,
    '                                ByVal address_soi As String,
    '                                ByVal address_district As String,
    '                                ByVal address_province As String,
    '                                ByVal address_building_name As String,
    '                                ByVal address_building_room As String,
    '                                ByVal address_building_floor As String,
    '                                ByVal sddress_sub_district As String,
    '                                ByVal address_zip As String) As BackEndInterface.Register.Command_Result

    '    Dim Result As BackEndInterface.Register.Command_Result = BackEndInterface.Get_Result(Base64_Certificate, Base64_capture, SERIAL_NO, KO_ID, Session("User_ID"), TXN_ID, LANGUAGE)


    '    )
    'End Function

    Private Function fn_Register() As BackEndInterface.Register.Command_Result
        Dim CUS_ID As Integer = 0
        If LANGUAGE = VDM_BL.UILanguage.TH Then
            CUS_ID = Customer_IDCard.CUS_ID
        Else
            CUS_ID = Customer_Passport.CUS_ID
        End If

        Dim SQL As String = "SELECT * FROM VW_CUSTOMER_DOC WHERE CUS_ID=" & CUS_ID
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        Dim CUS_NAME As String = "-"
        Dim CUS_SURNAME As String = "-"
        Dim NAT_CODE As String = "-"
        Dim CUS_GENDER As String = "-"
        Dim CUS_BIRTHDATE As String = "-"
        Dim CUS_PID As String = "-"
        Dim DOC_TYPE As String = "-"
        Dim CUS_DOC_EXPIRE As String = "-"
        Dim Base64_Certificate As String = "-"
        Dim Base64_capture As String = "-"
        Dim Face_Recognition_Result As String = "-"
        Dim is_identical As String = "-"
        Dim Confident_Ratio As String = "-"
        Dim address_number As String = "-"
        Dim address_moo As String = "-"
        Dim address_village As String = "-"
        Dim address_street As String = "-"
        Dim address_soi As String = "-"
        Dim address_district As String = "-"
        Dim address_province As String = "-"
        Dim address_building_name As String = "-"
        Dim address_building_room As String = "-"
        Dim address_building_floor As String = "-"
        Dim sddress_sub_district As String = "-"
        Dim address_zip As String = "-"

        If DT.Rows(0).Item("CUS_NAME").ToString <> "" Then CUS_NAME = DT.Rows(0).Item("CUS_NAME").ToString
        If DT.Rows(0).Item("CUS_SURNAME").ToString <> "" Then CUS_SURNAME = DT.Rows(0).Item("CUS_SURNAME").ToString
        If DT.Rows(0).Item("NAT_CODE").ToString <> "" Then NAT_CODE = DT.Rows(0).Item("NAT_CODE").ToString
        If DT.Rows(0).Item("CUS_GENDER").ToString <> "" Then CUS_GENDER = DT.Rows(0).Item("CUS_GENDER").ToString
        If DT.Rows(0).Item("CUS_BIRTHDATE").ToString <> "" Then CUS_BIRTHDATE = DT.Rows(0).Item("CUS_BIRTHDATE").ToString
        If DT.Rows(0).Item("CUS_PID").ToString <> "" Then CUS_PID = DT.Rows(0).Item("CUS_PID").ToString
        If DT.Rows(0).Item("DOC_TYPE").ToString <> "" Then DOC_TYPE = DT.Rows(0).Item("DOC_TYPE").ToString
        If DT.Rows(0).Item("CUS_DOC_EXPIRE").ToString <> "" Then CUS_DOC_EXPIRE = DT.Rows(0).Item("CUS_DOC_EXPIRE").ToString
        If DT.Rows(0).Item("Base64_Certificate").ToString <> "" Then Base64_Certificate = DT.Rows(0).Item("Base64_Certificate").ToString
        If DT.Rows(0).Item("Base64_capture").ToString <> "" Then Base64_capture = DT.Rows(0).Item("Base64_capture").ToString
        If DT.Rows(0).Item("Face_Recognition_Result").ToString <> "" Then Face_Recognition_Result = DT.Rows(0).Item("Face_Recognition_Result").ToString
        If DT.Rows(0).Item("is_identical").ToString <> "" Then is_identical = DT.Rows(0).Item("is_identical").ToString
        If DT.Rows(0).Item("Confident_Ratio").ToString <> "" Then Confident_Ratio = DT.Rows(0).Item("Confident_Ratio").ToString
        If DT.Rows(0).Item("address_number").ToString <> "" Then address_number = DT.Rows(0).Item("address_number").ToString
        If DT.Rows(0).Item("address_moo").ToString <> "" Then address_moo = DT.Rows(0).Item("address_moo").ToString
        If DT.Rows(0).Item("address_village").ToString <> "" Then address_village = DT.Rows(0).Item("address_village").ToString
        If DT.Rows(0).Item("address_street").ToString <> "" Then address_street = DT.Rows(0).Item("address_street").ToString
        If DT.Rows(0).Item("address_soi").ToString <> "" Then address_soi = DT.Rows(0).Item("address_soi").ToString
        If DT.Rows(0).Item("address_district").ToString <> "" Then address_district = DT.Rows(0).Item("address_district").ToString
        If DT.Rows(0).Item("address_province").ToString <> "" Then address_province = DT.Rows(0).Item("address_province").ToString
        If DT.Rows(0).Item("address_building_name").ToString <> "" Then address_building_name = DT.Rows(0).Item("address_building_name").ToString
        If DT.Rows(0).Item("address_building_room").ToString <> "" Then address_building_room = DT.Rows(0).Item("address_building_room").ToString
        If DT.Rows(0).Item("address_building_floor").ToString <> "" Then address_building_floor = DT.Rows(0).Item("address_building_floor").ToString
        If DT.Rows(0).Item("sddress_sub_district").ToString <> "" Then sddress_sub_district = DT.Rows(0).Item("sddress_sub_district").ToString
        If DT.Rows(0).Item("address_zip").ToString <> "" Then address_zip = DT.Rows(0).Item("address_zip").ToString

        Dim Result As BackEndInterface.Register.Command_Result
        If DT.Rows.Count > 0 Then
            Dim TXN_Code As String = BL.GET_TXN_CODE(TXN_ID)
            Result = BackEndInterface.Get_Result("-",
                                                    CUS_NAME,
                                                    CUS_SURNAME,
                                                    NAT_CODE,
                                                    CUS_GENDER,
                                                    CUS_BIRTHDATE,
                                                    CUS_PID,
                                                    DOC_TYPE,
                                                    CUS_DOC_EXPIRE,
                                                    Base64_Certificate,
                                                    Base64_capture,
                                                    Face_Recognition_Result,
                                                    is_identical,
                                                    Confident_Ratio,
                                                    address_number,
                                                    address_moo,
                                                    address_village,
                                                    address_street,
                                                    address_soi,
                                                    address_district,
                                                    address_province,
                                                    address_building_name,
                                                    address_building_room,
                                                    address_building_floor,
                                                    sddress_sub_district,
                                                    address_zip,
                                                    SIM_SERIAL,
                                                    KO_ID,
                                                    SHOP_CODE,
                                                    SHIFT_OPEN_BY,
                                                    TXN_ID,
                                                    TXN_Code)

        Else
            '-ไม่มีรายการ Customer

        End If



        Return Result
    End Function

    Private Sub lnkprint_ServerClick(sender As Object, e As EventArgs) Handles lnkprint.ServerClick
        'Print Slip และกลับหน้าหลัก 
        Print()
        Response.Redirect("Select_Language.aspx")

    End Sub

    Private Sub Print()

        Dim C As New Converter

        Dim SQL As String = "SELECT * FROM TB_SERVICE_TRANSACTION" & vbLf
        SQL &= "WHERE TXN_ID=" & TXN_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        If DT.Rows.Count = 0 OrElse IsDBNull(DT.Rows(0).Item("METHOD_ID")) Then Exit Sub

        Dim Script As String = "setTimeout( function(){ printSlip(" & TXN_ID & "); } , 1000);"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Print", Script, True)
    End Sub

End Class