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
                        <div id="dealTypePanel" class="filterPanel"></div>
                        <div id="propertyMarketSegmentPanel" class="filterPanel"></div>
                        <div id="commercialMarketSegmentPanel" class="filterPanel"></div>
                        <div id="layersPanel" class="filterPanel">
                             <div id="districtLayerFilterButton" class="layerButton filterButton inactive">Округа</div>
                             <div id="regionLayerFilterButton" class="layerButton filterButton inactive">Районы</div>
                             <div id="zoneLayerFilterButton" class="layerButton filterButton inactive">Зоны</div>
                             <div id="quartalLayerFilterButton" class="layerButton filterButton inactive">Кварталы</div>
                        </div>
                    </div>
                 </div>
            `).appendTo(parentDomContainer);
            this.addEventListeners();
        },
        addEventListeners: function () {
            document.getElementById("filterImage").addEventListener("click", function (e) {
                document.getElementById("filterImage").classList.toggle("inactive");
                document.getElementById("allFiltersContainer").classList.toggle("inactive");
            });
            document.getElementById("districtLayerFilterButton").addEventListener("click", function (e) { changeMapType(MapZoneType.district, e.target); });
            document.getElementById("regionLayerFilterButton").addEventListener("click", function (e) { changeMapType(MapZoneType.region, e.target); });
            document.getElementById("zoneLayerFilterButton").addEventListener("click", function (e) { changeMapType(MapZoneType.zone, e.target); });
            document.getElementById("quartalLayerFilterButton").addEventListener("click", function (e) { changeMapType(MapZoneType.quartal, e.target); });
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