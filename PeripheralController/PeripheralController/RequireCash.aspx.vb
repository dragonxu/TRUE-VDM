Imports CashReceiver
Imports CoinReciever
Imports System.Data

Public Class RequireCash
    Inherits System.Web.UI.Page

    Dim BL As New Core_BL

    Private ReadOnly Property CashReceiver As CashReceiver.CashReceiver
        Get
            If IsNothing(Application("CashReceiver")) Then
                '----------- Init Object And Connect---------
                Dim _cash As New CashReceiver.CashReceiver
                _cash.SetPort(BL.CashReciever_Port)

                Try
                    _cash.Close()
                Catch ex As Exception
                End Try

                Threading.Thread.Sleep(100)

                Application.Lock()
                Application("CashReceiver") = _cash
                Application.UnLock()

            End If
            Return Application("CashReceiver")
        End Get
    End Property

    Private ReadOnly Property CoinReceiver As CoinReciever.CoinReciever
        Get
            If IsNothing(Application("CoinReceiver")) Then
                '----------- Init Object And Connect---------
                Dim _coin As New CoinReciever.CoinReciever
                _coin.SetPort(BL.CoinReciever_Port)

                Try
                    _coin.Close()
                Catch ex As Exception
                End Try

                Threading.Thread.Sleep(100)

                Application.Lock()
                Application("CoinReceiver") = _coin
                Application.UnLock()

            End If
            Return Application("CoinReceiver")
        End Get
    End Property

    Dim Result As DataTable

    Dim Recieved As Integer = 0
    Dim StartWait As DateTime = Now
    Dim EndWait As DateTime = DateAdd(DateInterval.Minute, 2, Now)

    Private ReadOnly Property Required As Integer
        Get
            If IsNumeric(Request.QueryString("REQ")) Then
                Return Request.QueryString("REQ")
            Else
                Return 0
            End If
        End Get
    End Property

    Private ReadOnly Property callBackFunction As String
        Get
            If Not IsNothing(Request.QueryString("callback")) Then
                Return Request.QueryString("callback")
            Else
                Return ""
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Result = New DataTable
        Result.Columns.Add("amount")
        Result.Columns.Add("status")
        Result.Columns.Add("message")

        '--------------------- รอ Test-------------
        If Required > 0 Then
            WaitForRecieve()
        Else
            Dim DR As DataRow = Result.NewRow
            Result.Rows.Add(DR)
            DR("amount") = 0
            DR("status") = "false"
            DR("message") = "no requirement"
            callBack()
        End If

    End Sub

    Public Sub WaitForRecieve()

        Dim DR As DataRow = Result.NewRow
        DR("amount") = 0
        DR("status") = "false"
        DR("message") = ""
        Result.Rows.Add(DR)

        Try
            CashReceiver.Close()
            CashReceiver.Open()
        Catch : End Try

        Try
            CoinReceiver.Close()
            CoinReceiver.Open()
        Catch : End Try

        '---------------- Check Cash Status---------------
        Try
            Select Case CashReceiver.CurrentStatus
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
            callBack()
            Exit Sub
        End If

        '---------------- Check Coin Status---------------
        Try
            Select Case CoinReceiver.CurrentStatus
                Case CoinState.Unknown
                    DR("message") = "Coin Reciever is Unknown"
                    callBack()
                    Exit Sub
                Case CoinState.Unavailable
                    DR("message") = "Coin Reciever is Unavailable"
                    callBack()
                    Exit Sub
                Case CoinState.Sensor_1_problem
                    DR("message") = "Coin Reciever is Sensor_1_problem"
                    callBack()
                    Exit Sub
                Case CoinState.Sensor_2_problem
                    DR("message") = "Coin Reciever is Sensor_2_problem"
                    callBack()
                    Exit Sub
                Case CoinState.Sensor_3_problem
                    DR("message") = "Coin Reciever is Sensor_3_problem"
                    callBack()
                    Exit Sub
                Case CoinState.Disconnected
                    DR("message") = "Coin Reciever is Disconnected"
                    callBack()
                    Exit Sub
                Case CoinState.Ready
                    DR("message") = "" '---------------OK ------------------
            End Select
        Catch ex As Exception
            DR("message") = ex.Message
            callBack()
            Exit Sub
        End Try

        AddHandler CashReceiver.Received, AddressOf Cash_Received
        AddHandler CoinReceiver.Recieved, AddressOf Coin_Received

        '------------- รอจนกว่าจะหยอด
        While Recieved = 0 And Now < EndWait
            Threading.Thread.Sleep(200)
        End While
        '------------- ไม่หยอดสักที -------------
        Try : CashReceiver.Close() : Catch : End Try
        Try : CoinReceiver.Close() : Catch : End Try

        DR("amount") = Recieved
        DR("status") = (Recieved > 0).ToString.ToLower
        If Recieved = 0 Then
            DR("message") = "timeout"
        Else
            DR("message") = "success"
        End If
        callBack()
    End Sub

    Public Sub Cash_Received(Sender As Object, e As CashReceiver.CashEvent)
        Select Case e.Message
            Case "20", "50", "100", "500", "1000"
                Recieved = e.Message
        End Select
    End Sub

    Public Sub Coin_Received(Sender As Object, e As CoinReciever.CoinEvent)
        Select Case e.Message
            Case "1", "2", "5", "10"
                Recieved = e.Message
        End Select
    End Sub

    Private Sub callBack()
        Try
            CashReceiver.Close()
        Catch : End Try
        Try
            CoinReceiver.Close()
        Catch : End Try


        Dim Script As String = callBackFunction & "('" & Result.Rows(0).Item("amount") & "','" & Result.Rows(0).Item("status") & "','" & Result.Rows(0).Item("message") & "');"
        Response.Write(Script)
        Response.End()
    End Sub

End Class