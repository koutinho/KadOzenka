function addPrintButton(date, user) {
    var toolbar = $('.static-nav #toolbar').data('kendoToolBar');

    if (toolbar) {
        toolbar.add({
            type: 'button',
            text: '',
            icon: 'print',
            id: 'print',
            click: function () {
                if ($('.panelbar') && $('.panelbar').data('kendoPanelBar') && $('.panelbar').data('kendoPanelBar').expand) {
                    $('.panelbar').data('kendoPanelBar').expand($('li'), false);
                }

                var printWindow = window.open('', 'PRINT');

                printWindow.document.write($('html').html());
                printWindow.document.close();

                setTimeout(function () {
                    var waitForEl = function (selector, callback) {
                        if ($(printWindow.document).find(selector).length) {
                            callback();
                        } else {
                            setTimeout(function () {

                                waitForEl(selector, callback);
                            }, 100);
                        }
                    };

                    waitForEl('body', function () {
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

                        var getTextBoxWidthEl = $('<div></div>');
                        getTextBoxWidthEl.attr('id', 'getTextBoxWidthEl');
                        getTextBoxWidthEl.css('width', 'auto');
                        getTextBoxWidthEl.css('display', 'inline - block');
                        getTextBoxWidthEl.css('visibility', 'hidden');
                        getTextBoxWidthEl.css('position', 'fixed');
                        getTextBoxWidthEl.css('overflow', 'auto');

                        getTextBoxWidthEl.prependTo($(printWindow.document).find('body'));

                        $(printWindow.document).find('input.k-textbox').each(function (i, item) {

                            $(printWindow.document).find('#getTextBoxWidthEl').text($(item).val());

                            if ($(printWindow.document).find('#getTextBoxWidthEl').width() + 10 >= $(item).width()) {
                                var attributes = $(item).prop("attributes");
                                var value = $(item).val();
                                var newTextArea = $('<textarea>').append($(item).html());
                                newTextArea.text(value);

                                $.each(attributes, function () {
                                    newTextArea.attr(this.name, this.value);
                                });
                                $(newTextArea).css('padding-left', '.8em');
                                $(newTextArea).css('padding-right', '.8em');

                                $(item).replaceWith($(newTextArea));
                            }
                        });
                        $(printWindow.document).find('textarea').each(function (i, item) {

                            $(item).autoResize({
                                extraSpace: 5
                            });
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
                        var subTitleEl = $('<div></div>');
                        subTitleEl.html('Распечатано: ' + date + '<br/>' + user);
                        subTitleEl.css('padding', '0px 10px 5px 10px');

                        titleEl.after(subTitleEl);

                        /* Отобразить окно с отчетом */
                        //printWindow.addEventListener('afterprint', function () { printWindow.close(); });

                        //printWindow.print(); //in chrome it blocks parent window

                    });

                }, 200);
            }
        });
    }
}

/*
 * jQuery autoResize (textarea auto-resizer)
 * @copyright James Padolsey http://james.padolsey.com
 * @version 1.04
 */

(function ($) {

    $.fn.autoResize = function (options) {

        // Just some abstracted details,
        // to make plugin users happy:
        var settings = $.extend({
            onResize: function () { },
            animate: true,
            animateDuration: 150,
            animateCallback: function () { },
            extraSpace: 20,
            limit: 1000,
            resizeImmediately: true
        }, options);

        // Only textarea's auto-resize:
        this.filter('textarea').each(function () {

            // Get rid of scrollbars and disable WebKit resizing:
            var textarea = $(this).css({ resize: 'none', 'overflow-y': 'hidden' }),

                // Cache original height, for use later:
                origHeight = textarea.height(),

                // Need clone of textarea, hidden off screen:
                clone = (function () {

                    // Properties which may effect space taken up by chracters:
                    var props = ['height', 'width', 'lineHeight', 'textDecoration', 'letterSpacing'],
                        propOb = {};

                    // Create object of styles to apply:
                    $.each(props, function (i, prop) {
                        propOb[prop] = textarea.css(prop);
                    });

                    // Clone the actual textarea removing unique properties
                    // and insert before original textarea:
                    return textarea.clone().removeAttr('id').removeAttr('name').css({
                        position: 'absolute',
                        top: 0,
                        left: -9999
                    }).css(propOb).attr('tabIndex', '-1').insertBefore(textarea);

                })();

            clone.css({ 'width': '1100px' }); //Pdf width

            var lastScrollTop = null,
                updateSize = function (forceResize) {

                    // Prepare the clone:
                    clone.height(0).val($(this).val()).scrollTop(10000);

                    // Find the height of text:
                    var scrollTop = Math.max(clone.scrollTop(), origHeight) + settings.extraSpace,
                        toChange = $(this).add(clone);
                    scrollTop = Math.min(scrollTop, settings.limit);

                    // Don't do anything if scrollTip hasen't changed:
                    if (lastScrollTop === scrollTop) { return; }
                    lastScrollTop = scrollTop;

                    // Check for limit:
                    if (scrollTop == settings.limit) {
                        $(this).css('overflow-y', '');
                        if (!forceResize) {
                            return;
                        }
                    }
                    // Fire off callback:
                    settings.onResize.call(this);

                    // Either animate or directly apply height:
                    settings.animate && textarea.css('display') === 'block' ?
                        toChange.stop().animate({ height: scrollTop }, settings.animateDuration, settings.animateCallback)
                        : toChange.height(scrollTop);
                };
            if (settings.resizeImmediately) {
                updateSize.call(this, true);
            }

            // Bind namespaced handlers to appropriate events:
            textarea
                .unbind('.dynSiz')
                .bind('keyup.dynSiz', updateSize)
                .bind('keydown.dynSiz', updateSize)
                .bind('change.dynSiz', updateSize);

        });

        // Chain:
        return this;

    };



})(jQuery);