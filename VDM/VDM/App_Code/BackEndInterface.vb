Imports System.Web.Script.Serialization
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Configuration.ConfigurationManager
Imports System.Net
Imports System.IO

Public Class BackEndInterface

    Public Class General
        Public ReadOnly Property GetProductURL As String
            Get
                Return AppSettings("GetProductURL").ToString
            End Get
        End Property

        Public ReadOnly Property BackEndURL As String
            Get
                Return AppSettings("BackEndURL").ToString
            End Get
        End Property

        Public Function CreateRequest(ByVal URL As String) As WebRequest
            Dim webReq As WebRequest = WebRequest.Create(URL.Replace("//", "/"))
            '--------------- Config Web Request ------------
            webReq.Method = "POST"
            webReq.ContentType = "application/x-www-form-urlencoded"
            webReq.Timeout = 10000
            webReq.Headers.Add("WEB_METHOD_CHANNEL", "VENDING")
            webReq.Headers.Add("E2E_REFID", "")
            Return webReq
        End Function

        Public Function SendPostString(ByRef WebRequest As WebRequest, ByVal PostString As String) As String
            Dim C As New Converter
            WebRequest.ContentType = "application/x-www-form-urlencoded"
            Dim Data As Byte() = C.StringToByte(PostString, Converter.EncodeType._UTF8)

            WebRequest.ContentLength = Data.Length

            Dim ST = WebRequest.GetRequestStream()
            ST.Write(Data, 0, Data.Length)
            ST.Close()

            Dim WebResponse = WebRequest.GetResponse().GetResponseStream()

            Dim Reader As New StreamReader(WebResponse)
            Dim Result = Reader.ReadToEnd()
            Reader.Close()
            WebResponse.Close()

            Return Result
        End Function

        Public Function SendPostJSON(ByRef WebRequest As WebRequest, ByVal PostData As Dictionary(Of String, String)) As String

            Dim C As New Converter
            Dim JSONSrting As String = JsonConvert.SerializeObject(PostData, Formatting.Indented)
            WebRequest.ContentType = "application/json"
            Dim Data As Byte() = C.StringToByte(JSONSrting, Converter.EncodeType._UTF8)
            WebRequest.ContentLength = Data.Length

            Dim ST = WebRequest.GetRequestStream()
            ST.Write(Data, 0, Data.Length)
            ST.Close()

            Dim WebResponse = WebRequest.GetResponse().GetResponseStream()

            Dim Reader As New StreamReader(WebResponse)
            Dim Result = Reader.ReadToEnd()
            Reader.Close()
            WebResponse.Close()

            Return Result
        End Function

        '------- ส่ง Key ที่เป็น Dash มา----------
        Public Function CleanJSONDash(ByVal JSONString As String, ByVal Keys As String()) As String
            For i As Integer = 0 To Keys.Count - 1
                JSONString = JSONString.Replace("" & Keys(i) & "", "" & Keys(i).Replace("-", "_") & "")
            Next
            Return JSONString
        End Function

    End Class

    Public Class Get_Product_Info

        Public JSONString As String = ""

#Region "DataModel"
        Public Class Response

            Public Property response_data As Response

            Public Class Response
                Public Property Product As ProductMaster
                Public Property Price As String
                Public Property Captions As List(Of Caption)
            End Class

            Public Class ProductMaster
                Public Property CODE As String
                Public Property DESCRIPTION As String
                Public Property PLANT_CODE As String
                Public Property PRODUCT_BRAND_CODE As String
                Public Property PRODUCT_MODEL_CODE As String
                Public Property IS_SERIAL As String
                Public Property PRODUCT_TYPE As String
                Public Property DISPLAY_NAME As String
                Public Property IS_ENABLE As String
                Public Property IS_DELETE As String
                Public Property CATEGORY As String
                Public Property REQ_SALE_APPROACH As String
                Public Property IS_VAT As String
                Public Property VAT_RATE As String
                Public Property COMPANY_CODE As String
                Public Property IS_SIM As String
                Public Property REQUIRE_RECEIVE_FORM As String
                Public Property APPLE_CARE_SERVICE_CODE As String
                Public Property COLOR As String
                Public Property CAPACITY As String
                Public Property CATEGORY_RECOMMEND As String
            End Class

            Public Class Caption
                Public Property SEQ As String
                Public Property PRODUCT_CODE As String
                Public Property CAPTION_CODE As String
                Public Property CAPTION_DESC As String
                Public Property DETAIL As String
            End Class
        End Class

        Private CleanKeys() As String = {"response-data"}
