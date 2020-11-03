//Поиск точек, поиск адреса и кад номера.Настройки карты
var map;
var findPoints = [];
var findPointsIds = [];
var $addresses = $('#address');
var typeShortList = [/г\./gi, /пгт\./gi, /п\./gi, /д\./gi, /c\./gi, /ул\./gi, /строение/gi];
var myMarker;
var myPlacemark;
var currentAddress = null;
var targetObjectId = null;

function enableSegmentControl() {
    $('#segment').data('kendoDropDownList') && $('#segment').data('kendoDropDownList').enable(true);
}

function disableSegmentControl() {
    $('#segment').data('kendoDropDownList') && $('#segment').data('kendoDropDownList').enable(false);
}

/**
 * Загрузка оценочных параметров на страницу расчетов
 */
function loadCostFactorsForSegment() {
    if (!$('#Kn').val() && !$('#targetMarketObjectId').val()) {
        return;
    }
    kendo.ui.progress($('body'), true);
    var $containerCostFactors = $('#costFactors');
    $containerCostFactors.hide();
    var kn = $('#Kn').val();
    var targetMarketObjectId = $('#targetMarketObjectId').val();
    var segment = $('#segment').data('kendoDropDownList') && $('#segment').data('kendoDropDownList').value();
    var url = "/ExpressScore/GetCostFactorsForCalculate" + helper.objectToQuerystring({ targetKn: kn, targetMarketObjectId, segment }); // "@Url.Action("GetCostFactorsForCalculate", "ExpressScore")"
    $containerCostFactors.load(url,
        function (responseText, status) {
            if (status === "error") {
                Common.ShowError(responseText);
                $containerCostFactors.empty();
            }
            kendo.ui.progress($('body'), false);
            $containerCostFactors.show();
        });
}

function handlerChangeSegment() {
    $('.calculate').prop('disabled', true);
    removeFindPoints();
    loadCostFactorsForSegment();
}

function setCurrentAddress(address) {
    if (address) {
        currentAddress = address;
        return;
    }
    currentAddress = null;
}

/**
 * @return текущий формализованный адрес
 */
function getCurrentAddress() {
    return currentAddress;
}

function removeMyMarker() {
    if (map) {
        map.geoObjects.remove(myMarker);
        myMarker = null;
    }
}

function removeMyPlacemark() {
    if (map) {
        map.geoObjects.remove(myPlacemark);
        myPlacemark = null;
    }
}

function removeFindPoints() {
    findPoints.forEach(function (item) { if (map) map.geoObjects.remove(item); });
    findPoints = [];
    findPointsIds = [];
}

function declOfNum(n) {
    n = Math.abs(n) % 100;
    var n1 = n % 10;
    if (n > 10 && n < 20) {
        return 'ов';
    }
    if (n1 > 1 && n1 < 5) {
        return 'a';
    }
    if (n1 == 1) {
        return '';
    }
    return 'ов';
}

$("#slider").kendoSlider({
    increaseButtonTitle: "Right",
    decreaseButtonTitle: "Left",
    min: -0,
    max: 30,
    smallStep: 1,
    largeStep: 10,
    showButtons: false,
    dragHandleTitle: 0
});
var slider = $("#slider").getKendoSlider();
slider.wrapper.css("width", "100%");
slider.resize();
slider.bind('change', handlerChangeSlider);

function handlerChangeSlider(e) { $(slider.wrapper).find('.k-draghandle').prop('title', e.value); }

//Устанавливаем кад номер в 2-х случиях
//1 - ввели самии адрес
// 2 - указали точку на карте
function setKadNumber(address) {
    getKadNumber(address).then(function (data) {
        if (data.kadNumber) {
            $('#Kn').val(data.kadNumber);
            $('#targetMarketObjectId').val(data.marketObjectId);
            enableSegmentControl();
            loadCostFactorsForSegment();
        }
    }).catch(function (data) {
        disableSegmentControl();
        var errors = getErrors(data.Errors);
        Common.ShowError(errors);
        $('#Kn').val('');
        console.error("Произошла ошибка при получении кадастрового номера", data);
    });
}

