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

    //$(".formValidationGroup").validate({
    //    rules: {
    //        firstName: "required",
    //        lastName: "required",
    //        username: "required",
    //        password: "required",
    //        confirmPassword: {
    //            required: true,
    //            equalTo: "#password"
    //        },
    //        email: "required",
    //        confirmEmail: {
    //            required: true,
    //            equalTo: "#confirmEmail"
    //        }
    //    },
    //    messages:
    //        {
    //            firstName: "Please enter your first name",
    //            lastName: "Please enter your last name",
    //            username: "Please enter your desired username",
    //            password: "Please enter your password",
    //            confirmPassword:
    //                {
    //                    required: "Please confirm your password",
    //                    equalTo: "Must match your password"
    //                },
			 //   email: "Please enter your Email",
    //            confirmEmail:
    //                {
    //                    required: "Please confirm your Email",
    //                    equalTo: "Must match your Email"
    //                }
    //        }

    //});
});