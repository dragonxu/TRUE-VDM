<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Front_UI/MasterFront.Master" CodeBehind="Product_List.aspx.vb" Inherits="VDM.Product_List" %>

<%@ Register Src="~/PageNavigation.ascx" TagName="PageNavigation" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="devices">
        <div class="main3">
            <asp:Label ID="lblBrand_ID" runat="server" styles="display:none;"></asp:Label>

            <div class="pic-devices" style="margin-top: unset; padding-top: 100px;">
                <h3 class="true-l">Devices</h3>
            </div>

           <%-- ลองดึงข้อมูล Product ใส่ ตาราง--%>
            <asp:Panel ID="pnlTable" runat="server" Visible="false">
                <div class="frame">
                    <div class="no-more-tables">

                        <table class="table table-bordered  m-b-0">
                            <thead>
                                <tr>
                                    <th>CODE</th>
                                    <th>BRAND</th>
                                    <th>Product</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptList" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td data-title="PRODUCT" align="center">
                                                <asp:Image ID="img" runat="server" Width="60px"></asp:Image><br />
                                                <asp:Label ID="lblProductCode" runat="server" Style="display: none"></asp:Label>
                                                <asp:Image ID="imgBrand" runat="server" Width="60px" Style="display: none" />
                                                <asp:Label ID="lblDisplayName" runat="server" Style="display: none"></asp:Label>
                                            </td>

                                            <td data-title="Product">
                                                <asp:Label ID="lblModel" runat="server"></asp:Label></td>


                                            <td data-title="Price (Baht)" style="text-align: right;">
                                                <asp:Label ID="lblPrice" runat="server"></asp:Label>
                                                <asp:Button CssClass="btn btn-sm btn-success btn-shadow" ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" Style="display: none" />
                                            </td>


                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                    <uc1:PageNavigation ID="Pager" runat="server" PageSize="10" />
                </div>

            </asp:Panel>


            <div class="row"></div>
            <ul style="display: unset;">
                <asp:Repeater ID="rptBox" runat="server">
                    <ItemTemplate>
                        <li class="col-md-3"><a id="btnBrand" runat="server">
                            <p class="true-m">
                                <asp:Label ID="lblBrand" runat="server"></asp:Label>
                            </p>
                            <asp:Image ID="img" runat="server" Width="250px"></asp:Image></a></li>
                        <asp:Button ID="btnSelect" runat="server" Style="display: none;" CommandName="Select" />
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