// promises
var getKadNumber = (currentAddresses) => new Promise(function (resolve, reject) {
    var url = "/ExpressScore/GetKadNumber" + `?address=${currentAddresses}`;// "@Url.Action("GetKadNumber", "ExpressScore")" + `?address=${currentAddresses}`;
    kendo.ui.progress($('body'), true);
    $.get(url).done(function (data) {
        if (data.response) resolve(data.response);
        if (data.Errors) reject(data);
    }).fail(function (data) { reject(data); }).always(function () { kendo.ui.progress($('body'), false); });
});

var getAddressByKadNumber = (kadNumber) => new Promise(function (resolve, reject) {
    var url = "/ExpressScore/GetAddressByKadNumber" + `?kadNumber=${kadNumber}`;//"@Url.Action("GetAddressByKadNumber", "ExpressScore")" + `?kadNumber=${kadNumber}`;
    kendo.ui.progress($('body'), true);
    $.get(url).done(function (data) {
        if (data.response) resolve(data.response);
        if (data.Errors) reject(data.Errors);
    }).fail(function (data) { reject(data); }).always(function () { kendo.ui.progress($('body'), false); });
});

//fias
$addresses.on('focus', function () { $addresses.on('kladr_close_before', closeBefore); });
$addresses.on('blur', function () { $addresses.off('kladr_close_before', closeBefore); });

var addressForCompare = null;
function closeBefore() {
    if ($('#address').val() && $('#address').val() !== addressForCompare) {
        addressForCompare = $('#address').val();
        var myGeocoder = ymaps.geocode($('#address').val());
        setMarkerByGeoCoder(myGeocoder).then(function (geoObject) {
            removeFindPoints();
            if (geoObject.getPremiseNumber()) {
                setCurrentAddress(geoObject.getAddressLine());
                setKadNumber(geoObject.getAddressLine());
            }
        });
    }
}

$addresses.fias({
    oneString: true,
    parentType: $.fias.type.region,
    parentId: '7700000000000',
    labelFormat: function (obj, query) { return getCorrectRow(obj); },
    valueFormat: function (obj, query) { return getCorrectRow(obj); },
    sendBefore: function (query) {
        if (query.name) typeShortList.forEach(function (str) { query.name = query.name.replace(str, ''); });
        else query.name = '';
    }
});

function getCorrectRow(obj) {
    var arrayNames = [];
    obj.parents.forEach(function (item) {
        switch (item.contentType) {
            case 'street':
                arrayNames.splice(3, 0, ' ' + item.typeShort + '. ' + item.name);
                break;
            case 'city':
                arrayNames.splice(2, 0, ' ' + item.typeShort + '. ' + item.name);
                break;
            case 'region':
                arrayNames.splice(0, 0, ' ' + item.typeShort + '. ' + item.name);
                break;
            case 'district':
                arrayNames.splice(1, 0, ' ' + item.typeShort + '. ' + item.name);
                break;
            default:
                break;
        }
    });
    arrayNames.push(' ' + obj.typeShort + '. ' + obj.name);
    return arrayNames.filter((v, i, a) => a.indexOf(v) === i).join(',');
}

//fias geopoint
function setMarkerByGeoCoder(geocoder) {
    if (typeof geocoder === 'string') geocoder = ymaps.geocode(geocoder);
    return new Promise(function (resolve) {
        geocoder.then(function (res) {
            if (myMarker) removeMyMarker();
            if (myPlacemark) removeMyPlacemark();
            var firstGeoObject = res.geoObjects.get(0), bounds = firstGeoObject.properties.get('boundedBy');
            myMarker = firstGeoObject;
            map.geoObjects.add(firstGeoObject);
            map.setBounds(bounds, { checkZoomRange: true });
            resolve(firstGeoObject);
        });
    });
}



