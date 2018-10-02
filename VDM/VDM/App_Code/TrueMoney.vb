Imports Newtonsoft.Json
Imports System.Configuration.ConfigurationManager
Imports System.Net
Imports System.IO
Imports Org.BouncyCastle.Crypto
Imports Org.BouncyCastle.OpenSsl
Imports Org.BouncyCastle.Security
Imports Org.BouncyCastle.Crypto.Parameters
Imports System.Security.Cryptography
Imports System.Data.SqlClient

Public Class TrueMoney

    Dim ServerMapPath As String = AppSettings("ServerMapPath").ToString
    Dim TrueMoneyURL As String = AppSettings("TrueMoneyURL").ToString
    Dim XAPIKey As String = AppSettings("TrueMoneyXAPIKey").ToString
    Dim TrueMoneyMerchantID As String = AppSettings("TrueMoneyMerchantID").ToString

    Dim C As New Converter

    Public Class Response

        Public Property status As ResponseStatus
        Public Property data As ResponseData

        '---------- For Tracking And Debuging ---------
        Public Property Request As RequestData
        Public Property JSONString As String
        Public Property ConnectionMessage As String
        Public Property REQ_ID As Integer

        Public Class ResponseStatus
            Public Property code As String
            Public Property message As String
        End Class

        Public Class ResponseData
            Public Property payment_id As String
        End Class

        '------------- ส่วนนี้ัตัดได้ -------------
        Public Class RequestData
            '--------------Header----------------------
            Public Property X_API_Key As String
            Public Property X_API_Version As String
            Public Property Content_Signature As String
            Public Property TIMESTAMP As String
            Public Property Content_type As String
            '-----------------Post Data-------------------
            Public Property currency As String
            Public Property payment_code As String
            Public Property isv_payment_ref As String
            Public Property description As String
            Public Property payment_method As String
            Public Property merchant_id As String
            Public Property request_amount As String
            '----------------JSON String------------------
            Public Property PostString As String

        End Class

    End Class

    Public Function GetResult(ByVal ISV As String, ByVal Amount As Integer, ByVal CustomerQRCode As String, ByVal PaymentDescription As String, ByVal shopCode As String) As Response


        Dim request_amount As String = FormatNumber(Amount).Replace(".", "").Replace("-", "").Replace(",", "")
        Dim X_API_Version As String = "1.0"
        Dim payment_method As String = "BALANCE"
        '--------------- Create Request Body -----------
        Dim PostData As New Dictionary(Of String, String)
        PostData.Add("currency", "THB")
        PostData.Add("payment_code", CustomerQRCode)
        PostData.Add("isv_payment_ref", ISV)
        PostData.Add("description", PaymentDescription)
        PostData.Add("payment_method", payment_method)
        PostData.Add("merchant_id", TrueMoneyMerchantID)
        PostData.Add("request_amount", request_amount)

        Dim PostString As String = JsonConvert.SerializeObject(PostData, Formatting.Indented)

        Dim Req As WebRequest = WebRequest.Create(TrueMoneyURL)
        '--------------- Config Web Request ------------
        Dim TimeStamp As String = C.DateToEpoch(Now).ToString
        Dim Signature As String = CreateSignature(TimeStamp, PostString)
        Dim WaitMinutes As Integer = 11 '-------- รอกี่นาที ---------


        Req.Method = "POST"
        Req.Timeout = WaitMinutes * (60000)
        ''--------------- Config Header -----------------
        Req.Headers.Add("X-API-Key", XAPIKey)
        Req.Headers.Add("X-API-Version", X_API_Version)
        Req.Headers.Add("Content-Signature", Signature)
        Req.Headers.Add("TIMESTAMP", TimeStamp)
        Req.ContentType = "applicaton/json"

        '------------------- Save Log ----------------
        Dim BL As New VDM_BL
        '---------------- Save REQ Log ---------------
        Dim DR As DataRow
        Dim REQ_ID As Integer = BL.Get_NewID_Log("TrueMoney_Payment_REQ", "REQ_ID")
        Dim SQL As String = "SELECT TOP 0 * FROM TrueMoney_Payment_REQ"
        Dim DA As New SqlDataAdapter(SQL, BL.LogConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        DR = DT.NewRow : DT.Rows.Add(DR)
        DR("REQ_ID") = REQ_ID
        DR("X_API_Key") = XAPIKey
        DR("X_API_Version") = Req.Headers.Item(X_API_Version)
        DR("Content_Signature") = Signature
        DR("TIMESTAMP") = TimeStamp
        DR("Content_type") = Req.ContentType
        DR("currency") = "THB"
        DR("payment_code") = CustomerQRCode
        DR("isv_payment_ref") = ISV
        DR("description") = PaymentDescription
        DR("payment_method") = payment_method
        DR("merchant_id") = TrueMoneyMerchantID
        DR("request_amount") = request_amount
        DR("PostString") = PostString
        DR("REQ_Time") = Now
        Dim cmd As New SqlCommandBuilder(DA)
        DA.Update(DT)
        '---------------------------------------

        Dim Data As Byte() = C.StringToByte(PostString, Converter.EncodeType._UTF8)
        Req.ContentLength = Data.Length
        Dim ST As Stream = Req.GetRequestStream()
        ST.Write(Data, 0, Data.Length)
        ST.Close()

        Dim JSONString As String = ""
        '-------------- Try Request ------------
        Dim Result As New Response
        Try
            Dim Resp As Stream = Req.GetResponse().GetResponseStream()
            Dim Reader As New StreamReader(Resp)
            JSONString = Reader.ReadToEnd()
            Reader.Close()
            Resp.Close()
            Result = JsonConvert.DeserializeObject(Of Response)(JSONString)
            Result.ConnectionMessage = "Success"
        Catch ex As Exception
            Result = New Response
            Result.ConnectionMessage = ex.Message
            '----------------- Save Error Log -------------
            Dim BackEnd As New BackEndInterface.General
            BackEnd.UPDATE_LOG_ERROR_MESSAGE("TrueMoney_Payment_REQ", REQ_ID, ex.Message)
        End Try

        Result.Request = New Response.RequestData
        With Result.Request
            .X_API_Key = Req.Headers.Item("X-API-Key").ToString
            .X_API_Version = Req.Headers.Item("X-API-Version").ToString
            .Content_Signature = Req.Headers.Item("Content-Signature").ToString
            .TIMESTAMP = Req.Headers.Item("TIMESTAMP").ToString
            .Content_type = Req.ContentType
            '-----------------------------
            .currency = PostData("currency").ToString
            .payment_code = PostData("payment_code").ToString
            .isv_payment_ref = PostData("isv_payment_ref").ToString
            .description = PostData("description").ToString
            .payment_method = PostData("payment_method").ToString
            .merchant_id = PostData("merchant_id").ToString
            .request_amount = PostData("request_amount").ToString
            '-----------------------------
            .PostString = PostString
        End With
        '-----------------------------
        Result.JSONString = JSONString

        '---------------- Save RESP Log ---------------
        SQL = "SELECT TOP 0 * FROM TrueMoney_Payment_RESP"
        DA = New SqlDataAdapter(SQL, BL.LogConnectionString)
        DT = New DataTable
        DA.Fill(DT)
        DR("REQ_ID") = REQ_ID
        DR("code") = Result.status.code
        DR("message") = Result.status.message
        DR("payment_id") = Result.data.payment_id
        DR("JSONString") = Result.JSONString
        DR("ConnectionMessage") = Result.ConnectionMessage
        DR("RESP_TIME") = Now
        cmd = New SqlCommandBuilder(DA)
        DA.Update(DT)
        '----------------------------------------------

        Return Result

    End Function

    Private Function CreateSignature(ByVal TimeStamp As String, ByVal RequestBody As String) As String
        Return "digest-alg=RSA-SHA; key-id=KEY:RSA:rsf.org; data=" & Sign(TimeStamp & RequestBody)
    End Function

    Public Function Generate_ISV(ByVal ShopCode As String) As String ' Confirm จาก True
        Return ShopCode & "-" & Now.ToString("yyMMddhhmmssfff")
    End Function

#Region "Crypto And Key"

    Private ReadOnly Property TMN_PUBLIC_KEY As String
        Get
            Dim TMNKeyPath As String = ServerMapPath & "\bin\Key\TrueMoney.pem"
            Return C.ByteToString(ReadFile(TMNKeyPath), Converter.EncodeType._UTF8)
        End Get
    End Property

    Private Function _verify(ByVal buffer As Byte(), ByVal sign As Byte()) As Boolean
        Dim key As RsaKeyParameters = PublicKeyFactory.CreateKey(Convert.FromBase64String(TMN_PUBLIC_KEY))
        Dim param As RSAParameters = New RSAParameters With {
            .Modulus = key.Modulus.ToByteArrayUnsigned(),
            .Exponent = key.Exponent.ToByteArrayUnsigned()
        }

        Using csp = New RSACryptoServiceProvider()
            csp.ImportParameters(param)
            Return csp.VerifyData(buffer, CryptoConfig.MapNameToOID("SHA256"), sign)
        End Using
    End Function

    Private Function readPrivateKey() As AsymmetricKeyParameter
        Dim privateKeyPath As String = ServerMapPath & "\bin\Key\partner_private_key.pem"
        Dim privateKeyContent As String = C.ByteToString(ReadFile(privateKeyPath), Converter.EncodeType._UTF8)
        Dim Reader As New StringReader(privateKeyContent)
        Dim keyPair As AsymmetricCipherKeyPair = New PemReader(Reader).ReadObject()
        Return keyPair.Private
    End Function

    Private Function Sign(ByVal data As String) As String ', ByVal privateModulusHexString As String, ByVal privateExponentHexString As String) As String
        Dim key As AsymmetricKeyParameter = readPrivateKey()
        key = New RsaKeyParameters(key.IsPrivate, (CType(key, RsaPrivateCrtKeyParameters)).Modulus, (CType(key, RsaPrivateCrtKeyParameters)).Exponent)
        Dim sig As ISigner = SignerUtilities.GetSigner("SHA256WITHRSA")
        sig.Init(True, key)
        Dim bytes As Byte() = C.StringToByte(data, Converter.EncodeType._UTF8)
        sig.BlockUpdate(bytes, 0, bytes.Length)
        Dim signature As Byte() = sig.GenerateSignature()
        Dim signedString = Convert.ToBase64String(signature)
        Return signedString
    End Function


#End Region



End Class
