
var map;
var trafficLayer;
var directionsDisplay;
var directionsService;
var marker;
var columbusCenterPos = { lat: 39.9612, lng: -82.9988 };

$(document).ready(function () {

    var latString = $("#lat").html();
    var lngString = $("#lng").html();

    var severity = $("#Severity").html();
    var image = {
        url: 'https://image.ibb.co/m5wBRn/mild_pothole.png',
        scaledSize: new google.maps.Size(74, 74),
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(37, 37)
    };
    var orangemarker = {
        url: 'https://image.ibb.co/mokmbn/pothole_marker_orange.png',
        scaledSize: new google.maps.Size(74, 74),
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(37, 37)
    };
    var pinkmarker = {
        url: 'https://image.ibb.co/mQYHhS/pothole_marker_pink.png',
        scaledSize: new google.maps.Size(74, 74),
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(37, 37)
    };
    var green = {
        url: 'https://image.ibb.co/iVP197/pothole_marker_green.png',
        scaledSize: new google.maps.Size(74, 74),
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(37, 37)
    };
    var blue = {
        url: 'https://image.ibb.co/hKUONS/pothole_marker_blue.png',
        scaledSize: new google.maps.Size(74, 74),
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(37, 37)
    };
    var yellow = {
        url: 'https://image.ibb.co/gNKq2S/pothole_marker_yellow.png',
        scaledSize: new google.maps.Size(74, 74),
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(37, 37)
    };
    var red = {
        url: 'https://image.ibb.co/eVRsGn/pothole_marker_darker_red.png',
        scaledSize: new google.maps.Size(74, 74),
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(37, 37)
    };
    var shape = {
        coords: [1, 1, 1, 20, 18, 20, 18, 1],
        type: 'poly'
    };
    var latVal = parseFloat(latString);
    var lngVal = parseFloat(lngString);
    var markerpos = { lat: latVal, lng: lngVal };

    switch (severity) {
        case "1":
            image = green;
            break;
        case "2":
            image = yellow;
            break;
        case "3":
            image = orangemarker;
            break;
        case "4":
            image = red;
            break;
        case "5":
            image = pinkmarker;
            break;

    }
    marker = new google.maps.Marker({
        position: markerpos,
        map: map,
        icon: image,
        shape: shape,
        animation: google.maps.Animation.DROP,
    });
    map.panTo(markerpos);
});



function initMap() {
    directionsService = new google.maps.DirectionsService();
    directionsDisplay = new google.maps.DirectionsRenderer();
    map = new google.maps.Map(document.getElementById('map'), {
        center: columbusCenterPos,
        zoom: 13,
        zIndex: 0
    });
    directionsDisplay.setMap(map);
}