/**
 * Получаем значения комплексных факторов для поиска
 * @return {string} сереализованный Json
 */
function getComplexSearchParameters() {
    var res = [];
    $.each($('#costFactors').find('[id^="chFactor"]:checked'), function() {
        var $wrapperFactor = $(this).closest('.wrapper-factor');
        var $control = $wrapperFactor.find('[id*="DefaultValue"]');
        if ($control) {
            var attributeId = $control.attr('DataAttributeId');
            var referenceId = $control.attr('DataReferenceId');
            var value = $control.data('kendoDropDownList') && $control.data('kendoDropDownList').value();
            res.push({
                IdAttribute: attributeId,
                Value: value,
                referenceId
            });
        }
    });
    return JSON.stringify(res);
}

/**
 * Получаем значения комплексных факторов для расчета
 *@return {string} сереализованный Json
 */
function getComplexCalculateParameters() {
    var res = [];
    $.each($('#costFactors').find('[id*="DefaultValue"]'), function () {
        var attributeId = $(this).attr('DataAttributeId');
        var referenceId = $(this).attr('DataReferenceId');
        var value = $(this).data('kendoDropDownList') && $(this).data('kendoDropDownList').value();
        res.push({
            IdAttribute: attributeId,
            Value: value,
            referenceId
        });
    });
    return JSON.stringify(res);
}

// инициализация карты и евенты
function initMap(src) {
    var script = document.createElement('script');
    script.src = AppData.useSandBoxKey
        ? `${AppData.protocol}://api-maps.yandex.ru/${AppData.version}/?apikey=${AppData.sandboxKey}&lang=${
        AppData.lang}`
        : src;
    //`${AppData.protocol}://api-maps.yandex.ru/${AppData.version}/?apikey=${AppData.key}&lang=${AppData.lang}`;
    script.type = "text/javascript";
    document.head.appendChild(script);
    script.onload = function () {
        ymaps.ready(function () {
            map = new ymaps.Map('map',
                {
                    center: MapSettings.center,
                    zoom: MapSettings.zoom,
                    controls: ['fullscreenControl', 'zoomControl']
                });
            map.controls.get("zoomControl").options.set({ position: { top: 10, left: 10 } });
            map.events.add('click',
                function (e) {
                    // Получение координат щелчка
                    var coords = e.get('coords');
                    removeFindPoints();
                    clearFields();
                    if (myMarker) removeMyMarker();
                    if (myPlacemark) myPlacemark.geometry.setCoordinates(coords);
                    else {
                        myPlacemark = createPlacemark(coords);
                        map.geoObjects.add(myPlacemark);
                        myPlacemark.events.add('dragend',
                            function () { getAddress(myPlacemark.geometry.getCoordinates()); });
                    }
                    getAddress(coords);
                });
        });
    }
};

function clearFields() {
    $('#address').val('');
    $('#Kn').val('');
}

function getAddress(coords) {
    myPlacemark.properties.set('iconCaption', 'поиск...');
    ymaps.geocode(coords).then(function (res) {
        var firstGeoObject = res.geoObjects.get(0);
        if (firstGeoObject.getAdministrativeAreas().length === 0 ||
            firstGeoObject.getAdministrativeAreas()[0] !== "Москва") {
            removeMyPlacemark();
            Common.ShowError("Объект за пределами г.Москва");
            return;
        } else {
            $('.k-notification-error').each(function () {
                $(this).hide();
            });
        }
        if (firstGeoObject.getPremiseNumber()) {
            $('#address').val(firstGeoObject.getAddressLine().replace('Россия, ', ''));
            setKadNumber(firstGeoObject.getAddressLine());
            setCurrentAddress(firstGeoObject.getAddressLine());
        }
        myPlacemark.properties.set({
            // Формируем строку с данными об объекте.
            iconCaption: [
                firstGeoObject.getLocalities().length
                    ? firstGeoObject.getLocalities()
                    : firstGeoObject.getAdministrativeAreas(),
                firstGeoObject.getThoroughfare() || firstGeoObject.getPremise()
            ].filter(Boolean).join(', '),
            balloonContent: firstGeoObject.getAddressLine()
        });
    });
}

