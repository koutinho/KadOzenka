var map;

function init(){
    var script = document.createElement('script');
    script.src = `${AppData.protocol}://api-maps.yandex.ru/${AppData.version}/?apikey=${AppData.key}&lang=${AppData.lang}`;
    document.head.appendChild(script);
    script.onload = function () {
        var heatmapScript = document.createElement('script');
        heatmapScript.src = `${AppData.protocol}://yastatic.net/s3/mapsapi-jslibs/heatmap/0.0.1/heatmap.min.js`;
        document.head.appendChild(heatmapScript);
        heatmapScript.onload = function () {
            ymaps.ready(['Heatmap']).then(() => GetHeatmapData());
        };

        var wmsScript = document.createElement('script');
        wmsScript.src = `/MapScripts/layer-wms.js`;
        document.head.appendChild(wmsScript);

        ymaps.ready(function() {initMap();});
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
    //createLoadWmsWidget(); // кнопка для вызова модального окна wms сервиса
    AppData.defaultRemoveElements.forEach(x => map.controls.remove(x));
    changeDefaultControlPosition(map);
    addDisplayCountWidget(countWidgetPosition);
    addFilterWidget(filterWidgetPosition);
    //addLoadWmsWidget(); // кнопка для вызова модального окна wms сервиса
    GetClusterData(map.getBounds(), map.getZoom(), currentToken, params.has('objectId') ? params.get('objectId') : null);
    map.events.add('boundschange', function (event) { ChangeBounds(event); });
};

function changeDefaultControlPosition(map) {
    map.controls.get("rulerControl").options.set({position:{bottom: 10, right: 10}});
};

init();