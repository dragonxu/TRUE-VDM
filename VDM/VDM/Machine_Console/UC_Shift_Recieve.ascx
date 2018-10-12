<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Shift_Recieve.ascx.vb" Inherits="VDM.UC_Shift_Recieve" %>
<div class="card-block">
    <style type="text/css">
        .table > tbody > tr > td {
            border-color: transparent;
            height: 100px;
        }

        .table > tfoot > tr > td {
            /*border-color: transparent ;*/
            height: 100px;
        }
    </style>

    <style type="text/css">
        .table > tbody > tr > td {
            border-color: transparent;
            height: 100px;
        }

        .table > tfoot > tr > td {
            /*border-color: transparent ;*/
            height: 100px;
        }
    </style>

    <div class="no-more-tables  ">
        <table class="table m-b-0">
            <thead>
                <tr>

                    <th></th>
                    <th class="col-md-5" style="text-align: right;">เดิม</th>
                    <th style="padding-right: 0px; padding-left: 0px;"></th>
                    <th colspan="2">เอาออก</th>
                    <th style="padding-right: 0px; padding-left: 0px;"></th>
                    <th colspan="2">เอาเข้า</th>
                    <th style="padding-right: 0px; padding-left: 0px;"></th>
                    <th class="col-md-5" style="text-align: right;">คงเหลือ</th>
                    <th class="col-md-5" style="text-align: right;"><span>บาท</span></th>
                    <th style="padding-right: 0px; padding-left: 0px;"></th>

                </tr>
            </thead>
            <tbody>
                <tr>
                    <td data-title="Logo" colspan="2" style="height: unset;">
                        <h4><b>เครื่องรับเหรียญ</b></h4>
                    </td>
                </tr>

                <asp:Repeater ID="rptListCoinIn" runat="server">
                    <%--<ItemTemplate>
                        <tr>
                            <td data-title="Logo" style="text-align: center;">
                                <img id="img" runat="server" width="40">
                            </td>
                            <td data-title="เดิม"  style="text-align: right;">
                                <asp:Label ID="lbl_Before" runat="server"></asp:Label> 
                             </td>
                            <td data-title="เอาออก" style="padding-right: 0px;">
                                <asp:TextBox ID="txt_Pick" runat="server" class="form-control" Style="width: 80px; text-align: center;" OnTextChanged="txt_TextChanged_CoinIn" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td data-title="เอาออก" style="text-align: left; padding-left: 0px;">

                                <asp:Button CssClass="btn btn-dark" ID="btn_Pick_Full" runat="server" Style="font-size: 18px;" CommandName="Pick_Full" Text="เต็มจำนวน" />

                            <td data-title="เอาเข้า" style="padding-right: 0px;">
                                <asp:TextBox ID="txt_Input" runat="server" class="form-control" Style="width: 80px; text-align: center;" OnTextChanged="txt_TextChanged_CoinIn" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td data-title="เอาเข้า" style="text-align: left; padding-left: 0px;">

                                <asp:Button CssClass="btn btn-dark" ID="btn_Input_Full" runat="server" Style="font-size: 18px;" CommandName="Input_Full" Text="เต็มจำนวน" />

                            </td>

                            <td data-title="คงเหลือ"  style="text-align: right;">
                                <asp:Label ID="lbl_Remain" runat="server"></asp:Label>
                             </td>
                            <td data-title="จำนวนเงิน" style="text-align: right;">
                                <b>
                                    <asp:Label ID="lbl_Amount" runat="server"></asp:Label></b>

                                <asp:LinkButton CssClass="btn btn-dark " ID="lnktxtChange" runat="server" CommandName="txt_Change" Style="display: none;">
                              <span class ="h4">   </span>
                                </asp:LinkButton>
                            </td>


                        </tr>
                    </ItemTemplate>--%>

                    <ItemTemplate>
                        <tr>
                            <td data-title="Logo" style="text-align: center;">
                                <img id="img" runat="server" width="40">
                            </td>
                            <td data-title="เดิม" style="text-align: right;">
                                <asp:Label ID="lbl_Before" runat="server"></asp:Label>
                            </td>
                            <td data-title="-" style="text-align: center; padding-right: 0px; padding-left: 0px;">
                                <span>-</span>
                            </td>
                            <td data-title="เอาออก" style="padding-right: 0px;">
                                <asp:TextBox ID="txt_Pick" runat="server" CssClass="form-control osk-trigger"  data-osk-options="disableReturn" Style="width: 80px; text-align: center;" OnTextChanged="txt_TextChanged_CoinIn" AutoPostBack="true"></asp:TextBox>
                            </td>

                            <td data-title="เอาออก" style="text-align: left; padding-left: 0px;">

                                <asp:Button CssClass="btn btn-dark" ID="btn_Pick_Full" runat="server" Style="font-size: 16px;" CommandName="Pick_Full" Text="เต็มจำนวน" />
                            </td>
                            <td data-title="+" style="text-align: center; padding-right: 0px; padding-left: 0px;">
                                <span>+</span>
                            </td>
                            <td data-title="เอาเข้า" style="padding-right: 0px;">
                                <asp:TextBox ID="txt_Input" runat="server" CssClass="form-control osk-trigger"  data-osk-options="disableReturn" Style="width: 80px; text-align: center;" OnTextChanged="txt_TextChanged_CoinIn" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td data-title="เอาเข้า" style="text-align: left; padding-left: 0px;">

                                <asp:Button CssClass="btn btn-dark" ID="btn_Input_Full" runat="server" Style="font-size: 16px;" CommandName="Input_Full" Text="เต็มจำนวน" />

                            </td>
                            <td data-title="-" style="text-align: center; padding-right: 0px; padding-left: 0px;">
                                <span>=</span>
                            </td>
                            <td data-title="คงเหลือ" style="text-align: right;">
                                <asp:Label ID="lbl_Remain" runat="server"></asp:Label>
                            </td>
                            <td data-title="จำนวนเงิน" style="text-align: right;">
                                <b>
                                    <asp:Label ID="lbl_Amount" runat="server"></asp:Label></b>

                                <asp:LinkButton CssClass="btn btn-dark " ID="lnktxtChange" runat="server" CommandName="txt_Change" Style="display: none;">
                              <span class ="h4">   </span>
                                </asp:LinkButton>


                            </td>
                            <td data-title="img" style="text-align: right; padding-right: 0px; padding-left: 0px;">
                                <asp:Image ID="imgAlert" runat="server" ImageUrl="~/images/Icon/alert.gif" Visible="false" />
                            </td>


                        </tr>
                    </ItemTemplate>


                </asp:Repeater>
                <asp:Label ID="lbl_RemainCoinIn" runat="server" Style="display: none;"></asp:Label>
                <asp:Label ID="lbl_SumCoinIn" runat="server" Style="display: none;"></asp:Label>



                <tr>
                    <td data-title="Logo" colspan="2" style="height: unset;">
                        <h4><b>เครื่องรับแบงค์</b></h4>
                    </td>
                </tr>

                <asp:Repeater ID="rptListCashIn" runat="server">
                    <%--                    <ItemTemplate>
                        <tr>
                            <td data-title="Logo" style="text-align: center;">
                                <img id="img" runat="server" width="40">
                            </td>
                            <td data-title="เดิม" style="text-align: right;">
                                <asp:Label ID="lbl_Before" runat="server"></asp:Label>
                            </td>
                            <td data-title="เอาออก" style="padding-right: 0px;">
                                <asp:TextBox ID="txt_Pick" runat="server" class="form-control" Style="width: 80px; text-align: center;" OnTextChanged="txt_TextChanged_CashIn" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td data-title="เอาออก" style="text-align: left; padding-left: 0px;">

                                <asp:Button CssClass="btn btn-dark" ID="btn_Pick_Full" runat="server" Style="font-size: 18px;" CommandName="Pick_Full" Text="เต็มจำนวน" />

                            <td data-title="เอาเข้า" style="padding-right: 0px;">
                                <asp:TextBox ID="txt_Input" runat="server" class="form-control" Style="width: 80px; text-align: center;" OnTextChanged="txt_TextChanged_CashIn" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td data-title="เอาเข้า" style="text-align: left; padding-left: 0px;">

                                <asp:Button CssClass="btn btn-dark" ID="btn_Input_Full" runat="server" Style="font-size: 18px;" CommandName="Input_Full" Text="เต็มจำนวน" />

                            </td>

                            <td data-title="คงเหลือ" style="text-align: right;">
                                <asp:Label ID="lbl_Remain" runat="server"></asp:Label>
                            </td>
                            <td data-title="จำนวนเงิน" style="text-align: right;">
                                <b>
                                    <asp:Label ID="lbl_Amount" runat="server"></asp:Label></b>

                                <asp:LinkButton CssClass="btn btn-dark " ID="lnktxtChange" runat="server" CommandName="txt_Change" Style="display: none;">
                              <span class ="h4">   </span>
                                </asp:LinkButton>
                            </td>


                        </tr>
                    </ItemTemplate>--%>

                    <ItemTemplate>
                        <tr>
                            <td data-title="Logo" style="text-align: center;">
                                <img id="img" runat="server" width="40">
                            </td>
                            <td data-title="เดิม" style="text-align: right;">
                                <asp:Label ID="lbl_Before" runat="server"></asp:Label>
                            </td>
                            <td data-title="-" style="text-align: center; padding-right: 0px; padding-left: 0px;">
                                <span>-</span>
                            </td>
                            <td data-title="เอาออก" style="padding-right: 0px;">
                                <asp:TextBox ID="txt_Pick" runat="server" CssClass="form-control osk-trigger"  data-osk-options="disableReturn" Style="width: 80px; text-align: center;" OnTextChanged="txt_TextChanged_CashIn" AutoPostBack="true"></asp:TextBox>
                            </td>

                            <td data-title="เอาออก" style="text-align: left; padding-left: 0px;">

                                <asp:Button CssClass="btn btn-dark" ID="btn_Pick_Full" runat="server" Style="font-size: 16px;" CommandName="Pick_Full" Text="เต็มจำนวน" />
                            </td>
                            <td data-title="+" style="text-align: center; padding-right: 0px; padding-left: 0px;">
                                <span>+</span>
                            </td>
                            <td data-title="เอาเข้า" style="padding-right: 0px;">
                                <asp:TextBox ID="txt_Input" runat="server" CssClass="form-control osk-trigger"  data-osk-options="disableReturn" Style="width: 80px; text-align: center;" OnTextChanged="txt_TextChanged_CashIn" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td data-title="เอาเข้า" style="text-align: left; padding-left: 0px;">

                                <asp:Button CssClass="btn btn-dark" ID="btn_Input_Full" runat="server" Style="font-size: 16px;" CommandName="Input_Full" Text="เต็มจำนวน" />

                            </td>
                            <td data-title="-" style="text-align: center; padding-right: 0px; padding-left: 0px;">
                                <span>=</span>
                            </td>
                            <td data-title="คงเหลือ" style="text-align: right;">
                                <asp:Label ID="lbl_Remain" runat="server"></asp:Label>
                            </td>
                            <td data-title="จำนวนเงิน" style="text-align: right;">
                                <b>
                                    <asp:Label ID="lbl_Amount" runat="server"></asp:Label></b>

                                <asp:LinkButton CssClass="btn btn-dark " ID="lnktxtChange" runat="server" CommandName="txt_Change" Style="display: none;">
                              <span class ="h4">   </span>
                                </asp:LinkButton>


                            </td>
                            <td data-title="img" style="text-align: right; padding-right: 0px; padding-left: 0px;">
                                <asp:Image ID="imgAlert" runat="server" ImageUrl="~/images/Icon/alert.gif" Visible="false" />
                            </td>


                        </tr>
                    </ItemTemplate>


                </asp:Repeater>
                <asp:Label ID="lbl_RemainCashIn" runat="server" Style="display: none;"></asp:Label>
                <asp:Label ID="lbl_SumCashIn" runat="server" Style="display: none;"></asp:Label>

            </tbody>
            <tfoot>
                <tr>
                    <td colspan="7" data-title="Logo" style="text-align: right;"><b>รวม</b> </td>
                    <td data-title="รวม" style="text-align: right;">
                        <b>
                            <asp:Label ID="lblSum" runat="server"></asp:Label></b>
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>




</div>
