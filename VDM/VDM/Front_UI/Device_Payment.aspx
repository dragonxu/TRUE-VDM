<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Device_Payment.aspx.vb" Inherits="VDM.Device_Payment" %>

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
                        <div class="pic-step">
                            <p class="t-cart t-red true-b">SHOPING CART</p>
                            <p class="t-payment t-red true-b">PAYMENT</p>
                            <p class="t-complete true-b">COMPLETE ORDER</p>
                            <img src="images/pic-step2.png" />
                        </div>
                        <div class="step" id="formStep" runat="server">
                            <div class="step-payment">
                                <h3 class="true-b t-red">
                                    <asp:Label ID="lblPrice_str" runat="server" Text="ยอดชำระ"></asp:Label>
                                    <i title="฿">
                                        <asp:TextBox ID="txtCost" runat="server" Text="0" style="text-align: center;width: 300px;background-color: transparent;border: none;margin-top: -15px;" ReadOnly ="true"></asp:TextBox>

                                    </i>
                                    <asp:Label ID="lblCurrency_Str" runat="server" Text="บาท"></asp:Label></h3>
                                <div class="btu-payment" style="margin: 60px 0 0 0;">
                                    <div class="row" style="margin-left: 100px;">
<%--                                        <a class="current" id="lnkCash" runat ="server" >--%>
                                        <a  id="lnkCash" runat ="server" >
                                            <dt class="icon-cash">
                                                <span class="true-l">เงินสด<span>
                                            </dt>
                                        </a>
                                        <a  id="lnkCredit" runat ="server" >
                                            <dt class="icon-credit">
                                                <span class="true-l">บัตรเครดิต
                                                    <p class="text">บัตรเดบิต บัตรเงินสด</p>
                                                    <span></dt>
                                        </a>
                                        <a  id="lnkTruemoney" runat ="server" >
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
                                <%-- <asp:Panel ID="pnlSelectChoice" runat="server">
                                    <h4 class="margin true-m" style="margin-top: 60px;">เลือกวิธีชำระเงิน</h4>
                                </asp:Panel>--%>
                            </div>
                        </div>
                        
                        <asp:Panel ID="pnlCash" runat="server">
                            <form class="step">
                                <div class="step-payment">

                                    <h4 class="true-m">ชำระด้วยเงินสด</h4>

                                    <div class="f-remark">
                                        <p class="true-m">
                                            กรุณาเตรียมเงินให้พร้อม<br />
                                            เพราะเมื่อเริ่มชำระเงินแล้วจะ<u class="t-red">ไม่สามารถยกเลิกได้</u>
                                        </p>
                                        <p class="sub true-l">(หากจำเป็นต้องยกเลิก โปรดติดต่อพนักงาน)</p>
                                        <em></em>
                                        <p class="true-m">โปรดสอดธนบัตรทางช่องด้านขวา</p>
                                        <p class="sub true-l">(รับเฉพาะเงินบาทไทยเท่านั้น)</p>
                                    </div>
                                    <div class="f-cash">
                                        <div class="col-md-4" style="text-align: center;">
                                            <h3 class="true-b">ชำระแล้ว</h3>
                                        </div>
                                        <div class="col-md-6" style="text-align: center;">
                                            <h3>
                                                <p style="border: unset; padding-right: 30px; text-align: right;"> 
                                                    <asp:TextBox  class="true-b " ID="txtPaid" runat="server" Text="0" style="text-align: right;width: 300px;float: right;background-color: transparent;border: none;margin-top: -15px;"></asp:TextBox>
                                                </p>
                                            </h3>
                                        </div>
                                        <div class="col-md-2" style="text-align: center;">
                                            <h3 class="true-b">บาท</h3>
                                        </div>
                                        <div class="col-md-12" style="padding: 20px;"></div>

                                        <div class="col-md-4" style="text-align: center;">
                                            <h3 class="true-b t-red">เหลือ</h3>
                                        </div>
                                        <div class="col-md-6" style="text-align: center;">
                                            <h3>
                                                <p style="text-align: right; padding-right: 30px;"> 
                                                    <asp:TextBox   class="true-b  t-red" ID="txtRequire" runat="server" Text="0" style="text-align: right;width: 300px;float: right;background-color: transparent;border: none;margin-top: -5px;"></asp:TextBox>
                                                </p>
                                            </h3>
                                        </div>
                                        <div class="col-md-2" style="text-align: center;">
                                            <h3 class="true-b t-red">บาท</h3>
                                        </div>
                                    </div>
                                    <p class="time true-l">เวลาชำระเงินสดคงเหลือ 2:00:00 นาที</p>
                                </div>

                                <div style="position:absolute; top:100px;right:100px;" class="row">
                                    <div style="display:block;" class="col-lg-3">
                                        1 : <asp:TextBox ID="txt1" runat="server" Text="0"></asp:TextBox>
                                    </div>
                                    <div style="display:block;" class="col-lg-3">
                                        2 : <asp:TextBox ID="txt2" runat="server" Text="0"></asp:TextBox>
                                    </div>
                                    <div style="display:block;" class="col-lg-3">
                                        5 : <asp:TextBox ID="txt5" runat="server" Text="0"></asp:TextBox>
                                    </div>
                                    <div style="display:block;" class="col-lg-3">
                                        10 : <asp:TextBox ID="txt10" runat="server" Text="0"></asp:TextBox>
                                    </div>
                                    <div style="display:block;" class="col-lg-3">
                                        20 : <asp:TextBox ID="txt20" runat="server" Text="0"></asp:TextBox>
                                    </div>
                                    <div style="display:block;" class="col-lg-3">
                                        50 : <asp:TextBox ID="txt50" runat="server" Text="0"></asp:TextBox>
                                    </div>
                                    <div style="display:block;" class="col-lg-3">
                                        100 : <asp:TextBox ID="txt100" runat="server" Text="0"></asp:TextBox>
                                    </div>
                                    <div style="display:block;" class="col-lg-3">
                                        500 : <asp:TextBox ID="txt500" runat="server" Text="0"></asp:TextBox>
                                    </div>
                                    <div style="display:block;" class="col-lg-3">
                                        1000 : <asp:TextBox ID="txt1000" runat="server" Text="0"></asp:TextBox>
                                    </div> 
                                    <div style="display:block;" class="col-lg-3">
                                        รวมยอด : <%--<asp:TextBox ID="txtCost" runat="server" Text="0"></asp:TextBox>--%>
                                    </div>  
                                    <div style="display:block;" class="col-lg-3">
                                        จ่าย : <%--<asp:TextBox ID="txtPaid" runat="server" Text="0"></asp:TextBox>--%>
                                    </div>
                                    <div style="display:block;" class="col-lg-3">
                                        เหลือ : <%--<asp:TextBox ID="txtRequire" runat="server" Text="0"></asp:TextBox>--%>
                                    </div>  
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
                                         <div class="col-md-12" style ="text-align :center ;">
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
                                                <img src="images/icon-app.png" /></div>
                                            <p class="true-m">
                                                เปิดแอป TrueMoney Wallet<br />
                                                บนมือถือของท่าน
                                            </p>
                                        </div>
                                        <div class="f-app">
                                            <div class="pic">
                                                <img src="images/qr-code2.png" /></div>
                                            <p class="true-m">
                                                และสแกน QR Code
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </asp:Panel>
                        <div class="col-md-12">
                                    <asp:Button ID="btnSkip" runat="server" class="btu true-bs" Style="background: #635b5b; padding: 0 50px 0 50px; float: right; margin-top: 100px;" Text="ต่อไป" />
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

                function RequireCash()
                {
                    calculateTotal();
                    var required = parseInt($('#txtRequire').val());
                    if (required > 0) {
                        // Call Payment
                        var url = "http://localhost/RequireCash.aspx?REQ=" + required;
                        var xhr = new XMLHttpRequest();
                        xhr.open('GET', url + '&callback=updatePayment', true);
                        xhr.send();
                    } else {
                        // จ่ายครบ
                    }
                }
                
                
                var tryReq = 1;
                updatePayment = function (data)
                {
                    var status = data.data.status;
                    var message = data.data.message;
                    var amount = data.data.amount;
                    if (status) {
                        tryReq = 0;
                        /*---------Update Payment--------*/
                        $('#txt' + amount).val(parseInt($('#txt' + amount).val()) + 1);
                        RequireCash();
                    } else {
                        tryReq += 1;
                        ////Toastr Problem Message
                        if (tryReq > 3)
                        {
                            //  Error เกิน 3 ครั้ง
                        }
                    }
                }

                function calculateTotal()
                {
                    var money = [1, 2, 5, 10, 20, 50, 100, 500, 1000];
                    var paid = 0;
                    for (i = 0; i < money.length; i++)
                    {
                        paid += parseInt($('#txt' + money[i]).val()) * money[i];
                    }
                    $('#txtPaid').val(paid);
                    var cost = parseInt($('#txtCost').val());
                    var required = cost - paid;
                    $('#txtRequire').val(required);
                }        
                RequireCash(); // เริ่มจ่าย

            </script>

    </form>
    
</body>
</html>

