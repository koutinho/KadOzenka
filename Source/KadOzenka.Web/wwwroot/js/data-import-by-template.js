function initFilter() {
    $("#filter").on("input", function (event, enableAllTreeViewValues) {
        var query = this.value.toLowerCase();
        const treeView = $("#treeview").data("kendoTreeView");

        // Отключаем кнопку сопоставления после изменения результатов поиска и сбрасываем выделение
        treeView.select(null);
        treeView.dataItem(null);
        $("#matchButton").data('kendoButton').enable(false);

        var dataSource = treeView.dataSource;
        filter(dataSource, query);
        treeView.expand(".k-item");

        if (enableAllTreeViewValues)
            treeView.enable(true);
    });

    $("#filter").keydown(function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            return false;
        }
    });
}


function initMatchBtn(isFirstElementInComparisonBlockReadonly) {
    $("#matchButton").kendoButton({
        iconClass: "my-custom-icon-class",
        enable: false,
        click: function (e) {
	        var attributesTreeView = $("#treeview").data('kendoTreeView');
            var excelColumnsListBox = $("#columnsListBox").data('kendoListBox');
            if (attributesTreeView && excelColumnsListBox) {
		        var selectedAttribute = attributesTreeView.dataItem(attributesTreeView.select());
                var selectedFileColumn = excelColumnsListBox.dataItem(excelColumnsListBox.select());
		        if (selectedAttribute && selectedFileColumn) {
			        attributesTreeView.enable(attributesTreeView.select(), false);
			        excelColumnsListBox.enable(excelColumnsListBox.select(), false);
			        $("#matchButton").data('kendoButton').enable(false);
                    var matchedParametersList = $("#listView").data('kendoListView');
                    matchedParametersList.dataSource.add({
				        IsKey: false,
				        IsReadOnly: isFirstElementInComparisonBlockReadonly,
				        ColumnName: selectedFileColumn.Name,
				        ColumnIndex: selectedFileColumn.Id,
				        ColumnId: selectedFileColumn.Id,
				        ColumnUid: selectedFileColumn.uid,
				        AttributeDescription: selectedAttribute.text,
				        AttributeId: selectedAttribute.id,
				        AttributeUid: selectedAttribute.uid
			        });
			        var loadBtn = $("#loadButton").data('kendoButton');
			        if (loadBtn)
				        loadBtn.enable(true);
		        }
	        }
        }
    });
}


function initComparisonBlock() {
    $("#listView").kendoListView({
        dataSource: dataSource,
        template: kendo.template($("#template").html()),
        remove: function (e) {
            const columnsListBox = $("#columnsListBox").data("kendoListBox");
            columnsListBox.enable($(`.k-item[data-uid=${e.model.ColumnUid}]`), true);

            const treeView = $("#treeview").data("kendoTreeView");
            var attrElement = treeView.dataSource.get(e.model.AttributeId);
            attrElement.set("enabled", true);
        },
        dataBound: function () {
            var loadBtn = $("#loadButton").data('kendoButton');
            if (!loadBtn)
                return;
            $("#loadButton").data('kendoButton').enable(dataSource.data().length !== 0);
        }
    }).data("kendoListView");
}


function initColumnsFromExcelBlock() {
    $("#columnsListBox").kendoListBox({
        draggable: false,
        selectable: "single",
        dataValueField: "Id",
        dataTextField: "Name",
        change: function (e) {
            const treeView = $("#treeview").data('kendoTreeView');
            if (treeView) {
                var element = e.sender.select();
                var dataItem = e.sender.dataItem(element[0]);

                var dataSource = treeView.dataSource;
                var treeData = dataSource.data().map(i => i.items.map(j => j)).flat();

                treeView.select(null);
                $('#filter').val('').trigger('input');

                var select = selectAssist(treeData, dataItem.Name);
                var node = (select.uid != null) ? treeView.findByUid(select.uid) : null;

                $('#filter').val(select.filterText).trigger('input');
                treeView.select(node);
                if (node != null)
                    treeView.trigger('select', { node: node });

                var selected = treeView.select();
                var data = treeView.dataItem(selected);
                if (data) {
                    $("#matchButton").data('kendoButton').enable(true);
                }
            }
        }
    });
}


