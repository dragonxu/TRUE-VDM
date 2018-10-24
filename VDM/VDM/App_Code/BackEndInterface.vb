Imports Newtonsoft.Json
Imports System.Configuration.ConfigurationManager
Imports System.Net
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing

Public Class BackEndInterface

    Public Class General

        Public ReadOnly Property ValidateSerialURL As String
            Get
                Return AppSettings("ValidateSerialURL").ToString
            End Get
        End Property

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
            'Dim webReq As WebRequest = WebRequest.Create("http://www.tit-tech.co.th/cmpg/COM_Wallet.aspx?CUS_ID=1")
            Dim webReq As WebRequest = WebRequest.Create(URL)
            '--------------- Config Web Request ------------
            webReq.Method = "POST"
            webReq.Timeout = 10000

            webReq.Headers.Add("WEB_METHOD_CHANNEL", "VENDING")
            webReq.Headers.Add("E2E_REFID", "1939900150601")
            webReq.Headers.Add("LOGINNAME", "0011234")          'User ที่ Login เข้าระบบ User_Name
            webReq.Headers.Add("EMPLOYEEID", "0011234")         'User ที่ Login เข้าระบบ Sale_Code
            webReq.Headers.Add("ENGLISHNAME", "0011234")        'User ที่ Login เข้าระบบ Name + LastName
            webReq.Headers.Add("THAINAME", "0011234")           'User ที่ Login เข้าระบบ Name + LastName

            Return webReq
        End Function

        Public Function SendGetURL(ByRef WebRequest As WebRequest) As String

            WebRequest.Method = "GET"
            Dim WebResponse = WebRequest.GetResponse().GetResponseStream()
            Dim Reader As New StreamReader(WebResponse)
            Dim Result = Reader.ReadToEnd()
            Reader.Close()
            WebResponse.Close()

            Return Result
        End Function

        Public Function SendPostString(ByRef WebRequest As WebRequest, ByVal PostData As Dictionary(Of String, String)) As String

            Dim C As New Converter
            Dim PostString As String = String.Join("&", PostData.Select(Function(pair) String.Format("{0}={1}", pair.Key, pair.Value)).ToArray())
            'WebRequest.ContentType = "application/x-www-form-urlencoded"
            'WebRequest.ContentType = "application/json"
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
            Dim JSONString As String = JsonConvert.SerializeObject(PostData, Formatting.Indented)
            Return SendPostJSON(WebRequest, JSONString)
        End Function

        Public Function SendPostJSON(ByRef WebRequest As WebRequest, ByVal JSONString As String) As String

            Dim C As New Converter

            WebRequest.ContentType = "application/json"
            'WebRequest.ContentType = "application/x-www-form-urlencoded"

            Dim Data As Byte() = C.StringToByte(JSONString, Converter.EncodeType._UTF8)
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


        Public Sub UPDATE_LOG_ERROR_MESSAGE(ByVal TableName As String, ByVal REQ_ID As Integer, ByVal ErrorMessage As String)
            Dim SQL As String = "UPDATE " & TableName & " SET ErrorMessage='" & Replace(ErrorMessage, "'", "''") & "' WHERE REQ_ID=" & REQ_ID
            Dim BL As New VDM_BL
            BL.ExecuteNonQuery_Log(SQL)
        End Sub
    End Class

    Public Class Get_Product_Info

        Public JSONString As String = ""

#Region "DataModel"
        Public Class Response

            'Public Property response_data As Response
            Public Property Product As ProductMaster
            Public Property Price As String
            Public Property Captions As List(Of Caption)
            'Public Class Response
            '    Public Property Product As ProductMaster
            '    Public Property Price As String
            '    '
            'End Class

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

            Dim URL As String = (New BackEndInterface.General).GetProductURL & "?productCode=" & productCode
            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(URL)

            Dim PostData As New Dictionary(Of String, String)
            PostData.Add("productCode", productCode)

            JSONString = (New BackEndInterface.General).SendPostString(WebRequest, PostData)

            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)((New BackEndInterface.General).CleanJSONDash(JSONString, CleanKeys))
            Return Result
        End Function

    End Class

    Public Class Validate_Serial
        Public JSONString As String = ""

        Public Class Response

            Public Property JSONString As String
            Public Property ReturnValues As List(Of Object)
            Public Property IsError As String
            Public Property ErrorMessage As String
            Public Property IsNotTransaction As String
            Public Property REQ_ID As Integer

        End Class

        Public Function Get_Result(ByVal Shop As String, ByVal Serial As String) As Response

            '------------------- Save Log ----------------
            Dim BL As New VDM_BL
            '---------------- Save REQ Log ---------------
            Dim DR As DataRow
            Dim REQ_ID As Integer = BL.Get_NewID_Log("Backend_Validate_Serial_REQ", "REQ_ID")
            Dim SQL As String = "SELECT TOP 0 * FROM Backend_Validate_Serial_REQ"
            Dim DA As New SqlDataAdapter(SQL, BL.LogConnectionString)
            Dim DT As New DataTable
            DA.Fill(DT)
            DR = DT.NewRow : DT.Rows.Add(DR)
            DR("REQ_ID") = REQ_ID
            DR("Shop") = Shop
            DR("Serial") = Serial
            DR("REQ_Time") = Now
            Dim cmd As New SqlCommandBuilder(DA)
            DA.Update(DT)

            '---------------- Call ---------------
            Dim URL As String = (New BackEndInterface.General).ValidateSerialURL & "?Shop=" & Shop & "&Serial=" & Serial
            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(URL)

            Dim Result As Response = Nothing '------------- ยังไงก็ต้อง Return Result ----------------
            Try
                JSONString = (New BackEndInterface.General).SendGetURL(WebRequest)
                Result = JsonConvert.DeserializeObject(Of Response)(JSONString)
            Catch ex As Exception
                Result = New Response
                Result.ReturnValues = New List(Of Object)
                Result.IsError = True
                Result.ErrorMessage = ex.Message
                Result.IsNotTransaction = False
            End Try

            Result.JSONString = JSONString
            Result.REQ_ID = REQ_ID

            '------------- Save RESP Log -------------
            SQL = "SELECT TOP 0 * FROM Backend_Validate_Serial_RESP"
            DA = New SqlDataAdapter(SQL, BL.LogConnectionString)
            DT = New DataTable
            DA.Fill(DT)
            DR = DT.NewRow : DT.Rows.Add(DR)
            DR("REQ_ID") = REQ_ID
            DR("JSONString") = Result.JSONString
            If Result.ReturnValues.Count > 0 Then
                DR("CODE") = Result.ReturnValues(0)
            End If
            If Result.ReturnValues.Count > 1 Then
                DR("IS_SIM") = Result.ReturnValues(1)
            End If
            DR("IsError") = Result.IsError
            DR("ErrorMessage") = Result.ErrorMessage
            DR("IsNotTransaction") = Result.IsNotTransaction
            DR("RESP_Time") = Now

            cmd = New SqlCommandBuilder(DA)
            DA.Update(DT)

            Return Result
        End Function
    End Class

    Public Class Face_Recognition

        Private Const SubURL As String = "/profiles/customer/face-recognition"

