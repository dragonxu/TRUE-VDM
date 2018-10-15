Imports VDM
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationManager

Public Class VDM_BL

    Public ServerRoot As String = AppSettings("ServerRoot").ToString
    Public ConnectionString As String = ConnectionStrings("ConnectionString").ConnectionString
    Public LogConnectionString As String = ConnectionStrings("LogConnectionString").ConnectionString
    Public ServerMapPath As String = AppSettings("ServerMapPath").ToString
    Public PicturePath As String = AppSettings("PicturePath").ToString
    Public Product_Critical_Percent As Integer = 30 '----------- Level ที่สีแดงใน Product Stock -----------
    Public SIM_Critical_Percent As Integer = 30 '----------- Level ที่สีแดงใน Product Stock -----------


    Public LocalControllerURL As String = AppSettings("LocalControllerURL").ToString

    Public Sub ExecuteNonQuery(ByVal CommandText As String)
        Dim Command As New SqlCommand
        Dim Conn As New SqlConnection(ConnectionString)

        Conn.Open()
        With Command
            .Connection = Conn
            .CommandText = CommandText
            .ExecuteNonQuery()
            .Dispose()
        End With
        Conn.Close()
        Conn.Dispose()
    End Sub

    Public Sub ExecuteNonQuery_Log(ByVal CommandText As String)
        Dim Command As New SqlCommand
        Dim Conn As New SqlConnection(LogConnectionString)

        Conn.Open()
        With Command
            .Connection = Conn
            .CommandText = CommandText
            .ExecuteNonQuery()
            .Dispose()
        End With
        Conn.Close()
        Conn.Dispose()
    End Sub

    Public Function Get_NewID(ByRef Table_Name As String, ByRef Field_ID As String) As Integer
        Dim SQL As String = "SELECT IsNull(MAX(" & Field_ID & "),0)+1 FROM " & Table_Name & " "
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT.Rows(0).Item(0)
    End Function

    Public Function Get_NewID_Log(ByRef Table_Name As String, ByRef Field_ID As String) As Integer
        Dim SQL As String = "SELECT IsNull(MAX(" & Field_ID & "),0)+1 FROM " & Table_Name & " "
        Dim DA As New SqlDataAdapter(SQL, LogConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT.Rows(0).Item(0)
    End Function

    Public Enum UILanguage
        TH = 1
        EN = 2
        CN = 3
        JP = 4
        KR = 5
        RS = 6
    End Enum

    Public Enum Device
        CashIn = 1
        Cash20 = 2
        Cash50 = 18
        Cash100 = 3
        Cash500 = 19
        Cash1000 = 20
        CoinIn = 4
        Coin1 = 5
        Coin2 = 21
        Coin5 = 6
        Coin10 = 7
        Passport = 8
        Printer = 9
        ProductShelf = 10
        SIMDispenser1 = 11
        SIMDispenser2 = 12
        SIMDispenser3 = 13
        FrontQRReader = 15
        InternalQRReader = 16
        Camera = 17
    End Enum

    Public Enum DeviceType
        CashIn = 1
        CashOut = 2
        CoinIn = 3
        CoinOut = 4
        Printer = 5
        PassportScanner = 6
        IPCamera = 7
        SIMDispenser = 8
        BarcodeReader = 9
        ProductShelf = 10

    End Enum

    Public Enum Spec
        Color = 1
        Capacity = 2
        Warranty = 3
        Length = 4
        Width = 5
        Height = 6
        Weight = 7
        CapacityAccessories = 8
    End Enum

    Public Enum Category
        Mobile = 1
        Tablet = 2
        Accessories = 3
    End Enum

    Public Enum StockMovementType
        CheckIn = 1
        CheckOut = 2
        Sell = 3
        ChangeSlot = 4
    End Enum

    Public Enum ShiftStatus
        OnGoing = -1
        Close = 0
        Open = 1
        Unknown = 99
    End Enum

    Public Enum PaymentMethod
        UNKNOWN = 0
        CASH = 1
        TRUE_MONEY = 2
        CREDIT_CARD = 3
    End Enum

    Public Function Get_Language_Code(ByVal Language As UILanguage) As String
        Select Case Language
            Case UILanguage.TH
                Return "TH"
            Case UILanguage.EN
                Return "EN"
            Case UILanguage.CN
                Return "CN"
            Case UILanguage.JP
                Return "JP"
            Case UILanguage.KR
                Return "KR"
            Case UILanguage.RS
                Return "RS"
            Case Else
                Return "TH"
        End Select
    End Function

    Public Function Get_Language_Name(ByVal Language As UILanguage) As String
        Select Case Language
            Case UILanguage.TH
                Return "Thai"
            Case UILanguage.EN
                Return "English"
            Case UILanguage.CN
                Return "Chinese"
            Case UILanguage.JP
                Return "Japanese"
            Case UILanguage.KR
                Return "Korian"
            Case UILanguage.RS
                Return "Russian"
            Case Else
                Return ""
        End Select
    End Function

    Public Class DialogImage

        Public Property ModeAccept_Contact As Accept_Contact_EMP
        Public Property ModeID_Card As ID_Card
        Public Property ModeCredit_Card As Credit_Card
        Public Property ModeTruemoney As TrueMoney
        Public Property ModeFace As Face
        Public Property ModeCash As Cash
        Public Property ModeCoin As String = "images/Warning_Coin.png"

        Public Class Accept_Contact_EMP
            Public Property Alert_Show As String = "images/Confirm_Contact_CUST.png"

        End Class

        Public Class ID_Card
            Public Property Alert_Error As String = "images/Error_ID_Card.png"
            Public Property Alert_Repeat As String = "images/Rescan_ID_Card.png"
            Public Property Alert_Warning As String = "images/Warning_ID_Card.png"
            Public Property Alert_Success As String = "images/pic-idcard2.png"
            Public Property Alert_Show As String = "images/pic-idcard.png"

        End Class

        Public Class Credit_Card
            Public Property Alert_Warning As String = "images/Warning_Credit.png"
            Public Property Alert_Show As String = "images/icon-credit.png"
        End Class

        Public Class TrueMoney
            Public Property Alert_Warning As String = "images/Warning_Truemoney.png"
            Public Property Alert_Show As String = "images/icon-truemoney.png"
        End Class

        Public Class Face
            Public Property Alert_Warning As String = "images/Warning_Face.png"
            Public Property Alert_Show As String = "images/pic-scanface.png"

        End Class

        Public Class Cash
            Public Property Alert_Error As String = "images/Error_Recieve_Cash.png"
            Public Property Alert_Repeat As String = "images/Rescan_ID_Card.png"
            Public Property Alert_Show As String = "images/icon-cash.png"


        End Class

    End Class

    Public Function Get_Site_Icon_Path(ByVal SITE_ID As Integer) As String
        Return PicturePath & "\Site\" & SITE_ID
    End Function

    Public Function Get_Site_Icon(ByVal SITE_ID As Integer) As Byte()
        Dim FilePath As String = Get_Site_Icon_Path(SITE_ID)
        Return ReadFile(FilePath)
    End Function

    Public Sub Save_Site_Icon(ByVal SITE_ID As Integer, ByVal B As Byte())
        Dim FilePath As String = Get_Site_Icon_Path(SITE_ID)
        SaveFile(FilePath, B)
    End Sub

    Public Sub Delete_Site_Icon(ByVal SITE_ID As Integer)
        Dim FilePath As String = Get_Site_Icon_Path(SITE_ID)
        DeleteFile(FilePath)
    End Sub

    Public Function Get_Brand_Logo_Path(ByVal BRAND_ID As Integer) As String
        Return PicturePath & "\Brand\" & BRAND_ID
    End Function

    Public Function Get_Brand_Logo(ByVal BRAND_ID As Integer) As Byte()
        Dim FilePath As String = Get_Brand_Logo_Path(BRAND_ID)
        Return ReadFile(FilePath)
    End Function

    Public Sub Save_Brand_Logo(ByVal BRAND_ID As Integer, ByVal B As Byte())
        Dim FilePath As String = Get_Brand_Logo_Path(BRAND_ID)
        SaveFile(FilePath, B)
    End Sub

    Public Sub Delete_Brand_Logo(ByVal BRAND_ID As Integer)
        Dim FilePath As String = Get_Brand_Logo_Path(BRAND_ID)
        DeleteFile(FilePath)
    End Sub

    Public Function Get_Product_Picture_Path(ByVal PRODUCT_ID As Integer, ByVal Language As UILanguage) As String
        Return PicturePath & "\Product\" & PRODUCT_ID & "_" & Get_Language_Code(Language)
    End Function

    Public Function Get_Product_Picture(ByVal PRODUCT_ID As Integer, ByVal Language As UILanguage) As Byte()
        Dim FilePath As String = Get_Product_Picture_Path(PRODUCT_ID, Language)
        Return ReadFile(FilePath)
    End Function

    Public Sub Save_Product_Picture(ByVal PRODUCT_ID As Integer, ByVal Language As UILanguage, ByVal B As Byte())
        Dim FilePath As String = Get_Product_Picture_Path(PRODUCT_ID, Language)
        SaveFile(FilePath, B)
    End Sub

    Public Sub Delete_Product_Picture(ByVal PRODUCT_ID As Integer, ByVal Language As UILanguage)
        Dim FilePath As String = Get_Product_Picture_Path(PRODUCT_ID, Language)
        DeleteFile(FilePath)
    End Sub

    Public Function Get_SIM_Package_Picture_Path(ByVal SIM_ID As Integer, ByVal Language As UILanguage) As String
        Return PicturePath & "\SIM\Package\" & SIM_ID & "_" & Get_Language_Code(Language)
    End Function

    Public Function Get_SIM_Package_Picture(ByVal SIM_ID As Integer, ByVal Language As UILanguage) As Byte()
        Dim FilePath As String = Get_SIM_Package_Picture_Path(SIM_ID, Language)
        Return ReadFile(FilePath)
    End Function

    Public Sub Save_SIM_Package_Picture(ByVal SIM_ID As Integer, ByVal Language As UILanguage, ByVal B As Byte())
        Dim FilePath As String = Get_SIM_Package_Picture_Path(SIM_ID, Language)
        SaveFile(FilePath, B)
    End Sub

    Public Sub Delete_SIM_Package_Picture(ByVal SIM_ID As Integer, ByVal Language As UILanguage)
        Dim FilePath As String = Get_SIM_Package_Picture_Path(SIM_ID, Language)
        DeleteFile(FilePath)
    End Sub

    Public Function Get_SIM_Detail_Picture_Path(ByVal SIM_ID As Integer, ByVal Language As UILanguage) As String
        Return PicturePath & "\SIM\Detail\" & SIM_ID & "_" & Get_Language_Code(Language)
    End Function

    Public Function Get_SIM_Detail_Picture(ByVal SIM_ID As Integer, ByVal Language As UILanguage) As Byte()
        Dim FilePath As String = Get_SIM_Detail_Picture_Path(SIM_ID, Language)
        Return ReadFile(FilePath)
    End Function

    Public Sub Save_SIM_Detail_Picture(ByVal SIM_ID As Integer, ByVal Language As UILanguage, ByVal B As Byte())
        Dim FilePath As String = Get_SIM_Detail_Picture_Path(SIM_ID, Language)
        SaveFile(FilePath, B)
    End Sub

    Public Sub Delete_SIM_Detail_Picture(ByVal SIM_ID As Integer, ByVal Language As UILanguage)
        Dim FilePath As String = Get_SIM_Detail_Picture_Path(SIM_ID, Language)
        DeleteFile(FilePath)
    End Sub

    Public Sub Bind_DDL_Province(ByRef ddl As DropDownList, Optional ByVal PROVINCE_CODE As String = "")

        Dim SQL As String = "Select * FROM MS_PROVINCE ORDER BY PROVINCE_CODE"
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        ddl.Items.Clear()
        ddl.Items.Add(New ListItem("...", ""))
        For i As Integer = 0 To DT.Rows.Count - 1
            Dim Item As New ListItem(DT.Rows(i).Item("PROVINCE_NAME"), DT.Rows(i).Item("PROVINCE_CODE"))
            ddl.Items.Add(Item)
        Next
        ddl.SelectedIndex = 0
        For i As Integer = 0 To ddl.Items.Count - 1
            If ddl.Items(i).Value = PROVINCE_CODE Then
                ddl.SelectedIndex = i
                Exit For
            End If
        Next

    End Sub

    Public Sub Bind_DDL_Amphur(ByRef ddl As DropDownList, ByVal PROVINCE_CODE As String, Optional ByVal AMPHUR_CODE As String = "")

        Dim SQL As String = "SELECT * FROM MS_AMPHUR WHERE PROVINCE_CODE='" & PROVINCE_CODE.Replace("'", "''") & "' ORDER BY AMPHUR_CODE"
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        ddl.Items.Clear()
        ddl.Items.Add(New ListItem("...", ""))
        For i As Integer = 0 To DT.Rows.Count - 1
            Dim Item As New ListItem(DT.Rows(i).Item("AMPHUR_NAME"), DT.Rows(i).Item("AMPHUR_CODE"))
            ddl.Items.Add(Item)
        Next
        ddl.SelectedIndex = 0
        For i As Integer = 0 To ddl.Items.Count - 1
            If ddl.Items(i).Value = AMPHUR_CODE Then
                ddl.SelectedIndex = i
                Exit For
            End If
        Next

    End Sub

    Public Sub Bind_DDL_Tumbol(ByRef ddl As DropDownList, ByVal AMPHUR_CODE As String, Optional ByVal TUMBOL_CODE As String = "")

        Dim SQL As String = "SELECT * FROM MS_TUMBOL WHERE AMPHUR_CODE='" & AMPHUR_CODE.Replace("'", "''") & "' ORDER BY TUMBOL_CODE"
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        ddl.Items.Clear()
        ddl.Items.Add(New ListItem("...", ""))
        For i As Integer = 0 To DT.Rows.Count - 1
            Dim Item As New ListItem(DT.Rows(i).Item("TUMBOL_NAME"), DT.Rows(i).Item("TUMBOL_CODE"))
            ddl.Items.Add(Item)
        Next
        ddl.SelectedIndex = 0

        For i As Integer = 1 To ddl.Items.Count - 1
            If ddl.Items(i).Value = TUMBOL_CODE Then
                ddl.SelectedIndex = i
                Exit For
            End If
        Next

    End Sub

    Public Sub Bind_DDL_Site(ByRef ddl As DropDownList, Optional ByVal SITE_ID As Integer = 0)

        Dim SQL As String = "Select * FROM MS_Site WHERE Active_Status=1 ORDER BY SITE_CODE"
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        ddl.Items.Clear()
        ddl.Items.Add(New ListItem("...", ""))
        For i As Integer = 0 To DT.Rows.Count - 1
            Dim Item As New ListItem(DT.Rows(i).Item("SITE_NAME"), DT.Rows(i).Item("SITE_ID"))
            ddl.Items.Add(Item)
        Next
        ddl.SelectedIndex = 0
        For i As Integer = 1 To ddl.Items.Count - 1
            If ddl.Items(i).Value = SITE_ID Then
                ddl.SelectedIndex = i
                Exit For
            End If
        Next

    End Sub

    Public Sub Bind_DDL_Brand(ByRef ddl As DropDownList, Optional ByVal BRAND_CODE As String = "")

        Dim SQL As String = "Select * FROM MS_Brand ORDER BY BRAND_CODE"
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        ddl.Items.Clear()
        ddl.Items.Add(New ListItem("...", ""))
        For i As Integer = 0 To DT.Rows.Count - 1
            Dim Item As New ListItem(DT.Rows(i).Item("BRAND_NAME"), DT.Rows(i).Item("BRAND_ID"))
            ddl.Items.Add(Item)
        Next
        ddl.SelectedIndex = 0
        For i As Integer = 0 To ddl.Items.Count - 1
            If ddl.Items(i).Value = BRAND_CODE Then
                ddl.SelectedIndex = i
                Exit For
            End If
        Next

    End Sub

    Public Sub Bind_DDL_Spec(ByRef ddl As DropDownList, ByRef Language As UILanguage, Optional ByVal SPEC_ID As String = "", Optional ByVal Add_SPEC As Boolean = False)
        Dim SQL As String = "Select SPEC_ID, " & vbLf
        Select Case Language
            Case 1
                SQL &= " SPEC_NAME_TH SPEC_NAME " & vbLf
            Case 2
                SQL &= " SPEC_NAME_EN SPEC_NAME " & vbLf
            Case 3
                SQL &= " SPEC_NAME_CN SPEC_NAME " & vbLf
            Case 4
                SQL &= " SPEC_NAME_JP SPEC_NAME " & vbLf
            Case 5
                SQL &= " SPEC_NAME_KR SPEC_NAME " & vbLf
            Case 6
                SQL &= " SPEC_NAME_RS SPEC_NAME " & vbLf
        End Select
        SQL &= "  FROM MS_Spec ORDER BY SPEC_NAME"

        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        ddl.Items.Clear()
        ddl.Items.Add(New ListItem("...", ""))
        For i As Integer = 0 To DT.Rows.Count - 1
            Dim Item As New ListItem(DT.Rows(i).Item("SPEC_NAME"), DT.Rows(i).Item("SPEC_ID"))
            ddl.Items.Add(Item)
        Next
        If Add_SPEC Then
            ddl.Items.Add(New ListItem("+ Spec", "-1"))
        End If

        ddl.SelectedIndex = 0
        For i As Integer = 0 To ddl.Items.Count - 1
            If ddl.Items(i).Value = SPEC_ID Then
                ddl.SelectedIndex = i
                Exit For
            End If
        Next

    End Sub

#Region "Product"

    Public Function Get_Product_Info_From_ID(ByVal PRODUCT_ID As Integer) As DataTable
        Dim SQL As String = "SELECT * FROM VW_ALL_PRODUCT WHERE PRODUCT_ID=" & PRODUCT_ID
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

    Public Function Get_Product_Info_From_Code(ByVal PRODUCT_CODE As String) As DataTable
        Dim SQL As String = "SELECT * FROM VW_ALL_PRODUCT WHERE PRODUCT_CODE='" & PRODUCT_CODE.Replace("'", "''") & "'"
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

    Public Class BarcodeScanResult
        Public PRODUCT_ID As Integer = 0
        Public PRODUCT_CODE As String = ""
        Public DISPLAY_NAME As String = ""
        Public IS_SERIAL As Boolean = False
        Public TYPE As String = ""

        Public WIDTH As Integer = 0
        Public HEIGHT As Integer = 0
        Public DEPTH As Integer = 0

        Public SERIAL_NO As String = ""

        Public Result As Boolean = False
        Public Message As String = "Not found"

    End Class

    Public Function Get_Product_Barcode_Scan_Result(ByVal Shop_Code As String, ByVal Search As String) As BarcodeScanResult
        Dim Result As New BarcodeScanResult

        Dim SQL As String = "SELECT PRODUCT_ID,PRODUCT_CODE,DISPLAY_NAME,ISNULL(IS_SERIAL,0) IS_SERIAL,GS1,TYPE" & vbLf
        SQL &= ", WIDTH, HEIGHT, DEPTH, SERIAL_NO, SLOT_ID" & vbLf
        SQL &= " From VW_PRODUCT_FOR_SCAN" & vbLf
        SQL &= "WHERE PRODUCT_CODE='" & Search.Replace("'", "''") & "' OR GS1 ='" & Search.Replace("'", "''") & "'"

        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)

        If DT.Rows.Count > 0 Then '---------- ยิง MAT CODE มา-------------
            Result.PRODUCT_ID = DT.Rows(0).Item("PRODUCT_ID")
            Result.PRODUCT_CODE = DT.Rows(0).Item("PRODUCT_CODE").ToString
            Result.DISPLAY_NAME = DT.Rows(0).Item("DISPLAY_NAME").ToString
            Result.IS_SERIAL = DT.Rows(0).Item("IS_SERIAL")
            Result.TYPE = DT.Rows(0).Item("TYPE").ToString
            If Not IsDBNull(DT.Rows(0).Item("WIDTH")) Then Result.WIDTH = DT.Rows(0).Item("WIDTH")
            If Not IsDBNull(DT.Rows(0).Item("HEIGHT")) Then Result.HEIGHT = DT.Rows(0).Item("HEIGHT")
            If Not IsDBNull(DT.Rows(0).Item("DEPTH")) Then Result.DEPTH = DT.Rows(0).Item("DEPTH")

            If DT.Rows(0).Item("IS_SERIAL") Then
                '---------------- เป็น Product Serial แต่ไม่ได้ยิง Serial มา --------------
                Result.Result = False
                Result.Message = "Product นี้ต้องยิง Serial"
                Return Result
            Else
                Result.Result = True
                Result.Message = "Success"
                Return Result
            End If
        Else
            '----------- Get From TSM : Hardcode For Test--------------
            Result.IS_SERIAL = True
            Result.SERIAL_NO = Search
            Dim TSM As BackEndInterface.Validate_Serial.Response = Get_TSM_Product_Code_From_Serial(Shop_Code, Search)

            If Not IsNothing(TSM) AndAlso
                 Not IsNothing(TSM.ReturnValues) AndAlso
                 TSM.ReturnValues.Count > 1 AndAlso
                 Not IsNothing(TSM.ReturnValues(0)) AndAlso
                 Not IsNothing(TSM.ReturnValues(1)) AndAlso
                 Not CBool(TSM.ReturnValues(1)) AndAlso
                TSM.ReturnValues(0) <> "" Then
                Result.PRODUCT_CODE = TSM.ReturnValues(0)
            End If

            If Result.PRODUCT_CODE = "" Then
                Result.Result = False
                Result.Message = "ไม่พบเลข Barcode"
                Return Result
            Else
                SQL = "SELECT PRODUCT_ID,PRODUCT_CODE,DISPLAY_NAME,IS_SERIAL,GS1,TYPE" & vbLf
                SQL &= ",WIDTH, HEIGHT, DEPTH, SERIAL_NO, SLOT_ID" & vbLf
                SQL &= " From VW_PRODUCT_FOR_SCAN" & vbLf
                SQL &= "WHERE PRODUCT_CODE='" & Result.PRODUCT_CODE.Replace("'", "''") & "'"
                DT = New DataTable
                DA = New SqlDataAdapter(SQL, ConnectionString)
                DA.Fill(DT)
                If DT.Rows.Count = 0 Then
                    Result.Result = False
                    Result.Message = "ไม่พบ Product Code"
                    Return Result
                Else
                    Result.PRODUCT_ID = DT.Rows(0).Item("PRODUCT_ID")
                    Result.DISPLAY_NAME = DT.Rows(0).Item("DISPLAY_NAME").ToString
                    Result.TYPE = DT.Rows(0).Item("TYPE").ToString
                    If Not IsDBNull(DT.Rows(0).Item("WIDTH")) Then Result.WIDTH = DT.Rows(0).Item("WIDTH")
                    If Not IsDBNull(DT.Rows(0).Item("HEIGHT")) Then Result.HEIGHT = DT.Rows(0).Item("HEIGHT")
                    If Not IsDBNull(DT.Rows(0).Item("DEPTH")) Then Result.DEPTH = DT.Rows(0).Item("DEPTH")

                    '---------------- ตรวจสอบว่ามีอยู่ใน Stock ปัจจุบันหรือไม่----------------
                    DT.DefaultView.RowFilter = "SERIAL_NO='" & Search.Replace("'", "''") & "'"
                    If DT.DefaultView.Count > 0 Then
                        Result.Result = False
                        Result.Message = "สินค้านี้มีอยู่ใน Stock แล้ว (ซ้ำ)"
                        Return Result
                    Else
                        Result.Result = True
                        Result.Message = "Success"
                        Return Result
                    End If
                End If
            End If
        End If
    End Function

    Public Function Get_SIM_Barcode_Scan_Result(ByVal Shop_Code As String, ByVal Search As String) As BarcodeScanResult
        Dim Result As New BarcodeScanResult
        Result.IS_SERIAL = True
        Result.TYPE = "SIM"

        Dim SQL As String = "SELECT * FROM MS_SIM " & vbLf
        SQL &= " WHERE PRODUCT_CODE='" & Search.Replace("'", "''") & "'"

        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            Result.PRODUCT_ID = DT.Rows(0).Item("SIM_ID")
            Result.PRODUCT_CODE = DT.Rows(0).Item("PRODUCT_CODE").ToString
            Result.DISPLAY_NAME = DT.Rows(0).Item("DISPLAY_NAME").ToString
            Result.Result = False
            Result.Message = "Product นี้ต้องยิง Serial"
            Return Result
        End If

        '----------- Get mat Code From TSM----------

        SQL = "SELECT * " & vbLf
        SQL &= "FROM TMP_SIM" & vbLf
        SQL &= "WHERE SERIAL_NO='" & Search.Replace("'", "''") & "'"
        DT = New DataTable
        DA = New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)

        If DT.Rows.Count = 0 Then
            Result.Result = False
            Result.Message = "ไม่พบ Product Code"
            Return Result
        Else
            Result.PRODUCT_CODE = DT.Rows(0).Item("PRODUCT_CODE")
            Result.SERIAL_NO = Search

            SQL = "SELECT * " & vbLf
            SQL &= "From VW_SIM_FOR_SCAN" & vbLf
            SQL &= "WHERE PRODUCT_CODE='" & Result.PRODUCT_CODE.Replace("'", "''") & "'"
            DT = New DataTable
            DA = New SqlDataAdapter(SQL, ConnectionString)
            DA.Fill(DT)
            Result.PRODUCT_ID = DT.Rows(0).Item("SIM_ID")
            Result.DISPLAY_NAME = DT.Rows(0).Item("DISPLAY_NAME")
            DT.DefaultView.RowFilter = "SERIAL_NO='" & Search.Replace("'", "''") & "'"
            If DT.DefaultView.Count > 0 Then
                Result.Result = False
                Result.Message = "สินค้านี้มีอยู่ใน Stock แล้ว (ซ้ำ)"
                Return Result
            Else
                Result.Result = True
                Result.Message = "Success"
                Return Result
            End If
        End If
    End Function

    Public Function Get_TSM_Product_Code_From_Serial(ByVal SHOP As String, ByVal SERIAL As String) As BackEndInterface.Validate_Serial.Response
        ''----------- Get From TSM : Hardcode For Test--------------
        'Dim SQL As String = "SELECT PRODUCT_CODE,SERIAL_NO FROM TMP_Serial" & vbLf
        'SQL &= "WHERE SERIAL_NO='" & SERIAL_NO.Replace("'", "''") & "'"

        'Dim DT As New DataTable
        'Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        'DA.Fill(DT)

        'If DT.Rows.Count = 0 Then
        '    Return ""
        'Else
        '    Return DT.Rows(0).Item("PRODUCT_CODE").ToString
        'End If
        '---------------------------- Get From TSM -------------------------------
        Dim TSM As New BackEndInterface.Validate_Serial
        Dim Resp As BackEndInterface.Validate_Serial.Response = Nothing
        Try
            Resp = TSM.Get_Result(SHOP, SERIAL)
        Catch ex As Exception
            Resp = New BackEndInterface.Validate_Serial.Response
            With Resp
                .ErrorMessage = ex.Message
            End With
        End Try


        Return Resp

    End Function

    Public Sub Save_Product_Movement_Log(ByVal SHIFT_ID As Integer, ByVal SHIFT_STATUS As ShiftStatus, ByVal PRODUCT_ID As Integer, ByVal SERIAL_NO As String, ByVal MOVE_ID As StockMovementType, ByVal MOVE_FROM As String, ByVal MOVE_FROM_SLOT As Integer, ByVal MOVE_TO As String, ByVal MOVE_TO_SLOT As Integer, ByVal REMARK As String, ByVal MOVE_BY As Integer, ByVal MOVE_TIME As DateTime)
        Dim SQL As String = "Select TOP 0 * FROM TB_PRODUCT_MOVEMENT"
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, LogConnectionString)
        DA.Fill(DT)
        Dim DR As DataRow = DT.NewRow

        DR("HIS_ID") = Get_NewID_Log("TB_PRODUCT_MOVEMENT", "HIS_ID")

        If SHIFT_ID <> 0 Then DR("SHIFT_ID") = SHIFT_ID
        If SHIFT_STATUS <> ShiftStatus.Unknown Then DR("SHIFT_STATUS") = SHIFT_STATUS

        DR("PRODUCT_ID") = PRODUCT_ID
        DR("SERIAL_NO") = SERIAL_NO
        DR("MOVE_ID") = MOVE_ID
        DR("MOVE_FROM") = MOVE_FROM
        If MOVE_FROM_SLOT <> 0 Then DR("MOVE_FROM_SLOT") = MOVE_FROM_SLOT
        DR("MOVE_TO") = MOVE_TO
        If MOVE_TO_SLOT <> 0 Then DR("MOVE_TO_SLOT") = MOVE_TO_SLOT
        DR("REMARK") = REMARK
        DR("MOVE_BY") = MOVE_BY
        DR("MOVE_TIME") = MOVE_TIME

        DT.Rows.Add(DR)
        Dim cmd As New SqlCommandBuilder(DA)
        DA.Update(DT)
    End Sub

    Public Sub Save_SIM_Movement_Log(ByVal SHIFT_ID As Integer, ByVal SHIFT_STATUS As ShiftStatus, ByVal SIM_ID As Integer, ByVal SERIAL_NO As String, ByVal MOVE_ID As StockMovementType, ByVal MOVE_FROM As String, ByVal MOVE_FROM_SLOT As Integer, ByVal MOVE_TO As String, ByVal MOVE_TO_SLOT As Integer, ByVal REMARK As String, ByVal MOVE_BY As Integer, ByVal MOVE_TIME As DateTime)
        Dim SQL As String = "Select TOP 0 * FROM TB_SIM_MOVEMENT"
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, LogConnectionString)
        DA.Fill(DT)
        Dim DR As DataRow = DT.NewRow
        DR("HIS_ID") = Get_NewID_Log("TB_SIM_MOVEMENT", "HIS_ID")

        If SHIFT_ID <> 0 Then DR("SHIFT_ID") = SHIFT_ID
        If SHIFT_STATUS <> ShiftStatus.Unknown Then DR("SHIFT_STATUS") = SHIFT_STATUS

        DR("SIM_ID") = SIM_ID
        DR("SERIAL_NO") = SERIAL_NO
        DR("MOVE_ID") = MOVE_ID
        DR("MOVE_FROM") = MOVE_FROM
        If MOVE_FROM_SLOT <> 0 Then DR("MOVE_FROM_SLOT") = MOVE_FROM_SLOT
        DR("MOVE_TO") = MOVE_TO
        If MOVE_TO_SLOT <> 0 Then DR("MOVE_TO_SLOT") = MOVE_TO_SLOT
        DR("REMARK") = REMARK
        DR("MOVE_BY") = MOVE_BY
        DR("MOVE_TIME") = MOVE_TIME

        DT.Rows.Add(DR)
        Dim cmd As New SqlCommandBuilder(DA)
        DA.Update(DT)

    End Sub


