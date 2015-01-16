$(document).ready(function () {
    var map = null;
    var marker = null;
    var infoWindow = null;

    function addLatLng(event) {
        if (marker) marker.setMap(null);
        marker = new google.maps.Marker({
            position: event.latLng,
            draggable: true,
            title: event.latLng.toString(),
            map: map
        });
        if (event.noUpdate !== true) {
            $("input#Latitude")[0].value = marker.position.lat();
            $("input#Longitude")[0].value = marker.position.lng();
        }

        if (infoWindow) infoWindow.setMap(null);
        infoWindow = new google.maps.InfoWindow({ content: marker.position.toString() });

        //// UPDATE position in document
        google.maps.event.addListener(marker, 'click', function () {
            infoWindow.open(map, marker);
        });
        // google.maps.event.addListener(marker, 'dragstart', function() {
        // });
        google.maps.event.addListener(marker, 'drag', function () {
            infoWindow.setContent(marker.position.toString());
            infoWindow.setPosition(marker.position);
        });
         google.maps.event.addListener(marker, 'dragend', function() {
             // UPDATE position in document
             $("input#Latitude")[0].value = marker.position.lat();
             $("input#Longitude")[0].value = marker.position.lng();
         });
    }


    var mapOptions = {
        center: { lat: 50, lng: 18 },
        zoom: 5
    };

    var mapdiv = $("#map-container")[0];
    map = new google.maps.Map(mapdiv, mapOptions);
    if (mapdiv.className === "editable-map") {
        var lat = $("input#Latitude")[0].value;
        var lng = $("input#Longitude")[0].value;
        if (lat !== '' && lng !== '') {
            var pos = new google.maps.LatLng(lat, lng);
            addLatLng({ latLng: pos });
        }
        google.maps.event.addListener(map, 'click', addLatLng);
    } else {
        var lat = parseFloat($("#latitudeData")[0].textContent);
        var lng = parseFloat($("#longitudeData")[0].textContent);
        var pos = new google.maps.LatLng(lat, lng);
        marker = new google.maps.Marker({
            position: pos,
//            position: new google.maps.LatLng(lat, lng),
            title: pos.toString(),
            map: map
        });
    }

    $("input#Latitude").change(function() {
        var lat = $("input#Latitude")[0].value;
        var lng = $("input#Longitude")[0].value;
        if (lat !== '' && lng !== '') {
            var pos = new google.maps.LatLng(lat, lng);
            addLatLng({ latLng: pos, noUpdate: true });
        }
    });

    $("input#Longitude").change(function () {
        var lat = $("input#Latitude")[0].value;
        var lng = $("input#Longitude")[0].value;
        if (lat !== '' && lng !== '') {
            var pos = new google.maps.LatLng(lat, lng);
            addLatLng({ latLng: pos, noUpdate: true });
        }
    });
});