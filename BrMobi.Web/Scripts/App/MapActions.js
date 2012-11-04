$(function () {
    $('#mapActions').bind('contextmenu', function () { return false; });

    $('#mapActions .bus').click(markBus);
    $('#mapActions .rideOffer').click(markRideOffer);
    $('#mapActions .rideRequest').click(markRideRequest);
    $('#mapActions .help').click(markHelp);

    function markBus() {
        mark('bus');
    }

    function markRideOffer() {
        mark('rideOffer');
    }

    function markRideRequest() {
        mark('rideRequest');
    }

    function markHelp() {
        mark('help');
    }

    function mark(type) {
        if (!BrMobi.actualLat || !BrMobi.actualLng) {
            return false;
        }

        var param = { lat: BrMobi.actualLat, lng: BrMobi.actualLng };
        var url, type;

        switch (type) {
            case 'bus':
                url = 'MarkBus';
                type = 1;
                if (!BrMobi.showBus) {
                    $('#mapFilters .bus').click();
                }
                break;
            case 'rideOffer':
                url = 'MarkRideOffer';
                type = 2;
                if (!BrMobi.showRideOffer) {
                    $('#mapFilters .rideOffer').click();
                }
                break;
            case 'rideRequest':
                url = 'MarkRideRequest';
                type = 3;
                if (!BrMobi.showRideRequest) {
                    $('#mapFilters .rideRequest').click();
                }
                break;
            case 'help':
                url = 'MarkHelp';
                type = 4;
                if (!BrMobi.showHelp) {
                    $('#mapFilters .help').click();
                }
                break;
        }

        $('#mapActions').fadeOut(100);
        BrMobi.mask($('#mapCanvas'));

        $.post('Map/' + url, param, function (response) {
            BrMobi.unmask();
            
            var marker = new google.maps.Marker({
                position: new google.maps.LatLng(BrMobi.actualLat, BrMobi.actualLng),
                map: BrMobi.map,
                icon: response.ImagePath,
                type: type,
                id: response.Id
            });

            BrMobi.markers.push(marker);
            BrMobi.markersId.push(response.Id);

            google.maps.event.addListener(marker, 'click', function (e) {
                BrMobi.onMarkerClick(e, marker);
            });

            BrMobi.onMarkerClick(null, marker);
        });
    }
});