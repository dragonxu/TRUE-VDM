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
            pnlScanIDCard.Visible = True
            pnlFace_Recognition.Visible = False

            pnlModul_IDCard_Success.Visible = True
            SEQ_Face_Recognition = 1
        Else

        End If

    End Sub

    Private Sub btnSkip_ScanIDCard_Click(sender As Object, e As EventArgs) Handles btnSkip_ScanIDCard.Click
        'pnlScanIDCard.Visible = False  เป็นการซื้อ Device แต่ไม่ Scan บัตร User กดเอง
        'pnlScanIDCard.Visible = False
        Response.Redirect("Device_Payment.aspx?SIM_ID=" & SIM_ID)
    End Sub

    Private Sub btnStart_Take_Photos_Click(sender As Object, e As EventArgs) Handles btnStart_Take_Photos.Click
        pnlScanIDCard.Visible = False
        pnlModul_IDCard_Success.Visible = False
        pnlFace_Recognition.Visible = True
    End Sub

    Private Sub lnkClose_Click(sender As Object, e As EventArgs) Handles lnkClose.Click
        pnlModul_IDCard_Success.Visible = False
    End Sub

    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Select_Menu.aspx")
    End Sub

    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click
        Response.Redirect("SIM_Detail.aspx?SIM_ID=" & SIM_ID)
    End Sub


    Dim BackEndInterface As New BackEndInterface.Face_Recognition
    Private Sub btnFace_Recognition_Click(sender As Object, e As EventArgs) Handles btnFace_Recognition.Click
        If SEQ_Face_Recognition <= 3 Then

            Dim result As New BackEndInterface.Face_Recognition.Response
            result = BackEndInterface.Get_Result(SHOP_CODE, id_number.Text, Face_cust_certificate.Text, Face_cust_capture.Text, SEQ_Face_Recognition)

            If Not IsNothing(result) Then
                If result.status = "SUCCESSFUL" Then
                    ' ไปหน้าเริ่มจ่ายตัง
                    ' Insert ข้อมูลเข้า TB_CUSTOMER
                    Dim SQL As String = "SELECT * FROM TB_CUSTOMER WHERE CUS_PID=" & CUS_PID.Text & " OR CUS_PASSPORT_ID=" & CUS_PASSPORT_ID.Text
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
                    DR("CUS_PID") = CUS_PID.Text
                    DR("CUS_PASSPORT_ID") = CUS_PASSPORT_ID.Text
                    DR("CUS_PASSPORT_START") = CUS_PASSPORT_START.Text
                    DR("CUS_PASSPORT_EXPIRE") = CUS_PASSPORT_EXPIRE.Text
                    DR("CUS_IMAGE") = CUS_IMAGE.Text
                    DR("Update_Time") = Now

                    'Update TB_SERVICE_TRANSACTION
                    Dim SQL_Update As String = "UPDATE TB_SERVICE_TRANSACTION SET CUS_ID=" & CUS_ID & " WHERE TXN_ID=" & TXN_ID
                    BL.ExecuteNonQuery(SQL)

                    Response.Redirect("Device_Payment.aspx?SIM_ID=" & SIM_ID)
                Else
                    SEQ_Face_Recognition = SEQ_Face_Recognition + 1
                    ' สั่งถ่ายภาพหรือ แสกนบัตรใหม่  
                    '.....
                    '.....
                    '.....
                End If

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