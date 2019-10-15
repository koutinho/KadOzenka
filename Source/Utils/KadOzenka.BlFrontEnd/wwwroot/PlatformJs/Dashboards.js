// Сохранение изменений по рабочему столу (создание нового, редактирование или копирование стола)
function onDashboardWindowRefresh() {
    var wnd = this;
    //обработчик кнопки сохранить
    $(wnd.element).find('button[type="submit"]').on('click', function (e) {
        e.preventDefault();
        kendo.ui.progress($('body'), true);

        $("form").setValidator();

        var form = $(this).closest('form');
        var validator = form.data('kendoValidator');

        if (validator.validate()) {
            $.ajax({
                type: 'POST',
                url: '\\Dashboard\\SaveDashboard',
                data: $("form").serialize(),
                success: function (result) {
                    kendo.ui.progress($('body'), false);
                    Common.UI.ShowDialog({
                        content: 'Рабочий стол сохранен',
                        icon: 'ok',
                        showCloseBtn: true
                    });
                    var dropDownList = $('#dashBoardList').data('kendoDropDownList');
                    dropDownList.dataSource.read();
                    wnd.close();
                },
                error: function (result) {
                    kendo.ui.progress($('body'), false);
                    Common.UI.ShowDialog({
                        title: 'Ошибка',
                        content: 'result.responseText',
                        icon: 'error',
                        showCloseBtn: true
                    });
                }
            });
        }
    });
}

// Изменение размеров панелей и перегрузка панелей после сохранения изменений
function onEditLayoutWindowRefresh() {
    var wnd = this;

    var currentDashBoardId = getCurrentDashBoard();
    if (currentDashBoardId == undefined) {
        Common.UI.ShowDialog({
            title: 'Внимание!',
            content: 'Не выбран рабочий стол',
            icon: 'warning',
            showCloseBtn: true
        });
        return;
    }

    $(wnd.element).find('.make').on('click', function (e) {
        var formData = { 'id': currentDashBoardId, 'layout': this.getAttribute('value') };
        $.ajax({
            type: 'POST',
            url: '\\Dashboard\\SelectLayoutType',
            data: formData,
            success: function (result) {
                $("#dashBoardPanels").html(result);
                CheckForHide();
                wnd.close();
            },
            error: function (result) {
                kendo.ui.progress($('body'), false);
                Common.UI.ShowDialog({
                    title: 'Ошибка',
                    content: result.responseText,
                    icon: 'error',
                    showCloseBtn: true
                });
            }
        });
    });
}

// Добавление нового гаджета и перегрузка панелей после сохранения изменений
function onAddGadgetWindowClose() {
    var formData = { 'id': getCurrentDashBoard() };
    $.ajax({
        type: 'POST',
        url: '\\Dashboard\\DashboardPanels',
        data: formData,
        success: function (result) {
            $("#dashBoardPanels").html(result);
            CheckForHide();
        },
        error: function (result) {
            kendo.ui.progress($('body'), false);
            Common.UI.ShowDialog({
                title: 'Ошибка',
                content: result.responseText,
                icon: 'error',
                showCloseBtn: true
            });
        }
    });
}

function getCurrentDashBoard() {
    var currentDashBoardId = $('#dashBoardList').data('activeId');
    if (currentDashBoardId == null || currentDashBoardId == undefined) {
        currentDashBoardId = $('#dashBoardList').find('li:first').data("id");
    }

    return currentDashBoardId;
}

function HideEdit() {
    //$("#buttonEditLayout").hide();
    //$("#buttonAddGadget").hide();
    var elementsHide = $(".card-header.dashboard");
    for (var i = 0; i < elementsHide.length; ++i) {
        elementsHide.hide();
    }
}

function ShowEdit() {
    //$("#buttonEditLayout").show();
    //$("#buttonAddGadget").show();
    var elementsShow = $(".card-header.dashboard");
    for (var i = 0; i < elementsShow.length; ++i) {
        elementsShow.show();
    }
}

