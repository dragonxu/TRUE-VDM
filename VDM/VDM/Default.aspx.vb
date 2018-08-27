Imports System.Data
Imports System.Data.SqlClient

Public Class _Default
    Inherits System.Web.UI.Page

    Dim BL As New CMPG_BL

    Private ReadOnly Property Available_Site As DataTable
        Get
            Return Session("Available_Site")
        End Get
    End Property

    Private ReadOnly Property Site_Filter As String
        Get
            Dim Result As String = ""
            If Not IsNothing(Available_Site) Then
                For i As Integer = 0 To Available_Site.Rows.Count - 1
                    Result &= Available_Site.Rows(i).Item("Site_ID") & ","
                Next
            End If
            If Result <> "" Then Result = Result.Substring(0, Result.Length - 1)
            Return Result
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNumeric(Session("STAFF_ID")) Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('Please Login'); window.location.href='SignIn.aspx';", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            BindChart()
        End If

    End Sub

    Dim DT As DataTable
    Private Sub BindChart()
        DT = Dashboard_Default_Data(Now.Year)
        '------------- TXN_date, Service_ID,Service_Amount --------------
        BindLine()
        BindBar()
        BindRadar()
        BindNotification()
    End Sub

    Private Function Dashboard_Default_Data(ByVal initYear As Integer) As DataTable
        Dim SQL As String = "SELECT TXN_ID,TXN_Code,Site_ID,REPLACE(CONVERT(VARCHAR,Start_Time,111),'/','-')+' '+CONVERT(VARCHAR,Start_Time,108) TXN_TIME,Service_Amount" & vbLf
        SQL &= " FROM VW_Service_TXN" & vbLf
        SQL &= " WHERE Service_ID IS NOT NULL AND Service_Status=" & CMPG_BL.SystemStatus.Complete & vbLf

        If Not IsNothing(Available_Site) Then
            SQL &= " AND Site_ID IN (" & Site_Filter & ") " & vbLf
        End If


        SQL &= " ORDER BY Start_Time" & vbLf
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        Return DT
    End Function

    Private Sub BindLine()
        '----------------- Line Chart For This Week ------------
        Dim Script As String = "$( document ).ready(function() {" & vbLf
        Script &= "var _lineData=[" & vbLf
        Dim C As New Converter
        Dim Obj As Object

        Dim SUM2Week As Integer = 0
        For i As Integer = 14 To 0 Step -1
            Dim D As Date = DateAdd(DateInterval.Day, -i, Now)
            Obj = DT.Compute("SUM(Service_Amount)", "TXN_TIME LIKE '" & D.ToString("yyyy-MM-dd") & "%'")
            If Not IsDBNull(Obj) Then
                Script &= "['" & C.DateToString(D, "MMM dd yyyy") & "', " & CInt(Obj) & "]"
                SUM2Week += Obj
            Else
                Script &= "['" & C.DateToString(D, "MMM dd yyyy") & "',0]"
            End If
            If i <> 14 Then Script &= ","
        Next
        Script &= "];" & vbLf & vbLf
        Script &= "var lineData = [" & vbLf
        Script &= " {" & vbLf
        Script &= "     data: _lineData," & vbLf
        Script &= "     color:  '#fff'" & vbLf
        Script &= " }" & vbLf
        Script &= "];" & vbLf

        Script &= "render_Line(lineData);" & vbLf
        Script &= "});" & vbLf
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "LineChart", Script, True)

        '---------------- Last 2 Week2 ------------
        If SUM2Week > 0 Then
            lblSalesValues.Text = FormatNumber(SUM2Week, 0) & " ฿"
        Else
            lblSalesValues.Text = "-"
        End If

        '---------------- Daily Sales--------------
        Obj = DT.Compute("SUM(Service_Amount)", "TXN_TIME Like '" & Now.ToString("yyyy-MM-dd") & "%'")
        If Not IsDBNull(Obj) Then
            lblDailySales.Text = FormatNumber(Obj, 0) & " ฿"
        Else
            lblDailySales.Text = "-"
        End If

        '---------------- Monthly Sales--------------
        Obj = DT.Compute("SUM(Service_Amount)", "TXN_TIME LIKE '" & Now.ToString("yyyy-MM") & "%'")
        If Not IsDBNull(Obj) Then
            lblMonthlySales.Text = FormatNumber(Obj, 0) & " ฿"
        Else
            lblMonthlySales.Text = " - "
        End If

        '---------------- Yearly Sales--------------
        Obj = DT.Compute("SUM(Service_Amount)", "")
        If Not IsDBNull(Obj) Then
            lblYearlySales.Text = FormatNumber(Obj, 0) & " ฿"
        Else
            lblYearlySales.Text = " - "
        End If

        Dim StartDate As String = DateAdd(DateInterval.Day, -7, Now).ToString("yyyy-MM-dd")
        Dim EndDate As String = DateAdd(DateInterval.Day, 1, Now).ToString("yyyy-MM-dd")
        Obj = DT.Compute("SUM(Service_Amount)", "TXN_TIME>='" & StartDate & "-01' AND TXN_TIME<'" & EndDate & "'")
        If Not IsDBNull(Obj) Then
            lblWeeklySales.Text = FormatNumber(Obj, 0) & " ฿"
        Else
            lblWeeklySales.Text = " - "
        End If

    End Sub

    Private Sub BindBar()


        Dim M As Integer = DatePart(DateInterval.Month, Now)
        Dim Obj As Object
        'Run Series
        Dim Script As String = "$( document ).ready(function() {" & vbLf
        Script &= "var barData = [" & vbLf
        '------------------- Buy New SIM --------------------
        Script &= "{" & vbLf
        Script &= " data: [" & vbLf
        For i As Integer = 1 To M
            Dim D As New DateTime(Now.Year, i, 1)
            Script &= " [" & GetJavascriptTimestamp(D) & ","
            Obj = DT.Compute("SUM(Service_Amount)", "TXN_TIME LIKE '" & Now.Year & "-" & i.ToString.PadLeft(2, "0") & "-%'")
            If Not IsDBNull(Obj) Then
                Script &= CInt(Obj) & "]"
            Else
                Script &= "0]"
            End If
            If i <> M Then
                Script &= "," & vbLf
            Else
                Script &= vbLf
            End If
        Next
        Script &= " ]," & vbLf
        Script &= " bars: {" & vbLf
        Script &= "    show: true," & vbLf
        Script &= "    barWidth: 7 * 24 * 60 * 60 * 1000," & vbLf
        Script &= "    fill: true," & vbLf
        Script &= "    lineWidth: 0," & vbLf
        Script &= "    order: 1," & vbLf
        Script &= "    fillColor: $.staticApp.primary" & vbLf
        Script &= "     }" & vbLf
        Script &= "}" & vbLf
        Script &= "];" & vbLf


        Script &= " render_Bar(barData);" & vbLf
        Script &= "});" & vbLf
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "BarChart", Script, True)
    End Sub

    Private Sub BindRadar()
        DT.DefaultView.RowFilter = "TXN_TIME LIKE '" & Now.ToString("yyyy-MM-") & "%'"
        DT.DefaultView.RowFilter = ""
        Dim tmp As DataTable = DT.DefaultView.ToTable
        Dim Obj As Object
        Dim Script As String = "$( document ).ready(function() {" & vbLf
        Script &= "var radarChartData = {" & vbLf
        Script &= " labels: ["
        For i As Integer = 0 To 23
            Script &= "'" & i.ToString.PadLeft(2, "0") & "'"
            If i <> 23 Then
                Script &= ","
            End If
        Next
        Script &= "]," & vbLf
        Script &= " datasets: [{" & vbLf
        Script &= "            fillColor: '#0099cc'," & vbLf
        Script &= "            strokeColor: '#0099cc'," & vbLf
        'Script &= "            pointColor: 'rgba(220,220,220,1)'," & vbLf
        'Script &= "            pointStrokeColor: '#fff'," & vbLf
        Script &= "            data: ["
        For i As Integer = 0 To 23
            Obj = tmp.Compute("COUNT(Service_Amount)", "TXN_TIME LIKE '% " & i.ToString.PadLeft(2, "0") & "%'")
            If Not IsDBNull(Obj) Then
                Script &= CInt(Obj)
            Else
                Script &= "0"
            End If
            If i <> 23 Then
                Script &= ","
            End If
        Next
        Script &= "]" & vbLf
        Script &= "     }]" & vbLf
        Script &= "};"
        Script &= "render_Radar(radarChartData);" & vbLf
        Script &= "});" & vbLf
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RadarChart", Script, True)
    End Sub

    Private Sub BindNotification()
        'Dim dt As New DataTable
        'dt = BL.Dashboard_Notification_Data(uLoc_Code)

        'Dim builder As New StringBuilder
        'builder.Append("<ul Class='notifications'>")
        'builder.Append("<li>")
        'builder.Append("<ul Class='notifications-list'>")

        'For i As Integer = 0 To dt.Rows.Count - 1
        '    Dim d_name As String = dt.Rows(i)("d_name").ToString
        '    Dim problem As String = dt.Rows(i)("ds_name").ToString
        '    Dim terminal As String = dt.Rows(i)("ais_terminal_id").ToString

        '    builder.Append("<li>")
        '    builder.Append("<a href = 'javascript:;' >")
        '    builder.Append("<div class='notification-icon'>")
        '    builder.Append("<div class='circle-icon bg-danger text-white'>")
        '    builder.Append("<i class='icon-ban'></i>")
        '    builder.Append("</div>")
        '    builder.Append("</div>")
        '    builder.Append("<span Class='notification-message'>" & d_name & "</span>")
        '    builder.Append("<span Class='notification-message'>" & problem & "</span>")
        '    builder.Append("<span Class='notification-message'>Terminal:" & terminal & "</span>")
        '    builder.Append("</a>")
        '    builder.Append("</li>")
        'Next

        'builder.Append("</ul>")
        'builder.Append("</li>")
        'builder.Append("</ul>")

        'lblNotificationList.Text = builder.ToString()
    End Sub

End Class