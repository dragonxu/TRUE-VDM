<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Product_Slot.ascx.vb" Inherits="VDM.UC_Product_Slot" %>
<asp:LinkButton class="machine_slot" ID="Slot" runat="server" PixelPerMM="0.25" SLOT_ID="0" POS_X="0" SLOT_WIDTH="0" Width="1px" style="left:0px;" >

    <asp:Label ID="lblName" runat="server" CssClass="slot_caption"></asp:Label>
    <asp:Label ID="lblCode" runat="server" CssClass="slot_product_code"></asp:Label>
    <asp:Label ID="lblQuantity" runat="server" CssClass="slot_quantity" ForeColor="White"></asp:Label>
    <asp:Panel CssClass="machine_slot_quantity_bar" ID="QuantityBar" runat="server" BackColor="#eeeeee" PRODUCT_ID="0" Visible="false">
        <asp:Panel CssClass="machine_slot_quantity_level" ID="QuantityLevel" runat="server" BackColor="White">
        </asp:Panel>
    </asp:Panel>

</asp:LinkButton>