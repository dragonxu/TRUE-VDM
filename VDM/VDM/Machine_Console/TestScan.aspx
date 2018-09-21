<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Machine_Console/MasterStaffConsole.Master" CodeBehind="TestScan.aspx.vb" Inherits="VDM.TestScan" %>

<%@ Register Src="~/UC_Kiosk_Shelf.ascx" TagPrefix="uc1" TagName="UC_Kiosk_Shelf" %>


<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel ID="UDP" runat="server">
    <ContentTemplate>

          <div class="card-block">
                <div class="row ">
                    <h3 class="row m-a-0 text-uppercase bold mobile_group_head">
                        Product Stock <asp:LinkButton ID="btnTest" runat="server">Test</asp:LinkButton>
                    </h3>
                </div>
                <div class="row ">
                    <uc1:UC_Kiosk_Shelf runat="server" ID="Shelf" />
                    <div class="form-group" style="text-align:right">
                            <asp:LinkButton CssClass="btn btn-success btn-icon loading-demo mr5 btn-shadow" ID="btnSaveProduct" runat="server">
                              <i class="fa fa-save"></i>
                              <span>Save</span>
                            </asp:LinkButton>

                            <asp:LinkButton CssClass="btn btn-default btn-icon loading-demo mr5 btn-shadow" ID="btnResetProduct" runat="server">
                              <i class="fa fa-reply"></i>
                              <span>Reset</span>
                            </asp:LinkButton>
                      </div>
                </div>
            </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
<asp:Content ID="ScriptContainer" ContentPlaceHolderID="ScriptContainer" runat="server">
</asp:Content>
