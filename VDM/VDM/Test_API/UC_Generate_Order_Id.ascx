<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Generate_Order_Id.ascx.vb" Inherits="VDM.UC_Generate_Order_Id" %>

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
                        <td>dealer</td>
                        <td><asp:TextBox ID="dealer" runat="server"></asp:TextBox></td>

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
                    <br />  "status" : <asp:TextBox ID="status" runat="server"></asp:TextBox>,
                    <br />  "trx-id" : <asp:TextBox ID="trx_id" runat="server"></asp:TextBox>,
                    <br />  "process-instance" : <asp:TextBox ID="process_instance" runat="server"></asp:TextBox>,
                    <br />  "response-data" : <asp:TextBox ID="response_data" runat="server"></asp:TextBox>
                    <br />}
           </span> 
        </td>
    </tr>

    <tr>
        <td colspan ="3"><asp:Label ID="lblErr_Msg" runat="server" Style =" color :red;"  ></asp:Label></td>
    </tr>

</table>
