Imports System.Windows.Forms
Public Class FormKeyboard

    Enum CaseMode
        Lower = 1
        Upper = 2
    End Enum

    Private ReadOnly Property CurrentCase As CaseMode
        Get
            Select Case btn_a.Text
                Case "A"
                    Return CaseMode.Upper
                Case Else
                    Return CaseMode.Lower
            End Select
        End Get
    End Property

    Private Sub FormKeyboard_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height
        Me.Width = screenWidth
        Me.Left = 0
        Me.Height = 275
        Me.Top = screenHeight - Me.Height
        TableLayoutPanel1.Width = Me.Width - 20
        TableLayoutPanel2.Width = Me.Width - 20
        TableLayoutPanel3.Width = Me.Width - 20
        TableLayoutPanel4.Width = Me.Width - 20
        TableLayoutPanel5.Width = Me.Width - 20
        '----------- Add Button Handler ----------
        AddAlphaButtonHandler()
    End Sub

    Private Sub AddAlphaButtonHandler()
        '---------------- First Row ------------------
        AddHandler btn_grave.Click, AddressOf btnAlpha_Click
        AddHandler btn_1.Click, AddressOf btnAlpha_Click
        AddHandler btn_2.Click, AddressOf btnAlpha_Click
        AddHandler btn_3.Click, AddressOf btnAlpha_Click
        AddHandler btn_4.Click, AddressOf btnAlpha_Click
        AddHandler btn_5.Click, AddressOf btnAlpha_Click
        AddHandler btn_6.Click, AddressOf btnAlpha_Click
        AddHandler btn_7.Click, AddressOf btnAlpha_Click
        AddHandler btn_8.Click, AddressOf btnAlpha_Click
        AddHandler btn_9.Click, AddressOf btnAlpha_Click
        AddHandler btn_0.Click, AddressOf btnAlpha_Click
        AddHandler btn_minus.Click, AddressOf btnAlpha_Click
        AddHandler btn_equal.Click, AddressOf btnAlpha_Click
        'btn_backspace
        '---------------- Second Row ------------------
        AddHandler btn_tab.Click, AddressOf btnAlpha_Click
        AddHandler btn_q.Click, AddressOf btnAlpha_Click
        AddHandler btn_w.Click, AddressOf btnAlpha_Click
        AddHandler btn_e.Click, AddressOf btnAlpha_Click
        AddHandler btn_r.Click, AddressOf btnAlpha_Click
        AddHandler btn_t.Click, AddressOf btnAlpha_Click
        AddHandler btn_y.Click, AddressOf btnAlpha_Click
        AddHandler btn_u.Click, AddressOf btnAlpha_Click
        AddHandler btn_i.Click, AddressOf btnAlpha_Click
        AddHandler btn_o.Click, AddressOf btnAlpha_Click
        AddHandler btn_p.Click, AddressOf btnAlpha_Click
        AddHandler btn_left_sq.Click, AddressOf btnAlpha_Click
        AddHandler btn_right_sq.Click, AddressOf btnAlpha_Click
        '---------------- Third Row ------------------
        'btn_caplock
        AddHandler btn_a.Click, AddressOf btnAlpha_Click
        AddHandler btn_s.Click, AddressOf btnAlpha_Click
        AddHandler btn_d.Click, AddressOf btnAlpha_Click
        AddHandler btn_f.Click, AddressOf btnAlpha_Click
        AddHandler btn_g.Click, AddressOf btnAlpha_Click
        AddHandler btn_h.Click, AddressOf btnAlpha_Click
        AddHandler btn_j.Click, AddressOf btnAlpha_Click
        AddHandler btn_k.Click, AddressOf btnAlpha_Click
        AddHandler btn_l.Click, AddressOf btnAlpha_Click
        AddHandler btn_colon.Click, AddressOf btnAlpha_Click
        AddHandler btn_quote.Click, AddressOf btnAlpha_Click
        'btn_submit
        '---------------- Forth Row ------------------
        'btn_shift
        AddHandler btn_z.Click, AddressOf btnAlpha_Click
        AddHandler btn_x.Click, AddressOf btnAlpha_Click
        AddHandler btn_c.Click, AddressOf btnAlpha_Click
        AddHandler btn_v.Click, AddressOf btnAlpha_Click
        AddHandler btn_b.Click, AddressOf btnAlpha_Click
        AddHandler btn_n.Click, AddressOf btnAlpha_Click
        AddHandler btn_m.Click, AddressOf btnAlpha_Click
        AddHandler btn_comma.Click, AddressOf btnAlpha_Click
        AddHandler btn_dot.Click, AddressOf btnAlpha_Click
        AddHandler btn_slash.Click, AddressOf btnAlpha_Click
        'btn_keyboard
        '---------------- Fifth Row ------------------
        'btn_space
    End Sub

    Private Sub btnAlpha_Click(sender As Object, e As EventArgs)
        Dim btn As Button = sender
        'FormMain.Controls(0).Focus()

        Dim k As New KeyEvent()
        k.WindowsKeyCode = 0x0D
