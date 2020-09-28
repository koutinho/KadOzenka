function GetClusterData(bounds, zoom, token, objectId) {
    redrawWaiting();
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
            maxObjectsCount: MapSettings.maxObjectsCount,
            token: token,
            objectId: objectId,
            districts: DISTRICTS_DATA,
            marketSource: SOURCE_DATA,
            actualDate: ACTUAL_DATE
		},
        dataType: 'json',
        success: function (result) {
            if (result.token == currentToken) {
                var ids = [];
                result.arr.slice(0, MapSettings.leftMenuMaxValues).forEach(x => { if (x.id != undefined) ids.push(x.id); });
                initCluster(result.arr, zoom, result.allCount);
                changeObjectsCount(result.allCount);
            }
        }
    });
};

function GetHeatMapData() {
    $.ajax({
        type: "GET",
        url: "Map/HeatMapData",
        contentType: 'application/json; charset=utf-8',
        data: {
            colors: generateColor(
                document.getElementById('rgbInitialShowPanel').style.background.replace(/[^0-9,]/gi, '').split(','),
                document.getElementById('rgbResultShowPanel').style.background.replace(/[^0-9,]/gi, '').split(','),
                document.getElementById("splicedDeltaController").value).reverse().join(","),
            actualDate: ACTUAL_DATE
        },
        dataType: 'json',
        success: function (result) {
            heatMapData = result;
            if (currentLayer) changeLayer(currentLayer);
            setHeatMapButtonState(false);
        }
    });
};

function GetRequiredInfo(idsArray) {
    $.ajax({
        type: "POST",
        url: "Map/RequiredInfo",
        data: JSON.stringify(idsArray),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (result) {
            clearCardContainer();
            showCardContainer();
            for (var i = 0; i < result.length; i++) insertCard(result[i], i == (result.length - 1));
            for (var i = 0; i < result.length; i++) addEventsCard(result[i]);
        }
    });
};

function GetFilterData() {
    $.ajax({
        type: "POST",
        url: "Map/FindFilters",
        contentType: 'application/json; charset=utf-8',
        success: function (filterInfo) { refreshFilterWidget(filterInfo); }
    });
};

function SetFilterData(filter) {
    $.ajax({
        type: "GET",
        url: "Map/SetFilters",
        data: { filter: filter },
        contentType: 'application/json; charset=utf-8',
        success: function () { GetClusterData(map.getBounds(), map.getZoom(), currentToken, params.has('objectId') ? params.get('objectId') : null); }
    });
};

function SetAvaliableValues() {
    $.ajax({
        type: "GET",
        url: "Map/GetAvaliableValues",
        data: { },
        contentType: 'application/json; charset=utf-8',
        success: function (avaliableData) { avaliableCIPJSTypes = avaliableData.CIPJSType, avaliableSegments = avaliableData.MarketSegment, avaliableStatuses = avaliableData.Status, avaliableQualityClasses = avaliableData.QualityClass }
    });
};

function ChangeObject(object) {
    $.post("Map/ChangeObject", object).done(function (response) {
        GetRequiredInfo(ids);
        GetClusterData(map.getBounds(), map.getZoom(), currentToken, params.has('objectId') ? params.get('objectId') : null);
    }).fail(function (response, textStatus, errorThrown) {
        Common.ShowError(response.responseText);
    });
};