#Region "DataModel"

        Public Class Response

            Public Property JSONString As String
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

            Dim PostData As New Dictionary(Of String, String)
            PostData.Add("partner-code", partner_code)
            PostData.Add("id-number", id_number)
            PostData.Add("face-recog-cust-certificate", face_recog_cust_certificate)
            PostData.Add("face-recog-cust-certificate-id", "")
            PostData.Add("face-recog-cust-capture", face_recog_cust_capture)
            PostData.Add("face-recog-cust-capture-id", "")
            PostData.Add("seq", seq)
            PostData.Add("max-seq", "")

            'Dim JSONString As String = (New BackEndInterface.General).SendPostJSON(WebRequest, PostData)
            'Dim Result As Response = JsonConvert.DeserializeObject(Of Response)((New BackEndInterface.General).CleanJSONDash(JSONString, CleanKeys))


            Dim JSONString As String = ""
            Dim Result As Response = Nothing
            Try
                JSONString = (New BackEndInterface.General).SendPostJSON(WebRequest, PostData)
                Result = JsonConvert.DeserializeObject(Of Response)((New BackEndInterface.General).CleanJSONDash(JSONString, CleanKeys))

            Catch ex As Exception
                Result = New Response
                'Result.ReturnValues = New List(Of Object)
                'Result.IsError = True
                'Result.ErrorMessage = ex.Message
                'Result.IsNotTransaction = False
            End Try

            Result.JSONString = JSONString

            Return Result
        End Function

    End Class

    Public Class Prepaid_Validate_Register

        Private Const SubURL As String = "/profiles/customer/validate-prepaid-register"

#Region "DataModel"

        Public Class Response

            Public Property JSONString As String
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

        Public Function Get_Result(ByVal sim_serial As String,
                ByVal id_number As String,
                ByVal id_type As String) As Response

            '------------- ID Type I=PersonalCard,P=Passport,H=Other

            Dim GetString As String = ""
            GetString &= "key-type=SIM&"
            GetString &= "key-value=" & sim_serial & "&"
            GetString &= "id-number=" & id_number & "&"
            GetString &= "id-type=" & id_type & "&"
            GetString &= "allow-registered="

            Dim URL As String = (New BackEndInterface.General).BackEndURL & SubURL & "?" & GetString
            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(URL)

            Dim PostData As New Dictionary(Of String, String)
            Dim JSONString As String = (New BackEndInterface.General).SendGetURL(WebRequest)
            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)((New BackEndInterface.General).CleanJSONDash(JSONString, CleanKeys))
            Result.JSONString = JSONString
            Return Result
        End Function

    End Class

    Public Class Prepaid_Register

        Private Const SubURL As String = "/sales/order/submit"

        Public Enum Gender
            MALE = 1
            FEMALE = 2
        End Enum

        Public Enum IsRegistered
            Y = 1
            N = 0
        End Enum

        Public Enum SubActivity
            UNPAIR = 1
            PAIR = 2
        End Enum


