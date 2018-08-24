Imports VDM.BackEndInterface

Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim PD As New Get_Product_Info
        ''3000013633'----------- Device ----------
        ''3000013631'
        ''3000024133'
        ''3000050987'
        ''3000051011'
        ''3000036094'
        ''3000001852'
        ''3000050979'
        ''3000011962'
        'Dim Resp As Get_Product_Info.Response = PD.Get_Result("3000007057") '----------- Samsung ----------
        ''3000007057'----------- SIM ----------

        Dim PP As New Generate_Order_Id
        Dim Resp As Generate_Order_Id.Response = PP.Get_Result("18082400")


    End Sub

End Class