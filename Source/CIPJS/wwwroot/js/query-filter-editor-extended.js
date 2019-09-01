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
        self.reestrListUrl = config.reestrListUrl;
        self.joinTypeListUrl = config.joinTypeListUrl;
        self.functionsListUrl = config.functionsListUrl;
        self.filter = config.filter;
        self.readOnly = config.readOnly ? config.readOnly.toLowerCase() === "true" : false;
        self.window = config.window ? config.window : false;
        self.MainTemplate = "#ex-main-template"
        self.DialogTemplate = "#ex-dialog-template";
        self.DialogReestrTemplate = "#ex-dialog-reestr-template";
        self.ModalColumnTemplate = "#ex-modal-column-template";
        self.ModalColumnConstantTemplate = "#ex-modal-column-constant-template";
        self.ModalColumnConstantParameterTemplate = "#ex-modal-column-constant-parameter-template";
        self.ModalColumnConstantManualTemplate = "#ex-modal-column-constant-manual-template";
        self.ModalColumnConstantReferenceTemplate = "#ex-modal-column-constant-reference-template";
        self.ModalColumnAttributeTemplate = "#ex-modal-column-attribute-template";
        self.ModalColumnFunctionTemplate = "#ex-modal-column-function-template";
        self.ModalColumnSubqueryTemplate = "#ex-modal-column-subquery-template";
        self.ModalColumnConditionIfTemplate = "#ex-modal-column-condition-if-template";
        self.ModalColumnConditionCaseTemplate = "#ex-modal-column-condition-case-template";
        self.referenceTemplate = config.referenceTemplate ? config.referenceTemplate : "reference-template";
        self.booleanTemplate = config.booleanTemplate ? config.booleanTemplate : "boolean-template";
        self.idNumerator = config.idNumerator ? config.idNumerator : 1;
        self.nameNumerator = config.nameNumerator ? config.nameNumerator : 1;
        var columnNameTemplate = "Колонка_";
        // возможные варианты редактора
        // "filter" - фильтр
        // "column" - колонка - подзапрос 
        // "query"  - полный запрос - редактор без ограничений
        self.editorType = config.editorType ? config.editorType : "filter";

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
            initSourceEnterTypeDdl: function (el) {
                methods.initDropDownList(el);
            },
            initDataTypeDdl: function (el) {
                methods.initDropDownList(el);
            },
            initDataSourceDdl: function (el) {
                methods.initDropDownList(el);
            },
            initMainRegisterDdl: function (el) {
                methods.initDropDownList(el, {
                    filter: "contains",
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
                });
            },
            initJoinMainRegisterDdl: function (el) {
                methods.initDropDownList(el, {
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
                });
            },
            initQueryLevelDdl: function (el) {
                methods.initDropDownList(el);
            },
            initJoinTypeDdl: function (el) {
                methods.initDropDownList(el, {
                    dataTextField: "Text",
                    dataValueField: "Id",
                    dataSource: {
                        type: "json",
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
            initColumnGridToolbar: function (el) {
                methods.initToolbar(el, "addColumn", "saveSelectColumn");
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
                            var $el = e.item.element.closest(".k-content").find(".ex-lw-order-by");
                            if ($el.length) {
                                var listView = $el.data("kendoListView");

                                if (listView) {
                                    var selected = listView.select();
                                    var data = listView.dataItem(selected);
                                    AddColumnWindow(e.item.element, "saveSortColumn", data);
                                }
                            }
                        }
                    },
                    {
                        id: "deleteSort",
                        type: "button",
                        icon: "delete",
                        enable: false,
                        click: function (e) {
                            var $el = e.item.element.closest(".k-content").find(".ex-lw-order-by");

                            Common.UI.ShowConfirm({
                                title: 'Подтверждение',
                                content: 'Вы действительно хотите удалить сортировку?',
                                onSuccess: function (e) {
                                    var listView = $el.data("kendoListView");
                                    if (listView) {
                                        var row = listView.select();
                                        var dataItem = listView.dataItem(row);

                                        if (dataItem)
                                            listView.dataSource.remove(dataItem);
                                    }
                                }
                            });
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
                            text: "Сохранить", type: "button", click: function (e) { methods.saveJoin(e.sender); }
                        },
                        {
                            type: "buttonGroup", buttons: [
                                { text: "Внешнее", type: "Left", group: "join_type_group", togglable: true, selected: true },
                                { text: "Внутреннее", type: "Inner", group: "join_type_group", togglable: true }
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
                            var $el = e.item.element.closest(".k-content").find(".ex-lw-groupby");
                            if ($el.length) {
                                var listView = $el.data("kendoListView");

                                if (listView) {
                                    var selected = listView.select();
                                    var data = listView.dataItem(selected);
                                    AddColumnWindow(e.item.element, "saveGroupByColumn", data);
                                }
                            }
                        }
                    },
                    {
                        id: "deleteGroupColumn",
                        type: "button",
                        icon: "delete",
                        enable: false,
                        click: function (e) {
                            var $el = e.item.element.closest(".k-content").find(".ex-lw-groupby");

                            Common.UI.ShowConfirm({
                                title: 'Подтверждение',
                                content: 'Вы действительно хотите удалить группировку?',
                                onSuccess: function (e) {
                                    var listView = $el.data("kendoListView");
                                    if (listView) {
                                        var row = listView.select();
                                        var dataItem = listView.dataItem(row);

                                        if (dataItem)
                                            listView.dataSource.remove(dataItem);
                                    }
                                }
                            });
                        }
                    }]
                }).data("kendoToolBar");

                self.elements[el] = $item.wrapper;
                $(el).data("type-element", "toolBar");
            },
            initToolbar: function (el, buttonId, method) {
                var $item = $(el).kendoToolBar({
                    items: [{
                        id: buttonId,
                        type: "button",
                        icon: "plus-outline",
                        click: function (e) {
                            AddColumnWindow(e.item.element, method);
                        },
                    }]
                }).data("kendoToolBar");

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
                        case "addJoin":
                            return "Добавить объединение";
                        case "editSort":
                        case "editGroupColumn":
                            return "Редактировать";
                        case "deleteSort":
                            return "Удалить сортировку";
                        case "deleteGroupColumn":
                            return "Удалить группировку";
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
                        { field: "Title", title: "Наименование", template: '<a class="ex-edit-column" href="\\#">#: Title ? Title : "" #</a>' },
                        { field: "Sort", title: "Сортировка", template: '#: Sort ? Sort : "" #' },
                        { field: "Group", title: "Группировка", template: '#: Group ? Group : "" #' },
                        { field: "Function", title: "Функция", template: '#: Function ? Function : "" #' },
                        { command: [{ name: 'destroy', text: '' }], width: "40px", attributes: { class: 'close-command-cell' } }
                    ]
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
                            if (selectedItem) {
                                toolbar.enable("#editSort");
                                toolbar.enable("#deleteSort");
                            }
                            else {
                                toolbar.enable("#editSort", false);
                                toolbar.enable("#deleteSort", false);
                            }
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
                            if (selectedItem) {
                                toolbar.enable("#editGroupColumn");
                                toolbar.enable("#deleteGroupColumn");
                            }
                            else {
                                toolbar.enable("#editGroupColumn", false);
                                toolbar.enable("#deleteGroupColumn", false);
                            }
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
                    template: "<li><div style='padding-left: 10px; width: calc(100% - 25px); float: left;'>#:Title#</div><div style='display: inline'><span class='k-icon k-i-close-outline' title='Удалить параметр'></span></div></li>",
                }).data("kendoListView");

                self.elements[el] = $item.wrapper;
                $(el).data("type-element", "listView");
            },
            initFunctionsTree: function (el) {
                var $item = $(el).kendoTreeView({
                    loadOnDemand: false,
                    dataTextField: ["category", "text"],
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
                            $cbExFunctionExternal.attr("checked", false);

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
            initConditionTree: function (el) {
                var $item = $(el).kendoTreeView({
                    template: kendo.template($("#ex-tw-template").html()),
                    loadOnDemand: false,
                    dataSource: [{
                        id: 1, text: "Корневая группа", operator: "And", type: "group", root: true, expanded: true, items: []
                    }]
                }).data("kendoTreeView");

                self.elements[el] = $item.wrapper;
                $(el).data("type-element", "treeView");
            },
            initJoinsTree: function (el) {
                var $item = $(el).kendoTreeView({
                    template: kendo.template($("#ex-tw-template").html()),
                    loadOnDemand: false,
                    dataSource: [{
                        text: "Дополнительная настройка обединения", type: "join", expanded: true, items: [
                            {
                                text: "Группа", type: "group", expanded: true, items: [
                                    { text: "Условие", type: "condition" }
                                ]
                            }
                        ]
                    }]
                }).data("kendoTreeView");
                
                self.elements[el] = $item.wrapper;
                $(el).data("type-element", "treeView");
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
                    },
                    select: function (e) {
                        var $dataItem = this.dataItem(e.node);

                        if ($dataItem) {
                            var $item = e.sender.element.closest(".ex-content.k-window-content");
                            var $target = $item.data("target");
                            $target.text($dataItem.text);
                            $target.data("RegisterId", $dataItem.id);
                        }
                    }
                }).data("kendoTreeView");

                self.elements[el] = $item.wrapper;
                $(el).data("type-element", "treeView");
            },
            initConditionContextMenu: function (el) {
                var $template = $($("#ex-condition-menu-template").html());
                var $item = $template.kendoContextMenu({
                    target: el,
                    showOn: "click",
                    filter: ".f-ex-group",
                    open: function (e) {
                        var node = $(e.target).closest("li");
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
                                node.text(button.text());
                                node.data("value", btnVal);
                                break;
                            case "addCondition":
                                addCondition(treeView)
                                break;
                            case "addGroup":
                                addGroup(treeView);
                                break;
                            case "deleteGroup":
                                deleteNode(treeView);
                                break;
                        }
                    }
                }).data("kendoContextMenu");

                self.elements[el] = $item.wrapper;
            },
            initComparisonsContextMenu: function (el) {
                var $template = $($("#ex-comparisons-menu-template").html());
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
                    }
                }).data("kendoContextMenu");

                self.elements[el] = $item.wrapper;
            },
            initSplitter: function (el) {
                var $item = $(el).kendoSplitter({
                    panes: [{}, {}]
                }).data("kendoSplitter");

                self.elements[el] = $item.wrapper;
            },     
            saveSelectColumn: function (el, data) {
                if (el) {
                    var grid = el.closest(".k-content").find(".ex-columns-grid").data("kendoGrid");

                    if (grid && data) {
                        var dataSource = grid.dataSource;

                        if (data.New)
                        {
                            data.New = false;
                            dataSource.add(data);
                        }
                        else {
                            var $row = el.closest("tr");
                            var dataItem = grid.dataItem($row);
                            updateDataItem(dataItem, data);
                        }
                    }
                }
            },
            saveFiltrColumn: function (el, data) {
                if (el) {
                    el.text(data.Title);
                    data.New = false;
                    el.data("item", data);
                }
            },
            saveJoin: function (el) {
                if (el) {
                    var $dataItem;
                    var $dialog = el.element.closest(".ex-content.k-window-content");
                    var $target = $dialog.data("target");
                    var reestrTreeView = $dialog.find(".ex-treeview").data("kendoTreeView");   

                    if (reestrTreeView) {
                        var node = getSelectedNode(reestrTreeView);
                        $dataItem = reestrTreeView.dataItem(node);
                        $dataItem.set();
                    }

                    var reestrId = $dataItem ? $dataItem.id : undefined;
                    var reestrName = $dataItem ? $dataItem.text : undefined;
                    var joinType = el.getSelectedFromGroup("join_type_group").data("button").options.type;
                    var actualColumn = el.element.find("#actualBtn").data("item");

                    $target.text(reestrName);
                    $target.data("registerId", reestrId);
                    $target.data("joinType", joinType);
                    $target.data("actualColumn", actualColumn);

                    $dialog.data("kendoWindow").close();
                }
            },
            saveListViewItem: function (el, data) {
                if (el) {
                    var listView = el.closest(".k-content").find(".k-listview").data("kendoListView");
                    if (listView) {
                        if (data.New) {
                            data.New = false;
                            listView.dataSource.add(data);
                        }
                        else {
                            var selected = listView.select();
                            var dataItem = listView.dataItem(selected);
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
                    el.data("item", data);
                }
            },
            saveActualColumn: function (el, data) {                
                if (el) {
                    data.New = false;
                    el.data("item", data);
                }
            }
        };

        InitControls();        

        /* События */

        self.$element.on("click", ".ex-btn-delete", function (e) {
            e.preventDefault();
            var treeView = $(this).closest(".ex-treeview").data("kendoTreeView");
            var node = $(this).closest("li");
            var dataItem = treeView.dataItem(node);
            
            if (treeView && !dataItem.root)
                deleteNode(treeView);
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

        self.$element.on("click", "button[data-bind=saveFiltr]", function () {
            if (!self.saveUrl) {
                Common.ShowError("Не заполнен параметр saveUrl");
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

        self.$element.on('input', 'input.ex-column-value.only_float', function () {
            this.value = this.value.replace(/[^0-9.]/g, '');
            this.value = this.value.replace(/(\..*)\./g, '$1');
            //setQuery(self.$element);
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
            AddQueryWindow($(this), "saveQuery");
        });

        self.$element.on('click', '.ex-attributes, .ex-condition-value', function () {
            var data = $(this).data("item");
            var secondColumnData = $(".ex-condition").not($(this)).data("item");

            if (secondColumnData && secondColumnData.ReferenceId && secondColumnData.AttributeType == "Code") {
                if (!data)
                    data = { New: true };

                data.ExternalReferenceId = secondColumnData.ReferenceId;
                data.ExternalAttributeType = secondColumnData.AttributeType;
                data.EnterType = "REFERENCE";
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
            var $this = $(this);
            var $grid = $this.closest("div.ex-columns-grid").data("kendoGrid");
            if ($grid) {
                Common.UI.ShowConfirm({
                    title: 'Подтверждение',
                    content: 'Вы действительно хотите удалить эту колонку?',
                    onSuccess: function () {
                        $grid.removeRow($this.closest("tr"));
                    }
                });
            }
        });

        /* end */

        //удаляет все kendo элементы
        self.destroy = function () {
            if (!$.isEmptyObject(self.elements)) {
                $.each(self.elements, function (key, value) {
                    self.elements[key].remove();
                });
            }
        }

        function saveColumn(e) {
            var $item = e.sender.element.closest(".ex-dialog-column");
            var $target = $item.data("target");
            var $saveMethod = $item.data("save-column");

            var $alias = $item.find(".ex-tb-column-name").val();
            var $enterType = getKendoDropDownListValue(".ex-ddl-constant-enter-type", $item);
            var $parameterName = $item.find(".ex-parameter-name").length ? $item.find(".ex-parameter-name").val() : undefined;
            var $sourceEnterType = getKendoDropDownListValue(".ex-ddl-source-enter-type", $item);
            var $dataType = getKendoDropDownListValue(".ex-ddl-data-type", $item);
            var $dataSource = getKendoDropDownListValue(".ex-data-source", $item);
            var $type = getKendoDropDownListValue(".ex-ddl-constant-type", $item);
            var $value = $item.find(".ex-column-value").length ? $item.find("input.ex-column-value").val() : undefined;

            var $referenceId = undefined;
            var $referenceItemId = getKendoDropDownListValue(".ex-ddl-reference-list", $item, "input");
            var $referenceItem = getKendoDropDownListValue(".ex-ddl-reference-list", $item, "input", "text");
            var $attributeType = getKendoDropDownListValue(".ex-ddl-attribute-type", $item);
            var $attributeId = undefined;
            var $attributeName = undefined;
            var attributesTreeView = $item.find(".ex-tw-attributes").data("kendoTreeView");

            if (attributesTreeView) {
                var node = getSelectedNode(attributesTreeView);
                $dataItem = attributesTreeView.dataItem(node);
                $attributeId = $dataItem.id;
                $attributeName = $dataItem.text;
                $referenceId = $dataItem.referenceId;
            }

            var $function = $item.find(".ex-function-name").length ? $item.find(".ex-function-name").val() : undefined;
            var $functionId = undefined;
            var functionsTreeView = $item.find(".ex-tw-functions").data("kendoTreeView");

            if (functionsTreeView) {
                var node = getSelectedNode(functionsTreeView);
                $dataItem = functionsTreeView.dataItem(node);
                $functionId = $dataItem.id;
            }

            var $functionExternal = $item.find(".ex-function-external").length ? $item.find(".ex-function-external").prop("checked") : undefined;
            var $queryLevel = getKendoDropDownListValue(".ex-ddl-сonstant-query-level", $item);
            var $columnType = e.sender.getSelectedFromGroup("column_type_group").data("button").options.id;
            var $new = $item.data("new") ? $item.data("new") : undefined;

            if ($item && $target && $saveMethod) {
                var data = {
                    Alias: $alias ? $alias : undefined,
                    AttributeType: $attributeType ? $attributeType : undefined,
                    AttributeId: $attributeId ? $attributeId : undefined,
                    AttributeName: $attributeName? $attributeName : undefined,
                    ReferenceId: $referenceId ? $referenceId : undefined,
                    ReferenceItemId: $referenceItemId? $referenceItemId : undefined,
                    ReferenceItem: $referenceItem? $referenceItem : undefined,
                    Function: $function ? $function : undefined,
                    FunctionId: $functionId ? $functionId : undefined,
                    FunctionExternal: $functionExternal ? $functionExternal : undefined,
                    EnterType: $enterType ? $enterType : undefined,
                    ParameterName: $parameterName ? $parameterName : undefined,
                    SourceEnterType: $sourceEnterType ? $sourceEnterType : undefined,
                    DataType: $dataType ? $dataType : undefined,
                    DataSource: $dataSource ? $dataSource : undefined,
                    ColumnType: $columnType ? $columnType : undefined,
                    Type: $type ? $type : undefined,
                    Value: $value ? $value : undefined,
                    QueryLevel: $queryLevel ? $queryLevel : undefined,
                    Sort: undefined,
                    Group: undefined,
                    New: $new 
                };

                data.Title = getColumnText(data);

                if ($new)
                    self.nameNumerator++;

                methods[$saveMethod]($target, data);
                $item.data("kendoWindow").close();
            }
        }

        function getColumnText(data) {
            var text = "";

            if (data) {
                var text = data.Alias;

                switch (data.ColumnType) {
                    case "QSColumnConstant":
                        if (data.EnterType == "MANUAL")
                            text = data.Value;
                        else if (data.EnterType == "REFERENCE")
                            text = "{0} ({1})".format(data.ReferenceItem, data.ReferenceItemId);
                        break;
                    case "QSConditionSimple":
                        text = "{0} ({1})".format(data.AttributeName, data.AttributeId);
                        break;

                    /*case "ColumnFunction": RenderTemplate(self.ModalColumnFunctionTemplate, container, data);
                        break;
                    case "QSColumnQuery": RenderTemplate(self.ModalColumnSubqueryTemplate, container, data);
                        break;
                    case "QSColumnIf": RenderTemplate(self.ModalColumnConditionIfTemplate, container, data);
                        break;
                    case "QSColumnSwitch": RenderTemplate(self.ModalColumnConditionCaseTemplate, container, data);
                        break;*/
                }
            }

            return text;
        }

        //Обновить dataItem Kendo-элемента
        function updateDataItem(dataItem, data) {
            if (dataItem && data &&
                !$.isEmptyObject(dataItem) && !$.isEmptyObject(dataItem)) 
            {
                $.each(data, function (key, value) {
                    if (data.hasOwnProperty(key))
                        dataItem.set(key, value);
                })
            }
        }

        function getKendoDropDownListValue(el, container, input, type) {
            if (container && typeof container == "string")
                container = $(container);

            if (!input)
                input = "select";

            var $item = container ? $(input + el) : container.find(input + el);
            var $ddl = $item.length ? $item.data("kendoDropDownList") : undefined;

            return $ddl ? type == "text" ? $ddl.text() : $ddl.value() : undefined;
        }

        function AddColumnWindow(el, func, data) {
            var $dialogColumn = $($("#ex-dialog-column-template").html());

            $dialogColumn.html(InitColumnModalWindow(data));
            $dialogColumn.data("target", $(el));
            $dialogColumn.data("save-column", func);

            if (data && !data.New) {
                $dialogColumn.data("column", data);
            }

            var dialog = $dialogColumn.kendoWindow({
                actions: ["Close"],
                height: "500px",
                width: "1000px",
                appendTo: self.$element,
                modal: true,
                visible: false,
                resizable: false,
                close: function (e) {
                    this.destroy();
                }
            }).data('kendoWindow');

            if (data && !data.New)
                ShowContent($dialogColumn.find(".ex-modal-content"), data.ColumnType, data);
            else {
                $dialogColumn.data("new", true);
                RenderTemplate(self.ModalColumnConstantTemplate, $dialogColumn.find(".ex-modal-content"), data);
            }

            dialog.center().open();
        }

        function SelectReestrWindow(el) {
            var $dialogTemplate = $($("#ex-dialog-template").html());
            $dialogTemplate.data("target", $(el));

            var dialog = $dialogTemplate.kendoWindow({
                height: "300px",
                width: "545px",
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

            RenderTemplate(self.DialogReestrTemplate, $dialogTemplate);
            dialog.open();
        }

        function AddQueryWindow(el, func) {
            var $dialogTemplate = $($(self.DialogTemplate).html());
            $dialogTemplate.data("target", $(el));
            $dialogTemplate.data("save-query", func);            

            var $filterQuery = $dialogTemplate.filterQueryBuilderExtended(
            {
                registerViewId: self.registerViewId,
                saveUrl: self.saveUrl,
                saveParams: self.saveParams,
                deleteUrl: self.deleteUrl,
                attributesUrl: self.attributesUrl,
                referencesUrl: self.referencesUrl,
                filter: self.filter,
                window: true,
                idNumerator: self.idNumerator,
                nameNumerator: self.nameNumerator
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

                if (orderbyDataItem.sort === sort) {
                    orderbyDataItem.sort = undefined;
                    orderbyNode.find("span.label-sort").remove();
                }
                else {
                    if (!orderbyDataItem.sort)
                        orderbyNode.append("<span class='label label-sort' style='margin-left: 5px; position: relative; top: -2px'>" + sort + "</span>");
                    else
                        orderbyNode.find("span.label-sort").text(sort);

                    orderbyDataItem.sort = sort;
                }
            }
        }

        function restoreSort(node) {
            if (node) {
                var orderbyListView = self.$element.find(".ex-lw-order-by").data("kendoListView");
                var orderbyDataItem = orderbyListView.dataItem(node);

                if (orderbyDataItem.sort)
                    node.append("<span class='label label-sort' style='margin-left: 5px; position: relative; top: -2px'>" + orderbyDataItem.sort + "</span>");
            }
        }

        function initQueryInput() {
            self.$element.find(".filter-container").prepend('<div style="margin-bottom: 15px;"><label style="width: 60px;">Запрос:</label><input id="f_txt_query" type="text" class="k-textbox" style="width: calc(100% - 160px);"><button class="k-button f_button_save" style="margin-left: -1px;" title="Сохранить фильтр" data-bind="saveFiltr"><span class="k-icon k-i-save"></span>&nbsp;</button><button class="k-button f_button_delete" style="margin-left: -1px;" title="Удалить фильтр" data-bind="deleteFiltr"><span class="k-icon k-i-delete"></span>&nbsp;</button></div>');
        }        

        function setValidation(container, type, data) {
            if (container) {
                container.empty();
                var $input;

                switch (type) {
                    case "INTEGER":
                        $input = $('<input type="text" class="k-textbox ex-tb-constant-value only_int" style="width: 100%;" />');
                        container.append($input);
                        break;
                    case "DECIMAL":
                        $input = $('<input type="text" class="k-textbox ex-column-value only_float" style="width: 100%;" data-type-element="tb" data-element-role="Value" />');
                        container.append($input);
                        break;
                    case "BOOLEAN":
                        $input = $($("#" + self.booleanTemplate).html());
                        container.append($input);
                        break;
                    case "STRING":
                        $input = $('<input type="text" class="k-textbox ex-column-value" style="width: 100%;" data-type-element="tb" data-element-role="Value" />');
                        container.append($input);
                        break;
                    case "MANUAL":
                        RenderTemplate(self.ModalColumnConstantManualTemplate, container, data)
                        break;
                    case "PARAM":
                        RenderTemplate(self.ModalColumnConstantParameterTemplate, container, data)
                        break;
                    case "DATE":
                        $input = $('<input type="text" class="ex-column-value" style="width: 100%;" data-type-element="datepicker" data-element-role="Value" />');
                        container.append($input);
                        $input.kendoDatePicker({
                            format: "dd.MM.yyyy",
                            /*change: function () {
                                setQuery(self.$element);
                            }*/
                        });
                        break;
                    case "REFERENCE":
                        if (data.ExternalReferenceId && data.ExternalAttributeType)
                            getReferences(data.ExternalReferenceId, container, data.ReferenceItemId);
                        break;                    
                }

                if (!$.isEmptyObject(data) && (type == "INTEGER" || type == "DECIMAL" || type == "STRING" || type == "DATE"))
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
            if (treeView)
                treeView.append(
                    {
                        text: "Дополнительная настройка обединения", type: "join", expanded: true, items: [
                            {
                                text: "Группа", type: "group", expanded: true, items: [
                                    { text: "Условие", type: "condition" }
                                ]
                            }
                        ]
                    }
                );
        }

        function addCondition(treeView) {
            if (treeView)
                treeView.append(
                    { text: "Условие", type: "condition", selected: false },
                    getSelectedNode(treeView)
                );
        }

        function addGroup(treeView) {
            if (treeView)
                treeView.append(
                    { text: "Группа", type: "group", items: [], selected: false },
                    getSelectedNode(treeView)
                );
        }

        function InitColumnModalWindow(data) {
            var $resModal = $($(self.ModalColumnTemplate).html());
            var container = $resModal.find(".ex-modal-content");

            var toolbar = $resModal.find(".ex-toolbar-column-type").kendoToolBar({
                items: [
                    { type: "button", text: "Сохранить", click: saveColumn },
                    {
                        type: "buttonGroup", buttons: [
                            { text: "Константа", id: "QSColumnConstant", group: "column_type_group", togglable: true, toggle: function () { ShowContent(container, "QSColumnConstant", data) }, selected: true },
                            { text: "Показатель", id: "QSConditionSimple", group: "column_type_group", togglable: true, toggle: function () { ShowContent(container, "QSConditionSimple", data) } },
                            { text: "Функция", id: "ColumnFunction", group: "column_type_group", togglable: true, toggle: function () { ShowContent(container, "ColumnFunction", data) } },
                            { text: "Подзапрос", id: "QSColumnQuery", group: "column_type_group", togglable: true, toggle: function () { ShowContent(container, "QSColumnQuery", data) } },
                            { text: "Условие (IF)", id: "QSColumnIf", group: "column_type_group", togglable: true, toggle: function () { ShowContent(container, "QSColumnIf", data) } },
                            { text: "Условие (SWITCH)", id: "QSColumnSwitch", group: "column_type_group", togglable: true, toggle: function () { ShowContent(container, "QSColumnSwitch", data) } }
                        ]
                    }
                ]
            }).data("kendoToolBar");

            if (data && data.ColumnType) {
                toolbar.toggle($resModal.find("#" + data.ColumnType), true);
            }

            $resModal.find(".ex-toolbar-column-type .k-button-group").before(" Тип колонки: ");
            return $resModal;
        }

        function ShowContent(container, typeContent, data) {
            switch (typeContent) {
                case "QSColumnConstant": RenderTemplate(self.ModalColumnConstantTemplate, container, data);
                    break;
                case "QSConditionSimple": RenderTemplate(self.ModalColumnAttributeTemplate, container, data);
                    break;
                case "ColumnFunction": RenderTemplate(self.ModalColumnFunctionTemplate, container, data);
                    break;
                case "QSColumnQuery": RenderTemplate(self.ModalColumnSubqueryTemplate, container, data);
                    break;
                case "QSColumnIf": RenderTemplate(self.ModalColumnConditionIfTemplate, container, data);
                    break;
                case "QSColumnSwitch": RenderTemplate(self.ModalColumnConditionCaseTemplate, container, data);
                    break;
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

            SetElementsValues(container, data);
        }

        function ToggleTemplate(template, container) {
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
                        $el.attr("id", "ex-el-" + self.idNumerator);
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
                            SetValue($el, $type, $value, data);
                            $el.data("set", "true");

                            if ($type == "ddl" && $el.data("toggle-template"))
                                return false;
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
                            $ddl.value(value);
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
                        el.prop("checked", value);
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
                        break;
                    case "grid":
                        break;
                    case "toolBar":
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

        function InitControls() {
            if (self.registerViewId) {
                return $.ajax({
                    url: self.attributesUrl,
                    type: 'GET',
                    dataType: "html",
                    data: { registerViewId: self.registerViewId },
                    success: function (data) {
                        if (data) {
                            self.attributes = getAttributesDataSource(jQuery.parseJSON(data));

                            RenderTemplate(self.MainTemplate, self.$element);

                            /*if (!self.filter)
                                addGroup(self.$element, {}, true);
                            else
                                fillStruct();*/

                            if (self.window) {
                                self.$element.find(".filter-container").prepend("<div class='ex-subquery-toolbar'><div>");
                                self.$element.find(".ex-subquery-toolbar").kendoToolBar({
                                    items: [
                                        { type: "button", text: "Сохранить"/*, click: saveColumn */}
                                    ]
                                });
                            }
                            else
                                initQueryInput();

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

                            $ddl.value(value);

                            //setQuery(self.$element);
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