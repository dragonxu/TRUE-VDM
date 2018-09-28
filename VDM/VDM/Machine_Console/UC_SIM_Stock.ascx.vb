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
            If IsNothing(Session("VW_ALL_SIM")) Then
                Dim SQL As String = "SELECT * FROM VW_ALL_SIM"
                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                Dim DT As New DataTable
                DA.Fill(DT)
                Session("VW_ALL_SIM") = DT
            End If
            Return Session("VW_ALL_SIM")
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

    Public Property STOCK_DATA As DataTable
        Get
            If IsNothing(Session("STOCK_DATA_" & MY_UNIQUE_ID)) Then
                Dim SQL As String = "SELECT SIM_ID,PRODUCT_CODE,PRODUCT_NAME,SERIAL_NO,SLOT_NAME RECENT,SLOT_NAME [CURRENT]" & vbLf
                SQL &= " FROM VW_CURRENT_SIM_STOCK" & vbLf
                SQL &= "WHERE KO_ID=" & KO_ID
                Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
                Dim DT As New DataTable
                DA.Fill(DT)
                Session("STOCK_DATA_" & MY_UNIQUE_ID) = DT
            End If
            Return Session("STOCK_DATA_" & MY_UNIQUE_ID)
        End Get
        Set(value As DataTable)
            Session("STOCK_DATA_" & MY_UNIQUE_ID) = value
        End Set
    End Property

    Public Property SIM_Height As Integer
        Get
            Return SIMDispenser.SIM_Height
        End Get
        Set(value As Integer)
            SIMDispenser.SIM_Height = value
        End Set
    End Property

    Public ReadOnly Property BarcodeClientID As String
        Get
            Return txtBarcode.ClientID
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            BindData()
        Else
            initFormPlugin()
        End If

    End Sub

    Public Sub BindData()
        'MY_UNIQUE_ID = GenerateNewUniqueID()

        SCAN_SIM_ID = -1
        BindScanSIM()

        '-------- Left Side -------
        BindDispenserLayout()
        ResetSIMSlot()
        BindDispenserSIM()
    End Sub

#Region "CheckButton"

    Const CheckScanCss As String = "btn btn-success btn-icon-icon btn-sm"
    Const CheckSlotCss As String = "btn btn-primary btn-icon-icon btn-sm"
    Const UncheckCss As String = "btn btn-default btn-icon-icon btn-sm"
    Const CheckedText As String = "<i class='icon-check'></i>"
    Const UncheckedText As String = "<i class='icon-close'></i>"

    Private Function IsButtonCheck(ByRef btn As LinkButton) As Boolean
        Return btn.Text = CheckedText
    End Function

    Private Sub SetScanCheck(ByRef btn As LinkButton, ByVal Checked As Boolean)
        If Checked Then
            btn.CssClass = CheckScanCss
            btn.Text = CheckedText
        Else
            btn.CssClass = UncheckCss
            btn.Text = UncheckedText
        End If
    End Sub

    Private Sub SetSlotCheck(ByRef btn As LinkButton, ByVal Checked As Boolean)
        If Checked Then
            btn.CssClass = CheckSlotCss
            btn.Text = CheckedText
        Else
            btn.CssClass = UncheckCss
            btn.Text = UncheckedText
        End If
    End Sub
#End Region

    Private Sub initFormPlugin()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Plugin", "initFormPlugin();", True)
    End Sub

    Private Sub BindDispenserLayout() '------------ เรียกครั้งแรกครั้งเดียว ---------------
        'BL.Bind_Product_Shelf_Layout(Shelf, KO_ID)
        'ConfigShelfLayout()
    End Sub

    Private Sub ConfigDispenserLayout()

    End Sub

    Private Property SCAN_SIM_ID As Integer
        Get

        End Get
        Set(value As Integer)

        End Set
    End Property

#Region "Slot Panel"

    Public Property SLOT_ID As Integer
        Get
            Return lblSlotID.Attributes("SLOT_ID")
        End Get
        Set(value As Integer)
            lblSlotID.Attributes("SLOT_ID") = value
        End Set
    End Property

    Public Property SLOT_NAME As String
        Get
            Return lblSlotID.Text
        End Get
        Set(value As String)
            lblSlotID.Text = value
        End Set
    End Property

    Private Sub ResetSIMSlot()

    End Sub

#End Region

End Class