<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Device_Product_Detail.aspx.vb" Inherits="VDM.Device_Product_Detail" ResponseEncoding="UTF-8" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=tis-620">

    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no">
    <meta http-equiv="Page-Enter" content="revealTrans(Duration=2.0,Transition=12)">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=gb18030" />
    <meta http-equiv="Content-Type" content="text/html; charset=big5" />
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />



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

    <link href="css/slick.css" rel="stylesheet" type="text/css" />
    <link href="css/slick-theme.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" href="css/jquery.mCustomScrollbar.css">

    <style>
        header {
            position: relative;
        }

        .active {
            color: #FFF;
            background: #47464B;
        }
    </style>

    <style>
        textarea {
            /*padding: 10px;*/
            /*margin-top :-10px ;
                margin-left :10px;
				vertical-align: top;
				width: 100%;*/
            border-style: none;
            background: transparent;
            resize: none;
            font-size: 36px;
            /*line-height :50px;
                padding-bottom:0px;
                margin-bottom :5px;*/
        }

            textarea:focus {
                outline-style: unset;
                outline-width: 2px;
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
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpList" runat="server">
            <ContentTemplate>--%>
        <div class="warp">
            <header>
                <img src="images/bg-top.png" />
            </header>
            <main>
                <div class="priceplan">
                    <asp:Label ID="lblCode" runat="server" Style="display: none;" CAPACITY=""></asp:Label>
                    <div class="main3">
                        <div class="detail-slider">

                            <div>
                                <div class="description" style="margin-bottom: unset;">
                                    <div class="col-md-5">
                                        <div class="row">
                                            <div class="pic" style="padding: unset;">
                                                <asp:Image ID="img" runat="server"></asp:Image>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="priceplan">

                                                <div class="card-slider" style="padding: unset;">

                                                    <asp:Repeater ID="rptColor" runat="server" OnItemCommand="rptColor_ItemCommand">
                                                        <ItemTemplate>
                                                            <div id="BoxIndex" runat="server" style="float: left; width: 110px;">
                                                                <div class="priceplan">
                                                                    <%--                                                        <div  class="priceplan" style="margin-right :10px;width:unset ;" >--%>

                                                                    <div class="thumb">

                                                                        <span>
                                                                            <asp:LinkButton ID="lnkColor" runat="server"> </asp:LinkButton>
                                                                            <a id="btnColor" runat="server">
                                                                                <asp:Panel class="select-color" Style="padding: 10px 0; width: 90%;" ID="pnlSelect" runat="server">
                                                                                    <asp:Image ID="img" runat="server" Style="height: 80px; width: unset;"></asp:Image>
                                                                                </asp:Panel>
                                                                                <p class="true-m" style="width: unset;">
                                                                                    <asp:Label ID="lblColor" runat="server" Style="width: unset; text-align: center;font-size :25px;"></asp:Label>
                                                                                </p>
                                                                            </a>
                                                                            <asp:Button ID="btnSelect" runat="server" Style="display: none;" CommandName="Select" />
                                                                        </span>

                                                                        <%--<span>
                                                                    <asp:LinkButton ID="LinkButton2" runat="server">
                                                                    <i  class="current"><img src="images/iphonex-white.png"></i>
                                                                    </asp:LinkButton>
                                                                    <p class="true-m">GRSDEREY</p>
                                                                </span>--%>

                                                                        <%--<span><i class="current"><img src="images/iphonex-black.png"></i><p class="true-m">SPACEGREY</p></span>--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>

                                        </div>

                                    </div>
                                    <div class="col-md-7">
                                        <figure class="col-md-12">
                                            <div class="topic true-l">
                                                <div>
                                                    <h3>
                                                        <span class="true-l">
                                                            <asp:Label ID="lblDISPLAY_NAME" runat="server" Text="" Style="font-size: 78pt; line-height: 70px;"></asp:Label>
                                                        </span>
                                                    </h3>
                                                </div>
                                                <div class="capacity" style="padding-top: 20px;">
                                                    <asp:Repeater ID="rptCapacity" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkCapacity" runat="server" class="btu" Text="เลือก" CommandName="Select"></asp:LinkButton>

                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                            <asp:Repeater ID="rptSpec" runat="server">
                                                <ItemTemplate>
                                                    <%--<span class="true-l">
                                                        <div style="float: left;">
                                                            <p class="true-m">
                                                                <asp:Label ID="lblSPEC_NAME" runat="server"></asp:Label>

                                                            </p>
                                                        </div>
                                                        <div style="float: left;">
                                                            :
                                                        </div>
                                                        <div style="float: left;">
                                                            <asp:Label ID="lblDESCRIPTION" runat="server" Style="float: left;" Visible="false"></asp:Label>                                                            
                                                            <textarea id="txtarea" runat="server" readonly style="width: 100%; line-height: 26px;" visible ="false" ></textarea>
                                                            <div style="width: 100%; max-height: 300px; overflow:auto; font-size:16px;">
                                                                <asp:Label ID="lblDesc" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </span>--%>

                                                    <div class="true-l col-sm-12" style="font-size: 20px;">

                                                        <div style="float: left;">
                                                            <h6 class="true-m">
                                                                <asp:Label ID="lblSPEC_NAME" runat="server" Text="รายละเอียดสินค้า"></asp:Label></h6>
                                                        </div>
                                                        <div style="float: left; margin-top: 10px;">
                                                            <b>:</b>
                                                        </div>
                                                        <div style="float: left;">
                                                            <div style="width: 100%; max-height: 400px; overflow: auto; font-size: 20px; min-height: 40px; padding-left: 10px; margin-top: 10px;">
                                                                <asp:Label ID="lblDesc" runat="server" Style="font-size: 25px;"></asp:Label>
                                                            </div>
                                                        </div>


                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                            <%--<span class="true-l"><p class="true-m">ความสูง:</p>143.6 มม. (5.65 นิ้ว)</span>--%>
                                            <asp:Panel ID="pnlSPEC_Warranty" runat="server" Style="margin-top: 20px;">
                                                <span>
                                                    <p class="bottom true-m">
                                                        <div style="float: left; padding-right: 10px;">
                                                            <asp:Label ID="lblSPEC_Warranty" runat="server"></asp:Label>
                                                        </div>
                                                        <div style="float: left;">
                                                            <asp:Label ID="lblDESCRIPTION_Warranty" runat="server"></asp:Label>
                                                        </div>
                                                    </p>
                                                </span>
                                            </asp:Panel>

                                            <div class="thumb">
                                            </div>
                                        </figure>

                                    </div>

                                </div>
                                <div class="detail true-l">

                                    <h4 class="true-l">
                                        <asp:Label ID="lblDescription_Header" runat="server" Text="รายละเอียดสินค้า"></asp:Label></h4>
                                    <%--<li class="true-l"> </li>--%>
                                    <div>
                                        <asp:Label ID="lblDescription_Detail" runat="server"></asp:Label>

                                        <div style="width: 100%; max-height: 300px; overflow: auto; font-size: 25px;">
                                            <%-- <asp:Label ID="lblDesc" runat="server"></asp:Label>--%>
                                        </div>
                                    </div>

                                    <div class="term" style="margin: unset;">

                                        <div class="frame" style="border: unset; background-color: transparent;">
                                            <span id="content-d3" class="light" style="max-height: 250px;">
                                                <p class="true-l">
                                                    <div style="width: 100%;   font-size: 25px; margin-top: 20px;">
                                                        <asp:Label ID="lblDesc" runat="server"  ></asp:Label>
                                                    </div>
                                                </p>
                                            </span>
                                        </div>
                                    </div>

                                </div>
                                <div class="list" style="margin-bottom: unset;">
                                    <li class="no-border">
                                        <h3 class="true-l" style="width: unset; margin-right: 20px;">
                                            <asp:Label ID="lblPrice_str" runat="server" Text="ราคา"></asp:Label></h3>
                                        <h2 class="true-l" style="width: unset;" title="฿">
                                            <asp:Label ID="lblPrice_Money" runat="server" Text="39,000"></asp:Label>
                                            <span>
                                                <asp:Label ID="lblCurrency_Str" runat="server" Text="บาท"></asp:Label></span></h2>

                                        <div class="col-md-12">
                                            <asp:LinkButton ID="btnSelect_str" runat="server" class="btu true-bs" Text="เลือก"></asp:LinkButton>
                                        </div>

                                    </li>
                                    <br />
                                    <br />
                                    <br />
                                    <br />



                                </div>
                            </div>

                        </div>
                    </div>
                </div>
        </main>


        </div>

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




        <%--            </ContentTemplate>
        </asp:UpdatePanel>--%>
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

        $('.card-slider').slick({
            infinite: true,
            slidesToShow: 3,
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


<script src="js/jquery.mCustomScrollbar.js"></script>

<script>
    (function ($) {
        $(window).on("load", function () {

            $.mCustomScrollbar.defaults.scrollButtons.enable = true; //enable scrolling buttons by default
            $.mCustomScrollbar.defaults.axis = "yx"; //enable 2 axis scrollbars by default

            $("#content-d3").mCustomScrollbar({ theme: "dark-3" });

        });
    })(jQuery);
</script>

<script src="js/Autosize_textarea/autosize.js" type="text/javascript"></script>
<script>
    autosize(document.querySelectorAll('textarea'));
</script>


</html>
