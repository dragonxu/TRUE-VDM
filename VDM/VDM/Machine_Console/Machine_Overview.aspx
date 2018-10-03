<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Machine_Console/MasterStaffConsole.Master" CodeBehind="Machine_Overview.aspx.vb" Inherits="VDM.Machine_Overview" %>

<%@ Register Src="~/UC_Peripheral_UI.ascx" TagPrefix="uc1" TagName="UC_Peripheral_UI" %>
<%@ Register Src="~/UC_MoneyStock_UI.ascx" TagPrefix="uc1" TagName="UC_MoneyStock_UI" %>
<%@ Register Src="~/UC_Kiosk_Shelf.ascx" TagPrefix="uc1" TagName="UC_Kiosk_Shelf" %>
<%@ Register Src="~/Machine_Console/UC_SIM_Dispenser.ascx" TagPrefix="uc1" TagName="UC_SIM_Dispenser" %>

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
                    <h3 class="row m-a-0 m-b text-uppercase bold mobile_group_head">
                        Product Stock
                    </h3>
                </div>
                <div class="row ">
                    <uc1:UC_Kiosk_Shelf runat="server" ID="Shelf" />
                    <div class="form-group" style="text-align:right">
                            <asp:LinkButton CssClass="btn btn-success btn-icon loading-demo mr5 btn-shadow" ID="btnSaveProduct" runat="server">
                              <i class="fa fa-save"></i>
                              <span>Save</span>
                            </asp:LinkButton>

                            <asp:LinkButton CssClass="btn btn-default btn-icon loading-demo mr5 btn-shadow" ID="btnResetProduct" runat="server">
                              <i class="fa fa-reply"></i>
                              <span>Reset</span>
                            </asp:LinkButton>
                      </div>
                </div>
            </div>

            <div class="card-block">
                <div class="row ">
                    <h3>
                        <div class="row m-a-0 m-b text-uppercase bold mobile_group_head">
                            SIM Stock
                        </div>
                    </h3>
                </div>
                <div class="row ">
                    <uc1:UC_SIM_Dispenser runat="server" ID="Dispenser" />
                    
                </div>
            </div>

            <div class="row ">
                <div class="col-xs-12">
                    <p></p>
                    <asp:LinkButton ID="lnkShift" runat="server" class="btn btn-info btn-lg btn-block">
                    <i class="icon-settings"></i>
                        <span>Open/Close Shift check in stock</span>

                    </asp:LinkButton>

                </div>
                

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>
