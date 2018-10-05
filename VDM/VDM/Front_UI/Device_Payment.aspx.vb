Imports System.Data.SqlClient

Public Class Device_Payment
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

    Public ReadOnly Property TXN_ID As Integer
        Get
            Return Session("TXN_ID")
        End Get
    End Property

#End Region

    Public ReadOnly Property PRODUCT_ID As Integer
        Get
            Try
                Return CInt(Request.QueryString("PRODUCT_ID"))
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

#Region "SIM"

    Protected Property SIM_ID As Integer
        Get
            Try
                Return Request.QueryString("SIM_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(value As Integer)
            Request.QueryString("SIM_ID") = value
        End Set
    End Property

    'Protected Property D_ID As Integer
    '    Get
    '        Try
    '            Return Request.QueryString("D_ID")
    '        Catch ex As Exception
    '            Return 0
    '        End Try
    '    End Get
    '    Set(value As Integer)
    '        Request.QueryString("D_ID") = value
    '    End Set
    'End Property

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            ClearForm()
            If PRODUCT_ID = 0 Then
                Response.Redirect("Device_Brand.aspx")
                Exit Sub
            End If

            If PRODUCT_ID <> 0 Then
                BindProduct()
            Else
                BindSIM()
            End If

            BindProductInfo()
        Else
            initFormPlugin()
        End If
    End Sub

    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Private Sub ClearForm()
        pnlCash.Visible = False
        pnlCredit.Visible = False
        pnlTruemoney.Visible = False
        pnlSelectChoice.Visible = True
        'current
        lnkCash.Attributes("class") = ""
        lnkCredit.Attributes("class") = ""
        lnkTruemoney.Attributes("class") = ""

        '----------------------- Set Alway Focus Barcode ----------------------
        Dim Script As String = "stopFocusBarcode();" & vbLf
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "clearBarcode", Script, True)

    End Sub

    Private Sub BindProductInfo()
        Dim DT As DataTable = BL.Get_Product_Info_From_ID(PRODUCT_ID)

        txtCost.Text = FormatNumber(DT.Rows(0).Item("PRICE"), 0)
        txtRequire.Text = FormatNumber(DT.Rows(0).Item("PRICE"), 0)
        txtPaid.Text = 0

    End Sub

    Private Sub lnkCash_ServerClick(sender As Object, e As EventArgs) Handles lnkCash.ServerClick
        ClearForm()
        pnlSelectChoice.Visible = False
        pnlCash.Visible = True
        lnkCash.Attributes("class") = "current"
        '----------------เริ่มรับชำระ --------------
        Dim Script As String = "RequireCash();"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CashPayment", Script, True)
    End Sub

    Private Sub lnkCredit_ServerClick(sender As Object, e As EventArgs) Handles lnkCredit.ServerClick
        ClearForm()
        pnlSelectChoice.Visible = False
        pnlCredit.Visible = True
        lnkCredit.Attributes("class") = "current"
    End Sub

    Private Sub lnkTruemoney_ServerClick(sender As Object, e As EventArgs) Handles lnkTruemoney.ServerClick
        ClearForm()
        pnlSelectChoice.Visible = False
        pnlTruemoney.Visible = True
        lnkTruemoney.Attributes("class") = "current"

        '----------------------- Set Alway Focus Barcode ----------------------
        Dim Script As String = "txtBarcode='" & txtBarcode.ClientID & "';" & vbLf
        Script &= "startFocusBarcode();"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "focusBarcodeReader", Script, True)
    End Sub

    Private Sub btnBarcode_Click(sender As Object, e As EventArgs) Handles btnBarcode.Click
        ' Call True Money
        If txtBarcode.Text = "" Then Exit Sub
        Dim Barcode As String = txtBarcode.Text
        txtBarcode.Text = ""

        Dim TMN As New TrueMoney
        '------------------- Get Parameter --------------
        Dim DT As DataTable = BL.GetList_Kiosk(KO_ID)
        Dim ShopCode As String = DT.Rows(0).Item("SITE_CODE")
        DT = BL.Get_Product_Info_From_ID(PRODUCT_ID)
        Dim Amount As Integer = DT.Rows(0).Item("PRICE")
        Dim ISV As String = TMN.Generate_ISV(ShopCode)
        Dim PaymentDescription As String = "TRUE-VDM-" & ISV
        '-------------------- เรียก------------------------
        Dim RESP As TrueMoney.Response = TMN.GetResult(ISV, Amount, Barcode, PaymentDescription, ShopCode)
        '---------------- ตรวจสอบผลลัพธ์ -------------------
        If RESP.status.code.ToLower <> "success" Then
            Alert(Me.Page, RESP.status.message)
            Exit Sub
        End If

        '---------------- Gen SlipNo --------------------

        '---------------- Save Service Transaction-------
        Dim SQL As String = "SELECT * FROM TB_SERVICE_TRANSACTION WHERE TXN_ID=" & TXN_ID
        DT = New DataTable
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        DT.Rows(0).Item("METHOD_ID") = VDM_BL.PaymentMethod.TRUE_MONEY
        DT.Rows(0).Item("TMN_REQ_ID") = RESP.REQ_ID
        DT.Rows(0).Item("TMN_ISV") = RESP.Request.isv_payment_ref
        DT.Rows(0).Item("TMN_REQUEST_AMOUNT") = RESP.Request.request_amount
        DT.Rows(0).Item("TMN_STATUS_CODE") = RESP.status.code
        DT.Rows(0).Item("TMN_PAYMENT_ID") = RESP.data.payment_id
        DT.Rows(0).Item("TMN_RESP_TIME") = Now
        Dim cmd As New SqlCommandBuilder(DA)
        DA.Update(DT)

        '---------------- Goto Next Page ----------------
        Response.Redirect("Complete_Order.aspx?PRODUCT_ID=" & PRODUCT_ID)

    End Sub

    Private Sub btnSkip_Click(sender As Object, e As EventArgs) Handles btnSkip.Click
        Response.Redirect("Complete_Order.aspx?PRODUCT_ID=" & PRODUCT_ID)
    End Sub


    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Select_Menu.aspx")
    End Sub

    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click
        Response.Redirect("Device_Shoping_Cart.aspx?PRODUCT_ID=" & PRODUCT_ID)
    End Sub

