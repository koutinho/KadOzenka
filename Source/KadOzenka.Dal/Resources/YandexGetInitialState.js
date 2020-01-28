try {
    window._result = null;
    var req = new XMLHttpRequest();
    req.open("GET", arguments[0], true);

    req.onreadystatechange = function () {
        if (req.readyState === XMLHttpRequest.DONE) {
            if (req.status === 200) {
                var el = document.createElement('html');
                el.innerHTML = this.responseText;
                var innerHTML = el.getElementsByTagName('script').initial_state_script.innerHTML;
                var jsonStr = (innerHTML.substr("window.INITIAL_STATE = ".length)).slice(0, -1);
                window._result = jsonStr;
            } else {
                window._result = 'error!' + req.statusText;
            }
        }  
    };
    req.send(null);

} catch (error) {
    window._result = error;
}