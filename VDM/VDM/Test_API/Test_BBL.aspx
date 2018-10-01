<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Test_BBL.aspx.vb" Inherits="VDM.Test_BBL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TRUE-VSM</title>
</head>
<body>
<%--<form name="payFormCcard" method="post" action=" https://ipay.bangkokbank.com/b2c/eng/payment/payForm.jsp">
<input type="hidden" name="merchantId" value="1">
<input type="hidden" name="amount" value="1.0" >
<input type="hidden" name="orderRef" value="000000000014">
<input type="hidden" name="currCode" value="764" >
<input type="hidden" name="successUrl" value="http://www.tit-tech.co.th">
<input type="hidden" name="failUrl" value="http://www.google.com">
<input type="hidden" name="cancelUrl" value="http://www.yahoo.com">
<input type="hidden" name="payType" value="N">
<input type="hidden" name="lang" value="E">
<input type="hidden" name="remark" value="-">
<input type="submit" value="sample" />
</form>

<form name="payFormTrue" method="post" action="https://psipay.bangkokbank.com/b2c/eng/payment/payForm.jsp">
<input type="hidden" name="merchantId" value="3971">
<input type="hidden" name="amount" value="1.0" >
<input type="hidden" name="orderRef" value="TRUEVDM37847238904"> <!--เปลี่ยนทุกครั้ง-->
<input type="hidden" name="currCode" value="764" >
<input type="hidden" name="successUrl" value="http://www.tit-tech.co.th">
<input type="hidden" name="failUrl" value="http://www.google.com">
<input type="hidden" name="cancelUrl" value="http://www.yahoo.com">
<input type="hidden" name="payType" value="N">
<input type="hidden" name="lang" value="E">
<input type="hidden" name="remark" value="-">
<input type="submit" value="True Profile" />
</form>

<form name="payFormTrue" method="post" action=" https://ipay.bangkokbank.com/b2c/eng/payment/payForm.jsp">
<input type="hidden" name="merchantId" value="3971">
<input type="hidden" name="amount" value="1.0" >
<input type="hidden" name="orderRef" value="TRUEVDM-837294"> <!--เปลี่ยนทุกครั้ง-->
<input type="hidden" name="currCode" value="764" >
<input type="hidden" name="successUrl" value="http://www.tit-tech.co.th">
<input type="hidden" name="failUrl" value="http://www.google.com">
<input type="hidden" name="cancelUrl" value="http://www.yahoo.com">
<input type="hidden" name="payType" value="N">
<input type="hidden" name="lang" value="E">
<input type="hidden" name="remark" value="-">
<input type="submit" value="Sathit" />
</form>--%>

    
<form name="payForm" method="post" action="https://psipay.bangkokbank.com/b2c/eng/dPayment/payComp.jsp">
<input type="hidden" name="merchantId" value="3971">
<input type="hidden" name="amount" value="1.0" >
<input type="hidden" name="orderRef" value="11111111">
<input type="hidden" name="currCode" value="764" >
<input type="hidden" name="pMethod" value="VISA" >
<input type="hidden" name="cardNo" value="4546289950108110" >
<input type="hidden" name="securityCode" value="123" >
<input type="hidden" name="cardHolder" value="Testing" >
<input type="hidden" name="epMonth" value="12” >
<input type="hidden" name="epYear" value="2021" >
<input type="hidden" name="payType" value="N" >
<input type="hidden" name="successUrl" value="http://www.tit-tech.co.th">
<input type="hidden" name="failUrl" value="http://www.yahoo.com">
<input type="hidden" name="errorUrl" value="http://www.google.com">
<input type="hidden" name="lang" VALUE="E">
<!— Optional parameter
<input type="hidden" name="redirect" value="30">
<input type="hidden" name="orderRef1" value="">
<input type="hidden" name="orderRef2" value="">
<input type="hidden" name="orderRef3" value="">
<input type="hidden" name="orderRef4" value="">
<input type="hidden" name="orderRef5" value="">
<input type="hidden" name="holderEmail" value="tsathit40@hotmail.com">
<input type="hidden" name="issueCountry" value="764">
<input type="submit" value="Integrate">
</form>


</body>
</html>