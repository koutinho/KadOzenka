// Глобальная обработка ошибок
function myErrHandler(message, url, line, col, error) {
    var params = "logJSErr=logJSErr&message=" + message + '&url=' + url + '&line=' + line + '&col=' + col + '&error=' + error;
    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/CoreUI/LogJsError', true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4) {
            if (xhr.status == 200) {
                success = true;
            }
        }
    };
    xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhr.send(params);
};

window.onerror = myErrHandler;