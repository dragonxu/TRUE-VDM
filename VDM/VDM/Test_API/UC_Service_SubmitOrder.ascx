<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Service_SubmitOrder.ascx.vb" Inherits="VDM.UC_Service_SubmitOrder" %>
<table>
      <tr>
                        <td colspan ="3"><h3 style="text-align :center ;"> Service_SubmitOrder</h3></td>
                         
   </tr>
    <tr>
                        <td><h4 style="text-align :center ;">Request</h4></td>
                        <td>&nbsp;</td>
                        <td><h4 style="text-align :center ;">Response</h4></td>
   </tr>

    <tr style ="vertical-align :top;">
        <td style="line-height: 2;"> 
{
<br />  "ref-id": "1511170SHOP000000003",
<br />    "user-id": "302074309",
<br />    "order": {
<br />        "order-id": "15111700DRS000000002",
<br />        "approver": "",
<br />        "customer": {
<br />            "address-list": {
<br />                "CUSTOMER_ADDRESS": {
<br />                    "district": "ลำลูกกา",
<br />                    "moo": "-",
<br />                    "number": "-",
<br />                    "province": "ปทุมธานี",
<br />                    "street": "-",
<br />                    "sub-district": "บึงคำพร้อย",
<br />                    "zip": "10900"
<br />                }
<br />            },
<br />            "birthdate": "1967-01-01T00:00:00+0700",
<br />            "branch-code": "00000",
<br />            "contact-number": "0891378222",
<br />            "customer-id": "264",
<br />            "customer-level": "NON-TOP",
<br />            "customer-sublevel": "NONE",
<br />            "customer-sublevel_id": "2",
<br />            "firstname": "บุญมี",
<br />            "gender": "MALE",
<br />            "id-expire-date": "2015-11-18T00:00:00+0700",
<br />            "id-number": "2015093111151",
<br />            "id-type": "I",
<br />            "language": "TH",
<br />            "lastname": "กรรมบัง",
<br />            "tax-id": "2015093111151",
<br />            "title": "",
<br />            "title-code": "T1"
<br />        },
<br />          "sale-agent": {
<br />            "channel": "DRS",
<br />            "name": "Direct user009 Direct user009",
<br />            "partner-code": "30207406",
<br />            "partner-name": "Direct user009 Direct user009",
<br />            "partner-type": "Direct Dealer",
<br />            "sale-assist-code": "",
<br />            "sale-code": "30207406"
<br />        },
<br />        "order-items": [
<br />            {
<br />              "product-category": "TMV",
<br />              "name": "MIGRATE_PRE_TO_POST",
<br />                "product-id-name": "MSISDN",
<br />                "product-id-number": "0973590408",
<br />                "product-name": "SMRTAP35",
<br />                "product-type": "PRICEPLAN",
<br />                "reason-code": "CREQ",
<br />                "user-memo": "",
<br />                "address-list": {
<br />                    "BILLING_ADDRESS": {
<br />                        "district": "ลำลูกกา",
<br />                        "moo": "-",
<br />                        "number": "-",
<br />                        "province": "ปทุมธานี",
<br />                        "street": "-",
<br />                        "sub-district": "บึงคำพร้อย",
<br />                        "zip": "10900"
<br />                    }
<br />                },
<br />                "order-data": {
<br />                    "ACCOUNT-BILL-FORMAT": "E",
<br />                    "ACCOUNT-EMAIL": "Supapor_sae@truecorp.co.th",
<br />                    "SUBSCRIBER-TITLE-CODE": "T1"
<br />                },
<br />                "order-type": "CHANGE",
<br />                "primary-order-data": {
<br />                    "ACCOUNT-CATEGORY": "I",
<br />                    "ACCOUNT-SUB-TYPE": "RPI",
<br />                    "SIM": "896600401500005408"
<br />                }
<br />            }
<br />        ]
<br />    }
<br />}                 
<br /> 


        </td>
        <td >&nbsp;</td>
        <td style="line-height: 2;"> 
           <span>
                
 {
<br />  "status" : <asp:TextBox ID="status" runat="server" ></asp:TextBox>,
<br />  "display-messages" : [ {
<br />    "message" : <asp:TextBox ID="message" runat="server" ></asp:TextBox>,
<br />    "message-type" : <asp:TextBox ID="message_type" runat="server" ></asp:TextBox>,
<br />    "en-message" : <asp:TextBox ID="en_message" runat="server" ></asp:TextBox>,
<br />    "th-message" : <asp:TextBox ID="th_message" runat="server" ></asp:TextBox>
<br />  } ],
<br />  "trx-id" : <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>,
<br />  "process-instance" : <asp:TextBox ID="process_instance" runat="server" ></asp:TextBox>
<br />}


<br />


           </span> 
        </td>
    </tr>
        <tr>
        <td colspan ="3"><asp:Label ID="lblErr_Msg" runat="server" Style =" color :red;"  ></asp:Label></td>
    </tr>
</table>