
Imports CashDispenser

Public Class CashDispense
    Inherits System.Web.UI.Page

    Dim BL As New Core_BL

    Public ReadOnly Property NoteType As Integer
        Get
            If IsNumeric(Request.QueryString("Type")) Then
                Return Request.QueryString("Type")
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property Quantity As Integer
        Get
            If IsNumeric(Request.QueryString("Q")) Then
                Return Request.QueryString("Q")
            Else
                Return 0
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dispense()
    End Sub

    Public Sub Dispense()
        Dim DT As New DataTable
        DT.Columns.Add("status", GetType(Boolean))
        DT.Columns.Add("message", GetType(String))
        Dim DR As DataRow = DT.NewRow
        DT.Rows.Add(DR)
        Try
            Dim Cash As New CashDispenser.CashDispenser
            Select Case NoteType
                Case 20
                    Cash.SetPort(BL.Dispense20_Port)
                Case 50
                    Cash.SetPort(BL.Dispense50_Port)
                Case 100
                    Cash.SetPort(BL.Dispense100_Port)
                Case 500
                    Cash.SetPort(BL.Dispense500_Port)
            End Select

            Select Case Cash.CurrentStatus
                Case Status.Unknown
                    DR("status") = False
                    DR("message") = "Unknown"
                Case Status.Stock_less
                    DR("status") = False
                    DR("message") = "Stock_less"
                Case Status.Single_machine_payout
                    DR("status") = False
                    DR("message") = "Single_machine_payout"
                Case Status.Sensor_Error
                    DR("status") = False
                    DR("message") = "Sensor_Error"
                Case Status.Sensor_adjusting
                    DR("status") = False
                    DR("message") = "Sensor_adjusting"
                Case Status.Payout_fails
                    DR("status") = False
                    DR("message") = "Payout_fails"
                Case Status.Over_length
                    DR("status") = False
                    DR("message") = "Over_length"
                Case Status.Note_Not_Exit
                    DR("status") = False
                    DR("message") = "Note_Not_Exit"
                Case Status.Note_jam
                    DR("status") = False
                    DR("message") = "Note_jam"
                Case Status.Multiple_machine_payout
                    DR("status") = False
                    DR("message") = "Multiple_machine_payout"
                Case Status.Motor_Error
                    DR("status") = False
                    DR("message") = "Motor_Error"
                Case Status.Low_power_Error
                    DR("status") = False
                    DR("message") = "Low_power_Error"
                Case Status.Empty_note
                    DR("status") = False
                    DR("message") = "Empty_note"
                Case Status.Double_note_error
                    DR("status") = False
                    DR("message") = "Double_note_error"
                Case Status.Dispensing_busy
                    DR("status") = False
                    DR("message") = "Dispensing_busy"
                Case Status.Disconnected
                    DR("status") = False
                    DR("message") = "Disconnected"
                Case Status.Checksum_Error
                    DR("status") = False
                    DR("message") = "Checksum_Error"
                Case Status.Payout_successful, Status.Ready
                    Cash.Dispense(Quantity) '------------ Do Actoin------------
                    DR("status") = True
                    DR("message") = "success"
            End Select
        Catch ex As Exception
            DR("status") = False
            DR("message") = ex.Message
        End Try
        Response.Write(SingleRowDataTableToJSON(DT))
    End Sub

End Class