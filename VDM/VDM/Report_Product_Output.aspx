<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Report_Product_Output.aspx.vb" Inherits="VDM.Report_Product_Output" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/PageNavigation.ascx" TagName="PageNavigation" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">   

    <div class="page-title" style="">
        <div class="title">Reports &gt; สินค้าออกจากเครื่อง</div>
    </div>
    <asp:Label ID="lblUser_ID" runat="server" Style="display: none;"></asp:Label>
    <asp:UpdatePanel ID="udpSearch" runat="server">
        <ContentTemplate>
            <div class="card bg-white">
                <div class="card-header">
                    Display Condition
                </div>
                <div class="card-block">
                    <div class="row">
                        <div class="col-lg-5">
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Shop Code</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlShop_Name" runat="server" CssClass="form-control" AutoPostBack="true" Style="width: 100%;">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-5">
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Kiosk Code</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlKiosk_Code" runat="server" CssClass="form-control" AutoPostBack="True" Style="width: 100%;">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="row" style ="margin-bottom:15px;"></div>
                    <div class="row">


                        <div class="col-lg-5">
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Service</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlService" runat="server" CssClass="form-control" AutoPostBack="True" Style="width: 100%;">
                                        <asp:ListItem Value="-1" Text=""></asp:ListItem>
                                        <asp:ListItem Value="0" Text="PRODUCT"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="SIM"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div> 
                        <div class="col-lg-5">
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Move Time</label>
                                <div class="col-sm-8">
                                    <asp:TextBox CssClass="form-control m-b" ID="txtStartDate" runat="server" placeholder="Select Date"></asp:TextBox>

                                    <cc1:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server"
                                        Format="dd/MM/yyyy" TargetControlID="txtStartDate" PopupPosition="BottomLeft"></cc1:CalendarExtender>

                                </div>
                            </div>
                        </div>
                        <div class="col-lg-5">
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Product/SIM</label>
                                <div class="col-sm-8">
                                    <asp:TextBox CssClass="form-control m-b" ID="txtSearchProduct_SIM" runat="server" placeholder="Product/SIM"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-5">
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Serial No</label>
                                <div class="col-sm-8">
                                    <asp:TextBox CssClass="form-control m-b" ID="txtSearchSerial_No" runat="server" placeholder="Serial No"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-5">
                            <div class="form-group">
                                <label class="col-sm-4 control-label">By</label>
                                <div class="col-sm-8">
                                    <asp:TextBox CssClass="form-control m-b" ID="txtSearchBy" runat="server" placeholder="Name"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div> 
                    </div>
                    <div class="row">
                        <asp:LinkButton CssClass="btn btn-primary btn-icon loading-demo mr5 m-t btn-shadow" ID="btnApply" runat="server">
                  <i class="fa fa-check"></i>
                  <span>Apply</span>
                        </asp:LinkButton>
                       <%-- <asp:LinkButton CssClass="btn btn-success btn-icon loading-demo mr5 m-t btn-shadow" ID="lnkExcel" runat="server">
                <i class="fa fa-table"></i>
                <span>Export to Excel</span>
                        </asp:LinkButton>--%>
                    </div>

                </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="udpReport" runat="server">
        <ContentTemplate>
            <div class="card bg-white">
                <div class="card-header">
                    <asp:Label ID="lblHeader" runat="server" CssClass="h4"></asp:Label>

                </div>
                <div class="card-block">
                    <div class="no-more-tables">
                        <table class="table table-bordered m-b-0">
                            <thead>
                                <tr class="top_product_mode"> 
                                    <th>SHOP CODE</th>
                                    <th>KIOSK CODE</th>
                                    <th>MOVE TIME</th> 
                                    <th>PRODUCT</th>
                                    <th>SERIAL NO</th>
                                    <th>MOVEMENT</th>
                                    <th>BY</th>                                     
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptData" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td data-title="SHOP CODE" id="tdMode" runat="server">
                                                <asp:Label ID="lblSHOP_CODE" runat="server"></asp:Label></td>
                                            <td data-title="KIOSK CODE">
                                                <asp:Label ID="lblKIOSK_CODE" runat="server"></asp:Label></td>
                                            <td data-title="MOVE TIME">
                                                <asp:Label ID="lblMOVE_TIME" runat="server"></asp:Label></td> 
                                            <td data-title="PRODUCT">
                                                <asp:Label ID="lblPRODUCT" runat="server"></asp:Label></td>
                                            <td data-title="SERIAL NO">
                                                <asp:Label ID="lblSERIAL_NO" runat="server"></asp:Label></td>
                                            <td data-title="MOVEMENT">
                                                <asp:Label ID="lblMOVEMENT" runat="server"></asp:Label></td>
                                            <td data-title="BY">
                                                <asp:Label ID="lblBY" runat="server"></asp:Label></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>


                            </tbody>
                        </table>
                    </div>
                    <uc1:PageNavigation ID="Pager" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContainer" runat="server">
</asp:Content>
