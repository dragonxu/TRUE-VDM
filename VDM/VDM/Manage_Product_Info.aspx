<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Manage_Product_Info.aspx.vb" Inherits="VDM.Manage_Product_Info" %>

<%@ Register Src="~/UC_Product_Spec.ascx" TagPrefix="uc1" TagName="UC_Product_Spec" %>
<%@ Register Src="~/PageNavigation.ascx" TagName="PageNavigation" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



        <div class="page-title" style="">
        <div class="title">Management &gt; Manage Product Info</div>
    </div>
    <asp:UpdatePanel ID="udpList" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlList" runat="server" class="card bg-white">
                <div class="card-header">
                    Found :
                    <asp:Label ID="lblTotalList" runat="server"></asp:Label>
                    Product(s)
                </div>
                <div class="card-block">
                    <div class="no-more-tables">
                        <table class="table table-bordered  m-b-0">
                            <thead>
                                <tr>
                                    <th>Logo</th>
                                    <th>PRODUCT CODE</th>
                                    <th>BRAND</th>
                                    <th>MODEL</th>
                                    <th>DISPLAY NAME</th>
                                    <th>Price (Baht)</th>
                                    <th>Active</th>
                                    <th id="ColEdit" runat="server">Edit</th>
                                    <th id="ColDelete" runat="server">Delete</th>

                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptList" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td data-title="Logo">
                                                <asp:Image ID="img" runat="server" Width="60px" Height="60px"></asp:Image></td>
                                            <td data-title="PRODUCT CODE">
                                                <asp:Label ID="lblProductCode" runat="server"></asp:Label></td>
                                            <td data-title="RRAND">
                                                <asp:Label ID="lblBrand" runat="server"></asp:Label></td>
                                            <td data-title="MODEL">
                                                <asp:Label ID="lblModel" runat="server"></asp:Label></td>
                                            <td data-title="DISPLAY NAME">
                                                <asp:Label ID="lblDisplayName" runat="server"></asp:Label></td>

                                            <td data-title="Price (Baht)" style ="text-align :right ;">
                                                <asp:Label ID="lblPrice" runat="server"></asp:Label></td>
                                            <td data-title="Status">

                                                <asp:Panel ID="pnlChk" runat="server" Enabled="false" Style="text-align: center;">
                                                    <label class="col-sm-10 cb-checkbox cb-md" aria-disabled="false">
                                                        <asp:CheckBox ID="chkAvailable" runat="server" />
                                                    </label>
                                                </asp:Panel>
                                                <asp:Button ID="btnToggle" runat="server" CommandName="ToggleStatus" Style="display: none" Text="Toggle" />
                                            </td>
                                            <td data-title="Edit" id="Td1" runat="server">
                                                <asp:Button CssClass="btn btn-success" ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" /></td>
                                            <td data-title="Delete" id="Td2" runat="server">
                                                <input type="button" class="btn btn-danger" value="Delete" id="btnPreDelete" runat="server" />
                                                <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Style="display: none" />

                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                    <uc1:PageNavigation ID="Pager" runat="server" />
                    <div class="row m-t">
                        <asp:LinkButton CssClass="btn btn-primary btn-icon loading-demo mr5 btn-shadow" ID="btnAdd" runat="server">
                      <i class="fa fa-plus-circle"></i>
                      <span>Add new product</span>
                        </asp:LinkButton>
                    </div>
                </div>

            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="udpEdit" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpdateLogo" />
        </Triggers>

        <ContentTemplate>
            <asp:Panel ID="pnlEdit" runat="server" class="card bg-white">
                <asp:Button ID="btnUpdateLogo" runat="server" Style="display: none;" />
                <div class="card-header">
                    <asp:Label ID="lblEditMode" runat="server"></asp:Label>
                    Product
                </div>
                <div class="card-block">
                    <div class="form-horizontal">
                        <h4>Product Info</h4>
                        <div class="row ">
                            <div class="form-group col-sm-6">
                                <label class="col-sm-4 control-label">Product Code <span style="color: red;">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtCode" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>



                            <div class="form-group col-sm-6">
                                <label class="col-sm-4 control-label">Is Serial <span style="color: red;">*</span></label>
                                <div class="col-sm-6">
                                    <div class="col-sm-6">
                                        <div class="radio">
                                            <label>
                                                <asp:RadioButton ID="rdIsSerial_Yes" runat="server" GroupName="IsSerial" Text="Yes" />
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="radio">
                                            <label>
                                                <asp:RadioButton ID="rdIsSerial_No" runat="server" GroupName="IsSerial" Text="No" />
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row ">
                            <div class="form-group col-sm-6">
                                <label class="col-sm-4 control-label">Brand <span style="color: red;">*</span></label>


                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlBrand" runat="server" class="form-control" Width="100%"></asp:DropDownList>
                                </div>


                            </div>

                            <div class="form-group col-sm-6">
                                <label class="col-sm-4 control-label">Model <span style="color: red;">*</span></label>


                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtModel" runat="server" class="form-control"></asp:TextBox>
                                </div>


                            </div>
                            <div class="form-group col-sm-6" id="divRequire" runat="server" visible="false">
                                <label class="col-sm-4 control-label">Require Receive Form <span style="color: red;">*</span></label>
                                <div class="col-sm-6">
                                    <div class="col-sm-6">
                                        <div class="radio">
                                            <label>
                                                <asp:RadioButton ID="rdRequireReceive_Yes" runat="server" GroupName="RequireReceive" Text="Yes" /></label>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="radio">
                                            <label>
                                                <asp:RadioButton ID="rdRequireReceive_No" runat="server" GroupName="RequireReceive" Text="No" /></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row ">
                            <div class="form-group col-sm-6">
                                <label class="col-sm-4 control-label">Price <span style="color: red;">*</span></label>
                                <div class="col-sm-6">
                                    <div class="input-group m-b">

                                        <asp:TextBox ID="txtPrice" runat="server" class="form-control"></asp:TextBox>
                                        <span class="input-group-addon" style="font-size: small;">Baht</span>

                                    </div>
                                </div>
                            </div>


                            <div class="form-group col-sm-6">
                                <label class="col-sm-4 control-label"></label>
                                <div class="col-sm-6">
                                </div>
                            </div>
                        </div>


                        <div class="row ">
                            <asp:Button CssClass="btn btn-info" ID="btnGetProductTSM" runat="server" Text="Input Product Code and Click button >> Get Product info TSM" Width="100%" />
                        </div>

                    </div>

                    <div class="row">
                        <h4>Specification</h4>
                        <div class="form-group col-sm-12">
                            <div class="card">
                                <div class="card-block p-a-0">
                                    <div class="box-tab m-b-0">
                                        <ul class="nav nav-tabs">

                                            <li id="Tab_THAI" runat="server" class="active">
                                                <asp:LinkButton ID="lnkTab_THAI" runat="server" Text="THAI"></asp:LinkButton>
                                            </li>
                                            <li id="Tab_ENGLISH" runat="server" class="">
                                                <asp:LinkButton ID="lnkTab_ENGLISH" runat="server" Text="ENGLISH"></asp:LinkButton>
                                            </li>
                                            <li id="Tab_CHINESE" runat="server" class="">
                                                <asp:LinkButton ID="lnkTab_CHINESE" runat="server" Text="CHINESE"></asp:LinkButton>
                                            </li>
                                            <li id="Tab_JAPANESE" runat="server" class="">
                                                <asp:LinkButton ID="lnkTab_JAPANESE" runat="server" Text="JAPANESE"></asp:LinkButton>
                                            </li>
                                            <li id="Tab_KOREAN" runat="server" class="">
                                                <asp:LinkButton ID="lnkTab_KOREAN" runat="server" Text="KOREAN"></asp:LinkButton>
                                            </li>
                                            <li id="Tab_RUSSIAN" runat="server" class="">
                                                <asp:LinkButton ID="lnkTab_RUSSIAN" runat="server" Text="RUSSIAN"></asp:LinkButton>
                                            </li>

                                        </ul>
                                        <div class="tab-content">


                                            <asp:Panel ID="pnlTHAI" runat="server">
                                                <div class="tab-pane active in" id="THAI">
                                                    <h4>THAI</h4>
                                                    <div class="row ">
                                                        <div class="form-group  col-sm-12">
                                                            <label class="col-sm-2 control-label">Display Name <span style="color: red;">*</span></label>
                                                            <div class="col-sm-10">
                                                                <asp:TextBox ID="txtDisplayName_TH" runat="server" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group  col-sm-12">
                                                            <label class="col-sm-2 control-label">Description <span style="color: red;">*</span></label>
                                                            <div class="col-sm-10">
                                                                <asp:TextBox ID="txtDescription_TH" runat="server" TextMode="MultiLine" Rows="10" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="card-title col-sm-2 control-label" style="text-align: left;">Image  <span style="color: red;">*</span></label>

                                                        <%--<div class="col-sm-10">

                                                <div>
                                                    <img id="img" style="cursor: pointer; max-width: 50%;">
                                                </div>
                                                <div>

                                                    <asp:LinkButton ID="lnkUpload" runat="server" class="btn btn-success fileinput-button">
                                                              <i class="icon-plus"></i>
                                                            <span>Add Image...</span>

                                                    </asp:LinkButton>

                                                    <asp:FileUpload ID="FileUpload1" runat="server" Style="display: none;" />


                                                </div>

                                                <div class="help-block m-b">Desc : size</div>
                                            </div>--%>

                                                        <div class="col-sm-10">
                                                            <div>
                                                                <asp:Image ID="imgIcon_TH" runat="server" Style="cursor: pointer; max-width: 50%;" ImageUrl="~/images/BlankIcon.png" />
                                                            </div>
                                                            <div>
                                                                <asp:FileUpload ID="ful_TH" runat="server" />

                                                            </div>
                                                            <div class="help-block m-b">Support only image jpeg gif png, file dimension must be 1:1 (Square) and file size must not larger than 4MB</div>
                                                        </div>
                                                    </div>
                                                    <div class="row ">

                                                        <uc1:UC_Product_Spec runat="server" ID="UC_Product_Spec_TH" />

                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlENGLISH" runat="server">
                                                <div class="tab-pane" id="ENGLISH">
                                                    <h4>ENGLISH</h4>
                                                    <div class="row ">
                                                        <div class="form-group  col-sm-12">
                                                            <label class="col-sm-2 control-label">Display Name </label>
                                                            <div class="col-sm-10">
                                                                <asp:TextBox ID="txtDisplayName_EN" runat="server" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group  col-sm-12">
                                                            <label class="col-sm-2 control-label">Description </label>
                                                            <div class="col-sm-10">
                                                                <asp:TextBox ID="txtDescription_EN" runat="server" TextMode="MultiLine" Rows="10" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="card-title col-sm-2 control-label" style="text-align: left;">Image  </label>
                                                        <div class="col-sm-10">
                                                            <div>
                                                                <asp:Image ID="imgIcon_EN" runat="server" Style="cursor: pointer; max-width: 50%;" ImageUrl="~/images/BlankIcon.png" />
                                                            </div>
                                                            <div>
                                                                <asp:FileUpload ID="ful_EN" runat="server" />
                                                                <asp:Button ID="btnUpdateLogo_EN" runat="server" Style="display: none;" />
                                                            </div>
                                                            <div class="help-block m-b">Support only image jpeg gif png, file dimension must be 1:1 (Square) and file size must not larger than 4MB</div>
                                                        </div>
                                                    </div>
                                                    <div class="row ">
                                                        <uc1:UC_Product_Spec runat="server" ID="UC_Product_Spec_EN" />
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlCHINESE" runat="server">

                                                <div class="tab-pane" id="CHINESE">
                                                    <h4>CHINESE</h4>
                                                    <div class="row ">
                                                        <div class="form-group  col-sm-12">
                                                            <label class="col-sm-2 control-label">Display Name </label>
                                                            <div class="col-sm-10">
                                                                <asp:TextBox ID="txtDisplayName_CH" runat="server" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group  col-sm-12">
                                                            <label class="col-sm-2 control-label">Description </label>
                                                            <div class="col-sm-10">
                                                                <asp:TextBox ID="txtDescription_CH" runat="server" TextMode="MultiLine" Rows="10" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="card-title col-sm-2 control-label" style="text-align: left;">Image  </label>
                                                        <div class="col-sm-10">
                                                            <div>
                                                                <asp:Image ID="imgIcon_CH" runat="server" Style="cursor: pointer; max-width: 50%;" ImageUrl="~/images/BlankIcon.png" />
                                                            </div>
                                                            <div>
                                                                <asp:FileUpload ID="ful_CH" runat="server" />
                                                                <asp:Button ID="btnUpdateLogo_CH" runat="server" Style="display: none;" />
                                                            </div>
                                                            <div class="help-block m-b">Support only image jpeg gif png, file dimension must be 1:1 (Square) and file size must not larger than 4MB</div>
                                                        </div>
                                                    </div>
                                                    <div class="row ">
                                                        <uc1:UC_Product_Spec runat="server" ID="UC_Product_Spec_CH" />
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlJAPANESE" runat="server">
                                                <div class="tab-pane" id="JAPANESE">
                                                    <h4>JAPANESE</h4>
                                                    <div class="row ">
                                                        <div class="form-group  col-sm-12">
                                                            <label class="col-sm-2 control-label">Display Name </label>
                                                            <div class="col-sm-10">
                                                                <asp:TextBox ID="txtDisplayName_JP" runat="server" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group  col-sm-12">
                                                            <label class="col-sm-2 control-label">Description </label>
                                                            <div class="col-sm-10">
                                                                <asp:TextBox ID="txtDescription_JP" runat="server" TextMode="MultiLine" Rows="10" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="card-title col-sm-2 control-label" style="text-align: left;">Image  </label>
                                                        <div class="col-sm-10">
                                                            <div>
                                                                <asp:Image ID="imgIcon_JP" runat="server" Style="cursor: pointer; max-width: 50%;" ImageUrl="~/images/BlankIcon.png" />
                                                            </div>
                                                            <div>
                                                                <asp:FileUpload ID="ful_JP" runat="server" />
                                                                <asp:Button ID="btnUpdateLogo_JP" runat="server" Style="display: none;" />
                                                            </div>
                                                            <div class="help-block m-b">Support only image jpeg gif png, file dimension must be 1:1 (Square) and file size must not larger than 4MB</div>
                                                        </div>
                                                    </div>
                                                    <div class="row ">
                                                        <uc1:UC_Product_Spec runat="server" ID="UC_Product_Spec_JP" />
                                                    </div>

                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlKOREAN" runat="server">
                                                <div class="tab-pane" id="KOREAN">
                                                    <h4>KOREAN</h4>
                                                    <div class="row ">
                                                        <div class="form-group  col-sm-12">
                                                            <label class="col-sm-2 control-label">Display Name </label>
                                                            <div class="col-sm-10">
                                                                <asp:TextBox ID="txtDisplayName_KR" runat="server" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group  col-sm-12">
                                                            <label class="col-sm-2 control-label">Description </label>
                                                            <div class="col-sm-10">
                                                                <asp:TextBox ID="txtDescription_KR" runat="server" TextMode="MultiLine" Rows="10" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="card-title col-sm-2 control-label" style="text-align: left;">Image  </label>
                                                        <div class="col-sm-10">
                                                            <div>
                                                                <asp:Image ID="imgIcon_KR" runat="server" Style="cursor: pointer; max-width: 50%;" ImageUrl="~/images/BlankIcon.png" />
                                                            </div>
                                                            <div>
                                                                <asp:FileUpload ID="ful_KR" runat="server" />
                                                                <asp:Button ID="btnUpdateLogo_KR" runat="server" Style="display: none;" />
                                                            </div>
                                                            <div class="help-block m-b">Support only image jpeg gif png, file dimension must be 1:1 (Square) and file size must not larger than 4MB</div>
                                                        </div>
                                                    </div>
                                                    <div class="row ">
                                                        <uc1:UC_Product_Spec runat="server" ID="UC_Product_Spec_KR" />
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlRUSSIAN" runat="server">
                                                <div class="tab-pane" id="RUSSIAN">
                                                    <h4>RUSSIAN</h4>
                                                    <div class="row ">
                                                        <div class="form-group  col-sm-12">
                                                            <label class="col-sm-2 control-label">Display Name </label>
                                                            <div class="col-sm-10">
                                                                <asp:TextBox ID="txtDisplayName_RS" runat="server" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group  col-sm-12">
                                                            <label class="col-sm-2 control-label">Description </label>
                                                            <div class="col-sm-10">
                                                                <asp:TextBox ID="txtDescription_RS" runat="server" TextMode="MultiLine" Rows="10" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="card-title col-sm-2 control-label" style="text-align: left;">Image  </label>
                                                        <div class="col-sm-10">
                                                            <div>
                                                                <asp:Image ID="imgIcon_RS" runat="server" Style="cursor: pointer; max-width: 50%;" ImageUrl="~/images/BlankIcon.png" />
                                                            </div>
                                                            <div>
                                                                <asp:FileUpload ID="ful_RS" runat="server" />
                                                                <asp:Button ID="btnUpdateLogo_RS" runat="server" Style="display: none;" />
                                                            </div>
                                                            <div class="help-block m-b">Support only image jpeg gif png, file dimension must be 1:1 (Square) and file size must not larger than 4MB</div>
                                                        </div>
                                                    </div>
                                                    <div class="row ">
                                                        <uc1:UC_Product_Spec runat="server" ID="UC_Product_Spec_RS" />
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="form-group" style="margin-left: -5px;">
                        <h4 class="card-title col-sm-2 control-label" style="text-align: left;">Active Status </h4>

                        <label class="col-sm-10 cb-checkbox cb-md">
                            <asp:CheckBox ID="chkActive" runat="server" Checked="true" />
                        </label>
                    </div>
                    <div class="form-group" style="text-align: right">
                        <asp:LinkButton CssClass="btn btn-success btn-icon loading-demo mr5 btn-shadow" ID="btnSave" runat="server">
                              <i class="fa fa-save"></i>
                              <span>Save</span>
                        </asp:LinkButton>

                        <asp:LinkButton CssClass="btn btn-warning btn-icon loading-demo mr5 btn-shadow" ID="btnBack" runat="server">
                              <i class="fa fa-rotate-left"></i>
                              <span>Cancel</span>
                        </asp:LinkButton>
                    </div>
                </div>



            </asp:Panel>


        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContainer" runat="server">
</asp:Content>
