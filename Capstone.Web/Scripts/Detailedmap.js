
var map;
var trafficLayer;
var directionsDisplay;
var directionsService;
var marker;
var columbusCenterPos = { lat: 39.9612, lng: -82.9988 };
$(document).ready(function () {

        var latString = $("#lat").html();
        var lngString = $("#lng").html();
        var latVal = parseFloat(latString);
        var lngVal = parseFloat(lngString);
        var markerpos = { lat: latVal, lng: lngVal };
        marker = new google.maps.Marker({
            position: markerpos,
            map: map
        });

});



function initMap() {
    directionsService = new google.maps.DirectionsService();
    directionsDisplay = new google.maps.DirectionsRenderer();
    map = new google.maps.Map(document.getElementById('map'), {
        center: columbusCenterPos,
        zoom: 10
    });
    directionsDisplay.setMap(map);
}