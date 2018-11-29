Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Public Class Report_TMS_EDI
    Inherits System.Web.UI.Page
    Dim BL As New VDM_BL
    Dim CV As New Converter


    Protected Property USER_ID As Integer
        Get
            Return Val(lblUser_ID.Attributes("USER_ID"))
        End Get
        Set(value As Integer)
            lblUser_ID.Attributes("USER_ID") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNumeric(Session("USER_ID")) Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('กรุณาเข้าสู่ระบบ'); window.location.href='SignIn.aspx';", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            ClearForm()
            BindData()
        Else
            initFormPlugin()
        End If


    End Sub

    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "ReadOnly", "DateReadOnly();", True)
    End Sub

    Private Sub ClearForm()
        ddlService.SelectedIndex = 0
        BL.Bind_DDlShop_Code(ddlShop_Name)
        txtStartDate.Text = ""
    End Sub

    Private Sub BindData()
        lblHeader.Text = "รายงาน EDI ตัดสต็อค TSM "

        Dim Sql As String = ""
        Sql &= " SELECT * " & vbLf
        Sql &= " , dbo.udf_ReportTimeDisplay(DATE_TXN_END ,2) TimeDisplay" & vbLf
        Sql &= " FROM VW_RPT_TMS_EDI WHERE 1=1 " & vbLf
        '---SEARCH--
        If ddlService.SelectedIndex > 0 Then
            Sql &= " AND ITEM_TYPE='" & ddlService.SelectedItem.Text & "' " & vbLf
            lblHeader.Text &= " ประเภท " & ddlService.SelectedItem.Text
        Else
            lblHeader.Text &= " ทั้งหมดแยกตาม Product และ SIM "
        End If

        Try
            'Dim Date_TXN As DateTime = CV.StringToDate(txtStartDate.Text, "yyyy-MM-dd")
            Dim Date_Str As String = ModuleGlobal.FormatSystemDate(txtStartDate.Text)
            If Date_Str.ToString <> "" Then
                Sql &= " AND CONVERT(date, DATE_TXN_END)='" & Date_Str & "'"
                lblHeader.Text &= " ยอดขายของวันที่ " & Date_Str
            Else
                lblHeader.Text &= " ยอดขายทั้งหมด "
            End If
        Catch ex As Exception
        End Try

        If ddlShop_Name.SelectedIndex > 0 Then
            Sql &= " AND SITE_CODE='" & ddlShop_Name.SelectedValue & "' " & vbLf
            lblHeader.Text &= " สาขา " & ddlShop_Name.SelectedItem.Text
        Else
            lblHeader.Text &= " ทุกสาขา"
        End If
        Sql &= " ORDER BY DATE_TXN_END DESC " & vbLf

        Dim DA As New SqlDataAdapter(Sql, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        If DT.Rows.Count > 0 Then
            lblHeader.Text &= " พบ " & FormatNumber(DT.Rows.Count, 0) & " รายการ"

            Dim CASH_QTY As Object = DT.Compute("SUM(CASH_QTY)", "")
            Dim TMN_WALLET_QTY As Object = DT.Compute("SUM(TMN_WALLET_QTY)", "")
            Dim CREDIT_CARD_QTY As Object = DT.Compute("SUM(CREDIT_CARD_QTY)", "")
            Dim SUM_QTY As Object = DT.Compute("SUM(SUM_QTY)", "")
            Dim SUM_VALUE As Object = DT.Compute("SUM(SUM_VALUE)", "")

            If Not IsDBNull(CASH_QTY) Then
                lblSUM_Cash.Text = FormatNumber(CASH_QTY, 0)
            Else
                lblSUM_Cash.Text = 0
            End If
            If Not IsDBNull(TMN_WALLET_QTY) Then
                lblSUM_TMN_Wallet.Text = FormatNumber(TMN_WALLET_QTY, 0)
            Else
                lblSUM_TMN_Wallet.Text = 0
            End If
            If Not IsDBNull(CREDIT_CARD_QTY) Then
                lblSUM_Credit_Card.Text = FormatNumber(CREDIT_CARD_QTY, 0)
            Else
                lblSUM_Credit_Card.Text = 0
            End If
            If Not IsDBNull(SUM_QTY) Then
                lblSUM_QtyTotal.Text = FormatNumber(SUM_QTY, 0)
            Else
                lblSUM_QtyTotal.Text = 0
            End If
            If Not IsDBNull(SUM_VALUE) Then
                lblSUM_Value.Text = FormatNumber(SUM_VALUE, 2)
            Else
                lblSUM_Value.Text = 0
            End If
        Else
            lblHeader.Text &= " ไม่พบรายการ"
            lblSUM_Cash.Text = "-"
            lblSUM_TMN_Wallet.Text = "-"
            lblSUM_Credit_Card.Text = "-"
            lblSUM_QtyTotal.Text = "-"
            lblSUM_Value.Text = "-"
        End If

        rptData.DataSource = DT
        rptData.DataBind()

    End Sub


    Private Sub rptData_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptData.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim tdMode As HtmlTableCell = e.Item.FindControl("tdMode")
        Dim lblTime As Label = e.Item.FindControl("lblTime")
        Dim lblService As Label = e.Item.FindControl("lblService")
        Dim lblShop_Name As Label = e.Item.FindControl("lblShop_Name")
        Dim lblCash As Label = e.Item.FindControl("lblCash")
        Dim lblTMN_Wallet As Label = e.Item.FindControl("lblTMN_Wallet")
        Dim lblCredit_Card As Label = e.Item.FindControl("lblCredit_Card")
        Dim lblQtyTotal As Label = e.Item.FindControl("lblQtyTotal")
        Dim lblValue As Label = e.Item.FindControl("lblValue")
        Dim lnkTXT As HtmlAnchor = e.Item.FindControl("lnkTXT")
        Dim btnDownload As Button = e.Item.FindControl("btnDownload")

        'lblTime.Text = e.Item.DataItem("TimeDisplay").ToString

        If Not IsDBNull(e.Item.DataItem("DATE_TXN_END")) Then
            lblTime.Text = CV.DateToString(e.Item.DataItem("DATE_TXN_END"), "dd/MM/yyyy")
        End If
        lblTime.Attributes("DATE_TXN_END") = ModuleGlobal.FormatSystemDate(e.Item.DataItem("DATE_TXN_END"))

        lblService.Text = e.Item.DataItem("ITEM_TYPE").ToString
        lblShop_Name.Text = e.Item.DataItem("FULL_SITE_NAME").ToString
        lblShop_Name.Attributes("SITE_CODE") = e.Item.DataItem("SITE_CODE").ToString
        lblCash.Text = FormatNumber(e.Item.DataItem("CASH_QTY"), 0)
        lblTMN_Wallet.Text = FormatNumber(e.Item.DataItem("TMN_WALLET_QTY"), 0)
        lblCredit_Card.Text = FormatNumber(e.Item.DataItem("CREDIT_CARD_QTY"), 0)

        lblQtyTotal.Text = FormatNumber(e.Item.DataItem("SUM_QTY"), 0)
        lblValue.Text = FormatNumber(e.Item.DataItem("SUM_VALUE"), 2)

        lblTime.Attributes("FileName") = "BATCH_FILE_TSM_" & lblTime.Attributes("DATE_TXN_END").Replace("/", "") & "_" & Now.ToOADate.ToString.Replace(".", "") & ".txt"

        lnkTXT.Attributes("onclick") = "document.getElementById('" & btnDownload.ClientID & "').click();"
        'lnkTXT.HRef = "Temp/" & lblTime.Attributes("FileName").ToString

        Dim Script As String = "$(""#lnkTXT"").click();" & vbNewLine
        Script &= " TXTClick();"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "TXT", Script, True)
    End Sub
    Private Sub rptData_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptData.ItemCommand
        Dim lblTime As Label = e.Item.FindControl("lblTime")
        Dim lblService As Label = e.Item.FindControl("lblService")
        Dim lblShop_Name As Label = e.Item.FindControl("lblShop_Name")
        Dim lnkTXT As HtmlAnchor = e.Item.FindControl("lnkTXT")
        Select Case e.CommandName
            Case "lnk_Download"

            Case "Download"
                Try

