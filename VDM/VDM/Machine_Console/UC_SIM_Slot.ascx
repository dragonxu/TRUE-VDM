<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_SIM_Slot.ascx.vb" Inherits="VDM.UC_SIM_Slot" %>
<%@ Register Src="~/Machine_Console/UC_SIM.ascx" TagPrefix="uc1" TagName="UC_SIM" %>

<asp:Panel ID="pnlContainer" runat="server" CssClass="col-sm-4 p-t-0 sim_container" DEVICE_ID="0" >  
        <div class="row p-a-md m-t-0 text-center">
            <h4 class="row m-t-0 bold"><asp:Label ID="lblName" runat="server" Text="0"></asp:Label></h4>
            <h6 class="bold text-default m-t-0 m-b-0">Max Capacity : <asp:Label ID="lblMaxQuantity" runat="server"></asp:Label></h6>
            <div class="row sim_box">
                <asp:Panel ID="pnlSlot" runat="server" CssClass="btn-shadow sim_slot">
                    <asp:Repeater ID="rptSIM" runat="server">
                        <ItemTemplate>
                            <uc1:UC_SIM runat="server" id="SIM" />
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="pnlPointer" runat="server" CssClass="sim_quantity_pointer">
                       <small><asp:Label ID="lblQuanity" runat="server" ForeColor="DimGray"></asp:Label></small>
                    </asp:Panel>
                </asp:Panel>
            </div>
            <h4 class="bold text-default m-t-0" ID="pnlEmpty" runat="server" visible="false">Empty</h4>
            <asp:Panel CssClass="profile-avatar" ID="pnlProfile" runat="server" Visible="false" style="padding:5px; margin:0; max-width:400px;">
                <asp:Image CssClass="product-Image btn-shadow m-b" ID="imgSIM" runat="server" />
             <span class="bold text-default-darker h5"></span><asp:Label CssClass="bold text-deeppurple h5" ID="lblSIMCode" runat="server">3000065137</asp:Label>
                <h5 class="bold">ราคา <asp:Label ID="lblPrice" runat="server" CssClass="text-blue"></asp:Label> ฿</h5>
            </asp:Panel>
        </div>
    <asp:Button ID="btnSelect" runat="server" style="display:none;" />
</asp:Panel>