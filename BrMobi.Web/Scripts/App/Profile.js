$(function () {
    $('#profileContent input[name=birthDate]').mask('99/99/9999');

    $('#profileContent select[name=stateId]').change(function () {
        var stateId = $(this).val();

        var $city = $('#profileContent select[name=cityId]');
        $city.children('[value!=0]').remove();

        $.post('Profile/ListCities',
            {
                stateId: stateId
            },
            function (response) {
                $(response).each(function (i, item) {
                    $city.append('<option value={0}>{1}</option>'.format(item.Id, item.Name));
                });
            }
        );
    });

    $('#profileContent form input[type=submit]').click(function () {
        var $name = $('#profileContent form input[name=name]');

        if ($name.val() === "") {
            return false;
        }
    });
});