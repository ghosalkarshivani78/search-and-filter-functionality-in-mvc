$(".main-outer-container").addClass("sidebar-collapse");
$(".sidebar-toggler").addClass("active");
$('.sidebar-toggler').click(function() {
     $(".main-outer-container").toggleClass("sidebar-collapse");
	 $(".sidebar-toggler").toggleClass("active");
});

$(document).ready( function(){
  var wheight = $(window).height()- 177;
  $('.main-container').css("min-height", wheight + "px"); 
});	

$(document).ready(function(){
var myHeight = $('.sidebar').height()- 67;
$('.sidebar ul.panel-group').css("height", myHeight + "px"); 
$('.sidebar ul.panel-group').slimscroll({
	color: '#fff',
	size: '5px',
	height : myHeight
});		

});
		

$(document).ready(function() {
    $('.datatable').DataTable();
} );

$( function() {
    $( ".datepicker" ).datepicker();
});
