let Controller = (url, method, data, emptyHook = true, HookId, callback, localStorageKey) => {
    if (HookId != undefined && HookId.length > 1 && HookId[0] != '#') { HookId = '#' + HookId; }
    $.ajax({
        url: url,
        data: data,
        method: method,
        success: (res) => {
            if (emptyHook == true) {
                $(HookId).empty();
            }

            $(HookId).append(res);

            if (callback != undefined) {
                callback();
            }

            if (localStorageKey != undefined) {
                localStorage.setItem(localStorageKey, res);
            }
        },
        error: (res) => {
            console.log(res);
        }
    });
};

// Html input field must contain same name as field name in model or action parameter
let RedirectOnSuccess = (ApiUrl, HtmlIdArray, RedirectUrl, localStorageKey) => {
    // Preprocessing data
    let _data = {};
    HtmlIdArray.forEach((elem) => {
        if (elem != undefined && elem[0] != '#') {
            elem = '#' + elem;
        }

        _data[elem.substring(1, elem.length)] = $(elem).val();
    });

    // Get JWT Token
    Controller(ApiUrl, 'POST', _data, false, '', () => {
        window.location.href = RedirectUrl;
    }, localStorageKey);

    $.ajax({
        url: ApiUrl,
        method: 'GET',
        headers: {
            "Authorization": "Bearer " + localStorage.getItem('token')
        }
    });
}
