<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SignIn.aspx.vb" Inherits="VDM.SignIn" %>


<html class="no-js" lang="">

<head>
  <meta charset="utf-8" />
  <title>Welcome to - VENDING Management Console</title>
  <meta name="description" content="" />
  <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1, maximum-scale=1" />
  <!-- build:css({.tmp,app}) styles/app.min.css -->
  <link rel="stylesheet" type="text/css" href="styles/webfont.css" />
  <link rel="stylesheet" type="text/css" href="styles/climacons-font.css" />
  <link rel="stylesheet" type="text/css" href="vendor/bootstrap/dist/css/bootstrap.css" />
  <link rel="stylesheet" type="text/css" href="styles/font-awesome.css" />
  <link rel="stylesheet" type="text/css" href="styles/card.css" />
  <link rel="stylesheet" type="text/css" href="styles/sli.css" />
  <link rel="stylesheet" type="text/css" href="styles/animate.css" />
  <link rel="stylesheet" type="text/css" href="styles/app.css" />
  <link rel="stylesheet" type="text/css" href="styles/app.skins.css" />
  <!-- endbuild -->

   <!-- Custom Style -->
    <style type="text/css">
        body, .form-control {
            font-size: 0.8rem;        
        }
    </style>
  <!-- end costom style -->

</head>

<body class="page-loading">
 <form id="form1" runat="server">
  <!-- page loading spinner -->
  <!--<div class="pageload">
    <div class="pageload-inner">
      <div class="sk-rotating-plane"></div>
    </div>
  </div>-->
  <!-- /page loading spinner -->
  <div class="app signin usersession">
    <div class="session-wrapper">
      <div class="page-height-o row-equal align-middle">
        <div class="column">
          <div class="card bg-white no-border">
            <div class="card-block">
              <asp:Panel CssClass="form-layout" ID="pnlLogin" runat="server" DefaultButton="btnLogin">
                <div class="text-center m-b">
                  <h4 class="text-uppercase">VENDING Management Console</h4>
                    <p>
                        <img src="images/Logo128x128.png" />
                    </p>
                  <p>Sign In to Administrative Web</p>
                </div>
                <div class="form-inputs">
                  <label class="text-uppercase">User Name</label>
                  <asp:TextBox ID="txtUser" runat="server" CssClass="form-control input-lg" placeholder="User Name"></asp:TextBox>
                  <label class="text-uppercase">Password</label>
                  <asp:TextBox ID="txtPass" runat="server" class="form-control input-lg" TextMode="Password" placeholder="Password"></asp:TextBox>
                </div>
                <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-danger btn-block btn-lg " Text="Sign In" />
                <div style="text-align:center;">
                    <asp:Label ID="lblError" runat="server" CssClass="text-center m-b" style=" width:100%; color:red;">
                        Invalid admin username and password
                    </asp:Label>
                </div>
                  
              </asp:Panel>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- bottom footer -->
    <footer class="session-footer">
      <nav class="footer-right">
        <ul class="nav">
          <li>
            <a href="javascript:;">Support</a>
          </li>
          <li>
            <a href="javascript:;" class="scroll-up">
              <i class="fa fa-angle-up"></i>
            </a>
          </li>
        </ul>
      </nav>
      <nav class="footer-left hidden-xs">
        <ul class="nav">

        </ul>
      </nav>
    </footer>
    <!-- /bottom footer -->
  </div>
  <!-- build:js({.tmp,app}) scripts/app.min.js -->
  <script type="text/javascript" src="scripts/helpers/modernizr.js"></script>
  <script type="text/javascript" src="vendor/jquery/dist/jquery.js"></script>
  <script type="text/javascript" src="vendor/bootstrap/dist/js/bootstrap.js"></script>
  <script type="text/javascript" src="vendor/fastclick/lib/fastclick.js"></script>
  <script type="text/javascript" src="vendor/perfect-scrollbar/js/perfect-scrollbar.jquery.js"></script>
  <script type="text/javascript" src="scripts/helpers/smartresize.js"></script>
  <script type="text/javascript" src="scripts/constants.js"></script>
  <script type="text/javascript" src="scripts/main.js"></script>
  <!-- endbuild -->

</form>
</body>

</html>