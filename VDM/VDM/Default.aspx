<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Default.aspx.vb" Inherits="WEB._Default" %>
<asp:Content ID="HeaderContainer" ContentPlaceHolderID="HeaderContainer" runat="server">
  <!-- page stylesheets -->
  <link rel="stylesheet" href="vendor/c3/c3.min.css">
  <!-- end page stylesheets -->
  <style type="text/css">
      .m-t-n-g {
        margin-top:-1.5rem !important;
      }
      .notifications .notifications-list li a {
        padding: 0.5rem;
      }
  </style>
  <script src="vendor/jquery/dist/jquery.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
      <%--Line--%>
        <div class="m-x-n-g m-t-n-g overflow-hidden">
          <div class="card m-b-0 bg-primary-dark text-white p-a-md no-border">
            <h5 class="m-t-0">
              <span class="pull-right"><asp:Label ID="lblSalesValues" runat="server"></asp:Label> Last 2 weeks</span>
              <span>Serviced Transactions</span>
              </h5>
            <div class="chart dashboard-line labels-white" style="height:150px"></div>
          </div>
          <div class="card bg-white no-border">
            <div class="row text-center">
              <div class="col-sm-3 col-xs-6 p-t">
                <h4 class="m-t-0 m-b-0"><asp:Label ID="lblDailySales" runat="server"></asp:Label></h4>
                <small class="text-muted bold">Daily Serviced</small>
              </div>
              <div class="col-sm-3 col-xs-6 p-t">
                <h4 class="m-t-0 m-b-0"><asp:Label ID="lblWeeklySales" runat="server"></asp:Label></h4>
                <small class="text-muted bold">Weekly Serviced</small>
              </div>
              <div class="col-sm-3 col-xs-6 p-t">
                <h4 class="m-t-0 m-b-0"><asp:Label ID="lblMonthlySales" runat="server"></asp:Label></h4>
                <small class="text-muted bold">Monthly Serviced</small>
              </div>
              <div class="col-sm-3 col-xs-6 p-t">
                <h4 class="m-t-0 m-b-0"><asp:Label ID="lblYearlySales" runat="server"></asp:Label></h4>
                <small class="text-muted bold">Yearly Serviced</small>
              </div>
            </div>
          </div>
        </div>

        <%--Bar--%>
        <div class="col-sm-8">
            <div class="card bg-white" id="BarBlock">
                <div class="card-block text-center p-t-0">
                    <h5 class="text-primary">Annual Serviced Transactions</h5>
                    <div class="chart bar" style="height: 200px"></div>
                    
                    <a href="javascript:;" class="btn btn-info btn-xs">Completed Transaction</a>
                    <%--<a href="javascript:;" class="btn btn-danger btn-xs">Lost Transaction</a>--%>

                </div>
            </div>
        </div>

        <%--Radar--%>
        <div class="col-sm-4">
            <div class="card bg-white" id="RadarBlock">
                <div class="card-block text-center p-t-0" style="height:unset;">
                   <h5 class="text-primary">Frequency Service Time in This Month</h5>
                    <div class="canvas-holder">
                        <canvas class="radar" height="200"></canvas>
                    </div>
                    <a href="javascript:;" class="btn btn-info btn-xs">Completed Transaction</a>
                    <%--<a href="javascript:;" class="btn btn-danger btn-xs">Lost Transaction</a>--%>
                </div>
            </div>
            <%--Pie
            <div class="card bg-white" style="display:none;">
                <div class="card-block text-center">
                    <span class="text-green">XXXXXXXXXXXXXXXXXXX </span>
                    <div class="canvas-holder">
                        <canvas class="pie"></canvas>
                    </div>
                </div>
            </div>--%>
        </div>

        <%--<div class="col-md-4">
            <div class="card bg-white no-border"  id="NoticeBlock" style="overflow-y:scroll;">
              <div class="p-a bb text-danger text-center">
                NOTIFICATIONS
              </div>
              <asp:Literal ID="lblNotificationList" runat="server"></asp:Literal>              
            </div>            
          </div>--%>
     </div>
</asp:Content>
<asp:Content ID="ScriptContainer" ContentPlaceHolderID="ScriptContainer" runat="server">

    <!-- page scripts -->
    <script src="scripts/helpers/colors.js"></script>
    <script src="vendor/Chart.js/Chart.min.js"></script>
    <script src="vendor/flot/jquery.flot.js"></script>
    <script src="vendor/flot/jquery.flot.resize.js"></script>
    <script src="vendor/flot/jquery.flot.categories.js"></script>
    <script src="vendor/flot/jquery.flot.stack.js"></script>
    <script src="vendor/flot/jquery.flot.time.js"></script>
    <script src="vendor/flot/jquery.flot.pie.js"></script>
    <script src="vendor/flot-spline/js/jquery.flot.spline.js"></script>
    <script src="vendor/flot.orderbars/js/jquery.flot.orderBars.js"></script>
    <script src="vendor/d3/d3.min.js" charset="utf-8"></script>
    <script src="vendor/c3/c3.min.js"></script>
    <!-- end page scripts -->
      <!-- initialize page scripts -->
      <script src="scripts/Dashboard_Default.js" type="text/javascript"></script>
      <!-- end initialize page scripts -->

</asp:Content>