#End Region
        Public Function Get_Result(ByVal productCode As String) As Response

            Dim URL As String = (New BackEndInterface.General).GetProductURL
            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(URL)

            'Dim PostString As String = ""
            'PostString &= "productCode=" & productCode
            Dim PostData As New Dictionary(Of String, String)
            PostData.Add("productCode", productCode)

            'JSONString = (New BackEndInterface.General).GetJSONString(WebRequest, PostString)
            JSONString = (New BackEndInterface.General).SendPostJSON(WebRequest, PostData)

            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)((New BackEndInterface.General).CleanJSONDash(JSONString, CleanKeys))
            Return Result
        End Function

    End Class

    Public Class Face_Recognition

        Private Const SubURL As String = "/profiles/customer/face-recognition"
        Public JSONString As String = ""

#Region "DataModel"

        Public Class Response

            Public Property status As String
            Public Property fault As faultModel
            Public Property display_messages As List(Of display_message)
            Public Property trx_id As String
            Public Property process_instance As String
            Public Property response_data As Response

            Public Class faultModel
                Public Property name As String
                Public Property code As String
                Public Property message As String
                Public Property detailed_message As String
            End Class

            Public Class display_message
                Public Property message As String
                Public Property message_code As String
                Public Property message_type As String
                Public Property en_message As String
                Public Property th_message As String
                Public Property technical_message As String
            End Class

            Public Class Response
                Public Property face_recognition_result As String
                Public Property face_recognition_message As String
                Public Property over_max_allow As String
                Public Property over_max_allow_message As String
                Public Property is_identical As String
                Public Property confident_ratio As String
                Public Property face_recog_cust_certificate_id As String
                Public Property face_recog_cust_capture_id As String
            End Class

        End Class

        Private CleanKeys() As String = {
                                        "detailed-message",
                                        "display-messages",
                                        "message-code",
                                        "message-type",
                                        "en-message",
                                        "th-message",
                                        "technical-message",
                                        "trx-id",
                                        "process-instance",
                                        "response-data",
                                        "face-recognition-result",
                                        "face-recognition-message",
                                        "over-max-allow",
                                        "over-max-allow-message",
                                        "is-identical",
                                        "confident-ratio",
                                        "face-recog-cust-certificate-id",
                                        "face-recog-cust-capture-id"}

#End Region

        Public Function Get_Result(ByVal partner_code As String,
                                   ByVal id_number As String,
                                   ByVal face_recog_cust_certificate As String,
                                   ByVal face_recog_cust_capture As String,
                                   ByVal seq As String) As Response

            Dim URL As String = (New BackEndInterface.General).BackEndURL & SubURL
            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(URL)

            'Dim PostString As String = ""
            'PostString &= "partner-code=" & partner_code & "&"
            'PostString &= "id-number=" & id_number & "&"
            'PostString &= "face-recog-cust-certificate=" & face_recog_cust_certificate & "&"
            'PostString &= "face-recog-cust-certificate-id=" & "" & "&"
            'PostString &= "face-recog-cust-capture=" & face_recog_cust_capture & "&"
            'PostString &= "face-recog-cust-capture-id=" & "" & "&"
            'PostString &= "seq=" & seq & "&"
            'PostString &= "max-seq="

            Dim PostData As New Dictionary(Of String, String)
            PostData.Add("partner-code", partner_code)
            PostData.Add("id-number", id_number)
            PostData.Add("face-recog-cust-certificate", face_recog_cust_certificate)
            PostData.Add("face-recog-cust-certificate-id", "")
            PostData.Add("face-recog-cust-capture", face_recog_cust_capture)
            PostData.Add("face-recog-cust-capture-id", "")
            PostData.Add("seq", seq)
            PostData.Add("max-seq", "")

            'JSONString = (New BackEndInterface.General).GetJSONString(WebRequest, PostString)
            JSONString = (New BackEndInterface.General).SendPostJSON(WebRequest, PostData)

            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)((New BackEndInterface.General).CleanJSONDash(JSONString, CleanKeys))
            Return Result
        End Function


    End Class

    Public Class Prepaid_Validate_Register

        Private Const SubURL As String = "/profiles/customer/validate-prepaid-register"
        Public JSONString As String = ""

