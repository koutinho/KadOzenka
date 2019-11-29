var map;

function init(){
    var script = document.createElement('script');
    script.src = `${AppData.protocol}://api-maps.yandex.ru/${AppData.version}/?apikey=${AppData.key}&lang=${AppData.lang}`;
    document.head.appendChild(script);
    script.onload = function () { ymaps.ready(function() {initMap();}); }
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
    AppData.defaultRemoveElements.forEach(x => map.controls.remove(x));
    changeDefaultControlPosition(map);
    addDisplayCountWidget({ bottom: 10, left: 10 });
    GetClusterData(map.getBounds(), map.getZoom(), currentToken, params.has('objectId') ? params.get('objectId') : null);
    map.events.add('boundschange', function (event) { ChangeBounds(event); });
};

function changeDefaultControlPosition(map) {
    map.controls.get("rulerControl").options.set({position:{bottom: 10, right: 10}});
    map.controls.get("zoomControl").options.set({position:{top: 48, left: 10}});
};

init();