function createDisplayCountWidget() {
    countWidgetClass = function (options) {
        countWidgetClass.superclass.constructor.call(this, options);
        this._$content = null;
        this._geocoderDeferred = null;
    };
    ymaps.util.augment(countWidgetClass, ymaps.collection.Item, {
        onAddToMap: function (map) {
            countWidgetClass.superclass.onAddToMap.call(this, map);
            this._lastCenter = null;
            this.getParent().getChildElement(this).then(this._onGetChildElement, this);
        },
        _onGetChildElement: function (parentDomContainer) {
            this._$content = $(`
                <div id="CountControl" class="displayCountControl">
                    <div id="CountControlText" class="content">Загрузка объектов</div>
                    <div class="preloaderImage"></div>
                </div>
            `).appendTo(parentDomContainer);
            this._mapEventGroup = this.getMap().events.group();
            this._mapEventGroup.add('boundschange', this._createRequest, this);
            this._createRequest();
        }
    });
};

function createTargetWidget() {
    targetWidgetClass = function (options) {
        targetWidgetClass.superclass.constructor.call(this, options);
        this._$content = null;
        this._geocoderDeferred = null;
    };
    ymaps.util.augment(targetWidgetClass, ymaps.collection.Item, {
        onAddToMap: function (map) {
            targetWidgetClass.superclass.onAddToMap.call(this, map);
            this._lastCenter = null;
            this.getParent().getChildElement(this).then(this._onGetChildElement, this);
        },
        _onGetChildElement: function (parentDomContainer) {
            this._$content = $(`<div id="targetControl" onclick="toTarget()" class="targetControl"></div>`).appendTo(parentDomContainer);
            this._createRequest();
        }
    });
};

function creatFilterWidget() {
    filterWidgetClass = function (options) {
        filterWidgetClass.superclass.constructor.call(this, options);
        this._$content = null;
        this._geocoderDeferred = null;
    };
    ymaps.util.augment(filterWidgetClass, ymaps.collection.Item, {
        onAddToMap: function (map) {
            filterWidgetClass.superclass.onAddToMap.call(this, map);
            this._lastCenter = null;
            this.getParent().getChildElement(this).then(this._onGetChildElement, this);
        },
        _onGetChildElement: function (parentDomContainer) {
            this._$content = $(`
                <div id="filterControl" class="filterControl">
                    <div id="filterImage" class="filterImage"></div>
                    <div id="filterContainer" class="filterContainer">
                        <div id="dealTypeFilter" class="filterMainButton">
                            <div class="filterHeader">
                                Тип сделки
                                <div id="DealTypeFilterCounter" class="filterCounter Hidden">99</div>
                            </div>
                            <div id="dealTypefilterBody" class="filterBody"></div>
                        </div>
                        <div id="marketSegmentFilter" class="filterMainButton">
                            <div class="filterHeader">
                                Сегмент рынка
                                <div id="DealTypeFilterCounter" class="filterCounter Hidden">99</div>
                            </div>
                            <div id="marketSegmentfilterBody" class="filterBody"></div>
                        </div>
                        <div id="priceFilter" class="filterMainButton">
                            <div class="filterHeader">Цена</div>
                            <div class="filterBody">
                                <input id="PriceFromFilterTextBox" class="filterBodyTextBox" type="text" placeholder="Цена от">
                                <input id="PriceToFilterTextBox" class="filterBodyTextBox" type="text" placeholder="Цена до">
                                <div id="SearchPriceFilterButton" class="filterBodyRealButton">Применить</div>
                            </div>
                        </div>
                        <div id="metroFilter" class="filterMainButton">
                            <div class="filterHeader">Метро</div>
                            <div class="filterBody">
                            </div>
                        </div>
                    </div>
                </div>
            `).appendTo(parentDomContainer);
            this._createRequest();
        }
    });
};

function creatLayerWidget() {
    layerWidgetClass = function (options) {
        layerWidgetClass.superclass.constructor.call(this, options);
        this._$content = null;
        this._geocoderDeferred = null;
    };
    ymaps.util.augment(layerWidgetClass, ymaps.collection.Item, {
        onAddToMap: function (map) {
            layerWidgetClass.superclass.onAddToMap.call(this, map);
            this._lastCenter = null;
            this.getParent().getChildElement(this).then(this._onGetChildElement, this);
        },
        _onGetChildElement: function (parentDomContainer) {
            this._$content = $(`
                <div id="layerControl" class="filterControl">
                    <div id="layerImage" class="layerImage"></div>
                    <div id="layerContainer" class="filterContainer">
                        <div id="districtLayer" class="filterMainButton" onclick="changeMapType(1, 'districtLayerHeader')">
                            <div id="districtLayerHeader" class="filterHeader">Округа</div>
                        </div>
                        <div id="regionLayer" class="filterMainButton" onclick="changeMapType(2, 'regionLayerHeader')">
                            <div id="regionLayerHeader" class="filterHeader">Районы</div>
                        </div>
                        <div id="zoneLayer" class="filterMainButton" onclick="changeMapType(3, 'zoneLayerHeader')">
                            <div id="zoneLayerHeader" class="filterHeader">Зоны</div>
                        </div>
                        <div id="quartalLayer" class="filterMainButton" onclick="changeMapType(4, 'quartalLayerHeader')">
                            <div id="quartalLayerHeader" class="filterHeader">Кварталы</div>
                        </div>
                    </div>
                </div>
            `).appendTo(parentDomContainer);
            this._createRequest();
        }
    });
};

