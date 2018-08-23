<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Prepaid_Validate_Register.ascx.vb" Inherits="VDM.UC_Prepaid_Validate_Register" %>

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
                        <td>key_type</td>
                        <td><asp:TextBox ID="key_type" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>key_value</td>
                        <td><asp:TextBox ID="key_value" runat="server"></asp:TextBox></td>

                    </tr>
                    <tr>
                        <td>id_number</td>
                        <td><asp:TextBox ID="id_number" runat="server"   ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>id_type</td>
                        <td><asp:TextBox ID="id_type" runat="server"   ></asp:TextBox></td>
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
                <br />  "status": <asp:TextBox ID="status" runat="server"   ></asp:TextBox>,
                <br />  "trx-id": <asp:TextBox ID="trx_id" runat="server"   ></asp:TextBox>,
                <br />  "process-instance": <asp:TextBox ID="process_instance" runat="server"   ></asp:TextBox>,
                <br />  "response-data": {
                <br />    "subscriber" : <asp:TextBox ID="subscriber" runat="server"   ></asp:TextBox>,
                <br />    "sim" : <asp:TextBox ID="sim" runat="server"   ></asp:TextBox>,
                <br />    "imsi" : <asp:TextBox ID="imsi" runat="server"   ></asp:TextBox>,
                <br />    "sim-category" : <asp:TextBox ID="sim_category" runat="server"   ></asp:TextBox>,
                <br />    "priceplan" : <asp:TextBox ID="priceplan" runat="server"   ></asp:TextBox>,
                <br />    "company-code" : <asp:TextBox ID="company_code" runat="server"   ></asp:TextBox>,
                <br />    "is-registered" : <asp:TextBox ID="is_registered" runat="server"   ></asp:TextBox>,
                <br />    "firstname" : <asp:TextBox ID="firstname" runat="server"   ></asp:TextBox>,
                <br />    "lastname" : <asp:TextBox ID="lastname" runat="server"   ></asp:TextBox>
                <br />  }
                <br />}


           </span> 





        </td>
    </tr>
        <tr>
        <td colspan ="3"><asp:Label ID="lblErr_Msg" runat="server" Style =" color :red;"  ></asp:Label></td>
    </tr>
</table>