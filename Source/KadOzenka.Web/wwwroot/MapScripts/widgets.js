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
                            <div id="dealTypefilterBody" class="filterBody">
                                <!--
                                    <div id="RentSuggestionFilterButton" class="filterBodyButton">Предложение-аренда</div>
                                    <div id="SaleSuggestionFilterButton" class="filterBodyButton">Предложение-продажа</div>
                                    <div id="RentDealFilterButton" class="filterBodyButton">Сделка-аренда</div>
                                    <div id="SaleDealFilterButton" class="filterBodyButton">Сделка купли-продажи</div>
                                -->
                            </div>
                        </div>
                        <div id="marketSegmentFilter" class="filterMainButton">
                            <div class="filterHeader">
                                Сегмент рынка
                                <div id="DealTypeFilterCounter" class="filterCounter Hidden">99</div>
                            </div>
                            <div id="marketSegmentfilterBody" class="filterBody">
                                <!--
                                    <div id="AppartmentFilterButton" class="filterBodyButton">Апартаменты</div>
                                    <div id="ParkingFilterButton" class="filterBodyButton">Гаражи</div>
                                    <div id="HotelFilterButton" class="filterBodyButton">Гостиницы</div>
                                    <div id="IZHSFilterButton" class="filterBodyButton">ИЖС</div>
                                    <div id="MMFilterButton" class="filterBodyButton">Машиноместа</div>
                                    <div id="MZHSFilterButton" class="filterBodyButton">МЖС</div>
                                    <div id="OfficesFilterButton" class="filterBodyButton">Офисы</div>
                                    <div id="SkladFilterButton" class="filterBodyButton">Производство&nbsp;и&nbsp;склады</div>\
                                    <div id="GardenFilterButton" class="filterBodyButton">Садоводческое,&nbsp;огородническое&nbsp;и&nbsp;дачное&nbsp;использование</div>
                                    <div id="SanatoriumFilterButton" class="filterBodyButton">Санатории</div>
                                    <div id="TraidingFilterButton" class="filterBodyButton">Торговля</div>
                                -->
                            </div>
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

function addDisplayCountWidget(position) {
    map.controls.add(new countWidgetClass(), { float: 'none', position });
};

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

function removeTargetWidget() { map.controls.remove(targetWidget); };

function refreshFilterWidget(filterInfo) {
    var dealTypeData = '', marketSegmentData = '';
    filterInfo.dealTypeList.forEach(x => { dealTypeData += `<div id="${x.Name}FilterButton" class="filterBodyButton">${x.Value.replace(new RegExp(' ', 'g'), '&nbsp;')}</div>`; });
    filterInfo.marketSegmentList.forEach(x => { marketSegmentData += `<div id="${x.Name}FilterButton" class="filterBodyButton">${x.Value.replace(new RegExp(' ', 'g'), '&nbsp;')}</div>`; });
    document.getElementById('dealTypefilterBody').innerHTML = dealTypeData;
    document.getElementById('marketSegmentfilterBody').innerHTML = marketSegmentData;
}