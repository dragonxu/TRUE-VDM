﻿<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Manage_Brand.aspx.vb" Inherits="VDM.Setting_Brand" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
    <!-- page stylesheets -->
  <link rel="stylesheet" href="vendor/chosen_v1.4.0/chosen.min.css">
  <link rel="stylesheet" type="text/css" href="vendor/checkbo/src/0.1.4/css/checkBo.min.css" />
  <!-- end page stylesheets -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-title">
        <div class="title">Management > Brand</div>
    </div>
<asp:UpdatePanel ID="udpList" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlList" runat="server" CssClass="card bg-white" Visible="True">
            <div class="card-header">
                Found : <asp:Label ID="lblTotalList" runat="server"></asp:Label> Brand(s)

            </div>
            <div class="card-block">
                <div class="no-more-tables">

                <table class="table table-bordered m-b-0" style="text-align:center;">
                  <thead>
                    <tr>
                      <th>Logo</th>
                      <th>Code</th>
                      <th>Name</th>
                      <th>Total Product(s)</th>
                      <th>Action</th>
                    </tr>
                  </thead>
                  <tbody>
                  <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <tr>                 
                          <td data-title="Logo"><asp:Image ID="img" runat="server" Width="60px" Height="60px"></asp:Image></td>
                          <td data-title="Code"><asp:Label ID="lblCode" runat="server"></asp:Label></td>
                          <td data-title="Name"><asp:Label ID="lblName" runat="server"></asp:Label></td>
                          <td data-title="Total Product(s)" class="numeric"><asp:Label ID="lblProduct" runat="server"></asp:Label></td>
                          <td data-title="Action" style="width:140px;">
                              <div class="btn-group">
                                  <asp:Button CssClass="btn btn-success btn-sm btn-shadow" ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" />
                                  <input type="button" class="btn btn-danger btn-sm btn-shadow" value="Delete" id="btnPreDelete" runat="server" />
                                  <asp:Button ID="btnDelete" runat="server" CommandName="Delete" style="display:none" />
                              </div>
                              
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
                      <span>Add new brand</span>
                    </asp:LinkButton>
                </div>
              </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:UpdatePanel ID="udpEdit" runat="server">
    <Triggers>
        <asp:PostBackTrigger ControlID="btnUpdateLogo" />
    </Triggers>
    <ContentTemplate>
        <asp:Panel ID="pnlEdit" runat="server"  CssClass="card bg-white">
            <div class="card-header">
                <asp:Label ID="lblEditMode" runat="server"></asp:Label> Brand
            </div>
            <div class="card-block">
                <div class="row m-a-0">
                  <div class="col-lg-12 form-horizontal">
                      <h4 class="card-title">Brand Info</h4>

                      <div class="form-group">
                        <label class="col-sm-2 control-label">Code <span style="color:red">*</span></label>
                        <div class="col-sm-4">
                          <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>                      
                        </div>
                      </div>

                      <div class="form-group">
                        <label class="col-sm-2 control-label">Name <span style="color:red">*</span></label>
                        <div class="col-sm-4">
                          <asp:TextBox ID="txtName" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>                      
                        </div>
                      </div>

                      <div class="form-group">
                            <h4 class="card-title col-sm-2 control-label" style="text-align:left;">Logo</h4>
                 
                            <div class="col-sm-10">
                                
                                <div>
                                    <asp:Image ID="imgIcon" runat="server" style="cursor:pointer; max-width:50%;" ImageUrl="~/images/BlankIcon.png"/>
                                </div>  
                                <div>
                                    <asp:FileUpload ID="ful" runat="server"/> 
                                    <asp:Button ID="btnUpdateLogo" runat="server" style="display:none;" />
                                </div>                              
                                
                                <div class="help-block m-b">Support only image jpeg gif png, file dimension must be 1:1 (Square) and file size must not larger than 4MB</div>
                            </div>
                       </div>

                      <div class="form-group" style="margin-left:-5px;">
                            <h4 class="card-title col-sm-2 control-label" style="text-align:left;">Active Status </h4>  
                        
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