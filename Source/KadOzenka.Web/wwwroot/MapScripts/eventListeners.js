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
    var propertyTypeData = '', dealTypeData = '', commertialMarketSegmentData = '', propertyMarketSegmentData = '';
    filterInfo.propertyTypeFilter.propertyTypeList.forEach(x => propertyTypeData +=
        `<div id="${x.Name}FilterButton" elementId="${x.Id}" elementValue="${x.Value}" class="filterButton${x.Selected ? '' : ' inactive'}">${x.Value.replace(new RegExp(' ', 'g'), '&nbsp;')}</div>`);
    filterInfo.dealTypeFilter.dealTypeList.forEach(x => dealTypeData +=
        `<div id="${x.Name}FilterButton" elementId="${x.Id}" elementValue="${x.Value}" class="filterButton${x.Selected ? '' : ' inactive'}">${x.Value.replace(new RegExp(' ', 'g'), '&nbsp;')}</div>`);
    filterInfo.propertyMarketFilter.propertyMarketSegmentList.forEach(x => propertyMarketSegmentData +=
        `<div id="${x.Name}FilterButton" elementId="${x.Id}" elementValue="${x.Value}" class="filterButton${x.Selected ? '' : ' inactive'}">${x.Value.replace(new RegExp(' ', 'g'), '&nbsp;')}</div>`);
    filterInfo.commertialMarketFilter.commertialMarketSegmentList.forEach(x => commertialMarketSegmentData += 
        `<div id="${x.Name}FilterButton" elementId="${x.Id}" elementValue="${x.Value}" class="filterButton${x.Selected ? '' : ' inactive'}">${x.Value.replace(new RegExp(' ', 'g'), '&nbsp;')}</div>`);
    document.getElementById('propertyTypePanel').innerHTML = propertyTypeData;
    document.getElementById('dealTypePanel').innerHTML = dealTypeData;
    document.getElementById('propertyMarketSegmentPanel').innerHTML = propertyMarketSegmentData;
    document.getElementById('commercialMarketSegmentPanel').innerHTML = commertialMarketSegmentData;
    listenFilter(filterInfo.dealTypeFilter.dealTypeList, filterInfo);
    listenFilter(filterInfo.propertyTypeFilter.propertyTypeList, filterInfo);
    listenFilter(filterInfo.propertyMarketFilter.propertyMarketSegmentList, filterInfo);
    listenFilter(filterInfo.commertialMarketFilter.commertialMarketSegmentList, filterInfo);
};

function listenFilter(initialList, filterInfo) {
    initialList.forEach(x => {
        document.getElementById(`${x.Name}FilterButton`).addEventListener('click', function (e) {
            this.classList.toggle("inactive");
            refreshCurrentToken();
            SetFilterData(generateNewFilter(filterInfo));
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
            //setCurrentLayer('/MapJSONData/quartal.json');
            break;
    }
};

function setQuartalTiles() {
    imgLayer = new ymaps.Layer(imgTileSettings.imgUrlTemplate, { tileTransparent: imgTileSettings.tileTransparent });
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
                //if ((obj.geometry.coordinates[0][0][0] != 55.736392 && obj.geometry.coordinates[0][0][1] != 37.699307) &&
                //    (obj.geometry.coordinates[0][0][0] != 55.758117 && obj.geometry.coordinates[0][0][1] != 37.657738))
                SOM.add(new ymaps.GeoObject({
                    geometry: obj.geometry,
                    properties: { balloonContent: obj.name }
                }, {
                    fillColor: obj.options.fillColor,
                    strokeColor: obj.options.strokeColor,
                    fillOpacity: obj.options.fillOpacity,
                    strokeWidth: parseInt(obj.options.strokeWidth),
                    strokeOpacity: obj.options.strokeOpacity,
                    name: obj.name,
                    id: obj.id
                }));
            });
            SOM.events.add("click", function (e) {
                SOM.each(x => { if (x.options.get('fillColor') == 'FF0000') x.options.set('fillColor', defaultColor); });
                defaultColor = `${e.get('target').options.get('fillColor')}`;
                e.get('target').options.set('fillColor', "FF0000");
                console.log(e.get('target').geometry.getCoordinates());
                console.log(e.get('target').options.get('name'));
                console.log(e.get('target').options.get('id'));
            })
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