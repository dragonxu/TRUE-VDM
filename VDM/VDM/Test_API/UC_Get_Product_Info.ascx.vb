Public Class UC_Get_Product_Info
    Inherits System.Web.UI.UserControl

    Dim BackEndInterface As New BackEndInterface.Get_Product_Info

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btn_Request_Click(sender As Object, e As EventArgs) Handles btn_Request.Click
        Try


            Dim Response As New BackEndInterface.Get_Product_Info.Response
            Response = BackEndInterface.Get_Result(productCode.Text)
            If Not IsNothing(Response) Then

                Price.Text = Response.Price

                'Product
<<<<<<< HEAD
                CODE.Text = Response.Product.CODE
                DESCRIPTION.Text = Response.Product.DESCRIPTION
                PLANT_CODE.Text = Response.Product.PLANT_CODE
                PRODUCT_BRAND_CODE.Text = Response.Product.PRODUCT_BRAND_CODE
                PRODUCT_MODEL_CODE.Text = Response.Product.PRODUCT_MODEL_CODE
                IS_SERIAL.Text = Response.Product.IS_SERIAL
                PRODUCT_TYPE.Text = Response.Product.PRODUCT_TYPE
                DISPLAY_NAME.Text = Response.Product.DISPLAY_NAME
                IS_ENABLE.Text = Response.Product.IS_ENABLE
                IS_DELETE.Text = Response.Product.IS_DELETE
                CATEGORY.Text = Response.Product.CATEGORY
                REQ_SALE_APPROACH.Text = Response.Product.REQ_SALE_APPROACH
                IS_VAT.Text = Response.Product.IS_VAT
                VAT_RATE.Text = Response.Product.VAT_RATE
                COMPANY_CODE.Text = Response.Product.COMPANY_CODE
                IS_SIM.Text = Response.Product.IS_SIM
                REQUIRE_RECEIVE_FORM.Text = Response.Product.REQUIRE_RECEIVE_FORM
                APPLE_CARE_SERVICE_CODE.Text = Response.Product.APPLE_CARE_SERVICE_CODE
                COLOR.Text = Response.Product.COLOR
                CAPACITY.Text = Response.Product.CAPACITY
                CATEGORY_RECOMMEND.Text = Response.Product.CATEGORY_RECOMMEND

                ''Captions
                If Response.Captions.Count > 0 Then
                    SEQ.Text = Response.Captions(0).SEQ
                    PRODUCT_CODE.Text = Response.Captions(0).PRODUCT_CODE
                    CAPTION_CODE.Text = Response.Captions(0).CAPTION_CODE
                    CAPTION_DESC.Text = Response.Captions(0).CAPTION_DESC
                    DETAIL.Text = Response.Captions(0).DETAIL
                End If

=======
                CODE.Text = Response.response_data.Product.CODE
                DESCRIPTION.Text = Response.response_data.Product.DESCRIPTION
                PLANT_CODE.Text = Response.response_data.Product.PLANT_CODE
                PRODUCT_BRAND_CODE.Text = Response.response_data.Product.PRODUCT_BRAND_CODE
                PRODUCT_MODEL_CODE.Text = Response.response_data.Product.PRODUCT_MODEL_CODE
                IS_SERIAL.Text = Response.response_data.Product.IS_SERIAL
                PRODUCT_TYPE.Text = Response.response_data.Product.PRODUCT_TYPE
                DISPLAY_NAME.Text = Response.response_data.Product.DISPLAY_NAME
                IS_ENABLE.Text = Response.response_data.Product.IS_ENABLE
                IS_DELETE.Text = Response.response_data.Product.IS_DELETE
                CATEGORY.Text = Response.response_data.Product.CATEGORY
                REQ_SALE_APPROACH.Text = Response.response_data.Product.REQ_SALE_APPROACH
                IS_VAT.Text = Response.response_data.Product.IS_VAT
                VAT_RATE.Text = Response.response_data.Product.VAT_RATE
                COMPANY_CODE.Text = Response.response_data.Product.COMPANY_CODE
                IS_SIM.Text = Response.response_data.Product.IS_SIM
                REQUIRE_RECEIVE_FORM.Text = Response.response_data.Product.REQUIRE_RECEIVE_FORM
                APPLE_CARE_SERVICE_CODE.Text = Response.response_data.Product.APPLE_CARE_SERVICE_CODE
                COLOR.Text = Response.response_data.Product.COLOR
                CAPACITY.Text = Response.response_data.Product.CAPACITY
                CATEGORY_RECOMMEND.Text = Response.response_data.Product.CATEGORY_RECOMMEND

                'Dim Captions As New BackEndInterface.Get_Product_Info.Response.Caption

                'SEQ.Text = Response.Caption(0).SEQ
                'PRODUCT_CODE.Text = Captions.PRODUCT_CODE
                'CAPTION_CODE.Text = Captions.CAPTION_CODE
                'CAPTION_DESC.Text = Captions.CAPTION_DESC
                'DETAIL.Text = Captions.DETAIL
>>>>>>> parent of cb7bd5e... เพิ่ม Page API  Prepaid_Register

            End If
        Catch ex As Exception
            lblErr_Msg.Text = ex.Message.ToString()
        End Try
    End Sub

End Class