#End Region

#Region "Kiosk Management"



    Public Sub Bind_Product_Shelf_Layout(ByRef Shelf As UC_Product_Shelf, ByVal KO_ID As Integer)

        Shelf.ResetDimension()
        Shelf.ClearAllFloor()
        '--------------- BindShelf ----------------
        Dim SQL As String = "Select * FROM TB_PRODUCT_SHELF WHERE KO_ID=" & KO_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)
        If DT.Rows.Count = 0 Then Exit Sub

        Shelf.SHELF_ID = DT.Rows(0).Item("SHELF_ID")
        Shelf.SHELF_DEPTH = DT.Rows(0).Item("DEPTH")
        Shelf.SHELF_WIDTH = DT.Rows(0).Item("WIDTH")
        Shelf.SHELF_HEIGHT = DT.Rows(0).Item("HEIGHT")

        '------------ Bind Floor -----------------
        SQL = "Select FLOOR_ID, HEIGHT, POS_Y, " & vbLf
        SQL &= "0 HighLight, CAST(1 As BIT) ShowFloorName, CAST(1 As BIT) ShowMenu, NULL SlotDatas " & vbLf
        SQL &= "FROM TB_PRODUCT_FLOOR" & vbLf
        SQL &= "WHERE SHELF_ID=" & Shelf.SHELF_ID & vbLf
        SQL &= " ORDER BY FLOOR_ORDER" & vbLf
        DT = New DataTable
        DA = New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)

        For f As Integer = 0 To DT.Rows.Count - 1

            Dim FLOOR_ID As Integer = DT.Rows(f).Item("FLOOR_ID")
            Dim FLOOR_HEIGHT As Integer = DT.Rows(f).Item("HEIGHT")
            Dim POS_Y As Integer = DT.Rows(f).Item("POS_Y")

            Shelf.AddFloor(FLOOR_ID, FLOOR_HEIGHT, POS_Y, False, True, Nothing, True, f)
            Dim Floor As UC_Product_Floor = Shelf.Floors(f)
            '-------------- Bind Slot --------------
            SQL = "Select * FROM TB_PRODUCT_SLOT " & vbLf
            SQL &= "WHERE FLOOR_ID=" & FLOOR_ID & vbLf
            SQL &= "ORDER BY SLOT_ORDER"
            Dim ST = New DataTable
            DA = New SqlDataAdapter(SQL, ConnectionString)
            DA.Fill(ST)

            For s As Integer = 0 To ST.Rows.Count - 1
                Dim SLOT_ID As Integer = ST.Rows(s).Item("SLOT_ID")
                Dim POS_X As Integer = ST.Rows(s).Item("POS_X")
                Dim SLOT_WIDTH As Integer = ST.Rows(s).Item("WIDTH")
                Floor.AddSlot(SLOT_ID, Chr(Asc("A") + f) & "-" & s + 1, POS_X, SLOT_WIDTH, 0, "", 0, "", Drawing.Color.White, Drawing.Color.White, False)
            Next

        Next
    End Sub

    Public Sub Bind_Product_Shelf_Stock(ByRef Shelf As UC_Product_Shelf, ByVal KO_ID As Integer)
        Dim SQL As String = "SELECT PRODUCT_ID,PRODUCT_CODE,PRODUCT_NAME,SERIAL_NO,SLOT_NAME" & vbLf
        SQL &= "FROM VW_CURRENT_PRODUCT_STOCK" & vbLf
        SQL &= "WHERE KO_ID=" & KO_ID
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Bind_Product_Shelf_Stock(Shelf, DT)
    End Sub

    Public Sub Bind_Product_Shelf_Stock(ByRef Shelf As UC_Product_Shelf, ByVal STOCK_DATA As DataTable, Optional ByVal VW_ALL_PRODUCT As DataTable = Nothing)

        If IsNothing(VW_ALL_PRODUCT) Then
            Dim SQL As String = "SELECT * FROM VW_ALL_PRODUCT"
            Dim DA As New SqlDataAdapter(SQL, ConnectionString)
            VW_ALL_PRODUCT = New DataTable
            DA.Fill(VW_ALL_PRODUCT)
        End If


        Dim DT As DataTable = STOCK_DATA.Copy
        Dim Slots As List(Of UC_Product_Slot) = Shelf.Slots
        For i As Integer = 0 To Slots.Count - 1
            DT.DefaultView.RowFilter = "SLOT_NAME='" & Slots(i).SLOT_NAME & "'"
            If DT.DefaultView.Count = 0 Then
                Slots(i).PRODUCT_ID = 0
                Slots(i).PRODUCT_CODE = ""
                Slots(i).PRODUCT_QUANTITY = 0
                Slots(i).ShowQuantity = False
                Slots(i).ShowMask = True
                Slots(i).MaskContent = "<b class='text-default'>EMPTY</b>"
            Else
                Slots(i).PRODUCT_ID = DT.DefaultView(0).Item("PRODUCT_ID")
                Slots(i).PRODUCT_CODE = DT.DefaultView(0).Item("PRODUCT_CODE")
                Slots(i).PRODUCT_QUANTITY = DT.DefaultView.Count
                '----------- Calculate Percent -------------
                VW_ALL_PRODUCT.DefaultView.RowFilter = "PRODUCT_ID=" & Slots(i).PRODUCT_ID
                Dim DV As DataRowView = VW_ALL_PRODUCT.DefaultView(0)

                If Not IsDBNull(DV("DEPTH")) AndAlso DV("DEPTH") > 0 Then
                    Dim MaxQuantity As Integer = Math.Floor(Shelf.SHELF_DEPTH / DV("DEPTH"))
                    Dim Percent As Integer = Math.Floor((Slots(i).PRODUCT_QUANTITY * 100) / MaxQuantity)
                    Slots(i).PRODUCT_LEVEL_PERCENT = Percent & "%"
                    If Percent <= Product_Critical_Percent Then
                        Slots(i).PRODUCT_LEVEL_COLOR = Drawing.Color.Red
                    Else
                        Slots(i).PRODUCT_LEVEL_COLOR = Drawing.Color.Green
                    End If
                    Slots(i).QUANTITY_BAR_COLOR = Drawing.Color.FromKnownColor(Drawing.KnownColor.Silver)
                    Slots(i).ShowQuantity = True
                    Slots(i).ShowMask = False
                    Slots(i).MaskContent = ""
                Else
                    Slots(i).ShowProductCode = True
                    Slots(i).ShowQuantity = False
                    Slots(i).ShowMask = True
                    Slots(i).MaskContent = "<small class='text-warning'> Dimension is not set</small>"
                End If
            End If
        Next
    End Sub

    Public Sub Bind_SIMDispenser_Layout(ByRef Dispenser As UC_SIM_Dispenser, ByVal KO_ID As Integer, Optional ByVal Column12Style As Integer = 3)
        Dispenser.ClearAllSlot()
        Dispenser.KO_ID = KO_ID

        Dim SQL As String = "SELECT KO_ID,D.D_ID DEVICE_ID,D_Name SLOT_NAME,D.Max_Qty MAX_CAPACITY" & vbLf
        SQL &= "FROM TB_KIOSK_DEVICE KD" & vbLf
        SQL &= "INNER JOIN MS_DEVICE D On KD.D_ID=D.D_ID" & vbLf
        SQL &= "WHERE D.D_ID In (11,12,13) And KO_ID=" & KO_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)

        If DT.Rows.Count = 0 Then Exit Sub

        For i As Integer = 0 To DT.Rows.Count - 1
            Dispenser.AddSlot(DT.Rows(i).Item("SLOT_NAME"), DT.Rows(i).Item("DEVICE_ID"), DT.Rows(i).Item("MAX_CAPACITY"), 0, "", 0, False, True,
                              Drawing.Color.Green, UC_Product_Slot.HighLightMode.None, Column12Style)
        Next

    End Sub

    Public Function Get_Current_SIM_Stock(ByVal KO_ID As Integer) As DataTable
        Dim SQL As String = "SELECT SIM_ID,PRODUCT_CODE,PRODUCT_NAME,SERIAL_NO,SLOT_NAME RECENT,SLOT_NAME [CURRENT]" & vbLf
        SQL &= " FROM VW_CURRENT_SIM_STOCK" & vbLf
        SQL &= "WHERE KO_ID=" & KO_ID
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

    Public Function Get_SIM_SLOT_INFO(Optional ByVal KO_ID As Integer = 0) As DataTable
        Dim SQL As String = "SELECT KO_ID,D_ID,SLOT_NAME,SIM_ID,PRODUCT_CODE,PRODUCT_NAME,PRICE" & vbLf
        SQL &= ", COUNT(SERIAL_NO) TOTAL_SIM" & vbLf
        SQL &= " FROM VW_CURRENT_SIM_STOCK" & vbLf
        Dim Filter As String = ""
        If KO_ID <> 0 Then
            Filter &= " KO_ID=" & KO_ID & " AND "
        End If
        If Filter <> "" Then
            SQL &= " WHERE " & Filter.Substring(0, Filter.Length - 4) & vbLf
        End If
        SQL &= " GROUP BY KO_ID,D_ID,SLOT_NAME,SIM_ID,PRODUCT_CODE,PRODUCT_NAME,PRICE"
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

    Public Sub Bind_SIMDispenser_Stock(ByRef Dispenser As UC_SIM_Dispenser, ByVal STOCK_DATA As DataTable, Optional ByVal VW_ALL_SIM As DataTable = Nothing)
        If IsNothing(VW_ALL_SIM) Then
            Dim SQL As String = "SELECT * FROM VW_ALL_SIM"
            Dim DA As New SqlDataAdapter(SQL, ConnectionString)
            VW_ALL_SIM = New DataTable
            DA.Fill(VW_ALL_SIM)
        End If

        Dim DT As DataTable = STOCK_DATA.Copy
        Dim Slots As List(Of UC_SIM_Slot) = Dispenser.Slots
        For i As Integer = 0 To Slots.Count - 1
            DT.DefaultView.RowFilter = "SLOT_NAME='" & Slots(i).SLOT_NAME & "'"

            Slots(i).ClearAllSIM()
            If DT.DefaultView.Count = 0 Then
                Slots(i).SIM_ID = 0
                Slots(i).SIM_CODE = ""
                Slots(i).SIM_PRICE = "-"
                Slots(i).ShowPointer = False
                Slots(i).ShowSIMProfile = False
            Else
                Slots(i).SIM_ID = DT.DefaultView(0).Item("SIM_ID")
                Slots(i).SIM_CODE = DT.DefaultView(0).Item("PRODUCT_CODE")
                VW_ALL_SIM.DefaultView.RowFilter = "SIM_ID=" & Slots(i).SIM_ID
                Slots(i).SIM_PRICE = CInt(VW_ALL_SIM.DefaultView(0)("PRICE"))

                Slots(i).ShowPointer = True
                Slots(i).ShowSIMProfile = True

                Dim Quantity As Integer = DT.DefaultView.Count
                Dim Display_Color As Drawing.Color = Drawing.Color.White
                If (Quantity * 100 / Slots(i).MAX_CAPACITY) < 20 Then
                    Display_Color = Drawing.Color.Red
                ElseIf (Quantity * 100 / Slots(i).MAX_CAPACITY) < 40 Then
                    Display_Color = Drawing.Color.Orange
                Else
                    Display_Color = Drawing.Color.Green
                End If
                Dim Col() As String = {"SERIAL_NO"}
                Dim SIMSData As DataTable = DT.DefaultView.ToTable(True, Col)
                SIMSData.Columns.Add("DISPLAY_COLOR", GetType(Drawing.Color))
                For j As Integer = 0 To SIMSData.Rows.Count - 1
                    SIMSData.Rows(j).Item("DISPLAY_COLOR") = Display_Color
                Next
                Slots(i).AddSIM(SIMSData)
            End If
        Next
    End Sub

    Public Function Get_Arm_Position_From_SlotName(ByVal SLOT_NAME As String) As Integer
        Dim SQL As String = "Select * FROM MS_SLOT_ARM_POSITION WHERE SLOT_NAME='" & SLOT_NAME.Replace("'", "''") & "'"
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)

        If DT.Rows.Count = 0 Then Return 0
        Return DT.Rows(0)("POS_ID")
    End Function

    Public Function GetList_Kiosk(Optional ByVal KO_ID As Integer = 0) As DataTable
        Dim SQL As String = ""
        SQL &= " Select KO.KO_ID, KO.KO_CODE, KO.SITE_ID, Site.SITE_CODE,Site.SITE_NAME, KO.ZONE, KO.IP, KO.Mac   " & vbLf
        SQL &= " , KO.IsOnline, COUNT(SLOT.PRODUCT_ID) Total_Product, KO.Active_Status " & vbLf
        SQL &= " FROM MS_KIOSK KO           " & vbLf
        SQL &= " INNER JOIN MS_Site Site On KO.SITE_ID = Site.SITE_ID" & vbLf
        SQL &= " LEFT JOIN VW_KIOSK_SLOT SLOT On  KO.KO_ID=SLOT.KO_ID         " & vbLf
        SQL &= " " & vbLf
        If KO_ID <> 0 Then
            SQL &= " WHERE KO.KO_ID=" & KO_ID & vbLf
        End If
        SQL &= " GROUP BY KO.KO_ID, KO.KO_CODE, KO.SITE_ID, Site.SITE_CODE,Site.SITE_NAME, KO.ZONE, KO.IP, KO.Mac, KO.IsOnline, KO.Active_Status" & vbLf
        SQL &= " ORDER BY KO.KO_ID" & vbLf
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

    Public Function GetList_Device_Master() As DataTable
        Dim SQL As String = "SELECT * FROM MS_DEVICE WHERE Active_Status=1"
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DT.TableName = "Data"
        DA.Fill(DT)
        Return DT
    End Function

    Public Function GetList_Kiosk_Device(Optional ByVal KO_ID As Integer = 0) As DataTable
        Dim SQL As String = ""
        SQL &= " Select " & vbLf
        SQL &= " K.KO_ID, D.D_ID, D.D_Name, D.DT_ID, DT.DT_Name, " & vbLf
        SQL &= " D.Unit_Value, " & vbLf
        SQL &= " ISNULL(D.Max_Qty, D.Max_Qty) Max_Qty, " & vbLf
        SQL &= " ISNULL(D.Warning_Qty, D.Warning_Qty) Warning_Qty, " & vbLf
        SQL &= " ISNULL(D.Critical_Qty, D.Critical_Qty) Critical_Qty,KD.Current_Qty,DT.Movement_Direction," & vbLf
        SQL &= " DS.DS_ID,DS.DS_Name,DS.IsProblem,KD.Update_Time,D.D_Order,Icon_White,Icon_Red,Icon_Green" & vbLf
        SQL &= " FROM " & vbLf
        SQL &= " MS_KIOSK K " & vbLf
        SQL &= " INNER JOIN MS_DEVICE D On K.Active_Status=1 And D.Active_Status=1" & vbLf
        SQL &= " INNER JOIN MS_DEVICE_TYPE DT On D.DT_ID= DT.DT_ID " & vbLf
        SQL &= " LEFT JOIN TB_KIOSK_DEVICE KD On D.D_ID=KD.D_ID And KD.KO_ID=K.KO_ID" & vbLf
        SQL &= " LEFT JOIN MS_DEVICE_STATUS DS On KD.DS_ID=DS.DS_ID" & vbLf
        SQL &= "                             And D.DT_ID=DS.DT_ID" & vbLf
        If KO_ID <> 0 Then
            SQL &= " WHERE K.KO_ID=" & KO_ID & vbLf
        End If
        SQL &= " ORDER BY D.D_Order" & vbLf

        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DT.TableName = "Data"
        DA.Fill(DT)
        Return DT
    End Function

    Public Function GetList_Device_Status(Optional ByVal DT_ID As Integer = 0, Optional ByVal DS_ID As Integer = 0) As DataTable
        Dim SQL As String = ""
        SQL &= " Select DT_ID, DS_ID, DS_Name, IsProblem " & vbLf
        SQL &= " FROM MS_DEVICE_STATUS " & vbLf

        Dim Filter As String = ""
        If DT_ID <> 0 Then
            Filter &= " DT_ID = " & DT_ID & " And "
        End If
        If DS_ID <> 0 Then
            Filter &= " DS_ID = " & DS_ID & " And "
        End If
        If Filter <> "" Then
            Filter = " WHERE " & Filter.Substring(0, Filter.Length - 4)
        End If
        SQL &= Filter

        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function



    Public Sub Drop_Kiosk(ByVal KO_ID As Integer)
        '--------- Delete Relation---------
        Drop_PRODUCT_SHELF(KO_ID)
        Drop_SIM_SLOT(KO_ID)
        Drop_KIOSK_DEVICE(KO_ID)
        Drop_SHIFT(KO_ID)

        Dim SQL As String = "DELETE FROM MS_KIOSK WHERE KO_ID=" & KO_ID
        ExecuteNonQuery(SQL)
    End Sub

    Public Sub Drop_PRODUCT_STOCK_SERIAL(ByVal SERIAL_NO As String)
        Dim SQL As String = "Select SLOT_ID FROM TB_PRODUCT_SERIAL" & vbLf
        SQL &= "WHERE SERIAL_NO='" & SERIAL_NO.Replace("'", "''") & "'"
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)

        For i As Integer = 0 To DT.Rows.Count - 1
            Drop_PRODUCT_STOCK_SERIAL(DT.Rows(0).Item("SLOT_ID"), SERIAL_NO)
        Next
    End Sub

    Public Sub Drop_PRODUCT_STOCK_SERIAL(ByVal SLOT_ID As Integer)
        Dim SQL As String = "SELECT SERIAL_NO FROM TB_PRODUCT_SERIAL" & vbLf
        SQL &= "WHERE SLOT_ID=" & SLOT_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)

        For i As Integer = 0 To DT.Rows.Count - 1
            Drop_PRODUCT_STOCK_SERIAL(SLOT_ID, DT.Rows(0).Item("SERIAL"))
        Next
    End Sub

    Public Sub Drop_PRODUCT_STOCK_SERIAL(ByVal SLOT_ID As Integer, ByVal SERIAL_NO As String)
        Dim SQL As String = "DELETE FROM TB_PRODUCT_SERIAL" & vbLf
        SQL &= "WHERE SLOT_ID=" & SLOT_ID & " AND SERIAL_NO='" & SERIAL_NO.Replace("'", "''") & "'"
        ExecuteNonQuery(SQL)
    End Sub

    Public Sub Drop_PRODUCT_SLOT(ByVal FLOOR_ID As Integer, ByVal SLOT_ID As Integer)
        Drop_PRODUCT_STOCK_SERIAL(SLOT_ID)
        Dim SQL As String = "DELETE FROM TB_PRODUCT_SLOT WHERE FLOOR_ID=" & FLOOR_ID & " AND SLOT_ID=" & SLOT_ID
        ExecuteNonQuery(SQL)
    End Sub

    Public Sub Drop_PRODUCT_SLOT(ByVal FLOOR_ID As Integer)
        Dim SQL As String = "SELECT SLOT_ID FROM TB_PRODUCT_SLOT WHERE FLOOR_ID=" & FLOOR_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)
        For i As Integer = 0 To DT.Rows.Count - 1
            Drop_PRODUCT_SLOT(FLOOR_ID, DT.Rows(i).Item("SLOT_ID"))
        Next
    End Sub

    Public Sub Drop_PRODUCT_FLOOR(ByVal SHELF_ID As Integer, ByVal FLOOR_ID As Integer)
        Drop_PRODUCT_SLOT(FLOOR_ID)

        Dim SQL As String = "DELETE FROM TB_PRODUCT_FLOOR" & vbLf
        SQL &= "WHERE SHELF_ID=" & SHELF_ID & " AND FLOOR_ID=" & FLOOR_ID
        ExecuteNonQuery(SQL)
    End Sub

    Public Sub Drop_PRODUCT_FLOOR(ByVal SHELF_ID As Integer)
        Dim SQL As String = "SELECT FLOOR_ID FROM TB_PRODUCT_FLOOR WHERE SHELF_ID=" & SHELF_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)

        For i As Integer = 0 To DT.Rows.Count - 1
            Drop_PRODUCT_FLOOR(SHELF_ID, DT.Rows(i).Item("FLOOR_ID"))
        Next
    End Sub

    Public Sub Drop_PRODUCT_SHELF(ByVal KO_ID As Integer)
        Drop_PRODUCT_FLOOR(KO_ID)

        Dim SQL As String = "DELETE FROM TB_PRODUCT_SHELF" & vbLf
        SQL &= "WHERE KO_ID=" & KO_ID
        ExecuteNonQuery(SQL)
    End Sub

    Public Sub Drop_SIM_SLOT(ByVal KO_ID As Integer)
        Drop_SIM_Serial(KO_ID)

        Dim SQL As String = "DELETE FROM TB_KIOSK_DEVICE" & vbLf
        SQL &= "WHERE KO_ID=" & KO_ID
        ExecuteNonQuery(SQL)
    End Sub

    Public Sub Drop_SIM_SLOT(ByVal KO_ID As Integer, ByVal D_ID As Integer)
        Drop_SIM_Serial(KO_ID)

        Dim SQL As String = "DELETE FROM TB_KIOSK_DEVICE" & vbLf
        SQL &= "WHERE KO_ID=" & KO_ID & " AND D_ID=" & D_ID
        ExecuteNonQuery(SQL)
    End Sub

    Public Sub Drop_SIM_SERIAL(ByVal KO_ID As Integer)
        Dim SQL As String = "SELECT D_ID FROM TB_SIM_SERIAL WHERE KO_ID=" & KO_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)
        For i As Integer = 0 To DT.Rows.Count - 1
            Drop_SIM_SERIAL(KO_ID, DT.Rows(i).Item("D_ID"))
        Next
    End Sub

    Public Sub Drop_SIM_SERIAL(ByVal KO_ID As Integer, ByVal D_ID As Integer)
        Dim SQL As String = "SELECT SERIAL_NO FROM TB_SIM_SERIAL WHERE KO_ID=" & KO_ID & " AND D_ID=" & D_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)
        For i As Integer = 0 To DT.Rows.Count - 1
            Drop_SIM_SERIAL(KO_ID, D_ID, DT.Rows(i).Item("SERIAL_NO"))
        Next
    End Sub

    Public Sub Drop_SIM_SERIAL(ByVal KO_ID As Integer, ByVal D_ID As Integer, ByVal SERIAL_NO As String)
        Dim SQL As String = "DELETE FROM TB_SIM_SERIAL" & vbLf
        SQL &= "WHERE KO_ID=" & KO_ID & " AND D_ID=" & D_ID & " AND SERIAL_NO='" & SERIAL_NO.Replace("'", "''") & "'"
        ExecuteNonQuery(SQL)
    End Sub

    Public Sub Drop_KIOSK_DEVICE(ByVal KO_ID As Integer, ByVal D_ID As Integer)
        Dim SQL As String = "DELETE FROM TB_KIOSK_DEVICE" & vbLf
        SQL &= "WHERE KO_ID=" & KO_ID & " AND D_ID=" & D_ID
        ExecuteNonQuery(SQL)
    End Sub

    Public Sub Drop_KIOSK_DEVICE(ByVal KO_ID As Integer)
        Dim SQL As String = "SELECT D_ID FROM TB_KIOSK_DEVICE WHERE KO_ID=" & KO_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)

        For i As Integer = 0 To DT.Rows.Count - 1
            Drop_KIOSK_DEVICE(KO_ID, DT.Rows(i).Item("D_ID"))
        Next
    End Sub

    Public Sub Drop_SHIFT(ByVal KO_ID As Integer, ByVal SHIFT_ID As Integer)
        '--------- Delete Relation If Exists---------
        Dim SQL As String = "DELETE FROM TB_SHIFT" & vbLf
        SQL &= "WHERE KO_ID=" & KO_ID & " AND SHIFT_ID=" & SHIFT_ID
        ExecuteNonQuery(SQL)
    End Sub

    Public Sub Drop_SHIFT(ByVal KO_ID As Integer)
        Dim SQL As String = "SELECT SHIFT_ID FROM TB_SHIFT WHERE KO_ID=" & KO_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)
        For i As Integer = 0 To DT.Rows.Count - 1
            Drop_SHIFT(KO_ID, DT.Rows(i).Item("SHIFT_ID"))
        Next
    End Sub

