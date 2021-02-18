function onChange(classNameTag) {
    let tooltip = $('#' + classNameTag + 'Wrapper').data('kendoTooltip');
    if (tooltip) {
        let tree = $('input.' + classNameTag).data('kendoDropDownTree');
        tooltip.options.content = tree.text();
        tooltip.refresh();
    }
}

function onFiltering(e) {
    e.preventDefault();
    let query = e instanceof Array && e[0] ? '' : e.filter.value;
    if (query)
        query = query.toLowerCase();
    let dataSource = e.sender.dataSource;
    if (query === '')
        clearDataSource(dataSource);
    filterDataSource(dataSource, query);
    e.sender.trigger('onFiltered');
}

function filterDataSource(dataSource, query) {
    let hasVisibleChildren = false;
    let data = dataSource instanceof kendo.data.HierarchicalDataSource && dataSource.data();

    for (let i = 0; i < data.length; i++) {
        let item = data[i];
        let text = item.text.toLowerCase();
        let itemVisible =
            query === '' || text.indexOf(query) >= 0;
        let anyVisibleChildren = filterDataSource(item.children, query);
        hasVisibleChildren = hasVisibleChildren || anyVisibleChildren || itemVisible;
        item.hidden = (!item.hasChildren && !itemVisible) || (item.hasChildren && !anyVisibleChildren);
    }

    if (data) {
        dataSource.filter({field: 'hidden', operator: 'neq', value: true});
    }

    return hasVisibleChildren;
}

function clearDataSource(dataSource) {
    let data = dataSource._data;
    if (data && data.length > 0) {
        for (let i = 0; i < data.length; i++) {
            data[i].dirty = false;
            data[i].dirtyFields = {};
            data[i].expanded = false;
            if (data[i].hasChildren) {
                for (let j = 0; j < data[i].items.length; j++) {
                    data[i].items[j].dirty = false;
                    data[i].items[j].dirtyFields = {};
                    data[i].items[j].selected = false;
                }
            }
        }
    }
}

function setAutoClose(classNameTag) {
    let checkExist = setInterval(function () {
        let tree = $('input.' + classNameTag).data('kendoDropDownTree');
        if (tree !== undefined) {
            tree.options.autoClose = false;
            tree.treeview.bind('select', e => onSelected(e));
            tree.bind('change', () => onChange(classNameTag));
            //tree.bind('filtering', e => onFiltering(e));
            clearInterval(checkExist);
        }
    });
}

function onSelected(e) {
    if (e.sender.dataItem(e.node).hasChildren) {
        e.preventDefault()
    } else {
        e.sender.dropdowntree.close();
    }
}

function clearField(classNameTag) {
    let tree = $('input.' + classNameTag).data('kendoDropDownTree');
    tree.value('');
    tree.trigger('change');
    tree.filterInput.val('');
    tree.trigger('filtering', [true]);
}

function init(classNameTag, addFunction) {
    $(document).ready(function () {
        $('.add-button-' + classNameTag).on('click', addFunction)
    });
    $(document).ready(function () {
        $('.clear-button-' + classNameTag).on('click', () => clearField(classNameTag));
    });
    setAutoClose(classNameTag);
}