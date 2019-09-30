$.widget( ".leftSlidePanel", {

    _class_common: "left-slide-panel",   // класс всех элементов
    _class_panel: "slide-panel",         // класс главной панели
    _class_handle: "slide-panel-handle", // класс кнопки свернуть / показать панель
    _class_logo: "logo-min",             // класс логотипа
    _class_logo_text_block: "logo-text-block",           // класс текстового блок под логотипом
    _class_notifications: "slide-panel-notify",          // класс для блока уведомлений
    _class_notify_active: "slide-panel-notify-dot",      // класс для индикатора новых уведомлений 
    _class_left_panel_content: "left-panel-content",     // класс для блока содержимого панели
    _class_page_content: "page-content",                  // класс для блока содержимого страницы
    _class_close_element: "lp-close-element",            // класс для элемента сокрытия блока содержимого панели
    _class_show_element: "lp-open-element",              // класс для элемента раскрытия блока содержимого панели
    _class_separator_element: "left-panel-separator",    // класс для элемента разделителя
    _class_notify_content: "slide-panel-notify-content", // класс для элемента блока уведомлений панели
    _class_notify_message_title: "notify-message-title", // класс для заголовка уведомления
    _class_notify_message_text: "notify-message-text",   // класс для текста уведомления
    _grab_handle: null,                  // переменная-контейнер для кнопки свернуть / показать панель
    _logo_text_block: null,              // текстовый блок под логотипом
    _panel_content: null,                // переменная-контейнер для блока содержимого панели
    _notifications: null,                // переменная-контейнер для уведомлений
    _notifications_content: null,        // переменная-контейнер для блока уведомлений
    _notification_title: null,           // переменная-контейнер для заголовка уведомлений
    _notification_line: null,            // переменная для линии над уведомлениями
    _close_element: null,                // переменная для элемента сокрытия блока содержимого панели
    _logo: null,                         // переменная-контейнер для логотипа
    _timerId: null,                       // перемнная для хранения идентификатора таймера 
    _sliding: false,                     // устаавливается в 'true' во время анимации раскрытия панели
    _panelId: "Dashboard",               // идентификатор панели
    _contentLoad: false,                 // устаавливается в 'true' после загрузки контента панели
    _notifyActive: false,                // устаавливается в 'true' приполучении новых уведомлений

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
        load_content_url: "/LeftPanel/LoadContent",          // url для загрузки контента панели
        load_messages_url: "/CoreMessages/GetMessageList",   // url для загрузки непрочитанных сообщений
        load_messages_count: "/CoreMessages/GetMessageList", // количество загружаемых сообщений сообщений,
        frequency_load_messages: 30 // частота опросов для загрузки уведомлений, в секундах
    },

    _create: function () {
        var self = this;

        this.element
            .addClass( this._class_common )
            .addClass( this._class_panel );

        this._logo = $("<span></span>").addClass(this._class_logo);
        this.element.prepend(this._logo);

        this._grab_handle = $("<span></span>")
            .addClass(this._class_common)
            .addClass(this._class_handle);
        this._logo.after(this._grab_handle);

        this._panel_content = $("." + this._class_left_panel_content);
        if (!this.options.open)
            this._panel_content.css("display", "none");

        this._notifications = $("<div><hr class='notify-line' width='30px' align='left'><a href='/RegistersView/CoreMessagesMy'><span class='icon-notification'><span class='slide-panel-notify-dot' style='visibility: hidden;'></span></span><span class='notify-title'>Уведомления</span></a></div>")
            .addClass(this._class_notifications);
        this.element.append(this._notifications);

        this._notification_title = this._notifications.find(".notify-title");

        this._notification_line = this._notifications.find(".notify-line");

        this._notifications_content = $("<ul></ul>")
            .addClass(this._class_notify_content)
            .css("display", "none");
        this._notifications.append(this._notifications_content);

        this._logo_text_block = $("." + this._class_logo_text_block);

        this._page_content = $("." + this._class_page_content);

        /*this._close_element = $("<div></div>")
            .addClass(this._class_close_element);
        this._panel_content.append(this._close_element);*/

        self._load_notifications();
        this._timerId = setInterval(function () {
            self._load_notifications();
        }, this.options.frequency_load_messages * 1000);

        this._grab_handle.click( function (e) {
            self.toggle();
        });

        this._setOptions({
            "width": this.options.width,
            "disabled": this.options.disabled,
            "dock": this.options.dock,
            "hidden": this.options.hidden,
            "opacity": this.options.opacity,
            "open": this.options.open,
            "peek": this.options.peek,
            "position": this.options.position,
            "speed": this.options.speed
        });

        $(window).resize ( function () {
            self.refresh();  // обновляем панель при изменении размеров окна
        });

        this.element.on('click', '.group-title', function () {
            var $this = $(this);

            var $ul = $this.next('ul');

            var contentHide = $ul.css('display') == 'none';

            if (contentHide) {
                //self._panel_content.find('.' + self._class_separator_element).css("visibility", "hidden");
                $this.find('img.lp_arrow').attr('src', '../images/arrow_up.png');
                $ul.show(500);

            } else {
                //self._panel_content.find('.' + self._class_separator_element).css("visibility", "inherit"); 
                $this.find('img.lp_arrow').attr('src', '../images/arrow_down.png');
                $ul.hide(500);
            }
        });
    },

    _destroy: function () {

        this.element
            .removeClass( this._class_common )
            .removeClass( this._class_panel );

        if ( this._grab_handle !== null ) {
             this._grab_handle.remove();
        }
    },

    // устанавливаем параметры

    _setOption: function ( key, value ) {

        var self = this;

        var handlers = {
            "width": function () { self.width( value ); },
            "disabled": function () { self.disabled( value ); },
            "hidden": function () { self.hidden( value ); },
            "opacity": function () { self.opacity( value ); },
            "open": function () { self.open( value ); },
            "peek": function () { self.peek( value ); },
            "speed": function () { self.speed(value); },
            "load_content_url": function () { self.load_content_url(value); }
        };

        if ( key in handlers ) {
            handlers[key]();
        }

        this._super( key, value );
    },    

    // репозиционируем панель

    refresh: function () {
        self._contentLoad = false;
        this._reposition();
    },

    // переключаем режим открытия панели

    toggle: function () {

        this.open( !this.options.open );
    },

    // Устанавливаем ширину панели

    width: function (width ) {

        if (width === undefined ) {
            return this.options.width;
        }

        this.options.width = this._valid_integer ( width );
        this._reposition();
    },

    // устанавливаем опцию disabled, останавливаем функции open / close (замораживаем в текущем состоянии)

    disabled: function ( disabled ) {

        if ( disabled === undefined ) {
            return this.options.disabled;
        }

        this.options.disabled = this._valid_boolean( disabled );  // будет применено при следующем вызове open / close
    },

    hidden: function ( hidden ) {

        if ( hidden === undefined ) {
            return this.options.hidden;
        }

        this.options.hidden = this._valid_boolean( hidden );
        this.options.hidden ? this.element.hide() : this.element.show();
        this.options.hidden ? this._grab_handle.hide() : this._grab_handle.show();
    },

    // устанавливаем прозрачность

    opacity: function ( opacity ) {

        if ( opacity === undefined ) {
            return this.options.opacity;
        }

        this.options.opacity = this._valid_float( opacity );
        this._reposition();
    },

    open: function ( open ) {

        if ( open === undefined ) {
            return this.options.open;
        }

        if ( this.options.disabled ) { 

            this._trigger( "slide", null, { open: this.options.open, dock: this.options.dock } );

        } else {

            open = this._valid_boolean( open );

            if ( this.options.open != open ) {

                this.options.open = open;
                this._sliding = true;

                this._button_reposition();
                this._logo_reposition();
                this._notification_reposition();
                this._toggle_size_page_content();
                this._load_content();
                this._toggle_notifications_content();

                var amount = this.element.outerWidth();
                var self   = this;
                var propPanel = {};

                propPanel[this.options.dock] = (open ? "+=" : "-=") + (amount - this.options.peek);

                $( this.element ).animate( propPanel, this.options.speed, "linear", function() {
                    self._sliding = false;
                    self._trigger( "slide", null, { open: self.options.open, dock: self.options.dock } );
                });

                this._hide_content();

                this._logo_text_hide(!open);

                this.options.open ? this.element.css("overflow-x", "hidden") : this.element.css("overflow", "hidden");
            }
        }
    },

    peek: function ( peek ) {
        if ( peek === undefined ) {
            return this.options.peek;
        }

        this.options.peek = this._valid_integer( peek );
        this._reposition();
    },

    // Задаем скорость открытия/скрытия панели

    speed: function ( speed ) {
        if ( speed === undefined ) {
            return this.options.speed;
        }

        this.options.speed = this._valid_integer( speed );
    },

    // Задаем url для загрузки контента панели

    load_content_url: function (url) {
        if (url === undefined) {
            return this.options.load_content_url;
        }

        this.options.load_content_url = url;
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
            this._page_content.css("position", "absolute");
            this._page_content.css("left", "328px");
        }
        else {
            this._page_content.css("width", "calc(100% - " + this.options.peek + "px)");
            this._page_content.css("position", "relative");
            this._page_content.css("left", this.options.peek + "px");
        }
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

    _load_content: function () {
        var self = this;

        if (!self._contentLoad && self.options.load_content_url) {
            $.ajax({
                url: self.options.load_content_url,
                type: 'GET',
                data: { leftPanelId: self._panelId },
                success: function (response) {
                    if (response) {
                        self._contentLoad = true;
                        self._panel_content.append(response.Value);

                        var ulList = self._panel_content.find('ul[data-loaditemsurl]');

                        if (ulList.length > 0) {
                            ulList.each(function () {
                                self._load_list_items(this);
                            });
                        }
                    }
                },
                error: function (request) {
                    self._log('ошибка загрузки контента, url: ' + self.options.load_content_url + ' ' + request.status + ' ' + request.statusText, true);
                }
            });
        }
    },

    _load_list_items: function (item) {
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
                                $item.append('<li ' + (key + 1 == len ? 'style="border: none" ' : '') + 'data-id="' + value.id + '" title="' + value.name + '"><a href="#">' + value.name + '</a></li>');
                            });
                        }
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

    _hide_content: function () {
        if (this.options.open)
            this._panel_content.show(500);
        else
            this._panel_content.hide(500);
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

        this.element.css ( "width", "" );
        this.element.css ( "height", "" );

        this.element.css ( "left", "" );
        this.element.css ( "top", "" );
        this.element.css ( "right", "" );
        this.element.css ( "bottom", "" );
        this.element.css ( "opacity", this.options.opacity );

        this._grab_handle.css ( "left", "" );
        this._grab_handle.css ( "top", "" );
        this._grab_handle.css ( "right", "" );
        this._grab_handle.css ( "bottom", "" );
        this._grab_handle.css ( "opacity", this.options.opacity );

        // Задаем ширину

        this.element.css ( "width", this.options.width );
        this.element.css( "top", "0" );
        this.element.css( "bottom", "0" );

        var size_main = this.element.outerWidth();

        // задаем состояние открыта / скрыта
        this.element.css( this.options.dock, this.options.open ? 0 : -1 * ( size_main - this.options.peek ) );
    },

    _valid_boolean: function ( boolean ) {
        if ( jQuery.type( boolean ) === "string" ) {

            if ( $.inArray( boolean.trim().toLowerCase(), ["0", "false", "no"] ) !== -1 ) {
                boolean = false;
            }
        }

        return boolean ? true : false;
    },

    _valid_integer: function ( integer ) {

        return ( isNaN( parseInt( integer ))) ? 0 : integer;
    },

    _valid_float: function ( float ) {

        return ( isNaN( parseFloat( float ))) ? 0 : float;
    },

    _log: function(msg, warn) {
        if(window.console) {
            var message = 'leftSlidePanel -> ' + msg;
            if (warn) {
                console.warn(message);
            } else {
                console.log(message);
            }
        }
    }
});
