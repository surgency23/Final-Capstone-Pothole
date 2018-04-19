
$(document).ready(function () {

    $("#loginsubmit").on("click", function (event) {
        var username = $("#Username").val();
        var password = $("#Password").val();
        var user =
            {
                Username: username,
                Password: password,
            };

        $.ajax({
            type: "POST",
            dataType: "json",
            data: JSON.stringify(user),
            url: '@Url.Action("Login","User")',
            contentType: "application/json",
        }).done(function (result2) {
            console.log(result2);
        });
    });
});