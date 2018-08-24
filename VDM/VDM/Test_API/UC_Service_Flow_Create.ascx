<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Service_Flow_Create.ascx.vb" Inherits="VDM.UC_Service_Flow_Create" %>
<table>
      <tr>
                        <td colspan ="3"><h3 style="text-align :center ;"> Service_Flow_Create</h3></td>
                         
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
                        <td>orderId</td>
                        <td><asp:TextBox ID="orderId" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>flowName</td>
                        <td><asp:TextBox ID="FlowType" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>CREATE-BY</td>
                        <td><asp:TextBox ID="staffOpenShift" runat="server"   ></asp:TextBox></td>
                    </tr>

                    <tr>
                        <td>ID-NUMBER</td>
                        <td><asp:TextBox ID="thaiID" runat="server"   ></asp:TextBox></td>
                    </tr>                     
 
                     <tr>
                        <td>PRODUCT-ID-NUMBER</td>
                        <td><asp:TextBox ID="subscriber" runat="server"></asp:TextBox></td>
                    </tr>                     
                    <tr>
                        <td>PARTNER-CODE</td>
                        <td><asp:TextBox ID="shopCode" runat="server"></asp:TextBox></td>
                    </tr>                     
                    <tr>
                        <td>CUST-NAME</td>
                        <td><asp:TextBox ID="customerName" runat="server"></asp:TextBox></td>
                    </tr>                     
                    <tr>
                        <td>PROPO-PROMO</td>
                        <td><asp:TextBox ID="proposition" runat="server"></asp:TextBox></td>
                    </tr>                     
                    <tr>
                        <td>PRODUCT-NAME</td>
                        <td><asp:TextBox ID="pricePlan" runat="server"></asp:TextBox></td>
                    </tr>                     
                    <tr>
                        <td>SERIAL-NUMBER</td>
                        <td><asp:TextBox ID="sim" runat="server"></asp:TextBox></td>
                    </tr>                     
                    <tr>
                        <td>SALE-CODE</td>
                        <td><asp:TextBox ID="saleCode" runat="server"></asp:TextBox></td>
                    </tr>                     
                    <tr>
                        <td>FACE_RECOFNITION_STATUS</td>
                        <td><asp:TextBox ID="face_recognition_result" runat="server"></asp:TextBox></td>
                    </tr>                     
                    <tr>
                        <td>IS_IDENTICAL</td>
                        <td><asp:TextBox ID="is_identical" runat="server"></asp:TextBox></td>
                    </tr>                     
                    <tr>
                        <td>CONFIDENT_RATIO</td>
                        <td><asp:TextBox ID="confident_ratio" runat="server"></asp:TextBox></td>
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
                <br />  "display-messages" : [ {
                <br />    "message" : <asp:TextBox ID="message" runat="server"></asp:TextBox>,
                <br />    "message-type" : <asp:TextBox ID="message_type" runat="server"></asp:TextBox>,
                <br />    "en-message" : <asp:TextBox ID="en_message" runat="server"></asp:TextBox>,
                <br />    "th-message" : <asp:TextBox ID="th_message" runat="server"></asp:TextBox>
                <br />  } ],
                <br />  "trx-id" : <asp:TextBox ID="trx_id" runat="server"></asp:TextBox>,
                <br />  "process-instance" : <asp:TextBox ID="process_instance" runat="server"></asp:TextBox>,
                <br />  "response-data" : {
                <br />    "data" : {
                <br />      "THAI-ID" : <asp:TextBox ID="THAI_ID" runat="server"></asp:TextBox>,
                <br />      "LANG" : <asp:TextBox ID="LANG" runat="server"></asp:TextBox>
                <br />    },
                <br />    "flow-id" : <asp:TextBox ID="flow_id" runat="server"></asp:TextBox>,
                <br />    "flow-name" : <asp:TextBox ID="flow_name" runat="server"></asp:TextBox>,
                <br />    "create-date" : <asp:TextBox ID="create_date" runat="server"></asp:TextBox>,
                <br />    "create-by" : <asp:TextBox ID="create_by" runat="server"></asp:TextBox>
                <br />  }
                <br />}





           </span> 
        </td>
    </tr>
        <tr>
        <td colspan ="3"><asp:Label ID="lblErr_Msg" runat="server" Style =" color :red;"  ></asp:Label></td>
    </tr>
</table>

