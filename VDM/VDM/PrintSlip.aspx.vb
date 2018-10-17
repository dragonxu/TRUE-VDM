Imports System.Data.SqlClient
Public Class PrintSlip
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL

    Public ReadOnly Property TXN_ID As Integer
        Get
            If Not IsNothing(Request.QueryString("TXN_ID")) Then
                Return Request.QueryString("TXN_ID")
            Else
                Return 0
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            txtLocalControllerURL.Text = BL.LocalControllerURL
        End If


        Dim C As New Converter

        Dim SQL As String = "SELECT * FROM TB_SERVICE_TRANSACTION" & vbLf
        SQL &= "WHERE TXN_ID=" & TXN_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        If DT.Rows.Count = 0 OrElse IsDBNull(DT.Rows(0).Item("METHOD_ID")) Then Exit Sub
        '----------------- Check Print Method ID----------------
        Dim content As String = ""
        Select Case DT.Rows(0).Item("METHOD_ID")
            Case VDM_BL.PaymentMethod.CASH
                content = BL.GEN_CASH_CONFIRMATION_SLIP(TXN_ID)
                BL.UPDATE_CONFIRMATION_SLIP(TXN_ID, content)
            Case VDM_BL.PaymentMethod.TRUE_MONEY
                content = BL.GEN_TMN_CONFIRMATION_SLIP(TXN_ID)
                BL.UPDATE_CONFIRMATION_SLIP(TXN_ID, content)
            Case VDM_BL.PaymentMethod.CREDIT_CARD
                content = BL.GEN_CREDITCARD_CONFIRMATION_SLIP(TXN_ID)
                BL.UPDATE_CONFIRMATION_SLIP(TXN_ID, content)
        End Select
        '----------------- set Print Content ----------------
        lblPrintContent.Text = content.Replace(vbNewLine, "<br>").Replace(vbTab, "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;")

        Dim Script As String = "setTimeout(printDelegate, 1000);"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Print", Script, True)
    End Sub

End Class