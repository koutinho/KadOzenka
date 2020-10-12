(function () {
    $(document).ajaxError(function (e, xhr, options) {
        if (xhr.status === 401) {
            e.stopPropagation();
            window.top.location.reload();
        }
    });
})();