<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Get_Product_Info.ascx.vb" Inherits="VDM.UC_Get_Product_Info" %>
<table>
      <tr>
            <td colspan ="3"><h3 style="text-align :center ;"> Get_Product_Info</h3></td>
                         
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
                        <td>productCode</td>
                        <td><asp:TextBox ID="productCode" runat="server" ></asp:TextBox></td>
                    </tr> 
                    <tr>
                        <td> </td>
                        <td><asp:Button ID="btn_Request" runat="server" Text="OK" /></td>
                    </tr> 
                    
                    <tr>
                        <td> </td>
                        <td><h6 style ="color :blue ;" >
                            <br />==Test Product==
                            <br />3000011962
                            <br />3000013631
                            <br />3000024133
                            <br />3000050987
                            <br />3000051011
                            <br />3000036094
                            <br />3000001852
                            <br />3000050979
                            <br />==Test SIM======
                            <br />3000007057

                            </h6>

                        </td>
                    </tr>
                                       
                </table>
        </td>
        <td >&nbsp;</td>
        <td style="line-height: 2;"> 
           <span>
                
 {
<br />"response-data": {
<br />  "Product": {
<br />&nbsp;    "CODE": <asp:TextBox ID="CODE" runat="server" ></asp:TextBox>,
<br />&nbsp;    "DESCRIPTION": <asp:TextBox ID="DESCRIPTION" runat="server" ></asp:TextBox>,
<br />&nbsp;    "PLANT_CODE": <asp:TextBox ID="PLANT_CODE" runat="server" ></asp:TextBox>,
<br />&nbsp;    "PRODUCT_BRAND_CODE": <asp:TextBox ID="PRODUCT_BRAND_CODE" runat="server" ></asp:TextBox>,
<br />&nbsp;    "PRODUCT_MODEL_CODE": <asp:TextBox ID="PRODUCT_MODEL_CODE" runat="server" ></asp:TextBox>,
<br />&nbsp;    "IS_SERIAL": <asp:TextBox ID="IS_SERIAL" runat="server" ></asp:TextBox>,
<br />&nbsp;    "PRODUCT_TYPE": <asp:TextBox ID="PRODUCT_TYPE" runat="server" ></asp:TextBox>,
<br />&nbsp;    "DISPLAY_NAME": <asp:TextBox ID="DISPLAY_NAME" runat="server" ></asp:TextBox>,
<br />&nbsp;    "IS_ENABLE": <asp:TextBox ID="IS_ENABLE" runat="server" ></asp:TextBox>,
<br />&nbsp;    "IS_DELETE": <asp:TextBox ID="IS_DELETE" runat="server" ></asp:TextBox>,
<br />&nbsp;    "CATEGORY":<asp:TextBox ID="CATEGORY" runat="server" ></asp:TextBox>,
<br />&nbsp;    "REQ_SALE_APPROACH": <asp:TextBox ID="REQ_SALE_APPROACH" runat="server" ></asp:TextBox>,
<br />&nbsp;    "IS_VAT": <asp:TextBox ID="IS_VAT" runat="server" ></asp:TextBox>,
<br />&nbsp;    "VAT_RATE": <asp:TextBox ID="VAT_RATE" runat="server" ></asp:TextBox>,
<br />&nbsp;    "COMPANY_CODE": <asp:TextBox ID="COMPANY_CODE" runat="server" ></asp:TextBox>,
<br />&nbsp;    "IS_SIM": <asp:TextBox ID="IS_SIM" runat="server" ></asp:TextBox>,
<br />&nbsp;    "REQUIRE_RECEIVE_FORM": <asp:TextBox ID="REQUIRE_RECEIVE_FORM" runat="server" ></asp:TextBox>,
<br />&nbsp;    "APPLE_CARE_SERVICE_CODE": <asp:TextBox ID="APPLE_CARE_SERVICE_CODE" runat="server" ></asp:TextBox>,
<br />&nbsp;    "COLOR": <asp:TextBox ID="COLOR" runat="server" ></asp:TextBox>,
<br />&nbsp;    "CAPACITY": <asp:TextBox ID="CAPACITY" runat="server" ></asp:TextBox>,
<br />&nbsp;    "CATEGORY_RECOMMEND": <asp:TextBox ID="CATEGORY_RECOMMEND" runat="server" ></asp:TextBox>
<br />  },
<br />  "Price": <asp:TextBox ID="Price" runat="server" ></asp:TextBox>,
<br />&nbsp;      "ErrorMessage": <asp:TextBox ID="ErrorMessage" runat="server" ></asp:TextBox>,
<br />  "Captions": [
<br />&nbsp;    {
<br />&nbsp;      "SEQ": <asp:TextBox ID="SEQ" runat="server" ></asp:TextBox>,
<br />&nbsp;      "PRODUCT_CODE": <asp:TextBox ID="PRODUCT_CODE" runat="server" ></asp:TextBox>,
<br />&nbsp;      "CAPTION_CODE": <asp:TextBox ID="CAPTION_CODE" runat="server" ></asp:TextBox>,
<br />&nbsp;      "CAPTION_DESC": <asp:TextBox ID="CAPTION_DESC" runat="server" ></asp:TextBox>,
<br />&nbsp;      "DETAIL": <asp:TextBox ID="DETAIL" runat="server" ></asp:TextBox>
<br />    },
 
<br />&nbsp;      "L7" 

<br />&nbsp;      { 
<br />&nbsp;          "errCode": "<asp:TextBox ID="errCode" runat="server" ></asp:TextBox>", 
<br />&nbsp;          "errMsg": { 
<br />&nbsp;              "trx-id": "<asp:TextBox ID="errMsgtrx_ID" runat="server" ></asp:TextBox>", 
<br />&nbsp;              "status": "<asp:TextBox ID="errMsgstatus" runat="server" ></asp:TextBox>", 
<br />&nbsp;              "process-instance": "<asp:TextBox ID="errMsgprocess_instance" runat="server" ></asp:TextBox>", 
<br />&nbsp;              "fault": { 
<br />&nbsp;                  "name": "<asp:TextBox ID="faultname" runat="server" ></asp:TextBox>", 
<br />&nbsp;                  "code": "<asp:TextBox ID="faultcode" runat="server" ></asp:TextBox>", 
<br />&nbsp;                  "message": "<asp:TextBox ID="faultmessage" runat="server" ></asp:TextBox>", 
<br />&nbsp;                  "detailed-message": "<asp:TextBox ID="faultdetailed_message" runat="server" ></asp:TextBox>" 
<br />&nbsp;              }, 
<%--<br />&nbsp;              "display-messages": [ 
<br />&nbsp;                  { 
<br />&nbsp;                      "message": "Flow 18101900CMU060000001 was not found.", 
<br />&nbsp;                      "message-type": "ERROR", 
<br />&nbsp;                      "en-message": "Flow 18101900CMU060000001 was not found.", 
<br />&nbsp;                      "th-message": "Flow 18101900CMU060000001 was not found.", 
<br />&nbsp;                      "technical-message": "tmsapnpr1 (instance: SFF_node2) ActivityFlowNotFoundException Flow 18101900CMU060000001 was not found.. " 
<br />&nbsp;                  } 
<br />&nbsp;              ] --%>
<br />&nbsp;          } 
<br />&nbsp;      }

<br />&nbsp;          "JSON": "<asp:Label ID="lbljson" runat = "server" ></asp:Label>




           </span> 
        </td>
    </tr>
        <tr>
        <td colspan ="3"><asp:Label ID="lblErr_Msg" runat="server" Style =" color :red;"  ></asp:Label></td>
    </tr>
</table>