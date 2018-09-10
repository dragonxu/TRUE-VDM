<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Machine_Console/MaterStaffConsole.Master" CodeBehind="TestStock.aspx.vb" Inherits="VDM.TestStock" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="udpList" runat="server">
        <ContentTemplate>

            <div class="row">
            </div>

            <asp:Panel ID="pnlMachine" runat="server" CssClass="card bg-white" Visible="True">
                <div class="card-header">
                    <h3>Hardware</h3>

                </div>
                <div class="card-block">

                    <div class="col-sm-12">
                        
                            <div class="card card-block no-border bg-white row-equal align-middle">
                                <div class="col-sm-9">

                                    <div class="row m-a-0 text-uppercase bold mobile_group_head">
                                        Peripheral Condition
                                    </div>
                                    <div class="row demo-button">

                                        <span id="MainContent_rptList_Peripheral_0_rptDevice_0_spanDevice_0" class="btn btn-default" title="Coin In">
                                            <img id="MainContent_rptList_Peripheral_0_rptDevice_0_iconDevice_0" src="images/Icon/white/coin.png" style="width: 24px;">
                                            <span id="MainContent_rptList_Peripheral_0_rptDevice_0_lblDeviceName_0">Coin In</span>
                                        </span>

                                        <span id="MainContent_rptList_Peripheral_0_rptDevice_0_spanDevice_1" class="btn btn-default" title="Cash In">
                                            <img id="MainContent_rptList_Peripheral_0_rptDevice_0_iconDevice_1" src="images/Icon/white/cash.png" style="width: 24px;">
                                            <span id="MainContent_rptList_Peripheral_0_rptDevice_0_lblDeviceName_1">Cash In</span>
                                        </span>

                                        <span id="MainContent_rptList_Peripheral_0_rptDevice_0_spanDevice_2" class="btn btn-default" title="Coin 1">
                                            <img id="MainContent_rptList_Peripheral_0_rptDevice_0_iconDevice_2" src="images/Icon/white/coin1.png" style="width: 24px;">
                                            <span id="MainContent_rptList_Peripheral_0_rptDevice_0_lblDeviceName_2">Coin 1</span>
                                        </span>

                                        <span id="MainContent_rptList_Peripheral_0_rptDevice_0_spanDevice_3" class="btn btn-default" title="Cash 20">
                                            <img id="MainContent_rptList_Peripheral_0_rptDevice_0_iconDevice_3" src="images/Icon/white/cash20.png" style="width: 24px;">
                                            <span id="MainContent_rptList_Peripheral_0_rptDevice_0_lblDeviceName_3">Cash 20</span>
                                        </span>

                                        <span id="MainContent_rptList_Peripheral_0_rptDevice_0_spanDevice_4" class="btn btn-default" title="Cash 100">
                                            <img id="MainContent_rptList_Peripheral_0_rptDevice_0_iconDevice_4" src="images/Icon/white/cash100.png" style="width: 24px;">
                                            <span id="MainContent_rptList_Peripheral_0_rptDevice_0_lblDeviceName_4">Cash 100</span>
                                        </span>

                                        <span id="MainContent_rptList_Peripheral_0_rptDevice_0_spanDevice_5" class="btn btn-default" title="Camera">
                                            <img id="MainContent_rptList_Peripheral_0_rptDevice_0_iconDevice_5" src="images/Icon/white/camera.png" style="width: 24px;">
                                            <span id="MainContent_rptList_Peripheral_0_rptDevice_0_lblDeviceName_5">Camera</span>
                                        </span>

                                        <span id="MainContent_rptList_Peripheral_0_rptDevice_0_spanDevice_6" class="btn btn-default" title="Printer">
                                            <img id="MainContent_rptList_Peripheral_0_rptDevice_0_iconDevice_6" src="images/Icon/white/printer.png" style="width: 24px;">
                                            <span id="MainContent_rptList_Peripheral_0_rptDevice_0_lblDeviceName_6">Printer</span>
                                        </span>

                                        <span id="MainContent_rptList_Peripheral_0_rptDevice_0_spanDevice_7" class="btn btn-default" title="Passport">
                                            <img id="MainContent_rptList_Peripheral_0_rptDevice_0_iconDevice_7" src="images/Icon/white/contact.png" style="width: 24px;">
                                            <span id="MainContent_rptList_Peripheral_0_rptDevice_0_lblDeviceName_7">Passport</span>
                                        </span>

                                        <span id="MainContent_rptList_Peripheral_0_rptDevice_0_spanDevice_8" class="btn btn-default" title="Barcode">
                                            <img id="MainContent_rptList_Peripheral_0_rptDevice_0_iconDevice_8" src="images/Icon/white/barcode.png" style="width: 24px;">
                                            <span id="MainContent_rptList_Peripheral_0_rptDevice_0_lblDeviceName_8">Barcode</span>
                                        </span>

                                        <span id="MainContent_rptList_Peripheral_0_rptDevice_0_spanDevice_9" class="btn btn-default" title="SIM Disp">
                                            <img id="MainContent_rptList_Peripheral_0_rptDevice_0_iconDevice_9" src="images/Icon/white/simcard.png" style="width: 24px;">
                                            <span id="MainContent_rptList_Peripheral_0_rptDevice_0_lblDeviceName_9">SIM Disp</span>
                                        </span>

                                    </div>
                                </div>
                                <div class="col-sm-12">
                                </div>
                                <div class="col-sm-9">

                                    <div class="row m-a-0 text-uppercase bold mobile_group_head m-t">
                                        Money Stock Level
                                    </div>

                                    <div class="col-sm-3">
                                        <div id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_divContainer_0" class="row m-a-0 text-success">
                                            <i class="fa fa-circle"></i><span id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_lblName_0">Coin In</span> Level
            <span class="pull-right"><span id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_lblLevel_0">0 / 1000</span></span>
                                        </div>
                                        <div class="progress">
                                            <div id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_progress_0" class="progress-bar progress-bar-success" role="progressbar" style="width: 0%;"></div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_divContainer_1" class="row m-a-0 text-success">
                                            <i class="fa fa-circle"></i><span id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_lblName_1">Cash In</span> Level
            <span class="pull-right"><span id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_lblLevel_1">0 / 500</span></span>
                                        </div>
                                        <div class="progress">
                                            <div id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_progress_1" class="progress-bar progress-bar-success" role="progressbar" style="width: 0%;"></div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_divContainer_2" class="row m-a-0 text-danger">
                                            <i class="fa fa-circle"></i><span id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_lblName_2">Coin 1</span> Level
            <span class="pull-right"><span id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_lblLevel_2">0 / 1000</span></span>
                                        </div>
                                        <div class="progress">
                                            <div id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_progress_2" class="progress-bar progress-bar-danger" role="progressbar" style="width: 0%;"></div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_divContainer_3" class="row m-a-0 text-danger">
                                            <i class="fa fa-circle"></i><span id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_lblName_3">Cash 20</span> Level
            <span class="pull-right"><span id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_lblLevel_3">0 / 500</span></span>
                                        </div>
                                        <div class="progress">
                                            <div id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_progress_3" class="progress-bar progress-bar-danger" role="progressbar" style="width: 0%;"></div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_divContainer_4" class="row m-a-0 text-danger">
                                            <i class="fa fa-circle"></i><span id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_lblName_4">Cash 100</span> Level
            <span class="pull-right"><span id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_lblLevel_4">0 / 500</span></span>
                                        </div>
                                        <div class="progress">
                                            <div id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_progress_4" class="progress-bar progress-bar-danger" role="progressbar" style="width: 0%;"></div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_divContainer_5" class="row m-a-0 text-danger">
                                            <i class="fa fa-circle"></i><span id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_lblName_5">Printer</span> Level
            <span class="pull-right"><span id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_lblLevel_5">0 / 200</span></span>
                                        </div>
                                        <div class="progress">
                                            <div id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_progress_5" class="progress-bar progress-bar-danger" role="progressbar" style="width: 0%;"></div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_divContainer_6" class="row m-a-0 text-danger">
                                            <i class="fa fa-circle"></i><span id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_lblName_6">SIM Disp</span> Level
            <span class="pull-right"><span id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_lblLevel_6">0 / 100</span></span>
                                        </div>
                                        <div class="progress">
                                            <div id="MainContent_rptList_MoneyStock_0_rptMoneyStock_0_progress_6" class="progress-bar progress-bar-danger" role="progressbar" style="width: 0%;"></div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        
                    </div>




                    <div class="row">
                    </div>

                </div>
                <div class="row">
                </div>

            </asp:Panel>




            <asp:Panel ID="pnlProduct" runat="server" CssClass="card bg-white" Visible="True">
                <div class="card-header">
                    <h3>Product</h3>

                </div>
                <div class="card-block">
                    <div class="row ">
                        <div class="col-sm-8">
                        </div>
                        <div class="col-sm-4">
                        </div>
                    </div>


                </div>
            </asp:Panel>

            <div class="row ">
                <div class="col-xs-8">
                    <p></p>
                    <asp:LinkButton ID="lnkShift" runat="server" class="btn btn-info btn-lg btn-block">
                    <i class="icon-settings"></i>
                        <span>Open/Close Shift check in stock</span>

                    </asp:LinkButton>
                    <%-- <button type="button" class="btn btn-info btn-lg btn-block">
                        <i class="icon-settings"></i>
                        <span>Open/Close Shift check in stock</span>
                    </button>--%>
                </div>
                <div class="col-xs-4">
                    <p></p>
                    <button type="button" class="btn btn-warning btn-lg btn-block">
                        <i class="icon-logout"></i>
                        <span>Logout</span>
                    </button>
                </div>

            </div>



            <asp:Panel ID="pnlModalFillPaper" runat="server" Visible="False">
                <div class="sweet-overlay" tabindex="-1" style="opacity: 1.04; display: block;"></div>
                <div class="sweet-alert show-input showSweetAlert visible form-horizontal " style="display: block; width: 600px; margin-top: -200px; left: 50%;">

                    <h3><i class="icon-printer"></i>
                        <span>Fill Paper</span></h3>

                    <div class="card-block">
                        <div class="form-horizontal">

                            <div class="row ">
                                <div class="form-group col-sm-12">
                                    <label class="col-sm-6 control-label" style="padding-top: 17px;">จำนวนครั้งที่พิมพ์ได้ <span style="color: red;">*</span></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtPaperUnit" runat="server" class="form-control" Style="height: 34px; font-size: 0.8125rem; text-align: center;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row ">
                                <div class="form-group col-sm-12">
                                    <label class="col-sm-6 control-label" style="padding-top: 17px;">จำนวนครั้งพิมพ์สูงสุด <span style="color: red;">*</span></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtPaperMax" runat="server" class="form-control" Style="height: 34px; font-size: 0.8125rem; text-align: center;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>

                    <div class="sa-button-container">
                        <asp:LinkButton CssClass="btn btn-success btn-icon loading-demo mr5 btn-shadow" ID="btnOKSpec" runat="server">
                      <i class="fa fa-gavel"></i>
                      <span>Confirm</span>
                        </asp:LinkButton>

                        <asp:LinkButton CssClass="btn btn-default btn-icon loading-demo mr5 btn-shadow" ID="LinkButton1" runat="server">
                      <i class="fa fa-rotate-left"></i>
                      <span>Cancel</span>
                        </asp:LinkButton>

                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlModalKioskSetting" runat="server" Visible="False">
                <div class="sweet-overlay" tabindex="-1" style="opacity: 1.04; display: block;"></div>
                <div class="sweet-alert show-input showSweetAlert visible form-horizontal " style="display: block; width: 600px; margin-top: -200px; left: 50%;">

                    <h3><i class="icon-printer"></i>
                        <span>Kiosk Setting</span></h3>

                    <div class="card-block">
                        <div class="form-horizontal">

                            <div class="row ">
                                <div class="form-group col-sm-12">
                                    <label class="col-sm-6 control-label" style="padding-top: 17px;">Kiosk ID  <span style="color: red;">*</span></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="TextBox1" runat="server" class="form-control" Style="height: 34px; font-size: 0.8125rem; text-align: center;"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <span></span>
                                    </div>
                                </div>
                            </div>
                            <div class="row ">
                                <div class="form-group col-sm-12">
                                    <label class="col-sm-6 control-label" style="padding-top: 17px;">Screen Server  <span style="color: red;">*</span></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="TextBox2" runat="server" class="form-control" Style="height: 34px; font-size: 0.8125rem; text-align: center;"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <span class="col-sm-6 control-label" style="padding-top: 17px;">Sec</span>
                                    </div>
                                </div>
                            </div>
                            <div class="row ">
                                <div class="form-group col-sm-12">
                                    <label class="col-sm-6 control-label" style="padding-top: 17px;">Kiosk Time Out  <span style="color: red;">*</span></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="TextBox3" runat="server" class="form-control" Style="height: 34px; font-size: 0.8125rem; text-align: center;"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <span class="col-sm-6 control-label" style="padding-top: 17px;">Sec</span>
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>

                    <div class="sa-button-container">
                        <asp:LinkButton CssClass="btn btn-success btn-icon loading-demo mr5 btn-shadow" ID="LinkButton2" runat="server">
                      <i class="fa fa-gavel"></i>
                      <span>Confirm</span>
                        </asp:LinkButton>

                        <asp:LinkButton CssClass="btn btn-default btn-icon loading-demo mr5 btn-shadow" ID="LinkButton3" runat="server">
                      <i class="fa fa-rotate-left"></i>
                      <span>Cancel</span>
                        </asp:LinkButton>

                    </div>
                </div>
            </asp:Panel>



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
