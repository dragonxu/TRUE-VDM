Imports SimDispenser

Public Class SIMPicker
    Inherits System.Web.UI.Page

    Dim BL As New Core_BL

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
                Return CInt(Request.QueryString("TimeOut")) * 1000
            Else
                Return 10000
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
        Select Case SLOT_ID
            Case 1
                SIMPicker.Dispenser1(TimeOut)
            Case 2
                SIMPicker.Dispenser2(TimeOut)
            Case 3
                SIMPicker.Dispenser3(TimeOut)
            Case 4
                SIMPicker.Dispenser4(TimeOut)
            Case 5
                SIMPicker.Dispenser5(TimeOut)
        End Select
    End Sub

    Private Sub Back()
        SIMPicker.RotateBack()
    End Sub

    Private Sub Forward()
        SIMPicker.RotateForward()
    End Sub

    Private Sub callBack(ByVal Result As Boolean)
        Dim Script As String = callBackFunction & "('" & Result.ToString.ToLower & "');"
        Response.Write(Script)
        Response.End()
    End Sub

End Class