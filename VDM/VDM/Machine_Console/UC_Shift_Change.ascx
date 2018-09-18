<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Shift_Change.ascx.vb" Inherits="VDM.UC_Shift_Change" %>
<div class="card-block">
    <style type ="text/css" >
        .table > tbody > tr > td {
        border-color: transparent ;
        height :100px;
        }
        .table > tfoot > tr > td {
        /*border-color: transparent ;*/
         height :100px;
        }
    </style>

    <div class="no-more-tables  ">
        <table class="table m-b-0">
            <thead>
                <tr>
                    <th></th>
                    <th>เดิม</th>
                    <th colspan="2">เอาออก</th>
                    <th colspan="2">เอาเข้า</th>
                    <th>คงเหลือ</th>
                    <th class="col-md-5" style ="text-align :right ;"><span>บาท</span></th>

                </tr>
            </thead>
            <tbody>
                <tr>
                    <td data-title="Logo" style="text-align: center;">
                        <img id="imgcoin1" src="../images/Icon/green/coin1.png" width="40">
                    </td>
                    <td data-title="เดิม">
                        <asp:TextBox ID="txt_coin1_Before" runat="server" AutoPostBack ="true"  class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="เอาออก" style="padding-right: 0px;">
                        <asp:TextBox ID="txt_coin1_Pick" runat="server"  AutoPostBack ="true" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="เอาออก" style="text-align: left; padding-left: 0px;">
                         <asp:LinkButton CssClass="btn btn-dark " ID="btn_coin1_Pick_Full" runat="server">
                              <span class ="h4"> เต็มจำนวน </span>
                        </asp:LinkButton>
                    <td data-title="เอาเข้า" style="padding-right: 0px;">
                        <asp:TextBox ID="txt_coin1_Input" runat="server"  AutoPostBack ="true" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="เอาเข้า" style="text-align: left; padding-left: 0px;">
                        <asp:LinkButton CssClass="btn btn-dark " ID="btn_coin1_Input_Full" runat="server">
                              <span class ="h4"> เต็มจำนวน </span>
                        </asp:LinkButton>
                    </td>

                    <td data-title="คงเหลือ">
                        <asp:TextBox ID="txt_coin1_Remain" runat="server"  Enabled ="false"  class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="จำนวนเงิน" style="text-align: right;">
                        <b>
                            <asp:Label ID="lbl_coin1_Amount" runat="server" ></asp:Label></b>
                    </td>


                </tr>

                <tr>
                    <td data-title="Logo" style="text-align: center;">
                        <img id="imgcoin5" src="../images/Icon/green/coin5.png" width="40">
                    </td>
                    <td data-title="เดิม">
                        <asp:TextBox ID="txt_coin5_Before" runat="server" AutoPostBack ="true"  class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="เอาออก" style="padding-right: 0px;">
                        <asp:TextBox ID="txt_coin5_Pick" runat="server"  AutoPostBack ="true" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="เอาออก" style="text-align: left; padding-left: 0px;">
                         <asp:LinkButton CssClass="btn btn-dark " ID="btn_coin5_Pick_Full" runat="server">
                              <span class ="h4"> เต็มจำนวน </span>
                        </asp:LinkButton>
                    <td data-title="เอาเข้า" style="padding-right: 0px;">
                        <asp:TextBox ID="txt_coin5_Input" runat="server"  AutoPostBack ="true" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="เอาเข้า" style="text-align: left; padding-left: 0px;">
                        <asp:LinkButton CssClass="btn btn-dark " ID="btn_coin5_Input_Full" runat="server">
                              <span class ="h4"> เต็มจำนวน </span>
                        </asp:LinkButton>
                    </td>

                    <td data-title="คงเหลือ">
                        <asp:TextBox ID="txt_coin5_Remain" runat="server"  Enabled ="false"  class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="จำนวนเงิน" style="text-align: right;">
                        <b>
                            <asp:Label ID="lbl_coin5_Amount" runat="server" ></asp:Label></b>
                    </td>


                </tr>

                <tr>
                    <td data-title="Logo" style="text-align: center;">
                        <img id="imgcash20" src="../images/Icon/green/cash20.png" width="40">
                    </td>
                    <td data-title="เดิม">
                        <asp:TextBox ID="txt_cash20_Before" runat="server" AutoPostBack ="true"  class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="เอาออก" style="padding-right: 0px;">
                        <asp:TextBox ID="txt_cash20_Pick" runat="server"  AutoPostBack ="true" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="เอาออก" style="text-align: left; padding-left: 0px;">
                         <asp:LinkButton CssClass="btn btn-dark " ID="btn_cash20_Pick_Full" runat="server">
                              <span class ="h4"> เต็มจำนวน </span>
                        </asp:LinkButton>
                    <td data-title="เอาเข้า" style="padding-right: 0px;">
                        <asp:TextBox ID="txt_cash20_Input" runat="server"  AutoPostBack ="true" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="เอาเข้า" style="text-align: left; padding-left: 0px;">
                        <asp:LinkButton CssClass="btn btn-dark " ID="btn_cash20_Input_Full" runat="server">
                              <span class ="h4"> เต็มจำนวน </span>
                        </asp:LinkButton>
                    </td>

                    <td data-title="คงเหลือ">
                        <asp:TextBox ID="txt_cash20_Remain" runat="server"  Enabled ="false"  class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="จำนวนเงิน" style="text-align: right;">
                        <b>
                            <asp:Label ID="lbl_cash20_Amount" runat="server" ></asp:Label></b>
                    </td>


                </tr>

                <tr>
                    <td data-title="Logo" style="text-align: center;">
                        <img id="imgcash100" src="../images/Icon/green/cash100.png" width="40">
                    </td>
                    <td data-title="เดิม">
                        <asp:TextBox ID="txt_cash100_Before" runat="server" AutoPostBack ="true"  class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="เอาออก" style="padding-right: 0px;">
                        <asp:TextBox ID="txt_cash100_Pick" runat="server"  AutoPostBack ="true" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="เอาออก" style="text-align: left; padding-left: 0px;">
                         <asp:LinkButton CssClass="btn btn-dark " ID="btn_cash100_Pick_Full" runat="server">
                              <span class ="h4"> เต็มจำนวน </span>
                        </asp:LinkButton>
                    <td data-title="เอาเข้า" style="padding-right: 0px;">
                        <asp:TextBox ID="txt_cash100_Input" runat="server"  AutoPostBack ="true" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="เอาเข้า" style="text-align: left; padding-left: 0px;">
                        <asp:LinkButton CssClass="btn btn-dark " ID="btn_cash100_Input_Full" runat="server">
                              <span class ="h4"> เต็มจำนวน </span>
                        </asp:LinkButton>
                    </td>

                    <td data-title="คงเหลือ">
                        <asp:TextBox ID="txt_cash100_Remain" runat="server"  Enabled ="false"  class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="จำนวนเงิน" style="text-align: right;">
                        <b>
                            <asp:Label ID="lbl_cash100_Amount" runat="server" ></asp:Label></b>
                    </td>


                </tr>

 
            </tbody>
            <tfoot >
                <tr>
                    <td colspan ="7" data-title="Logo" style="text-align: right;"><b>รวม</b> </td>
                    <td data-title="รวม" style="text-align: right;">
                        <b>
                            <asp:Label ID="lblSum" runat="server" ></asp:Label></b>
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>












    <%--coin5--%>
