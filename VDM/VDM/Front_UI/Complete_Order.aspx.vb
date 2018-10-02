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

    Public ReadOnly Property TXN_ID As Integer
        Get
            Return Session("TXN_ID")
        End Get
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

        Dim SQL As String = ""
        SQL &= " SELECT RND,SLOT_NAME,TOTAL" & vbLf
        SQL &= " FROM" & vbLf
        SQL &= " (SELECT RAND() RND,SLOT_NAME ,COUNT(1) TOTAL" & vbLf
        SQL &= " FROM VW_CURRENT_PRODUCT_STOCK" & vbLf
        SQL &= " WHERE PRODUCT_ID=" & PRODUCT_ID & " AND KO_ID=" & KO_ID & vbLf
        SQL &= " GROUP BY SLOT_NAME) PRODCUT" & vbLf
        SQL &= " ORDER BY TOTAL DESC,RND" & vbLf

        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)

        If DT.Rows.Count = 0 Then
            Alert(Me.Page, "PRODUCT NOT FOUND")
            Exit Sub
        End If

        Dim SLOT_NAME As String = DT.Rows(0).Item("SLOT_NAME")
        '----------------- Call LocalController -----------------------

    End Sub


    Private Sub lnkHome_Click(sender As Object, e As ImageClickEventArgs) Handles lnkHome.Click
        Response.Redirect("Select_Menu.aspx")
    End Sub

    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click
        Response.Redirect("Device_Payment.aspx?PRODUCT_ID=" & PRODUCT_ID)
    End Sub

    Private Sub btnSkip_Click(sender As Object, e As EventArgs) Handles btnSkip.Click
        '------------------ ตัด Stock ----------------


        Response.Redirect("Thank_You.aspx?PRODUCT_ID=" & PRODUCT_ID)
    End Sub
End Class