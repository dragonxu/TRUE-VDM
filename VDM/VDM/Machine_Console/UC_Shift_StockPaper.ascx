<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Shift_StockPaper.ascx.vb" Inherits="VDM.UC_Shift_StockPaper" %>
<div class="card-block">
    <div class="row ">

        <div class="form-group  col-sm-12">
            <label class="col-sm-4 control-label h3">เต็มม้วนพิมพ์ได้ </label>
            <div class="col-sm-4" style ="text-align :center ;">
                 <h3><asp:Label ID="lblMaxPaper" runat ="server"  ></asp:Label></h3>
            </div>
            <div class="col-sm-4">
               <h3> <span   >ครั้ง</span></h3>
            </div>
        </div>

        <div class="form-group  col-sm-12">
            <label class="col-sm-4 control-label  h3">เหลือพิมพ์ได้ </label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtRemainPaper" runat="server" class="form-control h3" Style="text-align: center;"></asp:TextBox>
            </div>
            <div class="col-sm-1">
               <h3> <span   >ครั้ง</span></h3>
            </div>
            <div class="col-sm-2">

                <asp:LinkButton CssClass="btn btn-dark h3" ID="btnInput_Full" runat="server">
                       
                      <span class ="h4"> เติมม้วนใหม่ </span>
                </asp:LinkButton>


            </div>
        </div>
    </div>
</div>
