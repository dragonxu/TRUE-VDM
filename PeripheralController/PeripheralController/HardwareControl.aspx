<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="HardwareControl.aspx.vb" Inherits="PeripheralController.HardwareControl" %>

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
                    <table class="control-box" style="background:#aaaaaa; width:100%;">
                        <thead>
                            <tr>
                                <th colspan="2">
                                    Cash Reciever
                                </th>
                            </tr>
                        </thead>
                        
                        <tr>
                            <td>Port</td>
                            <td><asp:DropDownList ID="ddl"></asp:DropDownList></td>
                        </tr>
                    </table>
                </td>
                <td style="width:33%; padding:20px;">

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
