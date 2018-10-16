<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Test_BBL_Query.aspx.vb" Inherits="VDM.Test_BBL_Query" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TRUE-VSM</title>
</head>
<body>

<form name="payFormTrue" method="post" action="https://psipay.bangkokbank.com/b2c/eng/merchant/api/orderApi.jsp">

    

<input type="hidden" name="merchantId" value="3971">
<input type="hidden" name="loginId" value="admin">
<input type="hidden" name="password" value="truevdm2018">
<input type="hidden" name="actionType" value="Query">
<input type="hidden" name="orderRef" value="TRUE-VDM-181016001">


<input type="submit" value="Query" />

</form> 
    
</body>
</html>
