Imports System.Windows.Forms
Public Class FormKeyboard

    Public Property MainForm As FormMain

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

        Me.DoubleBuffered = True

        Me.BackColor = Color.White
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

    Dim KeyEvent As String = ""
    Private Sub AddAlphaButtonHandler()

        '------------------------- MouseDown ----------------------
        '-------- First Row ----------
        AddHandler btn_grave.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_1.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_2.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_3.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_4.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_5.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_6.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_7.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_8.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_9.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_0.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_minus.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_equal.MouseDown, AddressOf btn_alpha_MouseDown
        '-------- Second Row ----------
        AddHandler btn_tab.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_q.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_w.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_e.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_r.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_t.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_y.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_u.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_i.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_o.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_p.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_left_sq.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_right_sq.MouseDown, AddressOf btn_alpha_MouseDown
        '-------- Third Row ----------
        AddHandler btn_a.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_s.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_d.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_f.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_g.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_h.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_j.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_k.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_l.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_colon.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_quote.MouseDown, AddressOf btn_alpha_MouseDown
        '-------- Forth Row ----------
        AddHandler btn_z.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_x.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_c.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_v.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_b.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_n.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_m.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_comma.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_dot.MouseDown, AddressOf btn_alpha_MouseDown
        AddHandler btn_slash.MouseDown, AddressOf btn_alpha_MouseDown
        '---------------- CtrlButton ------------------
        AddHandler btn_tab.MouseDown, AddressOf btn_ctrl_MouseDown
        AddHandler btn_submit.MouseDown, AddressOf btn_ctrl_MouseDown
        AddHandler btn_backspace.MouseDown, AddressOf btn_ctrl_MouseDown
        AddHandler btn_space.MouseDown, AddressOf btn_ctrl_MouseDown
        AddHandler btn_delete.MouseDown, AddressOf btn_ctrl_MouseDown


        '------------------------- MouseUp ----------------------
        '-------- First Row ----------
        AddHandler btn_grave.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_1.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_2.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_3.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_4.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_5.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_6.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_7.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_8.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_9.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_0.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_minus.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_equal.MouseUp, AddressOf btn_MouseUp
        '-------- Second Row ----------
        AddHandler btn_tab.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_q.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_w.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_e.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_r.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_t.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_y.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_u.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_i.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_o.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_p.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_left_sq.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_right_sq.MouseUp, AddressOf btn_MouseUp
        '-------- Third Row ----------
        AddHandler btn_a.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_s.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_d.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_f.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_g.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_h.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_j.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_k.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_l.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_colon.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_quote.MouseUp, AddressOf btn_MouseUp
        '-------- Forth Row ----------
        AddHandler btn_z.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_x.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_c.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_v.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_b.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_n.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_m.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_comma.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_dot.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_slash.MouseUp, AddressOf btn_MouseUp
        '---------------- CtrlButton ------------------
        AddHandler btn_tab.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_submit.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_backspace.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_space.MouseUp, AddressOf btn_MouseUp
        AddHandler btn_delete.MouseUp, AddressOf btn_MouseUp


    End Sub

    Private Sub btn_MouseUp(sender As Object, e As MouseEventArgs)
        SendKeys.SendWait(KeyEvent)
    End Sub

    Private Sub btn_alpha_MouseDown(sender As Object, e As MouseEventArgs)
        Dim btn As Button = sender
        KeyEvent = btn.Text
        MainForm.Focus()
    End Sub

    Private Sub btn_ctrl_MouseDown(sender As Object, e As MouseEventArgs)
        Dim btn As Button = sender
        KeyEvent = btn.Tag
        MainForm.Focus()
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


End Class