<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Setting_Authorize.aspx.vb" Inherits="VDM.Setting_Authorize" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-title" style="">
        <div class="title">Setting &gt; Authorize</div>
    </div>

     <asp:UpdatePanel ID="udpList" runat="server">
        <ContentTemplate>
    <asp:Panel ID="pnlList" runat="server" CssClass="card bg-white" Visible="True">
        <div class="card-header">
            Found :
                    <asp:Label ID="lblTotalList" runat="server"></asp:Label>
            User(s)
        </div>
        <div class="card-block">
            <div class="no-more-tables">
                <table class="table table-bordered m-b-0">
                    <thead>
                        <tr>
                            <th>User ID</th>
                            <th>Login Name</th>
                            <th>Password</th>
                            <%--<th>First Name</th>
                            <th>Lasr Name</th>--%>
                            <th>Name</th>
                            <th>Status</th>
                            <th id="ColEdit" runat="server">Edit</th>
                            <th id="ColDelete" runat="server">Delete</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptList" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td data-title="User ID" id="td" runat="server" style="text-align: center;">
                                        <asp:Label ID="lblUserID" runat="server"></asp:Label></td>
                                    <td data-title="Login Name">
                                        <b><asp:Label ID="lblLoginName" runat="server"></asp:Label></b></td>
                                    <td data-title="Password">
                                        <asp:Label ID="lblPassword" runat="server"></asp:Label></td>
                                    <%--<td data-title="First Name">
                                        <asp:Label ID="lblFirstName" runat="server"></asp:Label></td>
                                    <td data-title="Lasr Name">
                                        <asp:Label ID="lblLastName" runat="server"></asp:Label></td>--%>
                                    <td data-title="Name">
                                        <asp:Label ID="lblFullName" runat="server"></asp:Label></td>
                                    <td data-title="Status">

                                        <asp:Panel ID="pnlChe" runat="server" Enabled="false" Style="text-align: center;">
                                            <label class="col-sm-10 cb-checkbox cb-md" aria-disabled="false">
                                                <asp:CheckBox ID="chkAvailable" runat="server" />
                                            </label>
                                        </asp:Panel>
                                        <asp:Button ID="btnToggle" runat="server" CommandName="ToggleStatus" Style="display: none" Text="Toggle" />
                                    </td>
                                    <td data-title="Edit" id="Td1" runat="server">
                                        <asp:Button CssClass="btn btn-success" ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" /></td>
                                    <td data-title="Delete" id="Td2" runat="server">
                                        <input type="button" class="btn btn-danger" value="Delete" id="btnPreDelete" runat="server" />
                                        <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Style="display: none" />
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
                      <span>Add User</span>
                </asp:LinkButton>

                <asp:LinkButton CssClass="btn btn-info btn-icon loading-demo mr5 btn-shadow" ID="btnDownloadForm" runat="server">
                      <i class="icon-arrow-down"></i>
                      <span>Download Form Excel</span>
                </asp:LinkButton>

                <asp:LinkButton CssClass="btn btn-info btn-icon loading-demo mr5 btn-shadow" ID="btnUploadUser" runat="server">
                      <i class="icon-arrow-up"></i>
                      <span>Upload User</span>
                </asp:LinkButton>

            </div>
        </div>
    </asp:Panel>
     </ContentTemplate>
    </asp:UpdatePanel>


        <asp:UpdatePanel ID="udpEdit" runat="server">
        <ContentTemplate>


    <asp:Panel ID="pnlEdit" runat="server" CssClass="card bg-white">
        <div class="card-header">
            <asp:Label ID="lblEditMode" runat="server"></asp:Label>
            User
            <asp:Label ID="lblUser_ID" runat="server" Style="display: none;"></asp:Label>
        </div>
        <div class="card-block">
            <div class="row m-a-0">
                <div class="col-lg-12 form-horizontal">
                    <h4 class="card-title">User Info</h4>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">Login Name <span style="color: red">*</span></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtLoginName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">Password <span style="color: red">*</span></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label" for="phone">First Name <span style="color: red">*</span></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label" for="phone">Last Name <span style="color: red">*</span></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group" style="margin-left: -5px;">
                        <h4 class="card-title col-sm-2 control-label" style="text-align: left;">Active Status </h4>

                        <label class="col-sm-10 cb-checkbox cb-md">
                            <asp:CheckBox ID="chkActive" runat="server" Checked="true" />
                        </label>
                    </div>
                    <div class="form-group" style="text-align: right">
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