function initAttributesTree() {
    $("#treeview").kendoTreeView({
        template: '<span #= item.isRequired ? \'style="color: red;"\' : "" # >#= item.text #</span>',
        loadOnDemand: false,
        autoScroll: true,
        select: function (e) {
            var item = this.dataItem(e.node);
            if (!item.parentId) {
                e.preventDefault();
                return;
            }
            var columnsFromExcel = $("#columnsListBox").data('kendoListBox');
            if (columnsFromExcel) {
                var selected = columnsFromExcel.select();
                var data = columnsFromExcel.dataItem(selected);
                if (data) {
                    $("#matchButton").data('kendoButton').enable(true);
                }
            }
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
                    return item.ParentId === parantItems[i].ItemId;
                });
                if (childItems) {
                    child.items = $.map(childItems,
                        function (item) {
                            return {
                                text: item.Description,
                                id: item.AttributeId,
                                parentId: item.ParentId,
                                referenceId: item.ReferenceId,
                                type: item.Type,
                                isRequired: item.IsRequired
                            };
                        });
                }
                source.push(child);
            }
        }
    }
    return source;
}


function selectAssist(treeData, searchText) {
    var select = { uid: null, filterText: null };

    // Полное совпадение
    var exactMatch = treeData.filter(i => i.text.toLowerCase() === searchText.toLowerCase());

    if (exactMatch.length === 1) {
        select.uid = exactMatch[0].uid;
        select.filterText = searchText;
        return select;
    }

    var wordArray = searchText.toLowerCase().split(' ');

    // Поиск по первым двум словам
    if (wordArray.length > 1) {
        var twoWordFilter = wordArray[0] + ' ' + wordArray[1];

        var twoWordMatch = treeData.filter(i => i.text.toLowerCase().includes(twoWordFilter));
        if (twoWordMatch.length === 1) {
            select.uid = twoWordMatch[0].uid;
            select.filterText = twoWordFilter;
            return select;
        }
        if (twoWordMatch.length > 1) {
            select.filterText = twoWordFilter;
            return select;
        }
    }

    // Поиск по первому слову
    var firstWordFilter = wordArray[0];
    var firstWordMatch =
        treeData.filter(i => i.text.toLowerCase().includes(firstWordFilter));

    if (firstWordMatch.length === 1) {
        select.uid = firstWordMatch[0].uid;
        select.filterText = firstWordFilter;
        return select;
    }
    if (firstWordMatch.length > 1) {
        select.filterText = firstWordFilter;
        return select;
    }
    return select;
}


function checkboxChange(e) {
    const check = e.checked;
    const listView = $("#listView").data('kendoListView');
    const node = $(e).closest(".matchItem");
    listView.dataItem(node).IsKey = check;
};

function resetData() {
    $("#matchButton").data('kendoButton').enable(false);
    $('#filter').val('').trigger('input', [true]);
    dataSource.data([]);
    var loadBtn = $("#loadButton").data('kendoButton');
    if (loadBtn)
        loadBtn.enable(false);
}

function filter(dataSource, query) {
    var hasVisibleChildren = false;
    var data = dataSource instanceof kendo.data.HierarchicalDataSource && dataSource.data();

    for (var i = 0; i < data.length; i++) {
        var item = data[i];
        var text = item.text.toLowerCase();
        var itemVisible =
            query === "" || text.indexOf(query) >= 0;
        var anyVisibleChildren = filter(item.children, query);
        hasVisibleChildren = hasVisibleChildren || anyVisibleChildren || itemVisible;
        item.hidden = (!item.hasChildren && !itemVisible) || (item.hasChildren && !anyVisibleChildren);
    }

    if (data) {
        dataSource.filter({ field: "hidden", operator: "neq", value: true });
    }

    return hasVisibleChildren;
}