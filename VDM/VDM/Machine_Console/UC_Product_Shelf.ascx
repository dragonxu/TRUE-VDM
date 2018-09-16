<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Product_Shelf.ascx.vb" Inherits="VDM.UC_Product_Shelf" %>
<%@ Register Src="~/Machine_Console/UC_Product_Floor.ascx" TagPrefix="uc1" TagName="UC_Product_Floor" %>

    
<asp:LinkButton CssClass="h2 btn btn-lg btn-info btn-shadow m-t-0 m-b" ID="lnkEdit"  Width="100%" runat="server" >
    <i class="fa fa-th"></i> Product Shelf
</asp:LinkButton>
<asp:Panel ID="Shelf" runat="server" CssClass="machine_stock btn-shadow" PixelPerMM="0.25" SHELF_WIDTH="0" SHELF_HEIGHT="0" SHELF_DEPTH="0">
    
    <asp:Repeater ID="rptFloor" runat="server">
        <ItemTemplate>
            <uc1:UC_Product_Floor runat="server" ID="Floor" />
        </ItemTemplate>
    </asp:Repeater>

</asp:Panel>
<asp:LinkButton CssClass="h2 btn btn-lg btn-info btn-shadow m-t m-b-0" ID="lnkAddFloor"  Width="100%" runat="server" >
    <i class="fa fa-plus"></i> Add Floor
</asp:LinkButton>