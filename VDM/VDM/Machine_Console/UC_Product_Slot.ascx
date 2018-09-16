<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Product_Slot.ascx.vb" Inherits="VDM.UC_Product_Slot" %>
<asp:Panel CssClass="machine_slot" ID="Slot" runat="server" PixelPerMM="0.25" SLOT_ID="0" Width="1px" style="right:0px;" >

    <asp:Label ID="lblName" runat="server" CssClass="slot_caption"></asp:Label>
    <asp:Label ID="lblCode" runat="server" CssClass="slot_product_code"></asp:Label>
    <asp:Label ID="lblQuantity" runat="server" CssClass="slot_quantity" ForeColor="White"></asp:Label>
    <asp:Panel CssClass="machine_slot_quantity_bar" ID="QuantityBar" runat="server" BackColor="#eeeeee" PRODUCT_ID="0" Visible="false">
        <asp:Panel CssClass="machine_slot_quantity_level" ID="QuantityLevel" runat="server" BackColor="White">
        </asp:Panel>
    </asp:Panel>
    <div class="slot_scale top-25 left-25">
        <asp:Label ID="lblXT" runat="server">0</asp:Label>,<asp:Label ID="lblYT" runat="server">0</asp:Label>
    </div>
    <div class="slot_scale text-center bottom-25 width100pc">
        <asp:Label ID="lblWidth" runat="server">0</asp:Label>
    </div>
    <asp:Panel id="pnlCoor" runat="server" CssClass="slot_scale text-left bottom-25">
        <asp:Label ID="lblXB" runat="server">0</asp:Label>,<asp:Label ID="lblYB" runat="server">0</asp:Label>
    </asp:Panel>
    <div class="slot_scale height100pc slot_scale_height">
        <div class="width100pc height100pc">
            <asp:Label ID="lblHeight" runat="server">0</asp:Label>
        </div>        
    </div>
</asp:Panel>

 <asp:Button ID="btnSelect" runat="server" style="display:none;" />