#Region "DataModel"

        Public Class Response
            Public Property status As String
            Public Property fault As faultModel
            Public Property display_messages As List(Of display_message)
            Public Property trx_id As String
            Public Property process_instance As String
            Public Property response_data As Response

            Public Class faultModel
                Public Property name As String
                Public Property code As String
                Public Property message As String
                Public Property detailed_message As String
            End Class

            Public Class display_message
                Public Property message As String
                Public Property message_code As String
                Public Property message_type As String
                Public Property en_message As String
                Public Property th_message As String
                Public Property technical_message As String
            End Class

            Public Class Response
                Public Property subscriber As String
                Public Property sim As String
                Public Property imsi As String
                Public Property sim_category As String
                Public Property priceplan As String
                Public Property company_code As String
                Public Property is_registered As String
                Public Property firstname As String
                Public Property lastname As String
            End Class
        End Class

        Private CleanKeys() As String = {"detailed-message",
                                        "display-messages",
                                        "message-code",
                                        "message-type",
                                        "en-message",
                                        "th-message",
                                        "technical-message",
                                        "trx-id",
                                        "process-instance",
                                        "response-data",
                                        "sim-category",
                                        "company-code",
                                        "is-registered"}

#End Region

        Public Function Get_Result(ByVal key_value As String,
                ByVal id_number As String,
                ByVal id_type As String) As Response

            Dim GetString As String = ""
            GetString &= "key-type=SIM&"
            GetString &= "key-value=" & key_value & "&"
            GetString &= "id-number=" & id_number & "&"
            GetString &= "id-type=" & id_type & "&"
            GetString &= "allow-registered="

            Dim URL As String = (New BackEndInterface.General).BackEndURL & SubURL & "?" & GetString
            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(URL)

            Dim PostData As New Dictionary(Of String, String)
            'JSONString = (New BackEndInterface.General).GetJSONString(WebRequest, "")
            JSONString = (New BackEndInterface.General).SendPostJSON(WebRequest, PostData)

            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)((New BackEndInterface.General).CleanJSONDash(JSONString, CleanKeys))
            Return Result
        End Function

    End Class

    Public Class Prepaid_Register

        Private Const SubURL As String = "/profiles/customer/validate-prepaid-register"
        Public JSONString As String = ""

#Region "DataModel"

        Public Class Response
            Public Property status As String
            Public Property fault As faultModel
            Public Property display_messages As List(Of display_message)
            Public Property trx_id As String
            Public Property process_instance As String
            Public Property response_data As String

            Public Class faultModel
                Public Property name As String
                Public Property code As String
                Public Property message As String
                Public Property detailed_message As String
            End Class

            Public Class display_message
                Public Property message As String
                Public Property message_code As String
                Public Property message_type As String
                Public Property en_message As String
                Public Property th_message As String
                Public Property technical_message As String
            End Class

        End Class

        Private CleanKeys() As String = {"detailed-message",
                                        "display-messages",
                                        "message-code",
                                        "message-type",
                                        "en-message",
                                        "th-message",
                                        "technical-message",
                                        "trx-id",
                                        "process-instance",
                                        "response-data"}

#End Region


    End Class

    Public Class Generate_Order_Id

        Private Const SubURL As String = "/aftersales/order/generate-id" '---------- Channel=??? , Dealer =???
        Public JSONString As String = ""

#Region "DataModel"

        Public Class Response
            Public Property status As String
            Public Property fault As faultModel
            Public Property display_messages As List(Of display_message)
            Public Property trx_id As String
            Public Property process_instance As String
            Public Property response_data As String '--- OrderID

            Public Class faultModel
                Public Property name As String
                Public Property code As String
                Public Property message As String
                Public Property detailed_message As String
            End Class

            Public Class display_message
                Public Property message As String
                Public Property message_code As String
                Public Property message_type As String
                Public Property en_message As String
                Public Property th_message As String
                Public Property technical_message As String
            End Class
        End Class

        Private CleanKeys() As String = {"detailed-message",
                                        "display-messages",
                                        "message-code",
                                        "message-type",
                                        "en-message",
                                        "th-message",
                                        "technical-message",
                                        "trx-id",
                                        "process-instance",
                                        "response-data"}

