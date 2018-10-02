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


    Private Property SLOT_NAME As String
        Get
            Return txtSlotName.Text
        End Get
        Set(value As String)
            txtSlotName.Text = value
        End Set
    End Property

    Private Property POS_ID As String
        Get
            Return txtPosID.Text
        End Get
        Set(value As String)
            txtPosID.Text = value
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
#End Region

    Public ReadOnly Property PRODUCT_ID As Integer
        Get
            Return CInt(Request.QueryString("PRODUCT_ID"))
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        BL.Update_Service_Transaction(TXN_ID, Me.Page) '-------------- Update ทุกหน้า ------------
        If Not IsPostBack Then
            PickUp()
        End If

    End Sub

    Private Sub PickUp()

        '--------------- Get All Product Slot------------
        Dim SQL As String = ""
        SQL &= " SELECT PRODUCT.SLOT_NAME,POS_ID,TOTAL" & vbLf
        SQL &= " FROM" & vbLf
        SQL &= " (SELECT SLOT_NAME ,COUNT(1) TOTAL" & vbLf
        SQL &= " FROM VW_CURRENT_PRODUCT_STOCK" & vbLf
        SQL &= " WHERE KO_ID=" & KO_ID & " AND PRODUCT_ID=" & PRODUCT_ID & vbLf
        SQL &= " GROUP BY SLOT_NAME) PRODUCT" & vbLf
        SQL &= " LEFT JOIN MS_SLOT_ARM_POSITION ARM ON PRODUCT.SLOT_NAME=ARM.SLOT_NAME" & vbLf
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)

        If DT.Rows.Count = 0 Then
            Alert(Me.Page, "PRODUCT NOT FOUND")
            Exit Sub
        End If
        '---------------- Random Selction -------------
        DT.Columns.Add("RND")
        For i As Integer = 0 To DT.Rows.Count - 1
            DT.Rows(i).Item("RND") = GenerateNewUniqueID()
        Next
        DT.DefaultView.Sort = "TOTAL DESC,RND ASC"

        SLOT_NAME = DT.DefaultView(0).Item("SLOT_NAME")
        POS_ID = DT.DefaultView(0).Item("POS_ID")

        '----------------- Call LocalController -----------------------

    End Sub


    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Select_Menu.aspx")
    End Sub

    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click
        Response.Redirect("Device_Payment.aspx?PRODUCT_ID=" & PRODUCT_ID)
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        '------------------ Update Transaction ------
        Dim SQL As String = "SELECT TOP 1 SERIAL_NO,SLOT_NAME,SLOT_ID" & vbLf
        SQL &= " FROM VW_CURRENT_PRODUCT_STOCK" & vbLf
        SQL &= " WHERE KO_ID=" & KO_ID & " AND PRODUCT_ID=" & PRODUCT_ID & " AND SLOT_NAME='" & SLOT_NAME.Replace("'", "") & "'" & vbLf
        SQL &= " ORDER BY ORDER_NO ASC" & vbLf
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        Dim SERIAL_NO As String = DT.Rows(0).Item("SERIAL_NO").ToString
        Dim SLOT_ID As Integer = DT.Rows(0).Item("SLOT_ID")
        '--------------- Get Product Price -------------
        DT = BL.Get_Product_Info_From_ID(PRODUCT_ID)
        Dim PRICE As Integer = DT.Rows(0).Item("PRICE")
        '--------------- Get Koisk Detail-------------
        DT = BL.GetList_Kiosk(KO_ID)
        Dim KO_CODE As String = DT.Rows(0).Item("KO_CODE")
        Dim SITE_CODE As String = DT.Rows(0).Item("SITE_CODE")
        '--------------- Get Shift Detail---------------
        DT = BL.Get_Kiosk_Current_Shift(KO_ID)
        Dim SHIFT_ID As Integer = DT.Rows(0).Item("SHIFT_ID")

        SQL = "SELECT TOP 0 * FROM TB_BUY_PRODUCT"
        DT = New DataTable
        DA = New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        Dim DR As DataRow = DT.NewRow
        DR("TXN_ID") = TXN_ID
        DR("ITEM_NO") = 1
        DR("PRODUCT_ID") = PRODUCT_ID
        DR("SERIAL_NO") = SERIAL_NO
        DR("UNIT_PRICE") = PRICE
        DR("QUANTITY") = 1
        DR("VAT") = DBNull.Value
        DR("TOTAL_PRICE") = PRICE
        DT.Rows.Add(DR)
        Dim cmd As New SqlCommandBuilder(DA)
        DA.Update(DT)
        '------------------ Save Log ----------------
        BL.Save_Product_Movement_Log(SHIFT_ID, VDM_BL.ShiftStatus.OnGoing, PRODUCT_ID, SERIAL_NO,
                                     VDM_BL.StockMovementType.Sell, SLOT_NAME, SLOT_ID, "Sell", 0, "ขายที่ " & SITE_CODE & " " & KO_CODE & " ไปเมื่อ " & Now.ToString("dd-MMM-yyyy hh:mm:ss"), 0, Now)
        '------------------ ตัด Stock----------------
        BL.Drop_PRODUCT_STOCK_SERIAL(SLOT_ID, SERIAL_NO)

        '------------------ ไปหน้า พิมพ์ใบเสร็จ----------
        Response.Redirect("Thank_You.aspx?PRODUCT_ID=" & PRODUCT_ID)
    End Sub
End Class