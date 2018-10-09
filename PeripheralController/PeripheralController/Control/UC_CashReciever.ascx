<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_CashReciever.ascx.vb" Inherits="PeripheralController.UC_CashReciever" %>

<table class="control-box" style="width:100%;">
    <thead>
        <tr>
            <th colspan="2">
                Cash Reciever
            </th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Cash Receiver Port</td>
            <td><asp:DropDownList ID="ddlCash" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>Coin Receiver Port</td>
            <td><asp:DropDownList ID="ddlCoin" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center;"><input type="button" ID="btnStart" runat="server" style="width:90%; padding:5px;" value="Start Recieve" /></td>
        </tr>
    </tbody>
    <tfoot>
        <tr>
            <td colspan="2" style="text-align:center; min-height:30px;">
                <asp:Label ID="lblResult" runat="server"></asp:Label>
            </td>
        </tr>
    </tfoot>
</table>