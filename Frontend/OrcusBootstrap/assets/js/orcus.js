const controller = (_url, _method = 'GET', _containerId = 'Container') => {
    if(_containerId[0] != '#') { _containerId = '#' + _containerId; }
    $('.page-loader-wrapper').css('display', 'block');
    $.ajax({
        url: _url,
        method: _method,
        success: (res) => {
            setTimeout(() => { $('.page-loader-wrapper').fadeOut(); }, 300);
            $(_containerId).empty();
            $(_containerId).append(res);
            if(typeof(onLoad) === 'function'){
                onLoad();
                onLoad = null;
            }
        },
        error: (res) => {
            swal.fire({
                title: 'An internal error has occured!',
                text: 'Please contact support team',
                icon: 'error'
            });
            console.error(res);
        }
    });
}

const LeftBarInit = () => {
    $( "#leftsidebar a" ).click((elem) => {
        // Set Container Head
        $('#PageTitle').text($(elem.target).text());

        let partial = $(elem.target).attr('partial');        
        if(partial == undefined || partial == NaN || partial == null)
            return;
        let partialArr = partial.split('.');
        if(partialArr[partialArr.length-1] != 'html'){
            partial += '.html'; 
        }

        controller(window.location.href + '/partialViews/' + partial);
        
        // Remove all active class
        $( "#leftsidebar a" ).parent().removeClass('active');
        
        // Add active class 
        $(elem.target).addClass('toggled');
        $(elem.target).parent().addClass('active');
        
        // Add class to 
        $(elem.target).parent().parent().parent().addClass('active');
    });
}

$(document).ready(() => {
    // Last change:  04/01/2018
    // Primary use:	Oreo - Responsive Bootstrap 4 Template
    // should be included in all pages. It controls some layout
    // "use strict";
    // initDonutChart();
    // Jknob();
    // MorrisArea();

    // Register Service Worker for PWA
    serviceWorkerRegistration();
});

async function serviceWorkerRegistration () {
    if('serviceWorker' in navigator){
        try {
            await navigator.serviceWorker.register('/assets/js/service-worker.js');
        } catch (e) {
            console.error(e);
            console.error('service worker registration failed');
        }
    }
}

const resetForm = (id) => {
    if(id[0] != '#') { id = '#' + id; }
    $(id).trigger("reset");
    $(id + ' select').val('');
    $(id + ' select').selectpicker('refresh');
}