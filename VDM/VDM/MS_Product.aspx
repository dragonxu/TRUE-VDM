<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="MS_Product.aspx.vb" Inherits="VDM.MS_Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



    <div class="main-content checkbo">



        <div class="page-title" style="margin-top: -80px;">
            <div class="title">Setting &gt; Product</div>
            <div class="sub-title">......</div>
        </div>
        <div id="MainContent_udpList">

            <div id="MainContent_pnlList" class="card bg-white">

                <div class="card-header">
                    count xx
                </div>
                <div class="card-block">
                    <div class="no-more-tables">
                        <table class="table table-bordered table-striped m-b-0">
                            <thead>
                                <tr>
                                    <th>Image</th>
                                    <th>PRODUCT CODE</th>
                                    <th>DISPLAY NAME</th>
                                    <th>Spec (n)</th>
                                    <th>Price (Baht)</th>
                                    <th>Active</th>
                                    <th>Edit</th>

                                    <th>Delete</th>

                                </tr>
                            </thead>
                            <tbody>

                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td> <button type="button" class="btn btn-success  ripple">Edit</button></td>
                                    <td><button type="button" class="btn btn-danger ripple">Delete</button></td>
                                </tr>



                            </tbody>
                        </table>
                    </div>
                    <div class="row m-t">
                    </div>
                </div>

            </div>

        </div>

        <div id="MainContent_pnlEdit" class="card bg-white">

            <div class="card-header">
                Edit Production
            </div>
            <div class="card-block">
                <div class="form-horizontal">
                    <%-- <h5>Specification</h5>--%>
                    <div class="row ">
                        <div class="form-group col-sm-6">
                            <label class="col-sm-4 control-label">Product Code <span style ="color :red;">*</span></label>
                            <div class="col-sm-6">
                                <input type="text" class="form-control   ">
                            </div>
                        </div>



                        <div class="form-group col-sm-6">
                            <label class="col-sm-4 control-label">Is Serial <span style ="color :red;">*</span></label>
                            <div class="col-sm-6">
                                <div class="col-sm-3">
                                    <div class="radio">
                                        <label>
                                            <input type="radio">Yes</label>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="radio">
                                        <label>
                                            <input type="radio">No</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row ">
                        <div class="form-group col-sm-6">
                            <label class="col-sm-4 control-label">Is Sim <span style ="color :red;">*</span></label>


                            <div class="col-sm-6">
                                <div class="col-sm-3">
                                    <div class="radio">
                                        <label>
                                            <input type="radio">Yes</label>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="radio">
                                        <label>
                                            <input type="radio">No</label>
                                    </div>
                                </div>
                            </div>


                        </div>


                        <div class="form-group col-sm-6">
                            <label class="col-sm-4 control-label">Requiire Receive Form <span style ="color :red;">*</span></label>
                            <div class="col-sm-6">
                                <div class="col-sm-3">
                                    <div class="radio">
                                        <label>
                                            <input type="radio">Yes</label>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="radio">
                                        <label>
                                            <input type="radio">No</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row ">
                        <div class="form-group col-sm-6">
                            <label class="col-sm-4 control-label">Price <span style ="color :red;">*</span></label>
                            <div class="col-sm-6">
                                <div class="input-group m-b">

                                    <input type="text" class="form-control">
                                    <span class="input-group-addon">Baht</span>

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
                    <h5>Specification</h5>
                    <div class="form-group col-sm-12">
                        <div class="card">
                            <div class="card-block p-a-0">
                                <div class="box-tab m-b-0">
                                    <ul class="nav nav-tabs">
                                        <li class="active"><a href="#home" data-toggle="tab">THAI</a>
                                        </li>
                                        <li><a href="#profile" data-toggle="tab">ENGLISH</a>
                                        </li>
                                        <li><a href="#about" data-toggle="tab">CHINESE</a>
                                        </li>
                                        <li><a href="#contacts" data-toggle="tab">JAPANESE</a>
                                        </li>
                                        <li><a href="#contacts" data-toggle="tab">KOREAN</a>
                                        </li>
                                        <li><a href="#contacts" data-toggle="tab">RUSSIAN</a>
                                        </li>
                                    </ul>
                                    <div class="tab-content">
                                        <div class="tab-pane active in" id="home">
                                            <div class="row ">

                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label">Display Name <span style ="color :red;">*</span></label>
                                                    <div class="col-sm-10">
                                                        <input type="text" class="form-control   ">
                                                    </div>
                                                </div>


                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label">Description <span style ="color :red;">*</span></label>
                                                    <div class="col-sm-10">
                                                        <textarea class="form-control" rows="3"></textarea>
                                                    </div>
                                                </div>



                                            </div>

                                            <div class="form-group">
                                                <label class="card-title col-sm-2 control-label" style="text-align: left;">Image  <span style ="color :red;">*</span></label>

                                                <div class="col-sm-10">

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
                                                </div>
                                            </div>

                                            <div class="row ">
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label">
                                                        Caption  <span style ="color :red;">*</span> 
                                                    </label>
                                                    <div class="col-sm-10">
                                                        <div style="float: right;">
                                                            <button class="btn btn-info ripple">Add Caption</button>
                                                        </div>
                                                        <div style="float: right; margin-right: 5px;">
                                                            <button class="btn btn-info ripple">Add Spec</button>
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

                                                                    <tr>
                                                                        <td>
                                                                            <asp:DropDownList class="form-control" ID="ddlSpec" runat="server"></asp:DropDownList></td>
                                                                        <td>
                                                                            <textarea class="form-control" rows="3"></textarea></td>
                                                                        <td>
                                                                            <button type="button" class="btn btn-danger ripple">Delete</button></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:DropDownList class="form-control" ID="DropDownList1" runat="server"></asp:DropDownList></td>
                                                                        <td>
                                                                            <textarea class="form-control" rows="3"></textarea></td>
                                                                        <td>
                                                                            <button type="button" class="btn btn-danger ripple">Delete</button></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:DropDownList class="form-control" ID="DropDownList2" runat="server"></asp:DropDownList></td>
                                                                        <td>
                                                                            <textarea class="form-control" rows="3"></textarea></td>
                                                                        <td>
                                                                            <button type="button" class="btn btn-danger ripple">Delete</button></td>
                                                                    </tr>

                                                                </tbody>
                                                            </table>
                                                        </div>




                                                    </div>
                                                </div>

                                                <div class="form-group col-sm-12">
                                                    <label class="col-sm-2 control-label">Status Active</label>


                                                    <div class="col-sm-10">

                                                        <asp:CheckBox ID="isActive" runat="server" />

                                                    </div>


                                                </div>




                                            </div>


                                        </div>
                                        <div class="tab-pane" id="profile">

                                            <div class="row ">

                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label">Display Name</label>
                                                    <div class="col-sm-10">
                                                        <input type="text" class="form-control   ">
                                                    </div>
                                                </div>


                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label">Description</label>
                                                    <div class="col-sm-10">
                                                        <textarea class="form-control" rows="3"></textarea>
                                                    </div>
                                                </div>



                                            </div>

                                            <div class="form-group">
                                                <label class="card-title col-sm-2 control-label" style="text-align: left;">Image</label>

                                                <div class="col-sm-10">

                                                    <div>
                                                        <img id="img" style="cursor: pointer; max-width: 50%;">
                                                    </div>
                                                    <div>

                                                        <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-success fileinput-button">
                                                              <i class="icon-plus"></i>
                                                            <span>Add Image...</span>

                                                        </asp:LinkButton>

                                                        <asp:FileUpload ID="FileUpload2" runat="server" Style="display: none;" />


                                                    </div>

                                                    <div class="help-block m-b">Desc : size</div>
                                                </div>
                                            </div>

                                            <div class="row ">
                                                <div class="form-group  col-sm-12">
                                                    <label class="col-sm-2 control-label">
                                                        Caption 
                                                    </label>
                                                    <div class="col-sm-10">
                                                        <div style="float: right;">
                                                            <button class="btn btn-info ripple">Add Caption</button>
                                                        </div>
                                                        <div style="float: right; margin-right: 5px;">
                                                            <button class="btn btn-info ripple">Add Spec</button>
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

                                                                    <tr>
                                                                        <td>
                                                                            <asp:DropDownList class="form-control" ID="DropDownList3" runat="server"></asp:DropDownList></td>
                                                                        <td>
                                                                            <textarea class="form-control" rows="3"></textarea></td>
                                                                        <td>
                                                                            <button type="button" class="btn btn-danger ripple">Delete</button></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:DropDownList class="form-control" ID="DropDownList4" runat="server"></asp:DropDownList></td>
                                                                        <td>
                                                                            <textarea class="form-control" rows="3"></textarea></td>
                                                                        <td>
                                                                            <button type="button" class="btn btn-danger ripple">Delete</button></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:DropDownList class="form-control" ID="DropDownList5" runat="server"></asp:DropDownList></td>
                                                                        <td>
                                                                            <textarea class="form-control" rows="3"></textarea></td>
                                                                        <td>
                                                                            <button type="button" class="btn btn-danger ripple">Delete</button></td>
                                                                    </tr>

                                                                </tbody>
                                                            </table>
                                                        </div>




                                                    </div>
                                                </div>

                                                <div class="form-group col-sm-12">
                                                    <label class="col-sm-2 control-label">Status Active</label>


                                                    <div class="col-sm-10">

                                                        <asp:CheckBox ID="CheckBox1" runat="server" />

                                                    </div>


                                                </div>




                                            </div>

                                            
                                            
                                        </div>
                                        <div class="tab-pane" id="about">
                                            <p>Maecenas sed diam eget risus varius blandit sit amet non magna. Etiam porta sem malesuada magna mollis euismod. Donec ullamcorper nulla non metus auctor fringilla. Aenean eu leo quam. Pellentesque ornare sem lacinia quam venenatis vestibulum.</p>
                                        </div>
                                        <div class="tab-pane" id="contacts">
                                            <p>Cras justo odio, dapibus ac facilisis in, egestas eget quam. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam quis risus eget urna mollis ornare vel eu leo. Aenean lacinia bibendum nulla sed consectetur. Maecenas sed diam eget risus varius blandit sit amet non magna.</p>
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






</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContainer" runat="server">
</asp:Content>
