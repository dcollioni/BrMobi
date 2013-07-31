$(function() {
    $('a#profile_picture_link').attr('href', 'http://facebook.com/' + BrMobi.user.name);
    $('img#profile_picture').attr('src', 'http://graph.facebook.com/' + BrMobi.user.name + '/picture');
    $('a#profile_picture_link').css('display', 'block');
    $('a#logout').css('display', 'block');
    
    $('a#logout').click(function () {
        var form = document.createElement("form");
        form.setAttribute("method", 'post');
        form.setAttribute("action", '/Account/LogOut');
        document.body.appendChild(form);
        form.submit();
    });
});