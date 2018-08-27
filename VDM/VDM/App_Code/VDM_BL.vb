Imports System.Configuration.ConfigurationManager

Public Class VDM_BL

    Public ConnectionString As String = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString

End Class