#End Region


#Region "Shift"

    Public Function Get_Kiosk_Current_Shift(Optional ByVal KO_ID As Integer = 0) As DataTable
        Dim SQL As String = ""
        SQL &= "   Select TOP 1 SHIFT_ID" & vbLf
        SQL &= "  ,KO_ID" & vbLf
        SQL &= "  ,SHIFT_Y,SHIFT_M,SHIFT_D,SHIFT_N,KO_CODE" & vbLf
        SQL &= "  ,dbo.FN_SHIFT_CODE(SHIFT_Y,SHIFT_M,SHIFT_D,ISNULL(KO_CODE,dbo.FN_KO_CODE(KO_ID)),SHIFT_N)    SHIFT_CODE" & vbLf
        SQL &= "  ,Open_Time,Open_By,USER_Open.EMPLOYEE_ID Open_EMP_ID , USER_Open.FIRST_NAME+' '+ USER_Open.LAST_NAME Open_By_Name" & vbLf
        SQL &= "  ,Close_Time,Close_By,USER_Close.EMPLOYEE_ID Close_EMP_ID,USER_Close.FIRST_NAME+' '+ USER_Close.LAST_NAME Close_By_Name" & vbLf
        SQL &= "  From TB_SHIFT" & vbLf
        SQL &= "  Left Join MS_USER USER_Open On USER_Open.USER_ID=TB_SHIFT.Open_By" & vbLf
        SQL &= "  Left Join MS_USER USER_Close ON USER_Close.USER_ID=TB_SHIFT.Close_By" & vbLf
        SQL &= "  Where KO_ID=" & KO_ID & vbLf
        SQL &= "  Order By SHIFT_ID DESC" & vbLf

        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DT.TableName = "Data"
        DA.Fill(DT)
        Return DT
    End Function

    Public Function GetKiosk_Current_QTY(ByRef KO_ID As Integer, ByRef D_ID As Integer, ByRef Unit_Value As Integer) As Integer
        Dim Current_QTY As Integer = 0
        Dim SQL As String = ""

        SQL &= "  --หา Stock ของ CoinIn CashIn para(KO_ID,D_ID,Unit_Value) " & vbLf

        SQL &= "  Select TOP 1 TB_TRANSACTION_STOCK.TXN_ID " & vbLf
        SQL &= "      ,TB_TRANSACTION_STOCK.D_ID " & vbLf
        SQL &= "      ,TB_TRANSACTION_STOCK.Unit_Value " & vbLf
        SQL &= "      ,TB_TRANSACTION_STOCK.BEFORE_QUANTITY " & vbLf
        SQL &= "      ,TB_TRANSACTION_STOCK.DIFF_QUANTITY " & vbLf
        SQL &= "    ,(TB_TRANSACTION_STOCK.BEFORE_QUANTITY)+ (TB_TRANSACTION_STOCK.DIFF_QUANTITY*MS_DEVICE_TYPE.Movement_Direction) CURRENT_QTY " & vbLf
        SQL &= "  From TB_TRANSACTION_STOCK " & vbLf
        SQL &= "  Left Join TB_SERVICE_TRANSACTION ON TB_SERVICE_TRANSACTION.TXN_ID=TB_TRANSACTION_STOCK.TXN_ID " & vbLf
        SQL &= "  Left Join MS_DEVICE ON MS_DEVICE.D_ID=TB_TRANSACTION_STOCK.D_ID " & vbLf
        SQL &= "  Left Join MS_DEVICE_TYPE ON MS_DEVICE_TYPE.DT_ID=MS_DEVICE.DT_ID  " & vbLf
        SQL &= "  WHERE TB_SERVICE_TRANSACTION.KO_ID =" & KO_ID & "   " & vbLf
        SQL &= "  And TB_TRANSACTION_STOCK.D_ID=" & D_ID & " " & vbLf
        SQL &= "  And TB_TRANSACTION_STOCK.Unit_Value=" & Unit_Value & "  " & vbLf
        SQL &= "  ORDER BY TB_TRANSACTION_STOCK.TXN_ID DESC " & vbLf
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            Current_QTY = Val(DT.Rows(0).Item("CURRENT_QTY").ToString.Replace(",", ""))
        End If
        Return Current_QTY
    End Function

    Public Function CheckDevice_Status(ByRef KO_ID As Integer, ByRef D_ID As Integer) As Integer
        Dim DS_ID As Integer = 1







        Return DS_ID
    End Function


