Imports System.Data
Imports System.Security.Cryptography
Imports System.IO

Module ModuleGlobal

#Region "Script"
    Public Sub ImplementJavaMoneyText(ByRef Obj As WebControls.TextBox, Optional ByVal MaxValue As Double = Double.MaxValue, Optional ByVal Align As String = "Right")
        Obj.Attributes("OnChange") &= "this.value=formatmoney(this.value,'0','" & MaxValue & "');"
        Obj.Style.Item("Text-Align") = Align
    End Sub

    Public Sub ImplementJavaIntegerText(ByRef Obj As WebControls.TextBox, ByVal IncludeComma As Boolean, Optional ByVal MaxValue As Long = Long.MaxValue, Optional ByVal Align As String = "Right")
        Obj.Attributes("OnChange") &= "this.value=formatinteger(this.value,0," & MaxValue & "," & IncludeComma.ToString.ToLower & ");"
        Obj.Style.Item("Text-Align") = Align
    End Sub

    Public Sub ImplementJavaOnlyNumberText(ByRef Obj As WebControls.TextBox, Optional ByVal Align As String = "Right")
        Obj.Attributes("OnChange") &= "this.value=formatonlynumber(this.value);"
        Obj.Style.Item("Text-Align") = Align
    End Sub

    Public Sub ImplementJavaNumericText(ByRef Obj As WebControls.TextBox, Optional ByVal Align As String = "Right")
        Obj.Attributes("OnChange") &= "this.value=formatnumeric(this.value,'-999999999999','999999999999');"
        Obj.Style.Item("Text-Align") = Align
    End Sub

    Public Sub ImplementJavaNumericText(ByRef Obj As WebControls.TextBox, ByVal MaxDecimalPlace As Integer, ByVal Align As String)
        Obj.Attributes("OnChange") &= "this.value=formatnumericLimitPlace(this.value,'-999999999999','999999999999'," & MaxDecimalPlace & ");"
        Obj.Style.Item("Text-Align") = Align
    End Sub

    Public Sub ImplementJavaFloatText(ByRef Obj As WebControls.TextBox, ByVal DecimalPlace As UInteger, Optional ByVal Align As String = "Right")
        Obj.Attributes("OnChange") &= "this.value=formatfloat(this.value,'-999999999999','999999999999'," & DecimalPlace & ");"
        Obj.Style.Item("Text-Align") = Align
    End Sub

    Public Sub ImplementJavaResizeTextboxByContent(ByRef Obj As WebControls.TextBox, ByVal OneCharacterWidth As Integer)
        Obj.Attributes("onKeyUp") &= GetJavaResizeTextboxByContent(Obj, OneCharacterWidth)
        Obj.Attributes("OnChange") &= GetJavaResizeTextboxByContent(Obj, OneCharacterWidth)
    End Sub

    Public Sub ImplementJavaResizeDropdonwByContent(ByRef Obj As WebControls.DropDownList, ByVal OneCharacterWidth As Integer)
        Obj.Attributes("OnChange") &= GetJavaResizeDropdonwByContent(Obj, OneCharacterWidth)
    End Sub

    Public Function GetJavaResizeTextboxByContent(ByRef Obj As WebControls.TextBox, ByVal OneCharacterWidth As Integer) As String
        Return "resizeTextboxByContent('" & Obj.ClientID & "'," & OneCharacterWidth & ");"
    End Function
    Public Function GetJavaResizeDropdonwByContent(ByRef Obj As WebControls.DropDownList, ByVal OneCharacterWidth As Integer) As String
        Return "resizeDropdownByContent('" & Obj.ClientID & "'," & OneCharacterWidth & ");"
    End Function

    Public Function FormatNumericText(ByVal Value As Double, Optional ByVal IncludedComma As Boolean = True) As String
        Dim Crop As String = Value.ToString
        Dim DecPlace As Integer = 0
        Dim SpltPath As String() = Crop.Split(".")
        If SpltPath.Length > 1 Then
            DecPlace = Len(SpltPath(1))
        End If
        Return FormatNumber(Value, DecPlace,,, IncludedComma)
    End Function

    Public Function FormatNumericTextLimitPlace(ByVal Value As Double, ByVal IncludedComma As Boolean, ByVal MaxDecimalPlace As Integer) As String
        Dim Crop As String = Value.ToString
        Dim DecPlace As Integer = 0
        Dim SpltPath As String() = Crop.Split(".")
        If SpltPath.Length > 1 Then
            DecPlace = Len(SpltPath(1))
            If DecPlace > MaxDecimalPlace Then DecPlace = MaxDecimalPlace
        End If
        Return FormatNumber(Value, DecPlace,,, IncludedComma)
    End Function

    Public Sub Redirect(ByVal Page As Page, ByVal URL As String)
        ScriptManager.RegisterStartupScript(Page, GetType(String), "Redirect", "window.location.href='" & URL & "';", True)
    End Sub

    Public Sub Alert(ByVal Page As Page, ByVal Message As String)
        ScriptManager.RegisterStartupScript(Page, GetType(String), "Alert", "alert('" & Message.Replace("'", "\""") & "');", True)
    End Sub

    Public Sub CloseWindow(ByVal Page As Page)
        ScriptManager.RegisterStartupScript(Page, GetType(String), "CloseWindow", "window.close();", True)
    End Sub

    Public Sub CloseTopWindow(ByVal Page As Page)
        ScriptManager.RegisterStartupScript(Page, GetType(String), "CloseWindow", "top.close();", True)
    End Sub

    Public Sub ShowDialogEditSVG(ByVal Page As Page, ByVal UNIQUE_POPUP_ID As String, ByVal OKButton As String, Optional ByVal DisplayTitle As Boolean = True, Optional ByVal DisplayType As Boolean = True, Optional ByVal DisplayDesc As Boolean = True, Optional ByVal DisplayTag As Boolean = True, Optional ByVal _ReadOnly As Boolean = False)
        Dim Script As String = "ShowDialogEditSVG('" & UNIQUE_POPUP_ID.Replace("'", "\'") & "','" & OKButton & "','" & DisplayTitle.ToString & "','" & DisplayType.ToString & "','" & DisplayDesc.ToString & "','" & DisplayTag & "','" & _ReadOnly & "','" & Now.ToOADate.ToString.Replace(".", "") & "');"
        ScriptManager.RegisterStartupScript(Page, GetType(String), UNIQUE_POPUP_ID, Script, True)
    End Sub

    Public Sub ShowDialogEditDoc(ByVal Page As Page, ByVal UNIQUE_POPUP_ID As String, ByVal OKButton As String, Optional ByVal DisplayTitle As Boolean = True, Optional ByVal DisplayType As Boolean = True, Optional ByVal DisplayDesc As Boolean = True, Optional ByVal DisplayTag As Boolean = True, Optional ByVal _ReadOnly As Boolean = False)
        Dim Script As String = "ShowDialogEditDoc('" & UNIQUE_POPUP_ID.Replace("'", "\'") & "','" & OKButton & "','" & DisplayTitle.ToString & "','" & DisplayType.ToString & "','" & DisplayDesc.ToString & "','" & DisplayTag & "','" & _ReadOnly & "','" & Now.ToOADate.ToString.Replace(".", "") & "');"
        ScriptManager.RegisterStartupScript(Page, GetType(String), UNIQUE_POPUP_ID, Script, True)
    End Sub

    Public Enum ToaStrMode
        Success = 1
        Warning = 2
        Danger = 3
        Info = 4
    End Enum

    Public Enum ToaStrPositon
        TopLeft = 1
        TopCenter = 2
        TopRight = 3
        BottomLeft = 4
        BottomCenter = 5
        BottomRight = 6
        TopFullWidth = 7
        BottomFullWidth = 8
    End Enum

    Public Sub Message_ToaStr(ByVal Message As String, ByVal Mode As ToaStrMode, ByVal Position As ToaStrPositon, ByVal Page As Page, Optional ByVal TimeOut_MS As Integer = 5000, Optional ByVal ExtendTimeOut_MS As Integer = 5000)
        Dim _mode As String = ""

        Select Case Mode
            Case ToaStrMode.Success
                _mode = "success"
            Case ToaStrMode.Warning
                _mode = "warning"
            Case ToaStrMode.Danger
                _mode = "error"
            Case ToaStrMode.Info
                _mode = "info"
        End Select

        Dim _position As String = ""
        Select Case Position
            Case ToaStrPositon.TopLeft
                _position = "toast-top-left"
            Case ToaStrPositon.TopCenter
                _position = "toast-top-center"
            Case ToaStrPositon.TopRight
                _position = "toast-top-right"
            Case ToaStrPositon.BottomLeft
                _position = "toast-bottom-left"
            Case ToaStrPositon.BottomCenter
                _position = "toast-bottom-center"
            Case ToaStrPositon.BottomRight
                _position = "toast-bottom-right"
            Case ToaStrPositon.TopFullWidth
                _position = "toast-top-full-width"
            Case ToaStrPositon.BottomFullWidth
                _position = "toast-bottom-full-width"
        End Select



        Message = Message.Replace("'", "\'").Replace(vbLf, "<br/>")

        Dim _option = " {" & vbLf
        _option &= "'extendedTimeOut': '" & ExtendTimeOut_MS & "'," & vbLf
        _option &= "'timeOut': '" & TimeOut_MS & "'," & vbLf
        _option &= "'positionClass':'" & _position & "'" & vbLf
        _option &= "}"
        Dim SCript As String = "toastr." & _mode & "('" & Message & "', '', " & _option & ");"

        ScriptManager.RegisterStartupScript(Page, GetType(String), "toastr_msg", SCript, True)
    End Sub
#End Region

    Public Function ReadFile(ByVal Path As String) As Byte()
        If IO.File.Exists(Path) Then
            Dim F As IO.Stream = IO.File.Open(Path, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite)
            Dim C As New Converter
            Dim B As Byte() = C.StreamToByte(F)
            F.Close()
            Return B
        Else
            Return Nothing
        End If
    End Function

    Public Sub SaveFile(ByVal Path As String, ByVal B As Byte())
        If File.Exists(Path) Then
            DeleteFile(Path)
        End If

        'Make Parent Folder if not existed
        Dim p As String() = Path.Split("\")
        Array.Resize(p, p.Length - 1)
        Dim ParentFolder As String = String.Join("\", p)
        If Not Directory.Exists(ParentFolder) Then
            Directory.CreateDirectory(ParentFolder)
        End If

        Dim F As FileStream = File.Open(Path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite)
        F.Write(B, 0, B.Length)
        F.Close()
    End Sub

    Public Function OriginalFileName(ByVal FullPath As String) As String
        Return FullPath.Substring(FullPath.LastIndexOf("\") + 1)
    End Function

    Public Function OriginalFileType(ByVal FullPath As String) As String
        Return FullPath.Substring(FullPath.LastIndexOf(".") + 1)
    End Function

    Public Function GetIPAddress() As String
        Return HttpContext.Current.Request.UserHostAddress
    End Function

    Public Function GetCurrentPageName() As String
        Dim URL As String = ""
        Try
            URL = HttpContext.Current.Request.ServerVariables("URL")
            URL = URL.Substring(URL.LastIndexOf("/") + 1)
        Catch : End Try
        Return URL
    End Function

    Public Function GenerateNewUniqueID() As String
        Return Guid.NewGuid.ToString
    End Function

    Public Sub DeleteFile(ByVal Path As String)
        Dim TryTime As Integer = 0
        Dim MaxTry As Integer = 5
        While File.Exists(Path) And TryTime < MaxTry
            TryTime += 1
            Try
                My.Computer.FileSystem.DeleteFile(Path)
            Catch
                Threading.Thread.Sleep(300)
            End Try
        End While
    End Sub

    Public Sub DeleteDirectory(ByVal Path As String)
        My.Computer.FileSystem.DeleteDirectory(Path, FileIO.DeleteDirectoryOption.DeleteAllContents)
    End Sub

    Public Sub PushString(ByRef Arr As String(), ByVal NextString As String)
        Array.Resize(Arr, Arr.Length + 1)
        Arr(Arr.Length - 1) = NextString
    End Sub


    Public Sub PushInt(ByRef Arr As Integer(), ByVal NextInt As Integer)
        Array.Resize(Arr, Arr.Length + 1)
        Arr(Arr.Length - 1) = NextInt
    End Sub

    Public Function MD5(ByVal Input As String) As String
        Return String.Join("", System.Security.Cryptography.MD5.Create().ComputeHash(System.Text.Encoding.ASCII.GetBytes(Input)).Select(Function(x) x.ToString("x2")))
    End Function

#Region "MaterialForm"
    Public Sub StoreMaterialForm(ByRef Panel As Control)
        For i As Integer = 0 To Panel.Controls.Count - 1
            Dim _ctrl As Control = Panel.Controls(i)
            If _ctrl.HasControls Then
                For j As Integer = 0 To _ctrl.Controls.Count - 1
                    StoreMaterialForm(_ctrl.Controls(j))
                Next
            Else
                Dim _class As String() = _ctrl.GetType().ToString().Split(".")
                Dim _ctrlClass As String = _class(_class.Length - 1)
                Select Case _ctrlClass
                    Case "TextBox"
                        If CType(_ctrl, TextBox).CssClass.IndexOf("form-control") > -1 And CType(_ctrl, TextBox).Text = "" Then
                            AddCssClass(_ctrl, "empty")
                        ElseIf CType(_ctrl, TextBox).CssClass.IndexOf("form-control") > -1 Then
                            RemoveCssClass(_ctrl, "empty")
                        End If
                End Select
            End If
        Next
    End Sub

    Public Sub AddCssClass(ByRef Control As TextBox, ByVal CssClass As String)
        Dim _Class() As String = Control.CssClass.Split(" ")
        Dim Result As String = ""
        For i As Integer = 0 To _Class.Length - 1
            If _Class(i) <> "" And _Class(i) <> CssClass And Result.IndexOf(" " & _Class(i)) = -1 Then
                Result &= " " & Trim(_Class(i))
            End If
        Next
        If Result <> "" Then Result = Result.Substring(1)
        Result &= " " & CssClass
        Control.CssClass = Result
    End Sub

    Public Sub RemoveCssClass(ByRef Control As TextBox, ByVal CssClass As String)
        Dim _Class() As String = Control.CssClass.Split(" ")
        Dim Result As String = ""
        For i As Integer = 0 To _Class.Length - 1
            If _Class(i) <> "" And _Class(i) <> CssClass Then
                Result &= " " & Trim(_Class(i))
            End If
        Next
        If Result <> "" Then Result = Result.Substring(1)
        Control.CssClass = Result
    End Sub
#End Region

#Region "JSON"
    '---- Report Result in Collection Format -----------
    Public Function DataTableToJSON(ByVal DT As DataTable) As String
        Dim JS As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim rows As New List(Of Dictionary(Of String, Object))
        For i As Integer = 0 To DT.Rows.Count - 1
            Dim row As New Dictionary(Of String, Object)
            For j As Integer = 0 To DT.Columns.Count - 1
                row.Add(DT.Columns(j).ColumnName, DT.Rows(i).Item(j))
            Next
            rows.Add(row)
        Next
        Return JS.Serialize(rows)
    End Function

    '- -------- Remove [Collection] Format ------------
    Public Function SingleRowDataTableToJSON(ByVal DT As DataTable) As String
        Dim JSON As String = DataTableToJSON(DT)
        If JSON <> "" Then
            JSON = JSON.Substring(1, JSON.Length - 2)
        End If
        Return JSON
    End Function
#End Region

    Public Function GetJavascriptTimestamp(ByVal input As DateTime) As Long
        Dim Span As TimeSpan = New System.TimeSpan(System.DateTime.Parse("1/1/1970").Ticks)
        Dim T As DateTime = input.Subtract(Span)
        Return T.Ticks / 10000
    End Function


    Public Function ExcelContentType(ByVal ContentType As String) As Boolean
        Dim Result As Boolean = True
        Select Case ContentType
            Case "application/vnd.ms-excel"
            Case "application/msexcel"
            Case "application/x-msexcel"
            Case "application/x-ms-excel"
            Case "application/x-excel"
            Case "application/x-dos_ms_excel"
            Case "application/xls"
            Case "application/x-xls"
            Case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Case Else
                Result = False
        End Select
        Return Result
    End Function

    Public Structure FileStructure
        Public ContentType As String
        Public FileName As String
        Public Content As Byte()
    End Structure

    Public Function ReportFriendlyTime(ByVal Minute As Integer) As String
        Select Case True
            Case Minute < 2
                Return "last minute"
            Case Minute < 60
                Return "last " & Minute & " minutes"
            Case Minute < 120
                Return "last hour"
            Case Minute < 2880
                Return "last " & Int(Minute / 60) & " hours"
            Case Minute < 44640
                Return "last " & Int(Minute / 1440) & " days"
                'Case Minute < 4017600
            Case Else
                Return "Last " & Int(Minute / 44640) & " months"
        End Select
    End Function

End Module
