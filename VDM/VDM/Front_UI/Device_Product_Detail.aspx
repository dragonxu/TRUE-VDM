<%@ Page Title="" Language="vb" AutoEventWireup="false" CodeBehind="Device_Product_Detail.aspx.vb" Inherits="VDM.Device_Product_Detail" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no">
    <title>TRUE</title>
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
    <div class="warp">
        <header>
            <img src="images/bg-top.png" />
        </header>
        <main>
            <div class="priceplan">
                <div class="main3">
                    <div class="detail-slider">
                        <div>
                            <div class="description">
                                <div class="pic">
                                    <img src="images/iphonex-white.png" />
                                </div>
                                <figure class="col-md-6">
                                    <div class="topic true-l">
                                        <h3>
                                             <span  class="true-l" ><asp:Label ID="lblDISPLAY_NAME" runat="server" Text="iPhone X" style="font-size :78pt;"></asp:Label> </span>
                                          
                                        </h3>
                                         
                                        <form class="capacity">

                                            <asp:Repeater ID="rptCapacity" runat="server">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnCapacity" runat="server" class="button active true-l" CommandName="Capacity" value="64GB" />
                                                </ItemTemplate>
                                            </asp:Repeater>

                                            <%--<input class="button active true-l" name="sliver64GB" type="button" value="64GB" />
                                            <input class="button true-l" name="sliver128GB" type="button" value="128GB" />--%>

                                        </form>
                                    </div>
                                    <asp:Repeater ID="rptSpec" runat="server">
                                        <ItemTemplate>
                                             <span class="true-l"><p class="true-m"><asp:Label ID="lblSPEC_NAME" runat ="server" ></asp:Label>:</p><asp:Label ID="lblDESCRIPTION" runat ="server" ></asp:Label></span>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                    <%--<span class="true-l"><p class="true-m">ความสูง:</p>143.6 มม. (5.65 นิ้ว)</span>--%>
                                     
                                    <span>
                                        <p class="bottom true-m"><asp:Label ID="lblSPEC_Warranty" runat ="server" ></asp:Label> &nbsp; <asp:Label ID="lblDESCRIPTION_Warranty" runat ="server" ></asp:Label> </p>
                                    </span>
                                    <div class="thumb">
                                        <asp:Repeater ID="rptColor" runat="server">
                                            <ItemTemplate>
                                                 <span><i class="current">
                                                     <a id="btnBrand" runat ="server" ><asp:Image ID="img" runat="server"  Width="311px"></asp:Image></a></i><p class="true-m"><asp:Label ID="lblColor" runat ="server" ></asp:Label></p>
                                                </span>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                       <%-- <span><i class="current">
                                            <img src="images/iphonex-white.png" /></i><p class="true-m">SILVER</p>
                                        </span>
                                        <span><i>
                                            <img src="images/iphonex-black.png" /></i><p class="true-m">SPACEGREY</p>
                                        </span>--%>
                                    </div>
                                </figure>
                            </div>
                            <ul class="detail">
                                <em> </em>
                                <h4 class="true-l"><asp:Label ID="lblDescription_Header" runat ="server" Text ="รายละเอียดสินค้า" ></asp:Label></h4>
                                <li class="true-l"><asp:Label ID="lblDescription_Detail" runat ="server" ></asp:Label></li>
                                <%--<li class="true-l">ดีไซน์แบบกระจกและอะลูมิเนียมทั้งหมด ทนน้ำและฝุ่น2</li>
                                <li class="true-l">กล้องคู่ความละเอียด 12MP พร้อมระบบ OIS, โหมดภาพถ่ายบุคคล, คุณสมบัติ "การจัดแสงภาพถ่ายบุคคล" และวิดีโอระดับ 4K สูงสุด 60 fps4</li>
                                <li class="true-l">กล้อง FaceTime HD ความละเอียด 7MP พร้อม Retina Flash เพื่อภาพเซลฟี่ที่สวยงามน่าทึ่ง</li>
                                <li class="true-l">5 Touch ID เพื่อการยืนยันตัวตนที่ปลอดภัย</li>
                                <li class="true-l">ชิพ A11 Bionic ที่ทรงพลังและฉลาดที่สุดในสมาร์ทโฟน</li>
                                <li class="true-l">การชาร์จแบบไร้สายที่ใช้ร่วมกับแท่นชาร์จ Qi ได้1</li>--%>
                            </ul>
                            <div class="list">
                                <li class="no-border">
                                    <h3 class="true-l"><asp:Label ID="lblPrice_str" runat ="server" Text ="ราคา" ></asp:Label></h3>
                                    <h2 class="true-l" title="฿"><asp:Label ID="lblPrice_Money" runat ="server" Text ="39,000" ></asp:Label></h2>
                                    <div class="col-md-12"><a id="clicksetp1" class="btu true-bs" href="#popup1"><asp:Label ID="lblbtnSelect_str" runat ="server" Text ="เลือก" ></asp:Label> </a></div>
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
                <span class="col-md-6"><a href="home.html">
                    <img src="images/btu-home.png" /></a></span>
                <span class="col-md-6"><a href="javascript:history.back();">
                    <img src="images/btu-prev.png" /></a></span>
            </div>
        </nav>
    </footer>
    <script type="text/javascript" src="js/slick.js"></script>
</html>
