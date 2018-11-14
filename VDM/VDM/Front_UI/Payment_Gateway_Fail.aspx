<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Payment_Gateway_Fail.aspx.vb" Inherits="VDM.Payment_Gateway_Fail" %>
<iframe id="paymentGatewayKeyboard" style="width:0px; height:0px; visibility:hidden;" src="Keyboard_Hide.html" onload="closeCredit();"></iframe>
<script type="text/javascript">

    function closeCredit() {
        parent.showCreditCardError();
    }
    closeCredit();

</script>