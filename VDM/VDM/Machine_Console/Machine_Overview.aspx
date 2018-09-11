<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Machine_Console/MasterStaffConsole.Master" CodeBehind="Machine_Overview.aspx.vb" Inherits="VDM.Machine_Overview" %>

<%@ Register Src="~/UC_Peripheral_UI.ascx" TagPrefix="uc1" TagName="UC_Peripheral_UI" %>
<%@ Register Src="~/UC_MoneyStock_UI.ascx" TagPrefix="uc1" TagName="UC_MoneyStock_UI" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="udpList" runat="server">
        <ContentTemplate>
            <div class="card-block">
                <div class="row ">
                    <h3>
                        <uc1:UC_Peripheral_UI runat="server" ID="UC_Peripheral_UI" />
                    </h3>
                </div>
            </div>


            <div class="card-block">
                <div class="row ">
                    <h3>
                        <uc1:UC_MoneyStock_UI runat="server" ID="UC_MoneyStock_UI" />
                    </h3>
                </div>
            </div>

            <div class="card-block">
                <div class="row ">
                    <h3>
                        <div class="row m-a-0 text-uppercase bold mobile_group_head">
                            Product Stock
                        </div>
                    </h3>
                </div>
            </div>

            <div class="card-block">
                <div class="row ">
                    <h3>
                        <div class="row m-a-0 text-uppercase bold mobile_group_head">
                            SIM Stock
                        </div>
                    </h3>
                </div>
            </div>

            <div class="row ">
                <div class="col-xs-8">
                    <p></p>
                    <asp:LinkButton ID="lnkShift" runat="server" class="btn btn-info btn-lg btn-block">
                    <i class="icon-settings"></i>
                        <span>Open/Close Shift check in stock</span>

                    </asp:LinkButton>

                </div>
                <div class="col-xs-4">
                    <p></p>
                    <asp:LinkButton ID="lnklogout" runat="server" class="btn btn-warning btn-lg btn-block">
                    <i class="icon-logout"></i>
                        <span>Logout</span>

                    </asp:LinkButton>

                </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>
