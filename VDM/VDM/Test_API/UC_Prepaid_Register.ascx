<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Prepaid_Register.ascx.vb" Inherits="VDM.UC_Prepaid_Register" %>
 <table>
    <tr>
                        <td colspan ="3"><h3 style="text-align :center ;">Prepaid_Register</h3></td>
                         
   </tr>
    <tr>
                        <td><h4 style="text-align :center ;">Request</h4></td>
                        <td>&nbsp;</td>
                        <td><h4 style="text-align :center ;">Response</h4></td>
   </tr>

    <tr style ="vertical-align :top;">
        <td style="line-height: 2;"> 
                <table>                   

                    <tr>
                        <td>OrderID</td>
                        <td><asp:TextBox ID="OrderID" runat="server" ></asp:TextBox></td>
                    </tr> 
                    <tr>
                        <td>customer_gender</td>
                        <td>
                            <asp:DropDownList ID="customer_gender" runat="server" >
                            <asp:ListItem Text="" Value="-1" Selected></asp:ListItem>
                            <asp:ListItem Text="MALE" Value="1" ></asp:ListItem>
                            <asp:ListItem Text="FEMALE" Value="2"   ></asp:ListItem>
                            </asp:DropDownList> 
                        </td>
                    </tr>
                    <tr>
                        <td>customer_title</td>
                        <td><asp:TextBox ID="customer_title" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>customer_language</td>
                        <td><asp:TextBox ID="customer_language" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>customer_title_code</td>
                        <td><asp:TextBox ID="customer_title_code" runat="server" Text ="T5" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>customer_firstname</td>
                        <td><asp:TextBox ID="customer_firstname" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>customer_lastname</td>
                        <td><asp:TextBox ID="customer_lastname" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>customer_birthdate</td>
                        <td><asp:TextBox ID="customer_birthdate" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>doc_type</td>
                        <td><asp:TextBox ID="doc_type" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>customer_id_number</td>
                        <td><asp:TextBox ID="customer_id_number" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>customer_id_expire_date</td>
                        <td><asp:TextBox ID="customer_id_expire_date" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>address_number</td>
                        <td><asp:TextBox ID="address_number" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>address_moo</td>
                        <td><asp:TextBox ID="address_moo" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>address_village</td>
                        <td><asp:TextBox ID="address_village" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>address_street</td>
                        <td><asp:TextBox ID="address_street" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>address_soi</td>
                        <td><asp:TextBox ID="address_soi" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>address_district</td>
                        <td><asp:TextBox ID="address_district" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>address_province</td>
                        <td><asp:TextBox ID="address_province" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>address_building_name</td>
                        <td><asp:TextBox ID="address_building_name" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>address_building_room</td>
                        <td><asp:TextBox ID="address_building_room" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>address_building_floor</td>
                        <td><asp:TextBox ID="address_building_floor" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>sddress_sub_district</td>
                        <td><asp:TextBox ID="sddress_sub_district" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>address_zip</td>
                        <td><asp:TextBox ID="address_zip" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>shopCode</td>
                        <td><asp:TextBox ID="shopCode" runat="server" ></asp:TextBox></td>
                    </tr>
                    <%--<tr>
                        <td>sale_partner_code</td>
                        <td><asp:TextBox ID="sale_partner_code" runat="server" ></asp:TextBox></td>
                    </tr>--%>

                    <tr>
                        <td>shopName</td>
                        <td><asp:TextBox ID="shopName" runat="server" Text="-"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>sale_code</td>
                        <td><asp:TextBox ID="sale_code" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>mat_code</td>
                        <td><asp:TextBox ID="mat_code" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>mat_desc</td>
                        <td><asp:TextBox ID="mat_desc" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>sim_serial</td>
                        <td><asp:TextBox ID="sim_serial" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>require_print_form</td>
                        <td>
                            <asp:DropDownList ID="require_print_form" runat="server" >
                            <asp:ListItem Text="true" Value="true" ></asp:ListItem>
                            <asp:ListItem Text="false" Value="false" Selected></asp:ListItem> 
                            </asp:DropDownList>  
                        </td>
                    </tr>
                    <tr>
                        <td>price_plan</td>
                        <td><asp:TextBox ID="price_plan" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>subscriber</td>
                        <td><asp:TextBox ID="subscriber" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>is_registered</td>
                        <td><asp:DropDownList ID="is_registered" runat="server" >
                            <asp:ListItem Text="" Value="-1" Selected></asp:ListItem>
                            <asp:ListItem Text="Y" Value="1" ></asp:ListItem>
                            <asp:ListItem Text="N" Value="0"   ></asp:ListItem>
                            </asp:DropDownList>  

                        </td>
                    </tr> 
                    <tr>
                        <td>sub_activity</td>
                        <td><asp:DropDownList ID="sub_activity" runat="server" >
                            <asp:ListItem Text="" Value="-1" Selected></asp:ListItem>
                            <asp:ListItem Text="UNPAIR" Value="1" ></asp:ListItem>
                            <asp:ListItem Text="PAIR" Value="2"   ></asp:ListItem>
                            </asp:DropDownList>  

                        </td>
                    </tr>
                   <tr>
                        <td>company_code</td>
                        <td><asp:TextBox ID="company_code" runat="server" ></asp:TextBox></td>
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
            <br />  "trx-id" : <asp:TextBox ID="trx_id" runat="server" ></asp:TextBox>,
            <br />  "status" : <asp:TextBox ID="status" runat="server" ></asp:TextBox>,
            <br />  "process-instance" : <asp:TextBox ID="process_instance" runat="server" ></asp:TextBox>,
            <br />  "response-data" : <asp:TextBox ID="response_data" runat="server" ></asp:TextBox>,
            
<br />
<br />
<br />  "display-messages" : [ {
<br />    "message" : <asp:TextBox ID="message" runat="server" ></asp:TextBox>,
<br />    "message_code" : <asp:TextBox ID="message_code" runat="server" ></asp:TextBox>,

<br />    "message-type" : <asp:TextBox ID="message_type" runat="server" ></asp:TextBox>,
<br />    "en-message" : <asp:TextBox ID="en_message" runat="server" ></asp:TextBox>,
<br />    "th-message" : <asp:TextBox ID="th_message" runat="server" ></asp:TextBox>
<br />    "technical_message" : <asp:TextBox ID="technical_message" runat="server" ></asp:TextBox>

<br />  } ],

<br />  "fault" : [ {
<br />    "name" : <asp:TextBox ID="name" runat="server" ></asp:TextBox>,
<br />    "code" : <asp:TextBox ID="code" runat="server" ></asp:TextBox>,
<br />    "message" : <asp:TextBox ID="messagefault" runat="server" ></asp:TextBox>,
<br />    "detailed_message" : <asp:TextBox ID="detailed_message" runat="server" ></asp:TextBox>
<br />  } ],
		






           </span> 
        </td>
    </tr>
        <tr>
        <td colspan ="3"><asp:Label ID="lblErr_Msg" runat="server" Style =" color :red;"  ></asp:Label></td>
    </tr>
</table>