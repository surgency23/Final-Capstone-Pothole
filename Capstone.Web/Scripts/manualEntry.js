

var map, infoWindow;
function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: 40.367474, lng: -82.996216 },
        zoom: 8,
        mapTypeId: 'roadmap'
    });






    var geocoder = new google.maps.Geocoder();

    google.maps.event.addListener(map, 'click', function (event) {
        geocoder.geocode({
            'latLng': event.latLng
        }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                if (results[0]) {
                    var address = results[0].formatted_address;
                   $("#search-box").val(results[0].formatted_address);
                    geocoder.geocode({ 'address': address }, function (results, status) {
                        if (status == 'OK') {
                            $("#Latitude").val(results[0].geometry.location.lat);
                            $("#Longitude").val(results[0].geometry.location.lng);
                            map.setCenter(results[0].geometry.location);
                            var marker = new google.maps.Marker({
                                map: map,
                                position: results[0].geometry.location
                            });
                        } else {
                            alert('Geocode was not successful for the following reason: ' + status);
                        }
                    });
                
                }
            }
        });
    });





    var input = document.getElementById('search-box');
    var searchBox = new google.maps.places.SearchBox(input);
    map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

    map.addListener('bounds_changed', function () {
        searchBox.setBounds(map.getBounds());
    });

    var markers = [];
    // Listen for the event fired when the user selects a prediction and retrieve
    // more details for that place.
    searchBox.addListener('places_changed', function () {
        var places = searchBox.getPlaces();

        if (places.length == 0) {
            return;
        }

        // Clear out the old markers.
        markers.forEach(function (marker) {
            marker.setMap(null);
        });
        markers = [];

        // For each place, get the icon, name and location.
        var bounds = new google.maps.LatLngBounds();
        places.forEach(function (place) {
            if (!place.geometry) {
                console.log("Returned place contains no geometry");
                return;
            }
            var icon = {
                url: place.icon,
                size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(25, 25)
            };
            $("#Latitude").val(place.geometry.location.lat);
            $("#Longitude").val(place.geometry.location.lng);
            // Create a marker for each place.
            markers.push(new google.maps.Marker({
                map: map,
                icon: icon,
                title: place.name,
                position: place.geometry.location
            }));
            if (place.geometry.viewport) {
                // Only geocodes have viewport.
                bounds.union(place.geometry.viewport);
            } else {
                bounds.extend(place.geometry.location);
            }
        });
        map.fitBounds(bounds);
    });




//    infoWindow = new google.maps.InfoWindow;

//    // Try HTML5 geolocation.
//    if (navigator.geolocation) {
//        navigator.geolocation.getCurrentPosition(function (position) {
//            var pos = {
//                lat: position.coords.latitude,
//                lng: position.coords.longitude
//            };
//            infoWindow.setPosition(pos);
//            infoWindow.setContent('Current Location.');
//            infoWindow.open(map);
//            map.setCenter(pos);
//        }, function () {
//            handleLocationError(true, infoWindow, map.getCenter());
//        });
//    } else {
//        // Browser doesn't support Geolocation
//        handleLocationError(false, infoWindow, map.getCenter());
//    }

//}

}





