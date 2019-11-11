var Common = {
    ShowError: function (message) {
        notificationPanel.hide();
        notificationPanel.setOptions({ autoHideAfter: 0 });

        var value;
        if (Array.isArray(message)) {
            value = message.reduce(function (result, value) { return result + '<br>' + value; });
        } else {
            value = message;
        }

        notificationPanel.show({ message: value }, 'error');
    },
    ShowMessage: function (message) {
        notificationPanel.hide();
        notificationPanel.setOptions({ autoHideAfter: 5000 });
        notificationPanel.show({ message: message }, 'success');
    },
    ClearNotification: function () {
        notificationPanel.hide();
    },
    GetUrlParameter: function (sParam) {
        var sPageURL = decodeURIComponent(window.location.search.substring(1)),
            sURLVariables = sPageURL.split('&'),
            sParameterName,
            i;

        for (i = 0; i < sURLVariables.length; i++) {
            sParameterName = sURLVariables[i].split('=');

            if (sParameterName[0] === sParam) {
                return sParameterName[1] === undefined ? true : sParameterName[1];
            }
        }
    },
    UpdateURLParameter: function (url, param, paramVal) {
        var TheAnchor = null;
        var newAdditionalURL = "";
        var tempArray = url.split("?");
        var baseURL = tempArray[0];
        var additionalURL = tempArray[1];
        var temp = "";

        if (additionalURL) {
            var tmpAnchor = additionalURL.split("#");
            var TheParams = tmpAnchor[0];
            TheAnchor = tmpAnchor[1];
            if (TheAnchor)
                additionalURL = TheParams;

            tempArray = additionalURL.split("&");

            for (var i = 0; i < tempArray.length; i++) {
                if (tempArray[i].split('=')[0] != param) {
                    newAdditionalURL += temp + tempArray[i];
                    temp = "&";
                }
            }
        }
        else {
            var tmpAnchor = baseURL.split("#");
            var TheParams = tmpAnchor[0];
            TheAnchor = tmpAnchor[1];

            if (TheParams)
                baseURL = TheParams;
        }

        if (TheAnchor)
            paramVal += "#" + TheAnchor;

        var rows_txt = temp + "" + param + "=" + paramVal;
        return baseURL + "?" + newAdditionalURL + rows_txt;
    },
    GetUrlParameters: function (url) {
        var parameters = [],
            urlQuery = null;
        if (url && url.length > 0) {
            var queryTermIndex = url.indexOf('?');
            if (queryTermIndex > -1) {
                urlQuery = url.substring(queryTermIndex, url.length);
            }
            else {
                return parameters;
            }
        }
        else {
            urlQuery = window.location.search;
        }
        var sPageURL = decodeURIComponent(urlQuery.substring(1)),
            sURLVariables = sPageURL.split('&');
        for (i = 0; i < sURLVariables.length; i++) {
            sParameter = sURLVariables[i].split('=');

            parameters.push({ Name: sParameter[0], Value: sParameter[1] });
        }
        return parameters;
    },
    IsExistsKeyInObject: function (obj, key) {
        var objectKeys = Object.keys(obj);
        for (var i = 0; i < objectKeys.length; i++) {
            if (objectKeys[i].toLowerCase() === key.toLowerCase()) {
                return true;
            }
        }
        return false;
    },
    DictionaryValueMapper: function (url, getParameters) {
        function mapperFn(options) {
            var params = $.isFunction(getParameters) ? getParameters() : {};

            params.values = options.value;

            $.ajax({
                url: url,
                type: 'GET',
                data: params,
                success: function (data) {
                    options.success(data);
                }
            });
        };
        return mapperFn;
    },
    Functions: {
        GenerateId: function () {
            return Math.floor(Math.random() * 26) + Date.now();
        },
        //skipHtmlFieldPrefix - удаляет HtmlFieldPrefix (возвращает имя свойства после последней точки)
        FormToObject: function (form, skipHtmlFieldPrefix) {
            var formObject = {},
                formDataArray = form.find(':input:not([data-skip="true"])').serializeArray();

            $.map(formDataArray, function (object, index) {
                var key = skipHtmlFieldPrefix ? object['name'].split('.').pop() : object['name'];
                formObject[key] = object['value'];
            });

            //доработка для чекбоксов
            $.each(form.find('input:checkbox'), function (index, input) {
                var checkbox = $(input);
                formObject[checkbox.attr('name')] = checkbox.is(':checked');
            });

            //доработка для мультиселектов Kendo
            $.each(form.find('select[data-role="multiselect"]'), function (index, select) {
                var multiSelect = $(select).data('kendoMultiSelect');
                if (multiSelect) {
                    formObject[$(multiSelect.element).attr('name')] = multiSelect.value();
                }
            });

            return formObject;
        },
        SerializeFormToJSON: function (form) {
            return JSON.stringify(Common.Functions.FormToObject(form));
        },
        SerializeFormToQueryString: function (form) {
            return $.param(Common.Functions.FormToObject(form), true);
        },
        //получает значение из объекта по ключу игнорируя префикс
        GetPropertyByName: function (object, keyName) {
            var regEx = new RegExp('^.+\.' + keyName + '$');
            for (key in object)
                if (regEx.test(key))
                    return object[key];
            return null;
        },
        //устанавливает свойства из объекта в объект из kendo.dataSource  
        SetPropertiesToDataSourceItem: function (dataItem, object) {
            var fields = dataItem.fields;

            for (key in object) {
                if (fields[key]) {
                    var objectValue = object[key];
                    if (fields[key].type != typeof (objectValue)) {
                        objectValue = Common.Functions.TryConvertToType(objectValue, fields[key]);
                    }
                    dataItem.set(key, objectValue);
                }
            }
            return dataItem;
        },
        TryConvertToType: function (value, type) {
            var returnValue = null,
                currentType = typeof (value);

            switch (currentType) {
                case 'string':
                    switch (type) {
                        //TODO kendo.parseDate                                                                             
                        case 'date':
                            returnValue = value;
                            break;
                        case 'number':
                            returnValue = value;
                            break;
                        case 'boolean':
                            returnValue = (value.toLowerCase() === "true");
                            break;
                        default:
                            returnValue = value;
                    }
                    break;
                default:
                    returnValue = value;
            }

            return returnValue;
        },
        GetDaysInMonth: function (month, year) {
            if (month && year) {
                return new Date(year, month, 0).getDate();
            }
            return null;
        },
        LowercaseFirstLetter: function (string) {
            if (typeof (string) === 'string') {
                return string.charAt(0).toLowerCase() + string.slice(1);
            }
            return '';
        },
        GetSectionNumberForSort: function (sectionString) {
            var sections = sectionString.split('.');
            var result = '';
            for (var i = 0; i < sections.length; i++) {
                result += sections[i].padStart(3, '0');
            }
            return result;
        },
        UpdateTabCaption: function (objectId, registerViewId, url) {
            $.ajax({
                type: 'POST',
                url: url,
                data: JSON.stringify({ objectId: objectId, registerViewId: registerViewId }),
                contentType: 'application/json',
                success: function (response) {
                    var ulList = window.parent['TabStrip'].children[0].children;
                    if (response.length == ulList.length) {
                        for (let i = 0; i < ulList.length; i++) {
                            ulList[i].children[1].innerText = response[i];
                        }
                    }
                    kendo.ui.progress($('body'), false);
                }, error: function (response) {
                    Common.ShowError(response.responseText);
                }
            });
        },
        GetDateByString: function (date, format, delimiter) {
            var formatLowerCase = format.toLowerCase();
            var formatItems = formatLowerCase.split(delimiter);
            var dateItems = date.split(delimiter);
            var monthIndex = formatItems.indexOf("mm");
            var dayIndex = formatItems.indexOf("dd");
            var yearIndex = formatItems.indexOf("yyyy");
            var month = parseInt(dateItems[monthIndex]) - 1;
            var formatedDate = new Date(dateItems[yearIndex], month, dateItems[dayIndex]);
            return formatedDate;
        }
    },
    UI: {
        CreateChooseWindow: function (config) {
            var windowPlacehoder = $('<div></div>').appendTo(document.body);
            kendo.ui.progress(windowPlacehoder, true);
            var window = windowPlacehoder.kendoWindow({
                title: config.title,
                modal: true,
                content: config.url,
                iframe: true,
                close: function (e) {
                    this.destroy();
                    if (e.userTriggered && config.closeCallback)
                        config.closeCallback();
                },
                refresh: function () {
                    var windowElement = $(this.element);
                    var iframeDomElement = windowElement.children("iframe")[0];
                    var iframeWindowObject = iframeDomElement.contentWindow;
                    var iframeDocumentObject = iframeDomElement.contentDocument;
                    var iframejQuery = iframeWindowObject.$;
                    var gridEl = iframejQuery('#' + config.gridId);
                    var gridKendoGrid = gridEl.data('kendoGrid');
                    var selectButton = iframejQuery('.k-grid-toolbar .k-button:contains("Выбрать")');

                    if (gridKendoGrid) {
                        if (!config.useCheckedItems)
                            gridKendoGrid.bind('dataBound', function () {
                                var grid = this;
                                grid.element.find('tr').css('cursor', 'pointer');
                                grid.element.find('tbody tr[data-uid]').dblclick(function (e) {
                                    e.preventDefault();
                                    selectButton.click();
                                });
                            });

                        selectButton
                            .off('click')
                            .on('click', function (e) {
                            if (config.useCheckedItems) {
                                iframeWindowObject.getCheckedItems().then(function(items) {
                                    if (!items || items.length === 0)
                                        return Common.ShowError('Не отмечено ни одной записи');
                                    config.callback(items);
                                    window.close();
                                });
                            } else {
                                var grid = gridEl.data('kendoGrid');
                                var dataItem = grid.dataItem(grid.select());
                                if (dataItem == null)
                                    Common.UI.ShowDialog({
                                        title: 'Ошибка',
                                        content: 'Не выбрана строка!',
                                        icon: 'error',
                                        showCloseBtn: true
                                    });
                                else if (config.callback)
                                    config.callback(dataItem);
                                window.close();
                            }
                            e.preventDefault();
                            return false;
                        });
                    }

                    kendo.ui.progress(windowPlacehoder, false);
                }
            }).data('kendoWindow');
            if (config.onLoad)
                window.element.children().load(config.onLoad);

            window.center();
            window.open().maximize();
            return window;
        },
        ChooseWindow: function (title, contentUrl, callbackFn, gridName) {
            return Common.UI.CreateChooseWindow({ title: title, url: contentUrl, callback: callbackFn, gridId: gridName});
        },
        //возвращает модальное окно Telerik AJAX
        GetRadWindow: function () {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement && window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        },
        //закрывает модальное окно Telerik AJAX
        GetRadWindowAndClose: function () {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement && window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            oWindow.close();
        },
        //параметры конфигурации
        //title - заголовок
        //content - html-строка содержимое
        //width - ширина
        //height - высота
        //showCloseBtn - показывать кнопку закрытия (по-умолчанию) под содержимым окна
        //closeBtnCaption - текст кнопки закрытия
        //additionalButtons - массив с конфигом кнопок для футера
        ////////////////////////////////////////////
        //конфиг кнопки
        //id = идентификатор
        //class = css классы
        //click = обработчик
        ////////////////////////////////////////////
        //onOpen - обработчик события "Окно открыто"
        //onClose - обработчик события "Окно закрыто"
        ShowDialog: function (config) {
            if (!config)
                config = {};

            //TODO подсчет ширины контента
            var uniqueId = 'dialog-' + Common.Functions.GenerateId(),
                windowCfg = {
                    title: config.title || '',
                    width: config.width || 500,
                    height: config.height || 135,
                    showCloseBtn: config.showCloseBtn,
                    closeBtnCaption: config.closeBtnCaption || 'OK',
                    additionalButtons: config.additionalButtons,
                    //константы
                    actions: [
                        "Close"
                    ],
                    modal: true,
                    //иначе событие open срабатывает дважды
                    visible: false
                };

            var iconBlock = '';
            var icon = '';
            if (config.icon || config.icon != 'none') {
                switch (config.icon) {
                    case 'info':
                        icon = 'k-i-information';
                        break
                    case 'confirm':
                        icon = 'k-i-question';
                        break
                    case 'warning':
                        icon = 'k-i-warning';
                        break
                    case 'ok':
                        icon = 'k-i-check';
                        break
                    case 'error':
                        icon = 'k-i-close-outline';
                        break
                }

                iconBlock = '<div class="dialog-icon k-icon ' + icon + '"></div>';
            }

            var dialogContent = $('<div></div>');
            var dialogBody = $('<div id="' + uniqueId + '" class="dialog-body">' + iconBlock + '<div class="dialog-content">' + (config.content || '') + '</div></div></div>');
            var dialogFooter = $('<div class="dialog-footer"></div>');

            if (!config.icon || config.icon == 'none') {
                dialogBody.find('.dialog-content').css('left', '15px');
            }

            if (config.onOpen && $.isFunction(config.onOpen)) {
                windowCfg.open = config.onOpen;
            }

            if (config.onClose && $.isFunction(config.onClose)) {
                windowCfg.close = config.onClose;
            }

            if (config.showCloseBtn) {
                var closeBtn = $('<button class="k-primary">' + (config.closeBtnCaption || 'OK') + '</button>');
                closeBtn.kendoButton();
                closeBtn.on('click', function (e) {
                    kendoDialog.close();
                    kendoDialog.destroy();
                });
                dialogFooter.append(closeBtn);
            }

            if (config.additionalButtons && config.additionalButtons.length > 0) {
                for (var i = 0; i < config.additionalButtons.length; i++) {
                    var button = $('<button style="margin-left:10px;">' + config.additionalButtons[i].caption + '</button>');
                    var id = config.additionalButtons[i].id ? config.additionalButtons[i].id : Common.Functions.GenerateId();
                    button.attr('id', id);
                    button.addClass(config.additionalButtons[i].class);
                    button.kendoButton();
                    button.on('click', config.additionalButtons[i].click);
                    dialogFooter.append(button);
                }
            }
            dialogContent.append(dialogBody);
            dialogContent.append(dialogFooter);
            $('body').append(dialogContent);
            var kendoDialog = dialogContent.kendoWindow(windowCfg).data('kendoWindow');
            kendoDialog.center().open();

            if (config.needMaximaze) {
                kendoDialog.maximize();
            }
        },
        ShowMessage: function (title, message) {
            Common.UI.ShowDialog({
                title: title,
                content: message,
                icon: 'info',
                showCloseBtn: true
            });
        },
        //параметры конфигурации
        //title - заголовок
        //content - html-строка содержимое
        //width - ширина
        //height - высота
        //onSuccess - обработчик кнопки "Да"
        //onFail - обработчик кнопки "Нет"
        ShowConfirm: function (windowCfg) {
            if (!windowCfg.onFail) {
                windowCfg.onFail = function () {
                    var window = $(this).closest('[data-role=window]').data('kendoWindow');
                    window.close();
                    window.destroy();
                };
            }

            Common.UI.ShowDialog({
                title: windowCfg.title,
                content: windowCfg.content,
                icon: windowCfg.icon || 'confirm',
                width: windowCfg.width,
                height: windowCfg.height,
                showCloseBtn: false,
                additionalButtons: [{
                    caption: 'Да',
                    click: function (e) {
                        windowCfg.onSuccess(e);

                        var window = $(this).closest('[data-role=window]').data('kendoWindow');
                        window.close();
                        window.destroy();
                    },
                    class: 'k-primary'
                }, {
                    caption: 'Нет',
                    click: windowCfg.onFail
                }]
            });
        },
        OpenSearchInRadWindow: function (title, url, onClose) {
            var mainWnd = window.parent || window;
            while (!(typeof mainWnd['radopen'] == 'function')) {
                mainWnd = mainWnd.parent;
            }
            wnd = mainWnd.radopen(url)
            wnd.set_title(title);
            wnd.set_behaviors(4);
            wnd.set_visibleStatusbar(false);
            wnd.set_destroyOnClose(true);
            wnd.maximize();
            if (onClose && $.isFunction(onClose))
                wnd.add_close(onClose);
        },
        ShowWindow: function (title, url, name, onClose, width, height, draggable, resizeble) {
            var windowPlacehoder = $('<div id="' + name + '"></div>');
            $('body').append(windowPlacehoder);

            var needMaximaze = true;
            var drag = false;
            var resize = false;

            if (draggable) {
                drag = draggable;
            }
            if (resizeble) {
                resize = resizeble;
            }

            var windowCfg = {
                title: title,
                content: url,
                iframe: true,
                draggable: drag,
                resizable: resize,
                close: function (e) {
                    this.destroy();
                },
                close2: function (e) {
                    this.destroy();
                }
            };

            if (width && height) {
                needMaximaze = false;
                windowCfg.width = width;
                windowCfg.height = height;
            }


            if (onClose && $.isFunction(onClose)) {
                windowCfg.customClose = function (e) {
                    onClose(e);
                    modalWindow.close();
                    modalWindow.destroy();
                };
            } else {
                windowCfg.customClose = function (e) {
                    modalWindow.close();
                    modalWindow.destroy();
                };
            }

            var modalWindow = windowPlacehoder.kendoWindow(windowCfg).data('kendoWindow');
            modalWindow.center().open();

            if (needMaximaze) {
                modalWindow.maximize();
            }

            function setWindowContentHeight() {
                //доработки для iframe
                var iframe = $(this.element).find('iframe')[0],
                    contentDocument = iframe.contentDocument ? iframe.contentDocument : iframe.contentWindow.document;
                iframe.style.visibility = "hidden";
                //10 пикселей добавляем для отсутствия скролла
                var newHeight = $(contentDocument.body).height();
                iframe.style.height = newHeight + "px";
                iframe.style.visibility = "visible";
            }

            modalWindow.bind('resize', setWindowContentHeight);
            modalWindow.bind('refresh', setWindowContentHeight);

            $(window).resize(function () {
                modalWindow.trigger('resize');
            });

            return modalWindow;
        },
        GetWindow: function (name, parentWindow) {
            var currentWindow = null;

            if (parentWindow == undefined) {
                if ($("#" + name).length && $("#" + name).data("kendoWindow") != undefined) {
                    return $("#" + name).data("kendoWindow");
                }
            }
            else {
                if (parentWindow.$("#" + name).length && parentWindow.$("#" + name).data("kendoWindow") != undefined) {
                    return parentWindow.$("#" + name).data("kendoWindow");
                }
            }

            return currentWindow;
        },
        CloseWindow: function (name, parentWindow) {
            if (parentWindow == undefined) {
                if ($("#" + name).length && $("#" + name).data("kendoWindow") != undefined) {
                    if ($("#" + name).data("kendoWindow").options.customClose) {
                        $("#" + name).data("kendoWindow").options.customClose($("#" + name).data("kendoWindow"));
                    } else {
                        $("#" + name).data("kendoWindow").close();
                    }
                }
            }
            else {
                if (parentWindow.$("#" + name).length && parentWindow.$("#" + name).data("kendoWindow") != undefined) {
                    if (parentWindow.$("#" + name).data("kendoWindow").options.customClose) {
                        parentWindow.$("#" + name).data("kendoWindow").options.customClose(parentWindow.$("#" + name).data("kendoWindow"));
                    } else {
                        parentWindow.$("#" + name).data("kendoWindow").close();
                    }
                }
            }
        },
        DirtyField: function (data, fieldName) {
            if (data.dirty && data.dirtyFields[fieldName]) {
                return "<span class='k-dirty'></span>";
            }
            else {
                return "";
            }
        },
        CustomTemplate: function (fieldName, template) {
            var dirtyTemplate = "#=Common.UI.DirtyField(data,\'" + fieldName + "\')#";

            if (template && template.length > 0) {
                return dirtyTemplate + " " + template;
            }

            return dirtyTemplate + "#:" + fieldName + " != null ? " + fieldName + " :''#";
        },
        ReloadParentWindowTabStrip: function (tabStripSelector, tabId) {
            if (window.parent) {
                var tabStrip = window.parent.$(tabStripSelector).data('kendoTabStrip');
                if (tabStrip) {
                    var reloadTab = tabStrip.tabGroup.find('[data-tab-id="' + tabId + '"]');
                    if (reloadTab.length > 0) {
                        var tabIndex = reloadTab.index();
                        var iframeContent = window.parent.$(tabStrip.contentElement(tabIndex)).find('iframe');
                        if (iframeContent.length > 0) {
                            iframeContent[0].contentWindow.location.reload();
                        }
                    }
                }
            }
        },
        ElementToolTip: function (element, filter, content) {
            //Вызываем после перестроения всего DOM 
            $(window).load(function () {
                element.kendoTooltip({
                    filter: filter,
                    content: content
                });
            })
        },
        GridToolTip: function (element, filter, content) {
            if (!filter) {
                filter = 'th:not(:empty),tr>td:not(:empty)'
            }
            if (!$.isFunction(content)) {
                content = function (e) {
                    if (e.target.text().length != 0) {
                        return e.target.text();
                    }
                }
            }
            Common.UI.ElementToolTip(element, filter, content);
        }
    }
};
var KendoExtension = {
    EditorReference: function (url, referenceId, valueFieldParam, textFieldParam, onSelectParam) {
        var valueField = valueFieldParam || 'ItemId';
        var textField = textFieldParam || 'Value';
        var url = url || '/Dictionary/GetItems';

        return function (container, options) {
            var inputPeriodValue = $('<input />');
            inputPeriodValue.appendTo(container).kendoDropDownList({
                dataTextField: textField,
                dataValueField: valueField,
                dataSource: new kendo.data.DataSource({
                    transport: {
                        read: {
                            url: url,
                            data: { referenceId: referenceId },
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json'
                        }
                    },
                    schema: {
                        model: {
                            id: "ItemId",
                            fields: {
                                ItemId: { editable: false, defaultValue: -1 },
                                Code: { editable: false },
                                Value: { editable: false }
                            }
                        }
                    }
                }),
                select: onSelectParam || function (e) {
                    var dataItem = options.model;
                    dataItem.set(options.field, e.dataItem.Value);
                    dataItem.set(options.field + '_Code', e.dataItem.ItemId);
                },
                dataBound: function (e) {
                    var dataItem = options.model;
                    var selectedId = dataItem[options.field + '_Code'];
                    var availableItems = this.dataSource.data();
                    for (var i = 0; i < availableItems.length; i++) {
                        if (availableItems[i].ItemId === selectedId) {
                            this.select(i);
                        }
                    }
                }
            })
        }
    },
    EditorDecimalPeriod: function (container, options) {
        //PeriodId
        var inputPeriodId = $('<input type="hidden"/>');
        inputPeriodId.attr('name', options.field + '.PeriodId');
        inputPeriodId.appendTo(container);

        //Value
        var inputPeriodValue = $('<input />');
        inputPeriodValue.attr('name', options.field + '.Value');
        inputPeriodValue.appendTo(container).kendoNumericTextBox({
            spinners: false,
            decimals: 2,
            min: 0,
            format: '#.00'
        });

        inputPeriodValue.on('focus', function () {
            var input = $(this);

            setTimeout(function () {
                input.select();
            });
        });
    },
    EditorDecimalNegativePeriod: function (container, options) {
        //PeriodId
        var inputPeriodId = $('<input type="hidden"/>');
        inputPeriodId.attr('name', options.field + '.PeriodId');
        inputPeriodId.appendTo(container);
        //Value
        var inputPeriodValue = $('<input />');
        inputPeriodValue.attr('name', options.field + '.Value');
        inputPeriodValue.appendTo(container).kendoNumericTextBox({
            spinners: false,
            decimals: 2,
            format: '#.00'
        });

        inputPeriodValue.on('focus', function () {
            var input = $(this);

            setTimeout(function () {
                input.select();
            });
        });
    },
    EditorIntPeriod: function (container, options) {
        //PeriodId
        var inputPeriodId = $('<input type="hidden"/>');
        inputPeriodId.attr('name', options.field + '.PeriodId');
        inputPeriodId.appendTo(container);

        //Value
        var inputPeriodValue = $('<input />');
        inputPeriodValue.attr('name', options.field + '.Value');
        inputPeriodValue.appendTo(container).kendoNumericTextBox({
            spinners: false,
            decimals: 0,
            min: 0
        });

        inputPeriodValue.on('focus', function () {
            var input = $(this);

            setTimeout(function () {
                input.select();
            });
        });
    },
    EditorDecimal: function (container, options, callback, decimals, format, min, max) {
        var inputPeriodValue = $('<input />');
        inputPeriodValue.attr('name', options.field);
        if (options.model.uid) {
            inputPeriodValue.attr('uid', options.model.uid);
        }
        inputPeriodValue.appendTo(container).kendoNumericTextBox({
            spinners: false,
            decimals: decimals || 4,
            min: min || 0,
            format: format || '#.0000'
        });

        if (max != undefined && max != null) {
            inputPeriodValue.data('kendoNumericTextBox').setOptions({ max: max })
        }

        inputPeriodValue.on('focus', function () {
            var input = $(this);

            setTimeout(function () {
                input.select();
            });
        });

        if (callback) {
            var kendoNumeric = inputPeriodValue.data('kendoNumericTextBox');
            if (kendoNumeric) {
                kendoNumeric.bind('change', callback);
            }
        }
    },
    EditorNumeric: function (container, options) {
        var inputPeriodValue = $('<input />');
        inputPeriodValue.attr('name', options.field);
        inputPeriodValue.appendTo(container).kendoNumericTextBox({
            spinners: false,
            decimals: 2,
            min: 0,
            format: '#.00'
        });

        inputPeriodValue.on('focus', function () {
            var input = $(this);

            setTimeout(function () {
                input.select();
            });
        });
    },
    EditorNumericNegative: function (container, options) {
        var inputPeriodValue = $('<input />');
        inputPeriodValue.attr('name', options.field);
        inputPeriodValue.appendTo(container).kendoNumericTextBox({
            spinners: false,
            decimals: 2,
            format: '#.00'
        });

        inputPeriodValue.on('focus', function () {
            var input = $(this);

            setTimeout(function () {
                input.select();
            });
        });
    },
    EditorPercent: function (container, options) {
        var inputPeriodValue = $('<input />');
        inputPeriodValue.attr('name', options.field);
        inputPeriodValue.appendTo(container).kendoNumericTextBox({
            spinners: false,
            decimals: 2,
            format: '#.00',
            min: 0,
            max: 100
        });

        inputPeriodValue.on('focus', function () {
            var input = $(this);

            setTimeout(function () {
                input.select();
            });
        });
    },
    EditorInt: function (container, options) {
        var inputPeriodValue = $('<input />');
        inputPeriodValue.attr('name', options.field);
        inputPeriodValue.appendTo(container).kendoNumericTextBox({
            spinners: false,
            decimals: 0,
            min: 0
        });

        inputPeriodValue.on('focus', function () {
            var input = $(this);

            setTimeout(function () {
                input.select();
            });
        });
    },
    EditorDate: function (container, options) {
        var inputValue = $('<input />');
        inputValue.attr('name', options.field);
        inputValue.appendTo(container).kendoDatePicker({ parseFormats: ['ddMMyyyy', 'ddMMyy', 'dd.MM.yyyy', 'dd.MM.yy', 'dd/MM/yyyy', 'dd/MM/yy'] });
    },
    EditorSectionNo: function (container, options) {
        var inputValue = $('<input type="text" class="k-textbox" style="width:100%;" />');
        inputValue.attr('name', options.field);
        inputValue.appendTo(container).on('keypress', function (e) {
            var regExp = /[\d\.]+/g;
            if (!regExp.test(e.key)) {
                e.preventDefault();
            }
        });
    },
    TemplateDecimalPeriod: function (field) {
        var fieldValue = field + '.Value';
        var template = '#: ' + field + ' && ' + fieldValue + ' ? kendo.toString(' + fieldValue + ', "n2") : ""#';

        return Common.UI.CustomTemplate(fieldValue, template);
    },
    TemplateDecimal: function (field) {
        return '#: ' + field + ' != null ? kendo.toString(' + field + ', "n2") : ""#';
    },
    TemplateSectionNo: function (dataItem) {
        var sectionsShifts = '';
        var sectionsCount = 0;

        if (dataItem.SectionNo) {
            var matches = dataItem.SectionNo.match(new RegExp('\\.\\d+', 'g'));
            if (matches) {
                sectionsCount = matches.length;
            }
        }

        for (var i = 0; i < sectionsCount; i++) {
            sectionsShifts += '\u00A0\u00A0\u00A0\u00A0';
        }

        return kendo.htmlEncode(sectionsShifts + dataItem.SectionNo);
    },
    EditorModal: function (url, title, callback) {
        var window = $('<div></div>');
        var grid = $('<div style="box-sizing: border-box;"></div>')
        var kendoWindow, kendoGrid;

        window.appendTo($('body'));
        grid.appendTo(window);

        kendoGrid = grid.kendoGrid({
            dataSource: new kendo.data.DataSource({
                type: 'aspnetmvc-ajax',
                transport: {
                    read: {
                        url: url,
                        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                        dataType: 'json'
                    }
                },
                schema: {
                    data: 'Data',
                    total: 'Total',
                    model: {
                        id: 'Id',
                        fields: {
                            Code: { type: 'string' },
                            Title: { type: 'string' }
                        }
                    }
                },
                pageSize: 5,
                serverPaging: true,
                serverFiltering: true
            }),
            filterable: {
                mode: 'row'
            },
            toolbar: [
                { name: 'save', text: 'Выбрать' },
                { name: 'cancel', text: 'Отмена' }
            ],
            height: '100%',
            sortable: true,
            selectable: 'row',
            pageable: {
                buttonCount: 5
            },
            columns: [{
                field: 'Id',
                hidden: true
            }, {
                field: 'Code',
                title: 'Код',
                width: 150,
                filterable: {
                    cell: {
                        template: function template(args) {
                            args.element.addClass('k-textbox').width('100%');
                        },
                        operator: 'contains',
                        showOperators: false
                    }
                }
            }, {
                field: 'Title',
                title: 'Наименование',
                filterable: {
                    cell: {
                        template: function template(args) {
                            args.element.addClass('k-textbox').width('100%');
                        },
                        operator: 'contains',
                        showOperators: false
                    }
                }
            }]
        }).data('kendoGrid');

        grid.find('.k-grid-toolbar .k-grid-save-changes').on('click', function (e) {
            e.preventDefault();

            var item = kendoGrid.dataItem(kendoGrid.select());

            if (item != null) {
                callback(item);
            } else {
                Common.UI.ShowDialog({ content: 'Необходимо выбрать строку', icon: 'warning' });
                return;
            }

            kendoWindow.close();

            return false;
        });

        grid.find('.k-grid-toolbar .k-grid-cancel-changes').on('click', function (e) {
            e.preventDefault();
            kendoWindow.close();
            return false;
        });

        kendoWindow = window.kendoWindow({
            width: '800px',
            height: '500px',
            title: title,
            modal: true,
            actions: [
                'Maximize',
                'Close'
            ],
            close: function (e) {
                this.destroy();
            }
        }).data('kendoWindow');

        kendoWindow.center().open();
        kendoGrid.resize();
    },
    ToggleEditMode: function (editMode) {
        $('[editmode="true"]').each(function (i, el) {
            var datePicker = $(el).data('kendoDatePicker');
            if (datePicker) {
                datePicker.readonly(!editMode);
                return;
            }

            var numericTextBox = $(el).data('kendoNumericTextBox');
            if (numericTextBox) {
                numericTextBox.readonly(!editMode);
                return;
            }

            var dropDownList = $(el).data('kendoDropDownList');
            if (dropDownList) {
                dropDownList.readonly(!editMode);
                return;
            }

            var autoComplete = $(el).data('kendoAutoComplete');
            if (autoComplete) {
                autoComplete.readonly(!editMode);
                return;
            }

            var multiSelect = $(el).data('kendoMultiSelect');
            if (multiSelect) {
                multiSelect.readonly(!editMode);
                return;
            }

            if ($(el).hasClass('k-checkbox')) {
                $(el).attr('disabled', !editMode);
                return;
            }

            if ($(el).hasClass('k-textbox')) {
                $(el).attr('readonly', !editMode);
                return;
            }

            if ($(el).hasClass('k-button')) {
                KendoExtension.ToggleButtonDisable(el, !editMode);
                return;
            }
        });
    },
    ToggleButtonDisable: function (el, disable) {
        if (disable === true) {
            $(el).css('pointer-events', 'none');
            $(el).attr('disabled', 'disabled');
            $(el).addClass('k-state-disabled');
        } else {
            $(el).css('pointer-events', 'auto');
            $(el).removeAttr('disabled');
            $(el).removeClass('k-state-disabled');
        }
    },
    ExportGridWithTemplatesContent: function (e) {
        var data = e.data;
        var gridColumns = e.sender.columns;
        var sheet = e.workbook.sheets[0];
        var visibleGridColumns = [];
        var columnTemplates = [];
        var footerTemplates = [];
        var dataItem;
        var elem = document.createElement('div');

        for (var i = 0; i < gridColumns.length; i++) {
            if (!gridColumns[i].hidden && !gridColumns[i].selectable) {
                if (gridColumns[i].headerTemplate) {
                    if (!gridColumns[i].headerTemplate.includes('input type="checkbox"')) {
                        visibleGridColumns.push(gridColumns[i]);
                    }
                } else {
                    visibleGridColumns.push(gridColumns[i]);
                }
            }
        }

        for (var i = 0; i < visibleGridColumns.length; i++) {
            if (visibleGridColumns[i].template) {
                columnTemplates.push({ cellIndex: i, cellName: visibleGridColumns[i].field, template: kendo.template(visibleGridColumns[i].template), format: visibleGridColumns[i].template });
            }
        }

        for (var i = 0; i < visibleGridColumns.length; i++) {
            if (visibleGridColumns[i].footerTemplate) {
                footerTemplates.push({ cellIndex: i, cellName: visibleGridColumns[i].field, template: kendo.template(visibleGridColumns[i].footerTemplate), format: visibleGridColumns[i].template });
            }
        }

        for (var i = 0; i < sheet.columns.length; ++i) {
            delete sheet.columns[i].width;
            sheet.columns[i].autoWidth = true;
            sheet.rows[0].cells[i].wrap = false;
            sheet.rows[0].cells[i].verticalAlign = "bottom";
            sheet.rows[0].cells[i].textAlign = "center";
        }
        for (var i = 1; i < sheet.rows.length; i++) {
            var row = sheet.rows[i];

            if (row.type == 'footer') {
                for (var j = 0; j < footerTemplates.length; j++) {
                    var footerTemplate = footerTemplates[j];
                    var dataItem = e.sender.dataSource.aggregates()[footerTemplate.cellName];

                    if (dataItem) {
                        elem.innerHTML = footerTemplate.template(dataItem);
                    } else {
                        elem.innerHTML = footerTemplate.template(1);
                    }
                    if (row.cells[footerTemplate.cellIndex] != undefined) {
                        row.cells[footerTemplate.cellIndex].wrap = true;
                        if (footerTemplate.format.includes('kendo.toString(')) {
                            if (footerTemplate.format.includes('n0') && dataItem) {
                                row.cells[footerTemplate.cellIndex].format = '# ##0';
                                row.cells[footerTemplate.cellIndex].value = dataItem['sum'] || dataItem['average'] || 0;
                            } else if (footerTemplate.format.includes('n2') && dataItem) {
                                row.cells[footerTemplate.cellIndex].format = '# ##0.00';
                                row.cells[footerTemplate.cellIndex].value = dataItem['sum'] || dataItem['average'] || 0;
                            } else {
                                row.cells[footerTemplate.cellIndex].value = elem.textContent || elem.innerText || "";
                            }
                        } else {
                            row.cells[footerTemplate.cellIndex].value = elem.textContent || elem.innerText || "";
                        }
                    }
                }
            } else {
                var dataItem = data[i - 1];

                if (dataItem) {
                    for (var j = 0; j < columnTemplates.length; j++) {
                        var columnTemplate = columnTemplates[j];

                        elem.innerHTML = columnTemplate.template(dataItem);

                        if (row.cells[columnTemplate.cellIndex] != undefined) {
                            row.cells[columnTemplate.cellIndex].wrap = true;
                            if (columnTemplate.format.includes('kendo.toString(')) {
                                if (columnTemplate.format.includes('n0')) {
                                    row.cells[columnTemplate.cellIndex].format = '# ##0';
                                    row.cells[columnTemplate.cellIndex].value = dataItem[columnTemplate.cellName] || 0;
                                } else if (columnTemplate.format.includes('n2')) {
                                    row.cells[columnTemplate.cellIndex].format = '# ##0.00';
                                    row.cells[columnTemplate.cellIndex].value = dataItem[columnTemplate.cellName] || 0;
                                } else {
                                    row.cells[columnTemplate.cellIndex].value = elem.textContent || elem.innerText || "";
                                }
                            } else {
                                row.cells[columnTemplate.cellIndex].value = elem.textContent || elem.innerText || "";
                            }
                        }
                    }
                }
            }
        }
    },
    CapitalizeFirstCharInWord: function(e) {
        if (e.keyCode === 32) {
            var currentValue = this.value,
                currentPosition = this.selectionStart,
                wordFirstCharPosition = currentValue.lastIndexOf(' ', currentPosition - 2) + 1;

            this.value = currentValue.substr(0, wordFirstCharPosition) + currentValue.charAt(wordFirstCharPosition).toUpperCase() + currentValue.substr(wordFirstCharPosition + 1, currentValue.length);
        }
    }
};

