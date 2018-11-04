<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Report_TMS_EDI.aspx.vb" Inherits="VDM.Report_TMS_EDI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="page-title" style="">
        <div class="title">Reports &gt; EDI ตัดสต็อค TSM</div>
    </div>
    <asp:Label ID="lblUser_ID" runat="server" Style="display: none;"></asp:Label>



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
                         <th rowspan ="2">Daily</th>
                      <th rowspan ="2">Service</th>     <%-- DEVICE OR SIM--%>
                      <th rowspan ="2">Shop Name</th>
                        <th colspan="3">Payment Method (QTY)</th>
                        <th colspan="2">Total</th>
                        <th rowspan ="2">Download</th>
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
                              <td data-title="Total Value"><asp:Label ID="lblValue" runat="server"></asp:Label></td>
                              <td data-title="Download">
                                   <div class="btn-group">
                                            <asp:Button CssClass="btn btn-success btn-shadow btn-sm" ID="btnDownload" runat="server" Text="Edit" CommandName="Download" />
                                   </div>
                              </td>
                            </tr>
                        </ItemTemplate>                          
                    </asp:Repeater>   
                 <%--                         
                    <tr>                 
                      <td colspan="3" class="bg-default-light bold">Average</td>
                      <td data-title="Total Transaction" class="bg-default-light bold"><asp:Label ID="lbl_AVG_Trans_Total" runat="server"></asp:Label></td>
                      <td data-title="Success Transaction" class="bg-default-light bold"><asp:Label ID="lbl_AVG_Trans_Success" runat="server"></asp:Label></td>                  
                      <td data-title="Success Qty" class="bg-default-light bold"><asp:Label ID="lbl_AVG_Qty_Success" runat="server"></asp:Label></td>
                       <td data-title="Total Qty" class="bg-default-light bold"><asp:Label ID="lbl_AVG_Qty_Total" runat="server"></asp:Label></td>                  
                      <td data-title="Total Value" class="bg-default-light bold"><asp:Label ID="lbl_AVG_Value" runat="server"></asp:Label></td>
                    </tr>--%>

                    <tr>                 
                        <td colspan="3" class="bg-default-light bold">Total</td>
                        <td data-title="Total Transaction" class="bg-default-light bold"><asp:Label ID="lbl_SUM_Trans_Total" runat="server"></asp:Label></td>
                        <td data-title="Success Transaction" class="bg-default-light bold"><asp:Label ID="lbl_SUM_Trans_Success" runat="server"></asp:Label></td>                  
                        <td data-title="Success Qty" class="bg-default-light bold"><asp:Label ID="lbl_SUM_Qty_Success" runat="server"></asp:Label></td>
                         <td data-title="Total Qty" class="bg-default-light bold"><asp:Label ID="lbl_SUM_Qty_Total" runat="server"></asp:Label></td>                  
                        <td data-title="Total Value" class="bg-default-light bold"><asp:Label ID="lbl_SUM_Value" runat="server"></asp:Label></td>
                        <td data-title="Total Value" class="bg-default-light bold"></td>
                   
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
</asp:Content>
