$(function () {

    $('#logonForm input[type=submit]').click(function () {
        var valid = true;
        
        $('#logonForm input[type=password], #logonForm input[type=text]').each(function (i, item) {
            if ($(item).val().trim() === '') {
                valid = false;
            }
        });

        return valid;
    });

    $("#logonForm2 form input[type=submit]").click(function () {
        var valid = true;
        
        $('#logonForm2 input[type=password], #logonForm2 input[type=email]').each(function (i, item) {
            if (!validateField($(item))) {
                valid = false;
            }
        });

        return valid;
    });

    $('#logonForm2 input[type=password], #logonForm2 input[type=email]').each(function (i, item) {
        $(item).blur(function () {
            return validateField($(this));
        });
    });

    function validateField($field) {
        clearError($field);

        var isRequired = $field.attr('required');
        if (isRequired) {
            validateRequired($field);
        }

        var isEmail = ($field.attr('type') === 'email');
        if (isEmail) {
            validateEmail($field);
        }

        var minLength = $field.attr('minlength');
        if (minLength) {
            validateMinLength($field);
        }

        return !$field.hasClass('error');
    }

    function clearError($field) {
        $field.removeClass('error').attr('title', '');
    }

    var errorMessages = {};
    errorMessages.required = 'Campo obrigatório.';
    errorMessages.email = 'E-mail inválido.';
    errorMessages.minlength = 'Mínimo {0} caracteres.';

    function markError($field, message) {
        $field.addClass('error').attr('title', message);
    }

    function validateRequired($field) {
        var value = $field.val().trim();

        if (value === '') {
            markError($field, errorMessages.required);
        }
    }

    function validateEmail($field) {
        var value = $field.val().trim();

        if (value !== '' && !BrMobi.validation.validateEmail(value)) {
            markError($field, errorMessages.email);
        }
    }

    function validateMinLength($field) {
        var value = $field.val().trim();
        var minLength = parseInt($field.attr('minlength'));

        if (value !== '' && value.length < minLength) {
            markError($field, errorMessages.minlength.format(minLength));
        }
    }
});