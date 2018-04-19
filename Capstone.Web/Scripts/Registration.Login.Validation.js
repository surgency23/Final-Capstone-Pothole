/// <reference path="./jquery-3.1.1.js" />
/// <reference path="./jquery.validate.js" />
$(document).ready(function () {

    $("#LoginForm").validate({
        rules: {
            Username: {
                required: true,
                minlength: 2
            },
            Password: {
                required: true,
                minlength: 8
            }
        },
        messages: {
            Username: {
                required: "Please enter your username"

            },
            Password: {
                required: "Please enter a password"
            }
        }
    });

    $("#RegisterForm").validate({
        rules: {
            Username: {
                required: true,
                minlength: 2
            },
            Password: {
                required: true,
                minlength: 8
            }
        },
        messages: {
            Username: {
                required: "TEstestest enter your username",
                minLength: "Mustalsdkfjlasdkjf"
            },
            Password: {
                required: "Please enter a password"
            }
        }
    });
});