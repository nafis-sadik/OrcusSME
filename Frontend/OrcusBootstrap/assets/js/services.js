const Route = {
    Base: 'https://localhost:44375/api',
    Outlet: {
        GetUserOutlets: '/Outlet/GetUserOutlets',
        Add: '/Outlet/Add',
        Update: '/Outlet/Update',
        Archive: '/Outlet/Archive',
        ECommerceOrder: '/Outlet/ECommerceOrder',
        GetOutlet: '/Outlet/GetOutlet',
        OrderSite: '/Outlet/OrderSite'
    },
    Categories: {
        Add: '/Categories/Add',
        Get: '/Categories/GetCategoriesOfOutlets/',
        Delete: '/Categories/Delete/',
        SaveHierarchy: '/Categories/SaveHierarchy/'
    }
}

const APIConsumptionGuest = (param) => {
    if(param.URL == "" || param.URL == null || param.URL == NaN){
        param.URL = Route.Base;
    }

    if(param.Method == "" || param.Method == null || param.Method == NaN){
        param.Method = 'GET';
    }

    if(typeof(param.Success) != 'function'){
        param.Success = (res) => {console.log(res);}
    }

    if(typeof(param.Error) != 'function'){
        param.Error = (res) => {
            console.error(res);
            $('.swal2-select').remove();
        }
    }

    $.ajax({
        url: param.URL,
        type: param.Method,
        dataType: "JSON",
        contentType: "application/json",
        data: JSON.stringify(param.Data),
        success: param.Success,
        error: param.Error
    });
}

const APIConsumptionAuth = (param) => {
    if(param.URL == "" || param.URL == null || param.URL == NaN) {
        param.URL = Route.Base;
    }

    if(param.Method == "" || param.Method == null || param.Method == NaN) {
        param.Method = 'GET';
    }

    if(typeof(param.Success) != 'function') {
        param.Success = (res) => { console.log(res); }
    }

    if(typeof(param.Error) != 'function') {
        param.Error = (res) => {
            console.log(res);
            let _response;
            if(res == undefined || res.response == undefined){
                _response = 'An internal error has occured!';
            } else {
                _response = res.responseJSON.response;
            }

            swal.fire({ 
                title: 'Oops...',
                text: 'Something went wrong!',
                footer: '<a onclick="" href="#">Tell us more about what you were doing</a>',
                icon: 'error'
            }).then(() => {
                if(res.status == 401) { localStorage.clear(); }
                window.location = "/sign-in.html";
            });
            $('.swal2-select').remove();
        }
    }

    $.ajax({
        url: param.URL,
        type: param.Method,
        dataType: "JSON",
        contentType: "application/json",
        headers: {
            'Authorization' : 'Bearer ' + localStorage.getItem('token')
        },
        data: JSON.stringify(param.Data),
        success: param.Success,
        error: param.Error
    });
}

const Service = {
    RefreshOutletListOnLeftSideBar : (id) => {
        if(id[0] != '#') { id = '#' + id; }
        let param = {
            URL: Route.Base + Route.Outlet.GetUserOutlets + '/' + localStorage.getItem('userId'),
            Method: 'GET',
            Success: (res) => {
                $(id).empty();
                
                $.each(res.response, (index, outlet) => {
                    if(index == 0){
                        $(id).append(
                            '<li class="active"><a partial="index-dashboard" id="'+ outlet.outletId +'">' + outlet.outletName + '</a></li>'
                        );
                    } else {
                        $(id).append(
                            '<li><a partial="index-dashboard" id="'+ outlet.outletId +'">' + outlet.outletName + '</a></li>'
                        );
                    }
                });
                
                $(id).append(
                    '<li><a partial="promo-config">Promo Code Config</a></li>'+
                    '<li><a partial="discount-campaign">Discount Campaign Manager</a></li>'
                );
                
                $.each($(".menu .list li.active"),function(a,b){var c=$(b).find("a:eq(0)");c.addClass("toggled"),c.next().show()});
                Waves.attach(".menu .list a",["waves-block"]);
                Waves.init();
                
                LeftBarInit();
            }
        };
        
        APIConsumptionAuth(param);
    },
    RefreshOutletListOnDropdown : (elemId) => {
        let param = {
            URL: Route.Base + Route.Outlet.GetUserOutlets + '/' + localStorage.getItem('userId'),
            Method: 'GET',
            Success: (res) => {
                if(elemId[0] != '#') { elemId = '#' + elemId; }
                $(elemId).empty();
                $(elemId).append('<option value="">-- Please select --</option>');
                $.each(res.response, (index, outlet) => {
                    $(elemId).append(
                        '<option value="'+ outlet.outletId +'">' + outlet.outletName + '</option>'
                    );
                })
                $(elemId).selectpicker('refresh');
            }
        };
        
        APIConsumptionAuth(param);
    },
}