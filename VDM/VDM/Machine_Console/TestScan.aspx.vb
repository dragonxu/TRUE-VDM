Imports System.Data
Imports System.Data.SqlClient

Public Class TestScan
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL

    Public Property KO_ID As Integer
        Get
            Return pnlScanProduct.Attributes("KO_ID")
        End Get
        Set(value As Integer)
            pnlScanProduct.Attributes("KO_ID") = value
        End Set
    End Property

    Public Property PixelPerMM As Double
        Get
            Return Shelf.PixelPerMM
        End Get
        Set(value As Double)
            Shelf.PixelPerMM = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            KO_ID = 1 '--------------- For Test ---------------
            BindShelf()
        Else
            initFormPlugin()
        End If

    End Sub

    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Private Sub BindShelf()
        PixelPerMM = 0.25
        BL.Bind_Product_Shelf(Shelf, KO_ID)

        '-------------- Configure ------------
        Shelf.ShowAddFloor = False
        Shelf.ShowEditShelf = False
        Shelf.HideFloorName()
        Shelf.HideFloorMenu()

        '------------ Hide All Scale----------
        For i As Integer = 0 To Shelf.Slots.Count - 1
            Shelf.Slots(i).ShowScale = False
        Next
        For i As Integer = 0 To Shelf.Floors.Count - 1
            Shelf.Floors(i).ShowScale = False
        Next
        Shelf.ShowScale = False

    End Sub

End Class