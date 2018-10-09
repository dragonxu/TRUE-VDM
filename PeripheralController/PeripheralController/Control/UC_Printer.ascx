<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Printer.ascx.vb" Inherits="PeripheralController.UC_Printer" %>

<table class="control-box" style="width:100%;">
    <thead>
        <tr>
            <th colspan="2">
               Printer
            </th>
        </tr>
    </thead>
    <tbody>
        <%--<tr>
            <td>Printer</td>
            <td><asp:DropDownList ID="ddlPrinter" runat="server" Width="100%"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>Text</td>
            <td><asp:TextBox ID="txtContent" runat="server" Width="100%" TextMode="MultiLine" Height="50px"></asp:TextBox></td>
        </tr>--%>
        <tr>
            <td colspan="2" style="text-align:center;"><asp:Button  ID="btnPrint" runat="server" style="width:90%; padding:5px;" value="Print" /></td>
        </tr>
    </tbody>
  
</table>