function CheckForHide() {
    if (!$('#cbEditMode').is(':checked'))
        HideEdit();
    else
        ShowEdit();

    //обработка строки поиска
    function triggerSearch(inputEl) {
        if (inputEl && inputEl.length > 0 && inputEl.is("[data-search-url]")) {
            var inputValue = inputEl.val();
            if (inputValue) {
                var searchUrl = inputEl.data('search-url');
                mainWindow.location.assign(searchUrl.replace('$searchTerm', inputValue));
            }
        }
    }


    //*****QuickSearch*****//
    var mainWindow = window.parent || window;
    $('.menu-panel-row-header.menu-panel-row-header-url').on('click', function () {
        var headerUrl = $(this).data('url');
        if (headerUrl) {
            mainWindow.location.assign(headerUrl);
        }
    });

    $(".menu-panel-row-search a").on('click', function (e) {
        e.preventDefault();
        triggerSearch($(this).closest('.menu-panel-row-search').find('input').first());
    });

    $(".menu-panel-row-search input").on('keyup', function (e) {
        e.preventDefault();
        //поиск на Enter
        if (e.keyCode == 13) {
            triggerSearch($(this));
        }
    });

    $('.open-in-radwindow').on('click', function (e) {
        e.preventDefault();
        var url = $(this).attr('href');
        if (!url.includes('UniqueSessionKey')) {
            if (url.includes('?')) {
                url += '&UniqueSessionKey=' + dashboardSettings.UniqueSessionKey;
            } else {
                url += '?UniqueSessionKey=' + dashboardSettings.UniqueSessionKey;
            }
        }
        Common.UI.ShowWindow(this.text, url);
    });
    //*****QuickSearch*****//
}