#End Region

#Region "SetUnit_Value CoinIn CashIn"


    Public Function GetCoinIn_List() As DataTable
        Dim DT_CoinIn As New DataTable
        DT_CoinIn.Columns.Add("Unit_Value")
        DT_CoinIn.Columns.Add("Icon_Green")
        DT_CoinIn.Columns.Add("Active_Status", GetType(Boolean))

        DT_CoinIn.Rows.Add(1, "images/Icon/Green/coin1.png", 1)
        DT_CoinIn.Rows.Add(2, "images/Icon/Green/coin2.png", 1)
        DT_CoinIn.Rows.Add(5, "images/Icon/Green/coin5.png", 1)
        DT_CoinIn.Rows.Add(10, "images/Icon/Green/coin10.png", 1)

        DT_CoinIn.DefaultView.RowFilter = "Active_Status=1"
        Return DT_CoinIn
    End Function

    Public Function GetCashIn_List() As DataTable
        Dim DT_CashIn As New DataTable
        DT_CashIn.Columns.Add("Unit_Value")
        DT_CashIn.Columns.Add("Icon_Green")
        DT_CashIn.Columns.Add("Active_Status", GetType(Boolean))

        DT_CashIn.Rows.Add(20, "images/Icon/Green/cash20.png", 1)
        DT_CashIn.Rows.Add(50, "images/Icon/Green/cash50.png", 1)
        DT_CashIn.Rows.Add(100, "images/Icon/Green/cash100.png", 1)
        DT_CashIn.Rows.Add(500, "images/Icon/Green/cash500.png", 1)
        DT_CashIn.Rows.Add(1000, "images/Icon/Green/cash1000.png", 1)

        DT_CashIn.DefaultView.RowFilter = "Active_Status=1"
        Return DT_CashIn
    End Function

