Imports Newtonsoft.Json
Imports System.Configuration.ConfigurationManager
Imports System.Net
Imports System.IO
Imports Org.BouncyCastle.Crypto
Imports Org.BouncyCastle.OpenSsl
Imports Org.BouncyCastle.Security
Imports Org.BouncyCastle.Crypto.Parameters
Imports System.Security.Cryptography

Public Class TrueMoney

    Dim ServerMapPath As String = AppSettings("ServerMapPath").ToString
    Dim C As New Converter

    Public ReadOnly Property URL As String
        Get
            Return AppSettings("TrueMoneyURL").ToString
        End Get
    End Property

    Public Function CreateRequest(ByVal URL As String) As WebRequest

        Dim webReq As WebRequest = WebRequest.Create(URL)
        '--------------- Config Web Request ------------
        webReq.Method = "POST"
        webReq.Timeout = 10000
        webReq.Headers.Add("X-Correlation-Id", (New Guid()).ToString)
        'webReq.Headers.Add("X-API-Key", xxxxxxxxxxxxxxxxxxxxxxx)
        webReq.Headers.Add("X-API-Version", "1.0")
        'webReq.Headers.Add("Content-Signature", xxxxxxxxxxxxxxxxxxxxxxx)
        webReq.Headers.Add("TIMESTAMP", C.DateToEpoch(Now).ToString)
        webReq.Headers.Add("Content-type", "applicaton/json")
        Return webReq
    End Function



#Region "Crypto And Key"

    Private ReadOnly Property TMN_PUBLIC_KEY As String
        Get
            Dim TMNKeyPath As String = ServerMapPath & "\bin\Key\TrueMoney.pem"
            Return "Waiting For True Money"
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
        Dim _privateKey As String = ServerMapPath & "\bin\Key\partner_private_key.pem"
        Dim reader As New StringReader(_privateKey)
        Dim keyPair As AsymmetricCipherKeyPair = New PemReader(reader).ReadObject()
        Return keyPair.Private
    End Function

    Public Function Sign(ByVal data As String, ByVal privateModulusHexString As String, ByVal privateExponentHexString As String) As String
        Dim key = readPrivateKey()
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
