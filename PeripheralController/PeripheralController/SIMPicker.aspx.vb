Imports SimDispenser

Public Class SIMPicker
    Inherits System.Web.UI.Page

    Dim BL As New Core_BL


    'Private ReadOnly Property Controller As Controller.Control
    '    Get
    '        If IsNothing(Application("Controller")) Then
    '            '----------- Init Object And Connect---------
    '            Dim _control As New Controller.Control
    '            _control.SetIP(BL.Product_Picker_IP, BL.Product_Picker_Port)
    '            _control.Connect()

    '            Threading.Thread.Sleep(200)

    '            Application.Lock()
    '            Application("Controller") = _control
    '            Application.UnLock()

    '        End If
    '        Return Application("Controller")
    '    End Get
    'End Property

    Private ReadOnly Property SIMPicker As SimDispenser.SimDispenser
        Get
            If IsNothing(Application("SIMPicker")) Then
                '----------- Init Object And Connect---------
                Dim _slot As New SimDispenser.SimDispenser
                _slot.SetPort(BL.SIM_Dispenser_Port)

                Application.Lock()
                Application("SIMPicker") = _slot
                Application.UnLock()

            End If
            Return Application("SIMPicker")
        End Get
    End Property

    Private ReadOnly Property Mode As String
        Get
            If Not IsNothing(Request.QueryString("Mode")) Then
                Return Request.QueryString("Mode").ToString
            Else
                Return ""
            End If
        End Get
    End Property

    Private ReadOnly Property SLOT_ID As Integer
        Get
            If IsNumeric(Request.QueryString("SLOT_ID")) Then
                Return CInt(Request.QueryString("SLOT_ID"))
            Else
                Return 0
            End If
        End Get
    End Property

    Private ReadOnly Property OpenTimeOut As Integer
        Get
            If IsNumeric(Request.QueryString("OpenTimeOut")) Then
                Return CInt(Request.QueryString("OpenTimeOut"))
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

    Private ReadOnly Property TimeOut As Integer
        Get
            If IsNumeric(Request.QueryString("TimeOut")) Then
                Return Request.QueryString("TimeOut")
            Else
                Return 15
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Select Case Mode.ToUpper
            Case "Break".ToUpper
                Break()
            Case "Pull".ToUpper
                Pull()
            Case "Back".ToUpper
                Back()
            Case "Forward".ToUpper
                Forward()
        End Select
    End Sub

    Private Sub Break()
        callBack(SIMPicker.Break())
    End Sub

    Private Sub Pull()
        Dim Result As Boolean = False
        Select Case SLOT_ID
            Case 1
                Result = SIMPicker.Dispenser1(TimeOut * 1000)
            Case 2
                Result = SIMPicker.Dispenser2(TimeOut * 1000)
            Case 3
                Result = SIMPicker.Dispenser3(TimeOut * 1000)
            Case 4
                Result = SIMPicker.Dispenser4(TimeOut * 1000)
            Case 5
                Result = SIMPicker.Dispenser5(TimeOut * 1000)
        End Select
        callBack(Result)
    End Sub

    Private Sub Back()
        Dim EndWait As DateTime = DateAdd(DateInterval.Second, TimeOut, Now)
        SIMPicker.RotateBack()
        While Now < EndWait
            Threading.Thread.Sleep(200)
        End While
        SIMPicker.Break()
        'callBack(True)
    End Sub

    Private Sub Forward()
        'Dim EndWait As DateTime = DateAdd(DateInterval.Second, TimeOut, Now)
        'SIMPicker.RotateForward()
        'While Now < EndWait
        '    Threading.Thread.Sleep(200)
        'End While
        'SIMPicker.Break()
        'Controller.BasketPickUp() '-----------OpenDoor

        'EndWait = DateAdd(DateInterval.Second, OpenTimeOut, Now)
        'While Now < EndWait
        '    Threading.Thread.Sleep(200)
        'End While

        'Controller.CloseGate() '-----------CloseDoor
        'callBack(True)
    End Sub

    Private Sub callBack(ByVal Result As Boolean)
        Dim Script As String = callBackFunction & "('" & Result.ToString.ToLower & "');"
        Response.Write(Script)
        Response.End()
    End Sub

End Class