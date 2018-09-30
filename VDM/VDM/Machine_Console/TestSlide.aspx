<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TestSlide.aspx.vb" Inherits="VDM.TestSlide" %>
<!doctype html>
<html class="no-js" lang="">

<head>
  
    <meta charset="UTF-8">
    <title>AdminLTE 2 | UI Sliders</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <!-- Bootstrap 3.3.2 -->
    <link href="Slider/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Font Awesome Icons -->
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- Ionicons -->
    <link href="http://code.ionicframework.com/ionicons/2.0.0/css/ionicons.min.css" rel="stylesheet" type="text/css" />
    <!-- Ion Slider -->
    <link href="Slider/css/ion.rangeSlider.css" rel="stylesheet" type="text/css" />
    <!-- ion slider Nice -->
    <link href="Slider/css/ion.rangeSlider.skinNice.css" rel="stylesheet" type="text/css" />
    <!-- bootstrap slider -->
    <link href="Slider/css/slider.css" rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="Slider/css/AdminLTE.min.css" rel="stylesheet" type="text/css" />
    <!-- AdminLTE Skins. Choose a skin from the css/skins 
         folder instead of downloading all of them to reduce the load. -->
    <link href="Slider/css/_all-skins.min.css" rel="stylesheet" type="text/css" />

</head>

<body class="page-loading">
  <form id="form" runat ="server" >
    <div class="wrapper">
      

      <!-- Content Wrapper. Contains page content -->
      <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
          <h1>
            Sliders
            <small>range sliders</small>
          </h1>
          <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">UI</a></li>
            <li class="active">Sliders</li>
          </ol>
        </section>

        <!-- Main content -->
        <section class="content">
          <div class="row">
            <div class="col-xs-12">
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">Ion Slider</h3>
                </div><!-- /.box-header -->

                <div class="box-body">
                  <%--<div class="row margin">
                    <div class="col-sm-6">
                      <input id="range_1" type="text" name="range_1" value="" />
                    </div>

                    <div class="col-sm-6">
                      <input id="range_2" type="text" name="range_2" value="1000;100000" data-type="double" data-step="500" data-postfix=" &euro;" data-from="30000" data-to="90000" data-hasgrid="true" />
                    </div>
                  </div>--%>
                  <div class="row margin">
                    <div class="col-sm-6">
                      <input id="range_58" type="text" name="range_5" value="" />
                    </div>
                    <%--<div class="col-sm-6">
                      <input id="range_6" type="text" name="range_6" value="" />
                    </div>--%>
                  </div>
                  <%--<div class="row margin">
                    <div class="col-sm-12">
                      <input id="range_4" type="text" name="range_4" value="10000;100000" />
                    </div>
                  </div>--%>
                </div>
                  


                <asp:TextBox ID="txt_Score" runat="server" placeholder="score" Text ="300"  ></asp:TextBox>
                  <a id="link" runat ="server"  ><span >CLICK</span></a>
                <div class="box-body">
                  <div class="row margin">
                    <div class="col-sm-6">
                      <input id="range_1" type="text" name="range_1" value="" />
                    </div>

                    <div class="col-sm-6">
                      <input id="range_2" type="text" name="range_2" value="1000;100000" data-type="double" data-step="500" data-postfix=" &euro;" data-from="30000" data-to="90000" data-hasgrid="true" />
                    </div>
                  </div>
                  <div class="row margin">
                    <div class="col-sm-6">
                      <input id="range_5" runat ="server"  type="text" name="range_5" value="" />
                    </div>
                    <div class="col-sm-6">
                      <input id="range_6" type="text" name="range_6" value="" />
                    </div>
                  </div>
                  <div class="row margin">
                    <div class="col-sm-12">
                      <input id="range_4" type="text" name="range_4" value="10000;100000" />
                    </div>
                  </div>
                </div>











                  <!-- /.box-body -->
              </div><!-- /.box -->
            </div><!-- /.col -->
          </div><!-- /.row -->

        </section><!-- /.content -->
      </div><!-- /.content-wrapper -->
      <footer class="main-footer">
        <div class="pull-right hidden-xs">
          <b>Version</b> 2.0
        </div>
        <strong>Copyright &copy; 2014-2015 <a href="http://almsaeedstudio.com">Almsaeed Studio</a>.</strong> All rights reserved.
      </footer>
    </div><!-- ./wrapper -->


