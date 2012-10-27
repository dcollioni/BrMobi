$(function () {
    BrMobi.map = new google.maps.Map(document.getElementById("mapCanvas"), {
        //center: new google.maps.LatLng(-30.050, -51.150),
        center: new google.maps.LatLng(-29.99924415034457, -51.07705464363096),
        zoom: 16,
        zoomControl: false,
        panControl: false,
        streetViewControl: false,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });

    BrMobi.markers = [];
    BrMobi.markersId = [];
    BrMobi.showBus = true;
    BrMobi.showRideOffer = false;
    BrMobi.showRideRequest = false;
    BrMobi.showHelp = false;
    BrMobi.infoWindow = null;

    var timer;
    var delay = (function () {
        timer = 0;
        return function (callback, ms) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    })();

    google.maps.event.addListener(BrMobi.map, 'zoom_changed', function () {
        if (timer) {
            clearTimeout(timer);
        }
    });

    google.maps.event.addListener(BrMobi.map, 'rightclick', function (e) {
        delay(function () {
            showContextMenu(e);
        }, 400);
    });

    google.maps.event.addListener(BrMobi.map, 'bounds_changed', function () {
        delay(function () {
            BrMobi.map.refreshMarkers();
        }, 400);
    });

    function showContextMenu(e) {
        BrMobi.actualLat = e.latLng.lat();
        BrMobi.actualLng = e.latLng.lng();

        $('#mapActions').fadeIn(100);
        $('#mapActions').css('top', e.pixel.y + 190);
        $('#mapActions').css('left', e.pixel.x + 55);
    }

    BrMobi.map.refreshMarkers = function () {
        if (BrMobi.map.getZoom() >= 16) {
            loadMarkers();
        }
        else {
            hideMarkers();
        }
    }

    function hideMarkers() {
        $.each(BrMobi.markers, function (i, marker) {
            marker.setVisible(false);
        });
    }

    function showMarkers() {
        $.each(BrMobi.markers, function (i, marker) {
            var visible;

            switch (marker.type) {
                case 1:
                    visible = BrMobi.showBus;
                    break;
                case 2:
                    visible = BrMobi.showRideOffer;
                    break;
                case 3:
                    visible = BrMobi.showRideRequest;
                    break;
                case 4:
                    visible = BrMobi.showHelp;
                    break;
            }

            marker.setVisible(visible);
        });
    }

    function loadMarkers() {
        var params = {
            sLat: BrMobi.map.getBounds().getSouthWest().lat().toString().replace('.', ','),
            sLng: BrMobi.map.getBounds().getSouthWest().lng().toString().replace('.', ','),
            nLat: BrMobi.map.getBounds().getNorthEast().lat().toString().replace('.', ','),
            nLng: BrMobi.map.getBounds().getNorthEast().lng().toString().replace('.', ',')
        };

        $.post('Map/LoadMarkers', params, function (response) {
            $.each(response, function (i, item) {
                if (BrMobi.markersId.indexOf(item.Id) == -1) {

                    var marker = new google.maps.Marker({
                        position: new google.maps.LatLng(item.Lat, item.Lng),
                        map: BrMobi.map,
                        icon: item.ImagePath,
                        visible: false,
                        type: item.Type,
                        id: item.Id
                    });

                    BrMobi.markers.push(marker);
                    BrMobi.markersId.push(item.Id);

                    google.maps.event.addListener(marker, 'click', function (e) {
                        onMarkerClick(e, marker);
                    });
                }
            });

            showMarkers();
        });
    }

    function onMarkerClick(e, marker) {
        var infoContent;

        switch (marker.type) {
            case 1:
                infoContent = getBusInfo(marker.id);
                break;
        }

        if (BrMobi.infoWindow) {
            BrMobi.infoWindow.close();
        }

        BrMobi.infoWindow = new google.maps.InfoWindow({
            content: infoContent,
            markerId: marker.id
        });

        BrMobi.infoWindow.open(BrMobi.map, marker);
    }

    function getBusInfo(id) {
        var content;

        $.ajax({
            type: 'POST',
            url: 'Map/GetBusInfo/{0}'.format(id),
            success: function (response) { content = response; },
            async: false
        });

        return content;
    }

    function loadBusInfo(id) {
        $.post('Map/GetBusInfo/' + id, {}, function (response) {
            $('#busInfo').html(response);
        });
    }

    $('.createLineForm input[type=submit]').live('click', function () {
        var valid = true;
        var $this = $(this);
        var $form = $this.parent();
        var $ul = $form.parent().parent().find('ul');
        var $name = $form.find('input[name=name]');
        var $url = $form.find('input[name=url]');

        valid = _.all($this.siblings(), function (item) { return $(item).val() !== ''; });

        if (valid) {
            $.post($form.attr('action'), {
                name: $name.val(),
                url: $url.val(),
                busMarkerId: BrMobi.infoWindow.markerId
            }, function (response) {
                $ul.find('.noLines').remove();
                $ul.append('<li><a href="{0}" target="_blank">{1}</a></li>'.format(response.InfoUrl, response.Name));
                BrMobi.infoWindow.setContent($ul.parent().parent()[0].innerHTML);
            });
        }

        return false;
    });
});