
function showSubMenu(element) {
    $(element).parent().parent().find('.dropdown-usual-menu').slideToggle(200);
}

function showMainMenu(event) {
    event.preventDefault();
    var element = event.target;
    var t = $(document).width() - ($(element).parent().parent().offset().left + $(element).parent().parent().width()) - 80;
    $(element).parent().parent().find('.dropdown-main-menu').css('margin-right', t + 'px');
    
    $(element).parent().parent().find('.dropdown-main-menu').slideToggle(300);
}

$(window).click(function (event) {
    if (!event.target.matches('.main-menu-icon')) {
        $('.top-panel .main-menu .dropdown-main-menu').hide();
    }
    if (!event.target.matches('.usual-menu-icon')) {
        $('.top-panel .usual-menu .dropdown-usual-menu').hide();
    }
});

$('#top-head-menu').ready(function () {
    setTimeout(function () {
        $('.usual-menus>ul>li>span').each(function (index, element) {
            $(element).removeClass();
            $(element).addClass('icon usual-menu-icon');
            $(element).addClass($(element).attr('iconClass'));
            $(element).wrap("<a href='#'></a>");  
        });

        $('.usual-menus>ul>li>a').each(function (index, element) {
            $(element).find('span.k-icon').remove();
        });

        $('.help-section>ul>li>span').each(function (index, element) {
            $(element).removeClass();
            $(element).empty();
            $(element).addClass('icon usual-menu-icon');
            $(element).addClass($(element).attr('iconClass'));
            $(element).wrap("<a href='#'></a>");
        });
    }, 1);
});