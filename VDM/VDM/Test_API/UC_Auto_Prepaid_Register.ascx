<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Auto_Prepaid_Register.ascx.vb" Inherits="VDM.UC_Auto_Prepaid_Register" %>
<table>
     <tr>
                        <td colspan ="3"><h3 style="text-align :center ;">Activity_Start</h3></td>
                         
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
                        <td>Face_cust_certificate</td>
                        <td>
                            <asp:TextBox ID="Face_cust_certificate" runat="server"  TextMode ="MultiLine"  Style ="margin: 0px; width: 161px; height: 131px;" ></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td>Face_cust_capture</td>
                        <td>
                            <asp:TextBox ID="Face_cust_capture" runat="server"  TextMode ="MultiLine"  Style ="margin: 0px; width: 161px; height: 131px;" ></asp:TextBox>
                        </td>
                    </tr>
                    
                     <tr>
                        <td>SIM_Serial</td>
                        <td><asp:TextBox ID="SIM_Serial" runat="server" ></asp:TextBox></td>
                    </tr>                    
                     <tr>
                        <td>KO_ID</td>
                        <td><asp:TextBox ID="KO_ID" runat="server" Text ="1" ></asp:TextBox></td>
                    </tr>                      
                      <tr>
                        <td>USER_ID</td>
                        <td><asp:TextBox ID="USER_ID" runat="server" Text ="00001" ></asp:TextBox></td>
                    </tr>   
                    <tr>
                        <td>TXN_ID * สำหรับดึง Mat_Code</td>
                        <td><asp:TextBox ID="TXN_ID" runat="server" Text ="1" ></asp:TextBox></td>
                    </tr>     
                    
                    
                    
         <%--ID Card--%>
            <div style="display: block;" class="col-lg-12">
                CUS_TITLE :<asp:TextBox ID="CUS_TITLE" runat="server" AutoPostBack="true"></asp:TextBox>
            </div>
            <div style="display: block;" class="col-lg-12">
                CUS_NAME :<asp:TextBox ID="CUS_NAME" runat="server" AutoPostBack="true"></asp:TextBox>
            </div>
            <div style="display: block;" class="col-lg-12">
                CUS_SURNAME :<asp:TextBox ID="CUS_SURNAME" runat="server" AutoPostBack="true"></asp:TextBox>
            </div>
            <div style="display: block;" class="col-lg-12">
                NAT_CODE :<asp:TextBox ID="NAT_CODE" runat="server" AutoPostBack="true"></asp:TextBox>
            </div>
            <div style="display: block;" class="col-lg-12">
                CUS_GENDER :<asp:TextBox ID="CUS_GENDER" runat="server" AutoPostBack="true"></asp:TextBox>
            </div>
            <div style="display: block;" class="col-lg-12">
                CUS_BIRTHDATE :<asp:TextBox ID="CUS_BIRTHDATE" runat="server" AutoPostBack="true"></asp:TextBox>
            </div>
            <div style="display: block;" class="col-lg-12">
                CUS_PID :<asp:TextBox ID="CUS_PID" runat="server" AutoPostBack="true"></asp:TextBox>
            </div>
            <div style="display: block;" class="col-lg-12">
                CUS_PASSPORT_ID :<asp:TextBox ID="CUS_PASSPORT_ID" runat="server" AutoPostBack="true"></asp:TextBox>
            </div>
            <div style="display: block;" class="col-lg-12">
                CUS_PASSPORT_START :<asp:TextBox ID="CUS_PASSPORT_START" runat="server" AutoPostBack="true"></asp:TextBox>
            </div>
            <div style="display: block;" class="col-lg-12">
                CUS_PASSPORT_EXPIRE :<asp:TextBox ID="CUS_PASSPORT_EXPIRE" runat="server" AutoPostBack="true"></asp:TextBox>
            </div>           
                    
                    
                    
                    
                    
                    
                    
                    
                                                         
                                     
                    <tr>
                        <td> </td>
                        <td><asp:Button ID="btn_Request" runat="server" Text="OK" /></td>
                    </tr> 
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                                       
                </table>
        </td>
        <td >&nbsp;</td>
        <td style="line-height: 2;"> 
           <span>
                
            <br />  "Register" : <asp:TextBox ID="ReturnResult" runat="server" ></asp:TextBox>

            <br />  "Command_Result.status" : <asp:TextBox ID="status" runat="server" ></asp:TextBox>
            <br />  "Command_Result.Message" : <asp:TextBox ID="Message" runat="server" ></asp:TextBox>
 
               

           </span> 
        </td>
    </tr>
        <tr>
        <td colspan ="3"><asp:Label ID="lblErr_Msg" runat="server" Style =" color :red;"  ></asp:Label></td>
    </tr>
</table>