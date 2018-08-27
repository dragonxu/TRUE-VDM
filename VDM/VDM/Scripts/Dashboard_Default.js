/**
 * ChartJS chart page
 */
var helpers;
function initChart()
{
    helpers = Chart.helpers;

    // Define global settings
    Chart.defaults.global.responsive = true;
    Chart.defaults.global.scaleFontFamily = $.staticApp.font;
    Chart.defaults.global.scaleFontSize = 10;
    Chart.defaults.global.tooltipFillColor = $.staticApp.primary;
    Chart.defaults.global.tooltipFontFamily = $.staticApp.font;
    Chart.defaults.global.tooltipFontSize = 12;
    Chart.defaults.global.tooltipTitleFontFamily = $.staticApp.font;
    Chart.defaults.global.tooltipTitleFontSize = 13;
    Chart.defaults.global.tooltipTitleFontStyle = '700';
    Chart.defaults.global.tooltipCornerRadius = 0;
}

function render_Radar(radarChartData) {
    var radar = $('.radar').get(0).getContext('2d');
    var myRadar = new Chart(radar).Radar(radarChartData, {
        pointDotRadius: 0,
        pointLabelFontFamily: '\'Roboto\'',
        pointLabelFontSize: 10
    });
}

function render_Pie(pieData) {
    var pie = $('.pie').get(0).getContext('2d');
    var myPie = new Chart(pie).Pie(pieData, {
        segmentShowStroke: false
    });
}

function render_Bar(barData) {
    $.plot($('.bar'), barData, {
        grid: {
            hoverable: false,
            clickable: false,
            labelMargin: 8,
            color: $.staticApp.border,
            borderWidth: 0,
        },
        xaxis: {
            mode: 'time',
            timeformat: '%b',
            tickSize: [1, 'month'],
            monthNames: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
            tickLength: 0,
            axisLabel: 'Month',
            axisLabelUseCanvas: true,
            axisLabelFontSizePixels: 12,
            axisLabelFontFamily: 'Roboto',
            axisLabelPadding: 5
        }
    });
}

function render_Line(data) {
    // Line chart   
    $('.dashboard-line').length && $.plot($('.dashboard-line'), data, {
        series: {
            lines: {
                show: true,
                lineWidth: 0,
            },
            splines: {
                show: true,
                lineWidth: 1
            }
        },
        grid: {
            borderWidth: 1,
            color: 'rgba(255,255,255,0.2)',
        },
        yaxis: {
            color: 'rgba(255,255,255,0.1)',
        },
        xaxis: {
            mode: 'categories'
        }
    });
}

(function ($) {
    'use strict';
    initChart();
   
    ////Radar chart
    //var radarChartData = {
    //    labels: ['Eating', 'Drinking', 'Sleeping', 'Designing', 'Coding', 'Partying', 'Running'],
    //    datasets: [{
    //        fillColor: 'rgba(220,220,220,1)',
    //        strokeColor: 'rgba(220,220,220,1)',
    //        pointColor: 'rgba(220,220,220,1)',
    //        pointStrokeColor: '#fff',
    //        data: [65, 59, 90, 81, 56, 55, 40]
    //    }, {
    //        fillColor: 'rgba(151,187,205,1)',
    //        strokeColor: 'rgba(151,187,205,1)',
    //        pointColor: 'rgba(151,187,205,1)',
    //        pointStrokeColor: '#fff',
    //        data: [28, 48, 40, 19, 96, 27, 100]
    //    }]
    //};
    //render_Radar(radarChartData);

    //// Pie chart
    //var pieData = [{
    //    value: 300,
    //    color: $.staticApp.danger,
    //    highlight: LightenDarkenColor($.staticApp.danger, 20),
    //    label: 'Danger'
    //}, {
    //    value: 50,
    //    color: $.staticApp.success,
    //    highlight: LightenDarkenColor($.staticApp.success, 20),
    //    label: 'Success'
    //}, {
    //    value: 100,
    //    color: $.staticApp.warning,
    //    highlight: LightenDarkenColor($.staticApp.warning, 20),
    //    label: 'Warning'
    //}, {
    //    value: 40,
    //    color: $.staticApp.bodyBg,
    //    highlight: LightenDarkenColor($.staticApp.bodyBg, 20),
    //    label: 'Body'
    //}, {
    //    value: 120,
    //    color: $.staticApp.dark,
    //    highlight: LightenDarkenColor($.staticApp.dark, 20),
    //    label: 'Dark'
    //}];
    //render_Pie(pieData);
   

    // Bar graph
    //var barData = [{
    //    data: [
    //      [1391761856000, 80],
    //      [1394181056000, 40],
    //      [1396859456000, 20],
    //      [1399451456000, 20],
    //      [1402129856000, 50]
    //    ],
    //    bars: {
    //        show: true,
    //        barWidth: 7 * 24 * 60 * 60 * 1000,
    //        fill: true,
    //        lineWidth: 0,
    //        order: 1,
    //        fillColor: $.staticApp.info
    //    }
    //},
    //{
    //    data: [
    //      [1391761856000, 50],
    //      [1394181056000, 30],
    //      [1396859456000, 10],
    //      [1399451456000, 70],
    //      [1402129856000, 30]
    //    ],
    //    bars: {
    //        show: true,
    //        barWidth: 7 * 24 * 60 * 60 * 1000,
    //        fill: true,
    //        lineWidth: 0,
    //        order: 2,
    //        fillColor: $.staticApp.danger
    //    }
    //}, {
    //    data: [
    //      [1391761856000, 30],
    //      [1394181056000, 60],
    //      [1396859456000, 40],
    //      [1399451456000, 40],
    //      [1402129856000, 40]
    //    ],
    //    bars: {
    //        show: true,
    //        barWidth: 7 * 24 * 60 * 60 * 1000,
    //        fill: true,
    //        lineWidth: 0,
    //        order: 3,
    //        fillColor: $.staticApp.success
    //    }
    //}];
    //render_Bar(barData);


    // Line chart
    /*
    var visits = [
        ["AAAA", 200],
        ["BBBB", 200],
        ["CCCC", 400],
        ["DDDD", 320],
        ["EEEE", 100],
        ["FFFF", 500],
        ["GGGG", 300],
        ["HHHH", 132],
        ["IIII", 231]
    ];
    var visitors = [
      ["AAAA", 423],
      ["BBBB", 545],
      ["CCCC", 233],
      ["DDDD", 343],
      ["EEEE", 122],
      ["FFFF", 33],
      ["GGGG", 444],
      ["HHHH", 454],
      ["IIII", 866]
    ];
    var data = [{
        data: visits,
        color: '#fff'
    }, {
        data: visitors,
        color: 'yellow'
    }
    ];
    render_Line(data);*/
    
    $(window).resize(function () {
        resizeScreen();
    });
    resizeScreen();
})(jQuery);

function resizeScreen() {
    //$('#RadarBlock').height($('#BarBlock').height());
    //$('#NoticeBlock').height($('#BarBlock').height());
}