/// <reference path="user.js" />
// Get the modal
$(document).ready(function () {
    $("#id01").modal('show');
});

var modal = document.getElementById('id01');

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}