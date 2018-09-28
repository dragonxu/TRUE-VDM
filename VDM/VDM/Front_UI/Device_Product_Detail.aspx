<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Device_Product_Detail.aspx.vb" Inherits="VDM.Device_Product_Detail" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no">
     <meta http-equiv="Page-Enter" content="revealTrans(Duration=2.0,Transition=12)">

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

    <style>
        header {
            position: relative;
        }

        .btn-active {
        height: 80px;width: unset;margin-top: 30px;background: #827b7b;border-radius: 10px;
        /* border: 10px; */
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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpList" runat="server">
            <ContentTemplate>
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
                                        <div class="description">
                                            <div class="col-md-5">
                                                <div class="row">
                                                    <div class="pic" style="padding: unset;">
                                                        <%-- <img src="images/iphonex-white.png" />    Width="250px"--%>
                                                        <asp:Image ID="img" runat="server"></asp:Image>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="card-slider" style="padding: unset; padding-left: 80px;">
                                                        <asp:Repeater ID="rptColor" runat="server" OnItemCommand="rptColor_ItemCommand">
                                                            <ItemTemplate>
                                                                <div>

                                                                    <span><i class="current" style="padding: 0 10px;">
                                                                        <asp:LinkButton ID="lnkColor" runat="server">
                                                                            <a id="btnColor" runat="server" >
                                                                                <asp:Image ID="img"  runat="server" Style="height: 70px; width: unset;"></asp:Image>
                                                                            </a>
                                                                        </asp:LinkButton>
                                                                        <asp:Button ID="btnSelect" runat="server" Style="display: none;" CommandName="Select" />
                                                                    </i>
                                                                        <p class="true-m">
                                                                            <asp:Label ID="lblColor" runat="server"></asp:Label>
                                                                        </p>
                                                                    </span>

                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>

                                                    </div>

                                                </div>

                                            </div>
                                            <div class="col-md-7">
                                                <figure class="col-md-12">
                                                    <div class="topic true-l">
                                                        <div>
                                                            <h3>
                                                                <span class="true-l">
                                                                    <asp:Label ID="lblDISPLAY_NAME" runat="server" Text="iPhone X" Style="font-size: 78pt; line-height: 70px;"></asp:Label>
                                                                </span>
                                                            </h3>
                                                        </div>
                                                        <div class="capacity" style="padding-top: 20px;">
                                                            <asp:Repeater ID="rptCapacity" runat="server">
                                                                <ItemTemplate>
                                                                    <%-- <asp:Button ID="btnCapacity" runat="server" class="active true-l" CommandName="Capacity" value="64GB" Visible="false" />
                                                                    --%>
                                                                    <asp:LinkButton ID="lnkCapacity" runat="server" class="btu active true-bs" Text="เลือก"></asp:LinkButton>

                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                            <%--<input class="button active true-l" name="sliver64GB" type="button" value="64GB" />
                                            <input class="button true-l" name="sliver128GB" type="button" value="128GB" />--%>
                                                        </div>
                                                    </div>
                                                    <asp:Repeater ID="rptSpec" runat="server">
                                                        <ItemTemplate>
                                                            <span class="true-l">
                                                                <div style="float: left;">
                                                                    <p class="true-m">
                                                                        <asp:Label ID="lblSPEC_NAME" runat="server"></asp:Label>

                                                                    </p>
                                                                </div>
                                                                <div style="float: left;">
                                                                    :
                                                                </div>
                                                                <div style="float: left;">
                                                                    <asp:Label ID="lblDESCRIPTION" runat="server" Style="float: left;"></asp:Label>
                                                                </div>
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:Repeater>

                                                    <%--<span class="true-l"><p class="true-m">ความสูง:</p>143.6 มม. (5.65 นิ้ว)</span>--%>
                                                    <asp:Panel ID="pnlSPEC_Warranty" runat="server">
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
                                                        <%--<asp:Repeater ID="rptColor" runat="server" >
                                                            <ItemTemplate>
                                                                <span><i class="current" style ="padding :20px 30px;">
                                                                    <asp:LinkButton ID="lnkColor" runat ="server" >
                                                                        <a id="btnColor" runat="server">
                                                                            <asp:Image ID="img" runat="server" Style ="height: 70px;width: unset;" ></asp:Image>
                                                                        </a>
                                                                    </asp:LinkButton>
                                                                      </i><p class="true-m">
                                                                            <asp:Label ID="lblColor" runat="server"></asp:Label>
                                                                        </p>
                                                                </span>
                                                            </ItemTemplate>
                                                        </asp:Repeater>--%>






                                                        <%-- <span><i class="current">
                                            <img src="images/iphonex-white.png" /></i><p class="true-m">SILVER</p>
                                        </span>
                                        <span><i>
                                            <img src="images/iphonex-black.png" /></i><p class="true-m">SPACEGREY</p>
                                        </span>--%>
                                                    </div>
                                                </figure>

                                            </div>

                                        </div>
                                        <ul class="detail">

                                            <h4 class="true-l">
                                                <asp:Label ID="lblDescription_Header" runat="server" Text="รายละเอียดสินค้า"></asp:Label></h4>
                                            <li class="true-l">
                                                <asp:Label ID="lblDescription_Detail" runat="server"></asp:Label></li>

                                        </ul>
                                        <div class="list">
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




            </ContentTemplate>
        </asp:UpdatePanel>
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


</html>