<%--    <div class="row">

        <div class="col-sm-2">
            <div class="row m-b">
                <div class="col-xs-4 text-right">
                    <img id="imgcoin5" src="../images/Icon/green/coin5.png" width="40px">
                    <span>เดิม</span>
                </div>
                <div class="col-xs-8">
                    <asp:TextBox ID="TextBox1" runat="server" class="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="col-sm-2">
            <div class="row m-b">
                <div class="col-xs-4 text-right">
                    <span>เอาออก</span>
                </div>
                <div class="col-xs-6">
                    <asp:TextBox ID="TextBox2" runat="server" class="form-control"></asp:TextBox>
                </div>
                <div class="col-xs-2">
                    <button type="button" class="btn btn-dark">เต็มจำนวน</button>
                </div>
            </div>
        </div>
        <div class="col-sm-2">
            <div class="row m-b">
                <div class="col-xs-4 text-right">
                    <span>เอาเข้า</span>
                </div>
                <div class="col-xs-6">
                    <asp:TextBox ID="TextBox3" runat="server" class="form-control"></asp:TextBox>
                </div>
                <div class="col-xs-2">
                    <button type="button" class="btn btn-dark">เต็มจำนวน</button>
                </div>
            </div>
        </div>
        <div class="col-sm-2">
            <div class="row m-b">
                <div class="col-xs-4 text-right">
                    <span>คงเหลือ</span>
                </div>
                <div class="col-xs-8">
                    <asp:TextBox ID="TextBox4" runat="server" class="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="col-sm-2">
            <div class="row m-b">
                <div class="col-xs-4 text-right">
                    <span>จำนวนเงิน</span>
                </div>
                <div class="col-xs-8">
                    <asp:Label ID="lblSumChangeCoin5" runat="server" Text="1,005"></asp:Label>
                </div>
            </div>
        </div>
    </div>--%>


</div>
