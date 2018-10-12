Imports System.Data.SqlClient

Public Class PrintBarcode
    Inherits System.Web.UI.Page

    Dim BL As New VDM_BL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'BindProductSerial()
        'BindProdcutNon()
        'BindProductAll()
        BindSIMSerial()
        'BindProductTest()
    End Sub


    Private Sub BindSIMSerial()
        Dim SQL As String = "SELECT 0 SIM_ID,MAT PRODUCT_CODE,SERIAL SERIAL_NO,MSISDN DISPLAY_NAME_TH" & vbLf
        SQL &= " FROM TMP_SIM_TEST SIM" & vbLf
        'SQL &= " INNER JOIN MS_SIM MS ON SIM.PRODUCT_CODE=MS.PRODUCT_CODE"
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        rptSIM.DataSource = DT
        rptSIM.DataBind()
    End Sub

    Private Sub BindProductSerial()
        Dim SQL As String = "SELECT TMP.PRODUCT_CODE" & vbLf
        SQL &= " ,SERIAL_NO,PD.DISPLAY_NAME_TH" & vbLf
        SQL &= " FROM TMP_Serial TMP" & vbLf
        SQL &= " INNER JOIN MS_Product PD ON TMP.Product_Code=PD.Product_Code" & vbLf
        SQL &= " WHERE TMP.PRODUCT_CODE='3000066334'"
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        rptSerial.DataSource = DT
        rptSerial.DataBind()

    End Sub

    Private Sub BindProductTest()
        Dim SQL As String = "SELECT MAT PRODUCT_CODE ,SERIAL SERIAL_NO,'' DISPLAY_NAME_TH" & vbLf
        SQL &= " FROM TMP_PRODUCT_TEST " & vbLf
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        rptSerial.DataSource = DT
        rptSerial.DataBind()

    End Sub

    Private Sub BindProductAll()
        Dim SQL As String = "SELECT PRODUCT_CODE" & vbLf
        SQL &= " ,DISPLAY_NAME_TH" & vbLf
        SQL &= " FROM MS_Product" & vbLf
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        rptNon.DataSource = DT
        rptNon.DataBind()

    End Sub


    Private Sub BindDataNon()
        Dim SQL As String = "SELECT PRODUCT_CODE" & vbLf
        SQL &= " ,DISPLAY_NAME_TH" & vbLf
        SQL &= " FROM MS_Product" & vbLf
        SQL &= " WHERE ISNULL(IS_SERIAL,0)=0" & vbLf
        Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        rptNon.DataSource = DT
        rptNon.DataBind()

    End Sub

    Private Sub rptNon_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptNon.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim lblCode As Label = e.Item.FindControl("lblCode")
        Dim lblName As Label = e.Item.FindControl("lblName")
        Dim img As Image = e.Item.FindControl("img")

        lblCode.Text = e.Item.DataItem("PRODUCT_CODE").ToString
        lblName.Text = e.Item.DataItem("DISPLAY_NAME_TH").ToString

        img.ImageUrl = "http://generator.barcodetools.com/barcode.png?gen=0&data=" & e.Item.DataItem("PRODUCT_CODE").ToString & "&bcolor=FFFFFF&fcolor=000000&tcolor=000000&fh=14&bred=0&w2n=2.5&xdim=2&w=200&h=100&debug=1&btype=7&angle=0&quiet=1&balign=2&talign=0&guarg=1&text=1&tdown=1&stst=1&schk=0&cchk=1&ntxt=1&c128=0"

    End Sub

    Private Sub rptSerial_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptSerial.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub
        Dim lblCode As Label = e.Item.FindControl("lblCode")
        Dim lblName As Label = e.Item.FindControl("lblName")
        Dim lblSerial As Label = e.Item.FindControl("lblSerial")
        Dim img As Image = e.Item.FindControl("img")

        lblCode.Text = e.Item.DataItem("PRODUCT_CODE").ToString
        lblName.Text = e.Item.DataItem("DISPLAY_NAME_TH").ToString
        lblSerial.Text = e.Item.DataItem("SERIAL_NO").ToString

        img.ImageUrl = "http://generator.barcodetools.com/barcode.png?gen=0&data=" & e.Item.DataItem("SERIAL_NO").ToString & "&bcolor=FFFFFF&fcolor=000000&tcolor=000000&fh=14&bred=0&w2n=2.5&xdim=2&w=200&h=100&debug=1&btype=7&angle=0&quiet=1&balign=2&talign=0&guarg=1&text=1&tdown=1&stst=1&schk=0&cchk=1&ntxt=1&c128=0"

    End Sub

    Private Sub rptSIM_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptSIM.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub
        Dim lblCode As Label = e.Item.FindControl("lblCode")
        Dim lblName As Label = e.Item.FindControl("lblName")
        Dim lblSerial As Label = e.Item.FindControl("lblSerial")
        Dim img As Image = e.Item.FindControl("img")

        lblCode.Text = e.Item.DataItem("PRODUCT_CODE").ToString
        lblName.Text = e.Item.DataItem("DISPLAY_NAME_TH").ToString
        lblSerial.Text = e.Item.DataItem("SERIAL_NO").ToString

        img.ImageUrl = "http://generator.barcodetools.com/barcode.png?gen=0&data=" & e.Item.DataItem("SERIAL_NO").ToString & "&bcolor=FFFFFF&fcolor=000000&tcolor=000000&fh=14&bred=0&w2n=2.5&xdim=2&w=200&h=100&debug=1&btype=7&angle=0&quiet=1&balign=2&talign=0&guarg=1&text=1&tdown=1&stst=1&schk=0&cchk=1&ntxt=1&c128=0"

    End Sub
End Class