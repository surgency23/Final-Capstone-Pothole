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
        var mm = today.getMonth() + 1;

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

    $("#reportBtnMan").on("click", function (event) {
        var address = $("#manualAddressEntry").val();
        var city = $("#manualCityEntry").val();
        var state = $("#manualStateEntry").val();

        addressData = {
            Address = address.toString(),
            City = city.toString(),
            State = state.toString()
        }

        var gls = newGoogleLocationService();
        var latlong = gls.GetLatLongFromAddress(address);
        $("#Latitude").val(latLong.Latitude);
        $("#Longitude").val(latLong.Latitude);

        //try {
        //    var latlong = gls.GetLatLongFromAddress(address);

        //}
        //catch (System.Net.WebException ex){
        //System.Console.WriteLine("Google Maps API Error {0}", ex.Message); }
    });
});
