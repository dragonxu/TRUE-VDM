<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Shift_StockPaper.ascx.vb" Inherits="VDM.UC_Shift_StockPaper" %>
<div class="card-block">
    <div class="row ">
        <div class="form-group  col-sm-12">
            <label class="col-sm-4 control-label  h3">จำนวนครั้งที่พิมพ์ได้ </label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtUnitPaper" runat="server" class="form-control h3" Style ="text-align:center ;"></asp:TextBox>
            </div>
            <div class="col-sm-2">
                
                <asp:LinkButton CssClass="btn btn-dark h3" ID="btnAdd" runat="server">
                       
                      <span class ="h4"> เต็มจำนวน </span>
                </asp:LinkButton>
                

            </div>
        </div>
        <div class="form-group  col-sm-12">
            <label class="col-sm-4 control-label h3">จำนวนครั้งพิมพ์สูงสุด </label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtMaxPaper" runat="server" class="form-control h3" Style ="text-align:center ;"></asp:TextBox>
            </div>
        </div>
    </div>
</div>
