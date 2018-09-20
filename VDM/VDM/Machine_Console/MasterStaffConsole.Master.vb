Imports System.Data.SqlClient

Public Class MasterStaffConsole
    Inherits System.Web.UI.MasterPage
    Dim BL As New VDM_BL

    Private ReadOnly Property KO_ID As Integer
        Get
            Return Session("KO_ID")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsNumeric(Session("USER_ID")) Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('Please Login'); window.location.href='Login.aspx';", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            LoginInfo()

            GetMachineInfo()

        End If
    End Sub

    Private Sub LoginInfo()
        If Not IsNumeric(Session("FULL_NAME")) Then
            lblLoginName.Text = Session("FULL_NAME")
        Else
            Session.Abandon()
            Response.Redirect("Login.aspx")
        End If

    End Sub

    Private Sub GetMachineInfo()
        Dim DT As DataTable = BL.GetList_Kiosk(KO_ID)
        If DT.Rows.Count > 0 Then
            lblMachine_Name.Text = DT.Rows(0).Item("KO_CODE").ToString()
            lblMachine_Location.Text = DT.Rows(0).Item("SITE_CODE").ToString()
            If DT.Rows(0).Item("ZONE").ToString() <> "" Then
                lblMachine_Zone.Text = DT.Rows(0).Item("ZONE").ToString()
            Else
                spanZone.Visible = False
            End If

            '--Set Shift
            Dim DT_Shift As DataTable = BL.GetShift_Kiosk(KO_ID)
            Dim strClose_Time As String = ""
            Dim strOpen_Time As String = ""
            Dim SHIFT_ID As Integer = 0
            Dim SHIFT_CODE As String = ""
            Dim SHIFT_Status As Integer

            If DT_Shift.Rows.Count = 0 Then
                'เปิด Shop ใหม่
            Else
                SHIFT_CODE = DT_Shift.Rows(0).Item("SHIFT_CODE").ToString()
                SHIFT_ID = DT_Shift.Rows(0).Item("SHIFT_ID")
                If Not IsDBNull(DT_Shift.Rows(0).Item("Open_Time")) Then
                    Dim Open_Time As DateTime = DT_Shift.Rows(0).Item("Open_Time")
                    strOpen_Time = Open_Time.ToString("dd-MMM-yyyy") & " (" & ReportFriendlyTime(DateDiff(DateInterval.Minute, Open_Time, Now)) & ")"
                End If
                If Not IsDBNull(DT_Shift.Rows(0).Item("Close_Time")) Then
                    Dim Close_Time As DateTime = DT_Shift.Rows(0).Item("Close_Time")
                    strClose_Time = Close_Time.ToString("dd-MMM-yyyy") & " (" & ReportFriendlyTime(DateDiff(DateInterval.Minute, Close_Time, Now)) & ")"
                End If

            End If

            Session("SHIFT_ID") = SHIFT_ID

            If BL.CheckShift_Open(DT_Shift) Then
                lblHeader_Shift.Text = "Close Shift " & SHIFT_CODE
                lblHeader_Shift_Time.Text = " (เปิดตั้งแต่ " & strOpen_Time & ")"
                SHIFT_Status = VDM_BL.ShiftStatus.Close
            Else
                lblHeader_Shift.Text = "Open Shift "
                lblHeader_Shift_Time.Text = " (ปิดล่าสุดตั้งแต่ " & strClose_Time & ")"
                SHIFT_Status = VDM_BL.ShiftStatus.Open
            End If

            Session("SHIFT_Status") = SHIFT_Status

        Else
            Session.Abandon()
            Response.Redirect("Login.aspx")
        End If


    End Sub

    Private Sub lnkLogout_ServerClick(sender As Object, e As EventArgs) Handles lnkLogout.ServerClick
        Session.Abandon()
        Response.Redirect("Login.aspx")
    End Sub
End Class