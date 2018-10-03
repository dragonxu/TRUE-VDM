<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Upload_User_Form.aspx.vb" Inherits="VDM.Upload_User_Form" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-title" style="">
        <div class="title">Setting &gt; Authorize  &gt; Upload User</div>
    </div>

  <%--  <asp:UpdatePanel ID="udpUpload" runat="server">
        <ContentTemplate>--%>
            <asp:Panel ID="pnlUpload" runat="server" CssClass="card bg-white" Visible="True">
                <div class="card-header">
                    Upload Excel
                </div>
                <div class="card-block">
                    <div class="form-group">
                        <label class="card-title col-sm-2 control-label" style="text-align: left; ">Flie</label>
                        <div class="col-sm-10">

                            <div>
                                <asp:FileUpload ID="ful_User" runat="server" />
                            </div>
                             
                        </div>
                    </div>

                    <%--<div class="col-sm-10">

                        <div>
                            <img id="img" style="cursor: pointer; max-width: 50%;">
                        </div>
                        <div>

                            <asp:LinkButton ID="lnkUpload" runat="server" class="btn btn-success fileinput-button">
                                                              <i class="icon-plus"></i>
                                                            <span>Add Image...</span>

                            </asp:LinkButton>

                            <asp:FileUpload ID="FileUpload1" runat="server" Style="display: none;" />


                        </div>

                        <div class="help-block m-b">Desc : size</div>
                    </div>
                    --%>
                    <div class="row"></div>
                    <div class="row m-t">
                        <asp:LinkButton CssClass="btn btn-primary btn-icon loading-demo mr5 btn-shadow" ID="btnUpload" runat="server">
                      <i class="icon-arrow-up"></i>
                      <span>Upload</span>
                        </asp:LinkButton>

                         <asp:LinkButton CssClass="btn btn-warning btn-icon loading-demo mr5 btn-shadow" ID="btnBack" runat="server">
                              <i class="fa fa-rotate-left"></i>
                              <span>Cancel</span>
                        </asp:LinkButton>

                    </div>
                    <div class="row"></div>
                </div>

            </asp:Panel>
      <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>


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
                                    <th>Employee ID</th>
                                    <th>Password</th>
                                    <th>Login Name</th>
                                    <th>First Name</th>
                                    <th>Last Name</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptList" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td data-title="User ID" id="td" runat="server" style ="text-align :center ; color:green ;">
                                                <asp:Label ID="lblUserID" runat="server"></asp:Label></td>
                                            <td data-title="Employee ID" style ="text-align :center ;">
                                                <asp:Label ID="lblEmployee_ID" runat="server"></asp:Label></td>
                                            <td data-title="Password">
                                                <asp:Label ID="lblPassword" runat="server"></asp:Label></td>
                                            <td data-title="Login Name">
                                                <asp:Label ID="lblLoginName" runat="server"></asp:Label></td>
                                            <td data-title="First Name" >
                                                <asp:Label ID="lblFirstName" runat="server"></asp:Label></td>
                                             <td data-title="Last Name" >
                                                <asp:Label ID="lblLastName" runat="server"></asp:Label></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>

                    <div class="row m-t">
                        <asp:LinkButton CssClass="btn btn-success btn-icon loading-demo mr5 btn-shadow" ID="btnConfirm" runat="server">
                      <i class="fa fa-plus-circle"></i>
                      <span>Confirm</span>
                        </asp:LinkButton>
                        
                        <asp:LinkButton CssClass="btn btn-default btn-icon loading-demo mr5 btn-shadow" ID="btnReset" runat="server">
                      <i class="icon-refresh"></i>
                      <span>Reset</span>
                        </asp:LinkButton>

                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContainer" runat="server">
</asp:Content>
