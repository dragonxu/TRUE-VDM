<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Printer_Stock_UI.ascx.vb" Inherits="VDM.UC_Printer_Stock_UI" %>
<asp:Panel ID="pnlPrinterStock" runat="server" CssClass="row text-danger p-t">
    <i class="icon-printer"></i> Printing Paper Level
    <span class="pull-right"><asp:Label ID="lblPrintPaper" runat="server"></asp:Label></span> <%--6 / 100--%>
</asp:Panel>                        
<asp:Panel ID="pnlPrinterProgress" runat="server" CssClass="progress">
    <asp:Panel ID="barPrinterLevel" runat="Server" CssClass="progress-bar progress-bar-danger" role="progressbar" Width="10%"></asp:Panel>
</asp:Panel>