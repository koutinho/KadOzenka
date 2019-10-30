(function ($) {
    function FilterQueryBuilder(el, config) {
        this.element = el;
        this.$element = $(el);

        this.options = config;
        this.init(config);
    }

    FilterQueryBuilder.prototype.init = function (config) {
        var self = this;

        self.saveUrl = config.saveUrl;
        self.saveParams = config.saveParams;
        self.deleteUrl = config.deleteUrl;
        self.deleteParams = config.saveParams;
        self.registerViewId = config.registerViewId;
        self.attributesUrl = config.attributesUrl;
        self.referencesUrl = config.referencesUrl;
        self.filter = config.filter;
        self.readOnly = config.readOnly ? config.readOnly.toLowerCase() === 'true' : false;
        self.conditionTemplate = config.conditionTemplate ? config.conditionTemplate : 'condition-template';
        self.groupTemplate = config.groupTemplate ? config.groupTemplate : 'group-template';
        self.referenceTemplate = config.referenceTemplate ? config.referenceTemplate : 'reference-template';
        self.booleanTemplate = config.booleanTemplate ? config.booleanTemplate : 'boolean-template';
        getAttributes();

        /* События */
        self.$element.on('click', 'button[data-bind=addCondition]', function () {
            addCondition($(this).parent().find('.group-conditions:first'));
            setQuery(self.$element);
        });
        self.$element.on('click', 'button[data-bind=addGroup]', function () {
            addGroup($(this).parent().find('.group-conditions:first'));
            setQuery(self.$element);
        });
        self.$element.on('click', 'button[data-bind=removeCondition]', function () {
            removeElement($(this).parent());
            setQuery(self.$element);
        });
        self.$element.on('click', 'button[data-bind=removeGroup]', function () {
            removeElement($(this).parent());
            setQuery(self.$element);
        });
        self.$element.on('click', 'button[data-bind=saveFiltr]', function () {
            if (!self.saveUrl) {
                Common.ShowError('Не заполнен параметр saveUrl');
                return;
            }

            var struct = getStruct(self.$element);

            $.ajax({
                url: self.saveUrl,
                type: 'POST',
                data: $.extend(config.saveParams, { filter: JSON.stringify(struct) }),
                success: function (response) {
                    if (response) {
                        if (response.Errors)
                            Common.ShowError(response.Errors.Message);
                        else
                            Common.ShowMessage('Фильтр сохранен');
                    }
                },
                error: function (request) {
                    log('ошибка сохранения фильтра, url: ' + self.saveUrl + ' ' + request.status + ' ' + request.statusText, true);
                }
            });
        });
        self.$element.on('click', 'button[data-bind=deleteFiltr]', function () {
            if (!self.deleteUrl) {
                Common.ShowError('Не заполнен параметр deleteUrl');
                return;
            }

            Common.UI.ShowConfirm({
                title: 'Подтверждение',
                content: 'Вы действительно хотите удалить фильтр?',
                onSuccess: function (e) {
                    $.ajax({
                        url: self.deleteUrl,
                        type: 'POST',
                        data: self.deleteParams,
                        success: function (response) {
                            if (response) {
                                if (response.Errors)
                                    Common.ShowError(response.Errors.Message);
                                else {
                                    emptyStruct();
                                    Common.ShowMessage('Фильтр удален');
                                }
                            }

                            var window = $('[class=dialog-body]').closest('[data-role=window]').data('kendoWindow');
                            window.close();
                            window.destroy();
                        },
                        error: function (request) {
                            log('ошибка удаления фильтра, url: ' + self.deleteUrl + ' ' + request.status + ' ' + request.statusText, true);
                        }
                    });
                }
            });
        });
        self.$element.on('input', 'input.condition_value.k-textbox', function () {
            setQuery(self.$element);
        });
        self.$element.on('input', 'input.only_int', function () {
            this.value = this.value.replace(/[^0-9]/g, '');
            setQuery(self.$element);
        });
        self.$element.on('input', 'input.only_float', function () {
            this.value = this.value.replace(/[^0-9.]/g, '');
            this.value = this.value.replace(/(\..*)\./g, '$1');
            setQuery(self.$element);
        });

        function s_attributesOnselect(e) {
            setVallidation(e.node.parent());
            setQuery(self.$element);
        };

        self.$element.on('change', 'select.f_references', function () {
            setQuery(self.$element);
        });
        self.$element.on('change', 'select.logicalOperators', function () {
            setQuery(self.$element);
        });
        self.$element.on('change', 'select.comparisons', function () {
            var $conteiner = $(this).parent();
            setVallidation($conteiner);
            setQuery(self.$element);
        });
        self.$element.on('change', 'select.booleanOperators', function () {
            setQuery(self.$element);
        });
        /* end */

        function log(msg, warn) {
            if (window.console) {
                var message = '$.fn.filterQueryBuilder -> ' + msg;
                if (warn) {
                    console.warn(message);
                } else {
                    console.log(message);
                }
            }
        };

        function initControls() {
            self.$element.find('.alert.alert-warning.alert-group:first').prepend('<div style="margin-bottom: 5px;"><label style="width: 60px;">Запрос:</label><input id="f_txt_query" type="text" class="k-textbox" style="width: calc(100% - 165px);margin-right: 5px;"><button class="k-button f_button_save" style="margin-right: 5px;" title="Сохранить фильтр" data-bind="saveFiltr"><span class="k-icon k-i-save"></span>&nbsp;</button><button class="k-button f_button_delete" style="margin-left: -1px;" title="Удалить фильтр" data-bind="deleteFiltr"><span class="k-icon k-i-delete"></span>&nbsp;</button></div>');
        }

        function getStruct(parent) {
            var out = {};
            if (parent) {
                out.group = [];
                var group = {};
                group.operator = parent.find('select.logicalOperators:first').val();

                var queryItems = parent.find('.group-conditions:first').children();
                if (queryItems.length > 0) {
                    var conditions = [];
                    var groups = [];
                    for (var i = 0; i < queryItems.length; i++) {
                        var $queryItem = $(queryItems[i]);
                        var condition = {};
                        if ($queryItem.hasClass('condition')) {
                            var attribute = $queryItem.find('span.s_attributes input.s_attributes').data("kendoDropDownTree").value();
                            var type = self.dataSource[attribute].type;
                            condition.attribute = attribute;
                            condition.referenceId = self.dataSource[attribute].referenceId;
                            condition.comparison = $queryItem.find('.comparisons').val();
                            condition.type = type;

                            if (type == 'DATE')
                                condition.value = $queryItem.find('input.condition_value').val();
                            else
                                condition.value = $queryItem.find('.condition_value').val();

                            conditions.push(condition);
                        }
                        else {
                            var childGroup = getStruct($queryItem);
                            if (!$.isEmptyObject(childGroup))
                                groups.push(childGroup);
                        }
                    }

                    if (conditions.length > 0) {
                        group.condition = conditions;
                    }

                    if (groups.length > 0) {
                        group.group = groups;
                    }

                    if (!$.isEmptyObject(group)) {
                        out.group.push(group);
                    }
                }
            }
            return out;
        }

        function getQuery(parent) {
            var out = '(';

            if (parent) {
                var queryItems = parent.find('.group-conditions:first').children();
                if (queryItems.length > 0) {
                    var operator = parent.find('select.logicalOperators:first option:selected').text();
                    for (var i = 0; i < queryItems.length; i++) {
                        var $queryItem = $(queryItems[i]);
                        if (i > 0)
                            out += " {0} ".format(operator);
                        if ($queryItem.hasClass('condition')) {
                            var $input;
                            var attributeText = $queryItem.find('span.s_attributes input.s_attributes').data("kendoDropDownTree").text();
                            var attributeValue = $queryItem.find('span.s_attributes input.s_attributes').data("kendoDropDownTree").value();

                            var type = attributeValue != 0 && typeof self.dataSource[attributeValue] !== undefined ? self.dataSource[attributeValue].type : '';
                            var comparison = $queryItem.find('.comparisons option:selected').text();
                            if (type == 'DATE')
                                $input = $queryItem.find('input.condition_value');
                            else
                                $input = $queryItem.find('.condition_value');

                            var value = '';

                            if ($input.getType() == 'text')
                                value = $input.val();
                            else if ($input.getType() == 'select')
                                value = $input.find('option:selected').text();

                            out += "{0} {1} {2}".format(attributeText, comparison, value);
                        }
                        else {
                            var childGroup = getQuery($queryItem);
                            if (childGroup)
                                out += childGroup;
                        }
                    }
                }
            }

            out += ')';

            return out
        }

        function setQuery(element) {
            var query = getQuery(element);
            $('#f_txt_query').val(query)

            $('.f_button_delete').prop('disabled', function (i, v) { return query == '()'; });
        }

        function unescapeJson(val) {
            return val
                .replace(/[\\"]/g, '"')
                .replace(/[\\\\]/g, "\\")
                .replace(/[\\/]/g, '\/')
        }

        function CreateStruct(parent, struct) {
            if (parent && struct) {
                if (struct.condition && struct.condition.length > 0) {
                    struct.condition.forEach(function (value) {
                        addCondition(parent.find('.group-conditions:first'), value);
                    });
                }
                if (struct.group && struct.group.length > 0) {
                    struct.group.forEach(function (g) {
                        g.group.forEach(function (value) {
                            var group = addGroup(parent.find('.group-conditions:first'), value);
                            CreateStruct(group, value);
                        });
                    });
                }
            }
        }

        function fillStruct() {
            var struct = $.parseJSON(self.filter);

            if (struct) {
                if (struct.group && struct.group.length > 0) {
                    var group = addGroup(self.$element, {}, true)
                    CreateStruct(group, struct.group[0]);
                }
            }
        }

        function emptyStruct() {
            self.$element.find('.group-conditions:first').empty();
            setQuery(self.$element);
        }

        function setVallidation(container) {
            var $input;

            var input_container = container.find('.f_inputs:first');
            var attributeElement = container.find('span.s_attributes:first input.s_attributes');
            if (attributeElement.length) {
                var attributeValue = attributeElement.data("kendoDropDownTree").value()
                if (attributeValue) {
                    var comparison = container.find('.comparisons:first option:selected');
                    var type = typeof self.dataSource[attributeValue] !== undefined ? self.dataSource[attributeValue].type : '';
                    var referenceid = typeof self.dataSource[attributeValue] !== undefined ? self.dataSource[attributeValue].referenceId : '';
                    setComparisons(container, type);

                    if (comparison.val() == 'IsNull' || comparison.val() == 'IsNotNull') {
                        input_container.empty();
                        return
                    }

                    if (referenceid) {
                        getReferences(referenceid, input_container);
                    }
                    else if (type && type != self.oldType || input_container.html() == '') {
                        input_container.empty();

                        switch (type) {
                            case 'INTEGER':
                                $input = $('<input type="text" class="condition_value k-textbox only_int"/>');
                                input_container.append($input);
                                break;
                            case 'DECIMAL':
                                $input = $('<input type="text" class="condition_value k-textbox only_float"/>');
                                input_container.append($input);
                                break;
                            case 'BOOLEAN':
                                var $res = $($('#' + self.booleanTemplate).html());
                                input_container.append($res);
                                break;
                            case 'STRING':
                                $input = $('<input type="text" class="condition_value k-textbox"/>');
                                input_container.append($input);
                                break;
                            case 'DATE':
                                $input = $('<input type="text" class="condition_value"/>');
                                input_container.append($input);
                                $input.kendoDatePicker({
                                    format: "dd.MM.yyyy",
                                    change: function () {
                                        setQuery(self.$element);
                                    }
                                });
                                break;
                        }

                        self.oldType = type;
                    }
                }
            }
        }

        function setComparisons(container, type)
        {
            if (container && type) {
                var $input = container.find('select.comparisons:first');
                var comparisons = [];

                switch (type) {
                    case 'INTEGER':
                        comparisons = ['Equal', 'NotEqual', 'Less', 'LessOrEqual', 'Greater', 'GreaterOrEqual', 'IsNull', 'IsNotNull'];
                        break;
                    case 'DECIMAL':
                        comparisons = ['Equal', 'NotEqual', 'Less', 'LessOrEqual', 'Greater', 'GreaterOrEqual', 'IsNull', 'IsNotNull'];
                        break;
                    case 'BOOLEAN':
                        comparisons = ['Equal', 'NotEqual', 'IsNull', 'IsNotNull'];
                        break;
                    case 'STRING':
                        comparisons = ['Equal', 'NotEqual', 'BeginFrom', 'EndTo', 'Contains', 'NotContains', 'IsNull', 'IsNotNull'];
                        break;
                    case 'DATE':
                        comparisons = ['Equal', 'NotEqual', 'Less', 'LessOrEqual', 'Greater', 'GreaterOrEqual', 'IsNull', 'IsNotNull'];
                        break;
                }

                var children = $input.children().show();

                for (var i = 0; i < children.length; i++) {
                    if ($.inArray(children[i].value, comparisons) === -1)
                        $(children[i]).hide();
                }

                if ($.inArray($input.val(), comparisons) === -1) {
                    $input.children().filter(function () { return $(this).css('display') != 'none'; }).eq(0).prop('selected', true);
                }
            }

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
                            self.dataSource = getDataSource(jQuery.parseJSON(data)); 
                            self.attributes = getAttributesSource(jQuery.parseJSON(data));

                            if (!self.filter)
                                addGroup(self.$element, {}, true);
                            else
                                fillStruct();

                            initControls();
                            setQuery(self.$element);

                            if (self.readOnly)
                                self.$element.prepend('<div style="position: absolute;height: 97%;width: 100%;z-index: 9999"></div>');
                        }
                    },
                    error: function (request) {
                        log('ошибка загрузки данных, url: ' + self.attributesUrl + ' ' + request.status + ' ' + request.statusText, true);
                    }
                });
            }
        };

        function getDataSource(data) {
            var source = [];

            if (data && data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    if (source[data] === 0)
                        continue;

                    source[data[i].AttributeId] = {
                        Description: data[i].Description,
                        parentId: data[i].ParentId,
                        itemId: data[i].ItemId,
                        referenceId: data[i].ReferenceId,
                        type: data[i].Type
                    };
                }
            }

            return source;
        }

        function getAttributesSource(data) {
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
                                return { text: item.Description, value: item.AttributeId, parentId: item.ParentId};
                            });
                        }
                        source.push(child);
                    }
                }
            }

            return source;
        }

        function getReferences(id, $conteiner, conVal) {
            if (id) {
                $conteiner.append('<span class="k-icon k-i-loading"></span>');
                return $.ajax({
                    url: self.referencesUrl,
                    type: 'GET',
                    dataType: "html",
                    data: { referenceId: id },
                    success: function (data) {
                        if (data) {
                            var references = jQuery.parseJSON(data);
                            var $res = $(Mustache.render($('#' + self.referenceTemplate).html(), references));

                            if (conVal) {
                                $res.val(conVal);
                            }

                            $conteiner.html($res);
                            setQuery(self.$element);
                        }
                    },
                    error: function (request) {
                        log('ошибка загрузки данных, url: ' + self.referencesUrl + ' ' + request.status + ' ' + request.statusText, true);
                    }
                });
            }
        };

        function addCondition(group, params) {
            if (group) {
                var $res = $(Mustache.render($('#' + self.conditionTemplate).html(), self.attributes));

                var ddTree = $res.find('input.s_attributes:first').kendoDropDownTree({
                    dataSource: self.attributes,
                    placeholder: "Выберите...",
                    dataTextField: "text",
                    dataValueField: "value",
                    filter: "startswith",
                    ignoreCase: true,
                    select: function (e) {
                        var item = this.dataItem(e.node);
                        if (!item.parentId) {
                            e.preventDefault();
                            return;
                        }
                    },
                    change: function (e) {
                        if (this.value() != 0) {
                            setVallidation(e.sender.element.closest('.condition'));
                            setQuery(self.$element);
                        }
                    }
                });

                if (params) {
                    if (params.attribute) {
                        ddTree.data("kendoDropDownTree").value(params.attribute);
                    }
                    if (params.comparison) {
                        $res.find('select.comparisons:first').val(params.comparison);
                    }
                }
                var attributeValue = ddTree.data("kendoDropDownTree").value();
                
                var referenceid = attributeValue != 0 && typeof self.dataSource[attributeValue] !== undefined ? self.dataSource[attributeValue].referenceId : '';
                var type = attributeValue != 0 && typeof self.dataSource[attributeValue] !== undefined ? self.dataSource[attributeValue].type : '';
                var $input_conteiner = $res.find('.f_inputs:first').empty();
                var conVal = '';

                if (params && params.value) {
                    var conVal = unescapeJson(params.value);
                }

                if (referenceid) {
                    getReferences(referenceid, $input_conteiner, conVal);
                }
                else {
                    setVallidation($res);

                    if (conVal) {
                        if (type == 'DATE')
                            $res.find('input.condition_value').data("kendoDatePicker").value(conVal);
                        else
                            $res.find('.condition_value').val(conVal);
                    }
                }                

                group.append($res);
            }
        };

        function addGroup(parent, params, root) {
            root = (typeof root !== 'undefined') ? root : false;

            var $res = undefined;

            if (parent) {
                $res = $($('#' + self.groupTemplate).html());

                if (root)
                    $res.find('button[data-bind=removeGroup]').remove();

                if (!$.isEmptyObject(params)) {
                    $res.find('select.logicalOperators:first').val(params.operator);
                }

                parent.append($res);
            }

            return $res;
        };

        function removeElement(element) {
            element.remove();
        };
    }

    $.fn.filterQueryBuilder = function (config) {
        return this.each(function () {
            if (!$.data(this, 'filterQueryBuilder')) {
                $.data(this, 'filterQueryBuilder', new FilterQueryBuilder(this, config));
            }
        });
    }
})(jQuery);