function createPlacemark(coords) {
    return new ymaps.Placemark(coords,
        { iconCaption: 'поиск...' },
        { preset: 'islands#violetDotIconWithCaption', draggable: true });
}

//Пересчет стоимости
function updateCost(cost, squareCost) {
    if (cost && squareCost) {
        $('#cost').text(cost.toLocaleString());
        $('#squareCost').text(squareCost.toLocaleString());
    }
}

function updateReportId(reportId = null) { if (reportId) $('#report').data('report-id', reportId); }



/**
 * Выполнение расчетов
 * @param {number} scenarioType тип сценария расчета
 */
function executeCalculate(scenarioType = null) {
    if (scenarioType === null || scenarioType === 0) {
        Common.ShowError("Не удалось получить тип расчета. Попробуйте еще раз");
        return;
    }
    var complexCalculateParameters = getComplexCalculateParameters();
    var data = {
        selectedPoints: findPointsIds,
        targetObjectId,
        scenarioType,
        segment: $('#segment').val(),
        address: $('#address').val(),
        kn: $('#Kn').val(),
        dealType: $("input[name='DealTypeShort']:checked").val(),
        targetMarketObjectId: $('#targetMarketObjectId').val(),
        complexCalculateParameters,
        square: $("#square").val()
    }
    var topBody = window.top.document.body;
    kendo.ui.progress($(topBody), true);
    var url = "/ExpressScore/CalculateCostTargetObject";
    $.post(url, data).done(function (data) {
        if (data.Errors) {
            var errors = getErrors(data.Errors);
            Common.ShowError(errors);
            return;
        }
        $('#successDialog').data('kendoDialog') && $('#successDialog').data('kendoDialog').open();
        $('.wrapper-success-dialog').html(data);
    })
        .fail(function (response) {
            Common.ShowError(response.responseText);
        })
        .always(function () { kendo.ui.progress($(topBody), false); });
    return;
}

function showDialog() { $('#dialog').data('kendoDialog') && $('#dialog').data('kendoDialog').open(); }

function initDialog() {
    $('#dialog').kendoDialog({
        width: "450px",
        title: "Выберите сценарий расчета",
        closable: false,
        modal: true,
        content: '<div class="wrapper-scenario">' + '<div id="scenario"></div>' + '</div>',
        visible: false,
        buttonLayout: "normal",
        actions: [
            { text: 'Отмена' },
            {
                text: 'Выбрать',
                primary: true,
                action: function (e) {
                    if (e.sender.element.find('#scenario')) {
                        var value = e.sender.element.find('#scenario').data('kendoDropDownList') &&
                            e.sender.element.find('#scenario').data('kendoDropDownList').value() ||
                            null;
                        executeCalculate(value);
                    }
                    return true;
                }
            }
        ]
    });

    $('#scenario').kendoDropDownList({
        dataTextField: "Text",
        dataValueField: "Value",
        value: 1,
        dataSource: {
            type: "json",
            transport: {
                read: "/ExpressScore/GetScenarioCalculate"
            }
        }
    });

    $('#successDialog').kendoDialog({
        width: 1300,
        height: 600,
        title: "Расчет успешно выполнен",
        closable: false,
        modal: true,
        content: '<div class="wrapper-success-dialog"></div>',
        visible: false,
        buttonLayout: "normal",
        actions: [{ text: 'Ок' }]
    });
}

