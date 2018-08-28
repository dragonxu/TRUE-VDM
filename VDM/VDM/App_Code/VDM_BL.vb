Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationManager

Public Class VDM_BL

    Public ConnectionString As String = ConnectionStrings("ConnectionString").ConnectionString
    Public ServerMapPath As String = AppSettings("ServerMapPath").ToString
    Public PicturePath As String = AppSettings("PicturePath").ToString

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

End Class
