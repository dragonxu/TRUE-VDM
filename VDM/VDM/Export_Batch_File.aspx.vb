Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class Export_Batch_File
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL
    Dim CV As New Converter

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim FileName As String = "Temp/myfile_" & Now.ToOADate.ToString.Replace(".", "") & ".txt"
        Dim FS As FileStream = File.OpenWrite(Server.MapPath(FileName))

        Dim Header As Header_ItemData = Header_line_item()
        Dim Header_Data As String() = New String() {Header.Record_Id, Header.From_System, Header.To_System, Header.STR_Event, Header.Batch_No, Header.File_Creation_Date, Header.File_Creation_Time}


        Dim DT_CASH As DataTable = GET_TXN_CASH_COMPLETE()

        Dim s As String
        Using sw As StreamWriter = New StreamWriter(FS)
            For i As Integer = 0 To Header_Data.Length - 1
                If i < Header_Data.Length - 1 Then
                    sw.Write(Header_Data(i) & "|")
                Else
                    sw.WriteLine(Header_Data(i))
                End If
            Next

            'Sale Header
            If DT_CASH.Rows.Count > 0 Then
                For i As Integer = 0 To DT_CASH.Rows.Count - 1
                    Dim Sale As Sales_Transaction_ItemData = Sales_Transaction_line_item(i, DT_CASH)
                    sw.Write(Sale.Record_Id & "|" & Sale.Document_Date & "|" & Sale.Branch_No & "|" & Sale.Plant_No & "|" & Sale.Proposition_Code & "|")
                    sw.Write(Sale.Sale_Code & "|" & Sale.Ref_Receipt & "|" & FormatNumber(Sale.Pay_Cash, 2).Replace(",", "") & "|" & FormatNumber(Sale.Pay_Card, 2).Replace(",", "") & "|" & FormatNumber(Sale.Pay_Cheque, 2).Replace(",", "") & "|")
                    sw.Write(FormatNumber(Sale.Pay_Other, 2).Replace(",", "") & "|" & FormatNumber(Sale.Total_Payment_Amount, 2).Replace(",", "") & "|" & Sale.Prefix_Name & "|" & Sale.First_Name & "|" & Sale.Last_Name & "|")
                    sw.Write(Sale.Address_Detail & "|" & Sale.Address_No & "|" & Sale.Moo & "|" & Sale.Trog & "|" & Sale.Soi & "|")
                    sw.Write(Sale.Road & "|" & Sale.Tumbol & "|" & Sale.Amphur & "|" & Sale.Province & "|" & Sale.Zip_Code & "|")
                    sw.Write(Sale.Telephone & "|" & Sale.Tax_ID & "|" & Sale.Cust_Branch_No)
                    sw.WriteLine("")

                Next
            End If

            'Sale Detail
            'If DT_CASH.Rows.Count > 0 Then
            For i As Integer = 0 To DT_CASH.Rows.Count - 1          'DT_CASH รอเปลี่ยน
                Dim Sale_Detail As Sales_Transaction_Detail_ItemData = Sales_Transaction_Detail_line_item(i, DT_CASH)
                sw.Write(Sale_Detail.Record_Id & "|" & Sale_Detail.Document_Date & "|" & Sale_Detail.Branch_No & "|" & Sale_Detail.Ref_Receipt)
                sw.Write(Sale_Detail.Line_No & "|" & Sale_Detail.Product_Code & "|" & Sale_Detail.Serial_No & "|" & Sale_Detail.Quantity)
                sw.Write(Sale_Detail.PM_Code & "|" & FormatNumber(Sale_Detail.Price_Per_Unit, 2).Replace(",", "") & "|" & FormatNumber(Sale_Detail.Discount_Percent, 2).Replace(",", "") & "|" & FormatNumber(Sale_Detail.Discount_Baht, 2).Replace(",", ""))
                sw.Write(FormatNumber(Sale_Detail.Total_Discount, 2).Replace(",", "") & "|" & FormatNumber(Sale_Detail.Total_Amount, 2).Replace(",", ""))
                sw.WriteLine("")
            Next
            'End If

            'Payment
            'If DT_CASH.Rows.Count > 0 Then
            For i As Integer = 0 To DT_CASH.Rows.Count - 1          'DT_CASH รอเปลี่ยน
                Dim Payment As Payment_Transaction_ItemData = Payment_Transaction_line_item(i, DT_CASH)
                sw.Write(Payment.Record_Id & "|" & Payment.Document_Date & "|" & Payment.Branch_No & "|" & Payment.Ref_Receipt)
                sw.Write(Payment.Pay_Type & "|" & Payment.Line_No & "|" & Payment.Card_Code & "|" & Payment.Credit_Card_ID & "|" & Payment.Credit_Card_Expired)
                sw.Write(Payment.Approve_Code & "|" & Payment.CHQ_No & "|" & Payment.CHQ_Date & "|" & Payment.CHQ_Bank_Code & "|" & Payment.Other_Code)
                sw.Write(Payment.Other_ID & "|" & FormatNumber(Payment.Pay_Amount, 2).Replace(",", ""))
                sw.WriteLine("")
            Next
            'End If

            'Footer
            'If DT_CASH.Rows.Count > 0 Then
            For i As Integer = 0 To DT_CASH.Rows.Count - 1          'DT_CASH รอเปลี่ยน
                Dim Footer As Footer_line_ItemData = Footer_line_item(i, DT_CASH)
                sw.Write(Footer.Record_Id & "|" & FormatNumber(Footer.Total_No_Rec, 2).Replace(",", "") & "|" & FormatNumber(Footer.Total_Amount, 2).Replace(",", ""))
            Next
            'End If

        End Using
    End Sub

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


    Private Function Sales_Transaction_line_item(ByVal Index As Integer, ByRef DT As DataTable) As Sales_Transaction_ItemData
        Dim Result As New Sales_Transaction_ItemData

        If DT.Rows.Count > 0 Then
            Result.Document_Date = IIf(Not IsDBNull(DT.Rows(Index).Item("DATE_TXN_END")), Get_Creation_Date(DT.Rows(Index).Item("DATE_TXN_END")), "")
            Result.Branch_No = IIf(Not IsDBNull(DT.Rows(Index).Item("SITE_CODE")), DT.Rows(Index).Item("SITE_CODE").ToString.Substring(DT.Rows(Index).Item("SITE_CODE").ToString.Length - 3), "")
            Result.Sale_Code = "USER_ID01" 'RTrim(LTrim(Session("USER_ID")))
            Result.Ref_Receipt = DT.Rows(Index).Item("SLIP_CODE").ToString
            Result.Pay_Cash = IIf(Not IsDBNull(DT.Rows(Index).Item("TOTAL_PRICE")), DT.Rows(Index).Item("TOTAL_PRICE").ToString.Replace(",", ""), 0.00)
            Result.Total_Payment_Amount = IIf(Not IsDBNull(DT.Rows(Index).Item("TOTAL_PRICE")), DT.Rows(Index).Item("TOTAL_PRICE").ToString.Replace(",", ""), 0.00)
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


    Private Function Payment_Transaction_line_item(ByVal Index As Integer, ByRef DT As DataTable) As Payment_Transaction_ItemData
        Dim Result As New Payment_Transaction_ItemData

        If DT.Rows.Count > 0 Then
            Result.Document_Date = IIf(Not IsDBNull(DT.Rows(Index).Item("DATE_TXN_END")), Get_Creation_Date(DT.Rows(Index).Item("DATE_TXN_END")), "")
            Result.Branch_No = IIf(Not IsDBNull(DT.Rows(Index).Item("SITE_CODE")), DT.Rows(Index).Item("SITE_CODE").ToString.Substring(DT.Rows(Index).Item("SITE_CODE").ToString.Length - 3), "")
            Result.Ref_Receipt = DT.Rows(Index).Item("SLIP_CODE").ToString
            Result.Line_No = Index + 1

        End If
        Return Result
    End Function






#End Region


    '    3.	Footer line item

#Region "Footer line item"

    Public Class Footer_line_ItemData
        Public Property Record_Id As String = "99"
        Public Property Total_No_Rec As Double
        Public Property Total_Amount As Double
    End Class
    Private Function Footer_line_item(ByVal Index As Integer, ByRef DT As DataTable) As Footer_line_ItemData
        Dim Result As New Footer_line_ItemData

        If DT.Rows.Count > 0 Then
            'Result.Total_No_Rec = ""
            'Result.Total_Amount = ""


        End If
        Return Result
    End Function


#End Region





    'VDM
    Public Function GET_TXN_CASH_COMPLETE(Optional Search As String = "") As DataTable

        Dim SQL As String = "SELECT *,CONVERT(date, TXN_END) DATE_TXN_END  FROM VW_TXN_CASH" & vbLf
        SQL &= "WHERE TXN_STEP='completed'"
        If Search <> "" Then
            SQL &= " AND CONVERT(date, TXN_END)='" & Search & "'"
        End If
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)

        Return DT

    End Function




End Class