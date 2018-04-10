/// <reference path="../Scripts/jquery-3.1.1.js" />

$(document).ready(function () {

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        x.innerHTML = "Geolocation is not supported by this browser.";
    }
    function showPosition(position) {
        $("#Latitude").val(position.coords.latitude);

        $("#Longitude").val(position.coords.longitude);
    }

    });


