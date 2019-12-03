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
                    <div id="filterDeatTypeContainer" class="filterContainer">
                        <div id="rentSuggestionFilter" class="filterButton">Предложение-аренда</div>
                        <div id="saleSuggestionFilter" class="filterButton">Предложение-продажа</div>
                        <div id="rentDealFilter" class="filterButton">Сделка-аренда</div>
                        <div id="saleDealFilter" class="filterButton">Сделка-продажа</div>
                    </div>
                </div>
            `).appendTo(parentDomContainer);
            this._createRequest();
        }
    });
};

function addDisplayCountWidget(position) { map.controls.add(new countWidgetClass(), { float: 'none', position }) };
function addFilterWidget(position) { map.controls.add(new filterWidgetClass(), { float: 'none', position }) };
function addTargetWidget(position) {
    if (!document.getElementById("targetControl")) {
        targetWidget = new targetWidgetClass();
        map.controls.add(targetWidget, { float: 'none', position });
    }
};

function removeTargetWidget() { map.controls.remove(targetWidget); };