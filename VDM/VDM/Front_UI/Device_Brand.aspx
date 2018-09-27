<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Device_Brand.aspx.vb" Inherits="VDM.Device_Brand" %>
 
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
    <style>
        header {
            position: relative;
        }
    </style>
    <script>
        $("#clicksetp1").fancybox();
        $("#clicksetp2").fancybox();
        $("#clicksetp3").fancybox();
    </script>
</head>
<body class="bg2">
    <form id="form1" runat="server">
        <div class="warp">
            <header>
                <img src="images/bg-top.png" />
            </header>
            <main>
                <div class="brand">
                    <asp:Label ID="lblCode" runat="server" Style="display: none;"></asp:Label>
                    <div class="main3">
                        <div class="detail-slider">

                            <asp:Repeater ID="rptPage" runat="server">
                                <ItemTemplate>
                                    <div>
                                        <div class="description">
                                             <div class="pic-devices" style ="margin-top :-200px;">
                                                <h3 class="true-l">Brands</h3>
                                                  
                                                <img src="images/pic-top_catalog.png" style="width: unset; padding-top: 120px;width :400px;" />
                                            </div>
                                            <ul>
                                                <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand">
                                                    <ItemTemplate>
                                                        <li class="col-md-4">
                                                            
                                                            <a id="btnBrand" runat="server">
                                                                <p class="true-m" style ="text-align:center;font-size: 30px;">
                                                                    <asp:Label ID="lblBrand" runat="server"></asp:Label>
                                                                </p>
                                                                <asp:Image ID="img" runat="server" Width="311px"></asp:Image>
                                                            </a>
                                                        </li>
                                                        <asp:Button ID="btnSelect" runat="server" Style="display: none;" CommandName="Select" />
                                                        
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>


                        </div>
                    </div>
                </div>
            </main>


        </div>
<%--        <footer style="bottom: 0px">
            <nav>
                <div class="main">
                    <span class="col-md-6"><a href="home.html">
                        <img src="images/btu-home.png" /></a></span>
                    <span class="col-md-6"><a href="javascript:history.back();">
                        <img src="images/btu-prev.png" /></a></span>
                </div>
            </nav>
        </footer>--%>
             <footer>
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

    </form>
</body>
<script type="text/javascript" src="js/slick.js"></script>

<script>
    $(document).ready(function () {

        $('.button').click(function () {
            $(this).addClass("active").siblings().removeClass("active");
        });

        $('.detail-slider').slick({
            infinite: true,
            slidesToShow: 1,
            slidesToScroll: 1,

        });

        $("#c-slider1").lightSlider({
            adaptiveHeight: true,
            auto: false,
            item: 3,
            slideMargin: 10,
            loop: true,
            pager: true,
            galleryMargin: 0,
            controls: false,
        });

        $("#c-slider2").lightSlider({
            adaptiveHeight: true,
            auto: false,
            item: 3,
            slideMargin: 10,
            loop: true,
            pager: true,
            galleryMargin: 0,
            controls: false,
        });

        $("#c-slider3").lightSlider({
            adaptiveHeight: true,
            auto: false,
            item: 3,
            slideMargin: 10,
            loop: true,
            pager: true,
            galleryMargin: 0,
            controls: false,
        });

    });

</script>


</html>
