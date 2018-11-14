Imports Controller

Public Class ProductPicker
    Inherits System.Web.UI.Page

    Dim BL As New Core_BL

    Private ReadOnly Property Controller As Controller.Control
        Get
            If IsNothing(Application("Controller")) Then
                '----------- Init Object And Connect---------
                Dim _control As New Controller.Control
                _control.SetIP(BL.Product_Picker_IP, BL.Product_Picker_Port)
                _control.Connect()

                Threading.Thread.Sleep(100)

                Application.Lock()
                Application("Controller") = _control
                Application.UnLock()

            End If
            Return Application("Controller")
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

    Private ReadOnly Property POS_ID As Integer
        Get
            If IsNumeric(Request.QueryString("POS_ID")) Then
                Return CInt(Request.QueryString("POS_ID"))
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Controller.SetIP(BL.Product_Picker_IP, BL.Product_Picker_Port)
        Try
            Controller.Omron.Disconnect()
        Catch ex As Exception
        End Try
        Try
            Controller.Connect()
        Catch ex As Exception
        End Try



        Select Case Mode.ToUpper
            Case "SetHome".ToUpper
                SetHome()
            Case "CloseGate".ToUpper
                CloseGate()
            Case "OpenGate".ToUpper
                OpenGate()
            Case "MoveTo".ToUpper
                MoveTo()
            Case "GoPick".ToUpper
                GoPick()
        End Select

        Try
            Controller.Omron.Disconnect()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SetHome()
        Dim Result As Boolean = Controller.HomePosition()
        callBack(Result, "")
    End Sub

    Private Sub CloseGate()
        Dim Result As Boolean = Controller.CloseGate()
        callBack(Result, "")
    End Sub

    Private Sub OpenGate()
        Dim Result As Boolean = Controller.BasketPickUp()
        callBack(Result, "")
    End Sub

    Private Sub MoveTo()
        Dim Result As Boolean = Controller.PositionMove(POS_ID)
        callBack(Result, "")
    End Sub

    Private Sub GoPick()
        Dim Result As Boolean = Controller.Process(POS_ID, OpenTimeOut * 1000)
        callBack(Result, "")
    End Sub

    Private Sub callBack(ByVal Result As Boolean, ByVal Message As String)
        Dim Script As String = callBackFunction & "(" & Result.ToString.ToLower & ",'" & Message.Replace("'", "") & "');"
        Response.Write(Script)
    End Sub

End Class