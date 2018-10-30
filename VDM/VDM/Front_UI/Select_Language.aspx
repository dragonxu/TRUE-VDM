<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Select_Language.aspx.vb" Inherits="VDM.Select_Language" %>

<%@ Register Src="~/Front_UI/UC_CommonUI.ascx" TagPrefix="uc1" TagName="UC_CommonUI" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no">
    <title>VENDING</title>

    <link href="css/true.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.min.css" rel="stylesheet">
    <link href="css/bootstrap-select.css" rel="stylesheet">

    <link href="css/true-popup.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery.fancybox.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery.fancybox.js"></script>
    <script type="text/javascript" src="js/bootstrap.js"></script>
    <script type="text/javascript" src="js/jquery-1.12.2.min.js"></script>
    <script>
        var $video = $('video'),
            $window = $(window);

        $(window).resize(function () {

            var height = $window.height();
            $video.css('height', height);

            var videoWidth = $video.width(),
                windowWidth = $window.width(),
            marginLeftAdjust = (windowWidth - videoWidth) / 2;

            $video.css({
                'height': height,
                'marginLeft': marginLeftAdjust
            });
        }).resize();
    </script>

</head>
<body>
    <form id="form" runat="server">
        <div class="warp bg">
            <!-- <header><img src="images/bg-top.png"/></header> -->
            <main>
                <div class="clip" id="div_video" runat="server">
                    <video controls autoplay loop>
                        <source id="video_tvc" runat="server" src="tvc/1.mp4" type="video/mp4" />
                    </video>
                </div>
                <%--<div class="pic-lang"><img src="images/pic-home.png"/></div>--%>

                <div class="pic-lang">
                    <img id="Poster_Img" runat="server" src="images/pic-home.png" /></div>
                <div class="lang">
                    <div class="main">
                        <h2 class="true-m">PLEASE SELECT LANGUAGE<br />
                            <font>to begin the transaction</font></h2>
                        <span class="col-md-4"><a id="TH" runat ="server" >
                            <img class="img-100" src="images/flag-th.png" /></a></span>
                        <span class="col-md-4"><a id="EN" runat ="server" >
                            <img class="img-100" src="images/flag-en.png" /></a></span>
                        <span class="col-md-4"><a id="CN" runat ="server" >
                            <img class="img-100" src="images/flag-cn.png" /></a></span>
                        <span class="col-md-4"><a id="JP" runat ="server" >
                            <img class="img-100" src="images/flag-jp.png" /></a></span>
                        <span class="col-md-4"><a id="KR" runat ="server" >
                            <img class="img-100" src="images/flag-kr.png" /></a></span>
                        <span class="col-md-4"><a id="RU" runat ="server" >
                            <img class="img-100" src="images/flag-ru.png" /></a></span>
                    </div>

<%--                    <div class="main">
                        <h2 class="true-m">PLEASE SELECT LANGUAGE<br />
                            <font>to begin the transaction</font></h2>
                        <span class="col-md-4"><a id="lnkTH" href="#popupTH">
                            <img class="img-100" src="images/flag-th.png" /></a></span>
                        <span class="col-md-4"><a id="lnkEN" href="#popupEN">
                            <img class="img-100" src="images/flag-en.png" /></a></span>
                        <span class="col-md-4"><a id="lnkCN" href="#popupCN">
                            <img class="img-100" src="images/flag-cn.png" /></a></span>
                        <span class="col-md-4"><a id="lnkJP" href="#popupJP">
                            <img class="img-100" src="images/flag-jp.png" /></a></span>
                        <span class="col-md-4"><a id="lnkKR" href="#popupKR">
                            <img class="img-100" src="images/flag-kr.png" /></a></span>
                        <span class="col-md-4"><a id="lnkRU" href="#popupRU">
                            <img class="img-100" src="images/flag-ru.png" /></a></span>
                    </div>--%>

                </div>
            </main>
            <footer>
                <div class="bottom-logo">
                    <img src="images/bg-bottom.png" />
                </div>

            </footer>

        </div>

        <uc1:UC_CommonUI runat="server" ID="UC_CommonUI" />

        <!--Popup-->
