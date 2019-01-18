function deleteStopUser(url) {
    var user = $('#UserName').val();
    var pass = $('#PassWord').val();
    var userToChange = $('#userId').val();

    $.post({
        url: url,
        data: {
            'email': user,
            'password': pass,
            'userToChange': userToChange
        },
        success: function (data) {
            if (data == "True") {
                loadDetails(userToChange); //instead of reloading URL, details of changed user will show
                $('#delete-stop-credentialCheck').modal('hide');
            } else {
                $('#deleteStopMessage').text("You entered the wrong credentials. Please try again.").css("color", "red");
                $('#delete-stop-credentialCheck').modal('show');
            }
        },
        error: function () {
            alert('fail');
        }
    });


//  this is the ajax call that brings up more details about the user from the details link       
function loadDetails(userId, firstName, lastName, edit = false) {
    if (!firstName == "") {
        const pageTitle = document.getElementById("page-title");
        pageTitle.innerHTML = lastName + ", " + firstName;
    }
    if (edit) {
        getURL = EditUserURL;
        contentDiv = "user-details-content";
    }
    else {
        getURL = ViewUserDetailsURL;
        contentDiv = "details-div";
    }

    jQuery.ajax({
        'url': getURL,
        'type': 'GET',
        'data': {
            id: userId
        },
        'success': function (data) {
            document.getElementById(contentDiv).innerHTML = data;
            if (edit) {
                // if loading edit page, re-parse validator to new form fields
                $.validator.unobtrusive.parse("#edituserform");
            }
        },
        'error': function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });
};
