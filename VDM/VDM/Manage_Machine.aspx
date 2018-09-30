<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Manage_Machine.aspx.vb" Inherits="VDM.Manage_Machine" %>

<%@ Register Src="~/UC_MoneyStock_UI.ascx" TagPrefix="uc" TagName="UC_MoneyStock_UI" %>
<%@ Register Src="~/UC_Peripheral_UI.ascx" TagPrefix="uc" TagName="UC_Peripheral_UI" %>
<%@ Register Src="~/UC_Kiosk_Shelf.ascx" TagPrefix="uc" TagName="UC_Kiosk_Shelf" %>
<%@ Register Src="~/Machine_Console/UC_SIM_Dispenser.ascx" TagPrefix="uc" TagName="UC_SIM_Dispenser" %>



<asp:Content ID="HeaderContainer" ContentPlaceHolderID="HeaderContainer" runat="server"></asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-title">
        <div class="title">Management > เครื่อง Vending</div>        
    </div>
    <asp:UpdatePanel ID="udpList" runat="server">
   <ContentTemplate>
        <asp:Panel ID="pnlList" runat="server" CssClass="card " Visible="True">
            <div class="card-header">
                Found : <asp:Label ID="lblTotalList" runat="server"></asp:Label> Machine(s)
            </div>
            <div class="card-block">                
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <div class="col-sm-12">
                             <div class="card card-block no-border bg-white row-equal align-middle">
                                 <div class="col-sm-3">
                                    <div class="col-sm-12">
                                        <div class="column col-sm-4" style="vertical-align:top; padding-left: 0; ">
                                            <asp:Image ID="imgKioskIcon" runat="server" width="90%" ImageUrl="images/icon/koisk_ok.png" />
                                        </div>
                                        <div class="column col-sm-8">                                            
                                            <h3 class="m-a-0 text-green" id="h3" runat="server"><asp:Label ID="lblKioskCode" runat="server"></asp:Label></h3>
                                            <h4 style="display:none;">KO_ID : <asp:Label ID="lblKOID" runat="server"></asp:Label></h4>
                                            <h6><asp:Label ID="lblSite" runat="server"></asp:Label> <br/>
                                                <asp:Label ID="lblZone" runat="server"></asp:Label><br/>
                                                <asp:Label ID="lblTotalProduct" runat="server"></asp:Label> product(s)
                                            </h6>
                                        </div>
                                        <div class="col-sm-12">
                                             <asp:LinkButton CssClass="btn btn-primary btn-icon loading-demo mr5 btn-shadow col-sm-12" ID="btnConsole" runat="server" CommandName="Console">
                                                 <i class="icon-target"></i>
                                                  <span>Staff Console</span>
                                             </asp:LinkButton>
                                             <asp:LinkButton CssClass="btn btn-success btn-icon loading-demo mr5 btn-shadow col-sm-12" ID="btnEdit" runat="server" CommandName="Setting">
                                                 <i class="fa fa-cog"></i>
                                                  <span>Setting</span>
                                             </asp:LinkButton>
                                             <a href="javascript:;" class="btn btn-default btn-icon loading-demo mr5 btn-shadow col-sm-12" ID="btnPreDelete" runat="server">
                                                 <i class="fa fa-close"></i>
                                                  <span>Remove this</span>
                                             </a>
                                             <asp:Button ID="btnDelete" runat="server" CommandName="Delete" style="display:none;"></asp:Button>
                                         </div>  
                                    </div>
                                     
                                    <%--<div class="col-sm-12 mobile_product">
                                        
                                        <uc:uc_SIM_Stock_UI ID="SIM_Stock" runat="server" />                                      
                                        <uc:UC_Printer_Stock_UI ID="Printer" runat="server" />
                                        <asp:Panel ID="pnlBlankPrinter" runat="server" Style="margin-top:20px;" Visible="false"></asp:Panel>                          
                                        
                                    </div> --%>                    
                                  </div>
                                  <div class="col-sm-9">                                       
                                      <uc:UC_Peripheral_UI ID="Peripheral" runat="server" />
                                  </div>
                                  <div class="col-sm-9">
                                      <uc:UC_MoneyStock_UI ID="MoneyStock" runat="server" />
                                  </div>     
                                     
                             </div> 
                        </div>
                        
                   </ItemTemplate>
                </asp:Repeater>

              
                <div class="row">
                    <asp:LinkButton CssClass="btn btn-primary btn-icon loading-demo mr5 btn-shadow" ID="btnAdd" runat="server">
                      <i class="fa fa-plus-circle"></i>
                      <span>Add new machine</span>
                    </asp:LinkButton>
                </div>
                <asp:Button ID="btnUpdateStatus" runat="server" style="display:none;" ClientIDMode="Static" />
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:UpdatePanel ID="udpEdit" runat="server">
    <ContentTemplate>
         <asp:Panel ID="pnlEdit" runat="server"  CssClass="card bg-white">
              <div class="card-header">
                <asp:Label ID="lblEditMode" runat="server"></asp:Label> Kiosk <asp:Label ID="lblCode" runat="server"></asp:Label>
              </div>
              <div class="card-block">
                <div class="row m-a-0">
                  <div class="col-lg-12 form-horizontal">
                  <div class="row m-b">
                      <div class="form-group">
                        <label class="card-title col-sm-2 control-label"><h4 style="margin-top: 0px;"> Kiosk Code </h4></label> 
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtCode" runat="server" CssClass="form-control col-lg-4" Font-Bold="true" style="text-align:center;"/>
                        </div>
                      </div>
                     
                       <div class="form-group">
                         <label class="col-sm-2 control-label">Site <span style="color:red">*</span></label>
                        <div class="col-sm-4">
                                <asp:DropDownList ID="ddlSite" runat="server" data-placeholder="..." CssClass="chosen form-control" Width="100%">
                                </asp:DropDownList>
                        </div>
                        <label class="col-sm-2 control-label">Zone </label>
                        <div class="col-sm-4">                         
                              <asp:TextBox ID="txtZone" runat="server" CssClass="form-control" style="text-align:center; "/>
                        </div>                    
                      </div>
                  </div>


                 <div class="form-group" style="margin-left:-5px; border-top:1px solid #ccc;">
                     <h4 class="card-title col-sm-12 control-label m-t m-b p-t bold" style="text-align:left;">Shelf Physical Setting</h4>
                     <div class="row">
                        <uc:UC_Kiosk_Shelf runat="server" ID="Shelf" />
                     </div>  
                  </div>
                  <div class="form-group" style="margin-left:-5px; border-top:1px solid #ccc;">
                      <h4 class="card-title col-sm-12 control-label m-t m-b p-t bold" style="text-align:left;">SIM Slot Monitoring</h4>
                     <div class="row">
                         <uc:UC_SIM_Dispenser runat="server" ID="Dispenser" />
                     </div> 
                  </div>
                      
                      <div class="form-group m-t" style="text-align:left;">
                            <h4 class="card-title col-sm-2 control-label" style="text-align:left; margin-top:0px;">Active Status </h4>  
                            <label class="col-sm-10 cb-checkbox cb-md">
                                <asp:CheckBox ID="chkActive" runat="server" Checked="true" />
                            </label>
                      </div>                 

                      <div class="form-group" style="text-align:right">
                            <asp:LinkButton CssClass="btn btn-success btn-icon loading-demo mr5 btn-shadow" ID="btnSave" runat="server">
                              <i class="fa fa-save"></i>
                              <span>Save</span>
                            </asp:LinkButton>

                            <asp:LinkButton CssClass="btn btn-warning btn-icon loading-demo mr5 btn-shadow" ID="btnBack" runat="server">
                              <i class="fa fa-rotate-left"></i>
                              <span>Cancel</span>
                            </asp:LinkButton>
                      </div>
                  </div>
                </div>
              </div>
         </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
<asp:Content ID="ScriptContainer" ContentPlaceHolderID="ScriptContainer" runat="server">

</asp:Content>
