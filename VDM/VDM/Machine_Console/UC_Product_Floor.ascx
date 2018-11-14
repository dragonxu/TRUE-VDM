<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Product_Floor.ascx.vb" Inherits="VDM.UC_Product_Floor" %>
<%@ Register Src="~/Machine_Console/UC_Product_Slot.ascx" TagPrefix="uc1" TagName="UC_Product_Slot" %>

<asp:Panel CssClass="machine_floor" ID="Floor" runat="server" FLOOR_ID="0" Height="0px" >
    
    <asp:LinkButton CssClass="btn btn-info btn-lg btn-block machine_floor_label btn-shadow" ID="CaptionBlock" runat="server">
                <asp:Label ID="FloorLabel" runat="server"></asp:Label>
    </asp:LinkButton>
    <asp:Panel ID="pnlMenu" runat="server" CssClass="machine_menu_block" >
        <div class="btn-group btn-shadow">
            <button type="button" class="btn btn-info btn-shadow" data-toggle="dropdown">
                <i class="fa fa-chevron-circle-left"></i>
                Action
            </button>
            <ul class="dropdown-menu" role="menu">
            <li>
                <asp:LinkButton ID="mnuFloorSetting" runat="server">Floor setting</asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="mnuAddFloor" runat="server">Add next floor</asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="mnuAddSlot" runat="server">Add slot</asp:LinkButton>
            </li>                                
            <li class="divider" id="mnuDivider" runat="server"></li>
            <li id="liRemoveSlot" runat="server">
                <asp:LinkButton ID="mnuClearAllSlot" runat="server">Remove all slot</asp:LinkButton>
            </li>
            <li style="display:none;">
                <asp:LinkButton ID="mnuClearAllProduct" runat="server" Visible="false">Remove all product</asp:LinkButton>
            </li>
            <li id="liRemoveFloor" runat="server">
                <asp:LinkButton ID="mnuRemoveFloor" runat="server">Remove this floor</asp:LinkButton>
            </li>
            </ul>
        </div>
    </asp:Panel>
    <div class="machine_block_container">
        <asp:Repeater ID="rptSlot" runat="server">
            <ItemTemplate>
                <uc1:UC_Product_Slot runat="server" id="Slot" onSelecting="Slot_Selecting" />
            </ItemTemplate>
        </asp:Repeater> 
    </div> 

    <asp:PlaceHolder ID="pnlScale" runat="server">
         <div class="slot_scale bottom-0 left-35" style="z-index:4; width:30px; text-align:right;">
            <div class="width100pc height100pc">
                <asp:Label ID="lblY" runat="server">0</asp:Label>
            </div>        
        </div>
        <div class="slot_scale top-0 left-35" style="z-index:4;width:30px; text-align:right;">
            <div class="width100pc height100pc">
                <asp:Label ID="lblY1" runat="server">0</asp:Label>
            </div>        
        </div>
        <div class="slot_scale height100pc slot_floor_height scale-left-red">
            <div class="width100pc height100pc">
                <asp:Label ID="lblHeight" runat="server">0</asp:Label>
            </div>        
        </div>
    </asp:PlaceHolder>
</asp:Panel>    
