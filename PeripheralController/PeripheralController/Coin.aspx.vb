Imports CoinDispenser

Public Class Coin
    Inherits System.Web.UI.Page

    Dim BL As New Core_BL
    Public ReadOnly Property Quantity As Integer
        Get
            Try
                Return Request.QueryString("Q")
            Catch ex As Exception
                Return 0
            End Try
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
            Dim Coin As New CoinDispenser.CoinDispenser
            Coin.SetPort(BL.CoinIn_Port)

            Select Case Coin.CurrentStatus
                Case Status.Insufficient_Coin
                    DR("status") = False
                    DR("message") = "Insufficient_Coin"
                Case Status.Unknown
                    DR("status") = False
                    DR("message") = "Unknown"
                Case Status.Unavailable
                    DR("status") = False
                    DR("message") = "Unavailable"
                Case Status.Shaft_Sensor_Failure
                    DR("status") = False
                    DR("message") = "Shaft_Sensor_Failure"
                Case Status.Reserved
                    DR("status") = False
                    DR("message") = "Reserved"
                Case Status.Prism_Sensor_Failure
                    DR("status") = False
                    DR("message") = "Prism_Sensor_Failure"
                Case Status.Motor_Problems
                    DR("status") = False
                    DR("message") = "Motor_Problems"
                Case Status.Inhibit_BA_if_hopper_problems_occurred
                    DR("status") = False
                    DR("message") = "Inhibit_BA_if_hopper_problems_occurred"
                Case Status.Enable_BA_if_hopper_problems_recovered
                    DR("status") = False
                    DR("message") = "Enable_BA_if_hopper_problems_recovered"
                Case Status.Disconnected
                    DR("status") = False
                    DR("message") = "Disconnected"
                Case Status.Dedects_coin_dispensing_activity_after_suspending_the_dispene_signal
                    DR("status") = False
                    DR("message") = "Dedects_coin_dispensing_activity_after_suspending_the_dispene_signal"
                Case Status.Ready, Status.Enable
                    Coin.Dispense(Quantity)
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