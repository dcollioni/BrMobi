(function (d) {
    var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
    if (d.getElementById(id)) { return; }
    js = d.createElement('script'); js.id = id; js.async = true;
    js.src = "//connect.facebook.net/en_US/all.js";
    ref.parentNode.insertBefore(js, ref);
} (document));

window.fbAsyncInit = function () {
    FB.init({
        appId: '597380940272164',
        status: false,
        cookie: true,
        xfbml: true
    });

    FB.Event.subscribe('auth.authResponseChange', function (response) {
        if (response.status === 'connected') {
            console.log('the user is logged into facebook and registered in our app');

            var form = document.createElement("form");
            form.setAttribute("method", 'post');
            form.setAttribute("action", '/Account/LogIn');

            var field = document.createElement("input");
            field.setAttribute("type", "hidden");
            field.setAttribute("name", 'accessToken');
            field.setAttribute("value", response.authResponse.accessToken);
            form.appendChild(field);
            document.body.appendChild(form);
            form.submit();
        }
        else if (response.status === 'not_authorized') {
            console.log('the user is logged into facebook but not registered in our app');
        }
        else {
            console.log('the user is not logged into facebook');
        }
    });
    FB.getLoginStatus();
};

$(function () {
    $('#facebook_button').click(function () { FB.login(); });

    $('.facebookButton').click(function () {
        openRegistration();
    });

    $('#mask').click(function () {
        closeRegistration();
    });

    $('#registration .cancel').click(function () {
        closeRegistration();
    });

    $('#registration form').submit(function () {
        $.post(
               'Account/RegisterEmail',
                $(this).serialize(),
                function (response) {
                    console.log(response.Message);
                }
            );
        return false;
    });

    $(document).keyup(function (e) {
        if (e.keyCode == 27) {
            closeRegistration();
        }
    });

    function openRegistration() {
        $('#mask, #registration').fadeIn();
        $('#registration .email').focus();
    }

    function closeRegistration() {
        $('#mask, #registration').fadeOut();
    }
});