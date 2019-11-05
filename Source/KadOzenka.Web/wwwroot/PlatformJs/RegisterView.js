// $_GET определаяется в scripts.js, но в IE 11 возникает ошибка, так как 2 JS-файла загружаются параллельно
var $_GET = $_GET || (function () {
    var get = {};
    if (document.location.toString().indexOf('?') !== -1) {
        var query = document.location
            .toString()
            // get the query string
            .replace(/^.*?\?/, '')
            // and remove any existing hash string (thanks, @vrijdenker)
            .replace(/#.*$/, '')
            .split('&');

        for (var i = 0, l = query.length; i < l; i++) {
            var aux = decodeURIComponent(query[i]).split('=');
            get[aux[0]] = aux[1];
        }
    }

    return get;
})();

var RegisterView = RegisterView || {

    InitRegisterView: function (registerViewSettings) {

        var clearSelection = false,
            pageChanged = false,
            isModelTransition = registerViewSettings.IsModelTransition,

            splitterSelector = "#verticalSplitter-" + registerViewSettings.CurrentRegisterId,
            gridSelector = "#Grid-" + registerViewSettings.CurrentRegisterId,
            tabStripSelector = "#TabStrip-" + registerViewSettings.CurrentRegisterId,
            gridToolbarSelector = "#GridToolBar-" + registerViewSettings.CurrentRegisterId,
            searchWindowSelector = "#SearchWindow-" + registerViewSettings.CurrentRegisterId,
            searchWindowToolbarSelector = "#SearchWindowToolBar-" + registerViewSettings.CurrentRegisterId,
            searchPanelContentSelector = "#menu-search-panel-content-" + registerViewSettings.CurrentRegisterId,
            sidePanelSelector = "#sidePanel-" + registerViewSettings.CurrentRegisterId,
            showRefreshButtonSelector = "#showRefreshButton-" + registerViewSettings.CurrentRegisterId,
            menuFiltersGridSelector = "#menu-filters_grid-" + registerViewSettings.CurrentRegisterId,
            footerStatusSelector = "#footerStatus-" + registerViewSettings.CurrentRegisterId,

            registerId = registerViewSettings.RegisterId,
            registerViewId = registerViewSettings.RegisterViewId,
            searchPanelWindowWidth = registerViewSettings.SearchPanelWindowWidth,
            searchPanelWindowHeight = registerViewSettings.SearchPanelWindowHeight,
            filterRegisterId = registerViewSettings.FilterRegisterId,
            columnWidthType = registerViewSettings.ColumnWidthType,

            callBackFunction = $_GET['CallBackFunction'],
            splitter = $(splitterSelector).data('kendoSplitter'),
            gridEl = $(gridSelector),
            grid = gridEl.data('kendoGrid'),
            tabStripEl = $(tabStripSelector),
            tabStrip = tabStripEl.data('kendoTabStrip'),
            gridToolbar = $(gridToolbarSelector).data('kendoToolBar'),
            searchWindow = $(searchWindowSelector).data('kendoWindow');

        ///////////////////////////////////////////////
        // Перенесено из Index.cshtml

        $(window).on('resize', function () {
            grid.resize();
        });

        if (columnWidthType == '1') {
            $(".contentPanel").css("display", "block");
        }

        //менеджер фильтрации
        var registerFilters = window.registerFilters = new function () {
            var _currentFilters = [];

            var descriptorKeys = [
                'field',
                'operator',
                'value'
            ];
            //TODO добавить флаг использовать Transion

            //?? неявно соответствует перечислению KapRemontWebMain._MVC.Model.FilterType
            this.filterTypes = {
                Dynamic: 0,
                Transition: 1
            };

            this.getAllFilters = function () {
                return _currentFilters;
            }

            this.set = function (filterType, filterDescriptor) {
                switch (filterType) {
                    case this.filterTypes.Transition:
                        _setTransitionFilter.call(this, filterDescriptor);
                        break;
                    case this.filterTypes.Dynamic:
                        _setDynamicFilter.call(this, filterDescriptor);
                        break;
                }
            };

            function _createFilterDescriptor(filterDescriptorConfig) {
                var filterDescriptor = {};

                for (key in filterDescriptorConfig) {
                    var lowerKey = Common.Functions.LowercaseFirstLetter(key);
                    if (descriptorKeys.indexOf(lowerKey) > -1) {
                        filterDescriptor[lowerKey] = filterDescriptorConfig[key];
                    }
                }

                if ($.isEmptyObject(filterDescriptor)) {
                    return null;
                }
                return filterDescriptor;
            }

            function _setTransitionFilter(filterDescriptorConfig) {
                var filterDescriptor = _createFilterDescriptor.call(this, filterDescriptorConfig);
                if (filterDescriptor) {
                    var currentTransionFilter = null;
                    for (var i = 0; i < _currentFilters.length; i++) {
                        if (_currentFilters[i].filterType = this.filterTypes.Transition) {
                            currentTransionFilter = _currentFilters[i];
                        }
                    }

                    if (!currentTransionFilter) {
                        var length = _currentFilters.push({
                            filterType: this.filterTypes.Transition
                        });
                        currentTransionFilter = _currentFilters[length - 1];
                    }

                    if (currentTransionFilter.filterDescriptors && currentTransionFilter.filterDescriptors.length > 0) {
                        currentTransionFilter.filterDescriptors[0] = filterDescriptor;
                    }
                    else {
                        currentTransionFilter.filterDescriptors = [filterDescriptor];
                    }
                }
            }

            function _setDynamicFilter(filterDescriptorConfig) {
                var filterDescriptor = _createFilterDescriptor.call(this, filterDescriptorConfig);

                if (filterDescriptor && filterDescriptor.field) {
                    var dynamicFilters = null;
                    var currentFieldFilterDescription = null;

                    for (var i = 0; i < _currentFilters.length; i++) {
                        if (_currentFilters[i].filterType = this.filterTypes.Dynamic && _currentFilters[i].filterDescriptors) {
                            dynamicFilters = _currentFilters[i];
                        }
                    }

                    if (!dynamicFilters) {
                        var length = _currentFilters.push({
                            filterType: this.filterTypes.Dynamic
                        });
                        dynamicFilters = _currentFilters[length - 1];
                    }

                    if (dynamicFilters.filterDescriptors) {
                        for (var i = 0; i < dynamicFilters.filterDescriptors.length; i++) {
                            if (dynamicFilters.filterDescriptors[i].field === filterDescriptor.field) {
                                currentFieldFilterDescription = dynamicFilters.filterDescriptors[i];
                            }
                        }
                    }
                    else {
                        dynamicFilters.filterDescriptors = [];
                    }

                    if (currentFieldFilterDescription) {
                        currentFieldFilterDescription = filterDescriptor;
                    }
                    else {
                        dynamicFilters.filterDescriptors.push(filterDescriptor);
                    }
                }
            }
        };

        //ресайзы панели поиска, грида, табов, фреймов и т.д.
        function setSearchWindowSize() {
            if (searchWindow) {
                var body = document.getElementsByTagName('body')[0];

                if (searchPanelWindowWidth) {
                    var width;
                    if (searchPanelWindowWidth.indexOf('%') > -1) {
                        width = getPercentageWidth(body, searchPanelWindowWidth);
                    }
                    else {
                        width = parseFloat(searchPanelWindowWidth);
                    }

                    if (width && !isNaN(width)) {
                        searchWindow.wrapper.css({ width: width })
                    }
                }

                if (searchPanelWindowHeight) {
                    var height;
                    if (searchPanelWindowHeight.indexOf('%') > -1) {
                        height = getPercentageHeight(body, searchPanelWindowHeight);
                    }
                    else {
                        height = parseFloat(searchPanelWindowHeight);
                    }
                    if (height && !isNaN(height)) {
                        searchWindow.wrapper.css({ height: height })
                    }
                }

                if (registerViewSettings.NeedOpenEmpty) searchWindow.center().open();
            }
        }

        function getPercentageWidth(containerHtmlElement, percentageSizeString) {
            return getPercentageSize(containerHtmlElement.clientWidth, percentageSizeString);
        }

        function getPercentageHeight(containerHtmlElement, percentageSizeString) {
            return getPercentageSize(containerHtmlElement.clientHeight, percentageSizeString);
        }

        function getPercentageSize(containerSize, percentageString) {
            if (containerSize && !isNaN(containerSize)) {
                var percentage = parseFloat(percentageString.replace('%', ''));
                if (!isNaN(percentage)) {
                    return containerSize * (percentage / 100);
                }
            }
            return null;
        }

        function resizeTabStripContent() {
            return;

            var iframeElements = tabStripEl.find('iframe.k-tabstrip-iframecontent'),
                //берем высоту текущего таба для всех
                activeTab = tabStripEl.find('.k-content.k-state-active');
            $.each(iframeElements, function () {
                //доработки для iframe
                var iframe = this,
                    contentDocument = iframe.contentDocument || iframe.contentWindow.document;
                /*iframe.style.visibility = "hidden";*/
                //удаляем 10 пикселей для отсутствия скролла
                var newHeight = activeTab.height();
                var newWidth = activeTab.width();
                /*iframe.style.height = newHeight + "px";
                iframe.style.width = newWidth + "px";
                iframe.style.visibility = "visible";*/

                //доработка для бага хрома
                //https://bugs.chromium.org/p/chromium/issues/detail?id=641881
                if (contentDocument.body && contentDocument.body.scrollHeight > newHeight) {
                    contentDocument.body.style.display = "none";
                    $(contentDocument.body).css("overflowY", "auto");
                    setTimeout(function () {
                        contentDocument.body.style.display = "block";
                    }, 0);
                }
            });
            tabStrip.resize(true);
        }

        function loadTabStripActiveTab() {
            var activeTabIframe = tabStripEl.find('.k-content.k-state-active .k-tabstrip-iframecontent');
            if (activeTabIframe.length > 0) {
                var currentSrc = activeTabIframe.attr('src'),
                    dataSrc = activeTabIframe.data('src');

                if (dataSrc && dataSrc !== currentSrc) {
                    activeTabIframe.attr('src', dataSrc);
                }
            }
        }

        //модальное окно kendo ui
        function OpenInKendoWindow(contentUrl, title, width, height, needRefresh) {
            var windowPlacehoder = $('<div id="registerModalWindow"></div>'),
                needMaximize = !width && !height,
                config = {
                    title: title,
                    iframe: true,
                    modal: true,
                    content: contentUrl,
                    close: function (e) {
                        if (needRefresh === true) {
                            grid.dataSource.read();
                        }
                        this.destroy();
                    }
                };

            config.height = height || "auto";
            config.width = width || "auto";

            //прикрепляем окно к html body
            $('body').append(windowPlacehoder);

            var window = windowPlacehoder.kendoWindow(config).data('kendoWindow');

            //события
            /*window.element.find('iframe.k-content-frame').on('load', function () {
                //доработки для iframe
                var iframe = this,
                    contentDocument = iframe.contentDocument ? iframe.contentDocument : iframe.contentWindow.document;
                iframe.style.visibility = "hidden";
                //10 пикселей добавляем для отсутствия скролла
                var newHeight = $(iframe).closest('.k-window-iframecontent').height();
                iframe.style.height = newHeight + "px";
                iframe.style.visibility = "visible";
            });*/

            kendo.ui.progress(windowPlacehoder, true);
            window.bind('refresh', function () {
                kendo.ui.progress(windowPlacehoder, false);
            });

            window.center();
            window.open();

            if (needMaximize) {
                window.maximize();
            }
        }

        //обновление состояния реестра после изменения строки
        function UpdateToolbarState(toolBarButtonStates) {
            $(gridSelector).find("input.dropdown-toolbar-button").each(function () {
                if ($(this).data("kendoDropDownList")) {
                    $(this).data("kendoDropDownList").dataSource.data([]);
                }
            });

            if (gridToolbar && toolBarButtonStates) {
                for (var i = 0; i < toolBarButtonStates.length; i++) {
                    var buttonState = toolBarButtonStates[i],
                        toolbarButtonSelector = '[data-button-id=' + buttonState.Id + ']';

                    if (buttonState.OwnerDropdownButtonId) {
                        var dropDownEl = $(gridToolbar.element)
                            .find('[data-button-id=' + buttonState.OwnerDropdownButtonId + ']')
                            .not('[data-owner-dropdown-button-id]'),
                            dropDown = dropDownEl.data("kendoDropDownList");

                        if (dropDown) {
                            var targetItem = dropDownEl.data('dropdown-items').filter(function (item) {
                                if (item.htmlAttributes["data-button-id"] == buttonState.Id) {
                                    return item;
                                }
                            })[0];

                            dropDown.dataSource.add(targetItem);
                            dropDown.refresh();

                            var buttonEl = dropDown.list.find(toolbarButtonSelector).closest('.k-item'),
                                dataItem = dropDown.dataItem(buttonEl);

                            if (buttonState.Hidden) {
                                dropDown.dataSource.remove(dataItem);
                            }

                            dataItem.set('enable', buttonState.Enable);

                            if (buttonState.Url) {
                                dataItem.get('htmlAttributes')['data-url'] = buttonState.Url;
                            }
                        }
                    }
                    else {
                        var buttonEl = $(gridToolbar.element)
                            .find(toolbarButtonSelector)
                            .not('[data-owner-dropdown-button-id]');

                        //доработка для dropdown'ов
                        if (buttonEl.hasClass('dropdown-toolbar-button')) {
                            buttonEl = buttonEl.closest('[data-uid]');
                        }

                        if (buttonState.Hidden) {
                            gridToolbar.hide(buttonEl);
                        }
                        else {
                            gridToolbar.show(buttonEl);
                        }

                        if (buttonState.Url) {
                            //не изменяем сам аттрибут, только jquery.cache
                            buttonEl.attr('data-url', buttonState.Url);
                        }

                        gridToolbar.enable(buttonEl, buttonState.Enable);
                    }
                }
            }
        }

        function UpdateTabStripState(tabStripStates) {
            if (tabStripStates && tabStrip) {
                var selectedTabIndex = tabStrip.select().index();
                var selectedContentUrl;
                var selectedElement;

                var firstVisibleTabIndex;
                var firstVisibleContentUrl;
                var firstVisibleElement;


                var changeSelectedTabStrip;

                for (var i = 0; i < tabStripStates.length; i++) {
                    var tabStripState = tabStripStates[i],
                        tabSelector = '.k-item[role="tab"][data-tab-id=' + tabStripState.Id + ']',
                        tabEl = $(tabStrip.element).find(tabSelector);

                    if (tabEl.length > 0) {
                        var tabContentId = tabEl.attr('aria-controls');
                        if (tabContentId) {
                            var tabContentEl = $(tabStrip.element).find('#' + tabContentId),
                                tabIndex = tabContentEl.index() - 1;

                            if (tabStripState.Visible) {
                                tabEl.show();
                                if (firstVisibleTabIndex == undefined) {
                                    firstVisibleTabIndex = tabIndex;
                                    firstVisibleContentUrl = tabStripState.ContentUrl;
                                    firstVisibleElement = tabContentEl;
                                }

                                if (tabIndex == selectedTabIndex) {
                                    selectedContentUrl = tabStripState.ContentUrl;
                                    selectedElement = tabContentEl;
                                }
                            }
                            else {
                                tabEl.hide();
                                if (selectedTabIndex == tabIndex) {
                                    changeSelectedTabStrip = true;
                                }
                            }

                            tabStrip.enable(tabEl, tabStripState.Enabled);

                            tabContentEl.find('iframe.k-tabstrip-iframecontent').data('src', tabStripState.ContentUrl);
                        }
                    }
                }

                var contentUrl;

                if (changeSelectedTabStrip) {
                    //если url пустой загружаем пустую страницу браузера
                    firstVisibleElement.find('iframe.k-tabstrip-iframecontent').attr('src', firstVisibleContentUrl || 'about:blank');

                    tabStrip.select(firstVisibleTabIndex);
                } else {
                    //если url пустой загружаем пустую страницу браузера
                    selectedElement.find('iframe.k-tabstrip-iframecontent').attr('src', selectedContentUrl || 'about:blank');
                }

                tabStrip.trigger("contentLoad");
            }
        }

        //обработчики событий
        function onGridDataBound(e) {
            var rows = this.tbody.find('tr');
            if (rows.length > 0) {
                rows.first().addClass('k-state-selected');
                this.trigger("change");
            }
            else if (tabStrip && tabStrip.element) {
                $(tabStrip.element).find('.k-item[role = "tab"]').hide();

                //если грид пустой то, загружаем в карточку пустую страницу браузера
                $(tabStrip.element).find('iframe.k-tabstrip-iframecontent').each(function () {
                    $(this).attr('src', 'about:blank');
                });
            }
        }

        function onGridSelectionChange(e) {
            var selectedRow = this.select(),
                dataItem = this.dataItem(selectedRow),
                objectId = dataItem ? dataItem.id : null,
                parameters = {
                    registerId: registerId,
                    objectId: objectId,
                    registerViewId: registerViewSettings.RegisterViewId,
                    uniqueSessionKey: registerViewSettings.UniqueSessionKey,
                    callBackFunction: callBackFunction
                };

            if (filterRegisterId) {
                parameters.filterRegisterId = filterRegisterId
            }

            var requestUrl = $(gridSelector).parent().parent().parent().find('[name=RequestUrl]').val();

            var urlParameters = Common.GetUrlParameters(requestUrl);
            if (urlParameters.length > 0) {
                for (var i = 0; i < urlParameters.length; i++) {
                    if (!Common.IsExistsKeyInObject(parameters, urlParameters[i].Name)) {
                        parameters[urlParameters[i].Name] = urlParameters[i].Value;
                    }
                }
            }

            $.ajax({
                url: registerViewSettings.RegistersUpdateStateUrl,
                type: 'GET',
                data: parameters,
                dataType: 'json',
                success: function (states) {
                    if (states) {
                        UpdateToolbarState(states.toolbarButtonStates);
                        UpdateTabStripState(states.tabStripStates);
                    }
                }
            });
        }

        function onSplitterResize(e) {
            resizeTabStripContent();
        }

        function onTabStripContentLoad(e) {
            resizeTabStripContent();
        }

        function onTabStripTabShow(e) {
            loadTabStripActiveTab();
            resizeTabStripContent();
        }

        function onGridToolbarItemClick(e) {
            e.preventDefault();

            if (e.target.data('command') === 'ExportToExcel') {
                var parameters = grid.GetDataFunc();
                parameters.CurrentLayoutId = registerViewSettings.CurrentLayoutId;

                var url = registerViewSettings.RegistersExportToExcelUrl + '?parametersJson=' + encodeURIComponent(JSON.stringify(parameters));

                window.open(url, '_blank');
                return;
            }

            var url = e.target.attr('data-url');
            if (url) {
                if (e.target.data('open-in-radwindow')
                    && e.target.data('open-in-radwindow').toString().toLowerCase() === "true") {
                    var title = e.target.html();

                    OpenInKendoWindow(url,
                        title,
                        e.target.data('window-width'),
                        e.target.data('window-height'),
                        e.target.data('need-refresh').toString().toLowerCase() === "true"
                    );
                }
                else if (e.target.data('open-in-new-window')
                    && e.target.data('open-in-new-window').toString().toLowerCase() === "true") {
                    var newWindow = window.open(url, '_blank');
                    newWindow.focus();
                }
                else {
                    window.location.href = url;
                }
            }
        }

        //orientation = "horizontal" : "vertical"
        function changeObjectCardOrientation(orientation) {
            var splitter = $(splitterSelector).data("kendoSplitter");

            if (splitter.orientation == orientation)
                return;

            var oldMarker = splitter._marker;

            splitter.element.children(".k-splitbar").remove();
            splitter.orientation = splitter.options.orientation = orientation;
            $(splitterSelector).kendoSplitter({
                orientation: splitter.orientation,
                panes: [{ collapsible: true }, { collapsible: true }],
                resize: onSplitterResize,
                collapse: onSplitterResize,
                expand: onSplitterResize
            });

            if (orientation == "vertical") {
                $(splitterSelector + " .k-pane:last").css({ left: "0px" });
            }

            var pos = $._data($(window)[0], "events").resize.map(function (e) { return e.namespace; }).indexOf('kendoSplitter' + oldMarker);
            $._data($(window)[0], "events").resize.splice(pos, 1);
            
            updateSplitterSettings(orientation, getSplitterSize());

            var toolbar = $("#GridToolBar-" + registerViewSettings.CurrentRegisterId).data("kendoToolBar");

            toolbar.enable($("#objectCardRight-" + registerViewSettings.CurrentRegisterId), orientation != "horizontal");
            toolbar.enable($("#objectCardBottom-" + registerViewSettings.CurrentRegisterId), orientation != "vertical");

            // Почему-то без этого groupButton не закрывается после блокировки кнопки
            $("#gearButton-" + registerViewSettings.CurrentRegisterId + "_wrapper .k-split-button-arrow").click()
        }

        $("#showSqlButton-" + registerViewSettings.CurrentRegisterId).bind("click", function (e) {

            var parameters = grid.GetDataFunc();
            parameters.CurrentLayoutId = registerViewSettings.CurrentLayoutId;

            var url = registerViewSettings.RegistersShowSqlUrl + '?parametersJson=' + encodeURIComponent(JSON.stringify(parameters));

            OpenInKendoWindow(url,
                "SQL-запрос",
                "500px",
                "600px",
                false
            );
        });

        $("#setLayoutButton-" + registerViewSettings.CurrentRegisterId).bind("click", function (e) {

            var url = registerViewSettings.CoreRegisterLayoutAllUrl;
            var title = 'Раскладки ' + registerViewSettings.CurrentRegisterViewTitle;

            Common.UI.ShowWindow(title, url);
        });

        $("#setQryButton-" + registerViewSettings.CurrentRegisterId).bind("click", function (e) {

            var url = registerViewSettings.CoreRegisterQryAllUrl;
            var title = 'Фильтры ' + registerViewSettings.CurrentRegisterViewTitle;

            Common.UI.ShowWindow(title, url);
        });


        $("#setPageSize-" + registerViewSettings.CurrentRegisterId).bind("click", function (e) {
            var windowPlacehoder = $('<div id="SetPageSizeWindow"></div>');
            $('body').append(windowPlacehoder);

            var url = registerViewSettings.CoreRegisterSetPageSize;
            var title = 'Установка количества строк на странице';

            var windowCfg = {
                title: title,
                content: url,
                iframe: false,
                modal: true,
                draggable: false,
                resizable: false,
                width: 400,
                height: 150,
                close: function () {
                    this.destroy();
                },
                close2: function () {
                    this.destroy();
                }
            };

            var modalWindow = windowPlacehoder.kendoWindow(windowCfg).data('kendoWindow');
            modalWindow.center().open();
        });

        $("#resetSortButton-" + registerViewSettings.CurrentRegisterId).bind("click", function (e) {
            grid.dataSource.sort({});
            if (grid && grid.resetSorting) {
                grid.resetSorting();
            }
        });

        $("#resetWidthButton-" + registerViewSettings.CurrentRegisterId).bind("click", function (e) {
            $("table", grid.element).css("width", "100%");
            $("col", grid.element).css("width", "100px");

            var $th = grid.element.find('th');
            $th.each(function (index) {
                grid.columns[index].width = $(this).width();
            });

            if (grid && grid.gridColumnResize) {
                grid.gridColumnResize();
            }
        });

        $("#useDefaultLayoutButton-" + registerViewSettings.CurrentRegisterId).bind("click", function (e) {
            window.location.href = registerViewSettings.UseDefaultLayoutUrl;
        });

        $("#objectCardRight-" + registerViewSettings.CurrentRegisterId).bind("click", function (e) {
            changeObjectCardOrientation("horizontal");
        });
        $("#objectCardBottom-" + registerViewSettings.CurrentRegisterId).bind("click", function (e) {
            changeObjectCardOrientation("vertical");
        });

        $("#objectCardOpen-" + registerViewSettings.CurrentRegisterId).bind("click", function (e) {
            var selectedItem = grid.dataItem(grid.select());

            var objId = selectedItem.id;
            var url = registerViewSettings.ObjectCardIndexUrl + '?ObjId=' + objId + '&RegisterViewId=' + registerViewSettings.CurrentRegisterViewId + '&useMasterPage=true';
            window.open(url, '_blank');
        });

        //подписи на события
        if (grid) {
            grid.bind("change", onGridSelectionChange);
            grid.bind("dataBound", onGridDataBound);
        }

        if (registerViewSettings.NeedOpenEmpty) {
            $(splitterSelector).hide();
            $(gridSelector).hide();
            if (searchWindow) {
            } else {
                var slideMenu = $('.movePanel[data-id="menu-search-panel"]');
                if (slideMenu === undefined || slideMenu == null || slideMenu.length <= 0) return;

                var isVisible = slideMenu.position().left > 0;
                if (isVisible) {
                    kendo.fx(slideMenu).slideInLeft().play();
                    slideMenu.removeClass("open");
                } else {
                    slideMenu.addClass("open");
                    kendo.fx(slideMenu).slideInLeft().reverse();
                }
            }
        }

        if (splitter) {
            splitter.bind("resize", onSplitterResize);
        }
        if (tabStrip) {
            tabStrip.bind("contentLoad", onTabStripContentLoad);
            tabStrip.bind("show", onTabStripTabShow);
        }

        if (gridToolbar) {
            gridToolbar.bind("click", onGridToolbarItemClick);
        }

        //обработка дропдаунов
        $(gridSelector).find(".dropdown-toolbar-button").each(function () {
            var dropDownEl = $(this);
            dropDownEl.kendoDropDownList({
                optionLabel: dropDownEl.data('dropdown-option-label'),
                dataTextField: "text",
                dataValueField: "url",
                dataSource: dropDownEl.data('dropdown-items'),
                autoWidth: true,
                height: "auto",
                template: '<span class="#: enable ? \'\' : \'k-state-disabled\' #" data-button-id="#: htmlAttributes[\'data-button-id\'] #">#: text #</span>',
                select: function (e) {
                    e.preventDefault();

                    if (e.dataItem.enable === true) {
                        var url = e.dataItem.htmlAttributes['data-url'];
                        if (url) {
                            if (e.dataItem.htmlAttributes['data-open-in-radwindow'] === true) {
                                OpenInKendoWindow(url,
                                    e.dataItem.text,
                                    e.dataItem.htmlAttributes['data-window-width'],
                                    e.dataItem.htmlAttributes['data-window-height'],
                                    e.dataItem.htmlAttributes['data-need-refresh']
                                );
                            }
                            else if (e.dataItem.htmlAttributes['data-open-in-new-window'] === true) {
                                var newWindow = window.open(url, '_blank');
                                newWindow.focus();
                            }
                            else {
                                window.location.href = url;
                            }
                        }
                    }
                }
            });

            //скрываем название кнопки дропдауна из списка
            dropDownEl.getKendoDropDownList().optionLabel.hide();

            //доработка по скрытию элементов дропдауна при инициализации
            $(dropDownEl.getKendoDropDownList().listView.element).find('> li.k-item').each(function () {
                var dataItem = dropDownEl.getKendoDropDownList().dataSource.at($(this).index());
                if (dataItem && dataItem.hidden == true) {
                    $(this).hide();
                }
            });
        });

        $(sidePanelSelector + ' #SearchWindowButton').on('click', function (e) {
            searchWindow.center().open();
        });

        setSearchWindowSize();

        $(searchWindowToolbarSelector + ' #SearchButton').on('click', function (e) {

            //закрываем отдельное окно
            if (searchWindow) {
                searchWindow.close();
            }
            //закрываем слайдер
            var slideMenu = $('.movePanel.open[data-id="menu-search-panel"] .header .close');
            if (slideMenu.length > 0) {
                slideMenu.trigger('click');
            }

            if (registerViewSettings.NeedOpenEmpty) {
                $(splitterSelector).show();
                $(gridSelector).show();
                $("#MessageClickSearch").hide();
                registerViewSettings.NeedOpenEmpty = false;
            }

            if (grid) {
                if (grid.gridClearSelection != undefined)
                    grid.gridClearSelection();
                searchAplied = true;
                grid.dataSource.read();
            }

            setTimeout(() => {
                $(window).resize();
            }, 100);

        });

        $(searchWindowToolbarSelector).closest(searchPanelContentSelector + ',' + searchWindowSelector).keypress(function (e) {
            if (e.which == 13) {
                $(searchWindowToolbarSelector + ' #SearchButton').click();
            }
        });

        $(searchWindowToolbarSelector + ' #ClearSearchButton').on('click', function (e) {
            clearSearchElements();
        });

        $(searchWindowToolbarSelector + ' #DefaultSearchButton').on('click', function (e) {
            $.ajax({
                type: 'POST',
                url: registerViewSettings.RegistersGetDefaultValuesForSearchUrl,
                data: { registerId: registerId },
                dataType: 'json',
                success: function (result) {
                    var elementList = $('[attributenumberid]');
                    for (var i = 0; i < elementList.length; ++i) {
                        if (elementList[i].getAttribute('data-role') == "dropdownlist") {
                            $('#' + elementList[i].id).data('kendoDropDownList').value("");
                        } else if (elementList[i].getAttribute('type') == "checkbox") {
                            elementList[i].checked = false;
                        } else {
                            elementList[i].value = "";
                        }
                    }

                    for (var item in result.Data) {
                        var elem = $('#' + item);
                        if (elem[0]) {
                            if (elem[0].getAttribute('data-role') == "dropdownlist") {
                                elem.data('kendoDropDownList').value(result.Data[item]);
                            } else if (elem[0].getAttribute('type') == "checkbox") {
                                elem[0].checked = (result.Data[item] == "true");
                            } else if (elem[0].getAttribute('data-role') == 'datepicker') {
                                elem.data('kendoDatePicker').value(kendo.parseDate(result.Data[item]));
                            }
                            else {
                                elem.val(result.Data[item]);
                            }
                        }
                    }
                },
                error: function (result) {
                    Common.UI.ShowDialog({
                        title: 'Ошибка',
                        content: result.responseText,
                        icon: 'error',
                        showCloseBtn: true
                    });
                }
            });
        });

        $(".footerPanel").hide();

        $(".window-footer-arrow-down").on('click', function (e) {
            $('.footerPanel').hide();
            $(".window-footer-arrow-up").show();
            if (splitter) {
                splitter.resize();
            }
            if (grid) {
                grid.resize();
            }
        });

        $(".window-footer-arrow-up").on('click', function (e) {
            $('.footerPanel').show();
            $(".window-footer-arrow-up").hide();
            if (splitter) {
                splitter.resize();
            }
            if (grid) {
                grid.resize();
            }
        });

        function clearSearchElements() {
            var elementList = $('[attributenumberid]');
            for (var i = 0; i < elementList.length; ++i) {
                if (elementList[i].getAttribute('data-role') == "dropdownlist") {
                    $('#' + elementList[i].id).data('kendoDropDownList').value("");
                } else {
                    elementList[i].value = "";
                }
            }
        }

        window.registerStatusClick = function () {
            var url = registerViewSettings.CoreRegisterLayoutUrl;
            var currentRegisterId = registerViewSettings.CurrentRegisterId;
            if (currentRegisterId != "") {
                url += "&LayoutRegisterId=" + currentRegisterId;
            }
            var currentRegisterViewId = registerViewSettings.CurrentRegisterViewId;
            if (currentRegisterViewId != "") {
                url += "&RegisterViewId=" + currentRegisterViewId;
            }
            var layoutId = registerViewSettings.CurrentLayoutId;
            if (layoutId != "") {
                var layoutKey = "&CurrentLayoutId=" + layoutId;
                url += "&RequestObjectId=" + layoutId + layoutKey;
                url += "&Transition=1&93300100=" + layoutId;
            }
            var title = 'Раскладки ' + registerViewSettings.CurrentRegisterViewTitle;
            Common.UI.ShowWindow(title, url);
        }

        window.openQueriesWindow = function (id) {
            var url = registerViewSettings.CoreRegisterQryUrl;
            var currentRegisterId = registerViewSettings.CurrentRegisterId;
            if (currentRegisterId != "") {
                url += "&FilterRegisterId=" + currentRegisterId;
            }
            var currentRegisterViewId = registerViewSettings.CurrentRegisterViewId;
            if (currentRegisterViewId != "") {
                url += "&RegisterViewId=" + currentRegisterViewId;
            }

            if (id)
                url += "&Transition=1&93600100=" + id;

            var title = 'Фильтры ' + registerViewSettings.CurrentRegisterViewTitle;
            Common.UI.ShowWindow(title, url);
        }

        function getSplitterSize() {
            var splitter = $(splitterSelector).data("kendoSplitter");

            var splitterElement = splitter.element;
            var paneElement = splitterElement.find(".k-pane:first");

            var ratio;

            if (splitter.orientation == "vertical") {
                ratio = Math.ceil(paneElement.height() * 100 / splitterElement.height());
            }
            else {
                ratio = Math.ceil(paneElement.width() * 100 / splitterElement.width());
            }

            return ratio;
        }

        function onSplitterResize() {
            var splitter = $(splitterSelector).data("kendoSplitter");
            var newSize = getSplitterSize();

            updateSplitterSettings(splitter.orientation, newSize);
        }

        function updateSplitterSettings(newOrientation, newSize) {
            $.ajax({
                url: registerViewSettings.RegistersUpdateSplitterSettingsUrl,
                type: 'GET',
                data: {
                    registerViewId: registerViewSettings.CurrentRegisterViewId,
                    orientation: newOrientation,
                    size: newSize
                },
                dataType: 'json'
            });
        }
    },

    InitGrid: function (registerViewSettings) {
        var clearSelection = false,
            pageChanged = false,
            isModelTransition = registerViewSettings.IsModelTransition,

            splitterSelector = "#verticalSplitter-" + registerViewSettings.CurrentRegisterId,
            gridSelector = "#Grid-" + registerViewSettings.CurrentRegisterId,
            tabStripSelector = "#TabStrip-" + registerViewSettings.CurrentRegisterId,
            gridToolbarSelector = "#GridToolBar-" + registerViewSettings.CurrentRegisterId,
            searchWindowSelector = "#SearchWindow-" + registerViewSettings.CurrentRegisterId,
            searchWindowToolbarSelector = "#SearchWindowToolBar-" + registerViewSettings.CurrentRegisterId,
            searchPanelContentSelector = "#menu-search-panel-content-" + registerViewSettings.CurrentRegisterId,
            sidePanelSelector = "#sidePanel-" + registerViewSettings.CurrentRegisterId,
            showRefreshButtonSelector = "#showRefreshButton-" + registerViewSettings.CurrentRegisterId,
            menuFiltersGridSelector = "#menu-filters_grid-" + registerViewSettings.CurrentRegisterId,
            footerStatusSelector = "#footerStatus-" + registerViewSettings.CurrentRegisterId,

            registerId = registerViewSettings.RegisterId,
            registerViewId = registerViewSettings.RegisterViewId,
            searchPanelWindowWidth = registerViewSettings.SearchPanelWindowWidth,
            searchPanelWindowHeight = registerViewSettings.SearchPanelWindowHeight,
            filterRegisterId = registerViewSettings.FilterRegisterId,
            columnWidthType = registerViewSettings.ColumnWidthType,

            callBackFunction = $_GET['CallBackFunction'],
            splitter = $(splitterSelector).data('kendoSplitter'),
            gridEl = $(gridSelector),
            grid = gridEl.data('kendoGrid'),
            tabStripEl = $(tabStripSelector),
            tabStrip = tabStripEl.data('kendoTabStrip'),
            gridToolbar = $(gridToolbarSelector).data('kendoToolBar'),
            searchWindow = $(searchWindowSelector).data('kendoWindow');

        ////////////////////////////////////////////////////
        // Перенесено из Grid.cshtml

        var gridReadRequest;

        grid.bind('dataBound', function (e) {
            var view = this.dataSource.view(),
                gridThis = this;
            //фильтр перехода
            var isTransition = $(sidePanelSelector + ' [name=IsTransition]').length > 0 ?
                $(sidePanelSelector + ' [name=IsTransition]').is(':checked') : isModelTransition;

            boldButton(GetSearchValues(), false, '#SearchWindowButton');
            boldButton(GetFilterValues(), isTransition, '#FiltersWindowButton');

            clearSelection = false;

            gridThis.element.find('input[id^=row-checkbox-]').on('click', function (e) {
                var checked = $(this).is(':checked'),
                    row = $(this).closest('tr[role="row"]'),
                    dataItem = gridThis.dataItem(row);

                gridThis.element.find('#headerColumnCheckBox').prop('checked', false);

                $.ajax({
                    url: registerViewSettings.RegistersSelectRowUrl,
                    type: 'POST',
                    data: { selected: checked, objectId: dataItem.id, uniqueSessionKey: registerViewSettings.UniqueSessionKey },
                    success: function () {
                        gridThis.trigger('change');
                    }
                });
            });

            setFooterStatus();

            if (view.length > 0) {
                $.ajax({
                    url: registerViewSettings.RegistersGetGridSpecialStylesUrl,
                    type: 'GET',
                    data: {
                        registerId: registerViewSettings.RegisterId, uniqueSessionKey: registerViewSettings.UniqueSessionKey
                    },
                    success: function (result) {
                        if (result && result.length > 0) {
                            var rows = grid.tbody.find("tr");
                            var cells = grid.tbody.find("td");
                            result.forEach(function (item, i, result) {
                                if (item.RowIndex != null) {
                                    var row = rows[item.RowIndex];
                                    row.setAttribute("style", item.Config);
                                    if (item.ColumnIndex != null) {
                                        var cell = cells[grid.columns.length * i + item.ColumnIndex];
                                        cell.setAttribute("style", item.Config);
                                    }
                                }
                            });
                        }
                    }
                });
            }
        });
        grid.bind('columnResize', gridColumnResize);
        grid.bind('sort', onSorting);
        grid.bind('page', function () { pageChanged = true; });

        grid.setOptions({
            excelExport: function (e) {
                e.workbook.fileName = registerViewSettings.LayoutName + '_' + kendo.toString(new Date(), 'dd.MM.yyyy') + '.xlsx';
                KendoExtension.ExportGridWithTemplatesContent(e);
            },
            dataSource: {
                transport: {
                    read: {
                        data: GetData,
                        beforeSend: function (xhr) {
                            gridReadRequest = xhr;
                        }
                    }
                },
                requestStart: function (e) {
                    if (registerViewSettings.NeedOpenEmpty === true) {
                        e.preventDefault();
                    }
                }
            },
        });

        grid.dataSource.bind('error', onError);

        $(document.body).keydown(function (e) {
            if (e && e.keyCode == 27 && gridReadRequest && gridReadRequest.abort) {
                gridReadRequest.abort();
            }
        });

        $(gridSelector).kendoTooltip({ autoHide: true, showAfter: 3000, position: "right", filter: ".forTooltips", content: getRatingTooltip });

        grid.thead.kendoTooltip({
            filter: "th",
            content: function (e) {
                return e.target.text();
            },
            position: "top",
            autoHide: true,
            showAfter: 500
        });

        $(showRefreshButtonSelector).on('click', function (e) {
            e.preventDefault();
            if (grid) {
                grid.dataSource.read();
            }
        });

        $(gridSelector + ' #headerColumnCheckBox').on('click', function (e) {
            clearSelection = !$(this).is(':checked');

            if (grid && grid.dataSource) {
                grid.dataSource.read();
            }
        });

        function GetSearchValues() {
            var elementList = $(searchWindowToolbarSelector).closest(searchPanelContentSelector + ',' + searchWindowSelector).find('[attributenumberid]');
            var search = [];
            for (var i = 0; i < elementList.length; ++i) {
                // val true всегда у checkbox, поэтому отдельную обработку делать должны мы
                if (elementList[i].getAttribute('class') == 'k-checkbox') {
                    if ($('[name="' + elementList[i].id + '"]:checkbox').is(':checked')) {
                        search.push({ key: elementList[i].id, value: true });
                    }
                }
                else {
                    var val = elementList[i].value;
                    if (val && elementList[i].id) { //todo core
                        search.push({ key: elementList[i].id, value: val });
                    }
                }
            }
            return search;
        }

        function GetFilterValues() { // toDo
            var databaseFiltersGrid = $(menuFiltersGridSelector).data('kendoGrid');
            var databaseFilters = [];
            //добавляем сохраненные фильтры
            if (databaseFiltersGrid) {
                var selectedDatabaseFilters = databaseFiltersGrid.select();
                for (var i = 0; i < selectedDatabaseFilters.length; i++) {
                    var dataItem = databaseFiltersGrid.dataItem(selectedDatabaseFilters[i]);
                    databaseFilters.push(dataItem.Id);
                }
            }

            if ($_GET["QueryId"]) {
                databaseFilters.push($_GET["QueryId"]);
            }

            return databaseFilters;
        }

        function GetData() {
            var parameters = {
                RegisterId: registerViewSettings.RegisterId,
                SearchApplied: searchAplied
            };

            parameters.PageChanged = pageChanged;
            pageChanged = false;

            var selectAll = $(gridSelector + ' #headerColumnCheckBox').is(':checked');
            parameters.SelectAll = selectAll;
            parameters.ClearSelection = clearSelection;

            if (registerViewSettings.LayoutIdHasValue == "True") {
                parameters.LayoutId = registerViewSettings.LayoutId;
            }

            if (registerViewSettings.ObjectRegisterIdHasValue == "True") {
                parameters.ObjectRegisterId = registerViewSettings.ObjectRegisterId;
            }

            if (registerViewSettings.ObjectIdHasValue == "True") {
                parameters.ObjectId = registerViewSettings.ObjectId;
            }

            if (registerViewSettings.RegisterViewIdIsNotEmpty == "True") {
                parameters.RegisterViewId = registerViewSettings.RegisterViewId;
                parameters.LayoutRegisterId = registerViewSettings.RegisterLayoutId;
                parameters.FilterRegisterId = registerViewSettings.RegisterLayoutId;
                parameters.ListRegisterId = registerViewSettings.RegisterLayoutId;
            }

            // поиск
            parameters.searchData = GetSearchValues();

            var registerViewSearch = $('.search-filter').data('registerViewSearch');
            if (registerViewSearch) {
                parameters.SearchDataNewDesign = registerViewSearch.filter ? registerViewSearch.filter : JSON.stringify(registerViewSearch.getStruct());
            }

            // фильтры
            parameters.databaseFilters = GetFilterValues();

            //списки
            if (typeof (getSelectedListItem) === "function") {
                parameters.selectedLists = getSelectedListItem();
            }
            //фильтр перехода
            var isTransition = $(sidePanelSelector + ' [name=IsTransition]').length > 0 ?
                $(sidePanelSelector + ' [name=IsTransition]').is(':checked') : isModelTransition;

            var transitionQueryString = $(gridSelector).closest('.k-content').find('[name=TransitionQueryString]').val() || $(gridSelector).closest('.mainContent').find('[name=TransitionQueryString]').val();
            var requestUrl = $(gridSelector).closest('.mainContent').find('[name=RequestUrl]').val() || $(gridSelector).closest('.k-content').find('[name=RequestUrl]').val();

            if (isTransition) {
                parameters.isTransition = true;
                parameters.transitionQueryString = transitionQueryString;
            }

            parameters.UniqueSessionKey = registerViewSettings.UniqueSessionKey;
            parameters.UniqueSessionKeySetManually = true;

            var urlParameters = Common.GetUrlParameters(requestUrl);
            if (urlParameters.length > 0) {
                for (var i = 0; i < urlParameters.length; i++) {
                    if (!Common.IsExistsKeyInObject(parameters, urlParameters[i].Name)) {
                        parameters[urlParameters[i].Name] = urlParameters[i].Value;
                    }
                }
            }

            return parameters;
        }

        function boldButton(collection, isTransition, id) {
            $(id + ' span')
                .closest('button')
                .toggleClass('use-filter', collection.length || isTransition ? true : false);
        }

        function getRatingTooltip(e) {
            var image = $(e.target[0]).find('img');
            if (image.attr('toolTip') !== undefined) {
                return image.attr('toolTip');
            } else {
                return e.target[0].innerText;
            }
        }

        function gridColumnResize(arg) {
            var columnsWidth = [];

            for (var i = 0; i < grid.columns.length; ++i) {
                columnsWidth.push(grid.columns[i].width === undefined ? '' : grid.columns[i].width);
            }

            $.ajax({
                url: registerViewSettings.RegistersSaveGridLayoutsWidthUrl,
                type: 'POST',
                data: {
                    columnsWidth: columnsWidth,
                    registerId: registerViewSettings.RegisterId,
                    UniqueSessionKey: registerViewSettings.UniqueSessionKey
                },
                error: function (result) {
                    Common.UI.ShowDialog({
                        title: 'Ошибка',
                        content: result.responseText,
                        icon: 'error',
                        showCloseBtn: true
                    });
                }
            });
        }

        function resetSorting() {
            $.ajax({
                url: registerViewSettings.RegistersResetGridSortUrl,
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({
                    registerId: registerViewSettings.RegisterId, layoutId: registerViewSettings.CurrentLayoutId, UniqueSessionKey: registerViewSettings.UniqueSessionKey
                }),
                error: function (result) {
                    Common.UI.ShowDialog({
                        title: 'Ошибка',
                        content: result.responseText,
                        icon: 'error',
                        showCloseBtn: true
                    });
                }
            });
        }

        function onSorting(arg) {
            $.ajax({
                url: registerViewSettings.RegistersSaveGridSortUrl,
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({
                    field: arg.sort.field, direction: (arg.sort.dir || "none"), registerId: registerViewSettings.RegisterId, layoutId: registerViewSettings.CurrentLayoutId, UniqueSessionKey: registerViewSettings.UniqueSessionKey
                }),
                error: function (result) {
                    Common.UI.ShowDialog({
                        title: 'Ошибка',
                        content: result.responseText,
                        icon: 'error',
                        showCloseBtn: true
                    });
                }
            });
        }

        function onError(e) {
            Common.UI.ShowDialog({
                title: 'Ошибка',
                content: e.xhr.responseText,
                icon: 'error',
                showCloseBtn: false
            });

            setFooterStatus();
        }

        function setFooterStatus() {
            if ($(footerStatusSelector)[0]) {
                $.ajax({
                    url: registerViewSettings.RegistersGetStatusUrl,
                    type: 'GET',
                    //contentType: 'application/json',
                    data: {
                        registerId: registerViewSettings.RegisterId, uniqueSessionKey: registerViewSettings.UniqueSessionKey
                    },
                    success: function (result) {
                        $(footerStatusSelector)[0].innerHTML = result;
                    }
                });
            }
        }

        grid.refreshGrid = function () {
            $(gridSelector).data('kendoGrid').dataSource.read();
        };

        grid.resetSorting = function () {
            resetSorting();
        };

        grid.gridColumnResize = function () {
            gridColumnResize();
        };

        grid.gridClearSelection = function () {
            clearSelection = true;
        };

        grid.dataSource.read();

        grid.GetDataFunc = function () {
            var parameters = GetData();
            if (this.dataSource.sort()) {
                parameters.Sort = '';
                $.each(this.dataSource.sort(), function (index, value) {
                    parameters.Sort += (index !== 0 ? '~' : '') + value.field + '-' + value.dir;
                });
            }
            return parameters;
        }
    },

    InitSearchPanel: function (registerViewSettings) {
        var sidePanelSelector = "#sidePanel-" + registerViewSettings.CurrentRegisterId,
            createListWindowSelector = "#CreateList-" + registerViewSettings.CurrentRegisterId,
            menuSettingsSelector = "#menu-setting_bar-" + registerViewSettings.CurrentRegisterId,
            menuFiltersGridSelector = "#menu-filters_grid-" + registerViewSettings.CurrentRegisterId,
            menuListsGridSelector = "#menu-lists_grid-" + registerViewSettings.CurrentRegisterId,

            splitterSelector = "#verticalSplitter-" + registerViewSettings.CurrentRegisterId,
            gridSelector = "#Grid-" + registerViewSettings.CurrentRegisterId,

            templateOperationListSelector = "#templateOperationList-" + registerViewSettings.CurrentRegisterId,
            menuListsGridToolbarSelector = "#menu-lists_grid-toolbar-" + registerViewSettings.CurrentRegisterId,
            selectRecordsFromSelectedListsBtnSelector = "#selectRecordsFromSelectedLists-" + registerViewSettings.CurrentRegisterId,
            createListFromSelectedRecordsBtnSelector = "#createListFromSelectedRecords-" + registerViewSettings.CurrentRegisterId,
            createAndApplyListFromCurrentSelectedBtnSelector = "#createAndApplyListFromCurrentSelected-" + registerViewSettings.CurrentRegisterId,
            addSelectedRecordsIntoListBtnSelector = "#addSelectedRecordsIntoList-" + registerViewSettings.CurrentRegisterId,
            delSelectedRecordsFromListBtnSelector = "#delSelectedRecordsFromList-" + registerViewSettings.CurrentRegisterId,
            layoutsSettingBtnSelector = "#layoutsSettingBtn-" + registerViewSettings.CurrentRegisterId,
            filtersSettingBtnSelector = "#filtersSettingBtn-" + registerViewSettings.CurrentRegisterId,
            saveStateBtnSelector = "#saveStateBtn-" + registerViewSettings.CurrentRegisterId,
            restoreStateBtnSelector = "#restoreStateBtn-" + registerViewSettings.CurrentRegisterId,
            listSettingBtnSelector = "#listSettingBtn-" + registerViewSettings.CurrentRegisterId;

        // признак, что надо выбирать все записи из основной таблицы
        var selectAllRecrods = false;

        var initSetting = function () {
            $(menuSettingsSelector).kendoPanelBar({
                expandMode: 'multiple',
                select: function () {
                    var selectEl = $(menuSettingsSelector + ' .k-group.k-panel span.k-state-selected');
                    selectEl.removeClass('k-state-selected');
                    selectEl.removeClass('k-state-focused');
                }
            });

            $(menuSettingsSelector).data('kendoPanelBar').expand($(menuSettingsSelector + '>li.k-item'));
        };

        var initFilters = function () {
            var dataSource = new kendo.data.DataSource({
                transport: {
                    read: {
                        url: registerViewSettings.RegistersGetFiltersSettingUrl,
                        dataType: "json",
                        data: {
                            UniqueSessionKey: registerViewSettings.UniqueSessionKey
                        },
                        schema: {
                            model: {
                                id: "Id",
                                fields: {
                                    Name: { type: "string" }
                                }
                            }
                        }
                    }
                }
            });

            $(menuFiltersGridSelector).kendoGrid({
                columns: [
                    { selectable: true, width: "40px" },
                    {
                        field: "Name",
                        title: "Наименование",
                        filterable: {
                            cell: {
                                suggestionOperator: "contains"
                            }
                        }
                    }
                ],
                dataBound: onFilterDataBound,
                filterable: { mode: "row" },
                dataSource: dataSource,
                toolbar: [
                    {
                        name: "find",
                        text: "Найти"
                    },
                    {
                        name: "reset",
                        text: "Сбросить"
                    },
                ]
            });

            //обработка кнопки "Найти"
            $(menuFiltersGridSelector + ' .k-button.k-grid-find').on('click', function (e) {
                e.preventDefault();
                var registerGrid = $('.register-grid').data('kendoGrid');
                if (registerGrid) {
                    if (registerGrid.gridClearSelection != undefined)
                        registerGrid.gridClearSelection();
                    searchAplied = true;
                    registerGrid.dataSource.read();
                }
            });

            //обработка кнопки "Сбросить"
            $(menuFiltersGridSelector + ' .k-button.k-grid-reset').on('click', function (e) {
                e.preventDefault();
                clearSelectedFilterItem();

                if (!window.location.origin) {
                    // For IE
                    window.location.origin = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '');
                }
                history.pushState(null, null, window.location.origin + window.location.pathname);

                var registerGrid = $('.register-grid').data('kendoGrid');
                if (registerGrid) {
                    if (registerGrid.gridClearSelection != undefined)
                        registerGrid.gridClearSelection();
                    searchAplied = true;
                    registerGrid.dataSource.read();
                }
            });
        };

        function onFilterDataBound(arg) {
            if ($_GET["QueryId"]) {
                selectFilterItem(parseInt($_GET["QueryId"]));
            }
        }

        function selectFilterItem(queryId) {
            var filterGrid = $(menuFiltersGridSelector).data('kendoGrid');
            var rows = filterGrid.dataSource.view();

            if (rows.length > 0) {
                for (var i = 0; i < rows.length; i++) {
                    if (rows[i].Id == queryId) {
                        filterGrid.tbody.find("tr[data-uid='" + rows[i].uid + "']").find(".k-checkbox").attr("checked", true);
                    }
                }
            }
        }

        // обработка пунктов меню на вкладке "Списки"
        // получение списка выбранных элементов-списков (вынесено в отдельную функцию, так как используется в несколькиз местах)
        function getSelectedListItem() {
            var menuListsGrid = $(menuListsGridSelector).data('kendoGrid');
            var listOfLists = [];
            if (menuListsGrid) {
                var view = menuListsGrid.dataSource.view();
                for (var i = 0; i < view.length; i++) {
                    if ($(menuListsGridSelector).data('kendoGrid').tbody.find("tr[data-uid='" + view[i].uid + "']").find(".k-checkbox").is(":checked")) {
                        listOfLists.push(view[i].Id);
                    }
                }
            }
            return listOfLists;
        }

        // отчистка выбранных элементов в таблице списков
        function clearSelectedListItem() {
            var view = $(menuListsGridSelector).data('kendoGrid').dataSource.view();
            for (var i = 0; i < view.length; i++) {
                if ($(menuListsGridSelector).data('kendoGrid').tbody.find("tr[data-uid='" + view[i].uid + "']").find(".k-checkbox").is(":checked")) {
                    $(menuListsGridSelector).data('kendoGrid').tbody.find("tr[data-uid='" + view[i].uid + "']").find(".k-checkbox").attr("checked", false);
                }
            }
        }

        // отчистка выбранных элементов в таблице фильтров
        function clearSelectedFilterItem() {
            var view = $(menuFiltersGridSelector).data('kendoGrid').dataSource.view();
            for (var i = 0; i < view.length; i++) {
                if ($(menuFiltersGridSelector).data('kendoGrid').tbody.find("tr[data-uid='" + view[i].uid + "']").find(".k-checkbox").is(":checked")) {
                    $(menuFiltersGridSelector).data('kendoGrid').tbody.find("tr[data-uid='" + view[i].uid + "']").find(".k-checkbox").attr("checked", false);
                }
            }
            $(menuFiltersGridSelector).data('kendoGrid').clearSelection();
            delete $_GET.QueryId;
        }

        // кнопка "Найти" на панели "Списки"
        var searchListButton = function () {
            var registerGrid = $(gridSelector).data('kendoGrid');
            if (registerGrid) {
                searchAplied = true;
                registerGrid.dataSource.read();
            }
        };

        // выбор элементов в таблице, по элементам из списков
        var selectRecordsFromSelectedListsFunction = function () {
            var listOfLists = getSelectedListItem();
            if (listOfLists.length > 0) {
                $.ajax({
                    url: registerViewSettings.RegistersGetSelectedElemensFromListsUrl,
                    type: 'POST',
                    contentType: 'application/json',
                    data: JSON.stringify({ ids: listOfLists }),
                    success: function (result) {
                        checkedIds = result;
                        clearSelectedListItem();
                        $(gridSelector).data("kendoGrid").dataSource.read()
                    },
                    error: function (result) {
                        Common.UI.ShowDialog({
                            title: 'Ошибка',
                            content: '<div style="text-align:center;padding:10px;">' + result.responseText + '</div>',
                            icon: 'error',
                            showCloseBtn: true
                        });
                    }
                });
            } else {
                Common.UI.ShowDialog({
                    title: 'Внимание',
                    content: 'Не выбрано ни одного списка!',
                    icon: 'warning',
                    showCloseBtn: true
                });
            }
        };

        // создать список на основе выбранных элементов
        var createListFromSelectedRecordsFunction = function () {
            selectAllRecrods = false;
            createList = $(createListWindowSelector).data('kendoWindow');
            createList.title('Создать список');
            createList.refresh({
                url: registerViewSettings.RegistersListIndexUrl,
                type: "GET",
                data: { UniqueSessionKey: registerViewSettings.UniqueSessionKey, UniqueSessionKeySetManually: true }
            });
            createList.center().open();
        };

        // создать список на основе всех текущих элементов таблицы
        var createAndApplyListFromCurrentSelectedFunction = function () {
            selectAllRecrods = true;
            createList = $(createListWindowSelector).data('kendoWindow');
            createList.title('Создать список');
            createList.refresh({
                url: registerViewSettings.RegistersListIndexUrl,
                type: "GET",
                data: { UniqueSessionKey: registerViewSettings.UniqueSessionKey, UniqueSessionKeySetManually: true }
            });
            createList.center().open();
        };

        // добавление в списки новых элементов, выбранных в таблице
        var addSelectedRecordsIntoListFunction = function () {
            if (checkedIds.length > 0) {
                var listOfLists = getSelectedListItem();
                if (listOfLists.length > 0) {
                    Common.UI.ShowConfirm({
                        title: 'Подтверждение',
                        content: 'Вы действительно хотите добавить элементы в списки?',
                        onSuccess: function (e) {
                            $.ajax({
                                url: registerViewSettings.RegistersAddElementsIntoListsUrl,
                                type: 'POST',
                                contentType: 'application/json',
                                data: JSON.stringify({ ids: listOfLists, selecetdElements: checkedIds }),
                                success: function () {
                                    Common.UI.ShowDialog({
                                        content: 'Списки успешно обновлены',
                                        icon: 'ok',
                                        showCloseBtn: true
                                    });
                                    clearSelectedListItem();
                                },
                                error: function () {
                                    Common.UI.ShowDialog({
                                        title: 'Ошибка',
                                        content: result.responseText,
                                        icon: 'error',
                                        showCloseBtn: true
                                    });
                                }
                            });
                        }
                    });
                } else {
                    Common.UI.ShowDialog({
                        title: 'Внимание',
                        content: 'Не выбрано ни одного списка!',
                        icon: 'warning',
                        showCloseBtn: true
                    });
                }
            } else {
                Common.UI.ShowDialog({
                    title: 'Внимание',
                    content: 'Не выбрано ни одной записи!',
                    icon: 'warning',
                    showCloseBtn: true
                });
            }
        };

        // удаление из списков элементов, выбранных в таблице
        var delSelectedRecordsFromListFunction = function () {
            if (checkedIds.length > 0) {
                var listOfLists = getSelectedListItem();
                if (listOfLists.length > 0) {
                    Common.UI.ShowConfirm({
                        title: 'Подтверждение',
                        content: 'Вы действительно хотите удалить элементы из списков?',
                        onSuccess: function (e) {
                            $.ajax({
                                url: registerViewSettings.RegistersDelElementsFromListUrl,
                                type: 'POST',
                                contentType: 'application/json',
                                data: JSON.stringify({ ids: listOfLists, selecetdElements: checkedIds }),
                                success: function () {
                                    clearSelectedListItem();
                                    Common.UI.ShowDialog({
                                        content: 'Списки успешно обновлены',
                                        icon: 'ok',
                                        showCloseBtn: true
                                    });
                                },
                                error: function () {
                                    Common.UI.ShowDialog({
                                        title: 'Ошибка',
                                        content: result.responseText,
                                        icon: 'error',
                                        showCloseBtn: true
                                    });
                                }
                            });
                        }
                    });
                } else {
                    Common.UI.ShowDialog({
                        title: 'Внимание',
                        content: 'Не выбрано ни одного списка!',
                        icon: 'warning',
                        showCloseBtn: true
                    });
                }
            } else {
                Common.UI.ShowDialog({
                    title: 'Внимание',
                    content: 'Не выбрано ни одной записи!',
                    icon: 'warning',
                    showCloseBtn: true
                });
            }
        };

        var initLists = function () {
            var dataSource = new kendo.data.DataSource({
                transport: {
                    read: {
                        url: registerViewSettings.RegistersGetListsSettingUrl,
                        dataType: "json",
                        data: { UniqueSessionKey: registerViewSettings.UniqueSessionKey, UniqueSessionKeySetManually: true },
                        schema: {
                            model: {
                                id: "Id",
                                fields: {
                                    Name: { type: "string" }
                                }
                            }
                        }
                    }
                }
            });
            $(menuListsGridSelector).kendoGrid({
                columns: [
                    { selectable: true, width: "40px" },
                    {
                        field: "Name",
                        title: "Наименование",
                        filterable: {
                            cell: {
                                suggestionOperator: "contains"
                            }
                        }
                    }
                ],
                dataBinding: onDataBinding,
                filterable: { mode: "row" },
                dataSource: dataSource,
                toolbar: [{ template: kendo.template($(templateOperationListSelector).html()) }],
                messages: {
                    commands: {
                        find: "Найти"
                    }
                }
            });

            $(menuListsGridToolbarSelector).kendoToolBar({
                items: [
                    { type: "button", text: "Найти", click: searchListButton },
                    {
                        type: "splitButton",
                        text: "Операции",
                        menuButtons: [
                            { text: "Выбрать записи из выбранных списков", id: selectRecordsFromSelectedListsBtnSelector.substring(1, selectRecordsFromSelectedListsBtnSelector.length), click: selectRecordsFromSelectedListsFunction },
                            { text: "Создать список из выбранных записей", id: createListFromSelectedRecordsBtnSelector.substring(1, createListFromSelectedRecordsBtnSelector.length), click: createListFromSelectedRecordsFunction },
                            { text: "Создать и применить список по текущей выборке", id: createAndApplyListFromCurrentSelectedBtnSelector.substring(1, createAndApplyListFromCurrentSelectedBtnSelector.length), click: createAndApplyListFromCurrentSelectedFunction },
                            { text: "Добавить выбранные записи в список", id: addSelectedRecordsIntoListBtnSelector.substring(1, addSelectedRecordsIntoListBtnSelector.length), click: addSelectedRecordsIntoListFunction },
                            { text: "Удалить выбранные записи из списка", id: delSelectedRecordsFromListBtnSelector.substring(1, delSelectedRecordsFromListBtnSelector.length), click: delSelectedRecordsFromListFunction }
                        ]
                    }
                ]
            });
        };

        // Если таблица со списками пустая, то ряд пуктов меню должна быть неактивной
        function onDataBinding(arg) {
            if ($(menuListsGridSelector).data('kendoGrid').dataSource.view().length == 0) {
                $(selectRecordsFromSelectedListsBtnSelector).addClass('k-state-disabled');
                $(addSelectedRecordsIntoListBtnSelector).addClass('k-state-disabled');
                $(delSelectedRecordsFromListBtnSelector).addClass('k-state-disabled');
            }
        }


        $(sidePanelSelector + ' .movePanel .header .close').on('click', function () {
            var panel = $(sidePanelSelector + " .movePanel.open");

            kendo.fx(panel).slideInLeft().play();
            panel.removeClass("open");
        });

        initSetting();
        initFilters();
        initLists();
        selectFilterItem(61);

        $(sidePanelSelector + " .sideButton").on('click', function (e) {
            var dataId = $(this).attr('data-id');
            var otherPanels = $(sidePanelSelector + " .movePanel.open[data-id!='" + dataId + "']");

            otherPanels.css("transform", "");
            otherPanels.removeClass("open");

            var panel = $(sidePanelSelector + " .movePanel[data-id='" + dataId + "']");
            if (panel === undefined || panel == null || panel.length <= 0) return;

            var isVisible = panel.position().left > 0;
            if (isVisible) {
                kendo.fx(panel).slideInLeft().play();
                panel.removeClass("open");
            } else {
                panel.addClass("open");
                kendo.fx(panel).slideInLeft().reverse();
            }

            return false;
        });

        $(sidePanelSelector + ' .sideButton').hover(function () {
            $(this).addClass('k-state-hover');
        }, function () {
            $(this).removeClass('k-state-hover');
        });

        $(layoutsSettingBtnSelector).on('click', function () {
            url = registerViewSettings.CoreRegisterLayoutAllUrl;
            title = 'Раскладки ' + registerViewSettings.CurrentRegisterViewTitle;

            Common.UI.ShowWindow(title, url);
        });

        $(filtersSettingBtnSelector).on('click', function () {
            url = registerViewSettings.CoreRegisterQryAllUrl;
            title = 'Фильтры ' + registerViewSettings.CurrentRegisterViewTitle;

            Common.UI.ShowWindow(title, url);
        });

        $(listSettingBtnSelector).on('click', function () {
            url = registerViewSettings.CoreRegisterListAllUrl;
            title = 'Списки ' + registerViewSettings.CurrentRegisterViewTitle;

            Common.UI.ShowWindow(title, url);
        });

        $(saveStateBtnSelector).on('click', function () {
            kendo.ui.progress($('body'), true);
            $.ajax({
                url: registerViewSettings.RegistersSaveStateUrl,
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({
                    UpperPanelSize: $(splitterSelector + ' .k-pane:first').height(),
                    CardPanelSize: $(splitterSelector + ' .k-pane:last').height()
                }),
                success: function () {
                    Common.UI.ShowMessage('Сохранение состояния', 'Сохранение состояния представления произведено успешно');
                    kendo.ui.progress($('body'), false);
                },
                error: function () {
                    Common.UI.ShowMessage('Сохранение состояния', 'Произошла ошибка. Обратитесь к администратору');
                    kendo.ui.progress($('body'), false);
                }
            });
        });

        $(restoreStateBtnSelector).on('click', function () {
            kendo.ui.progress($('body'), true);
            $.ajax({
                url: registerViewSettings.RegistersRestoreStateUrl,
                type: 'POST',
                contentType: 'application/json',
                success: function (result) {
                    if (result !== undefined && result != null) {
                        Common.UI.ShowMessage('Восстановление состояния', 'Восстановление состояния представления произведено успешно');
                    } else {
                        var splitter = $(splitterSelector).data("kendoSplitter");

                        splitter.expand(".k-pane:first");
                        splitter.expand(".k-pane:last");

                        if (result.UpperPanelSize == 0) {
                            splitter.collapse(".k-pane:first");
                        } else if (result.IsCollapseDownPanel) {
                            splitter.collapse(".k-pane:last");
                        } else {
                            splitter.size(".k-pane:first", result.UpperPanelSize);
                        }

                        Common.UI.ShowMessage('Восстановление состояния', 'Отсутствует сохраненное состояние');
                    }
                    kendo.ui.progress($('body'), false);
                },
                error: function () {
                    Common.UI.ShowMessage('Восстановление состояния', 'Произошла ошибка. Обратитесь к администратору');
                    kendo.ui.progress($('body'), false);
                }
            });
        });

        $(sidePanelSelector + ' [name=IsTransition]').on('change', function (e) {
            e.preventDefault();
            var registerGrid = $(gridSelector).data('kendoGrid');
            if (registerGrid) {
                registerGrid.dataSource.read();
            }
        });
    },

    InitFilterWindow: function (registerViewSettings) {
        var filterWindowSelector = "#filterWindow-" + registerViewSettings.CurrentRegisterId,
            menuFiltersGridSelector = "#menu-filters_grid-" + registerViewSettings.CurrentRegisterId,
            gridSelector = "#Grid-" + registerViewSettings.CurrentRegisterId,
            filterButtonSelector = "#filterButton-" + registerViewSettings.CurrentRegisterId,
            filterWindowContentClass = "filter-window-content",
            filterWindowFooter = "filter-window-footer";

        $.ajax({
            url: registerViewSettings.RegistersGetFiltersSettingUrl,
            type: 'GET',
            data: {
                UniqueSessionKey: registerViewSettings.UniqueSessionKey
            },
            dataType: 'json',
            success: function (data) {
                if (data && data.length > 0) {
                    var initWindow = function () {
                        var $dialogTemplate = $('<div class="' + filterWindowContentClass + '"><div class="k-window-actions"><span class="filter-window-title">Фильтры</span><a href="#" class="k-button k-bare k-button-icon k-button-close" style="float: right;"><span class="k-icon k-i-close" style="color: rgba(96, 101, 116, 0.5);"></span></a></div><div class="filter-window-grid-container"><div id="menu-filters_grid-' + registerViewSettings.CurrentRegisterId + '"></div></div><div class="' + filterWindowFooter + '"></div></div>');
                        var $dialog = $dialogTemplate.kendoWindow({
                            title: false,
                            draggable: false,
                            visible: false,
                            resizable: false,
                            width: '430px',
                            height: '370px'
                        }).data('kendoWindow');

                        $('.' + filterWindowFooter).append('<button type="button" class="k-button k-primary k-grid-find pull-right" style="width: 132px;">Найти</button><button type="button" class="k-button k-grid-reset pull-right" style="width: 105px;">Сбросить</button>')

                        return $dialog;
                    };


                    var initFilters = function () {
                        var dataSource = new kendo.data.DataSource({
                            transport: {
                                read: {
                                    url: registerViewSettings.RegistersGetFiltersSettingUrl,
                                    dataType: "json",
                                    data: {
                                        UniqueSessionKey: registerViewSettings.UniqueSessionKey
                                    },
                                    schema: {
                                        model: {
                                            id: "Id",
                                            fields: {
                                                Name: { type: "string" }
                                            }
                                        }
                                    }
                                }
                            }
                        });

                        $(menuFiltersGridSelector).kendoGrid({
                            columns: [
                                { selectable: true, width: "40px" },
                                {
                                    field: "Name",
                                    title: "Наименование"
                                }
                            ],
                            dataBound: onFilterDataBound,
                            dataSource: dataSource
                        });

                        //обработка кнопки "Найти"
                        $('.' + filterWindowFooter + ' .k-button.k-grid-find').on('click', function (e) {
                            e.preventDefault();
                            var registerGrid = $('.register-grid').data('kendoGrid');
                            if (registerGrid) {
                                if (registerGrid.gridClearSelection != undefined)
                                    registerGrid.gridClearSelection();
                                searchAplied = true;
                                registerGrid.dataSource.read();
                            }
                        });

                        //обработка кнопки "Сбросить"
                        $('.' + filterWindowFooter + ' .k-button.k-grid-reset').on('click', function (e) {
                            e.preventDefault();
                            clearSelectedFilterItem();

                            if (!window.location.origin) {
                                // For IE
                                window.location.origin = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '');
                            }
                            history.pushState(null, null, window.location.origin + window.location.pathname);

                            var registerGrid = $('.register-grid').data('kendoGrid');
                            if (registerGrid) {
                                if (registerGrid.gridClearSelection != undefined)
                                    registerGrid.gridClearSelection();
                                searchAplied = true;
                                registerGrid.dataSource.read();
                            }
                        });
                    };

                    function onFilterDataBound(arg) {
                        if ($_GET["QueryId"]) {
                            selectFilterItem(parseInt($_GET["QueryId"]));
                        }
                    }

                    function selectFilterItem(queryId) {
                        var filterGrid = $(menuFiltersGridSelector).data('kendoGrid');
                        if (filterGrid) {
                            var rows = filterGrid.dataSource.view();

                            if (rows.length > 0) {
                                for (var i = 0; i < rows.length; i++) {
                                    if (rows[i].Id == queryId) {
                                        filterGrid.tbody.find("tr[data-uid='" + rows[i].uid + "']").find(".k-checkbox").attr("checked", true);
                                    }
                                }
                            }
                        }
                    }

                    // отчистка выбранных элементов в таблице фильтров
                    function clearSelectedFilterItem() {
                        var view = $(menuFiltersGridSelector).data('kendoGrid').dataSource.view();
                        for (var i = 0; i < view.length; i++) {
                            if ($(menuFiltersGridSelector).data('kendoGrid').tbody.find("tr[data-uid='" + view[i].uid + "']").find(".k-checkbox").is(":checked")) {
                                $(menuFiltersGridSelector).data('kendoGrid').tbody.find("tr[data-uid='" + view[i].uid + "']").find(".k-checkbox").attr("checked", false);
                            }
                        }
                        $(menuFiltersGridSelector).data('kendoGrid').clearSelection();
                        delete $_GET.QueryId;
                    }

                    var dialog = initWindow();
                    initFilters();
                    selectFilterItem(61);

                    $(filterButtonSelector).on('click', function () {
                        dialog.center().open();
                    });

                    $('.' + filterWindowContentClass + ' .k-button-close').on('click', function () {
                        dialog.close();
                    });

                    $(filterWindowSelector + ' [name=IsTransition]').on('change', function (e) {
                        e.preventDefault();
                        var registerGrid = $(gridSelector).data('kendoGrid');
                        if (registerGrid) {
                            registerGrid.dataSource.read();
                        }
                    });
                } else {
                    $(filterButtonSelector).addClass("k-state-disabled");
                }
            }
        });

    }
};