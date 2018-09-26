Imports System.Data
Imports System.Data.SqlClient
Imports VDM

Public Class UC_SIM_Stock
    Inherits System.Web.UI.UserControl

    Dim BL As New VDM_BL

    Public Property KO_ID As Integer
        Get
            Return btnSeeShelf.Attributes("KO_ID")
        End Get
        Set(value As Integer)
            btnSeeShelf.Attributes("KO_ID") = value
        End Set
    End Property

    Public Property SHOP_CODE As String
        Get
            Return btnSeeShelf.Attributes("SHOP_CODE")
        End Get
        Set(value As String)
            btnSeeShelf.Attributes("SHOP_CODE") = value
        End Set
    End Property

    Public Property SHIFT_ID As Integer
        Get
            Return btnSeeShelf.Attributes("SHIFT_ID")
        End Get
        Set(value As Integer)
            btnSeeShelf.Attributes("SHIFT_ID") = value
        End Set
    End Property

    Public Property SHIFT_STATUS As VDM_BL.ShiftStatus
        Get
            Return btnSeeShelf.Attributes("SHIFT_STATUS")
        End Get
        Set(value As VDM_BL.ShiftStatus)
            btnSeeShelf.Attributes("SHIFT_STATUS") = value
        End Set
    End Property

    Public ReadOnly Property SLOT_CAPACITY As Integer
        Get
            Return Dispenser.SLOT_CAPACITY
        End Get
    End Property

    Public ReadOnly Property SIMDispenser As UC_SIM_Dispenser
        Get
            Return Dispenser
        End Get
    End Property

    Public ReadOnly Property VW_ALL_SIM As DataTable
        Get
            If IsNothing(Session("VW_ALL_SIM" & MY_UNIQUE_ID)) Then
                Dim SQL As String = "SELECT * FROM VW_ALL_SIM"
                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                Dim DT As New DataTable
                DA.Fill(DT)
                Session("VW_ALL_SIM_" & MY_UNIQUE_ID) = DT
            End If
            Return Session("VW_ALL_SIM_" & MY_UNIQUE_ID)
        End Get
    End Property

    Private ReadOnly Property MY_UNIQUE_ID() As String
        Get
            If btnSeeShelf.Attributes("MY_UNIQUE_ID") = "" Then
                btnSeeShelf.Attributes("MY_UNIQUE_ID") = GenerateNewUniqueID() '---- Needed ---------
            End If
            Return btnSeeShelf.Attributes("MY_UNIQUE_ID")
        End Get
    End Property

    'Public Property STOCK_DATA As DataTable
    '    Get
    '        If IsNothing(Session("STOCK_DATA_" & MY_UNIQUE_ID)) Then
    '            Dim SQL As String = "SELECT PRODUCT_ID,PRODUCT_CODE,PRODUCT_NAME,SERIAL_NO,SLOT_NAME RECENT,SLOT_NAME [CURRENT]" & vbLf
    '            SQL &= "FROM VW_CURRENT_PRODUCT_STOCK" & vbLf
    '            SQL &= "WHERE KO_ID=" & KO_ID
    '            Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
    '            Dim DT As New DataTable
    '            DA.Fill(DT)
    '            Session("STOCK_DATA_" & MY_UNIQUE_ID) = DT
    '        End If
    '        Return Session("STOCK_DATA_" & MY_UNIQUE_ID)
    '    End Get
    '    Set(value As DataTable)
    '        Session("STOCK_DATA_" & MY_UNIQUE_ID) = value
    '    End Set
    'End Property

End Class