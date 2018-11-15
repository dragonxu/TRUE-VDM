<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Payment_Gateway_Start.aspx.vb" Inherits="VDM.Payment_Gateway_Start" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" method="post">
    <asp:ScriptManager ID="scm" runat="server"></asp:ScriptManager>

        <div style="display:none;">
            <asp:Textbox runat="server" ID="merchantId" /> <br>
            <asp:Textbox runat="server" ID="amount"/> <br>
            <asp:Textbox runat="server" ID="orderRef"/> <br>
            <asp:Textbox runat="server" ID="currCode"/> <br>
            <asp:Textbox runat="server" ID="successUrl"/> <br>
            <asp:Textbox runat="server" ID="failUrl"/> <br>
            <asp:Textbox runat="server" ID="cancelUrl"/> <br>
            <asp:Textbox runat="server" ID="payType" Text="N" /> <br>
            <asp:Textbox runat="server" ID="lang" Text="E" /> <br>
            <asp:Textbox runat="server" ID="remark" Text ="-"/> <br>
            <asp:Button ID="btnOK" runat="server" Text="Go" />
        </div>

        <script tyle="text/javascript">
        
            function LinkGateway(REQ_ID) {
                //window.parent.document.getElementById("txtCreditReq").value = REQ_ID;
                setTimeout(function () { document.getElementById('btnOK').click(); }, 100);
            }
        
        </script>
    </form>
</body>
</html>