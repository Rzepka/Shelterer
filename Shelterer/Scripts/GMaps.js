$(document).ready(function () {
    var map = null;
    var marker = null;
    var infoWindow = null;

    var mapOptions = {
        center: { lat: 50, lng: 18 },
        zoom: 5
    };

    var mapdiv = $("#map-container")[0];
    var infoDiv = $("div#set-location-info")[0];

    map = new google.maps.Map(mapdiv, mapOptions);

    function addLatLng(event) {
        if (marker) marker.setMap(null);

        marker = new google.maps.Marker({
            position: event.latLng,
            draggable: true,
            title: event.latLng.toString(),
            map: map
        });

        $("input#Latitude")[0].value = marker.position.lat().toFixed(6);
        $("input#Longitude")[0].value = marker.position.lng().toFixed(6);

        if (infoWindow) infoWindow.setMap(null);
        infoWindow = new google.maps.InfoWindow({ content: marker.position.toString() });

        google.maps.event.addListener(marker, 'click', function () {
            infoWindow.open(map, marker);
        });

        google.maps.event.addListener(marker, 'drag', function () {
            infoWindow.setContent(marker.position.toString());
            infoWindow.setPosition(marker.position);
        });

        google.maps.event.addListener(marker, 'dragend', function() {
            $("input#Latitude")[0].value = marker.position.lat().toFixed(6);
            $("input#Longitude")[0].value = marker.position.lng().toFixed(6);
         });
    }

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
            title: pos.toString(),
            map: map
        });
    }

    $("input#Latitude").change(function() {
        var lat = $("input#Latitude")[0].value;
        var lng = $("input#Longitude")[0].value;
        if (lat !== '' && lng !== '') {
            var pos = new google.maps.LatLng(lat, lng);
            addLatLng({ latLng: pos });
        }
    });

    $("input#Longitude").change(function () {
        var lat = $("input#Latitude")[0].value;
        var lng = $("input#Longitude")[0].value;
        if (lat !== '' && lng !== '') {
            var pos = new google.maps.LatLng(lat, lng);
            addLatLng({ latLng: pos });
        }
    });

    $("button#set-location").click(function () {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(showPosition, showError);
        } else {
            infoDiv.innerHTML = "Geolocation is not supported by this browser.";
        }
    });

    function showPosition(position) {
        var lat = position.coords.latitude.toFixed(6);
        var lng = position.coords.longitude.toFixed(6);
        var pos = new google.maps.LatLng(lat, lng);
        addLatLng({ latLng: pos });
    }

    function showError(error) {
        switch (error.code) {
            case error.PERMISSION_DENIED:
                infoDiv.innerHTML = "User denied the request for Geolocation.";
                break;
            case error.POSITION_UNAVAILABLE:
                infoDiv.innerHTML = "Location information is unavailable.";
                break;
            case error.TIMEOUT:
                infoDiv.innerHTML = "The request to get user location timed out.";
                break;
            case error.UNKNOWN_ERROR:
                infoDiv.innerHTML = "An unknown error occurred.";
                break;
        }
    }


});
