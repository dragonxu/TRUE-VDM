<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Dialog.ascx.vb" Inherits="VDM.UC_Dialog" %>

<asp:Panel ID="pnlModul" runat="server" Visible ="False" >
                <div class="fancybox-overlay fancybox-overlay-fixed" style="width: auto; height: auto; display: block;">
                    <div class="fancybox-wrap fancybox-desktop fancybox-type-inline fancybox-opened" tabindex="-1" style="width: 818px; height: auto; position: absolute; top: 500px; left: 250px; opacity: 1; overflow: visible;">
                        <div class="fancybox-skin" style="padding: 0px; width: auto; height: auto;">
                            <div class="fancybox-outer">
                                <div class="fancybox-inner" style="overflow: auto; width: 818px; height: auto;">
                                    <div id="popup2" style="display: block;">

                                        <div class="privilege">
<%--                                            <h3 class="true-m half">หากท่านต้องการ<br/>ใบเสร็จรับเงินฉบับจริง<br/>หรือใบกำกับภาษี</h3>--%>

                                            <h3 class="true-m half"><asp:Label ID="lbl_HeaderMessage" runat ="server" ></asp:Label></h3>
                                            <div class="idcard">
                                                <img id="imgAlert" runat ="server" src="images/Confirm_Contact_CUST.png">
                                            </div>
<%--<h4 class="true-b">กรุณาติดต่อพนักงานก่อนทำรายการ</h4>--%>
                                            <h4 class="true-b"><asp:Label ID="lbl_DetailMessage" runat ="server" ></asp:Label></h4>
                                            <div class="icon" style="margin: 0px 0 50px 0">
                                                <asp:LinkButton ID="btnClose_Dialog" runat="server" class="btu true-l" Text="ดำเนินการต่อ"></asp:LinkButton>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div> 
                             
                        </div>
                    </div>
                </div>

            </asp:Panel>
