<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Manage_Machine.aspx.vb" Inherits="VDM.Manage_Machine" %>

<%@ Register Src="~/UC_MoneyStock_UI.ascx" TagPrefix="uc" TagName="UC_MoneyStock_UI" %>
<%@ Register Src="~/UC_Peripheral_UI.ascx" TagPrefix="uc" TagName="UC_Peripheral_UI" %>

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
                      
                      <%--<div class="form-group">
                        <label class="col-sm-2 control-label  m-b">#Kiosk on Location <span style="color:red">*</span></label>
                        <div class="col-sm-4 m-b">
                            <asp:TextBox ID="txtRunningNo" runat="server" CssClass="form-control" style="text-align:center; " AutoPostBack="true" />
                        </div>
                        <label class="col-sm-2 control-label m-b">AIS Terminal ID <span style="color:red">*</span></label>
                        <div class="col-sm-4 m-b">
                            <asp:TextBox ID="txtTerminal" runat="server" CssClass="form-control" style="text-align:center; "/>
                        </div>
                      </div>
                      <h4 class="card-title m-t">Network Information</h4>
                      <div class="form-group">
                        <label class="col-sm-2 control-label">Computer Name <span style="color:red">*</span></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtComName" runat="server" CssClass="form-control" style="text-align:center; "/>
                        </div>
                        <label class="col-sm-2 control-label">&nbsp;&nbsp;&nbsp;</label>
                        <div class="col-sm-4">
                            <input class="form-control" style="visibility:hidden;" />
                        </div>
                      </div>
                      <div class="form-group">
                        <label class="col-sm-2 control-label">IP Address <span style="color:red">*</span></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtIP1" runat="server" Width="50px" MaxLength="3" CssClass="form-control" style="text-align:center; display:inline; padding:0; " /> .
                            <asp:TextBox ID="txtIP2" runat="server" Width="50px" MaxLength="3" CssClass="form-control" style="text-align:center; display:inline; padding:0; " /> .
                            <asp:TextBox ID="txtIP3" runat="server" Width="50px" MaxLength="3" CssClass="form-control" style="text-align:center; display:inline; padding:0; " /> .
                            <asp:TextBox ID="txtIP4" runat="server" Width="50px" MaxLength="3" CssClass="form-control" style="text-align:center; display:inline; padding:0; " />
                         </div>                        
                        <label class="col-sm-2 control-label m-t">Mac Address <span style="color:red">*</span></label>
                        <div class="col-sm-4 m-t">
                          <asp:TextBox ID="txtMacAddress" runat="server" CssClass="form-control" Width="65%" style="display:inline;"></asp:TextBox>
                            <asp:Button ID="btnGetMac" runat="server" CssClass="btn btn-facebook" Width="30%" Text="Get By IP" Visible="false" /> 
                        </div>
                      </div>--%>                     

                  </div>
             
                 <%--<div class="row m-b">
                        <h4 class="card-title">Bundled Product(s) Selling on this machine</h4>
                        <div class="form-group" style="margin-left:-5px;">                        
                            
                            <asp:Repeater ID="rptBundledProduct" runat="server">
                                <ItemTemplate>
                                    <label class="col-sm-2 cb-checkbox cb-md">                                
                                        <asp:CheckBox ID="chk" runat="server" /> <asp:Label ID="lbl" runat="server"></asp:Label>
                                        <asp:Image ID="img" runat="server" Width="100%" />
                                    </label>
                                </ItemTemplate>
                            </asp:Repeater>                            
                         </div>   
                 </div>
                     <div class="row m-b">
                      <h4 class="card-title">SIM Slot Assignment</h4>  
                      
                     <asp:Repeater ID="rpt_SIM_Slot" runat="server">
                         <ItemTemplate>
                            <div class="card col-sm-2" style="padding:0; padding-right:0;">
                                <div class="card-header" style="text-align:center; padding:3px;">
                                    <img src="Render_Hardware_Icon.aspx?D=10&C=G" height="30" />
                                    <asp:Label ID="lblSIM" runat="server"></asp:Label>
                                </div>
                                <div class="card-block no-border bg-default row-equal align-middle">                                  
                                  <div class="form-group m-b">
                                    <div class="col-sm-12">
                                         <asp:DropDownList ID="ddlSIM" runat="server" Width="100%" CssClass="chosen form-control"></asp:DropDownList>
                                    </div>
                                  </div>
                                  <div class="form-group m-b">
                                    <label class="col-sm-6 control-label text-primary">Capaity <span style="color:red">*</span></label>
                                    <div class="col-sm-6">        
                                         <asp:TextBox ID="txtMax" runat="server" CssClass="form-control" Style="text-align:right; padding:0;" ></asp:TextBox>
                                    </div>
                                  </div>
                                  <div class="form-group m-b">
                                    <label class="col-sm-6 control-label text-warning">Warning at <span style="color:red">*</span></label>
                                    <div class="col-sm-6">               
                                         <asp:TextBox ID="txtWarning" runat="server" CssClass="form-control" Style="text-align:right;  padding:0;" ></asp:TextBox>
                                    </div>
                                  </div>
                                  <div class="form-group ">
                                    <label class="col-sm-6 control-label text-danger">Alert at <span style="color:red">*</span></label>
                                    <div class="col-sm-6">            
                                         <asp:TextBox ID="txtCritical" runat="server" CssClass="form-control" Style="text-align:right;  padding:0;" ></asp:TextBox>
                                    </div>
                                  </div>                      
                                </div>
                              </div>
                         </ItemTemplate>
                     </asp:Repeater> 

                  </div>                 
                   <div class="row m-b">
                      <h4 class="card-title">Material Stock Control Level</h4>

                        <asp:Repeater ID="rpt_Stock" runat="server">
                            <ItemTemplate>
                                  <div class="card col-sm-3" style="padding:0; padding-right:0;">
                                    <div class="card-header" style="text-align:center;">
                                        <asp:Image ImageUrl="Images/BlackDot.png" Height="30px" ID="imgIcon" runat="server" /> 
                                        <asp:Label ID="lblDeviceName" runat="server"></asp:Label>
                                    </div>
                                    <div class="card-block no-border bg-white row-equal align-middle">
                                      <div class="form-group m-b">
                                        <label class="col-sm-6 control-label text-primary">Stock Capaity <span style="color:red">*</span></label>
                                        <div class="col-sm-6">        
                                             <asp:TextBox ID="txtMax" runat="server" CssClass="form-control" ></asp:TextBox>
                                        </div>
                                      </div>
                                      <asp:Panel ID="pnlMoveDown" runat="server" Visible="false">
                                          <div class="form-group m-b">
                                            <label class="col-sm-6 control-label text-warning">Warning at <span style="color:red">*</span></label>
                                            <div class="col-sm-6">         
                                                 <asp:TextBox ID="txtWarningDown" runat="server" CssClass="form-control" ></asp:TextBox>
                                            </div>
                                          </div>
                                          <div class="form-group ">
                                            <label class="col-sm-6 control-label text-danger">Alert at <span style="color:red">*</span></label>
                                            <div class="col-sm-6">             
                                                 <asp:TextBox ID="txtCriticalDown" runat="server" CssClass="form-control" ></asp:TextBox>
                                            </div>
                                          </div>
                                      </asp:Panel>
                                      <asp:Panel ID="pnlMoveUp" runat="server" Visible="false">
                                          <div class="form-group ">
                                            <label class="col-sm-6 control-label text-danger">Alert at <span style="color:red">*</span></label>
                                            <div class="col-sm-6">             
                                                 <asp:TextBox ID="txtCriticalUp" runat="server" CssClass="form-control" ></asp:TextBox>
                                            </div>
                                          </div>
                                          <div class="form-group m-b">
                                            <label class="col-sm-6 control-label text-warning">Warning at <span style="color:red">*</span></label>
                                            <div class="col-sm-6">         
                                                 <asp:TextBox ID="txtWarningUp" runat="server" CssClass="form-control" ></asp:TextBox>
                                            </div>
                                          </div>                                          
                                      </asp:Panel>              
                                    </div>
                                  </div>
                            </ItemTemplate>
                        </asp:Repeater>
                                 
                   </div>
                  
                      <div class="form-group" style="margin-left:-5px; border-bottom:1px solid #ccc;">
                           <h4 class="card-title col-sm-2 control-label" style="text-align:left;">Ads Rotation</h4>
                            
                                    <table class="table m-b-0">
                                        <thead>
                                          <tr>
                                            <th class="col-md-10">
                                              <span></span>Ads</th>
                                            <th class="col-md-1">Setting</th>
                                            
                                          </tr>
                                        </thead>
                                        <tbody>
                            <asp:UpdatePanel ID="udpAds" runat="server">
                                <ContentTemplate>
                                    <asp:Repeater ID="rpt_Ads" runat="server">
                                        <ItemTemplate>
                                                  <tr>
                                                    <td class="ads_container">
                                                        <asp:Image ID="imgAds" runat="server" CssClass="ads" onclick="window.open(this.src);" />
                                                    </td>
                                                    <td>
                                                        <div class="form-group ">
                                                            <label class="col-sm-6 control-label text-danger">Duration(sec)</label>
                                                            <div class="col-sm-6">             
                                                                 <asp:TextBox  runat="server" CssClass="numeric" ID="txtDuration" Style="max-width:90%;"></asp:TextBox>                                                        
                                                            </div>
                                                            <asp:Button ID="btnUpdateSec" runat="server" style="display:none;" CommandName="Duration" />
                                                        </div> 
                                                        <div class="btn-group btn-group-justified">
                                                            <a href="#" ID="btnBrowseAds"  class="btn btn-facebook" runat="server" ><i class="icon-screen-desktop"></i></a>
                                                            <asp:FileUpload ID="ful" runat="server" style="display:none;" />
                                                        </div>
                                                        <div class="btn-group btn-group-justified">
                                                            <a ID="btnDown" runat="server" class="btn btn-success" href="javascript:;"><i class="fa fa-chevron-down"></i></a>
                                                            <a ID="btnUp" runat="server" class="btn btn-success" href="javascript:;"><i class="fa fa-chevron-up"></i></a>                                                  
                                                        </div>
                                                        <div class="btn-group btn-group-justified">
                                                            <a ID="btnDelete" runat="server" class="btn btn-danger" href="javascript:;"><i class="fa fa-times"></i></a>
                                                        </div>
                                                    </td>                                                             
                                                  </tr>    
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                                                  
                                      </tbody>
                                    </table>
                            <asp:TextBox ID="txtAds_RowIndex" ClientIDMode="Static" runat="server" style="display:none;"></asp:TextBox>
                            <asp:TextBox ID="txtAds_CommandName" ClientIDMode="Static" runat="server" style="display:none;"></asp:TextBox>
                            <asp:Button ID="btnAds_Command" ClientIDMode="Static" runat="server" style="display:none;"></asp:Button>

                            <a href="#" onclick="UploadInsertAds();return false;" class="btn btn-primary btn-icon loading-demo mr5 m-b btn-shadow col-sm-2" ID="btnInsertAds" runat="server" >
                                <i class="icon-plus"></i>
                                <span>Insert Ads</span>
                            </a>
                            <asp:FileUpload ID="fulInsertAds" ClientIDMode="Static" runat="server" style="display:none;" onchange="asyncAdsInsert();" />
                            <asp:Button ID="btnUpdateAds" ClientIDMode="Static" runat="server" style="display:none;" />
                            <div class="help-block col-sm-9 m-b">Support only image jpeg gif png, file dimension must be 1080x320 and file size must not larger than 12MB</div>
                        
                    </div>
                          <div class="row m-b" >
                            <asp:FileUpload ID="fulUploadVDO" ClientIDMode="Static" runat="server" style="display:none;"  />
                            <h4 class="card-title col-sm-2 control-label" style="text-align:left;">VDO Screen Saver</h4>
                            <a href="#" onclick="UploadVDO();return false;" class="btn btn-primary btn-icon loading-demo mr5 m-b btn-shadow col-sm-2"  >
                                <i class="icon-plus"></i>
                                <span>Choose VDO</span>
                            </a>
                            <span ID="lblVDOFileName" class="col-sm-8 m-b" ></span>
                      </div>
                          <div class="row m-b" >
                          <h3 class="card-title col-sm-2 control-label" style="text-align:left;">&nbsp;</h3>
                          <div class="help-block col-sm-8 m-b">Support only *.avi, file size must not larger than 100MB</div>
                      </div>--%>                 
                      
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
