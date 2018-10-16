﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Device_Shoping_Cart.aspx.vb" Inherits="VDM.Device_Shoping_Cart" %>

<%@ Register Src="~/Front_UI/UC_CommonUI.ascx" TagPrefix="uc1" TagName="UC_CommonUI" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Page-Exit" content="revealTrans(Duration=5.0,Transition=0)">
    <meta http-equiv="Page-Enter" content="revealTrans(Duration=5.0,Transition=8)">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no">
    <title>Kiosk</title>
    <link href="css/true.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.min.css" rel="stylesheet">
    <link href="css/bootstrap-select.css" rel="stylesheet">
    
    <link href="css/true-popup.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery.fancybox.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery.fancybox.js"></script>

    <script type="text/javascript" src="js/bootstrap.js"></script>
     <script src="js/jquery.mCustomScrollbar.js"></script>

    <link rel="stylesheet" href="css/jquery.mCustomScrollbar.css">
   

    <style type="text/css">
        header {
            position: relative;
        }

        .default {
            background: #5a5454 url(images/icon-cart.png) no-repeat left 40px top 10px;
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
                        <div class="pic-step">
                            <p class="t-cart t-red true-b">SHOPING CART</p>
                            <p class="t-payment true-b">PAYMENT</p>
                            <p class="t-complete true-b">COMPLETE ORDER</p>
                            <img src="images/pic-step1.png" />
                        </div>
                        <div class="description">
                            <div class="pic" style="padding: unset; text-align: center;">
                                <asp:Image ID="img" runat="server" Style="width: 70%; height:320px;"></asp:Image>
                            </div>
                            <figure class="col-md-6">
                                <div class="topic true-l">
                                    <h1 class="true-l" style="padding-bottom: 30px;">
                                        <asp:Label ID="lblDISPLAY_NAME" runat="server" Style="font-size: 60pt; line-height: 70px;"></asp:Label>

                                    </h1>
                                    <asp:Panel ID="pnlProduct" runat="server">

                                        <div class="capacity">

                                            <div style="float: left;">
                                                <asp:Panel ID="pnlCapacity" runat="server">
                                                    <p>
                                                        <asp:Label ID="lblCapacity" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </asp:Panel>
                                            </div>

                                            <div class="color true-m" style="float: left;">
                                                <asp:Label ID="lblColor" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </asp:Panel>


                                </div>



                                <%--Phase 2--%>
                                <%--<span class="cart1 true-l">
                                    <p class="true-m">
                                        <asp:Label ID="lblHeader_Package" runat="server" Text="พร้อมสมัครแพ็กเกจ"></asp:Label>
                                    </p>
                                    <br />
                                    <asp:Label ID="lblDetail_Package" runat="server" Text="4G+ FUN Unlimited 1499 -1899"></asp:Label>
                                </span>
                                <span class="cart1 true-l">
                                    <p class="true-m">
                                        <asp:Label ID="lblHeader_Promotion" runat="server" Text="สิทธิพิเศษจากทรูยู"></asp:Label>
                                    </p>
                                    <br />
                                    <asp:Label ID="lblDetail_Promotion" runat="server" Text="ทรูแบลคการ์ด ซื้อเคส UAG ในราคา 500.-"></asp:Label>

                                </span>--%>
                            </figure>
                        </div>

                        <div class="step-cart">
                            <div class="total" style="margin-bottom: 30px;">
                                <h4 class="true-b t-red">
                                    <asp:Label ID="lblPrice_str" runat="server" Text="ยอดชำระ"></asp:Label>
                                    <i title="฿">
                                        <asp:Label ID="lblPrice_Money" runat="server" Text=""></asp:Label></i>
                                    <asp:Label ID="lblCurrency_Str" runat="server" Text="บาท"></asp:Label>

                                </h4>
                            </div>

                            <%--cart-step2.html--%>
                        </div>


                        <div class="term" style="height:460px; margin:unset;">
                            <h2 class="true-m">Term & Condition</h2>
                            <div class="frame" style="height: 380px;">
                                <span id="content-d3" class="light" style="height:340px; overflow:hidden;">
                                    <p class="true-l">
                                        <%-- <asp:Label  class="true-l" ID="lblTerm_Condition" runat="server" Text=""></asp:Label>--%>
                                        ข้อกำหนดและเงื่อนไขรายการส่งเสริมการขาย 4G+ FUN Unlimited 299, 4G+ FUN Unlimited 399, 4G+ FUN Unlimited 499, 4G+ FUN Unlimited 599, 4G+ FUN Unlimited 699, 4G+ FUN Unlimited 899, 4G+ FUN Unlimited 1099, 4G+ FUN Unlimited 1299, 4G+ FUN Unlimited 1499, และ 4G+ FUN Unlimited 1899
                                        <br />
                                        <br />
                                        1. รายการส่งเสริมการขายนี้ สำหรับผู้สมัครใช้บริการเลขหมายใหม่โทรศัพท์เคลื่อนที่ ทรูมูฟ เอช ของบริษัท เรียล มูฟ จำกัด (“เรียลมูฟ”) ระบบรายเดือน ในนามบุคคลธรรมดา ตั้งแต่วันที่ 6 กุมภาพันธ์ 2559 ถึง 31 มีนาคม 2561 หรือจนกว่าจะมีการเปลี่ยนแปลง
                                        <br />
                                        2. รายละเอียดรายการส่งเสริมการขาย: อัตราค่าบริการยังไม่รวมภาษีมูลค่าเพิ่ม<br />
                                        2.1 รายการส่งเสริมการขาย 4G+ FUN Unlimited 299<br />
                                        สิทธิตามแพ็กเกจปกติ:<br />
                                        คิดอัตราค่าใช้บริการเหมาจ่ายขั้นต่ำรายเดือน 299 บาท ต่อเดือน ผู้ใช้บริการจะได้รับสิทธิใช้บริการ ดังนี้<br />
                                        (1) โทรทุกเครือข่ายจำนวน 100 นาทีต่อเดือน<br />
                                        (2) บริการ 4G ที่ความเร็วสูงสุด 300 เมกะบิตต่อวินาที (Mbps) และบริการ 3G/EDGE/GPRS ที่ความเร็วสูงสุด 42 เมกะบิตต่อวินาที (Mbps) เป็นจำนวน 1 กิกะไบต์ (GB)หลังจากนั้น จะใช้ได้ไม่จำกัดปริมาณที่ความเร็วสูงสุดไม่เกิน 128 กิโลบิตต่อวินาที (Kbps)<br />
                                        (3) บริการ Wi-Fi ที่ความเร็วสูงสุด 200 เมกะไบต์ต่อวินาที (Mbps) ไม่จำกัดปริมาณการใช้งาน<br />
                                        ได้ไม่จำกัดปริมาณการใช้งาน ที่ความเร็วสูงสุด 300 เมกะบิตต่อวินาที(Mbps)
                                        <br />
                                        <br />
                                        เป็นระยะเวลา 12 เดือน (รอบบิล) ผ่าน URL ที่เป็นไปตามข้อกำหนดและเงื่อนไขการใช้งานของแต่ละบริการ
                                        โดยสามารถรับไอเทมโค้ดได้ 1 ครั้ง
                                                    ผ่านแอปพลิเคชัน TrueID โดยลูกค้าจะต้องไม่มียอดค้างชำระตั้งแต่รอบบิลแรกเป็นต้นไป กล่อง TMH Exclusive Legend ประกอบด้วย Dragon Master Zuka (Exclusive Skin) หรือ ชิ้นส่วนฮีโร่ 40 ชิ้น หรือ ชิ้นส่วนฮีโร่ 20 ชิ้น หรือ ชิ้นส่วนฮีโร่ 10 ชิ้น โดยลูกค้าจะได้รับไอเทมใดไอเทมหนึ่ง
                                                    ไอเทมพิเศษที่ได้รับ มีจำนวนจำกัด ไม่สามารถแลกเปลี่ยนเป็นอย่างอื่นได้ บนโทรศัพท์เคลื่อนที่หรือแท๊บเล็ต ในกรณีที่ผู้ใช้บริการใช้ website อื่นหรือลิงค์หรือการแชร์ที่ปรากฏบนหน้าแอปพลิเคชันTrueID ผู้ใช้บริการจะต้องชำระค่าใช้บริการเพิ่มเติม(ถ้ามี)ตามอัตราที่ระบุในแพ็กเกจที่ผู้ใช้บริการเลือกใช้งานอยู่ ณ ขณะนั้น หรือหากในแพ็กเกจที่เลือกใช้ ไม่ได้ระบุไว้ ให้คิดค่าบริการตามเงื่อนไขที่บริษัทกำหนด
                                        เฉพาะบริการภายในแอปพลิเคชันเกม ROV บนโทรศัพทํเคลื่อนที่ หรือแท๊บเล็ต ในกรณีที่ ผู้ใช้บริการใช้ website อื่นหรือลิงค์หรือการแชร์ที่ปรากฏบนหน้าแอปพลิเคชันเกม ROV ผู้ใช้บริการจะต้องชำระค่าใช้บริการเพิ่มเติม (ถ้ามี) ตามอัตราที่ระบุในแพ็คเกจที่ผู้ใช้บริการเลือกใช้งานอยู่ ณ ขณะนั้น หรือหากในแพ็คเกจที่เลือกใช้ ไม่ได้ระบุไว้ ให้คิดค่าบริการตามเงื่อนไขที่บริษัทกำหนด
                                    </p>
                                </span>
                            </div>
                        </div>
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="udpList" runat="server">
                            <ContentTemplate>
                                <label class="check true-l" style="height:100px;">
                                    ข้าพเจ้าได้อ่านข้อกำหนดและเงื่อนไขทุกข้อแล้ว
                                    <asp:CheckBox ID="chkActive" runat="server" Checked="false" AutoPostBack="true" />
                                    <span class="checkmark"></span>
                                </label>


                                <div class="col-md-12" style="text-align: center;">
                                    <asp:Panel ID="pnlConfirm" runat="server">
                                        <asp:Button ID="btnConfirm_str" runat="server" class="order true-m btn-default " Text="ชำระเงิน" />
                                        <input type="button" class="order true-m" id="btnIDCard" runat="server" value="สั่งซื้อ" onclick="$('#clickIDCard').click();" />
                                        <a id="clickIDCard" runat="server" style="display:none;" href="#popupIPCard">สั่งซื้อ</a>
                                    </asp:Panel>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>


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

        <div id="popupIPCard" class="popup" >
          <div class="popup-frame" >
            <h3 class="true-m">ขั้นตอนการยืนยันตัวบุคคล</h3>
            <div class="icon"><img src="images/Popup/icon-idcard.png"/></div>
            <h4 class="true-b" style="padding-bottom:30px;">กรุณาสอดบัตรประชาชนของท่าน</h4>
            <%--<div class="bottom"><a class="btu true-l" onclick="$.fancybox.close();" href="javascript:;">ตกลง</a></div>--%>
          </div>
        </div>

    </form>
</body>



<script type="text/javascript">

    $("#clickIDCard").fancybox();

    (function ($) {
        $(window).on("load", function () {

            $.mCustomScrollbar.defaults.scrollButtons.enable = true; //enable scrolling buttons by default
            $.mCustomScrollbar.defaults.axis = "yx"; //enable 2 axis scrollbars by default
            $("#content-d3").mCustomScrollbar({ theme: "dark-3" });

        });
    })(jQuery);
</script>
</html>