#End Region


#Region "Kiosk Product"

    Public Function GetList_Current_Brand_Kiosk(Optional ByVal KO_ID As Integer = 0) As DataTable
        Dim SQL As String = ""
        SQL &= "   Select DISTINCT MS_BRAND.BRAND_ID , MS_BRAND.BRAND_NAME" & vbLf
        SQL &= "   From VW_CURRENT_PRODUCT_STOCK" & vbLf
        SQL &= "   INNER Join MS_BRAND ON MS_BRAND.BRAND_ID=VW_CURRENT_PRODUCT_STOCK.BRAND_ID" & vbLf
        SQL &= "   WHERE KO_ID =" & KO_ID & " And MS_BRAND.Active_Status = 1" & vbLf
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

    Public Function GetList_Current_Category_Kiosk(Optional ByVal KO_ID As Integer = 0) As DataTable
        Dim SQL As String = ""
        SQL &= "   Select   DISTINCT MS_PRODUCT_CAT.CAT_ID  , MS_PRODUCT_CAT.CAT_NAME " & vbLf
        SQL &= "   From VW_CURRENT_PRODUCT_STOCK" & vbLf
        SQL &= "   INNER Join MS_PRODUCT_CAT ON MS_PRODUCT_CAT.CAT_ID =VW_CURRENT_PRODUCT_STOCK.CAT_ID" & vbLf
        SQL &= "   WHERE KO_ID = " & KO_ID & "" & vbLf
        SQL &= "   UNION ALL" & vbLf
        SQL &= "   -- กรณี Product ไม่ได้ Set Category" & vbLf
        SQL &= "   Select   DISTINCT 0 CAT_ID  , 'Other' CAT_NAME " & vbLf
        SQL &= "   From VW_CURRENT_PRODUCT_STOCK" & vbLf
        SQL &= "   Where KO_ID =" & KO_ID & " And VW_CURRENT_PRODUCT_STOCK.CAT_ID Is NULL" & vbLf


        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

    Public Function Get_Show_Default_Product(Optional ByVal MODEL As String = "") As DataTable
        Dim SQL As String = ""
        SQL &= "   Select * " & vbLf
        SQL &= "   From VW_CURRENT_PRODUCT_DETAIL" & vbLf
        If MODEL <> "" Then
            SQL &= "  WHERE MODEL='" & MODEL & "'" & vbLf
        End If
        SQL &= "  ORDER BY PRODUCT_ID" & vbLf
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

    Public Function GetList_Product_Model(ByVal MODEL As String, Optional ByVal KO_ID As Integer = 0, Optional ByVal PRODUCT_ID As Integer = 0) As DataTable
        Dim SQL As String = ""
        SQL &= "  Select * FROM VW_CURRENT_PRODUCT_DETAIL " & vbLf
        SQL &= "  WHERE MODEL='" & MODEL.ToString() & "' AND KO_ID=" & KO_ID
        If PRODUCT_ID > 0 Then
            SQL &= "  AND PRODUCT_ID=" & PRODUCT_ID
        End If
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

    Public Function GetList_Product_Spec_Capacity(ByVal MODEL As String, Optional ByVal KO_ID As Integer = 0, Optional ByVal CAT_ID As Integer = 0, Optional ByRef LANGUAGE As Integer = UILanguage.TH) As DataTable
        Dim SQL As String = " SELECT * FROM ("
        SQL &= "  SELECT DISTINCT SPEC_ID  "
        SQL &= "  , SPEC_NAME_" & Get_Language_Code(LANGUAGE) & " SPEC_NAME"
        SQL &= "  , dbo.FN_Clean_Line_ITem(DESCRIPTION_" & Get_Language_Code(LANGUAGE) & ") DESCRIPTION_CAPACITY"
        Select Case CAT_ID
            Case Category.Accessories
                SQL &= " , 'mbps' Unit "
            Case Else
                SQL &= " , 'GB' Unit "
        End Select

        SQL &= "  FROM VW_CURRENT_PRODUCT_SPEC " & vbLf
        SQL &= "  WHERE KO_ID=" & KO_ID
        SQL &= "        AND MODEL='" & MODEL & "'" & vbLf

        Select Case CAT_ID
            Case Category.Accessories
                SQL &= "        AND SPEC_ID IN (" & Spec.CapacityAccessories & ") "
            Case Else
                SQL &= "        AND SPEC_ID IN (" & Spec.Capacity & ") "
        End Select
        SQL &= "   )TB ORDER BY DESCRIPTION_CAPACITY desc "

        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

    Public Function GetList_Product_Spec_Warranty(ByVal PRODUCT_ID As Integer, Optional ByVal KO_ID As Integer = 0, Optional ByRef LANGUAGE As Integer = UILanguage.TH) As DataTable
        Dim SQL As String = ""
        SQL &= "  SELECT SEQ ,PRODUCT_ID ,SPEC_ID  "
        SQL &= "  , SPEC_NAME_" & Get_Language_Code(LANGUAGE) & " SPEC_NAME"
        SQL &= "  , dbo.FN_Clean_Line_ITem(DESCRIPTION_" & Get_Language_Code(LANGUAGE) & ") DESCRIPTION"

        SQL &= "  FROM VW_CURRENT_PRODUCT_SPEC " & vbLf
        SQL &= "  WHERE KO_ID=" & KO_ID
        SQL &= "        AND PRODUCT_ID=" & PRODUCT_ID
        SQL &= "        AND SPEC_ID IN (" & Spec.Warranty & ") "
        SQL &= " ORDER BY SEQ "

        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

    Public Function GetList_Product_Spec_Color(ByVal MODEL As String, Optional ByVal KO_ID As Integer = 0, Optional ByRef LANGUAGE As Integer = UILanguage.TH) As DataTable
        Dim SQL As String = ""
        SQL &= "  SELECT SEQ ,PRODUCT_ID ,SPEC_ID  "
        SQL &= "  , SPEC_NAME_" & Get_Language_Code(LANGUAGE) & " SPEC_NAME"
        SQL &= "  , dbo.FN_Clean_Line_ITem(DESCRIPTION_" & Get_Language_Code(LANGUAGE) & ") DESCRIPTION_COLOR"

        SQL &= "  FROM VW_CURRENT_PRODUCT_SPEC " & vbLf
        SQL &= "  WHERE KO_ID=" & KO_ID
        SQL &= "        AND MODEL='" & MODEL & "'" & vbLf
        SQL &= "        AND SPEC_ID IN (" & Spec.Color & ") "
        SQL &= " ORDER BY SEQ "

        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

    Public Function GetList_Product_Spec_Other(ByVal PRODUCT_ID As Integer, Optional ByVal KO_ID As Integer = 0, Optional ByRef LANGUAGE As Integer = UILanguage.TH) As DataTable
        Dim SQL As String = ""
        SQL &= "  SELECT SEQ  "
        SQL &= "  , SPEC_NAME_" & Get_Language_Code(LANGUAGE) & " SPEC_NAME "
        'SQL &= "  , dbo.FN_Clean_Line_ITem(DESCRIPTION_" & Get_Language_Code(LANGUAGE) & ") DESCRIPTION"
        SQL &= "  , DESCRIPTION_" & Get_Language_Code(LANGUAGE) & " DESCRIPTION"

        SQL &= "  FROM VW_CURRENT_PRODUCT_SPEC " & vbLf
        SQL &= "  WHERE KO_ID=" & KO_ID
        SQL &= "        AND PRODUCT_ID=" & PRODUCT_ID
        SQL &= "        AND SPEC_ID NOT IN (" & Spec.Capacity & "," & Spec.CapacityAccessories & "," & Spec.Warranty & ") "
        SQL &= " ORDER BY SEQ "
        '"," & Spec.Color &
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function


    Public Function GetProduct_ID_Select(ByVal MODEL As String, ByVal COLOR As String, ByVal CAPACITY As String, Optional ByVal KO_ID As Integer = 0, Optional ByRef LANGUAGE As Integer = UILanguage.TH) As Integer
        Dim PRODUCT_ID As Integer = 0
        Dim SQL As String = ""
        SQL &= "  SELECT * FROM ( " & vbLf
        SQL &= "    SELECT DISTINCT " & vbLf
        SQL &= "           PRODUCT_ID, PRODUCT_CODE, PRODUCT_NAME, KO_ID, MODEL,CAT_ID  " & vbLf
        'SQL &= "    	  ,(SELECT DESCRIPTION_" & Get_Language_Code(LANGUAGE) & " FROM VW_CURRENT_PRODUCT_SPEC VW  WHERE SPEC_ID=" & Spec.Color & " And VW.PRODUCT_ID=VW_CURRENT_PRODUCT_SPEC.PRODUCT_ID) COLOR " & vbLf
        SQL &= "    	  ,(SELECT DESCRIPTION_TH FROM VW_CURRENT_PRODUCT_SPEC VW  WHERE SPEC_ID=" & Spec.Color & " And VW.PRODUCT_ID=VW_CURRENT_PRODUCT_SPEC.PRODUCT_ID) COLOR " & vbLf        '--ค้นหาสีใช้ชื่อ Field ภาษาไทยในการค้นหาเพื่อแก้ปัญหา where ด้วยภาษาอื่นๆแล้ว ไม่เจอ  TH บังคับกรอก

        SQL &= "    	  ,(SELECT DESCRIPTION_" & Get_Language_Code(LANGUAGE) & " FROM VW_CURRENT_PRODUCT_SPEC VW  WHERE SPEC_ID=" & Spec.Capacity & " And VW.PRODUCT_ID=VW_CURRENT_PRODUCT_SPEC.PRODUCT_ID) CAPACITY_Mobile " & vbLf
        SQL &= "    	  ,(SELECT DESCRIPTION_" & Get_Language_Code(LANGUAGE) & " FROM VW_CURRENT_PRODUCT_SPEC VW  WHERE SPEC_ID=" & Spec.CapacityAccessories & " And VW.PRODUCT_ID=VW_CURRENT_PRODUCT_SPEC.PRODUCT_ID) CAPACITY_Assessory " & vbLf
        SQL &= "      From VW_CURRENT_PRODUCT_SPEC " & vbLf
        SQL &= "      Where SPEC_ID In (" & Spec.Color & "," & Spec.Capacity & "," & Spec.CapacityAccessories & ")  AND KO_ID=" & KO_ID
        SQL &= "      Group BY   PRODUCT_ID, PRODUCT_CODE, PRODUCT_NAME, KO_ID,CAT_ID , MODEL, DESCRIPTION_" & Get_Language_Code(LANGUAGE) & " " & vbLf
        SQL &= "  ) TB  " & vbLf
        SQL &= "  WHERE MODEL='" & MODEL & "' "
        If COLOR <> "" Then
            SQL &= "   AND COLOR='" & COLOR & "' " & vbLf
        Else
            SQL &= "   AND COLOR IS NULL " & vbLf
        End If
        If CAPACITY <> "" Then
            SQL &= "   And ( CAPACITY_Mobile='" & CAPACITY & "' Or CAPACITY_Assessory='" & CAPACITY & "')" & vbLf
        Else
            SQL &= "   AND ( CAPACITY_Mobile Is NULL Or CAPACITY_Assessory Is NULL)" & vbLf
        End If

        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            PRODUCT_ID = DT.Rows(0).Item("PRODUCT_ID")
        End If
        Return PRODUCT_ID
    End Function

    Public Function GetProduct_Choice(ByVal MODEL As String, Optional ByVal COLOR As String = "", Optional ByVal CAPACITY As String = "", Optional ByVal CAT_ID As Integer = 0, Optional ByVal KO_ID As Integer = 0, Optional ByRef LANGUAGE As Integer = UILanguage.TH) As DataTable
        Dim SQL As String = ""
        SQL &= "  SELECT *   FROM ( " & vbLf
        SQL &= "    SELECT DISTINCT " & vbLf
        SQL &= "           PRODUCT_ID, PRODUCT_CODE, PRODUCT_NAME, KO_ID, MODEL,CAT_ID  " & vbLf
        SQL &= "    	  ,(SELECT DESCRIPTION_" & Get_Language_Code(LANGUAGE) & " FROM VW_CURRENT_PRODUCT_SPEC VW  WHERE SPEC_ID=" & Spec.Color & " And VW.PRODUCT_ID=VW_CURRENT_PRODUCT_SPEC.PRODUCT_ID) DESCRIPTION_COLOR " & vbLf
        SQL &= "    	  ,(SELECT DESCRIPTION_TH COLOR_TH FROM VW_CURRENT_PRODUCT_SPEC VW  WHERE SPEC_ID=" & Spec.Color & " And VW.PRODUCT_ID=VW_CURRENT_PRODUCT_SPEC.PRODUCT_ID) COLOR_TH " & vbLf

        If CAT_ID = Category.Accessories Then
            SQL &= "    	  ,(SELECT DESCRIPTION_" & Get_Language_Code(LANGUAGE) & " FROM VW_CURRENT_PRODUCT_SPEC VW  WHERE SPEC_ID=" & Spec.CapacityAccessories & " And VW.PRODUCT_ID=VW_CURRENT_PRODUCT_SPEC.PRODUCT_ID) DESCRIPTION_CAPACITY " & vbLf
            SQL &= " , 'mbps' Unit "
        Else
            SQL &= "    	  ,(SELECT DESCRIPTION_" & Get_Language_Code(LANGUAGE) & " FROM VW_CURRENT_PRODUCT_SPEC VW  WHERE SPEC_ID=" & Spec.Capacity & " And VW.PRODUCT_ID=VW_CURRENT_PRODUCT_SPEC.PRODUCT_ID) DESCRIPTION_CAPACITY " & vbLf
            SQL &= " , 'GB' Unit "
        End If


        SQL &= "      From VW_CURRENT_PRODUCT_SPEC " & vbLf
        SQL &= "      Where SPEC_ID In (" & Spec.Color & "," & Spec.Capacity & "," & Spec.CapacityAccessories & ")  AND KO_ID=" & KO_ID
        SQL &= "      Group BY   PRODUCT_ID, PRODUCT_CODE, PRODUCT_NAME, KO_ID, MODEL,CAT_ID , DESCRIPTION_" & Get_Language_Code(LANGUAGE) & " " & vbLf
        SQL &= "  ) TB  " & vbLf
        SQL &= "  WHERE MODEL='" & MODEL & "' "



        If COLOR <> "" Then
            SQL &= "   AND COLOR_TH='" & COLOR & "' " & vbLf
        End If

        If CAPACITY <> "" Then
            SQL &= "   And ( DESCRIPTION_CAPACITY='" & CAPACITY & "')" & vbLf
        End If

        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        Return DT
    End Function

