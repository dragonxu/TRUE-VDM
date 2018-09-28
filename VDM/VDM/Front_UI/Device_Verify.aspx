<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Device_Verify.aspx.vb" Inherits="VDM.Device_Verify" %>

<%@ Register Src="~/Front_UI/UC_Scan_IDCart.ascx" TagPrefix="uc1" TagName="UC_Scan_IDCart" %>
<%@ Register Src="~/Front_UI/UC_Scan_Face_Recognition.ascx" TagPrefix="uc1" TagName="UC_Scan_Face_Recognition" %>



<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <meta http-equiv="Page-Exit" content="revealTrans(Duration=2.0,Transition=0)">
    <%--<meta http-equiv="Page-Enter" content="revealTrans(Duration=2.0,Transition=8)">--%>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no">
    <title>Kiosk</title>
    <link href="css/true.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.min.css" rel="stylesheet">
    <link href="css/bootstrap-select.css" rel="stylesheet">
    <script type="text/javascript" src="js/jquery-1.12.2.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.js"></script>


    <link href="css/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    <link href="css/slick.css" rel="stylesheet" type="text/css" />
    <link href="css/slick-theme.css" rel="stylesheet" type="text/css" />
    <link href="css/lightslider.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery.fancybox.js"></script>
    <script type="text/javascript" src="js/lightslider.js"></script>


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
                        <div class="pic-step">
                            <p class="t-cart t-red true-b">SHOPING CART</p>
                            <p class="t-payment t-red true-b">PAYMENT</p>
                            <p class="t-complete true-b">COMPLETE ORDER</p>
                            <img src="images/pic-step2.png" />
                        </div>
                        <div class="step" id="formStep" runat="server">
                            <div class="step-payment">
                                <%--<h3 class="t-red true-b">รวมยอดชำระ <i title="฿">27,000</i></h3>--%>
                                <h3 class="true-b t-red">
                                    <asp:Label ID="lblPrice_str" runat="server" Text="ยอดชำระ"></asp:Label>
                                    <i title="฿">
                                        <asp:Label ID="lblPrice_Money" runat="server" Text="39,000"></asp:Label></i>
                                    <asp:Label ID="lblCurrency_Str" runat="server" Text="บาท"></asp:Label></h3>
                                <asp:Panel ID="pnlScanIDCard" runat="server">

                                    <uc1:UC_Scan_IDCart runat="server" ID="UC_Scan_IDCart" />



                                </asp:Panel>
                                <asp:Panel ID="pnlFace_Recognition" runat="server">
                                    <uc1:UC_Scan_Face_Recognition runat="server" ID="UC_Scan_Face_Recognition" />
                                </asp:Panel>
                                <div class="col-md-12">
                                    <asp:Button ID="btnSkip_ScanIDCard" runat="server" class="btu true-bs" Style="background: #635b5b; padding: 0 50px 0 50px; float: right; margin-top: 100px;" Text="ข้าม" />
                                </div>



                            </div>
                        </div>
                    </div>
                </div>
            </main>



            <asp:Panel ID="pnlModul_IDCard_Success" runat="server">

                <div class="fancybox-overlay fancybox-overlay-fixed" style="width: auto; height: auto; display: block;">
                    <div class="fancybox-wrap fancybox-desktop fancybox-type-inline fancybox-opened" tabindex="-1" style="width: 818px; height: auto; position: absolute; top: 250px; left: 122px; opacity: 1; overflow: visible;">
                        <div class="fancybox-skin" style="padding: 0px; width: auto; height: auto;">
                            <div class="fancybox-outer">
                                <div class="fancybox-inner" style="overflow: auto; width: 818px; height: auto;">
                                    <div id="popup2" style="display: block;">

                                        <div class="privilege">

                                            <h3 class="true-m" style="margin: 20px 0 60px 0;">สำเร็จ</h3>
                                            <div class="idcard">
                                                <img src="images/pic-idcard2.png">
                                            </div>

                                            <%--<h4 class="true-m t-red">อย่าลืมเก็บบัตรของท่าน</h4>--%>
                                            <div class="icon" style="margin: 0px 0 50px 0">
                                                <asp:LinkButton ID="btnStart_Take_Photos" runat="server" class="btu true-l" Text="เริ่มแสกนใบหน้า"></asp:LinkButton>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--<a title="Close" class="fancybox-item fancybox-close" href="javascript:;"></a>--%>
                            <asp:LinkButton ID="lnkClose" runat="server" class="fancybox-item fancybox-close"></asp:LinkButton>
                        </div>
                    </div>
                </div>

            </asp:Panel>










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
    </form>
</body>
</html>

