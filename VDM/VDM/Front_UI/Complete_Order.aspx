﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" CodeBehind="Complete_Order.aspx.vb" Inherits="VDM.Complete_Order" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no">
    <title>Kiosk</title>
    <link href="css/true.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.min.css" rel="stylesheet">
    <link href="css/bootstrap-select.css" rel="stylesheet">
    <script type="text/javascript" src="js/jquery-1.12.2.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.js"></script>
    <style>
        header {
            position: relative;
        }
    </style>
</head>
<body class="bg2">
    <form id="form1" runat="server">
        <div class="warp">
            <header>
                <img src="images/bg-top.png" /></header>
            <%--<main>
  <div class="priceplan">
    <div class="main3">
      <div class="pic-step">
        <p class="t-cart t-red true-b">SHOPING CART</p>
        <p class="t-payment t-red true-b">PAYMENT</p>
        <p class="t-complete t-red true-b">COMPLETE ORDER</p>
        <img src="images/pic-step3.png"/>
      </div>
      <div class="step">
        <div class="step-payment">
          <h4 class="margin-re true-m">ท่านได้ชำระค่าสินค้าแล้ว</h4>
          <div class="b-rceipt">
            <p class="true-m">ต้องการรับใบเสร็จหรือไม่?</p>
            <p class="sub true-m">(ในการเคลมสินค้าจะต้องแสดงใบเสร็จรับเงิน)</p>
            <span>
              <a class="red true-bs" href="receipt.html">ต้องการรับ</a>
              <a class="true-bs " href="pack.html">ไม่ต้องการรับ</a>
            </span>
          </div>
        </div>
      </div>
    </div>
  </div>
</main>--%>


            <main>
                <div class="priceplan">
                    <div class="main3">
                        <div class="pic-step">
                            <p class="t-cart t-red true-b">SHOPING CART</p>
                            <p class="t-payment t-red true-b">PAYMENT</p>
                            <p class="t-complete t-red true-b">COMPLETE ORDER</p>
                            <img src="images/pic-step4.png" />
                        </div>
                        <div class="pack">
                            <div class="step-payment" style ="padding :10px 0 100px 0;">
                            <h4 class="margin-re true-m">ท่านได้ชำระค่าสินค้าแล้ว</h4>
                                </div>
                            <img src="images/icon-pack.png">
                            <div class="step-payment">


                                <h4 class="margin-re true-m">กรุณารอสักครู่...</h4>
                            </div>
                        </div>
                    </div>
                </div>
            </main>

            <footer>
                <nav>
                    <div class="main">
                        <span class="col-md-6"><a href="home.html">
                            <img src="images/btu-home.png" /></a></span>
                        <span class="col-md-6"><a href="javascript:history.back();">
                            <img src="images/btu-prev.png" /></a></span>
                    </div>
                </nav>
            </footer>
        </div>
    </form>
</body>
</html>