#End Region

#Region "Kiosk SIM"

    Public Function GetList_Current_SIM_Kiosk(Optional ByVal KO_ID As Integer = 0, Optional ByVal SIM_ID As Integer = 0) As DataTable
        Dim SQL As String = ""
        SQL &= "   Select * FROM VW_CURRENT_SIM_DETAIL " & vbLf
        SQL &= "   WHERE KO_ID =" & KO_ID & " And Active_Status = 1" & vbLf
        If SIM_ID > 0 Then
            SQL &= " And SIM_ID = " & SIM_ID & "" & vbLf
        End If
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

#End Region

#Region "transaction Log Kiosk"
    'TB_SERVICE_TRANSACTION

    '--TXN Create
    'Public Function Save_TXN_Log(ByVal TXN_ID As Integer, ByVal KO_ID As Integer, ByVal LANG_CODE As Integer, Optional ByVal CUSTOMER_CARD As String = "", Optional ByVal CUS_IMAGE As Image = Nothing)
    '    Dim SQL As String = "SELECT * FROM TB_SERVICE_TRANSACTION WHERE TXN_ID=" & TXN_ID
    '    Dim DA As New SqlDataAdapter(SQL, ConnectionString)
    '    Dim DT As New DataTable
    '    DA.Fill(DT)
    '    Dim DR As DataRow

    '    If DT.Rows.Count = 0 Then

    '        TXN_ID = Get_NewID("TB_SERVICE_TRANSACTION", "TXN_ID")
    '        DR = DT.NewRow
    '        DR("TXN_ID") = TXN_ID
    '        DR("LANG_CODE") = LANG_CODE
    '        DR("TXN_START") = Now
    '        DR("KO_ID") = KO_ID
    '        DR("TXN_Y") = Now.Year
    '        DR("TXN_M") = Now.Month
    '        DR("TXN_D") = Now.Day
    '        DR("TXN_N") = Get_TXN_N(TXN_ID)
    '    Else
    '        DR = DT.Rows(0)
    '        DR("TXN_END") = Now

    '        If CUSTOMER_CARD <> "" Then
    '            Dim CUS_ID As Integer
    '            Dim DT_Customer As DataTable = GET_CUSTOMER(CUSTOMER_CARD, Nothing)
    '            If DT_Customer.Rows.Count > 0 Then
    '                CUS_ID = DT.Rows(0).Item("CUS_ID")
    '            End If

    '        End If

    '    End If

    '    DR("CUS_ID") = ""
    '    DR("CUS_IMAGE") = ""



    '    DR("SHIFT_ID") = ""
    '    DR("SHIFT_CODE") = ""
    '    'DR("SLIP_YEAR") = ""
    '    'DR("SLIP_MONTH") = ""
    '    'DR("SLIP_DAY") = ""
    '    'DR("SLIP_NO") = ""
    '    'DR("SLIP_CONTENT") = ""
    '    'DR("METHOD_ID") = ""
    '    DR("TMN_REQ_ID") = ""
    '    'End If

    '    Return 0
    'End Function

    'GET_CUS_Info
    Public Function GET_CUSTOMER(ByVal CUSTOMER_CARD As String, Optional ByVal CUS_ID As Integer = 0) As DataTable
        Dim SQL As String = "SELECT * FROM TB_CUSTOMER WHERE CUS_PID=" & CUSTOMER_CARD & " OR CUS_PASSPORT_ID=" & CUSTOMER_CARD
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

    Public Function Save_CUSTOMER()

        Return True
    End Function


    'Update_Print_Slip_Log
    'DR("SLIP_YEAR") = ""
    '   DR("SLIP_MONTH") = ""
    '   DR("SLIP_DAY") = ""
    '   DR("SLIP_NO") = ""
    '   DR("SLIP_CONTENT") = ""
    '   DR("METHOD_ID") = ""



    'Private Function Get_TXN_N(ByRef TXN_ID As String) As Integer
    '    Dim SQL As String = "SELECT IsNull(MAX(TXN_N),0)+1 FROM TB_SERVICE_TRANSACTION WHERE TXN_ID=" & TXN_ID
    '    Dim DA As New SqlDataAdapter(SQL, ConnectionString)
    '    Dim DT As New DataTable
    '    DA.Fill(DT)
    '    Return DT.Rows(0).Item(0)
    'End Function
