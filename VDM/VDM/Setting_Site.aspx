<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Setting_Site.aspx.vb" Inherits="VDM.Setting_Site" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
    <!-- page stylesheets -->
  <link rel="stylesheet" href="vendor/chosen_v1.4.0/chosen.min.css">
  <link rel="stylesheet" type="text/css" href="vendor/checkbo/src/0.1.4/css/checkBo.min.css" />
  <!-- end page stylesheets -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-title">
        <div class="title">Setting > Site</div>
    </div>
<asp:UpdatePanel ID="udpList" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlList" runat="server" CssClass="card bg-white" Visible="True">
            <div class="card-header">
                Found : <asp:Label ID="lblTotalList" runat="server"></asp:Label> Site(s)
            </div>
            <div class="card-block">
                <div class="no-more-tables">
                <table class="table table-bordered table-striped m-b-0">
                  <thead>
                    <tr>
                      <th>Icon</th>
                      <th>Code</th>
                      <th>Name</th>
                      <th>Address</th>
                      <th>Total Machine(s)</th>
                      <th id="ColEdit" runat="server">Edit</th>
                      <th id="ColDelete" runat="server">Delete</th>
                    </tr>
                  </thead>
                  <tbody>
                  <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <tr>                 
                          <td data-title="Icon" style="min-width:80px;"><asp:Image ID="img" runat="server" Width="80px" ImageUrl="images/BlankIcon.png" /></td>
                          <td data-title="Site Code"><asp:Label ID="lblCode" runat="server"></asp:Label></td>
                          <td data-title="Site Name"><asp:Label ID="lblName" runat="server"></asp:Label></td>
                          <td data-title="Address"><asp:Label ID="lblAddress" runat="server"></asp:Label></td>
                          <td data-title="Total Machine(s)" class="numeric"><asp:Label ID="lblKiosk" runat="server"></asp:Label></td>                
                          <td data-title="Edit" id="ColEdit" runat="server"><asp:Button CssClass="btn btn-success" ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" /></td>
                          <td data-title="Delete" id="ColDelete" runat="server">
                              <input type="btnPreDelete" class="btn btn-danger" value="Delete" id="btnPreDelete" runat="server" />
                              <asp:Button ID="btnDelete" runat="server" CommandName="Delete" style="display:none" />
                          </td>
                        </tr>
                    </ItemTemplate>
                  </asp:Repeater>            
                  </tbody>
                </table>
                </div>            
                <div class="row m-t">
                    <asp:LinkButton CssClass="btn btn-primary btn-icon loading-demo mr5 btn-shadow" ID="btnAdd" runat="server">
                      <i class="fa fa-plus-circle"></i>
                      <span>Add new site</span>
                    </asp:LinkButton>
                </div>
              </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="udpEdit" runat="server">
    <Triggers>
        <asp:PostBackTrigger ControlID="btnUpdateIcon" />
    </Triggers>
    <ContentTemplate>
        <asp:Panel ID="pnlEdit" runat="server"  CssClass="card bg-white">
            <div class="card-header">
                <asp:Label ID="lblEditMode" runat="server"></asp:Label> Site
            </div>
            <div class="card-block">
                <div class="row m-a-0">
                  <div class="col-lg-12 form-horizontal">
                      <h4 class="card-title">Site Info</h4>
                      <div class="form-group">
                        <label class="col-sm-2 control-label">Code <span style="color:red">*</span></label>
                        <div class="col-sm-4">
                          <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                        </div>
                      </div>
                      <div class="form-group">
                        <label class="col-sm-2 control-label">Name <span style="color:red">*</span></label>
                        <div class="col-sm-4">
                          <asp:TextBox ID="txtName" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>                      
                        </div>
                      </div>
                      
                      <div class="form-group">
                            <h4 class="card-title col-sm-2 control-label" style="text-align:left;">Icon</h4>
                 
                            <div class="col-sm-10">
                                
                                <div>
                                    <asp:Image ID="imgIcon" runat="server" style="cursor:pointer; max-width:50%;" ImageUrl="~/images/BlankIcon.png"/>
                                </div>  
                                <div>
                                    <asp:FileUpload ID="ful" runat="server"/> <asp:Button ID="btnUpdateIcon" runat="server"  Text="Upload" style="display:none;" />
                                </div>                              
                                
                                <div class="help-block m-b">Support only image jpeg gif png, file dimension must be 2:1 (WxH) and file size must not larger than 4MB</div>
                            </div>
                       </div>

                      <div class="card bg-white">
                          <div class="card-header">
                            <h5 class="card-title" style="text-align:left;"> Geolocation</h5>
                          </div>
                          <div class="card-block">
                                <div class="row m-a-0">
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Province</label>
                                        <div class="col-sm-10">
                                            <asp:DropDownList ID="ddlProvince" runat="server" CssClass="chosen form-control" style="width: 100%;" AutoPostBack="True">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                   </div>
                                   <div class="form-group">
                                        <label class="col-sm-2 control-label">Amphur</label>
                                        <div class="col-sm-10">
                                            <asp:DropDownList ID="ddlAmphur" runat="server" CssClass="chosen form-control" style="width: 100%;" AutoPostBack="True">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                   </div>
                                   <div class="form-group">
                                        <label class="col-sm-2 control-label">Tumbol</label>
                                        <div class="col-sm-10">
                                            <asp:DropDownList ID="ddlTumbol" runat="server" CssClass="chosen form-control" style="width: 100%;">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                   </div>
                                    <div class="form-group col-sm-6">
                                        <label class="col-sm-4 control-label"> LAT</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtLAT" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>   
                                        </div>
                                   </div>
                                  <div class="form-group col-sm-6">
                                        <label class="col-sm-4 control-label">LONG</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtLON" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>   
                                        </div>
                                   </div>
                                </div>
                          </div>
                      </div>
                      
                      

                      <div class="form-group" style="margin-left:-5px;">
                            <h5 class="card-title col-sm-2 control-label" style="text-align:left;">Active Status </h5>  
                        
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
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContainer" runat="server">
  
</asp:Content>
