$(function () {
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

    $('div#rides_list').data('page', 0);

    function getTruncatedString(str, upper) {
        if ($.trim(str).length > upper) {
            return $.trim(str).substring(0, upper - 3) + '...';
        }
        return $.trim(str);
    }

    function loadPage() {
        $('#rides_list_loader.loader').show();
        var grid = $('div#rides_list');
        if (grid.data('lastRequestCount') === 0) {
            return;
        }
        var nextPage = grid.data('page') + 1;
        grid.data('page', nextPage);

        $.get('Map/GetUserRides', { page: nextPage }, function (rides) {
            grid.data('lastRequestCount', rides.length);
            _.each(rides, function (ride, index) {
                var image = $('<div/>').addClass('image').addClass(ride.RideType);

                var separator = $('<div/>').addClass('separator');

                var destination = $('<div/>').addClass('firstrow').html(getTruncatedString(ride.Destination, 13));
                var dateTime = $('<div/>').addClass('secondrow').html(ride.DateTime.jsonToDate().format("dd/mm/yyyy HH:MM"));
                var text = $('<div/>').addClass('text').append(destination).append(dateTime);

                grid.append($('<div/>')
                .addClass((index + 1) % 2 === 0 ? 'even' : 'odd')
                .addClass('row')
                .append(image)
                .append(separator)
                .append(text)
                .data('lat', ride.Lat)
                .data('lng', ride.Lng));
            });

            $('div.row').click(function () {
                var location = new google.maps.LatLng($(this).data('lat'), $(this).data('lng'));
                BrMobi.map.setCenter(location);
            });
            $('#rides_list_loader.loader').hide();
        });
    }

    $('div#rides_list').scroll(function () {
        var $element = $(this);
        clearTimeout(BrMobi.scrollTimer);
        BrMobi.scrollTimer = setTimeout(function () {
            if ($element.scrollTop() === $('#rides_list')[0].scrollHeight - $('#rides_list').height()) {
                loadPage();
            }
        }, 500);
    });

    //load first two pages
    loadPage();
    loadPage();
});