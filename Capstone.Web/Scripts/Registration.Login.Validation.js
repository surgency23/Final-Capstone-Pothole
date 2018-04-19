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
                required: "Please enter your username",
                minlength: 2,
            },
            Password: {
                required: "Please enter a password",
                minlength: 8
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
                required: "Please enter your username",
                minlength: 2,
            },
                Password: {
                required: "Please enter a password",
                    minlength: 8
                }
        }
    });
});