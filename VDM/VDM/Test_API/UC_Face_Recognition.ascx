<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Face_Recognition.ascx.vb" Inherits="VDM.UC_Face_Recognition" %>
<table>
    <tr>
                        <td><h4 style="text-align :center ;">Request</h4></td>
                        <td>&nbsp;</td>
                        <td><h4 style="text-align :center ;">Response</h4></td>
                    </tr>

    <tr style ="vertical-align :top;">
        <td style="line-height: 2;">  
                <table>                   

                    <tr>
                        <td>partner_code</td>
                        <td><asp:TextBox ID="partner_code" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>id_number</td>
                        <td><asp:TextBox ID="id_number" runat="server"></asp:TextBox></td>

                    </tr>
                    <tr>
                        <td>face_recog_cust_certificate</td>
                        <td><asp:TextBox ID="face_recog_cust_certificate" runat="server" TextMode ="MultiLine" Style ="margin: 0px; width: 161px; height: 131px;" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>face_recog_cust_capture</td>
                        <td><asp:TextBox ID="face_recog_cust_capture" runat="server"  TextMode ="MultiLine"  Style ="margin: 0px; width: 161px; height: 131px;" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>seq</td>
                        <td><asp:TextBox ID="seq" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td> </td>
                        <td><asp:Button ID="btn_Request" runat="server" Text="OK" /></td>
                    </tr>
                </table>
        </td>
        <td >&nbsp;</td>
        <td style="line-height: 2;"> 
            {<br />
                "status": <asp:TextBox ID="status" runat="server"></asp:TextBox>,<br />
                "trx-id": <asp:TextBox ID="trx_id" runat="server"></asp:TextBox>,<br />
                "process-instance": <asp:TextBox ID="process_instance" runat="server"></asp:TextBox>,<br />
                "response-data": {<br />
                    "face-recognition-result" : <asp:TextBox ID="face_recognition_result" runat="server"></asp:TextBox>,<br />
                    "face-recognition-message" : <asp:TextBox ID="face_recognition_message" runat="server"></asp:TextBox>,<br />
                    "over-max-allow" : <asp:TextBox ID="over_max_allow" runat="server"></asp:TextBox>,<br />
                    "over-max-allow-message" : <asp:TextBox ID="over_max_allow_message" runat="server"></asp:TextBox>,<br />
                    "is-identical" : <asp:TextBox ID="is_identical" runat="server"></asp:TextBox>,<br />
                    "confident-ratio" : <asp:TextBox ID="confident_ratio" runat="server"></asp:TextBox>,<br />
                    "face-recog-cust-certificate-id" : <asp:TextBox ID="face_recog_cust_certificate_id" runat="server"></asp:TextBox>,<br />
                    "face-recog-cust-capture-id" : <asp:TextBox ID="face_recog_cust_capture_id" runat="server"></asp:TextBox><br />
                }<br />
            }<br />
        </td>
    </tr>
</table>