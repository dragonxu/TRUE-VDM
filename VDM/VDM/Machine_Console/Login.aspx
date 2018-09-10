<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="VDM.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <title>MACHINE CONSOLE</title>
  <meta name="description" content="">
  <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1, maximum-scale=1">
  <!-- build:css({.tmp,app}) styles/app.min.css -->
  <link rel="stylesheet" href="../Styles/webfont.css">
  <link rel="stylesheet" href="../styles/climacons-font.css">
  <link rel="stylesheet" href="../vendor/bootstrap/dist/css/bootstrap.css">
  <link rel="stylesheet" href="../styles/font-awesome.css">
  <link rel="stylesheet" href="../styles/card.css">
  <link rel="stylesheet" href="../styles/sli.css">
  <link rel="stylesheet" href="../styles/animate.css">
  <link rel="stylesheet" href="../styles/app.css">
  <link rel="stylesheet" href="../styles/app.skins.css">

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
                  <h4 class="text-uppercase">VENDING Management CONSOLE</h4>
                    <p>
                        <img id="imgLogo" src="../images/Icon/koisk_ab.png" style="width: 10%;">
                    </p>
                  <p>Sign In to MACHINE CONSOLE</p>
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
