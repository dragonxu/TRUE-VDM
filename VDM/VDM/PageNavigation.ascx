<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PageNavigation.ascx.vb" Inherits="VDM.PageNavigation" %>

<nav>
    <ul class="pagination pagination-sm  pagination-gap">
        <li class="pagination-items"><asp:LinkButton ID="btnFirst" SourceName="" PageSize="20" CurrentPage="0" MaximunPageCount="4" runat="server" ToolTip="First Page"><i class='fa fa-angle-double-left'></i></asp:LinkButton></li>
        <li class="pagination-items"><asp:LinkButton ID="btnBack" runat="server" ToolTip="Previous Page"><i class='fa fa-angle-left'></i></asp:LinkButton></li>
        <asp:Repeater ID="rptPage" runat="server">
                    <ItemTemplate>     
        <li class="pagination-items" id="liPage" runat="server"><asp:LinkButton  ID="btnPage" runat="server" ToolTip="Go to Page">1</asp:LinkButton></li>
                    </ItemTemplate>
        </asp:Repeater>
        <li class="pagination-items"><asp:LinkButton ID="btnNext" runat="server" ToolTip="Next Page"><i class='fa fa-angle-right'></i></asp:LinkButton></li>
        <li class="pagination-items"><asp:LinkButton ID="btnLast" runat="server" ToolTip="Last Page"><i class='fa fa-angle-double-right'></i></asp:LinkButton></li>
    </ul>
</nav>


