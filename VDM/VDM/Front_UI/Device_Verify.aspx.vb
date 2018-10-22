Imports System.Data
Imports System.Data.SqlClient

Public Class Device_Verify
    Inherits System.Web.UI.Page
    Dim BL As New VDM_BL
    Dim C As New Converter

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

    Private ReadOnly Property SIM_ID As Integer
        Get
            If IsNumeric(Request.QueryString("SIM_ID")) Then
                Return Request.QueryString("SIM_ID")
            Else
                Return 0
            End If
        End Get
    End Property
    Private ReadOnly Property SHOP_CODE As Integer
        Get
            Dim DT As DataTable = BL.GetList_Kiosk(KO_ID)
            If DT.Rows.Count > 0 Then
                Return DT.Rows(0).Item("SITE_CODE")
            Else
                Return ""
            End If
        End Get
    End Property

    Private Property SEQ_Face_Recognition As Integer
        Get
            Try
                Return id_number.Attributes("SEQ_Face_Recognition")
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(value As Integer)
            id_number.Attributes("SEQ_Face_Recognition") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            SEQ_Face_Recognition = 1

            'popupVerifing.Visible = False
            'popupCannotVerify.Visible = False

            Dim Script As String = "Face_Recognition();"
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Face", Script, True)

        Else

        End If

    End Sub

    Private Sub btnSkip_ScanIDCard_Click(sender As Object, e As EventArgs) Handles btnSkip_ScanIDCard.Click
        'pnlScanIDCard.Visible = False  เป็นการซื้อ Device แต่ไม่ Scan บัตร User กดเอง
        'pnlScanIDCard.Visible = False
        Response.Redirect("Device_Payment.aspx?SIM_ID=" & SIM_ID)
    End Sub

    'Private Sub btnStart_Take_Photos_Click(sender As Object, e As EventArgs) Handles btnStart_Take_Photos.Click
    '    pnlScanIDCard.Visible = False
    '    pnlModul_IDCard_Success.Visible = False
    '    pnlFace_Recognition.Visible = True
    'End Sub

    'Private Sub lnkClose_Click(sender As Object, e As EventArgs) Handles lnkClose.Click
    '    pnlModul_IDCard_Success.Visible = False
    'End Sub

    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Select_Menu.aspx")
    End Sub

    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click
        Response.Redirect("SIM_Detail.aspx?SIM_ID=" & SIM_ID)
    End Sub


    Dim BackEndInterface As New BackEndInterface.Face_Recognition
    Private Sub btnFace_Recognition_Click(sender As Object, e As EventArgs) Handles btnFace_Recognition.Click

        Dim CUS_IMAGE As Byte()
        If LANGUAGE = VDM_BL.UILanguage.TH Then
            Dim cus As New VDM_BL.Customer_IDCard
            If Not IsNothing(Session("Customer_IDCard")) Then
                cus = Session("Customer_IDCard")
            End If

            id_number.Text = cus.Citizenid
            Face_cust_certificate.Text = cus.Photo
            Face_cust_capture.Text = cus.FaceCamera


            CUS_PID.Text = cus.Citizenid

            CUS_TITLE.Text = cus.Th_Prefix
            CUS_NAME.Text = cus.Th_Firstname
            CUS_SURNAME.Text = cus.Th_Lastname
            NAT_CODE.Text = ""
            CUS_GENDER.Text = cus.Sex
            CUS_BIRTHDATE.Text = cus.Birthday
            CUS_PASSPORT_ID.Text = ""
            CUS_PASSPORT_START.Text = ""
            CUS_PASSPORT_EXPIRE.Text = ""

            CUS_IMAGE = C.StringToByte(cus.Photo, 0)


        Else
            Dim cus As New VDM_BL.Customer_Passport
            If Not IsNothing(Session("Customer_Passport")) Then
                cus = Session("Customer_Passport")
            End If

            id_number.Text = cus.PassportNo
            Face_cust_certificate.Text = cus.Photo
            Face_cust_capture.Text = cus.FaceCamera

            CUS_TITLE.Text = ""
            CUS_NAME.Text = cus.FirstName
            CUS_SURNAME.Text = cus.LastName
            NAT_CODE.Text = cus.Nationality
            CUS_GENDER.Text = cus.Sex
            CUS_BIRTHDATE.Text = cus.DateOfBirth
            CUS_PASSPORT_ID.Text = cus.PassportNo
            CUS_PASSPORT_START.Text = ""
            CUS_PASSPORT_EXPIRE.Text = cus.Expire

            CUS_IMAGE = C.StringToByte(cus.Photo, 0)

        End If

        '--รอเปลี่ยนเมื่อถ่ายรูปได้
        'Face_cust_capture.Text = "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"
        '------ ถ้ามีปัญหาทุกอย่างให้เรียก Javascript : showFaceProblem();-----------------

        If SEQ_Face_Recognition <= 3 Then

            Dim result As New BackEndInterface.Face_Recognition.Response
            result = BackEndInterface.Get_Result(SHOP_CODE, id_number.Text, Face_cust_certificate.Text, Face_cust_capture.Text, SEQ_Face_Recognition)

            If Not IsNothing(result.response_data) Then
                'If result.status = "SUCCESSFUL" Then
                If result.response_data.face_recognition_result.ToLower = "pass" Then

                    ' ไปหน้าเริ่มจ่ายตัง
                    ' Insert ข้อมูลเข้า TB_CUSTOMER
                    Dim SQL As String = "SELECT * FROM TB_CUSTOMER WHERE 1=1 "
                    If CUS_PID.Text <> "" Then
                        SQL &= " AND CUS_PID=" & CUS_PID.Text
                    ElseIf CUS_PASSPORT_ID.Text <> "" Then
                        SQL &= " AND CUS_PASSPORT_ID=" & CUS_PASSPORT_ID.Text
                    End If

                    Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                    Dim DT As New DataTable
                    DA.Fill(DT)
                    Dim DR As DataRow
                    Dim CUS_ID As Integer = 0
                    If DT.Rows.Count = 0 Then
                        DR = DT.NewRow
                        DT.Rows.Add(DR)
                        CUS_ID = BL.Get_NewID("TB_CUSTOMER", "CUS_ID")
                        DR("CUS_ID") = CUS_ID
                    Else
                        DR = DT.Rows(0)
                    End If
                    DR("CUS_TITLE") = CUS_TITLE.Text
                    DR("CUS_NAME") = CUS_NAME.Text
                    DR("CUS_SURNAME") = CUS_SURNAME.Text
                    DR("NAT_CODE") = NAT_CODE.Text
                    DR("CUS_GENDER") = CUS_GENDER.Text
                    DR("CUS_BIRTHDATE") = CUS_BIRTHDATE.Text
                    DR("CUS_PID") = IIf(CUS_PID.Text <> "", CUS_PID.Text, DBNull.Value)
                    DR("CUS_PASSPORT_ID") = IIf(CUS_PASSPORT_ID.Text <> "", CUS_PASSPORT_ID.Text, DBNull.Value)
                    DR("CUS_PASSPORT_START") = IIf(CUS_PASSPORT_START.Text <> "", CUS_PASSPORT_START.Text, DBNull.Value)
                    DR("CUS_PASSPORT_EXPIRE") = IIf(CUS_PASSPORT_EXPIRE.Text <> "", CUS_PASSPORT_EXPIRE.Text, DBNull.Value)
                    DR("CUS_IMAGE") = CUS_IMAGE
                    DR("Update_Time") = Now
                    Dim cmd As New SqlCommandBuilder(DA)
                    Try
                        DA.Update(DT)
                    Catch ex As Exception
                        Alert(Me.Page, ex.Message)
                        Exit Sub
                    End Try


                    'Update TB_SERVICE_TRANSACTION
                    Dim SQL_Update As String = "UPDATE TB_SERVICE_TRANSACTION SET CUS_ID=" & CUS_ID & " WHERE TXN_ID=" & TXN_ID
                    BL.ExecuteNonQuery(SQL)

                    Response.Redirect("Device_Payment.aspx?SIM_ID=" & SIM_ID)
                Else
                    SEQ_Face_Recognition = SEQ_Face_Recognition + 1
                    ' สั่งถ่ายภาพหรือ แสกนบัตรใหม่   
                    popupVerifing.Visible = False
                    Dim Script As String = "showFaceProblem();"
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Problem", Script, True)



                End If

                ' ไม่สามารถเชื่อมต่อ Network ได้
                popupVerifing.Visible = False
                Dim Script_Network As String = "showFaceProblem();"
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Problem", Script_Network, True)
            End If

        Else
            '---Alert แสดงข้อความไม่สามารถ Verify ตัวตนได้
            '.....
            '.....
            '.....
        End If

    End Sub

    Private Sub txt_TextChanged(sender As Object, e As EventArgs) Handles id_number.TextChanged, Face_cust_certificate.TextChanged, Face_cust_capture.TextChanged,
        CUS_TITLE.TextChanged, CUS_NAME.TextChanged, CUS_SURNAME.TextChanged, NAT_CODE.TextChanged, CUS_GENDER.TextChanged, CUS_BIRTHDATE.TextChanged, CUS_PID.TextChanged, CUS_PASSPORT_ID.TextChanged, CUS_PASSPORT_START.TextChanged, CUS_PASSPORT_EXPIRE.TextChanged
        If SHOP_CODE <> "" And id_number.Text <> "" And Face_cust_certificate.Text <> "" And Face_cust_capture.Text <> "" And CUS_TITLE.Text <> "" And CUS_NAME.Text <> "" And CUS_SURNAME.Text <> "" And NAT_CODE.Text <> "" And CUS_GENDER.Text <> "" And CUS_BIRTHDATE.Text <> "" And CUS_PID.Text <> "" And CUS_PASSPORT_ID.Text <> "" And CUS_PASSPORT_START.Text <> "" And CUS_PASSPORT_EXPIRE.Text <> "" Then
            Dim Script As String = "Face_Recognition(); "
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Face_Recognition", Script, True)
        End If

    End Sub
End Class