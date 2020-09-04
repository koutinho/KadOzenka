(function ($) {
    function filterQueryBuilderExtended(el, config) {
        this.element = el;
        this.$element = $(el);

        this.options = config;
        this.init(config);
    }

    filterQueryBuilderExtended.prototype.init = function (config) {
        var self = this;

        self.saveUrl = config.saveUrl;
        self.saveParams = config.saveParams;
        self.deleteUrl = config.deleteUrl;
        self.deleteParams = config.saveParams;
        self.registerViewId = config.registerViewId;
        self.attributesUrl = config.attributesUrl;
        self.referencesUrl = config.referencesUrl;
        self.referenceListUrl = config.referenceListUrl;
        self.reestrListUrl = config.reestrListUrl;
        self.joinTypeListUrl = config.joinTypeListUrl;
        self.functionsListUrl = config.functionsListUrl;
        self.analyticFunctionTypeListUrl = config.analyticFunctionTypeListUrl;
        self.columnSpecialTypeListUrl = config.columnSpecialTypeListUrl;
        self.filter = config.filter ? typeof (config.filter) == "string" ? jQuery.parseJSON(config.filter) : config.filter : {};
        self.readOnly = config.readOnly ? config.readOnly.toLowerCase() === "true" : false;
        self.window = config.window ? config.window : false;
        self.MainTemplate = "#ex-main-template";
        self.ConditionMenuTemplate = "#ex-condition-menu-template";
        self.ConditionIfMenuTemplate = "#ex-condition-if-menu-template";
        self.ComparisonsMenuTemplate = "#ex-comparisons-menu-template";
        self.ConditionMenuToolsTemplate = "#ex-condition-menu-tools-template";
        self.ConditionMenuEditTemplate = "#ex-condition-menu-edit-template";
        self.SubqueryMenuEditTemplate = "#ex-subquery-menu-edit-template";
        self.ColumnEditorTemplate = "#ex-column-editor-template";
        self.DialogTemplate = "#ex-dialog-template";
        self.DialogColumnTemplate = "#ex-dialog-column-template";
        self.DialogReestrTemplate = "#ex-dialog-reestr-template";
        self.ModalColumnTemplate = "#ex-modal-column-template";
        self.ModalColumnConstantTemplate = "#ex-modal-column-constant-template";
        self.ModalColumnConstantParameterTemplate = "#ex-modal-column-constant-parameter-template";
        self.ModalColumnConstantManualTemplate = "#ex-modal-column-constant-manual-template";
        self.ModalColumnConstantReferenceTemplate = "#ex-modal-column-constant-reference-template";
        self.ModalColumnConstantBooleanTemplate = "#ex-modal-column-constant-boolean-template";
        self.ModalColumnAttributeTemplate = "#ex-modal-column-attribute-template";
        self.ModalColumnSpecialTemplate = "#ex-modal-column-special-template";
        self.ModalColumnFunctionTemplate = "#ex-modal-column-function-template";
        self.ModalColumnAnalyticFunctionTemplate = "#ex-modal-column-analytic-function-template";
        self.ModalColumnSubqueryTemplate = "#ex-modal-column-subquery-template";
        self.ModalColumnConditionIfTemplate = "#ex-modal-column-condition-if-template";
        self.ModalColumnConditionCaseTemplate = "#ex-modal-column-condition-case-template";
        self.referenceTemplate = config.referenceTemplate ? config.referenceTemplate : "reference-template";
        self.booleanTemplate = config.booleanTemplate ? config.booleanTemplate : "boolean-template";
        self.idNumerator = config.idNumerator ? config.idNumerator : 0;
        self.nameNumerator = config.nameNumerator ? config.nameNumerator : 1;
        self.level = config.level ? config.level : 0;
        var columnNameTemplate = "Колонка_";
        // возможные варианты редактора
        // "Filter" - фильтр
        // "Layout" - фильтр раскладки
        // "Column" - колонка - подзапрос (пользовательская колонка)
        // "VirtualColumn" - виртуальная колонка 
        // "Query"  - полный запрос - редактор без ограничений
        self.editorType = config.editorType ? config.editorType : "Filter";

        self.elements = {};

        var methods = {
            initCheckBox: function (el) {
                $(el).after('<label for="' + el.substring(1) + '" class="k-checkbox-label k-no-text"></label>')
            },
            initDropDownList: function (el, config) {
                config = config ? config : {};
                if (el) {
                    var $item = $(el).kendoDropDownList({
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

                    self.elements[el] = $item.wrapper;
                    self.elements[$item.list.attr("id")] = $item.list;
                    $(el).data("type-element", "ddl");
                }
            },
            initAnalyticFunctonTypeDdl: function (el) {
                methods.initDropDownList(el, {
                    dataSource: {
                        serverFiltering: false,
                        transport: {
                            read: {
                                url: self.analyticFunctionTypeListUrl
                            }
                        }
                    }
                });
            },
            initEnterTypeDdl: function (el) {
                methods.initDropDownList(el, {
                    dataValueField: "value",
                    change: function (e) {                      
                        var value = e.sender.value();
                        var $container = e.sender.element.closest(".ex-modal-column-constant-content").find(".ex-value-container");
                        setValidation($container, value);
                    }
                });
            },
            initInputMethodDdl: function (el) {
                methods.initDropDownList(el, {
                    dataValueField: "value",
                    change: function (e) {
                        var value = e.sender.value();
                        var $container = e.sender.element.closest(".ex-modal-column-constant-content").find(".ex-value");
                        setValidation($container, value);
                    }
                });
            },
            initQueryLevelDdl: function (el) {
                var $dataSource = [];

                $dataSource.push({ id: 0, text: "Текущий уровень" });
                for (var i = 1; i <= self.level; i++) {
                    $dataSource.push({ id: i, text: "Уровень " + i });
                }

                methods.initDropDownList(el, {
                    dataSource: $dataSource
                });
            },
            initSourceEnterTypeDdl: function (el) {
                methods.initDropDownList(el, {
                    dataValueField: "value",
                    change: function (e) {
                        var value = e.sender.value();
                        var $container = e.sender.element.closest(".ex-modal-column-constant-content").find(".ex-value");

                        if (value == "Manual") {
                            e.sender.element.data("toggle-template", false);
                            e.sender.element.closest(".ex-modal-column-constant-content")
                                .find(".ex-ddl-data-type").closest('.form-group').show();
                            e.sender.element.closest(".ex-modal-column-constant-content")
                                .find(".ex-value").closest('.form-group').hide();
                        }
                        else {
                            e.sender.element.data("toggle-template", true);
                            e.sender.element.closest(".ex-modal-column-constant-content")
                                .find(".ex-ddl-data-type").closest('.form-group').hide();
                            e.sender.element.closest(".ex-modal-column-constant-content")
                                .find(".ex-value").closest('.form-group').show();
                        }

                        setValidation($container, value);
                    }
                });
            },
            initDataTypeDdl: function (el) {
                methods.initDropDownList(el, {
                    dataValueField: "value"
                });
            },
            initDataSourceDdl: function (el) {
                methods.initDropDownList(el);
            },
            initMainRegisterDdl: function (el) {
                methods.initDropDownList(el, {
                    filter: "contains",
                    dataSource: {
                        serverFiltering: false,
                        transport: {
                            read: {
                                url: self.reestrListUrl,
                                data: { registerViewId: self.registerViewId }
                            }
                        }
                    }
                });
            },
            initRegisterListDdl: function (el) {
                methods.initDropDownList(el, {
                    filter: "contains",
                    dataSource: {
                        serverFiltering: false,
                        transport: {
                            read: {
                                url: self.reestrListUrl
                            }
                        }
                    }
                });
            },
            initReferenceDdl: function (el) {
                methods.initDropDownList(el, {
                    filter: "contains",
                    dataSource: {
                        serverFiltering: false,
                        transport: {
                            read: {
                                url: self.referenceListUrl
                            }
                        }
                    }
                });
            },
            initSpecialTypeDdl: function (el) {
                methods.initDropDownList(el, {
                    filter: "contains",
                    dataSource: {
                        serverFiltering: false,
                        transport: {
                            read: {
                                url: self.columnSpecialTypeListUrl
                            }
                        }
                    }
                });
            },
            initJoinTypeDdl: function (el) {
                methods.initDropDownList(el, {
                    dataTextField: "Text",
                    dataValueField: "Value",
                    dataSource: {
                        serverFiltering: false,
                        transport: {
                            read: {
                                url: self.joinTypeListUrl
                            }
                        }
                    }
                });
            },
            initTabStrip: function (el) {
                var $item = $(el).kendoTabStrip({
                    show: function () {
                        $("input.ex-tb-filter").val("");
                    },
                    animation: {
                        open: {
                            effects: "fadeIn"
                        }
                    }
                }).data("kendoTabStrip");

                self.elements[el] = $item.wrapper;
                $(el).data("type-element", "tabStrip");
            },
            initColumnToolbar: function (el) {
                var $el = $(el);
                if ($el.length) {
                    var $dialogColumn = $el.closest('.ex-dialog-column');
                    var container = $dialogColumn.find(".ex-modal-content");

                    var $item = $el.kendoToolBar({
                        items: [
                            {
                                type: "button", id: "btnSaveColumn",
                                text: (self.editorType == "Column" || self.editorType == "VirtualColumn" ? "Сохранить" : "Ок"), click: saveColumn
                            },
                            {
                                type: "buttonGroup", buttons: [
                                    { text: "Константа", id: "QSColumnConstant", group: "column_type_group", togglable: true, toggle: function () { ShowContent(container, "QSColumnConstant", $dialogColumn.data('column')) }, selected: true },
                                    { text: "Показатель", id: "QSColumnSimple", group: "column_type_group", togglable: true, toggle: function () { ShowContent(container, "QSColumnSimple", $dialogColumn.data('column')) } },
                                    { text: "Специальная колонка", id: "QSColumnSpecial", group: "column_type_group", togglable: true, toggle: function () { ShowContent(container, "QSColumnSpecial", $dialogColumn.data('column')) } },
                                    { text: "Функция", id: "QSColumnFunction", group: "column_type_group", togglable: true, toggle: function () { ShowContent(container, "QSColumnFunction", $dialogColumn.data('column')) } },
                                    { text: "Аналит. функция", id: "QSColumnFunctionAnalytic", group: "column_type_group", togglable: true, toggle: function () { ShowContent(container, "QSColumnFunctionAnalytic", $dialogColumn.data('column')) } },
                                    { text: "Подзапрос", id: "QSColumnQuery", group: "column_type_group", togglable: true, toggle: function () { ShowContent(container, "QSColumnQuery", $dialogColumn.data('column')) } },
                                    { text: "Условие (IF)", id: "QSColumnIf", group: "column_type_group", togglable: true, toggle: function () { ShowContent(container, "QSColumnIf", $dialogColumn.data('column')) } },
                                    { text: "Условие (SWITCH)", id: "QSColumnSwitch", group: "column_type_group", togglable: true, toggle: function () { ShowContent(container, "QSColumnSwitch", $dialogColumn.data('column')) } }
                                ]
                            }
                        ]
                    }).data("kendoToolBar");

                    $el.find(".k-button-group").before(" Тип колонки: ");                    
                    
                    self.elements[el] = $item.wrapper;
                    $(el).data("type-element", "toolBar");
                }
            },
            initColumnGridToolbar: function (el) {
                var $item = $(el).kendoToolBar({
                    items: [{
                        id: "addColumn",
                        type: "button",
                        icon: "plus-outline",
                        click: function (e) {
                            AddColumnWindow(e.item.element, "saveSelectColumn");
                        }
                    },
                    {
                        id: "editColumn",
                        type: "button",
                        icon: "edit",
                        enable: false,
                        click: function (e) {
                            var $el = e.item.element.closest(".k-content").find(".ex-columns-grid");
                            if ($el.length) {
                                var grid = $el.data("kendoGrid");

                                if (grid) {
                                    var selected = grid.select();
                                    var data = grid.dataItem(selected);
                                    AddColumnWindow(e.item.element, "saveSelectColumn", data);
                                }
                            }
                        }
                    },
                    {
                        id: "deleteColumn",
                        type: "button",
                        icon: "delete",
                        enable: false,
                        click: function (e) {
                            var $el = e.item.element.closest(".k-content").find(".k-grid");
                            if ($el.length) {
                                gridRowDelete($el.data("kendoGrid"));
                            }
                        }
                    }]
                }).data("kendoToolBar");

                self.elements[el] = $item.wrapper;
                $(el).data("type-element", "toolBar");
            },
            initFilterToolbar: function (el) {
                var $item = $(el).kendoToolBar({
                    items: [{
                        id: "addFilterGroup",
                        type: "button",
                        icon: "plus-outline",
                        click: function (e) {
                            var $el = e.item.element.closest(".k-content").find(".ex-treeview");
                            if ($el.length) {
                                var treeView = $el.data("kendoTreeView");

                                if (treeView) {
                                    addNode(treeView, CreateEmptyGroupElement());
                                }
                            }
                        }
                    }]
                }).data("kendoToolBar");

                self.elements[el] = $item.wrapper;
                $(el).data("type-element", "toolBar");
            },
            initJoinToolbar: function (el) {
                var $item = $(el).kendoToolBar({
                    items: [{
                        id: "addJoin",
                        type: "button",
                        icon: "plus-outline",
                        click: function (e) {
                            var $el = e.item.element.closest(".k-content").find(".ex-treeview");
                            if ($el.length) {
                                var treeView = $el.data("kendoTreeView");

                                if (treeView) {
                                    treeView.select($());
                                    addJoin(treeView);
                                }
                            }
                        }
                    }]
                }).data("kendoToolBar");

                self.elements[el] = $item.wrapper;
                $(el).data("type-element", "toolBar");
            },
            initSortToolbar: function (el) {
                var $item = $(el).kendoToolBar({
                    items: [{
                        id: "addSort",
                        type: "button",
                        icon: "plus-outline",
                        click: function (e) {
                            AddColumnWindow(e.item.element, "saveSortColumn");
                        }
                    },
                    {
                        id: "editSort",
                        type: "button",
                        icon: "edit",
                        enable: false,
                        click: function (e) {
                            saveListViewItem(e.item.element, "saveSortColumn");
                        }
                    },
                    {
                        id: "deleteSort",
                        type: "button",
                        icon: "delete",
                        enable: false,
                        click: function (e) {
                            var $el = e.item.element.closest(".k-content").find(".k-listview");
                            deleteListViewItem($el, "Вы действительно хотите удалить сортировку?");
                        }
                    }]
                }).data("kendoToolBar");

                self.elements[el] = $item.wrapper;
                $(el).data("type-element", "toolBar");
            },
            initJoinTypeToolbar: function (el) {
                var $item = $(el).kendoToolBar({
                    items: [
                        {
                            text: "Ок", type: "button", click: function (e) { methods.saveJoin(e.sender); }
                        },
                        {
                            type: "buttonGroup", buttons: [
                                { text: "Внешнее", id: "Left", group: "join_type_group", togglable: true, selected: true },
                                { text: "Внутреннее", id: "Inner", group: "join_type_group", togglable: true }
                            ]
                        },
                        {
                            text: "Актуальность", type: "button", id: "actualBtn", click: function (e) {
                                var data = e.item.element.data("item");
                                AddColumnWindow(e.item.element, "saveActualColumn", data);
                            }
                        }
                    ]
                }).data("kendoToolBar");

                $(el).find(".k-button-group").before(" Объединение: ");
                $(el).find("#actualBtn").attr("data-element-role", "ActualColumn");
                $(el).find("#actualBtn").attr("data-type-element", "item");

                self.elements[el] = $item.wrapper;
                $(el).data("type-element", "toolBar");
            },
            initGroupByToolbar: function (el) {
                var $item = $(el).kendoToolBar({
                    items: [{
                        id: "addGroupColumn",
                        type: "button",
                        icon: "plus-outline",
                        click: function (e) {
                            AddColumnWindow(e.item.element, "saveGroupByColumn");
                        }
                    },
                    {
                        id: "editGroupColumn",
                        type: "button",
                        icon: "edit",
                        enable: false,
                        click: function (e) {
                            saveListViewItem(e.item.element, "saveGroupByColumn");
                        }
                    },
                    {
                        id: "deleteGroupColumn",
                        type: "button",
                        icon: "delete",
                        enable: false,
                        click: function (e) {
                            var $el = e.item.element.closest(".k-content").find(".k-listview");
                            deleteListViewItem($el, "Вы действительно хотите удалить группировку?");
                        }
                    }]
                }).data("kendoToolBar");

                self.elements[el] = $item.wrapper;
                $(el).data("type-element", "toolBar");
            },
            initFunctionParametersToolbar: function (el) {
                var $item = $(el).kendoToolBar({
                    items: [{
                        id: "addFuncParameter",
                        type: "button",
                        icon: "plus-outline",
                        click: function (e) {
                            AddColumnWindow(e.item.element, "saveFunctionParameter");
                        }
                    },
                    {
                        id: "editFuncParameter",
                        type: "button",
                        icon: "edit",
                        enable: false,
                        click: function (e) {
                            saveListViewItem(e.item.element, "saveFunctionParameter");
                        }
                    },
                    {
                        id: "deleteFuncParameter",
                        type: "button",
                        icon: "delete",
                        enable: false,
                        click: function (e) {
                            var $el = e.item.element.closest(".ex-content").find(".k-listview");
                            deleteListViewItem($el, 'Вы действительно хотите удалить параметр функции?');
                        }
                    }]
                }).data("kendoToolBar");

                $(el).find("a.k-button:first").before("Параметры: ");
                self.elements[el] = $item.wrapper;
                $(el).data("type-element", "toolBar");
            },
            initToolbarTooltip: function (el) {
                $(el).kendoTooltip({
                    filter: "a.k-button",
                    content: function (e) {
                        switch ($(e.target).attr('id')) {
                            case "addColumn":
                                return "Добавить колонку";
                            case "addSort":
                                return "Добавить сортировку";
                            case "addGroupColumn":
                                return "Добавить группировку";
                            case "addFilterGroup":
                                return "Добавить группу";
                            case "addJoin":
                                return "Добавить объединение";
                            case "addCondition":
                            case "addFuncParameter":
                                return "Добавить";
                            case "editSort":
                            case "editColumn":
                            case "editGroupColumn":
                            case "editFuncParameter":
                                return "Редактировать";
                            case "deleteColumn":
                                return "Удалить колонку";
                            case "deleteSort":
                                return "Удалить сортировку";
                            case "deleteGroupColumn":
                                return "Удалить группировку";
                            case "deleteFuncParameter":
                                return "Удалить";
                        }
                    },
                    position: "right",
                    autoHide: true,
                    showAfter: 500
                });
            },
            initColumnGrid: function (el) {
                var $item = $(el).kendoGrid({
                    scrollable: true,
                    selectable: true,
                    columns: [
                        { field: "AttributeId", hidden: true },
                        { field: "Alias", title: "Наименование", width: '15%', template: '<a class="ex-edit-column" href="\\#">#: Alias ? Alias : "" #</a>' },
                        { field: "TitleValue", title: "Значение", template: '#: TitleValue ? TitleValue : "" #', attributes: { style: 'text-align: left' } },
                        /*{ field: "OrderType", title: "Сортировка", template: '#: OrderType ? OrderType : "" #' },
                        { field: "Group", title: "Группировка", template: '#: Group ? Group : "" #' },
                        { field: "Function", title: "Функция", template: '#: Function ? Function : "" #' },*/
                        { command: [{ name: 'destroy', text: '' }], width: "45px", attributes: { class: 'close-command-cell', style: 'text-align: center' } }
                    ],
                    change: function (e) {
                        var selectedItem = e.sender.select();
                        var toolbar = e.sender.element.closest(".k-content").find(".ex-toolbar").data("kendoToolBar");

                        if (toolbar) {
                            toolbar.enable("#editColumn", selectedItem);
                            toolbar.enable("#deleteColumn", selectedItem);
                        }
                    }
                }).data("kendoGrid");

                self.elements[el] = $item.wrapper;
                $(el).data("type-element", "grid");
            },
            initOrderByListView: function (el) {
                var $item = $(el).kendoListView({
                    selectable: true,
                    template: "<li><span style='padding-left: 10px;'>#:Title#</span></li>",
                    autoBind: false,
                    dataBound: function (e) {
                        var elements = e.sender.element.find('li');
                        if (elements.length) {
                            elements.each(function () {
                                restoreSort($(this));
                            });
                        }

                        var toolbar = e.sender.element.closest(".k-content").find(".ex-toolbar").data("kendoToolBar");

                        if (toolbar) {
                            toolbar.enable("#editSort", false);
                            toolbar.enable("#deleteSort", false);
                        }
                    },
                    change: function (e) {
                        var selectedItem = e.sender.select();
                        var toolbar = e.sender.element.closest(".k-content").find(".ex-toolbar").data("kendoToolBar");

                        if (toolbar) {
                            toolbar.enable("#editSort", selectedItem);
                            toolbar.enable("#deleteSort", selectedItem);
                        }
                    }
                }).data("kendoListView");

                self.elements[el] = $item.wrapper;
                $(el).data("type-element", "listView");
            },
            initGroupByListView: function (el) {
                var $item = $(el).kendoListView({
                    selectable: true,
                    autoBind: false,
                    template: "<li><span style='padding-left: 10px;'>#:Title#</span></li>",
                    dataBound: function (e) {
                        var toolbar = e.sender.element.closest(".k-content").find(".ex-toolbar").data("kendoToolBar");
                        if (toolbar) {
                            toolbar.enable("#editGroupColumn", false);
                            toolbar.enable("#deleteGroupColumn", false);
                        }
                    },
                    change: function (e) {
                        var selectedItem = e.sender.select();
                        var toolbar = e.sender.element.closest(".k-content").find(".ex-toolbar").data("kendoToolBar");

                        if (toolbar) {
                            toolbar.enable("#editGroupColumn", selectedItem);
                            toolbar.enable("#deleteGroupColumn", selectedItem);
                        }
                    }
                }).data("kendoListView");

                self.elements[el] = $item.wrapper;
                $(el).data("type-element", "listView");
            },
            initFunctionParamsListView: function (el) {
                var $item = $(el).kendoListView({
                    selectable: true,
                    autoBind: false,
                    template: "<li><span style='padding-left: 10px;'>#:TitleValue#</span></li>",
                    dataBound: function (e) {
                        var toolbar = e.sender.element.closest(".ex-content").find(".ex-toolbar").data("kendoToolBar");
                        if (toolbar) {
                            toolbar.enable("#editFuncParameter", false);
                            toolbar.enable("#deleteFuncParameter", false);
                        }
                    },
                    change: function (e) {
                        var selectedItem = e.sender.select();
                        var toolbar = e.sender.element.closest(".ex-content").find(".ex-toolbar").data("kendoToolBar");

                        if (toolbar) {
                            toolbar.enable("#editFuncParameter", selectedItem);
                            toolbar.enable("#deleteFuncParameter", selectedItem);
                        }
                    }
                }).data("kendoListView");

                self.elements[el] = $item.wrapper;
                $(el).data("type-element", "listView");
            },
            initFunctionsTree: function (el) {
                var $item = $(el).kendoTreeView({
                    loadOnDemand: false,
                    dataTextField: ["text"],
                    dataSource: {
                        type: "json",
                        serverFiltering: false,
                        transport: {
                            read: {
                                url: self.functionsListUrl
                            }
                        },
                        schema: {
                            type: "json",
                            data: "data",
                            model: {
                                children: "items"
                            }
                        }
                    },
                    select: function (e) {
                        var item = this.dataItem(e.node);
                        if (item.category) {
                            e.preventDefault();
                            return;
                        }

                        var $cbExFunctionExternal = $(e.node).closest(".ex-splitter").find(".ex-function-external");
                        if ($cbExFunctionExternal)
                            $cbExFunctionExternal.prop("checked", false);

                        var $input = $(e.node).closest(".ex-splitter").find(".ex-function-name");
                        if ($input) {
                            $input.val(item.text);
                            $input.data("id", item.id);
                        }
                    }
                }).data("kendoTreeView");

                self.elements[el] = $item.wrapper;
                $(el).data("type-element", "treeView");
            },
            initAttributesTree: function (el) {
                var $item = $(el).kendoTreeView({
                    loadOnDemand: false,
                    dataSource: self.attributes,
                    dataBound: function () {
                        $(el).kendoTooltip({
                            filter: '[role="group"] > [role="treeitem"] .k-in',
                            showAfter: 500,
                            content: function (e) {
                                var treeItem = $item.dataItem(e.target);
                                var textFields = 'Тип&nbsp;данных';
                                var valueFiedls = treeItem.DataTypeName;

                                if (treeItem.ReferenceName && treeItem.ReferenceName.length > 0) {
                                    textFields += '<br>Справочник';
                                    valueFiedls += '<br>' + treeItem.ReferenceName;
                                }
                                if (treeItem.IsPrimaryKey == true) {
                                    textFields += '<br>Главный&nbsp;ключ';
                                    valueFiedls += '<br>Да';
                                }
                                if (treeItem.ForeignKey && treeItem.ForeignKey.length > 0) {
                                    textFields += '<br>Внешник&nbsp;ключ&nbsp;к';
                                    valueFiedls += '<br>' + treeItem.ForeignKey;
                                }
                                if (treeItem.DescriptionAttribute && treeItem.DescriptionAttribute.length > 0) {
                                    textFields += '<br>Описание';
                                    valueFiedls += '<br>' + treeItem.DescriptionAttribute;
                                }
                                if (treeItem.IsVirtual == true) {
                                    textFields += '<br>Виртуальная&nbsp;колонка';
                                    valueFiedls += '<br>Да';
                                }
                                if (treeItem.ColumnDbName && treeItem.ColumnDbName.length > 0) {
                                    textFields += '<br>Колонка&nbsp;таблицы';
                                    valueFiedls += '<br>' + treeItem.ColumnDbName;
                                }

                                var content =
                                    '<div style="text-align: left;">' +
                                    '<div style="font-weight: bold; padding: 2px 0px;">' + treeItem.text + ' (' + treeItem.id + ')</div>' +
                                    '<div style="display: flex; line-height: 1.6;">' +
                                    '<div style="padding-right: 10px;">' + textFields + '</div>' +
                                    '<div style="flex: 1;">' + valueFiedls + '</div>' +
                                    '</div>' +
                                    '</div>';

                                return content;
                            }
                        });
                    },
                    select: function (e) {
                        var item = this.dataItem(e.node);
                        if (!item.parentId) {
                            e.preventDefault();
                            return;
                        }
                    }
                }).data("kendoTreeView");

                self.elements[el] = $item.wrapper;
                $(el).data("type-element", "treeView");
            },
            initTree: function (el, dataSource) {
                var config = {
                    template: kendo.template($("#ex-tw-template").html()),
                    loadOnDemand: false
                };

                if (dataSource)
                    config.dataSource = dataSource;

                var $item = $(el).kendoTreeView(config).data("kendoTreeView");

                self.elements[el] = $item.wrapper;
                $(el).data("type-element", "treeView");
            },
            initJoinsTree: function (el) {
                methods.initTree(el);
            },
            initReestrTree: function (el) {
                var $item = $(el).kendoTreeView({
                    loadOnDemand: false,
                    dataTextField: "text",
                    dataValueField: "id",
                    dataSource: {
                        type: "json",
                        serverFiltering: false,
                        transport: {
                            read: {
                                url: self.reestrListUrl,
                                data: { registerViewId: self.registerViewId }
                            }
                        }
                    }
                }).data("kendoTreeView");

                self.elements[el] = $item.wrapper;
                $(el).data("type-element", "treeView");
            },
            initConditionTree: function (el) {
                methods.initTree(el);
            },
            initConditionIfTree: function (el) {
                var dataSource = [{
                        id: self.getNextId(),
                        Operator: "And",
                        OperatorText: "И",
                        Type: "groupIf",
                        root: true,
                        expanded: true,
                        selected: false,
                        items: [{
                                    id: self.getNextId(),
                                    Type: "columnIfResult",
                                    ValueOperand: { TitleValue: '<Выберите...>', New: true },
                                    selected: false
                               }]
                        },
                        {
                            id: self.getNextId(),
                            Type: "columnIfElse",
                            ValueOperand: { TitleValue: '<Выберите...>', New: true },
                            selected: false
                        }
                ];

                methods.initTree(el, dataSource);
            },
            initConditionSwitchTree: function (el) {
                var dataSource = [{
                    id: self.getNextId(),
                    Type: "columnSwitchValue",
                    expanded: true,
                    ValueOperand: { TitleValue: '<Выберите...>', New: true },
                    items: [
                               {   
                                   id: self.getNextId(),
                                   Type: "columnSwitch",
                                   LeftOperand: { TitleValue: '<Выберите...>', New: true },
                                   RightOperand: { TitleValue: '<Выберите...>', New: true },
                                   selected: false
                               }
                           ],
                    selected: false
                    }
                ];

                methods.initTree(el, dataSource);
            },
            initAnalyticFunctionTree: function (el) {
                var dataSource = [{
                    id: self.getNextId(),
                    Type: "aggregateFunc",
                    expanded: true,
                    selected: false,
                    items: []
                },
                {
                    id: self.getNextId(),
                    Type: "sortSettings",
                    expanded: true,
                    selected: false,
                    items: []
                }
                ];

                methods.initTree(el, dataSource);
            },
            initConditionContextMenu: function (el) {
                var $template = $($(self.ConditionMenuTemplate).html());
                var $item = $template.kendoContextMenu({
                    target: el,
                    showOn: "click",
                    filter: ".f-ex-group",
                    open: function (e) {
                        this.enable(e.item.find('li[data-value=paste]'), window.top.bufer != undefined && (window.top.bufer.type == 'group' || window.top.bufer.type == 'condition'));

                        var node = $(e.target).closest("li.k-item");
                        var treeView = node.closest(".ex-treeview").data("kendoTreeView");

                        if (treeView)
                            treeView.select(node);
                    },
                    select: function (e) {
                        var button = $(e.item);
                        var node = $(e.target);

                        var treeView = node.closest(".ex-treeview").data("kendoTreeView");
                        var btnVal = button.data("value");

                        switch (btnVal) {
                            case "And":
                            case "Or":
                                node.data("value", btnVal);

                                if (treeView) {
                                    var dataItem = treeView.dataItem(node);
                                    if (dataItem) {
                                        dataItem.set('Operator', btnVal);
                                        dataItem.set('OperatorText', button.text());
                                    }
                                }
                                break;
                            case "copy":
                                copyToClipboard(node.closest("li.k-item"), btnVal);
                                break;
                            case "paste":
                                pasteFromClipboard(node.closest("li.k-item"), btnVal);
                                break;
                            case "addCondition":
                                addCondition(treeView)
                                break;
                            case "addGroup":
                                addGroup(treeView);
                                break;
                            case "deleteGroup":
                                if (treeView) {
                                    var dataItem = treeView.dataItem(node);
                                    if (!dataItem.root)
                                        deleteNode(treeView);
                                }
                                break;
                        }
                    }
                }).data("kendoContextMenu");

                self.elements[el] = $item.wrapper;
            },
            initConditionIfContextMenu: function (el) {
                var $template = $($(self.ConditionIfMenuTemplate).html());
                var $item = $template.kendoContextMenu({
                    target: el,
                    showOn: "click",
                    filter: ".f-ex-group-if",
                    open: function (e) {
                        var node = $(e.target).closest("li.k-item");
                        var treeView = node.closest(".ex-treeview").data("kendoTreeView");

                        if (treeView)
                            treeView.select(node);
                    },
                    select: function (e) {
                        var node = $(e.target).closest("li");
                        var treeView = node.closest(".ex-treeview").data("kendoTreeView");
                        var btnVal = $(e.item).data("value");

                        switch (btnVal) {
                            case "addCondition":
                                var condition = CreateEmptyConditionElement();
                                treeView.insertBefore(condition, node.find("li:has(span.ex-condition-result)"));
                                break;
                            case "addGroup":
                                var group = CreateEmptyGroupElement();
                                treeView.insertBefore(group, node.find("li:has(span.ex-condition-result)"));
                                break;
                        }
                    }
                }).data("kendoContextMenu");

                self.elements[el] = $item.wrapper;
            },
            initConditionToolsContextMenu: function (el) {
                var $template = $($(self.ConditionMenuToolsTemplate).html());
                var $item = $template.kendoContextMenu({
                    target: el,
                    showOn: "click",
                    filter: ".ex-btn-condition-tools",
                    open: function (e) {
                        this.enable(e.item.find('li[data-value=paste_left_operand]'), window.top.bufer != undefined && window.top.bufer.type == 'column');
                        this.enable(e.item.find('li[data-value=paste_right_operand]'), window.top.bufer != undefined && window.top.bufer.type == 'column');

                        var node = $(e.target).closest("li.k-item");
                        var treeView = node.closest(".ex-treeview").data("kendoTreeView");

                        if (treeView) {
                            treeView.select(node);
                            var dataItem = treeView.dataItem(node);
                            if (dataItem)
                                e.item.find('li[data-value=copy_condition]').toggle(dataItem.Type != 'columnSwitch')
                        }
                    },
                    select: function (e) {
                        var btnVal = $(e.item).data("value");
                        var node = $(e.target.closest('li.k-item'));

                        if (btnVal.startsWith('copy'))
                            copyToClipboard(node, btnVal);
                        else if (btnVal.startsWith('paste'))
                            pasteFromClipboard(node, btnVal);
                    }
                }).data("kendoContextMenu");

                self.elements[el] = $item.wrapper;
            },
            initConditionEditContextMenu: function (el) {
                var $template = $($(self.ConditionMenuEditTemplate).html());
                var $item = $template.kendoContextMenu({
                    target: el,
                    showOn: "click",
                    filter: ".ex-btn-edit-menu",
                    open: function (e) {
                        this.enable(e.item.find('li[data-value=paste_column]'), window.top.bufer != undefined && window.top.bufer.type == 'column');

                        var node = $(e.target).closest("li.k-item");
                        var treeView = node.closest(".ex-treeview").data("kendoTreeView");

                        if (treeView)
                            treeView.select(node);
                    },
                    select: function (e) {
                        var btnVal = $(e.item).data("value");
                        var node = $(e.target.closest('li.k-item'));

                        switch (btnVal) {
                            case "copy_column":
                                copyToClipboard(node, btnVal);
                                break;
                            case "paste_column":
                                pasteFromClipboard(node, btnVal);
                                break;
                        }
                    }
                }).data("kendoContextMenu");

                self.elements[el] = $item.wrapper;
            },
            initSubQueryEditContextMenu: function (el) {
                var $template = $($(self.SubqueryMenuEditTemplate).html());
                var $item = $template.kendoContextMenu({
                    target: el,
                    showOn: "click",
                    open: function (e) {
                        this.enable(e.item.find('li[data-value=paste_subquery]'), window.top.bufer != undefined && window.top.bufer.type == 'subquery');
                    },
                    select: function (e) {
                        var $el = $('a.ex-add-query');
                        if ($el.length) {
                            var btnVal = $(e.item).data("value");
                            switch (btnVal) {
                                case "copy_subquery":
                                    var $query = $el.data("item");
                                    if (query) {
                                        window.top.bufer = { value: $query, type: 'subquery' };
                                    }
                                    break;
                                case "paste_subquery":
                                    if (window.top.bufer != undefined) {
                                        if (window.top.bufer.type == 'subquery') {
                                            $el.data("item", window.top.bufer.value);

                                            if (!$el.find("span.k-i-check-outline").length)
                                                $el.append('<span class="k-icon k-i-check-outline" style="margin-left: 5px;"></span>');

                                            var $queryTextEl = $el.parent().next('.query-text').data('item', window.top.bufer.value);
                                            methods.setQueryText($queryTextEl);
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }).data("kendoContextMenu");

                self.elements[el] = $item.wrapper;
            },
            initComparisonsContextMenu: function (el) {
                var $template = $($(self.ComparisonsMenuTemplate).html());
                var $item = $template.kendoContextMenu({
                    // listen to right-clicks on treeview container
                    target: el,
                    showOn: "click",
                    // show when node text is clicked
                    filter: ".ex-comparisons",
                    // handle item clicks
                    select: function (e) {
                        var button = $(e.item);
                        var node = $(e.target);

                        node.text(button.text());
                        node.data("value", button.data("value"));

                        var dataItem = getTreeViewDataItem(node);
                        if (dataItem) {
                            dataItem.set("condition", button.data("value"));
                            dataItem.set("ConditionText", button.text());
                        }
                    }
                }).data("kendoContextMenu");

                self.elements[el] = $item.wrapper;
            },
            initSplitter: function (el) {
                var params = getParameters($(el));
                if (!$.isEmptyObject(params) && params["size"]) {
                    var size = params["size"].split(',');
                }

                var $item = $(el).kendoSplitter({
                    panes: [
                               { size: size && size.length > 0 ? size[0] : "50%" },
                               { size: size && size.length > 1 ? size[1] : "50%" }
                           ]
                }).data("kendoSplitter");

                self.elements[el] = $item.wrapper;
            },
            // отобразить текстовое представление запроса
            setQueryText: function (el) {
                var queryText = '';
                var query = $(el).data("item");

                if (query && !$.isEmptyObject(query)) {
                    if (query.Columns.length) {
                        queryText += '<strong>Выбрать</strong><br/>'
                        $.each(query.Columns, function (index, val) {
                            queryText += '<span style="margin-left:10px">' + val.TitleValue + '</span>' + (index < query.Columns.length - 1 ? ',<br/>' : '<br/>');
                        });
                    }
                    if (query.Joins.length) {
                        queryText += getJoinText(query.Joins);
                        $.each(query.Joins, function (index, val) {
                            queryText += getJoinText(val) + (index < query.Joins.length - 1 ? ',<br/>' : '<br/>');
                        });
                    }
                    if (query.Filter.length && query.Filter[0].items.length) {
                        queryText += '<strong>Фильтр</strong><br/>' + getGroupText(query.Filter[0], true) + '<br/>';
                    }
                    if (query.Group.length) {
                        queryText += '<strong>Группировка</strong><br/>';
                        $.each(query.Group, function (index, val) {
                            queryText += '<span style="margin-left:10px">' + val.TitleValue + '</span>' + (index < query.Group.length - 1 ? ',<br/>' : '<br/>');
                        });
                    }
                    if (query.Sort.length) {
                        queryText += '<strong>Сортировка</strong><br/>';
                        $.each(query.Sort, function (index, val) {
                            queryText += '<span style="margin-left:10px">' + val.TitleValue + (!val.OrderType || val.OrderType == 'ASC' ? ' (Прямая)' : ' (Обратная)') + '</span>' + (index < query.Sort.length - 1 ? ',<br/>' : '<br/>');
                        });
                    }
                    queryText += '<br/><br/>';
                }

                function getGroupText(el, parent) {
                    var result = '';
                    if (!$.isEmptyObject(el)) {
                        if (el.Type == 'group') {
                            if (!parent)
                                result += '<strong>(</strong><br/>';
                            if (el.items.length) {
                                $.each(el.items, function (index, val) {
                                    result += getGroupText(val) + (index < el.items.length - 1 ? '<br/><strong>' + el.OperatorText + '</strong><br/>' : '');
                                });
                            }
                            if (!parent)
                                result += '<br/><strong>)</strong>';

                        } else {
                            result += getConditionText(el);
                        }
                    }
                    return result;
                }

                function getConditionText(condition) {
                    var result = '';
                    if (!$.isEmptyObject(condition) && condition.Type == 'condition')
                        result += '<span style="margin-left:10px">{0} <strong>{1}</strong> {2}</span>'.format(condition.LeftOperand.TitleValue, condition.ConditionText, condition.RightOperand.TitleValue);
                    return result;
                }

                function getJoinText(el) {
                    var result = '';
                    if (!$.isEmptyObject(el) && el.Type == 'join' && el.RegisterId) {
                        result += '<strong>Объединение</strong> {0}{1}<strong>Условие</strong><br/>'.format(el.text, (el.ActualColumn ? '<br/><strong>Актуально на</strong> ' + el.ActualColumn.TitleValue + '<br/>' : '<br/>'));
                        if (el.items.length) {
                            $.each(el.items, function (index, val) {
                                result += getGroupText(val, parent);
                            });
                        }
                    }
                    return result;
                }

                $(el).html(queryText);
            },
            saveSelectColumn: function (el, data) {
                if (el) {
                    var grid = self.$element.find(".ex-columns-grid").data("kendoGrid");

                    if (grid && data) {
                        var dataSource = grid.dataSource;

                        if (data.New) {
                            data.New = false;
                            dataSource.add(data);
                        }
                        else {
                            var dataItem = dataSource.get(data.id);
                            updateDataItem(dataItem, data);
                        }
                    }
                }
            },
            saveUserColumn: function (el, data) {
                if (!self.saveUrl) {
                    Common.ShowError("Не заполнен параметр saveUrl");
                    return;
                }

                $.ajax({
                    url: self.saveUrl,
                    type: "POST",
                    data: $.extend(config.saveParams, { column: JSON.stringify(data) }),
                    success: function (response) {
                        if (response) {
                            if (response.Errors)
                                Common.ShowError(response.Errors.Message);
                            else {
                                config.saveParams.columnId = response;
                                Common.ShowMessage("Пользовательская  колонка сохранена");
                            }
                        }
                    },
                    error: function (request) {
                        log("ошибка сохранения пользовательской колонки, url: " + self.saveUrl + " " + request.status + " " + request.statusText, true);
                    }
                });
            },
            saveFiltrColumn: function (el, data) {
                if (el) {
                    data.New = false;

                    var $dataItem = getTreeViewDataItem(el);
                    if ($dataItem) {
                        if (el.hasClass("ex-condition-right"))
                            $dataItem.set("RightOperand", data);
                        else if (el.hasClass("ex-condition-value"))
                            $dataItem.set("ValueOperand", data);
                        else
                            $dataItem.set("LeftOperand", data);
                    }
                }
            },
            saveJoin: function (el) {
                if (el) {
                    var $dialog = el.element.closest(".ex-content.k-window-content");

                    var reestrTreeView = $dialog.find(".ex-treeview").data("kendoTreeView");

                    if (reestrTreeView) {
                        var node = getSelectedNode(reestrTreeView);
                        var $reestrDataItem = reestrTreeView.dataItem(node);

                        var reestrId = $reestrDataItem ? $reestrDataItem.id : undefined;
                        var reestrName = $reestrDataItem ? $reestrDataItem.text : undefined;
                        var joinType = el.getSelectedFromGroup("join_type_group").data("button").options.id;
                        var joinText = el.getSelectedFromGroup("join_type_group").data("button").options.text;
                        var actualColumn = el.element.find("#actualBtn").data("item");
                        var $target = $dialog.data("target");

                        var $joinDataItem = getTreeViewDataItem($target);
                        if ($joinDataItem) {
                            var data = {
                                text: reestrName + " ({0})".format(joinText),
                                RegisterId: reestrId,
                                JoinType: joinType,
                                ActualColumn: actualColumn
                            };

                            updateDataItem($joinDataItem, data)
                        }
                    }

                    if ($dialog.data("kendoWindow"))
                        $dialog.data("kendoWindow").close();
                }
            },
            saveListViewItem: function (el, data) {
                if (el) {
                    var listView = el.closest(".ex-content").find(".k-listview").data("kendoListView");
                    if (listView) {
                        var dataSource = listView.dataSource;

                        if (data.New) {
                            data.New = false;
                            listView.dataSource.add(data);
                        }
                        else {
                            var dataItem = dataSource.get(data.id);
                            updateDataItem(dataItem, data);
                        }
                    }
                }
            },
            saveSortColumn: function (el, data) {
                methods.saveListViewItem(el, data);
            },
            saveGroupByColumn: function (el, data) {
                methods.saveListViewItem(el, data);
            },
            saveQuery: function (el, data) {
                if (el) {
                    data.New = false;

                    if (!el.data("item"))
                        el.append('<span class="k-icon k-i-check-outline" style="margin-left: 5px;"></span>');

                    var $queryTextEl = el.parent().next('.query-text').data('item', data);
                    methods.setQueryText($queryTextEl);

                    el.data("item", data);
                }
            },
            saveActualColumn: function (el, data) {
                if (el) {
                    data.New = false;

                    if (!el.data("item"))
                        el.append('<span class="k-icon k-i-check-outline" style="margin-left: 5px;"></span>');

                    el.data("item", data);
                }
            },
            saveFunctionParameter: function (el, data) {
                methods.saveListViewItem(el, data);
            }
        };

        InitControls();

        /* События */

        self.$element.on("click", ".ex-btn-delete", function (e) {
            e.preventDefault();
            var treeView = $(this).closest(".ex-treeview").data("kendoTreeView");
            var node = $(this).closest("li");

            if (treeView) {
                var dataItem = treeView.dataItem(node);
                if (!dataItem.root)
                    deleteNode(treeView);
            }
        });

        self.$element.on("click", ".ex-asc-sort", function (e) {
            setSort("ASC");
        });

        self.$element.on("click", ".ex-desc-sort", function (e) {
            setSort("DESC");
        });

        self.$element.on("input", ".ex-tb-filter", function () {
            var query = this.value.toLowerCase();
            var dataSource = $(this).closest(".ex-content").find(".ex-treeview").data("kendoTreeView").dataSource;

            filterAttributes(dataSource, query);
        });

        self.$element.on('input', 'input.ex-column-value.only_int', function () {
            this.value = this.value.replace(/[^0-9-]/g, '');
        });

        self.$element.on('input', 'input.ex-column-value.only_float', function () {
            this.value = this.value.replace(/[^0-9.-]/g, '');
            this.value = this.value.replace(/(\..*)\./g, '$1');
        });

        self.$element.on("change", ".ex-function-external", function () {
            $input = $(this).closest(".ex-content").find(".ex-function-name");
            $input.attr("readonly", !this.checked);

            if (this.checked) {
                $input.data('id', undefined).removeData('id');
                $input.val('');
                var treeView = $(this).closest(".ex-splitter").find(".ex-tw-functions").data("kendoTreeView");

                if (treeView)
                    treeView.select({});
            }
        });

        self.$element.on("click", "span.ex-join-group", function () {
            var $el = $(this).closest(".ex-treeview");
            if ($el.length) {
                var treeView = $el.data("kendoTreeView");

                if (treeView)
                    addGroup(treeView);
            }
        });

        self.$element.on("click", "span.ex-join-register", function () {
            SelectReestrWindow($(this));
        });

        self.$element.on("click", ".ex-add-query", function () {
            AddQueryWindow($(this), $(this).data("item"), "saveQuery");
        });

        self.$element.on("click", ".ex-add-condition-if", function () {
            var $el = $(this).closest(".ex-dialog-column").find(".ex-treeview");
            if ($el.length) {
                var treeView = $el.data("kendoTreeView");

                if (treeView) {
                    treeView.select($());
                    addConditionIf(treeView);
                }
            }
        });

        self.$element.on("click", ".ex-add-condition-switch", function () {
            var $el = $(this).closest(".ex-dialog-column").find(".ex-treeview");
            if ($el.length) {
                var treeView = $el.data("kendoTreeView");

                if (treeView) {
                    treeView.select($());
                    addConditionSwitch(treeView);
                }
            }
        });

        self.$element.on('click', '.ex-condition', function () {
            var data;
            var secondColumnData
            var $this = $(this);

            var treeView = $this.closest(".ex-treeview").data("kendoTreeView");
            if (treeView) {
                var node = $this.closest("li.k-item");
                var $dataItem = treeView.dataItem(node);
                var $rootDataItem = treeView.dataItem(treeView.element.find(".k-item:first"));
            }

            if ($dataItem) {
                if ($this.hasClass("ex-condition-left")) {
                    data = $dataItem.RightOperand;
                    secondColumnData = $rootDataItem ? $rootDataItem.ValueOperand : null;
                } else if ($this.hasClass("ex-condition-right")) {
                    data = $dataItem.RightOperand;
                    secondColumnData = $dataItem.LeftOperand;
                } else if ($this.hasClass("ex-condition-value")) {
                    data = $dataItem.ValueOperand;
                } else {
                    data = $dataItem.LeftOperand;
                    secondColumnData = $dataItem.RightOperand;
                }
            }

            if (secondColumnData && !secondColumnData.New) {
                if (secondColumnData.ReferenceId && secondColumnData.AttributeType == "Code" && data.EnterType != "PARAM") {
                    data.ExternalReferenceId = secondColumnData.ReferenceId;
                    data.ExternalAttributeType = secondColumnData.AttributeType;
                    data.EnterType = "REFERENCE";
                } else {
                    data.ConstantType = secondColumnData.ConstantType;
                }
            }

            AddColumnWindow(this, "saveFiltrColumn", data);
        });

        self.$element.on("click", ".ex-edit-column", function () {
            var grid = $(this).closest(".k-grid").data("kendoGrid");
            grid.select($(this).closest("tr"));
            var data = grid.dataItem($(this).closest("tr"));

            AddColumnWindow($(this), "saveSelectColumn", data);
        });

        self.$element.on("click", ".ex-columns-grid .k-grid-delete", function (e) {
            e.preventDefault();
            var $grid = $(this).closest("div.ex-columns-grid").data("kendoGrid");
            if ($grid)
                $grid.select($(this).closest('tr'));

            gridRowDelete($grid);
        });

        /* end */

        // удаляет все kendo элементы
        self.destroy = function () {
            if (!$.isEmptyObject(self.elements)) {
                $.each(self.elements, function (key, value) {
                    self.elements[key].remove();
                });
            }
        }

        // получить следующий идентификатор
        self.getNextId = function () {
            self.idNumerator++;
            return self.idNumerator;
        }

        // положить объект в буфер
        function copyToClipboard(node, type) {
            var treeView = node.closest(".ex-treeview").data("kendoTreeView");

            if (treeView) {
                var dataItem = treeView.dataItem(node);
                if (dataItem) {
                    var $value;
                    switch (type) {
                        case 'copy':
                            type = 'group';
                            $value = cloneObj(dataItem._childrenOptions.data);
                            break;
                        case 'copy_condition':
                            type = 'condition';
                            $value = cloneObj(dataItem._childrenOptions.data);
                            break;
                        case 'copy_left_operand':
                            type = 'column';
                            $value = cloneObj(dataItem.LeftOperand);
                            break;
                        case 'copy_right_operand':
                            type = 'column';
                            $value = cloneObj(dataItem.RightOperand);
                            break;
                        case 'copy_column':
                            type = 'column';
                            $value = cloneObj(dataItem.ValueOperand);
                            break;
                    }

                    $value.id = self.getNextId();
                    window.top.bufer = { value: $value, type: type };
                }
            }
        }

        // вставить объект из буфера
        function pasteFromClipboard(node, type) {
            var treeView = node.closest(".ex-treeview").data("kendoTreeView");

            if (treeView && window.top.bufer != undefined) {
                var dataItem = treeView.dataItem(node);
                if (dataItem) {
                    switch (type) {
                        case 'paste':
                            if (window.top.bufer.type == 'group' || window.top.bufer.type == 'condition')
                                addNode(treeView, window.top.bufer.value, true);
                            break;
                        case 'paste_left_operand':
                            if (window.top.bufer.type == 'column')
                                dataItem.LeftOperand = window.top.bufer.value;
                            break;
                        case 'paste_right_operand':
                            if (window.top.bufer.type == 'column')
                                dataItem.RightOperand = window.top.bufer.value;
                            break; paste_column
                        case 'paste_column':
                            if (window.top.bufer.type == 'column')
                                dataItem.ValueOperand = window.top.bufer.value;
                            break;
                    }
                    dataItem.dirty = true;
                    treeView.dataSource.sync();
                }
            }
        }

        // делаем глубокую копию объекта
        function cloneObj(obj) {
            return JSON.parse(JSON.stringify(obj));
        }

        // очистить фильтр
        function emptyStruct() {
            self.$element.find("input.ex-ddl-main-register").data("kendoDropDownList").select(0);
            self.$element.find("input.ex-ddl-join-type").data("kendoDropDownList").select(0);
            self.$element.find("input.ex-package-size").val(0);
            self.$element.find("input.ex-package-index").val(0);
            self.$element.find("input.ex-cb-distinct").prop("checked", false);
            self.$element.find("input.ex-cb-manual-join").prop("checked", false);
            self.$element.find('.ex-columns-grid').data('kendoGrid').dataSource.data([]);  
            self.$element.find('.ex-tw-condition').data('kendoTreeView').dataSource.data([]);

            var joinsTreeView = self.$element.find('.ex-tw-joins').data('kendoTreeView');
            joinsTreeView.dataSource.data([]);
            addJoin(joinsTreeView);

            self.$element.find('.ex-lw-order-by').data('kendoListView').dataSource.data([]);
            self.$element.find('.ex-lw-groupby').data('kendoListView').dataSource.data([]);
        }

        // получить структуру запроса
        self.getStruct = function () {
            var struct = {
                MainRegisterID: getKendoDropDownListValue(".ex-ddl-main-register", self.$element, "input"),
                JoinType: getKendoDropDownListValue(".ex-ddl-join-type", self.$element, "input"),
                PackageSize: self.$element.find("input.ex-package-size").val(),
                PackageIndex: self.$element.find("input.ex-package-index").val(),
                Distinct: self.$element.find("input.ex-cb-distinct").prop("checked"),
                ManualJoin: self.$element.find("input.ex-manual-join").prop("checked"),
                Columns: getColumnsList(),
                Joins: self.editorType == "Column" || self.editorType == "VirtualColumn" ? [] : getJoinStruct(),
                Filter: getFilterStruct(),
                Sort: getSortStruct(),
                Group: getGroupStruct()
            };
            
            return struct;
        }

        // Получить список колонок запроса
        function getColumnsList() {
            var $columns = [];

            var $columnsGrid = self.$element.find('.ex-columns-grid').data('kendoGrid');
            if ($columnsGrid)
                $columns = $columnsGrid.dataSource.data();

            return $columns;
        }

        // Получить струтуру фильтра запроса
        function getFilterStruct(parent) {
            var $filter = [];

            var $filterTreeView = self.$element.find('.ex-tw-condition').data('kendoTreeView');
            if ($filterTreeView)
                $filter = $filterTreeView.dataSource.data();

            return $filter;
        }

        // Получить список объединений запроса
        function getJoinStruct() {
            var $joinList = [];

            var $joinTreeView = self.$element.find('.ex-tw-joins').data('kendoTreeView');
            if ($joinTreeView)
                $joinList = $joinTreeView.dataSource.data();

            return $joinList;
        }

        // Получить сортировку запроса
        function getSortStruct() {
            var $sort = [];

            var $sortListView = self.$element.find('.ex-lw-order-by').data('kendoListView');
            if ($sortListView)
                $sort = $sortListView.dataSource.data();

            return $sort;
        }

        // Получить группировки запроса
        function getGroupStruct() {
            var $group = [];

            var $groupListView = self.$element.find('.ex-lw-groupby').data('kendoListView');
            if ($groupListView)
                $group = $groupListView.dataSource.data();

            return $group;
        }

        function saveColumn(e) {
            var $item = e.sender.element.closest(".ex-dialog-column");
            let $target = $item.data("target");
            var $saveMethod = $item.data("save-column");

            var $id = $item.find(".ex-column-id").val();
            var $columnType = e.sender.getSelectedFromGroup("column_type_group").data("button").options.id;
            var $alias = $item.find(".ex-tb-column-name").val();
            var $enterType = getKendoDropDownListValue(".ex-ddl-constant-enter-type", $item);
            var $parameterName = $item.find(".ex-parameter-name").length ? $item.find(".ex-parameter-name").val() : undefined;
            var $sourceEnterType = getKendoDropDownListValue(".ex-ddl-source-enter-type", $item);
            var $dataType = getKendoDropDownListValue(".ex-ddl-data-type", $item);
            var $dataSource = getKendoDropDownListValue(".ex-data-source", $item);
            var $constantType = getKendoDropDownListValue(".ex-ddl-constant-type", $item);
            var $value = $item.find(".ex-column-value").length ? $item.find(".ex-column-value:not(span)").val() : undefined;

            var $referenceId;
            var $referenceItemId = getKendoDropDownListValue(".ex-ddl-reference-list", $item, "input");
            var $referenceItem = getKendoDropDownListValue(".ex-ddl-reference-list", $item, "input", "text");
            var $enterFromReference = getKendoDropDownListValue(".ex-ddl-enter-from-reference", $item, "input");
            var $enterFromRegister = getKendoDropDownListValue(".ex-ddl-enter-from-register", $item, "input");

            var $specialColumnRegister = getKendoDropDownListValue(".ex-ddl-special-column-register", $item, "input");
            var $specialColumnType = getKendoDropDownListValue(".ex-ddl-special-column-type", $item, "input");
            var $specialColumnTypeItem = getKendoDropDownListValue(".ex-ddl-special-column-type", $item, "input", "text");

            var $attributeType = getKendoDropDownListValue(".ex-ddl-attribute-type", $item);
            var $attributeId;
            var $attributeName;
            var attributesTreeView = $item.find(".ex-tw-attributes").data("kendoTreeView");

            if (attributesTreeView) {
                let node = getSelectedNode(attributesTreeView);
                let $dataItem = attributesTreeView.dataItem(node);

                if ($dataItem) {
                    $attributeId = $dataItem.id;
                    $attributeName = $dataItem.text;
                    $referenceId = $dataItem.referenceId;
                    $constantType = $dataItem.type;
                }
            }

            var $function = $item.find(".ex-function-name").length ? $item.find(".ex-function-name").val() : undefined;
            var $functionId;
            var $functionParameters = getFunctionParameters($item);
            var functionsTreeView = $item.find(".ex-tw-functions").data("kendoTreeView");

            if (functionsTreeView) {
                let node = getSelectedNode(functionsTreeView);
                let $dataItem = functionsTreeView.dataItem(node);
                var $functionId = $dataItem.id;
            }

            var $functionExternal = $item.find(".ex-function-external").length ? $item.find(".ex-function-external").prop("checked") : undefined;
            var $queryLevel = getKendoDropDownListValue(".ex-ddl-query-level", $item, "input");

            var $analyticFunctonType = getKendoDropDownListValue(".ex-ddl-analytic-function-type", $item, "input");
            var $analyticFuncton;
            var analyticFnctionTreeView = $item.find(".ex-tw-analytic-function").data("kendoTreeView");
            if (analyticFnctionTreeView) {
                var $analyticFuncton = analyticFnctionTreeView.dataSource.data();
            }

            var $subQuery = $item.find(".ex-add-query").data("item");

            var $conditionIf;
            var treeViewIf = $item.find(".ex-tw-condition-if").data("kendoTreeView");
            if (treeViewIf) {
                $conditionIf = treeViewIf.dataSource.data();
            }

            var $conditionSwitch;
            var treeViewSwitch = $item.find(".ex-tw-condition-switch").data("kendoTreeView");
            if (treeViewSwitch) {
                $conditionSwitch = treeViewSwitch.dataSource.data();
            }

            var $new = $item.data("new") ? $item.data("new") : undefined;

            if ($item && $target && $saveMethod) {
                var data = {
                    id: $id ? $id : undefined,
                    Alias: $alias ? $alias : undefined,
                    AttributeType: $attributeType ? $attributeType : undefined,
                    AttributeId: $attributeId ? $attributeId : undefined,
                    AttributeName: $attributeName ? $attributeName : undefined,
                    ColumnType: $columnType ? $columnType : undefined,
                    ReferenceId: $referenceId ? $referenceId : undefined,
                    ReferenceItemId: $referenceItemId ? $referenceItemId : undefined,
                    ReferenceItem: $referenceItem ? $referenceItem : undefined,
                    SpecialColumnRegister: $specialColumnRegister ? $specialColumnRegister : undefined,
                    SpecialColumnType: $specialColumnType ? $specialColumnType : undefined,
                    SpecialColumnTypeItem: $specialColumnTypeItem ? $specialColumnTypeItem : undefined,
                    Function: $function ? $function : undefined,
                    FunctionParameters: $functionParameters,
                    FunctionId: $functionId ? $functionId : undefined,
                    FunctionExternal: $functionExternal ? $functionExternal : undefined,
                    AnalyticFunctonType: $analyticFunctonType ? $analyticFunctonType : undefined,
                    AnalyticFunction: $analyticFuncton ? $analyticFuncton : undefined,
                    EnterType: $enterType ? $enterType : undefined,
                    ParameterName: $parameterName ? $parameterName : undefined,
                    SourceEnterType: $sourceEnterType ? $sourceEnterType : undefined,
                    EnterFromReference: $enterFromReference ? $enterFromReference : undefined,
                    EnterFromRegister: $enterFromRegister ? $enterFromRegister : undefined,
                    DataType: $dataType ? $dataType : undefined,
                    DataSource: $dataSource ? $dataSource : undefined,
                    ConstantType: $constantType ? $constantType : undefined,
                    Value: $value ? $value : undefined,
                    SubQuery: $subQuery ? $subQuery : undefined,
                    ConditionIf: $conditionIf ? $conditionIf : undefined,
                    ConditionSwitch: $conditionSwitch ? $conditionSwitch : undefined,
                    QueryLevel: $queryLevel ? $queryLevel : undefined,
                    OrderType: undefined,
                    Group: undefined,
                    New: $new
                };

                data.Title = getColumnText(data);
                data.TitleValue = getTitleValue(data);


                if ($new) {
                    self.nameNumerator++;
                    data.id = self.getNextId();
                }

                methods[$saveMethod]($target, data);

                if ($item.data("kendoWindow"))
                    $item.data("kendoWindow").close();
            }
        }

        function getColumnText(data) {
            var text = "";

            if (data) {
                var text = data.Alias;

                switch (data.ColumnType) {
                    case "QSColumnConstant":
                        if (data.EnterType == "MANUAL") {
                            if (data.ConstantType == "BOOLEAN")
                                text = "{0} ({1})".format(data.Alias, data.Value && data.Value == "True" ? "Да" : 'Нет');
                            else
                                text = "{0} ({1})".format(data.Alias, data.Value ? data.Value : '');
                        }
                        else if (data.EnterType == "REFERENCE")
                            text = "{0} ({1})".format(data.ReferenceItem, data.ReferenceItemId);
                        else if (data.EnterType == "PARAM")
                            text = "Параметр {0}".format(data.ParameterName);
                        break;
                    case "QSColumnSimple":
                        text = "{0} ({1})".format(data.AttributeName, data.AttributeId);
                        break;
                    case "QSColumnSpecial":
                        text = "Специальная колонка {0}".format(data.SpecialColumnTypeItem);
                        break;
                    case "QSColumnFunctionAnalytic":
                        text = "Аналитическая функция";
                        break;
                    case "QSColumnQuery":
                        text = "<Подзапрос>";
                        break;
                    case "QSColumnIf":
                        text = "<Условие IF>";
                        break;
                    case "QSColumnSwitch":
                        text = "<Условие Switch>";
                        break;  
                }
            }

            return text;
        }

        function getTitleValue(data) {
            var titleValue = "";

            if (data) {
                switch (data.ColumnType) {
                    case "QSColumnConstant":
                        if (data.EnterType == "MANUAL") {
                            if (data.ConstantType == "BOOLEAN")
                                titleValue = data.Value == "True" ? "Да" : 'Нет';
                            else
                                titleValue = data.Value ? data.Value : '';
                        }
                        else if (data.EnterType == "REFERENCE")
                            titleValue = "{0} ({1})".format(data.ReferenceItem, data.ReferenceItemId);
                        else if (data.EnterType == "PARAM")
                            titleValue = "Параметр {0}".format(data.ParameterName);
                        break;
                    case "QSColumnSimple":
                        titleValue = "{0} ({1})".format(data.AttributeName, data.AttributeId);
                        break;
                    case "QSColumnSpecial":
                        titleValue = "Специальная колонка {0}".format(data.SpecialColumnTypeItem);
                        break;
                    case "QSColumnFunction":
                        var functionParameters = '';
                        if (data.FunctionParameters.length) {
                            $.each(data.FunctionParameters, function (index, value) {
                                functionParameters += (index !== 0 ? ', ' : '') + value.TitleValue;
                            });
                        }

                        titleValue = "функция {0}".format(data.Function) + (functionParameters ? ' ({0})'.format(functionParameters) : '');
                        break;
                    case "QSColumnFunctionAnalytic":
                        titleValue = "Аналитическая функция";
                        break;
                    case "QSColumnQuery":
                        titleValue = "<Подзапрос>";
                        break;
                    case "QSColumnIf":
                        titleValue = "<Условие IF>";
                        break;
                    case "QSColumnSwitch":
                        titleValue = "<Условие Switch>";
                        break;                    
                }
            }

            return titleValue;
        }

        function getFunctionParameters(el) {
            var result = [];

            if (el.length) {
                var listView = el.find('.ex-lw-function-parameters').data('kendoListView');

                if (listView)
                    result = listView.dataSource.data();
            }

            return result;
        }

        function deleteListViewItem(el, text) {
            if (el.length && text)
            {
                Common.UI.ShowConfirm({
                    title: 'Подтверждение',
                    content: text,
                    onSuccess: function (e) {
                        var listView = el.data("kendoListView");
                        if (listView) {
                            var row = listView.select();
                            var dataItem = listView.dataItem(row);

                            if (dataItem)
                                listView.dataSource.remove(dataItem);
                        }
                    }
                });
            }
        }

        function saveListViewItem(el, funcName) {
            if (el.length) {
                var listView = el.closest(".ex-content").find(".k-listview").data("kendoListView");

                if (listView) {
                    var selected = listView.select();
                    var data = listView.dataItem(selected);
                    AddColumnWindow(el, funcName, data);
                }
            }
        }

        //Обновить dataItem Kendo-элемента
        function updateDataItem(dataItem, data) {
            if (dataItem && data && !$.isEmptyObject(dataItem)) {
                $.each(data, function (key, value) {
                    if (data.hasOwnProperty(key))
                        dataItem.set(key, value);
                })
            }
        }

        //Получить значение выделенного элемента Kendo DropDownList
        function getKendoDropDownListValue(el, container, input, type) {
            if (container && typeof container == "string")
                container = $(container);

            if (!input)
                input = "select";

            var $item = container ? container.find(input + el) : $(input + el);
            var $ddl = $item.length ? $item.data("kendoDropDownList") : undefined;

            return $ddl ? type == "text" ? $ddl.text() : $ddl.value() : undefined;
        }

        function gridRowDelete(grid) {
            if (grid) {
                Common.UI.ShowConfirm({
                    title: 'Подтверждение',
                    content: 'Вы действительно хотите удалить эту колонку?',
                    onSuccess: function () {
                        grid.removeRow(grid.select());
                    }
                });
            }
        }

        function getTreeViewDataItem(el) {
            var $dataItem = null;

            if (el.length) {
                var treeView = el.closest(".ex-treeview").data("kendoTreeView");

                if (treeView) {
                    var node = el.closest("li.k-item");
                    $dataItem = treeView.dataItem(node);
                }
            }

            return $dataItem;
        }

        function AddColumnWindow(el, func, data) {
            var $dialogColumn = $($(self.DialogColumnTemplate).html());

            var dialog = $dialogColumn.kendoWindow({
                actions: ["Close"],
                height: "500px",
                width: "1100px",
                appendTo: self.$element,
                modal: true,
                visible: false,
                resizable: false,
                close: function (e) {
                    kendo.destroy(this.element);
                    this.destroy();
                }
            }).data('kendoWindow');

            if (data && !data.New) {
                $dialogColumn.data("column", data);
            }

            RenderTemplate(self.ModalColumnTemplate, $dialogColumn, data);
            InitColumnToolbarValues($dialogColumn, data);

            $dialogColumn.data("target", $(el));
            $dialogColumn.data("save-column", func);

            dialog.center().open();
        }

        function SelectReestrWindow(el) {
            var $dialogTemplate = $($("#ex-dialog-template").html());
            $dialogTemplate.data("target", $(el));

            var $dataItem = getTreeViewDataItem(el);
            var data = $dataItem ? $dataItem : undefined;

            var dialog = $dialogTemplate.kendoWindow({
                height: "300px",
                width: "550px",
                draggable: false,
                visible: false,
                resizable: false,
                appendTo: self.$element,
                position: {
                    top: el.offset().top + 22,
                    left: el.offset().left - 9
                },
                close: function (e) {
                    this.destroy();
                }
            }).data('kendoWindow');

            RenderTemplate(self.DialogReestrTemplate, $dialogTemplate, data);
            dialog.open();
        }

        function AddQueryWindow(el, filterData, saveFunc) {
            var $dialogTemplate = $($(self.DialogTemplate).html());
            $dialogTemplate.data("target", $(el));
            $dialogTemplate.data("save-query", saveFunc);

            var $filterQuery = $dialogTemplate.filterQueryBuilderExtended(
                {
                    registerViewId: self.registerViewId,
                    saveUrl: self.saveUrl,
                    saveParams: self.saveParams,
                    deleteUrl: self.deleteUrl,
                    attributesUrl: self.attributesUrl,
                    referencesUrl: self.referencesUrl,
                    referenceListUrl: self.referenceListUrl,
                    reestrListUrl: self.reestrListUrl,
                    joinTypeListUrl: self.joinTypeListUrl,
                    functionsListUrl: self.functionsListUrl,
                    analyticFunctionTypeListUrl: self.analyticFunctionTypeListUrl,
                    columnSpecialTypeListUrl: self.columnSpecialTypeListUrl,
                    filter: filterData,
                    window: true,
                    idNumerator: self.idNumerator,
                    nameNumerator: self.nameNumerator,
                    level: self.level,
                    editorType: "Query"
                }).data("filterQueryBuilderExtended");

            var dialog = $dialogTemplate.kendoWindow({
                actions: ["Close"],
                title: "Подзапрос",
                modal: true,
                visible: false,
                resizable: false,
                close: function (e) {
                    $filterQuery.destroy();
                    this.destroy();
                }
            }).data('kendoWindow');

            dialog.maximize().open();
        }

        function setSort(sort) {
            if (sort) {
                var orderbyListView = self.$element.find(".ex-lw-order-by").data("kendoListView");
                var orderbyNode = orderbyListView.select();
                var orderbyDataItem = orderbyListView.dataItem(orderbyNode);

                if (orderbyDataItem.OrderType === sort) {
                    orderbyDataItem.OrderType = undefined;
                    orderbyNode.find("span.label-sort").remove();
                }
                else {
                    if (!orderbyDataItem.OrderType)
                        orderbyNode.append("<span class='label label-sort' style='margin-left: 5px; position: relative; top: -2px'>" + sort + "</span>");
                    else
                        orderbyNode.find("span.label-sort").text(sort);

                    orderbyDataItem.OrderType = sort;
                }
            }
        }

        function restoreSort(node) {
            if (node) {
                var orderbyListView = self.$element.find(".ex-lw-order-by").data("kendoListView");
                if (orderbyListView) {
                    var orderbyDataItem = orderbyListView.dataItem(node);

                    if (orderbyDataItem.OrderType)
                        node.append("<span class='label label-sort' style='margin-left: 5px; position: relative; top: -2px'>" + orderbyDataItem.OrderType + "</span>");
                }
            }
        }

        function setValidation(container, type, data) {
            if (container) {
                container.empty();
                var $input;

                switch (type) {
                    case "INTEGER":
                        $input = $('<input type="text" class="k-textbox ex-column-value only_int" style="width: 100%;" data-type-element="tb" data-element-role="Value" />');
                        container.append($input);
                        break;
                    case "DECIMAL":
                        $input = $('<input type="text" class="k-textbox ex-column-value only_float" style="width: 100%;" data-type-element="tb" data-element-role="Value" />');
                        container.append($input);
                        break;
                    case "BOOLEAN":
                        RenderTemplate(self.ModalColumnConstantBooleanTemplate, container, data);
                        $input = container.find('select.ex-column-value');
                        break;
                    case "STRING":
                        $input = $('<input type="text" class="k-textbox ex-column-value" style="width: 100%;" data-type-element="tb" data-element-role="Value" />');
                        container.append($input);
                        break;
                    case "MANUAL":
                        RenderTemplate(self.ModalColumnConstantManualTemplate, container, data);
                        break;
                    case "PARAM":
                        RenderTemplate(self.ModalColumnConstantParameterTemplate, container, data);
                        break;
                    case "DATE":
                        $input = $('<input type="text" class="ex-column-value" style="width: 100%;" data-type-element="datepicker" data-element-role="Value" />');
                        container.append($input);
                        $input.kendoDatePicker({
                            format: "dd.MM.yyyy"
                        });
                        break;
                    case "Reference": // Параметр -> Ввод из справочника
                        $input = $('<input class="ex-ddl-enter-from-reference" data-init="initReferenceDdl" style="width: 100%;" data-element-role="EnterFromReference" />');
                        RenderTemplate($input, container, data);
                        break;
                    case "Register": // Параметр -> Ввод из реестра
                        $input = $('<input class="ex-ddl-enter-from-register" data-init="initRegisterListDdl" style="width: 100%;" data-element-role="EnterFromRegister" />');
                        RenderTemplate($input, container, data);
                        break;
                    case "REFERENCE":
                        if (data && data.ExternalReferenceId && data.ExternalAttributeType)
                            getReferences(data.ExternalReferenceId, container, data.ReferenceItemId);
                        break;
                }

                if (!$.isEmptyObject(data) && (type == "INTEGER" || type == "DECIMAL" || type == "BOOLEAN" || type == "STRING" || type == "DATE"))
                    SetValue($input, $input.data("type-element"), data[$input.data("element-role")]);
            }
        }

        function filterAttributes(dataSource, query) {
            var hasVisibleChildren = false;
            var data = dataSource instanceof kendo.data.HierarchicalDataSource && dataSource.data();

            for (var i = 0; i < data.length; i++) {
                var item = data[i];
                var text = item.text.toLowerCase();
                var itemVisible =
                    query === true
                    || query === ""
                    || text.indexOf(query) >= 0;

                var anyVisibleChildren = filterAttributes(item.children, itemVisible || query);

                hasVisibleChildren = hasVisibleChildren || anyVisibleChildren || itemVisible;

                item.hidden = !itemVisible && !anyVisibleChildren;
            }

            if (data) {
                dataSource.filter({ field: "hidden", operator: "neq", value: true });
            }

            return hasVisibleChildren;
        }

        function getSelectedNode(treeView) {
            var selectedNode = treeView.select();

            if (!selectedNode[0]) {
                selectedNode = treeView.wrapper.find("li.k-item").first();
            }

            return selectedNode;
        }

        function deleteNode(treeView) {
            treeView.remove(getSelectedNode(treeView));
        }

        function addJoin(treeView) {
            addNode(treeView, CreateEmptyJoinElement());
        }

        function CreateEmptyJoinElement() {
            return {
                id: self.getNextId(), text: "Дополнительная настройка объединения", Type: "join", expanded: true, items: [
                    {
                        id: self.getNextId(), Type: "group", Operator: "And", OperatorText: "И", expanded: true, items: [
                            {
                                id: self.getNextId(),
                                Type: "condition",
                                Condition: "Equal",
                                ConditionText: "Равно",
                                LeftOperand: { TitleValue: '<Выберите...>', New: true },
                                RightOperand: { TitleValue: '<Выберите...>', New: true },
                                selected: false
                            }
                        ]
                    }
                ]
            };
        }

        function CreateEmptyConditionElement() {
            return {
                id: self.getNextId(),
                Type: "condition",
                Condition: "Equal",
                ConditionText: "Равно",
                LeftOperand: { TitleValue: '<Выберите...>', New: true },
                RightOperand: { TitleValue: '<Выберите...>', New: true },
                selected: false
            };
        }

        function addCondition(treeView) {
            addNode(treeView, CreateEmptyConditionElement(), true);
        }

        function addConditionIf(treeView) {
            if (treeView) {
                var dataItem = [
                    {
                        id: self.getNextId(),
                        Operator: "And",
                        OperatorText: "И",
                        Type: "groupIf",
                        expanded: true,
                        selected: false,
                        items: [{
                            id: self.getNextId(),
                            Type: "columnIfResult",
                            ValueOperand: { TitleValue: '<Выберите...>', New: true },
                            selected: false
                        }]
                    }
                ];

                //addNode(treeView, dataItem);
                treeView.insertBefore(dataItem, treeView.element.find("li:has(span.ex-condition-else)").last());
            }
        }

        function addConditionSwitch(treeView) {
            var dataItem = {
                id: self.getNextId(),
                Type: "columnSwitch",
                LeftOperand: { TitleValue: '<Выберите...>', New: true },
                RightOperand: { TitleValue: '<Выберите...>', New: true },
                selected: false
            };

            addNode(treeView, dataItem, true);
        }

        function addGroup(treeView, root) {
            root = (typeof root !== 'undefined') ? root : false;
            addNode(treeView, CreateEmptyGroupElement(root), true);
        }

        function CreateEmptyGroupElement(root) {
            root = (typeof root !== 'undefined') ? root : false;

            var group = {
                id: self.getNextId(),
                Operator: "And",
                OperatorText: "И",
                Type: "group",
                selected: false,
                items: []
            };

            if (root) {
                group.root = true;
                group.expanded = true;
            }

            return group;
        }

        function addNode(treeView, data, toSelectedNode) {
            toSelectedNode = (typeof toSelectedNode !== 'undefined') ? toSelectedNode : false;

            if (treeView) {
                if (toSelectedNode)
                    treeView.append(data, getSelectedNode(treeView));
                else
                    treeView.append(data);
            }
        }

        function ShowContent(container, typeContent, data) {
            switch (typeContent) {
                case "QSColumnConstant": RenderTemplate(self.ModalColumnConstantTemplate, container, data);
                    break;
                case "QSColumnSimple": RenderTemplate(self.ModalColumnAttributeTemplate, container, data);
                    break;
                case "QSColumnSpecial": RenderTemplate(self.ModalColumnSpecialTemplate, container, data);
                    break;
                case "QSColumnFunction": RenderTemplate(self.ModalColumnFunctionTemplate, container, data);
                    break;
                case "QSColumnFunctionAnalytic": RenderTemplate(self.ModalColumnAnalyticFunctionTemplate, container, data);
                    break;
                case "QSColumnQuery": RenderTemplate(self.ModalColumnSubqueryTemplate, container, data);
                    break;
                case "QSColumnIf": RenderTemplate(self.ModalColumnConditionIfTemplate, container, data);
                    break;
                case "QSColumnSwitch": RenderTemplate(self.ModalColumnConditionCaseTemplate, container, data);
                    break;
            }
        }
        
        function InitColumnToolbarValues($dialogColumn, data) {
            var container = $dialogColumn.find(".ex-modal-content");

            if (data && data.ColumnType) {
                var toolbar = $dialogColumn.find('.ex-toolbar-column-type').data("kendoToolBar");
                if (toolbar)
                    toolbar.toggle($dialogColumn.find("#" + data.ColumnType), true);
            }

            if (data && !$.isEmptyObject(data) && !data.New) {
                ShowContent(container, data.ColumnType, data);
            }
            else {
                $dialogColumn.data("new", true);
                RenderTemplate(self.ModalColumnConstantTemplate, container, data);
            }
        }

        function RenderTemplate(template, container, data) {
            if (typeof container == "string")
                container = $(container);

            ToggleTemplate(template, container);

            if (!data || data.New) {
                if (!data)
                    data = { Alias: columnNameTemplate + self.nameNumerator, New: true }
                else
                    data.Alias = columnNameTemplate + self.nameNumerator;
            }

            SetDataSource(container, data);
            SetElementsValues(container, data);

            var $actionAfterSetValues = container.find("[data-after-set-value]");
            ExecuteMethods($actionAfterSetValues, "after-set-value");
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
            if ($elements.length) {
                $elements.each(function () {
                    var $el = $(this);
                    if (!$el.attr("id")) {
                        $el.attr("id", "ex-el-" + self.getNextId());
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

        function getParameters($elem) {
            var params = {};
            var data = $elem.data();
            for (var prop in data) {
                //ищем параметры
                if (/param/.test(prop)) {
                    var res = prop.substr(5, prop.length).toLowerCase();

                    if (/transition([0-9]+)/.test(res)) {
                        params[res.substring(10, res.length)] = data[prop];
                    }
                    else {
                        params[res] = data[prop];
                    }
                }
            }
            return params;
        };

        function SetDataSource(container, data) {
            if (container && data) {
                if (!$.isEmptyObject(data)) {
                    var elements = container.find("[data-datasource]")

                    if (elements.length) {
                        elements.each(function () {
                            var $el = $(this);
                            var dataSource = data[$el.data("datasource")];
                            var $type = $el.data("type-element");
                            var kendoElement;

                            switch ($type) {
                                case "treeView":
                                    kendoElement = $el.data("kendoTreeView");     
                                    break;
                                case "listView":
                                    kendoElement = $el.data("kendoListView");
                                    break;
                                case "grid":
                                    kendoElement = $el.data("kendoGrid");                                    
                                    break;  
                            }

                            if (kendoElement) {
                                if (dataSource && dataSource.length > 0) {
                                    var $dataSource = jQuery.extend(true, [], dataSource);

                                    kendoElement.setDataSource($dataSource);
                                    if ($type == "listView")
                                        kendoElement.dataSource.read();
                                    if ($type == "treeView")
                                        kendoElement.expand(".k-item");
                                }
                            }
                        });
                    }
                }
            }
        }

        function SetElementsValues(container, data) {
            if (container && data) {
                if (!$.isEmptyObject(data)) {
                    var elements = container.find("[data-element-role]")
                        .filter(function (i, item) {
                            return !$(item).data("set");
                        })
                        .sort(function (a, b) {
                            var priority_a = $(a).data('priority') ? $(a).data('priority') : 10;
                            var priority_b = $(a).data('priority') ? $(b).data('priority') : 10;
                            return priority_a > priority_b;
                        });

                    if (elements.length) {
                        elements.each(function () {
                            var $el = $(this);
                            var $value = data[$el.data("element-role")];
                            var $type = $el.data("type-element");

                            if ($value) {
                                SetValue($el, $type, $value, data);
                                $el.data("set", "true");

                                if ($type == "ddl" && $el.data("toggle-template"))
                                    return false;
                            }
                        });
                    }
                }
            }
        }

        function SetValue(el, type, value, data) {
            if (el && type && value) {
                switch (type) {
                    case "tb":
                        el.val(value);
                        break;
                    case "datepicker":
                        var $datepicker = el.data("kendoDatePicker");
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

                            if (el.data("toggle-template")) {
                                var $container = el.closest(".ex-modal-column-constant-content")
                                    .find(value == "PARAM" || value == "MANUAL" || value == "REFERENCE" ?
                                        ".ex-value-container" :
                                        ".ex-value");
                                setValidation($container, value, data);
                            }
                        }
                        break;
                    case "checkbox":
                        el.prop("checked", value).trigger('change');;
                        break;
                    case "treeView":
                        var treeview = el.data("kendoTreeView");
                        if (treeview) {
                            if (treeview.dataSource.data().length == 0) {
                                treeview.dataSource.read().then(function () {
                                    treeViewSetValue(treeview, value);
                                });
                            }
                            else {
                                treeViewSetValue(treeview, value);
                            }
                        }
                        break;
                    case "listView":
                        var listView = el.data("kendoListView");
                        if (listView)
                            listView.dataSource.data().push.apply(listView.dataSource.data(), value);
                        break;
                    case "grid":
                        var grid = el.data("kendoGrid");
                        if (grid)
                            grid.dataSource.data().push.apply(grid.dataSource.data(), value);
                        break;
                    case "toolBar":
                        var toolBar = el.data("kendoToolBar");

                        if (toolBar)
                            toolBar.toggle("#" + value, true);
                        break;
                    case "item":
                        el.data("item", value);
                        el.append('<span class="k-icon k-i-check-outline" style="margin-left: 5px;"></span>');
                        break;
                }
            }
        }

        function treeViewSetValue(treeview, value) {
            var dataItem = treeview.dataSource.get(value);
            var node = treeview.findByUid(dataItem.uid);
            treeview.select(node);

            var parentNode = treeview.parent(node);
            if (parentNode)
                treeview.expand(parentNode);
        }   

        function hideTabs($el, ids) {
            if ($el.length, ids && ids.length) {
                var $tabStrip = $el.data("kendoTabStrip");
                if ($tabStrip) {
                    $.each(ids, function (index, value) {
                        $($tabStrip.items()[value]).attr("style", "display:none");
                    });
                }
            }
        }

        function log(msg, warn) {
            if (window.console) {
                var message = '$.fn.filterQueryBuilderExtended -> ' + msg;
                if (warn) {
                    console.warn(message);
                } else {
                    console.log(message);
                }
            }
        }

        function InitControls() {
            return $.ajax({
                url: self.attributesUrl,
                type: "GET",
                dataType: "html",
                data: { registerViewId: self.registerViewId },
                success: function (data) {
                    if (data) {
                        self.attributes = getAttributesDataSource(jQuery.parseJSON(data));
                        self.level++;

                        if (self.editorType == "Column" || self.editorType == "VirtualColumn") {
                            RenderTemplate(self.ColumnEditorTemplate, self.$element, self.filter);
                            var $dialogColumn = self.$element.find('.ex-dialog-column');
                            $dialogColumn.data("save-column", "saveUserColumn");
                            $dialogColumn.data("target", self.$element);
                            InitColumnToolbarValues($dialogColumn, self.filter);
                        }
                        else
                            RenderTemplate(self.MainTemplate, self.$element, self.filter);

                        if (self.window) {
                            self.$element.find(".filter-container").prepend("<div class='ex-subquery-toolbar'><div>");
                            self.$element.find(".ex-subquery-toolbar").kendoToolBar({
                                items: [
                                    {
                                        type: "button",
                                        text: "Ок",
                                        icon: "check",
                                        click: function () {
                                            var dialog = this.element.closest('.k-window-content').data('kendoWindow');

                                            if (dialog) {
                                                var $target = dialog.element.data("target");
                                                var $saveMethod = dialog.element.data("save-query");
                                                methods[$saveMethod]($target, self.getStruct());
                                                dialog.close();
                                            }
                                        }
                                    }
                                ]
                            });
                        }
                        else if (self.editorType != "Column" && self.editorType != "VirtualColumn") {
                            self.$element.find(".filter-container").prepend("<div class='ex-query-toolbar'><div>");
                            self.$element.find(".ex-query-toolbar").kendoToolBar({
                                items: [
                                    {
                                        id: "btnSaveFiltr",
                                        type: "button",
                                        text: "Сохранить",
                                        icon: "save",
                                        click: function () {
                                            saveFilter();
                                        }
                                    },
                                    {
                                        id: "btnDeleteFiltr",
                                        type: "button",
                                        text: "Удалить",
                                        icon: "delete",
                                        click: function () {
                                            deleteFilter();
                                        }
                                    }
                                ]
                            });
                        }

                        if (self.editorType == "Filter")
                            hideTabs(self.$element.find(".ex-ts-options"), [0, 1, 3, 4, 5]);
                        else if (self.editorType == "Layout")
                            hideTabs(self.$element.find(".ex-ts-options"), [0, 1, 4, 5]);

                        if (self.readOnly)
                            self.$element.prepend('<div style="position: absolute;height: 97%;width: 100%;z-index: 9999"></div>');
                    }
                },
                error: function (request) {
                    log('ошибка загрузки данных, url: ' + self.attributesUrl + ' ' + request.status + ' ' + request.statusText, true);
                }
            });
        };

        function saveFilter() {
            if (!self.saveUrl) {
                Common.ShowError("Не заполнен параметр saveUrl");
                return;
            }

            var struct = self.getStruct();

            $.ajax({
                url: self.saveUrl,
                type: "POST",
                data: $.extend(config.saveParams, { filter: JSON.stringify(struct) }),
                success: function (response) {
                    if (response) {
                        if (response.Errors)
                            Common.ShowError(response.Errors.Message);
                        else
                            Common.ShowMessage("Фильтр сохранен");
                    }
                },
                error: function (request) {
                    log("ошибка сохранения фильтра, url: " + self.saveUrl + " " + request.status + " " + request.statusText, true);
                }
            });
        }

        function deleteFilter() {
            if (!self.deleteUrl) {
                Common.ShowError("Не заполнен параметр deleteUrl");
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
                        },
                        error: function (request) {
                            log('ошибка удаления фильтра, url: ' + self.deleteUrl + ' ' + request.status + ' ' + request.statusText, true);
                        }
                    });
                }
            });
        }

        function getAttributesDataSource(data) {
            var source = [];

            if (data && data.length) {
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
                                    type: item.Type,
                                    DescriptionAttribute: item.DescriptionAttribute,
                                    DataTypeName: item.DataTypeName,
                                    ReferenceName: item.ReferenceName,
                                    IsPrimaryKey: item.IsPrimaryKey,
                                    ForeignKey: item.ForeignKey,
                                    IsVirtual: item.IsVirtual,
                                    ColumnDbName: item.ColumnDbName
                                };
                            });
                        }
                        source.push(child);
                    }
                }
            }

            return source;
        }

        function getReferences(id, $conteiner, value) {
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
                            var $res = $(self.ModalColumnConstantReferenceTemplate).html();
                            $conteiner.html($res);
                            var $input = $conteiner.find(".ex-ddl-reference-list");

                            var $ddl = $input.kendoDropDownList({
                                filter: "contains",
                                dataValueField: "id",
                                dataTextField: "text",
                                dataSource: dataSource.references,
                                noDataTemplate: "Ничего не найдено!"
                            }).data("kendoDropDownList");

                            if (value)
                                $ddl.value(value);
                        }
                    },
                    error: function (request) {
                        log('ошибка загрузки данных, url: ' + self.referencesUrl + ' ' + request.status + ' ' + request.statusText, true);
                    }
                });
            }
        };
    }

    $.fn.filterQueryBuilderExtended = function (config) {
        return this.each(function () {
            if (!$.data(this, "filterQueryBuilderExtended")) {
                $.data(this, "filterQueryBuilderExtended", new filterQueryBuilderExtended(this, config));
            }
        });
    }
})(jQuery);