Imports System.Web.Script.Serialization
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Configuration.ConfigurationManager
Imports System.Net
Imports System.IO

Public Class BackEndInterface

    Public Class General
        Public ReadOnly Property BaseURL As String
            Get
                Return AppSettings("BackEndURL").ToString
            End Get
        End Property

        Public Function CreateRequest(ByVal SubURL As String) As WebRequest
            Dim webReq As WebRequest = WebRequest.Create(BaseURL & SubURL)
            '--------------- Config Web Request ------------
            webReq.Method = "POST"
            webReq.ContentType = "application/x-www-form-urlencoded"
            webReq.Timeout = 10000
            webReq.Headers.Add("WEB_METHOD_CHANNEL", "VENDING")
            webReq.Headers.Add("E2E_REFID", "")
            Return webReq
        End Function

        Public Function GetJSONString(ByRef WebRequest As WebRequest, ByVal PostString As String) As String

            Dim C As New Converter
            Dim PostData As Byte() = C.StringToByte(PostString, Converter.EncodeType._UTF8)
            WebRequest.ContentLength = PostData.Length

            Try
                Dim ReqStream As Stream = WebRequest.GetRequestStream()
                ReqStream.Write(PostData, 0, PostData.Length)
                ReqStream.Close()
            Catch ex As Exception
                Return ""
            End Try

            Dim WebResponse As WebResponse = WebRequest.GetResponse()
            Dim RespData As Stream = WebResponse.GetResponseStream
            Return C.ByteToString(C.StreamToByte(RespData), Converter.EncodeType._UTF8)
        End Function

        '------- ส่ง Key ที่เป็น Dash มา----------
        Public Function CleanJSONDash(ByVal JSONString As String, ByVal Keys As String()) As String
            For i As Integer = 0 To Keys.Count - 1
                JSONString = JSONString.Replace("" & Keys(i) & "", "" & Keys(i).Replace("-", "_") & "")
            Next
            Return JSONString
        End Function

    End Class

    Public Class Face_Recognition

        Private Const SubURL As String = "/profiles/customer/face-recognition"
        Public JSONString As String = ""

#Region "DataModel"

        Public Class Response
            Public Property status As String
            Public Property trx_id As String
            Public Property process_instance As String
            Public Property response_data As Response

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

#End Region

        Public Function Get_Result(ByVal partner_code As String,
                                   ByVal id_number As String,
                                   ByVal face_recog_cust_certificate As String,
                                   ByVal face_recog_cust_capture As String,
                                   ByVal seq As String) As Response

            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(SubURL)

            Dim PostString As String = ""
            PostString &= "partner-code=" & partner_code & "&"
            PostString &= "id-number=" & id_number & "&"
            PostString &= "face-recog-cust-certificate=" & face_recog_cust_certificate & "&"
            PostString &= "face-recog-cust-certificate-id=" & "" & "&"
            PostString &= "face-recog-cust-capture=" & face_recog_cust_capture & "&"
            PostString &= "face-recog-cust-capture-id=" & "" & "&"
            PostString &= "seq=" & seq & "&"
            PostString &= "max-seq="

            JSONString = (New BackEndInterface.General).GetJSONString(WebRequest, PostString)

            'JSONString =  ""
            'JSONString &= " {" & vbLf
            'JSONString &= "     ""status"": ""SUCCESSFUL""," & vbLf
            'JSONString &= "     ""trx_id"": ""3Q1W5RZRZNB16""," & vbLf
            'JSONString &= "     ""process_instance"": ""psaapdv1 (instance: SFF_node1)""," & vbLf
            'JSONString &= "     ""response_data"": {" & vbLf
            'JSONString &= "         ""face_recognition_result"" : ""pass""," & vbLf
            'JSONString &= "         ""face_recognition_message"" : ""xxxxx""," & vbLf
            'JSONString &= "         ""over_max_allow"" : ""true""," & vbLf
            'JSONString &= "         ""over_max_allow_message"" : ""ทำรายการเกินที่กำหนด""," & vbLf
            'JSONString &= "         ""is_identical"" : ""true""," & vbLf
            'JSONString &= "         ""confident_ratio"" : ""0.71""," & vbLf
            'JSONString &= "         ""face_recog_cust_certificate_id"" : ""xxyyzz""," & vbLf
            'JSONString &= "         ""face_recog_cust_capture_id"" : ""xxyyzz""" & vbLf
            'JSONString &= "     }" & vbLf
            'JSONString &= " }" & vbLf

            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)(JSONString)
            Return Result
        End Function


    End Class

    Public Class Prepaid_Validate_Register

        Private Const SubURL As String = "/profiles/customer/validate-prepaid-register"
        Public JSONString As String = ""

