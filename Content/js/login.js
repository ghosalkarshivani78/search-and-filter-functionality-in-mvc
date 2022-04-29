$('#register-btn').on('click', function() {
    $('.login-form').css("display", "none");
	$('.register-form').css("display", "block");
});

$('#forgot-btn').on('click', function() {
    $('.login-form').css("display", "none");
	$('.forgot-form').css("display", "block");
});

$('#login-btn').on('click', function() {
    $('.register-form').css("display", "none");
	$('.login-form').css("display", "block");
});

$('#supplier-btn').on('click', function() {
    $('.login-form').css("display", "none");
	$('.supplier-form').css("display", "block");
});

$('#cancel-btn').on('click', function() {
    $('.login-form').css("display", "block");
	$('.supplier-form').css("display", "none");
});

$(document).ready( function(){
  var wheight = $(window).height()- 0;
  $('.login-reg-wrap').css("min-height", wheight + "px");  
});	

