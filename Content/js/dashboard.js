$(document).ready( function(){
  var wheight = $(window).height()- 0;
  $('.main-dashboard').css("min-height", wheight + "px");  
});	


var doughnutData = [
            {value:90,color:"#58c94c"},
            {value:100-90,color:"#e1e7f0"}
        ];
        $( "#circle1" ).doughnutit({
            dnData: doughnutData,
            dnSize: 120,
            dnInnerCutout: 70,
            dnAnimation: true,
            dnAnimationSteps: 50,
            dnAnimationEasing: 'linear',
            dnShowText: true,
            dnFontSize: '70px',
            dnFontColor: "#819596",
            dnText: '',
            dnStartAngle: 30,
        });
var doughnutDataTwo = [
            {value:60,color:"#fe0984"},
            {value:100-60,color:"#e1e7f0"}
        ];
        $( "#circle2" ).doughnutit({
            dnData: doughnutDataTwo,
            dnSize: 120,
            dnInnerCutout: 70,
            dnAnimation: true,
            dnAnimationSteps: 50,
            dnAnimationEasing: 'linear',
            dnShowText: true,
            dnFontSize: '70px',
            dnFontColor: "#819596",
            dnText: '',
            dnStartAngle: 30,
        });
var doughnutDataThree = [
            {value:60,color:"#208ed5"},
            {value:100-60,color:"#e1e7f0"}
        ];
        $( "#circle3" ).doughnutit({
            dnData: doughnutDataThree,
            dnSize: 120,
            dnInnerCutout: 70,
            dnAnimation: true,
            dnAnimationSteps: 50,
            dnAnimationEasing: 'linear',
            dnShowText: true,
            dnFontSize: '70px',
            dnFontColor: "#819596",
            dnText: '',
            dnStartAngle: 30,
});





window.onload = function () {
var options = {
	exportEnabled: true,
	animationEnabled: true,
	title:{
		text: ""
	},
	subtitles: [{
		text: ""
	}],
	axisX: {
		title: ""
	},
	axisY: {
		title: "",
		titleFontColor: "#4F81BC",
		lineColor: "#4F81BC",
		labelFontColor: "#4F81BC",
		tickColor: "#4F81BC",
		includeZero: false
	},
	axisY2: {
		title: "",
		titleFontColor: "#C0504E",
		lineColor: "#C0504E",
		labelFontColor: "#C0504E",
		tickColor: "#C0504E",
		includeZero: false
	},
	axisY3: {
		title: "",
		titleFontColor: "#ff0000",
		lineColor: "#ff0000",
		labelFontColor: "#ff0000",
		tickColor: "#ff0000",
		includeZero: false
	},
	toolTip: {
		shared: false
	},
	legend: {
		cursor: "pointer",
		itemclick: toggleDataSeries
	},
	data: [{
		type: "spline",
		name: "",
		showInLegend: true,
		xValueFormatString: "MMM YYYY",
		yValueFormatString: "#,##0 Units",
		dataPoints: [
			{ x: new Date(2018, 0, 1),  y: 120 },
			{ x: new Date(2018, 1, 1), y: 135 },
			{ x: new Date(2018, 2, 1), y: 144 },
			{ x: new Date(2018, 3, 1),  y: 103 },
			{ x: new Date(2018, 4, 1),  y: 93 },
			{ x: new Date(2018, 5, 1),  y: 129 },
			{ x: new Date(2018, 6, 1), y: 143 },
			{ x: new Date(2018, 7, 1), y: 156 },
			{ x: new Date(2018, 8, 1),  y: 122 },
			{ x: new Date(2018, 9, 1),  y: 106 },
			{ x: new Date(2018, 10, 1),  y: 137 },
			{ x: new Date(2018, 11, 1), y: 142 }
		]
	},
	{
		type: "spline",
		name: "",
		axisYType: "secondary",
		showInLegend: true,
		xValueFormatString: "MMM YYYY",
		yValueFormatString: "$#,##0.#",
		dataPoints: [
			{ x: new Date(2018, 0, 1),  y: 19034.5 },
			{ x: new Date(2018, 1, 1), y: 20015 },
			{ x: new Date(2018, 2, 1), y: 27342 },
			{ x: new Date(2018, 3, 1),  y: 20088 },
			{ x: new Date(2018, 4, 1),  y: 20234 },
			{ x: new Date(2018, 5, 1),  y: 29034 },
			{ x: new Date(2018, 6, 1), y: 30487 },
			{ x: new Date(2018, 7, 1), y: 32523 },
			{ x: new Date(2018, 8, 1),  y: 20234 },
			{ x: new Date(2018, 9, 1),  y: 27234 },
			{ x: new Date(2018, 10, 1),  y: 33548 },
			{ x: new Date(2018, 11, 1), y: 32534 }
		]
	},	
	{
		type: "spline",
		name: "",
		axisYType: "primary",
		showInLegend: true,
		xValueFormatString: "MMM YYYY",
		yValueFormatString: "$#,##0.#",
		dataPoints: [
			{ x: new Date(2018, 0, 1),  y: 50 },
			{ x: new Date(2018, 1, 1), y: 35 },
			{ x: new Date(2018, 2, 1), y: 44 },
			{ x: new Date(2018, 3, 1),  y: 03 },
			{ x: new Date(2018, 4, 1),  y: 93 },
			{ x: new Date(2018, 5, 1),  y: 29 },
			{ x: new Date(2018, 6, 1), y: 43 },
			{ x: new Date(2018, 7, 1), y: 56 },
			{ x: new Date(2018, 8, 1),  y: 22 },
			{ x: new Date(2018, 9, 1),  y: 106 },
			{ x: new Date(2018, 10, 1),  y: 37 },
			{ x: new Date(2018, 11, 1), y: 42 }
		]
	}]
};
$("#chartContainer").CanvasJSChart(options);

function toggleDataSeries(e) {
	if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
		e.dataSeries.visible = false;
	} else {
		e.dataSeries.visible = true;
	}
	e.chart.render();
}

}



/*chart Bar*/
$('.vertical-bar .progress-fill span').each(function(){
  var percent = $(this).html();
  var pTop = 100 - ( percent.slice(0, percent.length - 1) ) + "%";
  $(this).parent().css({
    'height' : percent,
    'top' : pTop
  });
});