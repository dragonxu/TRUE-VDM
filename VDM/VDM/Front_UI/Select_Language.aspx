<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Select_Language.aspx.vb" Inherits="VDM.Select_Language" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no">
<title>TRUE</title>
<link href="css/true.css" rel="stylesheet" type="text/css" />
<link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
<link href="css/font-awesome.min.css" rel="stylesheet">
<link href="css/bootstrap-select.css" rel="stylesheet">
<script type="text/javascript" src="js/jquery-1.12.2.min.js"></script>
<script type="text/javascript" src="js/bootstrap.js"></script>
</head>
<body>
<div class="warp bg">
<!-- <header><img src="images/bg-top.png"/></header> -->
<main>
  <div class="pic-lang"><img src="images/pic-home.png"/></div>
  <div class="lang">
    <div class="main">
      <h2 class="true-m">Please Select Language</h2>
      <span class="col-md-4"><a id="TH" runat ="server" href="home.aspx"><img class="img-100" src="images/flag-th.png"/></a></span>
      <span class="col-md-4"><a id="EN" runat ="server" href="home.aspx"><img class="img-100" src="images/flag-en.png"/></a></span>
      <span class="col-md-4"><a id="CN" runat ="server" href="home.aspx"><img class="img-100" src="images/flag-cn.png"/></a></span>
      <span class="col-md-4"><a id="JP" runat ="server" href="home.aspx"><img class="img-100" src="images/flag-jp.png"/></a></span>
      <span class="col-md-4"><a id="KR" runat ="server" href="home.aspx"><img class="img-100" src="images/flag-kr.png"/></a></span>
      <span class="col-md-4"><a id="RU" runat ="server" href="home.aspx"><img class="img-100" src="images/flag-ru.png"/></a></span>
    </div>
  </div>
</main>
<footer><div class="bottom-logo"><img src="images/bg-bottom.png"/></div></footer>
</div>
</body>
</html>
