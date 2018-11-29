<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Report_TMS_EDI.aspx.vb" Inherits="VDM.Report_TMS_EDI" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="page-title" style="">
        <div class="title">Reports &gt; EDI ตัดสต็อค TSM</div>
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
                    <label class="col-sm-2 control-label">Service</label>
                    <div class="col-sm-10">                      
                      <asp:DropDownList ID="ddlService" runat="server" CssClass="form-control" AutoPostBack="True" style="width: 100%;">
                          <asp:ListItem Value ="-1" Text =""></asp:ListItem>
                          <asp:ListItem Value ="0" Text ="PRODUCT"></asp:ListItem>
                          <asp:ListItem Value ="1" Text ="SIM"></asp:ListItem>
                      </asp:DropDownList>
                    </div>
                  </div>                    
                </div>
                <div class="col-lg-5">
                   <div class="form-group">
                    <label class="col-sm-2 control-label">Shop Name</label>
                    <div class="col-sm-10">                      
                      <asp:DropDownList ID="ddlShop_Name" runat="server" CssClass="form-control" AutoPostBack="True" style="width: 100%;">
                          
                      </asp:DropDownList>
                    </div>
                  </div>                    
                </div> 

               
                <div class="col-lg-5">
                   <div class="form-group">
                    <label class="col-sm-2 control-label">Daily</label>
                    <div class="col-sm-10">  
                        <asp:TextBox CssClass="form-control m-b" ID="txtStartDate" runat="server" placeholder="Select Date"     ></asp:TextBox>
                        
                    <cc1:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server" 
                             Format="dd/MM/yyyy" TargetControlID="txtStartDate" PopupPosition="BottomLeft"></cc1:CalendarExtender>
                      
                    </div>
                  </div>                    
               </div>
            </div>
            <div class="row">
                <asp:LinkButton CssClass="btn btn-primary btn-icon loading-demo mr5 m-t btn-shadow" ID="btnApply" runat="server">
                  <i class="fa fa-check"></i>
                  <span>Apply</span>
                </asp:LinkButton>
                <asp:LinkButton CssClass="btn btn-success btn-icon loading-demo mr5 m-t btn-shadow" ID="lnkExcel" runat="server">
                <i class="fa fa-table"></i>
                <span>Export to Excel</span>
                </asp:LinkButton>
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
                        <th rowspan ="2" style ="width: 130px;">Daily</th>
                        <th rowspan ="2">Service</th>     <%-- DEVICE OR SIM--%>
                        <th rowspan ="2">Shop Name</th>
                        <th colspan="3">Payment Method (QTY)</th>
                        <th colspan="2">Total</th>
                        <th rowspan ="2" style ="width: 130px;">Download</th>
                    </tr>
                    <tr>
                     
                      <th>Cash</th>
                      <th>TMN Wallet</th>
                      <th>Credit Card</th>
                         
                      <th>Qty</th>
                      <th>Value</th>                     
                    </tr>
                  </thead>
                  <tbody>
                   <asp:Repeater ID="rptData" runat="server">
                        <ItemTemplate>
                            <tr>                 
                              <td data-title="Daily" id="tdMode" runat="server"><asp:Label ID="lblTime" runat="server"></asp:Label></td>
                              <td data-title="Service"><asp:Label ID="lblService" runat="server"></asp:Label></td>
                              <td data-title="Shop Name"><asp:Label ID="lblShop_Name" runat="server"></asp:Label></td>
                              <td data-title="Cash"><asp:Label ID="lblCash" runat="server"></asp:Label></td>
                              <td data-title="TMN Wallet"><asp:Label ID="lblTMN_Wallet" runat="server"></asp:Label></td>                  
                              <td data-title="Credit Card"><asp:Label ID="lblCredit_Card" runat="server"></asp:Label></td>
                              <td data-title="Total Qty"><asp:Label ID="lblQtyTotal" runat="server"></asp:Label></td>                  
                              <td data-title="Total Value" Style ="text-align :right ;"><asp:Label ID="lblValue" runat="server"></asp:Label></td>
                              <td data-title="Download">
                                   <div class="btn-group">
                                            <asp:Button CssClass="btn btn-success btn-shadow btn-sm" ID="btnDownload" runat="server"  Text="Batch File" CommandName="Download" value="Download!" Style ="display :none ;"  />
                                   
                                       <a id="lnkTXT" runat="server"   download="file-name"  CommandName="lnk_Download" style ="color :darkblue ;" ><i class="fa fa-fw fa-download"></i> Batch File</a>
                                       
                                       <%-- <a id="lnkTXT" runat="server" href ="Temp\BATCH_FILE_TSM_434242013973264.txt" download="file-name"  CommandName="lnk_Download" >TXT</a>--%>
                                      
                                       
                                        </div>
                              </td>
                            </tr>
                        </ItemTemplate>                          
                    </asp:Repeater>   
                 

                    <tr>                 
                        <td colspan="3" class="bg-default-light bold">Total</td>
                        <td data-title="Cash Qty" class="bg-default-light bold"><asp:Label ID="lblSUM_Cash" runat="server"></asp:Label></td>
                        <td data-title="TMN Wallet Qty" class="bg-default-light bold"><asp:Label ID="lblSUM_TMN_Wallet" runat="server"></asp:Label></td>                  
                        <td data-title="Credit Card Qty" class="bg-default-light bold"><asp:Label ID="lblSUM_Credit_Card" runat="server"></asp:Label></td>
                         <td data-title="Total Qty" class="bg-default-light bold"><asp:Label ID="lblSUM_QtyTotal" runat="server"></asp:Label></td>                  
                        <td data-title="Total Value" class="bg-default-light bold"  Style ="text-align :right ;"><asp:Label ID="lblSUM_Value" runat="server"></asp:Label></td>
                        <td data-title="" class="bg-default-light bold"></td>
                   
                         </tr>

                  </tbody>
                </table>
            </div>
        </div>
    </div>
 </ContentTemplate>
</asp:UpdatePanel>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContainer" runat="server">
     <script type="text/javascript">
         function TXTClick() {
             $("#lnkTXT").click();
         }
 

   </script>

</asp:Content>
