
var map;
var trafficLayer;
var directionsDisplay;
var directionsService;
var marker;
var columbusCenterPos = { lat: 39.9612, lng: -82.9988 };
$(document).ready(function () {

        var latString = $("#lat").html();
        var lngString = $("#lng").html();
        var severity = $("#severity").html();
        var latVal = parseFloat(latString);
        var lngVal = parseFloat(lngString);
        var markerpos = { lat: latVal, lng: lngVal };

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
            url: 'https://image.ibb.co/m5wBRn/mild_pothole.png',
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
        //#endregion 

        switch (severity) {
            case "Minor":
                image = green;
                break;
            case "Moderate":
                image = yellow;
                break;
            case "Hazard":
                image = orangemarker;
                break;
            case "Severe":
                image = red;
                break;
            case "Extreme Danger":
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
        zoom: 13
    });
    directionsDisplay.setMap(map);
}