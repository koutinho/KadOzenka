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
    map = new ymaps.Map(MapSettings.containerId, { center: MapSettings.center , zoom: MapSettings.zoom }, {suppressMapOpenBlock: true});
    AppData.defaultRemoveElements.forEach(x => map.controls.remove(x));
    changeDefaultControlPosition(map);
};

function changeDefaultControlPosition(map) {
    map.controls.get("rulerControl").options.set({position:{bottom: 10, right: 10}});
    map.controls.get("zoomControl").options.set({position:{top: 48, left: 10}});
};

init();