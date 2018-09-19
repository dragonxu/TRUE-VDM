<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Machine_Console/MasterStaffConsole.Master" CodeBehind="Manage_OpenClose_Shift.aspx.vb" Inherits="VDM.Manage_OpenClose_Shift" %>

<%@ Register Src="~/Machine_Console/UC_Shift_Change.ascx" TagPrefix="uc1" TagName="UC_Shift_Change" %>
<%@ Register Src="~/Machine_Console/UC_Shift_StockPaper.ascx" TagPrefix="uc1" TagName="UC_Shift_StockPaper" %>
<%@ Register Src="~/Machine_Console/UC_Shift_Recieve.ascx" TagPrefix="uc1" TagName="UC_Shift_Recieve" %>
<%@ Register Src="~/Machine_Console/UC_Shift_StockProduct.ascx" TagPrefix="uc1" TagName="UC_Shift_StockProduct" %>
<%@ Register Src="~/Machine_Console/UC_Shift_StockSIM.ascx" TagPrefix="uc1" TagName="UC_Shift_StockSIM" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="udpList" runat="server">
        <ContentTemplate>
            <div class="card-block">
                <div class="row">

                    <div class="display-columns">
                        <div class="column contacts-sidebar hidden-xs bg-white b-r" style="width: 350px;">
                            <div class="scroll">
                                <div class="p-a">
                                    <nav role="navigation">

                                        <ul class="nav nav-stacked nav-pills">


                                            <li style="margin-top: -5px;">

                                                <h2><b>สิ่งที่ทำ</b></h2>
                                            </li>
                                            <li id="MenuChange" runat="server">

                                                <a id="lnkChange" runat="server">
                                                    <h3><span class="btn btn-primary btn-outline btn-round" style="padding-right: 0.75rem; padding-left: 0.75rem; font-size: 20px;">1</span>
                                                        <span>จำนวนเงินทอน</span>

                                                        <div id="divMenuChange" runat ="server" class="message-sender" style="text-align: right;">
                                                            <p><span class=" h5 text-info">จำนวนเงิน</span><span class=" h5"> <asp:Label ID="lbl_Change_Amount" runat ="server" ></asp:Label> บาท</span></p>
                                                        </div>
                                                    </h3>
                                                </a>
                                            </li>


                                            <li id="MenuRecieve" runat="server">
                                                <a id="lnkRecieve" runat="server">
                                                    <h3><span class="btn btn-success btn-outline btn-round" style="padding-right: 0.75rem; padding-left: 0.75rem; font-size: 20px;">2</span>
                                                        <span>จำนวนเงินรับ</span>
                                                        <div id="divMenuRecieve" runat ="server" class="message-sender" style="text-align: right;">
                                                            <p><span class=" h5 text-info">จำนวนเงิน</span><span class=" h5"> <asp:Label ID="lbl_Recieve_Amount" runat ="server" ></asp:Label> บาท</span></p>
                                                        </div>
                                                    </h3>
                                                </a>
                                            </li>
                                            <li id="MenuStockProduct" runat="server">
                                                <a id="lnkStockProduct" runat="server">
                                                    <h3><span class="btn btn-info btn-outline btn-round" style="padding-right: 0.75rem; padding-left: 0.75rem; font-size: 20px;">3</span>
                                                        <span>Stock สินค้า</span>
                                                        <div id="divMenuStockProduct" runat ="server" class="message-sender" style="text-align: right;">
                                                            <p><span class=" h5 text-info">สินค้า</span><span class=" h5"> <asp:Label ID="lbl_Product_Count" runat ="server" ></asp:Label> รายการ</span></p>
                                                        </div>
                                                    </h3>
                                                </a>
                                            </li>
                                            <li id="MenuStockSIM" runat="server">
                                                <a id="lnkStockSIM" runat="server">
                                                    <h3><span class="btn btn-warning btn-outline btn-round" style="padding-right: 0.75rem; padding-left: 0.75rem; font-size: 20px;">4</span>
                                                        <span>Stock SIM</span>
                                                        <div id="divMenuStockSIM" runat ="server"  class="message-sender" style="text-align: right;">
                                                            <p><span class=" h5 text-info">SIM</span><span class=" h5"> <asp:Label ID="lbl_SIM_Count" runat ="server" ></asp:Label> รายการ</span></p>
                                                        </div>
                                                    </h3>
                                                </a>
                                            </li>
                                            <li id="MenuStockPaper" runat="server">
                                                <a id="lnkStockPaper" runat="server">
                                                    <h3><span class="btn btn-dark btn-outline btn-round" style="padding-right: 0.75rem; padding-left: 0.75rem; font-size: 20px;">5</span>
                                                        <span>Stock กระดาษพิมพ์</span>
                                                        <div id="divMenuStockPaper" runat ="server" class="message-sender" style="text-align: right;">
                                                            <p><span class=" h5 text-info">พิมพ์ได้</span><span class=" h5"> <asp:Label ID="lbl_Paper_Count" runat ="server" ></asp:Label> ครั้ง</span></p>
                                                        </div>
                                                    </h3>
                                                </a>
                                            </li>
                                            <li>
                                                <div class="col-xs-12" style="text-align: left;">
                                                    <div class="fa-hover text-default   ">
                                                        <a id="lnkBack" runat="server" title="Back / กลับไปหน้า Overview">
                                                            <h2><i class="fa fa-angle-double-left"></i><span class="h3" style="vertical-align: middle;">
                                                                กลับไปหน้า Overview </span>     </h2>
                                                        </a>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>
                                    </nav>
                                </div>
                            </div>
                        </div>
                        <div class="column contact-view" style="margin-top: -20px;">
                            <%--<div class="p-a">
                                <div class="pull-left p-r" style="text-align: center;">
                                    <img id="imgLogo" class="avatar avatar-md" src="../images/Icon/koisk_ab.png" style="width: 30%;">
                                </div>
                                <div class="overflow-hidden">
                                    <p><span class="lead h4 m-t-0 text-info"><b>Open Date  : </b></span><span class=" h4  ">xx/xx/xx</span>  &nbsp; &nbsp; <i class="fa fa-user"></i>&nbsp; User Name </p>
                                    <p><span class="lead h4 m-t-0 text-info"><b>Close Date  : </b></span></b><span class=" h4 ">xx/xx/xx</span>  &nbsp; &nbsp; <i class="fa fa-user"></i>&nbsp; User Name </p>

                                </div>
                            </div>--%>



                            <asp:Panel ID="pnlChange" runat="server">
                                <div class="p-a">
                                    <div class="pull-left p-r">
                                    </div>
                                    <div class="overflow-hidden">
                                        <h1><b>จำนวนเงินทอน</b>  
                                            <img id="imgChange" src="../images/Icon/green/change.png" width="40">
                                        </h1>
                                    </div>
                                </div>

                                <h4>
                                    <uc1:UC_Shift_Change runat="server" ID="UC_Shift_Change" />
                                </h4>
                            </asp:Panel>
                            <asp:Panel ID="pnlRecieve" runat="server">
                                <div class="p-a">
                                    <div class="overflow-hidden">
                                        <h1><b>จำนวนเงินรับ</b>  
                                            <img id="imgRecieve" src="../images/Icon/green/cash.png" width="40">
                                        </h1>
                                    </div>
                                </div>
                                <h4>
                                    <uc1:UC_Shift_Recieve runat="server" ID="UC_Shift_Recieve" />
                                </h4>
                            </asp:Panel>
                            <asp:Panel ID="pnlStockProduct" runat="server">
                                <div class="p-a">
                                    <div class="overflow-hidden">
                                        <h1><b>Stock สินค้า</b>  
                                            <img id="imgProduct" src="../images/Icon/green/barcode.png" width="40">
                                        </h1>
                                    </div>
                                </div>
                                <h4>
                                    <uc1:UC_Shift_StockProduct runat="server" ID="UC_Shift_StockProduct" />
                                </h4>
                            </asp:Panel>
                            <asp:Panel ID="pnlStockSIM" runat="server">
                                <div class="p-a">
                                    <div class="overflow-hidden">
                                        <h1><b>Stock SIM</b>  
                                            <img id="imgSIM" src="../images/Icon/green/simcard.png" width="40">
                                        </h1>
                                    </div>
                                </div>
                                <h4>
                                    <uc1:UC_Shift_StockSIM runat="server" ID="UC_Shift_StockSIM" />
                                </h4>
                            </asp:Panel>


                            <asp:Panel ID="pnlStockPaper" runat="server">
                                <div class="p-a">
                                    <div class="pull-left p-r">
                                    </div>
                                    <div class="overflow-hidden">
                                        <h1><b>Stock กระดาษพิมพ์</b>
                                            <img id="imgcash100" src="../images/Icon/green/printer.png" width="40">
                                        </h1>
                                    </div>
                                </div>

                                <h4>
                                    <uc1:UC_Shift_StockPaper runat="server" ID="UC_Shift_StockPaper" />
                                </h4>
                            </asp:Panel>

                            <asp:Panel ID="pnlbtn" runat="server">
                                <div class="card-block">
                                    <div class="row ">
                                        <div class="col-xs-9">
                                            <p></p>
                                            <asp:LinkButton ID="lnkConfirm" runat="server" class="btn btn-info btn-lg btn-block">
                                            <i class="fa fa-legal"></i>
                                                <span><asp:Label ID="lblConfirm" runat ="server" ></asp:Label></span>
                                                <%--Open Shift / ยืนยัน Open Shift
                                                Close Shift / ยืนยัน Close Shift--%>
                                            </asp:LinkButton>

                                        </div>
                                        <div class="col-xs-3">
                                            <p></p>
                                            <asp:LinkButton ID="lnkOK" runat="server" class="btn btn-success btn-lg btn-block">
                                                <i class="fa fa-save"></i>
                                                    <span>Next / ถัดไป</span>

                                            </asp:LinkButton>

                                        </div>
                                         

                                    </div>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlTemp" runat="server" Visible="false">
                                <div class="scroll full-height">
                                    <div class="full-height">
                                        <div class="contact-header m-b">
                                            <div class="contact-toolbar visible-xs m-b">
                                                <a href="#">
                                                    <span class="icon-close visible-xs m-r m-l"></span>
                                                </a>
                                            </div>
                                            <div class="p-a">
                                                <div class="pull-left p-r">
                                                    <img src="../images/face4.jpg" class="avatar avatar-lg img-circle" alt="">
                                                </div>
                                                <div class="overflow-hidden">
                                                    <h1><b>Kralj</b> Franc</h1>
                                                    <h3>Web developer</h3>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row m-b">
                                            <div class="col-xs-4 text-right">
                                                <span class="text-muted">email</span>
                                            </div>
                                            <div class="col-xs-8">
                                                <a href="email@example.com">email@example.com</a>
                                            </div>
                                        </div>
                                        <div class="row m-b">
                                            <div class="col-xs-4 text-right">
                                                <span class="text-muted">mobile</span>
                                            </div>
                                            <div class="col-xs-8">
                                                <span>+1 123 456 789</span>
                                            </div>
                                        </div>
                                        <div class="row m-b">
                                            <div class="col-xs-4 text-right">
                                                <span class="text-muted">home</span>
                                            </div>
                                            <div class="col-xs-8">
                                                <a href="http://www.example.com">http://www.example.com</a>
                                            </div>
                                        </div>
                                        <div class="row m-b">
                                            <div class="col-xs-4 text-right">
                                                <span class="text-muted">country</span>
                                            </div>
                                            <div class="col-xs-8">
                                                <span>Slovenia</span>
                                            </div>
                                        </div>
                                        <div class="row m-b">
                                            <div class="col-xs-4 text-right">
                                                <span class="text-muted">note</span>
                                            </div>
                                            <div class="col-xs-8">
                                                <span>Cras mattis consectetur purus sit amet fermentum. Integer posuere erat a ante venenatis dapibus posuere velit aliquet. Aenean lacinia bibendum nulla sed consectetur. Nulla vitae elit libero, a pharetra augue. Maecenas faucibus mollis interdum. Duis mollis, est non commodo luctus, nisi erat porttitor ligula, eget lacinia odio sem nec elit. Donec ullamcorper nulla non metus auctor fringilla.</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>

                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
