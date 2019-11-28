function GetClusterData(bounds, zoom, token) {
	$.ajax({
        type: "GET",
		url: "Map/Objects",
		contentType: 'application/json; charset=utf-8',
		data: {
			topLatitude: bounds ? bounds[0][0] : null,
			topLongitude: bounds ? bounds[0][1] : null,
			bottomLatitude: bounds ? bounds[1][0] : null,
            bottomLongitude: bounds ? bounds[1][1] : null,
            mapZoom: zoom,
            minClusterZoom: MapSettings.minClusterZoom,
            maxLoadedObjectsCount: MapSettings.maxLoadedObjectsCount,
            token: token
		},
        dataType: 'json',
        success: function (result) {
            if (result.token == currentToken) {
                ids = [];
                result.arr.slice(0, MapSettings.leftMenuMaxValues).forEach(x => { if (x.id != undefined) ids.push(x.id); });
                initCluster(result.arr, zoom);
                if (ids.length > 0) GetRequiredInfo(ids);
            }
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
            for (var i = 0; i < result.length; i++) insertCard(result[i], i == (result.length - 1));
        }
    });
}