#End Region

        Public Function Get_Result(ByVal dealer As String) As Response

            Dim GetString As String = ""
            GetString &= "channel=TLR&"
            GetString &= "dealer=" & dealer

            Dim URL As String = (New BackEndInterface.General).BackEndURL & SubURL & "?" & GetString
            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(URL)

            Dim PostData As New Dictionary(Of String, String)

            'JSONString = (New BackEndInterface.General).GetJSONString(WebRequest, "")
            JSONString = (New BackEndInterface.General).SendPostJSON(WebRequest, PostData)

            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)((New BackEndInterface.General).CleanJSONDash(JSONString, CleanKeys))
            Return Result
        End Function
    End Class

    Public Class Delete_File

        Private Const SubURL As String = "/sales/order/pdf/delete_file"
        Public JSONString As String = ""

#Region "DataModel"
        Public Class Response
            Public Property status As String
            Public Property fault As faultModel
            Public Property display_messages As List(Of display_message)
            Public Property trx_id As String
            Public Property process_instance As String
            Public Property order_id As String

            Public Class faultModel
                Public Property name As String
                Public Property code As String
                Public Property message As String
                Public Property detailed_message As String
            End Class

            Public Class display_message
                Public Property message As String
                Public Property message_code As String
                Public Property message_type As String
                Public Property en_message As String
                Public Property th_message As String
                Public Property technical_message As String
            End Class

        End Class

        Private CleanKeys() As String = {"detailed-message",
                                        "display-messages",
                                        "message-code",
                                        "message-type",
                                        "en-message",
                                        "th-message",
                                        "technical-message",
                                        "trx-id",
                                        "process-instance",
                                        "order-id"}

#End Region

        Public Function Get_Result(ByVal order_id As String) As Response

            Dim GetString As String = ""
            GetString &= "order-id=" & order_id & "&"
            GetString &= "form-type=FACE_RECOG_CUST_CERTIFICATE"

            Dim URL As String = (New BackEndInterface.General).BackEndURL & SubURL & "?" & GetString
            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(URL)

            Dim PostData As New Dictionary(Of String, String)

            'JSONString = (New BackEndInterface.General).GetJSONString(WebRequest, "")
            JSONString = (New BackEndInterface.General).SendPostJSON(WebRequest, PostData)

            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)((New BackEndInterface.General).CleanJSONDash(JSONString, CleanKeys))
            Return Result
        End Function

    End Class

    Public Class Save_File

        Private Const SubURL As String = "/sales/order/pdf/save_file"
        Public JSONString As String = ""

#Region "DataModel"
        Public Class Response
            Public Property status As String
            Public Property fault As faultModel
            Public Property display_messages As List(Of display_message)
            Public Property trx_id As String
            Public Property process_instance As String
            Public Property order_id As String

            Public Class faultModel
                Public Property name As String
                Public Property code As String
                Public Property message As String
                Public Property detailed_message As String
            End Class

            Public Class display_message
                Public Property message As String
                Public Property message_code As String
                Public Property message_type As String
                Public Property en_message As String
                Public Property th_message As String
                Public Property technical_message As String
            End Class

        End Class

        Private CleanKeys() As String = {"detailed-message",
                                        "display-messages",
                                        "message-code",
                                        "message-type",
                                        "en-message",
                                        "th-message",
                                        "technical-message",
                                        "trx-id",
                                        "process-instance",
                                        "order-id"}