#Region "Generate"
                    'Dim FileName As String = "Temp/BATCH_FILE_TSM_" & Now.ToOADate.ToString.Replace(".", "") & ".txt"



                    Dim FileName As String = "Temp/" & lblTime.Attributes("FileName").ToString
                    Dim FS As FileStream = File.OpenWrite(Server.MapPath(FileName))

                    Dim Header As Header_ItemData = Header_line_item()
                    Dim Header_Data As String() = New String() {Header.Record_Id, Header.From_System, Header.To_System, Header.STR_Event, Header.Batch_No, Header.File_Creation_Date, Header.File_Creation_Time}


                    Dim Count_Send_Data As Integer = 0

                    'Dim Search As String = Date_Str

                    Using sw As StreamWriter = New StreamWriter(FS)
                        For i As Integer = 0 To Header_Data.Length - 1
                            If i < Header_Data.Length - 1 Then
                                sw.Write(Header_Data(i) & "|")
                            Else
                                sw.WriteLine(Header_Data(i))
                            End If
                        Next

                        'จำนวนใบเสร็จต่อวัน
                        Dim DT_PRODUCT_SLIP As DataTable = GET_PRODUCT_SLIP_PER_DAY(lblService.Text, lblShop_Name.Attributes("SITE_CODE").ToString, lblTime.Attributes("DATE_TXN_END").ToString) '--วันที่
                        For slip As Integer = 0 To DT_PRODUCT_SLIP.Rows.Count - 1

                            'Sale Header 1 Record
                            Dim SLIP_DETAIL As DataTable = GET_PRODUCT_SLIP_DETIAL(DT_PRODUCT_SLIP.Rows(slip).Item("SLIP_CODE").ToString) '--SLIP_CODE
                            If SLIP_DETAIL.Rows.Count > 0 Then
                                Dim Sale As Sales_Transaction_ItemData = Sales_Transaction_line_item(SLIP_DETAIL)
                                sw.Write(Sale.Record_Id & "|" & Sale.Document_Date & "|" & Sale.Branch_No & "|" & Sale.Plant_No & "|" & Sale.Proposition_Code & "|")
                                sw.Write(Sale.Sale_Code & "|" & Sale.Ref_Receipt & "|" & FormatNumber(Sale.Pay_Cash, 2).Replace(",", "") & "|" & FormatNumber(Sale.Pay_Card, 2).Replace(",", "") & "|" & FormatNumber(Sale.Pay_Cheque, 2).Replace(",", "") & "|")
                                sw.Write(FormatNumber(Sale.Pay_Other, 2).Replace(",", "") & "|" & FormatNumber(Sale.Total_Payment_Amount, 2).Replace(",", "") & "|" & Sale.Prefix_Name & "|" & Sale.First_Name & "|" & Sale.Last_Name & "|")
                                sw.Write(Sale.Address_Detail & "|" & Sale.Address_No & "|" & Sale.Moo & "|" & Sale.Trog & "|" & Sale.Soi & "|")
                                sw.Write(Sale.Road & "|" & Sale.Tumbol & "|" & Sale.Amphur & "|" & Sale.Province & "|" & Sale.Zip_Code & "|")
                                sw.Write(Sale.Telephone & "|" & Sale.Tax_ID & "|" & Sale.Cust_Branch_No)
                                sw.WriteLine("")
                                Count_Send_Data = Count_Send_Data + 1
                            End If

                            'Sale Detail
                            If SLIP_DETAIL.Rows.Count > 0 Then
                                For i As Integer = 0 To SLIP_DETAIL.Rows.Count - 1
                                    Dim Sale_Detail As Sales_Transaction_Detail_ItemData = Sales_Transaction_Detail_line_item(i, SLIP_DETAIL)
                                    sw.Write(Sale_Detail.Record_Id & "|" & Sale_Detail.Document_Date & "|" & Sale_Detail.Branch_No & "|" & Sale_Detail.Ref_Receipt & "|")
                                    sw.Write(Sale_Detail.Line_No & "|" & Sale_Detail.Product_Code & "|" & Sale_Detail.Serial_No & "|" & Sale_Detail.Quantity & "|")
                                    sw.Write(Sale_Detail.PM_Code & "|" & FormatNumber(Sale_Detail.Price_Per_Unit, 2).Replace(",", "") & "|" & FormatNumber(Sale_Detail.Discount_Percent, 2).Replace(",", "") & "|" & FormatNumber(Sale_Detail.Discount_Baht, 2).Replace(",", "") & "|")
                                    sw.Write(FormatNumber(Sale_Detail.Total_Discount, 2).Replace(",", "") & "|" & FormatNumber(Sale_Detail.Total_Amount, 2).Replace(",", "") & "|")
                                    sw.WriteLine("")
                                    Count_Send_Data = Count_Send_Data + 1
                                Next

                                'Payment
                                Dim Payment As Payment_Transaction_ItemData = Payment_Transaction_line_item(slip, SLIP_DETAIL)
                                sw.Write(Payment.Record_Id & "|" & Payment.Document_Date & "|" & Payment.Branch_No & "|" & Payment.Ref_Receipt & "|")
                                sw.Write(Payment.Pay_Type & "|" & Payment.Line_No & "|" & Payment.Card_Code & "|" & Payment.Credit_Card_ID & "|" & Payment.Credit_Card_Expired & "|")
                                sw.Write(Payment.Approve_Code & "|" & Payment.CHQ_No & "|" & Payment.CHQ_Date & "|" & Payment.CHQ_Bank_Code & "|" & Payment.Other_Code & "|")
                                sw.Write(Payment.Other_ID & "|" & FormatNumber(Payment.Pay_Amount, 2).Replace(",", ""))
                                sw.WriteLine("")
                                Count_Send_Data = Count_Send_Data + 1

                            End If
                        Next

                        'Footer
                        Dim SLIP_DETAIL_ALL As DataTable = GET_PRODUCT_SLIP_DETIAL("", lblTime.Attributes("DATE_TXN_END").ToString) '--SLIP_CODE
                        If SLIP_DETAIL_ALL.Rows.Count > 0 Then


                            Dim SUM_Footer As Object = SLIP_DETAIL_ALL.Compute("SUM(TOTAL_PRICE)", "TOTAL_PRICE Is Not NULL")
                            Dim Total As Double = 0

                            If (Not IsNothing(Total)) Then
                                Total = SUM_Footer
                            End If
                            sw.Write("99" & "|" & Count_Send_Data & "|" & FormatNumber(Total, 2).Replace(",", ""))
                        End If
                    End Using
