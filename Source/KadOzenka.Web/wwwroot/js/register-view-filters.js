(function ($) {
    function registerViewSearch(el, config) {
        this.element = el;
        this.$element = $(el);

        this.options = config;
        this.init(config);
    }

    registerViewSearch.prototype.init = function (config) {
        var self = this;
        self.baseUrl = config.baseUrl ? config.baseUrl : '/';
        self.saveFilterUrl = self.baseUrl + "CoreUi/SaveSearchFilter";
        self.getFilterUrl = self.baseUrl + "CoreUi/GetSearchFilter";
        self.getDefaultSearchFilterUrl = self.baseUrl + "CoreUi/GetDefaultSearchFilter";
        self.registerViewId = config.registerViewId;
        self.registerId = config.registerId;
        self.attributesUrl = self.baseUrl + "CoreUi/GetAttributes";
        self.referencesUrl = self.baseUrl + "CoreUi/GetReference";
        self.needOpenEmpty = config.needOpenEmpty ? config.needOpenEmpty : false;
        self.registerSettings = config.registerSettings;
        self.filter = config.filter;
        self.AddMenuTemplate = "#rvs-add-menu-template";
        self.ButtonTemplate = "#rvs-button-template";
        self.IntegerControlTemplate = "#rvs-number-integer-control-template";
        self.DecimalControlTemplate = "#rvs-number-decimal-control-template";
        self.DateControlTemplate = "#rvs-date-control-template";
        self.BooleanControlTemplate = "#rvs-boolean-control-template";
        self.StringControlTemplate = "#rvs-string-control-template";
        self.ReferenceControlTemplate = "#rvs-reference-control-template";
        self.ReferenceCheckboxTemplate = "#rvs-reference-checkbox-template";
        self.ButtonsTemplate = "#rvs-control-buttons-template";
        self.ButtonText = '.rvs-control-button-text';
        self.ButtonTextValue = '.rvs-control-button-text-value';
        self.MainContentSelector = ".mainContent",
            self.GridSelector = "#Grid-" + config.registerId,
            self.SplitterSelector = "#verticalSplitter-" + config.registerId,
            self.GridToolBar = '#GridToolBar-' + config.registerId;
        self.SearchButton = '#gearButton-' + config.registerId;
        self.ClearButton = '#clearButton-' + config.registerId;
        self.ResetSearchButton = '#resetButton-' + config.registerId;
        self.idNumerator = config.idNumerator ? config.idNumerator : 1;
        self.initDefault = false;

        self.helpStringItems = {
            Equal: 'Искомое значение равно введенному пользователем в критерии поиска.',
            NotEqual: 'Искомое значение НЕ равно введенному пользователем в критерии поиска.',
            BeginFromNonCaseSensitive: 'Искомое значение начинается с введенного пользователем в критерии поиска.',
            EndTo: 'Искомое значение заканчивается на введенное пользователем в критерии поиска.',
            ContainsNonCaseSensitive: 'В результатах поиска будут выданы записи, содержащие введенный в критерии поиска пользователем слово или фразу.',
            NotContainsNonCaseSensitive: 'В результатах поиска будут выданы записи, НЕ содержащие введенный в критерии поиска пользователем слово или фразу.',
            Like: 'В результатах поиска будут выданы записи, содержащие введенный в критерии поиска пользователем текст по частям. Пример синтаксиса для поиска по частям адреса: <strong>"*Центральная*2"</strong>.<br/><br/>Результаты:<br/>1) Москва, г Московский, ул Центральная, Дом 2<br/>2) Москва, г Троицк, ул Центральная, Дом 12а',
            NotLike: 'В результатах поиска будут выданы записи, НЕ содержащие введенный в критерии поиска пользователем текст по частям. Пример синтаксиса для поиска по частям адреса: <strong>"*Центральная*2".</strong>',
            IsNull: 'В результаты поиска попадают записи с пустым значением в критерии поиска',
            IsNotNull: 'В результаты поиска попадают все непустые значения'
        };

        self.$element.addClass('rvs-main-content');
        getAttributes();

        var methods = {
            initAddMenu: function (el) {
                var $ddl = $(el).kendoDropDownTree({
                    dataSource: self.attributes,
                    autoWidth: true,
                    placeholder: "Фильтр",
                    dataTextField: "text",
                    dataValueField: "value",
                    noDataTemplate: "Ничего не найдено!",
                    filter: "contains",
                    ignoreCase: true,
                    select: function (e) {
                        var item = this.dataItem(e.node);
                        if (!item.parentId) {
                            e.preventDefault();
                            return;
                        }

                        item.set("enabled", false);
                        AddControll(item);
                    },
                    change: function (e) {
                        e.preventDefault();
                        e.sender.value('');
                    },
                    open: function (e) {
                        var activeControl = $('.rvs-control-button.active');

                        if (activeControl.length > 0) {
                            activeControl.removeClass('active');
                            var dialog = activeControl.data('dialog');
                            if (dialog)
                                dialog.close();
                        }
                    }
                }).data("kendoDropDownTree");
            },
            initDateTimePicker: function (el) {
                $(el).kendoDateTimePicker({
                    format: "dd.MM.yyyy HH:mm:ss",
                    parseFormats: ['ddMMyyyy', 'ddMMyy', 'dd.MM.yyyy', 'dd.MM.yy', 'dd/MM/yyyy', 'dd/MM/yy'],
                    change: function (e) {
                        var radio = e.sender.element.closest('.form-group').find('input[type=radio]');
                        if (radio.length && radio.prop('checked') == false)
                            radio.prop('checked', true);
                    }
                });
            },
            initDatePicker: function (el) {
                $(el).kendoDatePicker({
                    format: "dd.MM.yyyy",
                    parseFormats: ['ddMMyyyy', 'ddMMyy', 'dd.MM.yyyy', 'dd.MM.yy', 'dd/MM/yyyy', 'dd/MM/yy'],
                    change: function (e) {
                        var radio = e.sender.element.closest('.form-group').find('input[type=radio]');
                        if (radio.length && radio.prop('checked') == false)
                            radio.prop('checked', true);
                    }
                });
            },
            initButtonGroup: function (el) {
                $(el).kendoButtonGroup({
                    index: 0
                });
            },
            initCheckBox: function (el) {
                $(el).after('<label for="' + el.substring(1) + '" class="k-checkbox-label k-no-text"></label>')
            },
            initRadio: function (el) {
                $(el).after('<label for="' + el.substring(1) + '"class="k-radio-label" style="margin-top: 9px;" />')
            },
            initDropDownList: function (el, config) {
                config = config ? config : {};
                if (el) {
                    $(el).kendoDropDownList({
                        filter: config.filter ? config.filter : "none",
                        dataValueField: config.dataValueField ? config.dataValueField : "id",
                        dataTextField: config.dataTextField ? config.dataTextField : "text",
                        dataSource: config.dataSource ? config.dataSource : undefined,
                        noDataTemplate: "Ничего не найдено!",
                        change: function (e) {
                            if (config.change)
                                config.change(e);
                        },
                        select: function (e) {
                            if (config.select)
                                config.select(e);
                        }
                    }).data("kendoDropDownList");
                }
            },
            initDropDownConditionList: function (el, config) {
                methods.initDropDownList(el, {
                    change: function (e) {
                        var value = e.sender.value();
                        var $helpText = self.helpStringItems[value];

                        var $input = e.sender.element.closest('.rvs-control-data').find('input.rvs-value');
                        if ($input.length)
                            $input.attr('disabled', value == 'IsNull' || value == 'IsNotNull');

                        var $help = e.sender.element.closest('.rvs-control-data').find('span.k-icon.k-i-question');
                        if ($help.length)
                            $help.attr('title', $helpText);
                    }
                });
            },
            initDropDownFunctionList: function (el, config) {
                methods.initDropDownList(el, {
                    dataSource: [
                        { id: 'CurrentDate', text: 'Текущая дата' },
                        { id: 'BeginMonth', text: 'Начало месяца' },
                        { id: 'EndMonth', text: 'Конец месяца' },
                        { id: 'BeginQuarter', text: 'Начало квартала' },
                        { id: 'EndQuarter', text: 'Конец квартала' }
                    ],
                    change: function (e) {
                        var radio = e.sender.element.closest('.form-group').find('input[type=radio]');
                        if (radio.length && radio.prop('checked') == false)
                            radio.prop('checked', true);
                    }
                });
            },
            initTooltip: function (el) {
                $(el).kendoTooltip({
                    filter: "span.k-icon.k-i-question",
                    show: function (e) {
                        if (this.content.text() != this.target().data("title")) {
                            this.refresh();
                        }
                    },
                    content: function (e) {
                        var text = $(e.target).data("title") ? $(e.target).data("title") : '';
                        return '<div style="width: ' + text.length * .6 + 'em; max-width: 30em;text-align: left;">' + text + '</div>';
                    },
                    position: "right",
                    autoHide: true,
                    showAfter: 400
                });
            },
            initTabStrip: function (el) {
                $(el).kendoTabStrip({
                    animation: {
                        open: {
                            effects: "fadeIn"
                        }
                    }
                }).data("kendoTabStrip");
            },
        };

        /* События */

        self.$element.on("click", ".rvs-btn-delete", function (e) {
            e.preventDefault();
            var $button = $(this).closest('.rvs-control-button');
            var dialog = $button.data('dialog');

            if (dialog)
                dialog.close();

            var item = $button.data("control");

            if (item)
                item.set("enabled", true);

            $button.remove();

            //if ($('.rvs-main-content .rvs-control-button').length == 0)
            //    $('.k-widget.rvs-add-menu').find('.k-dropdown-wrap .k-input.k-readonly').text('Фильтр');

            SaveFilter();
        });

        $(self.MainContentSelector).on('input', 'input.only-int', function () {
            this.value = this.value.replace(/[^0-9]/g, '');
        }); //

        $(self.MainContentSelector).on('input', 'input.only-float', function () {
            this.value = this.value.replace(/[^0-9,]/g, '');
            this.value = this.value.replace(/(\,.*)\,/g, '$1');
        });

        self.$element.on('click', '.rvs-control-button', function () {
            var $this = $(this);

            var controls = $('.rvs-control-button.active').not($this);

            if (controls.length) {
                controls.removeClass('active');
                var dialog = controls.data('dialog');
                if (dialog)
                    dialog.close();
            }

            $this.toggleClass('active');

            if ($this.hasClass('active'))
                ShowControll($this);
            else {
                var dialog = $this.data('dialog');
                if (dialog)
                    dialog.close();
            }
        });

        $(self.MainContentSelector).on('click', '.rvs-btn-search', function () {
            var $this = $(this);

            // обнуляем счетчик загрузок данных
            if (self.registerSettings && self.registerSettings.UseDataReaderMode) {
                $(self.GridSelector).data('kendoGrid').content.scrollTop(0);
                self.registerSettings.ContentLoadCounter = 0;
            }

            if (self.needOpenEmpty) {
                $(self.SplitterSelector).show();
                $(self.GridSelector).show();
                $("#MessageClickSearch").hide();
                self.needOpenEmpty = false;

                var grid = $(self.GridSelector).data('kendoGrid');
                if (grid)
                    grid.dataSource.unbind("requestStart");
            }

            var dialog = $this.closest('.k-window-content').data('kendoWindow');

            if (dialog) {
                if (dialog.element && dialog.element.data('gridSelector') == self.GridSelector) {
                    SaveControlValue(dialog.element);
                    reloadGrid();
                    dialog.close();
                }
            }
        });

        $(self.MainContentSelector).on('click', '.rvs-btn-ok', function () {
            var $this = $(this);
            var dialog = $this.closest('.k-window-content').data('kendoWindow');

            if (dialog) {
                SaveControlValue(dialog.element);
                dialog.close();
            }
        });

        $(self.MainContentSelector).on('click', '.rvs-btn-clear', function () {
            var $this = $(this);
            var $window = $this.closest('.k-window-content');
            ClearValues($window);
        });

        $(self.MainContentSelector).on('input', 'input.rvs-input-filter', function () {
            var radio = $(this).closest('.form-group').find('input[type=radio]');
            if (radio.length && radio.prop('checked') == false)
                radio.prop('checked', true);
        });

        $(self.GridToolBar).on('click', self.SearchButton, function () {
            if (self.registerSettings && self.registerSettings.ShowSearchPanelButton)
                return;

            if (self.needOpenEmpty) {
                $(self.SplitterSelector).show();
                $(self.GridSelector).show();
                $("#MessageClickSearch").hide();
                self.needOpenEmpty = false;

                var grid = $(self.GridSelector).data('kendoGrid');
                if (grid)
                    grid.dataSource.unbind("requestStart");
            }

            reloadGrid();
        });

        $(self.SearchButton + '_optionlist').on('click', self.ClearButton, function () {
            var controls = $('.rvs-control-button');

            if (controls.length > 0) {
                var $activeControl = controls.filter('.active');
                if ($activeControl.length > 0) {
                    var dialog = $activeControl.data('dialog');
                    if (dialog)
                        dialog.close();
                }

                controls.each(function () {
                    $this = $(this);
                    var item = $this.data("control");
                    if (item) {
                        SetEmptyValue($this, item);
                        item.set("enabled", true);
                    }
                });

                controls.remove();

                if (self.getDefaultSearchFilterUrl) {
                    $.ajax({
                        url: self.getDefaultSearchFilterUrl,
                        type: 'GET',
                        data: { registerViewId: self.registerViewId },
                        success: function (data) {
                            if (data) {
                                self.initDefault = true;
                                self.filter = data;
                                InitFilter(function () {
                                    SaveFilter();

                                    var grid = $(self.GridSelector).data('kendoGrid');
                                    if (grid)
                                        grid.dataSource.read();
                                });
                            }
                        },
                        error: function (request) {
                            log('ошибка сохранения фильтра, url: ' + self.saveFilterUrl + ' ' + request.status + ' ' + request.statusText, true);
                        }
                    });
                } else {
                    SaveFilter();

                    var grid = $(self.GridSelector).data('kendoGrid');
                    if (grid)
                        grid.dataSource.read();
                }
            }
        });

        /* end */

        function reloadGrid() {
            var grid = $(self.GridSelector).data('kendoGrid');

            if (grid)
                grid.dataSource.read();
        }

        function SaveFilter() {
            if (self.saveFilterUrl) {
                var $filter = self.getStruct();

                $.ajax({
                    url: self.saveFilterUrl,
                    type: 'POST',
                    data: { registerViewId: self.registerViewId, filter: $filter.length > 0 ? JSON.stringify($filter) : '' },
                    success: function () {
                        self.initDefault = false;
                    },
                    error: function (request) {
                        log('ошибка сохранения фильтра, url: ' + self.saveFilterUrl + ' ' + request.status + ' ' + request.statusText, true);
                    }
                });
            }
        }

        function SaveControlValue($window) {
            var target = $window.data('target');
            var data = target.data('control');

            if (target) {
                target.removeClass('active');
                var $value = GetValue($window, data);
                target.data('value', $value);
                target.find(self.ButtonText).text($value.text);
                target.find(self.ButtonTextValue).text($value.textValue);
            }

            SaveFilter();
        }

        function ClearValues($window) {
            var target = $window.data('target');
            var data = target.data('control');

            SetEmptyValue(target, data);
            if (data.referenceId && !data.customSearchUrl) {
                var $refEl = $("input.rvs-ddl-reference");
                var ddlReference = $refEl.data("role") === "multiselect"
                    ? $refEl.data("kendoMultiSelect")
                    : $refEl.data("rvsListBox");

                if (ddlReference)
                    ddlReference.value([]);
            }
            else if (data.customSearchUrl && typeof csc_clearValue == typeof Function) {
                csc_clearValue();
            }
            else {
                var $elements = $window.find('input[id^=rvs-], input.rvs-value, input.rvs-input-filter');
                if ($elements.length) {
                    $elements.each(function () {
                        SetElementValue($(this));
                    });
                    $elements.filter('.k-radio.rvs-single').prop('checked', true);

                    var $tabStrip = $('.rvs-tab-strip').data('kendoTabStrip');
                    if ($tabStrip)
                        $tabStrip.select(0);
                }
            }
        }

        function InitFilter(callback) {
            var getObjById = function (list, id) {
                return list.filter(function (item) {
                    return item.id == id;
                })[0];
            };

            var initControls = function (struct) {
                var attributes = $(self.GridToolBar + ' input.rvs-add-menu').data('kendoDropDownTree');

                $.each(struct,
                    function (index, value) {
                        if (self.attributes && self.attributes.length > 0 && value) {
                            if (attributes) {
                                var dataItem = attributes.dataSource.get(value.id);
                                if (dataItem) {
                                    dataItem.set("enabled", false);
                                    AddControll(dataItem, value);
                                }
                            }
                        }
                    });

                if (callback)
                    callback();

                self.filter = '';
            };

            if (self.filter) {
                var struct = $.parseJSON(decodeURIComponent(self.filter.replace(/\+/g, '%20')));

                if (struct.length) {
                    if (!self.initDefault && self.getDefaultSearchFilterUrl) {
                        $.ajax({
                            url: self.getDefaultSearchFilterUrl,
                            type: 'GET',
                            data: { registerViewId: self.registerViewId },
                            success: function (data) {
                                if (data) {
                                    var defaultFilters = $.parseJSON(data);
                                    if (defaultFilters.length) {
                                        for (var ind in struct) {
                                            var obj = getObjById(defaultFilters, struct[ind].id);
                                            if (obj) {
                                                struct[ind].allowDelete = obj.allowDelete !== undefined ? obj.allowDelete : true;
                                                struct[ind].customSearchUrl = obj.customSearchUrl;
                                                struct[ind].enable = obj.enable;
                                            }
                                        }
                                    }
                                }
                                initControls(struct);
                            }
                        });
                    } else
                        initControls(struct);
                }
            }
        }

        function AddControll(data, value) {
            if (!$.isEmptyObject(data)) {
                data.customSearchUrl = value ? value.customSearchUrl : undefined;
                data.enable = value ? value.enable : undefined;

                var $button = $(Mustache.render($(self.ButtonTemplate).html(), data));
                $button.data("control", data);

                if (!data.enable) {
                    $button.find(self.ButtonText).css('cursor', 'default');
                    $button.find('.icon-arrow_down').css('cursor', 'default');
                    $button.addClass('disable');
                }

                self.$element.append($button);

                if (value) {
                    if (value.allowDelete === undefined)
                        value.allowDelete = true;

                    if (!value.allowDelete) {
                        $button.find('a.rvs-btn-delete').remove();
                        $button.append('<span>&nbsp;&nbsp;</span>');
                    }

                    var indСolon = value.text.search(':');
                    if (indСolon > -1 && !value.textValue) {
                        value.textValue = value.text.substring(indСolon + 2);
                        value.text = data.text;
                    }

                    $button.data("value", value);
                    $button.find(self.ButtonText).text(value.text);

                    $button.find(self.ButtonTextValue).text(value.textValue);
                }
                else {
                    SetEmptyValue($button, data);
                    SaveFilter();
                }
            }
        }

        function ShowControll(el) {
            if (el) {
                var data = el.data('control');

                if (data) {
                    if (data.enable == false)
                        return;

                    var $controlTemplate = $('<div style="padding: 15px"></div>');

                    var coords = getCoords(el);

                    var dialog = $controlTemplate.kendoWindow({
                        title: data.text,
                        draggable: false,
                        visible: false,
                        resizable: false,
                        appendTo: $('.mainContent'),
                        close: function (e) {
                            this.destroy();
                        },
                        position: {
                            top: coords.top + 33,
                            left: coords.left + 12
                        }
                    }).data('kendoWindow');

                    var type = data.type;
                    if (data.referenceId)
                        type = 'REFERENCE';
                    if (data.customSearchUrl)
                        type = 'CUSTOM';

                    switch (type) {
                        case 'INTEGER':
                            RenderTemplate(self.IntegerControlTemplate, $controlTemplate);
                            break;
                        case 'DECIMAL':
                            RenderTemplate(self.DecimalControlTemplate, $controlTemplate);
                            break;
                        case 'BOOLEAN':
                            RenderTemplate(self.BooleanControlTemplate, $controlTemplate);
                            break;
                        case 'STRING':
                            RenderTemplate(self.StringControlTemplate, $controlTemplate);
                            break;
                        case 'DATE':
                            RenderTemplate(self.DateControlTemplate, $controlTemplate);
                            break;
                        case 'REFERENCE':
                            var controlValue = el.data('value');
                            RenderReferences(data.referenceId, $controlTemplate, controlValue ? controlValue.value : undefined);
                            break;
                        case 'CUSTOM':
                            RenderCustomControl($controlTemplate, el.data('value'));
                            break;
                    }

                    if (type !== 'REFERENCE' && type !== 'CUSTOM') {
                        var controlData = $controlTemplate.find('.rvs-control-data');
                        dialog.setOptions({
                            width: controlData.data('width') === undefined ? "300px" : controlData.data('width') + "px",
                            height: controlData.data('height') === undefined ? "200px" : controlData.data('heigh') + "px",
                        });

                        positionDialog(controlData, dialog);
                    }

                    dialog.element.data('target', el);
                    dialog.element.data('gridSelector', self.GridSelector);
                    el.data('dialog', dialog);

                    SetValue(el, $controlTemplate);

                    dialog.open();
                }
            }
        }

        function RenderCustomControl($container, controlValue) {
            if (controlValue && !$.isEmptyObject(controlValue) && controlValue.customSearchUrl) {
                kendo.ui.progress($container, true);
                $.ajax({
                    url: controlValue.customSearchUrl,
                    type: 'GET',
                    success: function (data) {
                        if (data) {
                            $container.html(data);
                            $container.find('.rvs-control-data').append($(self.ButtonsTemplate).html());

                            if (typeof csc_initSearchControl == typeof Function)
                                csc_initSearchControl(controlValue.referenceId);

                            kendo.ui.progress($container, false);

                            if (controlValue && !$.isEmptyObject(controlValue)) {
                                if (controlValue.value && typeof csc_setSearchControlValue == typeof Function)
                                    csc_setSearchControlValue(controlValue.value);
                                if (controlValue.condition && typeof csc_setSearchControlConditionValue == typeof Function)
                                    csc_setSearchControlConditionValue(controlValue.condition);
                            }

                            var activeControl = $('.rvs-control-button.active');
                            if (activeControl.length) {
                                var dialog = activeControl.data('dialog');
                                if (dialog) {
                                    var controlData = $container.find('.rvs-control-data');

                                    dialog.setOptions({
                                        width: controlData.data('width') === undefined ? "300px" : controlData.data('width') + "px",
                                        height: controlData.data('height') === undefined ? "200px" : controlData.data('heigh') + "px",
                                    });

                                    positionDialog(controlData, dialog);
                                }
                            }
                        }
                    },
                    error: function (request) {
                        log('ошибка загрузки представления, url: ' +
                            controlValue.customSearchUrl + ' ' +
                            request.status + ' ' +
                            request.statusText, true);
                    }
                });
            }
        }

        function positionDialog(windowControl, dialog) {
            var dx = windowControl.data('width') - ($(self.MainContentSelector).width() - dialog.options.position.left);
            if (dx > 0) {
                dialog.setOptions({
                    position: {
                        left: dialog.options.position.left - dx - 22
                    }
                });
            }
        }

        function SetValue(el, control) {
            if (el.length > 0 && control.length > 0) {
                var valueControl = el.data('value');

                if (valueControl) {
                    switch (valueControl.type) {
                        case 'INTEGER':
                        case 'DECIMAL':
                        case 'DATE':
                            var $el = control.find('input[data-type=' + valueControl.typeControl + ']');

                            if (valueControl.typeControl.includes("function")) {
                                control.find('.rvs-tab-strip').data("kendoTabStrip").select(1);
                            }

                            if ($el.length) {
                                $el.prop("checked", true);
                                var $content = $el.closest('.form-group');

                                if ($content.length) {
                                    if (valueControl.typeControl == 'value' || valueControl.typeControl == 'function-value' ||
                                        valueControl.typeControl == 'day' || valueControl.typeControl == 'function-day') {
                                        SetElementValue($content.find('input.rvs-value'), valueControl.value);
                                    }
                                    else if (valueControl.typeControl == 'range' || valueControl.typeControl == 'function-range') {
                                        SetElementValue($content.find('input.rvs-from'), valueControl.from);
                                        SetElementValue($content.find('input.rvs-to'), valueControl.to);
                                    }
                                }
                            }
                            break;
                        case 'BOOLEAN':
                            var $el = control.find('.rvs-bool').data('kendoButtonGroup');
                            if ($el)
                                $el.select(valueControl.value - 1);
                            break;
                        case 'STRING':
                            SetElementValue($('select.rvs-condition'), valueControl.condition);
                            var $input = control.find('input.rvs-value');

                            if ($input.length) {
                                $input.val(valueControl.value);
                                $input.attr('disabled', valueControl.condition == 'IsNull' || valueControl.condition == 'IsNotNull');
                            }
                            break;
                    }
                }
            }
        }

        function SetEmptyValue(el, data) {
            if (el.length > 0 && data) {
                var type = data.type;
                if (data.referenceId)
                    type = 'REFERENCE';

                var result = {
                    typeControl: 'value',
                    text: data.text,
                    type: type,
                    id: data.id
                };

                el.data('value', result);
            }
        }

        function GetValue(control, data) {
            var result = { allowDelete: true, enable: data.enable };

            if (control.length > 0 && data) {
                var type = data.type;
                if (data.referenceId)
                    type = 'REFERENCE';
                if (data.customSearchUrl)
                    type = 'CUSTOM';

                var $text = data.text;
                var $textValue = '';

                switch (type) {
                    case 'INTEGER':
                    case 'DECIMAL':
                    case 'DATE':
                        var $el = control.find('input[type=radio]:checked');
                        if ($el.length) {
                            var $content = $el.closest('.form-group');
                            var $typeControl = $el.data('type');

                            var $value = GetElementValue($content.find('input.rvs-value'));
                            var $from = GetElementValue($content.find('input.rvs-from'));
                            var $to = GetElementValue($content.find('input.rvs-to'));

                            if ($typeControl === 'value' && $value) {
                                $textValue += $value;
                            }
                            else if ($typeControl === 'function-value') {
                                var $valueText = GetElementValue($content.find('input.rvs-value'), 'text');
                                if ($valueText) {
                                    $textValue += '[{0}]'.format($valueText);
                                }
                            }
                            else if ($typeControl === 'range') {
                                if ($from && $to) {
                                    $textValue += 'c {0} до {1}'.format($from, $to);
                                }
                                else if ($from) {
                                    $text += ': ';
                                    $textValue += 'Больше или равно ' + $from;
                                }
                                else if ($to) {
                                    $textValue += 'Меньше или равно ' + $to;
                                }
                            }
                            else if ($typeControl === 'function-range') {
                                var $fromText = GetElementValue($content.find('input.rvs-from'), 'text');
                                var $toText = GetElementValue($content.find('input.rvs-to'), 'text');

                                if ($fromText && $toText) {
                                    $textValue += 'c [{0}] до [{1}]'.format($fromText, $toText);
                                }
                                else if ($fromText) {
                                    $textValue += 'Больше или равно [{0}]'.format($fromText);
                                }
                                else if ($toText) {
                                    $textValue += 'Меньше или равно [{0}]'.format($toText);
                                }
                            }
                            else if ($typeControl === 'day') { $text += ': В течение ' + $value; }
                            else if ($typeControl === 'function-day') {
                                {
                                    var $valueText = GetElementValue($content.find('input.rvs-value'), 'text');
                                    $textValue += 'В течение [{0}]'.format($valueText);
                                }
                            }
                            else if ($typeControl === 'IsNull') {
                                $textValue += 'Пусто';
                            }
                            else if ($typeControl === 'IsNotNull') {
                                $textValue += 'Не пусто';
                            }

                            result = {
                                typeControl: $typeControl,
                                text: $text,
                                textValue: $textValue,
                                type: type,
                                value: $value,
                                from: $from,
                                to: $to,
                                enable: data.enable,
                                id: data.id
                            };

                            if ($typeControl === 'IsNull')
                                result.condition = 'IsNull';
                            else if ($typeControl === 'IsNotNull')
                                result.condition = 'IsNotNull';
                        }
                        break;
                    case 'BOOLEAN':
                        var $el = control.find('.rvs-bool').data('kendoButtonGroup');
                        if ($el) {
                            $textValue = $el.current().index() === 0 ? 'Да' : 'Нет';

                            result = {
                                typeControl: 'value',
                                type: type,
                                text: $text,
                                textValue: $textValue,
                                value: $el.current().index() + 1,
                                enable: data.enable,
                                id: data.id
                            };
                        }
                        break;
                    case 'STRING':
                        var $conditionDdl = $('select.rvs-condition').data("kendoDropDownList");

                        if ($conditionDdl) {
                            var val = GetElementValue(control.find('input.rvs-value'));
                            if (val) {
                                $textValue = $conditionDdl.text() + ' ' + val;
                            }

                            result = {
                                condition: $conditionDdl.value(),
                                typeControl: 'value',
                                type: type,
                                text: $text,
                                textValue: $textValue,
                                value: val,
                                enable: data.enable,
                                id: data.id
                            };
                        }
                        break;
                    case 'REFERENCE':
                        var $referenceList;
                        var $refEl = control.find("input.rvs-ddl-reference");
                        if ($refEl.data("role") == "multiselect")
                            $referenceList = control.find("input.rvs-ddl-reference").data("kendoMultiSelect");
                        else if ($refEl.data("role") == "rvslistbox")
                            $referenceList = control.find("input.rvs-ddl-reference").data("rvsListBox");

                        if ($referenceList) {
                            var val = $referenceList.value();

                            if (!$.isEmptyObject(val)) {
                                var names;
                                if ($refEl.data("role") == "multiselect") {
                                    names = $.map($referenceList.dataItems(), function (item) {
                                        return item.text;
                                    });
                                }
                                else if ($refEl.data("role") == "rvslistbox") {
                                    names = $('input.rvs-ddl.rvs-ddl-reference').data('rvsListBox').list
                                        .find('input[type=checkbox]').filter(function () {
                                            return this.checked == true;
                                        }).map(function () {
                                            return this.name;
                                        }).get();
                                }

                                $textValue = names.join(', ');
                            }

                            result = {
                                typeControl: 'value',
                                type: type,
                                text: $text,
                                textValue: $textValue,
                                value: $referenceList.value(),
                                referenceId: data.referenceId,
                                enable: data.enable,
                                id: data.id
                            };
                        }
                        break;
                    case 'CUSTOM':
                        if (typeof csc_getSearchControlValue == typeof Function) {
                            var val = csc_getSearchControlValue();

                            if (typeof csc_getText == typeof Function) {
                                var txt = csc_getText();
                                if (txt) {
                                    $textValue = txt;
                                }
                            }

                            result = {
                                typeControl: 'value',
                                type: data.type,
                                text: $text,
                                textValue: $textValue,
                                value: val,
                                enable: data.enable,
                                id: data.id,
                                referenceId: data.referenceId,
                                customSearchUrl: data.customSearchUrl
                            };

                            if (typeof csc_getSearchControlConditionValue == typeof Function)
                                result.condition = csc_getSearchControlConditionValue();
                            if (data.referenceId)
                                result.referenceId = data.referenceId;
                        }
                        break;
                }
            }

            return result;
        }


        function GetElementValue(el, type) {
            if (el) {
                var typeElement = el.data("type-element");
                var $value;

                if (typeElement) {
                    switch (typeElement) {
                        case "tb":
                            $value = el.val();
                            break;
                        case "dp":
                            var $datepicker = el.data("kendoDatePicker");
                            if ($datepicker)
                                $value = kendo.toString($datepicker.value(), "dd.MM.yyyy");
                            break;
                        case "dtp":
                            var $datepicker = el.data("kendoDateTimePicker");
                            if ($datepicker)
                                $value = kendo.toString($datepicker.value(), "dd.MM.yyyy HH:mm:ss");
                            break;
                        case "ddl":
                            var $ddl = el.data("kendoDropDownList");
                            if ($ddl) {
                                if (type == 'text')
                                    $value = $ddl.text();
                                else
                                    $value = $ddl.value();
                            }
                            break;
                        case "multiSelect":
                            var $ddl = el.data("kendoMultiSelect");
                            if ($ddl)
                                $value = $ddl.value();
                            break;
                        case "radio":
                            $value = el.prop("checked");
                            break;
                    }
                }
            }

            return $value;
        }

        function SetElementValue(el, value) {
            if (el) {
                var type = el.data("type-element");
                if (value === undefined)
                    value = '';

                if (type) {
                    switch (type) {
                        case "tb":
                            el.val(value);
                            break;
                        case "dp":
                            var $datepicker = el.data("kendoDatePicker");
                            if ($datepicker)
                                $datepicker.value(value);
                            break;
                        case "dtp":
                            var $datepicker = el.data("kendoDateTimePicker");
                            if ($datepicker)
                                $datepicker.value(value);
                            break;
                        case "ddl":
                            var $ddl = el.data("kendoDropDownList");
                            if ($ddl) {
                                if (!$ddl.dataSource.transport.options) {
                                    $ddl.value(value);
                                    $ddl.trigger("change");
                                }
                                else
                                    $ddl.dataSource.bind("requestEnd", function (e) {
                                        $ddl.value(value);
                                        $ddl.trigger("change");
                                    });
                            }
                            break;
                        case "radio":
                            el.prop("checked", value);
                            break;
                    }
                }
            }
        }

        function RenderReferences(id, $conteiner, value) {
            if (id) {
                $conteiner.append('<span class="k-icon k-i-loading"></span>');
                return $.ajax({
                    url: self.referencesUrl,
                    type: 'GET',
                    dataType: "html",
                    data: { referenceId: id },
                    success: function (data) {
                        if (data) {
                            var dataSource = jQuery.parseJSON(data);
                            var $res = $(self.ReferenceControlTemplate).html();
                            $conteiner.html($res);
                            var $input = $conteiner.find("input.rvs-ddl-reference");
                            $input.attr("id", "rvs-el-" + self.registerViewId + "-" + self.idNumerator);

                            if (dataSource && dataSource.references.length > 10) {
                                var $multiSelect = $input.kendoMultiSelect({
                                    placeholder: "Выберите...",
                                    filter: "contains",
                                    height: 500,
                                    dataValueField: "id",
                                    dataTextField: "text",
                                    dataSource: dataSource.references,
                                    noDataTemplate: "Ничего не найдено!"
                                }).data("kendoMultiSelect");

                                if (value)
                                    $multiSelect.value(value);
                            } else {
                                var $references = dataSource.references;
                                if ($references.length && $references[0].text == '')
                                    $references.shift();

                                var $list = $(Mustache.render($(self.ReferenceCheckboxTemplate).html(), { references: $references }));
                                $input.data('role', 'rvslistbox');
                                var $rvsListBox = {
                                    list: $list,
                                    data: $references,
                                    value: function (val) {
                                        var $result = [];
                                        if (val && val.length) {
                                            var $listItems = this.list.find('input[type=checkbox]');
                                            $listItems.each(function () {
                                                var $value = $.inArray(parseInt($(this).attr('value')), val);
                                                if ($value > -1)
                                                    this.checked = true;
                                            });
                                        }
                                        else if (!val) {
                                            var $listItems = this.list.find('input[type=checkbox]');
                                            $listItems.each(function () {
                                                if (this.checked)
                                                    $result.push(parseInt($(this).attr('value')));
                                            });
                                        }
                                        else if (val && val.length == 0) {
                                            this.list.find('input[type=checkbox]').prop("checked", false);
                                        }

                                        return $result;
                                    }
                                };

                                $input.data('rvsListBox', $rvsListBox);

                                $input.hide();
                                $input.parent().prepend($list);

                                if (value) {
                                    $rvsListBox.value(value);
                                }
                            }

                            var activeControl = $('.rvs-control-button.active');
                            if (activeControl.length) {
                                var dialog = activeControl.data('dialog');
                                if (dialog) {
                                    var controlData = $conteiner.find('.rvs-control-data');

                                    dialog.setOptions({
                                        width: controlData.data('width') === undefined ? "300px" : controlData.data('width') + "px",
                                        height: controlData.data('height') === undefined ? "200px" : controlData.data('heigh') + "px",
                                    });

                                    positionDialog(controlData, dialog);
                                }
                            }
                        }
                    },
                    error: function (request) {
                        log('ошибка загрузки данных, url: ' + self.referencesUrl + ' ' + request.status + ' ' + request.statusText, true);
                    }
                });
            }
        }

        function RenderTemplate(template, container) {
            if (typeof container == "string")
                container = $(container);

            ToggleTemplate(template, container);
        }

        function ToggleTemplate(template, container) {
            var $res = typeof template == "string" ? $($(template).html()) : template;

            container.empty().append($res);
            var $needInitElements = container.find("[data-init]");
            var $afterInitElements = container.find("[data-after-init]");

            ExecuteMethods($needInitElements, "init");
            ExecuteMethods($afterInitElements, "after-init");
        }

        function ExecuteMethods($elements, key) {
            if ($elements.length > 0) {
                $elements.each(function () {
                    var $el = $(this);
                    if (!$el.attr("id")) {
                        $el.attr("id", "rvs-el-" + self.registerViewId + "-" + self.idNumerator);
                        self.idNumerator++;
                    }

                    var $id = "#" + $el.attr("id");

                    var funcList = $el.data(key);
                    var functions = funcList.split(',');

                    $.each(functions, function (index, value) {
                        if (methods.hasOwnProperty(value))
                            methods[value]($id);
                    });
                });
            }
        }

        function getCoords(elem) {
            return {
                top: elem[0].offsetTop,
                left: elem[0].offsetLeft
            };

        }

        function getAttributes() {
            if (self.registerViewId) {
                return $.ajax({
                    url: self.attributesUrl,
                    type: 'GET',
                    dataType: "html",
                    data: { registerViewId: self.registerViewId },
                    success: function (data) {
                        if (data) {
                            self.attributes = getAttributesDataSource(jQuery.parseJSON(data));
                            RenderTemplate(self.AddMenuTemplate, self.$element);

                            InitFilter();
                        }
                    },
                    error: function (request) {
                        log('ошибка загрузки данных, url: ' + self.attributesUrl + ' ' + request.status + ' ' + request.statusText, true);
                    }
                });
            }
        }

        function getAttributesDataSource(data) {
            var source = [];

            if (data && data.length > 0) {
                var parantItems = data.filter(function (item) {
                    return (item.ParentId == null);
                });

                if (parantItems.length) {
                    for (var i = 0; i < parantItems.length; i++) {
                        var child = {};
                        child.text = parantItems[i].Description;

                        var childItems = data.filter(function (item) {
                            return item.ParentId == parantItems[i].ItemId;
                        });

                        if (childItems) {
                            child.items = $.map(childItems, function (item) {
                                return {
                                    text: item.Description,
                                    id: item.AttributeId,
                                    parentId: item.ParentId,
                                    referenceId: item.ReferenceId,
                                    type: item.Type
                                };
                            });
                        }
                        source.push(child);
                    }
                }
            }

            return source;
        }

        function log(msg, warn) {
            if (window.console) {
                var message = '$.fn.registerViewSearch -> ' + msg;
                if (warn) {
                    console.warn(message);
                } else {
                    console.log(message);
                }
            }
        };
    }

    registerViewSearch.prototype.getStruct = function () {
        var out = [];

        var controls = $(this.GridToolBar + ' .rvs-control-button');

        if (controls.length > 0) {
            controls.each(function () {
                var $value = $(this).data('value');

                if ($value)
                    out.push($(this).data('value'));
            });
        }

        return out;
    }

    $.fn.registerViewSearch = function (config) {
        return this.each(function () {
            if (!$.data(this, "registerViewSearch")) {
                $.data(this, "registerViewSearch", new registerViewSearch(this, config));
            }
        });
    }
})(jQuery);