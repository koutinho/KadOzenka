var map;
var editMode;

function init() {

    var script = document.createElement('script');
    if (AppData.useSandBoxKey) {
        var xhr = new XMLHttpRequest();
        xhr.open("GET", `${AppData.protocol}://api-maps.yandex.ru/${AppData.version}/?apikey=${AppData.sandboxKey}&lang=${AppData.lang}`)
        xhr.onreadystatechange = function () {
            if (xhr.readyState === XMLHttpRequest.DONE && xhr.status === 200) {
                var data = xhr.responseText.replaceAll(AppData.sandboxKey, AppData.key);
                script.innerHTML = data;
                setTimeout(() => { initMap(); SetAvaliableValues(); }, 2000);
                document.head.appendChild(script);
            }
        };
        xhr.send();
    }
    else {
        script.src = srciptSrc;
        console.log(script.src);
        document.head.appendChild(script);
        script.onload = function () { ymaps.ready(function () { initMap(); }); };
        SetAvaliableValues();
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