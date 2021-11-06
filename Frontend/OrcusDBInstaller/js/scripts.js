/*!
* Start Bootstrap - Coming Soon v6.0.5 (https://startbootstrap.com/theme/coming-soon)
* Copyright 2013-2021 Start Bootstrap
* Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-coming-soon/blob/master/LICENSE)
*/
// This file is intentionally blank
// Use this file to add JavaScript to your project
let stepCounter = 0;
let stateChange = true;
let baseUrl = 'http://localhost:52091/api'

$(document).ready(() => {
    $('#Heading').html('Application Installer');
    $('#Description').html('This is a simple installer to install database and create system admin.');
    $('#NextButton').click(() => {
        console.log(stepCounter)
        switch (stepCounter) {
            case 0:
                if(stateChange == true){
                    page1();
                    stepCounter++;
                }
                break;
            case 1:
                if($('#DbSelector').val() == 0){
                    $('#Description').append(alertDanger("You didn't select any database"));
                    break;
                }
                else {
                    localStorage.setItem('SelectedDatabase', $('#DbSelector').val());
                    page2();
                    stepCounter++;
                    stateChange = true;
                }
                break;
            case 2:    
                if ($('#email').val().includes('@')){
                    stateChange = true;
                    page3();
                    stepCounter++;
                }
                else {
                    $('#Description').append(alertDanger("You didn't provide a valid maill address"));
                }
                break;
            case 3:
                $.ajax({
                    url: baseUrl + '/Database/Create/',
                    type: 'POST',
                    dataType: "JSON",
                    contentType: "application/json",
                    data: JSON.stringify(localStorage),
                    success: () => {window.location = "http://127.0.0.1:5608/sign-in.html";},
                    error: (res) => {
                        console.log(res);
                        alert(res.responseText)
                    }
                });
                break;
            default:
                $('#Description').append(alertDanger("Error"));
        }
    });
});

let page1 = () => {
    localStorage.clear();
    $('#Description').empty();
    $('#Description').html(
        '<select id="DbSelector" class="form-select" aria-label="Default select example">'
            + '<option value="0" selected>Select Database</option>'
            + '<option value="1">MySql</option>'
            + '<option value="2">MSSql</option>'
            + '<option value="3">Postgres</option>'
        +'</select>'
        );
    stateChange = false;
}

let page2 = () => {
    $('#Description').html('<input class="form-control" id="email" type="email" placeholder="Enter email address..." aria-label="Enter email address..." data-sb-validations="required,email" /></div>');
}

let page3 = () => {
    localStorage.setItem('EmailAddress', $('#email').val());
    $('#Description').empty();
    $('#NextButton').html('Finish');
}

let alertDanger = (msg = 'An example danger alert with an icon') => {
    return '<div class="alert alert-warning alert-dismissible fade show" role="alert"><strong>Holy guacamole!</strong><p>'+msg+'</p><button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button> </div>';
}