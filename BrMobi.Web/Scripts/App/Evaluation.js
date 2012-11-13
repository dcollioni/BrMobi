$(function () {
    $('#evaluationContent input[type=submit]').click(function () {
        var valid = true;

        for (var i = 1; i < 13; i++) {
            var checked = $('#evaluationContent input[name=' + i + ']:checked').length;

            if (checked == 0) {
                valid = false;
                $('#evaluationContent .warning').fadeIn();
                location.href = '#evaluationContent';
            }
        }

        return valid;
    });
});