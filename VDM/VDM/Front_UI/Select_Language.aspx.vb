Public Class Select_Language
    Inherits System.Web.UI.Page
#Region "ส่วนที่เหมือนกันหมดทุกหน้า"
    Private ReadOnly Property KO_ID As Integer '------------- เอาไว้เรียกใช้ง่ายๆ ----------
        Get
            Try
                Return Request.Cookies("KO_ID").Value
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Private Property LANGUAGE As VDM_BL.UILanguage '-------- หน้าอื่นๆต้องเป็น ReadOnly --------
        Get
            Return Session("LANGUAGE")
        End Get
        Set(value As VDM_BL.UILanguage)
            Session("LANGUAGE") = value
        End Set
    End Property

    Public WriteOnly Property TXN_ID As Integer '-------- หน้าอื่นๆต้องเป็น ReadOnly : Return Session("TXN_ID")--------
        Set(value As Integer)
            Session("TXN_ID") = value
        End Set
    End Property

    Private ReadOnly Property POSTER_TYPE As String
        Get
            Try
                If Request.Cookies("POSTER_TYPE").Value.ToUpper = "MP4" Then
                    Return Request.Cookies("POSTER_TYPE").Value.ToUpper
                Else
                    Return "PNG"
                End If
            Catch ex As Exception
                Return "PNG"
            End Try
        End Get
    End Property
#End Region

    Dim BL As New VDM_BL
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        '---------- Start New Transaction After This Page----------
        Session.Remove("TXN_ID")
            Session.Remove("LANGUAGE")
            Session.Remove("Accept_Contact_EMP")
            Session.Remove("Customer_IDCard")
        Session.Remove("Customer_Passport")


        ''----Image 
        If POSTER_TYPE = "MP4" Then
            'video_tvc.Src = BL.PicturePath & "\Poster\TVC\" & KO_ID & ".mp4"
            'Poster_Img.Src = BL.PicturePath & "\Poster\Image\pic-home-tvc.png"
            video_tvc.Src = "Poster\tvc\" & KO_ID & ".mp4"
            Poster_Img.Src = "Poster\image\pic-home-tvc.png"
        Else
            div_video.Visible = False
            Poster_Img.Src = "Poster\image\" & KO_ID & ".png"
        End If

    End Sub

    Private Sub TH_ServerClick(sender As Object, e As EventArgs) Handles TH.ServerClick
        StartTransaction(VDM_BL.UILanguage.TH)
    End Sub
    Private Sub EN_ServerClick(sender As Object, e As EventArgs) Handles EN.ServerClick
        StartTransaction(VDM_BL.UILanguage.EN)
    End Sub

    Private Sub CN_ServerClick(sender As Object, e As EventArgs) Handles CN.ServerClick
        StartTransaction(VDM_BL.UILanguage.CN)
    End Sub

    Private Sub JP_ServerClick(sender As Object, e As EventArgs) Handles JP.ServerClick
        StartTransaction(VDM_BL.UILanguage.JP)
    End Sub

    Private Sub KR_ServerClick(sender As Object, e As EventArgs) Handles KR.ServerClick
        StartTransaction(VDM_BL.UILanguage.KR)
    End Sub

    Private Sub RU_ServerClick(sender As Object, e As EventArgs) Handles RU.ServerClick
        StartTransaction(VDM_BL.UILanguage.RS)
    End Sub

    Public Sub StartTransaction(ByVal SelectedLanguage As VDM_BL.UILanguage)
        Dim BL As New VDM_BL
        TXN_ID = BL.Gen_New_Service_Transaction(KO_ID, SelectedLanguage)
        LANGUAGE = SelectedLanguage
        Response.Redirect("Select_Menu.aspx")
    End Sub

End Class