#Region "PRODUCT"

    Private Sub BindProduct()
        Dim SQL As String = ""
        SQL &= "  SELECT * FROM VW_CURRENT_PRODUCT_DETAIL WHERE PRODUCT_ID=" & PRODUCT_ID & " AND KO_ID=" & KO_ID
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            Dim Path As String = BL.Get_Product_Picture_Path(PRODUCT_ID, LANGUAGE)
            If IO.File.Exists(Path) Then
                img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & PRODUCT_ID & "&LANG=" & LANGUAGE
            Else
                img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=PRODUCT&UID=" & PRODUCT_ID & "&LANG=" & VDM_BL.UILanguage.TH
            End If
            lblDISPLAY_NAME.Text = DT.Rows(0).Item("DISPLAY_NAME_" & BL.Get_Language_Code(LANGUAGE)).ToString()


        End If

        Dim SQL_Active As String = ""
        SQL_Active &= "  Select DISTINCT PRODUCT_ID,PRODUCT_CODE,PRODUCT_NAME,KO_ID " & vbLf
        SQL_Active &= "        ,SPEC_ID,SEQ " & vbLf
        SQL_Active &= "        ,SPEC_NAME_" & BL.Get_Language_Code(LANGUAGE).ToString() & ",DESCRIPTION_" & BL.Get_Language_Code(LANGUAGE).ToString()
        SQL_Active &= "        ,CAT_ID,MODEL       " & vbLf
        SQL_Active &= "    From VW_CURRENT_PRODUCT_SPEC " & vbLf
        SQL_Active &= "    Where PRODUCT_ID =" & PRODUCT_ID & " And SPEC_ID In (" & VDM_BL.Spec.Capacity & "," & VDM_BL.Spec.Color & ") " & vbLf
        SQL_Active &= "    AND KO_ID=" & KO_ID

        DA = New SqlDataAdapter(SQL_Active, BL.ConnectionString)
        Dim DT_Active As New DataTable
        DA.Fill(DT_Active)
        If DT_Active.Rows.Count > 0 Then
            For i As Integer = 0 To DT_Active.Rows.Count - 1
                If DT_Active.Rows(i).Item("SPEC_ID") = VDM_BL.Spec.Color Then
                    lblColor.Text = DT_Active.Rows(i).Item("DESCRIPTION_" & BL.Get_Language_Code(LANGUAGE)).ToString()
                End If
                pnlCapacity.Visible = False
                If DT_Active.Rows(i).Item("SPEC_ID") = VDM_BL.Spec.Capacity Then
                    Dim Unit As String = ""
                    Select Case IIf(Not IsDBNull(DT.Rows(0).Item("CAT_ID")), DT.Rows(0).Item("CAT_ID"), 0)
                        Case VDM_BL.Category.Accessories
                            Unit = "mbps"
                        Case Else
                            Unit = "GB"
                    End Select
                    lblCapacity.Text = DT_Active.Rows(i).Item("DESCRIPTION_" & BL.Get_Language_Code(LANGUAGE)).ToString() & "" & Unit
                    pnlCapacity.Visible = True
                End If
            Next
        End If
    End Sub

#End Region

#Region "SIM"
    Private Sub BindSIM()
        Dim DT As DataTable = BL.GetList_Current_SIM_Kiosk(KO_ID, SIM_ID)
        If DT.Rows.Count > 0 Then
            Dim Path As String = BL.Get_Product_Picture_Path(PRODUCT_ID, LANGUAGE)
            If IO.File.Exists(Path) Then
                img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=SIM_PACKAGE&UID=" & SIM_ID & "&LANG=" & LANGUAGE
            Else
                img.ImageUrl = "../RenderImage.aspx?Mode=D&Entity=SIM_PACKAGE&UID=" & SIM_ID & "&LANG=" & VDM_BL.UILanguage.TH
            End If
            lblDISPLAY_NAME.Text = DT.Rows(0).Item("DISPLAY_NAME_" & BL.Get_Language_Code(LANGUAGE)).ToString()

        End If

    End Sub

    'จ่ายครบ
    Private Sub btnCashPaid_Click(sender As Object, e As EventArgs) Handles btnCashPaid.Click

    End Sub

    'จ่ายครบ
    Private Sub btnCashTimeout_Click(sender As Object, e As EventArgs) Handles btnCashTimeout.Click

    End Sub

#End Region

End Class