#End Region
                    '---Insert Transaction
                    INSERT_TRANSACTION_BATCH_FILE(Header.Batch_No, lblTime.Attributes("DATE_TXN_END").ToString, FileName)

                    lnkTXT.HRef = FileName
                    lnkTXT.Attributes("download") = lblTime.Attributes("FileName").ToString

                Catch ex As Exception
                    'Alert(Me.Page, "ไม่สามารถ Download ได้")
                End Try

        End Select
    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        BindData()
    End Sub



#Region "Batch File"

#Region "Header"

    '    1.	Header line item
    Public Class Header_ItemData
        Public Property Record_Id As String
        Public Property From_System As String
        Public Property To_System As String
        Public Property STR_Event As String
        Public Property Batch_No As String
        Public Property File_Creation_Date As String
        Public Property File_Creation_Time As String
    End Class

    Private Function Header_line_item() As Header_ItemData
        Dim Result As New Header_ItemData
        Result.Record_Id = "00"
        Result.From_System = "ATB"
        Result.To_System = "TSM"
        Result.STR_Event = "WR01"   '--?????
        Dim New_Batch_No As Integer = BL.Get_NewID_Log("TB_EXPORT_BATCH_FILE", "BATCH_NO")
        Result.Batch_No = New_Batch_No.ToString.PadLeft(6, "0")
        Result.File_Creation_Date = Get_Creation_Date(Now)
        Result.File_Creation_Time = Get_Creation_Time()
        Return Result
    End Function