#End Region

        Public Function Get_Result(ByVal order_id As String,
                                   ByVal fileType As String,
                                   ByVal b64File As String) As Response

            Dim URL As String = (New BackEndInterface.General).BackEndURL & SubURL
            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(URL)

            'Dim PostString As String = ""
            'PostString &= "order-id=" & order_id & "&"
            'PostString &= "fileType=" & fileType & "&"
            'PostString &= "b64File=" & b64File & "&"
            'PostString &= "formType=FACE_RECOG_CUST_CERTIFICATE"

            Dim PostData As New Dictionary(Of String, String)
            PostData.Add("order-id", order_id)
            PostData.Add("fileType", fileType)
            PostData.Add("b64File", b64File)
            PostData.Add("formType", "FACE_RECOG_CUST_CERTIFICATE")

            'JSONString = (New BackEndInterface.General).GetJSONString(WebRequest, PostString)
            JSONString = (New BackEndInterface.General).SendPostJSON(WebRequest, PostData)

            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)((New BackEndInterface.General).CleanJSONDash(JSONString, CleanKeys))

            Return Result
        End Function

    End Class

    Public Class Service_Flow_Create

        Private Const SubURL As String = "/sales/flow/create"
        Public JSONString As String = ""

        Public Enum FlowType
            Mobile = 1
            Device = 2
        End Enum
#Region "DataModel"
        Public Class Response
            Public Property status As String
            Public Property fault As faultModel
            Public Property display_messages As List(Of display_message)
            Public Property trx_id As String
            Public Property process_instance As String
            Public Property response_data As Response

            Public Class faultModel
                Public Property name As String
                Public Property code As String
                Public Property message As String
                Public Property detailed_message As String
            End Class

            Public Class display_message
                Public Property message As String
                Public Property message_code As String
                Public Property message_type As String
                Public Property en_message As String
                Public Property th_message As String
                Public Property technical_message As String
            End Class

            Public Class Response
                Public Property data As language
                Public Class language
                    Public Property THAI_ID As String
                    Public Property LANG As String
                End Class
                Public Property flow_id As String
                Public Property flow_name As String
                Public Property create_date As String
                Public Property create_by As String
            End Class

        End Class

        Private CleanKeys() As String = {"detailed-message",
                                        "display-messages",
                                        "message-code",
                                        "message-type",
                                        "en-message",
                                        "th-message",
                                        "technical-message",
                                        "trx-id",
                                        "process-instance",
                                        "response-data",
                                        "THAI-ID",
                                        "flow-id",
                                        "flow-name",
                                        "create-date",
                                        "create-by"
                                        }

