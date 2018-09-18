Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationManager

Public Class VDM_BL

    Public ConnectionString As String = ConnectionStrings("ConnectionString").ConnectionString
    Public ServerMapPath As String = AppSettings("ServerMapPath").ToString
    Public PicturePath As String = AppSettings("PicturePath").ToString
    Public KioskID As String = AppSettings("KioskID").ToString

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

    Public Enum UILanguage
        TH = 1
        EN = 2
        CN = 3
        JP = 4
        KR = 5
        RS = 6
    End Enum

    Public Enum DeviceType
        CashIn = 1
        Cash20 = 2
        Cash100 = 3
        CoinIn = 4
        Coin1 = 5
        Coin5 = 6
        Printer = 7
        Passport = 8
        Camera = 9
        SIMDisp = 10
        DispenserSlot2 = 11
        Barcode = 12
    End Enum

    Public Enum ShiftStatus
        Close = 0
        Open = 1
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
                Return ""
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
                SQL &= " SPEC_NAME_CH SPEC_NAME " & vbLf
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
        Dim DA As New SqlDataAdapter(Sql, ConnectionString)
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
#End Region

#Region "Kiosk Management"
    Public Function GetList_Kiosk(Optional ByVal KO_ID As Integer = 0) As DataTable
        Dim SQL As String = ""
        SQL &= " SELECT KO.KO_ID,KO.KO_CODE,KO.SITE_ID,Site.SITE_CODE,KO.ZONE,KO.IP,KO.Mac   " & vbLf
        SQL &= " ,KO.IsOnline,COUNT(SLOT.PRODUCT_ID) Total_Product,KO.Active_Status " & vbLf
        SQL &= " FROM MS_KIOSK KO           " & vbLf
        SQL &= " INNER JOIN MS_Site Site on KO.SITE_ID = Site.SITE_ID" & vbLf
        SQL &= " LEFT JOIN VW_KIOSK_SLOT SLOT ON  KO.KO_ID=SLOT.KO_ID         " & vbLf
        SQL &= " " & vbLf
        If KO_ID <> 0 Then
            SQL &= " WHERE KO.KO_ID=" & KO_ID & vbLf
        End If
        SQL &= " GROUP BY KO.KO_ID,KO.KO_CODE,KO.SITE_ID,Site.SITE_CODE,KO.ZONE,KO.IP,KO.Mac,KO.IsOnline,KO.Active_Status" & vbLf
        SQL &= " ORDER BY KO.KO_ID" & vbLf
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

    Public Function GetList_Kiosk_Device(Optional ByVal KO_ID As Integer = 0) As DataTable
        Dim SQL As String = ""
        SQL &= " Select " & vbLf
        SQL &= " K.KO_ID, D.D_ID, D.D_Name, D.DT_ID, DT.DT_Name, " & vbLf
        SQL &= " D.Unit_Value, " & vbLf
        SQL &= " ISNULL(D.Max_Qty, D.Max_Qty) Max_Qty," & vbLf
        SQL &= " ISNULL(D.Warning_Qty,D.Warning_Qty) Warning_Qty," & vbLf
        SQL &= " ISNULL(D.Critical_Qty,D.Critical_Qty) Critical_Qty,KD.Current_Qty,DT.Movement_Direction," & vbLf
        SQL &= " DS.DS_ID,DS.DS_Name,DS.IsProblem,KD.Update_Time,D.D_Order,Icon_White,Icon_Red,Icon_Green" & vbLf
        SQL &= " FROM " & vbLf
        SQL &= " MS_KIOSK K " & vbLf
        SQL &= " INNER JOIN MS_DEVICE D ON K.Active_Status=1 AND D.Active_Status=1" & vbLf
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
        Drop_PRODUCT_FLOOR(KO_ID)
        Drop_KIOSK_DEVICE(KO_ID)
        Drop_SHIFT(KO_ID)

        Dim SQL As String = "DELETE FROM MS_KIOSK WHERE KO_ID=" & KO_ID
        ExecuteNonQuery(SQL)
    End Sub

    Public Sub Drop_PRODUCT_STOCK_SERIAL(ByVal SERIAL As String)
        Dim SQL As String = "SELECT SLOT_ID FROM TB_PRODUCT_SERIAL_STOCK" & vbLf
        SQL &= "WHERE SERIAL='" & SERIAL.Replace("'", "''") & "'"
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)

        For i As Integer = 0 To DT.Rows.Count - 1
            Drop_PRODUCT_STOCK_SERIAL(DT.Rows(0).Item("SLOT_ID"), SERIAL)
        Next
    End Sub

    Public Sub Drop_PRODUCT_STOCK_SERIAL(ByVal SLOT_ID As Integer)
        Dim SQL As String = "SELECT SERIAL FROM TB_PRODUCT_SERIAL_STOCK" & vbLf
        SQL &= "WHERE SLOT_ID=" & SLOT_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)

        For i As Integer = 0 To DT.Rows.Count - 1
            Drop_PRODUCT_STOCK_SERIAL(SLOT_ID, DT.Rows(0).Item("SERIAL"))
        Next
    End Sub

    Public Sub Drop_PRODUCT_STOCK_SERIAL(ByVal SLOT_ID As Integer, ByVal SERIAL As String)
        Dim SQL As String = "DELETE FROM TB_PRODUCT_SERIAL_STOCK" & vbLf
        SQL &= "WHERE SLOT_ID=" & SLOT_ID & " AND SERIAL='" & SERIAL.Replace("'", "''") & "'"
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

    Public Sub Drop_PRODUCT_FLOOR(ByVal KO_ID As Integer, ByVal FLOOR_ID As Integer)
        Drop_PRODUCT_SLOT(FLOOR_ID)

        Dim SQL As String = "DELETE FROM TB_PRODUCT_FLOOR" & vbLf
        SQL &= "WHERE KO_ID=" & KO_ID & " AND FLOOR_ID=" & FLOOR_ID
        ExecuteNonQuery(SQL)
    End Sub

    Public Sub Drop_PRODUCT_FLOOR(ByVal KO_ID As Integer)
        Dim SQL As String = "SELECT FLOOR_ID FROM TB_PRODUCT_FLOOR WHERE KO_ID=" & KO_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)

        For i As Integer = 0 To DT.Rows.Count - 1
            Drop_PRODUCT_FLOOR(KO_ID, DT.Rows(i).Item("FLOOR_ID"))
        Next
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

    Public Function GetShift_Kiosk(Optional ByVal KO_ID As Integer = 0) As DataTable
        Dim SQL As String = ""
        SQL &= "   Select TOP 1 SHIFT_ID" & vbLf
        SQL &= "  ,KO_ID" & vbLf
        SQL &= "  ,SHIFT_Y,SHIFT_M,SHIFT_D,SHIFT_N" & vbLf
        SQL &= "  ,'SHIFT'+SHIFT_Y+SHIFT_M+SHIFT_D+ convert(varchar,SHIFT_N)    SHIFT_CODE" & vbLf

        SQL &= "  ,Open_Time,Open_By , USER_Open.FIRST_NAME+' '+ USER_Open.LAST_NAME Open_By_Name" & vbLf
        SQL &= "  ,Close_Time,Close_By  , USER_Close.FIRST_NAME+' '+ USER_Close.LAST_NAME Close_By_Name" & vbLf
        SQL &= "  From TB_SHIFT" & vbLf
        SQL &= "  Left Join MS_USER USER_Open On USER_Open.USER_ID=TB_SHIFT.Open_By" & vbLf
        SQL &= "  Left Join MS_USER USER_Close ON USER_Close.USER_ID=TB_SHIFT.Close_By" & vbLf
        SQL &= "  Where KO_ID=" & KO_ID & vbLf
        SQL &= "  Order By SHIFT_ID" & vbLf

        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DT.TableName = "Data"
        DA.Fill(DT)
        Return DT
    End Function

    Public Function CheckShift_Open(ByRef DT As DataTable) As Boolean
        Dim IsOpen As Boolean = False
        If DT.Rows.Count > 0 Then
            If IsDBNull(DT.Rows(0).Item("Close_Time")) Then
                IsOpen = True
            End If
        End If
        Return IsOpen
    End Function

#End Region





End Class
