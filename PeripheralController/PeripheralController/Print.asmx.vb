Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Printer

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class Print
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function Print_Confirmation_Slip(ByVal TXN_CODE As String) As Boolean
        Dim _Printer As New Printer.Printer


        Return True
    End Function


End Class