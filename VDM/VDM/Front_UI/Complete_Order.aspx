<%@ Page Title="" Language="vb" AutoEventWireup="false" CodeBehind="Complete_Order.aspx.vb" Inherits="VDM.Complete_Order" %>

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
    <link href="css/true-popup.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery.fancybox.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.js"></script>
    <script type="text/javascript" src="js/jquery.fancybox.js"></script>
    <script type="text/javascript" src="../Scripts/extent.js"></script>

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
                <img src="images/bg-top.png" /></header>
            <%--<main>
  <div class="priceplan">
    <div class="main3">
      <div class="pic-step">
        <p class="t-cart t-red true-b">SHOPING CART</p>
        <p class="t-payment t-red true-b">PAYMENT</p>
        <p class="t-complete t-red true-b">COMPLETE ORDER</p>
        <img src="images/pic-step3.png"/>
      </div>
      <div class="step">
        <div class="step-payment">
          <h4 class="margin-re true-m">ท่านได้ชำระค่าสินค้าแล้ว</h4>
          <div class="b-rceipt">
            <p class="true-m">ต้องการรับใบเสร็จหรือไม่?</p>
            <p class="sub true-m">(ในการเคลมสินค้าจะต้องแสดงใบเสร็จรับเงิน)</p>
            <span>
              <a class="red true-bs" href="receipt.html">ต้องการรับ</a>
              <a class="true-bs " href="pack.html">ไม่ต้องการรับ</a>
            </span>
          </div>
        </div>
      </div>
    </div>
  </div>