$(function () {
    HideEdit();

    $('#main-left-panel').on('change', '#cbEditMode', function (e) {
        e.preventDefault();
        CheckForHide();
    });

    $('#main-left-panel').on('click', '#buttonEditLayout', function (e) {
        e.preventDefault();

        var currentDashBoardId = getCurrentDashBoard();
        if (currentDashBoardId == undefined) {
            Common.UI.ShowDialog({
                title: 'Внимание!',
                content: 'Не выбран рабочий стол',
                icon: 'warning',
                showCloseBtn: true
            });
            return;
        }

        editLayout = $('#EditLayout').data('kendoWindow');
        editLayout.title('Изменить макет');
        editLayout.refresh({ url: 'Dashboard\SelectLayoutType' + '?Id=' + currentDashBoardId, type: "GET" });
        editLayout.center().open();
    });

    $('#main-left-panel').on('click', '#buttonAddGadget', function (e) {
        e.preventDefault();

        var currentDashBoardId = getCurrentDashBoard();
        if (currentDashBoardId == undefined) {
            Common.UI.ShowDialog({
                title: 'Внимание!',
                content: 'Не выбран рабочий стол',
                icon: 'warning',
                showCloseBtn: true
            });
            return;
        }

        addGadget = $('#AddGadget').data('kendoWindow');
        addGadget.title('Добавить гаджет');
        addGadget.refresh({ url: '\\Dashboard\\AddGadget' + '?Id=' + currentDashBoardId, type: "GET" });
        addGadget.maximize();
        addGadget.open();
    });

    $('#main-left-panel').on('click', '#buttonCreateDashBoard', function (e) {
        e.preventDefault();
        var createDashboard = $('#Dashboard').data('kendoWindow');
        createDashboard.title('Новый Рабочий стол');
        createDashboard.refresh({ url: '\\Dashboard\\GetDashboard', type: "GET" });
        createDashboard.center().open();
    });

    $('#main-left-panel').on('click', '#buttonChangeDashBoard', function (e) {
        e.preventDefault();

        var currentDashBoardId = getCurrentDashBoard();
        if (currentDashBoardId == undefined) {
            Common.UI.ShowDialog({
                title: 'Внимание!',
                content: 'Не выбран рабочий стол',
                icon: 'warning',
                showCloseBtn: true
            });
            return;
        }

        editDashboard = $('#Dashboard').data('kendoWindow');
        editDashboard.title('Изменить Рабочий стол');
        editDashboard.refresh({ url: '\\Dashboard\\GetDashboard' + '?Id=' + currentDashBoardId, type: "GET" });
        editDashboard.center().open();
    });

    $('#main-left-panel').on('click', '#buttonCopyDashBoard', function (e) {
        e.preventDefault();

        var currentDashBoardId = getCurrentDashBoard();
        if (currentDashBoardId == undefined) {
            Common.UI.ShowDialog({
                title: 'Внимание!',
                content: 'Не выбран рабочий стол',
                icon: 'warning',
                showCloseBtn: true
            });
            return;
        }

        editDashboard = $('#Dashboard').data('kendoWindow');
        editDashboard.title('Копировать Рабочий стол');
        editDashboard.refresh({ url: '\\Dashboard\\GetDashboard' + '?CopyId=' + currentDashBoardId, type: "GET" });
        editDashboard.center().open();
    });

    $('#main-left-panel').on('click', '#buttonDeleteDashBoard', function (e) {
        e.preventDefault();
        v
        var currentDashBoardId = getCurrentDashBoard();
        if (currentDashBoardId == undefined) {
            Common.UI.ShowDialog({
                title: 'Внимание!',
                content: 'Не выбран рабочий стол',
                icon: 'warning',
                showCloseBtn: true
            });
            return;
        }

        Common.UI.ShowConfirm({
            title: 'Удаление',
            content: 'Вы действительно хотите удалить Рабочий стол?',
            onSuccess: function (e) {
                $.ajax({
                    url: '\\Dashboard\\DeleteDashboard',
                    type: 'POST',
                    data: { 'id': currentDashBoardId },
                    success: function () {
                        var currentDashBoardId = getCurrentDashBoard();
                        var $dashBoard = $('#dashBoardList').find('li[data-id="' + currentDashBoardId + '"');

                        if ($dashBoard.length > 0) {
                            $dashBoard.remove();
                            CheckForHide();
                        }
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
    });

    $('#main-left-panel').on('click', '#buttonDefaultDashBoard', function (e) {
        e.preventDefault();

        kendo.ui.progress($('body'), true);

        var currentDashBoardId = getCurrentDashBoard();
        if (currentDashBoardId == undefined) {
            Common.UI.ShowDialog({
                title: 'Внимание!',
                content: 'Не выбран рабочий стол',
                icon: 'warning',
                showCloseBtn: true
            });
            return;
        }

        $.ajax({
            url: '\\Dashboard\\SetUserDefaultDashboard',
            type: 'POST',
            data: { 'id': currentDashBoardId },
            success: function () {
                kendo.ui.progress($('body'), false);
            },
            error: function () {
                kendo.ui.progress($('body'), false);
            }
        });
    });

    $('#main-left-panel').on('click', '#buttoExportToExcel', function (e) {
        e.preventDefault();

        var currentDashBoardId = getCurrentDashBoard();
        if (currentDashBoardId == undefined) {
            Common.UI.ShowDialog({
                title: 'Внимание!',
                content: 'Не выбран рабочий стол',
                icon: 'warning',
                showCloseBtn: true
            });
            return;
        }

        window.location = '\\Dashboard\\ExportToExcel' + '?id=' + currentDashBoardId;
    });

    // Выбор другого рабочего стола и перегрузка панелей
    $('#main-left-panel').on('click', '#dashBoardList li a', function (e) {
        e.preventDefault();

        var $this = $(this);
        var selectedDashboardId = $this.parent().data("id");
        $('#dashBoardList').data('activeId', selectedDashboardId);

        kendo.ui.progress($("body"), true);
        $.ajax({
            type: 'POST',
            url: '\\Dashboard\\DashboardPanels',
            data: { 'id': selectedDashboardId },
            success: function (result) {
                $("#dashBoardPanels").html(result);
                CheckForHide();
                kendo.ui.progress($("body"), false);
            },
            error: function (result) {
                Common.UI.ShowDialog({
                    title: 'Ошибка',
                    content: result.responseText,
                    icon: 'error',
                    showCloseBtn: true
                });
                kendo.ui.progress($("body"), false);
            }
        });
    });
});