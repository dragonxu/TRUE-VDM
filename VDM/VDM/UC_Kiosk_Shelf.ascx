<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Kiosk_Shelf.ascx.vb" Inherits="VDM.UC_Kiosk_Shelf" %>

<%@ Register Src="~/Machine_Console/UC_Product_Shelf.ascx" TagPrefix="uc1" TagName="UC_Product_Shelf" %>

<div class="row">
        <div class="col-lg-1"></div> 
        <div class="col-lg-6">
            
            <uc1:UC_Product_Shelf runat="server" ID="Shelf"/>

        </div>
        <div class="col-lg-5 p-l-lg">
            <asp:Panel ID="pnlShelf" runat="server" CssClass="card bg-white panel_property" BorderWidth="2px" KO_ID="0">
                <div class="card-header">Shelf Properties</div>
                <div class="card-block">
                    <div class="row m-a-0">
                        <div class="col-lg-12 form-horizontal">              
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Max Width</label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtShelfWidth" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <label class="col-sm-3 control-label" style="text-align:left;">mm</label>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Max Height</label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtShelfHeight" runat="server" CssClass="form-control"></asp:TextBox>                                    
                                </div>
                                <label class="col-sm-3 control-label" style="text-align:left;">mm</label>
                            </div>                           
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Slot Depth</label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtShelfDepth" runat="server" CssClass="form-control"></asp:TextBox>                                     
                                </div>
                                <label class="col-sm-3 control-label" style="text-align:left;">mm</label>
                            </div>
                            <div class="form-group">
                                <div class="btn-group btn-group-justified">
                                    <asp:LinkButton CssClass="btn btn-warning btn-shadow" ID="btnClearShelf" runat="server">Clear all floor</asp:LinkButton>
                                    <asp:LinkButton CssClass="btn btn-primary btn-shadow" ID="btnApplyShelf" runat="server">Apply</asp:LinkButton>
                                    <asp:LinkButton CssClass="btn btn-default btn-shadow" ID="btnCloseShelf" runat="server">Close</asp:LinkButton>
                                </div> 
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlFloor" runat="server" CssClass="card bg-white panel_property" BorderWidth="2px">
                <div class="card-header">Floor : <b><asp:Label ID="lblFloorName" runat="server"></asp:Label></b> Properties</div>
                <div class="card-block">
                    <div class="row m-a-0">
                        <div class="col-lg-12 form-horizontal">              
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Width</label>
                                <div class="col-sm-5">
                                     <asp:TextBox ID="txtFloorWidth" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>                                     
                                </div>
                                <label class="col-sm-3 control-label" style="text-align:left;">mm</label>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Height</label>
                                <div class="col-sm-5">
                                     <asp:TextBox ID="txtFloorHeight" runat="server" CssClass="form-control"></asp:TextBox>                                     
                                </div>
                                <label class="col-sm-3 control-label" style="text-align:left;">mm</label>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Depth</label>
                                <div class="col-sm-5">
                                     <asp:TextBox ID="txtFloorDepth" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>                                     
                                </div>
                                <label class="col-sm-3 control-label" style="text-align:left;">mm</label>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label">POS-Y</label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtFloorY" runat="server" CssClass="form-control"></asp:TextBox>                                    
                                </div>
                                <label class="col-sm-3 control-label" style="text-align:left;">mm</label>
                            </div>
                            <div class="form-group">
                                <div class="btn-group btn-group-justified">
                                    <asp:LinkButton CssClass="btn btn-danger btn-shadow" ID="btnRemoveFloor" runat="server">Remove this</asp:LinkButton>
                                    <asp:LinkButton CssClass="btn btn-primary btn-shadow" ID="btnApplyFloor" runat="server">Apply</asp:LinkButton>
                                    <asp:LinkButton CssClass="btn btn-default btn-shadow" ID="btnCloseFloor" runat="server">Close</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlSlot" runat="server" CssClass="card bg-white panel_property" BorderWidth="2px">
                <div class="card-header">Slot : <b><asp:Label ID="lblSlotName" runat="server"></asp:Label></b> Properties</div>
                <div class="card-block">
                    <div class="row m-a-0">
                        <div class="col-lg-12 form-horizontal">              
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Width</label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtSlotWidth" runat="server" CssClass="form-control"></asp:TextBox>                                    
                                </div>
                                <label class="col-sm-3 control-label" style="text-align:left;">mm</label>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Height</label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtSlotHeight" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>                                    
                                </div>
                                <label class="col-sm-3 control-label" style="text-align:left;">mm</label>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Depth</label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtSlotDepth" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>                                    
                                </div>
                                <label class="col-sm-3 control-label" style="text-align:left;">mm</label>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label">POS-X</label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtSlotX" runat="server" CssClass="form-control"></asp:TextBox>                                    
                                </div>
                                <label class="col-sm-3 control-label" style="text-align:left;">mm</label>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label">POS-Y</label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtSlotY" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>                                    
                                </div>
                                <label class="col-sm-3 control-label" style="text-align:left;">mm</label>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Containing</label>
                                <div class="col-sm-6">
                                      <asp:Panel CssClass="col-xs-12 text-center m-b" ID="pnlProduct" runat="server">
                                        <asp:Label ID="lblProductName" runat="server" CssClass="h5"></asp:Label>
                                        <%--<asp:Image ID="imgProduct" runat="server" style="width:100%" />--%>
                                          <img id="imgProduct" style="width:100%;" />
                                                                   
                                        <small>CODE : <asp:Label ID="lblSlot_ProductCode" runat="server"></asp:Label></small>
                                        <asp:Label ID="lblSlotQuantity" runat="server" Font-Bold="true" ForeColor="SteelBlue" CssClass="h5"></asp:Label>
                                        <asp:Panel ID="pnlSlotCapacity" runat="server" CssClass="row bg-default left h6" Height="10px">
                                                <asp:Panel CssClass="bg-success" style="height:100%;" ID="levelProduct" runat="server" ></asp:Panel>  
                                                <div style="position:absolute; bottom:-15px;" class="text-grey">
                                                    Available Space : <b class="text-blue"><asp:Label ID="lblFreeSpace" runat="server"></asp:Label></b>
                                                </div>
                                                <div style="position:absolute; right:0px; bottom:-15px;" class="text-grey">
                                                    Max : <b class="text-danger"><asp:Label ID="lblMaxSpace" runat="server"></asp:Label></b>
                                                </div>                                                
                                         </asp:Panel>
                                      </asp:Panel>

                                      <asp:Panel CssClass="col-xs-12" ID="pnlEmpty" runat="server">
                                        <h3 class="bold text-default m-t-0" id="tagSlot_Empty" runat="server">Empty</h3>
                                     </asp:Panel>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="btn-group btn-group-justified">
                                    <asp:LinkButton CssClass="btn btn-danger btn-shadow" ID="btnRemoveSlot" runat="server">Remove this</asp:LinkButton>
                                    <asp:LinkButton CssClass="btn btn-warning btn-shadow" ID="btnMoveToSlot" runat="server">Move robot to</asp:LinkButton>
                                    <asp:LinkButton CssClass="btn btn-primary btn-shadow" ID="btnApplySlot" runat="server">Apply</asp:LinkButton>
                                    <asp:LinkButton CssClass="btn btn-default btn-shadow" ID="btnCloseSlot" runat="server">Close</asp:LinkButton>
                                </div>
                                
                             </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            
        </div>
    </div>