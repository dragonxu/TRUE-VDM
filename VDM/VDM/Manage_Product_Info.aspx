<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Manage_Product_Info.aspx.vb" Inherits="VDM.Manage_Product_Info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="main-content  ">

        <div class="page-title" style="margin-top: -80px;">
            <div class="title">Management &gt; Manage Product Info</div>
        </div>

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
                                <th>DISPLAY NAME</th>
                                <th>Spec (n)</th>
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
                                        <td data-title="Logo"><asp:Image ID="img" runat="server" Width="60px" Height="60px"></asp:Image></td>
                                        <td data-title="PRODUCT CODE"><asp:Label ID="lblProductCode" runat="server"></asp:Label></td>
                                        <td data-title="DISPLAY NAME"><asp:Label ID="lblDisplayName" runat="server"></asp:Label></td>
                                        <td data-title="Spec (n)"><asp:Label ID="lblCountSpec" runat="server"></asp:Label></td>
                                        <td data-title="Price (Baht)"><asp:Label ID="lblPrice" runat="server"></asp:Label></td>
                                        <td data-title="Active"><asp:Image ID="ImageActive" runat="server" ></asp:Image></td>
                                        <td data-title="Edit">
                                            <button type="button" class="btn btn-success  ripple">Edit</button></td>
                                        <td data-title="Delete">
                                            <button type="button" class="btn btn-danger ripple">Delete</button></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
                <div class="row m-t">
                    <asp:LinkButton CssClass="btn btn-primary btn-icon loading-demo mr5 btn-shadow" ID="btnAdd" runat="server">
                      <i class="fa fa-plus-circle"></i>
                      <span>Add new product</span>
                    </asp:LinkButton>
                </div>
            </div>

        </asp:Panel>



        <asp:Panel ID="pnlEdit" runat="server" class="card bg-white">

            <div class="card-header">
                <asp:Label ID="lblEditMode" runat="server"></asp:Label> Product
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
                                <div class="col-sm-3">
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
                                <asp:DropDownList ID="ddlBrand" runat="server" class="form-control"></asp:DropDownList>
                            </div>


                        </div>


                        <div class="form-group col-sm-6">
                            <label class="col-sm-4 control-label">Require Receive Form <span style="color: red;">*</span></label>
                            <div class="col-sm-6">
                                <div class="col-sm-3">
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




                </div>
                <div class="row">
                    <h4>Specification</h4>
                    <div class="form-group col-sm-12">
                        <div class="card">
                            <div class="card-block p-a-0">
                                <div class="box-tab m-b-0">
                                    <ul class="nav nav-tabs">
                                        <li id="Tab_THAI" runat="server" class="active"><a href="#THAI" data-toggle="tab">THAI</a>
                                        </li>
                                        <li id="Tab_ENGLISH" runat="server" class="active"><a href="#ENGLISH" data-toggle="tab">ENGLISH</a>
                                        </li>
                                        <li id="Tab_CHINESE" runat="server" class="active"><a href="#CHINESE" data-toggle="tab">CHINESE</a>
                                        </li>
                                        <li id="Tab_JAPANESE" runat="server" class="active"><a href="#JAPANESE" data-toggle="tab">JAPANESE</a>
                                        </li>
                                        <li id="Tab_KOREAN" runat="server" class="active"><a href="#KOREAN" data-toggle="tab">KOREAN</a>
                                        </li>
                                        <li id="Tab_RUSSIAN" runat="server" class="active"><a href="#RUSSIAN" data-toggle="tab">RUSSIAN</a>
                                        </li>
                                    </ul>
                                    <div class="tab-content">
                                        <div class="tab-pane active in" id="THAI">
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
                                                        <asp:TextBox ID="txtDescription_TH" runat="server" TextMode="MultiLine" Rows="4" class="form-control"></asp:TextBox>
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
                                                        <asp:Button ID="btnUpdateLogo_TH" runat="server" Style="display: none;" />
                                                    </div>
                                                    <div class="help-block m-b">Support only image jpeg gif png, file dimension must be 1:1 (Square) and file size must not larger than 4MB</div>
                                                </div>
                                            </div>
                                            <div class="row ">
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label">
                                                        Caption  <span style="color: red;">*</span>
                                                    </label>
                                                    <div class="col-sm-10">
                                                        <div style="float: right;">
                                                            <asp:Button ID="btnAddCapion_TH" runat="server" class="btn btn-info ripple" Text="Add Caption" />
                                                        </div>
                                                        <div style="float: right; margin-right: 5px;">
                                                            <asp:Button ID="btnAddSpec_TH" runat="server" class="btn btn-info ripple" Text="Add Spec" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label"></label>
                                                    <div class="col-sm-10">
                                                        <div class="no-more-tables">
                                                            <table class="table table-bordered  m-b-0">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Spec</th>
                                                                        <th>Description</th>
                                                                        <th class="col-md-2">Action</th>

                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="rptCaptionList_TH" runat="server">
                                                                        <ItemTemplate>

                                                                            <tr>
                                                                                <td>
                                                                                    <asp:DropDownList class="form-control" ID="ddlSpec" runat="server"></asp:DropDownList></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCaptionDescription" runat="server" TextMode="MultiLine" Rows="3" class="form-control"></asp:TextBox>
                                                                                </td>
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
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="tab-pane active in" id="ENGLISH">
                                            <div class="row ">
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label">Display Name <span style="color: red;">*</span></label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtDisplayName_EN" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label">Description <span style="color: red;">*</span></label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtDescription_EN" runat="server" TextMode="MultiLine" Rows="4" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="card-title col-sm-2 control-label" style="text-align: left;">Image  <span style="color: red;">*</span></label>
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
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label">
                                                        Caption  <span style="color: red;">*</span>
                                                    </label>
                                                    <div class="col-sm-10">
                                                        <div style="float: right;">
                                                            <asp:Button ID="btnAddCapion_EN" runat="server" class="btn btn-info ripple" Text="Add Caption" />
                                                        </div>
                                                        <div style="float: right; margin-right: 5px;">
                                                            <asp:Button ID="btnAddSpec_EN" runat="server" class="btn btn-info ripple" Text="Add Spec" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label"></label>
                                                    <div class="col-sm-10">
                                                        <div class="no-more-tables">
                                                            <table class="table table-bordered  m-b-0">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Spec</th>
                                                                        <th>Description</th>
                                                                        <th class="col-md-2">Action</th>

                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="rptCaptionList_EN" runat="server">
                                                                        <ItemTemplate>

                                                                            <tr>
                                                                                <td>
                                                                                    <asp:DropDownList class="form-control" ID="ddlSpec" runat="server"></asp:DropDownList></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCaptionDescription" runat="server" TextMode="MultiLine" Rows="3" class="form-control"></asp:TextBox>
                                                                                </td>
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
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="tab-pane active in" id="CHINESE">
                                            <div class="row ">
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label">Display Name <span style="color: red;">*</span></label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtDisplayName_CN" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label">Description <span style="color: red;">*</span></label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtDescription_CN" runat="server" TextMode="MultiLine" Rows="4" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="card-title col-sm-2 control-label" style="text-align: left;">Image  <span style="color: red;">*</span></label>
                                                <div class="col-sm-10">
                                                    <div>
                                                        <asp:Image ID="imgIcon_CN" runat="server" Style="cursor: pointer; max-width: 50%;" ImageUrl="~/images/BlankIcon.png" />
                                                    </div>
                                                    <div>
                                                        <asp:FileUpload ID="ful_CN" runat="server" />
                                                        <asp:Button ID="btnUpdateLogo_CN" runat="server" Style="display: none;" />
                                                    </div>
                                                    <div class="help-block m-b">Support only image jpeg gif png, file dimension must be 1:1 (Square) and file size must not larger than 4MB</div>
                                                </div>
                                            </div>
                                            <div class="row ">
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label">
                                                        Caption  <span style="color: red;">*</span>
                                                    </label>
                                                    <div class="col-sm-10">
                                                        <div style="float: right;">
                                                            <asp:Button ID="btnAddCapion_CN" runat="server" class="btn btn-info ripple" Text="Add Caption" />
                                                        </div>
                                                        <div style="float: right; margin-right: 5px;">
                                                            <asp:Button ID="btnAddSpec_CN" runat="server" class="btn btn-info ripple" Text="Add Spec" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label"></label>
                                                    <div class="col-sm-10">
                                                        <div class="no-more-tables">
                                                            <table class="table table-bordered  m-b-0">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Spec</th>
                                                                        <th>Description</th>
                                                                        <th class="col-md-2">Action</th>

                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="rptCaptionList_CN" runat="server">
                                                                        <ItemTemplate>

                                                                            <tr>
                                                                                <td>
                                                                                    <asp:DropDownList class="form-control" ID="ddlSpec" runat="server"></asp:DropDownList></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCaptionDescription" runat="server" TextMode="MultiLine" Rows="3" class="form-control"></asp:TextBox>
                                                                                </td>
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
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane active in" id="JAPANESE">
                                            <div class="row ">
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label">Display Name <span style="color: red;">*</span></label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtDisplayName_JP" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label">Description <span style="color: red;">*</span></label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtDescription_JP" runat="server" TextMode="MultiLine" Rows="4" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="card-title col-sm-2 control-label" style="text-align: left;">Image  <span style="color: red;">*</span></label>
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
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label">
                                                        Caption  <span style="color: red;">*</span>
                                                    </label>
                                                    <div class="col-sm-10">
                                                        <div style="float: right;">
                                                            <asp:Button ID="btnAddCapion_JP" runat="server" class="btn btn-info ripple" Text="Add Caption" />
                                                        </div>
                                                        <div style="float: right; margin-right: 5px;">
                                                            <asp:Button ID="btnAddSpec_JP" runat="server" class="btn btn-info ripple" Text="Add Spec" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label"></label>
                                                    <div class="col-sm-10">
                                                        <div class="no-more-tables">
                                                            <table class="table table-bordered  m-b-0">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Spec</th>
                                                                        <th>Description</th>
                                                                        <th class="col-md-2">Action</th>

                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="rptCaptionList_JP" runat="server">
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:DropDownList class="form-control" ID="ddlSpec" runat="server"></asp:DropDownList></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCaptionDescription" runat="server" TextMode="MultiLine" Rows="3" class="form-control"></asp:TextBox>
                                                                                </td>
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
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="tab-pane active in" id="KOREAN">
                                            <div class="row ">
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label">Display Name <span style="color: red;">*</span></label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtDisplayName_KR" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label">Description <span style="color: red;">*</span></label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtDescription_KR" runat="server" TextMode="MultiLine" Rows="4" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="card-title col-sm-2 control-label" style="text-align: left;">Image  <span style="color: red;">*</span></label>
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
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label">
                                                        Caption  <span style="color: red;">*</span>
                                                    </label>
                                                    <div class="col-sm-10">
                                                        <div style="float: right;">
                                                            <asp:Button ID="btnAddCapion_KR" runat="server" class="btn btn-info ripple" Text="Add Caption" />
                                                        </div>
                                                        <div style="float: right; margin-right: 5px;">
                                                            <asp:Button ID="btnAddSpec_KR" runat="server" class="btn btn-info ripple" Text="Add Spec" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label"></label>
                                                    <div class="col-sm-10">
                                                        <div class="no-more-tables">
                                                            <table class="table table-bordered  m-b-0">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Spec</th>
                                                                        <th>Description</th>
                                                                        <th class="col-md-2">Action</th>

                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="rptCaptionList_KR" runat="server">
                                                                        <ItemTemplate>

                                                                            <tr>
                                                                                <td>
                                                                                    <asp:DropDownList class="form-control" ID="ddlSpec" runat="server"></asp:DropDownList></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCaptionDescription" runat="server" TextMode="MultiLine" Rows="3" class="form-control"></asp:TextBox>
                                                                                </td>
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
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="tab-pane active in" id="RUSSIAN">
                                            <div class="row ">
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label">Display Name <span style="color: red;">*</span></label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtDisplayName_RS" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label">Description <span style="color: red;">*</span></label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtDescription_RS" runat="server" TextMode="MultiLine" Rows="4" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="card-title col-sm-2 control-label" style="text-align: left;">Image  <span style="color: red;">*</span></label>
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
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label">
                                                        Caption  <span style="color: red;">*</span>
                                                    </label>
                                                    <div class="col-sm-10">
                                                        <div style="float: right;">
                                                            <asp:Button ID="btnAddCapion_RS" runat="server" class="btn btn-info ripple" Text="Add Caption" />
                                                        </div>
                                                        <div style="float: right; margin-right: 5px;">
                                                            <asp:Button ID="btnAddSpec_RS" runat="server" class="btn btn-info ripple" Text="Add Spec" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label"></label>
                                                    <div class="col-sm-10">
                                                        <div class="no-more-tables">
                                                            <table class="table table-bordered  m-b-0">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Spec</th>
                                                                        <th>Description</th>
                                                                        <th class="col-md-2">Action</th>

                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="rptCaptionList_RS" runat="server">
                                                                        <ItemTemplate>

                                                                            <tr>
                                                                                <td>
                                                                                    <asp:DropDownList class="form-control" ID="ddlSpec" runat="server"></asp:DropDownList></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCaptionDescription" runat="server" TextMode="MultiLine" Rows="3" class="form-control"></asp:TextBox>
                                                                                </td>
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
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="form-group" style="margin-left:-5px;">
                            <h4 class="card-title col-sm-2 control-label" style="text-align:left;">Active Status </h4>  
                        
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









    </div>






</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContainer" runat="server">
</asp:Content>
