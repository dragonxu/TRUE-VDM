<%@ Page Title="" Language="vb" AutoEventWireup="false" CodeBehind="Complete_Order.aspx.vb" Inherits="VDM.Complete_Order" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no">
    <title>Kiosk</title>
    <link href="css/true.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.min.css" rel="stylesheet">
    <link href="css/bootstrap-select.css" rel="stylesheet">
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
        <div class="warp">
            <header><img src="images/bg-top.png" /></header>
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
                            <div class="step-payment" style ="padding :10px 0 100px 0;">
                            <h4 class="margin-re true-m">ท่านได้ชำระค่าสินค้าแล้ว</h4>
                                </div>
                            <img src="images/icon-pack.png">
                            <div class="step-payment">


                                <h4 class="margin-re true-m">กรุณารอสักครู่...</h4>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:TextBox ID="txtSlotName" runat="server"  Style="display:none;"></asp:TextBox>
                            <asp:TextBox ID="txtPosID" runat="server"  Style="display:none;" Text="0"></asp:TextBox>
                            <asp:TextBox ID="txtStatus" runat="server"  Style="display:none;"></asp:TextBox>
                            <asp:TextBox ID="txtMessage" runat="server"  Style="display:none;"></asp:TextBox>
                            <asp:Button ID="btnNext" runat="server" Style="display:none; /*background: #635b5b; padding: 0 50px 0 50px; float: right; margin-top: 100px;*/" Text="ข้าม" />
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
        </ContentTemplate>
        </asp:UpdatePanel>
        </div>

        <script type="text/javascript">

            function pickProduct() {
                // Gen URL
                var url = '<% 
            Dim BL As New VDM_BL
            Response.Write(BL.LocalControllerURL)
                    %>/ProductPicker.aspx?Mode=GoPick&OpenTimeOut=10&POS_ID=' + $('#txtPosID').val() + '&callback=picked';
                alert(url);
                    var script = document.createElement('script');
                    script.src = url;
                    var body = document.getElementsByTagName('body')[0];
                    body.appendChild(script);
            }

            function picked(status, message) {

                $('#txtStatus').val(status);
                $('#txtMessage').val(message);
                if (status) {
                    //$('#btnNext').click();
                } else {
                    // ไม่เจอ Product
                }
                $('#btnNext').click();
            }
        </script>

    </form>
</body>
</html>
