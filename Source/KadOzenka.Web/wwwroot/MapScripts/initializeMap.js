var map;

function init(){
    var script = document.createElement('script');
    script.src = `${AppData.protocol}://api-maps.yandex.ru/${AppData.version}/?apikey=${AppData.key}&lang=${AppData.lang}`;
    document.head.appendChild(script);
    script.onload = function () {
        ymaps.ready(function() {
            initMap();
            GetData();
        });
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
    AppData.defaultRemoveElements.forEach(x => map.controls.remove(x));
	changeDefaultControlPosition(map);

	map.events.add('boundschange', function (event) {
		const areParamsSet = new window.URLSearchParams((new URL(window.location)).search).has('center');

		const params = new window.URLSearchParams(window.location.search);
		const newCenter = event.get('newCenter');
		params.set('center', newCenter);
		const newZoom = event.get('newZoom');
		params.set('zoom', newZoom);
		if (window.history.pushState) {
			const newUrl = new URL(window.location.href);
			newUrl.search = params;
			window.history.pushState({ path: newUrl.href }, '', newUrl.href);
		}

		if (areParamsSet) {
			const newBounds = event.get('newBounds');
			const oldBounds = event.get('oldBounds');
			if (newBounds[0][0] !== oldBounds[0][0] ||
				newBounds[0][1] !== oldBounds[0][1] ||
				newBounds[1][0] !== oldBounds[1][0] ||
				newBounds[1][1] !== oldBounds[1][1]) {
				GetData(newBounds);
			}
		}
	});
};

function changeDefaultControlPosition(map) {
    map.controls.get("rulerControl").options.set({position:{bottom: 10, right: 10}});
    map.controls.get("zoomControl").options.set({position:{top: 48, left: 10}});
};

init();