function excludeFromCalculation() {
    checkMiniCard(data);
    var initElement = $(this);
    var buttonId = initElement[0].element[0].id;
    var objId = parseInt(buttonId.split('_')[1]);
    findPointsIds.remove(objId);
    findPoints.forEach(function (item) {
        if (map) {
            if (item.properties._data.pointId === objId) map.geoObjects.remove(item);
        }
    });
    if (slider) {
        slider.value(findPointsIds.length);
        $(slider.wrapper).find('.k-draghandle').prop('title', findPointsIds.length);
    }
    Common.ShowMessage("Объект исключен из расчета");
}

function slide(rightSlide, data) {
    console.log("slided");
    if (rightSlide) {
        if (data.currentSlide >= data.miniCardsContent.length - 1) data.currentSlide = -1;
        $(`#centralBlock_${data.id}`).html(data.miniCardsContent[++data.currentSlide]);
    } else {
        if (data.currentSlide <= 0) data.currentSlide = data.miniCardsContent.length;
        $(`#centralBlock_${data.id}`).html(data.miniCardsContent[--data.currentSlide]);
    }
    subscribeSlider(data);
}

function subscribeSlider(data) {
    $(`#numOfObjs_${data.objs[data.currentSlide].id}`)
        .text(`${data.currentSlide + 1} из ${data.miniCardsContent.length}`);
    $(`#excludeFromCalculation_${data.objs[data.currentSlide].id}`)
        .kendoButton({ click: function (e) { excludeFromCalculation(data); } });
}

function checkMiniCard(data) {
    if (data.miniCardsContent.length <= 1) {
        $(`#arrowLeft_${data.id}`).css("display", "none");
        $(`#arrowRight_${data.id}`).css("display", "none");
    } else {
        $(`#arrowLeft_${data.id}`).css("display", "block");
        $(`#arrowRight_${data.id}`).css("display", "block");
    }
}

/**
 * обработка после загрузки старницы
 */
