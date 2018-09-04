<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Product_Spec.ascx.vb" Inherits="VDM.UC_Product_Spec" %>


        <asp:TextBox ID="txtCode" runat="server" class="form-control" Style="display: none;"></asp:TextBox>
        <div class="form-group  col-sm-12">
            <label class="col-sm-2 control-label">
                Spec  <span style="color: red;">*</span>
            </label>
            <div class="col-sm-10">
                <div class="no-more-tables">
                    <table class="table table-bordered  m-b-0">
                        <thead>
                            <tr>
                                <th>Spec</th>
                                <th>Description</th>
                                <th class="col-md-1">Delete</th>

                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptCaptionList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td style="padding-top: 0;">
                                            <asp:DropDownList class="form-control" ID="ddlSpec_TH" runat="server" Style="border: none; width: 100%;" OnSelectedIndexChanged="ddlSpec_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            <asp:DropDownList class="form-control" ID="ddlSpec_EN" runat="server" Style="border: none; width: 100%;" OnSelectedIndexChanged="ddlSpec_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            <asp:DropDownList class="form-control" ID="ddlSpec_CH" runat="server" Style="border: none; width: 100%;" OnSelectedIndexChanged="ddlSpec_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            <asp:DropDownList class="form-control" ID="ddlSpec_JP" runat="server" Style="border: none; width: 100%;" OnSelectedIndexChanged="ddlSpec_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            <asp:DropDownList class="form-control" ID="ddlSpec_KR" runat="server" Style="border: none; width: 100%;" OnSelectedIndexChanged="ddlSpec_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            <asp:DropDownList class="form-control" ID="ddlSpec_RS" runat="server" Style="border: none; width: 100%;" OnSelectedIndexChanged="ddlSpec_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
 
                                        </td>
                                        <td style="padding: 0;">
                                            <asp:TextBox ID="txtDescription_TH" runat="server" TextMode="MultiLine" Rows="3" class="form-control" Style="border: none;" OnTextChanged="txtDescription_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:TextBox ID="txtDescription_EN" runat="server" TextMode="MultiLine" Rows="3" class="form-control" Style="border: none;" OnTextChanged="txtDescription_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:TextBox ID="txtDescription_CH" runat="server" TextMode="MultiLine" Rows="3" class="form-control" Style="border: none;" OnTextChanged="txtDescription_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:TextBox ID="txtDescription_JP" runat="server" TextMode="MultiLine" Rows="3" class="form-control" Style="border: none;" OnTextChanged="txtDescription_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:TextBox ID="txtDescription_KR" runat="server" TextMode="MultiLine" Rows="3" class="form-control" Style="border: none;" OnTextChanged="txtDescription_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:TextBox ID="txtDescription_RS" runat="server" TextMode="MultiLine" Rows="3" class="form-control" Style="border: none;" OnTextChanged="txtDescription_TextChanged" AutoPostBack="true"></asp:TextBox>

                                        </td>
                                        <td data-title="Delete" id="Td2" runat="server" style="text-align: center">
                                             <input type="button" class="btn btn-danger btn-xs" value="Delete" id="btnSpecPreDelete" runat="server" />
                                             <asp:Button ID="btnSpecDelete" runat="server" CommandName="Delete" style="display:none" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

        <div class="form-group  col-sm-12">
            <label class="col-sm-2 control-label">
                 
            </label>
            <div class="col-sm-10">
                <div style="float: left ;">
                    <asp:Button ID="btnAddCapion_TH" runat="server" class="btn btn-info ripple" Text="Add Spec" />
                </div> 
            </div>
        </div>




        <asp:Panel ID="pnlModal" runat="server" Visible="false">
            <div class="sweet-overlay" tabindex="-1" style="opacity: 1.04; display: block;"></div>


            <div class="sweet-alert show-input showSweetAlert visible form-horizontal "  style="display: block;width: 800px;margin-top: -200px;left: 40%;">
              
                <h3>Add Spec</h3>

                 <div class="card-block">
                    <div class="form-horizontal">

                <div class="row ">
                    <div class="form-group col-sm-6">
                        <label class="col-sm-4 control-label" style ="padding-top:17px;">Thai <span style="color: red;">*</span></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtSpec_THAI" runat="server" class="form-control" Style ="height: 34px;font-size: 0.8125rem;"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group col-sm-6">
                        <label class="col-sm-4 control-label" style ="padding-top:17px;">English <span style="color: red;">*</span></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtSpec_ENGLISH" runat="server" class="form-control"  Style ="height: 34px;font-size: 0.8125rem;"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row ">
                    <div class="form-group col-sm-6">
                        <label class="col-sm-4 control-label" style ="padding-top:17px;">Cninese <span style="color: red;">*</span></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtSpec_CHINESE" runat="server" class="form-control"  Style ="height: 34px;font-size: 0.8125rem;"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group col-sm-6">
                        <label class="col-sm-4 control-label" style ="padding-top:17px;">Japanese <span style="color: red;">*</span></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtSpec_JAPANESE" runat="server" class="form-control"  Style ="height: 34px;font-size: 0.8125rem;"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row ">
                    <div class="form-group col-sm-6">
                        <label class="col-sm-4 control-label" style ="padding-top:17px;">Korean <span style="color: red;">*</span></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtSpec_KOREAN" runat="server" class="form-control"  Style ="height: 34px;font-size: 0.8125rem;"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group col-sm-6">
                        <label class="col-sm-4 control-label" style ="padding-top:17px;">Russian <span style="color: red;">*</span></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtSpec_RUSSIAN" runat="server" class="form-control"  Style ="height: 34px;font-size: 0.8125rem;"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row ">
                    <div class="form-group col-sm-6">
                        <label class="col-sm-4 control-label" style ="padding-top:17px;">Is Qualitative<span style="color: red;"></span></label>
                        <div class="col-sm-6">
                              <label class="col-sm-10 cb-checkbox cb-md">
                            <asp:CheckBox ID="chkIS_QUALITATIVE" runat="server" Checked="false" />
                        </label>
                        </div>
                    </div>
                    <div class="form-group col-sm-6">
                        
                    </div>
                </div>

 


                        </div>
                     </div>

                 <div class="sa-button-container">
                     <div style="float: right;">
                    <asp:Button ID="btnClose" runat="server" class="btn btn-block " Text="Cancel" />
                </div>
                <div style="float: right; margin-right: 5px;">
                    <asp:Button ID="btnOKSpec" runat="server" class="btn btn-info " Text="Save" />
                </div>
                </div>
            </div>
        </asp:Panel>
