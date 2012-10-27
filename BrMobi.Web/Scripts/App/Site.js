$(function () {
    $(document).click(function () {
        $('.modal').fadeOut(200);
        $('#mapActions').fadeOut(100);
    });

    $('.modal').click(function (e) {
        e.stopPropagation();
    });

    $('#mapActions').click(function (e) {
        e.stopPropagation();
    });
});