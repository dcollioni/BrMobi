$(function () {
    $('#loggedUser .picture').click(function (e) {
        $('#changePicture input[type=text], #changePicture input[type=file]').val('');
        $('#changePicture').fadeToggle(200);
        e.stopPropagation();
    });

    $('#changePicture input[name=fake]').click(function () {
        $('#changePicture input[type=file]').click();
    });

    $('#changePicture input[type=file]').change(function () {
        $('#changePicture input[name=fake]').val($(this).val());
    });

    $('#changePicture input[type=submit]').click(function () {
        if ($('#changePicture input[type=file]').val() === '') {
            return false;
        }
    });
});