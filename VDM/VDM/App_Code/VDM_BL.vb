Imports System.Configuration.ConfigurationManager

Public Class VDM_BL

    Public BaseURL As String = AppSettings("BackEndURL").ToString
    Public ConnectionString As String = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString

End Class
