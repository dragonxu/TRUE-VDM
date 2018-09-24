<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Product_Shelf.ascx.vb" Inherits="VDM.UC_Product_Shelf" %>
<%@ Register Src="~/Machine_Console/UC_Product_Floor.ascx" TagPrefix="uc1" TagName="UC_Product_Floor" %>

    
<asp:LinkButton CssClass="h2 btn btn-lg btn-info btn-shadow m-t-0 m-b" ID="lnkEdit"  Width="100%" runat="server" >
    <i class="fa fa-th"></i> Product Shelf
</asp:LinkButton>
<asp:Panel ID="Shelf" runat="server" CssClass="machine_stock btn-shadow"  PixelPerMM="0.25" SHELF_DEPTH="1200" SHELF_ID="0">
    <div class="machine_block_container" >    
        <asp:Repeater ID="rptFloor" runat="server">
            <ItemTemplate>
                <uc1:UC_Product_Floor runat="server" ID="Floor"  
                    OnRequestAddFloor="Floor_RequestAddFloor"
                    OnRequestEdit="Floor_RequestEdit"
                    OnRequestRemove="Floor_RequestRemove"
                    OnRequestClearSlot="Floor_RequestClearSlot"
                    OnRequestClearProduct="Floor_RequestClearProduct"
                    OnRequestAddSlot="Floor_RequestAddSlot"
                    OnSlotSelecting="Slot_Selecting"
                 />
            </ItemTemplate>
        </asp:Repeater>

        <asp:PlaceHolder ID="pnlScale" runat="server">
            <div style="position:absolute; right:-40px; bottom:-40px; font-size:12px;">0,0</div>
            <div style="position:absolute; left:-120px; top:-40px; width:100px; text-align:right;  font-size:12px;"><asp:Label ID="lblWidth" runat="server">1900</asp:Label>,<asp:Label ID="lblHeight" runat="server">1900</asp:Label></div>
        </asp:PlaceHolder>
        
    </div>
</asp:Panel>
<asp:LinkButton CssClass="h2 btn btn-lg btn-info btn-shadow m-t m-b-0" ID="lnkAddFloor"  Width="100%" runat="server" >
    <i class="fa fa-plus"></i> Add Floor
</asp:LinkButton>