#End Region

        Public Function Get_Result(ByVal orderId As String,
                                   ByVal FlowType As FlowType,
                                    ByVal staffOpenShift As String,
                                    ByVal thaiID As String,
                                    ByVal subscriber As String,
                                    ByVal shopCode As String,
                                    ByVal customerName As String,
                                    ByVal proposition As String,
                                    ByVal pricePlan As String,
                                    ByVal sim As String,
                                    ByVal saleCode As String,
                                    ByVal face_recognition_result As String,
                                    ByVal is_identical As String,
                                    ByVal confident_ratio As String) As Response

            Dim GetString As String = ""
            GetString &= "orderId=" & orderId & "&"
            GetString &= "flowName=" & IIf(FlowType = FlowType.Mobile, "Mobile", "Device")

            'Dim PostString As String = ""
            'PostString &= "CREATE-BY=" & staffOpenShift & "&"
            'PostString &= "ID-NUMBER=" & thaiID & "&"
            'PostString &= "PRODUCT-ID-NUMBER=" & subscriber & "&"
            'PostString &= "PARTNER-CODE=" & shopCode & "&"
            'PostString &= "CUST-NAME=" & customerName & "&"
            'PostString &= "CAMPAIGN-CODE=&"
            'PostString &= "CAMPAIGN-NAME=&"
            'PostString &= "SERVICE-CODE=TMV&"
            'PostString &= "PROPO-PROMO=" & proposition & "&"
            'PostString &= "PRODUCT-NAME=" & pricePlan & "&"
            'PostString &= "SERVICE-NAME=POSTPAID&"
            'PostString &= "SERIAL-NUMBER=" & sim & "&"
            'PostString &= "SALE-CODE=" & saleCode & "&"
            'PostString &= "E-SIGNATURE=NO&"
            'PostString &= "READ-THAI-CARD=" & IIf(thaiID <> "", "YES", "NO") & "&"
            'PostString &= "SUBSCRIBER=" & subscriber & "&"
            'PostString &= "FACE_RECOFNITION_STATUS=" & face_recognition_result & "&"
            'PostString &= "IS_IDENTICAL=" & is_identical & "&"
            'PostString &= "CONFIDENT_RATIO=" & confident_ratio & "&"
            'PostString &= "FACE_RECOG_CUST_CERTIFICATE_SOURCE=READ_CARD&"
            'PostString &= "FACE_RECOG_CUST_CAPTURE_SOURCE=CAPTURE&"
            'PostString &= "CUST_CERTIFICATE_LASER_ID=&"
            'PostString &= "OS=VENDING&"

            Dim PostData As New Dictionary(Of String, String)
            PostData.Add("CREATE-BY", staffOpenShift)
            PostData.Add("ID-NUMBER", thaiID)
            PostData.Add("PRODUCT-ID-NUMBER", subscriber)
            PostData.Add("PARTNER-CODE", shopCode)
            PostData.Add("CUST-NAME", customerName)
            PostData.Add("CAMPAIGN-CODE", "")
            PostData.Add("CAMPAIGN-NAME", "")
            PostData.Add("SERVICE-CODE", "TMV")
            PostData.Add("PROPO-PROMO", proposition)
            PostData.Add("PRODUCT-NAME", pricePlan)
            PostData.Add("SERVICE-NAME", "POSTPAID")
            PostData.Add("SERIAL-NUMBER", sim)
            PostData.Add("SALE-CODE", saleCode)
            PostData.Add("E-SIGNATURE", "NO")
            PostData.Add("READ-THAI-CARD", IIf(thaiID <> "", "YES", "NO"))
            PostData.Add("SUBSCRIBER", subscriber)
            PostData.Add("FACE_RECOFNITION_STATUS", face_recognition_result)
            PostData.Add("IS_IDENTICAL", is_identical)
            PostData.Add("CONFIDENT_RATIO", confident_ratio)
            PostData.Add("FACE_RECOG_CUST_CERTIFICATE_SOURCE", "READ_CARD")
            PostData.Add("FACE_RECOG_CUST_CAPTURE_SOURCE", "CAPTURE")
            PostData.Add("CUST_CERTIFICATE_LASER_ID", "")
            PostData.Add("OS", "VENDING")

            Dim URL As String = (New BackEndInterface.General).BackEndURL & SubURL & "?" & GetString
            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(URL)

            'JSONString = (New BackEndInterface.General).GetJSONString(WebRequest, PostString)
            JSONString = (New BackEndInterface.General).SendPostJSON(WebRequest, PostData)

            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)((New BackEndInterface.General).CleanJSONDash(JSONString, CleanKeys))
            Return Result
        End Function

    End Class

    Public Class Service_Flow_Finish

        Private Const SubURL As String = "/sales/flow/finish"
        Public JSONString As String = ""

#Region "DataModel"

        Public Class Response
            Public Property status As String
            Public Property fault As faultModel
            Public Property display_messages As List(Of display_message)
            Public Property trx_id As String
            Public Property process_instance As String
            Public Property response_data As Response

            Public Class faultModel
                Public Property name As String
                Public Property code As String
                Public Property message As String
                Public Property detailed_message As String
            End Class

            Public Class display_message
                Public Property message As String
                Public Property message_code As String
                Public Property message_type As String
                Public Property en_message As String
                Public Property th_message As String
                Public Property technical_message As String
            End Class

            Public Class Response
                Public Property data As language
                Public Class language
                    Public Property THAI_ID As String
                    Public Property LANG As String
                End Class
                Public Property flow_id As String
                Public Property flow_name As String
                Public Property create_date As String
                Public Property create_by As String
                Public Property end_date As String
                Public Property end_by As String
            End Class

        End Class

        Private CleanKeys() As String = {"detailed-message",
                                        "display-messages",
                                        "message-code",
                                        "message-type",
                                        "en-message",
                                        "th-message",
                                        "technical-message",
                                        "trx-id",
                                        "process-instance",
                                        "response-data",
                                        "THAI-ID",
                                        "flow-id",
                                        "flow-name",
                                        "create-date",
                                        "create-by",
                                        "end-date",
                                        "end-by"
                                                }
