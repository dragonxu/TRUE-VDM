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
            Return Request.QueryString("Mode")
        End Get
    End Property

    Private ReadOnly Property POS_ID As Integer
        Get
            Try
                Return Request.QueryString("POS_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Private ReadOnly Property OpenTimeOut As Integer
        Get
            Try
                Return Request.QueryString("OpenTimeOut")
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Private ReadOnly Property callBackFunction As String
        Get
            Return Request.QueryString("callback")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Select Case Mode.ToUpper
            Case "SetHome".ToUpper
                SetHome()
            Case "GoPick".ToUpper
                GoPick()
        End Select
    End Sub

    Private Sub SetHome()
        callBack(Controller.HomePosition())
    End Sub

    Private Sub GoPick()
        callBack(Controller.Process(POS_ID, OpenTimeOut * 1000))
    End Sub

    Private Sub callBack(ByVal Result As Boolean)
        Dim Script As String = callBackFunction & "(" & Result.ToString.ToLower & ");"
        Response.Write(Script)
        Response.End()
    End Sub

End Class