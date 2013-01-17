$(function () {
    $('button[name=fullScreen]').click(function (e) {
        if($(this).hasClass('fullScreen')) {
            $('div.page > *:not(#main)').css('display', 'block');
            $('#main').css('margin', '50px 50px 10px');
        	$('#mapCanvas').css('height', 450);
        }
        else {
            $('div.page > *:not(#main)').css('display', 'none');
            $('#main').css('margin', 0);
        	$('#mapCanvas').css('height', $(window).height() - 75);
        }
        $(this).toggleClass('fullScreen');
    });
});