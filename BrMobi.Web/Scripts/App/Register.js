$(document).ready(function () {
    $("#registerForm form input[type=submit]").click(function () {
        var valid = true;

        $('#registerForm input[type=text], #registerForm input[type=password]').each(function (i, item) {
            var $field = $(this);
            $field.removeClass('error').attr('title', '');

            if ($field.val().trim() === '') {
                $field.addClass('error').attr('title', 'Campo obrigatório.');
                valid = false;
            }

            if ($field.attr('name') === 'email') {
                var emailValue = $field.val().trim();

                if (emailValue !== '' && !BrMobi.validation.validateEmail(emailValue)) {
                    $field.addClass('error').attr('title', 'E-mail inválido.');
                    valid = false;
                }
            }
        });

        return valid;
    });

    $("#registerForm form input[type=reset]").click(function () {
        $('#registerForm input.error').each(function (i, item) {
            $(item).removeClass('error').attr('title', '');
        });
    });

    $("#registerForm form input[name=name], #registerForm form input[name=password]").blur(function () {
        if ($(this).val().trim() !== '') {
            $(this).removeClass('error').attr('title', '');
        }
    });

    $("#registerForm form input[name=email]").blur(function () {
        if ($(this).val().trim() !== '') {
            if (BrMobi.validation.validateEmail($(this).val().trim())) {
                $(this).removeClass('error').attr('title', '');
            }
        }
    });
});