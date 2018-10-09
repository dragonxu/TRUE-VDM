<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="HardwareControl.aspx.vb" Inherits="PeripheralController.HardwareControl" %>

<%@ Register Src="~/Control/UC_CashReciever.ascx" TagPrefix="uc1" TagName="UC_CashReciever" %>
<%@ Register Src="~/Control/UC_Printer.ascx" TagPrefix="uc1" TagName="UC_Printer" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" >
        <table width="100%">
            <tr>
                <td style="width:33%; padding:20px;">
                    <uc1:UC_CashReciever runat="server" id="CashReciever" />
                </td>
                <td style="width:33%; padding:20px;">
                    <uc1:UC_Printer runat="server" ID="Printer" />
                </td>
                <td style="width:33%; padding:20px;">

                </td>
            </tr>
            <tr>

            </tr>
        </table>
    </form>
</body>
</html>
