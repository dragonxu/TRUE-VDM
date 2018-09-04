<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Test_TMN_Payment.aspx.vb" Inherits="VDM.Test_TMN_Payment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body{
            font-family:'Arial Unicode MS';           
        }
        table input,table textarea {
            width:100%;
        }
       
    </style>
    <!-- Custom scripts -->
     <script src="scripts/txtClientControl.js" type="text/javascript"></script>
      <!-- End Custom scripts -->
</head>
<body>
    <form id="form1" runat="server">
    <h2>Test True Money</h2>
        <table width="500">
            <tr><td width="50%">Invoice_NO <asp:Button ID="btnGen" runat="server" Text="Gen" style="width:unset;" /></td>
                <td width="50%"><asp:TextBox ID="Invoice_NO" runat="server"></asp:TextBox> </td>
            </tr>
            <tr><td>Amount</td>
                <td><asp:TextBox ID="Amount" runat="server"></asp:TextBox></td>
            </tr>
            <tr><td>CustomerQRCode</td>
                <td><asp:TextBox ID="CustomerQRCode" runat="server"></asp:TextBox></td>
            </tr>
            <tr><td>PaymentDescription</td>
                <td><asp:TextBox ID="PaymentDescription" runat="server"></asp:TextBox></td>
            </tr>
             <tr><td>shopCode</td>
                <td><asp:TextBox ID="shopCode" runat="server" Text="001"></asp:TextBox></td>
            </tr>
        </table>
        <asp:Button ID ="btnTest" runat="server" Text="Pay" /> <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        <table width="1000">
            <tr>
                <td><h2>Request</h2></td>
                <td><h2>Response</h2></td>
            </tr>
            <tr>
                <td width="600">
                    <table width="600">
                        <tr>
                            <td>X-API-Key</td>
                            <td><asp:TextBox ID="X_API_Key" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>X-API-Version</td>
                            <td><asp:TextBox ID="X_API_Version" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Content-Signature</td>
                            <td><asp:TextBox ID="Content_Signature" runat="server" TextMode="MultiLine" Height="180px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>TIMESTAMP</td>
                            <td><asp:TextBox ID="TIMESTAMP" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Content-type</td>
                            <td><asp:TextBox ID="Content_type" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>PostString</td>
                            <td><asp:TextBox TextMode="MultiLine" ID="RequestString" runat="server" Height="180px"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>               
                <td valign="top" width="400"> 
                    <asp:TextBox TextMode="MultiLine" ID="ResponseString" runat="server" Width="400px" Height="300px" ></asp:TextBox>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
