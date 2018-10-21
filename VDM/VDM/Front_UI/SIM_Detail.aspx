<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SIM_Detail.aspx.vb" Inherits="VDM.SIM_Detail" %>

<%@ Register Src="~/Front_UI/UC_CommonUI.ascx" TagPrefix="uc1" TagName="UC_CommonUI" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no">
    <title>Kiosk</title>
    <link href="css/true.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.min.css" rel="stylesheet">
    <link href="css/bootstrap-select.css" rel="stylesheet">
    <link href="css/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    <link href="css/slick.css" rel="stylesheet" type="text/css" />
    <link href="css/slick-theme.css" rel="stylesheet" type="text/css" />
    <link href="css/lightslider.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.12.2.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.js"></script>
    <script type="text/javascript" src="js/jquery.fancybox.js"></script>
    <script type="text/javascript" src="js/lightslider.js"></script>

    <link rel="stylesheet" href="css/jquery.mCustomScrollbar.css">
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
                <img src="images/bg-top.png" />
            </header>
            <main>
                <div class="priceplan">
                    <div class="main3">
                        <div class="detail-slider">
                            <asp:Label ID="lblCode" runat="server" Style="display: none;"></asp:Label>
                            <%--<asp:Repeater ID="rptPage" runat="server">
                                <ItemTemplate>--%>
                            <div>
                                <div class="description">
                                    <div class="pic">
                                        <div class="pic-shadow">
                                            <asp:Image ID="img" runat="server" Height="340px"></asp:Image>
                                        </div>
                                    </div>
                                    <figure class="col-md-7">
                                        <div class="topic true-l">
                                            <h1 class="true-l">
                                                <asp:Label ID="lblDISPLAY_NAME" runat="server" Text="" Style="font-size: 78pt; line-height: 70px;"></asp:Label></h1>
                                            <h5 class="true-m">

                                                <%--<asp:Label ID="lblDesc" runat="server"></asp:Label>--%>

                                            </h5>

                                            <%--<div class="term" style="margin: unset;">

                                                        <div class="frame" style="border: unset; background-color: transparent;">
                                                            <span id="content-d3" class="light" style="max-height: 280px;">
                                                                <p class="true-l">
                                                                    <div style="width: 100%;  font-size: 25px; margin-top: 20px;">
                                                                        <asp:Label ID="lblDesc" runat="server" ></asp:Label>
                                                                    </div>
                                                                </p>
                                                            </span>
                                                        </div>
                                                    </div>--%>

                                            <%--<div class="term" style="margin: unset;">
                             
                            <div class="frame">
                                <span id="content-d3" class="light">
                                    <p class="true-l"> 
                                        <div style="width: 100%; max-height: 300px;   font-size: 25px;margin-top:20px;">
                                            <asp:Label ID="lblDesc" runat="server"></asp:Label>
                                        </div>
                                    </p>
                                </span>
                            </div>
                        </div>--%>
                                        </div>
                                    </figure>
                                </div>

                                <div class="detail-sim" style ="margin-bottom:10px; width:80%; text-align:center;">

                                    <asp:Image ID="imgPrice" runat="server" Height="500px" style="width:unset;"></asp:Image>
                                </div>
                                <div class="true-l">
                                    <div class="term" style="margin: unset;">
                                        <div class="frame" style="border: unset; margin-top:40px; background-color: transparent;">
                                            <span id="content-d3" class="light" style="max-height: 300px;">
                                                
                                                    <div style="width: 100%; font-size: 30px; margin-top: 20px;">
                                                        <asp:Label ID="lblDesc" runat="server"></asp:Label>
                                                    </div>
                                               
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="list" style="margin-bottom: unset;">
                                    <li class="no-border">
                                        <h3 class="true-l" style="width: unset; margin-right: 20px;">
                                            <asp:Label ID="lblPrice_str" runat="server" Text="เริ่มต้นเพียง"></asp:Label></h3>
                                        <h2 class="true-l" style="width: unset;" title="฿">
                                            <asp:Label ID="lblPrice_Money" runat="server" Text=""></asp:Label>
                                            <span>
                                                <asp:Label ID="lblCurrency_Str" runat="server" Text="บาท"></asp:Label>
                                            </span></h2>

                                        <div class="col-md-12">
                                            <asp:LinkButton ID="btnSelect_str" runat="server" class="btu true-bs" Text="เลือก"></asp:LinkButton>
                                        </div>

                                    </li>
                                    <br />
                                </div>
                            </div>
                            <%--</ItemTemplate>
                            </asp:Repeater>--%>
                        </div>
                    </div>
                </div>
            </main>
            <footer style="bottom: 0px">
                <nav>
                    <div class="main">
                        <span class="col-md-6">
                            <asp:ImageButton ID="lnkHome" runat="server" ImageUrl="images/btu-home.png" />
                        </span>
                        <span class="col-md-6">
                            <asp:ImageButton ID="lnkBack" runat="server" ImageUrl="images/btu-prev.png" />
                        </span>
                    </div>
                </nav>
            </footer>
        </div>
        <uc1:UC_CommonUI runat="server" ID="UC_CommonUI" />
    </form>
</body>
<script type="text/javascript" src="js/slick.js"></script>
<script>
    $(document).ready(function () {

        $('.button').click(function () {
            $(this).addClass("active").siblings().removeClass("active");
        });
    });

</script>

<script src="js/jquery.mCustomScrollbar.js"></script>

<script>
    (function ($) {
        $(window).on("load", function () {

            $.mCustomScrollbar.defaults.scrollButtons.enable = true; //enable scrolling buttons by default
            $.mCustomScrollbar.defaults.axis = "y"; //enable 2 axis scrollbars by default

            $("#content-d3").mCustomScrollbar({ theme: "dark-3" });

        });
    })(jQuery);
</script>
</html>
