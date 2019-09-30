﻿(function ($) {
    function registerViewSearch(el, config) {
        this.element = el;
        this.$element = $(el);

        this.options = config;
        this.init(config);
    }

    registerViewSearch.prototype.init = function (config) {
        var self = this;

        self.saveFilterUrl = "/CoreUi/SaveSearchFilter";
        self.getFilterUrl = "/CoreUi/GetSearchFilter";
        self.registerViewId = config.registerViewId;
        self.registerId = config.registerId;
        self.attributesUrl = config.attributesUrl;
        self.referencesUrl = config.referencesUrl;
        self.filter = config.filter;
        self.AddMenuTemplate = "#rvs-add-menu-template";
        self.ButtonTemplate = "#rvs-button-template";
        self.IntegerControlTemplate = "#rvs-number-integer-control-template";
        self.DecimalControlTemplate = "#rvs-number-decimal-control-template";
        self.DateControlTemplate = "#rvs-date-control-template";
        self.BooleanControlTemplate = "#rvs-boolean-control-template";
        self.StringControlTemplate = "#rvs-string-control-template";
        self.ReferenceControlTemplate = "#rvs-reference-control-template";
        self.GridSelector = "#Grid-" + config.registerId,
        self.GridToolBar = '#GridToolBar-' + config.registerId;
        self.SearchButton = '#searchButton-' + config.registerId;
        self.ClearButton = '#clearButton-' + config.registerId;
        self.ResetSearchButton = '#resetButton-' + config.registerId;
        self.idNumerator = config.idNumerator ? config.idNumerator : 1;
        getAttributes();

        var methods = {
            initAddMenu: function (el) {
                $(el).kendoDropDownTree({
                    dataSource: self.attributes,
                    autoWidth: true,
                    placeholder: "Еще",
                    dataTextField: "text",
                    dataValueField: "value",
                    noDataTemplate: "Ничего не найдено!",
                    filter: "startswith",
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
                });
            },
            initDateTimePicker: function (el) {
                $(el).kendoDateTimePicker({
                    format: "dd.MM.yyyy HH:mm:ss"
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

                        var $input = e.sender.element.closest('.control-data').find('input.rvs-value');
                        if ($input.length > 0)
                            $input.attr('disabled', value == 'IsNull' || value == 'IsNotNull');
                    }
                });
            }
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

            SaveFilter();
        });

        self.$element.on('input', 'input.only-int', function () {
            this.value = this.value.replace(/[^0-9]/g, '');
        });

        self.$element.on('input', 'input.only-float', function () {
            this.value = this.value.replace(/[^0-9.]/g, '');
            this.value = this.value.replace(/(\..*)\./g, '$1');
        });

        self.$element.on('click', '.rvs-control-button', function () {
            var $this = $(this);

            var controls = $('.rvs-control-button.active').not($this);

            if (controls.length > 0) {
                controls.removeClass('active');
                var dialog = controls.data('dialog');
                if (dialog)
                    dialog.close();
            }

            $this.toggleClass('active');

            if ($this.hasClass('active'))
                ShowControll($this);
            else
            {
                var dialog = $this.data('dialog');
                if (dialog)
                    dialog.close();
            }
        });

        self.$element.on('click', '.rvs-btn-close', function () {
            var $this = $(this);
            var dialog = $this.closest('.k-window-content.k-content').data('kendoWindow');

            if (dialog)
                dialog.close();
        });

        $(self.GridToolBar).on('click', self.SearchButton, function () {
            reloadGrid();
        });

        $(self.GridToolBar).on('click', self.ClearButton, function () {
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
                SaveFilter();

                var grid = $(self.GridSelector).data('kendoGrid');
                if (grid)
                    grid.dataSource.read();
            }
        });

        $(self.GridToolBar).on('click', self.ResetSearchButton, function () {
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
                    var data = $this.data("control");
                    if (data) {
                        SetEmptyValue($this, data);
                        $this.find('.rvs-control-button-text').text(data.text);
                    }
                });

                SaveFilter();

                var grid = $(self.GridSelector).data('kendoGrid');
                if (grid)
                    grid.dataSource.read();
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
                    },
                    error: function (request) {
                        log('ошибка сохранения фильтра, url: ' + self.saveFilterUrl + ' ' + request.status + ' ' + request.statusText, true);
                    }
                });
            }
        }

        function InitFilter() {
            if (self.filter) {
                var struct = $.parseJSON(self.filter);

                if (struct.length > 0) {
                    var attributes = $('input.rvs-add-menu').data('kendoDropDownTree');

                    $.each(struct, function (index, value) {
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

                    reloadGrid();
                }
            }
        }

        function AddControll(data, value) {
            if (!$.isEmptyObject(data)) {
                var $button = $(Mustache.render($(self.ButtonTemplate).html(), data));
                $button.data("control", data);
                self.$element.find('span.rvs-add-menu').before($button);

                if (value) {
                    $button.data("value", value);
                    $button.find('.rvs-control-button-text').text(value.text);
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
                    var $controlTemplate = $('<div style="padding: 15px"></div>');

                    var coords = getCoords(el);

                    var dialog = $controlTemplate.kendoWindow({
                        title: data.text,
                        draggable: false,
                        visible: false,
                        resizable: false,
                        appendTo: self.$element,
                        close: function (e) {
                            var target = e.sender.element.data('target');
                            var data = el.data('control');

                            if (target) {
                                target.removeClass('active');
                                var $value = GetValue(e.sender.element, data)
                                target.data('value', $value);
                                target.find('.rvs-control-button-text').text($value.text);
                            }

                            SaveFilter();

                            this.destroy();
                        },
                        position: {
                            top: coords.top + 43,
                            left: coords.left
                        }
                    }).data('kendoWindow');

                    var type = data.type;
                    if (data.referenceId)
                        type = 'REFERENCE';

                    switch (type) {
                        case 'INTEGER':
                            RenderTemplate(self.IntegerControlTemplate, $controlTemplate, data);
                            break;
                        case 'DECIMAL':
                            RenderTemplate(self.DecimalControlTemplate, $controlTemplate, data);
                            break;
                        case 'BOOLEAN':
                            RenderTemplate(self.BooleanControlTemplate, $controlTemplate, data);
                            break;
                        case 'STRING':
                            RenderTemplate(self.StringControlTemplate, $controlTemplate, data);
                            break;
                        case 'DATE':
                            RenderTemplate(self.DateControlTemplate, $controlTemplate, data);
                            break;
                        case 'REFERENCE':
                            var controlValue = el.data('value');
                            RenderReferences(data.referenceId, $controlTemplate, controlValue ? controlValue.value : undefined);
                            break;
                    }

                    if (type != 'REFERENCE') {
                        var controlData = $controlTemplate.find('.control-data');

                        dialog.setOptions({
                            width: controlData.data('width') === undefined ? "300px" : controlData.data('width') + "px",
                            height: controlData.data('height') === undefined ? "200px" : controlData.data('heigh') + "px",
                        });
                    }

                    dialog.element.data('target', el);
                    el.data('dialog', dialog);

                    SetValue(el, $controlTemplate);

                    dialog.open();
                }
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
                            if (valueControl.typeControl == 'value') {
                                control.find('input[type=radio].rvs-single').prop("checked", true);

                                if (valueControl.type == 'DATE')
                                    control.find('input.rvs-value').data('kendoDateTimePicker').value(valueControl.value);
                                else
                                    control.find('input.rvs-value').val(valueControl.value);
                            } else if (valueControl.typeControl == 'value') {
                                control.find('input[type=radio].rvs-range').prop("checked", true);

                                if (valueControl.type == 'DATE') {
                                    control.find('input.rvs-from').data('kendoDateTimePicker').value(valueControl.from);
                                    control.find('input.rvs-to').data('kendoDateTimePicker').value(valueControl.to);
                                }
                                else {
                                    control.find('input.rvs-from').val(valueControl.from);
                                    control.find('input.rvs-to').val(valueControl.to);
                                }
                            } else if (valueControl.typeControl == 'null'){
                                control.find('input[type=radio].rvs-null').prop("checked", true);
                            } else if (valueControl.typeControl == 'not-null') {
                                control.find('input[type=radio].rvs-not-null').prop("checked", true);
                            }
                            break;
                        case 'BOOLEAN':
                            var $el = control.find('.rvs-bool').data('kendoButtonGroup');
                            if ($el)
                                $el.select(valueControl.value - 1);
                            break;
                        case 'STRING':
                            var $conditionDdl = $('select.rvs-condition').data("kendoDropDownList");

                            if ($conditionDdl) {
                                $conditionDdl.value(valueControl.condition);
                                var $input = control.find('input.rvs-value');

                                if ($input.length > 0) {
                                    $input.val(valueControl.value);
                                    $input.attr('disabled', valueControl.condition == 'IsNull' || valueControl.condition == 'IsNotNull');
                                }
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

                el.data('value', result)
            }
        }

        function GetValue(control, data) {
            var result = {};

            if (control.length > 0 && data) {
                var type = data.type;
                if (data.referenceId)
                    type = 'REFERENCE';

                var $text = data.text;

                switch (type) {
                    case 'INTEGER':
                    case 'DECIMAL':
                    case 'DATE':
                        var $el = control.find('input[type=radio]:checked');
                        if ($el.length > 0) {
                            var $typeControl = 'value';
                            if ($el.hasClass('rvs-range'))
                                var $typeControl = 'range';
                            else if ($el.hasClass('rvs-null'))
                                var $typeControl = 'null';
                            else if ($el.hasClass('rvs-not-null'))
                                var $typeControl = 'not-null';

                            var $value = type == 'DATE' ?
                                kendo.toString(control.find('input.rvs-value').data('kendoDateTimePicker').value(), "dd.MM.yyyy HH:mm:ss") :
                                control.find('input.rvs-value').val();
                            var $from = type == 'DATE' ?
                                kendo.toString(control.find('input.rvs-from').data('kendoDateTimePicker').value(), "dd.MM.yyyy HH:mm:ss") :
                                control.find('input.rvs-from').val();
                            var $to = type == 'DATE' ?
                                kendo.toString(control.find('input.rvs-to').data('kendoDateTimePicker').value(), "dd.MM.yyyy HH:mm:ss") :
                                control.find('input.rvs-to').val();

                            if ($typeControl == 'value') {
                                $text += $value ? ': ' + $value : '';
                            } else if ($typeControl == 'range') {
                                if ($from && $to)
                                    $text += ': c ' + $from + ' до ' + $to;
                                else if ($from)
                                    $text += ': Больше или равно ' + $from;
                                else if ($to)
                                    $text += ': Меньше или равно ' + $to;
                            } else if ($typeControl == 'null')
                                $text += ': Пусто';
                            else
                                $text += ': Не пусто';

                            result = {
                                typeControl: $typeControl,
                                text: $text,
                                type: type,
                                value: $typeControl == 'value' ? $value : undefined,
                                from: $typeControl == 'range' ? $from : undefined,
                                to: $typeControl == 'range' ? $to : undefined,
                                id: data.id
                            };

                            if ($typeControl == 'null')
                                result.condition = 'IsNull';
                            else if ($typeControl == 'not-null')
                                result.condition = 'IsNotNull';
                        }
                        break;
                    case 'BOOLEAN':
                        var $el = control.find('.rvs-bool').data('kendoButtonGroup');
                        if ($el) {
                            result = {
                                typeControl: 'value',
                                type: type,
                                text: $text + ($el.current().index() == 0 ? ': Да' : ': Нет'),
                                value: $el.current().index() + 1,
                                id: data.id
                            };
                        }
                        break;
                    case 'STRING':
                        var $conditionDdl = $('select.rvs-condition').data("kendoDropDownList");

                        if ($conditionDdl) {
                            var val = control.find('input.rvs-value').val();
                            result = {
                                condition: $conditionDdl.value(),
                                typeControl: 'value',
                                type: type,
                                text: val ? $text + ': ' + $conditionDdl.text() + ' ' + val : $text,
                                value: val,
                                id: data.id
                            };
                        }
                        break;
                    case 'REFERENCE':
                        var $referenceList = control.find("input.rvs-ddl-reference").data("kendoMultiSelect");
                        if ($referenceList) {
                            var val = $referenceList.value();

                            if (!$.isEmptyObject(val)) {
                                var names = $.map($referenceList.dataItems(), function (item) {
                                    return item.text;
                                });

                                $text += ': ' + names.join(', ');
                            }

                            result = {
                                typeControl: 'value',
                                type: type,
                                text: $text,
                                value: $referenceList.value(),
                                referenceId: data.referenceId,
                                id: data.id
                            };
                        }
                        break;
                }
            }

            return result;
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

                            var $multiSelect = $input.kendoMultiSelect({
                                placeholder: "Выберите...",
                                filter: "contains",
                                dataValueField: "id",
                                dataTextField: "text",
                                dataSource: dataSource.references,
                                noDataTemplate: "Ничего не найдено!"
                            }).data("kendoMultiSelect");

                            if (value)
                                $multiSelect.value(value);

                            var activeControl = $('.rvs-control-button.active');
                            if (activeControl.length > 0) {
                                var dialog = activeControl.data('dialog');
                                if (dialog) {
                                    var controlData = $conteiner.find('.control-data');

                                    dialog.setOptions({
                                        width: controlData.data('width') === undefined ? "300px" : controlData.data('width') + "px",
                                        height: controlData.data('height') === undefined ? "200px" : controlData.data('heigh') + "px",
                                    });
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

        function RenderTemplate(template, container, data) {
            if (typeof container == "string")
                container = $(container);

            ToggleTemplate(template, container, data);
        }

        function ToggleTemplate(template, container, data) {
            var $res = $($(template).html());

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
                        $el.attr("id", "rvs-el-" + self.idNumerator);
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
    }

    registerViewSearch.prototype.getStruct = function () {
        var out = [];

        var controls = $('.rvs-control-button');

        if (controls.length > 0) {
            controls.each(function () {
                var $value = $(this).data('value');

                if ($value)
                    out.push($(this).data('value'));
            });
        }

        return out
    }

    $.fn.registerViewSearch = function (config) {
        return this.each(function () {
            if (!$.data(this, "registerViewSearch")) {
                $.data(this, "registerViewSearch", new registerViewSearch(this, config));
            }
        });
    }
})(jQuery);