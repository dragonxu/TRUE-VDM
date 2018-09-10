<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Machine_Console/MaterStaffConsole.Master" CodeBehind="Machine_Overview.aspx.vb" Inherits="VDM.Machine_Overview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="udpList" runat="server">
        <ContentTemplate>


            <div class="row ">
                <div class="col-xs-8">
                    <p></p>
                    <asp:LinkButton ID="lnkShift" runat="server" class="btn btn-info btn-lg btn-block">
                    <i class="icon-settings"></i>
                        <span>Open/Close Shift check in stock</span>

                    </asp:LinkButton>
                    <%-- <button type="button" class="btn btn-info btn-lg btn-block">
                        <i class="icon-settings"></i>
                        <span>Open/Close Shift check in stock</span>
                    </button>--%>
                </div>
                <div class="col-xs-4">
                    <p></p>
                    <button type="button" class="btn btn-warning btn-lg btn-block">
                        <i class="icon-logout"></i>
                        <span>Logout</span>
                    </button>
                </div>

            </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>
