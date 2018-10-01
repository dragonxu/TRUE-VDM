<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_SIM_Stock.ascx.vb" Inherits="VDM.UC_SIM_Stock" %>
<%@ Register Src="~/Machine_Console/UC_SIM_Dispenser.ascx" TagPrefix="uc1" TagName="UC_SIM_Dispenser" %>

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
    <div class="col-sm-7" ondragover="draggedOver(event)">
        <uc1:UC_SIM_Dispenser runat="server" ID="Dispenser" />
        <asp:Panel ID="pnlSlotSIM" runat="server" CssClass="card bg-white m-b m-t-md" SIM_ID="0">
            <div class="card-header">
            <h3 class="m-t-0 m-b-0 pull-left">
                <img src="../images/Icon/green/shelf.png" height="30"> SIM Slot <asp:Label ID="lblSlotID" runat="server"></asp:Label>
            </h3>
            <asp:LinkButton ID="btnSeeDispenser" runat="server" CssClass="pull-right btn btn-primary btn-sm" KO_ID="0" SHOP_CODE="" MY_UNIQUE_ID="" SHIFT_ID="0" SHIFT_STATUS="99">
                <i class="fa fa-reply"></i>
                <span>see all</span>
            </asp:LinkButton>

            </div>
            <div class="card-block">
            <div class="row">
                <div class="m-t-n m-b">
                    <div class="row ">
                        <div class="col col-md-4 m-t text-center">
                            <asp:Image ID="imgSlot" runat="server" CssClass="product-Image" ImageUrl="../RenderImage.aspx?Mode=D&UID=1&Entity=SIM_Package&LANG=1" />
                        </div>
                        <div class="col col-md-8 p-t p-l">
                            <div class="row">
                                <h4>CODE : <asp:Label ID="lblSlot_SIMCode" runat="server"></asp:Label></h4><br/>
                                <h4 class="m-t-0 m-b-0 bold">
                                    <asp:Label ID="lblSlot_SIMName" runat="server"></asp:Label>
                                    <asp:Label ID="lblSlot_Quantity" runat="server" Font-Bold="true" ForeColor="SteelBlue" CssClass="h2"></asp:Label> 
                                </h4>    
                                                                
                            </div>
                            <h3 class="bold text-default m-t-0 col-md-4" id="tagSlot_Empty" runat="server">Empty</h3>                                 
                            <asp:Panel ID="pnlSlotCapacity" runat="server" CssClass="bg-default left h5" Height="10px">
                                <asp:Panel CssClass="bg-success" style="height:100%;" ID="levelSIM" runat="server" ></asp:Panel>  
                                <div style="position:absolute; bottom:-15px;" class="text-grey">
                                    Space : <b class="text-blue"><asp:Label ID="lblFreeSpace" runat="server"></asp:Label></b>
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
                                        <th><asp:LinkButton ID="chkSlot" runat="server"></asp:LinkButton></th>
                                        <th>Serial</th>
                                        <th>Recent</th>
                                        <th>Remove</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptSlot" runat="server">
                                        <ItemTemplate>                                                
                                            <tr id="tr" runat="server">
                                                <td colspan="2" style="cursor:pointer;">
                                                    <asp:LinkButton CssClass="btn btn-primary btn-sm btn-icon" style="display:none;" ID="chk" runat="server" CommandName="Check">
                                                        <i class="icon-check"></i>
                                                    </asp:LinkButton>
                                                    <asp:Label ID="lblSerial" runat="server"></asp:Label></td>
                                                <td><asp:Label ID="lblRecent" runat="server"></asp:Label></td>
                                                <td width="50">
                                                    <asp:LinkButton CssClass="btn btn-danger btn-icon-icon btn-sm"  ID="del" runat="server" CommandName="Delete">
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

    <asp:Panel ID="pnlScan" runat="server" CssClass="col-sm-5" >
        <div class="card bg-white m-b">
            <asp:Panel CssClass="card-header" ID="pnlBarcode" runat="server" DefaultButton="btnBarcode" > 
                <h3 class="m-t-0 m-b-0">
                    <img src="../images/Icon/green/shelf.png" height="30"> SIM รอจัดการ <asp:Label ID="lblTotalScan" runat="server"></asp:Label>
                    <asp:TextBox ID="txtBarcode" runat="server" style="position:fixed; left:500px; top:0px;"></asp:TextBox>
                    <asp:Button ID="btnBarcode" runat="server" style="display:none;"/>
                </h3>
            </asp:Panel>
            <ul class="nav nav-tabs" style="font-size:14px;">
                <asp:Repeater ID="rptSIMType" runat="server">
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
                        
            <asp:Panel ID="pnlScanSIM" runat="server" CssClass="card-block">
                <div class="row">
                    <div class="row">
                    <div class="m-t-n m-b">
                        <div class="row ">
                            <div class="col col-xs-4 m-t text-center">
                                <div CssClass="profile-avatar" style="padding:0; margin:0;">
                                    <asp:Image ID="imgScan" runat="server" CssClass="product-Image" ImageUrl="../RenderImage.aspx?Mode=D&UID=1&Entity=SIM_Package&LANG=1" />
                                </div>                                
                            </div>
                            <div class="col col-xs-8 p-t p-l">
                                <div class="row">
                                    <h4>CODE : <asp:Label ID="lblScan_SIMCode" runat="server"></asp:Label></h4>  
                                    <h4 class="m-t-0 m-b-0 bold"><asp:Label ID="lblScan_SIMName" runat="server"></asp:Label></h4>
                                </div>          
                            </div>
                        </div>
                        <div class="row m-t-md">
                            <div class="card bg-white m-b" style="font-size:14px;">                                                                 
                                <table class="table m-b-0">
                                    <thead>
                                        <tr>
                                            <th>
                                                <asp:LinkButton CssClass="btn btn-primary btn-sm btn-icon" ID="chkScan" runat="server">
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
                                                        <asp:LinkButton CssClass="btn btn-primary btn-sm btn-icon" ID="chk" style="display:none;" runat="server" CommandName="Check">
                                                            <i class="icon-check"></i>
                                                        </asp:LinkButton>
                                                        <asp:Label ID="lblSerial" runat="server"></asp:Label></td>
                                                    <td><asp:Label ID="lblRecent" runat="server"></asp:Label></td>  
                                                    <td width="50">
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
    </asp:Panel>
</div>

<!-----DragDrop Listener------->
<asp:TextBox ID="txtDragType" runat="server" style="display:none;"></asp:TextBox>
<asp:TextBox ID="txtDragArg" runat="server" style="display:none;"></asp:TextBox>
<asp:TextBox ID="txtDropType" runat="server" style="display:none;"></asp:TextBox>
<asp:TextBox ID="txtDropArg" runat="server" style="display:none;"></asp:TextBox>
<asp:Button ID="btnDropListener" runat="server" style="display:none;" />
<!-----DragDrop Listener------->