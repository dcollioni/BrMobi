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
    BrMobi.showBus = false;
    BrMobi.showRideOffer = true;
    BrMobi.showRideRequest = false;
    BrMobi.showHelp = false;
    BrMobi.infoWindow = null;
    BrMobi.locationMarker = null;
    BrMobi.directionsService = new google.maps.DirectionsService();
    BrMobi.directionsDisplay = new google.maps.DirectionsRenderer();

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

        if (BrMobi.infoWindow) {
            BrMobi.infoWindow.close();
        }
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
            sLat: BrMobi.map.getBounds().getSouthWest().lat(),
            sLng: BrMobi.map.getBounds().getSouthWest().lng(),
            nLat: BrMobi.map.getBounds().getNorthEast().lat(),
            nLng: BrMobi.map.getBounds().getNorthEast().lng()
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
                        BrMobi.onMarkerClick(e, marker);
                    });
                }
            });

            showMarkers();
        });
    }

    $('.createLineForm input[type=submit]').live('click', function () {
        var valid = true;
        var $this = $(this);
        var $form = $this.parent();
        var $name = $form.find('input[name=name]');
        var $url = $form.find('input[name=url]');

        valid = _.all($this.siblings(), function (item) { return $(item).val() !== ''; });

        if (valid) {
            BrMobi.mask($('#mapCanvas'));

            $.post($form.attr('action'), {
                name: $name.val(),
                url: $url.val(),
                busMarkerId: BrMobi.infoWindow.markerId
            }, function (response) {
                BrMobi.refreshInfoWindow();
                BrMobi.unmask();
            });
        }

        return false;
    });

    $('.rideOfferInfo .userName').live('mouseover', function () {
        $('.rideOfferInfo .userPicture').show();
    });

    $('#mapHeader input[name=search]').keypress(function (e) {
        if (e.which == 13) {
            var geocoder = new google.maps.Geocoder();
            var $searchField = $(this);
            var address = $searchField.val();

            geocoder.geocode({ 'address': address }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {

                    BrMobi.map.setCenter(results[0].geometry.location);
                    $searchField.val(results[0].formatted_address);

                    if (BrMobi.locationMarker) {
                        BrMobi.locationMarker.setVisible(false);
                    }

                    BrMobi.locationMarker = new google.maps.Marker({
                        map: BrMobi.map,
                        position: results[0].geometry.location
                    });
                }
            });
        }
    });

    $('.rideOfferInfo input[name=date]').live('focus', function () { $(this).mask('99/99/9999'); });
    $('.rideOfferInfo input[name=time]').live('focus', function () { $(this).mask('99:99'); });

    $('.rideOfferInfo form input[type=submit]').live('click', function () {
        var valid = true;
        var $this = $(this);
        var $form = $this.parent();
        var date = $form.find('input[name=date]').val();
        var time = $form.find('input[name=time]').val();
        var destination = $form.find('input[name=destination]').val();

        valid = _.all($this.siblings('input'), function (item) { return $(item).val() !== ''; });

        if (valid) {
            BrMobi.mask($('#mapCanvas'));

            $.post($form.attr('action'), {
                date: date,
                time: time,
                destination: destination,
                markerId: BrMobi.infoWindow.markerId
            }, function (response) {
                BrMobi.unmask();
            });
        }

        return false;
    });

    $('.rideOfferInfo a.destination').live('click', function () {
        var start = BrMobi.infoWindow.position;
        var end = $(this).text();

        var request = {
            origin: start,
            destination: end,
            travelMode: google.maps.DirectionsTravelMode.DRIVING
        };

        BrMobi.directionsService.route(request, function (response, status) {
            if (status == google.maps.DirectionsStatus.OK) {
                BrMobi.directionsDisplay.setMap(BrMobi.map);
                BrMobi.directionsDisplay.setDirections(response);
            }
        });
    });

    $('.rideOfferInfo .hitchhike').live('click', function () {
        BrMobi.mask($('#mapCanvas'));

        $.post('Map/AddHitchhiker',
            {
                markerId: BrMobi.infoWindow.markerId
            },
            function (response) {
                BrMobi.refreshInfoWindow();
                BrMobi.unmask();
            }
        );
    });

    $('.rideOfferInfo .undoHitchhike').live('click', function () {
        BrMobi.mask($('#mapCanvas'));

        $.post('Map/RemoveHitchhiker',
            {
                markerId: BrMobi.infoWindow.markerId
            },
            function (response) {
                BrMobi.refreshInfoWindow();
                BrMobi.unmask();
            }
        );
    });
});