#End Region

#Region "Service_Transaction"

    Public Sub Update_Service_Transaction(ByVal TXN_ID As Integer, ByRef Page As Page)
        Dim SQL As String = "UPDATE TB_SERVICE_TRANSACTION SET TXN_END=GETDATE() WHERE TXN_ID=" & TXN_ID
        ExecuteNonQuery(SQL)
    End Sub

    Public Function Gen_New_Service_Transaction(ByVal KO_ID As Integer, ByVal LANG As UILanguage) As Integer
        Dim SQL As String = "SELECT ISNULL(MAX(TXN_ID),0)+1 FROM TB_SERVICE_TRANSACTION"
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Dim TXN_ID As Integer = DT.Rows(0)(0)
        Dim TXN_Y As String = Now.Year.ToString.Substring(2, 2)
        Dim TXN_M As String = Now.Month.ToString.PadLeft(2, "0")
        Dim TXN_D As String = Now.Day.ToString.PadLeft(2, "0")

        SQL = "SELECT SITE_CODE" & vbLf
        SQL &= " FROM MS_KIOSK KO INNER JOIN MS_SITE ST On KO.SITE_ID=ST.SITE_ID" & vbLf
        SQL &= " WHERE KO.KO_ID=" & KO_ID
        DA = New SqlDataAdapter(SQL, ConnectionString)
        DT = New DataTable
        DA.Fill(DT)
        Dim SITE_CODE As String = DT.Rows(0)(0).ToString

        SQL = "SELECT ISNULL(MAX(CAST(TXN_N AS INT)),0)+1 FROM TB_SERVICE_TRANSACTION " & vbLf
        SQL &= "WHERE TXN_Y='" & TXN_Y & "' AND TXN_M='" & TXN_M & "' AND TXN_D='" & TXN_D & "'"
        DA = New SqlDataAdapter(SQL, ConnectionString)
        DT = New DataTable
        DA.Fill(DT)
        Dim TXN_N As String = DT.Rows(0)(0).ToString.PadLeft(3, "0")
        Dim LANG_CODE As String = Get_Language_Code(LANG)

        SQL = "EXEC dbo.SP_CURRENT_OPEN_SHIFT " & KO_ID
        DA = New SqlDataAdapter(SQL, ConnectionString)
        DT = New DataTable
        DA.Fill(DT)
        If DT.Rows.Count = 0 Then Return 0 '----------- Shift Current Closed ------------
        Dim SHIFT_ID As Integer = DT.Rows(0).Item("SHIFT_ID")
        Dim SHIFT_CODE As String = DT.Rows(0).Item("SHIFT_CODE")


        SQL = "SELECT TOP 0 * FROM TB_SERVICE_TRANSACTION"
        DA = New SqlDataAdapter(SQL, ConnectionString)
        DT = New DataTable
        DA.Fill(DT)
        Dim DR As DataRow = DT.NewRow
        DT.Rows.Add(DR)
        DR("TXN_ID") = TXN_ID
        DR("KO_ID") = KO_ID
        DR("SITE_CODE") = SITE_CODE
        DR("TXN_Y") = TXN_Y
        DR("TXN_M") = TXN_M
        DR("TXN_D") = TXN_D
        DR("TXN_N") = TXN_N
        DR("LANG_CODE") = LANG_CODE
        DR("CUS_ID") = DBNull.Value
        DR("CUS_IMAGE") = DBNull.Value
        DR("TXN_START") = Now
        DR("TXN_END") = Now
        DR("SHIFT_ID") = SHIFT_ID
        DR("SHIFT_CODE") = SHIFT_CODE
        DR("METHOD_ID") = DBNull.Value


        Dim cmd As New SqlCommandBuilder(DA)
        DA.Update(DT)

        Return TXN_ID
    End Function

    Public Function Get_Next_Product_To_Pick(ByVal KO_ID As Integer, ByVal PRODUCT_ID As Integer) As DataTable

        Dim Result As New DataTable
        Result.Columns.Add("POS_ID", GetType(Integer))
        Result.Columns.Add("SLOT_ID", GetType(Integer))
        Result.Columns.Add("SLOT_NAME", GetType(String))
        Result.Columns.Add("SERIAL_NO", GetType(String))

        Dim Sql As String = "" '-------------- คำนวนจำนวน ทั้งหมดในตู้
        Sql &= " SELECT POS_ID,PRODUCT.SLOT_ID,PRODUCT.SLOT_NAME,TOTAL" & vbLf
        Sql &= " FROM" & vbLf
        Sql &= " (SELECT SLOT_ID,SLOT_NAME ,COUNT(1) TOTAL" & vbLf
        Sql &= " FROM VW_CURRENT_PRODUCT_STOCK" & vbLf
        Sql &= " WHERE KO_ID=" & KO_ID & " AND PRODUCT_ID=" & PRODUCT_ID & vbLf
        Sql &= " GROUP BY SLOT_ID,SLOT_NAME) PRODUCT" & vbLf
        Sql &= " LEFT JOIN MS_SLOT_ARM_POSITION ARM ON PRODUCT.SLOT_NAME=ARM.SLOT_NAME" & vbLf
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(Sql, ConnectionString)
        DA.Fill(DT)

        If DT.Rows.Count = 0 Then
            Return Result
        End If
        '---------------- Random Selction -------------
        DT.Columns.Add("RND")
        For i As Integer = 0 To DT.Rows.Count - 1
            DT.Rows(i).Item("RND") = GenerateNewUniqueID()
        Next
        DT.DefaultView.Sort = "TOTAL DESC,RND ASC"

        Dim R As DataRow = Result.NewRow
        Result.Rows.Add(R)

        R("SLOT_ID") = DT.DefaultView(0).Item("SLOT_ID")
        R("SLOT_NAME") = DT.DefaultView(0).Item("SLOT_NAME")
        R("POS_ID") = DT.DefaultView(0).Item("POS_ID")

        '--------------- Get SERIAL NO by Order------------
        Sql = " SELECT TOP 1 SERIAL_NO FROM VW_CURRENT_PRODUCT_STOCK " & vbLf
        Sql &= " WHERE KO_ID=" & KO_ID & " AND PRODUCT_ID=" & PRODUCT_ID & " AND SLOT_NAME='" & R("SLOT_NAME").Replace("'", "''") & "' " & vbLf
        Sql &= " ORDER BY ORDER_NO ASC" & vbLf
        DA = New SqlDataAdapter(Sql, ConnectionString)
        DT = New DataTable
        DA.Fill(DT)
        R("SERIAL_NO") = DT.Rows(0).Item("SERIAL_NO") '--------- For Picker -----------

        Return Result
    End Function

    Public Function Get_Next_SIM_To_Pick(ByVal KO_ID As Integer, ByVal SIM_ID As Integer) As DataTable

        Dim SQL As String = " SELECT CASE WHEN SLOT_NAME LIKE '%1' THEN 1 " & vbLf
        SQL &= " When SLOT_NAME Like '%2' THEN 2 " & vbLf
        SQL &= " WHEN SLOT_NAME LIKE '%3' THEN 3 " & vbLf
        SQL &= " WHEN SLOT_NAME Like '%4' THEN 4 " & vbLf
        SQL &= " WHEN SLOT_NAME LIKE '%5' THEN 5 " & vbLf
        SQL &= " End SLOT_ID, " & vbLf
        SQL &= " SLOT_NAME,COUNT(SERIAL_NO) TOTAL " & vbLf
        SQL &= " FROM VW_CURRENT_SIM_STOCK " & vbLf
        SQL &= " WHERE KO_ID=" & KO_ID & " AND SIM_ID=" & SIM_ID & vbLf
        SQL &= " GROUP BY SLOT_NAME"

        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)

        DT.Columns.Add("RND")
        For i As Integer = 0 To DT.Rows.Count - 1
            DT.Rows(i).Item("RND") = GenerateNewUniqueID()
        Next
        DT.DefaultView.Sort = "TOTAL DESC,RND ASC"
        DT = DT.DefaultView.ToTable

        Return DT
    End Function

    Public Function Calculate_Change_Quantity(ByVal REMAIN As Integer, ByVal MoneyStock As DataTable) As DataTable

        Dim RESULT As New DataTable
        RESULT.Columns.Add("Unit_Value", GetType(Integer))
        RESULT.Columns.Add("Current_Qty", GetType(Integer))

        While REMAIN > 0 And MoneyStock.Rows.Count > 0
            '---------- Get Max Value Can Pay
            Dim NextToPay As Object = MoneyStock.Compute("MAX(Unit_Value)", "Unit_Value<=" & REMAIN) '
            If IsDBNull(NextToPay) Then
                Exit While '---------Nothing Can Pay Anymore
            End If
            RESULT.Rows.Add(NextToPay, 1)
            REMAIN -= NextToPay
            MoneyStock.DefaultView.RowFilter = "Unit_Value=" & NextToPay
            MoneyStock.DefaultView(0).Item("Current_Qty") -= 1
            If MoneyStock.DefaultView(0).Item("Current_Qty") = 0 Then
                MoneyStock.DefaultView(0).Row.Delete()
            End If
        End While

        Return RESULT
    End Function

#End Region

