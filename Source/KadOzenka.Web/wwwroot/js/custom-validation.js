function distinctErrors(errors) {
    var result = [];
    $.each(errors, function (index, event) {
        var events = $.grep(result, function (e) {
            return event.Message === e.Message;
        });
        if (events.length === 0) {
            result.push(event);
        }
    });

    return result;
}