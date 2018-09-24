<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PrintBarcode.aspx.vb" Inherits="VDM.PrintBarcode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        table {
            font-size:18px;
            font-family:Arial;
        }
        td {
            padding:10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <asp:Repeater ID="rptSerial" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><asp:Label ID="lblCode" runat="server"></asp:Label></td>
                        <td><asp:Label ID="lblName" runat="server"></asp:Label></td>
                        <td><asp:Image ID="img" runat="server" Width="200px" Height="80px"/></td>
                        <td><asp:Label ID="lblSerial" runat="server"></asp:Label></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            
            <asp:Repeater ID="rptNon" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><asp:Label ID="lblCode" runat="server"></asp:Label></td>
                        <td><asp:Label ID="lblName" runat="server"></asp:Label></td>
                        <td><asp:Image ID="img" runat="server" Width="200px" Height="80px"/></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    </form>
</body>
</html>
