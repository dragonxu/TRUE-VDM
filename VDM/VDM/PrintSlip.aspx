<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PrintSlip.aspx.vb" Inherits="VDM.PrintSlip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="Scripts/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server" style="width:100%; height:100%;">
        <asp:ScriptManager ID="scm" runat="server"></asp:ScriptManager>
         <asp:Label ID="lblPrintContent" runat="server" style="width:100%;"></asp:Label>
        <asp:TextBox ID="txtLocalControllerURL" runat="server" Style="display:none;"></asp:TextBox>
    
         <script type="text/javascript">
             String.prototype.replaceAll = function (search, replacement) {
                 var target = this;
                 return target.split(search).join(replacement);
             };

             var printDelegate = function () {
                 var content = $('#lblPrintContent').html().replaceAll('&nbsp;', ' ').replaceAll("<br>", '\n').replaceAll("&lt", "<").replaceAll("&gt", ">");
                 alert(content);
                 var url = $('#txtLocalControllerURL').val() + '/Print.aspx?Mode=Print';
                 alert(url);
                 var xhr = new XMLHttpRequest();
                 xhr.open("POST", url, true);
                 xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                 xhr.send(content);
             }

         </script>

    </form>
</body>
</html>
