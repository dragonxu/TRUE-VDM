Imports CoinDispenser

Public Class CoinDispense
    Inherits System.Web.UI.Page

    Dim BL As New Core_BL

    Public ReadOnly Property CoinType As Integer
        Get
            If IsNumeric(Request.QueryString("TYPE")) Then
                Return Request.QueryString("TYPE")
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
            Dim Coin As New CoinDispenser.CoinDispenser
            Select Case CoinType
                Case 1
                    Coin.SetPort(BL.Dispense1_Port)
                Case 2
                    Coin.SetPort(BL.Dispense2_Port)
                Case 5
                    Coin.SetPort(BL.Dispense5_Port)
                Case 10
                    Coin.SetPort(BL.Dispense10_Port)
            End Select

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
                    Coin.Dispense(Quantity) '------------ Do Actoin------------
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