<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Activity_End.ascx.vb" Inherits="VDM.UC_Activity_End" %>
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
                        <td>order-id</td>
                        <td><asp:TextBox ID="order_id" runat="server" ></asp:TextBox></td>
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
<br />  "status" : <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>,
<br />  "trx-id" : <asp:TextBox ID="TextBox2" runat="server" ></asp:TextBox>,
<br />  "process-instance" : <asp:TextBox ID="TextBox3" runat="server" ></asp:TextBox>,
<br />  "response-data" : {
<br />&nbsp;    "order-id" : <asp:TextBox ID="TextBox4" runat="server" ></asp:TextBox>,
<br />&nbsp;    "status" : <asp:TextBox ID="TextBox5" runat="server" ></asp:TextBox>,
<br />&nbsp;    "creator" : <asp:TextBox ID="TextBox6" runat="server" ></asp:TextBox>,
<br />&nbsp;    "create-date" : <asp:TextBox ID="TextBox7" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;    "customer" : {
<br />&nbsp;&nbsp;      "gender" : <asp:TextBox ID="TextBox8" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;      "language" : <asp:TextBox ID="TextBox9" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;      "title-code" : <asp:TextBox ID="TextBox10" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;      "firstname" : <asp:TextBox ID="TextBox11" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;      "lastname" : <asp:TextBox ID="TextBox12" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;      "birthdate" : <asp:TextBox ID="TextBox13" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;      "contact-number" : <asp:TextBox ID="TextBox14" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;      "id-type" : <asp:TextBox ID="TextBox15" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;      "id-number" : <asp:TextBox ID="TextBox16" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;      "tax-id" : <asp:TextBox ID="TextBox17" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;      "id-expire-date" : <asp:TextBox ID="TextBox18" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;      "branch-code" : <asp:TextBox ID="TextBox19" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;      "customer-id" : <asp:TextBox ID="TextBox20" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;      "customer-level" : <asp:TextBox ID="TextBox21" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;      "customer-sublevel" : <asp:TextBox ID="TextBox22" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;      "address-list" : {
<br />&nbsp;&nbsp;&nbsp;        "CUSTOMER_ADDRESS" : {
<br />&nbsp;&nbsp;&nbsp;          "number" : <asp:TextBox ID="TextBox23" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;&nbsp;          "moo" : <asp:TextBox ID="TextBox24" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;&nbsp;          "street" : <asp:TextBox ID="TextBox25" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;&nbsp;          "district" : <asp:TextBox ID="TextBox26" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;&nbsp;          "province" : <asp:TextBox ID="TextBox27" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;&nbsp;          "sub-district" : <asp:TextBox ID="TextBox28" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;&nbsp;          "zip" : <asp:TextBox ID="TextBox29" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;        }
<br />&nbsp;      }
<br />&nbsp;    },
<br />&nbsp;    "sale-agent" : {
<br />&nbsp;      "name" : <asp:TextBox ID="TextBox30" runat="server" ></asp:TextBox>,
<br />&nbsp;      "channel" : <asp:TextBox ID="TextBox31" runat="server" ></asp:TextBox>,
<br />&nbsp;      "partner-code" : <asp:TextBox ID="TextBox32" runat="server" ></asp:TextBox>,
<br />&nbsp;      "partner-name" : <asp:TextBox ID="TextBox33" runat="server" ></asp:TextBox>,
<br />&nbsp;      "sale-code" : <asp:TextBox ID="TextBox34" runat="server" ></asp:TextBox>,
<br />&nbsp;      "partner-type" : <asp:TextBox ID="TextBox35" runat="server" ></asp:TextBox>,
<br />&nbsp;    },
<br />&nbsp;    "order-items" : [ {
<br />&nbsp;&nbsp;      "product-category" : <asp:TextBox ID="TextBox36" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;      "order-type" : <asp:TextBox ID="TextBox37" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;      "name" : <asp:TextBox ID="TextBox38" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;      "status" : <asp:TextBox ID="TextBox39" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;      "item-id" : <asp:TextBox ID="TextBox40" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;      "address-list" : {
<br />&nbsp;&nbsp;&nbsp;        "BILLING_ADDRESS" : {
<br />&nbsp;&nbsp;&nbsp;          "number" : <asp:TextBox ID="TextBox41" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;&nbsp;          "moo" : <asp:TextBox ID="TextBox42" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;&nbsp;          "street" : <asp:TextBox ID="TextBox43" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;&nbsp;          "district" : <asp:TextBox ID="TextBox44" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;&nbsp;          "province" : <asp:TextBox ID="TextBox45" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;&nbsp;          "sub-district" : <asp:TextBox ID="TextBox46" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;&nbsp;          "zip" : <asp:TextBox ID="TextBox47" runat="server" ></asp:TextBox>,
<br />&nbsp;&nbsp;        }
<br />&nbsp;      },
<br />      "product-name" : <asp:TextBox ID="TextBox48" runat="server" ></asp:TextBox>,
<br />      "product-id-number" : <asp:TextBox ID="TextBox49" runat="server" ></asp:TextBox>,
<br />      "product-id-name" : <asp:TextBox ID="TextBox50" runat="server" ></asp:TextBox>,
<br />      "reason-code" : <asp:TextBox ID="TextBox51" runat="server" ></asp:TextBox>,
<br />      "order-data" : {
<br />&nbsp;        "ACCOUNT-BILL-FORMAT" : <asp:TextBox ID="TextBox52" runat="server" ></asp:TextBox>,
<br />&nbsp;        "ACCOUNT-EMAIL" :<asp:TextBox ID="TextBox53" runat="server" ></asp:TextBox>,
<br />&nbsp;        "SUBSCRIBER-TITLE-CODE" : <asp:TextBox ID="TextBox54" runat="server" ></asp:TextBox>,
<br />      },
<br />      "primary-order-data" : {
<br />&nbsp;        "SIM" : <asp:TextBox ID="TextBox55" runat="server" ></asp:TextBox>,
<br />&nbsp;        "ACCOUNT-SUB-TYPE" : <asp:TextBox ID="TextBox56" runat="server" ></asp:TextBox>,
<br />&nbsp;        "ACCOUNT-CATEGORY" : <asp:TextBox ID="TextBox57" runat="server" ></asp:TextBox>,
<br />      }
<br />    } ],
<br />    "last-modify-date" : <asp:TextBox ID="TextBox58" runat="server" ></asp:TextBox>,
<br />    "last-modify-by" : <asp:TextBox ID="TextBox59" runat="server" ></asp:TextBox>,
<br />  }
<br />}






           </span> 
        </td>
    </tr>
        <tr>
        <td colspan ="3"><asp:Label ID="lblErr_Msg" runat="server" Style =" color :red;"  ></asp:Label></td>
    </tr>
</table>