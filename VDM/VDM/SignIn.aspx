<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SignIn.aspx.vb" Inherits="VDM.SignIn" %>

<!DOCTYPE html>
<html class="no-js" lang="">
<head>
  <meta charset="utf-8">
  <title>VENDING - Management Console</title>
  <meta name="description" content="">
  <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1, maximum-scale=1">
  <!-- build:css({.tmp,app}) styles/app.min.css -->
  <link rel="stylesheet" type="text/css" href="styles/webfont.css">
  <link rel="stylesheet" type="text/css" href="styles/climacons-font.css">
  <link rel="stylesheet" type="text/css" href="vendor/bootstrap/dist/css/bootstrap.css">
  <link rel="stylesheet" type="text/css" href="styles/font-awesome.css">
  <link rel="stylesheet" type="text/css" href="styles/card.css">
  <link rel="stylesheet" type="text/css" href="styles/sli.css">
  <link rel="stylesheet" type="text/css" href="styles/animate.css">
  <link rel="stylesheet" type="text/css" href="styles/app.css">
  <link rel="stylesheet" type="text/css" href="styles/app.skins.css">
  <link rel="stylesheet" type="text/css" href="styles/onScreenKeyboard.css">
  <!-- endbuild -->
</head>

<body class="page-loading">
  
<!-- page loading spinner -->
<%--<div class="pageload">
    <div class="pageload-inner">
        <div class="sk-wave">
            <div class="sk-rect sk-rect1"></div>
            <div class="sk-rect sk-rect2"></div>
            <div class="sk-rect sk-rect3"></div>
            <div class="sk-rect sk-rect4"></div>
            <div class="sk-rect sk-rect5"></div>
        </div>        
    </div>
</div>--%>
<!-- /page loading spinner -->

  <div class="app signin v2 usersession">
    <div class="session-wrapper">
      <div class="session-carousel carousel slide carousel-fade" data-ride="carousel" data-interval="5000">
        <!-- Wrapper for slides -->
        <div class="carousel-inner" role="listbox">
          <div class="item active" style="background-image:url(../images/SignIn/1.png);background-size:cover;background-repeat: no-repeat;background-position: 50% 50%;">
          </div>
          <div class="item" style="background-image:url(../images/SignIn/2.png);background-size:cover;background-repeat: no-repeat;background-position: 50% 50%;">
          </div>
          <div class="item" style="background-image:url(../images/SignIn/3.png);background-size:cover;background-repeat: no-repeat;background-position: 50% 50%;">
          </div>
        </div>       
      </div>
      <div class="card bg-white no-border">
        <div class="card-block" style="padding-top:unset;">
          <form id="form1" role="form" runat="server" class="form-layout">
            <div class="text-center">
              
              <img src="images/Logo128x128.png" />

              <p style="font-size:12px;">Please sign in to your account</p>
            </div>
            <div class="form-inputs p-b">
              <label class="text-uppercase">Username</label>
              <asp:TextBox ID="txtUser" runat="server" CssClass="form-control input-lg osk-trigger" data-osk-options="disableReturn" required></asp:TextBox>
              <label class="text-uppercase">Password</label>
              <asp:TextBox ID="txtPass" runat="server" CssClass="form-control input-lg osk-trigger" data-osk-options="disableReturn" required TextMode="Password"></asp:TextBox>
            </div>
            <asp:LinkButton CssClass="btn btn-danger btn-block btn-lg m-b" ID="btnLogin" runat="server" Text="Login"></asp:LinkButton>
              <asp:Label ID="lblError" runat="server">Invalid admin username and password</asp:Label>
            <div class="divider">
             
            </div>
            <p class="text-center"><small><em>Management Tool by Autobox-TiT Development 2018</em></small>
            </p>

          </form>
        </div>
      </div>
      <div class="push"></div>
    </div>
  </div>
  <!-- build:js({.tmp,app}) scripts/app.min.js -->

  <script type="text/javascript" src="Scripts/jquery.min.js"></script>
  <script type="text/javascript" src="Scripts/jquery.onScreenKeyboard.js"></script>
  <script type="text/javascript" src="scripts/helpers/modernizr.js"></script>
  <%--<script type="text/javascript" src="vendor/jquery/dist/jquery.js"></script>--%>
  <script type="text/javascript" src="vendor/bootstrap/dist/js/bootstrap.js"></script>
  <script type="text/javascript" src="vendor/fastclick/lib/fastclick.js"></script>
  <script type="text/javascript" src="vendor/perfect-scrollbar/js/perfect-scrollbar.jquery.js"></script>
  <script type="text/javascript" src="scripts/jquery.cookie.min.js" type="text/javascript"></script>
  <script type="text/javascript" src="scripts/helpers/smartresize.js"></script>
  <script type="text/javascript" src="scripts/constants.js"></script>  
  <script type="text/javascript" src="Scripts/onScreenKeyboard.js"></script>
  <script type="text/javascript" src="scripts/main.js"></script>
  
  <!-- endbuild -->
</body>

</html>
