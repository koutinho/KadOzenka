var map;
var editMode;

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
    SetAvaliableValues();
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
    AppData.defaultRemoveElements.forEach(x => map.controls.remove(x));
    changeDefaultControlPosition(map);
    addDisplayCountWidget(countWidgetPosition);
    addFilterWidget(filterWidgetPosition);
    GetClusterData(map.getBounds(), map.getZoom(), currentToken, params.has('objectId') ? params.get('objectId') : null);
    editMode = document.getElementById('cbEditMode').checked;
    map.events.add('boundschange', function (event) { ChangeBounds(event); });
    document.getElementById('cbEditMode').addEventListener("click", function () {
        editMode = document.getElementById('cbEditMode').checked;
        enableEditableMode();
    });
};

function changeDefaultControlPosition(map) { map.controls.get("rulerControl").options.set({position:{bottom: 10, right: 10}}); };

init();