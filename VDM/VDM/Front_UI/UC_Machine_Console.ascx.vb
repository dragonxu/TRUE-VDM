Public Class UC_Machine_Console
    Inherits System.Web.UI.UserControl





    Private ReadOnly Property BRAND_ID As Integer
        Get
            Try
                Return Request.QueryString("BRAND_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property


#Region "PRODUCT"
    Protected ReadOnly Property PRODUCT_ID As Integer
        Get
            Try
                Return Request.QueryString("PRODUCT_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property
#End Region

#Region "SIM"

    Protected ReadOnly Property SIM_ID As Integer
        Get
            Try
                Return Request.QueryString("SIM_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property
    Protected ReadOnly Property D_ID As Integer
        Get
            Try
                Return Request.QueryString("D_ID")
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property


#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Select_Language
        'Select_Menu

        'Device_Brand
        'Product_List
        'Device_Product_Detail

        'SIM_List
        'SIM_Detail

        'Device_Shoping_Cart
        'Device_Verify
        'Device_Payment
        'Complete_Order
        'Thank_You


    End Sub

    Private Sub btnBack()
        '--BACK
        Select Case Page.TemplateControl.ToString
            Case "ASP." + "Select_Language_aspx".ToLower()

            Case "ASP." + "Select_Menu_aspx".ToLower()
                Response.Redirect("Select_Language.aspx")

            Case "ASP." + "Device_Brand_aspx".ToLower()
                Response.Redirect("Select_Menu.aspx")
            Case "ASP." + "Product_List_aspx".ToLower()
                Response.Redirect("Device_Brand.aspx")
            Case "ASP." + "Device_Product_Detail_aspx".ToLower()
                Response.Redirect("Product_List.aspx?BRAND_ID=" & BRAND_ID)

            Case "ASP." + "SIM_List_aspx".ToLower()
                Response.Redirect("Select_Menu.aspx")
            Case "ASP." + "SIM_Detail_aspx".ToLower()
                Response.Redirect("SIM_List.aspx")

            Case "ASP." + "Device_Shoping_Cart_aspx".ToLower()
                If PRODUCT_ID <> 0 Then
                    Response.Redirect("Device_Product_Detail.aspx?PRODUCT_ID=" & PRODUCT_ID)
                Else
                    Response.Redirect("SIM_Detail.aspx?SIM_ID=" & SIM_ID)
                End If
            Case "ASP." + "Device_Verify_aspx".ToLower()

                If PRODUCT_ID <> 0 Then
                    Response.Redirect("Device_Shoping_Cart.aspx?PRODUCT_ID=" & PRODUCT_ID)
                Else
                    Response.Redirect("Device_Shoping_Cart.aspx?SIM_ID=" & SIM_ID)
                End If
            Case "ASP." + "Device_Payment_aspx".ToLower()
                If PRODUCT_ID <> 0 Then
                    Response.Redirect("Device_Shoping_Cart.aspx?PRODUCT_ID=" & PRODUCT_ID)
                Else
                    Response.Redirect("Device_Shoping_Cart.aspx?SIM_ID=" & SIM_ID)
                End If
            Case "ASP." + "Complete_Order_aspx".ToLower()
                If PRODUCT_ID <> 0 Then
                    Response.Redirect("Device_Payment.aspx?PRODUCT_ID=" & PRODUCT_ID)
                Else
                    Response.Redirect("Device_Payment.aspx?SIM_ID=" & SIM_ID)
                End If
            Case "ASP." + "Thank_You_aspx".ToLower()
                Response.Redirect("Select_Language.aspx")
        End Select

    End Sub

    Private Sub btnLeft_Click(sender As Object, e As ImageClickEventArgs) Handles btnLeft.Click, btnRight.Click
        Response.Redirect("../Machine_Console/Login.aspx")
    End Sub
End Class