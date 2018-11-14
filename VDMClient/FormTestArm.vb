Public Class FormTestArm

    Dim Controller As New Controller.Control

    Private Sub FormTestArm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Controller = New Controller.Control
        Controller.SetIP("192.168.250.10", 9600)
        Try
            Controller.Connect()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FormMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            Controller.Omron.Disconnect()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnPick_Click(sender As Object, e As EventArgs) Handles btnPick.Click
        Try
            MsgBox(Controller.Process(CInt(txtPOS.Text), 20000))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        MsgBox(Controller.HomePosition())
    End Sub

    Private Sub btnOpenGate_Click(sender As Object, e As EventArgs) Handles btnOpenGate.Click
        MsgBox(Controller.BasketPickUp())
    End Sub

    Private Sub btnCloseGate_Click(sender As Object, e As EventArgs) Handles btnCloseGate.Click
        MsgBox(Controller.CloseGate())
    End Sub
End Class