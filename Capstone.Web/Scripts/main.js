/// <reference path="../Scripts/jquery-3.1.1.js" />

$(document).ready(function () {
    $("#reportBtn").on("click", function (getLocation) {

            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(showPosition);
            } else {
                x.innerHTML = "Geolocation is not supported by this browser.";
            }
        function showPosition(position) {
            $("#Latitude").val(position.coords.latitude);
            $("#Longitude").val(position.coords.longitude);
            }

        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!

        var yyyy = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd;
        }
        if (mm < 10) {
            mm = '0' + mm;
        }
        var today = dd + '/' + mm + '/' + yyyy;
            $("#DateReported").val(today);
    });

});
