function ChangeBounds(event) {
    const params = new window.URLSearchParams(window.location.search);
    const newCenter = event.get('newCenter');
    params.set('center', newCenter);
    const newZoom = event.get('newZoom');
    params.set('zoom', newZoom);
    if (window.history.replaceState) {
        const newUrl = new URL(window.location.href);
        newUrl.search = params;
        window.history.replaceState({ path: newUrl.href }, '', newUrl.href);
    }
    const newBounds = event.get('newBounds');
    refreshCurrentToken();
    GetClusterData(newBounds, newZoom, currentToken, params.has('objectId') ? params.get('objectId') : null);
};

function clickOnCluster(event) {
    ids = [];
    switch (event.get('target').options._name) {
        case ObjectTypes.cluster:
            event.get('target').properties.get('geoObjects').forEach(x => ids.push(x.properties.get('data')[0].id));
            break;
        case ObjectTypes.geoObject:
            ids.push(event.get('target').properties.get('data')[0].id);
            break;
    }
    createTargetMarker(event.get('target').geometry._coordinates, map.action.getCurrentState().zoom, ids);
    GetRequiredInfo(ids);
};

function changeObjectsCount(count) {
    document.getElementById("CountControl").classList.add("loaded");
    document.getElementById("CountControlText").innerHTML = `Объектов в видимой области ${count}`;
};

function redrawWaiting() {
    if (document.getElementById("CountControl")) {
        document.getElementById("CountControl").classList.remove("loaded");
        document.getElementById("CountControlText").innerHTML = `Загрузка объектов`;
    }
};

function removeTarget(placemark) {
    clearCardContainer();
    removeTargetWidget();
    removeTargetMarker(placemark);
    clusterSelected = null;
};

function refreshFilterWidget(filterInfo) {

    var propertyTypeData = '', dealTypeData = '', commertialMarketSegmentData = '', propertyMarketSegmentData = '', sourceTypeData = '', districtTypeData = '';
    filterInfo.propertyTypeFilter.propertyTypeList.forEach(x => propertyTypeData +=
        `<div id="${x.Name}FilterButton" elementId="${x.Id}" elementValue="${x.Value}" class="filterButton${x.Selected ? '' : ' inactive'}">${x.Value.replace(new RegExp(' ', 'g'), '&nbsp;')}</div>`);
    filterInfo.dealTypeFilter.dealTypeList.forEach(x => dealTypeData +=
        `<div id="${x.Name}FilterButton" elementId="${x.Id}" elementValue="${x.Value}" class="filterButton${x.Selected ? '' : ' inactive'}">${x.Value.replace(new RegExp(' ', 'g'), '&nbsp;')}</div>`);
    filterInfo.propertyMarketFilter.propertyMarketSegmentList.forEach(x => propertyMarketSegmentData +=
        `<div id="${x.Name}FilterButton" elementId="${x.Id}" elementValue="${x.Value}" class="filterButton${x.Selected ? '' : ' inactive'}">${x.Value.replace(new RegExp(' ', 'g'), '&nbsp;')}</div>`);
    filterInfo.commertialMarketFilter.commertialMarketSegmentList.forEach(x => commertialMarketSegmentData += 
        `<div id="${x.Name}FilterButton" elementId="${x.Id}" elementValue="${x.Value}" class="filterButton${x.Selected ? '' : ' inactive'}">${x.Value.replace(new RegExp(' ', 'g'), '&nbsp;')}</div>`);
    filterInfo.sourceTypeFilter.sourceTypeList.forEach(x => sourceTypeData += 
        `<div id="${x.Name}FilterButton" elementId="${x.Id}" elementValue="${x.Value}" class="filterButton${x.Selected ? '' : ' inactive'} sourceButton">${x.Value.replace(new RegExp(' ', 'g'), '&nbsp;')}</div>`);
    filterInfo.districtTypeFilter.districtTypeList.forEach(x => districtTypeData +=
        `<div id="${x.Name}FilterButton" elementId="${x.Id}" elementValue="${x.Value}" class="filterButton${x.Selected ? '' : ' inactive'} districtButton">${x.Value.replace(new RegExp(' ', 'g'), '&nbsp;')}</div>`);

    document.getElementById('propertyTypePanel').innerHTML = propertyTypeData;
    document.getElementById('dealTypePanel').innerHTML = dealTypeData;
    document.getElementById('propertyMarketSegmentPanel').innerHTML = propertyMarketSegmentData;
    document.getElementById('commercialMarketSegmentPanel').innerHTML = commertialMarketSegmentData;
    document.getElementById('sourcePanel').innerHTML = sourceTypeData;
    document.getElementById('districtPanel').innerHTML = districtTypeData;

    listenFilter(filterInfo.dealTypeFilter.dealTypeList, filterInfo, true);
    listenFilter(filterInfo.propertyTypeFilter.propertyTypeList, filterInfo, true);
    listenFilter(filterInfo.propertyMarketFilter.propertyMarketSegmentList, filterInfo, true);
    listenFilter(filterInfo.commertialMarketFilter.commertialMarketSegmentList, filterInfo, true);
    listenFilter(filterInfo.sourceTypeFilter.sourceTypeList, filterInfo, false, 'sourceButton', 'source');
    listenFilter(filterInfo.districtTypeFilter.districtTypeList, filterInfo, false, 'districtButton', 'district');

};

