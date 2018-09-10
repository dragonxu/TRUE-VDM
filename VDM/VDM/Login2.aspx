﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login2.aspx.vb" Inherits="VDM.Login2" %>

<!DOCTYPE html>


<html class="no-js" lang="">

<head>
  <meta charset="utf-8">
  <title>Reactor - Bootstrap Admin Template</title>
  <meta name="description" content="">
  <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1, maximum-scale=1">
  <!-- build:css({.tmp,app}) styles/app.min.css -->
  <link rel="stylesheet" href="styles/webfont.css">
  <link rel="stylesheet" href="styles/climacons-font.css">
  <link rel="stylesheet" href="vendor/bootstrap/dist/css/bootstrap.css">
  <link rel="stylesheet" href="styles/font-awesome.css">
  <link rel="stylesheet" href="styles/card.css">
  <link rel="stylesheet" href="styles/sli.css">
  <link rel="stylesheet" href="styles/animate.css">
  <link rel="stylesheet" href="styles/app.css">
  <link rel="stylesheet" href="styles/app.skins.css">
  <!-- endbuild -->
</head>

<body class="page-loading">
  <form id="form1" runat="server">
<!-- page loading spinner -->
<div class="pageload">
    <div class="pageload-inner">
        <div class="sk-wave">
            <div class="sk-rect sk-rect1"></div>
            <div class="sk-rect sk-rect2"></div>
            <div class="sk-rect sk-rect3"></div>
            <div class="sk-rect sk-rect4"></div>
            <div class="sk-rect sk-rect5"></div>
        </div>        
    </div>
</div>
<!-- /page loading spinner -->

  <div class="app signin v2 usersession">
    <div class="session-wrapper">
      <div class="session-carousel slide" data-ride="carousel" data-interval="3000">
        <!-- Wrapper for slides -->
       
      </div>
      <div class="card bg-white no-border">
        <div class="card-block">
          <form role="form" class="form-layout" action="/">
            <div class="text-center m-b">
              <h4 class="text-uppercase">Welcome back</h4>
              <p>Please sign in to your account</p>
            </div>
            <div class="form-inputs p-b">
              <label class="text-uppercase">Your email address</label>
              <input type="email" class="form-control input-lg" placeholder="Email Address" required>
              <label class="text-uppercase">Password</label>
              <input type="password" class="form-control input-lg" placeholder="Password" required>
              <a ui-sref="user.forgot">Forgotten password?</a>
            </div>
            <button class="btn btn-primary btn-block btn-lg m-b" type="submit">Login</button>
            <div class="divider">
              <span>OR</span>
            </div>
            <a class="btn btn-block no-bg btn-lg m-b" href="extras-signup2.html">Signup</a>
            <p class="text-center"><small><em>By clicking Log in you agree to our <a href="#">terms and conditions</a></em></small>
            </p>
          </form>
        </div>
      </div>
      <div class="push"></div>
    </div>
  </div>
  </form>
  <!-- build:js({.tmp,app}) scripts/app.min.js -->
  <script src="scripts/helpers/modernizr.js"></script>
  <script src="vendor/jquery/dist/jquery.js"></script>
  <script src="vendor/bootstrap/dist/js/bootstrap.js"></script>
  <script src="vendor/fastclick/lib/fastclick.js"></script>
  <script src="vendor/perfect-scrollbar/js/perfect-scrollbar.jquery.js"></script>
  <script src="scripts/helpers/smartresize.js"></script>
  <script src="scripts/constants.js"></script>
  <script src="scripts/main.js"></script>
  <!-- endbuild -->
</body>

</html>