function addDisplayCountWidget(position) { map.controls.add(new countWidgetClass(), { float: 'none', position }); };

function addFilterWidget(position) {
    map.controls.add(new filterWidgetClass(), { float: 'none', position });
    GetFilterData();
};

function addLayerWidget(position) { map.controls.add(new layerWidgetClass(), { float: 'none', position }); };

function changeMapType(count, id) {
    Array.from(document.getElementsByClassName("filterHeader")).forEach(x => { if (x.id != id) x.classList.remove("active"); });
    document.getElementById(id).classList.toggle("active");
    if (document.getElementById(id).classList.contains("active")) currentLayer = count; else currentLayer = null;
    changeLayer();
}

function addTargetWidget(position) {
    if (!document.getElementById("targetControl")) {
        targetWidget = new targetWidgetClass();
        map.controls.add(targetWidget, { float: 'none', position });
    }
};

function removeTargetWidget() { map.controls.remove(targetWidget); };

function createToggleHeatmapWidget() {
    toggleHeatmapClass = function (options) {
        toggleHeatmapClass.superclass.constructor.call(this, options);
        this._$content = null;
        this._geocoderDeferred = null;
    };
    ymaps.util.augment(toggleHeatmapClass, ymaps.collection.Item, {
        onAddToMap: function (map) {
            toggleHeatmapClass.superclass.onAddToMap.call(this, map);
            this._lastCenter = null;
            this.getParent().getChildElement(this).then(this._onGetChildElement, this);
            
        },
        _onGetChildElement: function (parentDomContainer) {
            this._$content = $(`
                <div id="ToggleHeatmap" class="toggleHeatmap">
                    <div id="ToggleHeatmapText" class="content">Отобразить тепловую карту</div>
                </div>
            `).appendTo(parentDomContainer);

            this._eventsGroup = ymaps.domEvent.manager.group(this._$content[0]);
            this._eventsGroup.add('click', function () {
                this.events.fire('click');
            }, this);
            this._createRequest();
        }
    });
};

function addToggleHeatmapWidget(position) {
    let toggleHeatmapControl = new toggleHeatmapClass();
    toggleHeatmapControl.events.add('click', () => {
        toggleHeatmap();
    });
    map.controls.add(toggleHeatmapControl, { float: 'none', position });
};

function refreshFilterWidget(filterInfo) {
    var dealTypeData = '', marketSegmentData = '';
    filterInfo.dealTypeList.forEach(x => { dealTypeData += `<div id="${x.Name}FilterButton" class="filterBodyButton">${x.Value.replace(new RegExp(' ', 'g'), '&nbsp;')}</div>`; });
    filterInfo.marketSegmentList.forEach(x => { marketSegmentData += `<div id="${x.Name}FilterButton" class="filterBodyButton">${x.Value.replace(new RegExp(' ', 'g'), '&nbsp;')}</div>`; });
    document.getElementById('dealTypefilterBody').innerHTML = dealTypeData;
    document.getElementById('marketSegmentfilterBody').innerHTML = marketSegmentData;
}

function refreshToggleHeatmapWidget(isHeatmapWidgetVisible) {
    if (isHeatmapWidgetVisible) {
        document.getElementById('ToggleHeatmapText').innerHTML = "Скрыть тепловую карту";
    } else {
        document.getElementById('ToggleHeatmapText').innerHTML = "Отобразить тепловую карту";
    }
}

//TODO: кнопка для вызова модального окна wms сервиса
function createLoadWmsWidget() {
    loadWmsClass = function (options) {
        loadWmsClass.superclass.constructor.call(this, options);
        this._$content = null;
        this._geocoderDeferred = null;
    };
    ymaps.util.augment(loadWmsClass, ymaps.collection.Item, {
        onAddToMap: function (map) {
            loadWmsClass.superclass.onAddToMap.call(this, map);
            this._lastCenter = null;
            this.getParent().getChildElement(this).then(this._onGetChildElement, this);

        },
        _onGetChildElement: function (parentDomContainer) {
            this._$content = $(`
                <div id="LoadWms" class="loadWmsButton">
                    <div id="LoadWmsText" class="content">Загрузить данные с WMS сервиса</div>
                </div>
            `).appendTo(parentDomContainer);

            this._eventsGroup = ymaps.domEvent.manager.group(this._$content[0]);
            this._eventsGroup.add('click', function () {
                this.events.fire('click');
            }, this);
            this._createRequest();
        }
    });
};

function addLoadWmsWidget(position) {
    let loadWmsControl = new loadWmsClass();
    loadWmsControl.events.add('click', () => {
        addWmsService();
    });
    map.controls.add(loadWmsControl, { float: 'right' });
};