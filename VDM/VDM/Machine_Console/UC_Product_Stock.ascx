<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Product_Stock.ascx.vb" Inherits="VDM.UC_Product_Stock" %>
<%@ Register Src="~/Machine_Console/UC_Product_Shelf.ascx" TagPrefix="uc1" TagName="UC_Product_Shelf" %>

    <style>
        td {
        text-align:center;
        }
        th {
            background-color:#f5f5f5;
        }

        .nav-tabs > li {
          width:auto;
          max-width:48%;
        }
       
        .nav-tabs > li.active::before {
             background-color:#4aa85d !important;
        }

        .nav-tabs > li.active {
            background-color:aliceblue;
            cursor:pointer;
        }

        .nav > li > a:hover {
            background-color:transparent !important;
        }

    </style>

<div class="row" >
    <div class="col-sm-6" ondragover="draggedOver(event)" >
        <asp:Panel ID="pnlZoom" runat="server" CssClass="row m-b-md btn-group" KO_ID="0" SHOP_CODE="" MY_UNIQUE_ID="" SHIFT_ID="0" SHIFT_STATUS="99">                     
            <asp:LinkButton ID="btnZoomOut" runat="server" CssClass="btn btn-warning">-</asp:LinkButton>
            <asp:LinkButton ID="btnZoomReset" runat="server" CssClass="btn btn-info">x</asp:LinkButton>
            <asp:LinkButton ID="btnZoomIn" runat="server" CssClass="btn btn-success">+</asp:LinkButton>                                  
        </asp:Panel>
        <uc1:uc_product_shelf runat="server" ID="Shelf" />
        
        <asp:Panel ID="pnlSlotProduct" runat="server" CssClass="card bg-white m-b m-t-md" PRODUCT_ID="0">
            <div class="card-header">
            <h3 class="m-t-0 m-b-0 pull-left">
                <img src="../images/Icon/green/shelf.png" height="30"> Slot : <asp:Label ID="lblSlotName" runat="server" SLOT_ID="0"></asp:Label>
            </h3>
            <asp:LinkButton ID="btnSeeShelf" runat="server" CssClass="pull-right btn btn-primary btn-sm">
                <i class="fa fa-reply"></i>
                <span>see all</span>
            </asp:LinkButton>

            </div>
            <div class="card-block">
            <div class="row">
                <div class="m-t-n m-b">
                    <div class="row ">
                        <div class="col col-xs-4 m-t text-center">
                            <asp:Image ID="imgSlot_Product" runat="server" CssClass="product-Image" ImageUrl="../RenderImage.aspx?Mode=D&UID=1&Entity=Product&LANG=1" />
                                        
                            <small>CODE : <asp:Label ID="lblSlot_ProductCode" runat="server"></asp:Label></small><br/>
                            <asp:Label ID="lblSlotQuantity" runat="server" Font-Bold="true" ForeColor="SteelBlue" CssClass="h2"></asp:Label>                                    
                        </div>
                        <div class="col col-xs-8 p-t p-l">
                            <div class="row">
                                <div class="col-xs-9">
                                    <h4 class="m-t-0 m-b-0"><asp:Label ID="lblSlot_ProductName" runat="server"></asp:Label></h4>
                                    <h6><asp:Label ID="lblSlot_ProductDesc" runat="server"></asp:Label></h6>
                                </div>
                                <div class="col-xs-3">
                                    <h3 class="bold text-default m-t-0" id="tagSlot_Empty" runat="server">Empty</h3>
                                    <asp:Image ID="imgSlot_Brand" runat="server" CssClass="profile-avatar" ImageUrl="../RenderImage.aspx?Mode=D&UID=1&Entity=Brand&LANG=1" />
                                </div>
                            </div>
                                            
                                <asp:Panel ID="pnlSlotProductSize" runat="server" CssClass="row text-center text-white  m-t">
                                    <div class="col-xs-4 bg-info p-t">
                                        <h4 class="m-t-0 m-b-0"><asp:Label ID="lblSlot_Product_Width" runat="server"></asp:Label></h4>
                                        <small>Package Width</small>
                                    </div>
                                    <div class="col-xs-4 bg-info p-t">
                                        <h4 class="m-t-0 m-b-0"><asp:Label ID="lblSlot_Product_Height" runat="server"></asp:Label></h4>
                                        <small>Package Height</small>
                                    </div>
                                    <div class="col-xs-4 bg-info p-t">
                                        <h4 class="m-t-0 m-b-0"><asp:Label ID="lblSlot_Product_Depth" runat="server"></asp:Label></h4>
                                        <small>Package Depth</small>
                                    </div>
                                </asp:Panel>
                                <div class="row text-center text-white">
                                    <div class="col-xs-4 bg-blue p-t">
                                        <h4 class="m-t-0 m-b-0"><asp:Label ID="lblSlot_Width" runat="server"></asp:Label></h4>
                                        <small>Slot Width</small>
                                    </div>
                                    <div class="col-xs-4 bg-blue p-t">
                                        <h4 class="m-t-0 m-b-0"><asp:Label ID="lblSlot_Height" runat="server"></asp:Label></h4>
                                        <small>Slot Height</small>
                                    </div>
                                    <div class="col-xs-4 bg-blue p-t">
                                        <h4 class="m-t-0 m-b-0"><asp:Label ID="lblSlot_Depth" runat="server"></asp:Label></h4>
                                        <small>Slot Depth</small>
                                    </div>
                                </div>
                                                                             
                                <asp:Panel ID="pnlSlotCapacity" runat="server" CssClass="row bg-default left h5" Height="10px">
                                    <asp:Panel CssClass="bg-success" style="height:100%;" ID="levelProduct" runat="server" ></asp:Panel>  
                                    <div style="position:absolute; bottom:-15px;" class="text-grey">
                                        Available Space : <b class="text-blue"><asp:Label ID="lblFreeSpace" runat="server"></asp:Label></b>
                                    </div>
                                    <div style="position:absolute; right:0px; bottom:-15px;" class="text-grey">
                                        Max : <b class="text-danger"><asp:Label ID="lblMaxSpace" runat="server"></asp:Label></b>
                                    </div>                                                
                                </asp:Panel>
                        </div>
                    </div>
                    <div class="row m-t-md">
                        <div class="card bg-white m-b" style="font-size:14px;">
                                                                 
                            <table class="table m-b-0 checkbo">
                                <thead>
                                    <tr>
                                        <th style="position:relative;"><asp:LinkButton ID="chkSlot" runat="server" style="position: absolute; top: 5px; left: 10px;"></asp:LinkButton></th>
                                        <th>Serial</th>
                                        <th>Recent</th>
                                        <th>Remove</th>
                                    </tr>
                                </thead>
                                <tbody>

                                    <asp:Repeater ID="rptSlot" runat="server">
                                        <ItemTemplate>                                                
                                            <tr id="tr" runat="server">
                                                <td colspan="2" style="cursor:pointer; text-align:left;">
                                                        <asp:LinkButton CssClass="btn btn-primary btn-sm btn-icon" style="display:none;" ID="chk" runat="server" CommandName="Check">
                                                            <i class="icon-check"></i>
                                                        </asp:LinkButton>
                                                        <asp:Label ID="lblSerial" runat="server"></asp:Label></td>
                                                <td style="cursor:pointer;"><asp:Label ID="lblRecent" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:LinkButton CssClass="btn btn-danger btn-icon-icon btn-sm" ID="del" runat="server" CommandName="Delete">
                                                        <i class="fa fa-remove"></i>
                                                    </asp:LinkButton>
                                                </td>                                              
                                            </tr>                                                        
                                        </ItemTemplate>
                                        <FooterTemplate></FooterTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>                                   
                                 
                            <asp:LinkButton ID="btnMoveRight" runat="server" CssClass="btn btn-primary btn-lg btn-block">                                          
                                <span>Move</span>
                                <i class="fa fa-arrow-right"></i>
                            </asp:LinkButton>

                        </div>
                    </div>
                               
                </div>
            </div>
            </div>
        </asp:Panel>

    </div>
           
        <div class="col-sm-6" id="pnlScan" runat="server">
            <div class="card bg-white m-b">
                <asp:Panel CssClass="card-header" ID="pnlBarcode" runat="server" DefaultButton="btnBarcode" > 
                <h3 class="m-t-0 m-b-0">
                    <img src="../images/Icon/green/shelf.png" height="30"> สินค้ารอจัดการ <asp:Label ID="lblTotalScan" runat="server"></asp:Label>
                    <asp:TextBox ID="txtBarcode" runat="server" style="position:fixed; left:500px; top:0px;"></asp:TextBox>
                    <asp:Button ID="btnBarcode" runat="server" style="display:none;"/>
                    </h3>
                </asp:Panel>
                <ul class="nav nav-tabs" style="font-size:14px;">
                    <asp:Repeater ID="rptProductTab" runat="server">
                        <ItemTemplate>
                            <li id="li" runat="server">
                                <asp:LinkButton id="lnk" runat="server" CommandName="Select">
                                    <asp:Image ID="img" runat="server" width="30px" />  &nbsp;
                                    <asp:Label ID="lblName" runat="server"></asp:Label> 
                                </asp:LinkButton>
                            </li>                                    
                        </ItemTemplate>
                    </asp:Repeater>
                    </ul>
                        
                <asp:Panel ID="pnlScanProduct" runat="server" CssClass="card-block">
                <div class="row">
                    <div class="row">
                    <div class="m-t-n m-b">
                        <div class="row ">
                            <div class="col col-xs-4 m-t text-center">
                                <div CssClass="profile-avatar" style="padding:0; margin:0;">
                                    <asp:Image ID="imgScan_Product" runat="server" CssClass="product-Image" ImageUrl="../RenderImage.aspx?Mode=D&UID=1&Entity=Product&LANG=1" />
                                </div>                                         
                                <small>CODE : <asp:Label ID="lblScan_ProductCode" runat="server"></asp:Label></small>                                        
                            </div>
                            <div class="col col-xs-8 p-t p-l">
                                <div class="row">
                                    <div class="col-xs-9">
                                        <h4 class="m-t-0 m-b-0"><asp:Label ID="lblScan_ProductName" runat="server"></asp:Label></h4>
                                        <h6><asp:Label ID="lblScan_ProductDesc" runat="server"></asp:Label></h6>
                                    </div>
                                    <div class="col-xs-3">
                                        <asp:Image ID="imgScan_Brand" runat="server" CssClass="product-Image" ImageUrl="../RenderImage.aspx?Mode=D&UID=1&Entity=Brand&LANG=1" />
                                    </div>
                                </div>
                                            
                                    <div class="row text-center text-white  m-t">
                                        <div class="col-xs-4 bg-success p-t">
                                            <h4 class="m-t-0 m-b-0"><asp:Label ID="lblScan_Product_Width" runat="server"></asp:Label></h4>
                                            <small>Package Width</small>
                                        </div>
                                        <div class="col-xs-4 bg-success p-t">
                                            <h4 class="m-t-0 m-b-0"><asp:Label ID="lblScan_Product_Height" runat="server"></asp:Label></h4>
                                            <small>Package Height</small>
                                        </div>
                                        <div class="col-xs-4 bg-success p-t">
                                            <h4 class="m-t-0 m-b-0"><asp:Label ID="lblScan_Product_Depth" runat="server"></asp:Label></h4>
                                            <small>Package Depth</small>
                                        </div>
                                    </div>      
                            </div>
                        </div>
                        <div class="row m-t-md">
                            <div class="card bg-white m-b" style="font-size:14px;">                                                                 
                                <table class="table m-b-0">
                                    <thead>
                                        <tr>
                                            <th style="position:relative;">
                                                <asp:LinkButton CssClass="btn btn-primary btn-sm btn-icon" ID="chkScan" runat="server" style="position: absolute; top: 5px; left: 10px;">
                                                    <i class="icon-check"></i>
                                                </asp:LinkButton>
                                            </th>
                                            <th>Serial</th>
                                            <th>Recent</th>
                                            <th>Remove</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptScan" runat="server">
                                            <ItemTemplate>
                                                <tr id="tr" runat="server">
                                                    <td colspan="2" style="cursor:pointer; text-align:left;">
                                                        <asp:LinkButton CssClass="btn btn-primary btn-sm btn-icon" style="display:none;" ID="chk" runat="server" CommandName="Check">
                                                            <i class="icon-check"></i>
                                                        </asp:LinkButton>
                                                        <asp:Label ID="lblSerial" runat="server"></asp:Label></td>
                                                    <td ><asp:Label ID="lblRecent" runat="server"></asp:Label></td>  
                                                    <td id="td3" runat="server">
                                                        <asp:LinkButton CssClass="btn btn-danger btn-icon-icon btn-sm" ID="del" runat="server" CommandName="Delete">
                                                            <i class="fa fa-remove"></i>
                                                        </asp:LinkButton>
                                                    </td>                                                
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                 </table>                                        
                                 
                                <asp:LinkButton ID="btnMoveLeft" runat="server" CssClass="btn btn-success btn-lg btn-block">                                          
                                    <i class="fa fa-arrow-left"></i>
                                    <span>Move</span>                                            
                                </asp:LinkButton>
                            </div>
                        </div>
                               
                    </div>
                </div>
                </div>
                </asp:Panel>
            </div>
        </div>
</div>


<!-----DragDrop Listener------->
<asp:TextBox ID="txtDragType" runat="server" style="display:none;"></asp:TextBox>
<asp:TextBox ID="txtDragArg" runat="server" style="display:none;"></asp:TextBox>
<asp:TextBox ID="txtDropType" runat="server" style="display:none;"></asp:TextBox>
<asp:TextBox ID="txtDropArg" runat="server" style="display:none;"></asp:TextBox>
<asp:Button ID="btnDropListener" runat="server" style="display:none;" />
<!-----DragDrop Listener------->