#Region "Printing"

    Dim PrintLine As String = "_________________________________"

    Public Function Get_New_Confirmation_Slip_No() As String
        Dim SQL As String = "SELECT TOP 1 SLIP_NO FROM"
        SQL &= "(" & vbLf
        SQL &= "	SELECT ISNULL(MAX(CAST(SLIP_NO AS INT)),0)+1 SLIP_NO" & vbLf
        SQL &= "	FROM TB_TRANSACTION_CASH" & vbLf
        SQL &= "	WHERE SLIP_YEAR=RIGHT(DATEPART(yyyy,GETDATE()),2) AND " & vbLf
        SQL &= "	CAST(SLIP_MONTH AS INT)=DATEPART(MM,GETDATE()) AND" & vbLf
        SQL &= "	CAST(SLIP_DAY AS INT)=DATEPART(dd,GETDATE())" & vbLf
        SQL &= "" & vbLf
        SQL &= "	UNION ALL " & vbLf
        SQL &= "" & vbLf
        SQL &= "	SELECT ISNULL(MAX(CAST(SLIP_NO AS INT)),0)+1 SLIP_NO" & vbLf
        SQL &= "	FROM TB_TRANSACTION_TMN" & vbLf
        SQL &= "	WHERE SLIP_YEAR=RIGHT(DATEPART(yyyy,GETDATE()),2) AND " & vbLf
        SQL &= "	CAST(SLIP_MONTH AS INT)=DATEPART(MM,GETDATE()) AND" & vbLf
        SQL &= "	CAST(SLIP_DAY AS INT)=DATEPART(dd,GETDATE())" & vbLf
        SQL &= ") A ORDER BY SLIP_NO DESC" & vbLf
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT.Rows(0).Item(0).ToString.PadLeft(3, "0") '-------------ณ วันนี้จองไว้ 3 แต่ DB เผื่อไว้ 4
    End Function

    Public Function GEN_DEFAULT_SLIP_HEADER() As String
        Dim Content As String = "   บริษัท ทรู ดิสทริบิวชั่น แอนด์ เซลส์ จำกัด" & vbLf
        Content &= "18 อาคารทรูทาวเวอร์ ถ.รัชดาภิเษก แขวงห้วยขวาง" & vbLf
        Content &= "    เขตห้วยขวาง กรุงเทพมหานคร 10310" & vbLf
        Content &= vbLf & vbLf
        Return Content
    End Function


    '------------ ถ้ามี Ads เพิ่ม Ads ตรงนี้---------
    Public Function GEN_SLIP_ADS() As String
        Dim Content As String = ""

        Return Content
    End Function

    Public Function GEN_CASH_CONFIRMATION_SLIP(ByVal TXN_ID As Integer) As String

        Dim Content As String = GEN_DEFAULT_SLIP_HEADER()

        Dim SQL As String = "SELECT * FROM VW_TXN_CASH" & vbLf
        SQL &= "WHERE TXN_ID=" & TXN_ID & " AND TXN_STEP='completed'"
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)
        If DT.Rows.Count = 0 Then
            Return Content
        End If

        Dim SITE_CODE As String = DT.Rows(0).Item("SITE_CODE").ToString
        Dim SITE_NAME As String = ""
        If Not IsDBNull(DT.Rows(0).Item("SITE_NAME")) Then
            SITE_NAME = "(" & DT.Rows(0).Item("SITE_NAME") & ")"
        End If
        Dim SLIP_CODE As String = DT.Rows(0).Item("SLIP_CODE").ToString
        Dim TXN_END As DateTime = DT.Rows(0).Item("TXN_END")
        Dim PRODUCT_CODE As String = DT.Rows(0).Item("PRODUCT_CODE")
        Dim TOTAL_PRICE As String = FormatNumber(DT.Rows(0).Item("TOTAL_PRICE"))
        Dim PRODUCT_NAME As String = DT.Rows(0).Item("PRODUCT_NAME")
        Dim SERIAL_NO As String = DT.Rows(0).Item("SERIAL_NO")
        Dim PAID As String = DT.Rows(0).Item("PAID")
        Dim ACTUAL_CHANGE As Double = DT.Rows(0).Item("ACTUAL_CHANGE")
        Dim REMAIN_CHANGE As Double = DT.Rows(0).Item("REMAIN_CHANGE")


        Content &= "สาขาที่ : " & SITE_CODE & SITE_NAME & vbLf
        Content &= "ใบยืนยันการรับชำระ                 " & TXN_END.ToString("dd/MM/yyyy") & vbLf
        Content &= "" & SLIP_CODE & " 	    " & TXN_END.ToString("hh:mm") & vbLf
        Content &= PrintLine & vbLf
        Content &= "รายการสินค้า" & vbLf
        Content &= " " & vbLf
        Content &= "1. " & PRODUCT_CODE & "                 " & FormatNumber(TOTAL_PRICE, 2) & vbLf
        Content &= PRODUCT_NAME & vbLf
        If Trim(SERIAL_NO) <> "" Then
            Content &= "S/N : " & SERIAL_NO & vbLf
            Content &= " " & vbLf
        End If
        Content &= PrintLine & vbLf
        Content &= "CASH                                " & FormatNumber(PAID, 2) & vbLf

        If ACTUAL_CHANGE > 0 Then
            Content &= PrintLine & vbLf
            Content &= "CHANGE                              " & FormatNumber(ACTUAL_CHANGE, 2) & vbLf
        End If
        If REMAIN_CHANGE > 0 Then
            Content &= PrintLine & vbLf
            Content &= "REMAIN CHANGE                           " & FormatNumber(REMAIN_CHANGE, 2) & vbLf
        End If
        Content &= PrintLine & vbLf
        Content &= " " & vbLf
        Content &= "ขอบคุณที่ใช้บริการ" & vbLf
        '----------------- Ads ----------------
        Content &= GEN_SLIP_ADS()
        '----------------- Ads ----------------
        Content &= PrintLine

        '------------ Set Default Parameter
        'Set_Default_Print_Content_Style(Content)

        Return Content
    End Function

    Public Function GEN_TMN_CONFIRMATION_SLIP(ByVal TXN_ID As Integer) As String
        Dim Content As String = GEN_DEFAULT_SLIP_HEADER()

        Dim SQL As String = "SELECT * FROM VW_TXN_TMN" & vbLf
        SQL &= "WHERE TXN_ID=" & TXN_ID & " AND TXN_STEP='completed'"
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)
        If DT.Rows.Count = 0 Then
            Return Content
        End If

        Dim SITE_CODE As String = DT.Rows(0).Item("SITE_CODE").ToString
        Dim SITE_NAME As String = ""
        If Not IsDBNull(DT.Rows(0).Item("SITE_NAME")) Then
            SITE_NAME = "(" & DT.Rows(0).Item("SITE_NAME") & ")"
        End If
        Dim SLIP_CODE As String = DT.Rows(0).Item("SLIP_CODE").ToString
        Dim TXN_END As DateTime = DT.Rows(0).Item("TXN_END")
        Dim PRODUCT_CODE As String = DT.Rows(0).Item("PRODUCT_CODE")
        Dim TOTAL_PRICE As String = FormatNumber(DT.Rows(0).Item("TOTAL_PRICE"))
        Dim PRODUCT_NAME As String = DT.Rows(0).Item("PRODUCT_NAME")
        Dim SERIAL_NO As String = DT.Rows(0).Item("SERIAL_NO")
        Dim TMN_ISV As String = DT.Rows(0).Item("TMN_ISV").ToString
        Dim TMN_REQUEST_AMOUNT As Double = DT.Rows(0).Item("TMN_REQUEST_AMOUNT")
        Dim TMN_PAYMENT_CODE As String = DT.Rows(0).Item("TMN_PAYMENT_CODE")
        Dim TMN_PAYMENT_ID As String = DT.Rows(0).Item("TMN_PAYMENT_ID").ToString
        Dim TMN_RESP_TIME As DateTime = DT.Rows(0).Item("TMN_RESP_TIME").ToString

        Content &= "สาขาที่ : " & SITE_CODE & SITE_NAME & vbLf
        Content &= "ใบยืนยันการรับชำระ   	        " & TXN_END.ToString("dd/MM/yyyy") & vbLf
        Content &= "" & SLIP_CODE & " 	    " & TXN_END.ToString("hh:mm") & vbLf
        Content &= PrintLine & vbLf
        Content &= "รายการสินค้า" & vbLf
        Content &= " " & vbLf
        Content &= "1. " & PRODUCT_CODE & "                         " & FormatNumber(TOTAL_PRICE, 2) & vbLf
        Content &= PRODUCT_NAME & vbLf
        If Trim(SERIAL_NO) <> "" Then
            Content &= "S/N : " & SERIAL_NO & vbLf
            Content &= " " & vbLf
        End If
        Content &= PrintLine & vbLf
        Content &= "TRUE MONEY                            " & FormatNumber(TMN_REQUEST_AMOUNT / 100, 2) & vbLf
        Content &= " " & vbLf
        Content &= "ISV :   " & TMN_ISV & vbLf
        Content &= "PAYMENT ID :    " & TMN_PAYMENT_ID & vbLf
        Content &= "PAYMENT CODE :" & TMN_PAYMENT_CODE & vbLf
        Content &= PrintLine & vbLf
        Content &= " " & vbLf
        Content &= "ขอบคุณที่ใช้บริการ" & vbLf
        '----------------- Ads ----------------
        Content &= GEN_SLIP_ADS()
        '----------------- Ads ----------------
        Content &= PrintLine

        ''------------ Set Default Parameter
        'Set_Default_Print_Content_Style(Content)
        Return Content
    End Function

    Public Function GEN_CREDITCARD_CONFIRMATION_SLIP(ByVal TXN_ID As Integer) As String
        Dim Content As String = GEN_DEFAULT_SLIP_HEADER()


        '------------ Set Default Parameter
        'Set_Default_Print_Content_Style(Content)
        Return Content
    End Function

    'Public Sub Set_Default_Print_Content_Style(ByRef DT As DataTable)
    '    For i As Integer = 0 To DT.Rows.Count - 1
    '        Dim DR As DataRow = DT.Rows(i)
    '        If IsDBNull(DR("Text")) Then
    '            DR("Text") = ""
    '        End If
    '        If IsDBNull(DR("ImagePath")) Then
    '            DR("ImagePath") = ""
    '        End If
    '        If IsDBNull(DR("FontSize")) Then
    '            DR("FontSize") = 10
    '        End If
    '        If IsDBNull(DR("FontName")) Then
    '            DR("FontName") = "FontA1x1"
    '        End If
    '        If IsDBNull(DR("Bold")) Then
    '            DR("Bold") = False
    '        End If
    '        If IsDBNull(DR("IsColor")) Then
    '            DR("IsColor") = False
    '        End If
    '        If IsDBNull(DR("ContentType")) Then
    '            DR("ContentType") = VDM_BL.PrintContentType.Text
    '        End If
    '    Next
    'End Sub

    Public Sub UPDATE_CONFIRMATION_SLIP(ByVal TXN_ID As Integer, ByVal Content As String)

        Dim SQL As String = "SELECT METHOD_ID FROM TB_SERVICE_TRANSACTION WHERE TXN_ID=" & TXN_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)
        If DT.Rows.Count = 0 OrElse IsDBNull(DT.Rows(0).Item("METHOD_ID")) Then
            Exit Sub
        End If
        Dim C As New Converter
        Dim METHOD_ID As PaymentMethod = DT.Rows(0).Item("METHOD_ID")

        Select Case METHOD_ID
            Case PaymentMethod.CASH
                SQL = "SELECT * FROM TB_TRANSACTION_CASH WHERE TXN_ID=" & TXN_ID
                DT = New DataTable
                DA = New SqlDataAdapter(SQL, ConnectionString)
                DA.Fill(DT)
                If DT.Rows.Count > 0 Then
                    DT.Rows(0).Item("SLIP_CONTENT") = Content
                    Dim cmd As New SqlCommandBuilder(DA)
                    DA.Update(DT)
                End If
            Case PaymentMethod.TRUE_MONEY
                SQL = "SELECT * FROM TB_TRANSACTION_TMN WHERE TXN_ID=" & TXN_ID
                DT = New DataTable
                DA = New SqlDataAdapter(SQL, ConnectionString)
                DA.Fill(DT)
                If DT.Rows.Count > 0 Then
                    DT.Rows(0).Item("SLIP_CONTENT") = Content
                    Dim cmd As New SqlCommandBuilder(DA)
                    DA.Update(DT)
                End If
        End Select

    End Sub

    Public Sub UPDATE_KIOSK_DEVICE_TRANSACTION_STOCK(ByVal KO_ID As Integer, ByVal TXN_ID As Integer, ByVal D_ID As VDM_BL.Device, ByVal DIFF As Integer)
        Dim Sql As String = "SELECT * FROM TB_KIOSK_DEVICE WHERE KO_ID=" & KO_ID & " AND D_ID=" & D_ID
        Dim DA As New SqlDataAdapter(Sql, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Dim DR As DataRow = Nothing
        Dim cmd As SqlCommandBuilder
        Dim BEFORE_QUANTITY As Integer = 0
        Dim BEFORE_TXN As Object = DBNull.Value

        If DT.Rows.Count = 0 Then
            '------------- Insert New Record -----------
            Sql = "SELECT * FROM TB_KIOSK_DEVICE WHERE KO_ID=" & KO_ID & " AND D_ID=" & D_ID
            DA = New SqlDataAdapter(Sql, ConnectionString)
            DT = New DataTable
            DA.Fill(DT)
            DR = DT.NewRow
            DR("KO_ID") = KO_ID
            DR("D_ID") = D_ID
            DR("Current_Qty") = IIf(DIFF > 0, DIFF, 0) '---------------- Update Current -------------
            DR("DT_ID") = D_ID
            DR("DS_ID") = DBNull.Value
            DR("Update_Time") = Now
            DT.Rows.Add(DR)
            cmd = New SqlCommandBuilder(DA)
            DA.Update(DT)
            BEFORE_QUANTITY = 0
        Else
            If IsDBNull(DT.Rows(0).Item("Current_Qty")) Then DT.Rows(0).Item("Current_Qty") = 0
            BEFORE_QUANTITY = DT.Rows(0).Item("Current_Qty")
            '---------------- Update Current -------------
            Dim NEW_QUANTITY As Integer = BEFORE_QUANTITY + DIFF
            DT.Rows(0).Item("Current_Qty") = IIf(NEW_QUANTITY > 0, NEW_QUANTITY, 0)
            cmd = New SqlCommandBuilder(DA)
            DA.Update(DT)
        End If

        'TB_TRANSACTION_STOCK
        Sql = "SELECT MAX(STOCK.TXN_ID) TXN_ID" & vbLf
        Sql &= " FROM TB_SERVICE_TRANSACTION TXN" & vbLf
        Sql &= " INNER JOIN TB_TRANSACTION_STOCK STOCK ON TXN.TXN_ID=STOCK.TXN_ID" & vbLf
        Sql &= " WHERE KO_ID=" & KO_ID & " AND D_ID=" & D_ID & " AND STOCK.TXN_ID<" & TXN_ID & vbLf
        DA = New SqlDataAdapter(Sql, ConnectionString)
        DT = New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 AndAlso Not IsDBNull(DT.Rows(0).Item("TXN_ID")) Then
            BEFORE_TXN = DT.Rows(0).Item("TXN_ID")
        End If

        Dim Unit_Value As Integer = 0
        Select Case D_ID
            Case Device.Coin1
                Unit_Value = 1
            Case Device.Coin5
                Unit_Value = 5
            Case Device.Coin10
                Unit_Value = 10
            Case Device.Cash20
                Unit_Value = 20
            Case Device.Cash50
                Unit_Value = 50
            Case Device.Cash100
                Unit_Value = 100
            Case Device.Cash500
                Unit_Value = 500
            Case Device.Cash1000
                Unit_Value = 1000
            Case Device.Printer
                Unit_Value = 0
        End Select

        Sql = "SELECT TOP 1 * FROM TB_TRANSACTION_STOCK WHERE TXN_ID=" & TXN_ID & " AND D_ID=" & D_ID & " AND Unit_Value=" & Unit_Value
        DA = New SqlDataAdapter(Sql, ConnectionString)
        DT = New DataTable
        DA.Fill(DT)
        If DT.Rows.Count = 0 Then
            DR = DT.NewRow
            DR("TXN_ID") = TXN_ID
            DR("D_ID") = D_ID
            DR("Unit_Value") = Unit_Value
            DT.Rows.Add(DR)
        Else
            DR = DT.Rows(0)
        End If
        DR = DT.NewRow
        DR("BEFORE_QUANTITY") = BEFORE_QUANTITY
        DR("DIFF_QUANTITY") = DIFF
        DR("BEFORE_TXN_ID") = BEFORE_TXN
        Try
            cmd = New SqlCommandBuilder(DA)
            DA.Update(DT)
        Catch : End Try

    End Sub

#End Region

    Public Function GET_TXN_PREPAID_REGISTER(ByVal TXN_ID As Integer)
        Dim SQL As String = "SELECT * FROM VW_TXN_PREPAID_REGISTER WHERE TXN_ID=" & TXN_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)
        Return DT
    End Function

    Public Function GET_TXN_CUS_ID(ByVal TXN_ID As Integer) As Integer
        Dim SQL As String = "SELECT * FROM TB_SERVICE_TRANSACTION WHERE TXN_ID=" & TXN_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)
        Dim CUS_ID As Integer = 0
        If DT.Rows.Count > 0 Then
            If Not IsDBNull(DT.Rows(0).Item("CUS_ID")) Then
                CUS_ID = DT.Rows(0).Item("CUS_ID")
            End If
        End If
        Return CUS_ID
    End Function

End Class
