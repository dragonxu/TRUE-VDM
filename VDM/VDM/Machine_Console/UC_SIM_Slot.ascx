<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_SIM_Slot.ascx.vb" Inherits="VDM.UC_SIM_Slot" %>
<%@ Register Src="~/Machine_Console/UC_SIM.ascx" TagPrefix="uc1" TagName="UC_SIM" %>

<asp:Panel ID="pnlContainer" runat="server" class="col-sm-4 p-t-0 sim_container" SLOT_ID="0" KO_ID="0" DEVICE_ID="0" >  
        <div class="row p-a-md m-t-0 text-center">
            <h4 class="row m-t-0">SLOT <asp:Label ID="lblID" runat="server"></asp:Label></h4>
            <div class="row sim_box">
                <asp:Panel ID="pnlSlot" runat="server" CssClass="btn-shadow sim_slot" Height="300px">
                    <asp:Repeater ID="rptSIM" runat="server">
                        <ItemTemplate>
                            <uc1:UC_SIM runat="server" id="SIM" />
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Panel style="bottom:-2px;" ID="pnlPointer" runat="server" >
                       <small>< <asp:Label ID="lblQuanity" runat="server"></asp:Label></small>
                    </asp:Panel>
                </asp:Panel>

            </div>
            <h3 class="bold text-default m-t-0" ID="pnlEmpty" runat="server">Empty</h3>
            <asp:Panel CssClass="profile-avatar" ID="pnlProfile" runat="server" style="padding:5px; margin:0;">
                <asp:Image CssClass="product-Image btn-shadow" ID="imgSIM" runat="server" />
                <asp:Label CssClass="bold text-deeppurple h5" ID="lblSIMCode" runat="server">3000065137</asp:Label>
                <h6 class="bold">ราคา <asp:Label ID="lblPrice" runat="server" CssClass="text-blue"></asp:Label> ฿</h6>
            </asp:Panel>
        </div>
    </asp:Panel>