<%--        <div id="popupTH" class="popup">
            <div class="popup-frame">
                <h3 class="true-m half">หากท่านต้องการ<br />
                    ใบเสร็จรับเงินฉบับจริง<br />
                    หรือใบกำกับภาษี</h3>
                <div class="icon half">
                    <img src="images/popup/icon-Tax-gif.gif" />
                </div>
                <h4 class="true-b">กรุณาติดต่อพนักงานก่อนทำรายการ</h4>
                <div class="bottom"><a id="TH" runat="server" class="btu true-l">ดำเนินการต่อ</a></div>
            </div>
        </div>

        <div id="popupEN" class="popup">
            <div class="popup-frame">
                <h3 class="true-m half">หากท่านต้องการ<br />
                    ใบเสร็จรับเงินฉบับจริง<br />
                    หรือใบกำกับภาษี</h3>
                <div class="icon half">
                    <img src="images/popup/icon-Tax-gif.gif" />
                </div>
                <h4 class="true-b">กรุณาติดต่อพนักงานก่อนทำรายการ</h4>
                <div class="bottom"><a id="EN" runat="server" class="btu true-l">ดำเนินการต่อ</a></div>
            </div>
        </div>

        <div id="popupCN" class="popup">
            <div class="popup-frame">
                <h3 class="true-m half">หากท่านต้องการ<br />
                    ใบเสร็จรับเงินฉบับจริง<br />
                    หรือใบกำกับภาษี</h3>
                <div class="icon half">
                    <img src="images/popup/icon-Tax-gif.gif" />
                </div>
                <h4 class="true-b">กรุณาติดต่อพนักงานก่อนทำรายการ</h4>
                <div class="bottom"><a id="CN" runat="server" class="btu true-l">ดำเนินการต่อ</a></div>
            </div>
        </div>

        <div id="popupJP" class="popup">
            <div class="popup-frame">
                <h3 class="true-m half">หากท่านต้องการ<br />
                    ใบเสร็จรับเงินฉบับจริง<br />
                    หรือใบกำกับภาษี</h3>
                <div class="icon half">
                    <img src="images/popup/icon-Tax-gif.gif" />
                </div>
                <h4 class="true-b">กรุณาติดต่อพนักงานก่อนทำรายการ</h4>
                <div class="bottom"><a id="JP" runat="server" class="btu true-l">ดำเนินการต่อ</a></div>
            </div>
        </div>

        <div id="popupKR" class="popup">
            <div class="popup-frame">
                <h3 class="true-m half">หากท่านต้องการ<br />
                    ใบเสร็จรับเงินฉบับจริง<br />
                    หรือใบกำกับภาษี</h3>
                <div class="icon half">
                    <img src="images/popup/icon-Tax-gif.gif" />
                </div>
                <h4 class="true-b">กรุณาติดต่อพนักงานก่อนทำรายการ</h4>
                <div class="bottom"><a id="KR" runat="server" class="btu true-l">ดำเนินการต่อ</a></div>
            </div>
        </div>

        <div id="popupRU" class="popup">
            <div class="popup-frame">
                <h3 class="true-m half">หากท่านต้องการ<br />
                    ใบเสร็จรับเงินฉบับจริง<br />
                    หรือใบกำกับภาษี</h3>
                <div class="icon half">
                    <img src="images/popup/icon-Tax-gif.gif" />
                </div>
                <h4 class="true-b">กรุณาติดต่อพนักงานก่อนทำรายการ</h4>
                <div class="bottom"><a id="RU" runat="server" class="btu true-l">ดำเนินการต่อ</a></div>
            </div>
        </div>

        <script type="text/javascript">
            $("#lnkTH").fancybox();
            $("#lnkEN").fancybox();
            $("#lnkCN").fancybox();
            $("#lnkJP").fancybox();
            $("#lnkKR").fancybox();
            $("#lnkRU").fancybox();
        </script>--%>

        <!--Popup-->

    </form>
</body>
</html>