function listenFilter(initialList, filterInfo, inPool, panelId, type) {
    initialList.forEach(x => {
        document.getElementById(`${x.Name}FilterButton`).addEventListener('click', function (e) {
            if (inPool) {
                this.classList.toggle("inactive");
                refreshCurrentToken();
                SetFilterData(generateNewFilter(filterInfo));
            }
            else {
                console.log(document.getElementsByClassName(panelId));
                Array.from(document.getElementsByClassName(panelId)).forEach(x => { if (x.id != this.id) x.classList.add("inactive"); });
                this.classList.toggle("inactive");
                switch (type) {
                    case 'source':
                        SOURCE_DATA = this.classList.contains("inactive") ? null : this.innerHTML.replace(/&nbsp;/g, " ");
                        break;
                    case 'district':
                        DISTRICTS_DATA = this.classList.contains("inactive") ? null : this.innerHTML.replace(/&nbsp;/g, " ");
                        break;
                }
                refreshCurrentToken();
                GetClusterData(map.getBounds(), map.getZoom(), currentToken, params.has('objectId') ? params.get('objectId') : null);
            }
        });
    });
};

function toTarget() { if (clusterSelected) map.setCenter(clusterSelected.coords, clusterSelected.zoom, "map"); };

function changeMapType(type, element) {
    Array.from(document.getElementsByClassName("layerButton")).forEach(x => { if (x.id != element.id) x.classList.add("inactive"); });
    element.classList.toggle("inactive");
    changeLayer(!element.classList.contains("inactive") ? type : 0);
};

function changeLayer(type) {
    if(type != null) currentLayer = type;
    map.geoObjects.remove(SOM);
    if (imgLayer != null) map.layers.remove(imgLayer);
    switch (currentLayer) {
        case MapZoneType.district:
            setCurrentLayer('/MapJSONData/districts.min.json');
            break;
        case MapZoneType.region:
            setCurrentLayer('/MapJSONData/regions.min.json');
            break;
        case MapZoneType.zone:
            setCurrentLayer('/MapJSONData/zones.min.json');
            break;
        case MapZoneType.quartal:
            setQuartalTiles();
            break;
    }
};

function setQuartalTiles() {
    if (heatMapData) {
        imgLayer =
            new ymaps.Layer(imgTileSettings.imgUrlHeatMapTemplate, { tileTransparent: imgTileSettings.tileTransparent });
        if (getMinMaxDataForHeatMap()) {
            var i = 0;
            Array.from(document.getElementById('legendPaleteContainer').getElementsByClassName('coloredSegment'))
                .forEach(x => { x.setAttribute('title', `${getMinMaxDataForHeatMap()[i].min} - ${getMinMaxDataForHeatMap()[i].max}`); i++; });
        }
    } else {
        imgLayer =
            new ymaps.Layer(imgTileSettings.imgUrlTransparentTemplate, { tileTransparent: imgTileSettings.tileTransparent });
    }
    map.layers.add(imgLayer);
};