</form>





    
    <!-- jQuery 2.1.3 -->
    <script src="Slider/js/jQuery-2.1.3.min.js"></script>
    <!-- Bootstrap 3.3.2 JS -->
    <script src="Slider/js/bootstrap.min.js" type="text/javascript"></script>
    <!-- FastClick -->
    <script src='Slider/js/fastclick.min.js'></script>
    <!-- AdminLTE App -->
    <script src="Slider/js/app.min.js" type="text/javascript"></script>
    <!-- AdminLTE for demo purposes -->
    <script src="Slider/js/demo.js" type="text/javascript"></script>
    <!-- Ion Slider -->
    <script src="Slider/js/ion.rangeSlider.min.js" type="text/javascript"></script>

    <!-- Bootstrap slider -->
    <script src="Slider/js/bootstrap-slider.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(function initSlider(elementID, max, step, value, txtScore) {

            $('.slider').slider();

            //$("#" + elementID).ionRangeSlider({
            //    min: 0,
            //    max: max,
            //    type: 'single',
            //    step: step,
            //    value: value,                    
            //    postfix: " บาท",
            //    prettify: false,
            //    hasGrid: true
            //});

            //$("#" + txtScore).val(value); // Value
            /* BOOTSTRAP SLIDER */
            var x = document.getElementById("txt_Score").value;
            var d = elementID;
            $("#range_5").ionRangeSlider({
              min: 0,
              max: x,
              type: 'single',
              step: step,
              postfix: " บาท",
              prettify: false,
              hasGrid: true
            });
            $("#" + txt_Score).val(value); // Value
        })

        //function initSlider1(elementID, value, min, max, pass, spanScore, txtScore, btnUpdate, ckConfirm) {
        //    $("#" + elementID).slider({
        //        range: "max",
        //        min: min,
        //        max: max,
        //        value: value,
        //        slide: function (event, ui) {
        //            if (ui.value < pass) setSliderColor(elementID, 'red');
        //            else setSliderColor(elementID, 'green');

        //            $("#" + spanScore).text(ui.value + '/' + max); // Display
        //            $("#" + txtScore).val(ui.value); // Value    

        //        },
        //        stop: function (event, ui) {
        //            $("#" + btnUpdate).click();
        //        }
        //    });
        //    if ($("#" + elementID).slider("value") < pass) setSliderColor(elementID, 'red'); else setSliderColor(elementID, 'green');
        //    $("#" + txtScore).val(value); // Value  
        //    $("#" + spanScore).text($("#" + elementID).slider("value") + '/' + max);
        //}




        $(function (elementID, max, step, value, txtScore) {
        /* BOOTSTRAP SLIDER */
        $('.slider').slider();

        /* ION SLIDER */
        $("#range_1").ionRangeSlider({
          min: 0,
          max: 5000,
          from: 1000,
          to: 4000,
          type: 'double',
          step: 1,
          prefix: "$",
          prettify: false,
          hasGrid: true
        });
        $("#range_2").ionRangeSlider();

        //$("#range_5").ionRangeSlider({
        //  min: 0,
        //  max: 10,
        //  type: 'single',
        //  step: 1,
        //  postfix: " บาท",
        //  prettify: false,
        //  hasGrid: true
        //});


        //$("#" + elementID).ionRangeSlider({
        //    min: 0,
        //    max: max,
        //    type: 'single',
        //    step: step,
        //    value: value,
        //    postfix: " บาท",
        //    prettify: false,
        //    hasGrid: true

        //});
        //$("#" + txtScore).val(value); // Value 



        $("#range_6").ionRangeSlider({
          min: -50,
          max: 50,
          from: 0,
          type: 'single',
          step: 1,
          postfix: "°",
          prettify: false,
          hasGrid: true
        });

        $("#range_4").ionRangeSlider({
          type: "single",
          step: 100,
          postfix: " light years",
          from: 55000,
          hideMinMax: true,
          hideFromTo: false
        });
        $("#range_3").ionRangeSlider({
          type: "double",
          postfix: " miles",
          step: 10000,
          from: 25000000,
          to: 35000000,
          onChange: function (obj) {
            var t = "";
            for (var prop in obj) {
              t += prop + ": " + obj[prop] + "\r\n";
            }
            $("#result").html(t);
          },
          onLoad: function (obj) {
            //
          }
        });
      });
    </script>



</body>

</html>