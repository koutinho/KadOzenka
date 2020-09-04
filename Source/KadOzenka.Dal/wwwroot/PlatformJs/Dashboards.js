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
                url: dashboardSettings.SaveDashboardUrl,
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
                                    var $li = $('#dashBoardList li').has('a[data-id=' + result.Id + ']');
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
                    Common.UI.ShowErrorDialog(result.responseText);
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
        url: dashboardSettings.DashboardPanelsUrl,
        data: formData,
        success: function (result) {
            $("#dashBoardPanels").html(result);
            LoadAsyncIndexCardData();
            CheckForHide();
        },
        error: function (result) {
            kendo.ui.progress($('body'), false);
            Common.UI.ShowErrorDialog(result.responseText);
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

function SetTitle(title) {
    if (!title)
        title = $('#dashBoardList a.active').length ? $('#dashBoardList a.active').text() : $('#dashBoardList').find('li:first a').text();
    $('.top-panel .first-row .main-header').html(title);
}

function SetDashBoard(selectedDashboardId) {
    kendo.ui.progress($("body"), true);
    $.ajax({
        type: 'POST',
        url: dashboardSettings.DashboardPanelsUrl,
        data: { 'id': selectedDashboardId },
        success: function (result) {
            SetTitle($('#dashBoardList li a[data-id=' + selectedDashboardId + ']').text());
            $("#dashBoardPanels").html(result);
            CheckForHide();   
            kendo.ui.progress($("body"), false);
            FormatIndexCardValues();
            LoadAsyncIndexCardData();
            SetUserDefaultDashboard();
        },
        error: function (result) {
            Common.UI.ShowErrorDialog(result.responseText);
            kendo.ui.progress($("body"), false);
        }
    });
}

function SetActiveLayout(dashBoard) {
    if (!dashBoard)
        dashBoard = getCurrentDashBoard();

    $.ajax({
        type: 'GET',
        url: dashboardSettings.GetCurrentLayoutTypeUrl,
        data: { id: dashBoard },
        success: function (result) {
            $("#dashBoardLayoutList a.active").removeClass('active');
            $("#dashBoardLayoutList a[data-value=" + result + "]").addClass("active");
        }
    });
}

function SetUserDefaultDashboard() {
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

    var subsystem = Common.GetUrlParameter('Subsystem');

    $.ajax({
        url: dashboardSettings.SetUserDefaultDashboardUrl,
        type: 'POST',
        data: { 'id': currentDashBoardId, 'subsystem': subsystem },
        success: function () {
        }
    });
}

function FormatIndexCardValues() {
    var $needFormatNumbers = $(".need-format-number");
    if ($needFormatNumbers.length) {
        $.each($needFormatNumbers, function () {
            this.text = Common.Functions.FormatNumber(this.text);
        });
    }
}

function LoadAsyncIndexCardData() {
    var $asyncPanels = $('.index-card-panel[data-type-load=Async]');
    if ($asyncPanels.length) {
        $.each($asyncPanels, function () {
            var $this = $(this);
            kendo.ui.progress($this, true);
            var $id = $this.data('id');

            $.ajax({
                url: dashboardSettings.GetIndexCardDataUrl,
                type: 'GET',
                data: { id: $id },
                success: function (data) {
                    if (data) {
                        $.each(data, function () {
                            $('.index-card-panel[data-id=' + this.Id + ']').find('a[data-num=' + this.Num + ']').text(Common.Functions.FormatNumber(this.Value));
                        });
                    }
                    kendo.ui.progress($('.index-card-panel[data-id=' + data[0].Id + ']'), false);
                },
                error: function (request) {
                    kendo.ui.progress($this, false);
                    console.log(request);
                }
            });
        });
    }
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
}

$(function () {
    HideEdit(); 
    FormatIndexCardValues();
    LoadAsyncIndexCardData();

    //*****Быстрый поиск*****//
    var mainWindow = window.parent || window;
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

    $('.mainContent').on('click', '.menu-panel-row-header.menu-panel-row-header-url', function () {
        var headerUrl = $(this).data('url');
        if (headerUrl) {
            mainWindow.location.assign(headerUrl);
        }
    });

    $('.mainContent').on('click', '.menu-panel-row-search a', function (e) {
        e.preventDefault();
        triggerSearch($(this).closest('.menu-panel-row-search').find('input').first());
    });

    $('.mainContent').on('keyup', '.menu-panel-row-search input', function (e) {
        e.preventDefault();
        //поиск на Enter
        if (e.keyCode == 13) {
            triggerSearch($(this));
        }
    });

    $('.mainContent').on('click', '.open-in-radwindow', function (e) {
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
    //*****Быстрый поиск*****//

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
        addGadget.refresh({ url: dashboardSettings.AddGadgetUrl + '?Id=' + currentDashBoardId, type: "GET" });
        addGadget.maximize();
        addGadget.open();
    });

    $('#main-left-panel').on('click', '#buttonCreateDashBoard', function (e) {
        e.preventDefault();
        var createDashboard = $('#Dashboard').data('kendoWindow');
        createDashboard.title('Новый Рабочий стол');
        var subsystem = Common.GetUrlParameter('Subsystem');
        createDashboard.refresh({ url: dashboardSettings.GetDashboardUrl + (subsystem ? '?subsystem=' + subsystem : ''), type: "GET" });
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
        editDashboard.refresh({ url: dashboardSettings.GetDashboardUrl + '?Id=' + currentDashBoardId, type: "GET" });
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
        editDashboard.refresh({ url: dashboardSettings.GetDashboardUrl + '?CopyId=' + currentDashBoardId, type: "GET" });
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
                    url: dashboardSettings.DeleteDashboardUrl,
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
                        Common.UI.ShowErrorDialog(result.responseText);
                    }
                });
            }
        });
    });

    $('#main-left-panel').on('click', '#buttonDefaultDashBoard', function (e) {
        e.preventDefault();
        SetUserDefaultDashboard();
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
                url: dashboardSettings.GetUserNameUrl,
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

        window.location = dashboardSettings.ExportToExcelUrl + '?id=' + currentDashBoardId;
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
	$('#main-left-panel').on('click', '#dashBoardLayoutList li a:not(#customView)', function (e) {
        var currentDashBoardId = getCurrentDashBoard();
        var value = $(this).data('value');
        var formData = { 'id': currentDashBoardId, 'layout': value };
        kendo.ui.progress($('body'), true);

        $.ajax({
            type: 'POST',
            url: dashboardSettings.SelectLayoutTypeUrl,
            data: formData,
            success: function (result) {
                kendo.ui.progress($('body'), false);

                $("#dashBoardPanels").html(result);
                CheckForHide();
            },
            error: function (result) {
                kendo.ui.progress($('body'), false);
                Common.UI.ShowErrorDialog(result.responseText);
            }
        });
	});

	//Вызов кастомных настроек панелей рабочего стола и сохранение их 
	var containerWindow = $('<div id="windowLayoutCustomView"></div>');
	$('body').append(containerWindow);

	$('#main-left-panel').on('click',
		'#dashBoardLayoutList li a#customView',
		function() {
			var windowForGridLink = containerWindow.kendoWindow({
				title: "Настройка отображения рабочего стола",
				visible: false,
				resizable: false,
				modal: true,
				close: function (e) {
					this.destroy();
				}
			}).data('kendoWindow');

			var currentDashboardId = getCurrentDashBoard();
			var url = dashboardSettings.CreateCustomLayoutUrl(currentDashboardId); 
			windowForGridLink.refresh(url);
			
			windowForGridLink.maximize().open();
		});

    $('.top-panel').parent().css('height', $('.top-panel').height());
    $('.top-panel').css('position', 'fixed');
    $('.top-panel').css('z-index', 1);
    $('.top-panel').css('width', 'inherit');
});