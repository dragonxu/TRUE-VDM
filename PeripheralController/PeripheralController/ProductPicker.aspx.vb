﻿Imports Controller

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

                Threading.Thread.Sleep(200)

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

        Response.Write(BL.Product_Picker_IP & "<br>" & vbLf)
        Response.Write(BL.Product_Picker_Port & "<br>" & vbLf)
        Controller.SetIP(BL.Product_Picker_IP, BL.Product_Picker_Port)
        Controller.Connect()
        Response.Write("Connected<br>" & vbLf)
        Controller.CloseGate()
        Response.Write("CloseGate<br>" & vbLf)

        Select Case Mode.ToUpper
            Case "SetHome".ToUpper
                Response.Write("BeforeSetHome<br>" & vbLf)
                SetHome()
                Response.Write("AfterSetHome<br>" & vbLf)
            Case "CloseGate".ToUpper
                CloseGate()
            Case "OpenGate".ToUpper
                OpenGate()
            Case "MoveTo".ToUpper
                MoveTo()
            Case "GoPick".ToUpper
                GoPick()
        End Select

    End Sub

    Private Sub SetHome()
        Dim Result As Boolean = Controller.HomePosition()
        Response.Write("SetHome_Result : " & Result & "<br>" & vbLf)
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
        Response.End()
    End Sub

End Class