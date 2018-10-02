Imports CashReceiver
Imports CoinReciever
Imports System.Data

Public Class RequireCash
    Inherits System.Web.UI.Page

    Dim BL As New Core_BL

    Dim Cash As CashReceiver.CashReceiver = Nothing
    Dim Coin As CoinReciever.CoinReciever = Nothing
    Dim Result As DataTable

    Private ReadOnly Property Required As Integer
        Get
            Try
                Return Request.QueryString("REQ")
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Result = New DataTable
        Result.Columns.Add("amount", GetType(Integer))
        Result.Columns.Add("status", GetType(Boolean))
        Result.Columns.Add("message", GetType(String))


        '--------------------- รอ Test-------------
        If Required > 0 Then
            WaitForRecieve()
        Else
            Dim DR As DataRow = Result.NewRow
            Result.Rows.Add(DR)
            DR("amount") = 0
            DR("status") = False
            DR("message") = "no requirement"
            Response.Write(SingleRowDataTableToJSON(Result))
            Response.End()
        End If

    End Sub

    Public Sub WaitForRecieve()

        Dim DR As DataRow = Result.NewRow
        DR("amount") = 0
        DR("status") = False
        DR("message") = ""
        Result.Rows.Add(DR)

        Cash = New CashReceiver.CashReceiver
        Coin = New CoinReciever.CoinReciever



        Cash.SetPort(BL.CashReciever_Port)
        Coin.SetPort(BL.CoinReciever_Port)

        Try
            Cash.Close()
            Cash.Open()
        Catch : End Try

        Try
            Coin.Close()
            Coin.Open()
        Catch : End Try

        '---------------- Check Cash Status---------------
        Try
            Select Case Cash.CurrentStatus
                Case CashState.Unknown
                    DR("message") = "Cash Reciever is Unknown"
                Case CashState.Unavailable
                    DR("message") = "Cash Reciever is Unavailable"
                Case CashState.StackerOpen
                    DR("message") = "Cash Reciever is StackerOpen"
                Case CashState.SensorProblem
                    DR("message") = "Cash Reciever is SensorProblem"
                Case CashState.Poweroff
                    DR("message") = "Cash Reciever is Poweroff"
                Case CashState.MotorFailure
                    DR("message") = "Cash Reciever is MotorFailure"
                Case CashState.Disconnected
                    DR("message") = "Cash Reciever is Disconnected"
                Case CashState.CheckSumError
                    DR("message") = "Cash Reciever is CheckSumError"
                Case CashState.BillRemove
                    DR("message") = "Cash Reciever is BillRemove"
                Case CashState.BillJam
                    DR("message") = "Cash Reciever is BillJam"
                Case CashState.BillFish
                    DR("message") = "Cash Reciever is BillFish"
                Case CashState.Ready
                    DR("message") = "" '---------------OK ------------------
            End Select
        Catch ex As Exception
            DR("message") = ex.Message
        End Try
        If DR("message") <> "" Then
            Response.Write(SingleRowDataTableToJSON(Result))
            Exit Sub
        End If

        '---------------- Check Coin Status---------------
        Try
            Select Case Coin.CurrentStatus
                Case CoinState.Unknown
                    DR("message") = "Coin Reciever is Unknown"
                Case CoinState.Unavailable
                    DR("message") = "Coin Reciever is Unavailable"
                Case CoinState.Sensor_1_problem
                    DR("message") = "Coin Reciever is Sensor_1_problem"
                Case CoinState.Sensor_2_problem
                    DR("message") = "Coin Reciever is Sensor_2_problem"
                Case CoinState.Sensor_3_problem
                    DR("message") = "Coin Reciever is Sensor_3_problem"
                Case CoinState.Disconnected
                    DR("message") = "Coin Reciever is Disconnected"
                Case CoinState.Ready
                    DR("message") = "" '---------------OK ------------------
            End Select
        Catch ex As Exception
            DR("message") = ex.Message
        End Try
        If DR("message") <> "" Then
            Response.Write(SingleRowDataTableToJSON(Result))
            Exit Sub
        End If

        Cash.Open()
        Coin.Open()

        AddHandler Cash.Received, AddressOf Cash_Received
        AddHandler Coin.Recieved, AddressOf Coin_Received

        '------------- รอจนกว่าจะหยอด
        While Recieved = 0 And StartWait < EndWait
            Threading.Thread.Sleep(200)
        End While
        '------------- ไม่หยอดสักที -------------
        Try : Cash.Close() : Catch : End Try
        Try : Coin.Close() : Catch : End Try

        DR("amount") = Recieved
        DR("status") = Recieved > 0
        If Recieved = 0 Then
            DR("message") = "timeout"
        Else
            DR("message") = "success"
        End If
        Response.Write(SingleRowDataTableToJSON(Result))
    End Sub

    Dim Recieved As Integer = 0
    Dim StartWait As DateTime = Now
    Dim EndWait As DateTime = DateAdd(DateInterval.Minute, 2, Now)

    Public Sub Cash_Received(Sender As Object, e As CashReceiver.CashEvent)

        Dim DR As DataRow = Result.Rows(0)

        Select Case e.Message
            Case "20", "50", "100", "500", "1000"
                Recieved = e.Message
                'DR("amount") = Recieved
                'DR("status") = True
                'DR("message") = "success"
                'Case Else
                '    DR("amount") = 0
                '    DR("status") = False
                '    DR("message") = e.State.ToString
        End Select
        'Cash.Close()
        'Response.Write(SingleRowDataTableToJSON(Result))
        'Response.End() '--------- ออกจาก Wait Loop
    End Sub

    Public Sub Coin_Received(Sender As Object, e As CoinReciever.CoinEvent)

        Dim DR As DataRow = Result.Rows(0)

        Select Case e.Message
            Case "1", "2", "5", "10"
                Recieved = e.Message
                '        DR("amount") = Recieved
                '        DR("status") = True
                '        DR("message") = "success"
                '    Case Else
                '        DR("amount") = 0
                '        DR("status") = False
                '        DR("message") = e.State.ToString
        End Select
        'Coin.Close()
        'Response.Write(SingleRowDataTableToJSON(Result))
        'Response.End() '--------- ออกจาก Wait Loop
    End Sub

End Class