#Region "DataModel"

        Public Class Response
            Public Property status As String
            Public Property trx_id As String
            Public Property process_instance As String
            Public Property response_data As Response

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

#End Region

        Public Function Get_Result(ByVal key_type As String,
                ByVal key_value As String,
                ByVal id_number As String,
                ByVal id_type As String) As Response

            Dim GetString As String = ""
            GetString &= "key-type=" & key_type & "&"
            GetString &= "key-value=" & key_value & "&"
            GetString &= "id-number=" & id_number & "&"
            GetString &= "id-type=" & id_type & "&"
            GetString &= "allow-registered="

            Dim URL As String = SubURL & "?" & GetString
            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(SubURL)

            JSONString = (New BackEndInterface.General).GetJSONString(WebRequest, "")
            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)(JSONString)
            Return Result
        End Function

    End Class

    'Public Class Prepaid_Generate_Order_Id

    'End Class

    Public Class Generate_Order_Id

        Private Const SubURL As String = "/aftersales/order/generate-id" '---------- Channel=??? , Dealer =???
        Public JSONString As String = ""

#Region "DataModel"

        Public Class Response
            Public Property status As String
            Public Property trx_id As String
            Public Property process_instance As String
            Public Property response_data As String
        End Class

#End Region

        Public Function Get_Result(ByVal dealer As String) As Response

            Dim GetString As String = ""
            GetString &= "channel=TLR&"
            GetString &= "dealer=" & dealer

            Dim URL As String = SubURL & "?" & GetString
            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(SubURL)

            JSONString = (New BackEndInterface.General).GetJSONString(WebRequest, "")
            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)(JSONString)
            Return Result
        End Function
    End Class

    Public Class Delete_File

        Private Const SubURL As String = "/sales/order/pdf/delete_file"
        Public JSONString As String = ""

#Region "DataModel"
        Public Class Response
            Public Property trx_id As String
            Public Property process_instance As String
            Public Property ref_id As String
            Public Property order_id As String
        End Class
#End Region

        Public Function Get_Result(ByVal order_id As String) As Response

            Dim GetString As String = ""
            GetString &= "order-id=" & order_id & "&"
            GetString &= "form-type=FACE_RECOG_CUST_CERTIFICATE"

            Dim URL As String = SubURL & "?" & GetString
            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(SubURL)

            JSONString = (New BackEndInterface.General).GetJSONString(WebRequest, "")
            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)(JSONString)
            Return Result
        End Function

    End Class

    Public Class Save_File

        Private Const SubURL As String = "/sales/order/pdf/save_file"
        Public JSONString As String = ""

#Region "DataModel"
        Public Class Response
            Public Property trx_id As String
            Public Property process_instance As String
            Public Property ref_id As String
            Public Property order_id As String
        End Class
