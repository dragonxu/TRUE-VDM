﻿Public Class Select_Language
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Session("KO_ID") = 1
        Session("LANGUAGE") = VDM_BL.UILanguage.TH
    End Sub

End Class