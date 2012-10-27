$(function () {
    $('#mapFilters .button').click(function (e) {
        $(this).toggleClass('active');
        e.stopPropagation();
    });

    $('#mapFilters .bus').click(function (e) {
        BrMobi.showBus = $(this).hasClass('active');
        BrMobi.map.refreshMarkers();
    });

    $('#mapFilters .rideOffer').click(function (e) {
        BrMobi.showRideOffer = $(this).hasClass('active');
        BrMobi.map.refreshMarkers();
    });

    $('#mapFilters .rideRequest').click(function (e) {
        BrMobi.showRideRequest = $(this).hasClass('active');
        BrMobi.map.refreshMarkers();
    });

    $('#mapFilters .help').click(function (e) {
        BrMobi.showHelp = $(this).hasClass('active');
        BrMobi.map.refreshMarkers();
    });
});