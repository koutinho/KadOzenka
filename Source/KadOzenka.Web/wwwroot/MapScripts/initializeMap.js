var map;

function init(){
    var script = document.createElement('script');
    script.src = `${AppData.protocol}://api-maps.yandex.ru/${AppData.version}/?apikey=${AppData.key}&lang=${AppData.lang}`;
    document.head.appendChild(script);
    script.onload = function () {
        /***Добавление wms-скриптов***/
        //var wmsScript = document.createElement('script');
        //wmsScript.src = `/MapScripts/layer-wms.js`;
        //document.head.appendChild(wmsScript);
        ymaps.ready(function() { initMap(); });
    }
};

function initMap() {
	const url = new URL(window.location);
	const params = new window.URLSearchParams(url.search);
	map = new ymaps.Map(
		MapSettings.containerId,
		{
			center: params.has('center') ? params.get('center').split(",") : MapSettings.center,
			zoom: params.has('zoom') ? params.get('zoom') : MapSettings.zoom
		},
		{ suppressMapOpenBlock: true }
    );
    createDisplayCountWidget();
    createTargetWidget();
    creatFilterWidget();
    creatLayerWidget();
    AppData.defaultRemoveElements.forEach(x => map.controls.remove(x));
    changeDefaultControlPosition(map);
    addDisplayCountWidget(countWidgetPosition);
    addFilterWidget(filterWidgetPosition);
    addLayerWidget(layerWidgetPosition);
    GetClusterData(map.getBounds(), map.getZoom(), currentToken, params.has('objectId') ? params.get('objectId') : null);
    map.events.add('boundschange', function (event) { ChangeBounds(event); });
    //var projection = map.options.get('projection');        
    //console.log(projection.fromGlobalPixels([157696, 81408], 10));
    //console.log(projection.fromGlobalPixels([159232, 81408], 10));
    //console.log(projection.fromGlobalPixels([159232, 83456], 10));
    //console.log(projection.fromGlobalPixels([157696, 83456], 10));
    //map.geoObjects.add(new ymaps.GeoObject({
    //    geometry: {
    //        type: "Polygon",
    //        coordinates: [
    //            [
    //                [56.347639626746336, 36.5625],
    //                [56.347639626746336, 38.67187499999999],
    //                [54.75350830256672, 38.67187499999999],
    //                [54.75350830256672, 36.5625]
    //            ]
    //        ]
    //    }
    //}));
};

function changeDefaultControlPosition(map) { map.controls.get("rulerControl").options.set({position:{bottom: 10, right: 10}}); };

init();