#End Region

        Public Function Get_Result(ByVal order_id As String,
                                   ByVal fileType As String,
                                   ByVal b64File As String) As Response

            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(SubURL)

            Dim PostString As String = ""
            PostString &= "order-id=" & order_id & "&"
            PostString &= "fileType=" & fileType & "&"
            PostString &= "b64File=" & b64File & "&"
            PostString &= "formType=FACE_RECOG_CUST_CERTIFICATE"

            JSONString = (New BackEndInterface.General).GetJSONString(WebRequest, PostString)
            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)(JSONString)

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
            Public Property display_messages() As Display_Message
            Public Property trx_id As String
            Public Property process_instance As String
            Public Property response_data As Response


            Public Class Display_Message
                Public Property message As String
                Public Property message_type As String
                Public Property en_message As String
                Public Property th_message As String
            End Class

            Public Class Response
                Public Property data As internal
                Public Class internal
                    Public Property THAI_ID As String
                    Public Property LANG As String
                End Class
                Public Property flow_id As String
                Public Property flow_name As String
                Public Property create_date As String
                Public Property create_by As String
            End Class

        End Class
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

            Dim URL As String = SubURL & "?" & GetString

            Dim PostString As String = ""
            PostString &= "CREATE-BY=" & staffOpenShift & "&"
            PostString &= "ID-NUMBER=" & thaiID & "&"
            PostString &= "PRODUCT-ID-NUMBER=" & subscriber & "&"
            PostString &= "PARTNER-CODE=" & shopCode & "&"
            PostString &= "CUST-NAME=" & customerName & "&"
            PostString &= "CAMPAIGN-CODE=&"
            PostString &= "CAMPAIGN-NAME=&"
            PostString &= "SERVICE-CODE=TMV&"
            PostString &= "PROPO-PROMO=" & proposition & "&"
            PostString &= "PRODUCT-NAME=" & pricePlan & "&"
            PostString &= "SERVICE-NAME=POSTPAID&"
            PostString &= "SERIAL-NUMBER=" & sim & "&"
            PostString &= "SALE-CODE=" & saleCode & "&"
            PostString &= "E-SIGNATURE=NO&"
            PostString &= "READ-THAI-CARD=" & IIf(thaiID <> "", "YES", "NO") & "&"
            PostString &= "SUBSCRIBER=" & subscriber & "&"
            PostString &= "FACE_RECOFNITION_STATUS=" & face_recognition_result & "&"
            PostString &= "IS_IDENTICAL=" & is_identical & "&"
            PostString &= "CONFIDENT_RATIO=" & confident_ratio & "&"
            PostString &= "FACE_RECOG_CUST_CERTIFICATE_SOURCE=READ_CARD&"
            PostString &= "FACE_RECOG_CUST_CAPTURE_SOURCE=CAPTURE&"
            PostString &= "CUST_CERTIFICATE_LASER_ID=&"
            PostString &= "OS=VENDING&"

            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(URL)
            JSONString = (New BackEndInterface.General).GetJSONString(WebRequest, PostString)
            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)(JSONString)
            Return Result
        End Function

    End Class

    Public Class Service_Flow_Finish

        Private Const SubURL As String = "/sales/flow/finish"
        Public JSONString As String = ""

#Region "DataModel"
        Public Class Response
            Public Property status As String
            Public Property display_messages() As Display_Message
            Public Property trx_id As String
            Public Property process_instance As String
            Public Property response_data As Response

            Public Class Display_Message
                Public Property message As String
                Public Property message_type As String
                Public Property en_message As String
                Public Property th_message As String
            End Class

            Public Class Response
                Public Property data As internal
                Public Class internal
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
#End Region

        Public Function Get_Result(ByVal orderId As String) As Response

            Dim GetString As String = "flow-id=" & orderId & ""

            Dim URL As String = SubURL & "?" & GetString

            Dim PostString As String = ""
            PostString &= "orderId=" & orderId

            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(URL)
            JSONString = (New BackEndInterface.General).GetJSONString(WebRequest, PostString)
            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)(JSONString)
            Return Result

        End Function

    End Class

    Public Class Activity_Start

        Private Const SubURL As String = "/sales/flow/activity/start"
        Public JSONString As String = ""

#Region "DataModel"
        Public Class Response
            Public Property trx_id As String
            Public Property status As String
            Public Property process_instance As String
            Public Property display_messages() As Display_Message

            Public Class Display_Message
                Public Property message As String
                Public Property message_type As String
                Public Property en_message As String
                Public Property th_message As String
            End Class
        End Class
#End Region

        Public Function Get_Result(orderId As String) As Response

            Dim GetString As String = "flow-id=" & orderId & "&"
            GetString &= "activity-id=CONFIRM_ORDER"

            Dim URL As String = SubURL & "?" & GetString
            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(URL)
            JSONString = (New BackEndInterface.General).GetJSONString(WebRequest, "")
            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)(JSONString)
            Return Result
        End Function

    End Class

    Public Class Activity_End

        Private Const SubURL As String = "/sales/flow/activity/end"
        Public JSONString As String = ""

#Region "DataModel"
        Public Class Response
            Public Property trx_id As String
            Public Property status As String
            Public Property process_instance As String
            Public Property display_messages() As Display_Message

            Public Class Display_Message
                Public Property message As String
                Public Property message_type As String
                Public Property en_message As String
                Public Property th_message As String
            End Class
        End Class
#End Region

        Public Function Get_Result(orderId As String) As Response
            Dim GetString As String = "flow-id=" & orderId & "&"
            GetString &= "orderId=" & orderId

            Dim URL As String = SubURL & "?" & GetString
            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(URL)
            JSONString = (New BackEndInterface.General).GetJSONString(WebRequest, "")
            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)(JSONString)
            Return Result
        End Function

    End Class

End Class
