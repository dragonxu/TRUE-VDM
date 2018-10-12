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
    </form>

<%--<input type="hidden" name="merchantId" value="3971">
<input type="hidden" name="amount" value="1.0" >
<input type="hidden" name="orderRef" value="TRUEVDM37847238904"> <!--เปลี่ยนทุกครั้ง-->
<input type="hidden" name="currCode" value="764" >
<input type="hidden" name="successUrl" value="http://www.tit-tech.co.th">
<input type="hidden" name="failUrl" value="http://www.google.com">
<input type="hidden" name="cancelUrl" value="http://www.yahoo.com">
<input type="hidden" name="payType" value="N">
<input type="hidden" name="lang" value="E">
<input type="hidden" name="remark" value="-">
<input type="submit" value="True Profile" />--%>
</body>
</html>