function setCurrentLayer(url) {
    if (UseManager) {
        SOM = new ymaps.ObjectManager();
        $.getJSON(url).done(function (geoJson) {
            geoJson.features.forEach(function (obj) { try { obj.properties.balloonContent = obj.properties.description; } catch{ } });
            SOM.add(geoJson);
            map.geoObjects.add(SOM);
        });
    }
    else {
        SOM = new ymaps.GeoObjectCollection();
        $.getJSON(url).done(function (geoJson) {
            var defaultColor = null;
            geoJson.features.forEach(function (obj) {
                var color = getCollorsForHeatMap(obj.name);
                var description = getDescriptionForHeatMap(obj.name);
                var opacity = color ? 0.75 : 0;
                SOM.add(new ymaps.GeoObject({
                    geometry: obj.geometry,
                    properties: { balloonContent: description }
                }, {
                    fillColor: color,
                    strokeColor: obj.options.strokeColor,
                    fillOpacity: opacity,
                    strokeWidth: parseInt(obj.options.strokeWidth),
                    strokeOpacity: obj.options.strokeOpacity,
                    name: obj.name,
                    id: obj.id
                }));
            });
            if (getMinMaxDataForHeatMap()) {
                var i = 0;
                Array.from(document.getElementById('legendPaleteContainer').getElementsByClassName('coloredSegment'))
                    .forEach(x => { x.setAttribute('title', `${getMinMaxDataForHeatMap()[i].min} - ${getMinMaxDataForHeatMap()[i].max}`); i++; });
            }
            map.geoObjects.add(SOM);
        });
    }
};

function enableEditableMode() {
    editElements = document.querySelectorAll(".DataContentContainer .DataItemContainer .EditContainer");
    editMode ? editElements.forEach(x => x.classList.remove("Hidden")) : editElements.forEach(x => x.classList.add("Hidden"));
};

function dataChanged(cartData) {
    if (cartData.lng != document.getElementById(`lngTextBox_${cartData.id}`).value ||
        cartData.lat != document.getElementById(`latTextBox_${cartData.id}`).value ||
        cartData.propertyTypeCode != document.getElementById(`typeSelect_${cartData.id}`).value ||
        cartData.statusCode != document.getElementById(`statusSelect_${cartData.id}`).value ||
        cartData.marketSegmentCode != document.getElementById(`segmentSelect_${cartData.id}`).value) {
        document.getElementById(`saveBtn_${cartData.id}`).classList.remove("blocked");
        document.getElementById(`undoBtn_${cartData.id}`).classList.remove("blocked");
    }
    else {
        document.getElementById(`saveBtn_${cartData.id}`).classList.add("blocked");
        document.getElementById(`undoBtn_${cartData.id}`).classList.add("blocked");
    }
};

function undoDataChanges(cartData) {
    if (!document.getElementById(`undoBtn_${cartData.id}`).classList.contains("blocked")) {
        document.getElementById(`lngTextBox_${cartData.id}`).value = cartData.lng;
        document.getElementById(`latTextBox_${cartData.id}`).value = cartData.lat;
        document.getElementById(`typeSelect_${cartData.id}`).value = cartData.propertyTypeCode;
        document.getElementById(`segmentSelect_${cartData.id}`).value = cartData.marketSegmentCode;
        document.getElementById(`statusSelect_${cartData.id}`).value = cartData.statusCode;
        dataChanged(cartData);
    }
};

function saveDataChanges(cartData) {
    if (!document.getElementById(`undoBtn_${cartData.id}`).classList.contains("blocked")) {
        var result = {
            id: cartData.id,
            lng: parseFloat(document.getElementById(`lngTextBox_${cartData.id}`).value),
            lat: parseFloat(document.getElementById(`latTextBox_${cartData.id}`).value),
            propertyTypeCode: parseInt(document.getElementById(`typeSelect_${cartData.id}`).value),
            marketSegmentCode: parseInt(document.getElementById(`segmentSelect_${cartData.id}`).value),
            statusCode: parseInt(document.getElementById(`statusSelect_${cartData.id}`).value)
        };
        ChangeObject(result);
    }
};

function createColorLegend(delta, initialColor, resultColor) {
    var resultColors = generateColor(initialColor.replace(/[^0-9,]/gi, '').split(','), resultColor.replace(/[^0-9,]/gi, '').split(','), delta).reverse();
    var content = '';
    for (var i = 0; i < delta; i++) { content += `<div class="coloredSegment" style="background: ${resultColors[i]}"></div>`; }
    document.getElementById('legendPaleteContainer').innerHTML = content;
};

function setHeatMapButtonState(active) {
    if (active) {
        document.getElementById("refreshHeatMapButton").classList.remove("inactive");
        document.getElementById("refreshHeatMapButton").innerHTML = "Идёт обновление<div class=\"preloaderImage\"></div>";
    }
    else {
        document.getElementById("refreshHeatMapButton").classList.add("inactive");
        document.getElementById("refreshHeatMapButton").innerHTML = "Обновить";
    }
};

function setActualDate(date) {
    ACTUAL_DATE = date.toLocaleDateString("ru-RU");
    GetClusterData(map.getBounds(), map.getZoom(), currentToken, params.has('objectId') ? params.get('objectId') : null);
};