</main>--%>
            <asp:ScriptManager ID="scp" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UDP" runat="server">
                <ContentTemplate>



                    <main>
                        <div class="priceplan">
                            <div class="main3">
                                <div class="pic-step">
                                    <p class="t-cart t-red true-b">SHOPING CART</p>
                                    <p class="t-payment t-red true-b">PAYMENT</p>
                                    <p class="t-complete t-red true-b">COMPLETE ORDER</p>
                                    <img src="images/pic-step4.png" />
                                </div>
                                <div class="pack">
                                    <div class="step-payment" style="padding: 10px 0 100px 0;">
                                        <h4 class="margin-re true-m">ท่านได้ชำระค่าสินค้าแล้ว</h4>
                                    </div>
                                    <img src="images/icon-pack-gif.gif">
                                    <img src="images/icon-Shadow.png">
                                    <div class="step-payment">


                                        <h4 class="margin-re true-m">กรุณารอสักครู่...</h4>
                                    </div>
                                </div>
                                <div class="col-md-12">

                                    <asp:TextBox ID="txtLocalControllerURL" runat="server" Style="display: none;"></asp:TextBox>
                                    <asp:TextBox ID="txtSerial" runat="server" Style="display: none;"></asp:TextBox>
                                    <asp:TextBox ID="txtSlotID" runat="server" Style="display: none;"></asp:TextBox>
                                    <asp:TextBox ID="txtSlotName" runat="server" Style="display: none;"></asp:TextBox>
                                    <asp:TextBox ID="txtPosID" runat="server" Style="display: none;" Text="0"></asp:TextBox>
                                    <asp:TextBox ID="txtStatus" runat="server" Style="display: none;"></asp:TextBox>
                                    <asp:TextBox ID="txtMessage" runat="server" Style="display: none;"></asp:TextBox>
                                    <asp:Button ID="btnNext" runat="server" Style="display: none; /*background: #635b5b; padding: 0 50px 0 50px; float: right; margin-top: 100px; */" Text="ข้าม" />
                                    <asp:Button ID="btnValidatePrepaid" runat="server" Style="display: none;" />
                                </div>

                                <asp:Panel ID="pnlBarcode" runat="server" DefaultButton="btnBarcode" Style="position: fixed; left: 500px; top: 100px;">
                                    <asp:TextBox ID="txtBarcode" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnBarcode" runat="server" />
                                    <button type="button" id="btnFocus" value="focus" />
                                </asp:Panel>

                            </div>
                        </div>
                    </main>
                    <footer style="bottom: 0px">
                        <nav>
                            <div class="main">
                                <span class="col-md-6">
                                    <asp:ImageButton ID="lnkHome" runat="server" ImageUrl="images/btu-home.png" Visible="false" />
                                </span>
                                <span class="col-md-6">
                                    <asp:ImageButton ID="lnkBack" runat="server" ImageUrl="images/btu-prev.png" Visible="false" />
                                </span>
                            </div>

                        </nav>
                    </footer>



                    <asp:Panel ID="pnltxt" runat="server" Visible ="false" >
                        <tr style="vertical-align: top;">
                            <td style="line-height: 2;">
                                <table>

                                    <tr>
                                        <td>Face_cust_certificate</td>
                                        <td>
                                            <asp:TextBox ID="Face_cust_certificate" runat="server" TextMode="MultiLine" Style="margin: 0px; width: 161px; height: 131px;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Face_cust_capture</td>
                                        <td>
                                            <asp:TextBox ID="Face_cust_capture" runat="server" TextMode="MultiLine" Style="margin: 0px; width: 161px; height: 131px;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>USER_ID</td>
                                        <td>
                                            <asp:TextBox ID="USER_ID" runat="server" Text="00001"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btn_Request" runat="server" Text="OK" /></td>
                                    </tr>

                                </table>
                            </td>
                            <td>&nbsp;</td>
                            <td style="line-height: 2;">
                                <span>

                                    <br />
                                    "Register" :
                                    <asp:TextBox ID="ReturnResult" runat="server"></asp:TextBox>
                                    <br />
                                    "Command_Result.status" :
                                    <asp:TextBox ID="status" runat="server"></asp:TextBox>
                                    <br />
                                    "Command_Result.Message" :
                                    <asp:TextBox ID="Message" runat="server"></asp:TextBox>
                                </span>
                            </td>
                        </tr>
                    </asp:Panel>


                    <div id="popupCannotRegister" class="popup">
                        <div class="popup-frame">
                            <h3 class="true-m" id="idCardAlertReason">ขออภัย ระบบไม่สามารถลงทะเบียนซิมได้</h3>
                            <div class="icon"><img src="images/Popup/icon-warning-face.png" /></div>
                            <h4 class="true-b"><asp:Label ID="lblMSG" runat ="server" ></asp:Label></h4>
                            <div class="bottom">
                                <a class="btu true-l" id="lnkprint" runat="server">ตกลง</a>
                            </div>
                        </div>
                    </div>
                    <a id="clickCannotRegister" style="display:none;"  href="#popupCannotRegister">Click</a>


                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <script type="text/javascript">

            $("#clickCannotRegister").fancybox();

            function pickProduct() {
                // Gen URL
                var url = $('#txtLocalControllerURL').val() + '/ProductPicker.aspx?Mode=GoPick&OpenTimeOut=10&POS_ID=' + $('#txtPosID').val() + '&callback=productPicked';

                var script = document.createElement('script');
                script.src = url;
                var body = document.getElementsByTagName('body')[0];
                body.appendChild(script);
            }

            function productPicked(status, message) {

                $('#txtStatus').val(status);
                $('#txtMessage').val(message);
                /*
                if (status) {
                    //$('#btnNext').click();
                } else {
                    // ไม่เจอ Product
                }*/
                $('#btnNext').click();
            }

            function pullSIM() {

                var url = $('#txtLocalControllerURL').val() + '/SIMPicker.aspx?Mode=PULL&TimeOut=15&SLOT_ID=' + $('#txtSlotID').val() + '&callback=pulled';
                var script = document.createElement('script');
                script.src = url;
                var body = document.getElementsByTagName('body')[0];
                body.appendChild(script);
            }

            
            function pulled(status) {
                if (status == 'true') {
                    rotateSIMToScan();
                } else {
                    // ดึงจน TimeOut
                }
            }

            function rotateSIMToScan() {
                var url = $('#txtLocalControllerURL').val() + '/SIMPicker.aspx?Mode=Back&TimeOut=10&callback=';
                var script = document.createElement('script');
                script.src = url;
                var body = document.getElementsByTagName('body')[0];
                body.appendChild(script);
                $('#txtBarcode').change(breakSIMSlot);
                $('#txtBarcode').focus();
            }

            function breakSIMSlot() {
                var url = $('#txtLocalControllerURL').val() + '/SIMPicker.aspx?Mode=Break&callback=';
                var script = document.createElement('script');
                script.src = url;
                var body = document.getElementsByTagName('body')[0];
                body.appendChild(script);
            }

            function sendSIMValidation() {
                setTimeout(function () { $('#btnValidatePrepaid').click(); }, 5000);
                $('#btnValidatePrepaid').click();
            }

            function sendSIMToCustomer() {
                $('#btnFocus').focus();
                var url = $('#txtLocalControllerURL').val() + '/SIMPicker.aspx?Mode=Forward&TimeOut=15&OpenTimeOut=10&callback=simPicked';
                var script = document.createElement('script');
                script.src = url;
                var body = document.getElementsByTagName('body')[0];
                body.appendChild(script);
            }

            function simPicked(status) {
                $('#btnNext').click();
            }


        </script>
        <uc1:UC_CommonUI runat="server" ID="UC_CommonUI" />
    </form>

</body>
</html>
