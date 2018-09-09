<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_MoneyStock_UI.ascx.vb" Inherits="VDM.UC_MoneyStock_UI" %>
<div class="row m-a-0 text-uppercase bold mobile_group_head">
    Money Stock Level
</div>
<asp:Repeater ID="rptMoneyStock" runat="server">
    <ItemTemplate>
    <div class="col-sm-4">
        <div class="row m-a-0 text-success" id="divContainer" runat="server">
            <i class="fa fa-circle"></i> <asp:Label ID="lblName" runat="server"></asp:Label> Level
            <span class="pull-right"><asp:Label ID="lblLevel" runat="server"></asp:Label></span>
        </div>                        
        <div class="progress">
                <div class="progress-bar progress-bar-success" role="progressbar" style="width:90%" id="progress" runat="server"></div>
        </div>
    </div>
    </ItemTemplate>
</asp:Repeater>