k.FocusOnEditableField = True
        k.IsSystemKey = False
        k.Type = KeyEventType.Char
        Browser.GetBrowser().GetHost().SendKeyEvent(k)



        FormMain.ChromeBrowser.GetBrowser()
        SendKeys.SendWait(btn.Text)
    End Sub

    Private Sub btn_shift_Click(sender As Object, e As EventArgs) Handles btn_shift.Click, btn_caplock.Click

        Dim ToCase As CaseMode
        If CurrentCase = CaseMode.Lower Then
            ToCase = CaseMode.Upper
        Else
            ToCase = CaseMode.Lower
        End If

        Select Case ToCase
            Case CaseMode.Lower
                '---------------- First Row ------------------
                'btn_grave
                btn_1.Text = "1"
                btn_2.Text = "2"
                btn_3.Text = "3"
                btn_4.Text = "4"
                btn_5.Text = "5"
                btn_6.Text = "6"
                btn_7.Text = "7"
                btn_8.Text = "8"
                btn_9.Text = "9"
                btn_0.Text = "0"
                btn_minus.Text = "-"
                btn_equal.Text = "="
                'btn_backspace
                '---------------- Second Row ------------------
                'btn_tab
                btn_q.Text = "q"
                btn_w.Text = "w"
                btn_e.Text = "e"
                btn_r.Text = "r"
                btn_t.Text = "t"
                btn_y.Text = "y"
                btn_u.Text = "u"
                btn_i.Text = "i"
                btn_o.Text = "o"
                btn_p.Text = "p"
                btn_left_sq.Text = "["
                btn_right_sq.Text = "]"
                '---------------- Third Row ------------------
                'btn_caplock
                btn_a.Text = "a"
                btn_s.Text = "s"
                btn_d.Text = "d"
                btn_f.Text = "f"
                btn_g.Text = "g"
                btn_h.Text = "h"
                btn_j.Text = "j"
                btn_k.Text = "k"
                btn_l.Text = "l"
                btn_colon.Text = ";"
                btn_quote.Text = "'"
                'btn_submit
                '---------------- Forth Row ------------------
                'btn_shift
                btn_z.Text = "z"
                btn_x.Text = "x"
                btn_c.Text = "c"
                btn_v.Text = "v"
                btn_b.Text = "b"
                btn_n.Text = "n"
                btn_m.Text = "m"
                btn_comma.Text = ","
                btn_dot.Text = "."
                btn_slash.Text = "/"
                'btn_keyboard
                '---------------- Fifth Row ------------------
                'btn_space
            Case CaseMode.Upper
                '---------------- First Row ------------------
                'btn_grave
                btn_1.Text = "!"
                btn_2.Text = "@"
                btn_3.Text = "#"
                btn_4.Text = "$"
                btn_5.Text = "%"
                btn_6.Text = "^"
                btn_7.Text = Chr(Asc("&"))
                btn_8.Text = "*"
                btn_9.Text = "("
                btn_0.Text = ")"
                btn_minus.Text = "_"
                btn_equal.Text = "+"
                'btn_backspace
                '---------------- Second Row ------------------
                'btn_tab
                btn_q.Text = "Q"
                btn_w.Text = "W"
                btn_e.Text = "E"
                btn_r.Text = "R"
                btn_t.Text = "T"
                btn_y.Text = "Y"
                btn_u.Text = "U"
                btn_i.Text = "I"
                btn_o.Text = "O"
                btn_p.Text = "P"
                btn_left_sq.Text = "["
                btn_right_sq.Text = "]"
                '---------------- Third Row ------------------
                'btn_caplock
                btn_a.Text = "A"
                btn_s.Text = "S"
                btn_d.Text = "D"
                btn_f.Text = "F"
                btn_g.Text = "G"
                btn_h.Text = "H"
                btn_j.Text = "J"
                btn_k.Text = "K"
                btn_l.Text = "L"
                btn_colon.Text = ":"
                btn_quote.Text = """"
                'btn_submit
                '---------------- Forth Row ------------------
                'btn_shift
                btn_z.Text = "Z"
                btn_x.Text = "X"
                btn_c.Text = "C"
                btn_v.Text = "V"
                btn_b.Text = "B"
                btn_n.Text = "N"
                btn_m.Text = "M"
                btn_comma.Text = "<"
                btn_dot.Text = ">"
                btn_slash.Text = "?"
                'btn_keyboard
                '---------------- Fifth Row ------------------
                'btn_space
        End Select

    End Sub

    Private Sub btn_tab_Click(sender As Object, e As EventArgs) Handles btn_tab.Click
        FormMain.Controls(0).Focus()
        SendKeys.Send("{TAB}")
    End Sub

    Private Sub btn_submit_Click(sender As Object, e As EventArgs) Handles btn_submit.Click
        FormMain.Controls(0).Focus()
        SendKeys.Send("{ENTER}")
    End Sub

    Private Sub btn_backspace_Click(sender As Object, e As EventArgs) Handles btn_backspace.Click
        FormMain.Controls(0).Focus()
        SendKeys.Send("{BACKSPACE}")
    End Sub

    Private Sub btn_space_Click(sender As Object, e As EventArgs) Handles btn_space.Click
        FormMain.Controls(0).Focus()
        SendKeys.Send(" ")
    End Sub

    Private Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click
        FormMain.Controls(0).Focus()
        SendKeys.Send("{DELETE}")
    End Sub
End Class