#End Region

        Public Function Get_Result(ByVal orderId As String) As Response

            Dim GetString As String = "flow-id=" & orderId & ""

            'Dim PostString As String = ""
            'PostString &= "orderId=" & orderId

            Dim PostData As New Dictionary(Of String, String)
            PostData.Add("orderId", orderId)

            Dim URL As String = (New BackEndInterface.General).BackEndURL & SubURL & "?" & GetString
            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(URL)

            'JSONString = (New BackEndInterface.General).GetJSONString(WebRequest, PostString)
            JSONString = (New BackEndInterface.General).SendPostJSON(WebRequest, PostData)
            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)((New BackEndInterface.General).CleanJSONDash(JSONString, CleanKeys))
            Return Result

        End Function

    End Class

    Public Class Activity_Start

        Private Const SubURL As String = "/sales/flow/activity/start"
        Public JSONString As String = ""

#Region "DataModel"
        Public Class Response
            Public Property status As String
            Public Property fault As faultModel
            Public Property display_messages As List(Of display_message)
            Public Property trx_id As String
            Public Property process_instance As String
            Public Property order_id As String

            Public Class faultModel
                Public Property name As String
                Public Property code As String
                Public Property message As String
                Public Property detailed_message As String
            End Class

            Public Class display_message
                Public Property message As String
                Public Property message_code As String
                Public Property message_type As String
                Public Property en_message As String
                Public Property th_message As String
                Public Property technical_message As String
            End Class

        End Class

        Private CleanKeys() As String = {"detailed-message",
                                        "display-messages",
                                        "message-code",
                                        "message-type",
                                        "en-message",
                                        "th-message",
                                        "technical-message",
                                        "trx-id",
                                        "process-instance"
                                         }

#End Region

        Public Function Get_Result(orderId As String) As Response

            Dim GetString As String = "flow-id=" & orderId & "&"
            GetString &= "activity-id=CONFIRM_ORDER"

            Dim PostData As New Dictionary(Of String, String)
            PostData.Add("orderId", orderId)
            PostData.Add("activityId", "CONFIRM_ORDER")


            Dim URL As String = (New BackEndInterface.General).BackEndURL & SubURL & "?" & GetString
            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(URL)

            'JSONString = (New BackEndInterface.General).GetJSONString(WebRequest, "")
            JSONString = (New BackEndInterface.General).SendPostJSON(WebRequest, PostData)

            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)((New BackEndInterface.General).CleanJSONDash(JSONString, CleanKeys))
            Return Result
        End Function

    End Class

    Public Class Activity_End

        Private Const SubURL As String = "/sales/flow/activity/end"
        Public JSONString As String = ""

#Region "DataModel"
        Public Class Response
            Public Property status As String
            Public Property fault As faultModel
            Public Property display_messages As List(Of display_message)
            Public Property trx_id As String
            Public Property process_instance As String
            Public Property order_id As String

            Public Class faultModel
                Public Property name As String
                Public Property code As String
                Public Property message As String
                Public Property detailed_message As String
            End Class

            Public Class display_message
                Public Property message As String
                Public Property message_code As String
                Public Property message_type As String
                Public Property en_message As String
                Public Property th_message As String
                Public Property technical_message As String
            End Class

        End Class

        Private CleanKeys() As String = {"detailed-message",
                                        "display-messages",
                                        "message-code",
                                        "message-type",
                                        "en-message",
                                        "th-message",
                                        "technical-message",
                                        "trx-id",
                                        "process-instance"
                                         }

#End Region

        Public Function Get_Result(orderId As String) As Response
            Dim GetString As String = "flow-id=" & orderId & "&"
            GetString &= "orderId=" & orderId

            Dim PostData As New Dictionary(Of String, String)
            PostData.Add("orderId", orderId)

            Dim URL As String = (New BackEndInterface.General).BackEndURL & SubURL & "?" & GetString
            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(URL)

            'JSONString = (New BackEndInterface.General).GetJSONString(WebRequest, "")
            JSONString = (New BackEndInterface.General).SendPostJSON(WebRequest, PostData)

            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)((New BackEndInterface.General).CleanJSONDash(JSONString, CleanKeys))
            Return Result
        End Function

    End Class

End Class
