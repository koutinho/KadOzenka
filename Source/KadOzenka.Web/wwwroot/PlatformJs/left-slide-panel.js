$.widget("ui.leftSlidePanel", {

    _class_common: "left-slide-panel",   // класс всех элементов
    _class_panel: "slide-panel",         // класс главной панели
    _class_handle: "slide-panel-handle", // класс кнопки свернуть / показать панель
    _class_logo: "logo-min",             // класс логотипа
    _class_logo_text_block: "logo-text-block",           // класс текстового блок под логотипом
    _class_notifications: "slide-panel-notify",          // класс для блока уведомлений
    _class_notify_active: "slide-panel-notify-dot",      // класс для индикатора новых уведомлений 
    _class_left_panel_content: "left-panel-content",     // класс для блока содержимого панели
    _class_left_panel_content_close: "left-panel-content-close", // класс, показывающий, что блок содержимого панели - скрыт
    _class_page_content: "page-content",                 // класс для блока содержимого страницы
    _class_separator_element: "left-panel-separator",    // класс для элемента разделителя
    _class_notify_content: "slide-panel-notify-content", // класс для элемента блока уведомлений панели
    _class_notify_message_title: "notify-message-title", // класс для заголовка уведомления
    _class_notify_message_text: "notify-message-text",   // класс для текста уведомления
    _class_left_panel_ddl: "left-panel-ddl",             // класс для списков
    _grab_handle: null,                  // переменная-контейнер для кнопки свернуть / показать панель
    _logo_text_block: null,              // текстовый блок под логотипом
    _panel_content: null,                // переменная-контейнер для блока содержимого панели
    _notifications: null,                // переменная-контейнер для уведомлений
    _notifications_content: null,        // переменная-контейнер для блока уведомлений
    _notification_title: null,           // переменная-контейнер для заголовка уведомлений
    _notification_line: null,            // переменная для линии над уведомлениями
    _close_element: null,                // переменная для элемента сокрытия блока содержимого панели
    _logo: null,                         // переменная-контейнер для логотипа
    _timerId: null,                      // перемнная для хранения идентификатора таймера 
    _sliding: false,                     // устаавливается в 'true' во время анимации раскрытия панели
    _contentLoad: false,                 // устаавливается в 'true' после загрузки контента панели
    _content_load_counter: 0,            // счетчик блоков контента, подгружаемых черех Ajax
    _notifyActive: false,                // устаавливается в 'true' приполучении новых уведомлений
    _methods: {},                        // список методов, которые будут выполнены после загрузки блока содержимого панели
    _class_header: "left-slide-header",  // класс для элементов, статичных при скролле

    options: {
        width: 302,         // ширина панели
        disabled: false,    // отключение панели
        dock: "left",       // по-умолчанию 'left'
        hidden: false,      // панель скрыта
        opacity: 1,         // прозрачность
        open: true,         // true - открыта, false - скрыта
        peek: 113,          // наскоько далеко в окно выдвинута панель
        position: 9,        // percentage position of the handle, 0 = top, 50 = middle, 100 = bottom
        speed: 400,         // скорость анимации, мс
        site_url: "/",      // количество загружаемых сообщений сообщений,
        load_content_url:    "/LeftPanel/LoadContent",       // url для загрузки контента панели
        load_messages_url:   "/CoreMessages/GetMessageList", // url для загрузки непрочитанных сообщений
        load_messages_count: "/CoreMessages/GetMessageList", // количество загружаемых сообщений сообщений,
        frequency_load_messages: 30, // частота опросов для загрузки уведомлений, в секундах
        panelId: "",                 // идентификатор панели
        registerSettings: null,      // параметры реестра
        uniqueSessionKey: "",        // уникальный идентификатор сессии
        context: ""                  // контекст формирования левой панели (может быть равен RegisterView или undefined)
    },

    _create: function () {
        var self = this;

        if (!self.options.panelId) {
            self.element.remove();
            return;
        }

        self.element
            .addClass(self._class_common)
            .addClass(self._class_panel);

        self._logo = $("<a></a>").addClass(self._class_logo).addClass(self._class_header);
        self.element.prepend(self._logo);

        self._grab_handle = $("<span></span>")
            .addClass(self._class_common)
            .addClass(self._class_header)
            .addClass(self._class_handle);
        self._logo.after(self._grab_handle);

        self._panel_content = $("." + self._class_left_panel_content);

        self._notifications = $("<div><hr class='notify-line' width='30px' align='left'><a href='/RegistersView/CoreMessagesMy'><span class='icon-notification'><span class='slide-panel-notify-dot' style='visibility: hidden;'></span></span><span class='notify-title'>Уведомления</span></a></div>")
            .addClass(self._class_notifications);
        self.element.append(self._notifications);

        self._notification_title = self._notifications.find(".notify-title");

        self._notification_line = self._notifications.find(".notify-line");

        self._notifications_content = $("<ul></ul>")
            .addClass(self._class_notify_content)
            .css("display", "none");
        self._notifications.append(self._notifications_content);

        self._logo_text_block = $("." + self._class_logo_text_block);
        self._logo_text_block.addClass(self._class_header);

        self._page_content = $("." + self._class_page_content);

        self._load_content();

        self._load_notifications();
        self._timerId = setInterval(function () {
            self._load_notifications();
        }, self.options.frequency_load_messages * 1000);

        self._grab_handle.click(function (e) {
            self.toggle();
        });

        self._setOptions({
            "width": self.options.width,
            "disabled": self.options.disabled,
            "dock": self.options.dock,
            "hidden": self.options.hidden,
            "opacity": self.options.opacity,
            "open": self.options.open,
            "peek": self.options.peek,
            "position": self.options.position,
            "speed": self.options.speed,
            "panelId": self.options.panelId,
            "site_url": self.options.site_url,
            "registerSettings": self.options.registerSettings,
            "uniqueSessionKey": self.options.uniqueSessionKey,
            "context": self.options.context,
        });

        self._logo.attr('href', self.options.site_url);

        $(window).resize(function () {
            self.refresh();  // обновляем панель при изменении размеров окна
        });

        self.element.on('click', '.group-title[data-item-type="list"]', function () {
            var $this = $(this);

            var parent = $this.closest('.' + self._class_left_panel_content);
            if (parent.hasClass(self._class_left_panel_content_close))
                self.open(true);

            var $ul = $this.next('ul');
            self._list_items_hide($this, $ul.css('display') == 'none');
        });

        self.element.on('click', '.group-title[data-item-type="button"]:not(.el-disable)', function () {
            CallAction($(this));
        });

        self.element.on('click', '.' + self._class_left_panel_ddl + ' li a:not(.el-disable)', function (e) {
            e.preventDefault();
            var $this = $(this);
            var parent = $this.closest('.' + self._class_left_panel_ddl);

            if (parent.find('li a.active').length > 0)
                parent.find('li a.active').removeClass('active');

            $this.toggleClass('active');

            CallAction($this);
        });

        function CallAction(el) {
            var grid = self.options.registerSettings ?
                $('#Grid-' + self.options.registerSettings.CurrentRegisterId).data('kendoGrid') :
                undefined;

            if (grid && el.data('command') === 'ExportToExcel') {
                ExportToExcel();
                return;
            }

            var url = el.attr('data-url');
            if (url) {
                if (el.data('open-in-radwindow')
                    && el.data('open-in-radwindow').toString().toLowerCase() === "true") {
                    var title = el.hasClass("active") ?
                        el.text() :
                        el.find('.left-panel-content-title').text();

                    Common.UI.ShowWindow(title, url, 'registerModalWindow',
                        el.data('need-refresh').toString().toLowerCase() === "true" && grid ?
                            function () { grid.dataSource.read(); }
                            : null,
                        el.data('window-width'),
                        el.data('window-height'));
                }
                else if (el.data('open-in-new-window')
                    && el.data('open-in-new-window').toString().toLowerCase() === "true") {
                    var newWindow = window.open(url, '_blank');
                    newWindow.focus();
                }
                else {
                    window.location.href = url;
                }
            }
        }

        function ExportToExcel() {
            var grid = self.options.registerSettings ?
                $('#Grid-' + self.options.registerSettings.CurrentRegisterId).data('kendoGrid') :
                undefined;

            if (grid) {
                var parameters = grid.GetDataFunc();
                parameters.CurrentLayoutId = self.options.registerSettings.CurrentLayoutId;
                var url = self.options.registerSettings.RegistersExportToExcelUrl + '?parametersJson=' + encodeURIComponent(JSON.stringify(parameters));
                window.open(url, '_blank');
            }
        }
    },

    _destroy: function (command, ) {

        this.element
            .removeClass(this._class_common)
            .removeClass(this._class_panel);

        if (this._grab_handle !== null) {
            this._grab_handle.remove();
        }
    },

    // устанавливаем параметры

    _setOption: function (key, value) {

        var self = this;

        var handlers = {
            "width": function () { self.width(value); },
            "disabled": function () { self.disabled(value); },
            "hidden": function () { self.hidden(value); },
            "opacity": function () { self.opacity(value); },
            "open": function () { self.open(value); },
            "peek": function () { self.peek(value); },
            "speed": function () { self.speed(value); },
            "panelId": function () { self.panelId(value); },
            "registerSettings": function () { self.registerSettings(value); },
            "uniqueSessionKey": function () { self.uniqueSessionKey(value); },
            "context": function () { self.context(value); },
            "load_content_url": function () { self.load_content_url(value); },
            "site_url": function () { self.site_url(value); }
        };

        if (key in handlers) {
            handlers[key]();
        }

        this._super(key, value);
    },

    // репозиционируем панель

    refresh: function () {
        self._contentLoad = false;
        this._reposition();
    },

    // переключаем режим открытия панели

    toggle: function () {

        this.open(!this.options.open);
    },

    // Устанавливаем ширину панели

    width: function (width) {

        if (width === undefined) {
            return this.options.width;
        }

        this.options.width = this._valid_integer(width);
        this._reposition();
    },

    // устанавливаем опцию disabled, останавливаем функции open / close (замораживаем в текущем состоянии)

    disabled: function (disabled) {

        if (disabled === undefined) {
            return this.options.disabled;
        }

        this.options.disabled = this._valid_boolean(disabled);  // будет применено при следующем вызове open / close
    },

    hidden: function (hidden) {

        if (hidden === undefined) {
            return this.options.hidden;
        }

        this.options.hidden = this._valid_boolean(hidden);
        this.options.hidden ? this.element.hide() : this.element.show();
        this.options.hidden ? this._grab_handle.hide() : this._grab_handle.show();
    },

    // устанавливаем прозрачность

    opacity: function (opacity) {

        if (opacity === undefined) {
            return this.options.opacity;
        }

        this.options.opacity = this._valid_float(opacity);
        this._reposition();
    },

    open: function (open) {

        if (open === undefined) {
            return this.options.open;
        }

        if (this.options.disabled) {

            this._trigger("slide", null, { open: this.options.open, dock: this.options.dock });

        } else {

            open = this._valid_boolean(open);

            if (this.options.open != open) {

                this.options.open = open;
                this._sliding = true;

                this._button_reposition();
                this._logo_reposition();
                this._notification_reposition();
                this._toggle_size_page_content();
                this._toggle_notifications_content();

                var amount = this.element.outerWidth();
                var self = this;
                var propPanel = {};

                propPanel[this.options.dock] = (open ? "+=" : "-=") + (amount - this.options.peek);

                $(this.element).animate(propPanel, this.options.speed, "linear", function () {
                    self._sliding = false;
                    self._trigger("slide", null, { open: self.options.open, dock: self.options.dock });
                });

                this._hide_content();

                this._logo_text_hide(!open);

                if (this.options.open) {
                    this.element.css("overflow", "");
                    this.element.css("overflow-x", "hidden");
                } else {
                    this.element.css("overflow", "hidden");
                    this.element.css("overflow-x", "");
                }
                
                $(window).trigger('resize');

                if (open) {
                    setTimeout(function() {

                            var maxBottom = 0;

                            $('.' + self._class_header).each(function(index, element) {

                                if (($(element).position().top + $(element).outerHeight(true)) > maxBottom) {
                                    maxBottom = $(element).position().top + $(element).outerHeight(true);
                                }

                                $(element).css('top', $(element).position().top);
                                $(element).css('left', $(element).position().left);
                                $(element).css('z-index', 3);
                            });

                            $('.' + self._class_header).each(function(index, element) {
                                $(element).css('position', 'fixed');
                            });

                            $('.' + self._class_left_panel_content).css('margin-top', maxBottom);
                            $('.' + self._class_left_panel_content).css('z-index', 2);

                            var replaceBlock = $('<div></div>')
                                .addClass('left-panel-replace-block')
                                .css('z-index', 2)
                                .css('height', maxBottom)
                                .css('width', self.element.width())
                                .css('position', 'fixed')
                                .css('background-color', self.element.css('background-color'));

                            $('.' + self._class_header).last().after(replaceBlock);

                        },
                        this.options.speed); //todo
                } else {
                    $('.left-panel-replace-block').remove();

                    $('.' + self._class_header).each(function (index, element) {
                        $(element).css('top', '');
                        $(element).css('left', '');
                        $(element).css('z-index', '');
                        $(element).css('position', '');
                    });

                    $('.' + self._class_left_panel_content).css('margin-top', '');
                    $('.' + self._class_left_panel_content).css('z-index', '');
                }
            }
        }
    },
    
    bind_events: function () {
        var self = this;
        var gridSelector = '#Grid-' + self.options.registerSettings.CurrentRegisterId;
        var grid = self.options.registerSettings ? $(gridSelector).data('kendoGrid') : undefined;

        //подписи на события
        if (grid) {
            grid.bind("change", onGridSelectionChange);
        }       

        function onGridSelectionChange (e) {
            var selectedRow = this.select(),
                dataItem = this.dataItem(selectedRow),
                objectId = dataItem ? dataItem.id : null,
                parameters = {
                    registerId: self.options.registerSettings.RegisterId,
                    objectId: objectId,
                    registerViewId: self.options.registerSettings.RegisterViewId,
                    uniqueSessionKey: self.options.registerSettings.UniqueSessionKey,
                    callBackFunction: $_GET['CallBackFunction']
                };

            if (self.options.registerSettings.FilterRegisterId) {
                parameters.filterRegisterId = self.options.registerSettings.FilterRegisterId;
            }

            var requestUrl = $(gridSelector).parent().parent().parent().find('[name=RequestUrl]').val();

            var urlParameters = Common.GetUrlParameters(requestUrl);
            if (urlParameters.length > 0) {
                for (var i = 0; i < urlParameters.length; i++) {
                    if (!Common.IsExistsKeyInObject(parameters, urlParameters[i].Name)) {
                        parameters[urlParameters[i].Name] = urlParameters[i].Value;
                    }
                }
            }

            $.ajax({
                url: self.options.registerSettings.RegistersUpdateStateUrl,
                type: 'GET',
                data: parameters,
                dataType: 'json',
                success: function (states) {
                    if (states) {
                        self.UpdateButtonsState(states.toolbarButtonStates);
                    }
                }
            });
        }
    },

    // обновление состояния кнопок левой панели, после изменения выбранной строки грида

    UpdateButtonsState: function (states) {
        var self = this;

        if (states) {
            for (var i = 0; i < states.length; i++) {
                var buttonState = states[i],
                    buttonSelector = '[data-button-id=' + buttonState.Id + ']';

                var buttonEl = null;

                if (buttonState.OwnerDropdownButtonId) {
                    buttonEl = self._panel_content
                        .find('[data-button-id=' + buttonState.OwnerDropdownButtonId + ']')
                        .find(buttonSelector);

                    buttonEl.parent().toggleClass("hidden", buttonState.Hidden);
                }
                else {
                    buttonEl = self._panel_content
                        .find(buttonSelector)
                        .not('[data-owner-dropdown-button-id]');

                    buttonEl.toggleClass("hidden", buttonState.Hidden);
                    buttonEl.next('.left-panel-separator').toggleClass("hidden", buttonState.Hidden);
                }
                              

                if (buttonState.Url) {
                    //не изменяем сам аттрибут, только jquery.cache
                    buttonEl.attr('data-url', buttonState.Url);
                }

                buttonEl.toggleClass("el-disable", !buttonState.Enable);
            }
        }
    },

    ReloadContent: function (func) {
        this._content_load_counter = 0;
        this._contentLoad = false;
        this._panel_content.empty();

        this._load_content(func);
    },

    AddMethod: function (name, func) {
        self = this;
        if (typeof (name) != "string" || !$.isFunction(func)) {
            return;
        }

        if (self._methods[name] == undefined) {
            self._methods[name] = func;
        }
    },
       
    peek: function (peek) {
        if (peek === undefined) {
            return this.options.peek;
        }

        this.options.peek = this._valid_integer(peek);
        this._reposition();
    },

    // Задаем скорость открытия/скрытия панели

    speed: function (speed) {
        if (speed === undefined) {
            return this.options.speed;
        }

        this.options.speed = this._valid_integer(speed);
    },

    // Задаем url для загрузки контента панели

    load_content_url: function (url) {
        if (url === undefined) {
            return this.options.load_content_url;
        }

        this.options.load_content_url = url;
    },

    // Задаем url для главного экрана сайта

    site_url: function (url) {
        if (url === undefined) {
            return this.options.site_url;
        }

        this.options.site_url = url;
    },

    // Задаем файл конфигурации для левой панели

    panelId: function (panelId) {
        this.options.panelId = panelId;
    },

    // параметры реестра

    registerSettings: function (registerSettings) {
        this.options.registerSettings = registerSettings;
    },

    uniqueSessionKey: function (uniqueSessionKey) {
        this.options.uniqueSessionKey = uniqueSessionKey;
    },

    context: function (context) {
        this.options.context = context;
    },

    content_elements_hide: function () {
        this._panel_content;
    },

    _logo_text_hide: function (show) {
        if (show)
            this._logo_text_block.hide(500);
        else
            this._logo_text_block.show(500);
    },

    _toggle_size_page_content: function () {
        var self = this;

        if (self.options.open) {
            this._page_content.css("width", "calc(100% - 328px)");
            this._page_content.css("left", "328px");
        }
        else {
            this._page_content.css("width", "calc(100% - " + this.options.peek + "px)");
            this._page_content.css("left", this.options.peek + "px");
        }
        this._page_content.css("position", "relative");
    },

    _toggle_notification_active: function (flag) {
        flag = this._valid_boolean(flag);

        if (flag != this._notifyActive) {
            this._notifications.find('.' + this._class_notify_active).css('visibility', (flag ? 'inherit' : 'hidden'));
            this._notifyActive = flag;
        }
    },

    _toggle_notifications_content: function () {
        this.options.open ?
            this._notifications_content.show() :
            this._notifications_content.hide();
    },

    _button_reposition: function () {
        if (this.options.open) {
            this._grab_handle.html("<span class='icon-arrow_left_big'></span >");
            this._grab_handle.removeClass("slide-panel-handle-close");
            this._logo.addClass("slide-panel-logo-open");
            this._logo.removeClass("slide-panel-logo-close");
        }
        else {
            this._grab_handle.html("<span class='icon-burger_menu_big'></span>");
            this._grab_handle.addClass("slide-panel-handle-close");
            this._logo.removeClass("slide-panel-logo-open");
            this._logo.addClass("slide-panel-logo-close");
        }
    },

    _logo_reposition: function () {
        if (this.options.open) {
            this._logo.addClass("slide-panel-logo-open");
            this._logo.removeClass("slide-panel-logo-close");
        }
        else {
            this._logo.removeClass("slide-panel-logo-open");
            this._logo.addClass("slide-panel-logo-close");
        }
    },

    _notification_reposition: function () {
        if (this.options.open) {
            this._notifications.removeClass("slide-panel-notify-close");
            this._notification_title.show();
            this._notification_line.hide();
        }
        else {
            this._notifications.addClass("slide-panel-notify-close");
            this._notification_title.hide();
            this._notification_line.show();
        }
    },

    _load_content: function (func) {
        var self = this;

        if (!self._contentLoad && self.options.load_content_url) {
            $.ajax({
                url: self.options.load_content_url,
                type: 'GET',
                data: { leftPanelId: self.options.panelId, context: self.options.context, uniqueSessionKey: self.options.uniqueSessionKey },
                success: function (response) {
                    if (response) {
                        self._panel_content.append(response.Value);

                        var afterLoadList = self._panel_content.find('ul[data-afterloadfunction]');
                        if (afterLoadList.length > 0) {
                            afterLoadList.each(function () {
                                var func = $(this).data('afterloadfunction');

                                if (typeof window[func] === 'function') {
                                    self.AddMethod(func, function () {
                                        window[func]();
                                    });
                                }
                            });
                        }

                        if (func) {
                            self.AddMethod("func" + (Object.keys(self._methods).length + 1), func);
                        }

                        var ulList = self._panel_content.find('ul[data-loaditemsurl]');

                        if (ulList.length > 0) {
                            ulList.each(function () {
                                self._load_list_items(this, ulList.length);
                            });
                        }
                        else
                            self._contentLoad = true;

                        self._hide_content();
                    }
                },
                error: function (request) {
                    self._log('ошибка загрузки контента, url: ' + self.options.load_content_url + ' ' + request.status + ' ' + request.statusText, true);
                }
            });
        }
    },

    _load_list_items: function (item, count) {
        var self = this;

        if (item) {
            var $item = $(item);
            var url = $item.data('loaditemsurl');

            if (url) {
                $.ajax({
                    url: url,
                    type: 'GET',
                    data: { count: self.load_messages_count },
                    success: function (response) {
                        if (response) {
                            var len = response.length;

                            $.each(response, function (key, value) {
                                $item.append('<li ' + (key + 1 == len ? 'style="border: none" ' : '') + '" title="' + value.name + '"><a ' + (value.active ? 'class="active" ' : '') + 'data-id="' + value.id + '" href="#">' + value.name + '</a></li>');
                            });
                        }
                    },
                    complete: function (e) {
                        self._set_content_load_counter(count);
                    },
                    error: function (request) {
                        self._log('ошибка загрузки элементов списка, url: ' + url + ' ' + request.status + ' ' + request.statusText, true);
                    }
                });
            }
        }
    },

    _load_notifications: function () {
        var self = this;

        $.ajax({
            url: self.options.load_messages_url,
            type: 'GET',
            success: function (response) {
                if (response && !$.isEmptyObject(response)) {
                    var len = response.length;

                    if (len > 0) {
                        self._notifications_content.html('');

                        $.each(response, function (key, value) {

                            var $li = $('<li ' + (key + 1 == len ? 'style="border: none" ' : '') + 'data-id="' + value.Id + '"><span' + value.name + '</a></li>');
                            $li.append('<div class="' + self._class_notify_message_title + '">' + value.Subject + '<br/><span class="notify-message-date">' + value.Date + '</span></div>');
                            $li.append('<div class="' + self._class_notify_message_text + '">' + value.Message + '</div>');

                            self._notifications_content.append($li);
                        });

                        self._toggle_notification_active(true);
                    } else {
                        self._toggle_notification_active(false);
                    }
                }
            },
            error: function (request) {
                self._log('ошибка загрузки списка уведомлений, url: ' + self.options.load_messages_url + ' ' + request.status + ' ' + request.statusText, true);
            }
        });
    },

    _list_items_hide: function (el, contentHide) {
        var $ul = el.next('ul');

        if (contentHide) {
            el.find('img.lp_arrow').attr('src', '../images/arrow_up.png');
            $ul.show(500);

        } else {
            el.find('img.lp_arrow').attr('src', '../images/arrow_down.png');
            $ul.hide(500);
        }
    },

    _hide_content: function () {
        var self = this;
        var els = self._panel_content.find('.lp-con-el');

        if (self.options.open) {
            els.show();
            self._panel_content.find('.' + self._class_separator_element).css("visibility", "inherit"); 
        } else {
            els.hide();
            self._panel_content.find('.' + self._class_separator_element).css("visibility", "hidden");
            els.each(function () {
                self._list_items_hide($(this).closest('.group-title'), false);
            });
        }
        self._panel_content.toggleClass(self._class_left_panel_content_close, !self.options.open);
    },

    _set_content_load_counter(count) {
        var self = this;
        self._content_load_counter++;

        if (self._content_load_counter == count) {
            self._contentLoad = true;

            // выполняем все методы, которые должны выполниться после загрузки контента
            $.each(self._methods, function (key, func) {
                func();
            });
        }
    },

    _stop_timer: function () {
        clearInterval(this._timerId);
    },

    _reposition: function () {

        this._button_reposition();
        this._logo_reposition();
        this._notification_reposition();
        this._logo_text_block.toggle(this.options.open);
        this._toggle_size_page_content();

        this.element.css("width", "");
        this.element.css("height", "");

        this.element.css("left", "");
        this.element.css("top", "");
        this.element.css("right", "");
        this.element.css("bottom", "");
        this.element.css("opacity", this.options.opacity);

        //this._grab_handle.css("left", "");
        //this._grab_handle.css("top", "");
        //this._grab_handle.css("right", "");
        //this._grab_handle.css("bottom", "");
        this._grab_handle.css("opacity", this.options.opacity);

        // Задаем ширину

        this.element.css("width", this.options.width);
        this.element.css("top", "0");
        this.element.css("bottom", "0");

        var size_main = this.element.outerWidth();

        // задаем состояние открыта / скрыта
        this.element.css(this.options.dock, this.options.open ? 0 : -1 * (size_main - this.options.peek));
    },

    _valid_boolean: function (boolean) {
        if (jQuery.type(boolean) === "string") {

            if ($.inArray(boolean.trim().toLowerCase(), ["0", "false", "no"]) !== -1) {
                boolean = false;
            }
        }

        return boolean ? true : false;
    },

    _valid_integer: function (integer) {

        return (isNaN(parseInt(integer))) ? 0 : integer;
    },

    _valid_float: function (float) {

        return (isNaN(parseFloat(float))) ? 0 : float;
    },

    _log: function (msg, warn) {
        if (window.console) {
            var message = 'leftSlidePanel -> ' + msg;
            if (warn) {
                console.warn(message);
            } else {
                console.log(message);
            }
        }
    }
});
