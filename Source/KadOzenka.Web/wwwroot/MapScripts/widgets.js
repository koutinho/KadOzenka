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
        _onGetChildElement: function (parentDomContainer) { this._$content = $(`<div id="targetControl" onclick="toTarget()" class="targetControl"></div>`).appendTo(parentDomContainer); }
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
                    <div id="filterImage" class="filterImage inactive"></div>
                    <div id="allFiltersContainer" class="allFiltersContainer inactive">
                        <div class="filterPanelContainer">
                            <div class="filterPanel filterLabel">Тип сделки</div>
                            <div id="dealTypePanel" class="filterPanel"></div>
                        </div>
                        <div class="filterPanelContainer">
                            <div class="filterPanel filterLabel">Тип объекта недвижимости</div>
                            <div id="propertyTypePanel" class="filterPanel"></div>
                        </div>
                        <div class="filterPanelContainer">
                            <div class="filterPanel filterLabel">Сегмент рынка</div>
                            <div id="propertyMarketSegmentPanel" class="filterPanel"></div>
                            <div id="commercialMarketSegmentPanel" class="filterPanel"></div>
                        </div>
                        <div class="filterPanelContainer">
                            <div class="filterPanel filterLabel">Тип деления</div>
                            <div id="layersPanel" class="filterPanel">
                                 <div id="districtLayerFilterButton" class="layerButton filterButton inactive">Округа</div>
                                 <div id="regionLayerFilterButton" class="layerButton filterButton inactive">Районы</div>
                                 <div id="zoneLayerFilterButton" class="layerButton filterButton inactive">Зоны</div>
                                 <div id="quartalLayerFilterButton" class="layerButton filterButton inactive">Кварталы</div>
                            </div>
                        </div>
                        <div class="filterPanelContainer">
                            <div class="filterPanel filterLabel">Округ</div>
                            <div id="districtPanel" class="filterPanel"></div>
                        </div>
                        <div class="filterPanelContainer">
                            <div class="filterPanel filterLabel">Источник данных</div>
                            <div id="sourcePanel" class="filterPanel"></div>
                        </div>
                        <div class="filterPanelContainer">
                            <div class="filterPanel filterLabel">Дата актуальности</div>
                            <div id="dateFilter" class="filterPanel">
                                <div id="MapDataPicker" class="datepicker-here"></div>
                            </div>
                        </div>
                        <div class="filterPanelContainer">
                            <div class="filterPanel filterAdditional">
                                <div id="PaletteControl" class="extendButton inactive"></div>
                                <div id="allPaletteContainer" class="allPaletteContainer inactive">
                                    <div class="paletteContainer">
                                        <div class="innerContainer">
                                            <div id="colorPickerContainerInitial" class="colorPickerContainer">
                                                <section>
                                                    <div id="rgbInitialValue"></div>
                                                    <div id="rgbInitialPicker"></div>
                                                </section>
                                                <div id="rgbInitialShowPanel" class="colorShowPanel"></div>
                                            </div>
                                        </div>
                                        <div class="innerContainer">
                                            <div id="colorPickerContainerResult" class="colorPickerContainer">
                                                <section>
                                                    <div id="rgbResultValue"></div>
                                                    <div id="rgbResultPicker"></div>
                                                </section>
                                                <div id="rgbResultShowPanel" class="colorShowPanel"></div>
                                            </div>
                                        </div>
                                        <div class="innerContainer">
                                            <div class="splicedDataContainer" id="splicedDeltaContent"></div>
                                        </div>
                                    </div>
                                    <div class="slidecontainer">
                                        <input type="range" min="4" max="30" value="4" class="slider" id="splicedDeltaController">
                                    </div>
                                    <div class="legendContainer" id="legendPaleteContainer">
                                    </div>
                                    <div class="filterPanel">
                                        <div id="refreshHeatMapButton" class="characteristicsButton filterButton refresh inactive">
                                            Обновить
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                 </div>
            `).appendTo(parentDomContainer);
            this.addEventListeners();
        },
        addEventListeners: function () {

            $('#MapDataPicker').datepicker({ inline: true, onSelect: function onSelect(fd, date) { setActualDate(date); } });

            document.getElementById("filterImage").addEventListener("click", function (e) {
                document.getElementById("filterImage").classList.toggle("inactive");
                document.getElementById("allFiltersContainer").classList.toggle("inactive");
            });
            document.getElementById("PaletteControl").addEventListener("click", function (e) {
                document.getElementById("PaletteControl").classList.toggle("inactive");
                document.getElementById("allPaletteContainer").classList.toggle("inactive");
            });

            document.getElementById("districtLayerFilterButton").addEventListener("click", function (e) { changeMapType(MapZoneType.district, e.target); });
            document.getElementById("regionLayerFilterButton").addEventListener("click", function (e) { changeMapType(MapZoneType.region, e.target); });
            document.getElementById("zoneLayerFilterButton").addEventListener("click", function (e) { changeMapType(MapZoneType.zone, e.target); });
            document.getElementById("quartalLayerFilterButton").addEventListener("click", function (e) { changeMapType(MapZoneType.quartal, e.target); });

            document.getElementById("splicedDeltaController").addEventListener("input", function () {
                document.getElementById("splicedDeltaContent").innerHTML = this.value;
                createColorLegend(document.getElementById("splicedDeltaController").value,
                    document.getElementById('rgbInitialShowPanel').style.background,
                    document.getElementById('rgbResultShowPanel').style.background);
            });

            document.getElementById("refreshHeatMapButton").addEventListener('click', function (e) {
                if (document.getElementById("refreshHeatMapButton").classList.contains("inactive")) {
                    setHeatMapButtonState(true);
                    GetHeatMapData();
                }
            });

            createColorPicker('rgbInitialValue', 'rgbInitialPicker', 'rgbInitialShowPanel');
            createColorPicker('rgbResultValue', 'rgbResultPicker', 'rgbResultShowPanel');
            document.getElementById("splicedDeltaContent").innerHTML = document.getElementById("splicedDeltaController").value;
            createColorLegend(document.getElementById("splicedDeltaController").value,
                document.getElementById('rgbInitialShowPanel').style.background,
                document.getElementById('rgbResultShowPanel').style.background);
        }
    });
}

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
        }
    });
};

function addDisplayCountWidget(position) { map.controls.add(new countWidgetClass(), { float: 'none', position }); };

function addFilterWidget(position) {
    map.controls.add(new filterWidgetClass(), { float: 'none', position });
    GetFilterData();
};

function addTargetWidget(position) {
    if (!document.getElementById("targetControl")) {
        targetWidget = new targetWidgetClass();
        map.controls.add(targetWidget, { float: 'none', position });
    }
};

function addLoadWmsWidget(position) {
    let loadWmsControl = new loadWmsClass();
    loadWmsControl.events.add('click', () => { addWmsService(); });
    map.controls.add(loadWmsControl, { float: 'right' });
};

function removeTargetWidget() { map.controls.remove(targetWidget); };