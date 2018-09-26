<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Scan_IDCart.ascx.vb" Inherits="VDM.UC_Scan_IDCart" %>
      
              
          <h4 class="true-m">กรุณาสอดบัตรประชาชนหรือพาสปอร์ต<br/>เพื่อชำระเงิน</h4>                   
          <div class="idcard">
             <img src="images/pic-idcard.png"/> 
          </div>

          <h3 class="true-m red"><asp:Label ID="lblCountdown" runat ="server" Text ="" ></asp:Label></h3>   
<p><h3 class="true-m red"><span id="countdowntimer">20 </span> วินาที</h3></p>

                                                <script type="text/javascript">
                                                    var timeleft = 20;
                                                    var downloadTimer = setInterval(function ()
                                                    {
                                                    timeleft--;
                                                    document.getElementById("countdowntimer").textContent = timeleft;

                                                    if (timeleft <= 0)
                                                        clearInterval(downloadTimer);
                                                     
                                                    },1000);
                                                </script>
                            

       
