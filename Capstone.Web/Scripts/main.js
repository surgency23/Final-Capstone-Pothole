﻿/// <reference path="../Scripts/jquery-3.1.1.js" />

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

    $("#manualButton").click(function () {
        $("#content").load("/Home/ManualPotHoleEntry").$("body");
    });

    ﻿function ColorChange() {/*method for navbar color change*/
        var x = document.getElementById("myTopnav");
        if (x.className === "topnav") {
            x.className += " responsive";
        } else {
            x.className = "topnav";
        }
    }
});
