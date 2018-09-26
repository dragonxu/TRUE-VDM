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

    Public ReadOnly Property SIMDispenser As UC_SIM_Dispenser
        Get
            Return Dispenser
        End Get
    End Property

End Class