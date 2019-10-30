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
                    var newDashboard = !$("form [name=Id]").val() || $("form [name=Id]").val() == -1 ? true : false;

                    Common.UI.ShowInfo({
                        content: 'Рабочий стол сохранен',
                        icon: 'ok',
                        onSuccess: function () {
                            Common.UI.CloseWindow('Dashboard', window.parent);

                            if (result) {
                                if (newDashboard) {
                                    $('#dashBoardList').append('<li title="' + result.Name + '"><a data-id="' + result.Id + '" href="#">' + result.Name + '</a></li>');
                                    $("#dashBoardList a.active").removeClass('active');
                                    $('#dashBoardList a[data-id=' + result.Id + ']').addClass("active");
                                    SetDashBoard(result.Id);
                                    SetActiveLayout(result.Id);
                                }
                                else {
                                    var $li = $('#dashBoardList li[data-id=' + result.Id + ']');
                                    $li.attr('title', result.Name);
                                    $li.find('a').text(result.Name);
                                }
                            }
                        }
                    });

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
    var currentDashBoardId = $('#dashBoardList a.active').data("id");
    if (currentDashBoardId == null || currentDashBoardId == undefined) {
        currentDashBoardId = $('#dashBoardList').find('li:first a').data("id");
    }

    return currentDashBoardId;
}

function SetDashBoard(selectedDashboardId) {
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
}

function SetActiveLayout(dashBoard) {
    if (!dashBoard)
        dashBoard = getCurrentDashBoard();

    $.ajax({
        type: 'GET',
        url: '\\Dashboard\\GetCurrentLayoutType',
        data: { id: dashBoard },
        success: function (result) {
            $("#dashBoardLayoutList a.active").removeClass('active');
            $("#dashBoardLayoutList a[data-value=" + result + "]").addClass("active");
        }
    });
}

function HideEdit() {
    var elementsHide = $(".card-header.dashboard");
    for (var i = 0; i < elementsHide.length; ++i) {
        elementsHide.hide();
    }
}

function ShowEdit() {
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
                        var $dashBoard = $('#dashBoardList').find('li a[data-id="' + currentDashBoardId + '"]');

                        if ($dashBoard.length > 0) {
                            $dashBoard.parent().remove();
                            var $el = $('#dashBoardList').find('li:first a');
                            if ($el.length > 0) {
                                SetDashBoard($el.data('id'));
                                $el.addClass("active");
                            }
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

    $('#main-left-panel').on('click', '#buttonPrintDashBoard', function (e) {
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

        if ($('.panelbar') && $('.panelbar').data('kendoPanelBar') && $('.panelbar').data('kendoPanelBar').expand) {
            $('.panelbar').data('kendoPanelBar').expand($('li'));
        }

        var printWindow = window.open('', 'PRINT');

        printWindow.document.write($('html').html());
        setTimeout(function () {
            $(printWindow.document).find('input[data-role="datepicker"]').each(function (i, item) {
                $(item).val($('#' + $(item).attr('id')).val());
            });
            $(printWindow.document).find('.static-nav').remove();
            $(printWindow.document).find('#ls_panelbar').css('width', 'auto');

            $(printWindow.document).find('span.k-select').each(function (i, item) {
                $(item).remove();
            });

            $(printWindow.document).find('span.k-icon').each(function (i, item) {
                $(item).remove();
            });

            $(printWindow.document).find('a').each(function (i, item) {
                $(item).removeAttr("href");
            });

            //...

            $('input[value=""]').each(function (i, item) {
                $(item).attr('value', $(item).val());
            });

            $('input[role="spinbutton"]').each(function (i, item) {
                $(item).attr('value', $(item).val());
            });

            /* Хлебные крошки */
            var titleEl = $('<div></div>');
            var pathItems = $(window.top.document).find('#PathInfo span.menu-breadcrumb').toArray();
            var breadCrumbs = [];
            $.each(pathItems, function (i, item) {
                breadCrumbs.push(pathItems[i].innerHTML);
            });
            var title = breadCrumbs.join(" / ");

            titleEl.css('padding', '10px');
            titleEl.css('font-size', '20px');
            titleEl.text(title);
            titleEl.prependTo($(printWindow.document).find('body'));

            /* Кем распечатано */
            kendo.ui.progress($('body'), true);
            $.ajax({
                url: '\\Dashboard\\GetUserName',
                type: 'GET',
                success: function (e) {
                    var subTitleEl = $('<div></div>');
                    console.log(currentDashBoardId);
                    var dasbName = $('#dashBoardList').find("li[data-id='" + currentDashBoardId + "']").text();
                    subTitleEl.html(dasbName + '<br/>' + 'Распечатано: ' + new Date().toLocaleString() + '<br/>' + e);
                    subTitleEl.css('padding', '0px 10px 5px 10px');

                    titleEl.after(subTitleEl);

                    //удаление панелей 
                    $(printWindow.document).find('#MainHead').empty();
                    $(printWindow.document).find('#main-left-panel').empty();

                    /* Отобразить окно с отчетом */
                    printWindow.addEventListener('afterprint', function () { printWindow.close(); });
                    printWindow.print();

                    kendo.ui.progress($('body'), false);
                },
                error: function () {
                    kendo.ui.progress($('body'), false);
                }
            });

        }, 200);
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
        var selectedDashboardId = $this.data("id");
        SetDashBoard(selectedDashboardId);
        SetActiveLayout(selectedDashboardId);
    });

    // Изменение размеров панелей и перегрузка панелей после сохранения изменений
    $('#main-left-panel').on('click', '#dashBoardLayoutList li a', function (e) {
        var currentDashBoardId = getCurrentDashBoard();
        var value = $(this).data('value');
        var formData = { 'id': currentDashBoardId, 'layout': value };
        kendo.ui.progress($('body'), true);

        $.ajax({
            type: 'POST',
            url: '\\Dashboard\\SelectLayoutType',
            data: formData,
            success: function (result) {
                kendo.ui.progress($('body'), false);

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
    });

    $('.top-panel').parent().css('height', $('.top-panel').height());
    $('.top-panel').css('position', 'fixed');
    $('.top-panel').css('z-index', 1);
    $('.top-panel').css('width', 'inherit');
});