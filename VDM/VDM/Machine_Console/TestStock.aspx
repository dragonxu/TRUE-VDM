<%@ Page Title="Stock" Language="vb" AutoEventWireup="false" MasterPageFile="~/Machine_Console/MaterStaffConsole.Master" CodeBehind="TestStock.aspx.vb" Inherits="VDM.TestStock" %>

<%@ Register Src="~/UC_Product_Shelf.ascx" TagPrefix="uc1" TagName="UC_Product_Shelf" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-lg-1"></div> 
        <div class="col-lg-6">
            
            <uc1:UC_Product_Shelf runat="server" ID="Shelf" />

        </div>
        <div class="col-lg-5 p-l-lg">
            <asp:Panel ID="pnlShelf" runat="server" CssClass="card bg-white">
                <div class="card-header">Shelf Properties</div>
                <div class="card-block">
                    <div class="row m-a-0">
                        <div class="col-lg-12 form-horizontal">              
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Max Width</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtShelfWidth" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Max Height</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtShelfHeight" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>                           
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Slot Depth</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtShelfDepth" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group pull-right">
                                <asp:LinkButton CssClass="btn btn-danger btn-shadow m-r" ID="btnClearShelf" runat="server">Clear All Floor</asp:LinkButton>
                                <asp:LinkButton CssClass="btn btn-primary btn-shadow m-r" ID="btnApplyShelf" runat="server">Apply</asp:LinkButton>
                                <asp:LinkButton CssClass="btn btn-default btn-shadow" ID="btnCancelShelf" runat="server">Cancel</asp:LinkButton>
                             </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlFloor" runat="server" CssClass="card bg-white">
                <div class="card-header">Floor : <b><asp:Label ID="lblFloorName" runat="server"></asp:Label></b> Properties</div>
                <div class="card-block">
                    <div class="row m-a-0">
                        <div class="col-lg-12 form-horizontal">              
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Width</label>
                                <div class="col-sm-8">
                                     <asp:TextBox ID="txtFloorWidth" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Height</label>
                                <div class="col-sm-8">
                                     <asp:TextBox ID="txtFloorHeight" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Depth</label>
                                <div class="col-sm-8">
                                     <asp:TextBox ID="txtFloorDepth" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label">POS-Y</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtFloorY" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group pull-right">
                                <asp:LinkButton CssClass="btn btn-danger btn-shadow m-r" ID="btnRemoveFloor" runat="server">Remove</asp:LinkButton>
                                <asp:LinkButton CssClass="btn btn-primary btn-shadow m-r" ID="btnApplyFloor" runat="server">Apply</asp:LinkButton>
                                <asp:LinkButton CssClass="btn btn-default btn-shadow" ID="btnCancelFloor" runat="server">Cancel</asp:LinkButton>
                             </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlSlot" runat="server" CssClass="card bg-white">
                <div class="card-header">Slot : <b><asp:Label ID="lblSlotName" runat="server"></asp:Label></b> Properties</div>
                <div class="card-block">
                    <div class="row m-a-0">
                        <div class="col-lg-12 form-horizontal">              
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Width</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtSlotWidth" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Height</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtSlotHeight" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Depth</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtSlotDepth" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label">POS-X</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtSlotX" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label">POS-Y</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtSlotY" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Containing</label>
                                <div class="col-sm-8">
                                    -
                                </div>
                            </div>
                            <div class="form-group pull-right">
                                <asp:LinkButton CssClass="btn btn-danger btn-shadow m-r" ID="btnRemoveSlot" runat="server">Remove</asp:LinkButton>
                                <asp:LinkButton CssClass="btn btn-primary btn-shadow m-r" ID="btnApplySlot" runat="server">Apply</asp:LinkButton>
                                <asp:LinkButton CssClass="btn btn-default btn-shadow" ID="btnCancelSlot" runat="server">Cancel</asp:LinkButton>
                             </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            
        </div>
    </div>
   

</asp:Content>

<asp:Content ID="ScriptContainer" runat="server" ContentPlaceHolderID="ScriptContainer"></asp:Content>
