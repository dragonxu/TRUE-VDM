<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Save_File.ascx.vb" Inherits="VDM.UC_Save_File" %>
<style> indent{ padding-left: 1.8em }</style>
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
                        <td>order_id</td>
                        <td><asp:TextBox ID="order_id_REQ" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>fileType</td>
                        <td><asp:TextBox ID="fileType" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>b64File</td>
                        <td><asp:TextBox ID="b64File" runat="server"  TextMode ="MultiLine" Style ="margin: 0px; width: 161px; height: 131px;"  ></asp:TextBox></td>
                    </tr>

                    <tr>
                        <td>form-type</td>
                        <td><asp:TextBox ID="form_type" runat="server" Text ="FACE_RECOG_CUST_CERTIFICATE" ></asp:TextBox></td>
                    </tr>                     
                    <tr>
                        <td> </td>
                        <td><asp:Button ID="btn_Request" runat="server" Text="OK" /></td>
                    </tr> 
                </table>
        </td>
        <td >&nbsp;</td>
        <td style="line-height: 2;"> 
           <span>
                    
                {
                <br />    "trx-id": <asp:TextBox ID="trx_id" runat="server" ></asp:TextBox>,
                <br />    "process-instance": <asp:TextBox ID="process_instance" runat="server" ></asp:TextBox>,
                <br />    "ref-id": <asp:TextBox ID="ref_id" runat="server" ></asp:TextBox>,
                <br />    "order-id": <asp:TextBox ID="order_id_RESP" runat="server" ></asp:TextBox>
                <br />}




           </span> 
        </td>
    </tr>
</table>
