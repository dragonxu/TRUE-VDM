<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Peripheral_UI.ascx.vb" Inherits="VDM.UC_Peripheral_UI" %>
    <div class="row m-a-0 text-uppercase bold mobile_group_head m-b">
        Peripheral Condition
    </div>
    <div class="row demo-button">
        <asp:Repeater ID="rptDevice" runat="server">
            <ItemTemplate>
                <span class="btn btn-success" title="Coin In" id="spanDevice" runat="Server">
                        <asp:Image ID="iconDevice" runat="server" Width="24px" />
                        <asp:Label ID="lblDeviceName" runat="Server"></asp:Label>
                </span>
            </ItemTemplate>
        </asp:Repeater>
    </div>         