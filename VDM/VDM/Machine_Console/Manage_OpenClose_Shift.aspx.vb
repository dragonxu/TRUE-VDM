Imports System.Data.SqlClient

Public Class Manage_OpenClose_Shift
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not IsNumeric(Session("USER_ID")) Then
        '    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('กรุณาเข้าสู่ระบบ'); window.location.href='Login.aspx';", True)
        '    Exit Sub
        'End If

        If Not IsPostBack Then
            ClearMenu()
        Else

            pnlbtn.Visible = True
        End If
    End Sub

    Private Sub ClearMenu()
        MenuChange.Attributes("class") = ""
        MenuRecieve.Attributes("class") = ""
        MenuStockProduct.Attributes("class") = ""
        MenuStockSIM.Attributes("class") = ""
        MenuStockPaper.Attributes("class") = ""

        '--เงินทอน
        pnlChange.Visible = False
        'เงินรับ
        pnlRecieve.Visible = False
        'Stock สินค้า
        pnlStockProduct.Visible = False
        'Stock Sim
        pnlStockSIM.Visible = False
        'Stock พิมพ์
        pnlStockPaper.Visible = False
        pnlbtn.Visible = False

    End Sub
    Private Sub lnkChange_ServerClick(sender As Object, e As EventArgs) Handles lnkChange.ServerClick
        ClearMenu()
        MenuChange.Attributes("class") = "active"
        pnlChange.Visible = True
        pnlbtn.Visible = True

    End Sub

    Private Sub lnkRecieve_ServerClick(sender As Object, e As EventArgs) Handles lnkRecieve.ServerClick
        ClearMenu()
        MenuRecieve.Attributes("class") = "active"
        pnlRecieve.Visible = True
        pnlbtn.Visible = True


    End Sub

    Private Sub lnkStockProduct_ServerClick(sender As Object, e As EventArgs) Handles lnkStockProduct.ServerClick
        ClearMenu()
        MenuStockProduct.Attributes("class") = "active"
        pnlStockProduct.Visible = True
        pnlbtn.Visible = True



    End Sub

    Private Sub lnkStockSIM_ServerClick(sender As Object, e As EventArgs) Handles lnkStockSIM.ServerClick
        ClearMenu()
        MenuStockSIM.Attributes("class") = "active"
        pnlStockSIM.Visible = True
        pnlbtn.Visible = True



    End Sub


    Private Sub lnkStockPaper_ServerClick(sender As Object, e As EventArgs) Handles lnkStockPaper.ServerClick
        ClearMenu()
        MenuStockPaper.Attributes("class") = "active"
        pnlStockPaper.Visible = True
        pnlbtn.Visible = True


    End Sub


End Class