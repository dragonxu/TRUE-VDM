<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Thank_You.aspx.vb" Inherits="VDM.Thank_You" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no">
<title>VENDING</title>
<link href="css/true.css" rel="stylesheet" type="text/css" />
<link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
<link href="css/font-awesome.min.css" rel="stylesheet">
<link href="css/bootstrap-select.css" rel="stylesheet">
<script type="text/javascript" src="js/jquery-1.12.2.min.js"></script>
<script type="text/javascript" src="js/bootstrap.js"></script>
<style>
  header{position:relative;}
</style>
</head>
<body class="bg2">
<form id="form1" runat="server">
<div class="warp">
<header><img src="images/bg-top.png"/></header>

<asp:ScriptManager ID="scm" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UDP" runat="server">
    <ContentTemplate>

    <main>
      <div class="priceplan">
        <div class="main3">
          <div class="thank">
            <img src="images/pic-thank.png">
              <h4 class="margin-re true-l"><font class="true-m">ท่่านได้ทำรายการเสร็จสิ้น</font><br/>กรุณารับใบเสร็จและสินค้า<br/>ทางช่องรับสินค้า</h4>
          </div>
        </div>
      </div>
    </main>
        <asp:TextBox ID="txtPrintContent" runat="server" Width="50%" Height="200px" TextMode="MultiLine"></asp:TextBox>
    <footer style="bottom: 0px">
        <nav>
            <div class="main">
                <span class="col-md-6">
                    <asp:ImageButton ID="lnkHome" runat="server" ImageUrl="images/btu-home.png" />
                </span>
                <span class="col-md-6">
                      <asp:TextBox ID="txtLocalControllerURL" runat="server" Style="display:none;"></asp:TextBox>
                </span>
            </div>
        </nav>
    </footer>

    </ContentTemplate>
</asp:UpdatePanel>
</div>

         <script type="text/javascript">

             String.prototype.replaceAll = function (search, replacement) {
                 var target = this;
                 return target.split(search).join(replacement);
             };

             var printDelegate = function () {
                 var content = $('#txtPrintContent').val().replaceAll("&lt", "<").replaceAll("&gt",">");                 
                 var url = $('#txtLocalControllerURL').val() + '/Print.aspx?Mode=Print';
                 var xhr = new XMLHttpRequest();
                 xhr.open("POST", url, true);
                 xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                 xhr.send(content);
             }
             
             var redirectDelegate = function () {
                 $('#lnkHome').click();
             }
             var waitSeconds = 10;// รอ 10 วินาที
             setTimeout(redirectDelegate, waitSeconds * 1000);
            

             function cashDispense(_type,qty) {
                 var url = $('#txtLocalControllerURL').val() + '/CashDispense.aspx?Type=' + _type + '&Q=' + qty + '&callback=';
                    var script = document.createElement('script');
                    script.src = url;
                    var body = document.getElementsByTagName('body')[0];
                    body.appendChild(script);
             }

             function coinDispense(_type,qty) {
                 var url = $('#txtLocalControllerURL').val() + '/CoinDispense.aspx?Type=' + _type + '&Q=' + qty + '&callback=';
                    var script = document.createElement('script');
                    script.src = url;
                    var body = document.getElementsByTagName('body')[0];
                    body.appendChild(script);
             }
         </script>

         </form>
    </body>
</html>

