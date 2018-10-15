Imports System.Data
Imports System.Data.SqlClient
Public Class Select_Menu
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL
#Region "ส่วนที่เหมือนกันหมดทุกหน้า"
    Private ReadOnly Property KO_ID As Integer '------------- เอาไว้เรียกใช้ง่ายๆ ----------
        Get
            Try
                Return Request.Cookies("KO_ID").Value
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Private ReadOnly Property LANGUAGE As VDM_BL.UILanguage '------- ต้องเป็น ReadOnly --------
        Get
            Try
                Return Session("LANGUAGE")
            Catch ex As Exception
                Return 0
            End Try

        End Get
    End Property

    Private ReadOnly Property TXN_ID As Integer
        Get
            Try
                Return Session("TXN_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsNumeric(Session("LANGUAGE")) Then
            Response.Redirect("Select_Language.aspx")
        End If


        'If Not IsPostBack Then
        '    If IsNothing(Session("Accept_Contact_EMP")) Then        'แสดงครั้งเดียว เคลียร์ Session("Accept_Contact_EMP") หลังจากเข้าหน้า Select_Language
        '        Dim img As New VDM_BL.DialogImage.Accept_Contact_EMP
        '        UC_Dialog.Set_Dialog("หากท่านต้องการ<br/>ใบเสร็จรับเงินฉบับจริง<br/>หรือใบกำกับภาษี", img.Alert_Show, "กรุณาติดต่อพนักงานก่อนทำรายการ", "ดำเนินการ", True)
        '        Session("Accept_Contact_EMP") = True
        '    End If
        'End If


        'Dim img As New VDM_BL.DialogImage.ID_Card
        'UC_Dialog.Set_Dialog("ท่านเสียบบัตรประชาชนผิดด้าน", img.Alert_Repeat, "กรุณาเสียบบัตรใหม่", "ตกลง", True)   'popup2.html
        'UC_Dialog.Set_Dialog("ไม่สามารถอ่านค่าของบัตรประชาชนได้", img.Alert_Warning, "กรุณาเสียบบัตรใหม่", "ตกลง", True) 'popup3.html
        'UC_Dialog.Set_Dialog("ท่านเสียบบัตรประชาชนผิดด้าน", img.Alert_Error, "กรุณาเสียบบัตรใหม่", "ตกลง", True) 'popup4.html
        'UC_Dialog.Set_Dialog("ขออภัย", img.Alert_Error, "ท่านมีอายุไม่ถึงเกณฑ์<br/>ที่จะเปิดเบอร์แบบรายเดือนได้", "ตกลง", True) 'popup4.html
        'UC_Dialog.Set_Dialog("ขออภัย", img.Alert_Error, "ท่านมีอายุไม่ถึงเกณฑ์<br/>ที่จะเปิดเบอร์แบบรายเดือนได้", "ตกลง", True) 'popup5.html

        'Dim img As New VDM_BL.DialogImage.Face
        'UC_Dialog.Set_Dialog("ขออภัย ระบบไม่สามารถอ่านใบหน้าได้", img.Alert_Warning, "กรุณาสแกนใบหน้าอีกครั้ง", "ตกลง", True)   'popup6.html

        'Dim img As New VDM_BL.DialogImage
        'UC_Dialog.Set_Dialog("ขออภัย ขณะนี้เงินทอนของตู้หมด", img.ModeCoin, "กรุณาติดต่อพนักงาน", "ตกลง", True)   'popup7.html

        'Dim img As New VDM_BL.DialogImage.Cash
        'UC_Dialog.Set_Dialog("ขออภัย ขณะนี้ช่องรับเงินสดเต็ม", img.Alert_Error, "กรุณาชำระด้วยวิธีอื่น<br/>หรือติดต่อพนักงาน", "ตกลง", True)   'popup8.html

        'Dim img As New VDM_BL.DialogImage.Credit_Card
        'UC_Dialog.Set_Dialog("ไม่สามารถทำรายการได้<br/>เนื่องจากบัตรเครดิตของท่านหมดอายุ", img.Alert_Warning, "กรุณาเปลี่ยนบัตรใหม่", "ตกลง", True)   'popup9.html

        'Dim img As New VDM_BL.DialogImage.TrueMoney
        'UC_Dialog.Set_Dialog("ท่านไม่สามารถชำระค่าบริการ<br/>ผ่านช่องทางนี้ได้", img.Alert_Warning, "กรุณาติดต่อพนักงาน", "ตกลง", True)   'popup10.html

    End Sub

    Private Sub lnkBack_Click(sender As Object, e As ImageClickEventArgs) Handles lnkBack.Click
        Response.Redirect("Select_Language.aspx")
    End Sub

    Private Sub lnkDevice_Click(sender As Object, e As EventArgs) Handles lnkDevice.Click
        Response.Redirect("Device_Brand.aspx")
    End Sub

    Private Sub lnkSim_Click(sender As Object, e As EventArgs) Handles lnkSim.Click
        Response.Redirect("SIM_List.aspx")
    End Sub


End Class