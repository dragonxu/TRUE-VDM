Imports System.Data.SqlClient

Public Class RenderImage
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL

    Private ReadOnly Property Mode As String
        Get
            Return Request.QueryString("Mode")
        End Get
    End Property

    Private ReadOnly Property UID As String
        Get
            Return Request.QueryString("UID")
        End Get
    End Property

    Private ReadOnly Property Language As VDM_BL.UILanguage
        Get
            Return Request.QueryString("LANG")
        End Get
    End Property

    Private ReadOnly Property Entity As String
        Get
            Return Request.QueryString("Entity")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim ErrorImage As String = "images/BlankIcon.png"
        Try
            Select Case Mode.ToUpper
                Case "S" '---------------- Session ----------------
                    ErrorImage = "images/BlankIcon.png"
                    Response.Clear()
                    Response.BinaryWrite(Session(UID))
                    Response.AddHeader("Content-Type", "image/png")
                Case "D" '--------------- DB ----------------------
                    ErrorImage = "images/BlankIcon.png"
                    Response.Clear()
                    Response.AddHeader("Content-Type", "image/png")
                    Select Case Entity.ToUpper
                        Case "SITE"
                            Dim B As Byte() = BL.Get_Site_Icon(UID)
                            Response.BinaryWrite(B)
                        Case "BRAND"
                            Dim B As Byte() = BL.Get_Brand_Logo(UID)
                            Response.BinaryWrite(B)
                        Case "PRODUCT"
                            Dim B As Byte() = BL.Get_Product_Picture(UID, Language)
                            Response.BinaryWrite(B)
                        Case "SIM_PACKAGE"
                            Dim B As Byte() = BL.Get_SIM_Package_Picture(UID, Language)
                            Response.BinaryWrite(B)
                        Case "SIM_DETAIL"
                            Dim B As Byte() = BL.Get_SIM_Detail_Picture(UID, Language)
                            Response.BinaryWrite(B)
                    End Select
            End Select
        Catch ex As Exception
            Response.Redirect(ErrorImage)
        End Try

    End Sub

End Class