#End Region

#Region "FN"

    Private Function Get_Creation_Date(ByVal Creation_Date As DateTime) As String
        Dim Date_Y As String = Creation_Date.Year.ToString
        Dim Date_M As String = Creation_Date.Month.ToString.PadLeft(2, "0")
        Dim Date_D As String = Creation_Date.Day.ToString.PadLeft(2, "0")
        Return Date_D & Date_M & Date_Y
    End Function

    Private Function Get_Creation_Time() As String
        Dim Time_H As String = Now.Hour.ToString.PadLeft(2, "0")
        Dim Time_M As String = Now.Minute.ToString.PadLeft(2, "0")
        Dim Time_S As String = Now.Second.ToString.PadLeft(2, "0")
        Return Time_H & ":" & Time_M & ":" & Time_S
    End Function


    Public Sub INSERT_TRANSACTION_BATCH_FILE(ByVal BATCH_NO As Integer, ByVal SEARCH As String, ByVal FILE_NAME As String)
        Dim Sql As String = "SELECT * FROM TB_EXPORT_BATCH_FILE WHERE 0=1"
        Dim DA As New SqlDataAdapter(Sql, BL.LogConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Dim DR As DataRow = Nothing
        Dim cmd As SqlCommandBuilder

        If DT.Rows.Count = 0 Then
            DR = DT.NewRow
            DR("BATCH_NO") = BATCH_NO
            DR("FILE_NAME") = FILE_NAME.ToString()
            DR("SEARCH") = SEARCH.ToString()
            DR("CREATE_TIME") = Now
            DR("CREATE_BY") = Session("USER_ID")
            DT.Rows.Add(DR)
        End If
        Try
            cmd = New SqlCommandBuilder(DA)
            DA.Update(DT)
        Catch : End Try

    End Sub


#End Region

#Region "01 = Sales Transaction Header"

    '    2.	Data line Item
    '       01 = Sales Transaction Header
    Public Class Sales_Transaction_ItemData
        Public Property Record_Id As String = "01"
        Public Property Document_Date As String
        Public Property Branch_No As String
        Public Property Plant_No As String = "C000"
        Public Property Proposition_Code As String = "999"
        Public Property Sale_Code As String
        Public Property Ref_Receipt As String
        Public Property Pay_Cash As Double
        Public Property Pay_Card As Double = 0.00
        Public Property Pay_Cheque As Double = 0.00
        Public Property Pay_Other As Double = 0.00
        Public Property Total_Payment_Amount As Double
        Public Property Prefix_Name As String = ""
        Public Property First_Name As String = ""
        Public Property Last_Name As String = ""
        Public Property Address_Detail As String = ""
        Public Property Address_No As String = ""
        Public Property Moo As String = ""
        Public Property Trog As String = ""
        Public Property Soi As String = ""
        Public Property Road As String = ""
        Public Property Tumbol As String = ""
        Public Property Amphur As String = ""
        Public Property Province As String = ""
        Public Property Zip_Code As String = ""
        Public Property Telephone As String = ""
        Public Property Tax_ID As String = ""
        Public Property Cust_Branch_No As String = "00000"
    End Class


    Private Function Sales_Transaction_line_item(ByRef DT As DataTable) As Sales_Transaction_ItemData
        Dim Result As New Sales_Transaction_ItemData

        Dim Cash As Object = DT.Compute("SUM(TOTAL_PRICE)", "TOTAL_PRICE IS NOT NULL AND METHOD_ID=" & VDM_BL.PaymentMethod.CASH)
        Dim True_Money As Object = DT.Compute("SUM(TOTAL_PRICE)", "TOTAL_PRICE IS NOT NULL AND METHOD_ID=" & VDM_BL.PaymentMethod.TRUE_MONEY)
        Dim Credit_Card As Object = DT.Compute("SUM(TOTAL_PRICE)", "TOTAL_PRICE IS NOT NULL AND METHOD_ID=" & VDM_BL.PaymentMethod.CREDIT_CARD)
        Dim Total As Object = DT.Compute("SUM(TOTAL_PRICE)", "TOTAL_PRICE IS NOT NULL")
        If DT.Rows.Count > 0 Then
            Result.Document_Date = IIf(Not IsDBNull(DT.Rows(0).Item("DATE_TXN_END")), Get_Creation_Date(DT.Rows(0).Item("DATE_TXN_END")), "")
            Result.Branch_No = IIf(Not IsDBNull(DT.Rows(0).Item("SITE_CODE")), DT.Rows(0).Item("SITE_CODE").ToString.Substring(DT.Rows(0).Item("SITE_CODE").ToString.Length - 3), "")
            Result.Sale_Code = "USER_ID01" 'RTrim(LTrim(Session("USER_ID")))
            Result.Ref_Receipt = DT.Rows(0).Item("SLIP_CODE").ToString
            Select Case DT.Rows(0).Item("METHOD_ID")
                Case VDM_BL.PaymentMethod.CASH
                    Result.Pay_Cash = IIf(Not IsNothing(Cash), Cash.ToString.Replace(",", ""), 0.00)
                Case VDM_BL.PaymentMethod.TRUE_MONEY
                    Result.Pay_Card = IIf(Not IsNothing(True_Money), True_Money.ToString.Replace(",", ""), 0.00)

                Case VDM_BL.PaymentMethod.CREDIT_CARD
                    Result.Pay_Card = IIf(Not IsNothing(Credit_Card), Credit_Card.ToString.Replace(",", ""), 0.00)
                Case VDM_BL.PaymentMethod.UNKNOWN
                    Result.Pay_Card = 0.00
                Case Else
            End Select
            Result.Total_Payment_Amount = IIf(Not IsNothing(Total), Total, 0.00)
        End If
        Return Result
    End Function


#End Region

#Region "02 = Sales Transaction Detail"

    '       02 = Sales Transaction Detail
    Public Class Sales_Transaction_Detail_ItemData
        Public Property Record_Id As String = "02"
        Public Property Document_Date As String
        Public Property Branch_No As String
        Public Property Ref_Receipt As String
        Public Property Line_No As Integer
        Public Property Product_Code As String
        Public Property Serial_No As String
        Public Property Quantity As Integer
        Public Property PM_Code As String = ""
        Public Property Price_Per_Unit As Double
        Public Property Discount_Percent As Double = 0.00
        Public Property Discount_Baht As Double = 0.00
        Public Property Total_Discount As Double = 0.00
        Public Property Total_Amount As Double

    End Class


    Private Function Sales_Transaction_Detail_line_item(ByVal Index As Integer, ByRef DT As DataTable) As Sales_Transaction_Detail_ItemData
        Dim Result As New Sales_Transaction_Detail_ItemData

        If DT.Rows.Count > 0 Then
            Result.Document_Date = IIf(Not IsDBNull(DT.Rows(Index).Item("DATE_TXN_END")), Get_Creation_Date(DT.Rows(Index).Item("DATE_TXN_END")), "")
            Result.Branch_No = IIf(Not IsDBNull(DT.Rows(Index).Item("SITE_CODE")), DT.Rows(Index).Item("SITE_CODE").ToString.Substring(DT.Rows(Index).Item("SITE_CODE").ToString.Length - 3), "")
            Result.Ref_Receipt = DT.Rows(Index).Item("SLIP_CODE").ToString
            Result.Line_No = Index + 1
            Result.Product_Code = DT.Rows(Index).Item("PRODUCT_CODE").ToString
            Result.Serial_No = DT.Rows(Index).Item("SERIAL_NO").ToString
            Result.Quantity = Val(DT.Rows(Index).Item("Quantity").ToString)
            Result.Price_Per_Unit = IIf(Not IsDBNull(DT.Rows(Index).Item("TOTAL_PRICE")), DT.Rows(Index).Item("TOTAL_PRICE").ToString.Replace(",", ""), 0.00)
            Result.Total_Amount = IIf(Not IsDBNull(DT.Rows(Index).Item("TOTAL_PRICE")), DT.Rows(Index).Item("TOTAL_PRICE").ToString.Replace(",", ""), 0.00)

        End If
        Return Result
    End Function






#End Region

#Region "03 = Payment Transaction"

    Public Class Payment_Transaction_ItemData
        Public Property Record_Id As String = "03"
        Public Property Document_Date As String
        Public Property Branch_No As String
        Public Property Ref_Receipt As String
        Public Property Pay_Type As String
        Public Property Line_No As Integer
        Public Property Card_Code As String = ""
        Public Property Credit_Card_ID As String
        Public Property Credit_Card_Expired As String
        Public Property Approve_Code As String
        Public Property CHQ_No As String = ""
        Public Property CHQ_Date As String = ""
        Public Property CHQ_Bank_Code As String = ""
        Public Property Other_Code As String = ""
        Public Property Other_ID As String = ""
        Public Property Pay_Amount As Double
    End Class


    Private Function Payment_Transaction_line_item(slip As Integer, ByRef DT As DataTable) As Payment_Transaction_ItemData
        Dim Result As New Payment_Transaction_ItemData
        Dim Total As Object = DT.Compute("SUM(TOTAL_PRICE)", "TOTAL_PRICE IS NOT NULL")

        If DT.Rows.Count > 0 Then
            Result.Document_Date = IIf(Not IsDBNull(DT.Rows(0).Item("DATE_TXN_END")), Get_Creation_Date(DT.Rows(0).Item("DATE_TXN_END")), "")
            Result.Branch_No = IIf(Not IsDBNull(DT.Rows(0).Item("SITE_CODE")), DT.Rows(0).Item("SITE_CODE").ToString.Substring(DT.Rows(0).Item("SITE_CODE").ToString.Length - 3), "")
            Result.Ref_Receipt = DT.Rows(0).Item("SLIP_CODE").ToString
            Result.Line_No = slip + 1
            Select Case DT.Rows(0).Item("METHOD_ID")
                Case VDM_BL.PaymentMethod.CASH
                    Result.Pay_Type = "CSH"

                Case VDM_BL.PaymentMethod.TRUE_MONEY
                    Result.Pay_Type = "CRD"
                    Result.Card_Code = "TMN Wallet"
                    Dim TMN_PAYMENT_CODE As String = DT.Rows(0).Item("TMN_PAYMENT_CODE").ToString & "00"
                    Dim CodeFront4 As String = TMN_PAYMENT_CODE.Substring(0, 4).ToString()
                    Dim CodeBack4 As String = TMN_PAYMENT_CODE.Substring(TMN_PAYMENT_CODE.Length - 4, 4)
                    Dim ConvertPAYMENT_CODE As String = CodeFront4.PadRight(TMN_PAYMENT_CODE.ToString.Length - 4, "x") & CodeBack4
                    'Result.Credit_Card_ID = TMN_PAYMENT_CODE & "00" & ConvertPAYMENT_CODE
                    Result.Credit_Card_ID = ConvertPAYMENT_CODE

                    Result.Approve_Code = DT.Rows(0).Item("SITE_CODE").ToString & "-" & DT.Rows(0).Item("Approve_Code").ToString  'ShopCode + ‘-’ + YYMMddhhmmssMSS ถึง Millisec

                Case VDM_BL.PaymentMethod.CREDIT_CARD
                    Result.Pay_Type = "CRD"
                    Result.Card_Code = "VISA"

                    Dim CREDIT_CARD_NO As String = DT.Rows(0).Item("CREDIT_CARD_NO").ToString       '--รอข้อมูล   ?????
                    Dim CodeFront6 As String = ""
                    Dim CodeBack4 As String = ""
                    If CREDIT_CARD_NO.Length >= 4 Then
                        CodeFront6 = CREDIT_CARD_NO.Substring(0, 6).ToString()
                        CodeBack4 = CREDIT_CARD_NO.Substring(CREDIT_CARD_NO.Length - 4, 4)
                    End If
                    Dim ConvertPAYMENT_CODE As String = CodeFront6.PadRight(CREDIT_CARD_NO.ToString.Length - 4, "x") & CodeBack4
                    Result.Credit_Card_ID = ConvertPAYMENT_CODE
                    Result.Approve_Code = DT.Rows(0).Item("Approve_Code").ToString    'Approve Code จริงจาก ธ   ?????  รอดึงข้อมูลจาก view


                Case VDM_BL.PaymentMethod.UNKNOWN
                    Result.Pay_Type = ""
                Case Else
            End Select
            Result.Credit_Card_Expired = "012100" 'Random ไปไกลๆ Fake 
            Result.Pay_Amount = IIf(Not IsNothing(Total), Total, 0.00)
        End If
        Return Result
    End Function

#End Region

#Region "Footer line item"
    Public Class Footer_line_ItemData
        Public Property Record_Id As String = "99"
        Public Property Total_No_Rec As Double
        Public Property Total_Amount As Double
    End Class
#End Region


    'VDM
    '--หาจำนวนใบเสร็จต่อวัน
    Public Function GET_PRODUCT_SLIP_PER_DAY(ByVal ITEM_TYPE As String, ByVal SITE_CODE As String, Optional SearchDate As String = "") As DataTable

        Dim SQL As String = "SELECT DISTINCT SLIP_CODE  FROM VW_TXN_COMPLETED WHERE 1=1 " & vbLf
        If ITEM_TYPE <> "" Then
            SQL &= " AND ITEM_TYPE='" & ITEM_TYPE & "' " & vbLf
        End If
        If SITE_CODE <> "" Then
            SQL &= " AND SITE_CODE='" & SITE_CODE & "' " & vbLf
        End If
        Try
            If SearchDate <> "" Then
                SQL &= " AND CONVERT(date, TXN_END)='" & SearchDate & "'"
            End If
        Catch ex As Exception
        End Try


        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        Return DT
    End Function

    Public Function GET_PRODUCT_SLIP_DETIAL(SLIP_CODE As String, Optional DATE_Search As String = "") As DataTable

        Dim SQL As String = "SELECT * FROM VW_TXN_COMPLETED WHERE 1=1 " & vbLf
        If SLIP_CODE <> "" Then
            SQL &= " AND SLIP_CODE='" & SLIP_CODE & "'"
        End If
        If DATE_Search <> "" Then
            SQL &= " AND  CONVERT(date, TXN_END)='" & DATE_Search & "'"
        End If

        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        Return DT
    End Function


#End Region










End Class