<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Device_Payment.aspx.vb" Inherits="VDM.Device_Payment" %>

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
    <script type="text/javascript" src="js/jquery-1.12.2.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.js"></script>


    <link href="css/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    <link href="css/slick.css" rel="stylesheet" type="text/css" />
    <link href="css/slick-theme.css" rel="stylesheet" type="text/css" />
    <link href="css/lightslider.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery.fancybox.js"></script>
    <script type="text/javascript" src="js/lightslider.js"></script>

    <script type="text/javascript" src="js/jquery-1.12.2.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.js"></script>
    
    <!---------VDM----------->
    <script src="../Scripts/txtClientControl.js" type="text/javascript" ></script>
    <script src="../Scripts/extent.js" type="text/javascript"></script>

    <style>
        header {
            position: relative;
        }
    </style>
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
                            <div class="main3">
                                <div class="pic-step" style="padding:100px 0 0px 0;">
                                    <p class="t-cart t-red true-b">SHOPING CART</p>
                                    <p class="t-payment t-red true-b">PAYMENT</p>
                                    <p class="t-complete true-b">COMPLETE ORDER</p>
                                    <img src="images/pic-step2.png" />
                                </div>
                        <div class="description" style ="margin :30px 0px 0px 0px;">
                            <div class="pic" style="padding: unset; text-align: center;">
                                <asp:Image ID="img" runat="server" Style="width: 60%;"></asp:Image>
                            </div>
                            <figure class="col-md-7">
                                <div class="topic true-l">
                                    <h2 class="true-l" style="padding-bottom: 30px;">
                                        <asp:Label ID="lblDISPLAY_NAME" runat="server" Style="font-size: 60pt; line-height: 70px;" Width="140%"></asp:Label>

                                    </h2>
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

                            </figure>
                        </div>                                
                                <div class="step" id="formStep" runat="server" style ="padding :0px 0 20px 0;">
                                    <div class="step-payment">
                                        <h3 class="true-b t-red">
                                            <asp:Label ID="lblPrice_str" runat="server" Text="ยอดชำระ"></asp:Label>
                                            <i title="฿">
                                                <asp:TextBox ID="txtCost" runat="server" Text="0" Style="text-align: center; width: 300px; background-color: transparent; border: none; margin-top: -15px;" ReadOnly="true"></asp:TextBox>

                                            </i>
                                            <asp:Label ID="lblCurrency_Str" runat="server" Text="บาท"></asp:Label></h3>
                                        <div class="btu-payment" style="margin: 30px 0 0 0;">
                                            <div class="row" style="margin-left: 100px;">
                                                <a id="lnkCash" runat="server">
                                                    <dt class="icon-cash">
                                                        <span class="true-l">เงินสด<span>
                                                    </dt>
                                                </a>
                                                <a id="lnkCredit" runat="server" visible="false">
                                                    <dt class="icon-credit">
                                                        <span class="true-l">บัตรเครดิต
                                                    <p class="text">บัตรเดบิต บัตรเงินสด</p>
                                                            <span></dt>
                                                </a>
                                                <a id="lnkTruemoney" runat="server">
                                                    <dt class="icon-truemoney">
                                                        <span class="true-l">ทรูมันนี่<span>
                                                    </dt>
                                                </a>
                                                <%--<a href="javascript:;">
                                            <dt class="icon-online-banking">
                                                <span class="true-l">Online<br />
                                                    banking<span>
                                            </dt>
                                        </a>--%>
                                            </div>
                                        </div>
                                <asp:Panel ID="pnlSelectChoice" runat="server">
                                    <h4 class="margin true-m" style="margin-top: 60px;">เลือกวิธีชำระเงิน</h4>
                                </asp:Panel>
                                    </div>
                                </div>

                                <asp:Panel ID="pnlCash" runat="server">
                                    <form class="step">
                                        <div class="step-payment">

                                            <h4 class="true-m" style ="margin :30px 0 20px 0; ">ชำระด้วยเงินสด</h4>

                                            <div class="f-remark">
                                                <p class="true-m" style="font-size:50px;">
                                                    กรุณาเตรียมเงินให้พร้อม<br />
                                                    เพราะเมื่อเริ่มชำระเงินแล้วจะ<u class="t-red">ไม่สามารถยกเลิกได้</u>
                                                </p>
                                                <p class="sub true-l" style="font-size:40px;">(หากจำเป็นต้องยกเลิก โปรดติดต่อพนักงาน)</p>
                                                <em style ="margin-left: 90px;"></em>
                                                <p class="true-m" style="font-size:50px;">โปรดสอดธนบัตรทางช่องด้านขวา</p>
                                                <p class="sub true-l" style="font-size:40px;">(รับเฉพาะเงินบาทไทยเท่านั้น)</p>
                                            </div>
                                            <div class="f-cash" style ="padding :20px 0;">
                                                <div class="col-md-4" style="text-align: center;">
                                                    <h3 class="true-b">ชำระแล้ว</h3>
                                                </div>
                                                <div class="col-md-6" style="text-align: center;">
                                                    <h3>
                                                        <p style="border: unset; padding-right: 30px; text-align: right;">
                                                            <asp:TextBox class="true-b " ID="txtPaid" runat="server" Text="0" Style="text-align: right; width: 300px; float: right; background-color: transparent; border: none; margin-top: -15px;"></asp:TextBox>
                                                        </p>
                                                    </h3>
                                                </div>
                                                <div class="col-md-2" style="text-align: center;">
                                                    <h3 class="true-b">บาท</h3>
                                                </div>

                                                <div class="col-md-4" style="text-align: center;">
                                                    <h3 class="true-b t-red">เหลือ</h3>
                                                </div>
                                                <div class="col-md-6" style="text-align: center;">
                                                    <h3>
                                                        <p style="text-align: right; padding-right: 30px;">
                                                            <asp:TextBox class="true-b  t-red" ID="txtRequire" runat="server" Text="0" Style="text-align: right; width: 300px; float: right; background-color: transparent; border: none; margin-top: -5px;"></asp:TextBox>
                                                        </p>
                                                    </h3>
                                                </div>
                                                <div class="col-md-2" style="text-align: center;">
                                                    <h3 class="true-b t-red">บาท</h3>
                                                </div>
                                            </div>
                                            <p class="time true-l">เวลาชำระเงินสดคงเหลือ <span id="castTimeOut"></span></p>
                                        </div>

                                        <div style="position: absolute; top: 300px; right: 100px; display:none;" class="row">
                                            <div style="display: block;" class="col-lg-3">
                                                1 :
                                                <asp:TextBox ID="txt1" runat="server" Text="0"></asp:TextBox>
                                            </div>
                                            <div style="display: block;" class="col-lg-3">
                                                2 :
                                                <asp:TextBox ID="txt2" runat="server" Text="0"></asp:TextBox>
                                            </div>
                                            <div style="display: block;" class="col-lg-3">
                                                5 :
                                                <asp:TextBox ID="txt5" runat="server" Text="0"></asp:TextBox>
                                            </div>
                                            <div style="display: block;" class="col-lg-3">
                                                10 :
                                                <asp:TextBox ID="txt10" runat="server" Text="0"></asp:TextBox>
                                            </div>
                                            <div style="display: block;" class="col-lg-3">
                                                20 :
                                                <asp:TextBox ID="txt20" runat="server" Text="0"></asp:TextBox>
                                            </div>
                                            <div style="display: block;" class="col-lg-3">
                                                50 :
                                                <asp:TextBox ID="txt50" runat="server" Text="0"></asp:TextBox>
                                            </div>
                                            <div style="display: block;" class="col-lg-3">
                                                100 :
                                                <asp:TextBox ID="txt100" runat="server" Text="0"></asp:TextBox>
                                            </div>
                                            <div style="display: block;" class="col-lg-3">
                                                500 :
                                                <asp:TextBox ID="txt500" runat="server" Text="0"></asp:TextBox>
                                            </div>
                                            <div style="display: block;" class="col-lg-3">
                                                1000 :
                                                <asp:TextBox ID="txt1000" runat="server" Text="0"></asp:TextBox>
                                            </div>
                                            <asp:TextBox ID="txtCashProblem" runat="server"></asp:TextBox>
                                            <asp:Button ID="btnCashCompleted" runat="server" />
                                            <asp:Button ID="btnCashTimeout" runat="server" />
                                            <asp:Button ID="btnCashProblem" runat="server" />
                                        </div>                                      
                                    </form>
                                </asp:Panel>


                                <asp:Panel ID="pnlCredit" runat="server">
                                    <form class="step">
                                        <div class="step-payment">
                                            <h4 class="true-m">ชำระด้วยบัตรเครดิต</h4>
                                            <div class="f-card">
                                                <fieldset>
                                                    <p class="true-m">Card Type</p>
                                                    <div class="field">
                                                        <label class="checkcard">
                                                            <input checked="checked" name="radio" type="radio">
                                                            <span class="radiobtn"></span>
                                                            <img src="images/icon-visa.png">
                                                        </label>
                                                        <label class="checkcard">
                                                            <input name="radio" type="radio">
                                                            <span class="radiobtn"></span>
                                                            <img src="images/icon-express.png">
                                                        </label>
                                                        <label class="checkcard">
                                                            <input name="radio" type="radio">
                                                            <span class="radiobtn"></span>
                                                            <img src="images/icon-dinersclub.png">
                                                        </label>
                                                    </div>
                                                </fieldset>
                                                <fieldset>
                                                    <p class="true-m">Card Number</p>
                                                    <div class="field">
                                                        <input class="true-l bg-field" name="" type="text">
                                                    </div>
                                                </fieldset>
                                                <fieldset>
                                                    <p class="true-m">Name on Card</p>
                                                    <div class="field">
                                                        <input class="true-l bg-field" name="" type="text">
                                                    </div>
                                                </fieldset>
                                                <fieldset>
                                                    <p class="true-m">CCV Number</p>
                                                    <div class="field">
                                                        <input class="true-l bg-field" name="" placeholder="***" type="password">
                                                    </div>
                                                </fieldset>
                                                <fieldset>
                                                    <p class="true-m">Expiration Date</p>
                                                    <div class="field-spit">
                                                        <input class="true-l bg-field" name="" placeholder="MM" type="text">
                                                    </div>
                                                    <em class="true-l">/</em>
                                                    <div class="field-spit">
                                                        <input class="true-l bg-field" name="" placeholder="YY" type="text">
                                                    </div>
                                                </fieldset>
                                                <div class="col-md-12" style="text-align: center;">
                                                    <input class="order true-m" name="" type="submit" value="ส่ง" />
                                                </div>
                                            </div>
                                        </div>
                                    </form>
                                </asp:Panel>

                                <asp:Panel ID="pnlTruemoney" runat="server">
                                    <form class="step">
                                        <div class="step-payment">
                                            <h4 class="true-m">ชำระด้วยทรูมันนี่</h4>
                                            <div class="frame">
                                                <div class="f-app">
                                                    <div class="pic">
                                                        <img src="images/icon-app.png" />
                                                    </div>
                                                    <p class="true-m">
                                                        เปิดแอป TrueMoney Wallet<br />
                                                        บนมือถือของท่าน
                                                    </p>
                                                </div>
                                                <div class="f-app">
                                                    <div class="pic">
                                                        <img src="images/qr-code2.png" />
                                                    </div>
                                                    <p class="true-m">
                                                        และสแกน QR Code
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </form>

                                    <asp:Panel ID="pnlBarcode" runat="server" DefaultButton="btnBarcode" Style="position: fixed; left: -500px; top: 0px;">
                                        <asp:TextBox ID="txtBarcode" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnBarcode" runat="server" Style="display: none;" />
                                    </asp:Panel>

                                </asp:Panel>
                                <div class="col-md-12">
                                    <asp:Button ID="btnSkip" runat="server" class="btu true-bs" Style="background: #635b5b; padding: 0 50px 0 50px; float: right; margin-top: 100px; display:none;" Text="ต่อไป" />
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

            </ContentTemplate>
        </asp:UpdatePanel>

        <script type="text/javascript">

            // เริ่มจ่าย 
            function RequireCash() {
                calculateTotal();
                var required = parseInt($('#txtRequire').val().replace(',', ''));
                if (required > 0) {
                    // Gen URL
                    var url = '<% 
            Dim BL As New VDM_BL
            Response.Write(BL.LocalControllerURL)
                    %>/RequireCash.aspx?REQ=' + required + '&callback=updatePayment';
                    // 
                    return;
                    var script = document.createElement('script');
                    script.src = url;
                    var body = document.getElementsByTagName('body')[0];
                    body.appendChild(script);

                } else {
                    // จ่ายครบ
                    clearInterval(cashTimer);
                    $('#btnCashCompleted').click();
                }
            }
  
            var tryPay = 0;
            function updatePayment(amount,status,message) {
               
                if (status == 'true') {
                    /*-----------Lock Mode-----------*/
                    $('footer').css("visibility", "hidden");                   
                    $('#pnlSelectChoice').css("display", "none");
                    $('.btu-payment').css("visibility", "hidden");
                    /*---------Update Payment--------*/
                    var qty = $('#txt' + amount).val().replace(',', '');
                    qty = parseInt(qty) + 1;
                    $('#txt' + amount).val(qty);
                    RequireCash();
                } else {
                    if (tryPay == 0) {
                        tryPay += 1;
                        RequireCash();
                        return
                    }
                    clearInterval(cashTimer);
                    $('#txtCashProblem').val(message);
                    $('#btnCashProblem').click();
                }
            }

            function calculateTotal() {
                var money = [1, 2, 5, 10, 20, 50, 100, 500, 1000];
                var paid = 0;
                for (i = 0; i < money.length; i++) {
                    paid += parseInt($('#txt' + money[i]).val()) * money[i];
                }
                var display = paid.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                $('#txtPaid').val(display);
                var cost = parseInt($('#txtCost').val().replace(',',''));
                var required = cost - paid;
                if (required < 0) required = 0;
                display = required.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                $('#txtRequire').val(display);
                
            }

            var cashSec = 120;
            var cashCounter = function () {
                if (cashSec <= 0) {
                    $('#txtCashProblem').val('ชำระเกินระยะเวลาที่กำหนด');
                    $('#btnCashTimeout').click();
                    clearInterval(cashTimer);
                } else {
                    cashSec -= 1;
                    //$('#castTimeOut').html(cashSec);
                    //var min = Math.floor(cashSec / 60).toString().padStart(2, '0');
                    var min = (cashSec - (cashSec % 60)) / 60;
                    var displayMin = min.toString();                   
                  
                    if (displayMin.length < 2) {
                        displayMin = '0' + displayMin;
                    }
                    
                    var sec = (cashSec % 60);
                    var displaySec = sec.toString();
                    if (displaySec.length < 2) {
                        displaySec = '0' + displaySec;
                    }
                    // display
                    $('#castTimeOut').html(displayMin + ':' + displaySec);
                }
            }
            var cashTimer = setInterval(cashCounter, 1000);
            

        </script>

        <uc1:UC_CommonUI runat="server" ID="CommonUI" />

    </form>

</body>
</html>