#Region "DataModel"

        Public Class Response

            Public Property JSONString As String
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

        Public Function Get_Result(ByVal OrderID As String,
                ByVal customer_gender As Gender,
                ByVal customer_title As String,
                ByVal customer_language As String,
                ByVal customer_title_code As String,
                ByVal customer_firstname As String,
                ByVal customer_lastname As String,
                ByVal customer_birthdate As String,
                ByVal customer_id_number As String,
                ByVal customer_id_expire_date As String,
                ByVal address_number As String,
                ByVal address_moo As String,
                ByVal address_village As String,
                ByVal address_street As String,
                ByVal address_soi As String,
                ByVal address_district As String,
                ByVal address_province As String,
                ByVal address_building_name As String,
                ByVal address_building_room As String,
                ByVal address_building_floor As String,
                ByVal sddress_sub_district As String,
                ByVal address_zip As String,
                ByVal shopCode As String,
                ByVal shopName As String,
                ByVal sale_code As String,
                ByVal mat_code As String,
                ByVal mat_desc As String,
                ByVal sim_serial As String,
                ByVal require_print_form As Boolean,
                ByVal price_plan As String,
                ByVal subscriber As String,
                ByVal is_registered As IsRegistered,
                ByVal sub_activity As SubActivity,
                ByVal company_code As String) As Response

            Dim PostString = ""

            PostString &= "{	" & vbLf
            PostString &= "    ""order"": {	" & vbLf
            PostString &= "        ""order-id"": """ & OrderID & """,	" & vbLf
            PostString &= "        ""customer"": {	" & vbLf
            PostString &= "            ""gender"": """ & IIf(customer_gender = Gender.MALE, "MALE", "FEMALE") & """,	" & vbLf
            PostString &= "            ""title"": """ & customer_title & """,	" & vbLf
            PostString &= "            ""language"": """ & customer_language & """,	" & vbLf
            PostString &= "            ""title-code"": ""T5"",	" & vbLf            '---Fix T5
            PostString &= "            ""firstname"": """ & customer_firstname & """,	" & vbLf
            PostString &= "            ""lastname"": """ & customer_lastname & """,	" & vbLf
            PostString &= "            ""birthdate"": """ & customer_birthdate & """,	" & vbLf
            PostString &= "            ""customer-type"": ""P"",	" & vbLf
            PostString &= "            ""id-type"": ""I"",	" & vbLf
            PostString &= "            ""id-number"": """ & customer_id_number & """,	" & vbLf
            PostString &= "            ""id-expire-date"": """ & customer_id_expire_date & """,	" & vbLf
            PostString &= "            ""customer-level"": ""NON-TOP"",	" & vbLf
            PostString &= "            ""customer-sublevel"": ""NONE"",	" & vbLf
            PostString &= "            ""customer-sublevel-id"": ""2"",	" & vbLf
            PostString &= "            ""address-list"": {	" & vbLf
            PostString &= "                ""CUSTOMER_ADDRESS"": {	" & vbLf
            PostString &= "                    ""number"": """ & address_number & """,	" & vbLf
            PostString &= "                    ""moo"": """ & address_moo & """,	" & vbLf
            PostString &= "                    ""village"": """ & address_village & """,	" & vbLf
            PostString &= "                    ""street"": """ & address_street & """,	" & vbLf
            PostString &= "                    ""soi"": """ & address_soi & """,	" & vbLf
            PostString &= "                    ""district"": """ & address_district & """,	" & vbLf
            PostString &= "                    ""province"": """ & address_province & """,	" & vbLf
            PostString &= "                    ""building-name"": """ & address_building_name & """,	" & vbLf
            PostString &= "                    ""building-room"": """ & address_building_room & """,	" & vbLf
            PostString &= "                    ""building-floor"": """ & address_building_floor & """,	" & vbLf
            PostString &= "                    ""sub-district"": """ & sddress_sub_district & """,	" & vbLf
            PostString &= "                    ""zip"": """ & address_zip & """	" & vbLf
            PostString &= "                }	" & vbLf
            PostString &= "            }	" & vbLf
            PostString &= "        },	" & vbLf
            PostString &= "        ""sale-agent"": {	" & vbLf
            PostString &= "            ""name"": ""-"",	" & vbLf
            PostString &= "            ""channel"": ""TLR"",	" & vbLf
            PostString &= "            ""partner-code"": """ & shopCode & """,	" & vbLf
            PostString &= "            ""partner-name"": """ & shopName & """,	" & vbLf
            PostString &= "            ""sale-code"": """ & sale_code & """,	" & vbLf
            PostString &= "            ""partner-type"": ""2""	" & vbLf
            PostString &= "        },	" & vbLf
            PostString &= "        ""order-items"": [	" & vbLf
            PostString &= "            {	" & vbLf
            PostString &= "                ""product-category"": ""GOODS"",	" & vbLf
            PostString &= "                ""order-type"": ""NEW"",	" & vbLf
            PostString &= "                ""name"": ""SALE_GOODS"",	" & vbLf
            PostString &= "                ""product-name"": """ & mat_desc & """,	" & vbLf
            PostString &= "                ""product-id-number"": """ & mat_code & """,	" & vbLf
            PostString &= "                ""product-id-name"": ""MATERIAL_CODE"",	" & vbLf
            PostString &= "                ""order-data"": { " & vbLf
            PostString &= "                    ""CURRENT-FLOW-ACTIVITY"": ""CONFIRM_ORDER""	" & vbLf        '--Add เพิ่ม
            PostString &= "                     },	" & vbLf

            PostString &= "                         ""primary-order-data"": {	" & vbLf
            PostString &= "                             ""IS-SIM"": ""1"",	" & vbLf
            PostString &= "                             ""SERIAL-NO"": """ & sim_serial & """,	" & vbLf
            PostString &= "                             ""REQUIRE_PRINTFORM"": """ & IIf(require_print_form, "true", "false") & """,	" & vbLf
            PostString &= "                             ""STOCK-TYPE"": ""TRANSFER""	" & vbLf
            PostString &= "                     }	" & vbLf
            PostString &= "            },	" & vbLf
            PostString &= "            {	" & vbLf
            PostString &= "                ""product-category"": ""TMV"",	" & vbLf
            PostString &= "                ""order-type"": ""NEW"",	" & vbLf
            PostString &= "                ""name"": ""PREPAID_REGISTER_PARENT"",	" & vbLf
            PostString &= "                ""product-name"": ""PRICEPLAN"",	" & vbLf
            PostString &= "                ""product-id-number"": ""0000000005"",	" & vbLf
            PostString &= "                ""product-id-name"": ""MSISDN_PARENT"",	" & vbLf
            PostString &= "                ""order-data"": {	" & vbLf
            PostString &= "                                     ""CURRENT-FLOW-ACTIVITY"": ""CONFIRM_ORDER""	" & vbLf        '--Add
            PostString &= "                                }	" & vbLf
            PostString &= "            },	" & vbLf
            PostString &= "	" & vbLf
            PostString &= "            {	" & vbLf
            PostString &= "                ""product-category"": ""TMV"",	" & vbLf
            PostString &= "                ""order-type"": ""NEW"",	" & vbLf
            PostString &= "                ""name"": ""PREPAID_REGISTER"",	" & vbLf
            PostString &= "                ""product-name"": """ & price_plan & """,	" & vbLf
            PostString &= "                ""product-id-number"": """ & subscriber & """,	" & vbLf
            PostString &= "                ""product-id-name"": ""MSISDN"",	" & vbLf
            PostString &= "                ""order-data"": {	" & vbLf
            PostString &= "                    ""REQUIRE-STOCK-ISSUE"": ""N"",	" & vbLf        '--Add
            PostString &= "                    ""CURRENT-FLOW-ACTIVITY"": ""CONFIRM_ORDER"",	" & vbLf        '--Add

            PostString &= "                    ""REF-PARENT-PRODUCT-IDNUMBER"": ""0000000005"",	" & vbLf
            PostString &= "                    ""IS-REGISTERED"": """ & IIf(is_registered = IsRegistered.Y, "Y", "N") & """,	" & vbLf
            PostString &= "                    ""SIM"": """ & sim_serial & """,	" & vbLf
            PostString &= "                    ""STOCK-TYPE"": ""TRANSFER""	" & vbLf
            PostString &= "                },	" & vbLf
            PostString &= "                ""primary-order-data"": {	" & vbLf
            PostString &= "                    ""ACCOUNT-SUB-TYPE-DESCRIPTION"": ""-"",	" & vbLf
            PostString &= "                    ""SUB-ACTIVITY"": """ & IIf(sub_activity = SubActivity.PAIR, "PREPAID_REGISTER_PAIR", "PREPAID_REGISTER_UNPAIR") & """,	" & vbLf
            PostString &= "	" & vbLf
            PostString &= "                    ""ACCOUNT-SUB-TYPE"": ""PRE"",	" & vbLf
            PostString &= "                    ""COMPANY-CODE"": """ & company_code & """	" & vbLf
            PostString &= "                }	" & vbLf
            PostString &= "            }	" & vbLf
            PostString &= "        ]	" & vbLf
            PostString &= "    }	" & vbLf
            PostString &= "}	" & vbLf

            Dim URL As String = (New BackEndInterface.General).BackEndURL & SubURL
            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(URL)

            'WebRequest.Headers.Add("WEB_METHOD_CHANNEL", "VENDING")
            'WebRequest.Headers.Add("E2E_REFID", customer_id_number)
            'WebRequest.Headers.Add("LOGINNAME", "0011234")          'User ที่ Login เข้าระบบ Sale_Code
            'WebRequest.Headers.Add("EMPLOYEEID", "0011234")

            'WebRequest.Headers.Add("ENGLISHNAME", "0011234")
            'WebRequest.Headers.Add("THAINAME", "0011234")

            WebRequest.Method = "POST"
            Dim PostData As New Dictionary(Of String, String)
            Dim JSONString As String = (New BackEndInterface.General).SendPostJSON(WebRequest, PostString)
            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)((New BackEndInterface.General).CleanJSONDash(JSONString, CleanKeys))
            Result.JSONString = JSONString
            Return Result

        End Function
    End Class


    Private Function PostJSON_Prepaid(ByVal JsonData As String, ByVal URL As String) As HttpWebRequest
        Dim objhttpWebRequest As HttpWebRequest
        Try
            Dim httpWebRequest = DirectCast(WebRequest.Create((New BackEndInterface.General).BackEndURL & URL), HttpWebRequest)
            httpWebRequest.ContentType = "application/json"
            httpWebRequest.Method = "POST"

            Using streamWriter = New StreamWriter(httpWebRequest.GetRequestStream())


                streamWriter.Write(JsonData)
                streamWriter.Flush()
                streamWriter.Close()
            End Using

            objhttpWebRequest = httpWebRequest

        Catch ex As Exception
            Console.WriteLine("Send Request Error[{0}]", ex.Message)

            Return Nothing
        End Try

        Return objhttpWebRequest

    End Function

    Private Function GetResponse(ByVal httpWebRequest As HttpWebRequest) As String
        Dim strResponse As String = "Bad Request:400"
        Try
            Dim httpResponse = DirectCast(httpWebRequest.GetResponse(), HttpWebResponse)
            Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                Dim result = streamReader.ReadToEnd()

                strResponse = result.ToString()
            End Using
        Catch ex As Exception
            Console.WriteLine("GetResponse Error[{0}]", ex.Message)

            Return ex.Message
        End Try

        Return strResponse

    End Function


    Public Class Generate_Order_Id

        Private Const SubURL As String = "/aftersales/order/generate-id" '---------- Channel=??? , Dealer =???

#Region "DataModel"

        Public Class Response

            Public Property JSONString As String
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

            Dim JSONString As String = (New BackEndInterface.General).SendGetURL(WebRequest)
            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)((New BackEndInterface.General).CleanJSONDash(JSONString, CleanKeys))
            Result.JSONString = JSONString
            Return Result
        End Function
    End Class

    Public Class Delete_File

        Private Const SubURL As String = "/sales/order/pdf/delete_file"

#Region "DataModel"
        Public Class Response

            Public Property JSONString As String
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
            GetString &= "form-type=CUST_ID_CARD"

            Dim URL As String = (New BackEndInterface.General).BackEndURL & SubURL & "?" & GetString
            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(URL)

            Dim JSONString As String = (New BackEndInterface.General).SendGetURL(WebRequest)
            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)((New BackEndInterface.General).CleanJSONDash(JSONString, CleanKeys))
            Result.JSONString = JSONString
            Return Result
        End Function

    End Class

    Public Class Save_File

        Private Const SubURL As String = "/sales/order/pdf/save_file"

#Region "DataModel"
        Public Class Response

            Public Property JSONString As String
            Public Property status As String
            Public Property fault As faultModel
            Public Property display_messages As List(Of display_message)
            Public Property trx_id As String
            Public Property process_instance As String
            Public Property ref_id As String
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
                                        "ref-id",
                                        "order-id"}

#End Region

        Public Function Get_Result(ByVal order_id As String,
                                   ByVal fileType As String,
                                   ByVal b64File As String) As Response

            Dim URL As String = (New BackEndInterface.General).BackEndURL & SubURL
            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(URL)

            Dim PostData As New Dictionary(Of String, String)
            PostData.Add("orderId", order_id)
            PostData.Add("fileType", fileType)
            PostData.Add("b64File", b64File)
            PostData.Add("formType", "CUST_ID_CARD")

            Dim JSONString As String = (New BackEndInterface.General).SendPostJSON(WebRequest, PostData)
            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)((New BackEndInterface.General).CleanJSONDash(JSONString, CleanKeys))
            Result.JSONString = JSONString
            Return Result
        End Function

    End Class

    Public Class Service_Flow_Create

        Private Const SubURL As String = "/sales/flow/create"

#Region "DataModel"
        Public Class Response

            Public Property JSONString As String
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
                                    ByVal staffOpenShift As String,
                                    ByVal thaiID As String,
                                    ByVal subscriber As String,
                                    ByVal shopCode As String,
                                    ByVal customerName As String,
                                    ByVal pricePlan As String,
                                    ByVal simSerial As String,
                                    ByVal face_recognition_result As String,
                                    ByVal is_identical As String,
                                    ByVal confident_ratio As String) As Response

            Dim GetString As String = ""
            GetString &= "flow-id=" & orderId & "&"
            GetString &= "flow-name=Mobile"

            Dim PostData As New Dictionary(Of String, String)
            PostData.Add("orderId", orderId)
            PostData.Add("flowName", "Mobile")
            PostData.Add("CREATE-BY", staffOpenShift)
            PostData.Add("ID-NUMBER", thaiID)
            PostData.Add("PRODUCT-ID-NUMBER", subscriber)
            PostData.Add("PARTNER-CODE", shopCode)
            PostData.Add("CUST-NAME", customerName)
            'PostData.Add("CAMPAIGN-CODE", "")
            'PostData.Add("CAMPAIGN-NAME", "")
            'PostData.Add("SERVICE-CODE", "TMV")
            PostData.Add("SERVICE-CODE", "Mobile")

            PostData.Add("PRODUCT-NAME", pricePlan)
            PostData.Add("SERIAL-NUMBER", simSerial)
            PostData.Add("SALE-CODE", staffOpenShift)
            PostData.Add("E-SIGNATURE", "NO")
            PostData.Add("READ-THAI-CARD", IIf(thaiID <> "", "YES", "NO"))
            PostData.Add("SUBSCRIBER", subscriber)
            PostData.Add("FACE_RECOFNITION_STATUS", face_recognition_result)
            PostData.Add("IS_IDENTICAL", is_identical)
            PostData.Add("CONFIDENT_RATIO", confident_ratio)
            PostData.Add("FACE_RECOG_CUST_CERTIFICATE_SOURCE", "READ_CARD")
            PostData.Add("FACE_RECOG_CUST_CAPTURE_SOURCE", "CAPTURE")
            'PostData.Add("CUST_CERTIFICATE_LASER_ID", "")
            PostData.Add("OS", "VENDING")

            Dim URL As String = (New BackEndInterface.General).BackEndURL & SubURL & "?" & GetString
            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(URL)

            Dim JSONString As String = (New BackEndInterface.General).SendPostJSON(WebRequest, PostData)
            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)((New BackEndInterface.General).CleanJSONDash(JSONString, CleanKeys))
            Result.JSONString = JSONString
            Return Result
        End Function

    End Class

    Public Class Service_Flow_Finish

        Private Const SubURL As String = "/sales/flow/finish"

#Region "DataModel"

        Public Class Response

            Public Property JSONString As String
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

            Dim GetString As String = "flow-id=" & orderId '& "&"
            'GetString &= "order-id=" & orderId

            Dim PostData As New Dictionary(Of String, String)
            PostData.Add("order-id", orderId)

            Dim URL As String = (New BackEndInterface.General).BackEndURL & SubURL & "?" & GetString
            Dim WebRequest As WebRequest = (New BackEndInterface.General).CreateRequest(URL)

            Dim JSONString As String = (New BackEndInterface.General).SendPostJSON(WebRequest, PostData)
            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)((New BackEndInterface.General).CleanJSONDash(JSONString, CleanKeys))
            Result.JSONString = JSONString
            Return Result

        End Function

    End Class

    Public Class Activity_Start

        Private Const SubURL As String = "/sales/flow/activity/start"

#Region "DataModel"
        Public Class Response

            Public Property JSONString As String
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

            Dim JSONString As String = (New BackEndInterface.General).SendPostJSON(WebRequest, PostData)
            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)((New BackEndInterface.General).CleanJSONDash(JSONString, CleanKeys))
            Result.JSONString = JSONString
            Return Result
        End Function

    End Class

    Public Class Activity_End

        Private Const SubURL As String = "/sales/flow/activity/end"

#Region "DataModel"
        Public Class Response

            Public Property JSONString As String
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

            Dim JSONString As String = (New BackEndInterface.General).SendPostJSON(WebRequest, PostData)
            Dim Result As Response = JsonConvert.DeserializeObject(Of Response)((New BackEndInterface.General).CleanJSONDash(JSONString, CleanKeys))
            Result.JSONString = JSONString
            Return Result
        End Function

    End Class


    Public Class Register

        Public Class Command_Result
            Public Property Status As String
            Public Property Message As String
        End Class
        Public Class CUSTOMER_INFO
            Public Property CUS_ID As String
            Public Property CUS_TITLE As String
            Public Property CUS_NAME As String
            Public Property CUS_SURNAME As String
            Public Property NAT_CODE As String
            Public Property CUS_GENDER As String
            Public Property CUS_BIRTHDATE As DateTime
            Public Property CUS_PID As String
            Public Property CUS_PASSPORT_ID As String
            Public Property CUS_PASSPORT_START As Date
            Public Property CUS_PASSPORT_EXPIRE As Date
            Public Property CUS_IMAGE As Image
        End Class

        '----------------- Function นี้ควร Return เป็น CommandResult จะได้ทั้งผลลัพธ์และ Error Message------------
        Public Function Get_Result(Face_cust_certificate As String, Face_cust_capture As String, SIM_Serial As String, KO_ID As Integer, USER_ID As Integer, TXN_ID As Integer, Optional ByVal LANGUAGE As VDM_BL.UILanguage = VDM_BL.UILanguage.TH, Optional ByVal Customer_IDCard As VDM_BL.Customer_IDCard = Nothing, Optional ByVal Customer_Passport As VDM_BL.Customer_Passport = Nothing) As Boolean
            Dim Result_Register As Boolean = True
            Dim SHOP_CODE As String = GET_SHOP_CODE(KO_ID)
            Dim Cust_Info As New CUSTOMER_INFO

            Dim Base64_Certificate As String = ""
            Dim Base64_capture As String = ""
            Dim address_number As String = "-"
            Dim address_moo As String = "-"
            Dim address_village As String = "-"
            Dim address_street As String = "-"
            Dim address_soi As String = "-"
            Dim address_district As String = "-"
            Dim address_province As String = "-"
            Dim address_building_name As String = "-"
            Dim address_building_room As String = "-"
            Dim address_building_floor As String = "-"
            Dim sddress_sub_district As String = "-"
            Dim address_zip As String = "-"
            Dim face_recognition_result As String = "-"
            Dim is_identical As String = "-"
            Dim confident_ratio As String = "-"

            Dim SQL As String = "SELECT * FROM TB_CUSTOMER_DOC WHERE CUS_ID=" & BL.GET_TXN_CUS_ID(TXN_ID)
            Dim DA As New SqlDataAdapter(SQL, BL.ConnectionString)
            Dim DT_Customer_Info As New DataTable
            DA.Fill(DT_Customer_Info)
            If DT_Customer_Info.Rows.Count > 0 Then

                Cust_Info.CUS_ID = DT_Customer_Info.Rows(0).Item("CUS_ID")
                Cust_Info.CUS_BIRTHDATE = IIf(Not IsDBNull(DT_Customer_Info.Rows(0).Item("CUS_BIRTHDATE")), DT_Customer_Info.Rows(0).Item("CUS_BIRTHDATE"), "-")
                Cust_Info.CUS_PID = IIf(Not IsDBNull(DT_Customer_Info.Rows(0).Item("PersonalID")), DT_Customer_Info.Rows(0).Item("PersonalID"), "")
                Cust_Info.CUS_PASSPORT_ID = IIf(Not IsDBNull(DT_Customer_Info.Rows(0).Item("PassportNo")), DT_Customer_Info.Rows(0).Item("PassportNo"), "")
                Cust_Info.CUS_PASSPORT_EXPIRE = IIf(Not IsDBNull(DT_Customer_Info.Rows(0).Item("Expire")), DT_Customer_Info.Rows(0).Item("Expire"), "-")
                Base64_Certificate = DT_Customer_Info.Rows(0).Item("DocPhoto").ToString
                Base64_capture = DT_Customer_Info.Rows(0).Item("DocPhoto").ToString
                face_recognition_result = DT_Customer_Info.Rows(0).Item("Face_Recognition_Result").ToString
                is_identical = DT_Customer_Info.Rows(0).Item("Face_Is_Identical").ToString
                confident_ratio = DT_Customer_Info.Rows(0).Item("Confident_Ratio").ToString

                If LANGUAGE = VDM_BL.UILanguage.TH Then
                    If Not IsNothing(Customer_IDCard) Then
                        Cust_Info.CUS_TITLE = IIf(Not IsDBNull(DT_Customer_Info.Rows(0).Item("Th_Prefix")), DT_Customer_Info.Rows(0).Item("Th_Prefix").ToString, "-")
                        Cust_Info.CUS_NAME = DT_Customer_Info.Rows(0).Item("Th_Firstname").ToString
                        Cust_Info.CUS_SURNAME = DT_Customer_Info.Rows(0).Item("Th_Lastname").ToString
                        Cust_Info.NAT_CODE = IIf(Not IsDBNull(DT_Customer_Info.Rows(0).Item("Nationality")), DT_Customer_Info.Rows(0).Item("Nationality"), "-")
                        Cust_Info.CUS_GENDER = IIf(Not IsDBNull(DT_Customer_Info.Rows(0).Item("Sex")), DT_Customer_Info.Rows(0).Item("Sex"), "0")

                        address_number = IIf(DT_Customer_Info.Rows(0).Item("addrHouseNo").ToString <> "", DT_Customer_Info.Rows(0).Item("addrHouseNo").ToString, "-")
                        address_moo = "-"
                        address_village = IIf(DT_Customer_Info.Rows(0).Item("addrVillageNo").ToString <> "", DT_Customer_Info.Rows(0).Item("addrVillageNo").ToString, "-")
                        address_street = IIf(DT_Customer_Info.Rows(0).Item("addrRoad").ToString <> "", DT_Customer_Info.Rows(0).Item("addrRoad").ToString, "-")
                        address_soi = "-"
                        address_district = IIf(DT_Customer_Info.Rows(0).Item("addrTambol").ToString <> "", DT_Customer_Info.Rows(0).Item("addrTambol").ToString, "-")
                        address_province = IIf(DT_Customer_Info.Rows(0).Item("addrProvince").ToString <> "", DT_Customer_Info.Rows(0).Item("addrProvince").ToString, "-")
                        address_building_name = "-"
                        address_building_room = "-"
                        address_building_floor = "-"
                        sddress_sub_district = "-"
                        address_zip = "-"
                    End If
                Else
                    If Not IsNothing(Customer_Passport) Then
                        Cust_Info.CUS_TITLE = IIf(Not IsDBNull(DT_Customer_Info.Rows(0).Item("En_Prefix")), DT_Customer_Info.Rows(0).Item("En_Prefix").ToString, "-")
                        Cust_Info.CUS_NAME = DT_Customer_Info.Rows(0).Item("En_Firstname").ToString
                        Cust_Info.CUS_SURNAME = DT_Customer_Info.Rows(0).Item("En_Lastname").ToString
                        Cust_Info.NAT_CODE = IIf(Not IsDBNull(DT_Customer_Info.Rows(0).Item("Nationality")), DT_Customer_Info.Rows(0).Item("Nationality"), "-")
                        If DT_Customer_Info.Rows(0).Item("Sex").ToString = "F" Then
                            Cust_Info.CUS_GENDER = 2
                        Else
                            Cust_Info.CUS_GENDER = 1
                        End If
                        address_number = "-"
                        address_moo = "-"
                        address_village = "-"
                        address_street = "-"
                        address_soi = "-"
                        address_district = "-"
                        address_province = "-"
                        address_building_name = "-"
                        address_building_room = "-"
                        address_building_floor = "-"
                        sddress_sub_district = "-"
                        address_zip = "-"
                    End If
                End If
            End If
            Dim Result As New Command_Result

#Region "Face_Recognition"

#End Region

#Region "Prepaid_Validate_Register"

            Dim BackEndPrepaid_Validate_Register As New Prepaid_Validate_Register
            Dim Response_Prepaid_Validate As New BackEndInterface.Prepaid_Validate_Register.Response
            Response_Prepaid_Validate = BackEndPrepaid_Validate_Register.Get_Result(SIM_Serial, IIf(Not IsDBNull(Cust_Info.CUS_PID), Cust_Info.CUS_PID, Cust_Info.CUS_PASSPORT_ID), IIf(Not IsDBNull(Cust_Info.CUS_PID), "I", "P"))
            Try
                If Not IsNothing(Response_Prepaid_Validate) Then
                    Result.Status = Response_Prepaid_Validate.status
                    Result.Message = "CUS_ID:" & IIf(Not IsDBNull(Cust_Info.CUS_PID), Cust_Info.CUS_PID, Cust_Info.CUS_PASSPORT_ID) & " Type:" & IIf(Not IsDBNull(Cust_Info.CUS_PID), "I", "P") & " SIM_Serial:" & SIM_Serial & " Step Prepaid_Validate_Register"
                End If
            Catch ex As Exception
                Result.Status = Response_Prepaid_Validate.status
                Result.Message = "CUS_ID:" & IIf(Not IsDBNull(Cust_Info.CUS_PID), Cust_Info.CUS_PID, Cust_Info.CUS_PASSPORT_ID) & " Type:" & IIf(Not IsDBNull(Cust_Info.CUS_PID), "I", "P") & " SIM_Serial:" & SIM_Serial & "Step Prepaid_Validate_Register" & ex.Message
                Result_Register = False
                Return Result_Register
                Exit Function
            End Try
#End Region

#Region "Generate_Order_Id"
            Dim BackEndGenerate_Order_Id As New Generate_Order_Id
            Dim Response_Generate_Order_Id As New BackEndInterface.Generate_Order_Id.Response
            Response_Generate_Order_Id = BackEndGenerate_Order_Id.Get_Result(SHOP_CODE)
            Try

                If Not IsNothing(Response_Generate_Order_Id) Then
                    Result.Status = Response_Generate_Order_Id.status
                    Result.Message = "CUS_ID:" & IIf(Not IsDBNull(Cust_Info.CUS_PID), Cust_Info.CUS_PID, Cust_Info.CUS_PASSPORT_ID) & " Type:" & IIf(Not IsDBNull(Cust_Info.CUS_PID), "I", "P") & " SIM_Serial:" & SIM_Serial & " Step Generate_Order_Id"
                End If
            Catch ex As Exception
                Result.Status = Response_Generate_Order_Id.status
                Result.Message = "ORDER_ID:" & Response_Generate_Order_Id.response_data & " Step Generate_Order_Id " & ex.Message
                Result_Register = False
                Return Result_Register
                Exit Function
            End Try
#End Region



#Region "Delete_File"
            Dim BackEndDelete_File As New Delete_File
            Dim Response_Delete_File As New BackEndInterface.Delete_File.Response
            Response_Delete_File = BackEndDelete_File.Get_Result(Response_Generate_Order_Id.response_data)
            Try
                If Not IsNothing(Response_Delete_File) Then
                    Result.Status = Response_Delete_File.status
                    Result.Message = "ORDER_ID:" & Response_Generate_Order_Id.response_data & " Step Generate_Order_Id"
                End If
            Catch ex As Exception
                Result.Status = Response_Delete_File.status
                Result.Message = "ORDER_ID:" & Response_Generate_Order_Id.response_data & " Step Generate_Order_Id " & ex.Message
                Result_Register = False
                Return Result_Register
                Exit Function
            End Try

#End Region

#Region "Save_File"
            Dim C As New Converter
            Dim Merge As Image  '--Merg บัตรประชาชนกับภาพ
            Merge = CombineImages(C.BlobToImage(Base64_Certificate.ToString()), C.BlobToImage(Base64_capture.ToString()), SHOP_CODE, Response_Prepaid_Validate.response_data.subscriber)
            Dim Merge_bytes As Byte() = C.ImageToByte(Merge)

            Dim BackEndSave_File As New Save_File
            Dim Response_Save_File As New BackEndInterface.Save_File.Response
            Response_Save_File = BackEndSave_File.Get_Result(Response_Generate_Order_Id.response_data, "PNG", Convert_Base64(Merge_bytes))
            Try

                If Not IsNothing(Response_Save_File) Then
                    Result.Status = Response_Save_File.status
                    Result.Message = "ORDER_ID:" & Response_Generate_Order_Id.response_data & " Step Save_File"
                End If

            Catch ex As Exception
                Result.Status = Response_Save_File.status
                Result.Message = "ORDER_ID:" & Response_Generate_Order_Id.response_data & " Step Save_File " & ex.Message
                Result_Register = False
                Return Result_Register
                Exit Function
            End Try

#End Region

#Region "Service_Flow_Create"
            Dim BackEndFlow_Create As New Service_Flow_Create
            Dim Response_Flow_Create As New BackEndInterface.Service_Flow_Create.Response
            Response_Flow_Create = BackEndFlow_Create.Get_Result(
                                    Response_Generate_Order_Id.response_data,
                                    USER_ID,
                                    IIf(Not IsDBNull(Cust_Info.CUS_PID), Cust_Info.CUS_PID, Cust_Info.CUS_PASSPORT_ID),
                                    Response_Prepaid_Validate.response_data.subscriber,
                                    SHOP_CODE,
                                    Cust_Info.CUS_NAME & " " & Cust_Info.CUS_SURNAME,
                                    Response_Prepaid_Validate.response_data.priceplan,
                                    SIM_Serial,
                                   face_recognition_result,
                                   is_identical,
                                   confident_ratio)

            Try

                If Not IsNothing(Response_Flow_Create) Then
                    Result.Status = Response_Flow_Create.status
                    Result.Message = "ORDER_ID:" & Response_Generate_Order_Id.response_data & " Step Service_Flow_Create"
                End If
            Catch ex As Exception
                Result.Status = Response_Flow_Create.status
                Result.Message = "ORDER_ID:" & Response_Generate_Order_Id.response_data & " Step Service_Flow_Create " & ex.Message
                Result_Register = False
                Return Result_Register
                Exit Function
            End Try


#End Region

#Region "Activity_Start"
            Dim BackEndActivity_Start As New Activity_Start
            Dim Response_Activity_Start As New BackEndInterface.Activity_Start.Response
            Response_Activity_Start = BackEndActivity_Start.Get_Result(Response_Generate_Order_Id.response_data)
            Try
                If Not IsNothing(Response_Activity_Start) Then
                    Result.Status = Response_Activity_Start.status
                    Result.Message = "ORDER_ID:" & Response_Generate_Order_Id.response_data & " Step Activity_Start"
                End If
            Catch ex As Exception
                Result.Status = Response_Activity_Start.status
                Result.Message = "ORDER_ID:" & Response_Generate_Order_Id.response_data & " Step Activity_Start " & ex.Message
                Result_Register = False
                Return Result_Register
                Exit Function
            End Try
#End Region

#Region "Activity_End"
            Dim BackEndActivity_End As New Activity_End
            Dim Response_Activity_End As New BackEndInterface.Activity_End.Response
            Response_Activity_End = BackEndActivity_End.Get_Result(Response_Generate_Order_Id.response_data)
            Try
                If Not IsNothing(Response_Activity_End) Then
                    Result.Status = Response_Activity_End.status
                    Result.Message = "ORDER_ID:" & Response_Generate_Order_Id.response_data & " Step Activity_End"
                End If
            Catch ex As Exception
                Result.Status = Response_Activity_End.status
                Result.Message = "ORDER_ID:" & Response_Generate_Order_Id.response_data & " Step Activity_End " & ex.Message
                Result_Register = False
                Return Result_Register
                Exit Function
            End Try
#End Region

#Region "Service_Flow_Finish"
            Dim BackEndFlow_Finish As New Service_Flow_Finish
            Dim Response_Flow_Finish As New BackEndInterface.Service_Flow_Finish.Response
            Response_Flow_Finish = BackEndFlow_Finish.Get_Result(Response_Generate_Order_Id.response_data)
            Try
                If Not IsNothing(Response_Flow_Finish) Then
                    Result.Status = Response_Flow_Finish.status
                    Result.Message = "ORDER_ID:" & Response_Generate_Order_Id.response_data & " Step Service_Flow_Finish"
                End If
            Catch ex As Exception
                Result.Status = Response_Flow_Finish.status
                Result.Message = "ORDER_ID:" & Response_Generate_Order_Id.response_data & " Step Service_Flow_Finish " & ex.Message
                Result_Register = False
                Return Result_Register
                Exit Function
            End Try
#End Region

#Region "Prepaid_Register"

            Dim DT_TXN_MAT_CODE As DataTable = BL.GET_TXN_PREPAID_REGISTER_PAID(TXN_ID)
            'Dim mat_code As String = "3000014920" 
            'Dim mat_desc As String = "ซิมซูเปอร์ฟัน ฟอร์ทีน"
            Dim mat_code As String = ""
            Dim mat_desc As String = ""
            If DT_TXN_MAT_CODE.Rows.Count > 0 Then
                mat_code = DT_TXN_MAT_CODE.Rows(0).Item("PRODUCT_CODE").ToString
                mat_desc = DT_TXN_MAT_CODE.Rows(0).Item("PRODUCT_NAME").ToString
            End If

            Dim _EXPIRE As String = ""
            If Cust_Info.CUS_PASSPORT_EXPIRE.ToString() <> "-" Then
                _EXPIRE = C.DateToString(Cust_Info.CUS_PASSPORT_EXPIRE, "yyyy-MM-dd'T'HH:mm:ss'+0700'")
            Else
                _EXPIRE = "-"
            End If
            'C.DateToString(Cust_Info.CUS_PASSPORT_EXPIRE, "yyyy-MM-dd'T'HH:mm:ss'+0700'"), '1990-02-05T00:00:00+0700  customer_id_expire_date

            Dim BackEndPrepaid_Register As New Prepaid_Register
            Dim Response_Prepaid_Register As New BackEndInterface.Prepaid_Register.Response
            Response_Prepaid_Register = BackEndPrepaid_Register.Get_Result(Response_Generate_Order_Id.response_data,
                Cust_Info.CUS_GENDER, '--ดูว่าบัตรส่งมาเป็นอะไร
                Cust_Info.CUS_TITLE,
                BL.Get_Language_Code(LANGUAGE).ToString(), 'Cust_Info.NAT_CODE
                "T5",
                Cust_Info.CUS_NAME,
                Cust_Info.CUS_SURNAME,
                C.DateToString(Cust_Info.CUS_BIRTHDATE, "yyyy-MM-dd'T'HH:mm:ss'+0700'"), '1990-02-05T00:00:00+0700  customer_birthdate
                IIf(Not IsDBNull(Cust_Info.CUS_PID), Cust_Info.CUS_PID, Cust_Info.CUS_PASSPORT_ID),
                _EXPIRE,
                address_number,
                address_moo,
                address_village,
                address_street,
                address_soi,
                address_district,
                address_province,
                address_building_name,
                address_building_room,
                address_building_floor,
                sddress_sub_district,
                address_zip,
                SHOP_CODE, 'shopCode.Text,
                "-", 'shopName.Text,
                USER_ID,
                mat_code,  'mat_code.Text,
                mat_desc,  'mat_desc.Text,
                SIM_Serial,
                False,
                Response_Prepaid_Validate.response_data.priceplan,
                Response_Prepaid_Validate.response_data.subscriber,
                0,' "N"
                2,  '"PAIR"
                Response_Prepaid_Validate.response_data.company_code)

            Dim SQL_Update As String = ""
            Try
                If Not IsNothing(Response_Prepaid_Register) Then
                    Result.Status = Response_Prepaid_Register.status
                    Result.Message = "ORDER_ID:" & Response_Generate_Order_Id.response_data & " Step Prepaid_Register"

                End If
            Catch ex As Exception
                Result.Status = Response_Prepaid_Register.status
                Result.Message = "ORDER_ID:" & Response_Generate_Order_Id.response_data & " Step Prepaid_Register " & ex.Message
                Result_Register = False

                SQL_Update = "UPDATE TB_SERVICE_TRANSACTION SET TSM_ORDER_ID='fail' ,  TSM_Result='" & Response_Prepaid_Register.JSONString & "' WHERE TXN_ID=" & TXN_ID
                BL.ExecuteNonQuery(SQL_Update)
                Return Result_Register
                Exit Function
            End Try

            'Update TB_SERVICE_TRANSACTION
            SQL_Update = "UPDATE TB_SERVICE_TRANSACTION SET TSM_ORDER_ID='" & Response_Generate_Order_Id.response_data & "',  TSM_Result='" & Response_Prepaid_Register.JSONString & "' WHERE TXN_ID=" & TXN_ID
            BL.ExecuteNonQuery(SQL_Update)
#End Region

            Return Result_Register
        End Function

        Public Function Convert_Base64(Image As Byte()) As String
            Dim base64String As String = Convert.ToBase64String(Image, 0, Image.Length)
            Return base64String
        End Function
        Dim BL As New VDM_BL
        Public Function GET_SHOP_CODE(KO_ID As Integer) As Integer
            Dim SHOP_CODE As String = ""
            Dim DT As DataTable = BL.GetList_Kiosk(KO_ID)
            If DT.Rows.Count > 0 Then
                SHOP_CODE = DT.Rows(0).Item("SITE_CODE")
            End If
            Return SHOP_CODE
        End Function

        Public Function CombineImages(ByVal img1 As Image, ByVal img2 As Image, Shop_Code As String, Subscriber As String) As Image
            Dim bmp As New Bitmap(Math.Max(img1.Width, img2.Width), img1.Height + img2.Height)
            Dim g As Graphics = Graphics.FromImage(bmp)

            g.DrawImage(img1, 0, 0, img1.Width, img1.Height)
            g.DrawImage(img2, 0, img1.Height, img2.Width, img2.Height)

            Dim theString As String = "ใช้สำหรับลงทะเบียนเบอร์โทรศัพท์ ทรู เท่านั้น" & vbNewLine & "dealer:" & Shop_Code & vbNewLine & "หมายเลขโทรศัพท์:" & Subscriber
            Dim the_font As Font = New Font("Comic Sans MS", 35, FontStyle.Bold)
            'g.RotateTransform(-45)
            Dim sz As SizeF = g.VisibleClipBounds.Size
            sz = g.MeasureString(theString, the_font)
            g.DrawString(theString, the_font, Brushes.Red, 10, (img1.Height) - 50)
            g.ResetTransform()


            g.Dispose()

            Return bmp
        End Function


    End Class








End Class
