String.prototype.format = function() {
    var s = this,
        i = arguments.length;

    while (i--) {
        s = s.replace(new RegExp('\\{' + i + '\\}', 'gm'), arguments[i]);
    }
    return s;
};

(function ($) {
    $.BrMobi = BrMobi = {};

    BrMobi.validation = {
        validateEmail : function (value) {
            return /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i.test(value);
        }
    };

    BrMobi.mask = function ($comp) {
        var top = $comp.position().top;
        var left = $comp.position().left;
        var width = $comp.width();
        var height = $comp.height();

        $('#mapMask').css('top', top);
        $('#mapMask').css('left', left);
        $('#mapMask').width(width);
        $('#mapMask').height(height);
        $('#mapMask').show();
        $('#maskText').show();

        top = $('#mapMask').position().top;
        height = $('#mapMask').height();
        left = $('#mapMask').position().left;
        width = $('#mapMask').width();
        var textHeight = $('#maskText').height();
        var textWidth = $('#maskText').width();

        $('#maskText').css('top', top + (height / 2) - (textHeight / 2));
        $('#maskText').css('left', left + (width / 2) - (textWidth / 2));
    };

    BrMobi.unmask = function () {
        $('#mapMask').hide();
        $('#maskText').hide();
    };

    BrMobi.onMarkerClick = function (e, marker) {
        var infoContent;

        switch (marker.type) {
            case 1:
                infoContent = getBusInfo(marker.id);
                break;
            case 2:
                infoContent = getRideOfferInfo(marker.id);
                break;
        }

        if (BrMobi.infoWindow) {
            BrMobi.infoWindow.close();
        }

        BrMobi.infoWindow = new google.maps.InfoWindow({
            content: infoContent,
            position: marker.getPosition(),
            markerId: marker.id,
            marker: marker
        });

        BrMobi.infoWindow.open(BrMobi.map, marker);
        
        BrMobi.directionsDisplay.setMap(null);
    };

    BrMobi.refreshInfoWindow = function () {
        if (BrMobi.infoWindow) {
            BrMobi.onMarkerClick(null, BrMobi.infoWindow.marker);
        }
    };

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

    function getRideOfferInfo(id) {
        var content;

        $.ajax({
            type: 'POST',
            url: 'Map/GetRideOfferInfo/{0}'.format(id),
            success: function (response) { content = response; },
            async: false
        });

        return content;
    }
})(jQuery);