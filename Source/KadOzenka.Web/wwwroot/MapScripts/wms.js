function addWmsService() {
    //TODO: вызов модального окна для загрузки слоев wms сервиса
    const container = document.getElementById("wmsLoadWindowContainer");
    container.classList.remove("Hidden");

    const window = $('#wmsLoadWindow').kendoWindow({
        visible: false,
        resizable: false,
        modal: true,
        title: 'Параметры WMS',
        width: 950,
        height: 450,
        close: function () {
            container.classList.add("Hidden");
        },
        open: function () {
            initWmsLoadWindowContent();
        }
    }).data('kendoWindow').content($('#wmsLoadWindowTemplate').html());
    window.center();
    window.open();

    //TODO: образец добавления слоев из росреестра на нашу карту (модуль в файле layer-wms.js)
    //ymaps.modules.require(['LayerWMS'], function(LayerWMS) {
    //    var layerWMS = new LayerWMS(
    //        'http://pkk5.rosreestr.ru/arcgis/services/Cadastre/CadastreWMS/MapServer/WMSServer?',
    //        {
    //            layers: '2,3,4,5,6,8,9,10,11,12,14,15,16,18,19,20,21,22,24,25,27,28,29,30,31,32,33',  
    //            uppercase: true,
    //            transparent: true,
    //            format: 'image/png',
    //            crs: 'CRS:84',
    //            version: '1.3.0',
    //            styles: ',,,,,,,,,,,,,,,,,,,,,,,,,,',
    //            //styles: ',',
    //            // server need offset?
    //            /*
    //            offsetX: 0.0013,
    //            offsetY: 0.00058
    //            */
    //        });
    //    map.layers.add(layerWMS);
       
    //});
}