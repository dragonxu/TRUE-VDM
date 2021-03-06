﻿Imports System.Data.SqlClient

Public Class UC_CommonUI
    Inherits System.Web.UI.UserControl

    Dim BL As New VDM_BL

    Public ReadOnly Property KO_ID
        Get

            If Not IsNothing(Request.QueryString("KO_ID")) AndAlso IsNumeric(Request.QueryString("KO_ID")) Then
                Return Request.QueryString("KO_ID")
            ElseIf Not IsNothing(Request.Cookies("KO_ID")) AndAlso IsNumeric(Request.Cookies("KO_ID").Value) Then
                Return Request.Cookies("KO_ID").Value
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property TXN_ID As Integer
        Get
            Try
                Return Session("TXN_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If KO_ID = 0 Then
            Response.Redirect("http://localhost")
            Response.End()
            Exit Sub
        End If

        If TXN_ID > 0 Then
            BL.Update_Service_Transaction(TXN_ID, Me.Page) '-------------- Update ทุกหน้า ------------
        End If
        '---------------- Check Shift Open ----------------
        Dim DT As New DataTable
        Dim SQL As String = "EXEC dbo.SP_CURRENT_OPEN_SHIFT " & KO_ID
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        DA.Fill(DT)
        If DT.Rows.Count = 0 Then
            '------------- Shift ยังไม่เปิด -------------
            Response.Redirect("http://localhost")
            Response.End()
            Exit Sub
        End If

    End Sub

End Class