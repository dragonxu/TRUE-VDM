<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Machine_Console/MasterStaffConsole.Master" CodeBehind="Manage_OpenClose_Shift.aspx.vb" Inherits="VDM.Manage_OpenClose_Shift" %>

<%@ Register Src="~/Machine_Console/UC_Shift_Change.ascx" TagPrefix="uc1" TagName="UC_Shift_Change" %>
<%@ Register Src="~/Machine_Console/UC_Shift_StockPaper.ascx" TagPrefix="uc1" TagName="UC_Shift_StockPaper" %>
<%@ Register Src="~/Machine_Console/UC_Shift_Recieve.ascx" TagPrefix="uc1" TagName="UC_Shift_Recieve" %>
<%@ Register Src="~/Machine_Console/UC_Product_Stock.ascx" TagPrefix="uc1" TagName="UC_Product_Stock" %>
<%@ Register Src="~/UC_Kiosk_Shelf.ascx" TagPrefix="uc1" TagName="UC_Kiosk_Shelf" %>
<%@ Register Src="~/Machine_Console/UC_Product_Shelf.ascx" TagPrefix="uc1" TagName="UC_Product_Shelf" %>
<%@ Register Src="~/Machine_Console/UC_SIM_Dispenser.ascx" TagPrefix="uc1" TagName="UC_SIM_Dispenser" %>
<%@ Register Src="~/Machine_Console/UC_SIM_Stock.ascx" TagPrefix="uc1" TagName="UC_SIM_Stock" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style  type="text/css">
        .nav-pills > li.active > a, 
        .nav-pills > li.active > a:hover,
        .nav-pills > li.active > a:focus,
        .nav > li > a:hover, 
        .nav > li > a:focus {
            background-color:beige !important;
            color: #454545 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="udpList" runat="server">
        <ContentTemplate>
            <div class="card-block">
                <div class="row">

                    <div class="display-columns">
                        <div class="column contacts-sidebar hidden-xs bg-white b-r">
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
                                                        <span>กระดาษพิมพ์</span>
                                                        <div id="divMenuStockPaper" runat ="server" class="message-sender" style="text-align: right;">
                                                            <p><span class=" h5 text-info">พิมพ์ได้</span><span class=" h5"> <asp:Label ID="lbl_Paper_Count" runat ="server" ></asp:Label> ครั้ง</span></p>
                                                        </div>
                                                    </h3>
                                                </a>
                                            </li>
                                            <li>
                                                <div class="col-xs-12 text-left">
                                                    <div class="fa-hover text-default   ">
                                                        <a id="lnkBack" runat="server" title="Back / กลับไปหน้า Overview">
                                                            <h3><i class="fa fa-angle-double-left"></i> Overview </h3>
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
                                        
                                        <h1 class="col-sm-8">  
                                            <img id="imgProduct" src="../images/Icon/red/shelf.png" width="40">
                                            <b>Stock สินค้า</b>  
                                        </h1>
                                        <h2 class="col-sm-4 ">
                                            <asp:LinkButton ID="btnManageProductStock" runat="server" CssClass="btn btn-danger btn-lg btn-block btn-shadow">
                                                <i class="icon-settings"></i>
                                                <span>จัดการ Stock</span>
                                            </asp:LinkButton>
                                        </h2>
                                        <div class="col-sm-8">                                            
                                            <uc1:UC_Product_Shelf runat="server" ID="Kiosk_Shelf" />
                                        </div>
                                        <div class="col-sm-4 ">
                                            <div class="row bg-default-light height100pc m-t p-t m-b">
                                              <div class="col-md-12">
                                                <div class="card card-block b-a-0 bg-teal text-white">
                                                  <div class="card-circle-bg-icon"> <i class="icon-handbag"></i> </div>
                                                  <div class="h4 m-a-0"><asp:Label ID="lbl_Product_Total" runat="server"></asp:Label></div>
                                                  <div>ทั้งหมด</div>
                                                </div>
                                              </div>
                                              <div class="col-md-12">
                                                <div class="card card-block b-a-0 bg-blue text-white">
                                                  <div class="card-circle-bg-icon"> <i class="icon-frame"></i> </div>
                                                  <div class="h4 m-a-0"><asp:Label ID="lbl_Product_Empty" runat="server"></asp:Label></div>
                                                  <div>Slot ว่าง</div>
                                                </div>
                                              </div>
                                              <div class="col-md-12">
                                                <div class="card card-block b-a-0 bg-success text-white">
                                                  <div class="card-circle-bg-icon"> <i class="icon-login"></i> </div>
                                                  <div class="h4 m-a-0"><asp:Label ID="lbl_Product_In" runat="server"></asp:Label></div>
                                                  <div>Scan สินค้าเข้า</div>
                                                </div>
                                              </div>
                                             <div class="col-md-12">
                                                <div class="card card-block b-a-0 bg-indigo text-white">
                                                  <div class="card-circle-bg-icon"> <i class="icon-logout"></i> </div>
                                                  <div class="h4 m-a-0"><asp:Label ID="lbl_Product_Out" runat="server"></asp:Label></div>
                                                  <div>Check Out</div>
                                                </div>
                                              </div>
                                              <div class="col-md-12">
                                                <div class="card card-block b-a-0 bg-purple text-white">
                                                  <div class="card-circle-bg-icon"> <i class="icon-refresh"></i> </div>
                                                  <div class="h4 m-a-0"><asp:Label ID="lbl_Product_Move" runat="server"></asp:Label></div>
                                                  <div>ย้าย Slot</div>
                                                </div>
                                              </div>

                                            </div>
                                            

                                        </div>
                                        

                                    </div>
                                </div>
                                
                            </asp:Panel>
                            <asp:Panel ID="pnlStockSIM" runat="server">
                                <div class="p-a">
                                    <div class="overflow-hidden">
                                        <h1 class="col-sm-8 "> 
                                            <img id="imgSIM" src="../images/Icon/green/simcard.png" width="40">
                                            <b>SIM Stock</b> 
                                        </h1>
                                        <h2 class="col-sm-4 ">
                                            <asp:LinkButton ID="btnManageSIMStock" runat="server" CssClass="btn btn-danger btn-lg btn-block btn-shadow">
                                                <i class="icon-settings"></i>
                                                <span>จัดการ Stock</span>
                                            </asp:LinkButton>
                                        </h2>
                                        <div class="col-sm-8">                                            
                                            <uc1:UC_SIM_Dispenser runat="server" ID="SIMDispenser" />
                                        </div>
                                        <div class="col-sm-4 ">
                                            <div class="row bg-default-light height100pc m-t p-t m-b">
                                              <div class="col-md-12">
                                                <div class="card card-block b-a-0 bg-teal text-white">
                                                  <div class="card-circle-bg-icon"> <i class="icon-handbag"></i> </div>
                                                  <div class="h4 m-a-0"><asp:Label ID="lbl_SIM_Total" runat="server"></asp:Label></div>
                                                  <div>ทั้งหมด</div>
                                                </div>
                                              </div>
                                              <div class="col-md-12">
                                                <div class="card card-block b-a-0 bg-blue text-white">
                                                  <div class="card-circle-bg-icon"> <i class="icon-frame"></i> </div>
                                                  <div class="h4 m-a-0"><asp:Label ID="lbl_SIM_Empty" runat="server"></asp:Label></div>
                                                  <div>Slot ว่าง</div>
                                                </div>
                                              </div>
                                              <div class="col-md-12">
                                                <div class="card card-block b-a-0 bg-success text-white">
                                                  <div class="card-circle-bg-icon"> <i class="icon-login"></i> </div>
                                                  <div class="h4 m-a-0"><asp:Label ID="lbl_SIM_In" runat="server"></asp:Label></div>
                                                  <div>Scan สินค้าเข้า</div>
                                                </div>
                                              </div>
                                             <div class="col-md-12">
                                                <div class="card card-block b-a-0 bg-indigo text-white">
                                                  <div class="card-circle-bg-icon"> <i class="icon-logout"></i> </div>
                                                  <div class="h4 m-a-0"><asp:Label ID="lbl_SIM_Out" runat="server"></asp:Label></div>
                                                  <div>Check Out</div>
                                                </div>
                                              </div>
                                              <div class="col-md-12">
                                                <div class="card card-block b-a-0 bg-purple text-white">
                                                  <div class="card-circle-bg-icon"> <i class="icon-refresh"></i> </div>
                                                  <div class="h4 m-a-0"><asp:Label ID="lbl_SIM_Move" runat="server"></asp:Label></div>
                                                  <div>ย้าย Slot</div>
                                                </div>
                                              </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <h4>
                                    
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

                            
                        </div>
                    </div>

                </div>
            </div>

<asp:Panel CssClass="modal-dialog" style="width:90%; margin: 0px auto; position:absolute; top:-20px;" ID="pnlScanProduct" runat="server"  Visible="false">
    <div class="modal-content" style="z-index:2;">
        <div class="modal-header">
          <asp:LinkButton CssClass="close" ID="lnkCloseScanProduct" runat="server" >×</asp:LinkButton>
          <h4 class="modal-title">Manage Product Stock</h4>
        </div>
        <div class="modal-body">
          <p>จัดการสินค้าที่อยู่ใน Shelf ด้วยการ 
              <img src="../images/Icon/green/signal_wifi_3.png" height="30" /> Scan Barcode หรือ 
              <img src="../images/Icon/green/repeat.png" height="30" />              
              เคลื่อนย้ายสินค้าที่อยู่ในตู้</p>
            <uc1:UC_Product_Stock runat="server" id="Product_Stock" />
        </div>
        <div class="modal-footer no-border">
            <asp:Button ID="btnResetScanProduct" runat="server" CssClass="btn btn-shadow btn-default" Text="Reset" />
            <asp:Button ID="btnCloseScanProduct" runat="server" CssClass="btn btn-shadow btn-primary" Text="Close" />
        </div>

    </div>
    <div div class="modal bs-modal-sm in" style="display: block; z-index:1;">

    </div>
</asp:Panel>

<asp:Panel CssClass="modal-dialog" style="width:90%; margin: 0px auto; position:absolute; top:-20px;" ID="pnlScanSIM" runat="server"  Visible="false">
    <div class="modal-content" style="z-index:2;">
        <div class="modal-header">
          <asp:LinkButton CssClass="close" ID="lnkCloseScanSIM" runat="server" >×</asp:LinkButton>
          <h4 class="modal-title">Manage SIM Stock</h4>
        </div>
        <div class="modal-body">
          <p>จัดการสินค้าที่อยู่ใน SIM Slot ด้วยการ 
              <img src="../images/Icon/green/signal_wifi_3.png" height="30" /> Scan Barcode หรือ 
              <img src="../images/Icon/green/repeat.png" height="30" />              
              เคลื่อนย้าย SIM ใน Slot</p>
                <uc1:UC_SIM_Stock runat="server" ID="SIMStock" />
        </div>
        <div class="modal-footer no-border">
            <asp:Button ID="btnResetScanSIM" runat="server" CssClass="btn btn-shadow btn-default" Text="Reset" />
            <asp:Button ID="btnCloseScanSIM" runat="server" CssClass="btn btn-shadow btn-primary" Text="Close" />
        </div>

    </div>
    <div div class="modal bs-modal-sm in" style="display: block; z-index:1;">

    </div>
</asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