var redirectPage = function (url, newTab) {
    newTab === true ? window.open(url, '_blank') : window.location = url;
}

var refreshView = function (url, params, container, requestType, successfunc) {
    if (url == undefined || url == "" || params == null) return;

    if (requestType == undefined || requestType == "")
        requestType = "POST";

    //var content = $(container).html();
    //$(container).html('<div style="position: relative;">' + content + '<div class="k-loading-image" background-color: #00000f;></div>');
    //$(container).empty().append('<div class="k-loading-image"></div>');
    //$(container).html('<div style="position: relative;"><div class="k-loading-image" background-color: #0000ff;></div > ');

    $(container).append('<div class="k-loading-image"></div>');

    $.ajax({
        type: requestType,
        url: url,
        data: params,
        success: function (response) {
            $(container).empty().append(response);
            if (successfunc != undefined) {
                successfunc();
            }
        },
        error: function (request, error) {
            if (window.console) {
                window.console.log("RefreshView -> ");
            }
        }
    });
}

// Глобальные переменные Get
var $_GET = {};
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
        $_GET[aux[0]] = aux[1];
    }
}

// реализация функции string.format
// пример '{0} {1} {2}'.format(3.14, 'fg', 'foo');
// результат '3.14 fg foo'
String.prototype.format = function () {
    var args = arguments;
    return this.replace(/\{\{|\}\}|\{(\d+)\}/g, function (m, n) {
        if (m == "{{") { return "{"; }
        if (m == "}}") { return "}"; }
        return args[n];
    });
};

Date.prototype.truncateTime = function () {
    this.setHours(0, 0, 0, 0);
    return this;
};

Array.prototype.remove = function (x) {
    var i = this.indexOf(x);
    if (i === -1) return;
    this.splice(i, 1);
}

$(function () {
	var notificationPanel = $('#notificationPanel').data('kendoNotification');

	if (notificationPanel) {
		notificationPanel.setOptions({
			position: {
				pinned: true
			},
			stacking: 'down',
			show: function (e) {
				if (e.sender.getNotifications().length == 1) {
					var element = e.element.parent(),
						eWidth = element.width(),
						eHeight = element.height(),
						wWidth = $(window).width(),
						wHeight = $(window).height(),
						newTop, newLeft;

					newLeft = Math.floor(wWidth / 2 - eWidth / 2);
					newTop = Math.floor(10);

					e.element.parent().css({ top: newTop, left: newLeft });
				}
			}
		});
	}
});