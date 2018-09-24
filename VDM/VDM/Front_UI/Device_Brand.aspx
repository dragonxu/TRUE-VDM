<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Front_UI/MasterFront.Master"   CodeBehind="Device_Brand.aspx.vb" Inherits="VDM.Device_Brand" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="devices">
    <div class="main3">
      <div class="pic-devices"><h3 class="true-l">Brands</h3><img src="images/pic-top_catalog.png"/></div>
 

        <ul>
           <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <li class="col-md-4"><a id="btnBrand" runat ="server" ><p class="true-m"><asp:Label ID="lblBrand" runat ="server" ></asp:Label></p><asp:Image ID="img" runat="server"  Width="311px"></asp:Image></a></li>
                        <asp:Button ID="btnSelect" runat ="server" Style ="display:none;" CommandName ="Select" />
                         </ItemTemplate>
           </asp:Repeater>
        </ul>

    </div>
  </div>
     <footer>
                <nav>
                    <div class="main">
                        <span class="col-md-6">
                            <asp:ImageButton ID="lnkHome" runat="server" ImageUrl="images/btu-home.png" />
                            

                        </span>
                        <span class="col-md-6"><a href="javascript:history.back();">
                            <img src="images/btu-prev.png" /></a></span>
                    </div>
                </nav>
            </footer>
</asp:Content>