$(document).ready(function () {
    $('#Kn').on('blur',
        function () {
            var val = $(this).val().replace(/_/gi, '');
            $('#Kn').val(val);
            if (!!val && !val.startsWith(':')) {
                getAddressByKadNumber(val).then(function (data) {
                    if (data.address) {
                        $('#address').val(data.address.replace('Россия, ', ''));
                        $('#targetMarketObjectId').val(data.marketObjectId);
                        setMarkerByGeoCoder(data.address).then(function () { });
                        setCurrentAddress(data.address);
                        enableSegmentControl();
                        loadCostFactorsForSegment();
                    }
                }).catch(function (data) {
                    disableSegmentControl();
                    var errors = getErrors(data);
                    $('#address').val('');
                    Common.ShowError(errors);
                    console.error("Произошла ошибка при получении адреса", error);
                });
            }
        });

    /**
     * Поиск аналогов (обработка жмака по кнопке поиск)
     */
    $('.analogs').on('click',
        function () {
            removeFindPoints();
            var point = myPlacemark || myMarker;
            var coordinates = (point && point.geometry.getCoordinates()) || null;
            var segment = $('#segment').val();
            var quality = $("#slider").getKendoSlider() && $("#slider").getKendoSlider().value() || 0;
            var dealTypeShort = $("input[name='DealTypeShort']:checked").val();
            var actualDate = kendo.toString($("#ActualDate").data('kendoDatePicker').value(), "MM/dd/yyyy");
            var complexSearchParameters = "";
            var kn = $('#Kn').val();
            if (quality === 0) {
                Common.ShowError('Выберите количество аналогов');
                return;
            }
            if (!segment || segment === "0") {
                Common.ShowError('Заполните сегмент');
                return;
            }
            if (!coordinates) {
                Common.ShowError('Выберите или заполните целевой объект');
                return;
            }
            if (!actualDate) {
                Common.ShowError('Заполните дату актуальности ');
                return;
            }

            complexSearchParameters = getComplexSearchParameters();
            kendo.ui.progress($('body'), true);

            var data = {
                segment,
                kn,
                dealTypeShort,
                actualDate,
                quality,
                SelectedLat: coordinates[0],
                SelectedLng: coordinates[1],
                SearchParameters: complexSearchParameters
            }
            var url = "/ExpressScore/GetNearestObjects" + helper.objectToQuerystring(data);// "@Url.Action("GetNearestObjects", "ExpressScore")" +;

            $.get(url).done(function (data) {
                if (data.updateTargetObjectUrl) {
                    $("#updateTargetObject").html("");
                    $("#updateTargetObject").load(data.updateTargetObjectUrl);
                    var modal = $("#updateTargetObject");
                    ShowModal(modal,
                        '50%',
                        '50%',
                        "Внесение недостающих данных",
                        () => {
                            if (myMarker) removeMyMarker();
                            if (myPlacemark) removeMyPlacemark();
                            clearFields();
                        });
                    return;
                }
                if (data.Errors) {
                    var errors = getErrors(data.Errors);
                    Common.ShowError(errors);
                    return;
                }
                if (data.response && data.response.targetObjectId) targetObjectId = data.response.targetObjectId;
                if (data.response && data.response.coordinates) {
                    Common.ShowMessage(
                        `Было найдено ${data.response.coordinates.length} объект${declOfNum(data.response
                            .coordinates.length)}.`);
                    $(slider.wrapper).find('.k-draghandle').prop('title', data.response.coordinates.length);
                    slider.value(data.response.coordinates.length);
                    $('.calculate').prop('disabled', false);

                    var coords_array = [];
                    data.response.coordinates.forEach(function (data) {
                        var rindex = coords_array.findIndex(coord => (coord.lng.toFixed(4) == data.Lng.toFixed(4) &&
                            coord.lat.toFixed(4) == data.Lat.toFixed(4)));
                        if (rindex != -1) {
                            coords_array[rindex].objs.push({
                                id: data.Id,
                                address: data.Address,
                                area: data.Area,
                                cadastralNumber: data.CadastralNumber,
                                dealType: data.DealType,
                                images: data.Images,
                                market: data.Market,
                                price: data.Price,
                                pricePerMeter: data.PricePerMeter,
                                propertyMarketSegment: data.PropertyMarketSegment,
                                propertyType: data.PropertyType
                            });
                            coords_array[rindex].miniCardsContent.push(data.ObjectMiniCardContent);
                        } else
                            coords_array.push({
                                lng: data.Lng,
                                lat: data.Lat,
                                miniCardContainer: data.ObjectMiniCard,
                                id: data.Id,
                                address: data.Address,
                                currentSlide: 0,
                                objs: [
                                    {
                                        id: data.Id,
                                        address: data.Address,
                                        area: data.Area,
                                        cadastralNumber: data.CadastralNumber,
                                        dealType: data.DealType,
                                        images: data.Images,
                                        market: data.Market,
                                        price: data.Price,
                                        pricePerMeter: data.PricePerMeter,
                                        propertyMarketSegment: data.PropertyMarketSegment,
                                        propertyType: data.PropertyType
                                    }
                                ],
                                miniCardsContent: [data.ObjectMiniCardContent]
                            });
                    });

                    console.log(data.response.coordinates);
                    console.log(coords_array);

                    coords_array.forEach(function (data) {
                        data.objs.forEach(obj => findPointsIds.push(obj.id));
                        var balloonContentLayout = ymaps.templateLayoutFactory.createClass(
                            data.miniCardContainer,
                            {
                                build: function () {
                                    balloonContentLayout.superclass.build.call(this);
                                    $(`#centralBlock_${data.id}`).html(data.miniCardsContent[0]);
                                    $(`#arrowLeft_${data.id}`).click(function () { slide(false, data); });
                                    $(`#arrowRight_${data.id}`).click(function () { slide(true, data); });
                                    subscribeSlider(data);
                                    checkMiniCard(data);
                                },
                                clear: function () {
                                    $("#excludeFromCalculation_" + data.objs[data.currentSlide].id)
                                        .unbind('click', excludeFromCalculation);
                                    balloonContentLayout.superclass.clear.call(this);
                                    data.currentSlide = 0;
                                }
                            }
                        );
                        var newPoint = new ymaps.Placemark(
                            [data.lat, data.lng],
                            { pointId: data.id },
                            {
                                balloonContentLayout: balloonContentLayout,
                                balloonPanelMaxMapArea: 0,
                                preset: 'islands#orangeDotIconWithCaption'
                            }
                        );
                        newPoint.properties.set({
                            iconCaption: data.miniCardsContent.length +
                                ` объект${declOfNum(data.miniCardsContent.length)}. ${data.address}`
                        });
                        newPoint.options.set('balloonMinWidth', 500);
                        newPoint.options.set('balloonMaxWidth', 800);
                        findPoints.push(newPoint);
                        map.geoObjects.add(newPoint);
                    });
                    map.setZoom(13);
                }
            }).always(function () { kendo.ui.progress($('body'), false); });
        });

    initDialog();
    $('.calculate').on('click',
        function () {
            showDialog();
        });

    $('#square').on('blur', function () {
        var currentSquare = parseFloat($(this).val());
        var squareControl = $('#costFactors').find('[TypeControl*="SquareFactor"]')[0];
        if (squareControl) {
            var $dropDownSquare = $(squareControl).data('kendoDropDownList');
            var dataItem = $dropDownSquare && $dropDownSquare.dataItem();
            var items = $dropDownSquare && $dropDownSquare.dataSource.data() || [];

            if (dataItem && dataItem.item.useInterval) {
                items.forEach(function (item) {
                    if (item.item.useInterval) {
                        try {
                            var valueFrom = parseFloat(item.item.valueFrom);
                            var valueTo = parseFloat(item.item.valueTo);
                            var isThisInterval = currentSquare >= valueFrom && currentSquare < valueTo;
                            if (isThisInterval) {
                                skipChangeSquareHandler = true;
                                $dropDownSquare.value(item.Value);
                            };
                        } catch (e) {
                            console.error(e);
                        }
                    }
                });
            } else if (dataItem && !dataItem.item.useInterval) {
                try {
                    var map = new Map();
                    items.forEach(function (item) {
                        if (!item.item.useInterval) {
                            var delta = Math.abs(parseFloat(item.item.value) - currentSquare);
                            map.set(item.Value, delta);
                        }
                    });
                    var val = [...map.entries()].sort((a, b) => a[1] - b[1])[0][0];
                    skipChangeSquareHandler = true;
                    $dropDownSquare.value(val);
                } catch (e) {
                    console.error(e);
                }
            }

        }
    });
});


var helper = {

    /**
     * 
     * @param {Object<any>} obj 
     * @returns {string} 
     */
    objectToQuerystring: (obj) => {
        return Object.keys(obj).reduce(function (str, key, i) {
            var delimiter, val;
            delimiter = (i === 0) ? '?' : '&';
            key = encodeURIComponent(key);
            val = encodeURIComponent(obj[key]);
            return [str, delimiter, key, '=', val].join('');
        }, '');
    }
}

//Хендлеры
// переменная для устаранения циклических событий
var skipChangeSquareHandler = false;
function handlerChangeSquare(e) {

    if (skipChangeSquareHandler) {skipChangeSquareHandler = false; return;}
    var $control = this;
    var item = $control.dataItem();

    var isIntervalReference = item && item.item.useInterval;
    var value;
    if (isIntervalReference) {
         value = item && item.item.valueFrom;
    } else {
        value = item && item.item.value;
    }
    if (value) {
        $('#square').data('kendoNumericTextBox') && $('#square').data('kendoNumericTextBox').value(parseFloat(value));
    }
}