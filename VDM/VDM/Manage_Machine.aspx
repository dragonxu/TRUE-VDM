<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Manage_Machine.aspx.vb" Inherits="VDM.Manage_Machine" %>

<asp:Content ID="HeaderContainer" ContentPlaceHolderID="HeaderContainer" runat="server">
</asp:Content>
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
                
                <%--<asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>--%>
                        
                        <div class="col-sm-12">
                             <div class="card card-block no-border bg-white row-equal align-middle">
                                 <div class="col-sm-3">
                                    <div class="col-sm-12">
                                        <div class="column">
                                            <asp:Image ID="imgKioskIcon" runat="server" ImageUrl="images/icon/100/koisk_ok.png" Height="60px" />
                                        </div>
                                        <div class="column">                                            
                                            <h3 class="m-a-0 text-green" id="h3" runat="server"><asp:Label ID="lblKioskCode" runat="server"></asp:Label></h3>
                                            <h4>Ko_ID : <asp:Label ID="lblKOID" runat="server"></asp:Label></h4>
                                            <h4>IP : <asp:Label ID="lblIP" runat="server"></asp:Label></h4>
                                            <h6><asp:Label ID="lblLocation" runat="server"></asp:Label> 
                                                <asp:Label ID="lblNetworkScope" runat="server"></asp:Label> 
                                                <asp:Label ID="lblRunningNo" runat="server"></asp:Label> <br />
                                                <asp:Label ID="lblTerminalID" runat="server"></asp:Label><br />
                                                selling 
                                                <asp:Label ID="lblBundledProduct" runat="server"></asp:Label> product(s)
                                            </h6>
                                        </div>
                                    </div>

                                    <div class="col-sm-12 mobile_product">
                                        
                                        <%--<uc:uc_SIM_Stock_UI ID="SIM_Stock" runat="server" /> --%>                                       
                                        <%--<uc:UC_Printer_Stock_UI ID="Printer" runat="server" />--%>
                                        <asp:Panel ID="pnlBlankPrinter" runat="server" Style="margin-top:20px;" Visible="false"></asp:Panel>                          
                                        
                                    </div>
                                     

                                  </div>
                                  <%--<div class="col-sm-9">                                       
                                      <uc:UC_Peripheral_UI ID="Peripheral" runat="server" />
                                  </div>--%>
                                  <%--<div class="col-sm-9">
                                      <uc:UC_MoneyStock_UI ID="MoneyStock" runat="server" />
                                  </div> --%>    
                                 <div class="col-sm-9">
                                     <asp:LinkButton CssClass="btn btn-primary btn-icon loading-demo mr5 btn-shadow col-sm-4" ID="btnMonitor" runat="server" CommandName="Edit">
                                         <i class="icon-target"></i>
                                          <span>Realtime Monitoring</span>
                                     </asp:LinkButton>
                                     <asp:LinkButton CssClass="btn btn-success btn-icon loading-demo mr5 btn-shadow col-sm-4" ID="btnEdit" runat="server" CommandName="Edit">
                                         <i class="fa fa-cog"></i>
                                          <span>Change configuration</span>
                                     </asp:LinkButton>
                                     <asp:LinkButton CssClass="btn btn-danger btn-icon loading-demo mr5 btn-shadow col-sm-4" ID="btnDelete" runat="server" CommandName="Delete">
                                          <i class="fa fa-close"></i>
                                          <span>Remove this machine</span>
                                     </asp:LinkButton>
                                 </div>       
                             </div> 
                        </div>
                        
        <%--           </ItemTemplate>
                </asp:Repeater>--%>

              
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


</asp:Content>
<asp:Content ID="ScriptContainer" ContentPlaceHolderID="ScriptContainer" runat="server">

</asp:Content>
