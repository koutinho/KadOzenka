function GetData() {
    $.ajax({
        type: "GET",
        url: "Map/Objects",
        contentType: 'application/json; charset=utf-8',       
        dataType: 'json',
        success: function (result) {
            ids = [];
            result.slice(0, 20).forEach(x => ids.push(x.id));
            initCluster(result);
            GetRequiredInfo(ids);
        }
    });
}

function GetRequiredInfo(idsArray) {
    $.ajax({
        type: "POST",
        url: "Map/RequiredInfo",
        data: JSON.stringify(idsArray),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (result) {
            clearCardContainer();
            result.forEach(x => insertCard(x));
        }
    });
}